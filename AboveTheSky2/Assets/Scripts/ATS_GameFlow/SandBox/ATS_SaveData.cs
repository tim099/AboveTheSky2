
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UCL.Core.JsonLib;
using UnityEngine;

namespace ATS
{
    public class ATS_SaveData
    {
        public string m_Dir;

        public Dictionary<string, JsonData> m_Files = new Dictionary<string, JsonData>();
        /// <summary>
        /// Dir
        /// </summary>
        public Dictionary<string, ATS_SaveData> m_Dirs = new();

        public static string FileName(string iKey) => $"{iKey}.json";

        public ATS_SaveData() { }
        public ATS_SaveData(string iDir)
        {
            m_Dir = iDir;
        }

        public void AddFile(string key, JsonData jsonData)
        {
            if (jsonData == null)
            {
                return;
            }
            if (m_Files.ContainsKey(key))
            {
                Debug.LogError($"SaveData.AddFile key:{key}, m_Files.ContainsKey(key)");
                return;
            }
            m_Files[key] = jsonData;
        }
        public JsonData LoadFile(string key, bool errorLogIfNotExist = true)
        {
            string aPath = Path.Combine(m_Dir, FileName(key));
            if (!File.Exists(aPath))
            {
                if (errorLogIfNotExist)
                {
                    Debug.LogError($"SaveData.LoadFile, !File.Exists(aPath) aPath:{aPath}");
                }
                return null;
            }
            string aJson = File.ReadAllText(aPath);
            return JsonData.ParseJson(aJson);
        }

        public void AddFolder(string key, ATS_SaveData saveData)
        {
            if (saveData == null)
            {
                return;
            }
            if (m_Dirs.ContainsKey(key))
            {
                Debug.LogError($"SaveData.AddFolder key:{key}, m_Dirs.ContainsKey(key)");
                return;
            }
            m_Dirs[key] = saveData;
        }
        public ATS_SaveData LoadFolder(string key)
        {
            string aPath = Path.Combine(m_Dir, key);
            if (!Directory.Exists(aPath))
            {
                Debug.LogError($"SaveData.LoadFolder, !Directory.Exists(aPath) aPath:{aPath}");
                return null;
            }
            return new ATS_SaveData(aPath);
        }
        public void Save(string dir)
        {
            Debug.LogError($"Save dir:{dir}");
            Directory.CreateDirectory(dir);

            if (m_Files.Count > 0)
            {
                foreach (var key in m_Files.Keys)
                {
                    var json = m_Files[key];
                    string savePath = Path.Combine(dir, FileName(key));
                    File.WriteAllText(savePath, json.ToJsonBeautify());
                }
            }
            if (m_Dirs.Count > 0)
            {
                foreach (var key in m_Dirs.Keys)
                {
                    var save = m_Dirs[key];
                    save.Save(Path.Combine(dir, key));
                }
            }

        }
        //public void Load(string dir)
        //{
        //    if (!Directory.Exists(dir))
        //    {
        //        Debug.LogError($"SaveData.Load !Directory.Exists(dir), dir:{dir}");
        //        return;
        //    }
        //    var aDirs = Directory.GetDirectories(dir);

        //}
    }
}
