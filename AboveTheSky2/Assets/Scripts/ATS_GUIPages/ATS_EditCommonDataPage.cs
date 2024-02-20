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
            GUILayout.Label("Test", UCL_GUIStyle.LabelStyle);
        }
    }
}
