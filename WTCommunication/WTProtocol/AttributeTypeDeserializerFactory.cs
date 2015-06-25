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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTProtocol.AttributeTypes;

namespace WTProtocol
{
    /// <summary>
    /// Used to return an attribute type specific deserializer.
    /// </summary>
    public static class AttributeTypeDeserializerFactory
    {
        /// <summary>
        /// Returns deserializer for specific attribute type. The deserializer will operate on the original input
        /// stream of the currently deserialized message. For this, it receives the complete stream and the
        /// current byteIndex of the calling message deserializer
        /// </summary>
        /// <param name="name">Name of the attribute type that should be deserialized</param>
        /// <param name="stream">Byte stream of the incoming message</param>
        /// <param name="byteIndex">current byte index of the message deserializer within the stream</param>
        /// <returns>The deserializer of the respective attribute type</returns>
        public static AttributeTypeDeserializer GetDeserializer(string name, byte[] stream, int byteIndex)
        {
            switch (name)
            {
                case "bool": return new Bool(stream, byteIndex);
                case "real": return new Real(stream, byteIndex);
                case "transform": return new Transform(stream, byteIndex);
                case "assetReference": return new AssetReference(stream, byteIndex);
                case "assetReferenceList": return new AssetReferenceList(stream, byteIndex);
                default: throw new
                    NotImplementedException("Deserializer for Attribute Type " + name + " is not implemented");
            }
        }
    }
}
