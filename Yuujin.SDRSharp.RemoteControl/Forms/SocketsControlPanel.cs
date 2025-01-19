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
using static System.Net.Mime.MediaTypeNames;

namespace Yuujin.SDRSharp.RemoteControl.Forms
{
    public partial class SocketsControlPanel : UserControl
    {
        private ISharpControl _control;
        private string? _statusText { get; set; }
        private int _statusCount { get; set; } = 0;

        public SocketsControlPanel(ISharpControl control)
        {
            _control = control;
            InitializeComponent();
        }

        private void SetErrorStatus(string text)
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

            statusLabel.ForeColor = Color.Red;
            statusLabel.Text = $"Status: {text} {(_statusCount == 0 ? "" : $"{_statusCount}")}";

            //JsonSerializer.Deserialize();
        }

        private void SetGoodStatus(string text)
        {

        }

        private void SetNeutralStatus(string text)
        {

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

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            var text = textBox1.Text;

            if(!ValidateIpAddress(text))
            {
                e.Cancel = true;
                SetErrorStatus($"Provided IP address is not valid");
                return;
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
        }

        
    }
}
