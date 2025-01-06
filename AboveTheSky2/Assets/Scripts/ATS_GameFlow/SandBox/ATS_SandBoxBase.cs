
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.UI;
using UnityEngine;


namespace ATS
{
    public interface ATSI_SandBox : ATSI_Index
    {
        void Init(ATS_SandBox iSandBox, ATSI_SandBox iParent);
        /// <summary>
        /// Logic base update
        /// </summary>
        void GameUpdate();

        void ContentOnGUI(UCL_ObjectDictionary iDic);

        ATS_Region Region { get; }

        /// <summary>
        /// 當前所在地區
        /// </summary>
        ATS_RegionGrid RegionGrid { get; }

        ATS_PathFinder PathFinder { get; }

        #region Save & Load
        void SaveGame(ATS_SaveData iSaveData);
        void LoadGame(ATS_SaveData iJson);

        (SaveType, string) SaveKey { get; }
        Dictionary<string, ATSI_SandBox> SaveComponentsDic { get; }
        string TypeName { get; }
        #endregion
    }
    public class ATS_SandBoxBase : UCL.Core.JsonLib.UnityJsonSerializable, ATSI_SandBox, UCLI_FieldOnGUI, UCLI_ShortName
    {
        public const string MainFileKey = "Main";

        public ATS_SandBox p_SandBox { get; private set; } = null;
        public ATSI_SandBox Parent { get; private set; } = null;

        virtual public ATS_RegionGrid RegionGrid
        {
            get
            {
                if (Parent != null)
                {
                    return Parent.RegionGrid;
                }
                return null;//p_SandBox.GetAirShipRegionGrid();
            }
        }
        virtual public ATS_Region Region
        {
            get
            {
                if (Parent == null)
                {
                    return null;
                }
                return Parent.Region;
            }
        }
        virtual public ATS_PathFinder PathFinder
        {
            get
            {
                if (Parent == null)
                {
                    return null;
                }
                return Parent.PathFinder;
            }
        }
        virtual public string TypeName => GetType().Name;
        virtual public string GetShortName() => $"{GetType().Name}";
        virtual public GameState CurGameState => p_SandBox.CurGameState;

        virtual public int Index { get => m_Index; set => m_Index = value; }


        [SerializeField] private int m_Index = -1;

        /// <summary>
        /// 需要存檔的Component
        /// </summary>
        virtual public Dictionary<string, ATSI_SandBox> SaveComponentsDic
        {
            get
            {
                if (m_Components.IsNullOrEmpty())
                {
                    return null;
                }
                Dictionary<string, ATSI_SandBox> dic = null;
                foreach (var component in m_Components)
                {
                    var saveKey = component.SaveKey;
                    switch (saveKey.Item1)
                    {
                        case SaveType.Json:
                        case SaveType.File:
                        case SaveType.Folder:
                            {
                                var key = saveKey.Item2;
                                if (!string.IsNullOrEmpty(key))
                                {
                                    if (dic == null)
                                    {
                                        dic = new Dictionary<string, ATSI_SandBox>();
                                    }
                                    dic[key] = component;
                                }
                                break;
                            }
                    }

                }
                return dic;
            }
        }
        virtual public (SaveType, string) SaveKey => (SaveType.None, string.Empty);

        protected List<ATSI_SandBox> m_Components = new List<ATSI_SandBox>();//[UCL.Core.PA.UCL_FieldOnGUI]
        protected bool m_Inited = false;

        #region Getter
        //public ATS_AirShip AirShip => p_SandBox.m_AirShip;

        #endregion

        virtual public void Init(ATS_SandBox iSandBox, ATSI_SandBox iParent)
        {
            if (m_Inited)
            {
                return;
            }
            m_Inited = true;
            p_SandBox = iSandBox;
            Parent = iParent;

            p_SandBox.AddSandBoxItem(this);
            //foreach (var aComponent in m_Components)
            //{
            //    aComponent.Init(iSandBox);
            //}
        }
        virtual public void AddComponent(ATSI_SandBox iComponent)
        {
            try
            {
                m_Components.Add(iComponent);
                iComponent.Init(p_SandBox, this);
            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
                Debug.LogError($"SandBoxBase.AddComponent({GetType().FullName}), Exception:{ex}");
                //throw new System.Exception($"SandBoxBase.AddComponent({GetType().FullName}), Exception:{ex}", ex);
            }
        }
        virtual public void RemoveComponent(ATSI_SandBox iComponent)
        {
            try
            {
                m_Components.Remove(iComponent);
                p_SandBox.RemoveSandBoxItem(iComponent);
            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
                Debug.LogError($"SandBoxBase.RemoveComponent({GetType().FullName}), Exception:{ex}");
                //throw new System.Exception($"SandBoxBase.AddComponent({GetType().FullName}), Exception:{ex}", ex);
            }
        }
        /// <summary>
        /// Logic base update
        /// </summary>
        virtual public void GameUpdate()
        {
            foreach (var aComponent in m_Components)
            {
                try
                {
                    aComponent.GameUpdate();
                }
                catch (System.Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }

        /// <summary>
        /// 用在ATS_SandboxPage
        /// </summary>
        virtual public void ContentOnGUI(UCL_ObjectDictionary iDic)
        {
            int aIndex = 0;
            foreach (var aComponent in m_Components)
            {
                aComponent.ContentOnGUI(iDic.GetSubDic("Component", aIndex++));
            }
        }
        virtual public object OnGUI(string iFieldName, UCL_ObjectDictionary iDataDic, UCL_GUILayout.DrawObjectParams iParams)
        {
            var aDrawObjExSetting = new UCL_GUILayout.DrawObjExSetting();
            void ShowComponents()
            {
                UCL_GUILayout.DrawObjectData(m_Components, iDataDic.GetSubDic("Components"), "Components");
            }
            aDrawObjExSetting.OnShowField = ShowComponents;
            UCL_GUILayout.DrawField(this, iDataDic.GetSubDic("Data"), iFieldName, iDrawObjExSetting: aDrawObjExSetting);

            return this;
        }
        public override JsonData SerializeToJson()
        {
            JsonData aJson = base.SerializeToJson();
            //Save Component
            return aJson;
        }
        public override void DeserializeFromJson(JsonData iJson)
        {
            base.DeserializeFromJson(iJson);
        }
        #region Save & Load
        const string ComponentsKey = "Components";

        virtual public JsonData SaveMain()
        {
            return SerializeToJson();
            //return null;
        }
        virtual public void LoadMain(JsonData iJson)
        {
            DeserializeFromJson(iJson);
        }
        virtual public void SaveComponents(ATS_SaveData iSaveData)
        {
            var aSaveComponentsDic = SaveComponentsDic;
            //if(aSaveComponentsDic != null)
            //{
            //    Debug.LogError($"{GetType().FullName}.SaveComponents, aSaveComponentsDic:{aSaveComponentsDic.Keys.ConcatString()}");
            //}
            //else
            //{
            //    Debug.LogError($"{GetType().FullName}.SaveComponents, aSaveComponentsDic == null");
            //}

            if (aSaveComponentsDic.IsNullOrEmpty())
            {
                return;
            }
            ATS_SaveData aComponents = iSaveData;
            //SaveData aComponents = new SaveData();
            //saveData.AddFolder(ComponentsKey, aComponents);
            foreach (var key in aSaveComponentsDic.Keys)
            {
                var aComponent = aSaveComponentsDic[key];
                aComponent.SaveGame(aComponents);
                //aComponents.AddFolder(key, aComponent.SaveGame(aComponents));
            }
        }
        virtual public void LoadComponents(ATS_SaveData iSaveData)
        {
            var aSaveComponentsDic = SaveComponentsDic;
            if (aSaveComponentsDic.IsNullOrEmpty())
            {
                return;
            }
            //var aComponents = saveData.LoadFolder(ComponentsKey);

            foreach (var key in aSaveComponentsDic.Keys)
            {
                var aComponent = aSaveComponentsDic[key];
                if (aComponent == null)
                {
                    Debug.LogError($"LoadComponents key:{key},saveData.m_Dir:{iSaveData.m_Dir} aComponent == null");
                    continue;
                }
                try
                {
                    aComponent.LoadGame(iSaveData);
                }
                catch(System.Exception ex)
                {
                    Debug.LogError($"LoadComponents key:{key},saveData.m_Dir:{iSaveData.m_Dir}, Exception:{ex}");
                    Debug.LogException(ex);
                }

            }
        }
        virtual public void SaveGame(ATS_SaveData iSaveData)
        {
            var aSaveKey = SaveKey;
            var aSaveType = aSaveKey.Item1;
            var aKey = aSaveKey.Item2;
            Debug.LogError($"SaveGame ${GetType().FullName}, aSaveType:{aSaveType}");
            //return null;
            switch (aSaveType)
            {
                case SaveType.Folder:
                    {
                        ATS_SaveData aSaveData = new ATS_SaveData(aKey);
                        aSaveData.AddFile(MainFileKey, SaveMain());
                        SaveComponents(aSaveData);
                        iSaveData.AddFolder(aKey, aSaveData);
                        break;
                    }
                case SaveType.File:
                    {
                        iSaveData.AddFile(aKey, SaveMain());
                        break;
                    }
                case SaveType.Json:
                    {
                        break;
                    }
            }

        }
        virtual public void LoadGame(ATS_SaveData iSaveData)
        {

            var aSaveKey = SaveKey;
            var aSaveType = aSaveKey.Item1;
            var aKey = aSaveKey.Item2;
            //Debug.LogError($"LoadGame aKey:{aKey} path:{iSaveData.m_Dir}");
            switch (aSaveType)
            {
                case SaveType.Folder:
                    {
                        var aSaveData = iSaveData.LoadFolder(aKey);
                        if (aSaveData == null)
                        {
                            Debug.LogError($"LoadGame aKey:{aKey}, path:{iSaveData.m_Dir}, aSaveData == null");
                            break;
                        }
                        var aMain = aSaveData.LoadFile(MainFileKey, false);
                        LoadMain(aMain);
                        LoadComponents(aSaveData);
                        break;
                    }
                case SaveType.File:
                    {
                        var aMain = iSaveData.LoadFile(aKey);
                        if (aMain == null)
                        {
                            Debug.LogError($"LoadGame[{GetType().FullName}] aKey:{aKey}, path:{iSaveData.m_Dir}, aMain == null");
                        }
                        LoadMain(aMain);
                        break;
                    }
                case SaveType.Json:
                    {
                        break;
                    }
            }
        }
        #endregion
    }


    public interface ATSI_Index
    {
        int Index { get; set; }
    }
    public class ATS_Indexer : UCL.Core.JsonLib.UnityJsonSerializable
    {
        //https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers/
        // Define the indexer to allow client code to use [] notation.
        public ATSI_Index this[int i]
        {
            get { return m_Items[i]; }//get { return GetItem(i); }
            private set { m_Items[i] = value; }
        }

        [UCL.Core.ATTR.UCL_HideInJson]
        public Dictionary<int, ATSI_Index> m_Items = new();

        /// <summary>
        /// 目前最大Index已經分配到多少
        /// </summary>
        public int m_CurIndex = 0;
        /// <summary>
        /// 刪除Item時會把空出來的Index存起來 下次分配時會優先使用
        /// </summary>
        //public List<int> m_DeletedIndexs = new List<int>();
        public ATS_Indexer()
        {

        }
        public override JsonData SerializeToJson()
        {
            return base.SerializeToJson();
        }
        public void AddItem(ATSI_Index iItem)
        {
            if(iItem.Index < 0)//Need to Init
            {
                //if (!m_DeletedIndexs.IsNullOrEmpty())
                //{
                //    iItem.Index = m_DeletedIndexs[0];
                //    m_DeletedIndexs.RemoveAt(0);
                //}
                //else
                {
                    while (m_Items.ContainsKey(m_CurIndex))//避免重複Index
                    {
                        if(m_CurIndex < int.MaxValue - 1)
                        {
                            ++m_CurIndex;
                        }
                        else//循環使用
                        {
                            m_CurIndex = 0;
                        }
                    }
                    iItem.Index = m_CurIndex++;
                }
            }
            m_Items[iItem.Index] = iItem;
        }
        public void RemoveItem(ATSI_Index iItem)
        {
            int aIndex = iItem.Index;
            //m_DeletedIndexs.Add(aIndex);
            m_Items.Remove(aIndex);
        }
        public ATSI_Index GetItem(int iIndex)
        {
            return m_Items[iIndex];
        }
    }
}
