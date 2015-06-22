using KIARA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace WTProtocol
{
    internal class WTDeserializer
    {
        byte[] currentInputStream;
        MessageBase deserializedMessage;
        UInt16 currentMessageType;

        int byteIndex = 0;

        public IMessage Deserialize(string stream)
        {
            deserializedMessage = new MessageBase();
            deserializedMessage.Parameters = new List<object>();
            currentInputStream = System.Text.Encoding.UTF8.GetBytes(stream);
            byteIndex = 0;
            ReadMessageID();
            ProcessBody();
            return deserializedMessage;
        }

        private void ReadMessageID()
        {
            UInt16 messageID = ReadUInt16();
            currentMessageType = messageID;
            switch (messageID)
            {
                case 100: deserializedMessage.MethodName = "tundra.login"; break;
            }
        }

        private void ProcessBody()
        {
            switch (currentMessageType)
            {
                case 100: deserializedMessage.Type = MessageType.REQUEST; DeserializeLoginMessage(); break;
            }
        }

        private void DeserializeLoginMessage()
        {
            UInt16 stringLength = ReadUInt16();
            string loginProperties = ReadString(stringLength);
            deserializedMessage.Parameters.Add(loginProperties);
        }
        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private byte ReadByte()
        {
            byte value = currentInputStream[byteIndex];
            byteIndex++;
            return value;
        }

        private UInt16 ReadUInt16()
        {
            UInt16 result = BitConverter.ToUInt16(currentInputStream, byteIndex);
            byteIndex += 2;
            return result;
        }

        private string ReadString(UInt16 length)
        {
            byte[] byteValue = new byte[length];
            Array.Copy(currentInputStream, byteIndex, byteValue, 0, length);
            string result = System.Text.Encoding.UTF8.GetString(byteValue);
            byteIndex += length;
            return result;
        }

        private object ReadVLE()
        {
            int low = ReadByte();
            if ((low & 0x80) == null)
                return low;

            low = low & 0x7f;
            int med = ReadByte();
            if ((med & 0x80) == 0)
                return low | (med << 7);

            med = med & 0x7f;
            var high = ReadUInt16();
            return low | (med << 7) | (high << 14);
        }

    }
}
