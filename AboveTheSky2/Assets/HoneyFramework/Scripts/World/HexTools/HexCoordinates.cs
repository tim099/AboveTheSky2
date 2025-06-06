using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HoneyFramework
{

    public class HexCoordinates
    {

        /// <summary>
        /// Converts World3D Space to Hex Space but keeps floating point precision. 
        /// Which can be useful for example during unit movement on Hex World when they travel across hexes
        /// NOTE! Y parameter from World3D Space would be ignored (Hex world is flat for Hex Space
        /// NOTE2! Do not directly cast it to integer if you don't want to get data artifacts! 
        /// use CustomRoundingForHexes instead on result of this function OR GetHexCoordAt directly on vector you planned to provide here as parameter
        /// </summary>
        /// <param name="pos">World3D Space</param>
        /// <returns></returns>
        static public Vector3 WorldToHex(Vector3 pos)
        {
            //Convert world flat coordinates into hex FLOAT position
            float TWO_THIRD = 2.0f / 3.0f;
            float ONE_THIRD = 1.0f / 3.0f;
            float COMPONENT = ONE_THIRD * Mathf.Sqrt(3);

            float x = TWO_THIRD * pos.x / Hex.hexRadius;
            float y = (COMPONENT * pos.z - ONE_THIRD * pos.x) / Hex.hexRadius;
            float z = -x - y;

            return new Vector3(x, y, z);
        }


        /// <summary>
        /// Changes Hex Space position into World3D Space plan (no height involved)
        /// </summary>
        /// <param name="pos">Hex coordinate</param>
        /// <returns></returns>
        static public Vector2 HexToWorld(Vector3i pos)
        {            
            Vector2 worldPos = Hex.GetDirX() * pos.x + Hex.GetDirY() * pos.y + Hex.GetDirZ() * pos.z;

            return worldPos;
        }

        /// <summary>
        /// Converts Hex Space floating point value into plan in World3D Space. 
        /// Can be used if objects are not fully within hexes but uses hex coordinates
        /// </summary>
        /// <param name="pos">floating point Hex World position</param>
        /// <returns></returns>
        static public Vector2 HexToWorld(Vector3 pos)
        {
            Vector2 worldPos = Hex.GetDirX() * pos.x + Hex.GetDirY() * pos.y + Hex.GetDirZ() * pos.z;

            return worldPos;
        }

        /// <summary>
        /// Hex Space to World3D Space conversion
        /// </summary>
        /// <param name="pos">Hex coordinate</param>
        /// <returns></returns>
        static public Vector3 HexToWorld3D(Vector3i pos)
        {
            Vector2 worldPos = HexToWorld(pos);

            return new Vector3(worldPos.x, 0, worldPos.y);
        }

        /// <summary>
        /// Hex Space floating point into World3d Space
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        static public Vector3 HexToWorld3D(Vector3 pos)
        {
            Vector2 worldPos = HexToWorld(pos);

            return new Vector3(worldPos.x, 0, worldPos.y);
        }

        /// <summary>
        /// Finds hex on plan(2d) in World3D Space coordinate. 
        /// Useful if you need to find hex under floating point position;
        /// </summary>
        /// <param name="worldPos">World3D Space position, usially X and Z parts of normal 3d vector.</param>
        /// <returns></returns>
        static public Vector3i GetHexCoordAt(Vector3 worldPos)
        {
            Vector3 pos = WorldToHex(worldPos);
            //we cant use floating hex position, so before return we need to convert it to integer.
            //also its important to understand that floating point position contains some artifacts if converted separately into integers
            //we have to do some post-calculation cleanup to be able to recover from them. 
            return CustomRoundingForHexes(pos);
        }

        static public Vector3i GetHexCoordAt(Vector2 worldPos)
        {
            return GetHexCoordAt(VectorUtils.Vector2To3D(worldPos));
        }

        /// <summary>
        /// Used for conversion from floating point to int based coordinates avoiding rounding artifacts specific to hex systems
        /// </summary>
        /// <param name="position"></param>    
        /// <returns></returns>
        static public Vector3i CustomRoundingForHexes(Vector3 position)
        {
            int roundedX = Mathf.RoundToInt(position.x);
            int roundedY = Mathf.RoundToInt(position.y);
            int roundedZ = Mathf.RoundToInt(position.z);

            //find delta between rounded and original value
            float dx = Mathf.Abs((float)roundedX - position.x);
            float dy = Mathf.Abs((float)roundedY - position.y);
            float dz = Mathf.Abs((float)roundedZ - position.z);

            //value which after rounding get most offset contains biggest artifacts, we want to discard it and recover form {a + b + c = 0} equation
            if (dz > dy && dz > dx) { roundedZ = -roundedX - roundedY; }
            else if (dy > dx) { roundedY = -roundedX - roundedZ; }
            else { roundedX = -roundedY - roundedZ; }

            return new Vector3i(roundedX, roundedY, roundedZ);
        }


        /// <summary>
        /// Returns distance between two given hex coordinates
        /// </summary>
        /// <param name="hexA"></param>
        /// <param name="hexB"></param>
        /// <returns></returns>
        static public int HexDistance(Vector3i hexA, Vector3i hexB)
        {
            return Mathf.Max(Mathf.Abs(hexB.x - hexA.x), Mathf.Abs(hexB.y - hexA.y), Mathf.Abs(hexB.z - hexA.z));
        }

    }
}