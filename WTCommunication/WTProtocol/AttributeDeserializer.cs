using FIVES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTProtocol.Components;

namespace WTProtocol
{
    public class AttributeDeserializer : DataDeserializer
    {
        public AttributeDeserializer(byte[] inputStream) : base(inputStream) { }

        public Dictionary<string, object> DeserializeAttributes(string componentName)
        {
            Dictionary<string, object> attributes = new Dictionary<string, object>();
            TundraComponent component = ComponentMap.Components.Single(c => c.Name == componentName);
            List<TundraAttribute> componentAttributes = component.Attributes;

            for (int i = 0; i < attributes.Count; i++)
            {
                string attributeTypeName = componentAttributes[i].Name;
                var attributeValue =
                    AttributeTypeDeserializerFactory
                    .GetDeserializer(attributeTypeName, currentInputStream, byteIndex)
                    .Deserialize(ref byteIndex);
            }
            return attributes;
        }
    }
}
