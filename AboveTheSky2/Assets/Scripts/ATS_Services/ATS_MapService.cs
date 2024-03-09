
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
// Create time : 03/09 2024 12:22
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATS
{
    /// <summary>
    /// 負責處理地圖相關邏輯
    /// </summary>
    public class ATS_MapService : UCL.Core.Game.UCL_GameService
    {
        public static ATS_MapService Ins { get; private set; }


        public override void Init()
        {
            Ins = this;
            base.Init();
        }
    }
}
