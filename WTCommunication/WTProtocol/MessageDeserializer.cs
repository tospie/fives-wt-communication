using KIARA;
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
