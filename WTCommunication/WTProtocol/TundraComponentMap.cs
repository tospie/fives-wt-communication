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
