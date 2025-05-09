
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
    public class RegionBuildings : ATS_SandBoxBase
    {
        public List<ATS_Building> m_Buildings = new List<ATS_Building>();
        public override SaveInfo SaveKey => new SaveInfo(SaveType.File, "RegionBuildings");
        public void Build(ATS_Building iBuilding)
        {
            m_Buildings.Add(iBuilding);
            AddComponent(iBuilding);//要在AddComponent後 ATS_Building才會Init
        }
        public override void LoadMain(JsonData iJson)
        {
            base.LoadMain(iJson);
            foreach (var aData in m_Buildings)
            {
                AddComponent(aData);//還原
            }
            //Debug.LogError($"LoadMain RegionResources iJson:{iJson.ToJsonBeautify()}");
        }
    }
}
