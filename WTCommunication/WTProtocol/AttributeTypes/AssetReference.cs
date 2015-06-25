using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol.AttributeTypes
{
    public class AssetReference : AttributeTypeDeserializer
    {
        public AssetReference(byte[] inputStream, int byteIndex) : base(inputStream, byteIndex) { }

        public override object Deserialize(ref int outIndex)
        {
            string result = ReadString();
            outIndex = this.byteIndex;
            return result;
        }
    }
}
