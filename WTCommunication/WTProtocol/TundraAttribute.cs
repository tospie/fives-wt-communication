using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol
{
    public struct TundraAttribute
    {
        public int ID;
        public string Name;
    }

    public static class AttributeMap
    {
        public static List<TundraAttribute> Attributes = new List<TundraAttribute>
        {
            new TundraAttribute{ID = 1, Name = "avatar"},
            new TundraAttribute{ID = 17, Name = "mesh"},
            new TundraAttribute{ID = 20, Name = "placeable"}
        };
    }
}
