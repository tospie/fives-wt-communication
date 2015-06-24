﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol.AttributeTypes
{
    public class AssetReferenceList : AttributeTypeDeserializer
    {

        public AssetReferenceList(byte[] inputStream, ref int byteIndex) : base(inputStream, ref byteIndex) { }

        public override object Deserialize()
        {
            List<object> references = new List<object>();
            byte numberOfReferences = ReadByte();
            for (byte n = 0; n < numberOfReferences; n++)
            {
                references.Add(new AssetReference(currentInputStream, ref byteIndex));
            }

            return references;
        }

        public override void Deserialize(ref KIARA.MessageBase deserializedMessage)
        {
            throw new NotImplementedException();
        }

    }
}