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
    /// Represents a Tundra Component as defined in the Tundra domain model. Contains fixed ID as defined by the spec,
    /// given Name and a list of Tundra Attributes
    /// </summary>
    public class TundraComponent
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public List<TundraAttribute> Attributes { get; private set; }

        public TundraComponent(int id, string name)
        {
            ID = id;
            Name = name;
            Attributes = new List<TundraAttribute>();
        }

        /// <summary>
        /// Adds an attribute as defined under the type ID
        /// </summary>
        /// <param name="name">Name of the attribute in the component</param>
        /// <param name="typeId">Fixed TypeID as assigned in Tundra Spec</param>
        public void AddAttribute(string name, int typeId)
        {
            TundraAttributeType attributeType = AttributeMap.Attributes.Single(a => a.ID == typeId);
            Attributes.Add(new TundraAttribute(name, attributeType));
        }

        /// <summary>
        /// Adds an attribute as defined under the type name
        /// </summary>
        /// <param name="name">Name of the attribute in the component</param>
        /// <param name="typeId">TypeName as assigned in Tundra Spec</param>
        public void AddAttribute(string name, string typeName)
        {
            TundraAttributeType attributeType = AttributeMap.Attributes.Single(a => a.Name == typeName);
            Attributes.Add(new TundraAttribute(name, attributeType));
        }

        /// <summary>
        /// Returns the Tundra Attribute Type of an attribute of this component
        /// </summary>
        /// <param name="name">Name of the attribute of which type should be returned</param>
        /// <returns>Tundra Attribute Type object of the respective attribute</returns>
        public TundraAttributeType GetTypeOfAttribute(string name)
        {
            return Attributes.Single(a => a.Name == name).Type;
        }
    }
}
