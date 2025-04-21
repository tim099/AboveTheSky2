using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
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
        protected ATS_SandBox m_SandBox = null;
        protected CancellationTokenSource m_CST = null;

        private bool m_End = false;
        private bool m_Pause = false;
        private System.DateTime m_PrevUpdateTime;
        private System.DateTime m_StartTime;
        #region RunTimeData
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
            if(m_CST != null)
            {
                m_CST.Cancel();
                m_CST.Dispose();
            }
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
            InitSandBox();
        }
        private void InitSandBox()
        {
            m_SandBox = new ATS_SandBox();
            m_SandBox.Init();
            UpdateLoop().Forget();
            InitSandBoxAsync().Forget();
        }
        private async UniTask InitSandBoxAsync()
        {
            m_CST = new CancellationTokenSource();
            var token = m_CST.Token;
            for (int i = 0; i < 3; i++)
            {
                m_SandBox.m_AirShip.Spawn();
                await UniTask.WaitForSeconds(0.5f, cancellationToken: token);
                token.ThrowIfCancellationRequested();
            }
            for (int i = 0; i < 9; i++)
            {
                m_SandBox.m_AirShip.SpawnResource();
                await UniTask.WaitForSeconds(0.1f, cancellationToken: token);
                token.ThrowIfCancellationRequested();
            }
        }
        private string SaveFolder => Path.Combine(Application.persistentDataPath, "Saves");
        private string SavePath => Path.Combine(SaveFolder, "Save01");
        protected override void TopBarButtons()
        {
            base.TopBarButtons();
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            if (GUILayout.Button(UCL_LocalizeManager.Get("Open Saves"), UCL_GUIStyle.ButtonStyle, GUILayout.ExpandWidth(false)))
            {
                Directory.CreateDirectory(SavePath);
                UCL.Core.FileLib.WindowsLib.OpenExplorer(SaveFolder);
            }
#endif
            if (GUILayout.Button(UCL_LocalizeManager.Get("Save"), UCL_GUIStyle.ButtonStyle, GUILayout.ExpandWidth(false)))
            {
                var aPath = SavePath;
                if (Directory.Exists(aPath))
                {
                    Directory.Delete(aPath, true);
                }
                ATS_SaveData aSaveData = new ATS_SaveData(aPath);
                m_SandBox.SaveGame(aSaveData);
                //SaveData aSaveData = m_SandBox.SaveGame();
                aSaveData.Save(aPath);
            }
            if (GUILayout.Button(UCL_LocalizeManager.Get("Load"), UCL_GUIStyle.ButtonStyle, GUILayout.ExpandWidth(false)))
            {
                InitSandBox();
                ATS_SaveData aSaveData = new ATS_SaveData(SavePath);
                //aSaveData.Load(SavePath);
                m_SandBox.LoadGame(aSaveData);
            }
            if (!m_Pause)
            {
                if (GUILayout.Button(UCL_LocalizeManager.Get("Pause"), UCL_GUIStyle.ButtonStyle, GUILayout.ExpandWidth(false)))
                {
                    m_Pause = true;
                }
            }
            else
            {
                if (GUILayout.Button(UCL_LocalizeManager.Get("Play"), UCL_GUIStyle.ButtonStyle, GUILayout.ExpandWidth(false)))
                {
                    m_Pause = false;
                }
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
            m_SandBox.ContentOnGUI(m_Dic.GetSubDic("SandBox"));
        }


        private async UniTask UpdateLoop()
        {
            const int MaxUpdatePerFrame = 10;
            m_StartTime = m_PrevUpdateTime = System.DateTime.Now;
            //int aFrameCount = 0;
            double aOffSet = 0f;
            int updateTimes = 0;//

            while (!m_End)
            {
                var aNow = System.DateTime.Now;
                double delMS = (aNow - m_PrevUpdateTime).TotalMilliseconds;
                double del = (delMS + aOffSet) - ATS_SandBox.LogicIntervalMS;
                if (del >= 0)
                {
                    aOffSet = del;
                    if (!m_Pause)
                    {
                        m_SandBox.GameUpdate();
                    }
                    m_PrevUpdateTime = aNow;

                }

                if (updateTimes >= MaxUpdatePerFrame || aOffSet < ATS_SandBox.LogicIntervalMS)
                {
                    updateTimes = 0;
                    await UniTask.Yield();
                }
                else
                {
                    ++updateTimes;
                }

            }
        }

    }
}
