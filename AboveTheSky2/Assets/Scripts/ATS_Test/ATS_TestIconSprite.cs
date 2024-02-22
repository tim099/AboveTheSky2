
// ATS_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 02/20 2024 20:12
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UCL.Core;
using UnityEngine;
using UnityEngine.Networking;

namespace ATS
{
    public class ATS_TestIconSprite : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI m_TextMeshPro;
        // Start is called before the first frame update
        void Start()
        {
            m_TextMeshPro.spriteAsset = ATS_IconSprite.SpriteAsset;
            m_TextMeshPro.text += ATS_IconSpriteGenData.Icon_Heal.TMPKey;
            UCL.Core.UI.UCL_GUIPageController.CurrentRenderIns.Push(new Page.ATS_EditorMenuPage());
            //Test().Forget();
        }
        async UniTask Test()
        {
            //string aStr = await UCL_StreamingAssets.LoadString(".Install/CommonData/ATS_IconSprite/Icon_Heal.json");
            //Debug.Log(aStr);
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
