using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIVES;
using System.IO;
using ClientManagerPlugin;

namespace WTCommunicationPlugin
{
    public class WTCommunicationPluginInitalizer : IPluginInitializer
    {
        public List<string> ComponentDependencies
        {
            get { return new List<string>(); }
        }

        public void Initialize()
        {
            LoadIdl();
        }

        public string Name
        {
            get { return "WebTundraCommunication"; }
        }

        public List<string> PluginDependencies
        {
            get
            {
                return new List<string>
                {
                    "WebTundraComponents",  // Needed to load all WT specific components and IDL
                    "KIARAPlugin",          // Needed for tundra communication service definition
                    "ClientManager"         // Needed for service registration and binding
                };
            }
        }

        public void Shutdown()
        {
        }

        private void LoadIdl()
        {
            string idlContents = File.ReadAllText("tundraCommunication.kiara");
            KIARAPlugin.KIARAServerManager.Instance.KiaraServer.AmendIDL(idlContents);
        }

        private void RegisterService()
        {
            ClientManager.Instance.RegisterClientService("tundra", true, new Dictionary<string, Delegate>
            {
                {"editAttributes", (Action<string, string, string, object>)EditAttributes}
            });
        }

        private void EditAttributes(string entityGuid, string componentName, string attributeName, object value)
        {
            Entity entity = World.Instance.FindEntity(entityGuid);
            entity[componentName][attributeName].Suggest(value);
        }
    }
}
