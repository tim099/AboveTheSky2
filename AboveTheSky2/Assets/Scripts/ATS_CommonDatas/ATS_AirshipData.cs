// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.LocalizeLib;
using UCL.Core.Page;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    public class ATS_AirshipData : UCL_Asset<ATS_AirshipData>
    {
        public ATS_RegionEntry m_Region = new ATS_RegionEntry();
        /// <summary>
        /// 預設建築
        /// </summary>
        public List<ATS_Building> m_Buildings = new List<ATS_Building>();

        public override void Preview(UCL_ObjectDictionary iDataDic, bool iIsShowEditButton = false)
        {
            GUILayout.BeginVertical();
            base.Preview(iDataDic, iIsShowEditButton);
            //GUILayout.Label("ATS_AirshipData.Preview", UCL_GUIStyle.LabelStyle);
            var aRegion = m_Region.GetData();
            if (aRegion != null)
            {
                var aGrid = aRegion.m_GridData;
                aGrid.DrawGrid(iDataDic.GetSubDic("Grid"));
                if (!m_Buildings.IsNullOrEmpty())
                {
                    foreach(var aBuilding in m_Buildings)
                    {
                        aBuilding.DrawOnGrid(aGrid);
                    }
                }
            }
            GUILayout.EndVertical();
        }


        //#region Runtime
        //public void Init(ATS_SandBox iSandBox)
        //{

        //}
        ///// <summary>
        ///// Logic base update
        ///// </summary>
        //public void GameUpdate()
        //{

        //}
        ///// <summary>
        ///// 用在ATS_SandboxPage
        ///// </summary>
        //public void ContentOnGUI()
        //{

        //}
        //#endregion
    }
    public class ATS_AirshipDataEntry : UCL_AssetEntryDefault<ATS_AirshipData>
    {
        public const string DefaultID = "AirShip_01";


        public ATS_AirshipDataEntry() { m_ID = DefaultID; }
        public ATS_AirshipDataEntry(string iID) { m_ID = iID; }
    }
}
