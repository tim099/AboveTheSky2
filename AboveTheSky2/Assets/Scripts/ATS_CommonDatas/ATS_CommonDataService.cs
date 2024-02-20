using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATS
{
    /// <summary>
    /// 負責所有關於CommonData的操作
    /// </summary>
    public class ATS_CommonDataService
    {
        public static ATS_CommonDataService Ins
        {
            get
            {
                if(s_Ins == null)
                {
                    s_Ins = new ATS_CommonDataService();
                }
                return s_Ins;
            }
        }
        private static ATS_CommonDataService s_Ins = null;




    }
}
