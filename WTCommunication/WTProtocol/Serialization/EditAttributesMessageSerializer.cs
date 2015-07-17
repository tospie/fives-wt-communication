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
using FIVES;
using KIARA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol
{
    public class EditAttributesMessageSerializer : MessageSerializer
    {
        public EditAttributesMessageSerializer(IMessage message) : base(message) { }

        private Dictionary<string, object> updateInfo;
        string componentName;
        Dictionary<object, object> attributeUpdates;

        public override byte[] Serialize()
        {
            updateInfo = (Dictionary<string, object>)currentMessage.Parameters[0];
            AddVLEValue(0); // sceneID - not used
            Guid entityGuid = new Guid((string)updateInfo["entityGuid"]);
            AddVLEValue(getIntIdFromGuid(entityGuid));

            Dictionary<object, object> updatedComponents = (Dictionary<object, object>)updateInfo["updatedComponents"];
            foreach (KeyValuePair<object, object> c in updatedComponents)
            {
                addComponentUpdate(c);
            }

            return dataView.ToArray();
        }

        private void addComponentUpdate(KeyValuePair<object, object> componentUpdate)
        {
            componentName = (string)componentUpdate.Key;
            Dictionary<string, object>  attributes = (Dictionary<string, object>)componentUpdate.Value;
            attributeUpdates = (Dictionary<object, object>)attributes["updates"];
            object value = 1;

            AddVLEValue(getComponentIDInEntity());

            byte[] dataBlock = getAttributeDataBlock();
            AddVLEValue((uint)dataBlock.Length);
            AddArrayBuffer(dataBlock);
        }

        private byte[] getAttributeDataBlock()
        {
            byte[] attributeDataBlock = new byte[0];
            TundraComponent component = TundraComponentMap.Instance.FindComponent(componentName);
            attributeDataBlock = new AttributeDatablockSerializer().Serialize(component, attributeUpdates);
            return attributeDataBlock;
        }

        private uint getComponentIDInEntity()
        {
            Entity e = World.Instance.FindEntity(((string)updateInfo["entityGuid"]));
            uint componentId = (uint)e[componentName]["componentID"].Value;
            return componentId;
        }
    }
}
