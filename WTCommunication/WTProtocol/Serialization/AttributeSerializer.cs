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
using WTProtocol.AttributeTypeSerializers;

namespace WTProtocol
{
    public class AttributeSerializer : DataSerializer
    {
        public byte[] Serialize(string componentTypeName, Dictionary<string, object> attributes)
        {
            TundraComponent component = TundraComponentMap.Instance.FindComponent(componentTypeName);
            foreach (TundraAttribute a in component.Attributes)
            {
                if (a.Name == "componentID")
                    continue;

                object value = attributes[a.Name];
                AttributeTypeSerializer serializer = AttributeTypeSerializerFactory.GetTypeSerializer(a.Type.Name);
                writer.Write(serializer.Serialize(value));
            }
            return dataView.ToArray();
        }
    }
}
