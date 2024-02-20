using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATS
{
    public class ATS_Util<T> where T : class, new()
    {
        private static T s_Util = null;
        public static T Util => s_Util == null ? s_Util = new T() : s_Util;
    }
}
