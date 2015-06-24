﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol
{
    public class AttributeDeserializer : DataDeserializer
    {
        public AttributeDeserializer(byte[] inputStream) : base(inputStream) { }

        public override void Deserialize(ref KIARA.MessageBase deserializedMessage)
        {
            throw new NotImplementedException(
                @"AttributeDeserializer does not implement deserialization to message object.
                Use deserialization to attributes object instead");
        }

        public Dictionary<string, object> DeserializeAttributes()
        {
            Dictionary<string, object> attributes = new Dictionary<string, object>();
            return attributes;
        }
    }
}