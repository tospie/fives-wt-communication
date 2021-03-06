﻿// This file is part of FiVES.
//
// FiVES is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation (LGPL v3)
//
// FiVES is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
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
        /// <param name="bitIndex">Current bit index of the message deserializer within the stream</param>
        /// <returns>The deserializer of the respective attribute type</returns>
        public static AttributeTypeDeserializer GetDeserializer(string name, byte[] stream, int byteIndex, int bitIndex)
        {
            switch (name)
            {
                case "bool": return new Bool(stream, byteIndex, bitIndex);
                case "real": return new Real(stream, byteIndex, bitIndex);
                case "transform": return new Transform(stream, byteIndex, bitIndex);

                // Asset Reference and Entity Reference use same representation
                case "assetReference":
                case "entityReference": return new AssetReference(stream, byteIndex, bitIndex);
                case "assetReferenceList": return new AssetReferenceList(stream, byteIndex, bitIndex);
                case "int": return new IntDeserializer(stream, byteIndex, bitIndex);
                case "string": return new StringDeserializer(stream, byteIndex, bitIndex);
                default: throw new
                    NotImplementedException("Deserializer for Attribute Type " + name + " is not implemented");
            }
        }
    }
}
