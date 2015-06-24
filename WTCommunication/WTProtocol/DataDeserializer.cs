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

        /// <summary>
        /// Implemented by derived classes that provide specific deserialization routines. Interprets the
        /// Deserializer's bytestream as message or parts of messages.
        /// </summary>
        /// <param name="deserializedMessage">The resulting deserialized message. Passed from the caller</param>
        public abstract void Deserialize(ref MessageBase deserializedMessage);

        public DataDeserializer(byte[] inputStream)
        {
            currentInputStream = inputStream;
        }

        /// <summary>
        /// <summary>
        /// Reads the next byte from the input stream and returns the value as unsigned 8 bit integer.
        /// </summary>
        /// <returns>Value as 8 bit unsigned integer</returns>
        protected byte ReadByte()
        {
            byte value = currentInputStream[byteIndex];
            byteIndex++;
            return value;
        }

        /// <summary>
        /// Reads the next two bytes from the input stream and returns the value as 16 bit unsigned integer
        /// </summary>
        /// <returns>Value as 16 bit unsigned integer</returns>
        protected UInt16 ReadUInt16()
        {
            UInt16 result = BitConverter.ToUInt16(currentInputStream, byteIndex);
            Int16 test = BitConverter.ToInt16(currentInputStream, byteIndex);
            byteIndex += 2;
            return result;
        }


        /// <summary>
        /// Reads a string from the input stream. For that, it first reads a single byte that encodes the
        /// total string length. After that, it reads the next n bytes from the input stream and returns them
        /// as UTF8 encoded string.
        /// </summary>
        /// <returns>n bytes as UTF8 encoded string</returns>
        protected string ReadString()
        {
            byte byteLength = ReadByte();
            return ReadString(byteLength);
        }

        /// <summary>
        /// Reads the next <paramref name="length"/> bytes from the input stream and returns them as UTF8
        /// encoded string
        /// </summary>
        /// <param name="length">Number of bytes that should be read from the input stream</param>
        /// <returns>Next <paramref name="length"/> bytes as UTF8 encoded string</returns>
        protected string ReadString(UInt16 length)
        {
            byte[] byteValue = new byte[length];
            Array.Copy(currentInputStream, byteIndex, byteValue, 0, length);
            string result = System.Text.Encoding.UTF8.GetString(byteValue);
            byteIndex += length;
            return result;
        }

        /// <summary>
        /// Reads a variable length encoded integer value. Depending on how many bytes the value needs for
        /// encoding, it reads the next 1, 2 or 4 bytes from the stream and returns them as unsigned integer
        /// </summary>
        /// <returns>Value as 8, 16 or 32 bit integer</returns>
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
