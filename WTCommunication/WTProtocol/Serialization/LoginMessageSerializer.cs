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
using SINFONI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol
{
    /// <summary>
    /// Used to serialize a login reply message to binary format
    /// </summary>
    public class LoginMessageSerializer : MessageSerializer
    {
        public LoginMessageSerializer(IMessage message) : base(message) { }

        public override byte[] Serialize()
        {
            if (currentMessage.Type == MessageType.REQUEST)
                return new byte[0];
            else
            {
                AddLoginReply();
                return dataView.ToArray();
            }
        }

        private void AddLoginReply()
        {
            Dictionary<string, object> reply = (Dictionary<string, object>)currentMessage.Result;
            byte success = (bool)reply["Success"] ? (byte)1 : (byte)0;
            AddValue((bool)reply["Success"]);
            AddVLEValue((UInt32)reply["ConnectionID"]);
            string replyData = (string)(reply["ReplyData"]);
            AddValue((UInt16)(replyData.Length));
            AddValue(replyData);
        }
    }
}
