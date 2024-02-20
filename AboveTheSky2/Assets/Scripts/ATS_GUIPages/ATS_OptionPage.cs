using System.Collections;
using System.Collections.Generic;
using UCL.Core.LocalizeLib;
using UnityEngine;
using UCL.Core.EditorLib.Page;
namespace ATS.Page
{
    public class ButtonData
    {
        public ButtonData() { }
        public ButtonData(string iText, System.Action iClickAction = null, GUIStyle iStyle = null
            , bool iIsCloseOptionPageAfterClick = true)
        {
            m_Text = iText;
            m_ClickAction = iClickAction;
            m_Style = iStyle;
            m_IsCloseOptionPageAfterClick = iIsCloseOptionPageAfterClick;
        }
        public GUIStyle m_Style = null;
        public string m_Text = string.Empty;
        /// <summary>
        /// 是否在點選後關閉彈窗
        /// </summary>
        public bool m_IsCloseOptionPageAfterClick = true;
        public System.Action m_ClickAction = null;
    }
    /// <summary>
    /// 選項頁面
    /// 可以設定選項 標題 內文
    /// </summary>
    public class ATS_OptionPage : UCL_EditorPage
    {
        /// <summary>
        /// 確認刪除彈窗
        /// </summary>
        /// <param name="iDeleteTarget"></param>
        /// <param name="iConfirmAct"></param>
        /// <returns></returns>
        static public ATS_OptionPage ConfirmDelete(string iDeleteTarget, System.Action iConfirmAct)
        {
            var aPage = new ATS_OptionPage(UCL_LocalizeManager.Get("ConfirmDelete", iDeleteTarget), string.Empty
                        , new ButtonData(UCL_LocalizeManager.Get("Delete"), () => {
                            iConfirmAct?.Invoke();
                        }, UCL.Core.UI.UCL_GUIStyle.GetButtonStyle(Color.red))
                        , new ButtonData(UCL_LocalizeManager.Get("Cancel")));
            UCL.Core.UI.UCL_GUIPageController.CurrentRenderIns.Push(aPage);
            return aPage;
        }
        static public ATS_OptionPage Create(string iTitle, string iContext, params ButtonData[] iButtonDatas)
        {
            var aPage = new ATS_OptionPage(iTitle, iContext, iButtonDatas);
            UCL.Core.UI.UCL_GUIPageController.CurrentRenderIns.Push(aPage);
            return aPage;
        }
        //public override bool IsPopup => true;
        public override bool IsWindow => true;
        //public override string WindowName => UCL_LocalizeManager.Get("Alert");
        public string m_Title = string.Empty;
        public string m_Context = string.Empty;
        public ButtonData[] m_ButtonDatas = null;
        public ATS_OptionPage()
        {

        }
        public ATS_OptionPage(string iTitle, string iContext, params ButtonData[] iButtonDatas)
        {
            m_Title = iTitle;
            m_Context = iContext;
            m_ButtonDatas = iButtonDatas;
        }
        protected override void TopBar()
        {
            if(!string.IsNullOrEmpty(m_Title)) GUILayout.Label(m_Title, UCL.Core.UI.UCL_GUIStyle.GetLabelStyle(Color.white, 22));
        }
        protected override void ContentOnGUI()
        {
            if (!string.IsNullOrEmpty(m_Context)) GUILayout.Label(m_Context);
            if (!m_ButtonDatas.IsNullOrEmpty())
            {
                using (var aScope = new GUILayout.HorizontalScope("box"))
                {
                    for (int i = 0; i < m_ButtonDatas.Length; i++)
                    {
                        var aButtonData = m_ButtonDatas[i];
                        if (i > 0) GUILayout.Space(15);
                        bool aIsClicked = false;
                        if(aButtonData.m_Style != null)
                        {
                            aIsClicked = GUILayout.Button(aButtonData.m_Text, aButtonData.m_Style, GUILayout.MinWidth(60), GUILayout.ExpandWidth(false));
                        }
                        else
                        {
                            aIsClicked = GUILayout.Button(aButtonData.m_Text, GUILayout.MinWidth(60), GUILayout.ExpandWidth(false));
                        }
                        if (aIsClicked)
                        {
                            if (aButtonData.m_IsCloseOptionPageAfterClick)
                            {
                                BackButtonClicked();
                            }
                            aButtonData?.m_ClickAction?.Invoke();
                        }
                    }
                }
            }

        }
    }
}