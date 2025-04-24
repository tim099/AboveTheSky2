
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections.Generic;
using UnityEngine;

namespace ATS
{
    /// <summary>
    /// 建造建築的工作
    /// </summary>
    public class ATS_WorkConstruct : ATS_Work
    {
        public enum ConstructingState
        {
            /// <summary>
            /// 尚未開始建造
            /// </summary>
            None = 0,
            /// <summary>
            /// 準備搬運建造資源
            /// </summary>
            WaitForResource,
            /// <summary>
            /// 搬運建造資源中
            /// </summary>
            Hauling,
            /// <summary>
            /// 建造建築(需要建築工 尋找工人)
            /// </summary>
            Build,
            /// <summary>
            /// 建造完成
            /// </summary>
            Done,
        }

        /// <summary>
        /// 建造階段
        /// </summary>
        public ConstructingState m_ConstructingState = ConstructingState.None;
        /// <summary>
        /// 建築目標
        /// </summary>
        public ATS_BuildingRef m_Target = new();
        /// <summary>
        /// 當前階段的工作
        /// </summary>
        public List<ATS_JobRef> m_Jobs = new();
        public ATS_BuildingData BuildingData => m_Target.Value.m_BuildingDataEntry.GetData();
        public override void Start()
        {
            base.Start();
            var cost = BuildingData.m_ConstructCost;
            if (cost.m_Consume.IsNullOrEmpty())//不需要資源則跳過搬運資源階段
            {
                m_ConstructingState = ConstructingState.Build;
            }
            else//搬運所需資源
            {
                m_ConstructingState = ConstructingState.WaitForResource;
            }
        }
        public override void Update()
        {
            //Debug.LogError($"ATS_WorkConstruct.Update m_ConstructingState:{m_ConstructingState}");
            var building = m_Target.Value;
            switch (m_ConstructingState)
            {
                case ConstructingState.WaitForResource://搬運所需資源
                    {
                        //只在所有資源滿足時開始搬運
                        ATS_Recipe cost = BuildingData.m_ConstructCost;
                        if (cost.CheckResourceEnough(Region.Data.m_Resources.m_StorageResources))//先確認是否滿足建造資源需求
                        {
                            //Debug.LogError($"cost.CheckResourceEnough");
                            bool SearchStorage(Cell iCell, PathNode iPathNode)
                            {
                                return iCell.IsStorage;
                            }
                            //先確定有到達倉庫的路徑
                            var result = Region.PathFinder.Search(building.m_Pos.x, building.m_Pos.y, SearchStorage);
                            if (!result.IsNullOrEmpty())//有到達倉庫的路徑
                            {
                                var cell = result[0].cell;
                                var storage = cell.m_Building.Value;//倉庫建築
                                                                    //從倉庫取出資源
                                //Debug.LogError($"{GetShortName()}, storage:{storage.BuildingData.ID},Pos:{cell.m_Pos}");

                                //生成搬運資源的Job
                                foreach (var consume in cost.m_Consume)
                                {
                                    var res = Region.Data.m_Resources.TakeResource(consume, cell.m_Pos.x + 0.5f, cell.m_Pos.y);
                                    JobHauling aJobHauling = new JobHauling();
                                    aJobHauling.Init(building, res);//搬運到這個建築
                                    Region.AddJob(aJobHauling, cell);//註冊Job
                                    m_Jobs.Add(new ATS_JobRef(aJobHauling));//記錄所有搬運工作 或是動態判斷當前庫存資源是否滿足建造
                                }

                                m_ConstructingState = ConstructingState.Hauling;
                            }
                        }
                        //TODO 生成搬運資源的工作
                        //TODO 開始建造

                        break;
                    }
                case ConstructingState.Hauling:
                    {
                        for(int i = m_Jobs.Count - 1; i >= 0; i--)
                        {
                            var job = m_Jobs[i].Value;
                            if(job == null || job.Complete || job.Cancel)//TODO 處理Cancel的情況
                            {
                                m_Jobs.RemoveAt(i);
                            }
                        }

                        if (m_Jobs.IsNullOrEmpty())//搬運完成準備進行建造
                        {
                            m_ConstructingState = ConstructingState.Build;
                        }
                        break;
                    }
                case ConstructingState.Build:
                    {
                        //尋找工人
                        break;
                    }
            }
        }
    }
}
