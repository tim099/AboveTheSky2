using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ATS
{
    public class ATS_EditorMenuWindow : EditorWindow
    {
        [UnityEditor.InitializeOnLoadMethod]
        public static void Init()
        {
            ATS_EditorMenu.sShowEditWindowAct = (iCardEditor) => ATS_EditorMenuWindow.ShowWindow(iCardEditor);
        }
        Rect m_GridRegion = new Rect();
        public ATS_EditorMenu m_Editor;
        public void Init(ATS_EditorMenu iEditor)
        {
            m_Editor = iEditor;
        }
        [UnityEditor.MenuItem("ATS/Menu")]
        public static void ShowWindow()
        {
            ATS_EditorMenuWindow.ShowWindow(new ATS_EditorMenu());
        }
        [UnityEditor.MenuItem("ATS/RefreshGameData")]
        public static void Refresh()
        {
            //ATS_GameManager.RefreshGameDataStatic();
        }
        private void OnInspectorUpdate()
        {
            Repaint();
        }
        public static ATS_EditorMenuWindow ShowWindow(ATS_EditorMenu iTarget)
        {
            var aWindow = EditorWindow.GetWindow<ATS_EditorMenuWindow>("EditorMenu");
            aWindow.Init(iTarget);
            return aWindow;
        }
        private void OnGUI()
        {
            if (m_Editor == null)
            {
                m_Editor = new ATS_EditorMenu();
            }
            UCL.Core.UI.UCL_GUIStyle.IsInEditorWindow = true;
            m_Editor.Init();
            m_Editor.EditWindow(0);

            //Debug.LogError("OnGUI:" + System.DateTime.Now.ToString());
            if (Event.current.type == EventType.Repaint)
            {
                var aNewRgn = GUILayoutUtility.GetLastRect();
                if (aNewRgn != m_GridRegion || UCL.Core.UI.UCL_GUILayout.s_RequireRepaint)
                {
                    UCL.Core.UI.UCL_GUILayout.s_RequireRepaint = false;
                    m_GridRegion = aNewRgn;
                    Repaint();
                }
            }
            UCL.Core.UI.UCL_GUIStyle.IsInEditorWindow = false;
        }
    }
}
