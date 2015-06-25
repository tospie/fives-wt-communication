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
    }
}
