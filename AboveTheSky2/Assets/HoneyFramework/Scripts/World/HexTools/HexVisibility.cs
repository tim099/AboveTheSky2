using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

namespace HoneyFramework
{

    public class HexVisibility
    {

        /// <summary>
        /// Experimental visibility functionality which is able to detect hexes partially visible etc by raycasting hex borders and "casting shadows" on hexes behind
        /// </summary>
        /// <param name="obstacles"></param>
        /// <param name="mapCenter"></param>
        /// <param name="mapRadius"></param>
        /// <param name="searchCenter"></param>
        /// <param name="searchRadius"></param>
        /// <returns></returns>
        static public List<PFVector3i> GetVisibility(List<Vector3i> obstacles, Vector3i mapCenter, int mapRadius, Vector3i searchCenter, int searchRadius)
        {
            List<Vector3i> questionedHexes = HexNeighbors.GetSharedRange(searchCenter, searchRadius, mapCenter, mapRadius, false);

            //lets make List ordered by rotation value from search center
            List<PFVector3i> data = new List<PFVector3i>();
            List<PFVector3i> obstaclesData = new List<PFVector3i>();
            Vector2 searchCenter2d = HexCoordinates.HexToWorld(searchCenter);
            foreach (Vector3i v in questionedHexes)
            {
                if (v == searchCenter) continue;

                PFVector3i pfv = new PFVector3i(v);
                pfv.positionNormal = (pfv.worldPosition - searchCenter2d).normalized;
                //get view rotation from search center to hex
                pfv.rotation = Vector2Angle(Vector2.right, pfv.positionNormal);
                //set default state to visible
                pfv.visibility = PFVector3i.Visibility.fullyVisible;
                //mark them as obstacles if they are one
                pfv.obstacle = obstacles.Contains(v);
                //record distance from searchcenter
                pfv.distance = HexCoordinates.HexDistance(searchCenter, v);

                data.Add(pfv);

                if (pfv.obstacle)
                {
                    obstaclesData.Add(pfv);
                }
            }

            // take now obstacles right angle right angle and make offset by their radius. This will find us roughly hex borders. 
            //We will use those values as rotation and find all hexes which fit within the brackets. 
            //all hexes which are in radius bigger than obstacle would become invisible.
            //NOTE! there is no point in testing invisible hexes which are on maximum search radius, as there cant be more hexes behind!


            foreach (PFVector3i obstacle in obstaclesData)
            {
                if (obstacle.visibility == PFVector3i.Visibility.invisible) continue;

                //lets find two points offest to right and left from obstacle 
                Vector2 temp = new Vector2(-obstacle.positionNormal.y, obstacle.positionNormal.x);
                Vector2 left = obstacle.worldPosition + temp * Hex.hexRadius * 1.0f;
                Vector2 toLeftNorm = left - searchCenter2d;
                toLeftNorm.Normalize();
                float rLeft = Vector2Angle(Vector2.right, toLeftNorm);

                temp = new Vector2(obstacle.positionNormal.y, -obstacle.positionNormal.x);
                Vector2 right = obstacle.worldPosition + temp * Hex.hexRadius * 1.0f;
                Vector2 toRightNorm = right - searchCenter2d;
                toRightNorm.Normalize();
                float rRight = Vector2Angle(Vector2.right, toRightNorm);

                Debug.Log("LR " + rLeft + "/" + rRight);

                float halfShadow;

                List<PFVector3i> shadowedArea;
                if (rLeft < rRight)
                {
                    //halfshadow is allowed on 5% error area
                    halfShadow = (Mathf.PI * 2 + rLeft - rRight) * 0.05f;
                    shadowedArea = data.FindAll(o => (obstacle.distance < o.distance &&                     //consider only hexes further than current hex
                                                      o.visibility != PFVector3i.Visibility.invisible &&    //consider only hexes which are not hidden
                                                     (                                                      //consider only hexes which are within rotation arc
                                                        (o.rotation > rRight - halfShadow) ||
                                                        (o.rotation < rLeft + halfShadow)
                                                     )));
                }
                else
                {
                    //halfshadow is allowed on 5% error area
                    halfShadow = (rLeft - rRight) * 0.05f;
                    shadowedArea = data.FindAll(o => (obstacle.distance < o.distance &&                     //consider only hexes further than current hex
                                                      o.visibility != PFVector3i.Visibility.invisible &&    //consider only hexes which are not hidden
                                                     (                                                      //consider only hexes which are within rotation arc
                                                        (o.rotation > rRight - halfShadow) &&
                                                        (o.rotation < rLeft + halfShadow)
                                                     )));
                }

                foreach (PFVector3i hex in shadowedArea)
                {
                    if (Mathf.Abs(hex.rotation - rRight) < halfShadow || Mathf.Abs(hex.rotation - rLeft) < halfShadow)
                    {
                        if (hex.visibility == PFVector3i.Visibility.halfVisible)
                        {
                            hex.visibility = PFVector3i.Visibility.invisible;
                        }
                        else
                        {
                            hex.visibility = PFVector3i.Visibility.halfVisible;
                        }
                    }
                    else
                    {
                        hex.visibility = PFVector3i.Visibility.invisible;
                    }
                }

            }
            return data;

        }



        /// <summary>
        /// return hex positions between flat world position. 
        /// Used for lines of site etc.
        /// </summary>
        /// <param name="p1">world position 1</param>
        /// <param name="p2">world position 2</param>
        /// <param name="sections"></param>
        /// <returns></returns>
        static private List<PFVector3i> GetRayCastHexes(Vector2 p1, Vector2 p2, int sections, Dictionary<Vector3i, PFVector3i> data)
        {
            List<PFVector3i> ret = new List<PFVector3i>();

            Vector2 direction = p2 - p1;
            direction /= sections;

            for (int i = 0; i <= sections; i++)
            {
                Vector2 testPos = p1 + direction * i;
                Vector3i pos = HexCoordinates.GetHexCoordAt(testPos);
                if (data.ContainsKey(pos))
                {
                    PFVector3i p = data[pos];
                    ret.Add(p);
                }
            }

            return ret;

        }

        /// <summary>
        /// Simple raycast between two hex centers. May prove to be not too great if any colliers are considered
        /// </summary>
        /// <param name="start"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        static public List<PFVector3i> GetRayCastHexes(Vector3i start, Vector3i target, Dictionary<Vector3i, PFVector3i> data)
        {
            List<PFVector3i> ret;

            //if distance is 0, we return list with only one element
            if (start == target)
            {
                ret = new List<PFVector3i>();
                if (data.ContainsKey(start))
                {
                    ret.Add(data[start]);
                }
                return ret;
            }

            //if distance is non 0 we will make vector between centers and divide it onto N parts, where N is hex distance
            int distance = HexCoordinates.HexDistance(start, target);

            Vector2 p1 = HexCoordinates.HexToWorld(start);
            Vector2 p2 = HexCoordinates.HexToWorld(target);

            return GetRayCastHexes(p1, p2, distance, data);
        }

        /// <summary>
        /// Positions which end up on both lists are visible fully. Visibility is obstruct only after both rays get obstacles. 
        /// It is possible to get obstacle earlier on one and later on another list!
        /// </summary>
        /// <param name="start"></param>
        /// <param name="target"></param>
        /// <param name="listLeft"></param>
        /// <param name="listRight"></param>
        /// <returns></returns>
        static public void GetAdvancedRayCastHexes(PFVector3i start, PFVector3i target, ref List<PFVector3i> listLeft, ref List<PFVector3i> listRight, Dictionary<Vector3i, PFVector3i> data)
        {
            //if distance is 0, we return list with only one element
            if (start == target)
            {
                listLeft.Add(start);
                listRight.Add(start);
            }

            //if distance is non 0 we will make vector between centers and divide it onto N parts, where N is hex distance
            int distance = HexCoordinates.HexDistance(start.position, target.position);

            Vector2 p1 = start.worldPosition;
            Vector2 p2 = target.worldPosition;
            Vector2 dir = p2 - p1;
            dir.Normalize();

            //offset vector would be half of the hex radius to ensure it doesnt go outside of the hex and is not insanely small
            dir *= Hex.hexRadius * 0.5f;

            Vector2 left = new Vector2(-dir.y, dir.x);
            Vector2 right = new Vector2(dir.y, -dir.x);

            listLeft = GetRayCastHexes(p1 + left, p2 + left, distance, data);
            listRight = GetRayCastHexes(p1 + right, p2 + right, distance, data);


        }


        /// <summary>
        /// BruteForce Field of view test
        /// </summary>
        /// <param name="start"></param>
        /// <param name="radius"></param>
        /// <param name="obstacles"></param>
        /// <returns></returns>
        static public void GetFieldOfView(PFVector3i start, int radius, PFVector3i mapCenter, int mapRadius, Dictionary<Vector3i, PFVector3i> data)
        {
            //Get maximum range, which would work as view targets

            List<Vector3i> targets = HexNeighbors.GetSharedRange(start.position, radius, mapCenter.position, mapRadius, true);


            foreach (Vector3i t in targets)
            {
                List<PFVector3i> left = new List<PFVector3i>();
                List<PFVector3i> right = new List<PFVector3i>();

                bool leftBlocked = false;
                bool rightBlocked = false;

                if (data.ContainsKey(t))
                {
                    GetAdvancedRayCastHexes(start, data[t], ref left, ref right, data);
                }

                //mark hexes to be hidden or half hidden.
                //if hes is half hidden it means that there is at least partial visibility to it, in which case we will not hide it anymore which would be possible if enough ray density is casted            
                for (int i = 0; i < left.Count; i++)
                {
                    if (leftBlocked && rightBlocked)
                    {
                        if (left[i].visibility != PFVector3i.Visibility.halfVisible) left[i].visibility = PFVector3i.Visibility.invisible;
                        if (right[i].visibility != PFVector3i.Visibility.halfVisible) right[i].visibility = PFVector3i.Visibility.invisible;
                    }
                    else if (leftBlocked)
                    {
                        if (left[i].visibility != PFVector3i.Visibility.halfVisible) left[i].visibility = PFVector3i.Visibility.invisible;
                        right[i].visibility = PFVector3i.Visibility.halfVisible;
                    }
                    else if (rightBlocked)
                    {
                        left[i].visibility = PFVector3i.Visibility.halfVisible;
                        if (right[i].visibility != PFVector3i.Visibility.halfVisible) right[i].visibility = PFVector3i.Visibility.invisible;
                    }
                }
            }
        }

        static private int FindIndexCrossing(List<Vector3i> indexedList, List<Vector3i> controlList)
        {
            for (int i = 0; i < indexedList.Count; i++)
            {
                if (controlList.Contains(indexedList[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        static private float Vector2Angle(Vector2 vector1, Vector2 vector2)
        {
            return Mathf.Atan2(vector2.y, vector2.x) - Mathf.Atan2(vector1.y, vector1.x);
        }
    }
}