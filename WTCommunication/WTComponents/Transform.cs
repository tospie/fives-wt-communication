﻿// This file is part of FiVES.
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
