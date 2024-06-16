using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.LocalizeLib;
using UCL.Core.MathLib;
using UCL.Core.Page;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    /// <summary>
    /// Sanbox中使用的實際Job
    /// </summary>
    public class ATS_Job : UCL.Core.JsonLib.UnityJsonSerializable, UCLI_TypeList
    {
        static List<System.Type> s_Types = null;
        virtual public IList<System.Type> GetAllTypes()
        {
            if(s_Types == null)
            {
                s_Types = new List<System.Type>();
                s_Types.Add(typeof(JobHauling));
            }
            return s_Types;
        }

        virtual public void WorkingUpdate(ATS_Minion iMinion)
        {

        }
    }
    public class JobHauling : ATS_Job
    {
        public ATS_Building m_Building;
        public ATS_Resource m_Resource;

        public JobHauling() { }
        public void Init(ATS_Building iBuilding, ATS_Resource iResource)
        {
            m_Building = iBuilding;
            m_Resource = iResource;
            m_Resource.SetState(ATS_Resource.ResourceState.PrepareToHaul);
        }
        override public void WorkingUpdate(ATS_Minion iMinion)
        {
            if(iMinion.m_MoveData.m_Path == null)
            {
                var aPos = m_Resource.m_Pos.ToVector2Int;
                //return true if find target
                int CheckNode(Cell iCell, PathNode iNode)
                {
                    if (iCell.m_Pos == aPos)//距離滿足aTargetDistance 找到目標位置
                    {
                        return 0;
                    }
                    return int.MaxValue;//尚未找到目標 回傳與目標的距離
                }
                iMinion.m_MoveData.m_Path = iMinion.PathFinder.SearchPath(iMinion.m_Pos.x, iMinion.m_Pos.y, CheckNode);
            }
        }
    }

    /// <summary>
    /// 細分的工作內容
    /// </summary>
    public class ATS_Task
    {

    }


    /// <summary>
    /// 工作(例如 搬運 建造)
    /// </summary>
    public class ATS_JobData : UCL_Asset<ATS_JobData>
    {
        /// <summary>
        /// 工作類型
        /// </summary>
        public enum JobType
        {
            /// <summary>
            /// 搬運
            /// </summary>
            Haul,

            /// <summary>
            /// 工作(包含建造等在建築內作業的工作)
            /// </summary>
            Work,
        }


        public JobType m_JobType = JobType.Haul;

        public override void Preview(UCL_ObjectDictionary iDataDic, bool iIsShowEditButton = false)
        {
            base.Preview(iDataDic, iIsShowEditButton);
        }
    }
    public class ATS_JobEntry : UCL_AssetEntryDefault<ATS_JobData>
    {
        public const string DefaultID = "Haul";


        public ATS_JobEntry() { m_ID = DefaultID; }
        public ATS_JobEntry(string iID) { m_ID = iID; }


    }
}
