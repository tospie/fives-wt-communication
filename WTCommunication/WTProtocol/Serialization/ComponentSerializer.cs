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
    public class ComponentSerializer : DataSerializer
    {
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
