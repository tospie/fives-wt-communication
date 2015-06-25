using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol.AttributeTypes
{
    public class Bool : AttributeTypeDeserializer
    {
        public Bool(byte[] inputStream, int byteIndex) : base(inputStream, byteIndex) { }

        public override object Deserialize(ref int outIndex)
        {
            bool result = ReadBool();
            outIndex = this.byteIndex;
            return result;
        }
    }
}
