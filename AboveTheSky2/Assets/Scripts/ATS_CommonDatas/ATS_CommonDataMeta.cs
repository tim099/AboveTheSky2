
// RCG_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 10/21 2023
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.LocalizeLib;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    public static class FileMetaExtensions
    {
        public static string GetGroupID(this CommonDataMeta.FileMeta iFileMeta)
        {
            if(iFileMeta == null)
            {
                return string.Empty;
            }
            return iFileMeta.m_Group;
        }
    }
    public class CommonDataMeta : UnityJsonSerializable, UCLI_NameOnGUI
    {
        /// <summary>
        /// Save in PlayerPrefs
        /// </summary>
        public class PlayerPrefsData : UnityJsonSerializable
        {
            public enum FilterType
            {
                CheckBox = 0,
                Dropdown,
            }
            public class GroupData : UnityJsonSerializable
            {
                public bool m_IsEnable = true;
            }
            public FilterType m_FilterType = FilterType.CheckBox;
            #region CheckBox
            public bool m_ShowAll = true;
            public bool m_ShowOthers = true;
            #endregion
            #region Dropdown
            /// <summary>
            /// if m_SelectedGroup is string.Empty, then show all
            /// </summary>
            public string m_SelectedGroup = string.Empty;
            #endregion



            public Dictionary<string, GroupData> m_GroupDatas = new();

            public void OnGUI(UCL_ObjectDictionary iDic, CommonDataMeta iCommonDataMeta)
            {
                var aExternalGroups = iCommonDataMeta.m_Groups;
                List<string> aDeletedKeys = new List<string>();
                foreach (var aKey in m_GroupDatas.Keys)
                {
                    if (!aExternalGroups.ContainsKey(aKey))
                    {
                        aDeletedKeys.Add(aKey);
                    }
                }
                foreach(var aKey in aDeletedKeys)//remove deleted keys
                {
                    m_GroupDatas.Remove(aKey);
                }
                foreach (var aKey in aExternalGroups.Keys)//add not exist keys
                {
                    if (!m_GroupDatas.ContainsKey(aKey))
                    {
                        m_GroupDatas.Add(aKey, new GroupData());
                    }

                    iCommonDataMeta.GroupsPopup.Add(aKey);
                }
                const int MaxColumn = 8;
                const int ShowCount = MaxColumn - 2;
                int aRemainCheckBoxCount = 0;
                List<string> aKeys = m_GroupDatas.Keys.ToList();
                using (var aScope = new GUILayout.HorizontalScope("box"))
                {
                    GUILayout.Label(UCL_LocalizeManager.Get("FilterMode"), UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));
                    m_FilterType = UCL_GUILayout.PopupAuto(m_FilterType, iDic, "FilterType", 6, GUILayout.Width(140));
                    switch (m_FilterType)
                    {
                        case FilterType.CheckBox:
                            {
                                m_ShowAll = UCL_GUILayout.CheckBox(m_ShowAll);
                                GUILayout.Label("Show All", UCL_GUIStyle.LabelStyle, GUILayout.MinWidth(60), GUILayout.ExpandWidth(false));

                                if (!m_ShowAll)
                                {
                                    m_ShowOthers = UCL_GUILayout.CheckBox(m_ShowOthers);
                                    GUILayout.Label("Others", UCL_GUIStyle.LabelStyle, GUILayout.MinWidth(60), GUILayout.ExpandWidth(false));
                                    int aCurShowCount = Mathf.Min(aKeys.Count, ShowCount);
                                    aRemainCheckBoxCount = aKeys.Count - aCurShowCount;

                                    for(int i = 0; i < aCurShowCount; i++)
                                    {
                                        var aKey = aKeys[i];
                                        var aGroup = m_GroupDatas[aKey];
                                        aGroup.m_IsEnable = UCL_GUILayout.CheckBox(aGroup.m_IsEnable);
                                        GUILayout.Label(aKey, UCL_GUIStyle.LabelStyle, GUILayout.MinWidth(60), GUILayout.ExpandWidth(false));
                                    }

                                }



                                break;
                            }
                        case FilterType.Dropdown:
                            {
                                List<string> aList = new List<string>();
                                aList.Add(string.Empty);
                                aList.Append(aExternalGroups.Keys);
                                m_SelectedGroup = aList[UCL_GUILayout.PopupAuto(aList.IndexOf(m_SelectedGroup), aList, iDic, "SelectedGroup", 6, GUILayout.Width(300))];
                                break;
                            }
                    }
                }
                if (aRemainCheckBoxCount > 0)
                {
                    //GUILayout.Space(20);
                    int aRowCount = Mathf.CeilToInt((float)aRemainCheckBoxCount / MaxColumn);
                    //GUILayout.Label($"aRowCount:{aRowCount},aRemainCheckBoxCount:{aRemainCheckBoxCount}");
                    using (var aScope2 = new GUILayout.VerticalScope())
                    {
                        for (int i = 0; i < aRowCount; i++)
                        {
                            using (var aScope3 = new GUILayout.HorizontalScope())
                            {
                                GUILayout.Space(20);
                                for (int j = 0; j < MaxColumn; j++)
                                {
                                    int aIndex = i * MaxColumn + j + ShowCount;
                                    if (aIndex >= aKeys.Count)
                                    {
                                        break;
                                    }
                                    var aKey = aKeys[aIndex];
                                    var aGroup = m_GroupDatas[aKey];
                                    aGroup.m_IsEnable = UCL_GUILayout.CheckBox(aGroup.m_IsEnable);
                                    GUILayout.Label(aKey, UCL_GUIStyle.LabelStyle, GUILayout.MinWidth(60), GUILayout.ExpandWidth(false));
                                }
                            }
                        }
                    }

                }
            }
            public bool CheckShowData(string iGroup, string iSelectedGroup, FilterType iFilterType)
            {
                switch (iFilterType)
                {
                    case FilterType.CheckBox:
                        {
                            if (m_ShowAll) return true;
                            if (string.IsNullOrEmpty(iGroup))//No group Data
                            {
                                //Debug.LogError("string.IsNullOrEmpty(aGroup)");
                                return m_ShowOthers;
                            }
                            if (m_GroupDatas.ContainsKey(iGroup))
                            {
                                return m_GroupDatas[iGroup].m_IsEnable;
                            }
                            break;
                        }
                    case FilterType.Dropdown:
                        {
                            return CheckShowGroup(iGroup, iSelectedGroup);
                            //if (string.IsNullOrEmpty(iGroup))//No group Data
                            //{
                            //    return true;
                            //}
                            //if (string.IsNullOrEmpty(iSelectedGroup) || iSelectedGroup == iGroup)
                            //{
                            //    return true;
                            //}
                            //return false;
                        }
                }

                return true;
            }
            public bool CheckShowGroup(string iGroup, string iSelectedGroup)
            {
                if (string.IsNullOrEmpty(iGroup))//No group Data
                {
                    return true;
                }
                if (string.IsNullOrEmpty(iSelectedGroup) || iSelectedGroup == iGroup)
                {
                    return true;
                }
                return false;
            }
        }
        public class Group : UnityJsonSerializable//, UCLI_IsEnable
        {
            public List<string> m_IDs = new List<string>();
            public string m_MD5 = "Tmp";
            //public bool IsEnable { get => m_IsEnable; set => m_IsEnable = value; }
            //public bool m_IsEnable = true;
        }
        public class FileMeta : UCL.Core.JsonLib.UnityJsonSerializable
        {
            public string m_ID;
            public string m_MD5;
            public string m_Group;
        }
        public ATS_FileData FileDatas { get; set; }
        public string PlayerPrefsKey => FileDatas.m_FolderPath;

        //public string m_SaveDate;

        //public string m_MD5;
        public PlayerPrefsData PlayerPrefsMeta { get; set; }
        public bool RequireClearDic { get; set; } = false;

        public Dictionary<string, Group> m_Groups = new();
        public Dictionary<string, FileMeta> m_FileMetas = new();
        //public bool m_ShowAll = true;
        //public bool m_ShowOthers = true;
        public bool m_EditGroup = false;
        virtual public void NameOnGUI(UCL.Core.UCL_ObjectDictionary iDataDic, string iDisplayName)
        {
            {
                GUILayout.Label(iDisplayName, UCL_GUIStyle.LabelStyle);//, GUILayout.Width(180), GUILayout.ExpandWidth(true)
            }


            m_EditGroup = UCL_GUILayout.CheckBox(m_EditGroup);
            GUILayout.Label(UCL_LocalizeManager.Get("EditGroup"), UCL_GUIStyle.LabelStyle);

            GUILayout.Space(10);
            if (GUILayout.Button(UCL_LocalizeManager.Get("Save"), UCL_GUIStyle.ButtonStyle))
            {
                Save();
            }
        }

        public FileMeta GetFileMeta(string iID)
        {
            if (!m_FileMetas.ContainsKey(iID))
            {
                m_FileMetas.Add(iID, new FileMeta());
            }
            return m_FileMetas[iID];
        }

        public void Init(ATS_FileData iFileData)
        {
            FileDatas = iFileData;
            PlayerPrefsMeta = new PlayerPrefsData();
            if (PlayerPrefs.HasKey(PlayerPrefsKey))
            {
                string aJson = PlayerPrefs.GetString(PlayerPrefsKey);
                JsonData aData = JsonData.ParseJson(aJson);
                PlayerPrefsMeta.DeserializeFromJson(aData);
            }
        }
        public override void DeserializeFromJson(JsonData iJson)
        {
            base.DeserializeFromJson(iJson);
            if (m_FileMetas.Count > 0)
            {
                List<string> aRemoveList = new List<string>();
                var aIDs = FileDatas.GetFileIDs();
                foreach (var aKey in m_FileMetas.Keys)
                {
                    if (!aIDs.Contains(aKey))
                    {
                        aRemoveList.Add(aKey);
                    }
                }
                foreach (var aKey in aRemoveList)
                {
                    m_FileMetas.Remove(aKey);
                }
            }

        }
        //const string MD5SaveKey = "##MD5##";
        public override JsonData SerializeToJson()
        {
            //m_SaveDate = System.DateTime.Now.ToString("yyyyy/MM/dd HH:mm:ss.ff");
            //m_MD5 = MD5SaveKey;
            var aJson = base.SerializeToJson();
            return aJson;
        }
        /// <summary>
        /// Write to File
        /// </summary>
        public void Save()
        {
            string aMetaStr = SerializeToJson().ToJson();
            FileDatas.SaveCommonDataMetaJson(aMetaStr);
            PlayerPrefs.SetString(PlayerPrefsKey, PlayerPrefsMeta.SerializeToJson().ToJson());
            //string aMd5 = aMetaStr.ConvertToMD5();
            //FileDatas.SaveCommonDataMetaJson(aMetaStr.Replace(MD5SaveKey, aMd5));
        }
        private List<string> GroupsPopup { get; set; } = new List<string>();
        private bool IsModified { get; set; } = false;
        public void OnGUI(UCL_ObjectDictionary iDic)
        {
            if (RequireClearDic)
            {
                RequireClearDic = false;
                iDic.Clear();
            }
            IsModified = false;
            int aGroupCount = m_Groups.Count;
            UCL_GUILayout.DrawObjectData(this, iDic.GetSubDic("Data"));
            if (aGroupCount != m_Groups.Count)
            {
                IsModified = true;
            }

            GroupsPopup.Clear();
            GroupsPopup.Add(string.Empty);//No Group

            PlayerPrefsMeta.OnGUI(iDic.GetSubDic("PlayerPrefsMeta"), this);

            //using (var aScope = new GUILayout.HorizontalScope("box"))
            //{
            //    GUILayout.Label("Filter Group", UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));
            //    PlayerPrefsMeta.m_ShowAll = UCL_GUILayout.CheckBox(PlayerPrefsMeta.m_ShowAll);
            //    GUILayout.Label("Show All", UCL_GUIStyle.LabelStyle, GUILayout.MinWidth(60), GUILayout.ExpandWidth(false));
            //    foreach (var aKey in m_Groups.Keys)
            //    {
            //        GroupsPopup.Add(aKey);
            //        if (!PlayerPrefsMeta.m_ShowAll)
            //        {
                        
            //            var aGroup = m_Groups[aKey];
            //            aGroup.IsEnable = UCL_GUILayout.CheckBox(aGroup.IsEnable);
            //            GUILayout.Label(aKey, UCL_GUIStyle.LabelStyle, GUILayout.MinWidth(60), GUILayout.ExpandWidth(false));
            //        }
            //    }
            //    if (!PlayerPrefsMeta.m_ShowAll)
            //    {
            //        PlayerPrefsMeta.m_ShowOthers = UCL_GUILayout.CheckBox(PlayerPrefsMeta.m_ShowOthers);
            //        GUILayout.Label("Others", UCL_GUIStyle.LabelStyle, GUILayout.MinWidth(60), GUILayout.ExpandWidth(false));
            //    }
            //}
        }
        /// <summary>
        /// 抓取全部要顯示的物品ID
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllShowData(IList<string> iIDs, PlayerPrefsData.FilterType? iFilterType = null)
        {
            return GetAllShowData(iIDs, PlayerPrefsMeta.m_SelectedGroup, iFilterType);
        }

        /// <summary>
        /// 抓取全部要顯示的物品ID
        /// </summary>
        /// <param name="iIDs"></param>
        /// <param name="iTargetGroup">分組ID 空字串則顯示全部</param>
        /// <returns></returns>
        public List<string> GetAllShowData(IList<string> iIDs, string iTargetGroup, PlayerPrefsData.FilterType? iFilterType = null)
        {
            List<string> aResult = new List<string>();
            for (int i = 0; i < iIDs.Count; i++)
            {
                string aID = iIDs[i];
                if (CheckShowData(aID, iTargetGroup, iFilterType))
                {
                    aResult.Add(aID);
                    continue;
                }
            }
            try
            {
                aResult.Sort((iA, iB) =>
                {
                    var aMetaA = GetFileMeta(iA);

                    var aMetaB = GetFileMeta(iB);
                    string aGroupA = aMetaA.GetGroupID();
                    string aGroupB = aMetaB.GetGroupID();
                    if (aGroupA == aGroupB)
                    {
                        return iA.CompareTo(iB);
                    }
                    else if (aGroupA.IsNullOrEmpty())
                    {
                        if (aGroupB.IsNullOrEmpty())
                        {
                            return 0;
                        }
                        else if (iTargetGroup.IsNullOrEmpty())
                        {
                            return -1;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else if (aGroupB.IsNullOrEmpty())
                    {
                        if (iTargetGroup.IsNullOrEmpty())
                        {
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }

                    int aGroupCompare = aGroupA.CompareTo(aGroupB);
                    if (aGroupCompare != 0)
                    {
                        return aGroupCompare;
                    }
                    return iA.CompareTo(iB);
                });
            }
            catch(System.Exception e)
            {
                Debug.LogException(e);
            }

            return aResult;
        }
        public bool CheckShowData(string iID, string iTargetGroup, PlayerPrefsData.FilterType? iFilterType = null)
        {
            var aFile = GetFileMeta(iID);
            string aGroup = aFile.GetGroupID();
            PlayerPrefsData.FilterType aFilterType = PlayerPrefsMeta.m_FilterType;
            if (iFilterType.HasValue)
            {
                aFilterType = iFilterType.Value;
            }
            return PlayerPrefsMeta.CheckShowData(aGroup, iTargetGroup, aFilterType);
        }
        public bool CheckShowGroup(string iID, string iTargetGroup)
        {
            var aFile = GetFileMeta(iID);
            string aGroup = aFile.GetGroupID();
            return PlayerPrefsMeta.CheckShowGroup(aGroup, iTargetGroup);
        }
        public void OnGUI_ShowData(string iID, UCL_ObjectDictionary iDic, int iWidth)
        {
            var aFile = GetFileMeta(iID);
            var aGroupsPopup = GroupsPopup;
            if (!aGroupsPopup.IsNullOrEmpty())
            {
                int aOldIndex = aGroupsPopup.IndexOf(aFile.m_Group);
                var aIndex = UCL_GUILayout.PopupAuto(aOldIndex, aGroupsPopup, iDic, "Group", 6, GUILayout.MinWidth(iWidth));//, GUILayout.MinWidth(80)
                if (aOldIndex != aIndex)
                {
                    IsModified = true;
                    aFile.m_Group = aGroupsPopup[aIndex];
                }
            }
        }
        public void OnGUIEnd()
        {
            //Check modified and save
            if (IsModified) Save();
            IsModified = false;
        }

    }
}
