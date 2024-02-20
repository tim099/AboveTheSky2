using System.Collections;
using System.Collections.Generic;
using UCL.Core.EditorLib.Page;
using UCL.Core.LocalizeLib;
using UCL.Core.MathLib;
using UCL.Core.UI;
using UnityEditor;
using UnityEngine;

namespace ATS.Page
{
    public class ATS_CommonSelectPage<T> : UCL_EditorPage where T : class, ATSI_CommonData, new()
    {
        static public ATS_CommonSelectPage<T> Create()
        {
            var aPage = new ATS_CommonSelectPage<T>();
            UCL_GUIPageController.CurrentRenderIns.Push(aPage);

            return aPage;
        }

        protected UCL.Core.UCL_ObjectDictionary m_EditTmpDatas = new UCL.Core.UCL_ObjectDictionary();
        protected ATSI_Preview m_Preview = null;
        protected string m_CreateDes = string.Empty;
        protected string m_TypeName = string.Empty;
        protected CommonDataMeta m_Meta = null;
        protected ATS_CommonData<T> m_Util = default;
        public override bool IsWindow => true;
        public override void Init(UCL.Core.UI.UCL_GUIPageController iGUIPageController)
        {


            base.Init(iGUIPageController);
            string aTypeName = typeof(T).Name;
            m_TypeName = aTypeName;
            string aKey = "Create_" + aTypeName.Replace("RCG_", string.Empty);
            if (UCL_LocalizeManager.ContainsKey(aKey))
            {
                m_CreateDes = UCL_LocalizeManager.Get(aKey);
            }
            else
            {
                m_CreateDes = UCL_LocalizeManager.Get("CreateNew");
            }
            m_Meta = Util.CommonDataMetaIns;
            //Debug.LogError("m_CreateDes:" + m_CreateDes);
            OnResume();
        }
        public ATS_CommonData<T> Util
        {
            get
            {
                if (m_Util == null)
                {
                    m_Util = ATSI_CommonData.GetUtilByType(typeof(T)) as ATS_CommonData<T>;
                }
                return m_Util;
            }

        }//RCG_CommonData<T>.Util as RCG_CommonData<T>;
        public override void OnResume()
        {
            m_Preview = null;
            Util.ClearCache();
            m_Meta = Util.CommonDataMetaIns;
            //Debug.LogError($"OnResume m_Meta:{m_Meta.m_FileMetas.ConcatString(iMeta => $"{iMeta.Key}:{iMeta.Value.m_Group}")}");
        }
        public override void OnClose()
        {
            base.OnClose();
            m_Meta.Save();
        }
        protected override void ContentOnGUI()
        {
            m_Meta.OnGUI(m_EditTmpDatas.GetSubDic("Meta"));
            DrawSelectTargets();
        }
        protected override void TopBarButtons()
        {
            if (GUILayout.Button(m_CreateDes, UCL_GUIStyle.ButtonStyle, GUILayout.ExpandWidth(false)))
            {
                ATS_CommonEditPage.Create(new T());
            }
#if UNITY_EDITOR
            if (GUILayout.Button(UCL_LocalizeManager.Get("RefreshData"), UCL_GUIStyle.ButtonStyle, GUILayout.ExpandWidth(false)))
            {
                Util.RefreshAllDatas();
            }
            if (GUILayout.Button(UCL_LocalizeManager.Get("RemoveMetas"), UCL_GUIStyle.ButtonStyle, GUILayout.ExpandWidth(false)))
            {
                var aPath = Util.StreamingAssetFolderPath;
                var aFiles = UCL.Core.FileLib.Lib.GetFiles(aPath, "*.meta");
                foreach (var aFile in aFiles)
                {
                    System.IO.File.Delete(aFile);
                }
            }
#endif
#if UNITY_STANDALONE_WIN
            if (GUILayout.Button(UCL_LocalizeManager.Get("OpenFolder"), UCL_GUIStyle.ButtonStyle, GUILayout.ExpandWidth(false)))
            {
                UCL.Core.FileLib.WindowsLib.OpenExplorer(Util.StreamingAssetFolderPath);
            }
#endif
            using (new GUILayout.HorizontalScope("box"))
            {
                //GUILayout.Label("Type:", UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));
                //GUILayout.Space(10);
                GUILayout.Label($"Type : {m_TypeName}", UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));
                if (GUILayout.Button(UCL_LocalizeManager.Get("Copy"), UCL_GUIStyle.ButtonStyle, GUILayout.ExpandWidth(false)))
                {
                    GUIUtility.systemCopyBuffer = m_TypeName;
                }
            }

            //if (GUILayout.Button(UCL_LocalizeManager.Get("Open Folder"), UCL_GUIStyle.ButtonStyle, GUILayout.ExpandWidth(false)))
            //{
            //    System.Diagnostics.Process.Start("explorer.exe", @"c:\teste");
            //}
        }
        virtual protected void DrawSelectTargets()
        {
            GUILayout.BeginHorizontal();
            ATS_StaticFunctions.DrawSelectTargetList(Util.GetEditableIDs(), m_EditTmpDatas.GetSubDic("SelectTarget"),
                (iID) => {
                    ATS_CommonEditPage.Create(Util.GetData(iID));
                },
                (iID) => {
                    m_Preview = Util.CreateData(iID);
                },
                (iID) => {
                    Util.Delete(iID);
                }, m_Meta);
            m_Preview?.Preview(true);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}
