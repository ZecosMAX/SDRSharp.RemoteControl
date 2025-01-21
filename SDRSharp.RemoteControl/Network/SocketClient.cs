using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Yuujin.SDRSharp.RemoteControl.Network
{
    public class SocketClient
    {
        public required Guid Guid { get; set; }
        public required TcpClient Client { get; set; }   
        public required CancellationTokenSource CancellationTokenSource { get; set; }

        public DateTime LastMessage { get; set; }

        public bool IsHandshaked { get; set; }
    }
}
