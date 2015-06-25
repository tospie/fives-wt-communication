using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTProtocol
{
    /// <summary>
    /// Represents an Attribute as defined in Tundra domain model. Consists of Type and specific instance
    /// name
    /// </summary>
    public class TundraAttribute
    {
        /// <summary>
        /// Name of the attribute instance within the component
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Type of the Attribute
        /// </summary>
        public TundraAttributeType Type { get; private set; }

        public TundraAttribute(string name, TundraAttributeType type)
        {
            Name = name;
            Type = type;
        }
    }

    /// <summary>
    /// Tundra Attribute Type, containing both name and the respective fixed type ID
    /// </summary>
    public struct TundraAttributeType
    {
        public int ID;
        public string Name;
    }

    /// <summary>
    /// Used to map Attribute Type names to their fixed ID and vice versa
    /// </summary>
    public static class AttributeMap
    {
        public static List<TundraAttributeType> Attributes = new List<TundraAttributeType>
        {
            new TundraAttributeType{ID = 1, Name = "string"},
            new TundraAttributeType{ID = 2, Name = "int"},
            new TundraAttributeType{ID = 3, Name = "real"},
            /*
             * TYPES 4 - 7 NOT ADDED YET
             */
            new TundraAttributeType{ID = 8, Name = "bool"},
            /*
             * TYPES 9 & 10 NOT ADDED YET
             */
            new TundraAttributeType{ID = 11, Name = "assetReference"},
            new TundraAttributeType{ID = 12, Name = "assetReferenceList"},
            new TundraAttributeType{ID = 13, Name = "entityReference"},
            /*
             * TYPES 14 & 15 NOT ADDED YET
             */
            new TundraAttributeType{ID = 16, Name = "transform"}
            /*
             * TYPES 16 NOT ADDED YET
             */
        };
    }
}
