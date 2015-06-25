using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol
{
    /// <summary>
    /// Deserializes set of attributes of a component. It receives the Attribute data block of the incoming byte stream
    /// as input and interprets the block according to the attribute types that are specified for the specific
    /// component
    /// </summary>
    public class AttributeDeserializer : DataDeserializer
    {
        public AttributeDeserializer(byte[] inputStream) : base(inputStream) { }

        /// <summary>
        /// Deserializes a set of attributes for a specific component.
        /// </summary>
        /// <param name="componentName">Name of the component that is currently deserialized</param>
        /// <returns>Map of attribute names to attribute values</returns>
        public Dictionary<string, object> DeserializeAttributes(string componentName)
        {
            Dictionary<string, object> attributes = new Dictionary<string, object>();
            TundraComponent component = TundraComponentMap.Instance.Components.Single(c => c.Name == componentName);

            List<TundraAttribute> componentAttributes = component.Attributes;

            for (int i = 0; i < componentAttributes.Count; i++)
            {
                string attributeName = componentAttributes[i].Name;
                string attributeTypeName = componentAttributes[i].Type.Name;
                var attributeValue =
                    AttributeTypeDeserializerFactory
                    .GetDeserializer(attributeTypeName, currentInputStream, byteIndex)
                    .Deserialize(ref byteIndex);
                attributes.Add(attributeName, attributeValue);
            }
            return attributes;
        }
    }
}
