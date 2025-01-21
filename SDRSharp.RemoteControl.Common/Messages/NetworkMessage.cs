using System;
using System.Collections.Generic;
using System.IO.Hashing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDRSharp.RemoteControl.Common.Messages
{
    public class NetworkMessage
    {
        public static readonly ushort Header = 0x5A2D; // by accident unicode 0x5A2D = 娭 (Grandma). I'll leave it like that
        public int Id { get; set; } = 0;
        public int Type { get; set; } = 0;
        public int PayloadSize => Payload.Length;
        public byte[] Payload { get; set; } = [];
        public uint CRC32 { get; set; } = 0;

        public virtual void InitializeFromByteArray(byte[] array)
        {
            using var ms = new MemoryStream(array);
            using var br = new BinaryReader(ms);

            var header = br.ReadUInt16();
            var id = br.ReadInt32();
            var type = br.ReadInt32();
            var payloadSize = br.ReadInt32();
            var bytes = br.ReadBytes(payloadSize);
            var crc32 = br.ReadUInt32();

            if (header != 0x5A2D)
                throw new ArgumentException("Received packet is invalid/corrupted");

            if (Crc32.HashToUInt32(array.SkipLast(2).ToArray()) != crc32)
                throw new ArgumentException("Received packet is corrupted. CRC32 does not match");
        }

        public byte[] ConvertToByteArray()
        {
            byte[] bytes = [
                ..BitConverter.GetBytes(Header),
                ..BitConverter.GetBytes(Id),
                ..BitConverter.GetBytes(Type),
                ..BitConverter.GetBytes(PayloadSize),
                ..Payload
            ];

            byte[] crc32 = Crc32.Hash(bytes);

            return [..bytes, ..crc32];
        }

        public bool Verify()
        {
            byte[] bytes = [
                ..BitConverter.GetBytes(Header),
                ..BitConverter.GetBytes(Id),
                ..BitConverter.GetBytes(Type),
                ..BitConverter.GetBytes(PayloadSize),
                ..Payload
            ];

            var crc32 = Crc32.HashToUInt32(bytes);
            return crc32 == CRC32;
        }
    }
}
