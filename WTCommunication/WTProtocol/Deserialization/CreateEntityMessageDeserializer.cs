﻿// This file is part of FiVES.
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
    public class CreateEntityMessageDeserializer : MessageDeserializer
    {
        public CreateEntityMessageDeserializer(byte[] inputStream) : base(inputStream) { }

        public override void Deserialize(ref MessageBase deserializedMessage)
        {
            uint sceneID = ReadVLE(); // Currently not used
            string entityID = ReadGuidAsVLE();
            byte temporary = ReadByte(); // Currently not used in FiVES
            byte[] componentBytes = GetRemainingBytes();
            Dictionary<string, object> entityInfo = new ComponentDeserializer(componentBytes).Deserialize();

            // This is an attempt to convert the Tundra int IDs to the FiVES GUID format
            entityInfo["guid"] = entityID;
            entityInfo["owner"] = new Guid().ToString();
            deserializedMessage.Parameters.Add(entityInfo);
        }
    }
}
