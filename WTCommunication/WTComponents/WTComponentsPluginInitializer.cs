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
                "KIARAPlugin" // For amending KIARA IDL with definition of Transforsm struct
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
            placeable.AddAttribute<Guid>("parentRef");
            placeable.AddAttribute<string>("parentBone");
            ComponentRegistry.Instance.Register(placeable);
        }

        private void RegisterMeshComponent()
        {
            ComponentDefinition mesh = new ComponentDefinition("mesh");
            mesh.AddAttribute<Transform>("nodeTransformation");
            mesh.AddAttribute<string>("meshRef");
            mesh.AddAttribute<string>("skeletonRef");
            mesh.AddAttribute<List<string>>("materialRefs");
            mesh.AddAttribute<float>("drawDistance");
            mesh.AddAttribute<bool>("castShadows");
            mesh.AddAttribute<bool>("useInstancing");
            ComponentRegistry.Instance.Register(mesh);
        }
        #endregion

        private void ReadIDL()
        {
            string idlContent = File.ReadAllText("tundraComponents.kiara");
            KIARAPlugin.KIARAServerManager.Instance.KiaraServer.AmendIDL(idlContent);
        }
    }
}
