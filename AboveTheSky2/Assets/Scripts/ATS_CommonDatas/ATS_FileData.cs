using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ATS
{
    public class ATS_FileData
    {
        public static string GetFolderPath(System.Type iType) => $".Install/CommonData/{iType.Name}";

        /// <summary>
        /// 檔案格式(例如json)
        /// </summary>
        public string m_FileFormat = "json";
        /// <summary>
        /// 資料夾路徑
        /// </summary>
        [UCL.Core.PA.UCL_FolderExplorer(typeof(ATS_StreamingAssets), ATS_StreamingAssets.ReflectKeyStreamingAssetsPath)]
        public string m_FolderPath = string.Empty;
        public SearchOption m_SearchOption = SearchOption.TopDirectoryOnly;
        /// <summary>
        /// 緩存的檔案ID
        /// </summary>
        List<string> m_FileIDs = null;
        /// <summary>
        /// 緩存的檔名
        /// </summary>
        List<string> m_FileNames = null;
        #region static
        //static Dictionary<string, RCG_FileData> s_FileDatasDic = null;
        public static ATS_FileData GetFileData(string iFolderPath, string iFileFormat = "json")
        {
            return new ATS_FileData(iFolderPath, iFileFormat);
        }
        #endregion


        public ATS_FileData() { }
        public ATS_FileData(string iFolderPath, string iFileFormat = "json")
        {
            m_FolderPath = iFolderPath;
            m_FileFormat = iFileFormat;
        }
        /// <summary>
        /// 抓取所有檔名(不含副檔名
        /// </summary>
        /// <param name="iIsUseCache"></param>
        /// <returns></returns>
        public List<string> GetFileIDs(bool iIsUseCache = true)
        {
            if (m_FileIDs == null || !iIsUseCache)
            {
                ATS_StreamingAssets.CheckAndCreateDirectory(m_FolderPath);
                //Debug.LogError("m_FolderPath:" + m_FolderPath);
                m_FileIDs = ATS_StreamingAssets.GetFilesName(m_FolderPath, "*." + m_FileFormat);
            }
            return m_FileIDs;
        }
        public const string CommonDataMetaName = ".CommonDataMeta";
        public string CommonDataMetaPath => System.IO.Path.Combine(m_FolderPath, CommonDataMetaName);
        public string GetCommonDataMetaJson()
        {
            ATS_StreamingAssets.CheckAndCreateDirectory(m_FolderPath);
            if (!ATS_StreamingAssets.FileExists(CommonDataMetaPath))
            {
                return string.Empty;
            }
            return ATS_StreamingAssets.ReadAllText(CommonDataMetaPath);
        }
        public void SaveCommonDataMetaJson(string iJson)
        {
            ATS_StreamingAssets.CheckAndCreateDirectory(m_FolderPath);
            ATS_StreamingAssets.WriteAllText(CommonDataMetaPath, iJson);
        }
        /// <summary>
        /// 抓取所有檔名(含副檔名
        /// </summary>
        /// <param name="iIsUseCache"></param>
        /// <returns></returns>
        public List<string> GetFileNames(bool iIsUseCache = true)
        {
            if (m_FileNames == null || !iIsUseCache)
            {
                //RCG_StreamingAssets.CheckAndCreateDirectory(m_FolderPath);
                //Debug.LogError("m_FolderPath:" + m_FolderPath);
                m_FileNames = ATS_StreamingAssets.GetFilesName(m_FolderPath, "*." + m_FileFormat, false);
            }
            return m_FileNames;
        }
        /// <summary>
        /// 根據ID抓取檔案路徑(ID = 檔名去掉副檔名部分)
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public string GetSavePath(string iID)
        {
            return string.Format("{0}/{1}.{2}", m_FolderPath, iID, m_FileFormat);
        }
        /// <summary>
        /// 根據ID存檔
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iContents"></param>
        public void WriteAllText(string iID, string iContents)
        {
            //RCG_StreamingAssets.CheckAndCreateDirectory(m_FolderPath);
            ATS_StreamingAssets.WriteAllText(GetSavePath(iID), iContents);
        }

        /// <summary>
        /// 根據ID讀檔
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public string ReadAllText(string iID)
        {
            return ATS_StreamingAssets.ReadAllText(GetSavePath(iID));
        }
        /// <summary>
        /// 根據ID刪除檔案
        /// </summary>
        /// <param name="path"></param>
        public void DeleteFile(string iID)
        {
            ATS_StreamingAssets.DeleteFile(GetSavePath(iID));
        }
        /// <summary>
        /// 根據ID判斷檔案是否存在
        /// </summary>
        /// <param name="iPath"></param>
        /// <returns></returns>
        public bool FileExists(string iID)
        {
            return ATS_StreamingAssets.FileExists(GetSavePath(iID));
        }
        /// <summary>
        /// 抓取系統中實際檔案路徑
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public string GetFileSystemPath(string iID)
        {
            return ATS_StreamingAssets.GetFileSystemPath(GetSavePath(iID));
        }
    }
}
