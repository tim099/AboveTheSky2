using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HoneyFramework
{    
    /*
     *  Settings which change for different quality settings
     */
    public class QualityOptions
    {
        public string name = "unknown";        
        public int chunkTextureSize = 2048;
    }

    /*
     *  single setting instance. It is possible to have multiple settings collection (eg per profile)
     *  allows to configure game based on number of factors
     */
    public class MHGameSettings : MHType
    {
        public enum DataName
        {            
            ChunkTextureSize,
        }
                
        public List<QualityOptions> qualitySettings = new List<QualityOptions>();

        public int worldSeed = 0;
        public bool dx11Mode = true;
        public bool markers = true;

        static public List<MHGameSettings> list;

        /// <summary>
        /// Gets instance or creates one of none is found
        /// </summary>
        /// <returns></returns>
        static public MHGameSettings GetInstance()
        {
            if (list == null)
            {
                list = new List<MHGameSettings>();
            }

            if (list.Count < 1)
            {
                MHGameSettings gs = new MHGameSettings();                

                list.Add(gs);
                MHDatabase.SaveDB<MHGameSettings>();
            }

            return list[0];
        }

        /// <summary>
        /// Returns seed used to generate world
        /// </summary>
        /// <returns></returns>
        static public int GetWorldSeed()
        {
            if (!DataManager.isInitialized)
            {
                Debug.LogError("using uninitialized settings!");
                return 0;
            }
            if (list.Count == 0) return 0;

            return list[0].worldSeed;
        }

        /// <summary>
        /// Informs if user prefers to run in DX11 mode
        /// </summary>
        /// <returns></returns>
        static public bool GetDx11Mode()
        {
            if (!DataManager.isInitialized)
            {
                Debug.LogError("using uninitialized settings!");
                return false;
            }
            if (list.Count == 0) return false;

            return list[0].dx11Mode;
        }

        /// <summary>
        /// Should we use markers (dx11 mode only for now)
        /// </summary>
        /// <returns></returns>
        static public bool GetMarkersMode()
        {
            if (!DataManager.isInitialized)
            {
                Debug.LogError("using uninitialized settings!");
                return false;
            }
            if (list.Count == 0) return false;

            return list[0].markers;
        }

        /// <summary>
        /// Used for predefined settings from current quality settings.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static public T GetSetting<T>(DataName name)
        {
            if (!DataManager.isInitialized)
            {
                Debug.LogError("using uninitialized settings!");
                return default(T);
            }

            int qualityLevel = QualitySettings.GetQualityLevel();
            if (qualityLevel < 0) qualityLevel = 0;
            string qName = QualitySettings.names[qualityLevel];

            QualityOptions qSettings = MHGameSettings.GetInstance().qualitySettings.Find(o => o.name == qName);
            if (qSettings == null)
            {
                qSettings = new QualityOptions();
                qSettings.name = qName;
#if UNITY_EDITOR
                Debug.LogWarning("Selected Quality settings not found! Default quality settings used and would be saved under requested name");

                MHGameSettings.GetInstance().qualitySettings.Add(qSettings);
                MHDatabase.SaveDB<MHGameSettings>();
#endif
            }

            switch (name)
            {                
                case DataName.ChunkTextureSize:
                    return (T)Convert.ChangeType(qSettings.chunkTextureSize, typeof(T));
                default:
                    return default(T);
            }            

        }
    }
}
