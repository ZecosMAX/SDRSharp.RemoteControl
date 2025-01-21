using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDRSharp.RemoteControl.Common.Responses
{
    public class HandshakeVerificationResponse : BaseResponse
    {
        public Guid AssignedClientId { get; set; }
        public int KeepaliveTimeout { get; set; } = 30;
    }
}
