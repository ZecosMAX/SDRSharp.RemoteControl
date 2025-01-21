using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDRSharp.RemoteControl.Common.Messages
{
    public class WelcomeMessage : NetworkMessage
    {
        public WelcomeMessage() 
        {
            Type = 1;
        }
    }
}
