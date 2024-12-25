
// ATS_AutoHeader
// to change the auto header please go to RCG_AutoHeader.cs
// Create time : 02/20 2024 19:13
/* 
AutoHeader Test
to change the auto header please go to RCG_AutoHeader.cs
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace ATS
{
    public static class ATS_TMPTools
    {
        const float BearingY = 0.85f;
        public class SpriteAssetElement
        {
            //public string path;
            public string name;

            public Texture2D outputTexture;

            public Rect rect;
        }
        public static void ClearSpriteAsset(TMP_SpriteAsset iSpriteAsset)
        {
            if(iSpriteAsset == null)
            {
                return;
            }
            if (iSpriteAsset.spriteSheet != null)
            {
                GameObject.DestroyImmediate(iSpriteAsset.spriteSheet);
            }
            if (iSpriteAsset.material != null)
            {
                GameObject.DestroyImmediate(iSpriteAsset.material);
            }
            GameObject.DestroyImmediate(iSpriteAsset);
        }
        static Material s_SpriteAssetMaterial = null;
        public static TMP_SpriteAsset CreateSpriteAsset(List<Texture2D> iTextures,
            List<string> iTextureNames = null,
            List<ATS_IconSprite> iIconSprites = null,
            int iSpriteSize = 64, int iMaxAtlasSize = 4096)
        {
            if (iTextureNames == null)
            {
                iTextureNames = new List<string>();
                for (int i = 0; i < iTextures.Count; i++)
                {
                    iTextureNames.Add(iTextures[i].name);
                }
            }
            //pack the textures into the atlas
            var aTextures = new Texture2D[iTextures.Count];
            for (int i = 0; i < iTextures.Count; i++)
            {
                // aTextures[i] = iTextures[i].CreateResizeTexture(iSpriteSize, iSpriteSize);
                var aTexture = iTextures[i];
                if(aTexture == null)
                {
                    Debug.LogError($"CreateSpriteAsset iTextures[{i}] == null");
                    continue;
                }
                try
                {
                    var aFormat = TextureFormat.RGBA32;//aTexture.format;
                    //switch (aFormat)
                    //{
                    //    case TextureFormat.DXT5:
                    //        {
                    //            aFormat = TextureFormat.RGBA32;
                    //            break;
                    //        }
                    //}
                    //if (aFormat == TextureFormat.DXT5)
                    //{
                    //    aFormat = TextureFormat.RGBA32;
                    //}
                    var aCols = UCL.Core.TextureLib.Lib.GetPixels(aTexture, iSpriteSize, iSpriteSize);
                    var aNewTexture = new Texture2D(iSpriteSize, iSpriteSize, aFormat, true);
                    aNewTexture.SetPixels(aCols);
                    aTextures[i] = aNewTexture;
                }
                catch(System.Exception e)
                {
                    Debug.LogException(e);
                    Debug.LogError($"aTexture:{aTexture.name},aFormat:{aTexture.format},Exception:{e}");
                }

                
            }

            var aSpriteSheet = new Texture2D(1, 1, TextureFormat.ARGB32, true, false);
            aSpriteSheet.filterMode = FilterMode.Bilinear;
            var rects = aSpriteSheet.PackTextures(aTextures, 20, iMaxAtlasSize, false);
            //cleanup textures
            foreach (var texture in aTextures)
            {
                Object.DestroyImmediate(texture);
            }

            float scaleW = (float)aSpriteSheet.width;
            float scaleH = (float)aSpriteSheet.height;

            for (int i = 0; i < rects.Length; i++)
            {
                var rect = rects[i];
                rects[i] = new Rect(rect.x * scaleW, rect.y * scaleH, rect.width * scaleW, rect.height * scaleH);
            }
            aSpriteSheet.Apply(true, false);

            // Create new Sprite Asset
            var aSpriteAsset = ScriptableObject.CreateInstance<TMP_SpriteAsset>();

            // Compute the hash code for the sprite asset.
            aSpriteAsset.hashCode = TMP_TextUtilities.GetSimpleHashCode(aSpriteAsset.name);
            aSpriteAsset.spriteInfoList = new List<TMP_Sprite>();

            // Assign new Sprite Sheet texture to the Sprite Asset.
            aSpriteAsset.spriteSheet = aSpriteSheet;


            if(s_SpriteAssetMaterial == null)
            {
                Shader shader = Shader.Find("TextMeshPro/Sprite");

                s_SpriteAssetMaterial = new Material(shader);
            }
            // Add new default material for sprite asset.

            s_SpriteAssetMaterial.SetTexture(ShaderUtilities.ID_MainTex, aSpriteAsset.spriteSheet);

            aSpriteAsset.material = s_SpriteAssetMaterial;
            s_SpriteAssetMaterial.hideFlags = HideFlags.HideInHierarchy;

            aSpriteAsset.spriteCharacterTable.Clear();
            aSpriteAsset.spriteGlyphTable.Clear();

            List<TMP_SpriteGlyph> spriteGlyphTable = aSpriteAsset.spriteGlyphTable;
            List<TMP_SpriteCharacter> spriteCharacterTable = aSpriteAsset.spriteCharacterTable;
            for (int i = 0; i < iTextures.Count; i++)
            {
                TMP_SpriteGlyph spriteGlyph = new TMP_SpriteGlyph();
                ATS_IconSprite aIconSprite = null;
                if (iIconSprites != null && i < iIconSprites.Count)
                {
                    aIconSprite = iIconSprites[i];
                }
                float aBearingY = BearingY;
                float aBearingX = 0f;
                if (aIconSprite != null)
                {
                    aBearingX = aIconSprite.m_BearingX;
                    aBearingY = aIconSprite.m_BearingY;
                }
                spriteGlyph.index = (uint)i;
                spriteGlyph.metrics = new UnityEngine.TextCore.GlyphMetrics(iSpriteSize, iSpriteSize,
                    aBearingX * iSpriteSize, aBearingY * iSpriteSize, iSpriteSize);
                spriteGlyph.glyphRect = new UnityEngine.TextCore.GlyphRect(rects[i]);//sprite.rect
                spriteGlyph.scale = 1.0f;

                spriteGlyphTable.Add(spriteGlyph);

                TMP_SpriteCharacter spriteCharacter = new TMP_SpriteCharacter(0, spriteGlyph);
                spriteCharacter.name = iTextureNames[i];

                if (aIconSprite != null)
                {
                    spriteCharacter.scale = aIconSprite.m_Scale;
                }
                else
                {
                    spriteCharacter.scale = 1.0f;
                }
                if (spriteCharacter.unicode == 0) spriteCharacter.unicode = 0xFFFE;
                spriteCharacterTable.Add(spriteCharacter);
            }
            //Debug.LogError($"1 spriteCharacterTable:{aSpriteAsset.spriteCharacterTable.Count}");
            aSpriteAsset.UpdateLookupTables();
            //Debug.LogError($"2 spriteCharacterTable:{aSpriteAsset.spriteCharacterTable.Count}");
            return aSpriteAsset;
        }


        //        public static void CreateIconSpriteSheetEditor(string iOuputAtlasPath = "Assets/Resources/Sprites/TMPIconSprites")
        //        {
        //#if UNITY_EDITOR
        //            int MAX_ATLAS_SIZE = 4096;
        //            int SPRITE_SIZE = 64;

        //            var aSpriteAssetElements = new List<SpriteAssetElement>();
        //            foreach (var iIconSpriteID in ATS_IconSprite.Util.GetAllIDs())
        //            {
        //                var aSpriteData = ATS_IconSprite.Util.GetData(iIconSpriteID);

        //                var aSpriteAssetElement = new SpriteAssetElement();
        //                aSpriteAssetElements.Add(aSpriteAssetElement);

        //                //aSpriteAssetElement.path = aSpriteData.m_Icon.Path;
        //                aSpriteAssetElement.name = iIconSpriteID;// System.IO.Path.GetFileNameWithoutExtension(element.path);

        //                //scale the input texture
        //                aSpriteAssetElement.outputTexture = aSpriteData.IconTexture.CreateResizeTexture(SPRITE_SIZE, SPRITE_SIZE);
        //                aSpriteAssetElement.outputTexture.name = aSpriteAssetElement.name;
        //            }

        //            //pack the textures into the atlas
        //            var textures = aSpriteAssetElements.ConvertAll<Texture2D>(e => e.outputTexture).ToArray();

        //            var atlasTexture = new Texture2D(0, 0, TextureFormat.ARGB32, true, false);
        //            atlasTexture.filterMode = FilterMode.Bilinear;
        //            var rects = atlasTexture.PackTextures(textures, 0, MAX_ATLAS_SIZE, false);

        //            float scaleW = (float)atlasTexture.width;
        //            float scaleH = (float)atlasTexture.height;
        //            var spriteMetaDatas = new UnityEditor.SpriteMetaData[textures.Length];
        //            for (int i = 0; i < aSpriteAssetElements.Count; i++)
        //            {
        //                var element = aSpriteAssetElements[i];
        //                var rect = rects[i];
        //                element.rect = rect;

        //                var pixelRect = new Rect(rect.x * scaleW, rect.y * scaleH, rect.width * scaleW, rect.height * scaleH); //metadata needs pixel rects;

        //                //https://docs.unity3d.com/ScriptReference/SpriteMetaData.html
        //                var aMeta = new UnityEditor.SpriteMetaData()
        //                {
        //                    name = element.name,
        //                    rect = pixelRect,
        //                    //element.meta.pivot = new Vector2(0f, 0f); // no need if using alignment ?
        //                    border = new Vector4(0, 0, 0, 0),
        //                    alignment = 6,
        //                };
        //                spriteMetaDatas[i] = aMeta;

        //            }
        //            atlasTexture.Apply(true, false);
        //            System.IO.File.WriteAllBytes(iOuputAtlasPath + ".png", atlasTexture.EncodeToPNG());

        //            //set up the sprite importer settings
        //            UnityEditor.AssetDatabase.Refresh();

        //            UnityEditor.TextureImporter importer = UnityEditor.AssetImporter.GetAtPath(iOuputAtlasPath + ".png")
        //                as UnityEditor.TextureImporter;
        //            importer.textureType = UnityEditor.TextureImporterType.Sprite;
        //            importer.spriteImportMode = UnityEditor.SpriteImportMode.Multiple;
        //            importer.textureCompression = UnityEditor.TextureImporterCompression.Uncompressed;
        //            importer.filterMode = FilterMode.Bilinear;
        //            importer.mipmapEnabled = true;
        //            importer.maxTextureSize = MAX_ATLAS_SIZE;
        //            importer.spritesheet = spriteMetaDatas;

        //            UnityEditor.EditorUtility.SetDirty(importer);
        //            importer.SaveAndReimport();

        //            //cleanup textures
        //            foreach (var element in aSpriteAssetElements)
        //            {
        //                Object.DestroyImmediate(element.outputTexture);
        //            }

        //            //NEXT: add all the sprite metadata to the sprite asset

        //            var spriteSheetTexture = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture>(iOuputAtlasPath + ".png");

        //            // Create new Sprite Asset
        //            TMP_SpriteAsset spriteAsset = ScriptableObject.CreateInstance<TMP_SpriteAsset>();
        //            UnityEditor.AssetDatabase.CreateAsset(spriteAsset, (iOuputAtlasPath + ".asset"));

        //            // Compute the hash code for the sprite asset.
        //            spriteAsset.hashCode = TMP_TextUtilities.GetSimpleHashCode(spriteAsset.name);
        //            spriteAsset.spriteInfoList = new List<TMP_Sprite>();

        //            List<TMP_SpriteGlyph> spriteGlyphTable = new List<TMP_SpriteGlyph>();
        //            List<TMP_SpriteCharacter> spriteCharacterTable = new List<TMP_SpriteCharacter>();

        //            // Assign new Sprite Sheet texture to the Sprite Asset.
        //            spriteAsset.spriteSheet = spriteSheetTexture;

        //            string filePath = UnityEditor.AssetDatabase.GetAssetPath(spriteSheetTexture);

        //            // Get all the Sprites sorted by Index
        //            Sprite[] sprites = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(filePath).
        //                Select(x => x as Sprite).Where(x => x != null).OrderByDescending(x => x.rect.y).ThenBy(x => x.rect.x).ToArray();

        //            for (int i = 0; i < sprites.Length; i++)
        //            {
        //                Sprite sprite = sprites[i];

        //                TMP_SpriteGlyph spriteGlyph = new TMP_SpriteGlyph();
        //                spriteGlyph.index = (uint)i;
        //                spriteGlyph.metrics = new UnityEngine.TextCore.GlyphMetrics(sprite.rect.width, sprite.rect.height,
        //                    -sprite.pivot.x, sprite.rect.height - sprite.pivot.y, sprite.rect.width);
        //                spriteGlyph.glyphRect = new UnityEngine.TextCore.GlyphRect(sprite.rect);
        //                spriteGlyph.scale = 1.0f;
        //                spriteGlyph.sprite = sprite;

        //                spriteGlyphTable.Add(spriteGlyph);

        //                TMP_SpriteCharacter spriteCharacter = new TMP_SpriteCharacter(0, spriteGlyph);
        //                spriteCharacter.name = sprite.name;
        //                spriteCharacter.scale = 1.0f;

        //                spriteCharacterTable.Add(spriteCharacter);
        //            }


        //            // Add new default material for sprite asset.
        //            Shader shader = Shader.Find("TextMeshPro/Sprite");
        //            Material material = new Material(shader);
        //            material.SetTexture(ShaderUtilities.ID_MainTex, spriteAsset.spriteSheet);

        //            spriteAsset.material = material;
        //            material.hideFlags = HideFlags.HideInHierarchy;
        //            UnityEditor.AssetDatabase.AddObjectToAsset(material, spriteAsset);

        //            // Update Lookup tables.
        //            spriteAsset.UpdateLookupTables();

        //            spriteAsset.spriteCharacterTable.Clear();
        //            spriteAsset.spriteGlyphTable.Clear();
        //            spriteAsset.spriteCharacterTable.AddRange(spriteCharacterTable);
        //            spriteAsset.spriteGlyphTable.AddRange(spriteGlyphTable);

        //            for (int i = 0; i < spriteAsset.spriteCharacterTable.Count; i++)
        //            {
        //                TMP_SpriteCharacter spriteCharacter = spriteAsset.spriteCharacterTable[i];
        //                if (spriteCharacter.unicode == 0)
        //                    spriteCharacter.unicode = 0xFFFE;
        //            }

        //            UnityEditor.EditorUtility.SetDirty(spriteAsset);
        //            UnityEditor.AssetDatabase.SaveAssets();
        //            UnityEditor.AssetDatabase.ImportAsset(UnityEditor.AssetDatabase.GetAssetPath(spriteAsset));  // Re-import font asset to get the new updated version.
        //            UnityEditor.AssetDatabase.Refresh(UnityEditor.ImportAssetOptions.ForceUpdate);
        //#endif
        //        }
    }
}

