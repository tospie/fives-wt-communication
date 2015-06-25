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
    /// Used to deserialize information about a component that was received with a messag
    /// </summary>
    public class ComponentDeserializer : DataDeserializer
    {
        public ComponentDeserializer(byte[] inputStream) : base(inputStream) { }

        /// <summary>
        /// Deserializes the received components data block to a component object that contains information about the
        /// component names, attached attributes and current attribute values
        /// </summary>
        /// <returns>A map from component names to components</returns>
        public Dictionary<string, object> Deserialize()
        {
            Dictionary<string, object> components = new Dictionary<string, object>();
            uint numComponents = ReadVLE();
            for (var i = 0; i < numComponents; i++)
            {
                uint componentID = ReadVLE();
                uint componentTypeID = ReadVLE();
                string componentName = ReadString();
                TundraComponent component = TundraComponentMap.Instance.Components.Single(c => c.ID == componentTypeID);
                string componentTypeName = component.Name;
                uint attributeDataBlockSize = ReadVLE();
                byte[] attributeDataBlock = new byte[attributeDataBlockSize];
                Array.Copy(currentInputStream, byteIndex, attributeDataBlock, 0, attributeDataBlockSize);
                Dictionary<string, object> attributes =
                    new AttributeDeserializer(attributeDataBlock).DeserializeAttributes(componentTypeName);
                components.Add(componentTypeName, attributes);
            }
            return components;
        }
    }
}
