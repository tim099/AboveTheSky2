using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HoneyFramework
{
    class Cloud
    {
        static public List<Texture> clouds = null;
        static public List<Texture> shade = null;
        //those values are used to offset rendering depth, this way clouds will be positioned on the ground but will be rendered above terrain (mountains etc)
        //this is a way to avoid problem with being able to use perspective/camera move to look below cloud as opposite to simply moving cloud object to thigh attitude
        static public float cloudLevel = -250f;
        static public float shadeLevel = -245f;
        
        public GameObject obj = null;
        public Hex owner = null;
        public List<Chunk> requestedBy = new List<Chunk>();
        public int order = 0;

        public void SetOwner(Hex h)
        {
            owner = h;
            UpdateMaterial();
            if (h!= null)
            {
                Vector2 pos = h.GetWorldPosition();
                obj.transform.position = VectorUtils.Vector2To3D(pos);
            }
        }

        public void UpdateMaterial()
        {
            if (obj == null || owner == null) return;
            if (clouds == null) GetTextures();
            
            switch (owner.GetVisibility())
            {
                case Hex.Visibility.NotVisible:
                    if (clouds.Count > 0)
                    {
                        MeshRenderer mr = obj.GetComponent<MeshRenderer>();
                        if (clouds.IndexOf(mr.material.mainTexture) < 0)
                        {                            
                            int index = Random.Range(0, clouds.Count);
                            mr.material.mainTexture = clouds[index];
                            mr.material.SetFloat("_Offset", cloudLevel);

                            obj.GetComponent<Renderer>().sortingOrder = 100 + order;
                        }
                        obj.SetActive(true);                        
                    }
                    break;
                    
                case Hex.Visibility.Shadowed:
                    if (shade.Count > 0)
                    {
                        MeshRenderer mr = obj.GetComponent<MeshRenderer>();
                        if (shade.IndexOf(mr.material.mainTexture) < 0)
                        {
                            int index = Random.Range(0, shade.Count);
                            mr.material.mainTexture = shade[index];
                            mr.material.SetFloat("_Offset", shadeLevel);

                            obj.GetComponent<Renderer>().sortingOrder = 50 + order;
                        }
                        obj.SetActive(true);
                    }
                    break;
                case Hex.Visibility.FullyVisible:
                    obj.SetActive(false);
                    break;
            }
            
        }

        public void GetTextures()
        {
            Texture[] textures = Resources.LoadAll<Texture>("FoW");

            clouds = new List<Texture>();
            shade = new List<Texture>();

            foreach (Texture t in textures)
            {
                if (t.name.Contains("Cloud"))
                {
                    clouds.Add(t);
                }
                else
                {
                    shade.Add(t);
                }
            }
        }            
    }

    public class FogOfWar : MonoBehaviour
    {
        private List<Cloud> clouds = new List<Cloud>();
        private GameObject root = null;
        #region Static
        static private FogOfWar instance;
        static private List<Hex> dirtyHexes = new List<Hex>();

        private Dictionary<Chunk, bool> chunkCloudUpdates = new Dictionary<Chunk, bool>();

        static public void Initialize()
        {
            World w = World.GetInstance();
            if (w.GetComponent<FogOfWar>() == null)
            {
                w.gameObject.AddComponent<FogOfWar>();
            }
        }
        
        static public void ChunkUpdate(Chunk chunk, bool visible)
        {
            if (instance == null)
            {
                Debug.LogError("FogOfWar is not yet initialized. Maybe it should be initialized earlier?");
                return;
            }

            instance.ChangeChunkStatus(chunk, visible);
        }

        static public void AddDirtyHex(Hex h)
        {
            dirtyHexes.Add(h);
        }

        #endregion        

        // Use this for initialization
        void Start()
        {
            instance = this;
            root = new GameObject();
            root.transform.SetParent(World.GetInstance().transform);
        }

        // Update is called once per frame after normal game loop
        void LateUpdate()
        {
            foreach(Hex h in dirtyHexes)
            {
                Cloud c = clouds.Find(o => o.owner == h);

                if (c != null)
                {
                    c.UpdateMaterial();
                }
            }
            dirtyHexes.Clear();

            if (chunkCloudUpdates.Count > 0)
            {
                foreach(var v in chunkCloudUpdates)
                {
                    ChangeChunkStatusUpdate(v.Key, v.Value);
                }
                chunkCloudUpdates.Clear();
            }
        }

        /// <summary>
        /// Update collection of terrain covering clouds to ensure visible chunks get what they deserve, and hidden chunks do not lock resources
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="visible"></param>
        /// <returns></returns>
        public void ChangeChunkStatus(Chunk chunk, bool visible)
        {
            chunkCloudUpdates[chunk] = visible;
        }

        void ChangeChunkStatusUpdate(Chunk chunk, bool visible)
        { 
            if (visible)
            {
                //provide cloud instances
                foreach (KeyValuePair<Vector3i, Hex> pair in chunk.hexesCovered)
                {                                       
                    Cloud c = clouds.Find(o => o.owner == pair.Value);
                    if (c == null)
                    {
                        c = GetFree();
                    }
                    //if more than one chunk need this cloud (they share the same hex) then on leaving one of those chunks cloud will not be turned off
                    c.requestedBy.Add(chunk);

                    c.SetOwner(pair.Value);                    
                }
            
            }
            else
            {
                //hide instance
                foreach (KeyValuePair<Vector3i, Hex> pair in chunk.hexesCovered)
                {
                    Cloud c = clouds.Find(o => o.owner == pair.Value);
                    if (c != null)
                    {
                        if (c.requestedBy.Contains(chunk))
                        {
                            c.requestedBy.Remove(chunk);
                        }

                        if (c.requestedBy.Count == 0)
                        {
                            SetFree(c);
                        }                        
                    }                                        
                }
            }
        }

        /// <summary>
        /// Finds unused cloud or creates new one
        /// </summary>
        /// <returns></returns>
        private Cloud GetFree()
        {
            Cloud c = clouds.Find( o => o.owner == null);
            if (c == null)
            {
                c = new Cloud();
                if (World.GetInstance().fogOfWarBase != null)
                {
                    c.obj = (GameObject)GameObject.Instantiate(World.GetInstance().fogOfWarBase);
                    c.obj.transform.SetParent(World.GetInstance().transform);
                    c.order = Random.Range(0, 50);
                }
                clouds.Add(c);
            }                        
            return c;
        }

        /// <summary>
        /// Releases chosen cloud
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private void SetFree(Cloud c)
        {            
            if (c != null)
            {
                c.obj.SetActive(false);
                c.SetOwner(null);                
            }            
        }  

    }

}