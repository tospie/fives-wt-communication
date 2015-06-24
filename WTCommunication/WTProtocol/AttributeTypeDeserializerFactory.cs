using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTProtocol.AttributeTypes;

namespace WTProtocol
{
    public static class AttributeTypeDeserializerFactory
    {
        public static AttributeTypeDeserializer GetDeserializer(string name, byte[] stream, ref int byteIndex)
        {
            switch (name)
            {
                case "bool": return new Bool(stream, ref byteIndex);
                case "real": return new Real(stream, ref byteIndex);
                case "transform": return new Transform(stream, ref byteIndex);
                case "assetReference": return new AssetReference(stream, ref byteIndex);
                case "assetReferenceList": return new AssetReferenceList(stream, ref byteIndex);
                default: throw new
                    NotImplementedException("Deserializer for Attribute Type " + name + " is not implemented");
            }
        }
    }
}
