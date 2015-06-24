
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol.AttributeTypes
{
    public abstract class AttributeTypeDeserializer : DataDeserializer
    {
        public AttributeTypeDeserializer(byte[] inputStream, ref int byteIndex) : base(inputStream) {
            this.byteIndex = byteIndex;
        }

        public abstract object Deserialize();
    }
}
