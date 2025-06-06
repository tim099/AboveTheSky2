using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using HoneyFramework;
/*
 * Class designed to display and manage data for terrain definitions for hexes. 
 */
class TerrainEditor : EditorWindow 
{
    static TerrainEditor window;

    WorldOven worldOven;            
    MHTerrain.Type selectedEnum = MHTerrain.Type.Hills;
    Vector2 scrall = Vector2.zero;
	
	[MenuItem ("Window/HoneyFramework/Terrain Editor")]
	static void Init () 
    {
		// Get existing open window or if none, make a new one:
        window = (TerrainEditor)EditorWindow.GetWindow(typeof(TerrainEditor));        
	}

    void OnGUI()
    {
        bool requiresApply = false;

        //We now allow to reload definitions of the terrains. Its most likely first thing user have to do when opens this window, 
        //but may be useful as well later when some changes occurs during window session
        if (GUILayout.Button("Reload definitions"))
        {
            window = (TerrainEditor)EditorWindow.GetWindow(typeof(TerrainEditor));
            TerrainDefinition.ReloadDefinitions();
            return;
        }

        if (!DataManager.isInitialized)
        {
            GUILayout.Label("Missing data manager initialization!");
            return;
        }
        if (window == null)
        {
            GUILayout.Label("Missing window reference!");
            return;
        }

        List<TerrainDefinition> terrains = TerrainDefinition.definitions;
        bool add = false;
        int del = -1;

        //Button which allows to create new terrain definition slot
        GUI.color = Color.green;
        if (GUILayout.Button("+"))
        {
            add = true;
            requiresApply = true;
        }
        GUI.color = Color.white;        

        //this part will calculate how many items in a row we want to display
        Rect r = window.position;
        int horizontalCount = Mathf.Max(1, (int)r.width / 610);

        scrall = EditorGUILayout.BeginScrollView(scrall);
        GUILayout.BeginHorizontal();
        for (int i = 0; i < terrains.Count; i++)
        {
            if (i % horizontalCount == 0)
            {
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
            }
            if (!EditTerrain(terrains[i], ref requiresApply))
            {
                del = i;
                requiresApply = true;
            }
        }
        GUILayout.EndHorizontal();

        //check if any terrain requested removal
        if (del > -1)
        {
            MHTerrain.list.Remove(terrains[del].source);
            MHDatabase.SaveDB<MHTerrain>();
            TerrainDefinition.ReloadDefinitions();
        }

        //check if new terrain definition should be initialized
        if (add)
        {
            MHTerrain terrain = new MHTerrain();
            terrain.CreateDBIndex();
            MHTerrain.list.Add(terrain);
            MHDatabase.SaveDB<MHTerrain>();
            TerrainDefinition.ReloadDefinitions();
        }
        EditorGUILayout.EndScrollView();

        //if any element noticed change, database will save file
        if(requiresApply)
        {
            MHDatabase.SaveDB<MHTerrain>();
        }
    }

    /// <summary>
    /// Single terrain definition edit. 
    /// </summary>
    /// <param name="t"> terrain definition to display </param>
    /// <param name="requiresApply">returns information if terrain changed</param>
    /// <returns></returns>
    public bool EditTerrain(TerrainDefinition t, ref bool requiresApply)
    {
        bool v = true;
        GUILayout.BeginVertical("Box", GUILayout.Width(600));
            GUI.color = Color.red;

            //button which allows to mark terrain for removal
            if (GUILayout.Button("x"))
            {
                v = false;
            }
            GUI.color = Color.white;

            GUILayout.BeginHorizontal();
                //terrain specific tags and settings
                GUILayout.BeginVertical( GUILayout.Width(200));
                    GUILayout.BeginHorizontal();
                    GUI.color = t.source.IsNameUnique() ? Color.white : Color.red;
                    GUILayout.Label("Terrain Name:");                    
                    if (TextField(ref t.source.name, GUILayout.Width(110)))
                    {
                        MHDatabase.SaveDB<MHTerrain>();
                    }
                    GUI.color = Color.white;
                    GUILayout.EndHorizontal();

                    MHTerrain.Mode m = (MHTerrain.Mode)EditorGUILayout.EnumPopup(t.source.mode, GUILayout.Width(180));
                    if (m != t.source.mode)
                    {
                        t.source.mode = m;
                        MHDatabase.SaveDB<MHTerrain>();
                    }

                    DisplayAvaliableTags(t);                    
                GUILayout.EndVertical();
                
                //foreground editor for terrain
                GUILayout.BeginVertical("Box", GUILayout.Width(150));                    
                    DisplayAvaliableForegrounds(t);
                GUILayout.EndVertical();

                //texture list used to bake terrain
                GUILayout.BeginVertical(GUILayout.Width(250));
                    if (ObjectField<Texture>("Diffuse:", ref t.diffuse, false))
                    {
                        t.source.diffusePath = GetAssetPath(AssetDatabase.GetAssetPath(t.diffuse));
                        t.source.UseDiffusenameIfNoName();
                        requiresApply = true;
                    }
                    if (ObjectField<Texture>("Height:", ref t.height, false))
                    {
                        t.source.heightPath = GetAssetPath(AssetDatabase.GetAssetPath(t.height));
                        requiresApply = true;
                    }
                    if (ObjectField<Texture>("Mixer:", ref t.mixer, false))
                    {
                        t.source.mixerPath = GetAssetPath(AssetDatabase.GetAssetPath(t.mixer));
                        requiresApply = true;
                    }
                GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        return v;
    }

    /// <summary>
    /// Finds path for asset location
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public string GetAssetPath(string path)
    {
        string s = "Resources/";
        if (path.Contains(s))
        {
            int index = path.IndexOf(s);
            int extIndex = path.LastIndexOf(".");
            int charsCount = extIndex - index - s.Length;
            if (charsCount > 0)
            {
                string shortPath = path.Substring(index + s.Length, charsCount);
                return shortPath;
            }
        }

        return path;
    }

    
    /// <summary>
    /// Displays tags available for terrains to select from and those currently chosen for this terrain
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public void DisplayAvaliableTags(TerrainDefinition t)
    {
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUI.color = Color.green;
        if (GUILayout.Button("+",GUILayout.Width(19)))
        {
            if (!t.source.typeList.Contains(selectedEnum))
            {
                t.source.typeList.Add(selectedEnum);
                MHDatabase.SaveDB<MHTerrain>();
            }
        }

        GUI.color = Color.white;
        selectedEnum = (MHTerrain.Type)EditorGUILayout.EnumPopup(selectedEnum);
        GUILayout.EndHorizontal();

        int toRemove = -1;
        for (int i = 0; i < t.source.typeList.Count; i++ )
        {
            GUILayout.BeginHorizontal();
            GUI.color = Color.red;
            if (GUILayout.Button("x", GUILayout.Width(19)))
            {
                toRemove = i;
            }
            GUI.color = Color.white;

            GUILayout.Label(t.source.typeList[i].ToString());
            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();

        if (toRemove >= 0)
        {
            t.source.typeList.RemoveAt(toRemove);
            MHDatabase.SaveDB<MHTerrain>();
        }
    }

    /// <summary>
    /// Displays tags available for terrains to select from and those currently chosen for this terrain
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public void DisplayAvaliableForegrounds(TerrainDefinition t)
    {       
        Color c = TerrainDefinition.GetColor(t.source.foregroundColor);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Theme Color:");
        Color newColor = EditorGUILayout.ColorField(c);

        GUILayout.EndHorizontal();

        if (newColor != c)
        {
            uint color = (uint)(newColor.r * 255) << 16 | (uint)(newColor.g * 255) << 8 | (uint)(newColor.b * 255);
            t.source.foregroundColor = color.ToString("X");
            MHDatabase.SaveDB<MHTerrain>();
        }

        if (GUILayout.Button("Foreground selector", GUILayout.Width(150)))
        {
            SpriteSelectorWindow.OpenWindow(t);
        }

        MHSimpleCounter toRemove = null;
        foreach (MHSimpleCounter fgType in t.source.fgTypes)
        {
            
            GUILayout.BeginHorizontal();
            GUI.color = Color.red;
            if (GUILayout.Button("x", GUILayout.Width(19) ))
            {
                toRemove = fgType;
            }
            GUI.color = Color.white;
            GUILayout.Label(fgType.name);
            if (IntField(ref fgType.count))
            {
                MHDatabase.SaveDB<MHTerrain>();
            }            
            GUILayout.EndHorizontal();
        }  
      
        if (toRemove != null)
        {
            t.source.fgTypes.Remove(toRemove);
            MHDatabase.SaveDB<MHTerrain>();
        }
    }

    /// <summary>
    /// Keeps window updated in case sprite editor changes some terrain content
    /// </summary>
    /// <returns></returns>
    void OnInspectorUpdate()
    {
        Repaint();
    }



    /// <summary>
    /// Wrapper functionality which displays text field and informs if value have changed
    /// </summary>
    /// <param name="data"> reference to string which is managed </param>
    /// <param name="par"> optional parameters for text field</param>
    /// <returns>true if value have changed </returns>
    static public bool TextField(ref string data, params GUILayoutOption[] par)
    {
        string newData = GUILayout.TextField(data, par);

        if (newData != data)
        {
            data = newData;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Wrapper functionality for object field.
    /// </summary>
    /// <param name="name"> title for object edit </param>
    /// <param name="data"> object reference which have to be managed </param>
    /// <param name="allowSceneObjects"> should scene objects be allowd for this field </param>
    /// <param name="par"> extra parameters for layout design </param>
    /// <returns> returns true if value have been changed </returns>
    static public bool ObjectField<T>(string name, ref T data, bool allowSceneObjects, params GUILayoutOption[] par) where T : UnityEngine.Object
    {
        Type t;
        if (data == null)
        {
            t = typeof(UnityEngine.Object);
        }
        else
        {
            t = data.GetType();
        }

        T newData = EditorGUILayout.ObjectField(name, data, t, allowSceneObjects, par) as T;

        if (newData != data)
        {
            data = newData;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Wrapper for integer field, which gives feedback when value changes
    /// </summary>
    /// <param name="data"> integer reference for management </param>
    /// <param name="par"> extra parameters for layout design </param>
    /// <returns> returns true if value have been changed </returns>
    static public bool IntField(ref int data, params GUILayoutOption[] par)
    {
        string newData = GUILayout.TextField("" + data, par);

        if (newData != "" + data)
        {
            try
            {
                data = Convert.ToInt32(newData);
            }
            catch (OverflowException e)
            {
                Console.WriteLine("The number cannot fit in an Int32. " + e);
            }
            catch
            {
                data = 0;
            }

            return true;
        }

        return false;
    }
}
