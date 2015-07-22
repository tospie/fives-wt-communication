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

namespace WTProtocol
{
    /// <summary>
    /// Used to serialize a component with its attributes to binary representation
    /// </summary>
    public class ComponentSerializer : DataSerializer
    {
        /// <summary>
        /// Takes an object that contains information about an entity that changed its component and serializes
        /// it to binary representation, containing about the component type, local ID, and all its attributes
        /// and attribute values
        /// </summary>
        /// <param name="entity">Entity object that changed some of its components</param>
        /// <returns>Components in binary representation</returns>
        public byte[] Serialize(Dictionary<object, object> entity)
        {
            // Entity Info contains GUID and Owner followed by n components
            int componentCount = entity.Count - 2;
            AddVLEValue((uint)componentCount);
            byte[] attributeDataBlock = new byte[0];

            foreach (string elementName in entity.Keys)
            {
                if (elementName == "guid" || elementName == "owner")
                    continue;

                var component = entity[elementName];
                Dictionary<string, object> attributes = (Dictionary<string, object>)component;

                uint componentID = (uint)attributes["componentID"];
                int componentTypeID = TundraComponentMap.Instance.FindComponent(elementName).ID;

                AddVLEValue(componentID);
                AddVLEValue((uint)componentTypeID);
                AddValue("", 0); // ignoring componentnames for now

                attributeDataBlock =
                    attributeDataBlock.Concat(new AttributeSerializer().Serialize(elementName, attributes)).ToArray();

                var blockLength = attributeDataBlock.Length;
                AddVLEValue((uint)blockLength);
                writer.Write(attributeDataBlock);
            }
            return dataView.ToArray();
        }
    }
}
