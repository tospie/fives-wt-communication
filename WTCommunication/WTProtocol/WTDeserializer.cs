using KIARA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace WTProtocol
{
    public class WTDeserializer : DataDeserializer
    {
        MessageBase deserializedMessage;
        UInt16 currentMessageType;

        public WTDeserializer(byte[] inputStream) : base(inputStream) {}

        public override void Deserialize(ref MessageBase deserializedMessage)
        {
            this.deserializedMessage = deserializedMessage;
            ReadMessageID();
            ProcessBody();
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
        {
        }

        }

        {
        }
    }
}
