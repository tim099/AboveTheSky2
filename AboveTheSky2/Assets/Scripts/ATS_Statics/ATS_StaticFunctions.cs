using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UCL.Core;
using UCL.Core.LocalizeLib;
using UCL.Core.Page;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ATS
{

    public static class ATS_StaticEvents
    {
        static public System.Action s_OnRefreshGamedata = null;
        /// <summary>
        /// 觸發刷新資料事件(清除檔案 資料夾路徑等快取)
        /// </summary>
        public static void TriggerOnRefreshGamedata()
        {
            if (s_OnRefreshGamedata != null)
            {
                s_OnRefreshGamedata.Invoke();
            }
        }
    }
    public static class ATS_StaticFunctions
    {
        public static string LocalizeFieldName(string iDisplayName)
        {
            if (iDisplayName[0] == 'm' && iDisplayName[1] == '_')
            {
                iDisplayName = iDisplayName.Substring(2, iDisplayName.Length - 2);
            }
            iDisplayName = UCL_LocalizeManager.Get(iDisplayName);
            return iDisplayName;
        }
    }
}

