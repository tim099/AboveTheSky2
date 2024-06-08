
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    public interface ISandBox
    {
        void Init(ATS_SandBox iSandBox, ISandBox iParent);
        /// <summary>
        /// Logic base update
        /// </summary>
        void GameUpdate();

        void ContentOnGUI(UCL_ObjectDictionary iDic);

        ATS_Region Region { get; }

        /// <summary>
        /// 當前所在地區
        /// </summary>
        ATS_RegionGrid RegionGrid { get; }

        ATS_PathFinder PathFinder { get; }
    }
    public class SandBoxBase : UCL.Core.JsonLib.UnityJsonSerializable, ISandBox, UCLI_FieldOnGUI
    {
        public ATS_SandBox p_SandBox { get; private set; } = null;
        public ISandBox Parent { get; private set; } = null;

        virtual public ATS_RegionGrid RegionGrid
        {
            get
            {
                if(Parent != null)
                {
                    return Parent.RegionGrid;
                }
                return p_SandBox.GetAirShipRegionGrid();
            }
        }
        virtual public ATS_Region Region
        {
            get
            {
                if (Parent == null)
                {
                    return null;
                }
                return Parent.Region;
            }
        }
        virtual public ATS_PathFinder PathFinder
        {
            get
            {
                if (Parent == null)
                {
                    return null;
                }
                return Parent.PathFinder;
            }
        }
        virtual public GameState CurGameState => p_SandBox.CurGameState;



        protected List<ISandBox> m_Components = new List<ISandBox>();//[UCL.Core.PA.UCL_FieldOnGUI]
        protected bool m_Inited = false;

        #region Getter
        //public ATS_AirShip AirShip => p_SandBox.m_AirShip;

        #endregion

        virtual public void Init(ATS_SandBox iSandBox, ISandBox iParent)
        {
            if (m_Inited)
            {
                return;
            }
            m_Inited = true;
            p_SandBox = iSandBox;
            Parent = iParent;
            //foreach (var aComponent in m_Components)
            //{
            //    aComponent.Init(iSandBox);
            //}
        }
        virtual public void AddComponent(ISandBox iComponent)
        {
            try
            {
                m_Components.Add(iComponent);
                iComponent.Init(p_SandBox, this);
            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
                Debug.LogError($"SandBoxBase.AddComponent({GetType().FullName}), Exception:{ex}");
                //throw new System.Exception($"SandBoxBase.AddComponent({GetType().FullName}), Exception:{ex}", ex);
            }

        }
        /// <summary>
        /// Logic base update
        /// </summary>
        virtual public void GameUpdate()
        {
            foreach (var aComponent in m_Components)
            {
                try
                {
                    aComponent.GameUpdate();
                }
                catch (System.Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }

        /// <summary>
        /// 用在ATS_SandboxPage
        /// </summary>
        virtual public void ContentOnGUI(UCL_ObjectDictionary iDic)
        {
            int aIndex = 0;
            foreach (var aComponent in m_Components)
            {
                aComponent.ContentOnGUI(iDic.GetSubDic("Component", aIndex++));
            }
        }
        virtual public object OnGUI(string iFieldName, UCL_ObjectDictionary iDataDic)
        {
            var aDrawObjExSetting = new UCL_GUILayout.DrawObjExSetting();
            void ShowComponents()
            {
                UCL_GUILayout.DrawObjectData(m_Components, iDataDic.GetSubDic("Components"), "Components");
            }
            aDrawObjExSetting.OnShowField = ShowComponents;
            UCL_GUILayout.DrawField(this, iDataDic.GetSubDic("Data"), iFieldName, iDrawObjExSetting: aDrawObjExSetting);
            
            return this;
        }
        public override JsonData SerializeToJson()
        {
            JsonData aJson = base.SerializeToJson();
            //Save Component
            return aJson;
        }
    }
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
    /// <summary>
    /// base game enviroment
    /// </summary>
    public class ATS_SandBox : SandBoxBase
    {
        const int LogicIntervalMS = 30;

        public ATS_AirShip m_AirShip = new ATS_AirShip();

        
        private bool m_End = false;
        private bool m_Pause = false;
        //private float m_TimeScale = 1f;


        private System.DateTime m_PrevUpdateTime;
        private System.DateTime m_StartTime;



        override public GameState CurGameState => m_GameState;
        public GameState m_GameState = GameState.Boot;

        public void Init()
        {
            Init(this, null);
        }
        override public void Init(ATS_SandBox iSandBox, ISandBox iParent)
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
        
    }

    public static partial class SandBoxExtension
    {
        public static ATS_AirShip GetAirShip(this SandBoxBase iSandBoxBase)
        {
            return iSandBoxBase.p_SandBox.m_AirShip;
        }
        public static ATS_RegionGrid GetAirShipRegionGrid(this SandBoxBase iSandBoxBase)
        {
            return iSandBoxBase.GetAirShip().RegionGrid;
        }
        //
    }
}
