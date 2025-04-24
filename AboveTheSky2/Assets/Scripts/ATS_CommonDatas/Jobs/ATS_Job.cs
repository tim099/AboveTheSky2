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
    [UCL.Core.ATTR.UCL_IgnoreInTypeListable]
    public class ATS_Job : ATS_SandBoxBase, UCLI_TypeListable
    {

        /// <summary>
        /// 作為自動賦予Index的Key 有相同TypeName的類型會共用同一組Indexer
        /// UCLI_TypeListable都要特別處理 因為不同Type要共用TypeName
        /// </summary>
        public override string TypeName => typeof(ATS_Job).Name;


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

        //#region Interface
        //static List<System.Type> s_Types = null;
        //virtual public IList<System.Type> GetAllTypes()
        //{
        //    if(s_Types == null)
        //    {
        //        s_Types = new List<System.Type>();
        //        s_Types.Add(typeof(JobHauling));
        //    }
        //    return s_Types;
        //}
        //#endregion

        public JobState m_JobState = JobState.Pending;

        public ATS_MinionRef m_Worker = new();
        /// <summary>
        /// 工作已完成
        /// </summary>
        public bool Complete => m_JobState == JobState.Complete;
        /// <summary>
        /// 工作中斷
        /// </summary>
        public bool Cancel => m_JobState == JobState.Cancel;
        

        /// <summary>
        /// 開始執行工作
        /// </summary>
        virtual public void Start(ATS_Minion worker)
        {
            m_Worker.Value = worker;
            SetJobState(JobState.Working);
        }
        virtual public void End()
        {
            Region.Data.m_Jobs.Remove(this);
        }
        virtual public void WorkingUpdate()
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
    

    /// <summary>
    /// 細分的工作內容
    /// </summary>
    public class ATS_Task
    {

    }


    /// <summary>
    /// 工作(例如 搬運 建造)
    /// </summary>
    //public class ATS_JobData : UCL_Asset<ATS_JobData>
    //{
    //    /// <summary>
    //    /// 工作類型
    //    /// </summary>
    //    public enum JobType
    //    {
    //        /// <summary>
    //        /// 搬運
    //        /// </summary>
    //        Haul,

    //        /// <summary>
    //        /// 工作(包含建造等在建築內作業的工作)
    //        /// </summary>
    //        Work,
    //    }


    //    public JobType m_JobType = JobType.Haul;

    //    public override void Preview(UCL_ObjectDictionary iDataDic, bool iIsShowEditButton = false)
    //    {
    //        base.Preview(iDataDic, iIsShowEditButton);
    //    }
    //}
    //public class ATS_JobEntry : UCL_AssetEntryDefault<ATS_JobData>
    //{
    //    public const string DefaultID = "Haul";


    //    public ATS_JobEntry() { m_ID = DefaultID; }
    //    public ATS_JobEntry(string iID) { m_ID = iID; }


    //}
}
