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
