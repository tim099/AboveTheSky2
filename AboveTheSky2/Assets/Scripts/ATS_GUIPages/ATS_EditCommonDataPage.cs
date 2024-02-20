using System;
using System.Collections;
using System.Collections.Generic;
using UCL.Core.EditorLib.Page;
using UCL.Core.UI;
using UnityEngine;

namespace ATS.Page
{
    public class ATS_EditCommonDataPage : ATS_EditorPage
    {
        static public ATS_EditCommonDataPage Create() => UCL_EditorPage.Create<ATS_EditCommonDataPage>();

        public ATS_EditCommonDataPage()
        {

        }
        ~ATS_EditCommonDataPage()
        {

        }

        protected override void ContentOnGUI()
        {
            //GUILayout.Label("Test", UCL_GUIStyle.LabelStyle);

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
                            if(GUILayout.Button($"Edit: {aType.FullName}", UCL_GUIStyle.ButtonStyle))
                            {
                                aUtil.CreateSelectPage();
                            }
                            //GUILayout.Label($"{aType.FullName}");
                            //aUtil.RefreshAllDatas();
                            //Debug.LogWarning($"Util:{aUtil.GetType().FullName}.RefreshAllDatas()");
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
