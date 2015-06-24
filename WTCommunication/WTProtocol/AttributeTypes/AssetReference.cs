using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol.AttributeTypes
{
    public class AssetReference : AttributeTypeDeserializer
    {
        public AssetReference(byte[] inputStream, int byteIndex) : base(inputStream, ref byteIndex) { }

        public override void Deserialize(ref KIARA.MessageBase deserializedMessage)
        {
            throw new NotImplementedException();
        }

        public override object Deserialize()
        {
            return ReadString();
        }
    }
}
