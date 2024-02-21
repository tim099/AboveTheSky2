using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.EditorLib.Page;
using UCL.Core.LocalizeLib;
using UCL.Core.UI;
using UnityEngine;

namespace ATS.Page
{
    public class ATS_EditorMenuPage : UCL_CommonEditorPage
    {
        public override string WindowName => UCL_LocalizeManager.Get("EditorMenu");
        protected override bool ShowCloseButton => !UI.ATS_EditorMenu.IsInEditWindow;
        protected override bool ShowBackButton => UI.ATS_EditorMenu.IsInEditWindow ? false : base.ShowBackButton;

        UCL_ObjectDictionary m_Dic = new UCL_ObjectDictionary();

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

                if (GUILayout.Button(UCL_LocalizeManager.Get("DeveloperEditor"), UCL_GUIStyle.GetButtonStyle(Color.yellow)))
                {
                    ATS_DeveloperPage.Create();
                }

                if (GUILayout.Button(UCL_LocalizeManager.Get("Edit CommonData"), UCL_GUIStyle.GetButtonStyle(Color.yellow)))
                {
                    ATS_EditCommonDataPage.Create();
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
