
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
        public GameObject m_Test;
        GameObject m_GameManager;
        
        private void Awake()
        {
            UCL_DebugLogService.Init();
            Init().Forget();
        }
        async UniTask Init()
        {
            var aCancellationToken = gameObject.GetCancellationTokenOnDestroy();
            await UniTask.WaitUntil(()=> UCL_ModuleService.Initialized, cancellationToken: aCancellationToken);
            var aGameManager = await Addressables.LoadAssetAsync<GameObject>("Assets/Addressables/Prefabs/UCL_GameManager.prefab");
            m_GameManager = Instantiate(aGameManager, transform);
            await ATS_IconSprite.InitSpriteAsset(aCancellationToken);
            m_Test.gameObject.SetActive(true);
        }
        private void Start()
        {
            
        }
    }
}
