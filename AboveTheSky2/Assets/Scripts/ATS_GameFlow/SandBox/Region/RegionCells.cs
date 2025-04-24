
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    public class RegionCells : ATS_SandBoxBase
    {
        /// <summary>
        /// 所有的建築格(地塊)
        /// </summary>
        [UCL.Core.ATTR.UCL_HideInJson]
        public Cell[,] m_GridCells = null;

        /// <summary>
        /// 存檔用
        /// </summary>
        [SerializeField] private List<Cell> m_Cells = new List<Cell>();
        //public override string SaveKey => "RegionCells";
        public override (SaveType, string) SaveKey => (SaveType.File, "RegionCells");

        public void Init(ATS_RegionGrid iGridData)
        {
            m_GridCells = iGridData.CreateCells();
        }

        public override void LoadMain(JsonData iJson)
        {
            base.LoadMain(iJson);
            foreach (Cell aCell in m_Cells)
            {
                m_GridCells[aCell.m_Pos.x, aCell.m_Pos.y] = aCell;
            }
            m_Cells.Clear();//讀檔後清理
        }
        public override JsonData SaveMain()
        {
            foreach (Cell aCell in m_GridCells)
            {
                m_Cells.Add(aCell);
            }
            var aJson = base.SaveMain();
            m_Cells.Clear();
            return aJson;
        }
    }
}
