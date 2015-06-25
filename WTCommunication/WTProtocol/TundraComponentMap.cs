using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol
{
    /// <summary>
    /// Contains Tundra Components as defined in the spec and allows to query them by either name or ID
    /// </summary>
    public class TundraComponentMap
    {
        public static TundraComponentMap Instance { get { return instance; } }

        public List<TundraComponent> Components { get; private set; }

        private TundraComponentMap()
        {
            Components = new List<TundraComponent>();
            AddMeshComponent();
        }

        private void AddMeshComponent()
        {
            TundraComponent meshComponent = new TundraComponent(17, "mesh");
            meshComponent.AddAttribute("nodeTransformation", "transform");
            meshComponent.AddAttribute("meshRef", "assetReference");
            meshComponent.AddAttribute("skeletonRef", "assetReference");
            meshComponent.AddAttribute("materialRefs", "assetReferenceList");
            meshComponent.AddAttribute("drawDistance", "real");
            meshComponent.AddAttribute("castShadows", "bool");
            meshComponent.AddAttribute("useInstancing", "bool");
            Components.Add(meshComponent);
        }

        private static readonly TundraComponentMap instance = new TundraComponentMap();
    }
}
