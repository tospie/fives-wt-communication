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

using FIVES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTProtocol;

namespace WTCommunicationPlugin
{
    /// <summary>
    /// An attribute update from WebTundra requires the instance of an component within an entity being known. Because
    /// of that, the message will pass the binary encodet attribute updates to the SINFONI service implementation that
    /// then finalizes deserialization within the plugin
    /// </summary>
    public class AttributeUpdateDeserializer : DataDeserializer
    {
        private Entity UpdatedEntity;
        private TundraComponent UpdatedComponent;

        /// <summary>
        /// Constructor. Receives the binary encodet attribute updates and writes the deserialized values into the
        /// entity's actual attribute
        /// </summary>
        /// <param name="inputStream">Binary encoded attribute updates</param>
        /// <param name="updatedComponent">TundraComponent of which attributes were updates</param>
        /// <param name="updatedEntity">Entity of which attributes were updates</param>
        public AttributeUpdateDeserializer(byte[] inputStream, TundraComponent updatedComponent, Entity updatedEntity)
            : base(inputStream)
        {
            UpdatedEntity = updatedEntity;
            UpdatedComponent = updatedComponent;
        }

        /// <summary>
        /// Deserializes the provided binary encoded attribute updates
        /// </summary>
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
