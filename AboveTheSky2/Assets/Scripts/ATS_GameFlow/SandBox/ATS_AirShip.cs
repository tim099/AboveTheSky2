
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
    public class ATS_AirShip : ATS_SandBoxBase
    {
        public class RuntimeData : UnityJsonSerializable
        {
            //public List<ATS_Building> m_Buildings = new List<ATS_Building>();
        }
        #region Runtime
        private int m_GameUpdateCount = 0;
        private ATS_AirshipData m_AirshipData = null;

        /// <summary>
        /// Region不Serialize 因為會覆蓋掉Init時new出來的
        /// </summary>
        private ATS_Region m_Region;
        /// <summary>
        /// 入口位置(船員進入或離開的位置)
        /// </summary>
        [SerializeField] private ATS_Vector2Int m_EntrancePos = new();
        [SerializeField] private ATS_AirshipDataEntry m_AirshipDataEntry = new ATS_AirshipDataEntry();
        #endregion



        #region Getter
        override public ATS_RegionGrid RegionGrid => m_Region.RegionGrid;
        public override ATS_PathFinder PathFinder => m_Region.PathFinder;
        #endregion
        public ATS_AirShip()
        {
            //Debug.LogError("new ATS_AirShip");
        }
        public ATS_AirShip(ATS_AirshipDataEntry airshipDataEntry)
        {
            m_AirshipDataEntry.ID = airshipDataEntry.ID;
            //Debug.LogError("new ATS_AirShip");
        }
        override public void Init(ATS_SandBox iSandBox, ATSI_SandBox iParent)
        {
            base.Init(iSandBox, iParent);
            //暫時抓取預設的AirShip(初始飛船)

            m_AirshipData = m_AirshipDataEntry.GetData(false);
            var region = m_AirshipData.m_Region.GetData(false);
            SetRegion(new ATS_Region(region));
            //Debug.LogError("Init Airship");
        }
        public override void GameInit()
        {
            base.GameInit();



            foreach (var aBuilding in m_AirshipData.m_Buildings)//建造預設建築
            {
                try
                {
                    var aNewBuilding = aBuilding.CloneObject();
                    if (aNewBuilding.BuildingData.CheckBuildingType(BuildingType.Entrance))
                    {
                        m_EntrancePos.Set(aNewBuilding.m_Pos);
                        //Debug.LogError($"EntrancePos:{m_EntrancePos}");
                    }
                    m_Region.Build(aNewBuilding);//建造並設定為已建造完成
                }
                catch (System.Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }
        public void SetRegion(ATS_Region region)
        {
            m_Region = region;
            AddComponent(m_Region);
        }
        public override void LoadGame(ATS_SaveData iSaveData)
        {
            base.LoadGame(iSaveData);
            //m_Region = new();
            //AddComponent(m_Region);

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


        public override SaveInfo SaveKey => new SaveInfo(SaveType.Folder, "AirShip");

        //public override Dictionary<string, ISandBox> SaveComponentsDic
        //{
        //    get
        //    {
        //        Dictionary<string, ISandBox> saveComponentsDic = new Dictionary<string, ISandBox>();
        //        saveComponentsDic["Region"] = m_Region;
        //        return saveComponentsDic;
        //    }
        //}

        /// <summary>
        /// 隨機生成一個單位(船員或其他生物)
        /// </summary>
        public void Spawn()
        {
            var aIDs = ATS_CreatureData.Util.GetAllIDs();
            var aID = UCL_Random.Instance.RandomPick(aIDs);
            var aMinion = new ATS_Minion(aID, m_EntrancePos.x + 0.5f, m_EntrancePos.y + UCL_Random.Instance.Range(0, ATS_Const.GroundHeight));
            //aMinion.m_Position = new Vector3(m_EntrancePos.x, m_EntrancePos.y, 0);
            if(m_Region == null)
            {
                Debug.LogError("m_Region == null");
                return;
            }
            m_Region.Spawn(aMinion);
        }
        public void SpawnResource()
        {
            int aTargetDistance = UCL_Random.Instance.Range(1, 8);//目標最遠距離
            //return true if find target
            int CheckNode(Cell iCell, PathNode iNode)
            {
                if (iNode.m_Distance >= aTargetDistance)
                {
                    return 0;
                }
                int val = aTargetDistance - iNode.m_Distance;
                //Debug.LogError($"val:{val},iNode:{iNode.m_Pos},iNode.m_Distance:{iNode.m_Distance}");
                return val - Random.Range(0, aTargetDistance);//+ 
            }
            var aPath = PathFinder.SearchPath(m_EntrancePos.x, m_EntrancePos.y, CheckNode);
            //Debug.LogError($"m_Path:{aPath.m_Path.ConcatString(iPos => $"{iPos.x},{iPos.y}")}");


            ATS_Resource aRes = new ATS_Resource(m_SpawnResType.m_ID, UCL_Random.Instance.Range(1, 99));
            if (aPath != null && !aPath.m_Path.IsNullOrEmpty())
            {
                var aPos = aPath.m_Path.LastElement();

                aRes.m_Pos.x = aPos.x + UCL_Random.Instance.Range(-0.4f, 0.4f);
                aRes.m_Pos.y = aPos.y + ATS_Const.GroundHeight + UCL_Random.Instance.Range(0.1f, 0.5f);
            }

            m_Region.SpawnResource(aRes);
        }
        private ATS_ResourceEntry m_SpawnResType = new ATS_ResourceEntry();
        /// <summary>
        /// 用在ATS_SandboxPage
        /// </summary>
        override public void ContentOnGUI(UCL_ObjectDictionary iDic)
        {


            base.ContentOnGUI(iDic);
            GUILayout.Label($"ATS_AirShip m_GameUpdateCount:{m_GameUpdateCount}", UCL_GUIStyle.LabelStyle);

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

        public override void DeserializeFromJson(JsonData iJson)
        {
            //m_AirshipData = new();
            //SetRegion(new());

            base.DeserializeFromJson(iJson);
            //Debug.LogError($"DeserializeFromJson m_EntrancePos:{m_EntrancePos}");
        }
    }
}
