﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTProtocol.Components;

namespace WTProtocol
{
    public class ComponentDeserializer : DataDeserializer
    {
        public ComponentDeserializer(byte[] inputStream) : base(inputStream) { }

        public Dictionary<string, object> Deserialize()
        {
            Dictionary<string, object> components = new Dictionary<string, object>();
            uint numComponents = ReadVLE();
            for (var i = 0; i < numComponents; i++)
            {
                uint componentID = ReadVLE();
                uint componentTypeID = ReadVLE();
                string componentName = ReadString();
                TundraComponent component = ComponentMap.Components.Single(c => c.ID == componentTypeID);
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
