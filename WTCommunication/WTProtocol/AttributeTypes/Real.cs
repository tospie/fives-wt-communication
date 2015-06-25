using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol.AttributeTypes
{
    public class Real : AttributeTypeDeserializer
    {
        public Real(byte[] inputStream, int byteIndex) : base(inputStream, byteIndex) { }

        public override object Deserialize(ref int outIndex)
        {
            float result = ReadFloat();
            outIndex = this.byteIndex;
            return result;
        }
    }
}
