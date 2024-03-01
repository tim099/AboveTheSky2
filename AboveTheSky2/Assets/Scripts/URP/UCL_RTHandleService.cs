
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
// Create time : 02/29 2024 09:45
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UCL.Core
{
    public class UCL_RTHandleService
    {
        public static UCL_RTHandleService Ins
        {
            get
            {
                if(s_Ins == null)
                {
                    Init();
                }
                return s_Ins;
            }
        }
        public static void Init()
        {
            if(s_Ins != null)
            {
                return;
            }
            s_Ins = new UCL_RTHandleService();
        }
        private static UCL_RTHandleService s_Ins = null;
        private static RTHandleSystem s_RTHandleSystem = null;
        private static bool s_Inited = false;

        
        private List<RTHandle> m_RTHandles =  new List<RTHandle> ();
        public UCL_RTHandleService()
        {
            if(!s_Inited)
            {
                s_Inited = true;
                RTHandles.Initialize(Screen.width, Screen.height);
                s_RTHandleSystem = new RTHandleSystem();
                s_RTHandleSystem.Initialize(Screen.width, Screen.height);
            }

        }
        void OnApplicationQuit()
        {
            foreach (var aHandle in m_RTHandles)
            {
                aHandle.Release();
                //RTHandles.Release(aHandle);
            }
            m_RTHandles.Clear();
        }
        //~UCL_RTHandleService()
        //{
        //    foreach(var aHandle in m_RTHandles)
        //    {
        //        RTHandles.Release(aHandle);
        //    }
        //    //m_RTHandles.Clear();
        //}
        public void Release(RTHandle iHandle)
        {
            if(iHandle == null)
            {
                return;
            }
            m_RTHandles.Remove(iHandle);
            iHandle.Release();
            //RTHandles.Release(iHandle);
        }

        public RTHandle Alloc(string iName, RenderTextureDescriptor iRenderTextureDescriptor)
        {
            var aHandle = RTHandles.Alloc(Vector2.one, iRenderTextureDescriptor, name: iName);
            
            m_RTHandles.Add(aHandle);
            return aHandle;
        }
    }
}
