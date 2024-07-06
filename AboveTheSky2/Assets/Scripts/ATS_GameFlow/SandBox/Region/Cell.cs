using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    /// <summary>
    /// 基礎的單元格
    /// </summary>
    public class Cell : UCL.Core.JsonLib.UnityJsonSerializable, UCLI_ShortName
    {
        //[UCL.Core.ATTR.UCL_HideInJson]
        public ATS_Vector2Int m_Pos = new ATS_Vector2Int();
        /// <summary>
        /// 此格子上的建築(一個建築可以占用多個格子)
        /// </summary>
        public ATS_BuildingRef m_Building = new ATS_BuildingRef();

        
        public ATS_TileDataEntry m_TileDataEntry = new ATS_TileDataEntry();
        /// <summary>
        /// 路徑狀態
        /// </summary>
        public int m_PathState = 0;
        /// <summary>
        /// 此地塊上的所有工作
        /// </summary>
        public List<ATS_JobRef> m_Jobs = new();
        /// <summary>
        /// 掉落在此地的資源(存檔時忽略 因為ATS_Resource會自己在讀檔時還原)
        /// </summary>
        [UCL.Core.ATTR.UCL_HideInJson]
        public List<ATS_Resource> m_Resources = new();// { get; private set; } = new ();


        /// <summary>
        /// 對應建築中的哪個格子(一個建築可以占用多個格子)
        /// </summary>
        public ATS_Vector2Int BuildingCellPos
        {
            get
            {
                if(m_Building.Value == null)
                {
                    return m_Pos;
                }
                return m_Building.Value.m_Pos - m_Pos;
            }
        }
        public ATS_TileData TileData => m_TileDataEntry.GetData();


        public string GetShortName() => $"Cell[{m_Pos}]";
        public Cell() { }
        public Cell(string iTileID, int x, int y)
        {
            m_TileDataEntry.ID = iTileID;
            m_Pos.x = x;
            m_Pos.y = y;
        }

        public bool CanBuild
        {
            get
            {
                if (m_Building.Value != null) return false;//已被占用
                if (!TileData.CanBuild) return false;//非可建造地塊
                return true;
            }
        }

        /// <summary>
        /// 是否能夠進入此地塊
        /// </summary>
        public bool Passable
        {
            get
            {
                return !TileData.m_TilePathState.GetPathState(PathState.Obstacle);
            }
        }
        public int BuildingPathState
        {
            get
            {
                if (m_Building.Value == null)
                {
                    return 0;
                }
                //return m_Building.BuildingData.GetPathState(m_BuildingCellPos.x, m_BuildingCellPos.y);
                return m_Building.Value.GetPathState(m_Pos.x, m_Pos.y);
            }
        }
        /// <summary>
        /// 地塊本身&建築的通行資訊
        /// </summary>
        public int SelfPathState
        {
            get
            {
                int aPathState = TileData.m_TilePathState.m_PathState;//(int)(PathState.Left | PathState.Right | PathState.Down);//所有可通行地塊都能往左右與下方移動
                aPathState |= BuildingPathState;//額外抓取建築物的通行資訊

                return aPathState;
            }
        }
        public override JsonData SerializeToJson()
        {
            return base.SerializeToJson();
        }
        public void SetBuilding(ATS_Building iBuilding)
        {
            m_Building.Value = iBuilding;
        }
        /// <summary>
        /// 生成搬運工作
        /// </summary>
        public void GenerateHaulJob(ATS_Building iBuilding)
        {
            if (m_Resources.IsNullOrEmpty())
            {
                return;
            }
            foreach (var aRes in m_Resources.ToList())
            {
                Debug.LogError($"Cell.GenerateHaulJob, aRes:{aRes}");
                //aRes.SetState(ATS_Resource.ResourceState.PrepareToHaul);
                
                JobHauling aJobHauling = new JobHauling();
                aJobHauling.Init(iBuilding, aRes);
                iBuilding.Region.Data.m_Jobs.Add(aJobHauling);
                
                m_Jobs.Add(new ATS_JobRef(aJobHauling));
            }

        }
    }
}
