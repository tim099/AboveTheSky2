
// ATS_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 02/20 2024 19:16

// ATS_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 02/20 2024 19:10
/* AutoHeader Test */
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UCL.Core.LocalizeLib;
using UnityEngine;
using UCL.Core.UI;
using TMPro;
using UCL.Core;
using UCL.Core.Page;


namespace ATS
{
    public class ATS_IconSpriteGenData : ATS_CommonGenDataDefault<ATS_IconSprite>
    {
        public const string HealID = "Icon_Heal";
        public const string DefaultID = HealID;

        static public ATS_IconSpriteGenData Icon_Heal => new ATS_IconSpriteGenData(HealID);


        public ATS_IconSpriteGenData() { m_ID = DefaultID; }
        public ATS_IconSpriteGenData(string iID) { m_ID = iID; }


        //public override bool IsEmpty => string.IsNullOrEmpty(m_ID);
        //public string GetDescription(string iDes) => RCG_BattleSetting.IsShowOnUI? TMPKey : iDes;
        public string TMPKey => $"<sprite name={m_ID}>";
        override public string ID { get => m_ID; set => m_ID = value; }
        public override string GetShortName()
        {
            var aSprite = GetData();
            if (aSprite == null) return string.Empty;
            return aSprite.LocalizeName;
        }
        public Sprite Icon
        {
            get
            {
                var aIconSprite = GetData();
                if (aIconSprite == null) return null;
                return aIconSprite.IconSprite;
            }
        }
    }
    /// <summary>
    /// TMP專用圖示
    /// </summary>
    public class ATS_IconSprite : UCL_Asset<ATS_IconSprite>
    {
        #region static
        private static TMP_SpriteAsset s_SpriteAsset = null;
        public static TMP_SpriteAsset SpriteAsset 
        {
            get 
            { 
                if(s_SpriteAsset == null)
                {
                    s_SpriteAsset = GenerateSpriteAsset();
                }
                return s_SpriteAsset;
            }
        }
        public static void CheckSpriteAsset()
        {
            if (s_SpriteAsset == null)
            {
                s_SpriteAsset = GenerateSpriteAsset();
            }
        }
        public static void ClearSpriteAsset()
        {
            if (s_SpriteAsset == null) return;
            ATS_TMPTools.ClearSpriteAsset(s_SpriteAsset);
            s_SpriteAsset = null;
        }
        public static TMP_SpriteAsset GenerateSpriteAsset()
        {
            List<Texture2D> aTextures = new List<Texture2D>();
            List<ATS_IconSprite> aIconSprites = new List<ATS_IconSprite>();
            List<string> aNames = new List<string>();
            foreach (var aIconSpriteID in ATS_IconSprite.Util.GetAllIDs())
            {
                var aSpriteData = ATS_IconSprite.Util.GetData(aIconSpriteID);
                aIconSprites.Add(aSpriteData);
                aTextures.Add(aSpriteData.IconTexture);
                aNames.Add(aIconSpriteID);
            }
            TMP_SpriteAsset aAsset = ATS_TMPTools.CreateSpriteAsset(aTextures, aNames, aIconSprites);
            return aAsset;
        }
        #endregion
        public const string DefaultIconsPath = ATS_SpriteData.SpriteFolder + "/Icons";

        
        public override UCL_CommonSelectPage<ATS_IconSprite> CreateCommonSelectPage()
        {
            var aPage = new RCG_IconSpriteEditorPage();
            UCL.Core.UI.UCL_GUIPageController.CurrentRenderIns.Push(aPage);
            return aPage;
        }

        #region must override 一定要override的部份
        /// <summary>
        /// 預覽
        /// </summary>
        /// <param name="iIsShowEditButton">是否顯示編輯按鈕</param>
        override public void Preview(UCL.Core.UCL_ObjectDictionary iDataDic, bool iIsShowEditButton = false)
        {
            GUILayout.BeginHorizontal();
            using (var aScope = new GUILayout.VerticalScope("box", GUILayout.MinWidth(130)))
            {

                GUILayout.Label($"{UCL_LocalizeManager.Get("Preview")}({ID})", UCL.Core.UI.UCL_GUIStyle.LabelStyle);
                if (IconTexture != null) GUILayout.Box(IconTexture, GUILayout.Width(64), GUILayout.Height(64));
                //UCL.Core.UI.UCL_GUILayout.LabelAutoSize(LocalizeName);

                if (iIsShowEditButton)
                {
                    if (GUILayout.Button(UCL_LocalizeManager.Get("Edit")))
                    {
                        UCL_CommonEditPage.Create(this);
                    }
                }
            }
            GUILayout.EndHorizontal();
        }

        public ATS_IconSprite()
        {
            ID = "New Skill";
        }
        public ATS_IconSprite(string iID)
        {
            Init(iID);
        }
        #endregion


        /// <summary>
        /// 圖示Icon
        /// </summary>
        public ATS_SpriteData m_Icon = new ATS_SpriteData(DefaultIconsPath);
        /// <summary>
        /// 縮放
        /// </summary>
        public float m_Scale = 1f;
        public float m_BearingX = 0f;
        public float m_BearingY = 0.85f;
        public string LocalizeName => UCL_LocalizeManager.Get(ID);

        public ATS_SpriteData Icon => m_Icon;
        virtual public Sprite IconSprite => m_Icon == null ? null : m_Icon.Sprite;
        virtual public Texture2D IconTexture => m_Icon == null ? null : m_Icon.Texture;
        public string TMPKey => $"<sprite name={ID}>";
    }

    public class RCG_IconSpriteEditorPage : UCL_CommonSelectPage<ATS_IconSprite>
    {

        protected override void ContentOnGUI()
        {
            base.ContentOnGUI();
        }

        protected override void TopBarButtons()
        {
            base.TopBarButtons();
//#if UNITY_EDITOR
//            if (GUILayout.Button("Output sprite sheet", GUILayout.ExpandWidth(false)))
//            {
//                ATS_TMPTools.CreateIconSpriteSheetEditor();
//            }
//#endif
            if (GUILayout.Button("Refresh SpriteAsset", GUILayout.ExpandWidth(false)))
            {
                ATS_IconSprite.ClearSpriteAsset();
                //CreateSpriteAsset
            }
        }
 
    }

}