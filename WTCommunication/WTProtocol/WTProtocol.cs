using KIARA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTProtocol
{
    public class WTProtocol : IProtocol
    {
        public IMessage DeserializeMessage(object message)
        {
            MessageBase deserializedMessage = new MessageBase();
            deserializedMessage.Parameters = new List<object>();
            new WTDeserializer((byte[])message).Deserialize(ref deserializedMessage);
            return deserializedMessage;
        }

        public string MimeType
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { return "wt-websocket"; }
        }

        public object SerializeMessage(IMessage message)
        {
            return Serializer.SerializeMessage(message);
        }

        private WTSerializer Serializer = new WTSerializer();
    }
}
