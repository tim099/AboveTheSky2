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
        public ATS_Vector2Int m_Pos = new ATS_Vector2Int();
        /// <summary>
        /// 此格子上的建築(一個建築可以占用多個格子)
        /// </summary>
        public ATS_Building m_Building = null;
        /// <summary>
        /// 對應建築中的哪個格子(一個建築可以占用多個格子)
        /// </summary>
        public Vector2Int m_BuildingCellPos;

        public ATS_TileData m_TileData = null;
        public int m_PathState = 0;
        /// <summary>
        /// 此地塊上的所有工作
        /// </summary>
        public List<ATS_Job> m_Jobs = new();
        /// <summary>
        /// 掉落在此地的資源
        /// </summary>
        public List<ATS_Resource> m_Resources = new();// { get; private set; } = new ();
        public string GetShortName() => $"Cell {m_Pos}";
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
                if(m_Building == null)
                {
                    return 0;
                }
                return m_Building.BuildingData.GetPathState(m_BuildingCellPos.x, m_BuildingCellPos.y);
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
        public void SetBuilding(ATS_Building iBuilding, Vector2Int iBuildingCellPos)
        {
            m_Building = iBuilding;
            m_BuildingCellPos = iBuildingCellPos;
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
            foreach(var aRes in m_Resources.ToList())
            {
                Debug.LogError($"Cell.GenerateHaulJob, aRes:{aRes}");
                //aRes.SetState(ATS_Resource.ResourceState.PrepareToHaul);
                JobHauling aJobHauling = new JobHauling();
                aJobHauling.Init(iBuilding, aRes);
                m_Jobs.Add(aJobHauling);
            }
            
        }
    }
    /// <summary>
    /// Runtime region(Sandbox)
    /// </summary>
    public class ATS_Region : SandBoxBase
    {
        public class RuntimeData : UnityJsonSerializable
        {
            public RegionCells m_Cells = new RegionCells();
            public RegionBuildings m_Buildings = new RegionBuildings();
            public RegionMinions m_Minions = new RegionMinions();
            public RegionResources m_Resources = new RegionResources();
        }
        


        #region Runtime

        private ATS_RegionData m_Region = null;

        private RuntimeData m_RuntimeData = new RuntimeData();
        /// <summary>
        /// 建造模式中的緩存資料(包含建築與要建造的位置等)
        /// </summary>
        private BuildData m_BuildData = new BuildData();
        private ATS_PathFinder m_PathFinder = new ATS_PathFinder();
        /// <summary>
        /// 尋路用
        /// </summary>
        override public ATS_PathFinder PathFinder => m_PathFinder;
        /// <summary>
        /// 所有的建築格(地塊)
        /// </summary>
        private Cell[,] m_Cells = null;
        /// <summary>
        /// 所有的建築格(地塊)
        /// </summary>
        public Cell[,] Cells => m_Cells;
        #endregion



        #region Getter
        override public ATS_RegionGrid RegionGrid => m_Region.m_GridData;
        public override ATS_Region Region => this;

        public int Width => m_Region.Width;
        public int Height => m_Region.Height;
        #endregion
        public ATS_Region() { }
        public ATS_Region(ATS_RegionData iRegionData)
        {
            m_Region = iRegionData;
        }
        override public void Init(ATS_SandBox iSandBox, ISandBox iParent)
        {
            base.Init(iSandBox, iParent);
            //暫時抓取預設的AirShip(初始飛船)
            //ATS_AirshipDataEntry aAirshipDataEntry = new ATS_AirshipDataEntry();

            AddComponent(m_Region.m_GridData);

            AddComponent(m_RuntimeData.m_Cells);
            AddComponent(m_RuntimeData.m_Buildings);
            AddComponent(m_RuntimeData.m_Minions);
            AddComponent(m_RuntimeData.m_Resources);

            m_Cells = m_Region.m_GridData.CreateCells();
            foreach (Cell cell in m_Cells)
            {
                m_RuntimeData.m_Cells.m_Cells.Add(cell);
            }

            AddComponent(m_PathFinder);
        }
        #region Build
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
            foreach (var aCellPos in aCellsPos)
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
            m_RuntimeData.m_Buildings.Build(iBuilding);
            var aBuildPos = new Vector2Int(iBuilding.m_Pos.x, iBuilding.m_Pos.y);
            var aCellsPos = iBuilding.BuildingCells;
            foreach (var aCellPos in aCellsPos)//將建築設定到地塊上
            {
                var aCell = m_Cells[aCellPos.x, aCellPos.y];
                aCell.SetBuilding(iBuilding, aCellPos - aBuildPos);
                //aCell.m_Building = iBuilding;
            }
            PathFinder.RequireRefreshAllPathState = true;//標記需要刷新所有路徑 會在下次Update時刷新

            //PathFinder.RefreshAllPathState();//刷新所有路徑
            //foreach (var aCellPos in aCellsPos)
            //{
            //    //Debug.LogError($"Build RefreshPathState aCellPos:{aCellPos}");
            //    PathFinder.RefreshPathState(aCellPos.x, aCellPos.y);//刷新建築占用地塊的路徑
            //}
        }
        #endregion

        #region Spawn
        /// <summary>
        /// 生成一個單位(船員或其他生物)
        /// </summary>
        /// <param name="iMinion"></param>
        public void Spawn(ATS_Minion iMinion)
        {
            m_RuntimeData.m_Minions.Spawn(iMinion);
        }
        /// <summary>
        /// 生成一個資源物件(實體)
        /// </summary>
        /// <param name="iResource"></param>
        public void SpawnResource(ATS_Resource iResource)
        {
            m_RuntimeData.m_Resources.Add(iResource);
        }
        #endregion

        /// <summary>
        /// Logic base update
        /// </summary>
        override public void GameUpdate()
        {
            base.GameUpdate();
        }

        /// <summary>
        /// 用在ATS_SandboxPage
        /// </summary>
        override public void ContentOnGUI(UCL_ObjectDictionary iDic)
        {
            switch (CurGameState)
            {
                case GameState.Build:
                    {
                        var aIDs = ATS_BuildingData.Util.GetAllIDs();

                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Building ID", UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));
                        m_BuildData.BuildingID = UCL_GUILayout.PopupAuto(m_BuildData.BuildingID, aIDs, iDic, "BuildingID");
                        GUILayout.EndHorizontal();
                        break;
                    }
            }
            using (var aScope = new GUILayout.HorizontalScope())
            {
                GUILayout.Space(UCL_GUIStyle.GetScaledSize(32));
                using (var aScope2 = new GUILayout.VerticalScope())
                {
                    base.ContentOnGUI(iDic.GetSubDic("BaseContentOnGUI"));//繪製區域&建築
                }
            }

            var aGrid = RegionGrid;//p_SandBox.GetAirShipRegionGrid();
            aGrid.DrawMouseFrame();
            switch (p_SandBox.CurGameState)
            {
                case GameState.Build:
                    {
                        var aBuildingData = m_BuildData.m_BuildingData;
                        //Debug.LogError($"MousePos:{m_Region.m_GridData.MousePos},Event.current.type:{Event.current.type}");
                        //bool aClicked = false;
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
                                    if (aX < 0 || aX >= aWidth || aY < 0 || aY >= aHeight)
                                    {
                                        return;
                                    }
                                    var aRect = m_Region.m_GridData.GetCellRect(aX, aY, 1, 1);
                                    if (GUI.Button(aRect, name))
                                    {
                                        //Debug.LogError($"Move Pos:{m_BuildData.m_Pos},dir:{dir}");
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
                                    string aName = aCanBuild ? "✔️" : "X";
                                    //CheckCanBuild
                                    if (GUI.Button(aRect, aName, UCL_GUIStyle.GetButtonStyle(aCanBuild ? Color.green : Color.red)))
                                    {
                                        if (aCanBuild)
                                        {
                                            
                                            var aBuildingState = aBuildingData.m_RequireConstruct? BuildingState.Blueprint : BuildingState.Constructed;
                                            var aBuilding = new ATS_Building(aBuildingData.ID, aPosition, aBuildingState);

                                            Debug.Log($"Build:{aBuildingData.ID},aPosition:{aPosition},aBuildingState:{aBuildingState}");
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
                        //aGrid.DrawMouseFrame();

                        var aCurrentEvent = Event.current;

                        //Debug.LogError($"m_BuildData.m_PrevClickCount:{m_BuildData.m_PrevClickCount},aCurrentEvent.clickCount:{aCurrentEvent.clickCount}");
                        //https://forum.unity.com/threads/check-if-any-button-is-pressed.271010/
                        if (aCurrentEvent.type == EventType.MouseDown)//m_BuildData.m_PrevClickCount > 0 &&  點擊後
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
    public class RegionCells : SandBoxBase
    {
        public List<Cell> m_Cells = new List<Cell>();
    }
    public class RegionBuildings : SandBoxBase
    {
        public List<ATS_Building> m_Buildings = new List<ATS_Building>();

        public void Build(ATS_Building iBuilding)
        {
            m_Buildings.Add(iBuilding);
            AddComponent(iBuilding);//要在AddComponent後 ATS_Building才會Init
        }
    }
    public class RegionMinions : SandBoxBase
    {
        public List<ATS_Minion> m_Minions = new ();

        public void Spawn(ATS_Minion iMinion)
        {
            m_Minions.Add(iMinion);
            AddComponent(iMinion);//要在AddComponent後 ATS_Minion才會Init
        }
    }
    public class RegionResources : SandBoxBase
    {
        /// <summary>
        /// 散落在地上的資源(可搬運)
        /// </summary>
        public List<ATS_Resource> m_Resources = new ();
        /// <summary>
        /// 所有儲藏在區域內的資源
        /// </summary>
        public Dictionary<ATS_ResourceEntry, int> m_StorageResources = new Dictionary<ATS_ResourceEntry, int>();
        public void Add(ATS_Resource iResource)
        {
            m_Resources.Add(iResource);
            AddComponent(iResource);
        }
        /// <summary>
        /// 把散落在地上的資源放入倉庫
        /// </summary>
        /// <param name="iResource"></param>
        public void AddToStorage(ATS_Resource iResource)
        {
            m_Resources.Remove(iResource);
            RemoveComponent(iResource);
            AddToStorage(iResource.m_ResourceAmount.m_Resource, iResource.m_ResourceAmount.m_Amount);
        }
        public void AddToStorage(ATS_ResourceEntry iRes, int iAmount)
        {
            if (!m_StorageResources.ContainsKey(iRes))
            {
                m_StorageResources.Add(iRes, 0);
            }
            m_StorageResources[iRes] += iAmount;
        }
        public override void ContentOnGUI(UCL_ObjectDictionary iDic)
        {
            base.ContentOnGUI(iDic);
        }
    }
}
