
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    public enum GameState
    {
        /// <summary>
        /// 初始化
        /// </summary>
        Boot = 0,
        /// <summary>
        /// 遊戲循環
        /// </summary>
        GameLoop,
        /// <summary>
        /// 建築模式
        /// </summary>
        Build,
    }
    public enum SaveType
    {
        None,
        Folder,
        File,
        Json,
    }






    /// <summary>
    /// base game enviroment
    /// </summary>
    public class ATS_SandBox : ATS_SandBoxBase
    {
        /// <summary>
        /// 當前正在存檔或讀檔的ATS_SandBox
        /// </summary>
        public static ATS_SandBox s_CurSaveSandBox
        {
            get;
            private set;
        }


        const int LogicIntervalMS = 30;


        protected ATS_AirShip m_AirShip = new ATS_AirShip();

        //[UCL.Core.ATTR.UCL_HideInJson]
        [SerializeField]
        protected Dictionary<string, ATS_Indexer> m_SandBoxItems = new ();

        private bool m_End = false;
        private bool m_Pause = false;
        //private float m_TimeScale = 1f;


        private System.DateTime m_PrevUpdateTime;
        private System.DateTime m_StartTime;
        private List<System.Action> m_OnLoadEndAction = new List<System.Action>();


        override public GameState CurGameState => m_GameState;
        public override (SaveType, string) SaveKey => (SaveType.Folder, "SandBox");

        public GameState m_GameState = GameState.Boot;

        public void Init()
        {
            Init(this, null);
        }
        override public void Init(ATS_SandBox iSandBox, ATSI_SandBox iParent)
        {
            if (m_Inited)
            {
                return;
            }
            SetGameState(GameState.Boot);
            //m_Components.Add(m_AirShip);
            base.Init(iSandBox, iParent);
            AddComponent(m_AirShip);


            //if (m_Inited)
            //{
            //    return;
            //}
            //m_Inited = true;
            //m_Components.Add(m_AirShip);
            //foreach(var aComponent in m_Components)
            //{
            //    aComponent.Init(iSandBox);
            //}
            //m_AirShip.Init(iSandBox);
            UpdateLoop().Forget();

        }


        public void SetGameState(GameState gameState)
        {
            m_GameState = gameState;
        }
        public void End()
        {
            m_End = true;
        }
        private async UniTask UpdateLoop()
        {
            m_StartTime = m_PrevUpdateTime = System.DateTime.Now;
            //int aFrameCount = 0;
            double aOffSet = 0f;
            SetGameState(GameState.GameLoop);
            while (!m_End)
            {
                var aNow = System.DateTime.Now;
                double delMS = ((aNow - m_PrevUpdateTime).TotalMilliseconds);
                if((delMS + aOffSet) >= LogicIntervalMS)
                {
                    aOffSet += delMS - LogicIntervalMS;

                    if (!m_Pause)
                    {
                        GameUpdate();
                    }
                    //Debug.LogError($"GameUpdate() delMS:{delMS}");
                    m_PrevUpdateTime = aNow;

                    //{
                    //    var aTotal = (aNow - m_StartTime).TotalMilliseconds;
                    //    ++aFrameCount;
                    //    Debug.LogError($"aFrameCount:{aFrameCount},aTotal:{aTotal},Average:{aTotal / aFrameCount},aOffSet:{aOffSet}");
                    //}

                }
                //if(delMS < LogicIntervalMS)
                //{
                //    await Task.Delay(LogicIntervalMS - (int)delMS);
                //}
                //await Task.Delay(LogicIntervalMS);
                await UniTask.Yield();
            }
        }


        //System.DateTime m_Test;
        /// <summary>
        /// Logic base update
        /// </summary>
        override public void GameUpdate()
        {
            base.GameUpdate();
            //var aNow = System.DateTime.Now;
            //Debug.LogError($"GameUpdate() TotalMilliseconds:{(aNow - m_Test).TotalMilliseconds}");
            //m_Test = aNow;
            //foreach (var aComponent in m_Components)
            //{
            //    aComponent.GameUpdate();
            //}
            //m_AirShip.GameUpdate();
        }
        /// <summary>
        /// 用在ATS_SandboxPage
        /// </summary>
        override public void ContentOnGUI(UCL_ObjectDictionary iDic)
        {
            GUILayout.Label($"SandBox Time:{(System.DateTime.Now - m_StartTime).TotalSeconds}", UCL_GUIStyle.LabelStyle);
            GUILayout.BeginHorizontal();
            GUILayout.Label("CurGameState", UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));
            m_GameState = UCL_GUILayout.PopupAuto(CurGameState, iDic, "GameState");
            GUILayout.EndHorizontal();
            int aIndex = 0;
            foreach (var aComponent in m_Components)
            {
                aComponent.ContentOnGUI(iDic.GetSubDic("Component", aIndex++));
            }
            UCL_GUILayout.DrawObjectData(this, iDic.GetSubDic("Data"));
        }
        /// <summary>
        /// 記錄所有SandBoxItems
        /// 存檔時根據在List中的index來保存
        /// </summary>
        /// <param name="iSandBox"></param>
        public void AddSandBoxItem(ATSI_SandBox iSandBox)
        {
            var aTypeName = iSandBox.GetType().Name;
            if (!m_SandBoxItems.ContainsKey(aTypeName))
            {
                m_SandBoxItems[aTypeName] = new ATS_Indexer();
            }
            m_SandBoxItems[aTypeName].AddItem(iSandBox);
        }
        public void RemoveSandBoxItem(ATSI_SandBox iSandBox)
        {
            var aTypeName = iSandBox.GetType().Name;
            if (!m_SandBoxItems.ContainsKey(aTypeName))
            {
                Debug.LogError($"RemoveSandBoxItem !m_SandBoxItems.ContainsKey(aTypeName), aTypeName:{aTypeName}");
                return;
            }
            m_SandBoxItems[aTypeName].RemoveItem(iSandBox);
        }
        public T GetSandBoxItemByIndex<T>(int iIndex) where T : class, ATSI_SandBox, new()
        {
            var aTypeName = typeof(T).Name;
            if (!m_SandBoxItems.ContainsKey(aTypeName))
            {
                return null;
            }
            return m_SandBoxItems[aTypeName].GetItem(iIndex) as T;
        }
        /// <summary>
        /// 讀檔結束時會觸發的Action
        /// </summary>
        /// <param name="iAction"></param>
        public void AddOnLoadEndAction(System.Action iAction)
        {
            m_OnLoadEndAction.Add(iAction);
        }
        public override JsonData SaveMain()
        {
            return SerializeToJson();
        }
        public override void SaveGame(ATS_SaveData saveData)
        {
            s_CurSaveSandBox = this;

            base.SaveGame(saveData);
            //SaveData aSaveData = new SaveData();
            //aSaveData.AddFile("SandBox", SerializeToJson());
            //aSaveData.AddFolder("AirShip", m_AirShip.SaveGame());

            //return aSaveData;
        }
        public override void LoadGame(ATS_SaveData iSaveData)
        {
            s_CurSaveSandBox = this;
            
            base.LoadGame(iSaveData);

            foreach(var aAct in m_OnLoadEndAction)
            {
                try
                {
                    aAct?.Invoke();
                }
                catch(System.Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
            m_OnLoadEndAction.Clear();
        }
    }

    public static partial class SandBoxExtension
    {
        //public static ATS_AirShip GetAirShip(this SandBoxBase iSandBoxBase)
        //{
        //    return iSandBoxBase.p_SandBox.m_AirShip;
        //}
        //public static ATS_RegionGrid GetAirShipRegionGrid(this SandBoxBase iSandBoxBase)
        //{
        //    return iSandBoxBase.GetAirShip().RegionGrid;
        //}
    }
}
