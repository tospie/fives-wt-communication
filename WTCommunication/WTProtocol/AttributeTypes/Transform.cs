using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol.AttributeTypes
{
    public class Transform : AttributeTypeDeserializer
    {
        public Transform(byte[] inputStream, ref int byteIndex) : base(inputStream, ref byteIndex) { }

        public override object Deserialize()
        {
            Dictionary<string, object> transform = new Dictionary<string,object>();
            transform["pos"] = deserializeVec3();
            transform["rot"] = deserializeVec3();
            transform["scale"] = deserializeVec3();
            return transform;
        }

        private Dictionary<string, object> deserializeVec3()
        {
            Dictionary<string, object> Vector = new Dictionary<string, object>();
            Vector["x"] = ReadFloat();
            Vector["y"] = ReadFloat();
            Vector["z"] = ReadFloat();
            return Vector;
        }

        public override void Deserialize(ref KIARA.MessageBase deserializedMessage)
        {
            throw new NotImplementedException();
        }
    }
}
