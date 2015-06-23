using KIARA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol
{
    public class LoginMessageDeserializer : DataDeserializer
    {
        public LoginMessageDeserializer(byte[] inputStream) : base(inputStream) { }

        public override void Deserialize(ref MessageBase deserializedMessage)
        {
            UInt16 stringLength = ReadUInt16();
            string loginProperties = ReadString(stringLength);
            deserializedMessage.Parameters.Add(loginProperties);
        }
    }
}
