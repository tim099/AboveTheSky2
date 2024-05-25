
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    /// <summary>
    /// Runtime airship
    /// </summary>
    public class ATS_AirShip : ISandBox
    {
        private int m_GameUpdateCount = 0;
        public void Init(ATS_SandBox iSandBox)
        {

        }

        /// <summary>
        /// Logic base update
        /// </summary>
        public void GameUpdate()
        {
            ++m_GameUpdateCount;
        }

        /// <summary>
        /// 用在ATS_SandboxPage
        /// </summary>
        public void ContentOnGUI()
        {
            GUILayout.Label($"ATS_AirShip m_GameUpdateCount:{m_GameUpdateCount}", UCL_GUIStyle.LabelStyle);
        }
    }
}
