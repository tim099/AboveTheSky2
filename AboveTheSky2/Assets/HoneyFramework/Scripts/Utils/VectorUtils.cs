using UnityEngine;
using System.Collections;

namespace HoneyFramework
{
    public class VectorUtils
    {

        /// <summary>
        /// converts 3d space to 2d plane
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        static public Vector2 Vector3To2D(Vector3 v)
        {
            return new Vector2(v.x, v.z);
        }

        /// <summary>
        /// Converts 2d plane to 3d space
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        static public Vector3 Vector2To3D(Vector2 v)
        {
            return new Vector3(v.x, 0.0f, v.y);
        }
    }
}