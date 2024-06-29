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
        [UCL.Core.ATTR.UCL_HideInJson]
        public ATS_Vector2Int m_Pos = new ATS_Vector2Int();
        /// <summary>
        /// 此格子上的建築(一個建築可以占用多個格子)
        /// </summary>
        public ATS_Building m_Building = null;

        /// <summary>
        /// 對應建築中的哪個格子(一個建築可以占用多個格子)
        /// </summary>
        public ATS_Vector2Int BuildingCellPos
        {
            get
            {
                if(m_Building == null)
                {
                    return m_Pos;
                }
                return m_Building.m_Pos - m_Pos;
            }
        }

        public ATS_TileData m_TileData = null;
        public int m_PathState = 0;
        /// <summary>
        /// 此地塊上的所有工作
        /// </summary>
        public List<ATS_Job> m_Jobs = new();
        /// <summary>
        /// 掉落在此地的資源
        /// </summary>
        [UCL.Core.ATTR.UCL_HideInJson]
        public List<ATS_Resource> m_Resources = new();// { get; private set; } = new ();
        public string GetShortName() => $"Cell[{m_Pos}]";
        public Cell() { }
        public Cell(ATS_TileData iTileData, int x, int y)
        {
            m_TileData = iTileData;
            m_Pos.x = x;
            m_Pos.y = y;
        }

        public bool CanBuild
        {
            get
            {
                if (m_Building != null) return false;//已被占用
                if (!m_TileData.CanBuild) return false;//非可建造地塊
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
                return !m_TileData.m_TilePathState.GetPathState(PathState.Obstacle);
            }
        }
        public int BuildingPathState
        {
            get
            {
                if (m_Building == null)
                {
                    return 0;
                }
                //return m_Building.BuildingData.GetPathState(m_BuildingCellPos.x, m_BuildingCellPos.y);
                return m_Building.GetPathState(m_Pos.x, m_Pos.y);
            }
        }
        public int SelfPathState
        {
            get
            {
                int aPathState = m_TileData.m_TilePathState.m_PathState;//(int)(PathState.Left | PathState.Right | PathState.Down);//所有可通行地塊都能往左右與下方移動
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
            m_Building = iBuilding;
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
                m_Jobs.Add(aJobHauling);
            }

        }
    }
}
