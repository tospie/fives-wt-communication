// This file is part of FiVES.
//
// FiVES is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// FiVES is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with FiVES.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol.AttributeTypes
{
    public class AssetReferenceList : AttributeTypeDeserializer
    {

        public AssetReferenceList(byte[] inputStream, int byteIndex, int bitIndex)
            : base(inputStream, byteIndex, bitIndex) { }

        public override object Deserialize(ref int outIndex, ref int outBitIndex)
        {
            List<object> references = new List<object>();
            byte numberOfReferences = ReadByte();
            for (byte n = 0; n < numberOfReferences; n++)
            {
                references.Add(new AssetReference(currentInputStream, this.byteIndex, this.bitIndex));
            }
            outIndex = this.byteIndex;
            outBitIndex = this.bitIndex;
            return references;
        }

    }
}
