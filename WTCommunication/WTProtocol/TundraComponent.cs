using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol
{
    public class TundraComponent
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public List<TundraAttribute> Attributes { get; private set; }

        public TundraComponent(int id, string name)
        {
            ID = id;
            Name = name;
            Attributes = new List<TundraAttribute>();
        }

        public void AddAttribute(string name, int typeId)
        {
            TundraAttributeType attributeType = AttributeMap.Attributes.Single(a => a.ID == typeId);
            Attributes.Add(new TundraAttribute(name, attributeType));
        }

        public void AddAttribute(string name, string typeName)
        {
            TundraAttributeType attributeType = AttributeMap.Attributes.Single(a => a.Name == typeName);
            Attributes.Add(new TundraAttribute(name, attributeType));
        }
    }

    public class ComponentMap
    {
        public static ComponentMap Instance {
            get
            {
                return instance;
            }
        }

        private ComponentMap()
        {
            AddMeshComponent();
        }

        public List<TundraComponent> Components = new List<TundraComponent>
        {
            new TundraComponent(1, "avatar"),
            new TundraComponent(20, "placeable")
        };

        private void AddMeshComponent()
        {
            TundraComponent meshComponent = new TundraComponent(17, "mesh");
            meshComponent.AddAttribute("nodeTransformation", "transform");
            meshComponent.AddAttribute("meshRef", "assetReference");
            meshComponent.AddAttribute("skeletonRef", "skeletonReference");
            meshComponent.AddAttribute("meshMaterial", "assetReferenceList");
            meshComponent.AddAttribute("drawDistance", "real");
            meshComponent.AddAttribute("castShadows", "bool");
            meshComponent.AddAttribute("useInstancing", "bool");
            Components.Add(meshComponent);
        }

        private static ComponentMap instance = new ComponentMap();
    }
}
