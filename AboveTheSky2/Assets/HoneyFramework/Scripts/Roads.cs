using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;

namespace HoneyFramework
{
    /*
     * Simple system for temporary roads. Note that it does not store them for saves. 
     * If you want to store road data with hexes it could be more permanent, some of the users my want to use this more dynamic and for this purpose 
     * class road was created.
     */
    public class Roads
    {
        static private Roads instance;
        static private int offsetToRoadsInMarkerAtlass = 8;

        List<Vector3i> roadLocations = new List<Vector3i>();

        //following markers graphic this array helps to define which marker to use for certain road layout.
        //note that they represent unrotated variation and then variation of all other exits starting from left to right from one to six exits.
        List<bool[]> roadLayouts = new List<bool[]>
        {
            new bool[] {true, false, false, false, false, false},
            new bool[] {true, true, false, false, false, false},
            new bool[] {true, false, true, false, false, false},
            new bool[] {true, false, false, true, false, false},
            new bool[] {true, false, false, false, true, false},
            new bool[] {true, false, false, false, false, true},
            new bool[] {true, true, true, false, false, false},
            new bool[] {true, true, false, true, false, false},

            new bool[] {true, true, false, false, true, false},
            new bool[] {true, true, false, false, false, true},
            new bool[] {true, false, true, true, false, false},
            new bool[] {true, false, true, false, true, false},
            new bool[] {true, false, true, false, false, true},
            new bool[] {true, false, false, true, true, false},
            new bool[] {true, false, false, true, false, true},
            new bool[] {true, false, false, false, true, true},

            new bool[] {true, true, true, true, false, false},
            new bool[] {true, true, true, false, true, false},
            new bool[] {true, true, true, false, false, true},
            new bool[] {true, true, false, true, true, false},
            new bool[] {true, true, false, true, false, true},
            new bool[] {true, true, false, false, true, true},
            new bool[] {true, false, true, true, true, false},
            new bool[] {true, false, true, true, false, true},
            
            new bool[] {true, false, true, false, true, true},
            new bool[] {true, false, false, true, true, true},
            new bool[] {true, true, true, true, true, false},
            new bool[] {true, true, true, true, false, true},
            new bool[] {true, true, true, false, true, true},
            new bool[] {true, true, false, true, true, true},
            new bool[] {true, false, true, true, true, true},
            new bool[] {true, true, true, true, true, true}
        };

        Vector3i[] directions = new Vector3i[6]
        {
            new Vector3i(0, -1, 1), //down
            new Vector3i(-1, 0, 1), //down - left
            new Vector3i(-1, 1, 0), //top - left
            new Vector3i(0, 1, -1), //top
            new Vector3i(1, 0, -1), //top - right
            new Vector3i(1, -1, 0)  //down - right
        };

        private Roads() {}
        
        static public Roads GetInstance()
        {
            if (instance == null)
            {
                instance = new Roads();
            }
            return instance;
        }

        /// <summary>
        /// Sets road to defined existence status at certain location
        /// </summary>
        /// <param name="position"></param>
        /// <param name="exists"></param>
        /// <returns></returns>
        static public void SetRoad(Vector3i position, bool exists)
        {
            GetInstance().SetRoadAt(position, exists);
        }

        /// <summary>
        /// Switches road from existing to non existing at certain location or vice versa
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        static public void SwitchRoadAt(Vector3i position)
        {
            GetInstance().SetRoadAt(position, !GetInstance().roadLocations.Contains(position));
        }

        /// <summary>
        /// Sets road to defined existence status at certain location
        /// </summary>
        /// <param name="position"></param>
        /// <param name="exists"></param>
        /// <returns></returns>
        public void SetRoadAt(Vector3i position, bool exists)
        {
            if (roadLocations.Contains(position) == exists) return;

            if (exists == false)
            {
                roadLocations.Remove(position);                
            }
            else
            {
                roadLocations.Add(position);
            }

            UpdateArea(HexNeighbors.GetRange(position, 1));
        }

        /// <summary>
        /// Updates area of the roads based on the current status of theirs neighborhood.
        /// </summary>
        /// <param name="locations"></param>
        /// <returns></returns>
        public void UpdateArea(List<Vector3i> locations)
        {
            foreach(Vector3i pos in locations)
            {
                if (World.GetInstance().hexes.ContainsKey(pos))
                {
                    UpdateMarkerAt(pos, roadLocations.Contains(pos));
                }
            }
        }

        /// <summary>
        /// Do single marker update based on specified location neighborhood status
        /// </summary>
        /// <param name="position"></param>
        /// <param name="noRoad"></param>
        /// <returns></returns>
        public void UpdateMarkerAt(Vector3i position, bool haveRoad)
        {
            if (!haveRoad)
            {
                HexMarkers.ClearMarkerLayer(position, HexMarkers.Layer.Borders);
                return;
            }

            int startDirection = -1;
            List<bool[]> bestLayouts = new List<bool[]>();
            for(int i=0; i<directions.Length; i++)
            {
                bool isAt = roadLocations.Contains(position + directions[i]);
                if (!isAt && startDirection == -1) continue;

                if (startDirection == -1)
                {
                    startDirection = i;                    
                }
                else if (bestLayouts.Count == 0)
                {
                    //find layouts which exits in default place and have second exit with offset according to index difference
                    int secondExit = i - startDirection;
                    bestLayouts = roadLayouts.FindAll(o => o[secondExit] == isAt);
                }
                else
                {
                    int secondExit = i - startDirection;
                    bestLayouts = bestLayouts.FindAll(o => o[secondExit] == isAt);
                }
            }

            //best count is 0 if no neighbours have road
            if (bestLayouts.Count == 0 )
            {
                if (startDirection == -1)
                {
                    HexMarkers.SetMarkerType(position, offsetToRoadsInMarkerAtlass, HexMarkers.Layer.Borders, 0f);
                }
                else
                {
                    HexMarkers.SetMarkerType(position, offsetToRoadsInMarkerAtlass, HexMarkers.Layer.Borders, HexMarkers.directionZeroOneScale[startDirection]);
                }
                return;
            }
            
            //Note that all roads with fewer exits are earlier in the list and those with the same number of exits but then have them "earlier on the list" are before the other as well
            //this way first on our filtered list layout is the one which firs our needs best without extra filtering afterwards.

            int layoutIndex = roadLayouts.IndexOf(bestLayouts[0]);
            HexMarkers.SetMarkerType(position, offsetToRoadsInMarkerAtlass + layoutIndex, HexMarkers.Layer.Borders, HexMarkers.directionZeroOneScale[startDirection]);
        }
    }
}