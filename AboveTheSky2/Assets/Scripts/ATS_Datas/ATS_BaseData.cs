
// ATS_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 02/20 2024 18:49

// RCG_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 10/17 2023
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UnityEngine;

namespace ATS
{
    public enum DataLoadType
    {
        StreamingAssets = 0,
        Addressable,
    }
    public class ATS_BaseData : UCL.Core.JsonLib.UnityJsonSerializable, UCLI_ShortName
    {
        public DataLoadType m_LoadType = DataLoadType.StreamingAssets;
        [UCL.Core.ATTR.AlwaysExpendOnGUI]
        [UCL.Core.PA.Conditional("m_LoadType", false, DataLoadType.StreamingAssets)]
        public ATS_StreamingAssetsData m_StreamingAssetsData = new ATS_StreamingAssetsData();

        [UCL.Core.ATTR.AlwaysExpendOnGUI]
        [UCL.Core.PA.Conditional("m_LoadType", false, DataLoadType.Addressable)]
        public UCL_AddressableData m_AddressableData = new UCL_AddressableData();

        virtual public string GetShortName()
        {
            return Path;
        }
        virtual public void Init(string iFolderPath, string iFileName = "")
        {
            m_StreamingAssetsData.m_FolderPath = iFolderPath;
            m_StreamingAssetsData.m_FileName = iFileName;
        }
        virtual public string Key
        {
            get
            {
                switch (m_LoadType)
                {
                    case DataLoadType.Addressable:
                        {
                            return m_AddressableData.Key;
                        }
                    case DataLoadType.StreamingAssets:
                        {
                            return m_StreamingAssetsData.Key;
                        }
                }
                return $"RCG_AudioData.m_LoadType:{m_LoadType}, Key Undefined!!";
            }
        }
        virtual public string Path 
        {
            get
            {
                switch (m_LoadType)
                {
                    case DataLoadType.Addressable:
                        {
                            return m_AddressableData.Key;
                        }
                    case DataLoadType.StreamingAssets:
                        {
                            return m_StreamingAssetsData.Path;
                        }
                }
                return $"RCG_AudioData.m_LoadType:{m_LoadType}, Path Undefined!!";
            }
        }
        virtual public bool IsEmpty
        {
            get
            {
                switch (m_LoadType)
                {
                    case DataLoadType.Addressable:
                        {
                            return m_AddressableData.IsEmpty;
                        }
                    case DataLoadType.StreamingAssets:
                        {
                            return m_StreamingAssetsData.IsEmpty;
                        }
                }
                return true;
            }
        }
    }
}
