using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol
{
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

        public void AddAttribute(string name, int typeId)
        {
            TundraAttributeType attributeType = AttributeMap.Attributes.Single(a => a.ID == typeId);
            Attributes.Add(new TundraAttribute(name, attributeType));
        }

        public void AddAttribute(string name, string typeName)
        {
            TundraAttributeType attributeType = AttributeMap.Attributes.Single(a => a.Name == typeName);
            Attributes.Add(new TundraAttribute(name, attributeType));
        }
    }
}
