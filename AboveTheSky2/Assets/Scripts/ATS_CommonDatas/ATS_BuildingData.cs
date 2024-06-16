
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
    public enum BuildingType
    {
        /// <summary>
        /// 入口(單位生成位置)
        /// </summary>
        Entrance,
        /// <summary>
        /// 工廠
        /// </summary>
        Factory,
        /// <summary>
        /// 武器
        /// </summary>
        Weapon,
        /// <summary>
        /// 提供路徑(例如梯子 可以讓船員上下移動)
        /// </summary>
        Path,
        /// <summary>
        /// 倉庫
        /// </summary>
        Storage,
    }
    public class ATS_BuildingData : UCL_Asset<ATS_BuildingData>
    {
        /// <summary>
        /// 是否需要建造過程(false則會立刻建造完成 且沒有成本)
        /// </summary>
        public bool m_RequireConstruct = true;

        /// <summary>
        /// 建造成本(建築被拆除時會返還)
        /// </summary>
        [UCL.Core.PA.Conditional("m_RequireConstruct", false, true)]
        public ATS_Recipe m_ConstructCost = new ATS_Recipe();

        /// <summary>
        /// 圖示Icon
        /// </summary>
        public UCL_SpriteAsset m_Sprite = new UCL_SpriteAsset();

        /// <summary>
        /// 該建築能夠生產的產品
        /// </summary>
        public List<ATS_RecipeEntry> m_Recipes = new ();

        public List<BuildingType> m_BuildingTypes = new List<BuildingType>();
        /// <summary>
        /// 包含建築額外提供的路徑(例如 梯子可以往上爬)
        /// </summary>
        public ATS_BuildingGrid m_GridData = new ATS_BuildingGrid();


        public Texture2D Texture => m_Sprite.Texture;
        public int Width => m_GridData.m_Width;
        public int Height => m_GridData.m_Height;
        /// <summary>
        /// 是否為倉庫
        /// </summary>
        public bool IsStorage => m_BuildingTypes.Contains(BuildingType.Storage);
        public bool CheckBuildingType(BuildingType iBuildingType) => m_BuildingTypes.Contains(iBuildingType);

        /// <summary>
        /// 獲取對應
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int GetPathState(int x, int y)
        {
            return m_GridData.Grid[x, y];
        }

        public override void Preview(UCL_ObjectDictionary iDataDic, bool iIsShowEditButton = false)
        {
            base.Preview(iDataDic, iIsShowEditButton);
            var aTexture = m_Sprite.Texture;
            if (aTexture != null)
            {
                float aUnitSize = UCL_GUIStyle.GetScaledSize(64f);
                GUILayout.Box(aTexture, GUILayout.Width(Width * aUnitSize), GUILayout.Height(Height * aUnitSize));
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
                    if((aGridIndex & (int)PathState.Occupied) != 0)//只抓取占用的位置
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
        public override int MaxIndex => int.MaxValue;

        /// <summary>
        /// 編輯時用來記錄當前選取的位置
        /// </summary>
        private Vector2Int CurSelectedPos { get; set; }
        /// <summary>
        /// 編輯用的TilePathState
        /// </summary>
        private TilePathState CurTilePathState { get; set; } = new TilePathState();
        public override object OnGUI(string iFieldName, UCL_ObjectDictionary iDataDic)
        {
            return base.OnGUI(iFieldName, iDataDic);
        }
        protected override void DrawCell(Rect iRect, Rect iCellRect, int x, int y, GUIStyle iButtonStyle)
        {
            //int aIndex = Grid[x, y];


            //if(CurSelectedPos.x == x && CurSelectedPos.y == y)
            //{
            //    GUI.DrawTexture(iCellRect, ATS_StaticTextures.TileFrame);
            //}


            if (GUI.Button(iCellRect, $"{x},{y}", iButtonStyle))//if (GUILayout.Button($"{Grid[x, y]}", aButtonStyle, aWidthOption, aHeightOption))
            {
                CurSelectedPos = new Vector2Int(x, y);
                //int aVal = Grid[x, y] + 1;
                //if (aVal >= MaxIndex)
                //{
                //    aVal = 0;
                //}
                //Grid[x, y] = aVal;
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
            GUI.DrawTexture(GetCellRect(CurSelectedPos.x, CurSelectedPos.y, 1, 1), ATS_StaticTextures.TileFrame);

            var aGridIndex = Grid[CurSelectedPos.x, CurSelectedPos.y];
            CurTilePathState.m_PathState = aGridIndex;

            UCL_GUILayout.DrawObjectData(CurTilePathState, iDataDic.GetSubDic("TilePathState"), $"TilePathState({CurSelectedPos.x},{CurSelectedPos.y})");

            Grid[CurSelectedPos.x, CurSelectedPos.y] = CurTilePathState.m_PathState;
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
