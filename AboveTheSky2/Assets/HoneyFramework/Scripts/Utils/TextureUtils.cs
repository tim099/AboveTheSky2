#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEditor;

namespace HoneyFramework
{

    /*
     * Helper class for texture conversion.
     */
    public class TextureUtils
    {

        /// <summary>
        /// This helper function is designed for application of certain settings to texture importer. It will return original settings in case someone want to reapply them later
        /// </summary>
        /// <param name="texture"> texture which settings get modified </param>
        /// <param name="readable"> sets flag to texture allowing it to be red. </param>
        /// <param name="alphaIsTransparency"> should texture use alpha as transparency? Note that shaders may use alpha as they wish anyway, but this setting helps to display texture better within editor </param>
        /// <param name="filterMode"> shoudl texture reading occur in single pixel(Point), flat interpolation of up to 4 pixels(Bilinear) or 8 pixels when mip maps are avaliable(Trilinear)</param>
        /// <param name="format"> texture mode (texture compression status), Note that not all types are supported on all platforms </param>
        /// <param name="type"> unity texture type, helps to identify texture functionality </param>
        /// <param name="apply"> If false, change only specific properties. Exactly which, depends on type </param>
        /// <param name="maxTextureSize"> limit texture to this size (scale proportionally) </param>
        /// <returns> previous texture settings</returns>
        static public TextureImporterSettings ChangeTextureSettings(Texture texture,
                                                                    bool readable,
                                                                    bool alphaIsTransparency,
                                                                    FilterMode filterMode,
                                                                    TextureImporterFormat format,
                                                                    TextureImporterType type,
                                                                    bool apply,
                                                                    int maxTextureSize)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            if (path.Length > 0)
            {
                return ChangeTextureSettings(path, readable, alphaIsTransparency, filterMode, format, type, apply, maxTextureSize);
            }

            Debug.LogWarning("Texture is most likely no longer valid or part of the assets!");

            return null;
        }

        /// <summary>
        /// This helper function is designed for application of certain settings to texture importer. It will return original settings in case someone want to reapply them later
        /// </summary>
        /// <param name="path"> asset path to texture which settings get modified </param>
        /// <param name="readable"> sets flag to texture allowing it to be red. </param>
        /// <param name="alphaIsTransparency"> should texture use alpha as transparency? Note that shaders may use alpha as they wish anyway, but this setting helps to display texture better within editor </param>
        /// <param name="filterMode"> shoudl texture reading occur in single pixel(Point), flat interpolation of up to 4 pixels(Bilinear) or 8 pixels when mip maps are avaliable(Trilinear)</param>
        /// <param name="format"> texture mode (texture compression status), Note that not all types are supported on all platforms </param>
        /// <param name="type"> unity texture type, helps to identify texture functionality </param>
        /// <param name="apply"> If false, change only specific properties. Exactly which, depends on type </param>
        /// <param name="maxTextureSize"> limit texture to this size (scale proportionally) </param>
        /// <returns> previous texture settings</returns>
        static public TextureImporterSettings ChangeTextureSettings(string path,
                                                                    bool readable,
                                                                    bool alphaIsTransparency,
                                                                    FilterMode filterMode,
                                                                    TextureImporterFormat format,
                                                                    TextureImporterType type,
                                                                    bool apply,
                                                                    int maxTextureSize)
        {            
            if (File.Exists(path))
            {
                TextureImporter textureImporter = (TextureImporter)AssetImporter.GetAtPath(path);

                AssetDatabase.StartAssetEditing();
                TextureImporterSettings setting = new TextureImporterSettings();
                TextureImporterSettings prevSettings = new TextureImporterSettings();
                textureImporter.ReadTextureSettings(setting);
                textureImporter.ReadTextureSettings(prevSettings);
                setting.readable = readable;
                setting.textureFormat = format;
                setting.alphaIsTransparency = alphaIsTransparency;
                setting.filterMode = FilterMode.Bilinear;
                setting.maxTextureSize = maxTextureSize;
                setting.ApplyTextureType(type, apply);
                textureImporter.SetTextureSettings(setting);
                AssetDatabase.Refresh();
                AssetDatabase.StopAssetEditing();

                AssetDatabase.ImportAsset(path);
                return prevSettings;
            }
            return null;
        }

        /// <summary>
        /// This helper function is designed for application of certain settings to texture importer. It will return original settings in case someone want to reapply them later
        /// </summary>
        /// <param name="texture"> texture which settings get modified </param>
        /// <param name="readable"> sets flag to texture allowing it to be red. </param>       
        /// <returns> previous texture settings</returns>
        static public TextureImporterSettings ChangeTextureSettings(Texture texture, bool readable)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            if (path.Length > 0)
            {
                return ChangeTextureSettings(path, readable);
            }

            Debug.LogWarning("Texture is most likely no longer valid or part of the assets!");

            return null;
        }

        /// <summary>
        /// This helper function is designed for application of certain settings to texture importer. It will return original settings in case someone want to reapply them later
        /// </summary>
        /// <param name="path"> asset path to texture which settings get modified </param>
        /// <param name="readable"> sets flag to texture allowing it to be red. </param>        
        /// <returns> previous texture settings</returns>
        static public TextureImporterSettings ChangeTextureSettings(string path, bool readable)
        {
            Debug.Log("Setting file to be readable at: " + path);
            if (File.Exists(path))
            {
                TextureImporter textureImporter = (TextureImporter)AssetImporter.GetAtPath(path);

                AssetDatabase.StartAssetEditing();
                TextureImporterSettings setting = new TextureImporterSettings();
                TextureImporterSettings prevSettings = new TextureImporterSettings();
                textureImporter.ReadTextureSettings(setting);
                textureImporter.ReadTextureSettings(prevSettings);
                setting.readable = readable;
                textureImporter.SetTextureSettings(setting);
                AssetDatabase.Refresh();
                AssetDatabase.StopAssetEditing();

                AssetDatabase.ImportAsset(path);
                return prevSettings;
            }
            return null;
        }

        /// <summary>
        /// This helper function is designed for application of setting block
        /// </summary>
        /// <param name="texture"> texture which settings get modified </param>
        /// <param name="settings"></param>      
        /// <returns> previous texture settings</returns>
        static public void ChangeTextureSettings(Texture texture, TextureImporterSettings settings)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            if (path.Length > 0)
            {
                ChangeTextureSettings(path, settings);
                return;
            }

            Debug.LogWarning("Texture is most likely no longer valid or part of the assets!");
        }
        
        /// <summary>
        /// This helper function is designed for application of setting block
        /// </summary>
        /// <param name="path"> asset path to texture which settings get modified </param>
        /// <param name="settings"></param>
        /// <returns></returns>
        static public void ChangeTextureSettings(string path, TextureImporterSettings settings)
        {
            if (File.Exists(path))
            {
                TextureImporter textureImporter = (TextureImporter)AssetImporter.GetAtPath(path);

                AssetDatabase.StartAssetEditing();
                textureImporter.SetTextureSettings(settings);
                AssetDatabase.Refresh();
                AssetDatabase.StopAssetEditing();

                AssetDatabase.ImportAsset(path);
            }
        }
    }
}
#endif