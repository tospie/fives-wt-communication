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
using SINFONI;

namespace WTProtocol
{
    class RemoveEntityMessageSerializer : MessageSerializer
    {
        public RemoveEntityMessageSerializer(IMessage message) : base(message) { }

        public override byte[] Serialize()
        {
            AddVLEValue(0); // dummy scene ID, not used yet
            AddVLEValue(getIntIdFromGuid(new Guid((string)currentMessage.Parameters[0])));
            return dataView.ToArray();
        }
    }
}
