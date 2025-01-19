using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuujin.SDRSharp.RemoteControl.Common.Messages
{
    public class WelcomeMessage : NetworkMessage
    {
        public WelcomeMessage() 
        {
            Type = 0;
        }

        public override void InitializeFromByteArray(byte[] array)
        {
            base.InitializeFromByteArray(array);
        }
    }
}
