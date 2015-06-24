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
            byte[] componentBytes = GetRemainingBytes();
            Dictionary<string, object> entityInfo = new ComponentDeserializer(componentBytes).Deserialize();

            // This is an attempt to convert the Tundra int IDs to the FiVES GUID format
            entityInfo["guid"] = new Guid(entityID, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            entityInfo["owner"] = new Guid();
            deserializedMessage.Parameters.Add(entityInfo);
        }
    }
}
