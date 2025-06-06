using UnityEngine;
using System;
using Pathfinding;

namespace HoneyFramework
{
    [Serializable()]
    public struct Vector4Serializable
    {
        public float x, y, z, w;

        public Vector4Serializable(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector4Serializable(Vector4 v)
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
            this.w = v.w;
        }
        
        public static implicit operator Vector4(Vector4Serializable v)
        {
            return v.GetVector();
        }

        public static implicit operator Color(Vector4Serializable v)
        {
            return v.GetColor();
        }
        
        public static implicit operator Vector4Serializable(Vector4 v)
        {
            return new Vector4Serializable(v);
        }

        public static implicit operator Vector4Serializable(Color v)
        {
            return new Vector4Serializable(v);
        }
       
        public Vector4 GetVector()
        {
            return new Vector4(x, y, z, w);
        }

        public Color GetColor()
        {
            return new Color(x, y, z, w);
        }
    }
}