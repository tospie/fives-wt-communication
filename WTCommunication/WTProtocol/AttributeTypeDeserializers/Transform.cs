// This file is part of FiVES.
//
// FiVES is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation (LGPL v3)
//
// FiVES is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with FiVES.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol.AttributeTypes
{
    public class Transform : AttributeTypeDeserializer
    {
        public Transform(byte[] inputStream, int byteIndex, int bitIndex)
            : base(inputStream, byteIndex, bitIndex) { }

        public override object Deserialize(ref int outIndex, ref int outBitIndex)
        {
            Dictionary<string, object> transform = new Dictionary<string,object>();
            transform["pos"] = deserializeVec3();
            transform["rot"] = deserializeVec3();
            transform["scale"] = deserializeVec3();
            outIndex = this.byteIndex;
            outBitIndex = this.bitIndex;
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
    }
}
