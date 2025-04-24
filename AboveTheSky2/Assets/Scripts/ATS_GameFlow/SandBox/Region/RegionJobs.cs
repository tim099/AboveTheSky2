
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
    /// <summary>
    /// 負責Job & Work
    /// </summary>
    public class RegionJobs : ATS_SandBoxBase
    {
        public List<ATS_Job> m_Jobs = new();
        public List<ATS_Work> m_Work = new();
        public override (SaveType, string) SaveKey => (SaveType.File, "RegionJobs");
        public void Add(ATS_Job job)
        {
            m_Jobs.Add(job);
            AddComponent(job);//要在AddComponent後 ATS_Minion才會Init
        }
        public void Add(ATS_Work work)
        {
            m_Work.Add(work);
            AddComponent(work);//要在AddComponent後 ATS_Minion才會Init
        }
        public void Remove(ATS_Job job)
        {
            m_Jobs.Remove(job);
            RemoveComponent(job);
        }
        public void Remove(ATS_Work work)
        {
            m_Work.Remove(work);
            RemoveComponent(work);
        }
        public override void LoadMain(JsonData iJson)
        {
            base.LoadMain(iJson);
            foreach (var job in m_Jobs)
            {
                AddComponent(job);//還原
            }
            foreach (var work in m_Work)
            {
                AddComponent(work);//還原
            }
            //Debug.LogError($"LoadMain RegionResources iJson:{iJson.ToJsonBeautify()}");
        }
    }
}
