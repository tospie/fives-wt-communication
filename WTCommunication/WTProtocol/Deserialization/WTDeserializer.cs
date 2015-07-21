// This file is part of FiVES.
//
// FiVES is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// FiVES is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with FiVES.  If not, see <http://www.gnu.org/licenses/>.
using SINFONI;
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
                // TODO: Find suitable FiVES wrapper
                /* case 111: deserializedMessage.MethodName = "objectsync.createComponents"; break; */
                case 113: deserializedMessage.MethodName = "tundra.editAttributes"; break;
                case 116: deserializedMessage.MethodName = "objectsync.removeObject"; break;
            }
        }

        /// <summary>
        /// Processes the message body. Passes the data block which contains the message payload to the respective
        /// message type deserializer
        /// </summary>
        private void ProcessBody()
        {
            byte[] bodyBytes = GetRemainingBytes();
            deserializedMessage.Type = MessageType.REQUEST;
            MessageDeserializerFactory
                .GetDeserializerForMessage(currentMessageType, bodyBytes).Deserialize(ref deserializedMessage);
        }
    }
}
