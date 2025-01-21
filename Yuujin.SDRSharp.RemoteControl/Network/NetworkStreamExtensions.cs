using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Yuujin.SDRSharp.RemoteControl.Network
{
    public static class NetworkStreamExtensions
    {
        public static async Task<T> ReadTypeAsync<T>(this NetworkStream stream, CancellationToken cancellationToken = default, bool bigEndian = false) where T : struct 
        {
            var sizeOfType = Marshal.SizeOf<T>();
            var buffer = new byte[sizeOfType];

            await stream.ReadExactlyAsync(buffer.AsMemory(), cancellationToken);

            object result;
            if      (typeof(T) == typeof(byte))     result = buffer[0];
            else if (typeof(T) == typeof(char))     result = BitConverter.ToChar(buffer);
            else if (typeof(T) == typeof(short))    result = BitConverter.ToInt16(buffer);
            else if (typeof(T) == typeof(ushort))   result = BitConverter.ToUInt16(buffer);
            else if (typeof(T) == typeof(int))      result = BitConverter.ToInt32(buffer);
            else if (typeof(T) == typeof(uint))     result = BitConverter.ToUInt32(buffer);
            else if (typeof(T) == typeof(long))     result = BitConverter.ToInt64(buffer);
            else if (typeof(T) == typeof(ulong))    result = BitConverter.ToUInt64(buffer);
            else if (typeof(T) == typeof(float))    result = BitConverter.ToSingle(buffer);
            else if (typeof(T) == typeof(double))   result = BitConverter.ToDouble(buffer);
            else throw new NotSupportedException("Invalid Type");

            return (T)Convert.ChangeType(result, typeof(T));
        }
    }
}
