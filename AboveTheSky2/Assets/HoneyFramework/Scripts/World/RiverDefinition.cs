using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HoneyFramework
{
    /*
     *  Definition of the river related data, may be used by gameplay and other systems which needs to know details about river
     */
    public class RiverDefinition
    {
        static public List<RiverDefinition> definitions = new List<RiverDefinition>();

        public string name = "RiverName";
        public List<Vector3i> riverArea;
        public List<RiverData> definitionPath;
        public RiverData processedPath;

    }
}