using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SDRSharp.RemoteControl.Common.Responses
{
    public class BaseResponse
    {
        public Guid RequestId { get; set; }
        public Guid ResponseId { get; set; } = Guid.NewGuid();
        public DateTime ResponseDateTime { get; set; } = DateTime.Now;

        public byte[] ConvertToByteArray(Encoding? encoding = null!)
        {
            encoding ??= Encoding.UTF8;

            var jsonString = JsonSerializer.Serialize(this);
            var bytes = encoding.GetBytes(jsonString);

            return bytes;
        }
    }
}
