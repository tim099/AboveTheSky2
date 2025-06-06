using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HoneyFramework
{
     /*
     * base class for single hex. It contains all data required by the system to define its design, type and behavior
     */
    [Serializable()]
    public class Hex : ISerializable
    {
        //We are using system which uses coordinates:

        //      Y \
        //         \
        //          ------ X
        //         /
        //      Z /
        //this way all hexes coordinate is always build of 3 components: X, Y Z, and sum of the value on all of them is equal 0
        //eg: (1, 1, -2)
        //distance is calculated as:   Math.max( x2-x1, y2-y1, z2-z1); 
        //which gives easy way to get result even in huge map

        //      
        //      -=| X |=-    To East
        //
        //     \    0    /
        //      \_______/
        //  -1  /       \    1 
        // ____/    0    \______
        //     \         /      
        //  -1  \_______/    1
        //      /       \
        //     /    0    \

        //      -=| Y |=-      To North-West
        //     \    1    /
        //      \_______/
        //   1  /       \    0
        // ____/    0    \______ 
        //     \         /      
        //   0  \_______/   -1
        //      /       \
        //     /   -1    \

        //      -=| Z |=-      To South-West
        //     \   -1    /
        //      \_______/
        //   0  /       \   -1
        // ____/    0    \______
        //     \         /
        //   1  \_______/    0
        //      /       \
        //     /    1    \

        static protected Vector2 Xdir;
        static protected Vector2 Ydir;
        static protected Vector2 Zdir;
        static protected bool dirInitialized = false;

        //Distance from hex center to hex corner
        static public float hexRadius = 1f;

        //hex own texture coverage radius(half width)
        static public float hexTextureScale = 1.6f * hexRadius;
        //calculation which considers area which may be influenced directly by texture, then adds small margin to ensure interaction(neighbor) pixels are taken into account as well
        static public float hexTexturePotentialReach = hexTextureScale * hexRadius * Mathf.Sqrt(2) + 0.001f; //~ 2.262 with default honey settings

        static public float foregroundRadius = 1.1f; //NOTE! this value should not be bigger than hexTexturePotentialReach because their foreground may not be picked up by any chunk! 

        [NonSerialized]
        public Vector3i position;
        [NonSerialized]
        public TerrainDefinition terrainType;
        [NonSerialized]
        public float orderPosition;
        [NonSerialized]
        public float rotationAngle;

        [NonSerialized]
        public List<ForegroundData> foregroundData = new List<ForegroundData>();
        [NonSerialized]
        public List<Hex> directionsPassingRiver = new List<Hex>();

        public enum Visibility
        {
            FullyVisible,
            Shadowed,
            NotVisible,
        }

        private Visibility visibility = Visibility.FullyVisible;

        /// <summary>
        /// Deserialization constructor to match serialization protocol
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Hex(SerializationInfo info, StreamingContext context)
        {
            position        = (Vector3i)info.GetValue("position", typeof(Vector3i));
            orderPosition   = (float)info.GetValue("orderPosition", typeof(float));
            rotationAngle   = (float)info.GetValue("rotationAngle", typeof(float));
            foregroundData  = (List<ForegroundData>)info.GetValue("foregroundData", typeof(List<ForegroundData>));
            directionsPassingRiver = (List<Hex>)info.GetValue("directionsPassingRiver", typeof(List<Hex>));

            int terrainIndex = (int)info.GetValue("terrainIndex", typeof(int));

            if (terrainIndex < TerrainDefinition.definitions.Count)
            {
                terrainType = TerrainDefinition.definitions[terrainIndex];
            }
            else
            {
                Debug.Log("Terrain index out of bound!");
            }
        }


        /// <summary>
        /// Serialization protocol which overrides default serialization behavior
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("position", position, typeof(Vector3i));
            info.AddValue("orderPosition", orderPosition, typeof(float));
            info.AddValue("rotationAngle", rotationAngle, typeof(float));
            info.AddValue("foregroundData", foregroundData, typeof(List<ForegroundData>));
            info.AddValue("directionsPassingRiver", directionsPassingRiver, typeof(List<Hex>));

            int terrainIndex = TerrainDefinition.definitions.IndexOf(terrainType);
            info.AddValue("terrainIndex", terrainIndex, typeof(int));
        }

        public Hex()
        {

        }

        /// <summary>
        /// returns direction of the X axis from hexagonal space to world
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetDirX()
        {
            if (!dirInitialized) InitializeDirections();

            return Xdir;
        }

        /// <summary>
        /// returns direction of the Y axis from hexagonal space to world
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetDirY()
        {
            if (!dirInitialized) InitializeDirections();

            return Ydir;
        }

        /// <summary>
        /// returns direction of the Z axis from hexagonal space to world
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetDirZ()
        {
            if (!dirInitialized) InitializeDirections();

            return Zdir;
        }


        private static void InitializeDirections()
        {
            Quaternion Yrot = Quaternion.Euler(0, 0, 120);
            Quaternion Zrot = Quaternion.Euler(0, 0, 240);

            Vector3 alongXVecotr = new Vector3(Hex.hexRadius, 0, 0);

            Xdir = (Vector2)(alongXVecotr);
            Ydir = (Vector2)(Yrot * alongXVecotr);
            Zdir = (Vector2)(Zrot * alongXVecotr);
        }

        /// <summary>
        /// Converts hex position to world 2d plane (X ,Z)
        /// </summary>
        /// <returns></returns>
        public Vector2 GetWorldPosition()
        {
            return HexCoordinates.HexToWorld(position);
        }

        /// <summary>
        /// Generates foreground data of this single hex, later chunks will take control of foreground which have covered them but production occurs here.
        /// </summary>
        /// <returns></returns>
        public void GenerateForegroundData()
        {
            foregroundData.Clear();

            Vector2 flatHexPosition = GetWorldPosition();
            Vector3 hexPosition = new Vector3(flatHexPosition.x, 0.0f, flatHexPosition.y);

            foreach (MHSimpleCounter type in terrainType.source.fgTypes)
            {
                for (int i = 0; i < type.count; i++)
                {
                    ForegroundData data = new ForegroundData();

                    //produces value focused around center of the range
                    float rad = (UnityEngine.Random.Range(0, foregroundRadius) + UnityEngine.Random.Range(0, foregroundRadius)) * 0.5f;
                    Quaternion q = Quaternion.Euler(0.0f, UnityEngine.Random.Range(0.0f, 360.0f), 0.0f);
                    data.position = q * new Vector3(rad, 0.0f, 0.0f) + hexPosition;

                    data.color = TerrainDefinition.GetRandomizedColor(terrainType.source.foregroundColor, 30);
                    data.colorFinal = data.color;
                    data.name = type.name;
                    data.scale = UnityEngine.Random.Range(0.002f * Hex.hexRadius, 0.0026f * Hex.hexRadius);
                    //data.horizontalInverse = Random.Range(0, 2) < 1 ? false : true;

                    foregroundData.Add(data);
                }
            }
        }

        /// <summary>
        /// Triggers proper world rebuild if data of this hex is changed. Process is very heavy and far from great, but may provide great functionality if 
        /// terrain updates are required by your project or you do some level of live editing world (eg as designers tool)
        /// </summary>
        /// <returns></returns>
        public void RebuildChunksOwningThisHex()
        {
            //Note! this shouldn't be called if world is not ready

            //get hex center in world
            Vector2 pos = HexCoordinates.HexToWorld(position);            

            //expand its influence by the texture halfWidth (aka radius) which would let us find all hexes which influence our chunk even with border of their texture
            float xMin = pos.x - Hex.hexTexturePotentialReach;
            float yMin = pos.y - Hex.hexTexturePotentialReach;
            float xMax = pos.x + Hex.hexTexturePotentialReach;
            float yMax = pos.y + Hex.hexTexturePotentialReach;
            
            List<Chunk> chunks = new List<Chunk>();

            Chunk c;
            c = Chunk.WorldToChunk(new Vector3(xMin, 0f, yMin));
            World.GetInstance().PrepareChunkData(c.position); 
            chunks.Add(c);
            c = Chunk.WorldToChunk(new Vector3(xMax, 0f, yMin));
            if (!chunks.Contains(c)) { World.GetInstance().PrepareChunkData(c.position); chunks.Add(c); }
            c = Chunk.WorldToChunk(new Vector3(xMin, 0f, yMax));
            if (!chunks.Contains(c)) { World.GetInstance().PrepareChunkData(c.position); chunks.Add(c); }
            c = Chunk.WorldToChunk(new Vector3(xMax, 0f, yMax));
            if (!chunks.Contains(c)) { World.GetInstance().PrepareChunkData(c.position); }

            World.GetInstance().ReadyToPolishHex(this);
            
        }

        /// <summary>
        /// Informs if hex lies next to the river
        /// </summary>
        /// <returns></returns>
        public bool IsNextToRiver()
        {
            return directionsPassingRiver.Count > 0;
        }


        /// <summary>
        /// Returns current hex visibility status
        /// </summary>
        /// <returns></returns>
        public Visibility GetVisibility()
        {
            return visibility;
        }


        /// <summary>
        /// Allows to set current visibility status and informs World Data Texture about this change (if needed)
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public void SetVisibility(Visibility v)
        {
            if (v != visibility)
            {
                visibility = v;
                
                //ensure fog of war knows that data have changed. so that it can update accordingly
                FogOfWar.AddDirtyHex(this);
            }
        }
    }
}