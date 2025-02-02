using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATS.UI
{
    public class ATS_EditorMenu
    {
        /// <summary>
        /// 標記目前是否在編輯器視窗中
        /// </summary>
        public static bool IsInEditWindow = false;
#if UNITY_EDITOR
        public static System.Action<ATS_EditorMenu> sShowEditWindowAct = null;
        [UCL.Core.ATTR.UCL_FunctionButton]
        public void ShowEditWindow()
        {
            sShowEditWindowAct?.Invoke(this);
        }
#endif
        UCL.Core.UI.UCL_GUIPageController m_GUIPageController = new UCL.Core.UI.UCL_GUIPageController();
        bool m_Inited = false;
        virtual public void Init()
        {

            if (m_Inited) return;
            m_Inited = true;
            UCL.Core.ServiceLib.UCL_UpdateService.AddUpdateAction(() =>
            {
                m_GUIPageController.Update();
            });
            //Debug.LogError("Application.isPlaying:" + Application.isPlaying);
        }
        virtual public void EditWindow(int iID)
        {
            //if (!Application.isPlaying) UCL.Core.UI.UCL_GUIPageController.Ins = m_GUIPageController;
            IsInEditWindow = true;
            var aPrevCol = GUI.contentColor;
            GUI.contentColor = Color.white;
            //Debug.LogError($"1 Event.current.type:{Event.current.type}");
            GUILayout.BeginVertical();
            if (m_GUIPageController.TopPage == null)//空的 新增一個選單頁面
            {
                m_GUIPageController.Push(new Page.ATS_EditorMenuPage());
            }
            m_GUIPageController.DrawOnGUI();

            GUILayout.EndVertical();

            GUI.contentColor = aPrevCol;
            IsInEditWindow = false;
        }
    }
}
