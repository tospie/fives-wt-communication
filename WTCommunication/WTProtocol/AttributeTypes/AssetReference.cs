using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol.AttributeTypes
{
    public class AssetReference : AttributeTypeDeserializer
    {
        public AssetReference(byte[] inputStream, int byteIndex) : base(inputStream, byteIndex) { }
        public AssetReference(byte[] inputStream, ref int byteIndex) : base(inputStream, ref byteIndex) { }

        public override object Deserialize(ref int outIndex)
        public override void Deserialize(ref KIARA.MessageBase deserializedMessage)
        {
            throw new NotImplementedException();
        }

        {
            string result = ReadString();
            outIndex = this.byteIndex;
            return result;
        }
    }
}
