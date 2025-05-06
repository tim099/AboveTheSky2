
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs

// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System;
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
    /// 建造模式中的緩存資料(包含建築與要建造的位置等)
    /// </summary>
    public class BuildData : UCL.Core.JsonLib.UnityJsonSerializable
    {
        /// <summary>
        /// 建造模式中 當前要建造的建築ID
        /// </summary>
        public string BuildingID
        {
            get => m_BuildingID;
            set
            {
                if (m_BuildingID == value) return;

                m_BuildingID = value;
                if (!string.IsNullOrEmpty(m_BuildingID))
                {
                    m_BuildingData = ATS_BuildingData.Util.GetData(m_BuildingID);
                }
                else
                {
                    m_BuildingData = null;
                }
            }
        }
        private string m_BuildingID;


        public ATS_BuildingData m_BuildingData = null;
        public Vector2Int m_Pos = Vector2Int.zero;
        public int m_PrevClickCount = 0;


        public IList<Vector2Int> BuildingCells
        {
            get
            {
                if(m_BuildingData == null)
                {
                    return Array.Empty<Vector2Int>();
                }
                return m_BuildingData.GetBuildingCells(m_Pos);
            }
        }
    }

    public enum BuildingState
    {
        /// <summary>
        /// 藍圖
        /// </summary>
        Blueprint,
        /// <summary>
        /// 建設完成
        /// </summary>
        Constructed,
        /// <summary>
        /// 被摧毀
        /// </summary>
        Destroyed,
        /// <summary>
        /// 建築中
        /// </summary>
        Constructing,
    }
    public enum ConstructingState
    {
        /// <summary>
        /// 尚未開始建造
        /// </summary>
        None = 0,
        /// <summary>
        /// 準備搬運建造資源
        /// </summary>
        Haul,
        /// <summary>
        /// 搬運建造資源中
        /// </summary>
        Hauling,
        /// <summary>
        /// 建造建築(需要建築工)
        /// </summary>
        Build,
        /// <summary>
        /// 建造完成
        /// </summary>
        Done,
    }
    public class ATS_BuildingRef : ATS_SandBoxRef<ATS_Building>
    {
        public override string GetDisplayName(string iFieldName)
        {
            var data = Value;
            if(data != null)
            {
                return $"[{Index}]{iFieldName}({data.GetShortName()})";
            }
            return $"[{Index}]{iFieldName}";
        }
    }
    /// <summary>
    /// Sandbox中使用的建築
    /// </summary>
    public class ATS_Building : ATS_SandBoxBase
    {
        /// <summary>
        /// 建築類型
        /// </summary>
        public ATS_BuildingDataEntry m_BuildingDataEntry = new ATS_BuildingDataEntry();

        /// <summary>
        /// 建築位置
        /// </summary>
        public ATS_Vector2Int m_Pos = new ATS_Vector2Int();
        /// <summary>
        /// 當前狀態(藍圖 建設完成 被摧毀狀態 建築中)
        /// </summary>
        public BuildingState m_BuildingState = BuildingState.Constructed;
        /// <summary>
        /// 建造階段
        /// </summary>
        public ConstructingState m_ConstructingState = ConstructingState.None;
        //{
        //    get
        //    {
        //        return _ConstructingState;
        //    }
        //    set
        //    {
        //        //if (_ConstructingState != value)
        //        {
        //            Debug.LogError($"({Index}){GetShortName()}({m_BuildingState}), _ConstructingState:{_ConstructingState}, value:{value}");
        //        }
                
        //        _ConstructingState = value;
        //    }
        //}
        //public ConstructingState _ConstructingState;
        /// <summary>
        /// 避免過度頻繁的判斷部分邏輯(例如搬運工作)
        /// </summary>
        public int m_LogicTimer = 0;

        /// <summary>
        /// 目前的工作隊列
        /// </summary>
        public List<ATS_WorkRef> m_Works = new();
        /// <summary>
        /// 建築內的工人(生產or建造)
        /// </summary>
        public List<ATS_MinionRef> m_Workers = new();
        /// <summary>
        /// 所有儲藏在區域內的資源
        /// </summary>
        public Dictionary<ATS_ResourceEntry, int> m_StorageResources = new Dictionary<ATS_ResourceEntry, int>();
        #region Getter

        public ATS_BuildingData BuildingData => m_BuildingDataEntry.GetData();

        public IList<Vector2Int> BuildingCells => BuildingData.GetBuildingCells(m_Pos.ToVector2Int);
        /// <summary>
        /// 是否為可用的倉庫(可取出資源)
        /// </summary>
        public bool IsStorage => m_BuildingState == BuildingState.Constructed && BuildingData.IsStorage;
        override public string GetShortName() => $"{m_BuildingDataEntry.ID} {m_Pos}[{m_BuildingState}]";
        #endregion

        public ATS_Building() { }
        public ATS_Building(string iID, Vector2Int iPos, BuildingState iBuildingState = BuildingState.Blueprint)
        {
            m_BuildingDataEntry.ID = iID;
            m_Pos.Set(iPos);
            //m_X = iPos.x;
            //m_Y = iPos.y;
            m_BuildingState = iBuildingState;
        }
        /// <summary>
        /// 獲取對應路徑狀態
        /// </summary>
        /// <param name="x">地塊在Region中的x</param>
        /// <param name="y">地塊在Region中的y</param>
        /// <returns></returns>
        public int GetPathState(int x, int y)
        {
            return BuildingData.GetPathState(x - m_Pos.x, y - m_Pos.y);//傳入相對位置
        }


        /// <summary>
        /// 根據ATS_RegionGrid繪製在GUI上
        /// </summary>
        /// <param name="iGrid"></param>
        public void DrawOnGrid(ATS_RegionGrid iGrid)
        {
            Color? aGUIColor = null;
            switch (m_BuildingState)
            {
                case BuildingState.Constructing:
                case BuildingState.Blueprint:
                    {
                        aGUIColor = UCL_Color.Half.White;
                        break;
                    }
            }
            if (aGUIColor.HasValue) UCL_GUIStyle.PushGUIColor(aGUIColor.Value);

            BuildingData.DrawOnGrid(iGrid, m_Pos.x, m_Pos.y);

            if (aGUIColor.HasValue) UCL_GUIStyle.PopGUIColor();

            if (m_BuildingState == BuildingState.Constructing)//Show progress
            {
                
                const float Width = 0.8f;
                const float Height = 0.2f;
                float x = m_Pos.x + 0.5f * (BuildingData.m_GridData.m_Width) - 0.5f * Width;// + 0.5f;
                float y = m_Pos.y + 0.5f * (BuildingData.m_GridData.m_Height);// + 0.5f;
                

                float progress = 0f;// 0.4f;
                if (!m_Works.IsNullOrEmpty())
                {
                    progress = Mathf.Clamp01(m_Works.FirstOrDefault().Value.Progress);
                }
                var rect = iGrid.GetCellRect(x, y, Width, Height);
                UCL_GUILayout.ProgressBar(rect, progress);

                //const float offSet = 0.02f;
                //var rect = iGrid.GetCellRect(x, y, Width, Height);
                //using (new UCL_GUIStyle.UCL_GUIColorScope(Color.black))
                //{
                //    GUI.DrawTexture(rect, UCL_StaticTextures.White);
                //}
                //rect = iGrid.GetCellRect(x + offSet, y + offSet, progress * (Width - 2f * offSet), Height - 2f * offSet);
                //using (new UCL_GUIStyle.UCL_GUIColorScope(Color.green))
                //{
                //    GUI.DrawTexture(rect, UCL_StaticTextures.White);
                //}

            }

        }

        public override void Init(ATS_SandBox iSandBox, ATSI_SandBox iParent)
        {
            base.Init(iSandBox, iParent);
        }
        /// <summary>
        /// 用在ATS_SandboxPage
        /// </summary>
        override public void ContentOnGUI(UCL_ObjectDictionary iDic)
        {
            ATS_RegionGrid aGrid = RegionGrid;
            DrawOnGrid(aGrid);
        }
        /// <summary>
        /// 把資源放入建築
        /// (根據情況判斷進入倉庫或是存放在建築中)
        /// </summary>
        /// <param name="resource"></param>
        public void AddToStorage(ATS_Resource resource)
        {
            if(IsStorage)//建設完成&是倉庫 進入倉庫
            {
                resource.AddToStorage();
            }
            else//未建設完成 or 非倉庫 存放到建築中作為材料
            {
                var res = resource.m_ResourceAmount;
                StorageResource(res.m_Resource, res.m_Amount);//搬運到建築內(非進入倉庫)
                Region.Data.m_Resources.RemoveResource(resource);//銷毀資源實體
            }

        }
        /// <summary>
        /// 把資源存入這個建築中(用來建造或生產)
        /// </summary>
        /// <param name="iRes"></param>
        /// <param name="iAmount"></param>
        public void StorageResource(ATS_ResourceEntry iRes, int iAmount)
        {
            if (!m_StorageResources.ContainsKey(iRes))
            {
                m_StorageResources.Add(iRes, 0);
            }
            m_StorageResources[iRes] += iAmount;
        }
        /// <summary>
        /// 進入建築
        /// </summary>
        /// <param name="worker"></param>
        public void EnterBuilding(ATS_Minion worker)
        {
            m_Workers.Add(new ATS_MinionRef(worker));
            worker.SetState(MinionState.WorkingInBuilding);
        }

        const int LogicUpdateInterval = 10;
        const int ResourceNotFindInterval = 30;
        public override void GameUpdate()
        {
            base.GameUpdate();
            try
            {
                m_Works.UpdateWork();
            }
            catch (Exception e)
            {
                Debug.LogError($"{GetShortName()},ex:{e}");
            }
            

            if (m_LogicTimer > 0)
            {
                --m_LogicTimer;
            }
            else
            {
                m_LogicTimer = LogicUpdateInterval;

                switch (m_BuildingState)
                {
                    case BuildingState.Blueprint://需要等待建造完成
                        {
                            m_BuildingState = BuildingState.Constructing;//切換到建築中的狀態
                            var work = new ATS_WorkConstruct();
                            work.Init(this);
                            Region.Data.m_Jobs.Add(work);//註冊工作
                            m_Works.Add(new ATS_WorkRef(work));//記錄到當前工作隊列

                            m_ConstructingState = ConstructingState.None;
                            break;
                        }
                    case BuildingState.Constructing:
                        {
                            //等待ATS_WorkConstruct完成
                            break;
                        }
                    case BuildingState.Constructed://建造完成的建築
                        {
                            if (IsStorage)//倉庫 判斷附近是否有能搬運的資源
                            {
                                bool SearchResource(Cell iCell, PathNode iPathNode)
                                {
                                    if (!iCell.m_Resources.IsNullOrEmpty())
                                    {
                                        return true;
                                    }
                                    return false;
                                }
                                var aResult = PathFinder.Search(m_Pos.x, m_Pos.y, SearchResource);

                                if (!aResult.IsNullOrEmpty())//有可搬運的資源
                                {
                                    var aCell = aResult[0].cell;
                                    aCell.GenerateHaulJob(this);//生成搬運的工作
                                }
                                else
                                {
                                    m_LogicTimer = ResourceNotFindInterval;//目前沒找到可搬運的資源 延長下次搜尋的間格
                                }
                            }
                            break;
                        }
                }

            }
            
        }

        public override JsonData SerializeToJson()
        {
            //if (!m_Works.IsNullOrEmpty())
            //{
            //    Debug.LogError($"DeserializeFromJson m_Works:{m_Works.AllFieldToString()}");
            //}
            return base.SerializeToJson();
        }
        public override void DeserializeFromJson(JsonData iJson)
        {
            base.DeserializeFromJson(iJson);
            //if (!m_Works.IsNullOrEmpty())
            //{
            //    Debug.LogError($"DeserializeFromJson m_Works:{m_Works.AllFieldToString()}");
            //    Debug.LogError($"iJson:{iJson.ToJsonBeautify()}");
            //}
            //m_Pos.m_X = m_X;
            //m_Pos.m_Y = m_Y;
        }

        //public override void LoadComponents(ATS_SaveData iSaveData)
        //{
        //    base.LoadComponents(iSaveData);
        //    if (!m_Works.IsNullOrEmpty())
        //    {
        //        Debug.LogError($"LoadGame m_Works:{m_Works.AllFieldToString()}");
        //    }
        //}

        //public override void LoadGame(ATS_SaveData iSaveData)
        //{
        //    base.LoadGame(iSaveData);
        //    if (!m_Works.IsNullOrEmpty())
        //    {
        //        Debug.LogError($"LoadGame m_Works:{m_Works.AllFieldToString()}");
        //    }
        //}
        //public override void LoadMain(JsonData iJson)
        //{
        //    base.LoadMain(iJson);
        //    if (!m_Works.IsNullOrEmpty())
        //    {
        //        Debug.LogError($"LoadGame m_Works:{m_Works.AllFieldToString()}");
        //    }
        //}
    }
}
