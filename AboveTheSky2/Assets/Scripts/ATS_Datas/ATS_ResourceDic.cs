
// ATS_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 02/20 2024 19:01

// ATS_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 02/20 2024 18:51
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATS
{
    /// <summary>
    /// 會在Editor模式下把資源分成Editor版本跟PlayMode版本
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ATS_ResourceDic<T>
    {
        public ATS_ResourceDic()
        {
#if UNITY_EDITOR
            ATS_StaticEvents.s_OnRefreshGamedata = ATS_StaticEvents.s_OnRefreshGamedata.AssignAction(Clear);
#endif
        }
        public void Clear()
        {
#if UNITY_EDITOR
            m_EditorMenuDatas.Clear();
            m_RuntimeDatas.Clear();
#endif
        }
        ~ATS_ResourceDic()
        {
#if UNITY_EDITOR
            ATS_StaticEvents.s_OnRefreshGamedata -= Clear;
#endif
        }

#if UNITY_EDITOR
        public Dictionary<string, T> Datas => UI.ATS_EditorMenu.IsInEditWindow ? m_EditorMenuDatas : m_RuntimeDatas;

        Dictionary<string, T> m_EditorMenuDatas = new Dictionary<string, T>();
        Dictionary<string, T> m_RuntimeDatas = new Dictionary<string, T>();
#else
        public Dictionary<string, T> Datas = new Dictionary<string, T>();
#endif
    }
}