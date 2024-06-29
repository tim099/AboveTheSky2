
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using UCL.Core;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    public class ATS_RegionEntry : UCL_AssetEntryDefault<ATS_RegionData>
    {
        public const string DefaultID = "AirShip_01";


        public ATS_RegionEntry() { m_ID = DefaultID; }
        public ATS_RegionEntry(string iID) { m_ID = iID; }
    }
    public class ATS_RegionData : UCL_Asset<ATS_RegionData>//, ISandBox
    {
        public ATS_RegionGrid m_GridData = new ATS_RegionGrid();

        public int Width => m_GridData.m_Width;
        public int Height => m_GridData.m_Height;


        public override void Preview(UCL_ObjectDictionary iDataDic, bool iIsShowEditButton = false)
        {
            GUILayout.BeginVertical();
            base.Preview(iDataDic, iIsShowEditButton);
            m_GridData.ContentOnGUI(iDataDic.GetSubDic("GridData"));

            GUILayout.EndVertical();
        }

        //#region SandBox
        //public void Init(ATS_SandBox iSandBox, ISandBox iParent)
        //{

        //    m_GridData.Init(iSandBox);
        //}

        ///// <summary>
        ///// Logic base update
        ///// </summary>
        //public void GameUpdate()
        //{
        //    m_GridData.GameUpdate();
        //}
        ///// <summary>
        ///// 用在ATS_SandboxPage
        ///// </summary>
        //public void ContentOnGUI(UCL_ObjectDictionary iDic)
        //{
        //    m_GridData.ContentOnGUI(iDic.GetSubDic("GridData"));
        //}
        //#endregion
    }

    /// <summary>
    /// 地區(含飛船)用的Grid
    /// </summary>
    public class ATS_RegionGrid : ATS_GridData
    {
        public static Vector2Int NullPos => Vector2Int.left;
        public enum GridEditMode
        {
            None,
            DrawTile,
        }
        public override int MaxIndex => ATS_TileData.Util.GetAllIDs().Count;

        public Vector2Int MousePos { get; private set; }
        private int m_TileIndex { get; set; } = 0;
        private GridEditMode EditMode { get; set; } = GridEditMode.None;

        public Cell[,] CreateCells()
        {
            var aIDs = ATS_TileData.Util.GetAllIDs();
            var aCells = new Cell[m_Width, m_Height];
            for (int y = 0; y < m_Height; y++)
            {
                for (int x = 0; x < m_Width; x++)
                {
                    ATS_TileData aTile = null;
                    int aIndex = Grid[x, y];
                    try
                    {
                        string aID = aIDs[aIndex];
                        aTile = ATS_TileData.Util.GetData(aID);
                    }
                    catch(System.Exception ex)
                    {
                        Debug.LogException(ex);
                        Debug.LogError($"CreateCells x:{x},y:{y},aIndex:{aIndex},Exception:{ex}");
                    }
                    finally
                    {
                        aCells[x, y] = new Cell(aTile, x, y);
                    }
                }
            }
            return aCells;
        }

        protected override void DrawCell(Rect iRect, Rect iGridRect, int x, int y, GUIStyle iButtonStyle)
        {
            //base.DrawCell(iRect, iGridRect, x, y, iButtonStyle);
            int aIndex = Grid[x, y];
            if (aIndex >= GridIDs.Count) aIndex = GridIDs.Count - 1;

            string aID = GridIDs[aIndex];
            var aTile = ATS_TileData.Util.GetData(aID);
            if (aTile.m_Show)
            {
                GUI.DrawTexture(iGridRect, aTile.Texture);
            }

            //if (GUI.Button(iGridRect, aTile.m_Sprite.Texture, iButtonStyle))//, $"{Grid[x, y]}"
            //{
            //    //int aVal = Grid[x, y] + 1;
            //    //if (aVal >= MaxIndex)
            //    //{
            //    //    aVal = 0;
            //    //}
            //    Grid[x, y] = m_TileIndex;
            //}

        }
        protected override void EditGrid(UCL_ObjectDictionary iDataDic)
        {
            GridIDs = ATS_TileData.Util.GetAllIDs();

            Texture2D aTileTexture = null;


            GUILayout.BeginHorizontal();
            GUILayout.Label("EditMode", UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));
            EditMode = UCL_GUILayout.PopupAuto(EditMode, iDataDic, "EditMode");
            GUILayout.EndHorizontal();

            switch (EditMode)
            {
                case GridEditMode.DrawTile:
                    {
                        GUILayout.BeginHorizontal();

                        GUILayout.Label("Tile", UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));

                        if (m_TileIndex >= 0 && m_TileIndex < GridIDs.Count)
                        {
                            float aSize = UCL_GUIStyle.GetScaledSize(22f);

                            string aID = GridIDs[m_TileIndex];
                            var aTile = ATS_TileData.Util.GetData(aID);
                            aTileTexture = aTile.Texture;
                            if (aTileTexture != null)
                            {
                                GUILayout.Box(aTileTexture, GUILayout.Width(aSize), GUILayout.Height(aSize));
                            }
                        }
                        m_TileIndex = UCL_GUILayout.PopupAuto(GridIDs, iDataDic, "TileID");
                        GUILayout.EndHorizontal();

                        break;
                    }
            }

            DrawGrid(iDataDic);


            if (MousePos != NullPos)
            {
                switch (EditMode)
                {
                    case GridEditMode.DrawTile:
                        {

                            if (aTileTexture != null)
                            {
                                var aRect = GetCellRect(MousePos.x, MousePos.y, 1f, 1f);//new Rect(aMousePos, aSize * Vector2.one)
                                UCL_GUIStyle.PushGUIColor(new Color(1, 1, 1, 0.7f));
                                GUI.DrawTexture(aRect, aTileTexture);
                                UCL_GUIStyle.PopGUIColor();
                            }

                            var aCurrentEvent = Event.current;
                            if (aCurrentEvent.clickCount > 0)
                            {
                                Grid[MousePos.x, MousePos.y] = m_TileIndex;
                            }
                            break;
                        }
                }

                DrawMouseFrame();
            }
        }
        public void DrawMouseFrame()
        {
            if (MousePos != NullPos)
            {
                var aRect = GetCellRect(MousePos.x, MousePos.y, 1f, 1f);
                GUI.DrawTexture(aRect, ATS_StaticTextures.TileFrame);
                //https://stackoverflow.com/questions/62224353/how-to-show-an-outlined-gui-box-in-unity
                string aText = $"{MousePos.x},{MousePos.y}";
                GUI.Label(aRect, aText, UCL_GUIStyle.GetLabelStyle(Color.green, 14));
                //GUI.Label(aRect, aText, UCL_GUIStyle.GetLabelStyle(Color.white, 15));
            }
        }
        public override void DrawGrid(UCL_ObjectDictionary iDataDic)
        {
            GridIDs = ATS_TileData.Util.GetAllIDs();
            base.DrawGrid(iDataDic);
            if (Event.current.type == EventType.Repaint)//只在Repaint時判斷
            {
                MousePos = UCL_GUILayout.GetMousePosInGrid(GridRect, m_Width, m_Height);
            }
        }

        public override void Init(ATS_SandBox iSandBox, ATSI_SandBox iParent)
        {
            base.Init(iSandBox, iParent);
        }
    }
}
