
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System;
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.UI;
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

    public enum BuildingState
    {
        /// <summary>
        /// 藍圖
        /// </summary>
        Blueprint,
        /// <summary>
        /// 建設完成
        /// </summary>
        Constructed,
        /// <summary>
        /// 被摧毀狀態
        /// </summary>
        Destroyed,
    }

    /// <summary>
    /// Sandbox中使用的建築
    /// </summary>
    public class ATS_Building : SandBoxBase
    {
        /// <summary>
        /// 建築類型
        /// </summary>
        public ATS_BuildingDataEntry m_BuildingDataEntry = new ATS_BuildingDataEntry();

        /// <summary>
        /// 建築位置
        /// </summary>
        public ATS_Vector2Int m_Pos = new ATS_Vector2Int();
        public BuildingState m_BuildingState = BuildingState.Constructed;
        /// <summary>
        /// 避免過度頻繁的判斷部分邏輯(例如搬運工作)
        /// </summary>
        public int m_LogicTimer = 0;

        #region Getter

        public ATS_BuildingData BuildingData => m_BuildingDataEntry.GetData();

        public IList<Vector2Int> BuildingCells => BuildingData.GetBuildingCells(m_Pos.ToVector2Int);
        #endregion

        public ATS_Building() { }
        public ATS_Building(string iID, Vector2Int iPos, BuildingState iBuildingState = BuildingState.Blueprint)
        {
            m_BuildingDataEntry.ID = iID;
            m_Pos.Set(iPos);
            //m_X = iPos.x;
            //m_Y = iPos.y;
            m_BuildingState = iBuildingState;
        }
        public override void DeserializeFromJson(JsonData iJson)
        {
            base.DeserializeFromJson(iJson);
            //m_Pos.m_X = m_X;
            //m_Pos.m_Y = m_Y;
        }

        /// <summary>
        /// 根據ATS_RegionGrid繪製在GUI上
        /// </summary>
        /// <param name="iGrid"></param>
        public void DrawOnGrid(ATS_RegionGrid iGrid)
        {
            Color? aGUIColor = null;
            switch (m_BuildingState)
            {
                case BuildingState.Blueprint:
                    {
                        aGUIColor = UCL_Color.Half.White;
                        break;
                    }
            }
            if (aGUIColor.HasValue) UCL_GUIStyle.PushGUIColor(aGUIColor.Value);

            BuildingData.DrawOnGrid(iGrid, m_Pos.x, m_Pos.y);

            if (aGUIColor.HasValue) UCL_GUIStyle.PopGUIColor();
        }

        public override void Init(ATS_SandBox iSandBox, ISandBox iParent)
        {
            base.Init(iSandBox, iParent);
        }
        /// <summary>
        /// 用在ATS_SandboxPage
        /// </summary>
        override public void ContentOnGUI(UCL_ObjectDictionary iDic)
        {
            ATS_RegionGrid aGrid = RegionGrid;
            DrawOnGrid(aGrid);
        }
        const int LogicUpdateInterval = 10;
        const int ResourceNotFindInterval = 30;
        public override void GameUpdate()
        {
            base.GameUpdate();
            if (m_LogicTimer > 0)
            {
                --m_LogicTimer;
            }
            else
            {
                m_LogicTimer = LogicUpdateInterval;
                if (BuildingData.IsStorage)//倉庫 判斷附近是否有能搬運的資源
                {
                    bool SearchResource(Cell iCell, PathNode iPathNode)
                    {
                        if (!iCell.m_Resources.IsNullOrEmpty())
                        {
                            return true;
                        }
                        return false;
                    }
                    var aResult = PathFinder.Search(m_Pos.x, m_Pos.y, SearchResource);

                    if(!aResult.IsNullOrEmpty())//有可搬運的資源
                    {
                        var aCell = aResult[0].Item1;
                        aCell.GenerateHaulJob(this);//生成搬運的工作
                        //if (!aCell.Resources.IsNullOrEmpty())
                        //{
                        //    var aTarget = aCell.Resources[0];

                        //    Debug.LogError($"ATS_Building.GameUpdate, aTarget:{aTarget}");
                        //}
                        
                    }
                    else
                    {
                        m_LogicTimer = ResourceNotFindInterval;//目前沒找到可搬運的資源 延長下次搜尋的間格
                    }
                }
            }
            
        }
    }
}
