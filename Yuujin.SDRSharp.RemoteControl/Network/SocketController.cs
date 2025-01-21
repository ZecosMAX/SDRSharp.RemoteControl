using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Yuujin.SDRSharp.RemoteControl.Common.Commands;

namespace Yuujin.SDRSharp.RemoteControl.Network
{
    public class SocketController
    {
        private CancellationTokenSource? _listenerCts;
        private TcpListener? _listener;
        private bool isBlocking = false;

        public event EventHandler OnClientConnected = null!;
        public event EventHandler OnClientDisconnected = null!;
        public event EventHandler OnStatusChanged = null!;

        public List<TcpClient> ConnectedClients { get; set; } = [];

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

        private void UpdateStatus()
        {
            try
            {
                OnStatusChanged?.Invoke(this, EventArgs.Empty);
            }
            catch { }
        }

        private async Task AcceptTask()
        {
            try
            {
                while (_listenerCts?.IsCancellationRequested == false)
                {
                    if(Listener == null)
                        return;

                    var client = await Listener.AcceptTcpClientAsync(_listenerCts.Token);
                    ConnectedClients.Add(client);

                    _ = Task.Run(async () => { await RecieveTask(client); });

                    if (ConnectedClients.Count == 1)
                    {
                        try
                        {
                            OnStatusChanged?.Invoke(this, EventArgs.Empty);
                        } 
                        catch { }
                    }

                    try
                    {  
                        OnClientConnected?.Invoke(this, EventArgs.Empty);
                    }
                    catch { }
                }
            }
            catch { }
        }

        private async Task RecieveTask(TcpClient client)
        {
            try
            {
                using var stream = client.GetStream();
                while (true)
                {
                    var command = await JsonSerializer.DeserializeAsync<BaseCommand>(stream);
                }
            }
            catch { }
            finally
            {
                client.Close();
            }
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
