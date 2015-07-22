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
using WTComponentsPlugin;
using FIVES;

namespace WTProtocol.AttributeTypeSerializers
{
    class TransformSerializer : AttributeTypeSerializer
    {
        public override byte[] Serialize(object value)
        {
            Transform transform = (Transform)value;

            AddValue((float)transform.pos.x);
            AddValue((float)transform.pos.y);
            AddValue((float)transform.pos.z);
            AddValue((float)transform.rot.x);
            AddValue((float)transform.rot.y);
            AddValue((float)transform.rot.z);
            AddValue((float)transform.scale.x);
            AddValue((float)transform.scale.y);
            AddValue((float)transform.scale.z);

            return dataView.ToArray();
        }
    }
}
