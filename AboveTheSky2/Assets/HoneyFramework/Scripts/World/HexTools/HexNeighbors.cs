using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HoneyFramework
{
    /*
     * Class which is designed to find areas of the hexes around other specified hex.
     */
    public class HexNeighbors
    {
        static public Vector3i[] neighbours = new Vector3i[]{
                                                new Vector3i( 1,  0, -1), //  X,  0, -Z
                                                new Vector3i( 1, -1,  0), //  X, -Y,  0
                                                new Vector3i( 0,  1, -1), //  0,  Y, -Z
                                                new Vector3i(-1,  1,  0), // -X,  Y,  0
                                                new Vector3i( 0, -1,  1), //  0, -Y,  Z
                                                new Vector3i(-1,  0,  1)};// -X,  0,  Z


        /// <summary>
        /// Function which is able to find hex position which are shared area of two radius areas. 
        /// </summary>
        /// <param name="centerA"></param>
        /// <param name="radiusA"></param>
        /// <param name="centerB"></param>
        /// <param name="radiusB"></param>
        /// <param name="onlyBorder"> if true, result area will contain list of only most external set of the hexes </param>
        /// <returns> note return MAY not contain positions provided as centers or no results at all! </returns>
        static public List<Vector3i> GetSharedRange(Vector3i centerA, int radiusA, Vector3i centerB, int radiusB, bool onlyBorder)
        {
            if (radiusA < 0 || radiusB < 0) return new List<Vector3i>();

            //if map is smaller than test area lets inverse
            if (radiusB < radiusA) return GetSharedRange(centerB, radiusB, centerA, radiusA, onlyBorder);

            List<Vector3i> results = new List<Vector3i>();

            for (int x = -radiusA; x <= radiusA; x++)
            {
                for (int y = Mathf.Max(-radiusA, -x - radiusA); y <= Mathf.Min(radiusA, -x + radiusA); y++)
                {
                    int z = -x - y;

                    //v is just distance from centerA
                    Vector3i v = new Vector3i(x, y, z);
                    Vector3i vA = v + centerA; //point in real space

                    int distanceA = HexCoordinates.HexDistance(Vector3i.zero, v);
                    int distanceB = HexCoordinates.HexDistance(centerB, vA);

                    if (onlyBorder)
                    {
                        if ((distanceB <= radiusB && distanceA == radiusA) || (distanceA < radiusA && distanceB == radiusB))
                        {
                            results.Add(vA);
                        }
                    }
                    else if (distanceB <= radiusB && distanceA <= radiusA)
                    {
                        results.Add(vA);
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Gets all hex positions within specified radius
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="maxDistance"></param>
        /// <returns> return will contain center and all positions within radius</returns>
        static public List<Vector3i> GetRange(Vector3i startPosition, int maxDistance)
        {
            return GetRange(startPosition, maxDistance, 0);
        }

        /// <summary>
        /// Gets hexes positions within specified range form starting hex
        /// </summary>
        /// <param name="startPosition">hex position which is center of the circle</param>
        /// <param name="maxDistance">maximum included in result</param>
        /// <param name="minDistance">minimum included in result</param>
        /// <returns> return will contain center if min radius is 0 and all positions between min and max radius </returns>
        static public List<Vector3i> GetRange(Vector3i startPosition, int maxDistance, int minDistance)
        {
            if (maxDistance < 0 || minDistance > maxDistance) return new List<Vector3i>();
            List<Vector3i> results = new List<Vector3i>();

            for (int x = -maxDistance; x <= maxDistance; x++)
            {
                for (int y = Mathf.Max(-maxDistance, -x - maxDistance); y <= Mathf.Min(maxDistance, -x + maxDistance); y++)
                {
                    int z = -x - y;
                    //distance from starting point have to be within range minimumRadius => maximumRadius
                    //our calculation takes care only for those up to maximum range but we have to discard those which are in range shorter than allowed minimum
                    Vector3i v = new Vector3i(x, y, z);
                    if (HexCoordinates.HexDistance(Vector3i.zero, v) >= minDistance)
                    {
                        results.Add(v + startPosition);
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Finds all hex centers within 2d wold square. useful while converting coverage of wold space volume into hex space
        /// </summary>
        /// <param name="r">2d space to be covered. Note that you may want to extend rectangle by at least hex radius if you need to know it there is cross coverage. By default it returns only positions of the centers which fit within rect</param>
        /// <returns></returns>
        static public List<Vector3i> GetHexCentersWithinSquare(Rect r)
        {
            Vector3i center = HexCoordinates.GetHexCoordAt(r.center);
            List<Vector3i> ret = new List<Vector3i>();
            ret.Add(center);

            int ring = 1;

            bool anyInRange = true;
            while (anyInRange)
            {

                anyInRange = false;
                List<Vector3i> ringPositions = GetRange(center, ring, ring);
                foreach (Vector3i pos in ringPositions)
                {
                    //check if rectangle contains world position of this hex
                    if (r.Contains(HexCoordinates.HexToWorld(pos)))
                    {
                        anyInRange = true;
                        ret.Add(pos);
                    }
                }
                ring++;
            }
            return ret;
        }
    }
}