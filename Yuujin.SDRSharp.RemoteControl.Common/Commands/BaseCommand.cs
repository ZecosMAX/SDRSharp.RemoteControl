using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuujin.SDRSharp.RemoteControl.Common.Commands
{
    public class BaseCommand
    {
        public string? CommandType { get; set; }
        public object? Payload { get; set; }
    }
}
