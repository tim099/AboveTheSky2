
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UnityEngine;

namespace ATS
{



    [UCL.Core.ATTR.UCL_GroupIDAttribute(ATS_AssetGroup.ATS)]
    [UCL.Core.ATTR.UCL_Sort((int)ATS_AssetGroup.AssetType.ATS_Test)]
    public class ATS_Test : UCL_Asset<ATS_Test>
    {
        public override void Preview(UCL_ObjectDictionary iDataDic, bool iIsShowEditButton = false)
        {
            GUILayout.BeginVertical();
            var data = m_Region.GetData();
            data.Preview(iDataDic, false);//預覽區塊內容
            GUILayout.EndVertical();
        }
        /// <summary>
        /// 是否要顯示(false則此地塊隱藏)
        /// </summary>
        public bool m_Show = true;
        /// <summary>
        /// 地塊類型
        /// </summary>
        public ATS_TileDataEntry m_Tile = new ATS_TileDataEntry();
        /// <summary>
        /// 地圖區塊
        /// </summary>
        public ATS_RegionEntry m_Region = new ATS_RegionEntry();
    }

    public class ATS_TestEntry : UCL_AssetEntryDefault<ATS_Test>
    {
        public const string DefaultID = "Default";


        public ATS_TestEntry() { m_ID = DefaultID; }
        public ATS_TestEntry(string iID) { m_ID = iID; }
    }
}
