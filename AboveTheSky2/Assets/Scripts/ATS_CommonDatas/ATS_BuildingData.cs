
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.LocalizeLib;
using UCL.Core.MathLib;
using UCL.Core.Page;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    public class ATS_BuildingData : UCL_Asset<ATS_BuildingData>
    {
        /// <summary>
        /// 建設工作量 0或以下代表無須建造過程
        /// </summary>
        public int m_Work = 100;
        /// <summary>
        /// 圖示Icon
        /// </summary>
        public UCL_SpriteAsset m_Sprite = new UCL_SpriteAsset();

        /// <summary>
        /// 建造材料
        /// </summary>
        public List<ATS_ResourceData> m_Consume = new List<ATS_ResourceData>();

        public List<ATS_RecipeEntry> m_Recipes = new ();

        public ATS_BuildingGrid m_GridData = new ATS_BuildingGrid();


        public Texture2D Texture => m_Sprite.Texture;
        public int Width => m_GridData.m_Width;
        public int Height => m_GridData.m_Height;

        public override void Preview(UCL_ObjectDictionary iDataDic, bool iIsShowEditButton = false)
        {
            base.Preview(iDataDic, iIsShowEditButton);
            var aTexture = m_Sprite.Texture;
            if (aTexture != null)
            {
                GUILayout.Box(aTexture, GUILayout.Width(64), GUILayout.Height(64));
            }
        }
        public void DrawOnGrid(ATS_RegionGrid iGrid, int x, int y)
        {
            var aTexture = Texture;
            if (aTexture == null)
            {
                return;
            }
            var aRect = iGrid.GetCellRect(x, y, Width, Height);
            GUI.DrawTexture(aRect, aTexture);
        }


        public override void OnGUI(UCL_ObjectDictionary iDataDic)
        {
            m_GridData.p_Building = this;

            base.OnGUI(iDataDic);
        }

        public List<Vector2Int> GetBuildingCells(Vector2Int iBuildPosition)
        {
            List<Vector2Int> aCells = new List<Vector2Int>();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var aGridIndex = m_GridData.Grid[x, y];
                    if(aGridIndex > 0)
                    {
                        aCells.Add(iBuildPosition + new Vector2Int(x, y));
                    }
                }
            }

            return aCells;
        }
    }
    public class ATS_BuildingDataEntry : UCL_AssetEntryDefault<ATS_BuildingData>
    {
        public const string DefaultID = "CraftingTable";


        public ATS_BuildingDataEntry() { m_ID = DefaultID; }
        public ATS_BuildingDataEntry(string iID) { m_ID = iID; }
    }
    /// <summary>
    /// 建築專用的Grid
    /// </summary>
    public class ATS_BuildingGrid : ATS_GridData
    {
        public ATS_BuildingData p_Building { get; set; } = null;
        public override int MaxIndex => 5;


        public override object OnGUI(string iFieldName, UCL_ObjectDictionary iDataDic)
        {
            return base.OnGUI(iFieldName, iDataDic);
        }
        protected override void DrawCell(Rect iRect, Rect iCellRect, int x, int y, GUIStyle iButtonStyle)
        {
            int aIndex = Grid[x, y];
            if (GUI.Button(iCellRect, $"{aIndex}", iButtonStyle))//if (GUILayout.Button($"{Grid[x, y]}", aButtonStyle, aWidthOption, aHeightOption))
            {
                int aVal = Grid[x, y] + 1;
                if (aVal >= MaxIndex)
                {
                    aVal = 0;
                }
                Grid[x, y] = aVal;
            }
        }
        public override void DrawGrid(UCL_ObjectDictionary iDataDic)
        {
            if (p_Building == null)
            {
                return;
            }
            GetGridRect(UCL_GUIStyle.GetScaledSize(64));

            var aTexture = p_Building.Texture;
            if (aTexture != null)
            {
                GUI.DrawTexture(GridRect, aTexture);
            }
            UCL_GUIStyle.PushGUIColor(new Color(1, 1, 1, 0.5f));
            DrawCells();
            UCL_GUIStyle.PopGUIColor();

            //GUILayout.Label($"Building:{p_Building.ID}", UCL_GUIStyle.LabelStyle);
            //float aGridSize = UCL_GUIStyle.GetScaledSize(32);
            //GridRect = GUILayoutUtility.GetRect(m_Width * aGridSize, m_Height * aGridSize, GUILayout.ExpandWidth(false));
            ////Rect aCellRect = new Rect(0, 0, aGridSize, aGridSize);

            //var aTexture = p_Building.Texture;
            //if (aTexture != null)
            //{
            //    GUI.DrawTexture(GridRect, aTexture);
            //}

            ////using (var aScope = new GUILayout.VerticalScope())
            //{
            //    UCL_GUIStyle.PushGUIColor(new Color(1, 1, 1, 0.5f));

            //    GUIStyle aButtonStyle = UCL_GUIStyle.GetButtonStyle(Color.white, 16);
            //    for (int y = 0; y < m_Height; y++)
            //    {
            //        for (int x = 0; x < m_Width; x++)
            //        {
            //            //aCellRect.position = aRect.position + new Vector2(x * aGridSize, y * aGridSize);
            //            var aCellRect = GetCellRect(x, y, 1, 1);
            //            DrawCell(GridRect, aCellRect, x, y, aButtonStyle);
            //        }
            //    }
            //    UCL_GUIStyle.PopGUIColor();
            //}
        }
    }
}
