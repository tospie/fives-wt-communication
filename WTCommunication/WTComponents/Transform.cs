using FIVES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTComponentsPlugin
{
    /// <summary>
    /// Transformation of an Entity that consists of position in 3D Space, Rotation and scale
    /// </summary>
    public struct Transform
    {
        public Vector pos; // Position in 3D space as 3D Vector (x, y, z)
        public Vector rot; // rotation of this transform in degrees, using the Euler ZYX convention.
        public Vector scale; // scale of the Entity
    }
}
