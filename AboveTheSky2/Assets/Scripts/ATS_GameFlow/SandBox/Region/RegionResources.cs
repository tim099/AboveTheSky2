
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    public class RegionResources : ATS_SandBoxBase
    {
        /// <summary>
        /// 散落在地上的資源(可搬運)
        /// </summary>
        public List<ATS_Resource> m_Resources = new();
        /// <summary>
        /// 所有儲藏在區域內的資源
        /// </summary>
        public Dictionary<ATS_ResourceEntry, int> m_StorageResources = new Dictionary<ATS_ResourceEntry, int>();

        public override SaveInfo SaveKey => new SaveInfo(SaveType.File, "RegionResources");

        public override JsonData SaveMain()
        {
            return SerializeToJson();
        }
        public override void LoadMain(JsonData iJson)
        {
            base.LoadMain(iJson);
            foreach (var aRes in m_Resources)
            {
                AddComponent(aRes);//還原
            }
            Debug.LogError($"LoadMain RegionResources iJson:{iJson.ToJsonBeautify()}");
        }
        public override void LoadGame(ATS_SaveData iSaveData)
        {
            Debug.LogError($"LoadGame RegionResources");
            base.LoadGame(iSaveData);
        }
        //public override string SaveKey => "RegionResources";
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
        /// <summary>
        /// 銷毀資源(當被存放到建築時呼叫)
        /// </summary>
        /// <param name="iResource"></param>
        public void RemoveResource(ATS_Resource iResource)
        {
            m_Resources.Remove(iResource);
            RemoveComponent(iResource);
        }
        /// <summary>
        /// 在指定位置(倉庫)取出資源
        /// </summary>
        /// <param name="resAmount"></param>
        public ATS_Resource TakeResource(ResourceAmount resAmount, float x, float y)
        {
            var resType = resAmount.m_Resource;
            int amount = resAmount.m_Amount;
            //檢查是否有足夠資源 足夠時扣除資源
            if (m_StorageResources.TryGetValue(resType, out int val))
            {
                if (val < amount)//數量不足
                {
                    Debug.LogError($"{GetType().Name}.TakeResource , resource:{resType.ID}, val:{val} < amount:{amount}");
                    return null;
                }
                m_StorageResources[resType] -= amount;//扣除資源
            }
            else//資源不足 取出失敗
            {
                Debug.LogError($"{GetType().Name}.TakeResource ,no resource:{resAmount.m_Resource.ID}!!");
                return null;
            }
            ATS_Resource res = new(resType.ID, amount);//生成資源

            res.m_Pos.x = x;
            res.m_Pos.y = y;
            Region.SpawnResource(res);//將資源物件生成到區域中
            return res;
        }
        /// <summary>
        /// 存入資源
        /// </summary>
        /// <param name="iRes"></param>
        /// <param name="iAmount"></param>
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
            using (var aScope = new GUILayout.VerticalScope())
            {
                GUILayout.BeginHorizontal();
                int aCount = 0;
                foreach (var aKey in m_StorageResources.Keys)
                {
                    if (aCount % 2 == 0)
                    {
                        GUILayout.FlexibleSpace();
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                    }
                    aCount++;

                    var aResData = aKey.GetData();
                    var aResAmount = m_StorageResources[aKey];
                    GUILayout.BeginHorizontal();
                    float aSize = UCL_GUIStyle.GetScaledSize(16);
                    UCL_GUILayout.DrawTexture(aResData.Texture, aSize, aSize);
                    //GUILayout.Box(aResData.Texture, UCL_GUIStyle.BoxStyle);
                    GUILayout.Label($"{aResData.ID} : {aResAmount}", UCL_GUIStyle.LabelStyle, GUILayout.ExpandWidth(false));
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }

        }
    }
}
