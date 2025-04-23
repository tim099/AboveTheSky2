
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using UnityEngine;

namespace ATS
{
    /// <summary>
    /// 搬運工作 把資源搬到倉庫or搬運建築生產材料
    /// </summary>
    public class JobHauling : ATS_Job
    {
        public enum HaulingState
        {
            Init = 0,
            MoveToResource,
            Haul,
            Hauling,
        }
        /// <summary>
        /// 要搬到哪裡(目標建築)
        /// </summary>
        public ATS_BuildingRef m_Building = new();
        /// <summary>
        /// 要搬運的資源
        /// </summary>
        public ATS_ResourceRef m_Resource = new();
        public HaulingState m_HaulingState = HaulingState.Init;

        public JobHauling() { }
        public void Init(ATS_Building iBuilding, ATS_Resource iResource)
        {
            m_Building.Value = iBuilding;
            m_Resource.Value = iResource;
            m_Resource.Value.SetState(ATS_Resource.ResourceState.PrepareToHaul);//避免被重複搬運
        }

        override public void WorkingUpdate(ATS_Minion iMinion)
        {
            switch (m_HaulingState)
            {
                case HaulingState.Init:
                    {
                        //走到資源位置
                        var aPath = iMinion.PathFinder.FindPath(iMinion.m_Pos, m_Resource.Value.m_Pos);
                        if (aPath == null)//找不到前往資源的路
                        {
                            SetJobState(JobState.Cancel);
                            m_Resource.Value.SetState(ATS_Resource.ResourceState.Dropped);
                            //中斷
                            return;
                        }
                        iMinion.m_MoveData.m_Path = aPath;

                        m_HaulingState = HaulingState.MoveToResource;
                        break;
                    }
                case HaulingState.MoveToResource:
                    {
                        if (iMinion.MoveUpdate())//Move Complete
                        {
                            m_Resource.Value.SetState(ATS_Resource.ResourceState.Hauling);
                            m_HaulingState = HaulingState.Haul;
                        }
                        break;
                    }
                case HaulingState.Haul:
                    {
                        var aPath = iMinion.PathFinder.FindPath(iMinion.m_Pos, m_Building.Value.m_Pos.ToATS_Vector3);
                        iMinion.m_MoveData.m_Path = aPath;
                        m_HaulingState = HaulingState.Hauling;
                        break;
                    }
                case HaulingState.Hauling:
                    {
                        if (iMinion.MoveUpdate())//Move Complete
                        {
                            m_Building.Value.AddToStorage(m_Resource.Value);//搬運完成 存入建築內


                            SetJobState(JobState.Complete);
                            //m_Completed = true;
                        }
                        else//搬運中
                        {
                            m_Resource.Value.m_Pos.Set(iMinion.m_Pos + new ATS_Vector3(0, iMinion.Height - 0.5f * ATS_Resource.ResourceSize, 0));
                        }
                        break;
                    }
            }

        }
    }
}
