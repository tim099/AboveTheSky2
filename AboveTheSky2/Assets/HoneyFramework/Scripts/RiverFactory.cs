using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Runtime.Serialization;

namespace HoneyFramework
{
    /*
     * class for managing data of river's single node, used for river mesh creation.
     */
    [Serializable()]
    public class RiverData
    {
        public Vector3Serializable position;
        public Vector3Serializable nextDirection;
        public RiverData next;        
        public bool usedByPostProcess;

        public List<Vector3i> neighbours = new List<Vector3i>();

        public int streamDistance;

        /// <summary>
        /// More flexible constructor used for postprocessed and interpolated river nodes.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="customPositions"></param>
        /// <returns></returns>
        public RiverData(Vector3 pos, bool customPositions)
        {
            //there is no test during finishing for data corruption, as they will be different by definition (ends in mid hex)
            position = pos;
        }

        /// <summary>
        /// Constructor setting hex corner as river data. 
        /// This function contains debug block which will fire errors in editor if some modifications made it plan river routes outside hex borders.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public RiverData(Vector3 pos)
        {
#if UNITY_EDITOR
            float offX = Mathf.Abs(pos.x) - (int)Mathf.Abs(pos.x);
            float offY = Mathf.Abs(pos.y) - (int)Mathf.Abs(pos.y);
            float offZ = Mathf.Abs(pos.z) - (int)Mathf.Abs(pos.z);

            if (offX < 0.49f || offX > 0.51f ||
                offY < 0.49f || offY > 0.51f ||
                offZ < 0.49f || offZ > 0.51f)
            {
                Debug.Log("River data corrupted!!!! : " + pos + " should be always: (integer + 0.5f) ;");
            }
#endif
            position = pos;
        }

        /// <summary>
        /// Finds hex neighbours of river node. 
        /// Note! that this makes sense only before you interpolate and/or post process river shape.
        /// </summary>
        /// <returns></returns>
        public List<Vector3i> GetNeighbours()
        {
            List<Vector3i> ret = new List<Vector3i>();
            for (float x = -0.5f; x <= 0.5f; x += 1.0f)
                for (float y = -0.5f; y <= 0.5f; y += 1.0f)
                {
                    Vector3i pos = Convert(position.x + x, position.y + y);
                    if (IsNextToThisPoint(pos))
                    {
                        ret.Add(pos);
                    }
                }

            return ret;
        }

        /// <summary>
        /// Convert floating point position in hex coordinates into Hex coordinate only by rounding. Less precise than Utils function but faster, and works for this case.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Vector3i Convert(float x, float y)
        {
            Vector3 pos = new Vector3(x, y, -x - y);
            return new Vector3i(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
        }

        /// <summary>
        /// detects if hex position is within radius making it neighbor of the river node
        /// </summary>
        /// <param name="hexPos"></param>
        /// <returns></returns>
        public bool IsNextToThisPoint(Vector3i hexPos)
        {
            Vector3 offset = hexPos - position.GetVector();
            if (offset.x > -0.51f && offset.x < 0.51f &&
                offset.y > -0.51f && offset.y < 0.51f &&
                offset.z > -0.51f && offset.z < 0.51f)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// two nodes laying to close one another will be seen as one
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool IsTheSameAs(Vector3 pos)
        {
            Vector3 temp = pos - position;
            return temp.sqrMagnitude < 0.1f ? true : false;
        }

        /// <summary>
        /// returns 2d Unity world position
        /// </summary>
        /// <returns></returns>
        public Vector2 WorldPosition()
        {
            //rivers uses hex system to position itself and share their internal subsystem. Thats why when we need to find their real position we have to use the same subsystem to counter it
            return HexCoordinates.HexToWorld(position);
        }

        /// <summary>
        /// allows to attach next node. this way we may have "many to one" connections unlike in normal lists where its "one to one" sequence
        /// </summary>
        /// <param name="nextRiverElement"></param>
        /// <returns></returns>
        public void SetNext(RiverData nextRiverElement)
        {
            next = nextRiverElement;
            nextDirection = nextRiverElement.position.GetVector() - position.GetVector();
        }
    }

    public class RiverFactory
    {
        //in hexagonal space those vectors represent 6 "center to corner" directions
        //because axis there are set on flat space every 120 degree. 
        //If you add invert directions it will make direction each 60 degree
        static private Vector3[] directions = new Vector3[] { Vector3.up, Vector3.down, Vector3.left, Vector3.down, Vector3.forward, Vector3.back };


        /// <summary>
        /// Function used to initialize defined number of rivers within world. Count may be smaller though if river fails to create itself within number of tries
        /// </summary>
        /// <param name="world">world requesting river creation</param>
        /// <param name="expectedCount">maximum count of the rivers. </param>
        /// <returns></returns>
        static public void CreateRivers(World world, int expectedCount)
        {
            World.GetInstance().riversStart.Clear();

            int worldRadius = World.GetInstance().hexRadius;
            for (int i = 0; i < expectedCount; i++)
            {
                int x = UnityEngine.Random.Range(-worldRadius, worldRadius + 1);
                int y = UnityEngine.Random.Range(Mathf.Max(-worldRadius - x, -worldRadius), Mathf.Min(worldRadius - x + 1, worldRadius + 1)); //limit Y to value which doesn't exceeds together with X map radius

                Vector3i pos = new Vector3i(x, y, 0);
                pos.z = -pos.x - pos.y;
                Hex h = world.hexes[pos];

                //each river have 5 chances to get produced. If Is unable to do so will be terminated
                int tries = 5;
                while (!GenerateSingleRiver(world, h) && tries > 0)
                {
                    tries--;
                }
            }

            //After rivers are ready, inform all involved hexes about rivers they stand next to
            foreach (RiverData r in World.GetInstance().riversStart)
            {
                InformHexesNextToRiver(r);             
            }

            //Smoothen rivers
            foreach (RiverData r in World.GetInstance().riversStart)
            {               
                PostProcesRiver(r);
            }

            //inform each river segments about their distance from stream
            foreach (RiverData r in World.GetInstance().riversStart)
            {
                RiverData walker = r;

                int index = 0;
                while (walker != null)
                {
                    if (walker.streamDistance < index)
                    {
                        walker.streamDistance = index;
                    }
                    walker = walker.next;
                    index++;
                }
            }

            //BuildRiversMeshes();
        }

        static public void BuildRiversMeshes()
        {
            BuildMeshes(World.GetInstance().riversStart);
        }

        static private void InformHexesNextToRiver(RiverData startPoint)
        {
            if (startPoint == null || startPoint.next == null ||
                startPoint.next.next == null)
            {
                return;
            }

            RiverData walker = startPoint;
            walker.neighbours = walker.GetNeighbours();

            while (walker.next != null)
            {
                walker.next.neighbours = walker.next.GetNeighbours();

                //find common neighbours for this and next node. this way we get two hexes which are divided by this section of river
                List<Vector3i> common = walker.neighbours.FindAll(o => walker.next.neighbours.IndexOf(o) > -1);

                if (common.Count == 2)
                {
                    Hex h1 = World.GetInstance().hexes[common[0]];
                    Hex h2 = World.GetInstance().hexes[common[1]];

                    //its possible that river goes on some borders of the world. We need to ensure both hexes are valid
                    if (h1 != null && h2 != null)
                    {
                        //ensure each neighbor is add only once. 
                        //It is possible that some rivers have some common areas which may link two hexes twice. We do not need to add this twice
                        if (h1.directionsPassingRiver.IndexOf(h2) == -1)
                        {
                            h1.directionsPassingRiver.Add(h2);
                        }

                        if (h2.directionsPassingRiver.IndexOf(h1) == -1)
                        {
                            h2.directionsPassingRiver.Add(h1);
                        }

                    }
                }

                walker = walker.next;
            }
        }

        /// <summary>
        /// Smoothen river ensuring natural shape (no too sharp corners which may fight with map resolution)
        /// </summary>
        /// <param name="startPoint"></param>
        /// <returns></returns>
        static private void PostProcesRiver(RiverData startPoint)
        {
            if (startPoint == null || startPoint.next == null ||
                startPoint.next.next == null)
            {
                return;
            }

            RiverData walker = startPoint;
            RiverData stopAt = null; //this would be used to stop river serquence if its processed second time (for merged rivers)
            int blendNodeCount = 5;

            //Build river node list and ensure nodes know their neighbours
            List<RiverData> riverSequence = new List<RiverData>();
            while (walker != null)
            {
                if (stopAt == null && walker.usedByPostProcess)
                {
                    blendNodeCount--;
                    if (blendNodeCount <= 0)
                    {
                        stopAt = walker;                        
                    }
                }

                walker.neighbours = walker.GetNeighbours();
                riverSequence.Add(walker);
                walker.usedByPostProcess = true;
                walker = walker.next;                
            }           

            RiverData p1;
            RiverData p2;
            RiverData p3;

            RiverData pN0; //interpolated position with previous node data
            RiverData pN1; //interpolated position with current node data
            RiverData pNi; //smoothened position inserted between two interpolated values and two original values

            List<RiverData> newRiverSequence = new List<RiverData>();
            newRiverSequence.Add(riverSequence[0]);

            int finalIndex = -1;

            for (int i = 0; i < riverSequence.Count - 2; i++)
            {
                p1 = riverSequence[i];
                p2 = riverSequence[i + 1];
                p3 = riverSequence[i + 2];

                pN0 = newRiverSequence[newRiverSequence.Count - 1];

                Vector3 v1 = (p1.position.GetVector() + p3.position.GetVector()) * 0.5f;
                Vector3 v2 = (v1 + p2.position) * 0.5f;

                pN1 = new RiverData(v2, true);
                pN1.neighbours = p2.neighbours;

                Vector3 interPoint = (pN0.position.GetVector() + pN1.position.GetVector() + p1.position.GetVector() + p2.position.GetVector()) * 0.25f;
                pNi = new RiverData(interPoint, true);

                newRiverSequence.Add(pNi);
                newRiverSequence.Add(pN1);

                pN0 = pN1;
                
                if (p1 == stopAt)
                {
                    //blending rivers ends at some point
                    finalIndex = newRiverSequence.Count;
                }
            }

            //stretch tight corners and add additional points to smoothen mesh
            Vector3i last = Vector3i.down; //invalid position used as default which will never be found with matching hex (aka null for this case)
            for (int i = 0; i < riverSequence.Count - 1; i++)
            {
                int common = 0;
                List<Vector3i> commonArea = new List<Vector3i>();
                //lets find how long we can walk finding common neighbor
                for (int k = i; k < riverSequence.Count; k++)
                {
                    List<Vector3i> newCommonArea;
                    if (k == i)
                    {
                        newCommonArea = riverSequence[k].neighbours;
                    }
                    else
                    {
                        newCommonArea = commonArea.Intersect(riverSequence[k].neighbours).ToList();
                    }
                    if (newCommonArea == null)
                    {
                        Debug.LogWarning("new common area is null at " + k + "; " + riverSequence[k].position);
                    }
                    if (newCommonArea.Count == 0)
                    {
                        break;
                    }

                    commonArea = newCommonArea;
                    common++;
                }

                if (common >= 4 && last != commonArea[0])
                {
                    if (commonArea.Count != 1) Debug.LogError("Common area count is " + commonArea.Count + " which is wrong. It should at this stage always reach 1");

                    int newListIndex = i * 2;
                    int newListSecondIndex = (i + common) * 2;
                    int smoothingMargin = 2;
                    Vector3 center = new Vector3(commonArea[0].x, commonArea[0].y, commonArea[0].z);
                    // -smoothingMargin and +smoothingMargin is just smoothing offset                
                    for (int j = newListIndex - smoothingMargin; j < newListSecondIndex + 1 + smoothingMargin; j++)
                    {
                        if (j > 0 && j < newRiverSequence.Count)
                        {
                            Vector3 dir = newRiverSequence[j].position - center;

                            if (j >= newListSecondIndex)
                            {
                                int offset = j - newListSecondIndex;
                                newRiverSequence[j].position += dir.normalized * (1 + smoothingMargin - offset) * 0.06f; //smoothing offset
                            }
                            if (j < newListIndex)
                            {
                                int offset = newListIndex - j;
                                newRiverSequence[j].position += dir.normalized * (1 + smoothingMargin - offset) * 0.06f; //smoothing offset
                            }
                        }
                    }
                    last = commonArea[0];
                }
            }

            //rebuild river tree

            for (int i = 0; i < newRiverSequence.Count - 1; i++)
            {                
                newRiverSequence[i].SetNext(newRiverSequence[i + 1]);
                if (finalIndex > -1 && finalIndex == i) break;
            }

            //only add finishing point if its not override by blending from another river
            if (finalIndex == -1)
            {
                newRiverSequence[newRiverSequence.Count - 1].SetNext(riverSequence[riverSequence.Count - 1]);
            }
        }
        
        /// <summary>
        /// Produces data for single river in the world
        /// </summary>
        /// <param name="world"></param>
        /// <param name="hex"></param>
        /// <returns></returns>
        static private bool GenerateSingleRiver(World world, Hex hex)
        {
            Vector3i pos = hex.position;
            Vector3 fPos = pos;
            //this randomizes integer: 0 or 1, unity documentation is wrong saying that 2 would be included as possible result
            int offx = UnityEngine.Random.Range(0, 2); 
            int offy = UnityEngine.Random.Range(0, 2);
            //Now we by offseting by -.5 we acheve values of -0.5 or +0.5 (form 0 or 1)
            fPos.x += offx - 0.5f;
            fPos.y += offy - 0.5f;
            //if both x and y get their roll as 1, then z have to be 0. if both are 0 then z need to be 1.
            //in other cases we let z to roll its own fate
            if (offx == 1 && offy == 1) fPos.z += 0.0f - 0.5f;
            else if (offx == 0 && offy == 1) fPos.z += UnityEngine.Random.Range(0, 2) - 0.5f;
            else if (offx == 1 && offy == 0) fPos.z += UnityEngine.Random.Range(0, 2) - 0.5f;
            else if (offx == 0 && offy == 0) fPos.z += 1.0f - 0.5f;

            //resulted coordinate will give you eg (-0.5, 0.5, 0.5) which is corner between: (0,0,0), (-1,1,0) and (-1,0,1)
            //to see relation between those numbers better lets align them all:
            //River:(-0.5, 0.5, 0.5)
            //hex1 :( 0.0, 0.0, 0.0)
            //hex2 :(-1.0, 1.0, 0.0) 
            //hex3 :(-1.0, 0.0, 1.0)

            RiverData startPoint = new RiverData(fPos);
            RiverData walker = startPoint;
            RiverData prevWalker = null;

            Debug.Log("Plan river from " + fPos);

            //check if new river doesn't start ON another river
            RiverData crossing = CrossingOtherRiver(world, walker, startPoint);
            if (crossing != null) return false;

            Debug.Log("River started");


            world.riversStart.Add(startPoint);           
            bool riverGenerated = true;

            Vector3 preferedDirection = Vector3.zero;

            while (true)
            {
                //check if current walker point is not on the invalid area borders and should terminate river
                List<Vector3i> neighbours = walker.GetNeighbours();
                foreach (Vector3i v in neighbours)
                {
                    if (!world.hexes.ContainsKey(v) || world.hexes[v].terrainType.source.typeList.Contains(MHTerrain.Type.Sea))
                    {
                        //we will stop rivers IN the border water or missing hexes (border hexes?)                        
                        walker.SetNext(new RiverData(v, true));
                        Debug.Log("river end at " + v);

                        //set flag to inform loop that we have finished river production
                        riverGenerated = false;
                        break;
                    }
                }
                if (riverGenerated == false) break;

                //Produce next step for river
                Vector3 nexPoint = NextRiverDirectionGuided(walker, prevWalker, ref preferedDirection);
                RiverData newWalker = new RiverData(walker.position + nexPoint);
                walker.SetNext(newWalker);
                prevWalker = walker;
                walker = newWalker;

                //Check if new walker is not crossing other rivers
                crossing = CrossingOtherRiver(world, walker, startPoint);

                if (crossing == startPoint)
                {
                    //river crosses itself
                    world.riversStart.Remove(startPoint);
                    return false;
                }
                else if(crossing != null)
                {
                    prevWalker.next = crossing;
                    return true;
                }                
            }            

            return true;
        }

        /// <summary>
        /// Produces two directions and then choses one which leads it to its preferred direction
        /// </summary>
        /// <param name="riverPoint"></param>
        /// <param name="previousRiverPoint"></param>
        /// <param name="prefDirection"></param>
        /// <returns></returns>
        static private Vector3 NextRiverDirectionGuided(RiverData riverPoint, RiverData previousRiverPoint, ref Vector3 prefDirection)
        {
            Vector3 v1 = NextRiverDirection(riverPoint, previousRiverPoint);
            Vector3 v2 = NextRiverDirection(riverPoint, previousRiverPoint);

            if ( Vector3.Dot(v1, prefDirection) > Vector3.Dot(v2, prefDirection))
            {
                if (previousRiverPoint == null) prefDirection = v1;
                return v1;
            }

            if (previousRiverPoint == null) prefDirection = v2;
            return v2;
        }
         

        /// <summary>
        /// produces valid direction for next river point
        /// </summary>
        /// <param name="riverPoint"></param>
        /// <param name="previousRiverPoint"></param>
        /// <returns></returns>
        static private Vector3 NextRiverDirection(RiverData riverPoint, RiverData previousRiverPoint)
        {
            if (previousRiverPoint == null)
            {
                Vector3 dir;
                while (true)
                {
                    dir = directions[UnityEngine.Random.Range(0, directions.Length)];
                    Vector3 nextPoint = riverPoint.position + dir;
                    RiverData r = new RiverData(nextPoint);
                    if (r.GetNeighbours().Count == 3)
                    {
                        return dir;
                    }
                }
            }

            // get previous direction and then shift it to opposite value in any of the other vectors
            // eg: (1,0,0) will became (0,-1,0) or (0,0,-1)
            Vector3 direction = previousRiverPoint.nextDirection;
            Vector3 newDir;
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                newDir = new Vector3(-direction.y, -direction.z, -direction.x);
            }
            else
            {
                newDir = new Vector3(-direction.z, -direction.x, -direction.y);
            }

            return newDir;
        }

        /// <summary>
        /// process river node by node and compares it with other registered rivers in world to see if they cross anywhere
        /// </summary>
        /// <param name="world"></param>
        /// <param name="river">river starting point</param>
        /// <returns></returns>
        static private RiverData CrossingOtherRiver(World world, RiverData river, RiverData currentRoot)
        {
            foreach (RiverData r in world.riversStart)
            {
                bool theSameRiver = false;
                if (r.IsTheSameAs(currentRoot.position))
                {
                    theSameRiver = true;
                }
                RiverData walker = r;

                while (walker != null)
                {                    
                    if (walker.IsTheSameAs(river.position))
                    {
                        if (theSameRiver)
                        {
                            //if point next is not exactly same in both cases we consider this point to be different points
                            if (river.next != walker.next)
                            {
                              //  Debug.Log("crossing itself");
                                //river crosses itself. by returning this currentRoot we can identify this problem easier outside this function
                                return currentRoot;
                            }                            
                        }
                        else
                        {
                           // Debug.Log("crossing blend");
                            //this river ends up in another river. 
                            //we will return merge point so that river can override its own doubled node
                                                       
                            return walker;
                        }
                    }
                    walker = walker.next;
                }
            }
               
            return null;
        }        

        #region Mesh Creation
 

        /// <summary>
        /// Manages construction of the whole mesh for the river
        /// </summary>
        /// <param name="riverStarts"></param>
        /// <returns></returns>
        static public void BuildMeshes(List<RiverData> riverStarts)
        {
            List<RiverData> servedNodes = new List<RiverData>();

            foreach (RiverData start in riverStarts)
            {
                List<RiverData> riverPath = new List<RiverData>();
                RiverData walker = start;
                while (walker != null && servedNodes.FindIndex(0, o => o == walker) == -1)
                {
                    servedNodes.Add(walker);
                    riverPath.Add(walker);
                    walker = walker.next;
                }

                if (riverPath.Count == 0) continue;

                if (walker != null)
                {
                    riverPath.Add(walker);
                }

                Mesh m = PathSprites(riverPath);
                if (m != null)
                {
                    GameObject riverRoot = GameObject.Find("RiverRoot");
                    GameObject river = GameObject.Instantiate(WorldOven.GetInstance().riverBase) as GameObject;
                    MeshFilter mf = river.GetComponent<MeshFilter>();
                    mf.mesh = m;
                    river.transform.parent = riverRoot.transform;
                    river.transform.localPosition = Vector3.zero;

                    riverRoot = GameObject.Find("RiverSmoothener");
                    river = GameObject.Instantiate(WorldOven.GetInstance().riverBase) as GameObject;
                    mf = river.GetComponent<MeshFilter>();
                    mf.mesh = m;
                    river.transform.parent = riverRoot.transform;
                    river.transform.localPosition = Vector3.zero;
                }
            }
        }

        /// <summary>
        /// Produces path of the sprites for the river containing multiple row sections.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static private Mesh PathSprites(List<RiverData> path)
        {            
            float stepV = 0.5f / 3;
            float startV = -(path.Count * stepV); // start negative and end in 0. this way common part for few rivers would be the same

            MeshPreparationData meshData = new MeshPreparationData();
            RiverData previous = null;
            for (int i = 0; i < path.Count - 1; i++)
            {
                RiverData current = path[i];
                RiverData next = path[i + 1];

                Vector2 startPos = current.WorldPosition();
                Vector2 endPos = next.WorldPosition();

                Vector2 pTOc = previous != null ? startPos - previous.WorldPosition() : Vector2.zero;
                Vector2 cTOn = endPos - startPos;
                Vector2 nTOf = next.next != null ? next.next.WorldPosition() - endPos : Vector2.zero;

                Vector2 startDir = (pTOc + cTOn).normalized;
                Vector2 endDir = (cTOn + nTOf).normalized;

                Color startC = new Color(1, 1, 1, 1.0f - (float)Mathf.Max(i + 4 - path.Count, 0) / 3.0f);
                Color endC = new Color(1, 1, 1, 1.0f - (float)Mathf.Max(i + 5 - path.Count, 0) / 3.0f);

                //scale river at the beginning making it thinner 
                float scaleFrom = Mathf.Min(5, i) * 0.025f + 0.03f;
                float scaleTo = Mathf.Min(5, i + 1) * 0.025f + 0.03f;

                //produce data for single row
                AddRowOfSprites(meshData,
                                startV, scaleFrom, VectorUtils.Vector2To3D(startPos), VectorUtils.Vector2To3D(startDir), startC,
                                startV + stepV, scaleTo, VectorUtils.Vector2To3D(endPos), VectorUtils.Vector2To3D(endDir), endC);

                previous = current;
                startV += stepV;
            }

            //check if anything have been produced
            if (meshData.vertexList.Count == 0) return null;

            //fill data to mesh
            Mesh m = new Mesh();
            m.vertices = meshData.vertexList.ToArray();
            m.uv = meshData.uvList.ToArray();
            m.triangles = meshData.indexList.ToArray();
            m.colors = meshData.colorList.ToArray();

            return m;
        }

        /// <summary>
        /// Adds row of sprites constructing single section of the river. 
        /// Note! that this is not fitting full size of the texture, but only small bit of it
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startV"></param>
        /// <param name="startScale"></param>
        /// <param name="startPos"></param>
        /// <param name="startDir"></param>
        /// <param name="startC"></param>
        /// <param name="endV"></param>
        /// <param name="endScale"></param>
        /// <param name="endPos"></param>
        /// <param name="endDir"></param>
        /// <param name="endC"></param>
        /// <returns></returns>
        static private void AddRowOfSprites(MeshPreparationData data,
                                            float startV, float startScale, Vector3 startPos, Vector3 startDir, Color startC,
                                            float endV, float endScale, Vector3 endPos, Vector3 endDir, Color endC)
        {
            Vector3 startRightAngle = new Vector3(startDir.z, startDir.y, -startDir.x);
            Vector3 endRightAngle = new Vector3(endDir.z, endDir.y, -endDir.x);

            //split count defines how many sprites in a row would be created
            int splitCount = 5;

            Vector3 startLeft = startPos - startRightAngle * ((float)splitCount) * 0.5f * startScale;
            Vector3 endLeft = endPos - endRightAngle * ((float)splitCount) * 0.5f * endScale;

            for (int i = 0; i < splitCount; i++)
            {
                QuadPreparation qData = new QuadPreparation();
                qData.v0 = startLeft + startRightAngle * startScale * i;
                qData.v1 = startLeft + startRightAngle * startScale * (i + 1);

                qData.v2 = endLeft + endRightAngle * endScale * (i + 1);
                qData.v3 = endLeft + endRightAngle * endScale * i;

                qData.minUV = new Vector2(((float)i) / ((float)splitCount), startV);
                qData.maxUV = new Vector2(((float)i + 1) / ((float)splitCount), endV);

                qData.c0 = startC;
                qData.c1 = startC;
                qData.c2 = endC;
                qData.c3 = endC;

                MeshPreparationData.AddSingleSprite(data, qData);
            }
        }
   
        #endregion
    }
}