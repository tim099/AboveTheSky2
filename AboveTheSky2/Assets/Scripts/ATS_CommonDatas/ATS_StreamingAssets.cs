
// ATS_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 02/20 2024 18:33

using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

namespace ATS
{
    public static class ATS_StreamingAssets
    {
        public const string ReflectKeyStreamingAssetsPath = "StreamingAssetsPath";
        /// <summary>
        /// 根目錄位置
        /// </summary>
        public static string StreamingAssetsPath
        {
            get
            {
                if(s_StreamingAssetsPath == null)
                {
                    if (Application.platform == RuntimePlatform.WindowsEditor)//Editor環境不安裝 直接抓Application.streamingAssetsPath
                    {
                        s_StreamingAssetsPath = Application.streamingAssetsPath;
                    }
                    else//其他平台安裝到 Application.dataPath/Install
                    {
                        s_StreamingAssetsPath = Application.streamingAssetsPath;//暫時沒有安裝功能 先用舊的
                        //s_StreamingAssetsPath = Application.dataPath + "/Install";
                    }
                }

                return s_StreamingAssetsPath;
            }
        }
        static string s_StreamingAssetsPath = null;

        /// <summary>
        /// 檢查安裝是否完成
        /// </summary>
        public static void CheckInstall()
        {


        }
        /// <summary>
        /// 將路徑轉為FileSystem路徑
        /// </summary>
        /// <param name="iPath"></param>
        /// <returns></returns>
        public static string GetFileSystemPath(string iPath)
        {
            return StreamingAssetsPath + "/" + iPath;
        }
        /// <summary>
        /// 取代BetterStreamingAssets.ReadAllText
        /// 根據情況自動切換讀取方式
        /// </summary>
        /// <param name="iPath"></param>
        /// <returns></returns>
        public static string ReadAllText(string iPath)
        {
            return File.ReadAllText(GetFileSystemPath(iPath));
            //return BetterStreamingAssets.ReadAllText(iPath);
        }
        /// <summary>
        /// 取代BetterStreamingAssets.ReadAllText
        /// 根據情況自動切換讀取方式
        /// </summary>
        /// <param name="iPath"></param>
        /// <returns></returns>
        public static async UniTask<string> ReadAllTextAsync(string iPath, CancellationToken cancellationToken = default)
        {
            return await File.ReadAllTextAsync(GetFileSystemPath(iPath), cancellationToken);
        }
        /// <summary>
        /// 取代BetterStreamingAssets.ReadAllText
        /// 根據情況自動切換讀取方式
        /// </summary>
        /// <param name="iPath"></param>
        /// <returns></returns>
        public static byte[] ReadAllBytes(string iPath)
        {
            var aPath = GetFileSystemPath(iPath);
            if (!File.Exists(aPath))
            {
                Debug.LogError("ReadAllBytes !File.Exists aPath:" + aPath);
                return null;
            }
            return File.ReadAllBytes(aPath);
            //return BetterStreamingAssets.ReadAllBytes(iPath);
        }
        /// <summary>
        /// 取代BetterStreamingAssets.ReadAllText
        /// 根據情況自動切換讀取方式
        /// </summary>
        /// <param name="iPath"></param>
        /// <returns></returns>
        public static async UniTask<byte[]> ReadAllBytesAsync(string iPath, CancellationToken iToken = default)
        {
            var aPath = GetFileSystemPath(iPath);
            if (!File.Exists(aPath))
            {
                Debug.LogError("ReadAllBytes !File.Exists aPath:" + aPath);
                return null;
            }
            return await File.ReadAllBytesAsync(aPath, iToken);
        }
        public static async UniTask<byte[]> ReadAllBytesAsyncThreaded(string iPath, CancellationToken iToken = default)
        {
            bool aLoaded = false;
            byte[] aBytes = null;
            System.Threading.ThreadPool.QueueUserWorkItem(o =>
            {
                try
                {
                    aBytes = ATS_StreamingAssets.ReadAllBytes(iPath);
                }
                catch (System.Exception e)
                {
                    Debug.LogException(e);
                }
                finally
                {
                    aLoaded = true;
                }
            });
            await UniTask.WaitUntil(() => aLoaded, cancellationToken: iToken);
            iToken.ThrowIfCancellationRequested();
            return aBytes;
        }
        /// <summary>
        /// 取代BetterStreamingAssets.FileExists
        /// 根據情況自動切換讀取方式
        /// </summary>
        /// <param name="iPath"></param>
        /// <returns></returns>
        public static bool FileExists(string iPath)
        {
            return File.Exists(GetFileSystemPath(iPath));
            //return BetterStreamingAssets.FileExists(iPath);
        }
        public static bool DirectoryExists(string iPath)
        {
            return Directory.Exists(GetFileSystemPath(iPath));
        }
        /// <summary>
        /// Create Directory if not exist
        /// </summary>
        /// <param name="iPath"></param>
        public static void CheckAndCreateDirectory(string iPath)
        {
            string aPath = GetFileSystemPath(iPath);
            if (!Directory.Exists(aPath))
            {
                Directory.CreateDirectory(aPath);
            }
        }
        /// <summary>
        /// 刪除檔案 限定PC環境(非PC環境需要先安裝)
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFile(string iPath)
        {
            string aPath = GetFileSystemPath(iPath);
            if (!File.Exists(aPath))
            {
                Debug.LogError("FileDelete Fail!!aPath:" + aPath);
                return;
            }
#if UNITY_EDITOR
            string aMetaPath = aPath + ".meta";
            if (File.Exists(aMetaPath)) File.Delete(aMetaPath);
#endif
            File.Delete(aPath);
        }
        /// <summary>
        /// 取代BetterStreamingAssets.GetFiles
        /// 根據情況自動切換讀取方式
        /// </summary>
        /// <param name="iPath"></param>
        /// <returns></returns>
        public static string[] GetFiles(string iPath)
        {
            return Directory.GetFiles(GetFileSystemPath(iPath));
            //return BetterStreamingAssets.GetFiles(iPath);
        }
        /// <summary>
        /// 取代BetterStreamingAssets.GetFiles
        /// 根據情況自動切換讀取方式
        /// </summary>
        /// <param name="iPath"></param>
        /// <returns></returns>
        public static string[] GetFiles(string iPath, string iSearchPattern)
        {
            return Directory.GetFiles(GetFileSystemPath(iPath), iSearchPattern);
            //return BetterStreamingAssets.GetFiles(iPath, iSearchPattern);
        }
        /// <summary>
        /// 取代BetterStreamingAssets.GetFiles
        /// 根據情況自動切換讀取方式
        /// </summary>
        /// <param name="iPath"></param>
        /// <param name="iSearchPattern"></param>
        /// <param name="iSearchOption"></param>
        /// <returns></returns>
        public static string[] GetFiles(string iPath, string iSearchPattern, SearchOption iSearchOption)
        {
            return Directory.GetFiles(GetFileSystemPath(iPath), iSearchPattern, iSearchOption);
            //return BetterStreamingAssets.GetFiles(iPath, iSearchPattern, iSearchOption);
        }

        /// <summary>
        /// 抓取檔名(不含路徑 排除.meta)
        /// </summary>
        /// <param name="iPath">路徑</param>
        /// <param name="iSearchPattern"></param>
        /// <param name="iIsRemoveExtension">是否移除附檔名</param>
        /// <returns></returns>
        public static List<string> GetFilesName(string iPath, string iSearchPattern, bool iIsRemoveExtension = true)
        {
            if (!DirectoryExists(iPath))
            {
                return new List<string>();
            }
            var aFolderPath = GetFileSystemPath(iPath);
            var aFiles = Directory.GetFiles(aFolderPath, iSearchPattern, SearchOption.TopDirectoryOnly);
            var aFileNames = new List<string>();
            if (aFiles != null)
            {
                int aDiscardLen = aFolderPath.Length + 1;
                for (int i = 0; i < aFiles.Length; i++)
                {
                    var aPath = aFiles[i];
                    if(aPath.Substring(aPath.Length - 5, 5) != ".meta")
                    {
                        string aFileName = aPath.Substring(aDiscardLen, aPath.Length - aDiscardLen);
                        if(iIsRemoveExtension) aFileName = UCL.Core.FileLib.Lib.RemoveFileExtension(aFileName);
                        aFileNames.Add(aFileName);
                    }
                }
            }
            return aFileNames;
            //return BetterStreamingAssets.GetFiles(iPath, iSearchPattern, iSearchOption);
        }

        /// <summary>
        /// 限定PC環境的StreamingAsset寫檔
        /// </summary>
        /// <param name="iPath"></param>
        /// <param name="iContents"></param>
        public static void WriteAllText(string iPath, string iContents)
        {
            string aPath = GetFileSystemPath(iPath);
            File.WriteAllText(aPath, iContents);
        }
    }
}

