
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
    public class ATS_RecipeEntry : UCL_AssetEntryDefault<ATS_Recipe>
    {
        public const string DefaultID = "GoldIngot";


        public ATS_RecipeEntry() { m_ID = DefaultID; }
        public ATS_RecipeEntry(string iID) { m_ID = iID; }


    }
    public class ATS_Recipe : UCL_Asset<ATS_Recipe>
    {
        /// <summary>
        /// 原料
        /// </summary>
        public List<ATS_ResourceData> m_Consume = new List<ATS_ResourceData>();
        /// <summary>
        /// 產品
        /// </summary>
        public List<ATS_ResourceData> m_Product = new List<ATS_ResourceData>();
        /// <summary>
        /// 工作量
        /// </summary>
        public int m_Work = 100;

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
                if (iIsShowEditButton)
                {
                    if (GUILayout.Button(UCL_LocalizeManager.Get("Edit"), UCL_GUIStyle.ButtonStyle))
                    {
                        UCL_CommonEditPage.Create(this);
                    }
                }
                GUILayout.Label($"{UCL_LocalizeManager.Get("Preview")}({ID})", UCL.Core.UI.UCL_GUIStyle.LabelStyle);
                

                GUILayout.Label($"{UCL_LocalizeManager.Get("Consume")}", UCL.Core.UI.UCL_GUIStyle.LabelStyle);
                foreach(var aConsume in m_Consume)
                {
                    aConsume.OnGUI();
                    //GUILayout.Label($"{aConsume}", UCL.Core.UI.UCL_GUIStyle.LabelStyle);
                }
                GUILayout.Space(15);
                GUILayout.Label($"{UCL_LocalizeManager.Get("Product")}", UCL.Core.UI.UCL_GUIStyle.LabelStyle);
                foreach (var aProduct in m_Product)
                {
                    aProduct.OnGUI();
                    //GUILayout.Label($"{aProduct.ToString()}", UCL.Core.UI.UCL_GUIStyle.LabelStyle);
                }

                GUILayout.Label($"{UCL_LocalizeManager.Get("Work")} : {m_Work}", UCL.Core.UI.UCL_GUIStyle.LabelStyle);
                //UCL.Core.UI.UCL_GUILayout.LabelAutoSize(LocalizeName);


            }
            GUILayout.EndHorizontal();
        }


        public ATS_Recipe()
        {
            ID = "New Recipe";
        }
        #endregion
    }
}
