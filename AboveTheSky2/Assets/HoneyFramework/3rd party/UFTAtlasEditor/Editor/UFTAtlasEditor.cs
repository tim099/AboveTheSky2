using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;


/*
 * Thanks AngryAnt for a greate drag'n'drop example at this page http://angryant.com/2009/09/18/gui-drag-drop/
 * 
 */



public class UFTAtlasEditor : EditorWindow {
	[SerializeField]
	private UFTAtlas uftAtlas;
	
	private bool isAtlasDirty=false;
	public Vector2 scrollPosition = Vector2.zero;
	public Vector2 atlasTexturesScrollPosition=Vector2.zero;
	public string ATLAS_FILE_MASK="atlas_";
	
	
	UFTAtlasMetadata atlasMetadata;
	private static string EDITOR_PREFS_KEY_ATLAS_WIDHT="uft.atlasEditor.width";
	private static string EDITOR_PREFS_KEY_ATLAS_HEIGHT="uft.atlasEditor.height";
	
	
	public static int DEFAULT_ATLAS_WIDTH=512;
	public static int DEFAULT_ATLAS_HEIGHT=512;

    public string alignSuccesful = "";
	
	
	[MenuItem ("Window/UFT Atlas Editor")]
    static void ShowWindow () {    		
		EditorWindow.GetWindow <UFTAtlasEditor>("UFT Atlas Editor",typeof(SceneView));				
    }
	
	void OnEnable(){
		initOnNewParameters ();
		
		registerListeners ();
		
	}

	public void initOnNewParameters ()
	{
		atlasMetadata=null;
		uftAtlas=UFTAtlas.CreateInstance<UFTAtlas>();
		//get free name
		string atlasName=UFTFileUtil.getFileName(ATLAS_FILE_MASK);
		uftAtlas.atlasName=atlasName;		
		readWidthHeightFromEditorPrefs();		
	}

	public void registerListeners ()
	{
		UFTAtlasEditorEventManager.onNeedToRepaint+=onNeedToRepaint;
		UFTAtlasEditorEventManager.onAtlasChange+=onAtlasChange;		
		UFTAtlasEditorEventManager.onAddNewEntry+=onAddNewEntry;
		UFTAtlasEditorEventManager.onRemoveEntry+=onRemoveEntry;
		UFTAtlasEditorEventManager.onStopDragging+=onStopDragging;
		UFTAtlasEditorEventManager.onStartDragging+=onStartDragging; 
		UFTAtlasEditorEventManager.onTextureSizeChanged+=onTextureSizeChanged;
		UFTAtlasEditorEventManager.onAtlasSizeChanged+=onAtlasSizeChanged;
		UFTAtlasEditorEventManager.onAtlasMetadataObjectChanged+=onAtlasMetadataObjectChanged;
	}
	
	

	public void onAtlasSizeChanged (int width, int height)
	{
		EditorPrefs.SetInt(EDITOR_PREFS_KEY_ATLAS_WIDHT,width);
		EditorPrefs.SetInt(EDITOR_PREFS_KEY_ATLAS_HEIGHT,height);
	}
	
	
	public void readWidthHeightFromEditorPrefs(){
		int width= EditorPrefs.GetInt(EDITOR_PREFS_KEY_ATLAS_WIDHT,DEFAULT_ATLAS_WIDTH);
		int height= EditorPrefs.GetInt(EDITOR_PREFS_KEY_ATLAS_HEIGHT,DEFAULT_ATLAS_HEIGHT);
		uftAtlas.atlasWidth=(UFTAtlasSize) width;
		uftAtlas.atlasHeight=(UFTAtlasSize)height;		
	}

	
	
	
	public void onAddNewEntry (UFTAtlasEntry uftAtlasEntry)
	{		
		Undo.RecordObject(uftAtlasEntry,"UFTAtlasEntry" + uftAtlasEntry.id);
	}

	public void onStopDragging (UFTAtlasEntry uftAtlasEntry)
	{
		//
	}

	public void onStartDragging (UFTAtlasEntry uftAtlasEntry)
	{
		registerSnapshotTargetState (uftAtlasEntry);
	}

	public void onRemoveEntry (UFTAtlasEntry uftAtlasEntry)
	{		
		registerSnapshotTargetState (uftAtlasEntry);
	}

	void registerSnapshotTargetState (UFTAtlasEntry uftAtlasEntry)
	{
		registerSnapshotForEntry (uftAtlasEntry);
		
	}

	void onTextureSizeChanged (UFTAtlasEntry uftAtlasEntry)
	{
		registerSnapshotForEntry (uftAtlasEntry);
	}

	void registerSnapshotForEntry (UFTAtlasEntry uftAtlasEntry)
	{
		registerAtlasSnapshot ();

        Undo.RecordObject(uftAtlasEntry, "stop dragging uftAtlasEntry id=" + uftAtlasEntry.id);		
	}	
	
	public void onAtlasMetadataObjectChanged (UFTAtlasMetadata atlasMetadata)
	{		
		registerAtlasSnapshot ();
		initOnNewParameters();
		if (atlasMetadata!=null){
			//create UFTAtlas from metadata.
			uftAtlas=UFTAtlas.CreateInstance<UFTAtlas>();
			uftAtlas.readPropertiesFromMetadata(atlasMetadata);			
		}
		
	}

	public void registerAtlasSnapshot ()
	{
		//Register Undo for the previous state	
        Undo.RecordObject(uftAtlas, "atlas");	
	}
	
		
	void Update(){
		Repaint();	
	}

	private void onAtlasChange ()
	{
		isAtlasDirty=true;
        registerAtlasSnapshot();		
	}
	
	
	private void onNeedToRepaint(){
		Repaint ();
	}	
	
	void OnGUI () {				
		if (isAtlasDirty){
			EditorUtility.SetDirty(uftAtlas);
			isAtlasDirty=false;
		}
		EditorGUILayout.Separator();
		EditorGUILayout.BeginHorizontal();
			
			EditorGUILayout.BeginVertical (new GUILayoutOption[]{GUILayout.Width(200f)});
				EditorGUILayout.Separator();		
				EditorGUILayout.LabelField("Atlas:");
				string newName=EditorGUILayout.TextField("name",uftAtlas.atlasName);
				if (newName!=uftAtlas.atlasName){
					uftAtlas.atlasName=newName;
				}
		
				UFTAtlasMetadata newMeta= (UFTAtlasMetadata) EditorGUILayout.ObjectField((Object)atlasMetadata,typeof(UFTAtlasMetadata),false);
				if (newMeta!=atlasMetadata){					
					if (uftAtlas.atlasEntries.Count==0 || (uftAtlas.atlasEntries.Count>0 && EditorUtility.DisplayDialog("This action will remove all existing textures on your atlas", "Do you want to proceed?","Proceed","Cancel"))){
						if (UFTAtlasEditorEventManager.onAtlasMetadataObjectChanged!=null)
							UFTAtlasEditorEventManager.onAtlasMetadataObjectChanged(newMeta);
						atlasMetadata=newMeta;
					}								
				}
		
				EditorGUILayout.BeginHorizontal();
					
					if (GUILayout.Button("new")){
						if (uftAtlas.atlasEntries.Count==0 || (uftAtlas.atlasEntries.Count>0 && EditorUtility.DisplayDialog("This action will remove all existing textures on your atlas", "Do you want to proceed?","Proceed","Cancel"))){
							onClickNew();		
						}
					}
					if (GUILayout.Button("save"))
						onClickSave();
				EditorGUILayout.EndHorizontal();
                
				UFTAtlasSize newWidth = (UFTAtlasSize) EditorGUILayout.EnumPopup("width",uftAtlas.atlasWidth);
				UFTAtlasSize newHeight = (UFTAtlasSize) EditorGUILayout.EnumPopup("height",uftAtlas.atlasHeight);

                uftAtlas.currentDragMode = (UFTAtlas.DragMode) EditorGUILayout.EnumPopup(uftAtlas.currentDragMode);

				EditorGUILayout.Separator();
				EditorGUILayout.LabelField("Align:");
		
				uftAtlas.borderSize= Mathf.Max(0, EditorGUILayout.IntField("border size",uftAtlas.borderSize));
                bool autoAlign = GUILayout.Button("auto align & size");
                bool normalAlign = GUILayout.Button("align");
                if ((autoAlign || normalAlign) && uftAtlas != null && uftAtlas.atlasEntries.Count > 0)
                {
                    bool successful = true;
                    if (normalAlign)
                    {
                        successful = uftAtlas.arrangeEntriesUsingUnityPackager();                        
                    }
                    if (autoAlign)
                    {                        
                        UFTAtlasSize oldWidth = uftAtlas.atlasWidth;
                        UFTAtlasSize oldHeight = uftAtlas.atlasHeight;                        

                        int w = (int)128;
                        int h = (int)128;

                        while (true)
                        {                                                        
                            uftAtlas.atlasHeight = (UFTAtlasSize)(w);                            
                            uftAtlas.atlasWidth = (UFTAtlasSize)(h);
                            if (uftAtlas.arrangeEntriesUsingUnityPackager()) { break; }

                            if (uftAtlas.atlasWidth == UFTAtlasSize._4096 && uftAtlas.atlasHeight == UFTAtlasSize._4096)
                            {
                                successful = false;
                                break;
                            }

                            uftAtlas.atlasHeight = (UFTAtlasSize)(w * 2);
                            uftAtlas.atlasWidth = (UFTAtlasSize)(h);
                            if (uftAtlas.arrangeEntriesUsingUnityPackager()) { break; }

                            uftAtlas.atlasHeight = (UFTAtlasSize)(w);
                            uftAtlas.atlasWidth = (UFTAtlasSize)(h * 2);
                            if (uftAtlas.arrangeEntriesUsingUnityPackager()) { break; }
                            
                            w *= 2;
                            h *= 2;
                        }

                        if (!successful)
                        {
                            uftAtlas.atlasWidth = oldWidth;
                            uftAtlas.atlasHeight = oldHeight;
                        }
                        else if (uftAtlas.atlasWidth != oldWidth || uftAtlas.atlasHeight != oldHeight)
                        {
                            newWidth = uftAtlas.atlasWidth;
                            newHeight = uftAtlas.atlasHeight;

                            uftAtlas.atlasWidth = oldWidth;
                            uftAtlas.atlasHeight = oldHeight;
                        }
                    }
                    if (successful)
                    {
                        alignSuccesful = "Alignment successful. ";
                    }
                    else
                    {
                        alignSuccesful = "Alignment failed! Try extending atlas size";
                    }
                }

                GUILayout.Label(alignSuccesful);
				
				if (newWidth!=uftAtlas.atlasWidth || newHeight!=uftAtlas.atlasHeight){
					uftAtlas.atlasWidth=newWidth;
					uftAtlas.atlasHeight=newHeight;
					
					if (UFTAtlasEditorEventManager.onAtlasSizeChanged!=null)
						UFTAtlasEditorEventManager.onAtlasSizeChanged((int)newWidth,(int)newHeight);
				}
		
				EditorGUILayout.Separator();
				EditorGUILayout.LabelField("Atlas Textures ["+uftAtlas.atlasEntries.Count+"]:");				
				atlasTexturesScrollPosition = EditorGUILayout.BeginScrollView(atlasTexturesScrollPosition);
				
				if (uftAtlas.atlasEntries.Count >0)
                {
					List<string> names= uftAtlas.atlasEntries.ConvertAll(new System.Converter<UFTAtlasEntry,string>(uftAtlasEntryToString));
					names.Sort();
					foreach (string name in names) 
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUI.color = Color.red;
                        if (GUILayout.Button("x"))
                        {
                            int index = uftAtlas.atlasEntries.FindIndex(o => o.textureName == name);
                            uftAtlas.atlasEntries.RemoveAt(index);
                        }
                        GUI.color = Color.white;
						EditorGUILayout.LabelField(name,GUILayout.MaxWidth(220f));
                        EditorGUILayout.EndHorizontal();
					}					
				}		
				EditorGUILayout.EndScrollView();
				EditorGUILayout.Separator();
			EditorGUILayout.EndVertical();
			
				
			EditorGUILayout.BeginVertical();	
				EditorGUILayout.Separator();
				EditorGUILayout.BeginHorizontal();
					
					scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
						
							
					GUILayoutUtility.GetRect((int)uftAtlas.atlasWidth,(int)uftAtlas.atlasHeight);
					uftAtlas.OnGUI();
				
					EditorGUILayout.EndScrollView();
					EditorGUILayout.Separator();
				EditorGUILayout.EndHorizontal();				
			EditorGUILayout.EndVertical();
			
		
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.Separator();
		
		 if (Event.current.type == EventType.DragUpdated || Event.current.type == EventType.DragPerform){
	        DragAndDrop.visualMode = DragAndDropVisualMode.Copy; // show a drag-add icon on the mouse cursor
			if(Event.current.type==EventType.DragPerform)
				HandleDroppedObjects(DragAndDrop.objectReferences);
            
			DragAndDrop.AcceptDrag();
	        Event.current.Use();
	    }		
	}



	void HandleDroppedObjects (Object[] objectReferences)
	{
		bool addedSomething=false;
		foreach (Object item in objectReferences) {
			if(typeof(Texture2D)==item.GetType()){
				Texture2D texture=(Texture2D)item;
				addedSomething=true;
				
				if (isTextureCanvasContainsTexture (texture)){
					Debug.Log("one of texture is already on Canvas");
				} else {
					string assetPath=AssetDatabase.GetAssetPath(texture);

                    //ensure texture is readable
                    HoneyFramework.TextureUtils.ChangeTextureSettings(texture, true, true, FilterMode.Bilinear, TextureImporterFormat.ARGB32, TextureImporterType.Default, true, 2048);
                    if (assetPath == "")
                    {
                        Debug.Log("Asset path missing for " + texture.name);
                    }
                    else
                    {
                        Debug.Log("Proper asset path for: "+ assetPath);
                    }
                    uftAtlas.addNewEntry(texture, assetPath);                    
				}				
			}
		}
		
		if (!addedSomething)
			Debug.Log("there was no any Texture2D in dropped content");
	}


	void onClickSave ()
	{        
		string assetPath="Assets/"+uftAtlas.atlasName;		
		
        if (atlasMetadata == null || (EditorUtility.DisplayDialog("You are editing atlas", "Do you want to overwrite it?", "Yes", "No")))
        {					
			// if we have already metadata, that's mean that we want to override object, so just need to take old path
			string texturePath=assetPath+".png";
			string metadataPath=assetPath+".asset";
			if (atlasMetadata!=null){				
				texturePath=AssetDatabase.GetAssetPath(atlasMetadata.texture);
				metadataPath=AssetDatabase.GetAssetPath(atlasMetadata);

                atlasMetadata = uftAtlas.saveAtlasTextureAndGetMetadata(texturePath, atlasMetadata);
                EditorUtility.SetDirty(atlasMetadata);
                AssetDatabase.SaveAssets();                
			}
            else
            {
                atlasMetadata = uftAtlas.saveAtlasTextureAndGetMetadata(texturePath, null);
                AssetDatabase.CreateAsset(atlasMetadata, metadataPath);
            }            
		}
	}

	void onClickNew ()
	{
		registerAtlasSnapshot ();		
		initOnNewParameters();
	}

	
	private bool isTextureCanvasContainsTexture (Texture2D texture)
	{
		return uftAtlas.atlasEntries.Find(toc => toc.texture == texture)!=null;
	}
	
	
	
	// this function used as converter in OnGUI to show list of the entry names
	private static string uftAtlasEntryToString(UFTAtlasEntry uftAtlasEntry){
		return uftAtlasEntry.textureName;
	}
	
}
