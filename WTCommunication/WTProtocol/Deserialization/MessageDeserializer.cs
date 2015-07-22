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
    public abstract class MessageDeserializer : DataDeserializer
    {
        public MessageDeserializer(byte[] inputStream) : base(inputStream) { }

        /// <summary>
        /// Implemented by derived classes that provide specific deserialization routines. Interprets the
        /// Deserializer's bytestream as message or parts of messages.
        /// </summary>
        /// <param name="deserializedMessage">The resulting deserialized message. Passed from the caller</param>
        public abstract void Deserialize(ref MessageBase deserializedMessage);
    }
}
