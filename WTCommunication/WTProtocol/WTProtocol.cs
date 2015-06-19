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
            return Deserializer.Deserialize((string)message);
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
        private WTDeserializer Deserializer = new WTDeserializer();
    }
}
