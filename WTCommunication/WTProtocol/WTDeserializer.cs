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
                case 110: deserializedMessage.MethodName = "clientsync.receiveNewObjects"; break;
            }
        }

        private void ProcessBody()
        {
            byte[] bodyBytes = GetRemainingBytes();
            switch (currentMessageType)
            {
                case 100: deserializedMessage.Type = MessageType.REQUEST;
                          new LoginMessageDeserializer(bodyBytes).Deserialize(ref deserializedMessage);
                          break;
                case 110: deserializedMessage.Type = MessageType.REQUEST;
                           new CreateEntityMessageDeserializer(bodyBytes).Deserialize(ref deserializedMessage);
                           break;
            }
        }
    }
}
