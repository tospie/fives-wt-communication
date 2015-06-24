using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol.AttributeTypes
{
    public class Real : AttributeTypeDeserializer
    {
        public Real(byte[] inputStream, ref int byteIndex) : base(inputStream, ref byteIndex) { }

        public override object Deserialize()
        {
            return ReadFloat();
        }

        public override void Deserialize(ref KIARA.MessageBase deserializedMessage)
        {
            throw new NotImplementedException();
        }
    }
}
