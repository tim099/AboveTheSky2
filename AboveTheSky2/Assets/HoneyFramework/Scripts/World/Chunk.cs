using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using UnityEngine;


namespace HoneyFramework
{
    /*
     *  Single chunk is most basic next to the hex world construction part. 
     *  Chunk usually covers multiple hexes (around 33) and shares some of them with neighbour chunks
     */
    [Serializable()]
    public class Chunk : ISerializable
    {
        //single chunk texture resolution
        public static int TextureSize
        {
            get { return MHGameSettings.GetSetting<int>(MHGameSettings.DataName.ChunkTextureSize); }
        }

        //single chunk size
        public static float ChunkSizeInWorld
        {
            get { return 10f; }
        }        

        //Textures used for terrain. Note that textures are inverted on x and y! in case you want them sampled for color against world position
        public Texture2D diffuse;
        public Texture2D height;
        public Texture2D shadows;

        public List<Texture2D> texturesForCleanup = new List<Texture2D>();

        public Dictionary<Vector3i, Hex> hexesCovered = new Dictionary<Vector3i, Hex>();
        public List<ForegroundData> foregroundData = new List<ForegroundData>(); //foreground data which fits within chunk

        public Vector2i position;
        public GameObject chunkObject;
        public GameObject foregroundObject;
        public World worldOwner;

        public Material chunkMaterial;

        public bool diffuseCompressed;
        public bool heightCompressed;

        /// <summary>
        /// Deserialization constructor to match serialization protocol
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Chunk(SerializationInfo info, StreamingContext context)
        {
            position = (Vector2i)info.GetValue("position", typeof(Vector2i));
        }


        /// <summary>
        /// Serialization protocol which overrides default serialization behavior
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("position", position, typeof(Vector2i));
        }


        public Chunk(Vector2i position, World worldOwner)
        {
            this.position = position;
            this.worldOwner = worldOwner;
        }

        /// <summary>
        /// Returns rect of the chunk in World plane (its not Hex space, it is unity world space (X,Z) plane)
        /// </summary>
        /// <param name="pos"> x/y position of the chunk among other chunks. </param>
        /// <returns></returns>
        static public Rect GetRect(Vector2i pos)
        {
            Rect r = new Rect(pos.x * ChunkSizeInWorld - ChunkSizeInWorld * 0.5f, pos.y * ChunkSizeInWorld - ChunkSizeInWorld * 0.5f, ChunkSizeInWorld, ChunkSizeInWorld);
            return r;
        }

        /// <summary>
        /// Returns rect of the chunk in World plane (its not Hex space, it is unity world space (X,Z) plane)
        /// </summary>
        /// <returns></returns>
        public Rect GetRect()
        {
            return GetRect(position);
        }

        /// <summary>
        /// Transforms 3d point projection onto (X, Z) plane space and then into this chunk UV space. This way you may identify if point is within or outside of this chunk
        /// </summary>
        /// <param name="world3DPos"></param>
        /// <returns></returns>
        public Vector2 GetWorldToUV(Vector3 world3DPos)
        {
            Rect r = GetRect();
            Vector2 world2D = new Vector2(world3DPos.x, world3DPos.z);
            Vector2 uv = (world2D - new Vector2(r.xMin, r.yMin)) / r.width;
            return uv;
        }

        /// <summary>
        /// returns chunk scale. It allows to define how big area is covered by single chunk. But may interfere with tessellation height which scales based on object size
        /// </summary>
        /// <returns></returns>
        static public float ChunkSizeScale()
        {
            return ChunkSizeInWorld / 10f;
        }

        /// <summary>
        /// returns chunk under world position
        /// </summary>
        /// <param name="world3DPos"></param>
        /// <returns></returns>
        static public Chunk WorldToChunk(Vector3 world3DPos)
        {
            Vector2 pos2d = VectorUtils.Vector3To2D(world3DPos);
            return WorldToChunk(pos2d);            
        }

        static public Chunk WorldToChunk(Vector2 world2DPos)
        {
            world2DPos /= ChunkSizeInWorld;
            Vector2i pos2di = new Vector2i(Mathf.RoundToInt(world2DPos.x), Mathf.RoundToInt(world2DPos.y));

            if (World.instance.chunks.ContainsKey(pos2di))
            {
                return World.instance.chunks[pos2di];
            }

            return null;
        }

        /// <summary>
        /// Finds and returns chunks which are covering specified rectangular area
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        static public List<Chunk> GetChunksInRect(Rect area)
        {
            //ensuring that math will not skip any chunk by imprecision
            float step = ChunkSizeInWorld * 0.99f;
            Chunk c;

            List<Chunk> chunks = new List<Chunk>();
            for (float x = area.xMin; x < area.xMax; x += step)
                for (float y = area.yMin; y < area.yMax; y += step)
                {
                    c = WorldToChunk(new Vector2(x, y));
                    if (!chunks.Contains(c)) chunks.Add(c);
                }

            //final 3 corners which most likely are missed by loop

            c = WorldToChunk(new Vector2(area.xMax, area.yMin));
            if (!chunks.Contains(c)) chunks.Add(c);

            c = WorldToChunk(new Vector2(area.xMin, area.yMax));
            if (!chunks.Contains(c)) chunks.Add(c);

            c = WorldToChunk(new Vector2(area.xMax, area.yMax));
            if (!chunks.Contains(c)) chunks.Add(c);

            return chunks;
        }

        /// <summary>
        /// Produces chunk assets, and ensures their height is continues with neighbours
        /// </summary>
        /// <returns></returns>
        public void CreateGameObjectWithTextures()
        {
            WeldChunk();

            if (MHGameSettings.GetDx11Mode())
            {
                if (chunkObject == null)
                {
                    if (MHGameSettings.GetMarkersMode())
                    {
                        chunkObject = GameObject.Instantiate(worldOwner.chunkBaseDx11WithMarkers) as GameObject;
                    }
                    else
                    {
                        chunkObject = GameObject.Instantiate(worldOwner.chunkBaseDx11) as GameObject;
                    }
                }
            }
            else
            {
                if (chunkObject == null)
                {
                    if (MHGameSettings.GetMarkersMode())
                    {
                        chunkObject = GameObject.Instantiate(worldOwner.chunkBaseWithMarkers) as GameObject;
                    }
                    else
                    {
                        chunkObject = GameObject.Instantiate(worldOwner.chunkBase) as GameObject;
                    }
                }

                MeshFilter m = chunkObject.GetComponent<MeshFilter>();
                if (m.sharedMesh != null)  { GameObject.Destroy(m.sharedMesh); }
                m.sharedMesh = ProduceTerrainMesh(this);
            }

            chunkObject.transform.parent = worldOwner.transform;
            chunkObject.name = "Chunk" + position;

            Vector2 center = GetRect().center;
            chunkObject.transform.localPosition = new Vector3(center.x, 0, center.y);
            float scale = ChunkSizeInWorld / 10.0f;
            chunkObject.transform.localScale = new Vector3(scale, scale, scale);

            MeshRenderer mr = chunkObject.GetComponent<MeshRenderer>();
            mr.material.SetTexture("_MainTex", diffuse);
            mr.material.SetTexture("_HeightTex", height);

            chunkMaterial = mr.material;

            SetMarkerMaterials();            

            MeshFilter mf = chunkObject.GetComponent<MeshFilter>();
            Bounds b = mf.mesh.bounds;

            if (MHGameSettings.GetDx11Mode() && b.size.y < 1.0f)
            {
                //extending bounds so that chunk is loaded before it enters screen, flat (plane) bounding box is not enough for our needs, and our world works as plane just before rendering                
                b.Expand(new Vector3(0, 2, 0));
                mf.mesh.bounds = b;                
            }

            ChunkBehaviour cb = chunkObject.GetComponent<ChunkBehaviour>();
            if (cb == null)
            {
                cb = chunkObject.AddComponent<ChunkBehaviour>();
            }
            cb.owner = this;

            if (texturesForCleanup.Count > 0)
            {
                foreach (Texture2D t in texturesForCleanup)
                {
                    GameObject.Destroy(t);
                }
                texturesForCleanup.Clear();
            }
        }

        public void SetMarkerMaterials()
        {
            if (chunkMaterial != null && MHGameSettings.GetMarkersMode())
            {
                chunkMaterial.SetTexture("_MarkersGraphic", HexMarkers.GetMarkersTexture());
                chunkMaterial.SetTexture("_MarkersPositionData", HexMarkers.GetHexDataTexture());
                chunkMaterial.SetVector("_MarkerSettings", HexMarkers.GetMarkersSettings());
            }
        }

        /// <summary>
        /// links previous with next height value which may differ on borders thanks to small artifact accumulation over baking process
        /// </summary>
        /// <returns></returns>
        public void WeldChunk()
        {
            bool welded = false;
            Vector2i prevY = position + new Vector2i(0, -1);
            if (worldOwner.chunks.ContainsKey(prevY))
            {
                Chunk c = worldOwner.chunks[prevY];
                if (c.heightCompressed)
                {
                    Debug.LogError("CopyLastPixelRows uses compressed height!" + c.position);
                }

                CopyLastPixelRows(c.height, height, false);
                welded = true;
            }

            Vector2i prevX = position + new Vector2i(-1, 0);
            if (worldOwner.chunks.ContainsKey(prevX))
            {
                Chunk c = worldOwner.chunks[prevX];
                if (c.heightCompressed)
                {
                    Debug.LogError("CopyLastPixelColums uses compressed height!" + c.position);
                }
                CopyLastPixelColums(c.height, height, false);
                welded = true;

                c.CompressHeight();
            }

            if (welded)
            {
                height.Apply();
            }
        }

        /// <summary>
        /// copy between textures. If texture will be compressed we need to copy 4 rows instead!
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="forCompression"></param>
        /// <returns></returns>
        public void CopyLastPixelRows(Texture2D from, Texture2D to, bool forCompression)
        {
            int tSize = from.height;

            for (int i = 0; i < 4; i++)
            {
                if (!forCompression && i != 0) return;
                Color[] data = from.GetPixels(0, i, tSize, 1);
                to.SetPixels(0, tSize - 1 - i, tSize, 1, data);
            }
        }

        /// <summary>
        /// copy column between textures. If texture will be compressed we need to copy 4 column instead!
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="forCompression"></param>
        /// <returns></returns>
        public void CopyLastPixelColums(Texture2D from, Texture2D to, bool forCompression)
        {
            int tSize = from.height;

            for (int i = 0; i < 4; i++)
            {
                if (!forCompression && i != 0) return;
                Color[] data = from.GetPixels(i, 0, 1, tSize);
                to.SetPixels(tSize - 1 - i, 0, 1, tSize, data);
            }
        }

        /// <summary>
        /// It is possible to compress height textures to save memory space but this way we get visible artifacts on water borders, slower build and impossibility to later modify single chunk
        /// </summary>
        /// <returns></returns>
        public void CompressHeight()
        {
            if (!heightCompressed)
            {
                //we would just mark height as compressed but we don't want to compress it anymore
                //heightCompressed = true;

                
                //height.Compress(false);
                //height.Apply();
            }
        }

        /// <summary>
        /// Compresses single chunk diffuse textures
        /// </summary>
        /// <returns></returns>
        public void CompressDiffuse()
        {
            if (!diffuseCompressed)
            {
                diffuseCompressed = true;
                diffuse.Compress(false);
                diffuse.filterMode = FilterMode.Bilinear;
                diffuse.Apply();
            }
        }

        /// <summary>
        /// parses through controlled hexes and produces single list of the foreground data which fits within chunk 
        /// Note that single hex may be shared up to even 4 chunks and so its foreground elements!
        /// </summary>
        /// <returns></returns>
        public void GetForegroundData()
        {
            if (height == null)
            {
                Debug.LogError("Missing height! Cant build foreground without this data!");
            }
                        
            // hexes should have pre-generated foreground content. Chunks simply finds which elements belong to them, and then sort by z            
            foreach (KeyValuePair<Vector3i, Hex> pair in hexesCovered)
            {
                Hex h = pair.Value;
                foreach (ForegroundData d in h.foregroundData)
                {
                    Vector2 uv = GetWorldToUV(d.position);

                    if (uv.x > 0.0f && uv.x <= 1.0f &&
                        uv.y > 0.0f && uv.y <= 1.0f)
                    {
                        Vector2i textureUV = new Vector2i((int)((1 - uv.x) * height.width), (int)((1 - uv.y) * height.height));
                        Color heightColor = height.GetPixel(textureUV.x, textureUV.y);
                        Vector2i ShadowUV = new Vector2i((int)((1 - uv.x) * shadows.width), (int)((1 - uv.y) * shadows.height));
                        Color shadowsCenter = shadows.GetPixel(ShadowUV.x, ShadowUV.y);
                        if (heightColor.a > 0.495f && heightColor.a < 0.75f)
                        {
                            d.position.y = (heightColor.a - 0.5f) * 2f - 0.02f;

                            float LandSX = shadows.GetPixel(ShadowUV.x + 1, ShadowUV.y).r;
                            float LandSY = shadows.GetPixel(ShadowUV.x, ShadowUV.y + 1).r;
                            float LandSXX = shadows.GetPixel(ShadowUV.x - 1, ShadowUV.y).r;
                            float LandSYY = shadows.GetPixel(ShadowUV.x, ShadowUV.y - 1).r;
                           
                            //adds value form 5 points then uses 0.2 to scale it to 0-1 value
                            // then it subtracts 0.5 moving it into range from  0.5 to -0.5
                            // scales result (as its rarely reaches far from 0) and then adds 1 ensuring previous 0 is now 1, which is neutral for multiplication
                            float extraLightning = ((((shadowsCenter.r + LandSX + LandSY + LandSXX + LandSYY) * 0.2f + -0.5f) * 5.0f) + 1f);                            

                            //cut down over burnings and too deep shadows if any showed up
                            float lightAndShadow = Mathf.Min(1.5f, Mathf.Max(0.6f, extraLightning));

                            d.colorFinal = d.color.GetColor() * lightAndShadow;
                            foregroundData.Add(d);
                        }
                    }
                }
            }

            //sort foreground ensuring their order is proper for the camera (and so will be mesh)
            foregroundData.Sort(
                    delegate(ForegroundData a, ForegroundData b)
                    {
                        //return inverse of the order
                        return -a.position.z.CompareTo(b.position.z);
                    }
                );

            //if none exists, create foreground object
            if (foregroundObject == null)
            {
                foregroundObject = GameObject.Instantiate(World.GetInstance().foregroundBase) as GameObject;
                foregroundObject.transform.parent = chunkObject.transform;                
            }

            //build foreground mesh and set it to mesh filter
            float angle = worldOwner.terrainCamera.transform.rotation.eulerAngles.x;
            angle = Mathf.Min(40.0f, angle);
            Mesh m = ForegroundFactory.BuildForeground(this, World.GetInstance().foregroundAtlas, angle, World.GetInstance().transform.position);
            MeshFilter mf = foregroundObject.GetComponent<MeshFilter>();
            mf.mesh = m;

            MeshRenderer mr = foregroundObject.GetComponent<MeshRenderer>();
            mr.material.mainTexture = World.GetInstance().foregroundAtlas.texture;
            mr.gameObject.GetComponent<Renderer>().sortingOrder = position.x-20;

        }

        /// <summary>
        /// this function will build mesh used by chunks when not using dx11 tessellation.
        /// </summary>
        /// <returns></returns>
        static public Mesh ProduceTerrainMesh(Chunk source)
        {
            int meshDensity = 75;
            MeshPreparationData data = new MeshPreparationData();
            
            float quadScale = Chunk.ChunkSizeInWorld / meshDensity;
            Vector3 offset = new Vector3(-Chunk.ChunkSizeInWorld / 2f, 0, -Chunk.ChunkSizeInWorld / 2f);

            int vertexRowCount = meshDensity +1;
            
            //build vertex map
            for (int y = 0; y < vertexRowCount; y++)
            {
                for (int x = 0; x < vertexRowCount; x++)
                {
                    float u = 1 - x / (float)meshDensity;
                    float v = 1 - y / (float)meshDensity;                    
                    float h =(source.height.GetPixelBilinear(u, v).a - 0.5f) * 1.6f;

                    data.vertexList.Add(new Vector3(x * quadScale, h, y * quadScale) + offset);

                    data.uvList.Add(new Vector2(u, v));
                }
            }

            //build normal map
            for (int y = 0; y < vertexRowCount; y++)
            {
                for (int x = 0; x < vertexRowCount; x++)
                {
                    //we take value of the vertex height in center and 4 neighbour vertices clamped to mesh size
                    int center = y * vertexRowCount + x;
                    int top = y > 0 ? (y - 1) * vertexRowCount + x : center;
                    int bottom = y < meshDensity ? (y + 1) * vertexRowCount + x : center;
                    int left = x > 0 ? y * vertexRowCount + x - 1 : center;
                     int right     = x < meshDensity   ? y * vertexRowCount + x +1     : center;

                    //now define normal direction based on (top vs bottom) and (left vs right)
                    Vector3 normal = Vector3.up * 0.1f 
                        + Vector3.left * (data.vertexList[left].y - data.vertexList[right].y) 
                        + Vector3.forward * (data.vertexList[top].y - data.vertexList[bottom].y);
                    normal.Normalize();

                    data.normalsList.Add(normal);
                }
            }

            //index vertices to build triangles. 
            //Note that we do not iterate here over extended size (we use meshDensity instead of vertexRowCount, 
            //to ensure we have one vertex more on right size to take for the last quads)
            for (int y = 0; y < meshDensity; y++)
            {
                for (int x = 0; x < meshDensity; x++)
                {
                    int row1 = y * vertexRowCount + x;
                    int row2 = (y + 1) * vertexRowCount + x;

                    // v1      v2
                    // X--------X
                    // |        |
                    // |        |
                    // |        |
                    // X--------X
                    // v3      v4

                    int v1 = row1;
                    int v2 = row1 + 1;
                    int v3 = row2;
                    int v4 = row2 + 1;

                    data.indexList.Add(v1);
                    data.indexList.Add(v3);
                    data.indexList.Add(v4);

                    data.indexList.Add(v1);
                    data.indexList.Add(v4);
                    data.indexList.Add(v2);

                }
            }
            
            Mesh m = new Mesh();
            m.name = "ChunkMesh_" + meshDensity + "x" + meshDensity;
            m.vertices = data.vertexList.ToArray();
            m.uv = data.uvList.ToArray();
            m.triangles = data.indexList.ToArray();
            m.normals = data.normalsList.ToArray();

            return m;
        }

        /// <summary>
        /// Clears foreground and ensures compression flags are reset
        /// </summary>
        /// <returns></returns>
        public void InvalidateChunkData()
        {
            diffuseCompressed = false;
            heightCompressed = false;           
        }

        /// <summary>
        /// Clears data used by foreground for regeneration
        /// </summary>
        /// <returns></returns>
        public void CleanupForeground(bool removeObject)
        {
            if (foregroundObject != null)
            {
                MeshFilter mf = foregroundObject.GetComponent<MeshFilter>();
                if (mf != null && mf.sharedMesh != null)
                {
                    GameObject.Destroy(mf.sharedMesh);
                }

                if (removeObject)
                {
                    MeshRenderer m = foregroundObject.GetComponent<MeshRenderer>();
                    if (m != null && m.material != null)
                    {
                        GameObject.Destroy(m.material);
                    }
                    GameObject.Destroy(foregroundObject);
                    foregroundObject = null;
                }
            }
            foregroundData.Clear();  
        }

        /// <summary>
        /// Clears list of hexes covered by chunk
        /// </summary>
        /// <returns></returns>
        public void ClearHexesCovered()
        {
            hexesCovered.Clear();            
        }
    }
}