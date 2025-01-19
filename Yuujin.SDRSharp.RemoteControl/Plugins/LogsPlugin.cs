using SDRSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuujin.SDRSharp.RemoteControl.Forms;
using Yuujin.SDRSharp.RemoteControl.Network;

namespace Yuujin.SDRSharp.RemoteControl.Plugins
{
    public class LogsPlugin : ISharpPlugin, ICanLazyLoadGui, ISupportStatus, IExtendedNameProvider
    {
        private RemoteControlLogView _gui = null!;
        private ISharpControl _control = null!;

        public string DisplayName => "Logs Viewer";
        public string Category => "Remote Control";
        public string MenuItemName => DisplayName;
        public bool IsActive => _gui != null && _gui.Visible;

        public SocketController? Controller { get; set; }

        public UserControl Gui
        {
            get
            {
                return _gui;
            }
        }

        public void LoadGui()
        {
            _gui ??= new RemoteControlLogView();
        }

        public void Initialize(ISharpControl control)
        {
            LoadGui();

            _control = control;

            Controller = new SocketController();
        }

        public void Close()
        {
        }
    }
}
