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
    public class AttributeDatablockSerializer : DataSerializer
    {
        public byte[] Serialize(TundraComponent component, Dictionary<object,object> attributeUpdates)
        {
            AddBits(1, 0); // Always use individual indexing for simplicity

            // Number of updated attributes. We ignore "componentID" as this is an artificial attribute
            // introduced by FiVES
            AddValue(
                attributeUpdates.ContainsKey("componentID") ?
                (byte)(attributeUpdates.Count - 1)
                : (byte)(attributeUpdates.Count)
            );

            foreach (KeyValuePair<object, object> attributeUpdate in attributeUpdates)
            {
                // componentID is an artificial attribute we introduced and that we don't want to serialize
                if ((string)attributeUpdate.Key != "componentID")
                {
                    AddValue((byte)getIndexOfAttribute(component, (string)attributeUpdate.Key));
                    string attributeTypeName = component.GetTypeOfAttribute((string)attributeUpdate.Key).Name;
                    AddArrayBuffer(AttributeTypeSerializerFactory.GetTypeSerializer(attributeTypeName)
                        .Serialize(attributeUpdate.Value));
                }
            }
            return dataView.ToArray();
        }

        private int getIndexOfAttribute(TundraComponent component, string attributeName)
        {
            TundraAttribute attribute = component.Attributes.Single(a => a.Name == attributeName);
            return component.Attributes.IndexOf(attribute);
        }
    }
}
