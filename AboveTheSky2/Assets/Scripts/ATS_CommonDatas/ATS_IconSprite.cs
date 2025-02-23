
// ATS_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 02/20 2024 19:16

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UCL.Core.LocalizeLib;
using UnityEngine;
using UCL.Core.UI;
using TMPro;
using UCL.Core;
using UCL.Core.Page;
using Cysharp.Threading.Tasks;
using System.Threading;


namespace ATS
{
    public class ATS_IconSpriteEntry : UCL_AssetEntryDefault<ATS_IconSprite>
    {
        public const string HealID = "Icon_Heal";
        public const string HealID2 = "Icon_Heal2";
        public const string DefaultID = HealID;

        static public ATS_IconSpriteEntry Icon_Heal => new ATS_IconSpriteEntry(HealID);
        static public ATS_IconSpriteEntry Icon_Heal2 => new ATS_IconSpriteEntry(HealID2);

        public ATS_IconSpriteEntry() { m_ID = DefaultID; }
        public ATS_IconSpriteEntry(string iID) { m_ID = iID; }


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
        /// <summary>
        /// Please InitSpriteAsset before access ATS_IconSprite.SpriteAsset
        /// </summary>
        public static TMP_SpriteAsset SpriteAsset 
        {
            get 
            { 
                //if(s_SpriteAsset == null)
                //{
                //    s_SpriteAsset = GenerateSpriteAsset();
                //}
                return s_SpriteAsset;
            }
        }
        public static async UniTask InitSpriteAsset(CancellationToken iToken)
        {
            if (s_SpriteAsset == null)
            {
                s_SpriteAsset = await GenerateSpriteAsset(iToken);
            }
        }
        public static void ClearSpriteAsset()
        {
            if (s_SpriteAsset == null) return;
            ATS_TMPTools.ClearSpriteAsset(s_SpriteAsset);
            s_SpriteAsset = null;
        }
        public static async UniTask<TMP_SpriteAsset> GenerateSpriteAsset(CancellationToken iToken)
        {
            List<Texture2D> aTextures = new List<Texture2D>();
            List<ATS_IconSprite> aIconSprites = new List<ATS_IconSprite>();
            List<string> aNames = new List<string>();
            var aIDs = ATS_IconSprite.Util.GetAllIDs();
            //Debug.LogError($"aIDs:{aIDs.ConcatString()}");
            foreach (var aIconSpriteID in aIDs)
            {
                try
                {
                    var aSpriteData = ATS_IconSprite.Util.GetData(aIconSpriteID);
                    if (aSpriteData.m_Disable)
                    {
                        continue;
                    }
                    aIconSprites.Add(aSpriteData);
                    var aSpriteEntry = aSpriteData.m_Sprite;
                    //Debug.LogError($"aSpriteData:{aSpriteData.ID},aSpriteEntry:{aSpriteEntry.ID}");
                    var aTexture = await aSpriteEntry.GetData().GetTextureAsync(iToken);
                    aTextures.Add(aTexture);//aSpriteData.IconTexture
                    aNames.Add(aIconSpriteID);
                }
                catch(System.Exception e)
                {
                    Debug.LogException(e);
                }

            }
            TMP_SpriteAsset aAsset = ATS_TMPTools.CreateSpriteAsset(aTextures, aNames, aIconSprites);
            return aAsset;
        }

        #endregion
        public const string DefaultIconsPath = ATS_SpriteData.SpriteFolder + "/Icons";

        
        public override UCL_SelectAssetPage<ATS_IconSprite> CreateSelectAssetPage()
        {
            var aPage = new ATS_IconSpriteEditorPage();
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
                if (IconTexture != null) GUILayout.Box(IconTexture, GUILayout.Width(UCL_GUIStyle.GetScaledSize(64)), GUILayout.Height(UCL_GUIStyle.GetScaledSize(64)));
                //UCL.Core.UI.UCL_GUILayout.LabelAutoSize(LocalizeName);

                if (iIsShowEditButton)
                {
                    if (GUILayout.Button(UCL_LocalizeManager.Get("Edit"), UCL_GUIStyle.ButtonStyle))
                    {
                        UCL_CommonEditPage.Create(this);
                    }
                }
            }
            GUILayout.EndHorizontal();
        }

        public ATS_IconSprite()
        {
            ID = "New IconSprite";
        }
        #endregion


        /// <summary>
        /// 圖示Icon
        /// </summary>
        public UCL_SpriteAssetEntry m_Sprite = new UCL_SpriteAssetEntry();

        /// <summary>
        /// 是否要包含在TMP圖集中
        /// </summary>
        public bool m_Disable = false;
        /// <summary>
        /// 縮放
        /// </summary>
        public float m_Scale = 1f;
        public float m_BearingX = 0f;
        public float m_BearingY = 0.85f;
        public string LocalizeName => UCL_LocalizeManager.Get(ID);

        virtual public Sprite IconSprite => m_Sprite.Sprite;
        virtual public Texture2D IconTexture => m_Sprite.Texture;
        public string TMPKey => $"<sprite name={ID}>";
    }

    public class ATS_IconSpriteEditorPage : UCL_SelectAssetPage<ATS_IconSprite>
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
            if (GUILayout.Button("Refresh SpriteAsset", UCL_GUIStyle.ButtonStyle, GUILayout.ExpandWidth(false)))
            {
                ATS_IconSprite.ClearSpriteAsset();
                //CreateSpriteAsset
            }
        }
 
    }

}