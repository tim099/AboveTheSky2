
// ATS_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 02/20 2024 19:50
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UCL.Core.JsonLib;
using UCL.Core.LocalizeLib;
using UnityEngine;

namespace ATS
{

    public interface ATSI_CommonData : UCLI_CommonEditable, UCLI_Preview
    {
        string FolderPath { get; }
        ATSI_CommonData CreateCommonData(string iID);
        List<string> GetAllIDs();
        /// <summary>
        /// 根據ID抓取物品設定
        /// </summary>
        /// <param name="iID">ID</param>
        /// <param name="iUseCache">使否使用緩存的資料</param>
        /// <returns></returns>
        ATSI_CommonData GetCommonData(string iID, bool iUseCache = true);
        void RefreshAllDatas();
        /// <summary>
        /// 生成一個編輯選單頁面(用來選取要編輯的物品)
        /// </summary>
        void CreateSelectPage();

        #region static
        /// <summary>
        /// 當前正在CreateData的RCGI_CommonData
        /// </summary>
        public static ATSI_CommonData s_CurCreateData = null;
        public static ATSI_CommonData GetUtilByType(System.Type iType)
        {
            var aPropInfoUtil = iType.GetProperty("Util", BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            if (aPropInfoUtil != null)
            {
                MethodInfo[] aMethodInfos = aPropInfoUtil.GetAccessors();
                //aMethInfosStr = $",MethodInfos:{aMethodInfos.ConcatString(iMethod => iMethod.Name)}";
                if (aMethodInfos.Length > 0)
                {
                    try
                    {
                        MethodInfo aMethodInfo = aMethodInfos[0];
                        var aUtil = aMethodInfo.Invoke(null, null);//Get Util
                        return aUtil as ATSI_CommonData;
                    }
                    catch (Exception iE)
                    {
                        Debug.LogError($"RCGI_CommonData.GetUtilByType aType:{iType.FullName},Exception:{iE}");
                        Debug.LogException(iE);
                    }
                }
            }
            Debug.LogError($"RCGI_CommonData.GetUtilByType aType:{iType.FullName},Fail!!");
            return default;
        }
        private static List<Type> s_AllCommonDataTypes = null;
        /// <summary>
        /// 抓取所有CommonData的Type
        /// </summary>
        /// <returns></returns>
        public static List<Type> GetAllCommonDataTypes()
        {
            if(s_AllCommonDataTypes == null)
            {
                var aAllTypes = typeof(ATSI_CommonData).GetAllITypesAssignableFrom();
                s_AllCommonDataTypes = new List<Type>();
                foreach (var aType in aAllTypes)
                {
                    if (aType.IsGenericType || aType.IsInterface)
                    {
                        continue;
                    }
                    s_AllCommonDataTypes.Add(aType);
                }
            }

            return s_AllCommonDataTypes;
        }
        public static void RefreshAllCommonDatasWithReflection()
        {
            //RCG_GameInitData.Ins.Save();
            //HashSet<Type> aIgnoreTypes = new HashSet<Type>() { typeof(RCG_CommonTag)};
            foreach (var aType in GetAllCommonDataTypes())
            {
                try
                {
                    string aPropInfosStr = string.Empty;
                    try
                    {
                        ATSI_CommonData aUtil = GetUtilByType(aType);//Get Util
                        if (aUtil != null)
                        {
                            //aMethInfosStr += $"Result:{aUtil.GetType().FullName}";
                            aUtil.RefreshAllDatas();
                            Debug.LogWarning($"Util:{aUtil.GetType().FullName}.RefreshAllDatas()");
                        }
                    }
                    catch (Exception iE)
                    {
                        Debug.LogError($"RCGI_CommonData aType:{aType.FullName},Exception:{iE}");
                        Debug.LogException(iE);
                    }
                }
                catch (Exception iE)
                {
                    Debug.LogException(iE);
                }

            }
        }


        #endregion
    }
    public class ATS_CommonData<T> : ATS_Util<T>, ATSI_CommonData where T : class, ATSI_CommonData, UCLI_CommonEditable, new()
    {
        #region must override 一定要override的部份
        //public static string GetFolderPath(System.Type iType) => $"Install/.CommonData/{iType.Name.Replace("RCG_", string.Empty)}";

        /// <summary>
        /// 檔案路徑
        /// </summary>
        virtual public string FolderPath => ATS_FileData.GetFolderPath(GetType());

        virtual public string StreamingAssetFolderPath => Path.Combine(Application.streamingAssetsPath, FolderPath);
        virtual public ATSI_CommonData CreateCommonData(string iID) => CreateData(iID) as ATSI_CommonData;

        /// <summary>
        /// 預覽
        /// </summary>
        /// <param name="iIsShowEditButton"></param>
        virtual public void Preview(UCL.Core.UCL_ObjectDictionary iDataDic, bool iIsShowEditButton = false)
        {
            GUILayout.BeginHorizontal();
            using (var aScope = new GUILayout.VerticalScope("box", GUILayout.MinWidth(130)))
            {
                GUILayout.Label($"{UCL_LocalizeManager.Get("Preview")}({ID})", UCL.Core.UI.UCL_GUIStyle.LabelStyle);

                if (iIsShowEditButton)
                {
                    if (GUILayout.Button(UCL_LocalizeManager.Get("Edit")))
                    {
                        Page.ATS_CommonEditPage.Create(this);
                    }
                }
            }
            GUILayout.EndHorizontal();
        }
        /// <summary>
        /// 繪製編輯介面
        /// </summary>
        /// <param name="iDataDic"></param>
        virtual public void OnGUI(UCL.Core.UCL_ObjectDictionary iDataDic)
        {
            GUILayout.BeginVertical();

            using (var scope = new GUILayout.VerticalScope("box"))//, GUILayout.Width(500)
            {
                UCL.Core.UI.UCL_GUILayout.DrawObjectData(this, iDataDic, string.Empty, true, ATS_StaticFunctions.LocalizeFieldName);
            }
            using (new GUILayout.VerticalScope("box"))//預覽
            {
                bool aIsShow = false;
                using (new GUILayout.HorizontalScope())
                {
                    aIsShow = UCL.Core.UI.UCL_GUILayout.Toggle(iDataDic, "ShowPreview", iDefaultValue: true);
                    UCL.Core.UI.UCL_GUILayout.LabelAutoSize(UCL_LocalizeManager.Get("Preview"));
                }

                if (aIsShow)
                {
                    using (new GUILayout.VerticalScope(GUILayout.Width(200)))
                    {
                        Preview(iDataDic.GetSubDic("Preview"), false);
                    }
                }
            }

            GUILayout.EndVertical();
        }
        #endregion

        #region FakeStatic

        /// <summary>
        /// 生成一個編輯選單頁面(用來選取要編輯的物品)
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, T> s_DataDic = null;


        virtual public T CreateData(string iID)
        {
            if (!ContainsID(iID))
            {
                string aLog = $"Create {GetType().Name} ID: {iID},Not Exist!!";
                Debug.LogError(aLog);
                //throw new System.Exception(aLog);
                return null;
            }
            var aData = new T();
            ATSI_CommonData.s_CurCreateData = aData;
            var aTmp = aData as ATS_CommonData<T>;
            aTmp.Init(iID);
            ATSI_CommonData.s_CurCreateData = null;
            return aData;
        }

        /// <summary>
        /// 根據ID抓取物品設定
        /// </summary>
        /// <param name="iID">ID</param>
        /// <param name="iUseCache">使否使用緩存的資料</param>
        /// <returns></returns>
        virtual public T GetData(string iID, bool iUseCache = true)
        {
            if (string.IsNullOrEmpty(iID))
            {
                Debug.LogError("GetData, string.IsNullOrEmpty(iID)");
                return default;
            }
            if (!iUseCache)
            {
                return CreateData(iID);
            }
            if (s_DataDic == null) s_DataDic = new Dictionary<string, T>();
            if (!s_DataDic.ContainsKey(iID))
            {
                s_DataDic[iID] = CreateData(iID);
            }
            return s_DataDic[iID];
        }
        /// <summary>
        /// 根據ID抓取物品設定
        /// </summary>
        /// <param name="iID">ID</param>
        /// <param name="iUseCache">使否使用緩存的資料</param>
        /// <returns></returns>
        virtual public ATSI_CommonData GetCommonData(string iID, bool iUseCache = true) => GetData(iID, iUseCache);
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="iID"></param>
        public void Delete(string iID)
        {
            FileDatas.DeleteFile(iID);
            ClearCache();
        }
        /// <summary>
        /// 生成一個編輯選單頁面(用來選取要編輯的物品)
        /// </summary>
        virtual public void CreateSelectPage()
        {
            CreateCommonSelectPage();
        }
        /// <summary>
        /// 生成一個編輯選單頁面(用來選取要編輯的物品)
        /// </summary>
        /// <returns></returns>
        virtual public Page.ATS_CommonSelectPage<T> CreateCommonSelectPage()
        {
            return Page.ATS_CommonSelectPage<T>.Create();
        }
        #endregion


        private UCL.Core.UCL_ObjectDictionary m_PreviewDic = null;
        protected UCL.Core.UCL_ObjectDictionary PreviewDic
        {
            get
            {
                if (m_PreviewDic == null) m_PreviewDic = new UCL.Core.UCL_ObjectDictionary();
                return m_PreviewDic;
            }
        }


        virtual public void Init(string iID)
        {
            ID = iID;
            try
            {
                if (FileDatas.FileExists(ID))
                {
                    string aData = FileDatas.ReadAllText(ID);
                    DeserializeFromJson(JsonData.ParseJson(aData));
                }
                else
                {
                    Debug.LogError("Init ID:" + iID + ",file not exist!!");
                }
            }
            catch (System.Exception iE)
            {
                Debug.LogException(iE);
            }

        }

        /// <summary>
        /// 存檔路徑
        /// </summary>
        virtual public string SavePath => FileDatas.GetFileSystemPath(ID);

        /// <summary>
        /// 檢查物品是否存在
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public bool ContainsID(string iID) => FileDatas.FileExists(iID);
        virtual public ATS_FileData FileDatas => ATS_FileData.GetFileData(FolderPath);

        virtual public JsonData Save()
        {
            var aJson = SerializeToJson();
            FileDatas.WriteAllText(ID, aJson.ToJsonBeautify());
            //Debug.LogError("Save:" + ID);
            ClearCache();
            return aJson;
        }
        /// <summary>
        /// 根據ID刪除資料
        /// </summary>
        /// <param name="iID"></param>
        //virtual public void Delete(string iID)
        //{
        //    Util
        //}
        /// <summary>
        /// 清除緩存
        /// </summary>
        virtual public void ClearCache()
        {
            if (this != Util) Util.ClearCache();
            if (s_DataDic != null) s_DataDic.Clear();
            //s_DataDic = null;
            //Debug.LogError("ClearCache:" + ID);
        }
        virtual public string ID { get; set; }


        private CommonDataMeta m_CommonDataMeta = null;
        public CommonDataMeta CommonDataMetaIns
        {
            get
            {
                if (m_CommonDataMeta == null)
                {
                    m_CommonDataMeta = CreateCommonDataMeta();
                }
                return m_CommonDataMeta;
            }
        }
        virtual public CommonDataMeta CreateCommonDataMeta()
        {
            CommonDataMeta aCommonDataMeta = new CommonDataMeta();
            aCommonDataMeta.Init(FileDatas);

            string aJson = FileDatas.GetCommonDataMetaJson();
            if (!string.IsNullOrEmpty(aJson))
            {
                JsonData aData = JsonData.ParseJson(aJson);
                aCommonDataMeta.DeserializeFromJson(aData);
            }

            return aCommonDataMeta;
        }
        /// <summary>
        /// 抓取此資料所屬的分組
        /// </summary>
        /// <returns></returns>
        public string GroupID
        {
            get
            {
                var aMeta = CommonDataMetaIns.GetFileMeta(ID);
                return aMeta.m_Group;
            }
        }


        /// <summary>
        /// 抓取所有資料的ID(有緩存)
        /// </summary>
        /// <returns></returns>
        virtual public List<string> GetAllIDs()
        {
            return FileDatas.GetFileIDs();
        }

        /// <summary>
        /// 抓取所有可編輯資料的ID
        /// </summary>
        /// <returns></returns>
        virtual public List<string> GetEditableIDs()
        {
            return GetAllIDs();
        }
        /// <summary>
        /// 通過讀檔 再存檔更新所有資料
        /// (除了檔案格式變更外請勿使用此Function)
        /// </summary>
        virtual public void RefreshAllDatas()
        {
            var aIDs = GetEditableIDs();
            for (int i = 0; i < aIDs.Count; i++)
            {
                string aID = aIDs[i];
                var aData = CreateData(aID);
                aData.Save();
            }
        }
        /// <summary>
        /// 複製一份
        /// </summary>
        /// <returns></returns>
        virtual public UCLI_CommonEditable CloneInstance()
        {
            var aClone = this.CloneObject();
            aClone.ID = this.ID;
            return aClone;
        }
        virtual public JsonData SerializeToJson()
        {
            return JsonConvert.SaveFieldsToJsonUnityVer(this);
        }
        virtual public void DeserializeFromJson(JsonData iJson)
        {
            JsonConvert.LoadFieldFromJsonUnityVer(this, iJson);
        }
    }
}
