using KIARA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol
{
    public abstract class DataDeserializer
    {
        protected byte[] currentInputStream;
        protected int byteIndex = 0;

        public abstract void Deserialize(ref MessageBase deserializedMessage);

        public DataDeserializer(byte[] inputStream)
        {
            currentInputStream = inputStream;
        }

        protected byte ReadByte()
        {
            byte value = currentInputStream[byteIndex];
            byteIndex++;
            return value;
        }

        protected UInt16 ReadUInt16()
        {
            UInt16 result = BitConverter.ToUInt16(currentInputStream, byteIndex);
            Int16 test = BitConverter.ToInt16(currentInputStream, byteIndex);
            byteIndex += 2;
            return result;
        }

        protected string ReadString()
        {
            byte byteLength = ReadByte();
            return ReadString(byteLength);
        }

        protected string ReadString(UInt16 length)
        {
            byte[] byteValue = new byte[length];
            Array.Copy(currentInputStream, byteIndex, byteValue, 0, length);
            string result = System.Text.Encoding.UTF8.GetString(byteValue);
            byteIndex += length;
            return result;
        }

        protected uint ReadVLE()
        {
            uint low = ReadByte();
            if ((low & 0x80) == 0)
                return low;

            low = low & 0x7f;
            uint med = ReadByte();
            if ((med & 0x80) == 0)
                return low | (med << 7);

            med = med & 0x7f;
            uint high = ReadUInt16();
            return low | (med << 7) | (high << 14);
        }
    }
}
