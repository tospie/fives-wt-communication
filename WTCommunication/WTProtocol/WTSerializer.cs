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
using KIARA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using WTCommunicationPlugin;

namespace WTProtocol
{
    internal class WTSerializer
    {
        MemoryStream dataView = new MemoryStream();
        IMessage currentMessage;
        BinaryWriter writer;
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        public byte[] SerializeMessage(IMessage message)
        {
            currentMessage = message;
            InitalizeSerialization();
            AddMessageID();
            AddPayload();
            return dataView.ToArray();
        }

        private void InitalizeSerialization()
        {
            dataView = new MemoryStream();
            writer = new BinaryWriter(dataView);
        }

        private void AddMessageID()
        {
            UInt16 messageID = 0;
            switch (currentMessage.MethodName)
            {
                case "tundra.login": messageID = GetLoginMessageType(currentMessage.Type); break;
                case "objectsync.receiveNewObjects": messageID = 110; break;
                case "tundra.editAttributes": messageID = 113; break;
                case "objectSync.removeObject": messageID = 116; break;
                // This is a hack. There is currently only one reply-message type which is the login-reply.
                // We thus blindly assume that any response sent to a Tundra remote end is a login reply.
                default: if (currentMessage.Type == MessageType.RESPONSE) messageID = 101; break;
            }
            AddValue(messageID);
        }

        private void AddPayload()
        {
            switch (currentMessage.MethodName)
            {
                case "tundra.login": break;
                // Same hack as above. Treat every response as login response
                default: if (currentMessage.Type == MessageType.RESPONSE) AddLoginPayload(MessageType.RESPONSE); break;
            }
        }

        private void AddLoginPayload(MessageType type)
        {
            if (type == MessageType.REQUEST)
                return;
            else
                AddLoginReply();
        }

        private void AddLoginReply()
        {
            Dictionary<string, object> reply = (Dictionary<string, object>)currentMessage.Result;
            byte success = (bool)reply["Success"] ? (byte)1 : (byte)0;
            AddValue((byte)success);
            AddVLEValue((UInt32)reply["ConnectionID"]);
            string replyData = (string)(reply["ReplyData"]);
            AddValue((UInt16)(replyData.Length));
            AddValue(replyData);
        }

        private void AddVLEValue(UInt32 value)
        {
            if (value < 0x80)
            {
                AddValue((byte)value);
            }
            else if (value < 0x4000)
            {
                AddValue((byte)(value & 0x7f | 0x80));
                AddValue((byte)(value >> 7));
            }
            else
            {
                AddValue((byte)(value & 0x7f | 0x80));
                AddValue((byte)(((value >> 7) & 0x7f) | 0x80));
                AddValue((UInt16)(value >> 14));
            }
        }

        private void AddValue(byte value)
        {
            using (var ms = new MemoryStream())
            {
                writer.Write(value);
            }
        }

        private void AddValue(UInt16 value)
        {
            using (var ms = new MemoryStream())
            {
                byte[] valueAsByteArray = BitConverter.GetBytes(value);
                writer.Write(valueAsByteArray);
            }
        }

        private void AddValue(string value)
        {
            using (var ms = new MemoryStream())
            {
                byte[] valueAsByteArray = System.Text.Encoding.UTF8.GetBytes(value);
                writer.Write(valueAsByteArray);
            }
        }


        private UInt16 GetLoginMessageType(MessageType type)
        {
            if (type == MessageType.REQUEST)
                return 100;
            else
                return 101;
        }
    }
}
