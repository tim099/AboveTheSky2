using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using HoneyFramework;
/*
 *  Class designed for work with Terrain editor where it allows for visual selection of foreground elements on different terrain types
 */
public class SpriteSelectorWindow : EditorWindow
{
    static SpriteSelectorWindow window;
    static TerrainDefinition curentTerrain;
    static string[] atlasCollection;
    static List<UFTAtlasMetadata> atlasMetadata = new List<UFTAtlasMetadata>();
    static UFTAtlasMetadata selectedAtlas = null;

    Vector2 scroll = Vector2.zero;

    //storage for textures read from atlas.
    //We do not load images from sources because textures could change and will not represent what we will achieve at runtime.
    static Dictionary<string, Texture2D> deAtlassedTextures = new Dictionary<string, Texture2D>();

    //defines how big single graphic would be.
    int imageSize = 150;

    static void Init()
    {
        // Get existing open window or if none, make a new one:
        window = (SpriteSelectorWindow)EditorWindow.GetWindow(typeof(SpriteSelectorWindow));
    }

    public static void OpenWindow(TerrainDefinition selectedTerrain)
    {
        Init();
        curentTerrain = selectedTerrain;
    }


    void OnGUI()
    {        

        if (curentTerrain == null) return;

        // If its first run or refresh have been requested by  pressing button we will fetch atlasses available for us in project
        // so that the user my chose one of them for foreground        
        if (atlasCollection == null || GUILayout.Button("Find Sprite Atlases"))
        {
            List<string> atlasses = new List<string>();
            atlasMetadata = new List<UFTAtlasMetadata>();

            string[] guids = AssetDatabase.FindAssets("t:UFTAtlasMetadata");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);

                if (path != null && path.Length > 0)
                {
                    UFTAtlasMetadata t = AssetDatabase.LoadAssetAtPath(path, typeof(UFTAtlasMetadata)) as UFTAtlasMetadata;
                    if (!atlasMetadata.Contains(t))
                    {
                        atlasses.Add(t.atlasName);
                        atlasMetadata.Add(t);
                    }
                }
            }

            atlasCollection = atlasses.ToArray();
        }
        
        if (atlasCollection.Length > 0)
        {
            int selected = selectedAtlas == null ? 0 : atlasMetadata.IndexOf(selectedAtlas);
            if (selected == -1) selected = 0;

            int newSelection = EditorGUILayout.Popup(selected, atlasCollection);
            if (newSelection != selected || selectedAtlas == null)
            {
                selectedAtlas = atlasMetadata[newSelection];
                deAtlassedTextures.Clear();

                //preparation for reading atlas texture. Its likely that texture is unreadable and compressed. 
                //for this process we will have to change its mode temporarily, and store old setting to reapply them later
                TextureImporterSettings oldSettings = TextureUtils.ChangeTextureSettings(selectedAtlas.texture, true);
                if (oldSettings == null) return;

                // Converting part of the atlas texture into separated textures fitting to the scale of the window is done using atlas mipmaps and coordinate scaling

                foreach (UFTAtlasEntryMetadata m in selectedAtlas.entries)
                {
                    Rect size = m._pixelRect;
                    int width = Mathf.RoundToInt(size.width);
                    int height = Mathf.RoundToInt(size.height);
                    int minX = Mathf.RoundToInt(size.xMin);
                    int minY = Mathf.RoundToInt(size.yMin);
                    int maxSize = Mathf.Max(width, height);

                    //mipmap levels are x1, x2, x4, x8. to match original texture. 
                    //we have to invert this process to find best mipmap level fitting our texture size.
                    // "imageSize +1" ensures that if size is exactly matching we do not need to use smaller mipmap, because division would return less than 1
                    int mLevel = (int)Mathf.Sqrt(maxSize / (imageSize + 1));
                    if (selectedAtlas.texture.mipmapCount < mLevel) mLevel = 0;                    

                    //height starts from the bottom of the texture, so we need to find "min" taking into account                    
                    minY = selectedAtlas.texture.height - minY - height;

                    //scale values using bytewise operations to correct mipmap level
                    width = width >> mLevel;
                    height = height >> mLevel;
                    minX = minX >> mLevel;
                    minY = minY >> mLevel;

                    Texture2D texture = new Texture2D(width, height, TextureFormat.ARGB32, true);

                    Color[] colors = selectedAtlas.texture.GetPixels(minX,
                                                                     minY, 
                                                                     width,
                                                                     height, 
                                                                     mLevel);
                    texture.SetPixels(colors);
                    texture.Apply();
                                        
                    deAtlassedTextures[m.name] = texture;
                }

                //reapplying original atlas texture settings 
                TextureUtils.ChangeTextureSettings(selectedAtlas.texture, oldSettings);
            }
                                    
        }
        else
        {
            EditorGUILayout.LabelField("No atlased sprites have been found in resource folders!");
            return;
        }

        if (selectedAtlas == null || window == null) return;

        //find how many items fit in a row
        Rect r = window.position;
        int horizontalCount = Mathf.Max(1, (int)r.width / imageSize);

        //using try ensures no errors thrown by repaint if the structure of the editor gui change. 
        //they are harmless but annoying when they show for single frame.
        try
        {
            scroll = GUILayout.BeginScrollView(scroll);
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            for (int i = 0; i < selectedAtlas.entries.Length; i++)
            {
                UFTAtlasEntryMetadata t = selectedAtlas.entries[i];
                int index = curentTerrain.source.fgTypes.FindIndex(o => o.name == t.name);
                if (TextureButton(t, index >= 0))
                {
                    if (index >= 0)
                    {
                        curentTerrain.source.fgTypes.RemoveAt(index);
                        MHDatabase.SaveDB<MHTerrain>();
                    }
                    else
                    {
                        MHSimpleCounter c = new MHSimpleCounter();
                        c.name = t.name;
                        c.count = 1;
                        curentTerrain.source.fgTypes.Add(c);
                        MHDatabase.SaveDB<MHTerrain>();
                    }
                }

                //end of line if enough elements are in line
                if (((i + 1) % horizontalCount) == 0)
                {
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
        }
        catch { /* repaint updates sometimes may get interrupted. Nothing to worry about */ }
    }

    /// <summary>
    /// function designed to display single texture block with button and description. 
    /// Colored fore best readability
    /// </summary>
    /// <param name="texture">texture metadata to show. Note! texture will be red from previously prepared dictionary using name from this metadata</param>
    /// <param name="selected">information about this element being selected or not</param>
    /// <returns>true if button within this element have been pressed. Most likely will result in change state of the foreground selection</returns>
    bool TextureButton(UFTAtlasEntryMetadata texture, bool selected)
    {

        bool ret = false;
        if (selected)
        {
            GUI.color = Color.yellow;
        }
        else
        {
            GUI.color = Color.white;
        }

        GUILayout.BeginVertical("Box", GUILayout.Width(imageSize - 6));
        GUI.color = Color.white;
        ret = GUILayout.Button(deAtlassedTextures[texture.name], GUILayout.Width(imageSize - 10), GUILayout.Height(imageSize - 10));
        GUILayout.Label(texture.name);
        GUILayout.EndVertical();                

        return ret;
    }

    /// <summary>
    /// this keeps window up to date if changes are done in terrain editor directly.
    /// </summary>
    /// <returns></returns>
    void OnInspectorUpdate()
    {
        Repaint();
    }
}
