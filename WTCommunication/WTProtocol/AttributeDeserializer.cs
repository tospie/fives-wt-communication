using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol
{
    public class AttributeDeserializer : DataDeserializer
    {
        public AttributeDeserializer(byte[] inputStream) : base(inputStream) { }

        public Dictionary<string, object> DeserializeAttributes(string componentName)
        {
            Dictionary<string, object> attributes = new Dictionary<string, object>();
            TundraComponent component = ComponentMap.Components.Single(c => c.Name == componentName);
            List<string> attributeTypeNames = component.Attributes;
            for (int i = 0; i < attributeTypeNames.Count; i++)
            {
                string attributeName = attributeTypeNames[i];
                var attributeValue =
                    AttributeTypeDeserializerFactory
                    .GetDeserializer(attributeName, currentInputStream, byteIndex)
                    .Deserialize(ref byteIndex);
            }
            return attributes;
        }
    }
}
