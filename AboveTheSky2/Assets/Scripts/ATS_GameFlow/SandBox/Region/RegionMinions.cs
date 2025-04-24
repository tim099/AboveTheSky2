
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    public class RegionMinions : ATS_SandBoxBase
    {
        public List<ATS_Minion> m_Minions = new();

        public override (SaveType, string) SaveKey => (SaveType.File, "RegionMinions");
        public void Spawn(ATS_Minion iMinion)
        {
            m_Minions.Add(iMinion);
            AddComponent(iMinion);//要在AddComponent後 ATS_Minion才會Init
        }
        public override void LoadMain(JsonData iJson)
        {
            base.LoadMain(iJson);
            foreach (var aData in m_Minions)
            {
                AddComponent(aData);//還原
            }
            //Debug.LogError($"LoadMain RegionResources iJson:{iJson.ToJsonBeautify()}");
        }
    }
}
