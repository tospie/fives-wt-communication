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

namespace WTProtocol
{
    public class EditAttributesMessageDeserializer : MessageDeserializer
    {
        public EditAttributesMessageDeserializer(byte[] inputStream) : base(inputStream) { }

        public override void Deserialize(ref KIARA.MessageBase deserializedMessage)
        {
            uint sceneID = ReadVLE();
            string entityID = ReadGuidAsVLE();
            Dictionary<uint, byte[]> updatedComponents = new Dictionary<uint, byte[]>();
            while (byteIndex < currentInputStream.Length)
            {
                uint componentID = ReadVLE();
                uint attributeDataBlockSize = ReadVLE();
                byte[] attributeData = new byte[attributeDataBlockSize];
                Array.Copy(currentInputStream, byteIndex, attributeData, 0, attributeDataBlockSize);
                updatedComponents.Add(componentID, attributeData);
            }

            deserializedMessage.Parameters.Add(entityID);
            deserializedMessage.Parameters.Add(updatedComponents);
        }
    }
}
