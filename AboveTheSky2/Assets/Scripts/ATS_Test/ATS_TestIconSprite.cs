
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
using UnityEngine.UI;

namespace ATS
{
    public class ATS_TestIconSprite : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI m_TextMeshPro;
        public Image m_Image;
        // Start is called before the first frame update
        void Start()
        {
            m_TextMeshPro.spriteAsset = ATS_IconSprite.SpriteAsset;
            {
                var aIcon = ATS_IconSpriteEntry.Icon_Heal;
                var aIconData = aIcon.GetData();
                if (aIconData != null)
                {
                    //m_TextMeshPro.text += aIcon.TMPKey;
                    m_Image.sprite = aIconData.IconSprite;
                }
                else
                {
                    Debug.LogError("ATS_TestIconSprite aIconData == null");
                }
                var aIDs = ATS_IconSprite.Util.GetAllIDs();
                foreach (var aID in aIDs)
                {
                    try
                    {
                        var aIconSprite = ATS_IconSprite.Util.GetData(aID);
                        if (aIconSprite != null && !aIconSprite.m_Disable)
                        {
                            m_TextMeshPro.text += aIconSprite.TMPKey;
                        }
                    }
                    catch(System.Exception e)
                    {
                        Debug.LogException(e);
                    }

                }
            }
            //{
            //    var aIcon = ATS_IconSpriteGenData.Icon_Heal2;
            //    var aIconData = aIcon.GetData();
            //    if (aIconData != null)
            //    {
            //        m_TextMeshPro.text += aIcon.TMPKey;
            //    }
            //}
            //UCL.Core.UI.UCL_GUIPageController.CurrentRenderIns.Push(new Page.ATS_EditorMenuPage());
            //Test().Forget();
        }
        //async UniTask Test()
        //{
        //    //string aStr = await UCL_StreamingAssets.LoadString(".Install/CommonData/ATS_IconSprite/Icon_Heal.json");
        //    //Debug.Log(aStr);
        //}
        // Update is called once per frame
        void Update()
        {
            //m_TextMeshPro.spriteAsset = ATS_IconSprite.SpriteAsset;
        }
    }
}
