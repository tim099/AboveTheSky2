//using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;
using System.Collections;

namespace HoneyFramework
{
    /*
     * Class which manages all world content. Its not designed to be instead of gameplay classes but rather be best point for all information about terrain, world settings and status
     */
    public class World : MonoBehaviour
    {
        static public World instance;

        public enum Status
        {
            NotReady,
            Preparation,
            TerrainGeneration,
            Finishing,
            Foreground,
            Ready
        }

        public enum GeneratorMode
        {
            Random,
            Perlin,
        }

        public Status status = Status.NotReady;


        public Dictionary<Vector2i, Chunk> chunks = new Dictionary<Vector2i, Chunk>();
        public Dictionary<Vector3i, Hex> hexes = new Dictionary<Vector3i, Hex>();
        public List<RiverData> riversStart = new List<RiverData>();

        private List<Chunk> chunksToPolish = new List<Chunk>();
        private List<Hex> hexesToPolish = new List<Hex>();

        public int hexRadius = 15;
        public int chunkRadius = 1;

        public GameObject chunkBaseDx11;
        public GameObject chunkBaseDx11WithMarkers;
        public GameObject chunkBase;
        public GameObject chunkBaseWithMarkers;
        public GameObject foregroundBase;
        public GameObject fogOfWarBase;
        public Camera terrainCamera;
        public UFTAtlasMetadata foregroundAtlas;
        public int seed;

        public WorldOven ovenBase;

        /// <summary>
        /// Self checking awake
        /// </summary>
        /// <returns></returns>
        private void Awake()
        {
            instance = this;
            Application.targetFrameRate = 5000;

            terrainCamera = GameObject.Find("WorldCamera").GetComponent<Camera>();

            if (chunkBase == null) Debug.LogError("Missing chunk base! you need to drag & drop one to World instance if you want to use Older DX mode!");
            if (chunkBaseWithMarkers == null) Debug.LogError("Missing chunk base! you need to drag & drop one to World instance if you want to use Older DX mode with markers!");
            if (chunkBaseDx11 == null) Debug.LogError("Missing chunkDx11 base! you need to drag & drop one to World instance if you want to use DX11 mode!");
            if (chunkBaseDx11WithMarkers == null) Debug.LogError("Missing chunkDx11 base! you need to drag & drop one to World instance if you want to use DX11 mode with markers!");
            if (foregroundBase == null) Debug.LogError("Missing foreground base! you need to drag & drop one to World instance!");

            if (terrainCamera == null) Debug.LogError("Missing terrain camera! It may be child of World object, please drag & drop it here!");
            if (foregroundAtlas == null) Debug.LogError("Missing foreground atlas! you need to drag & drop one to World instance!");
        }

        public void ReadyToPolishChunk(Chunk c)
        {
            chunksToPolish.Add(c);
        }

        public void ReadyToPolishHex(Hex h)
        {
            hexesToPolish.Add(h);
        }

        /// <summary>
        /// Initialization of basic data. This function calls world planning and related data loading and creation
        /// </summary>
        /// <returns></returns>
        public void Initialize()
        {
            status = Status.Preparation;

            TerrainDefinition.ReloadDefinitions();

            GenerateBasicWorld(hexRadius, GeneratorMode.Random);

            RiverFactory.CreateRivers(this, hexRadius);

            for (int x = -chunkRadius; x <= chunkRadius; x++)
                for (int y = -chunkRadius; y <= chunkRadius; y++)
                {
                    PrepareChunkData(new Vector2i(x, y));
                }

            status = Status.TerrainGeneration;
        }

        public void InitializeFromSave()
        {
            status = Status.Preparation;

            TerrainDefinition.ReloadDefinitions();

            SaveManager.Load(this);

            for (int x = -chunkRadius; x <= chunkRadius; x++)
                for (int y = -chunkRadius; y <= chunkRadius; y++)
                {
                    PrepareChunkData(new Vector2i(x, y));
                }

            status = Status.TerrainGeneration;
        }

        static public World GetInstance()
        {
            if (instance == null)
            {
                Debug.LogError("World instance not initialized or lost!");
            }

            return instance;
        }

        /// <summary>
        /// Basic terrain generator with no patterns
        /// </summary>
        /// <param name="radius"></param>
        /// <returns></returns>
        public void GenerateBasicWorld(int radius, GeneratorMode mode)
        {
            hexes = new Dictionary<Vector3i, Hex>();            

            List<Vector3i> rangeHexes = HexNeighbors.GetRange(new Vector3i(), radius);
            int terrainCount = TerrainDefinition.definitions.Count;
            if (terrainCount < 1)
            {
                Debug.LogError("no terrain definitions to use!");
                return;
            }

            List<TerrainDefinition> tdList = TerrainDefinition.definitions.FindAll(o => o.source.mode == MHTerrain.Mode.normal);

            foreach (Vector3i v in rangeHexes)
            {
                hexes.Add(v, GetHexDefinition(mode, v, tdList));
            }
        }

        /// <summary>
        /// Produces hex definition for single hex. Different algorithms my result in different world shape and design
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="position"></param>
        /// <param name="tdList"></param>
        /// <returns></returns>
        static public Hex GetHexDefinition(GeneratorMode mode, Vector3i position, List<TerrainDefinition> tdList)
        {
            Hex hex = new Hex();
            int index = 0;

            hex.orderPosition = UnityEngine.Random.Range(0.0f, 1.0f);
            hex.rotationAngle = UnityEngine.Random.Range(0.0f, 360.0f);
            TerrainDefinition def = null;

            switch (mode)
            {
                case GeneratorMode.Random:
                    index = UnityEngine.Random.Range(0, tdList.Count);
                    def = tdList[index];
                    break;

                case GeneratorMode.Perlin:
                    float xScale = .16f;
                    float yScale = .16f;
                    float value = Mathf.PerlinNoise(position.x * xScale, position.y * yScale);
                    index = (int)(value * (tdList.Count - 1));
                    def = tdList[index];
                    break;
            }

            hex.terrainType = def;
            hex.position = position;

            GetInstance().ReadyToPolishHex(hex);

            return hex;
        }


        /// <summary>
        /// Creates chunk and requests World Oven to bake assets for it
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool PrepareChunkData(Vector2i pos)
        {
            Chunk chunk;
            if (chunks.ContainsKey(pos))
            {                
                chunk = chunks[pos];
                chunk.ClearHexesCovered();
            }
            else
            {
                chunk = new Chunk(pos, this);
            }
            Rect r = chunk.GetRect();

            //expand its influence by the texture halfWidth (aka radius) which would let us find all hexes which influence our chunk even with border of their texture
            r.xMin -= Hex.hexTexturePotentialReach;
            r.yMin -= Hex.hexTexturePotentialReach;
            r.xMax += Hex.hexTexturePotentialReach;
            r.yMax += Hex.hexTexturePotentialReach;

            List<Vector3i> intersections = HexNeighbors.GetHexCentersWithinSquare(r);
            bool foundAny = false;

            foreach (Vector3i v in intersections)
            {
                //if hex exists at those coordinates, add it to chunk management
                if (hexes.ContainsKey(v))
                {
                    chunk.hexesCovered[v] = hexes[v];
                    foundAny = foundAny || !(hexes[v].terrainType.source.mode == MHTerrain.Mode.IsBorderType);
                }
                else
                {
                    //produce border hexes which will fill empty space in chunk
                    Hex border = new Hex();
                    TerrainDefinition td = TerrainDefinition.definitions.Find(o => o.source.mode == MHTerrain.Mode.IsBorderType);
                    if (td == null) td = TerrainDefinition.definitions[0];
                    border.terrainType = td;
                    border.rotationAngle = 0;                    
                    border.orderPosition = UnityEngine.Random.Range(0.0f, 1.0f);
                    border.rotationAngle = UnityEngine.Random.Range(0.0f, 360.0f);

                    border.position = v;
                    hexes.Add(v, border);

                    chunk.hexesCovered[v] = border;
                }
            }

            if (foundAny == false)
            {
                return false;
            }
            
            chunks[pos] = chunk;           

            WorldOven.AddDirtyChunk(chunk);

            return true;
        }        

        /// <summary>
        /// Starts coroutine which ads finishing data to chunks
        /// </summary>
        /// <returns></returns>
        public bool StartPolishingWorld()
        {
            if (chunks.Count > 0)
            {
                StartCoroutine("FinishingWorld");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Coroutine which ensures height compression check (which knows itself if is required or not) 
        /// It as well do processing foreground data and generation of the mesh for it
        /// </summary>
        /// <returns></returns>
        IEnumerator FinishingWorld()
        {
            status = Status.Finishing;

            //we will try to compress chunks. They will ignore this command if they are already compressed

            CoroutineHelper.StartTimer();
            foreach (Chunk c in chunksToPolish)
            {             
                c.CompressHeight();
                if (CoroutineHelper.CheckIfPassed(20)) { yield return null; CoroutineHelper.StartTimer(); }
            }

            //now start preparation for trees
            status = Status.Foreground;

            foreach (Hex h in hexesToPolish )
            {
                h.GenerateForegroundData();
                if (CoroutineHelper.CheckIfPassed(20)) { yield return null; CoroutineHelper.StartTimer(); }
            }
            hexesToPolish.Clear();

            foreach (Chunk c in chunksToPolish)
            {
                c.CleanupForeground(false);
                c.GetForegroundData();
                if (CoroutineHelper.CheckIfPassed(20)) { yield return null; CoroutineHelper.StartTimer(); }
            }
            chunksToPolish.Clear();

            status = Status.Ready;
        }

        /// <summary>
        /// returns expected terrain height based on the same data used by shader to build terrain. 
        /// NOTE! shader may shrink distances below water level to smoothen rivers and water borders.
        /// Look into TesselationDX11 shader to ensure calculations match if you need those values correct!
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        static public float GetWorldHeightAt(Vector3 world3Dposition)
        {
            Chunk chunk = Chunk.WorldToChunk(world3Dposition);
            if (chunk == null || chunk.height == null) return 0f;

            Vector2 uv = chunk.GetWorldToUV(world3Dposition);
            int x = (int)(Mathf.Clamp01(1f - uv.x) * chunk.height.width);
            int y = (int)(Mathf.Clamp01(1f - uv.y) * chunk.height.height);

            Color pixel = chunk.height.GetPixel(x, y);

            //pixel is 0 - 1 value. we will move it to -1 to 1
            float heightBase = (pixel.a - 0.5f) * 2f;

            return heightBase * Chunk.ChunkSizeScale();
        }

        /// <summary>
        /// This function allows you to color radius of foreground with new color. Note that this does not produce variation as it used to with normal generation
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        static public void PaintTrees(Vector2 position, float radius, Color color)
        {
            Vector3i hexPosition = HexCoordinates.GetHexCoordAt(position);

            //get hexagonal radius of our interest. taking into account possibility that some trees will go off the hex borders
            int r = (int)((radius + Hex.foregroundRadius * 1.1f) / (Hex.hexRadius * 2f));

            List<Vector3i> positions = HexNeighbors.GetRange(hexPosition, r);            
            World w = World.GetInstance();

            float sqradius = radius*radius;

            foreach (Vector3i pos in positions)
            {
                if (w.hexes.ContainsKey(pos))
                {
                    Hex h = w.hexes[pos];
                    if (h.foregroundData == null) continue;

                    foreach (ForegroundData f in h.foregroundData)
                    {
                        Vector2 dist = position - new Vector2(f.position.x, f.position.z);
                        if (dist.sqrMagnitude < sqradius)
                        {
                            f.color = color;
                            f.colorFinal = color;
                        }
                    }
                }
            }

            Rect rect = new Rect(  position.x - radius,
                                position.y - radius,
                                radius*2,
                                radius*2 );
            List<Chunk> chunks = Chunk.GetChunksInRect(rect);

            foreach(Chunk c in chunks)
            {
                c.CleanupForeground(false);
                c.GetForegroundData();
            }            
        }
    }
}