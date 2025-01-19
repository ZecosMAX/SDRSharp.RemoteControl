using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDRSharp.Common;
using SDRSharp.Radio;
using Yuujin.SDRSharp.RemoteControl.Network;
using Yuujin.SDRSharp.RemoteControl.Plugins;
using static System.Net.Mime.MediaTypeNames;

namespace Yuujin.SDRSharp.RemoteControl.Forms
{
    public partial class SocketsControlPanel : UserControl
    {
        private SocketController? _socketController => _parent.Controller;
        private RawSocketsControlPlugin _parent;
        private ISharpControl _control;

        private string? _statusText { get; set; }
        private int _statusCount { get; set; } = 0;

        public SocketsControlPanel(ISharpControl control, RawSocketsControlPlugin parentPlugin)
        {
            _control = control;
            _parent = parentPlugin;

            InitializeComponent();
        }

        private void SetErrorStatus(string text)
        {
            statusLabel.ForeColor = Color.Red;
            SetStatus(text);
        }

        private void SetGoodStatus(string text)
        {
            statusLabel.ForeColor = Color.Green;
            SetStatus(text);
        }

        private void SetNeutralStatus(string text)
        {
            statusLabel.ForeColor = SystemColors.ControlText;
            SetStatus(text);
        }

        private void SetStatus(string text)
        {
            if (_statusText != text)
            {
                _statusText = text;
                _statusCount = 0;
            }
            else
            {
                _statusCount++;
            }

            statusLabel.Text = $"Status: {text} {(_statusCount == 0 ? "" : $"{_statusCount}")}";
        }

        private bool ValidateIpAddress(string ipAddress)
        {
            if (!IPEndPoint.TryParse(ipAddress, out var ipEndpoint))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void startServerButton_Click(object sender, EventArgs e)
        {
            var address = textBox1.Text;

            if (!ValidateIpAddress(address))
            {
                SetErrorStatus($"Provided IP address is not valid");
                return;
            }

            if (_socketController == null)
                return;

            _socketController.StartListen(IPEndPoint.Parse(address));
        }
    }
}
