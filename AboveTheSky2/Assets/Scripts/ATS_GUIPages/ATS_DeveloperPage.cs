using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UCL.Core.EditorLib.Page;
using UCL.Core.JsonLib;
using UCL.Core.UI;
using UnityEngine;

namespace ATS.Page
{
    public class ATS_DeveloperPage : ATS_EditorPage
    {
        public enum EditMode
        {
            Normal,
            LowRam,
        }
        [System.Serializable]
        public class RunTimeData : UCL.Core.JsonLib.UnityJsonSerializable
        {
            public EditMode m_EditMode = EditMode.Normal;
            /// <summary>
            /// 是否要開啟Debug模式?
            /// </summary>
            public bool m_EnableDebugMode = false;

            public string m_RegexStr = string.Empty;
            public string m_RegexMatchTarget = string.Empty;
        }
        static public ATS_DeveloperPage Create() => UCL_EditorPage.Create<ATS_DeveloperPage>();
        static public bool IsLowRamMode => Data.m_EditMode == Page.ATS_DeveloperPage.EditMode.LowRam;
        static public RunTimeData Data
        {
            get
            {
                if (s_RunTimeData == null)
                {
                    s_RunTimeData = LoadRunTimeData();
                }
                return s_RunTimeData;
            }
        }
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

        const string ScriptsRootKey = "RCG_DeveloperPageScriptsRoot";
        const string RunTimeDataKey = "s_RunTimeData";
        string m_ScriptsRoot = "";
        string m_TargetScriptsRoot = "";
        UCL.Core.UCL_ObjectDictionary m_Dic = new UCL.Core.UCL_ObjectDictionary();


        public ATS_DeveloperPage()
        {
            m_ScriptsRoot = PlayerPrefs.GetString(ScriptsRootKey);
        }
        ~ATS_DeveloperPage()
        {
            SaveConfig();
        }
        void SaveConfig()
        {
            PlayerPrefs.SetString(ScriptsRootKey, m_ScriptsRoot);
            SaveRunTimeData();
        }
        protected override void TopBarButtons()
        {
            base.TopBarButtons();
            GUILayout.Space(30);
            if (GUILayout.Button(UCL.Core.LocalizeLib.UCL_LocalizeManager.Get("Save"), GUILayout.ExpandWidth(false)))
            {
                SaveConfig();
            }
        }
        void LoadConfig()
        {
            m_ScriptsRoot = PlayerPrefs.GetString(ScriptsRootKey);
            //m_RegexStr = PlayerPrefs.GetString("m_RegexStr", string.Empty);
            //s_RunTimeData.m_RegexMatchTarget = PlayerPrefs.GetString("s_RunTimeData.m_RegexMatchTarget", string.Empty);
            s_RunTimeData = LoadRunTimeData();
        }
        public override void Init(UCL_GUIPageController iGUIPageController)
        {
            base.Init(iGUIPageController);
            LoadConfig();
        }
        public override void OnClose()
        {
            SaveConfig();

            base.OnClose();
        }
        // Using encoding from BOM or UTF8 if no BOM found,
        // check if the file is valid, by reading all lines
        // If decoding fails, use the local "ANSI" codepage
        public string DetectFileEncoding(Stream iFileStream)
        {
            //Encoding aEncoding = null;
            var Utf8EncodingVerifier = Encoding.GetEncoding("utf-8", new EncoderExceptionFallback(), new DecoderExceptionFallback());
            using (var aReader = new StreamReader(iFileStream, Utf8EncodingVerifier,
                   detectEncodingFromByteOrderMarks: true, leaveOpen: true, bufferSize: 1024))
            {
                string detectedEncoding;
                try
                {
                    while (!aReader.EndOfStream)
                    {
                        var line = aReader.ReadLine();
                    }
                    //aEncoding = reader.CurrentEncoding;
                    detectedEncoding = aReader.CurrentEncoding.BodyName;
                }
                catch (Exception iE)
                {
                    Debug.LogWarning(iE);
                    // Failed to decode the file using the BOM/UT8. 
                    // Assume it's local ANSI
                    detectedEncoding = "ISO-8859-1";
                }
                // Rewind the stream
                iFileStream.Seek(0, SeekOrigin.Begin);
                //Debug.LogError("detectedEncoding:" + detectedEncoding);
                aReader.Close();
                return detectedEncoding;
            }
        }
        protected override void ContentOnGUI()
        {
            //if (GUILayout.Button("RefreshAllDatas(With Reflection)", UCL_GUIStyle.ButtonStyle))
            //{
            //    RCGI_CommonData.RefreshAllCommonDatasWithReflection();

            //}


            //if (GUILayout.Button("RefreshGamedata", UCL_GUIStyle.ButtonStyle))
            //{
            //    RCG_GameManager.TriggerOnRefreshGamedata();
            //}

            //GUILayout.Space(20);

            //{

            //    using (new GUILayout.HorizontalScope("box"))
            //    {
            //        GUILayout.Box("Language", UCL_GUIStyle.BoxStyle, GUILayout.ExpandWidth(false));
            //        string aLangCode = UCL_LocalizeManager.s_LangName;
            //        RCG_LanguageCodeGenData aLanguageCodeGenData = new RCG_LanguageCodeGenData(aLangCode);
            //        UCL_GUILayout.DrawObjectData(aLanguageCodeGenData, m_Dic.GetSubDic("LanguageCode"), "LanguageCode", false);
            //        string aNewLangCode = aLanguageCodeGenData.ID;
            //        if (aNewLangCode != aLangCode)
            //        {
            //            RCG_LocalizeService.SetLanguage(aNewLangCode);
            //        }
            //    }
            //}
            UCL_GUILayout.DrawObjectData(s_RunTimeData, m_Dic.GetSubDic("RunTimeData"), "RunTimeData", false);

            //s_RunTimeData.m_EditMode = UCL_GUILayout.PopupAuto(s_RunTimeData.m_EditMode, m_Dic.GetSubDic("EditMode"));
            GUILayout.Space(20);
            using (new GUILayout.VerticalScope("box"))
            {
                m_ScriptsRoot = UCL.Core.UI.UCL_GUILayout.FolderExplorer(m_Dic.GetSubDic("ScriptsRoot"), m_ScriptsRoot, Application.dataPath, "ScriptsRoot");
                if (GUILayout.Button("Refresh scripts encoding"))
                {
                    bool aCancel = UCL.Core.EditorLib.EditorUtilityMapper.DisplayCancelableProgressBar("Refresh scripts encoding", "Init", 0.1f);
                    var aRoot = Application.dataPath + "/" + m_ScriptsRoot;
                    var aFiles = Directory.GetFiles(aRoot, "*.cs", System.IO.SearchOption.AllDirectories);
                    for (int i = 0; i < aFiles.Length; i++)
                    {
                        var aFilePath = aFiles[i];
                        aCancel = UCL.Core.EditorLib.EditorUtilityMapper.DisplayCancelableProgressBar("Refresh scripts encoding", aFilePath,
                            0.9f * ((float)(i + 1) / aFiles.Length) + 0.1f);
                        if (aCancel) break;
                        string aEncode = string.Empty;
                        using (var aStream = File.OpenRead(aFilePath))
                        {
                            aEncode = DetectFileEncoding(aStream);
                        }

                        try
                        {
                            if (aEncode != "utf-8")//應該是Big5
                            {
                                var aTexts = File.ReadAllText(aFilePath, System.Text.Encoding.GetEncoding(950));
                                File.WriteAllText(aFilePath, aTexts, System.Text.Encoding.GetEncoding(65001));
                                Debug.LogError("File:" + aFilePath + ",aEncode:" + aEncode);
                            }
                        }
                        catch (System.Exception iE)
                        {
                            Debug.LogException(iE);
                        }



                    }

                    UCL.Core.EditorLib.EditorUtilityMapper.ClearProgressBar();
                    //File.WriteAllText(path, string.Empty, new UTF8Encoding(false));
                }
                m_TargetScriptsRoot = UCL.Core.UI.UCL_GUILayout.TextField("TargetScriptsRoot", m_TargetScriptsRoot);
                if (GUILayout.Button("Refresh targets encoding"))
                {
                    bool aCancel = UCL.Core.EditorLib.EditorUtilityMapper.DisplayCancelableProgressBar("Refresh scripts encoding", "Init", 0.1f);
                    var aRoot = m_TargetScriptsRoot;
                    var aFiles = Directory.GetFiles(aRoot, "*.cs", System.IO.SearchOption.AllDirectories);
                    for (int i = 0; i < aFiles.Length; i++)
                    {
                        var aFilePath = aFiles[i];
                        aCancel = UCL.Core.EditorLib.EditorUtilityMapper.DisplayCancelableProgressBar("Refresh scripts encoding", aFilePath,
                            0.9f * ((float)(i + 1) / aFiles.Length) + 0.1f);
                        if (aCancel) break;
                        string aEncode = string.Empty;
                        using (var aStream = File.OpenRead(aFilePath))
                        {
                            aEncode = DetectFileEncoding(aStream);
                        }

                        try
                        {
                            if (aEncode != "utf-8")//應該是Big5
                            {
                                var aTexts = File.ReadAllText(aFilePath, System.Text.Encoding.GetEncoding(950));
                                File.WriteAllText(aFilePath, aTexts, System.Text.Encoding.GetEncoding(65001));
                                Debug.LogError("File:" + aFilePath + ",aEncode:" + aEncode);
                            }
                        }
                        catch (System.Exception iE)
                        {
                            Debug.LogException(iE);
                        }



                    }

                    UCL.Core.EditorLib.EditorUtilityMapper.ClearProgressBar();
                    //File.WriteAllText(path, string.Empty, new UTF8Encoding(false));
                }
            }

//            if (RCG_DataService.Ins != null)
//            {
//                UCL_GUILayout.DrawObjectData(RCG_DataService.Ins.CurPlayerData, m_Dic.GetSubDic("PlayerData"), "PlayerData");
//                UCL_GUILayout.DrawObjectData(RCG_DataService.Ins.m_GameData, m_Dic.GetSubDic("GameData"), "GameData");
//            }
//            //List<RCG_SkillTagGenData> aList = new List<RCG_SkillTagGenData>();
//            //RCG_SkillTagGenData aA = new RCG_SkillTagGenData("A");
//            //RCG_SkillTagGenData aB = new RCG_SkillTagGenData("A");
//            //RCG_SkillTagGenData aC = new RCG_SkillTagGenData("C");
//            //aList.Add(aA);
//            //GUILayout.Label($"Test aA == aB{(aA == aB).ToString()},EQ:{(aA.Equals(aB)).ToString()}" +
//            //    $",aList.Contains(aA):{aList.Contains(aA)},aList.Contains(aB):{aList.Contains(aB)}" +
//            //    $",aList.Contains(aC):{aList.Contains(aC)}");
//            RegexTest();

//            GUILayout.Space(30);
//#if UNITY_EDITOR
//            var aPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Install", ".AudioClips");
//            if (GUILayout.Button(UCL_LocalizeManager.Get($"RemoveMetas : {aPath}"), UCL_GUIStyle.ButtonStyle))
//            {

//                RemoveMetaDatas(aPath);
//            }
//#endif

//#if UNITY_STANDALONE_WIN
//            if (GUILayout.Button(UCL_LocalizeManager.Get($"Open GameFolder"), UCL_GUIStyle.ButtonStyle))
//            {
//                UCL.Core.FileLib.WindowsLib.OpenExplorer(RCG_GameManager.GameFolderPath);
//            }
//#endif
        }

        private void RemoveMetaDatas(string iPath)
        {
            Debug.LogError($"RemoveMetaDatas iPath:{iPath}");
            var aFiles = UCL.Core.FileLib.Lib.GetFiles(iPath, "*.meta");
            foreach (var aFile in aFiles)
            {
                System.IO.File.Delete(aFile);
            }
            var aDirs = System.IO.Directory.GetDirectories(iPath);
            if (aDirs.Length > 0)
            {
                foreach (var aDir in aDirs)
                {
                    RemoveMetaDatas(aDir);
                }
            }
        }

        void RegexTest()
        {
            using (new GUILayout.VerticalScope("box"))//Regex Test
            {
                s_RunTimeData.m_RegexStr = UCL.Core.UI.UCL_GUILayout.TextField("RegexStr", s_RunTimeData.m_RegexStr);
                s_RunTimeData.m_RegexMatchTarget = UCL.Core.UI.UCL_GUILayout.TextField("RegexMatchTarget", s_RunTimeData.m_RegexMatchTarget);
                if (!string.IsNullOrEmpty(s_RunTimeData.m_RegexStr) && !string.IsNullOrEmpty(s_RunTimeData.m_RegexMatchTarget))
                {

                    string aResult = string.Empty;
                    try
                    {
                        Regex aRegex = new Regex(s_RunTimeData.m_RegexStr);
                        var aMatches = aRegex.Matches(s_RunTimeData.m_RegexMatchTarget);
                        GUILayout.Label("Matches.Count:" + aMatches.Count);
                        foreach (Match aMatch in aMatches)
                        {
                            using (new GUILayout.VerticalScope("box"))
                            {
                                GUILayout.Label(aMatch.Value);
                                GUILayout.Label("MatchIndex:" + aMatch.Index);
                                GUILayout.Label("MatchLength:" + aMatch.Length);
                            }

                        }

                        aResult = aRegex.HightLight(s_RunTimeData.m_RegexMatchTarget, s_RunTimeData.m_RegexStr, Color.red);

                    }
                    catch (System.Exception iE)
                    {
                        Debug.LogException(iE);
                    }

                    GUILayout.Box(aResult, UCL.Core.UI.UCL_GUIStyle.BoxStyle);
                }
            }
        }
    }
}
