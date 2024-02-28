
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
// Create time : 02/27 2024 12:52
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UCL.Core.Game;
using UCL.Core.LocalizeLib;
using UnityEngine;
using UnityEngine.UI;
using UCL.Core;
using URP;
namespace ATS.UI
{
    public class ATS_MainMenu : UCL.Core.Game.UCL_GameUI
    {
        public static ATS_MainMenu Ins = null;

        
        public static async UniTask<ATS_MainMenu> CreateAsync()
        {
            var aUI = await UCL_UIService.Ins.CreateUIFromAddressable<ATS_MainMenu>();
            return aUI;
        }

        [SerializeField] private Button m_TestButton;

        [SerializeField] private Button m_OpenEditorMenuPageButton;

        [SerializeField] private GameObject m_Test;

        public RawImage m_RawImage;
        public Material m_BlitMat;
        //[SerializeField] Button m_LoadAutoSaveButton = null;
        private bool m_LoadingUI = false;
        private bool m_Inited = false;
        
        public override bool Reusable => true;
        private void Awake()
        {
            UCL_LocalizeManager.OnLanguageChanged += OnLanguageChanged;
            m_TestButton.onClick.AddListener(Test);
            m_OpenEditorMenuPageButton.onClick.AddListener(OpenEditorMenuPage);
        }
        private void Test()
        {
            m_Test.ToggleActiveState();
        }
        private void OpenEditorMenuPage()
        {
            UCL.Core.UI.UCL_GUIPageController.CurrentRenderIns.Push(new Page.ATS_EditorMenuPage());
        }
        private void OnDestroy()
        {
            UCL_LocalizeManager.OnLanguageChanged -= OnLanguageChanged;
            //UCL.Core.Game.UCL_GameAudioService.Instance.PopBGM();
        }
        public override void Init()
        {
            if (!m_Inited)
            {
                m_Inited = true;

            }
            //Debug.LogError("Init()");
            Ins = this;
        }

        public override void Close()
        {
            //Debug.LogError("Close");
            Ins = null;
            UCL.Core.Game.UCL_GameAudioService.Ins.StopBGM();
            base.Close();
        }

        protected void OnLanguageChanged()
        {
            //SetDifficulty(RCG_DataService.Ins.Difficulty);
        }

        private void Update()
        {
            //var aBlitToRenderTexture = new BlitToRenderTexture();
            //aBlitToRenderTexture.Material = m_BlitMat;
            //aBlitToRenderTexture.RemoveAfterBlit = true;
            //aBlitToRenderTexture.RenderPassEvent = UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPostProcessing;
            //URP_BlitRendererFeature.AddBlitRequest(aBlitToRenderTexture);

            //aBlitToRenderTexture.CompleteCallback = (iBlitRendererFeature) =>
            //{
            //    Debug.LogError("CompleteCallback");
            //    m_RawImage.texture = iBlitRendererFeature.RTHandle.rt;
            //};
        }
    }
}
