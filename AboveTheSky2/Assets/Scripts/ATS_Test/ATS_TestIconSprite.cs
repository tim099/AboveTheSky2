
// ATS_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 02/20 2024 20:12
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
