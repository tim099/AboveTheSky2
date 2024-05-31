
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
// Create time : 02/22 2024 09:14
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UCL.Core;
using UCL.Core.Game;
using UCL.Core.ServiceLib;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ATS
{
    public class ATS_Boot : MonoBehaviour
    {
        public static ATS_Boot Ins { get; private set; } = null;

        public UCL_GameObjectAssetEntry m_GameManagerAssetEntry = new UCL_GameObjectAssetEntry("GameManager");


        [SerializeField] private string m_GameManagerKey = "Assets/Addressables/Prefabs/ATS_GameManager.prefab";
        /// <summary>
        /// TestMode會直接使用m_GameManagerKey讀取GameManager
        /// </summary>
        [SerializeField] private bool m_TestMode = false;
        GameObject m_GameManager;
        
        private void Awake()
        {
            Ins = this;

            
            Init().Forget();
        }
        async UniTask Init()
        {
            UCL_DebugLogService.Init();
            UCL_RTHandleService.Init();

            var aToken = gameObject.GetCancellationTokenOnDestroy();
            //Debug.LogError("ATS_Boot.Init()");
            var aCancellationToken = gameObject.GetCancellationTokenOnDestroy();
            if (m_TestMode)
            {
                var aGameManager = await Addressables.LoadAssetAsync<GameObject>(m_GameManagerKey);
                m_GameManager = Instantiate(aGameManager, null);
                var aGM = m_GameManager.GetComponent<UCL_GameManager>();
                if (aGM != null)
                {
                    await aGM.InitAsync();
                }
                else
                {
                    Debug.LogError("ATS_Boot.Init() GM == null");
                }
            }

            await UnityEngine.AddressableAssets.Addressables.InitializeAsync();

            var aCatalogUpdates = await Addressables.CheckForCatalogUpdates(false);
            //UCL.Core.UI.UCL_GUIPageController.CurrentRenderIns.Push(new Page.ATS_EditorMenuPage());


            await UniTask.WaitUntil(()=> UCL_ModuleService.Initialized, cancellationToken: aCancellationToken);
            //Debug.LogError("UCL_ModuleService.Initialized");
            Debug.LogError($"UCL_ModuleService.Modules:{UCL_ModuleService.Ins.LoadedModules.ConcatString(iModule => iModule.ID)}");
            if (!m_TestMode)
            {
                var aGameManager = await m_GameManagerAssetEntry.GetData().LoadAsync(aToken);
                m_GameManager = Instantiate(aGameManager, null);
                var aGM = m_GameManager.GetComponent<UCL_GameManager>();
                if (aGM != null)
                {
                    await aGM.InitAsync();
                }
                else
                {
                    Debug.LogError("ATS_Boot.Init() GM == null");
                }
            }



            await ATS_IconSprite.InitSpriteAsset(aCancellationToken);

            await UI.ATS_MainMenu.CreateAsync();

            //UCL.Core.MathLib.UCL_Noise.GeneratePerm();
            //Debug.LogError("ATS_IconSprite.InitSpriteAsset");
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                UCL.Core.UI.UCL_GUIPageController.CurrentRenderIns.Push(new Page.ATS_EditorMenuPage());
            }
        }
    }
}
