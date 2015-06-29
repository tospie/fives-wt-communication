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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIVES;
using System.IO;
using ClientManagerPlugin;
using KIARA;

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
            RegisterService();
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
                    "KIARA",          // Needed for tundra communication service definition
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
            ClientManager.Instance.RegisterClientService("tundra", false, new Dictionary<string, Delegate>
            {
                {"login", (Func<Connection, string, LoginReply>)Login},
                {"editAttributes", (Action<string, List<ComponentUpdate>>)EditAttributes}
            });
        }

        private LoginReply Login(Connection connection, string loginProperties)
        {
            ClientManager.Instance.ReceiveAuthenticatedClient(connection);
            return new LoginReply {
                Success = true,
                ConnectionID = NumConnectedClients++,
                ReplyData = "[]"
            };
        }

        private void EditAttributes(string entityGuid, int componentId, byte[] attributeData)
        {
            Entity entity = World.Instance.FindEntity(entityGuid);
            entity[componentName][attributeName].Suggest(value);
        }

        private UInt32 NumConnectedClients = 0;
    }
}
