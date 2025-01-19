﻿using System.Windows.Forms;
using SDRSharp.Common;
using SDRSharp.Radio;
using Yuujin.SDRSharp.RemoteControl.Forms;
using Yuujin.SDRSharp.RemoteControl.Network;

namespace Yuujin.SDRSharp.RemoteControl.Plugins
{
    public class RawSocketsControlPlugin : ISharpPlugin, ICanLazyLoadGui, ISupportStatus, IExtendedNameProvider
    {
        private SocketsControlPanel _gui = null!;
        private ISharpControl _control = null!;

        public string DisplayName => "Raw Sockets";
        public string Category => "Remote Control";
        public string MenuItemName => DisplayName;
        public bool IsActive => Controller != null && Controller.Status.HasFlag(SocketControllerStatus.Active);

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
            _gui ??= new SocketsControlPanel(_control, this);
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
