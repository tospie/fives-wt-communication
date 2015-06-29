using FIVES;
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
using WTProtocol;

namespace WTCommunicationPlugin
{
    public class AttributeUpdateDeserializer : DataDeserializer
    {
        private Entity UpdatedEntity;
        private TundraComponent UpdatedComponent;

        public AttributeUpdateDeserializer(byte[] inputStream, TundraComponent updatedComponent, Entity updatedEntity)
            : base(inputStream)
        {
            UpdatedEntity = updatedEntity;
            UpdatedComponent = updatedComponent;
        }

        public void DeserializeAttributeUpdate()
        {
            if (ReadBits(1) == 0)
            {
                deserializeWithIndividualIndexing();
            }
            else
            {
                deserializeWithFlagIndexing();
            }
        }

        private void deserializeWithIndividualIndexing()
        {
            byte numberOfChangedAttributes = ReadByte();
            for(int i = 0; i < numberOfChangedAttributes; i++)
            {
                byte attributeIndex = ReadByte();
                updateAttributeFromInput(attributeIndex);
            }
        }

        private void deserializeWithFlagIndexing()
        {
            int attributeCounter = 0;
            while (byteIndex < currentInputStream.Length)
            {
                if (ReadBits(1) == 0)
                {
                    attributeCounter++;
                    continue;
                }

                updateAttributeFromInput(attributeCounter++);
            }
        }

        private void updateAttributeFromInput(int attributeIndex)
        {
            var updatedAttribute = UpdatedComponent.Attributes.ElementAt(attributeIndex);
            var attributeValue = AttributeTypeDeserializerFactory
                .GetDeserializer(updatedAttribute.Type.Name, currentInputStream, byteIndex, bitIndex)
                .Deserialize(ref byteIndex, ref bitIndex);
            UpdatedEntity[UpdatedComponent.Name][updatedAttribute.Name].Suggest(attributeValue);
        }
    }
}
