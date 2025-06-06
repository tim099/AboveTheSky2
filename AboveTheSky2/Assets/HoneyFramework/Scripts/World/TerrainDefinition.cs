using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace HoneyFramework
{
    /*
     * terrain definition builds itself around MHTerrain source and loads required textures based on instructions from MHTerrain so that they may be refered directly later
     */
    public class TerrainDefinition
    {
        static public List<TerrainDefinition> definitions = new List<TerrainDefinition>();
        static public Dictionary<string, Color> colorLibrary = new Dictionary<string, Color>();

        public Texture diffuse = null;
        public Texture height = null;
        public Texture mixer = null;

        public MHTerrain source = null;        

        /// <summary>
        /// forces global reload of all terrain definitions. Enusres they up to date status
        /// </summary>
        /// <returns></returns>
        static public void ReloadDefinitions()
        {
            if (!DataManager.isInitialized)
            {
                DataManager.GetInstance();
                if (!DataManager.isInitialized)
                {
                    Debug.LogError("Data Manager is not initialized! no definitions would be loaded...");
                    return;
                }
            }

            definitions = new List<TerrainDefinition>();

            foreach (MHTerrain t in MHTerrain.list)
            {
                TerrainDefinition td = new TerrainDefinition();
                td.source = t;
                td.diffuse = LoadTexture(t.diffusePath);
                td.height = LoadTexture(t.heightPath);
                td.mixer = LoadTexture(t.mixerPath);
                definitions.Add(td);
            }
        }

        /// <summary>
        /// helper function which loads single texture from resource path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static public Texture2D LoadTexture(string path)
        {
            UnityEngine.Object o = Resources.Load(path);
            Texture2D t = o as Texture2D;

            return t;
        }

        /// <summary>
        /// translates string hex color value into color structure
        /// </summary>
        /// <param name="hexValue"></param>
        /// <returns></returns>
        static public Color GetColor(string hexValue)
        {
            if (colorLibrary.ContainsKey(hexValue))
            {
                return colorLibrary[hexValue];
            }
            int value = Convert.ToInt32(hexValue, 16);

            Color c = new Color(((value >> 16) & 0xFF) / 255.0f,
                                ((value >> 8) & 0xFF) / 255.0f,
                                ((value) & 0xFF) / 255.0f);

            colorLibrary[hexValue] = c;

            return c;
        }

        /// <summary>
        /// Gets color build around 
        /// </summary>
        /// <param name="hexValue"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        static public Color GetRandomizedColor(string hexValue, float radius)
        {
            Color c = GetColor(hexValue);
            radius /= 255.0f;
            float rScalar = 1.0f + UnityEngine.Random.Range(-radius, radius);
            float gScalar = 1.0f + UnityEngine.Random.Range(-radius, radius);
            float bScalar = 1.0f + UnityEngine.Random.Range(-radius, radius);

            return new Color(Mathf.Clamp01(c.r * rScalar), Mathf.Clamp01(c.g * gScalar), Mathf.Clamp01(c.b * bScalar));

        }

    }
}