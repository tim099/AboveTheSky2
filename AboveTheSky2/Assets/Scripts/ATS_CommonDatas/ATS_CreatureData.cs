
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.LocalizeLib;
using UCL.Core.MathLib;
using UCL.Core.Page;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    /// <summary>
    /// 生物基礎單位
    /// </summary>
    public class ATS_CreatureData : UCL_Asset<ATS_CreatureData>
    {
        /// <summary>
        /// 圖示Icon
        /// </summary>
        public UCL_SpriteAsset m_Sprite = new UCL_SpriteAsset();

        public Texture2D Texture => m_Sprite.Texture;

        public override void Preview(UCL_ObjectDictionary iDataDic, bool iIsShowEditButton = false)
        {
            base.Preview(iDataDic, iIsShowEditButton);
            var aTexture = m_Sprite.Texture;
            if (aTexture != null)
            {
                GUILayout.Box(aTexture, GUILayout.Width(64), GUILayout.Height(64));
            }
        }
    }
    public class ATS_CreatureDataEntry : UCL_AssetEntryDefault<ATS_CreatureData>
    {
        public const string DefaultID = "DefaultID";


        public ATS_CreatureDataEntry() { m_ID = DefaultID; }
        public ATS_CreatureDataEntry(string iID) { m_ID = iID; }
    }
}
