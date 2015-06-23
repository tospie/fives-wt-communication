using KIARA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol
{
    public class CreateEntityMessageDeserializer : DataDeserializer
    {
        public CreateEntityMessageDeserializer(byte[] inputStream) : base(inputStream) { }

        public override void Deserialize(ref MessageBase deserializedMessage)
        {
            uint sceneID = ReadVLE(); // Currently not used
            uint entityID = ReadVLE();
            byte temporary = ReadByte(); // Currently not used in FiVES
        }
    }
}
