
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.MathLib;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{



    /// <summary>
    /// Runtime airship
    /// </summary>
    public class ATS_AirShip : SandBoxBase
    {
        public class RuntimeData : UnityJsonSerializable
        {
            //public List<ATS_Building> m_Buildings = new List<ATS_Building>();
        }
        #region Runtime
        private int m_GameUpdateCount = 0;
        private ATS_AirshipData m_AirshipData = null;

        private ATS_Region m_Region;
        /// <summary>
        /// 入口位置(船員進入或離開的位置)
        /// </summary>
        private Vector2Int m_EntrancePos;
        #endregion



        #region Getter
        override public ATS_RegionGrid RegionGrid => m_Region.RegionGrid;
        public override ATS_PathFinder PathFinder => m_Region.PathFinder;
        #endregion

        override public void Init(ATS_SandBox iSandBox, ISandBox iParent)
        {
            base.Init(iSandBox, iParent);
            //暫時抓取預設的AirShip(初始飛船)
            ATS_AirshipDataEntry aAirshipDataEntry = new ATS_AirshipDataEntry();
            m_AirshipData = aAirshipDataEntry.GetData(false);
            var aRegion = m_AirshipData.m_Region.GetData(false);
            m_Region = new ATS_Region(aRegion);
            AddComponent(m_Region);          


            foreach(var aBuilding in m_AirshipData.m_Buildings)//建造預設建築
            {
                try
                {
                    var aNewBuilding = aBuilding.CloneObject();
                    if (aNewBuilding.BuildingData.CheckBuildingType(BuildingType.Entrance))
                    {
                        m_EntrancePos = aNewBuilding.m_Pos.ToVector2Int;
                        //Debug.LogError($"EntrancePos:{m_EntrancePos}");
                    }
                    m_Region.Build(aNewBuilding);//建造並設定為已建造完成
                }
                catch(System.Exception ex)
                {
                    Debug.LogException(ex);
                }
            }

            //throw new System.Exception("Test");
        }

        /// <summary>
        /// Logic base update
        /// </summary>
        override public void GameUpdate()
        {
            
            ++m_GameUpdateCount;
            //m_Region.GameUpdate();
            base.GameUpdate();
        }

        private ATS_ResourceEntry m_SpawnResType = new ATS_ResourceEntry();
        /// <summary>
        /// 用在ATS_SandboxPage
        /// </summary>
        override public void ContentOnGUI(UCL_ObjectDictionary iDic)
        {
            UCL_GUILayout.DrawObjectData(m_SpawnResType, iDic.GetSubDic("m_SpawnResType"));
            using (var aScope = new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Spawn", UCL_GUIStyle.ButtonStyle))
                {
                    var aIDs = ATS_CreatureData.Util.GetAllIDs();
                    var aID = UCL_Random.Instance.RandomPick(aIDs);
                    var aMinion = new ATS_Minion(aID, m_EntrancePos.x + 0.5f, m_EntrancePos.y + UCL_Random.Instance.Range(0, ATS_Const.GroundHeight));
                    //aMinion.m_Position = new Vector3(m_EntrancePos.x, m_EntrancePos.y, 0);
                    m_Region.Spawn(aMinion);
                }

                
                if (GUILayout.Button("Spawn Resource", UCL_GUIStyle.ButtonStyle))
                {
                    int aTargetDistance = UCL_Random.Instance.Range(1, 6);
                    //return true if find target
                    int CheckNode(Cell iCell, PathNode iNode)
                    {
                        if (iNode.m_Distance >= aTargetDistance)
                        {
                            return 0;
                        }
                        return aTargetDistance - iNode.m_Distance;
                    }
                    var aPath = PathFinder.SearchPath(m_EntrancePos.x, m_EntrancePos.y, CheckNode);
                    //Debug.LogError($"({m_Pos.x},{m_Pos.y}), m_Path:{m_Path.m_Path.ConcatString(iPos => $"{iPos.m_Pos.x},{iPos.m_Pos.y}")}");


                    ATS_Resource aRes = new ATS_Resource(m_SpawnResType.m_ID, UCL_Random.Instance.Range(1, 99));
                    if(aPath != null && !aPath.m_Path.IsNullOrEmpty())
                    {
                        var aPos = aPath.m_Path.LastElement();

                        aRes.m_Pos.x = aPos.x + UCL_Random.Instance.Range(0.01f,0.99f);
                        aRes.m_Pos.y = aPos.y + ATS_Const.GroundHeight + UCL_Random.Instance.Range(0.1f, 0.5f);
                    }

                    m_Region.SpawnResource(aRes);
                }
                switch (CurGameState)
                {
                    case GameState.Build:
                        {
                            if (GUILayout.Button("Cancel", UCL_GUIStyle.ButtonStyle))
                            {
                                p_SandBox.SetGameState(GameState.GameLoop);
                            }
                            break;
                        }
                    case GameState.GameLoop:
                        {
                            if (GUILayout.Button("Build", UCL_GUIStyle.ButtonStyle))
                            {
                                p_SandBox.SetGameState(GameState.Build);
                            }
                            break;
                        }
                }

            }

            base.ContentOnGUI(iDic);
            GUILayout.Label($"ATS_AirShip m_GameUpdateCount:{m_GameUpdateCount}", UCL_GUIStyle.LabelStyle);
            //switch (p_SandBox.CurGameState)
            //{
            //    case GameState.Build:
            //        {
            //            var aIDs = ATS_BuildingData.Util.GetAllIDs();

            //            GUILayout.BeginHorizontal();
            //            GUILayout.Label("Building ID", UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));
            //            m_BuildData.BuildingID = UCL_GUILayout.PopupAuto(m_BuildData.BuildingID, aIDs, iDic, "BuildingID");
            //            GUILayout.EndHorizontal();
            //            //if(m_BuildData.m_BuildingData != null)
            //            //{
            //            //    var aTexture = m_BuildData.m_BuildingData.Texture;
            //            //    if(aTexture != null)
            //            //    {
            //            //        float aSize = UCL_GUIStyle.GetScaledSize(64);
            //            //        GUILayout.Box(aTexture,GUILayout.Width(aSize),GUILayout.Height(aSize));
            //            //    }
            //            //}
                        
            //            break;
            //        }
            //}
            ////using (var aScope = new GUILayout.HorizontalScope())
            ////{
            ////    GUILayout.Space(UCL_GUIStyle.GetScaledSize(32));
            ////    using (var aScope2 = new GUILayout.VerticalScope())
            ////    {
            ////        m_Region.ContentOnGUI(iDic.GetSubDic("Region"));
            ////    }  
            ////}
            //using (var aScope = new GUILayout.HorizontalScope())
            //{
            //    GUILayout.Space(UCL_GUIStyle.GetScaledSize(32));
            //    using (var aScope2 = new GUILayout.VerticalScope())
            //    {
            //        base.ContentOnGUI(iDic.GetSubDic("BaseContentOnGUI"));//繪製區域&建築
            //    }
            //}
            
            //var aGrid = p_SandBox.GetAirShipRegionGrid();

            //switch (p_SandBox.CurGameState)
            //{
            //    case GameState.Build:
            //        {
            //            var aBuildingData = m_BuildData.m_BuildingData;
            //            //Debug.LogError($"MousePos:{m_Region.m_GridData.MousePos},Event.current.type:{Event.current.type}");
            //            //bool aClicked = false;
            //            if (aBuildingData != null)
            //            {
            //                var aPosition = m_BuildData.m_Pos;
            //                aBuildingData.DrawOnGrid(aGrid, aPosition.x, aPosition.y);

            //                {
            //                    int aWidth = Width;
            //                    int aHeight = Height;
            //                    void DrawMoveButton(int x, int y, Vector2Int dir, string name)
            //                    {
            //                        int aX = aPosition.x + x;
            //                        int aY = aPosition.y + y;
            //                        if(aX < 0 || aX >= aWidth || aY < 0 || aY >= aHeight)
            //                        {
            //                            return;
            //                        }
            //                        var aRect = m_Region.m_GridData.GetCellRect(aX, aY, 1, 1);
            //                        if (GUI.Button(aRect, name))
            //                        {
            //                            //Debug.LogError($"Move Pos:{m_BuildData.m_Pos},dir:{dir}");
            //                            m_BuildData.m_Pos += dir;
            //                        }
            //                    }

            //                    void DrawBuild(int x, int y)
            //                    {
            //                        int aX = aPosition.x + x;
            //                        int aY = aPosition.y + y;
            //                        if (aX < 0 || aX >= aWidth || aY < 0 || aY >= aHeight)//Out of range
            //                        {
            //                            return;
            //                        }
            //                        var aRect = m_Region.m_GridData.GetCellRect(aX, aY, 1, 1);
            //                        bool aCanBuild = CheckCanBuild(m_BuildData);
            //                        string aName = aCanBuild? "✔️":"X";
            //                        //CheckCanBuild
            //                        if (GUI.Button(aRect, aName, UCL_GUIStyle.GetButtonStyle(aCanBuild? Color.green : Color.red)))//
            //                        {
            //                            if (aCanBuild)
            //                            {
            //                                Debug.Log($"Build:{aBuildingData.ID},aPosition:{aPosition}");
            //                                var aBuilding = new ATS_Building(aBuildingData.ID, aPosition);
            //                                Build(aBuilding);
            //                            }
            //                        }
            //                    }
            //                    UCL_GUIStyle.PushGUIColor(new Color(1, 1, 1, 0.5f));
            //                    DrawMoveButton(-1, 0, Vector2Int.left, "A");
            //                    DrawMoveButton(1, 0, Vector2Int.right, "D");
            //                    DrawMoveButton(0, 1, Vector2Int.up, "W");
            //                    DrawMoveButton(0, -1, Vector2Int.down, "S");
                                
            //                    //UCL_GUIStyle.RestoreGUIColor();

            //                    //UCL_GUIStyle.SetGUIColor(new Color(1, 1, 1, 0.4f));
            //                    DrawBuild(0, 0);
            //                    UCL_GUIStyle.PopGUIColor();

            //                }
            //            }
            //            aGrid.DrawMouseFrame();

            //            var aCurrentEvent = Event.current;

            //            //Debug.LogError($"m_BuildData.m_PrevClickCount:{m_BuildData.m_PrevClickCount},aCurrentEvent.clickCount:{aCurrentEvent.clickCount}");
            //            //https://forum.unity.com/threads/check-if-any-button-is-pressed.271010/
            //            if (aCurrentEvent.type == EventType.MouseDown)//m_BuildData.m_PrevClickCount > 0 &&  點擊後
            //            {
            //                var aPos = m_Region.m_GridData.MousePos;
            //                //Debug.LogError($"Click!! aPos:{aPos}");
            //                if (aPos != ATS_RegionGrid.NullPos)
            //                {
            //                    m_BuildData.m_Pos = aPos;
            //                }
            //            }

            //            m_BuildData.m_PrevClickCount = aCurrentEvent.clickCount;
            //            break;
            //        }
            //}
            
            //UCL_GUILayout.DrawObjectData(m_RuntimeData.m_Buildings, iDic.GetSubDic("m_Buildings"), "Buildings");
            //GUILayout.Space(20);
        }
    }
}
