using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol
{
    public struct TundraComponent
    {
        public int ID;
        public string Name;
        public List<string> Attributes;
    }

    public static class ComponentMap
    {
        public static List<TundraComponent> Components = new List<TundraComponent>
        {
            new TundraComponent{ID = 1, Name = "avatar"},
            new TundraComponent{ID = 17, Name = "mesh",
                Attributes = new List<string>{
                    "transform", "assetReference", "assetReference", "assetReferenceList", "real", "bool", "bool"
                }
            },
            new TundraComponent{ID = 20, Name = "placeable"}
        };
    }
}
