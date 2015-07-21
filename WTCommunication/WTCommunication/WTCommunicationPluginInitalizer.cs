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
using SINFONI;
using WTProtocol;

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
                    "SINFONI",          // Needed for tundra communication service definition
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
            SINFONIPlugin.SINFONIServerManager.Instance.SinfoniServer.AmendIDL(idlContents);
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

        private void EditAttributes(string entityGuid, List<ComponentUpdate> updatedComponents)
        {
            Entity entity = World.Instance.FindEntity(entityGuid);
            foreach (ComponentUpdate c in updatedComponents)
            {
                TundraComponent updatedComponent = getTundraComponentById(entity, (int)c.componentId);
                deserializeAttributeValues(c.attributeData, updatedComponent, entity);
            }
        }

        private TundraComponent getTundraComponentById(Entity entity, int componentId)
        {
            Component componentWithId = getEntityComponentById(entity, componentId);
            string componentName = componentWithId.Definition.Name;
            return TundraComponentMap.Instance.FindComponent(componentName);
        }

        private Component getEntityComponentById(Entity entity, int componentId)
        {
            foreach (Component c in entity.Components)
            {
                if (c.Definition.ContainsAttributeDefinition("componentID")
                    && (uint)entity[c.Definition.Name]["componentID"].Value == (uint)componentId)
                    return c;
            }

            throw new ComponentAccessException("No Component with Tundra ID " + componentId + " set in entity "
                + entity.Guid);
        }

        private void deserializeAttributeValues(byte[] attributeData, TundraComponent component, Entity entity)
        {
            var d = new AttributeUpdateDeserializer(attributeData, component, entity);
            d.DeserializeAttributeUpdate();
        }

        private UInt32 NumConnectedClients = 0;
    }
}
