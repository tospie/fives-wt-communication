// This file is part of FiVES.
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
using WTProtocol.AttributeTypeSerializers;

namespace WTProtocol
{
    /// <summary>
    /// Used to return a serializer object w.r.t. a specific attribute type
    /// </summary>
    public static class AttributeTypeSerializerFactory
    {
        /// <summary>
        /// Returns a serializer implementation specific to a Tundra attribute type
        /// </summary>
        /// <param name="typeName">Attribute type name</param>
        /// <returns>Serializer for the respective Attribute type</returns>
        public static AttributeTypeSerializer GetTypeSerializer(string typeName)
        {
            switch(typeName)
            {
                // Entity Refernence and Asset Reference use same representation
                case "entityReference":
                case "assetReference": return new AssetReferenceSerializer();
                case "assetReferenceList": return new AssetReferenceListSerializer();
                case "bool": return new BoolSerializer();
                case "real": return new RealSerializer();
                case "transform": return new TransformSerializer();
                case "string": return new StringSerializer();
                case "int": return new IntSerializer();
                default: throw new
                     NotImplementedException("Serializer for Attribute Type " + typeName + " is not implemented");
            }
        }
    }
}
