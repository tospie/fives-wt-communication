using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol
{
    /// <summary>
    /// Used to deserialize information about a component that was received with a messag
    /// </summary>
    public class ComponentDeserializer : DataDeserializer
    {
        public ComponentDeserializer(byte[] inputStream) : base(inputStream) { }

        /// <summary>
        /// Deserializes the received components data block to a component object that contains information about the
        /// component names, attached attributes and current attribute values
        /// </summary>
        /// <returns>A map from component names to components</returns>
        public Dictionary<string, object> Deserialize()
        {
            Dictionary<string, object> components = new Dictionary<string, object>();
            uint numComponents = ReadVLE();
            for (var i = 0; i < numComponents; i++)
            {
                uint componentID = ReadVLE();
                uint componentTypeID = ReadVLE();
                string componentName = ReadString();
                TundraComponent component = TundraComponentMap.Instance.Components.Single(c => c.ID == componentTypeID);
                string componentTypeName = component.Name;
                uint attributeDataBlockSize = ReadVLE();
                byte[] attributeDataBlock = new byte[attributeDataBlockSize];
                Array.Copy(currentInputStream, byteIndex, attributeDataBlock, 0, attributeDataBlockSize);
                Dictionary<string, object> attributes =
                    new AttributeDeserializer(attributeDataBlock).DeserializeAttributes(componentTypeName);
                components.Add(componentTypeName, attributes);
            }
            return components;
        }
    }
}
