using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

namespace HoneyFramework
{
    /*
     * Basic actor for objects which should be placed on hex world
     */
    public class Actor : MonoBehaviour
    {

        /// <summary>
        /// Allows to set actor at center of specified hex
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public void SetHexPosition(Hex location)
        {
            SetHexPosition(location.position);
        }

        /// <summary>
        /// Allows to set actor at position which is in hex world.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public void SetHexPosition(Vector3i position)
        {
            Vector3 pos = HexCoordinates.HexToWorld3D(position);

            transform.localPosition = pos;
        }

        /// <summary>
        /// Allows to set actor at position which is in 3D World coordinates. 
        /// </summary>
        /// <param name="position">3d world position at which actor would be placed. Note that world Y position would be overriden by world height </param>
        /// <returns></returns>
        public void SetWorldPosition(Vector3 position)
        {            
            float h = World.GetWorldHeightAt(position);
            position.y = h;
            transform.localPosition = position;
        }

    }
}