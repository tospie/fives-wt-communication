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

namespace WTProtocol
{
    /// <summary>
    /// Deserializes set of attributes of a component. It receives the Attribute data block of the incoming byte stream
    /// as input and interprets the block according to the attribute types that are specified for the specific
    /// component
    /// </summary>
    public class AttributeDeserializer : DataDeserializer
    {
        public AttributeDeserializer(byte[] inputStream) : base(inputStream) { }

        /// <summary>
        /// Deserializes a set of attributes for a specific component.
        /// </summary>
        /// <param name="componentName">Name of the component that is currently deserialized</param>
        /// <returns>Map of attribute names to attribute values</returns>
        public Dictionary<string, object> DeserializeAttributes(string componentName)
        {
            Dictionary<string, object> attributes = new Dictionary<string, object>();

            TundraComponent component = TundraComponentMap.Instance.FindComponent(componentName);

            List<TundraAttribute> componentAttributes = component.Attributes;

            for (int i = 0; i < componentAttributes.Count; i++)
            {
                string attributeName = componentAttributes[i].Name;
                string attributeTypeName = componentAttributes[i].Type.Name;
                var attributeValue =
                    AttributeTypeDeserializerFactory
                    .GetDeserializer(attributeTypeName, currentInputStream, byteIndex)
                    .Deserialize(ref byteIndex);
                attributes.Add(attributeName, attributeValue);
            }
            return attributes;
        }
    }
}
