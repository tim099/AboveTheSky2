using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;

namespace HoneyFramework
{
    /*
     * System which works with advanced chunk rendering shaders to draw hex markers and selectors
     */
    public class HexMarkers : MonoBehaviour
    {
        public enum Layer
        {
            Borders, 
            Friendly, 
            Movement,
            Extra,
        }

        public enum MarkerType
        {
            None     = 0,
            Friendly = 1,  //Layer Friendly
            Enemy    = 2,  //Layer Friendly
            Movement = 3,  //Layer Movement   

            HalfRoad = 8,
        
            //its up to you to name markers you want to use, 
            //its important to define texture size though
            MAX = 64
        }

        static public float[] directionZeroOneScale = new float[] { 0f,
                                                               1f / 6f,
                                                               2f / 6f,
                                                               3f / 6f,
                                                               4f / 6f,
                                                               5f / 6f};

        static public HexMarkers instance;

        float markerTypeCount = (float)MarkerType.MAX;
        static int textureMapSize = 64;
        static int dataSize = 2;
        public Texture2D markersTexture;        
        Texture2D hexData;
        Color32[] colorData;
        bool dirty;

        void Awake()
        {
            instance = this;

            hexData = new Texture2D(textureMapSize * dataSize, textureMapSize * dataSize, TextureFormat.ARGB32, false, false);
            hexData.filterMode = FilterMode.Point;

            colorData = new Color32[textureMapSize * textureMapSize * dataSize * dataSize];
            hexData.SetPixels32(colorData);
            hexData.Apply();
            dirty = false;
        }

        /// <summary>
        /// Markers contain textures which are used to color selected hexes
        /// </summary>
        /// <returns></returns>
        static public Texture2D GetMarkersTexture()
        {
            return instance.markersTexture;
        }

        /// <summary>
        /// Data which defines which hexes should be marked by the system
        /// </summary>
        /// <returns></returns>
        static public Texture2D GetHexDataTexture()
        {
            return instance.hexData;
        }

        static public Vector4 GetMarkersSettings()
        {
            //x - horizontal texture count on marker atlas
            //y - vertical texture count on marker atlas
            //z - width of hex data texture without counting data size
            //w - data size per hex
            return new Vector4(8f, 8f, textureMapSize, dataSize);
        }

        /// <summary>
        /// Marks the Texture as dirty so its updated at the next render
        /// </summary>
        /// <returns></returns>
        static public void MarkAsDirtyDataTexture()
        {
            if (instance != null)
            {
                instance.dirty = true;
            }
        }

        /// <summary>
        /// Clears All markers
        /// </summary>
        /// <returns></returns>
        static public void ClearAllMarkers()
        {
            if (instance != null)
            {
                for (int i = 0; i < instance.colorData.Length; i++)
                    instance.colorData[i] = Color.black;
                instance.hexData.SetPixels32(instance.colorData);
                instance.dirty = true;
            }
        }

        /// <summary>
        /// Allows to store hex value in texture data later copied to texture used by chunks
        /// </summary>
        /// <param name="position"></param>
        /// <param name="type">0 to 8 value which defines which hex to use. Note that 0 is "use nothing" type, indexes start at 1 to 8 including</param>
        /// <returns></returns>
        static public void SetMarkerType(Vector3i position, MarkerType type)
        {
            if (instance != null)
            {
                switch (type)
                {
                    case MarkerType.Friendly:
                    case MarkerType.Enemy:
                        instance.SetMarkerType(position, type, Layer.Friendly, 0f);
                        break;

                    case MarkerType.Movement:
                        instance.SetMarkerType(position, type, Layer.Movement, 0f);
                        break;
                }   
            }
        }

        static public void SetMarkerType(Vector3i position, int type, Layer layer, float rotation)
        {
            if (instance != null)
            {
                instance.SetMarkerType(position, layer, rotation, type);
            }
        }

        static public void ClearMarkerLayer(Vector3i position, Layer layer)
        {
            if (instance != null)
            {                
                instance.SetMarkerType(position, MarkerType.None, layer, 0f);
            }
        }

        static public void ClearMarkerType(Vector3i position, MarkerType type)
        {
            if (instance != null)
            {                
                switch (type)
                {                    
                    case MarkerType.Friendly:
                    case MarkerType.Enemy:
                        instance.SetMarkerType(position, MarkerType.None, Layer.Friendly, 0f);
                        break;

                    case MarkerType.Movement:
                        instance.SetMarkerType(position, MarkerType.None, Layer.Movement, 0f);
                        break;
                }                
            }
        }

        /// <summary>
        /// Places marker at certain position, layer and requested rotation of the object
        /// </summary>
        /// <param name="position"></param>
        /// <param name="type">enum type of the marker</param>
        /// <param name="layer">layer to place marker on</param>
        /// <param name="markerRotation">Radians rotation of the texture</param>
        /// <returns></returns>
        public void SetMarkerType(Vector3i position, MarkerType type, Layer layer, float markerRotation)
        {
            SetMarkerType(position, layer, markerRotation, (int)type);
        }

        /// <summary>
        /// int based version of the set marker which have rotated input variables to avoid visual studio confusion
        /// </summary>
        /// <param name="position"></param>
        /// <param name="layer"></param>
        /// <param name="markerRotation"> 0-1 value </param>
        /// <param name="iType"></param>
        /// <returns></returns>
        public void SetMarkerType(Vector3i position, Layer layer, float markerRotation, int iType)
        {           
            //convert position to texture space index
            int x = position.x * dataSize;
            int y = position.y * dataSize;

            if (x < 0) { x = textureMapSize * dataSize + x; }
            if (y < 0) { y = textureMapSize * dataSize + y; }

            Color c = hexData.GetPixel(x, y);            
            Color rotations = hexData.GetPixel(x + 1, y);

            switch (layer)
            {
                case Layer.Borders:
                    c.r = (float)iType / markerTypeCount;  
                    rotations.r = markerRotation;
                    break;

                case Layer.Friendly:
                    c.g = (float)iType / markerTypeCount;
                    rotations.g = markerRotation;
                    break;

                case Layer.Movement:
                    c.b = (float)iType / markerTypeCount;
                    rotations.b = markerRotation;
                    break;

                case Layer.Extra:
                    c.a = (float)iType / markerTypeCount;
                    rotations.a = markerRotation;
                    break;
            }            
            hexData.SetPixel(x, y, c);
            hexData.SetPixel(x + 1, y, rotations);
            
            dirty = true;            
        }		

        void LateUpdate()
        {
            if (dirty)
            {
               // hexData.SetPixels32(colorData);
                hexData.Apply();

                foreach (KeyValuePair<Vector2i, Chunk> pair in World.instance.chunks)
                {
                    pair.Value.SetMarkerMaterials();
                }
                dirty = false;
            }
        }

    }
}