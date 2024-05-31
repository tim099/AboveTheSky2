
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    /// <summary>
    /// 基礎的單元格
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// 此格子上的建築(一個建築可以占用多個格子)
        /// </summary>
        public ATS_Building m_Building = null;
        public ATS_TileData m_TileData = null;

        public Cell() { }
        public Cell(ATS_TileData iTileData)
        {
            m_TileData = iTileData;
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
    }


    /// <summary>
    /// Runtime airship
    /// </summary>
    public class ATS_AirShip : SandBoxBase
    {
        public class RuntimeData : UnityJsonSerializable
        {
            public List<ATS_Building> m_Buildings = new List<ATS_Building>();
        }
        #region Runtime
        private int m_GameUpdateCount = 0;
        private ATS_AirshipData m_AirshipData = null;

        private ATS_Region m_Region = null;

        private RuntimeData m_RuntimeData = new RuntimeData();
        /// <summary>
        /// 建造模式中的緩存資料(包含建築與要建造的位置等)
        /// </summary>
        private BuildData m_BuildData = new BuildData();
        /// <summary>
        /// 所有的建築格(地塊)
        /// </summary>
        private Cell[,] m_Cells = null;
        #endregion



        #region Getter
        public ATS_RegionGrid RegionGrid => m_Region.m_GridData;
        public int Width => m_Region.Width;
        public int Height => m_Region.Height;
        #endregion

        override public void Init(ATS_SandBox iSandBox)
        {
            base.Init(iSandBox);
            //暫時抓取預設的AirShip(初始飛船)
            ATS_AirshipDataEntry aAirshipDataEntry = new ATS_AirshipDataEntry();
            m_AirshipData = aAirshipDataEntry.GetData(false);
            m_Region = m_AirshipData.m_Region.GetData(false);
            m_Region.Init(iSandBox);
            m_Cells = m_Region.CreateCells();


            foreach(var aBuilding in m_AirshipData.m_Buildings)//建造預設建築
            {
                try
                {
                    Build(aBuilding.CloneObject());
                }
                catch(System.Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }

        /// <summary>
        /// 判斷BuildData是否為可以建造的狀態
        /// </summary>
        /// <returns></returns>
        public bool CheckCanBuild(BuildData iBuildData)
        {
            var aBuildingData = iBuildData.m_BuildingData;
            if (aBuildingData == null)
            {
                return false;
            }
            var aCellsPos = iBuildData.BuildingCells;
            foreach(var aCellPos in aCellsPos)
            {
                var aCell = m_Cells[aCellPos.x, aCellPos.y];
                if (!aCell.CanBuild)//已被占用
                {
                    return false;
                }
            }
            //m_Cells[x, y]
            return true;
        }
        public void Build(ATS_Building iBuilding)
        {
            m_RuntimeData.m_Buildings.Add(iBuilding);
            AddComponent(iBuilding);//要在AddComponent後 ATS_Building才會Init

            var aCellsPos = iBuilding.BuildingCells;
            foreach (var aCellPos in aCellsPos)//將建築設定到地塊上
            {
                var aCell = m_Cells[aCellPos.x, aCellPos.y];
                aCell.m_Building = iBuilding;
            }


        }
        /// <summary>
        /// Logic base update
        /// </summary>
        override public void GameUpdate()
        {
            ++m_GameUpdateCount;
            m_Region.GameUpdate();
        }

        /// <summary>
        /// 用在ATS_SandboxPage
        /// </summary>
        override public void ContentOnGUI(UCL_ObjectDictionary iDic)
        {
            GUILayout.Label($"ATS_AirShip m_GameUpdateCount:{m_GameUpdateCount}", UCL_GUIStyle.LabelStyle);
            switch (p_SandBox.CurGameState)
            {
                case GameState.Build:
                    {
                        var aIDs = ATS_BuildingData.Util.GetAllIDs();

                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Building ID", UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));
                        m_BuildData.BuildingID = UCL_GUILayout.PopupAuto(m_BuildData.BuildingID, aIDs, iDic, "BuildingID");
                        GUILayout.EndHorizontal();
                        //if(m_BuildData.m_BuildingData != null)
                        //{
                        //    var aTexture = m_BuildData.m_BuildingData.Texture;
                        //    if(aTexture != null)
                        //    {
                        //        float aSize = UCL_GUIStyle.GetScaledSize(64);
                        //        GUILayout.Box(aTexture,GUILayout.Width(aSize),GUILayout.Height(aSize));
                        //    }
                        //}
                        
                        break;
                    }
            }
            using (var aScope = new GUILayout.HorizontalScope())
            {
                GUILayout.Space(UCL_GUIStyle.GetScaledSize(32));
                using (var aScope2 = new GUILayout.VerticalScope())
                {
                    m_Region.ContentOnGUI(iDic.GetSubDic("Region"));
                }  
            }

            base.ContentOnGUI(iDic.GetSubDic("BaseContentOnGUI"));//繪製建築
            var aGrid = p_SandBox.GetAirShipRegionGrid();

            switch (p_SandBox.CurGameState)
            {
                case GameState.Build:
                    {
                        var aBuildingData = m_BuildData.m_BuildingData;
                        //Debug.LogError($"MousePos:{m_Region.m_GridData.MousePos},Event.current.type:{Event.current.type}");

                        if (aBuildingData != null)
                        {
                            var aPosition = m_BuildData.m_Pos;
                            aBuildingData.DrawOnGrid(aGrid, aPosition.x, aPosition.y);

                            {
                                int aWidth = Width;
                                int aHeight = Height;
                                void DrawMoveButton(int x, int y, Vector2Int dir, string name)
                                {
                                    int aX = aPosition.x + x;
                                    int aY = aPosition.y + y;
                                    if(aX < 0 || aX >= aWidth || aY < 0 || aY >= aHeight)
                                    {
                                        return;
                                    }
                                    var aRect = m_Region.m_GridData.GetCellRect(aX, aY, 1, 1);
                                    if (GUI.Button(aRect, name))
                                    {
                                        m_BuildData.m_Pos += dir;
                                    }
                                }

                                void DrawBuild(int x, int y)
                                {
                                    int aX = aPosition.x + x;
                                    int aY = aPosition.y + y;
                                    if (aX < 0 || aX >= aWidth || aY < 0 || aY >= aHeight)//Out of range
                                    {
                                        return;
                                    }
                                    var aRect = m_Region.m_GridData.GetCellRect(aX, aY, 1, 1);
                                    bool aCanBuild = CheckCanBuild(m_BuildData);
                                    string aName = aCanBuild? "✔️":"X";
                                    //CheckCanBuild
                                    if (GUI.Button(aRect, aName, UCL_GUIStyle.GetButtonStyle(aCanBuild? Color.green : Color.red)))//
                                    {
                                        if (aCanBuild)
                                        {
                                            Debug.Log($"Build:{aBuildingData.ID},aPosition:{aPosition}");
                                            var aBuilding = new ATS_Building(aBuildingData.ID, aPosition);
                                            Build(aBuilding);
                                        }
                                    }
                                }
                                UCL_GUIStyle.PushGUIColor(new Color(1, 1, 1, 0.5f));
                                DrawMoveButton(-1, 0, Vector2Int.left, "A");
                                DrawMoveButton(1, 0, Vector2Int.right, "D");
                                DrawMoveButton(0, 1, Vector2Int.up, "W");
                                DrawMoveButton(0, -1, Vector2Int.down, "S");
                                
                                //UCL_GUIStyle.RestoreGUIColor();

                                //UCL_GUIStyle.SetGUIColor(new Color(1, 1, 1, 0.4f));
                                DrawBuild(0, 0);
                                UCL_GUIStyle.PopGUIColor();

                            }
                        }
                        
                        var aCurrentEvent = Event.current;
                        //Debug.LogError($"m_BuildData.m_PrevClickCount:{m_BuildData.m_PrevClickCount},aCurrentEvent.clickCount:{aCurrentEvent.clickCount}");
                        if (m_BuildData.m_PrevClickCount > 0 && aCurrentEvent.clickCount == 0)//點擊後
                        {
                            
                            var aPos = m_Region.m_GridData.MousePos;
                            //Debug.LogError($"Click!! aPos:{aPos}");
                            if (aPos != ATS_RegionGrid.NullPos)
                            {
                                m_BuildData.m_Pos = aPos;
                            }
                        }

                        m_BuildData.m_PrevClickCount = aCurrentEvent.clickCount;
                        break;
                    }
            }
            
            UCL_GUILayout.DrawObjectData(m_RuntimeData.m_Buildings, iDic.GetSubDic("m_Buildings"), "Buildings");
            GUILayout.Space(20);
        }
    }
}
