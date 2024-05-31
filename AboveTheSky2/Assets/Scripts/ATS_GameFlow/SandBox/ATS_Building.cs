
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System;
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UnityEngine;

namespace ATS
{
    /// <summary>
    /// 建造模式中的緩存資料(包含建築與要建造的位置等)
    /// </summary>
    public class BuildData : UCL.Core.JsonLib.UnityJsonSerializable
    {
        /// <summary>
        /// 建造模式中 當前要建造的建築ID
        /// </summary>
        public string BuildingID
        {
            get => m_BuildingID;
            set
            {
                if (m_BuildingID == value) return;

                m_BuildingID = value;
                if (!string.IsNullOrEmpty(m_BuildingID))
                {
                    m_BuildingData = ATS_BuildingData.Util.GetData(m_BuildingID);
                }
                else
                {
                    m_BuildingData = null;
                }
            }
        }
        private string m_BuildingID;


        public ATS_BuildingData m_BuildingData = null;
        public Vector2Int m_Pos = Vector2Int.zero;
        public int m_PrevClickCount = 0;


        public IList<Vector2Int> BuildingCells
        {
            get
            {
                if(m_BuildingData == null)
                {
                    return Array.Empty<Vector2Int>();
                }
                return m_BuildingData.GetBuildingCells(m_Pos);
            }
        }
    }
    /// <summary>
    /// 實際的建築
    /// </summary>
    public class ATS_Building : SandBoxBase
    {
        public ATS_BuildingDataEntry m_BuildingDataEntry = new ATS_BuildingDataEntry();
        public int m_X;
        public int m_Y;


        /// <summary>
        /// 建築位置
        /// </summary>
        public Vector2Int Pos => new Vector2Int(m_X,m_Y);

        public ATS_BuildingData BuildingData => m_BuildingDataEntry.GetData();

        public IList<Vector2Int> BuildingCells => BuildingData.GetBuildingCells(Pos);


        public ATS_Building() { }
        public ATS_Building(string iID, Vector2Int iPos)
        {
            m_BuildingDataEntry.ID = iID;
            m_X = iPos.x;
            m_Y = iPos.y;
        }

        public void DrawOnGrid(ATS_RegionGrid iGrid)
        {
            BuildingData.DrawOnGrid(iGrid, m_X, m_Y);
        }

        public override void Init(ATS_SandBox iSandBox)
        {
            base.Init(iSandBox);
        }
        /// <summary>
        /// 用在ATS_SandboxPage
        /// </summary>
        override public void ContentOnGUI(UCL_ObjectDictionary iDic)
        {
            ATS_RegionGrid aGrid = p_SandBox.GetAirShipRegionGrid();
            BuildingData.DrawOnGrid(aGrid, Pos.x, Pos.y);
        }
    }
}
