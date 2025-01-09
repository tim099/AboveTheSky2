using Cysharp.Threading.Tasks;
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
    public class ATS_EditorMenuPage : UCL_CommonEditorPage
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

        public override string WindowName => UCL_LocalizeManager.Get("EditorMenu");
        protected override bool ShowCloseButton => !UI.ATS_EditorMenu.IsInEditWindow;
        protected override bool ShowBackButton => UI.ATS_EditorMenu.IsInEditWindow ? false : base.ShowBackButton;

        UCL_ObjectDictionary m_Dic = new UCL_ObjectDictionary();

        #region
        const string RunTimeDataKey = "s_RunTimeData";
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

        public override void Init(UCL_GUIPageController iGUIPageController)
        {
            base.Init(iGUIPageController);
            s_RunTimeData = LoadRunTimeData();
            var aStyleData = UCL_GUIStyle.CurStyleData;
            aStyleData.SetScale(s_RunTimeData.m_Scale);
            if (!UCL_ModuleService.Initialized)
            {
                UCL_ModuleService.WaitUntilInitialized(default).Forget();
            }
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

            using (var aScope = new GUILayout.VerticalScope("box"))//, GUILayout.MaxWidth(320)
            {
                UCL_GUIStyle.SetSizeOnGUI();


                if (GUILayout.Button("DebugLog", UCL_GUIStyle.ButtonStyle))
                {
                    UCL_DebugLogPage.Create();
                    //UCL.Core.DebugLib.UCL_DebugLog.Instance.Toggle();
                }
                if (GUILayout.Button(UCL_LocalizeManager.Get("DeveloperEditor"), UCL_GUIStyle.GetButtonStyle(Color.yellow)))
                {
                    ATS_DeveloperPage.Create();
                }

                if (GUILayout.Button(UCL_LocalizeManager.Get("Edit Modules"), UCL_GUIStyle.GetButtonStyle(Color.yellow)))
                {
                    UCL_ModuleServiceEditPage.Create();
                }
                if (GUILayout.Button(UCL_LocalizeManager.Get("Sandbox"), UCL_GUIStyle.GetButtonStyle(Color.yellow)))
                {
                    ATS_SandboxPage.Create();
                }
                if (GUILayout.Button(UCL_LocalizeManager.Get("ModResourcesService"), UCL_GUIStyle.GetButtonStyle(Color.yellow)))
                {
                    UCL_ModResourcesServicePage.Create();
                }
                //#region GameSetting
                //{//Edit GameSetting
                //    using (var aTagScope = new GUILayout.HorizontalScope())
                //    {
                //        bool aShow = UCL.Core.UI.UCL_GUILayout.Toggle(m_Dic, "EditGameSetting");
                //        using (var aTagScopeV = new GUILayout.VerticalScope())
                //        {

                //            GUILayout.Label(UCL_LocalizeManager.Get("EditGameSetting"));
                //            if (aShow)
                //            {
                //                if (GUILayout.Button(UCL_LocalizeManager.Get("GameInitDataEditor")))
                //                {
                //                    RCG_GameInitData.Util.CreateCommonSelectPage();
                //                    //RCG_EditGameInitDataPage.Create();
                //                }
                //                if (GUILayout.Button(UCL_LocalizeManager.Get("GameSettingEditor")))
                //                {
                //                    RCG_GameSettingData.Util.CreateCommonSelectPage();
                //                }
                //                if (GUILayout.Button(UCL_LocalizeManager.Get("GameVersionEditor")))
                //                {
                //                    RCG_GameVersionData.Util.CreateCommonSelectPage();
                //                }
                //                if (GUILayout.Button(UCL_LocalizeManager.Get("ResourceTypeEditor")))
                //                {
                //                    RCG_ResourceTypeData.Util.CreateCommonSelectPage();
                //                }
                //                if (GUILayout.Button(UCL_LocalizeManager.Get("BGMDataEditor")))
                //                {
                //                    RCG_BGMData.Util.CreateCommonSelectPage();
                //                }
                //                if (GUILayout.Button(UCL_LocalizeManager.Get("SEDataEditor")))
                //                {
                //                    RCG_SEData.Util.CreateCommonSelectPage();
                //                }

                //                if (GUILayout.Button(UCL_LocalizeManager.Get("PoolingDataEditor")))
                //                {
                //                    RCG_PoolingData.Util.CreateCommonSelectPage();
                //                }



                //                GUILayout.Space(10);
                //                if (GUILayout.Button(UCL_LocalizeManager.Get("RuntimeStructEditor")))
                //                {
                //                    RCG_RuntimeStructData.Util.CreateCommonSelectPage();
                //                }
                //                if (GUILayout.Button(UCL_LocalizeManager.Get("RuntimeDataEditor")))
                //                {
                //                    RCG_RuntimeData.Util.CreateCommonSelectPage();
                //                }

                //                GUILayout.Space(10);

                //                if (GUILayout.Button(UCL_LocalizeManager.Get("RuntimeScriptEditor")))
                //                {
                //                    RCG_RuntimeScriptData.Util.CreateCommonSelectPage();
                //                }


                //                GUILayout.Space(10);


                //            }
                //        }
                //    }
                //}
                //#endregion

            }
        }

    }
}
