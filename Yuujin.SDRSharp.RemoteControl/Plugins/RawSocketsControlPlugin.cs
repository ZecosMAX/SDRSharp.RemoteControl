using System.Windows.Forms;
using SDRSharp.Common;
using SDRSharp.Radio;
using Yuujin.SDRSharp.RemoteControl.Forms;
using Yuujin.SDRSharp.RemoteControl.Network;

namespace Yuujin.SDRSharp.RemoteControl.Plugins
{
    public class RawSocketsControlPlugin : ISharpPlugin, ICanLazyLoadGui, ISupportStatus, IExtendedNameProvider
    {
        private SocketsControlPanel _gui;
        private ISharpControl _control;

        public string DisplayName => "Raw Sockets";
        public string Category => "Remote Control";
        public string MenuItemName => DisplayName;
        public bool IsActive => _gui != null && _gui.Visible;

        public SocketController? Controller { get; set; }

        public UserControl Gui
        {
            get
            {
                LoadGui();
                return _gui;
            }
        }

        public void LoadGui()
        {
            _gui ??= new SocketsControlPanel(_control);
        }

        public void Initialize(ISharpControl control)
        {
            _control = control;
        }

        public void Close()
        {
        }
    }
}
