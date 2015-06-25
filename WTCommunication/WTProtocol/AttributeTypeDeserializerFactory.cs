using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTProtocol.AttributeTypes;

namespace WTProtocol
{
    public static class AttributeTypeDeserializerFactory
    {
        public static AttributeTypeDeserializer GetDeserializer(string name, byte[] stream, int byteIndex)
        {
            switch (name)
            {
                case "bool": return new Bool(stream, byteIndex);
                case "real": return new Real(stream, byteIndex);
                case "transform": return new Transform(stream, byteIndex);
                case "assetReference": return new AssetReference(stream, byteIndex);
                case "assetReferenceList": return new AssetReferenceList(stream, byteIndex);
                default: throw new
                    NotImplementedException("Deserializer for Attribute Type " + name + " is not implemented");
            }
        }
    }
}
