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
    public class ATS_Job : ATS_SandBoxBase //UCL.Core.JsonLib.UnityJsonSerializable
        , UCLI_TypeList
    {
        public enum JobState
        {
            /// <summary>
            /// 尚未開始
            /// </summary>
            Pending = 0,
            /// <summary>
            /// 工作中
            /// </summary>
            Working,
            /// <summary>
            /// 中斷(例如搬運時找不到路徑)
            /// </summary>
            Cancel,
            /// <summary>
            /// 已完成
            /// </summary>
            Complete,
        }

        #region Interface
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
        #endregion

        public JobState m_JobState = JobState.Pending;

        /// <summary>
        /// 工作已完成
        /// </summary>
        public bool Complete => m_JobState == JobState.Complete;
        /// <summary>
        /// 工作中斷
        /// </summary>
        public bool Cancel => m_JobState == JobState.Cancel;
        public override string TypeName => typeof(ATS_Job).Name;

        /// <summary>
        /// 開始執行工作
        /// </summary>
        virtual public void Start()
        {
            SetJobState(JobState.Working);
        }
        virtual public void End()
        {
            Region.Data.m_Jobs.Remove(this);
        }
        virtual public void WorkingUpdate(ATS_Minion iMinion)
        {

        }
        virtual public void SetJobState(JobState state)
        {
            m_JobState = state;
        }
    }

    public class ATS_JobRef : ATS_SandBoxRef<ATS_Job>
    {
        public ATS_JobRef() { }
        public ATS_JobRef(ATS_Job iJob)
        {
            Value = iJob;
        }
        //public override string TypeName => base.TypeName;
        //public override void DeserializeFromJson(JsonData iJson)
        //{
        //    base.DeserializeFromJson(iJson);
        //    var aIndex = iJson.GetInt(-1);
        //    Debug.LogError($"ATS_JobRef Index:{aIndex}");
        //}
    }
    public class JobHauling : ATS_Job
    {
        public enum HaulingState
        {
            Init = 0,
            MoveToResource,
            Haul,
            Hauling,
        }
        public ATS_BuildingRef m_Building = new ();
        public ATS_ResourceRef m_Resource = new ();
        public HaulingState m_HaulingState = HaulingState.Init;
        
        public JobHauling() { }
        public void Init(ATS_Building iBuilding, ATS_Resource iResource)
        {
            m_Building.Value = iBuilding;
            m_Resource.Value = iResource;
            m_Resource.Value.SetState(ATS_Resource.ResourceState.PrepareToHaul);
        }

        override public void WorkingUpdate(ATS_Minion iMinion)
        {
            switch (m_HaulingState)
            {
                case HaulingState.Init:
                    {
                        //走到資源位置
                        var aPath = iMinion.PathFinder.FindPath(iMinion.m_Pos, m_Resource.Value.m_Pos);
                        if(aPath == null)//找不到前往資源的路
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
                            //搬運完成
                            m_Resource.Value.AddToStorage();
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
