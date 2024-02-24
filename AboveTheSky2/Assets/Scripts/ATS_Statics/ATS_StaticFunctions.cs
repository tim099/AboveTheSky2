using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UCL.Core;
using UCL.Core.LocalizeLib;
using UCL.Core.Page;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ATS
{

    public static class ATS_StaticEvents
    {
        static public System.Action s_OnRefreshGamedata = null;
        /// <summary>
        /// 觸發刷新資料事件(清除檔案 資料夾路徑等快取)
        /// </summary>
        public static void TriggerOnRefreshGamedata()
        {
            if (s_OnRefreshGamedata != null)
            {
                s_OnRefreshGamedata.Invoke();
            }
        }
    }
    public static class ATS_StaticFunctions
    {


        #region GUILayout
        /// <summary>
        /// 繪製選取編輯目標的列表(目前 裝備 道具都用這個繪製)
        /// </summary>
        /// <param name="iIDs2">所有目標ID</param>
        /// <param name="iDic">緩存</param>
        /// <param name="iEditAct">點下編輯時呼叫</param>
        /// <param name="iPreviewAct">點下預覽時呼叫</param>
        /// <param name="iDeleteAct">點下刪除時呼叫</param>
        /// <param name="FontSize"></param>
        static public void DrawSelectTargetList(IList<string> iIDs, UCL.Core.UCL_ObjectDictionary iDic,
            System.Action<string> iEditAct, System.Action<string> iPreviewAct, System.Action<string> iDeleteAct,
            UCL_AssetMeta iMeta = null,
            int FontSize = 20) {
            GUILayout.BeginVertical();
            Regex aRegex = null;
            string aSearchName = UCL.Core.UI.UCL_GUILayout.TextField(UCL_LocalizeManager.Get("Search"), iDic, "SearchName");
            if (!string.IsNullOrEmpty(aSearchName))
            {
                try
                {
                    aRegex = new Regex(aSearchName.ToLower(), RegexOptions.Compiled);
                }
                catch (System.Exception iE)
                {
                    Debug.LogException(iE);
                }
            }
            int aVerticalScopeWidth = 450;
            
            const int EditGroupWidth = 150;
            bool aIsEditGroup = false;
            if (iMeta != null)
            {
                aIsEditGroup = iMeta.m_EditGroup;
            }
            if (aIsEditGroup)
            {
                aVerticalScopeWidth += (EditGroupWidth + 10);
            }
            int aScrollWidth = aVerticalScopeWidth + 40;
            GUILayout.BeginHorizontal();
            iDic.SetData("ScrollPos", GUILayout.BeginScrollView(iDic.GetData("ScrollPos", Vector2.zero), GUILayout.MinWidth(aScrollWidth)));

            using (var aScope = new GUILayout.VerticalScope("box", GUILayout.MinWidth(aVerticalScopeWidth)))
            {
                if(iMeta != null)
                {
                    iIDs = iMeta.GetAllShowData(iIDs);
                }
                
                for (int i = 0; i < iIDs.Count; i++)
                {
                    string aID = iIDs[i];
                    //if(iMeta != null && !iMeta.CheckShowData(aID))
                    //{
                    //    continue;
                    //}
                    if (aRegex != null && !aRegex.IsMatch(aID.ToLower()))//根據輸入 過濾顯示的目標
                    {
                        continue;
                    }
                    GUILayout.BeginHorizontal();
                    using (var aScope2 = new GUILayout.HorizontalScope("box"))
                    {
                        string aDisplayName = aID;
                        if (aRegex != null)//標記符合搜尋條件的部分
                        {
                            aDisplayName = aRegex.HightLight(aDisplayName, aSearchName, Color.red);
                        }

                        if (UCL.Core.UI.UCL_GUILayout.ButtonAutoSize(UCL_LocalizeManager.Get("Delete"), FontSize, Color.red, Color.white))
                        {
                            UCL_OptionPage.ConfirmDelete(aID, () => iDeleteAct(aID));
                        }

                        GUILayout.Box(aDisplayName, UCL.Core.UI.UCL_GUIStyle.BoxStyle, GUILayout.MinWidth(160), GUILayout.MaxWidth(250));
                        if (UCL.Core.UI.UCL_GUILayout.ButtonAutoSize(UCL_LocalizeManager.Get("Edit"), FontSize))
                        {
                            iEditAct(aID);
                            //RCG_EditItemPage.Create(RCG_ItemData.GetItemData(aID));
                        }
                        if (UCL.Core.UI.UCL_GUILayout.ButtonAutoSize(UCL_LocalizeManager.Get("Preview"), FontSize))
                        {
                            iPreviewAct(aID);
                        }

                    }
                    if (aIsEditGroup)
                    {
                        using (var aScope2 = new GUILayout.HorizontalScope("box", GUILayout.MinWidth(EditGroupWidth)))
                        {
                            iMeta.OnGUI_ShowData(aID, iDic.GetSubDic(aID), EditGroupWidth - 5);
                        }
                    }
                    GUILayout.EndHorizontal();
                }
            }
            if(iMeta != null)
            {
                iMeta.OnGUIEnd();
            }
            GUILayout.EndScrollView();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
        #endregion

        public static string LocalizeFieldName(string iDisplayName)
        {
            if (iDisplayName[0] == 'm' && iDisplayName[1] == '_')
            {
                iDisplayName = iDisplayName.Substring(2, iDisplayName.Length - 2);
            }
            iDisplayName = UCL_LocalizeManager.Get(iDisplayName);
            return iDisplayName;
        }
    }
}

