
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{


    public class ATS_TestDemo : UCL_Asset<ATS_TestDemo>
    {
        [UCL.Core.ATTR.UCL_IgnoreInTypeListable]
        public class Test
        {

        }

        public class A : Test
        {
            public int m_A = 5;
        }
        public class B : Test
        {
            public string m_B = "Test";
        }
        public class C : Test
        {
            public A m_A = new A();
            public B m_B = new B();
        }

        [UCL.Core.ATTR.UCL_SerializeReference]
        public Test m_Test = new A();

        /// <summary>
        /// 測試用
        /// </summary>
        public int m_TestInt = 3;
        /// <summary>
        /// 測試用
        /// </summary>
        public List<string> m_TestList;
        /// <summary>
        /// 測試用
        /// </summary>
        public Dictionary<string, int> m_TestDictionary;

        public ATS_RegionEntry m_RegionEntry = new();
        public ATS_TestDemoEntry m_TestDemoEntry = new();
        public override void Preview(UCL_ObjectDictionary iDataDic, bool iIsShowEditButton = false)
        {
            GUILayout.BeginVertical();
            if (iIsShowEditButton)
            {
                ShowEditButtonOnGUI();
            }
            GUILayout.Label($"m_TestInt:{m_TestInt}", UCL_GUIStyle.LabelStyle);

            GUILayout.EndVertical();
        }
    }

    public class ATS_TestDemoEntry : UCL_AssetEntryDefault<ATS_TestDemo>
    {
        public const string DefaultID = "Default2";

        public ATS_TestDemoEntry() { m_ID = DefaultID; }
        public ATS_TestDemoEntry(string iID) { m_ID = iID; }
    }
}
