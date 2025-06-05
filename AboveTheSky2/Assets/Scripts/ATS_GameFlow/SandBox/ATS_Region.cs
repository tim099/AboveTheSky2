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
    /// Runtime region(Sandbox)
    /// </summary>
    public class ATS_Region : ATS_SandBoxBase
    {
        public class RuntimeData : UnityJsonSerializable
        {
            public RegionCells m_Cells = new RegionCells();
            public RegionBuildings m_Buildings = new RegionBuildings();
            public RegionMinions m_Minions = new RegionMinions();
            /// <summary>
            /// Region內所有已儲存的資源
            /// </summary>
            public RegionResources m_Resources = new RegionResources();
            public RegionJobs m_Jobs = new RegionJobs();

            public void Init(ATS_Region iRegion)
            {
                iRegion.AddComponent(m_Cells);
                iRegion.AddComponent(m_Buildings);
                iRegion.AddComponent(m_Minions);
                iRegion.AddComponent(m_Resources);
                iRegion.AddComponent(m_Jobs);
            }
        }
        


        #region Runtime

        private ATS_RegionData m_Region = null;

        private RuntimeData m_RuntimeData = new RuntimeData();
        /// <summary>
        /// 建造模式中的緩存資料(包含建築與要建造的位置等)
        /// </summary>
        private BuildData m_BuildData = new BuildData();

        /// <summary>
        /// 尋路用
        /// </summary>
        private ATS_PathFinder m_PathFinder = new ATS_PathFinder();
        /// <summary>
        /// 尋路用
        /// </summary>
        override public ATS_PathFinder PathFinder => m_PathFinder;

        /// <summary>
        /// 所有的建築格(地塊)
        /// </summary>
        public Cell[,] Cells => m_RuntimeData.m_Cells.m_GridCells;
        /// <summary>
        /// SandBox執行期的所有資訊
        /// </summary>
        public RuntimeData Data => m_RuntimeData;

        #endregion



        #region Getter
        override public ATS_RegionGrid RegionGrid => m_Region.m_GridData;
        public override ATS_Region Region => this;

        public int Width => m_Region.Width;
        public int Height => m_Region.Height;
        #endregion
        public ATS_Region() {
            //Debug.LogError($"1 ATS_Region:{this.GetHashCode()}");
        }
        public ATS_Region(ATS_RegionData iRegionData)
        {
            //Debug.LogError($"2 ATS_Region:{this.GetHashCode()}");
            m_Region = iRegionData;
        }
        override public void Init(ATS_SandBox iSandBox, ATSI_SandBox iParent)
        {
            //Debug.LogError($"ATS_Region Init:{this.GetHashCode()}");
            base.Init(iSandBox, iParent);
            //暫時抓取預設的AirShip(初始飛船)
            //ATS_AirshipDataEntry aAirshipDataEntry = new ATS_AirshipDataEntry();

            AddComponent(m_Region.m_GridData);

            m_RuntimeData.Init(this);

            m_RuntimeData.m_Cells.Init(m_Region.m_GridData);


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
                var aCell = Cells[aCellPos.x, aCellPos.y];
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
            //var aBuildPos = new Vector2Int(iBuilding.m_Pos.x, iBuilding.m_Pos.y);
            var aCellsPos = iBuilding.BuildingCells;
            foreach (var aCellPos in aCellsPos)//將建築設定到地塊上
            {
                var aCell = Cells[aCellPos.x, aCellPos.y];
                aCell.SetBuilding(iBuilding);
                //aCell.m_Building = iBuilding;
            }
            PathFinder.RequireRefreshAllPathState = true;//標記需要刷新所有路徑 會在下次Update時刷新
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

        #region Job
        public void AddJob(ATS_Job iJob, Cell cell)
        {
            Data.m_Jobs.Add(iJob);

            cell.m_Jobs.Add(new ATS_JobRef(iJob));//把工作註冊到Cell 才能被搜尋到
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

            UCL_GUILayout.DrawObjectData(m_RuntimeData, iDic.GetSubDic("RuntimeData"), "RuntimeData");
            GUILayout.Space(20);
        }

        //public override string SaveKey => "Region";
        public override SaveInfo SaveKey => new SaveInfo(SaveType.Folder, "Region");

        //public override JsonData SaveMain()
        //{
        //    return m_RuntimeData.SerializeToJson();
        //}
    }
}
