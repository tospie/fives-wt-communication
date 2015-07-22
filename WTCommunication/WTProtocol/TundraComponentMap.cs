// This file is part of FiVES.
//
// FiVES is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation (LGPL v3)
//
// FiVES is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
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

        /// <summary>
        /// Searches the list of registered Components for a component with the specified type name.
        /// </summary>
        /// <param name="typeName">TypeName of the component that should be returned</param>
        /// <returns>TundraComponent with the respective name</returns>
        public TundraComponent FindComponent(string typeName)
        {
            try
            {
                return Components.Single(c => c.Name == typeName);
            }
            catch (Exception)
            {
                throw new KeyNotFoundException("TundraComponent with Type Name " + typeName + " could not be found");
            }
        }

        /// <summary>
        /// Searches the list of registered Components for a component with the specified type ID.
        /// </summary>
        /// <param name="typeName">Type ID of the component that should be returned</param>
        /// <returns>TundraComponent with the respective ID</returns>
        public TundraComponent FindComponent(uint typeID)
        {
            try
            {
                return Components.Single(c => c.ID == typeID);
            }
            catch (Exception)
            {
                throw new KeyNotFoundException("TundraComponent with Type ID " + typeID + " could not be found");
            }
        }

        private TundraComponentMap()
        {
            Components = new List<TundraComponent>();
            AddMeshComponent();
            AddPlaceableComponent();
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

        private void AddPlaceableComponent()
        {
            TundraComponent meshComponent = new TundraComponent(20, "placeable");
            meshComponent.AddAttribute("transform", "transform");
            meshComponent.AddAttribute("drawDebug", "bool");
            meshComponent.AddAttribute("visible", "bool");
            meshComponent.AddAttribute("selectionLayer", "int");
            meshComponent.AddAttribute("parentRef", "entityReference");
            meshComponent.AddAttribute("parentBone", "string");
            Components.Add(meshComponent);
        }

        private static readonly TundraComponentMap instance = new TundraComponentMap();
    }
}
