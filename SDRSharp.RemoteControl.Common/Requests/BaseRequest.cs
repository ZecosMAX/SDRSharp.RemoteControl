using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDRSharp.RemoteControl.Common.Requests
{
    public class BaseRequest
    {
        public string? CommandType { get; set; }
        public object? Payload { get; set; }
    }
}
