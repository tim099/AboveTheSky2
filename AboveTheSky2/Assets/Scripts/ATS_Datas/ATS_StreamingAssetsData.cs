
// ATS_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 02/20 2024 19:03

// ATS_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 02/20 2024 18:49

// RCG_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 09/26 2023
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UCL.Core;
using UCL.Core.JsonLib;
using UnityEngine;
using UnityEngine.Networking;

namespace ATS
{
    public class ATS_StreamingAssetsData : UCL.Core.JsonLib.UnityJsonSerializable
    {
        [UCL.Core.PA.UCL_FolderExplorer(typeof(UCL_StreamingAssets), UCL_StreamingAssets.ReflectKeyStreamingAssetsPath)]
        public string m_FolderPath;

        public List<string> GetAllFileNames()
        {
            var aFileDatas = UCL_StreamingAssetsFileData.GetFileData(m_FolderPath, "*");
            List<string> aIconPaths = new List<string>() { string.Empty };//可選空的
            aIconPaths.Append(aFileDatas.GetFileNames());
            return aIconPaths;
        }

        /// <summary>
        /// 檔案名稱
        /// </summary>
        [UCL.Core.PA.UCL_List("GetAllFileNames")]
        public string m_FileName = string.Empty;

        public ATS_StreamingAssetsData() { }
        public ATS_StreamingAssetsData(string iFolderPath, string iName = "")
        {
            m_FolderPath = iFolderPath;
            m_FileName = iName;
        }
        //public override void DeserializeFromJson(JsonData iJson)
        //{
        //    base.DeserializeFromJson(iJson);
        //    m_FolderPath = m_FolderPath.Replace("Install/AudioClips", "Install/.AudioClips");
        //}
        public virtual string Path => System.IO.Path.Combine(m_FolderPath, m_FileName);
        public bool IsEmpty => string.IsNullOrEmpty(m_FileName);
        public string Key => $"{m_FolderPath}_{m_FileName}";


        public async UniTask<byte[]> ReadAllBytesAsync(CancellationToken iToken)
        {
            var aPath = Path;

            byte[] aBytes;

            //Check if we should use UnityWebRequest or File.ReadAllBytes
            if (aPath.Contains("://") || aPath.Contains(":///"))//Android
            {
                UnityWebRequest aUnityWebRequest = UnityWebRequest.Get(aPath);
                await aUnityWebRequest.SendWebRequest();
                iToken.ThrowIfCancellationRequested();
                aBytes = aUnityWebRequest.downloadHandler.data;
            }
            else
            {
                aBytes = File.ReadAllBytes(aPath);
            }
            return aBytes;
        }
        /// <summary>
        /// 取代BetterStreamingAssets.ReadAllText
        /// 根據情況自動切換讀取方式
        /// </summary>
        /// <param name="iPath"></param>
        /// <returns></returns>
        public byte[] ReadAllBytes()
        {
            return UCL_StreamingAssets.ReadAllBytes(Path);
        }

        public string ReadAllText()
        {
            return UCL_StreamingAssets.ReadAllText(Path);
        }
    }
}
