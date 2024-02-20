
// ATS_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 02/20 2024 18:49

// RCG_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 09/25 2023
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ATS
{
    /// <summary>
    /// https://docs.unity3d.com/Packages/com.unity.addressables@1.3/manual/MemoryManagement.html
    /// </summary>
    [System.Serializable]
    public class ATS_AddressableData : UCL.Core.JsonLib.UnityJsonSerializable
    {
        public class AddressableLoaded : IDisposable
        {
            public AsyncOperationHandle m_Handle;
            public UnityEngine.Object m_Object;
            public bool m_Loaded = false;
            public void Dispose()
            {
                Addressables.Release(m_Handle);
            }
        }

        virtual protected List<string> GetAddressablePath() => ATS_Addressable.GetAddressablePath();

        [UCL.Core.PA.UCL_List("GetAddressablePath")]
        public string m_AddressablePath = string.Empty;

        virtual public List<string> GetAddressableKeys() => ATS_Addressable.GetAllAddressableKeys(m_AddressablePath);

        [UCL.Core.PA.UCL_List("GetAddressableKeys")]
        public string m_AddressableKey = string.Empty;

        /// <summary>
        /// 是否是空的(不考慮檔案格式不符)
        /// </summary>
        public bool IsEmpty => string.IsNullOrEmpty(m_AddressableKey);
        public string Key => m_AddressableKey;
        public string FileName => UCL.Core.FileLib.Lib.GetFileName(m_AddressableKey);
        private static Dictionary<string, AddressableLoaded> s_LoadedDic = new();
        virtual public async UniTask<UnityEngine.Object> LoadAsync(CancellationToken iToken)
        {
            var aObj = await Addressables.LoadAssetAsync<UnityEngine.Object>(m_AddressableKey);
            iToken.ThrowIfCancellationRequested();
            return aObj;
        }
        
        virtual public async UniTask<T> LoadAsync<T>(CancellationToken iToken) where T : UnityEngine.Object
        {
            if (string.IsNullOrEmpty(m_AddressableKey)) return null;
            bool aIsComponent = typeof(Component).IsAssignableFrom(typeof(T));

            if (s_LoadedDic.ContainsKey(m_AddressableKey))//Need to Fix
            {
                var aData = s_LoadedDic[m_AddressableKey];
                if (!aData.m_Loaded)//Wait Until Loaded
                {
                    await UniTask.WaitUntil(() => aData.m_Loaded, cancellationToken: iToken);
                }
                if(aData.m_Object == null)
                {
                    Debug.LogError($"RCG_PrefabResData.LoadAsync, aData.m_Object == null");
                    
                    s_LoadedDic.Remove(m_AddressableKey);
                    aData.Dispose();
                }
                else if (aIsComponent)
                {
                    return TryGetComponent<T>(aData);
                }
                else if (aData.m_Object is T aTmp)
                {
                    return aTmp;
                }
                else
                {
                    Debug.LogError($"RCG_PrefabResData.LoadAsync, aData.m_Object:{aData.m_Object.GetType().FullName},T:{typeof(T).FullName}");

                    s_LoadedDic.Remove(m_AddressableKey);
                    aData.Dispose();
                }
                
            }

            if (aIsComponent)
            {
                AddressableLoaded aData = await CreateAddressableLoaded<GameObject>(iToken);
                
                return TryGetComponent<T>(aData);
            }

            {
                AddressableLoaded aData = await CreateAddressableLoaded<T>(iToken);
                return aData.m_Object as T;
            }

        }
        private T TryGetComponent<T>(AddressableLoaded iData) where T : UnityEngine.Object
        {
            GameObject aGameObject = iData.m_Object as GameObject;
            if (aGameObject == null)
            {
                Debug.LogError($"RCG_PrefabResData.LoadAsync aGameObject == null,m_AddressableKey:{m_AddressableKey}");
                return null;
            }
            T aComponent = aGameObject.GetComponent<T>();
            if (aComponent == null)
            {
                Debug.LogError($"RCG_PrefabResData.LoadAsync,aGameObject:{aGameObject.name} aComponent == null,m_AddressableKey:{m_AddressableKey}");
                return null;
            }
            return aComponent;
        }
        private async UniTask<AddressableLoaded> CreateAddressableLoaded<T>(CancellationToken iToken) where T : UnityEngine.Object
        {
            var aHandle = Addressables.LoadAssetAsync<T>(m_AddressableKey);
            AsyncOperationHandle aAsyncOperationHandle = aHandle;

            AddressableLoaded aData = new AddressableLoaded();
            aData.m_Handle = aAsyncOperationHandle;

            aData.m_Object = await aHandle;
            s_LoadedDic[m_AddressableKey] = aData;
            aData.m_Loaded = true;
            iToken.ThrowIfCancellationRequested();
            return aData;
        } 
        public static void ReleaseAll()
        {
            foreach(var aKey in s_LoadedDic.Keys)
            {
                var aData = s_LoadedDic[aKey];
                aData.Dispose();
            }
            s_LoadedDic.Clear();
        }
    }
}
