
// ATS_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 02/20 2024 19:11
using System;
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.LocalizeLib;
using UCL.Core.UI;
using UnityEngine;



namespace UCL.Core
{
    public interface UCLI_AssetEntry : UCLI_ID
    {
        /// <summary>
        /// 供編輯器使用 可以選取分組 但存成Json時不保存
        /// </summary>
        string GroupID { get; set; }
        bool IsEmpty { get; }
        /// <summary>
        /// 對應的RCGI_CommonData類型
        /// </summary>
        System.Type CommonDataType { get; }

        #region static

        public static System.Action<UCLI_AssetEntry> s_DeserializeAction = null;
        #endregion
    }

    [UCL.Core.ATTR.EnableUCLEditor]
    public class UCL_AssetEntry<T> : UCL.Core.JsonLib.UnityJsonSerializable, UCLI_AssetEntry, UCLI_ShortName, UCLI_NameOnGUI, UCLI_FieldOnGUI
        , IEquatable<UCL_AssetEntry<T>> where T : class, UCLI_Asset, UCLI_Preview, new()
    {
        protected const string FuncKeyGetAllIDs = "GetAllIDs";

        //[UCL.Core.PA.UCL_List(FuncKeyGetAllIDs)] public string m_ID = string.Empty;

        virtual public string GetShortName() => UCL.Core.LocalizeLib.UCL_LocalizeManager.Get(ID);
        virtual public string ID { get ; set ; }
        virtual public bool IsEmpty => string.IsNullOrEmpty(ID);

        public System.Type CommonDataType => typeof(T);
        /// <summary>
        /// 供編輯器使用 可以選取分組 但存成Json時不保存
        /// </summary>
        public string GroupID { get; set ; } = string.Empty;
        protected T Util => (UCL_Util<T>.Util);
        protected UCL_Asset<T> CommonDataUtil => Util as UCL_Asset<T>;
        /// <summary>
        /// 抓取道具資料(道具 裝備 卡牌等都算)
        /// </summary>
        /// <param name="iUseCache">是否使用緩存的資料 false會直接生成一份新的</param>
        /// <returns></returns>
        public T GetData(bool iUseCache = true) => Util.GetCommonData(ID, iUseCache) as T;
        public bool Exist() => Util.GetAllIDs().Contains(ID);
        /// <summary>
        /// 抓取此類型中所有的ID
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllIDs() => Util.GetAllIDs();
        public IList<string> GetLocalizeIDs()
        {
            return GetLocalizeIDs(GetAllIDs());
        }
        public IList<string> GetLocalizeIDs(IList<string> iList)
        {
            string[] aDisplayList = new string[iList.Count];
            for (int i = 0; i < iList.Count; i++)
            {
                string aKey = iList[i];
                if (UCL.Core.LocalizeLib.UCL_LocalizeManager.ContainsKey(aKey))
                {
                    aDisplayList[i] = string.Format("{0}({1})", UCL.Core.LocalizeLib.UCL_LocalizeManager.Get(aKey), aKey);
                }
                else
                {
                    aDisplayList[i] = aKey;
                }
            }
            return aDisplayList;
        }
        virtual protected void SelectGroupIDOnGUI(UCL.Core.UCL_ObjectDictionary iDataDic)
        {
            var aUtil = CommonDataUtil;
            var aCommonDataMeta = aUtil.CommonDataMetaIns;
            var aGroups = aCommonDataMeta.m_Groups;
            if (!aGroups.IsNullOrEmpty())
            {
                List<string> aGroupIDs = new List<string>();
                aGroupIDs.Add(string.Empty);
                foreach (var aGroup in aGroups.Keys)
                {
                    aGroupIDs.Add(aGroup);
                }
                var aAt = UCL.Core.UI.UCL_GUILayout.PopupAuto(aGroupIDs.IndexOf(GroupID), aGroupIDs, iDataDic, "GroupID", 8, GUILayout.ExpandWidth(true));
                GroupID = aGroupIDs[aAt];
            }
        }
        virtual protected void SelectIDOnGUI(UCL.Core.UCL_ObjectDictionary iDataDic)
        {
            var aIDs = GetAllIDs();
            if (!GroupID.IsNullOrEmpty())
            {
                //GUILayout.Label(GroupID);
                var aCommonDataMeta = CommonDataUtil.CommonDataMetaIns;
                aIDs = aCommonDataMeta.GetAllShowData(aIDs, GroupID, CommonDataMeta.PlayerPrefsData.FilterType.Dropdown);
            }
            if (!aIDs.IsNullOrEmpty())
            {
                var aAt = UCL.Core.UI.UCL_GUILayout.PopupAuto(aIDs.IndexOf(ID), GetLocalizeIDs(aIDs), iDataDic, "ID", 8, GUILayout.ExpandWidth(true));
                if (aAt >= 0 && aAt < aIDs.Count)
                {
                    ID = aIDs[aAt];
                }
            }
        }
        virtual public void NameOnGUI(UCL.Core.UCL_ObjectDictionary iDataDic, string iDisplayName)
        {
            GUILayout.Label(iDisplayName, UCL.Core.UI.UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));

            if (!CommonDataUtil.CommonDataMetaIns.m_Groups.IsNullOrEmpty())
            {
                using (var aHorizontalScope = new GUILayout.HorizontalScope(GUILayout.MinWidth(140)))
                {
                    SelectGroupIDOnGUI(iDataDic);
                }
            }

                
            using (var aHorizontalScope = new GUILayout.HorizontalScope(GUILayout.MinWidth(200)))
            {
                SelectIDOnGUI(iDataDic);
            }

        }
        virtual public object OnGUI(string iFieldName, UCL.Core.UCL_ObjectDictionary iDataDic)
        {
            //UCL.Core.UI.UCL_GUILayout.DrawObjExSetting aSetting = new UCL_GUILayout.DrawObjExSetting();
            //aSetting.OnShowField = () =>
            //{
            //    using (var aVerticalScope = new GUILayout.VerticalScope())
            //    {
            //        if (!CommonDataUtil.CommonDataMetaIns.m_Groups.IsNullOrEmpty())
            //        {
            //            using (var aHorizontalScope = new GUILayout.HorizontalScope())
            //            {
            //                GUILayout.Label(UCL_LocalizeManager.Get("GroupID"), UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));
            //                SelectGroupIDOnGUI(iDataDic);
            //            }
            //        }

            //        using (var aHorizontalScope = new GUILayout.HorizontalScope())
            //        {
            //            GUILayout.Label("ID", UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));
            //            SelectIDOnGUI(iDataDic);
            //        }
            //    }
            //};


            UCL_GUILayout.DrawField(this, iDataDic.GetSubDic("Data"), iFieldName, false);//, iDrawObjExSetting : aSetting
            return this;
        }


        [UCL.Core.ATTR.UCL_DrawOnGUI]
        virtual public void Preview(UCL.Core.UCL_ObjectDictionary iDic)
        {
            GUILayout.BeginHorizontal();
            bool aIsPreview = UCL.Core.UI.UCL_GUILayout.Toggle(iDic, "Preview");
            GUILayout.Label(UCL.Core.LocalizeLib.UCL_LocalizeManager.Get("Preview"));
            GUILayout.EndHorizontal();
            if (aIsPreview)
            {
                var aData = Util.GetCommonData(ID);
                if (aData != null)
                {
                    aData.Preview(iDic.GetSubDic("Preview"), true);
                }
            }
        }

        public override void DeserializeFromJson(JsonData iJson)
        {
            base.DeserializeFromJson(iJson);

            UCLI_AssetEntry.s_DeserializeAction?.Invoke(this);

            //if(RCG_RenamePage.s_IsRenaming)//check rename
            //{
            //    var aData = RCG_RenamePage.s_RenameData;
            //    if(aData.m_Type == typeof(T))//this is the rename type!!
            //    {
            //        if(aData.m_ID == ID)//Rename!!
            //        {
            //            ID = aData.m_NewID;
            //        }
            //    }
            //}
        }


        #region PA
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
        public override bool Equals(object iObj)
        {
            return Equals(iObj as UCL_AssetEntry<T>);
        }

        public virtual bool Equals(UCL_AssetEntry<T> iObj)
        {
            if (iObj == null) return false;
            return iObj.ID == ID;
        }
        #endregion
    }

    [System.Serializable]
    public class ATS_CommonGenDataDefault<T> : UCL_AssetEntry<T> where T : class, UCLI_Asset, UCLI_Preview, new()
    {
        override public string ID { get => m_ID; set => m_ID = value; }

        [UCL.Core.PA.UCL_List(FuncKeyGetAllIDs)] 
        [UCL.Core.ATTR.UCL_HideOnGUI]
        public string m_ID = "Default";

        override public object OnGUI(string iFieldName, UCL.Core.UCL_ObjectDictionary iDataDic)
        {
            UCL.Core.UI.UCL_GUILayout.DrawObjExSetting aSetting = new UCL_GUILayout.DrawObjExSetting();
            aSetting.OnShowField = () =>
            {
                using (var aVerticalScope = new GUILayout.VerticalScope())
                {
                    if (!CommonDataUtil.CommonDataMetaIns.m_Groups.IsNullOrEmpty())
                    {
                        using (var aHorizontalScope = new GUILayout.HorizontalScope())
                        {
                            GUILayout.Label(UCL_LocalizeManager.Get("GroupID"), UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));
                            SelectGroupIDOnGUI(iDataDic);
                        }
                    }

                    using (var aHorizontalScope = new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label("ID", UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));
                        SelectIDOnGUI(iDataDic);
                    }
                }
            };


            UCL_GUILayout.DrawField(this, iDataDic.GetSubDic("Data"), iFieldName, false, iDrawObjExSetting: aSetting);
            return this;
        }
    }
}

