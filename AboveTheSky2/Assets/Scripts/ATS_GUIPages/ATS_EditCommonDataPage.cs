using System;
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.EditorLib.Page;
using UCL.Core.UI;
using UnityEngine;

namespace ATS.Page
{
    public class ATS_EditCommonDataPage : UCL_CommonEditorPage
    {
        static public ATS_EditCommonDataPage Create() => UCL_EditorPage.Create<ATS_EditCommonDataPage>();
        UCL_ObjectDictionary m_DataDic = new UCL_ObjectDictionary();
        public ATS_EditCommonDataPage()
        {

        }
        ~ATS_EditCommonDataPage()
        {

        }

        protected override void ContentOnGUI()
        {
            if (!UCL_ModuleService.Initialized)
            {
                return;
            }
            UCL_ModuleService.Ins.OnGUI(m_DataDic.GetSubDic("ModuleService"));
            //GUILayout.Label("Test", UCL_GUIStyle.LabelStyle);
            var aLabelStyle = UCL_GUIStyle.GetLabelStyle(Color.white, 18);
            var aButtonStyle = UCL_GUIStyle.GetButtonStyle(Color.white, 18);
            foreach (var aType in ATSI_CommonData.GetAllCommonDataTypes())
            {
                try
                {
                    string aPropInfosStr = string.Empty;
                    try
                    {
                        ATSI_CommonData aUtil = ATSI_CommonData.GetUtilByType(aType);//Get Util
                        if (aUtil != null)
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label(aType.Name, aLabelStyle, GUILayout.ExpandWidth(false));
                            if(GUILayout.Button($"Edit", aButtonStyle, GUILayout.Width(100)))
                            {
                                aUtil.CreateSelectPage();
                            }
                            //GUILayout.Label($"{aType.FullName}");
                            //aUtil.RefreshAllDatas();
                            //Debug.LogWarning($"Util:{aUtil.GetType().FullName}.RefreshAllDatas()");
                            GUILayout.EndHorizontal();
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
    }
}
