
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections.Generic;
using UCL.Core;
using UnityEngine;

namespace ATS
{
    /// <summary>
    /// 工作(可以同時有多名工作者)
    /// 例如 建造建築物
    /// </summary>
    [UCL.Core.ATTR.UCL_IgnoreInTypeListable]
    public class ATS_Work : ATS_SandBoxBase, UCLI_TypeListable
    {
        /// <summary>
        /// 作為自動賦予Index的Key 有相同TypeName的類型會共用同一組Indexer
        /// UCLI_TypeListable都要特別處理 因為不同Type要共用TypeName
        /// </summary>
        public override string TypeName => typeof(ATS_Work).Name;

        public enum WorkState
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

        /// <summary>
        /// 工作者
        /// </summary>
        public List<ATS_MinionRef> m_Workers = new();

        public WorkState m_WorkState = WorkState.Pending;
        virtual public void Start()
        {
            m_WorkState = WorkState.Working;
        }
        virtual public void Update()
        {

        }
    }

    public class ATS_WorkRef : ATS_SandBoxRef<ATS_Work>
    {
        public ATS_WorkRef() { }
        public ATS_WorkRef(ATS_Work iJob)
        {
            Value = iJob;
        }
    }

    public static partial class WorkExtensions
    {
        /// <summary>
        /// 只執行對列第一個work
        /// </summary>
        /// <param name="works"></param>
        public static void UpdateWork(this IList<ATS_WorkRef> works)
        {
            if (works.IsNullOrEmpty())
            {
                return;
            }
            
            var work = works[0].Value;
            if (work == null)//理論上不應該發生 目前在讀檔後會觸發
            {
                string msg = $"UpdateWork work == null , Index:{works[0].Index}";
                //works.RemoveAt(0);
                Debug.LogError(msg);
                throw new System.Exception(msg);
                
                //return;
            }
            
            if(work.m_WorkState == ATS_Work.WorkState.Pending)
            {
                work.Start();
            }
            work.Update();
            if (work.m_WorkState is ATS_Work.WorkState.Complete or ATS_Work.WorkState.Cancel)
            {
                works.RemoveAt(0);//done or cancel
            }
        }
    }
}
