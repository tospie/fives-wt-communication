using KIARA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace WTProtocol
{
    /// <summary>
    /// Deserializer implementation that takes an incoming bytestream from the network and deserializes it to a SINFONI
    /// Message object
    /// </summary>
    public class WTDeserializer : MessageDeserializer
    {
        MessageBase deserializedMessage;
        UInt16 currentMessageType;

        public WTDeserializer(byte[] inputStream) : base(inputStream) {}

        /// <summary>
        /// Deserializes the Bit Stream to a SINFONI message object. The message is passed as reference from the
        /// SINFONI Protocol object and constructed while the byte stream is interpreted
        /// </summary>
        /// <param name="deserializedMessage">SINFONI Message object that is constructed during deserialization</param>
        public override void Deserialize(ref MessageBase deserializedMessage)
        {
            this.deserializedMessage = deserializedMessage;
            ReadMessageID();
            ProcessBody();
        }

        /// <summary>
        /// Reads the Message ID from the beginning of the bytestream. Message codes, as used by Tundra Protocol, are
        /// mapped to SINFONI service names
        /// </summary>
        private void ReadMessageID()
        {
            UInt16 messageID = ReadUInt16();
            currentMessageType = messageID;
            switch (messageID)
            {
                case 100: deserializedMessage.MethodName = "tundra.login"; break;
                case 110: deserializedMessage.MethodName = "objectsync.receiveNewObjects"; break;
            }
        }

        /// <summary>
        /// Processes the message body. Passes the data block which contains the message payload to the respective
        /// message type deserializer
        /// </summary>
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
