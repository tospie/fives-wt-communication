using KIARA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTProtocol
{
    /// <summary>
    /// Implements parts of the Tundra protocol as defined at
    /// https://github.com/realXtend/tundra/wiki/Tundra-protocol#attribute-types-and-their-binary-encodings .
    /// as SINFONI protocol module.
    /// </summary>
    public class WTProtocol : IProtocol
    {
        /// <summary>
        /// Deserializes an incoming byte stream to a SINFONI message object.
        /// </summary>
        /// <param name="message">Incoming byte stream</param>
        /// <returns>SINFONI message object that contains incoming data</returns>
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
