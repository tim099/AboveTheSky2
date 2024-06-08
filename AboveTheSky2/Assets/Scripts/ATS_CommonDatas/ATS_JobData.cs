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
}
