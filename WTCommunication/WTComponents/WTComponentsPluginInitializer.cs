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

namespace WTComponentsPlugin
{
    public class WTComponentsPluginInitializer : IPluginInitializer
    {
        public List<string> ComponentDependencies
        {
            get { return new List<string>(); }
        }

        public void Initialize()
        {
            RegisterBaseComponents();
            ReadIDL();
        }

        public string Name
        {
            get { return "WebTundraComponents"; }
        }

        public List<string> PluginDependencies
        {

            get { return new List<string>{
                "SINFONI" // For amending SINFONI IDL with definition of Transforsm struct
            }; }
        }

        public void Shutdown()
        {
        }

        private void RegisterBaseComponents()
        {
            RegisterPlaceableComponent();
            RegisterMeshComponent();
            RegisterAvatarComponent();
        }

        #region Component Definitions
        private void RegisterAvatarComponent()
        {
            ComponentDefinition avatarComponent = new ComponentDefinition("avatar");
            avatarComponent.AddAttribute<string>("appearanceRef");
            ComponentRegistry.Instance.Register(avatarComponent);
        }

        private void RegisterPlaceableComponent()
        {
            ComponentDefinition placeable = new ComponentDefinition("placeable");
            placeable.AddAttribute<Transform>("transform");
            placeable.AddAttribute<bool>("drawDebug");
            placeable.AddAttribute<bool>("visible");
            placeable.AddAttribute<int>("selectionLayer");
            placeable.AddAttribute<string>("parentRef");
            placeable.AddAttribute<string>("parentBone");

            // this is actually a little hack! FiVES does not assign individual ID's to components
            // so we need a place to store the ID that arrives from Tundra and is also expected there
            placeable.AddAttribute<uint>("componentID");
            ComponentRegistry.Instance.Register(placeable);
        }

        private void RegisterMeshComponent()
        {
            ComponentDefinition mesh = new ComponentDefinition("mesh");
            mesh.AddAttribute<Transform>("nodeTransformation");
            mesh.AddAttribute<string>("meshRef");
            mesh.AddAttribute<string>("skeletonRef");
            mesh.AddAttribute<List<object>>("materialRefs");
            mesh.AddAttribute<float>("drawDistance");
            mesh.AddAttribute<bool>("castShadows");
            mesh.AddAttribute<bool>("useInstancing");

            // this is actually a little hack! FiVES does not assign individual ID's to components
            // so we need a place to store the ID that arrives from Tundra and is also expected there
            mesh.AddAttribute<uint>("componentID");
            ComponentRegistry.Instance.Register(mesh);
        }
        #endregion

        private void ReadIDL()
        {
            string idlContent = File.ReadAllText("tundraComponents.kiara");
            SINFONIPlugin.SINFONIServerManager.Instance.SinfoniServer.AmendIDL(idlContent);
        }
    }
}
