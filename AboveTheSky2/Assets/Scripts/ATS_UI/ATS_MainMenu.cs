
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
using UCL;
using UCL.Core.TextureLib;
using UCL.Core.MathLib;
namespace ATS.UI
{
    public class ATS_MainMenu : UCL.Core.Game.UCL_GameUI
    {
        public static ATS_MainMenu Ins = null;
        public enum ButtonType : int
        {
            Test = 0,
            OpenEditorMenu,
            Play,
            GeneratePerm,
        }
        [System.Serializable]
        public class ButtonSetting
        {
            public ButtonType m_Type;
            public Button m_Button;
        }
        
        public static async UniTask<ATS_MainMenu> CreateAsync()
        {
            var aUI = await UCL_UIService.Ins.CreateUIFromAddressable<ATS_MainMenu>();
            return aUI;
        }

        [SerializeField] private GameObject m_Test;
        [SerializeField] private List<ButtonSetting> m_ButtonSettings;

        public RawImage m_RawImage;
        public RawImage m_RawImage2;
        public Material m_BlitMat;
        //[SerializeField] Button m_LoadAutoSaveButton = null;
        private bool m_LoadingUI = false;
        private bool m_Inited = false;
        private UCL_Texture2D m_Texture;
        public Vector2 m_OffSet = Vector2.zero;
        public float m_Scale = 10f;
        public override bool Reusable => true;
        private void Awake()
        {
            UCL_LocalizeManager.OnLanguageChanged += OnLanguageChanged;
            //m_TestButton.onClick.AddListener(Test);
            //m_OpenEditorMenuPageButton.onClick.AddListener(OpenEditorMenuPage);
            foreach(var aButtonSetting in m_ButtonSettings)
            {
                aButtonSetting.m_Button.onClick.AddListener(() => OnClickButton(aButtonSetting.m_Type));
            }
            const int Size = 1024;
            m_Texture = new UCL_Texture2D(Size, Size);
            //m_RawImage.gameObject.TrySetActive(true);
        }

        private void OnClickButton(ButtonType iButtonType)
        {
            switch(iButtonType)
            {
                case ButtonType.Test:
                    {
                        Test();
                        break;
                    }
                case ButtonType.OpenEditorMenu:
                    {
                        OpenEditorMenuPage();
                        break;
                    }
                case ButtonType.Play:
                    {
                        //UCL_Noise.GeneratePerm();

                        //UCL_BlitPass.s_RenderTexture;
                        m_RawImage.gameObject.SetActive(!m_RawImage.gameObject.activeSelf);
                        break;
                    }
                case ButtonType.GeneratePerm:
                    {
                        UCL_Noise.GeneratePerm();
                        break;
                    }
            }
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
        private float m_Timer = 0f;
        private void Update()
        {
            if (m_RawImage.gameObject.activeInHierarchy)
            {
                m_Timer += Time.deltaTime;
                if(m_Timer > 0.3f)
                {
                    m_Timer = 0;
                    m_Texture.Draw((iX, iY) =>
                    {
                        float aVal = UCL_Noise.PerlinNoiseUnsigned(m_Scale * (iX) + m_OffSet.x, m_Scale * (iY) + m_OffSet.y);
                        return new Color(aVal, aVal, aVal, 1f);
                    });
                    m_RawImage.texture = m_Texture.GetTexture();
                }

            }
            //m_RawImage.texture = UCL_BlitPass.s_RenderTexture;
            //if (UCL_BlitPass.s_RTHandle != null)
            //{
            //    m_RawImage2.texture = UCL_BlitPass.s_RTHandle;
            //}
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
