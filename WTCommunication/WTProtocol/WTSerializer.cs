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
            AddValue(success);
            AddValue((string)reply["ConnectionID"]);
            AddValue((UInt16)0);
            AddValue("");
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
