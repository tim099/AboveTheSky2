
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    public interface ISandBox
    {
        void Init(ATS_SandBox iSandBox);
        /// <summary>
        /// Logic base update
        /// </summary>
        void GameUpdate();

        void ContentOnGUI();
    }
    /// <summary>
    /// base game enviroment
    /// </summary>
    public class ATS_SandBox : ISandBox
    {
        const int LogicIntervalMS = 100;
        public ATS_AirShip m_AirShip = new ATS_AirShip();


        private List<ISandBox> m_Components = new List<ISandBox>();
        private bool m_Inited = false;
        private bool m_End = false;
        private bool m_Pause = false;
        //private float m_TimeScale = 1f;


        private System.DateTime m_PrevUpdateTime;
        private System.DateTime m_StartTime;
        public void Init()
        {
            Init(this);
        }
        public void Init(ATS_SandBox iSandBox)
        {
            if(m_Inited)
            {
                return;
            }
            m_Inited = true;
            m_Components.Add(m_AirShip);
            foreach(var aComponent in m_Components)
            {
                aComponent.Init(iSandBox);
            }
            //m_AirShip.Init(iSandBox);
            UpdateLoop().Forget();

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
        public void GameUpdate()
        {
            //var aNow = System.DateTime.Now;
            //Debug.LogError($"GameUpdate() TotalMilliseconds:{(aNow - m_Test).TotalMilliseconds}");
            //m_Test = aNow;
            foreach (var aComponent in m_Components)
            {
                aComponent.GameUpdate();
            }
            //m_AirShip.GameUpdate();
        }
        /// <summary>
        /// 用在ATS_SandboxPage
        /// </summary>
        public void ContentOnGUI()
        {
            GUILayout.Label($"SandBox Time:{(System.DateTime.Now - m_StartTime).TotalSeconds}", UCL_GUIStyle.LabelStyle);
            foreach (var aComponent in m_Components)
            {
                aComponent.ContentOnGUI();
            }
        }
        
    }
}
