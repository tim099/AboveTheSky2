using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATS
{
    public class ATS_AutoHeader : UnityEditor.AssetModificationProcessor
    {
        const string Header =
@"
// ATS_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs";
        const string HeaderFormat =
    @"// {0} : {1}";
        /// <summary>
        /// https://prasetion.medium.com/automatically-adding-namespace-in-unity-c-script-dccbb7b4e728
        /// Adding automatically namespace in created script
        /// Edit -> Project Settings -> Editor -> C# Project Generation Section, then add your name of namespace in Root Namespace
        /// </summary>
        public static void OnWillCreateAsset(string iNewFileMeta)
        {
            Debug.LogWarning($"OnWillCreateAsset iNewFileMeta:{iNewFileMeta}");
            //RCG_GameManager.RefreshFileInspectorsStatic();


            try
            {
                string aFilePath = iNewFileMeta.Replace(".meta", "");
                if (aFilePath.EndsWith(".cs"))
                {
                    Debug.LogWarning("Create New File:" + aFilePath);
                    string aStr = System.IO.File.ReadAllText(aFilePath);
                    System.Text.StringBuilder aSB = new System.Text.StringBuilder();
                    aSB.AppendLine(Header);

                    aSB.AppendLine(string.Format(HeaderFormat, "Create time", System.DateTime.Now.ToString("MM/dd yyyy HH:mm")));// HH:mm
                                                                                                                                 //aSB.AppendLine(string.Format(HeaderFormat, "Author", System.Security.Principal.WindowsIdentity.GetCurrent().Name));
                    aSB.Append(aStr);
                    System.IO.File.WriteAllText(aFilePath, aSB.ToString());
                }
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
            }

            try
            {
                ATS_StaticEvents.TriggerOnRefreshGamedata();
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
