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

using SINFONI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol
{
    /// <summary>
    /// Used to serialize a message for newly created entities to binary format
    /// </summary>
    public class CreateEntityMessageSerializer : MessageSerializer
    {
        public CreateEntityMessageSerializer(IMessage message) : base(message) { }

        public override byte[] Serialize()
        {
            Dictionary<object, object> entity = (Dictionary<object, object>)currentMessage.Parameters[0];
            AddVLEValue((byte)0); // scene ID. At this point, a dummy
            AddVLEValue(getIntIdFromGuid((Guid)entity["guid"]));
            AddValue((byte)0); // Temporary flag. As workaround, currently no entity is temporary

            writer.Write(new ComponentSerializer().Serialize(entity));
            return dataView.ToArray();
        }
    }
}
