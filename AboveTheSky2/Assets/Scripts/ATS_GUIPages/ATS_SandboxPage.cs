using System;
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.EditorLib.Page;
using UCL.Core.JsonLib;
using UCL.Core.LocalizeLib;
using UCL.Core.Page;
using UCL.Core.UI;
using Unity.VisualScripting;
using UnityEngine;

namespace ATS.Page
{
    public class ATS_SandboxPage : UCL_CommonEditorPage
    {
        [System.Serializable]
        public class RunTimeData : UCL.Core.JsonLib.UnityJsonSerializable
        {

            public float m_Scale = 1f;


            public override JsonData SerializeToJson()
            {
                return base.SerializeToJson();
            }
        }

        static public ATS_SandboxPage Create() => UCL_EditorPage.Create<ATS_SandboxPage>();
        public override string WindowName => "ATS_SandboxPage";
        protected override bool ShowCloseButton => !UI.ATS_EditorMenu.IsInEditWindow;
        protected override bool ShowBackButton => true;

        protected UCL_ObjectDictionary m_Dic = new UCL_ObjectDictionary();
        protected ATS_SandBox m_SandBox = new ATS_SandBox();
        #region
        const string RunTimeDataKey = "ATS_SandboxPage.RunTimeData";

        static RunTimeData s_RunTimeData = null;
        static RunTimeData LoadRunTimeData()
        {
            if (PlayerPrefs.HasKey(RunTimeDataKey))
            {
                try
                {
                    string aJsonStr = PlayerPrefs.GetString(RunTimeDataKey);
                    JsonData aJson = JsonData.ParseJson(aJsonStr);
                    var aRunTimeData = JsonConvert.LoadDataFromJsonUnityVer<RunTimeData>(aJson);
                    if (aRunTimeData != null) return aRunTimeData;
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
            return new RunTimeData();
        }
        static void SaveRunTimeData()
        {
            PlayerPrefs.SetString(RunTimeDataKey, UCL.Core.JsonLib.JsonConvert.SaveDataToJsonUnityVer(s_RunTimeData).ToJson());
        }
        #endregion
        public override void OnClose()
        {
            if(m_SandBox != null)
            {
                m_SandBox.End();
            }
            base.OnClose();
        }
        public override void OnPause()
        {
            
            base.OnPause();
        }
        public override void Init(UCL_GUIPageController iGUIPageController)
        {
            UCL.Core.UCL_ModResourcesService.ReleaseAll();
            base.Init(iGUIPageController);
            s_RunTimeData = LoadRunTimeData();

            m_SandBox.Init();
        }
        /// <summary>
        /// 繪製選單 開啟其他編輯器
        /// </summary>
        protected override void ContentOnGUI()
        {
            if (!UCL_ModuleService.Initialized)
            {
                return;
            }
            GUILayout.Label("ATS_SandboxPage");
            m_SandBox.ContentOnGUI(m_Dic.GetSubDic("SandBox"));
        }

    }
}
