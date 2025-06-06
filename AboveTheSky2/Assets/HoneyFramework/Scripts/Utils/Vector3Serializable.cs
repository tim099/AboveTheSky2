using UnityEngine;
using System;
using Pathfinding;

namespace HoneyFramework
{
    [Serializable()]
    public struct Vector3Serializable
    {
        public float x, y, z;       

        public Vector3Serializable(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3Serializable(Vector3 v)
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
        }
        
        public static implicit operator Vector3(Vector3Serializable v)
        {
            return v.GetVector();
        }
        
        public static implicit operator Vector3Serializable(Vector3 v)
        {
            return new Vector3Serializable(v);
        }
       
        public Vector3 GetVector()
        {
            return new Vector3(x, y, z);
        }
    }
}