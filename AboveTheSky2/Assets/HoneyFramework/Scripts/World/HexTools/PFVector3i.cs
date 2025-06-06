using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HoneyFramework
{
    /// <summary>
    /// useful vector3i wrapper used for more complicated calculations around it after casting 3di hex position into 2d floating point world plane
    /// </summary>
    public class PFVector3i
    {
        public enum Visibility
        {
            fullyVisible,
            halfVisible,
            invisible
        }

        public Vector3i position;
        public Vector2 worldPosition;
        public bool obstacle;
        public Visibility visibility = Visibility.fullyVisible;

        //extra data used by some algorithms
        public Vector2 positionNormal; //normal from test center
        public float rotation;       //rotation from Vector2.right of the positionNormal
        public int distance;       //distance from test center


        public PFVector3i(Vector3i pos)
        {
            position = pos;
            worldPosition = HexCoordinates.HexToWorld(pos);
        }

        public static explicit operator Vector3i(PFVector3i c)
        {
            return c.position;
        }


    }
}