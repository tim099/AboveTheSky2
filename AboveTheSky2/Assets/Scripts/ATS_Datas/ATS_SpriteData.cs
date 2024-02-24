
// ATS_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 02/20 2024 18:46
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.LocalizeLib;
using UCL.Core.TextureLib;
using UCL.Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace ATS
{
    /// <summary>
    /// Sprite通用資料格式
    /// </summary>
    [System.Serializable]
    [UCL.Core.ATTR.EnableUCLEditor]
    public class ATS_SpriteData : UCL.Core.JsonLib.UnityJsonSerializable, UCL.Core.UCLI_Icon, UCL.Core.UCLI_NameOnGUI
    {
        public string Key
        {
            get
            {
                switch (m_DataLoadType)
                {
                    case DataLoadType.StreamingAssets:
                        {
                            return m_StreamingAssetsData.Key;
                        }
                    case DataLoadType.Addressable:
                        {
                            return m_AddressableData.Key;
                        }
                }
                return string.Empty;
            }
        }
        public class DetailSetting
        {
            /// <summary>
            /// 是否保持長寬比例
            /// </summary>
            public bool m_IsPreserveAspect = false;

            public bool m_UpdateMipMap = true;
            public FilterMode m_FilterMode = FilterMode.Bilinear;

        }
        public const string SpriteFolder = ".Install/Sprites";

        public DataLoadType m_DataLoadType = DataLoadType.StreamingAssets;

        [UCL.Core.ATTR.AlwaysExpendOnGUI]
        [UCL.Core.PA.Conditional("m_DataLoadType", false, DataLoadType.StreamingAssets)]
        public ATS_StreamingAssetsData m_StreamingAssetsData = new ATS_StreamingAssetsData(SpriteFolder);

        [UCL.Core.ATTR.AlwaysExpendOnGUI]
        [UCL.Core.PA.Conditional("m_DataLoadType", false, DataLoadType.Addressable)]
        public UCL_AddressableData m_AddressableData = new UCL_AddressableData();


        public DetailSetting m_DetailSetting = new DetailSetting();

        /// <summary>
        /// 是否保持長寬比例
        /// </summary>
        public bool IsPreserveAspect => m_DetailSetting.m_IsPreserveAspect;
        public float Aspect
        {
            get
            {
                var aTex = Texture;
                if (aTex == null) return 1f;
                return aTex.width / (float)aTex.height;
            }
        }
        public ATS_SpriteData() { }
        public ATS_SpriteData(string iFolderPath)
        {
            Init(DataLoadType.StreamingAssets, iFolderPath, string.Empty);
        }
        public ATS_SpriteData(string iPath, string iName)
        {
            Init(DataLoadType.StreamingAssets, iPath, iName);
        }
        public ATS_SpriteData(DataLoadType iDataLoadType, string iPath, string iName)
        {
            Init(iDataLoadType, iPath, iName);
        }
        public void Init(DataLoadType iDataLoadType, string iPath, string iName)
        {
            //m_FolderPath = iFolderPath;
            //m_SpriteName = iSpriteName;
            m_DataLoadType = iDataLoadType;
            switch (m_DataLoadType)
            {
                case DataLoadType.StreamingAssets:
                    {
                        m_StreamingAssetsData.m_FolderPath = iPath;
                        m_StreamingAssetsData.m_FileName = iName;
                        break;
                    }
                case DataLoadType.Addressable:
                    {
                        m_AddressableData.m_AddressablePath = iPath;
                        m_AddressableData.m_AddressableKey = iName;
                        break;
                    }
            }

        }

        virtual public void NameOnGUI(UCL.Core.UCL_ObjectDictionary iDataDic, string iDisplayName)
        {
            {
                GUILayout.Label(iDisplayName, UCL.Core.UI.UCL_GUIStyle.LabelStyle);
            }
#if UNITY_STANDALONE_WIN
            switch (m_DataLoadType)
            {
                case DataLoadType.Addressable:
                    {
                        break;
                    }
                case DataLoadType.StreamingAssets:
                    {
                        var aPath = UCL.Core.UCL_StreamingAssets.GetFileSystemPath(FolderPath);
                        if (Directory.Exists(aPath))
                        {
                            if (GUILayout.Button(UCL_LocalizeManager.Get("OpenFolder"), UCL_GUIStyle.ButtonStyle, GUILayout.ExpandWidth(false)))
                            {
                                UCL.Core.FileLib.WindowsLib.OpenExplorer(aPath);
                            }
                        }
                        break;
                    }
            }
#endif
        }

        //public override void DeserializeFromJson(JsonData iJson)
        //{
        //    base.DeserializeFromJson(iJson);

        //}
        /// <summary>
        /// 將設定放到目標Image
        /// </summary>
        /// <param name="iImage"></param>
        public void SetImage(Image iImage)
        {
            if (IsEmpty)
            {
                iImage.sprite = null;
                return;
            }
            iImage.sprite = Sprite;
            iImage.preserveAspect = IsPreserveAspect;
        }
        public async UniTask SetImageAsync(Image iImage, CancellationToken iToken)
        {
            if (IsEmpty)
            {
                iImage.sprite = null;
                return;
            }
            var aSprite = await GetSpriteAsync(iToken);
            iImage.sprite = aSprite;
            iImage.preserveAspect = IsPreserveAspect;
        }
        /// <summary>
        /// 圖片預覽功能
        /// </summary>
        [UCL.Core.ATTR.UCL_DrawOnGUI]
        public void PreviewOnGUI(UCL.Core.UCL_ObjectDictionary iDic)
        {
            GUILayout.BeginHorizontal();
            bool aIsPreview = UCL.Core.UI.UCL_GUILayout.Toggle(iDic, "Preview");
            GUILayout.Label(UCL.Core.LocalizeLib.UCL_LocalizeManager.Get("Preview"));
            GUILayout.EndHorizontal();
            if (aIsPreview)
            {
                if (!IsEmpty)
                {
                    var aTex = Texture;
                    if (aTex != null)
                    {
                        float aAspect = aTex.width / (float)aTex.height;
                        GUILayout.Label("height:" + aTex.height + ", width:" + aTex.width + ", Aspect:" + aAspect);

                        //GUILayout.BeginHorizontal();
                        GUILayout.Box(Texture, GUILayout.Height(128), GUILayout.Width(aAspect * 128));
                    }
                    //else
                    //{
                    //    if (!IsLoadingTexture)
                    //    {
                    //        PrewarmTextureAsync().Forget();
                    //    }
                    //}
                }

            }
        }
        public string FolderPath => m_StreamingAssetsData.m_FolderPath;//Application.streamingAssetsPath + "/" + m_FolderPath;


        /// <summary>
        /// 抓取對應Sprite
        /// </summary>
        public Sprite Sprite //=> UCL_Texture == null ? null : UCL_Texture.sprite;
        {
            get
            {
                var aDic = s_SpritesDic.Datas;
                var aKey = Path;
                if (aDic.ContainsKey(aKey))
                {
                    var aSprite = aDic[aKey];
                    if (aSprite == null)
                    {
                        aDic.Remove(aKey);
                    }
                }
                if (!aDic.ContainsKey(aKey))
                {
                    var aTex = Texture;
                    if (aTex == null)
                    {
                        return null;
                    }
                    aDic[aKey] = UCL.Core.TextureLib.Lib.CreateSprite(aTex);
                }

                return aDic[aKey];
            }
        }
        public Texture2D IconTexture => Texture;

        static ATS_ResourceDic<Sprite> s_SpritesDic = new();
        static ATS_ResourceDic<Texture2D> s_TexturesDic = new();
        static ATS_ResourceDic<bool> s_TexturesLoadingDic = new();
        public Texture2D Texture
        {
            get
            {
                if (IsEmpty) return null;

                var aKey = Key;
                var aDic = s_TexturesDic.Datas;
                if (aDic.ContainsKey(aKey))//Remove invalid texture
                {
                    var aTex = aDic[aKey];
                    if (aTex == null)
                    {
                        Debug.LogWarning($"s_TexturesDic[{aKey}] == null, UI.RCG_EditorMenu.IsInEditWindow:{UI.ATS_EditorMenu.IsInEditWindow}");
                        aDic.Remove(aKey);
                    }
                }

                if (!aDic.ContainsKey(aKey))
                {
                    switch (m_DataLoadType)
                    {
                        case DataLoadType.StreamingAssets:
                            {
                                byte[] aBytes = m_StreamingAssetsData.ReadAllBytes();
                                var aTexture = CreateTexture2D(aBytes);
                                aDic[aKey] = aTexture;
                                break;
                            }
                        case DataLoadType.Addressable:
                            {
                                if (!IsLoadingTexture)
                                {
                                    PrewarmTextureAsync().Forget();
                                }
                                break;
                            }
                    }
                }


                if (aDic.ContainsKey(aKey))
                {
                    return aDic[aKey];
                }
                return null;
            }
        }

        private Texture2D CreateTexture2D(byte[] iBytes)
        {
            var aTexture = new Texture2D(1, 1);// LoadImage will default the format and resize the dimensions
            aTexture.LoadImage(iBytes);
            aTexture.filterMode = m_DetailSetting.m_FilterMode;
            if (m_DetailSetting.m_UpdateMipMap)
            {
                aTexture.Apply(true);
            }
            //aTexture.Apply(m_DetailSetting.m_UpdateMipMap);
            return aTexture;
        }
        public virtual string Path
        {
            get
            {
                switch (m_DataLoadType)
                {
                    case DataLoadType.Addressable:
                        {
                            return m_AddressableData.Key;
                        }
                }
                return m_StreamingAssetsData.Path;
            }
        }

        /// <summary>
        /// 是否未設定圖片
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                switch(m_DataLoadType)
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
        public bool IsLoadingTexture => s_TexturesLoadingDic.Datas.ContainsKey(Path);
        public async UniTask<Sprite> GetSpriteAsync(CancellationToken iToken = default)
        {
            if (IsEmpty) return null;

            var aDic = s_SpritesDic.Datas;
            var aKey = Path;
            if (aDic.ContainsKey(aKey))
            {
                var aSprite = aDic[aKey];
                if (aSprite == null)
                {
                    aDic.Remove(aKey);
                }
            }

            if (!aDic.ContainsKey(aKey))
            {
                var aTex = await GetTextureAsync(iToken);
                iToken.ThrowIfCancellationRequested();
                if (aTex == null)
                {
                    Debug.LogError($"RCG_SpriteData.GetSpriteAsync aTex == null!!aKey:{aKey}");
                    return null;
                }

                if (!aDic.ContainsKey(aKey))
                {
                    aDic[aKey] = UCL.Core.TextureLib.Lib.CreateSprite(aTex);
                }
            }
            var aResult = aDic[aKey];
            if (aResult == null)
            {
                Debug.LogError($"RCG_SpriteData.GetSpriteAsync aSprite == null!!aKey:{aKey}");
            }
            return aResult;
        }
        public async UniTask<Texture2D> GetTextureAsync(CancellationToken iToken = default)
        {
            if (IsEmpty) return null;
            var aKey = Path;
            var aDic = s_TexturesDic.Datas;
            if (aDic.ContainsKey(aKey))//Remove Invalid Texture
            {
                var aTex = aDic[aKey];
                if (aTex == null)
                {
                    Debug.LogWarning("s_Textures[aKey].Texture == null aKey:" + aKey);
                    aDic.Remove(aKey);
                }
            }

            if (!aDic.ContainsKey(aKey))
            {
                await PrewarmTextureAsync(iToken);
            }

            return aDic[aKey];
        }

        public async UniTask PrewarmTextureAsync(CancellationToken iToken = default)
        {
            if (IsEmpty) return;
            var aKey = Path;
            var aDic = s_TexturesDic.Datas;
            var aSpritesDic = s_SpritesDic.Datas;
            if (aDic.ContainsKey(aKey))
            {
                var aTex = aDic[aKey];
                if (aTex == null)//Remove Invalid Texture
                {
                    Debug.LogWarning($"s_Textures[{aKey}].Texture == null");
                    aDic.Remove(aKey);
                }
                else//Texture Loaded 
                {
                    return;
                }
            }

            var aLoadingDic = s_TexturesLoadingDic.Datas;
            if (aLoadingDic.ContainsKey(aKey))//Loading
            {
                await UniTask.WaitUntil(() => !aLoadingDic.ContainsKey(aKey), cancellationToken: iToken);
                return;
            }

            {//Load Texture
                aLoadingDic.Add(aKey, true);
                try
                {
                    switch (m_DataLoadType)
                    {
                        case DataLoadType.Addressable:
                            {
                                //Debug.LogError($"RCG_SpriteData.GetTextureAsync m_LoadType:{m_LoadType},aKey:{aKey}");
                                var aObject = await m_AddressableData.LoadAsync(iToken);
                                if (aObject is Sprite aSprite)
                                {
                                    aSpritesDic[aKey] = aSprite;
                                    aDic[aKey] = aSprite.texture;
                                    //Debug.LogError($"RCG_SpriteData.GetTextureAsync aSprite:{aSprite.name}");
                                }
                                if (aObject is Texture2D aTexture)
                                {
                                    aDic[aKey] = aTexture;
                                }
                                else
                                {
                                    Debug.LogError($"RCG_SpriteData.GetTextureAsync aKey:{aKey}, aObject.GetType().FullName:{aObject.GetType().FullName}");
                                }
                                break;
                            }
                        case DataLoadType.StreamingAssets:
                            {
                                byte[] aBytes = null;
                                aBytes = await UCL.Core.UCL_StreamingAssets.ReadAllBytesAsync(aKey, iToken);
                                iToken.ThrowIfCancellationRequested();
                                if (!aDic.ContainsKey(aKey) && aBytes != null)
                                {
                                    var aTexture = CreateTexture2D(aBytes);
                                    aDic[aKey] = aTexture;
                                }
                                break;
                            }
                    }
                }
                catch(System.Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    aLoadingDic.Remove(aKey);
                }
            }
        }
    }
}

