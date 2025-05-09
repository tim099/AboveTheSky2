
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

        /// <summary>
        /// 對應的ATS_SandBoxData
        /// </summary>
        public ATS_SandBoxData Data { get; set; } = null;

        public const int LogicIntervalMS = 30;

        /// <summary>
        /// TODO 同時支援多個AirShip
        /// </summary>
        public ATS_AirShip m_AirShip { get; set; } = null;

        //[UCL.Core.ATTR.UCL_HideInJson]
        [SerializeField]
        protected Dictionary<string, ATS_Indexer> m_SandBoxItems = new ();


        private System.DateTime m_PrevUpdateTime;
        private System.DateTime m_StartTime;
        private List<System.Action> m_OnLoadEndAction = new List<System.Action>();
        /// <summary>
        /// 每次Update時+1
        /// </summary>
        public long m_Timer = 0;
        //TODO 可以設定要在Timer等於特定值時執行的事

        override public GameState CurGameState => m_GameState;
        public override SaveInfo SaveKey => new SaveInfo(SaveType.Folder, "SandBox");

        public GameState m_GameState = GameState.Boot;
        /// <summary>
        /// 當前選取的建築
        /// </summary>
        public ATS_BuildingRef m_SelectedBuilding = new();
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
            base.Init(iSandBox, iParent);

            //SetAirShip(Data.m_InitAirship.GetData().Create());

            SetGameState(GameState.GameLoop);
            m_StartTime = System.DateTime.Now;
        }
        public void SetAirShip(ATS_AirShip airShip)
        {
            m_AirShip = airShip;

            AddComponent(m_AirShip);
        }
        /// <summary>
        /// 只在遊戲開始時觸發的初始化(讀檔時略過)
        /// </summary>
        public override void GameInit()
        {
            //SetAirShip(Data.m_InitAirship.GetData().Create());
            base.GameInit();
        }
        public void SetGameState(GameState gameState)
        {
            m_GameState = gameState;
            switch (m_GameState)
            {
                case GameState.GameLoop:
                    {
                        m_SelectedBuilding.Value = null;//clear
                        break;
                    }
            }
        }
        public void End()
        {
            //m_End = true;
        }

        /// <summary>
        /// Logic base update
        /// </summary>
        override public void GameUpdate()
        {
            //Debug.LogError($"GameUpdate():{++m_Test}");

            base.GameUpdate();
            ++m_Timer;
            //var aNow = System.DateTime.Now;
            //Debug.LogError($"GameUpdate() TotalMilliseconds:{(aNow - m_Test).TotalMilliseconds}");
            //m_Test = aNow;
            //foreach (var aComponent in m_Components)
            //{
            //    aComponent.GameUpdate();
            //}
            //m_AirShip.GameUpdate();
        }
        private ATS_ResourceEntry m_SpawnResType = new ATS_ResourceEntry();
        /// <summary>
        /// 用在ATS_SandboxPage
        /// </summary>
        override public void ContentOnGUI(UCL_ObjectDictionary iDic)
        {
            GUILayout.Label($"Timer:{m_Timer}, SandBox Time:{(System.DateTime.Now - m_StartTime).TotalSeconds}", UCL_GUIStyle.LabelStyle);
            GUILayout.BeginHorizontal();
            GUILayout.Label("CurGameState", UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));
            m_GameState = UCL_GUILayout.PopupAuto(CurGameState, iDic, "GameState");
            GUILayout.EndHorizontal();
            UCL_GUILayout.DrawObjectData(m_SpawnResType, iDic.GetSubDic("m_SpawnResType"));
            using (var aScope = new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Spawn", UCL_GUIStyle.ButtonStyle))
                {
                    m_AirShip.Spawn();
                }


                if (GUILayout.Button("Spawn Resource", UCL_GUIStyle.ButtonStyle))
                {
                    m_AirShip.SpawnResource();
                }
                switch (CurGameState)
                {
                    case GameState.Build:
                        {
                            if (GUILayout.Button("Cancel", UCL_GUIStyle.ButtonStyle))
                            {
                                SetGameState(GameState.GameLoop);
                            }
                            break;
                        }
                    case GameState.GameLoop:
                        {
                            if (GUILayout.Button("Build", UCL_GUIStyle.ButtonStyle))
                            {
                                SetGameState(GameState.Build);
                            }
                            break;
                        }
                }

            }

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
            var aTypeName = iSandBox.TypeName;
            if (!m_SandBoxItems.ContainsKey(aTypeName))
            {
                m_SandBoxItems[aTypeName] = new ATS_Indexer();
            }
            m_SandBoxItems[aTypeName].AddItem(iSandBox);
        }
        public void RemoveSandBoxItem(ATSI_SandBox iSandBox)
        {
            var aTypeName = iSandBox.TypeName;
            if (!m_SandBoxItems.ContainsKey(aTypeName))
            {
                Debug.LogError($"RemoveSandBoxItem !m_SandBoxItems.ContainsKey(aTypeName), aTypeName:{aTypeName}");
                return;
            }
            m_SandBoxItems[aTypeName].RemoveItem(iSandBox);
        }
        public T GetSandBoxItemByIndex<T>(int iIndex, string iTypeName) where T : class, ATSI_SandBox, new()
        {
            if (!m_SandBoxItems.ContainsKey(iTypeName))
            {
                return null;
            }
            return m_SandBoxItems[iTypeName].GetItem(iIndex) as T;
        }
        //public T GetSandBoxItemByIndex<T>(int iIndex) where T : class, ATSI_SandBox, new()
        //{
        //    var aTypeName = typeof(T).Name;
        //    if (!m_SandBoxItems.ContainsKey(aTypeName))
        //    {
        //        return null;
        //    }
        //    return m_SandBoxItems[aTypeName].GetItem(iIndex) as T;
        //}
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
            SetAirShip(new ATS_AirShip());
            base.LoadGame(iSaveData);
            
            foreach (var aAct in m_OnLoadEndAction)
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
