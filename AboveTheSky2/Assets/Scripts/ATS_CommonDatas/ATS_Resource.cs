
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.LocalizeLib;
using UCL.Core.Page;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    public class ATS_ResourceData : UCLI_ShortName
    {
        public ATS_ResourceEntry m_Resource = new ATS_ResourceEntry();
        public int m_Amount = 1;

        public string GetShortName() => ToString();
        public override string ToString()
        {
            return $"{m_Resource.ID} : {m_Amount}";
        }
        public void OnGUI()
        {
            using (var aScope = new GUILayout.HorizontalScope())
            {
                var aResData = m_Resource.GetData();
                int aSize = UCL_GUIStyle.GetScaledSize(24);
                if (aResData != null)
                {
                    var aTexture = aResData.IconTexture;
                    if(aTexture != null)
                    {
                        GUILayout.Box(aTexture, GUILayout.Width(aSize), GUILayout.Height(aSize));
                    }
                }
                GUILayout.Label(ToString(), UCL_GUIStyle.LabelStyle, GUILayout.Height(aSize));
            }
        }
    }
    public class ATS_ResourceEntry : UCL_AssetEntryDefault<ATS_Resource>
    {
        public const string DefaultID = "Clay";


        public ATS_ResourceEntry() { m_ID = DefaultID; }
        public ATS_ResourceEntry(string iID) { m_ID = iID; }


    }
    public class ATS_Resource : UCL_Asset<ATS_Resource>
    {
        public ATS_IconSpriteEntry m_Icon = new ATS_IconSpriteEntry();
        /// <summary>
        /// 基礎價格(價值)
        /// </summary>
        public int m_Price = 10;
        /// <summary>
        /// 占用倉儲的量(每單位)
        /// 例如Size = 100時 1000單位的倉庫可以放十份這個資源
        /// </summary>
        public int m_Size = 100;
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
                GUILayout.Label($"Price : {m_Price}", UCL.Core.UI.UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));
                GUILayout.Label($"Size : {m_Size}", UCL.Core.UI.UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));


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

        
        public ATS_Resource()
        {
            ID = "New Resource";
        }
        #endregion

        virtual public Texture2D IconTexture
        {
            get
            {
                var aData = m_Icon.GetData();
                if (aData == null) return null;
                return aData.IconTexture;
            }
        }
    }
}
