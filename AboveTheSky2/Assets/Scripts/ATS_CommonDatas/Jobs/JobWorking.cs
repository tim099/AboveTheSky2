
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using UnityEngine;

namespace ATS
{
    /// <summary>
    /// 建造or生產工作 到達建築並開始建造
    /// </summary>
    public class JobWorking : ATS_Job
    {
        public enum WorkingState
        {
            Init = 0,
            MoveToBuilding,
            Working,
        }
        /// <summary>
        /// 工作的目標建築(建造or生產)
        /// </summary>
        public ATS_BuildingRef m_Building = new();
        /// <summary>
        /// 對應的工作
        /// </summary>
        public ATS_WorkRef m_Work = new();
        public WorkingState m_WorkingState = WorkingState.Init;

        public JobWorking() { }
        public void Init(ATS_Building iBuilding, ATS_Work iWork)
        {
            m_Building.Value = iBuilding;
            m_Work.Value = iWork;
        }

        override public void WorkingUpdate()
        {
            ATS_Minion worker = m_Worker.Value;
            switch (m_WorkingState)
            {
                case WorkingState.Init:
                    {
                        //走到資源位置
                        var aPath = worker.PathFinder.FindPath(worker.m_Pos, m_Building.Value.m_Pos.ToATS_Vector3);
                        if (aPath == null)//找不到前往資源的路
                        {
                            SetJobState(JobState.Cancel);
                            //中斷
                            return;
                        }
                        worker.m_MoveData.m_Path = aPath;

                        m_WorkingState = WorkingState.MoveToBuilding;
                        break;
                    }
                case WorkingState.MoveToBuilding:
                    {
                        if (worker.MoveUpdate())//Move Complete
                        {
                            m_WorkingState = WorkingState.Working;
                            //Enter Building
                            m_Building.Value.EnterBuilding(worker);
                        }
                        break;
                    }
                case WorkingState.Working:
                    {
                        var work = m_Work.Value;
                        if (work == null || work.m_WorkState is ATS_Work.WorkState.Complete or ATS_Work.WorkState.Cancel)//工作完成 或 取消
                        {
                            SetJobState(JobState.Complete);
                        }
                        break;
                    }
            }

        }
    }
}