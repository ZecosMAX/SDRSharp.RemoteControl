using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SDRSharp.RemoteControl.Extensions;
using SDRSharp.RemoteControl.Common.Messages;
using SDRSharp.RemoteControl.Common.Responses;

namespace SDRSharp.RemoteControl.Network
{
    public class SocketController
    {
        private CancellationTokenSource? _listenerCts;
        private TcpListener? _listener;
        private bool isBlocking = false;

        public event EventHandler OnClientConnected = null!;
        public event EventHandler OnClientDisconnected = null!;
        public event EventHandler OnStatusChanged = null!;

        public ConcurrentDictionary<Guid, SocketClient> ConnectedClients { get; set; } = [];

        public bool IsBlocking { get => isBlocking; set { UpdateStatus(); isBlocking = value; } }
        public TcpListener? Listener { get => _listener; set { if (_listener != value) { var oldListener = _listener; _listener = value; OnListenerUpdated(oldListener, value); } } }
        public SocketControllerStatus Status 
        { 
            get
            {
                var status = SocketControllerStatus.Unknown;

                if (Listener != null || ConnectedClients.Count > 0)
                {
                    status |= SocketControllerStatus.Active;
                }
                else
                { 
                    status |= SocketControllerStatus.Stopped;
                }

                if (Listener != null && !IsBlocking)
                {
                    status |= SocketControllerStatus.Listening;
                }
                else
                {
                    status |= SocketControllerStatus.Blocking;
                }

                return status;
            } 
        }

        public void StartListen(IPEndPoint listenEndpoint)
        {
            Listener = new TcpListener(listenEndpoint);
        }

        private void OnListenerUpdated(TcpListener? oldListener, TcpListener? newListener)
        {
            if (oldListener != null)
            {
                _listenerCts?.Cancel();
                _listenerCts?.Dispose();

                oldListener.Stop();
                oldListener.Dispose();
            }

            if (newListener != null)
            {
                _listenerCts = new CancellationTokenSource();

                newListener.Start();
                _ = Task.Run(AcceptTask);
            }

            UpdateStatus();
        }

        private void UpdateStatus() => OnStatusChanged?.SafeInvoke(this, EventArgs.Empty);

        private async Task AcceptTask()
        {
            try
            {
                while (_listenerCts?.IsCancellationRequested == false)
                {
                    if(Listener == null)
                        return;

                    var client = await Listener.AcceptTcpClientAsync(_listenerCts.Token);

                    var guid = Guid.NewGuid();
                    var socketClient = new SocketClient()
                    { 
                        Client = client,
                        Guid = guid,
                        CancellationTokenSource = new CancellationTokenSource(),
                        LastMessage = DateTime.Now, 
                    };

                    _ = Task.Run(async () => { await RecieveTask(socketClient); });
                }
            }
            catch { }
        }

        private async Task RecieveTask(SocketClient client)
        {
            try
            {
                using var stream = client.Client.GetStream();
                while (!client.CancellationTokenSource.IsCancellationRequested)
                {
                    var message = await GetNetworkMessageAsync(stream, client.CancellationTokenSource.Token);
                    
                    if (message == null)
                        continue;

                    var respMessage = HandleMessage(client, message);

                    if (respMessage != null)
                        stream.Write(respMessage.ConvertToByteArray().AsSpan());
                }
            }
            catch { }
            finally
            {
                client.Client.Close();
                if (ConnectedClients.TryRemove(client.Guid, out _))
                {
                    OnClientDisconnected?.SafeInvoke(this, EventArgs.Empty);
                }
            }
        }

        private async Task<NetworkMessage?> GetNetworkMessageAsync(NetworkStream stream, CancellationToken cancellationToken)
        {
            var header = await stream.ReadTypeAsync<ushort>(cancellationToken);

            if (header != NetworkMessage.Header)
                return null;

            var messageId = await stream.ReadTypeAsync<int>(cancellationToken);
            var messageType = await stream.ReadTypeAsync<int>(cancellationToken);
            var payloadSize = await stream.ReadTypeAsync<int>(cancellationToken);
            var payload = new byte[payloadSize]!; await stream.ReadExactlyAsync(payload.AsMemory(), cancellationToken);
            var crc32 = await stream.ReadTypeAsync<uint>(cancellationToken);

            var message = new NetworkMessage()
            {
                Id = messageId,
                Type = messageType,
                Payload = payload,
                CRC32 = crc32,
            };

            if (!message.Verify())
                return null;

            return message;
        }

        private NetworkMessage? HandleMessage(SocketClient client, NetworkMessage message)
        {
            // Handle handshaking the client
            // if the client hasn't performed hanshaking yet
            if (!ConnectedClients.ContainsKey(client.Guid))
            {
                // Expect a welcome message
                // If it is not. Close the connection
                if(message.Type != 1)
                {
                    client.CancellationTokenSource.Cancel();
                    client.Client.Close();
                    return null;
                }

                // Otherwise, accept client and send handshake verification
                ConnectedClients.TryAdd(client.Guid, client);
                OnClientConnected?.SafeInvoke(this, null!);


                var verificationMessage = new WelcomeMessage
                {
                    Payload = new HandshakeVerificationResponse()
                    {
                        AssignedClientId = client.Guid,
                        KeepaliveTimeout = 30,
                        RequestId = Guid.Empty,
                    }.ConvertToByteArray()
                };

                return verificationMessage;
            }

            return null;
        }
    }

    [Flags]
    public enum SocketControllerStatus
    {
        Unknown     = 0,
        Stopped     = 1 << 0,
        Active      = 1 << 1,
        Listening   = 1 << 2,
        Blocking    = 1 << 3,
    }
}
