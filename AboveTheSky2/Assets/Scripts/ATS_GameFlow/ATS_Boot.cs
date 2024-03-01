
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
            await UnityEngine.AddressableAssets.Addressables.InitializeAsync();

            //UCL.Core.UI.UCL_GUIPageController.CurrentRenderIns.Push(new Page.ATS_EditorMenuPage());

            await UniTask.WaitUntil(()=> UCL_ModuleService.Initialized, cancellationToken: aCancellationToken);
            //Debug.LogError("UCL_ModuleService.Initialized");
            var aGameManager = await m_GameManagerAssetEntry.GetData().LoadAsync(aToken);

            //var aGameManager = await Addressables.LoadAssetAsync<GameObject>("Assets/Addressables/Prefabs/ATS_GameManager.prefab");
            m_GameManager = Instantiate(aGameManager, null);
            await ATS_IconSprite.InitSpriteAsset(aCancellationToken);
            await UI.ATS_MainMenu.CreateAsync();
            //Debug.LogError("ATS_IconSprite.InitSpriteAsset");
        }
        private void Start()
        {
            
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
