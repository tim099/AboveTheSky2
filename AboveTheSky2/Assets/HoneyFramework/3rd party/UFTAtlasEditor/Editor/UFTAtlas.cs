using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


[Serializable]
public class UFTAtlas : ScriptableObject {

    public enum DragMode
    {
        DragImageOrPivot,
        DragImageOnly,
        DragPivotOnly
    }

    public DragMode currentDragMode = DragMode.DragImageOrPivot;

	[SerializeField]
	private UFTAtlasSize _atlasWidth;

	public UFTAtlasSize atlasWidth {
		get {
			return this._atlasWidth;
		}
		set {
			_atlasWidth = value;
			sendEventAtlasWidthHeightChanged();
		}
	}	
	
	
	[SerializeField]
	private UFTAtlasSize _atlasHeight;

	public UFTAtlasSize atlasHeight {
		get {
			return this._atlasHeight;
		}
		set {
			_atlasHeight = value;
			sendEventAtlasWidthHeightChanged();
		}
	}	
	[SerializeField]
	public List<UFTAtlasEntry> atlasEntries;	
	
	[SerializeField]
	public int borderSize=2;
	
	[SerializeField]
	private string _atlasName;

	public string atlasName {
		get {
			return this._atlasName;
		}
		set {
			_atlasName = value;
			sendEventAtlasChanged();
		}
	}	
	
	Texture2D atlasCanvasBG;
	
	public static Texture2D borderTexture;
    public static Texture2D pivotImage;
	
	private static int atlasTileCubeFactor=16; // it equals of the cube size on bg
	private Rect atlasBGTexCoord=new Rect(0,0,1,1);
	
	
	private UFTAtlasEntry clickedTextureOnCanvas;
	private bool recreateTexturesPositions=false;
	
	
	void OnEnable() {
		hideFlags = HideFlags.HideAndDontSave;
		initParams ();	
	}
	
	public void sendEventAtlasWidthHeightChanged ()
	{		
		if (UFTAtlasEditorEventManager.onAtlasSizeChanged!=null)
						UFTAtlasEditorEventManager.onAtlasSizeChanged((int)atlasWidth,(int)atlasHeight);		
	}

    public void readPropertiesFromMetadata(UFTAtlasMetadata atlasMetadata)
    {
        atlasWidth = (UFTAtlasSize)atlasMetadata.texture.width;
        atlasHeight = (UFTAtlasSize)atlasMetadata.texture.height;
        List<UFTAtlasEntry> entries = new List<UFTAtlasEntry>();
        foreach (UFTAtlasEntryMetadata meta in atlasMetadata.entries)
        {
            UFTAtlasEntry entry = UFTAtlasEntry.CreateInstance<UFTAtlasEntry>();
            try
            {
                entry.readPropertiesFromMetadata(meta);
                entry.uftAtlas = this;
                entries.Add(entry);
            }
            catch (TextureDoesNotExistsException e)
            {
                Debug.LogWarning("texture " + e.texturePath + " does not exists exception");
            }
        }
        this.atlasEntries = entries;
        this.atlasName = atlasMetadata.atlasName;
    }

#if UNITY_EDITOR
	public void OnGUI(){
		int width=(int)atlasWidth;
		int height=(int)atlasHeight;
		
		
		//check if in previous frame we clicked on texture, if we did, move this texture to the last index in collection
		if (recreateTexturesPositions){
			atlasEntries.Remove(clickedTextureOnCanvas);
			atlasEntries.Add(clickedTextureOnCanvas);
			recreateTexturesPositions=false;	
		}
		
		
		// check if user pressed button delete, in this case we will remove last element in the list		
		if ((Event.current.type==EventType.KeyDown) && (Event.current.keyCode == KeyCode.Delete) && (atlasEntries!=null) && (atlasEntries.Count >0)){			
			removeLatestEntryFromList ();
		}				
		
		Rect canvasRect = new Rect (0, 0, width, height);
		GUI.DrawTextureWithTexCoords(canvasRect,UFTTextureUtil.atlasCanvasBGTile,atlasBGTexCoord,false);
		
		if(atlasEntries!=null)
        {
			foreach(UFTAtlasEntry toc in  atlasEntries)
            {					
				toc.OnGUI();
			}	
	
			// draw border if mouse under the canvas			
			if (canvasRect.Contains (Event.current.mousePosition)){
				
				Color color=GUI.color;									
				foreach(UFTAtlasEntry toc in  atlasEntries){
                    
                    GUI.color = UFTTextureUtil.borderColorDict[UFTTextureState.showBorder];
                    GUI.Box(toc.canvasRect, GUIContent.none, UFTTextureUtil.borderStyle);

                    Rect r = new Rect(toc.canvasRect.xMin + toc.pivot.x - 4, toc.canvasRect.yMin + toc.pivot.y - 4, 8, 8);
                    GUI.color = Color.white;
                    GUI.DrawTexture(r, pivotImage);
				}	
				GUI.color=color;
			}		
		}		
	}
	
	public void addNewEntry(Texture2D texture, string assetPath){		
		string name=assetPath.Substring(assetPath.LastIndexOf('/')+1);		
        if (name.Contains("."))
        {
            name = name.Substring(0, name.LastIndexOf("."));
        }
		Rect rect=new Rect(0,0,texture.width,texture.height);
		UFTAtlasEntry uftAtlasEntry=UFTAtlasEntry.CreateInstance<UFTAtlasEntry>();
		uftAtlasEntry.assetPath=assetPath;
		uftAtlasEntry.textureName=name;		
		uftAtlasEntry.canvasRect=rect;
		uftAtlasEntry.texture=texture;
        uftAtlasEntry.pivot = new Vector2( rect.center.x, rect.max.y) - rect.min;
		uftAtlasEntry.uftAtlas=this;
		atlasEntries.Add( uftAtlasEntry);
		if (UFTAtlasEditorEventManager.onAddNewEntry!=null)
			UFTAtlasEditorEventManager.onAddNewEntry(uftAtlasEntry);
		sendEventAtlasChanged();
	}
	
	
	
	void initParams ()
	{		
		borderTexture=UFTTextureUtil.createOnePxBorderTexture();
        pivotImage = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/HoneyFramework/3rd party/UFTAtlasEditor/Pivot.psd", typeof(Texture2D)) as Texture2D;

		
		if(atlasEntries==null)
			atlasEntries=new List<UFTAtlasEntry>();
		
		//init listeners
		UFTAtlasEditorEventManager.onDragInProgress+=onDragInProgressListener;
		UFTAtlasEditorEventManager.onStopDragging+=onStopDraggingListener;
		UFTAtlasEditorEventManager.onStartDragging+=onStartDraggingListener;
		UFTAtlasEditorEventManager.onAtlasSizeChanged+=onAtlasSizeChanged;
		onAtlasSizeChanged((int)atlasWidth,(int)atlasHeight);
	}
	
	public void removeAllEntries(){
		while(atlasEntries.Count>0)
			removeLatestEntryFromList();		
	}
	
	public void trimAllEntries(){
		bool somethingChanged=false;
		foreach(UFTAtlasEntry atlasEntry in atlasEntries){
			if (atlasEntry.trimTexture())
				somethingChanged=true;
		}
		if (somethingChanged)
			sendEventAtlasChanged();
	}
	
	
	private void onDragInProgressListener(){
		Repaint();	
	}
	
	private void onStopDraggingListener(UFTAtlasEntry uftAtlasEntry){		
		Repaint();			
	}

	void removeLatestEntryFromList ()
	{		
		UFTAtlasEntry latestEntry= atlasEntries[atlasEntries.Count-1];
		if (UFTAtlasEditorEventManager.onRemoveEntry!=null)
			UFTAtlasEditorEventManager.onRemoveEntry(latestEntry);			
		atlasEntries.Remove(latestEntry);
		sendEventAtlasChanged();
	}
	
	private void Repaint(){
		if (UFTAtlasEditorEventManager.onNeedToRepaint!=null)
			UFTAtlasEditorEventManager.onNeedToRepaint();
	}
	
	
	private void sendEventAtlasChanged(){
		if (UFTAtlasEditorEventManager.onAtlasChange!=null)
			UFTAtlasEditorEventManager.onAtlasChange();
	}
	
	// we cant move this element to the last position in the list, because in paralel iterator can use list
	// because of that we will just store this value, and in nex frame in OnGUI function
	// we will move this object to the last position
	private void onStartDraggingListener (UFTAtlasEntry textureOnCanvas)
	{		
		clickedTextureOnCanvas=textureOnCanvas;		
		recreateTexturesPositions=true;
	}
	
	
	// in case if atlas size is changed, we need to send event with new size
	void onAtlasSizeChanged (int atlasWidth, int atlasHeight)
	{	
		atlasBGTexCoord=new Rect(0,0,atlasWidth/atlasTileCubeFactor,atlasHeight/atlasTileCubeFactor);				
	}

    //this function build atlas texture, create atlas metadata and return it	
	public UFTAtlasMetadata saveAtlasTextureAndGetMetadata(string assetPath, UFTAtlasMetadata metadata){
		List<UFTAtlasEntryMetadata> entryMeta = atlasEntries.ConvertAll(new Converter<UFTAtlasEntry,UFTAtlasEntryMetadata>(entryToEntryMetaConverter));

        Texture2D texture = buildAtlasTexture2d();

		texture=UFTTextureUtil.saveTexture2DToAssets(texture, assetPath);

        if (metadata == null)
        {
            metadata = UFTAtlasMetadata.CreateInstance<UFTAtlasMetadata>();
        }
        metadata.entries = entryMeta.ToArray();
        metadata.texture = texture;
        metadata.atlasName = atlasName;

        return metadata;
	}

#endif

    private static UFTAtlasEntryMetadata entryToEntryMetaConverter(UFTAtlasEntry entry)
    {		
		return entry.getMetadata();
	}				
	
	public bool  arrangeEntriesUsingUnityPackager(){
		int width=(int)atlasWidth;
		int height=(int)atlasHeight;
		Texture2D tmpTexture=new Texture2D(width,height);
		Texture2D[] entries= atlasEntries.ConvertAll<Texture2D>(entry=>entry.texture).ToArray();
		Rect[] rects=tmpTexture.PackTextures(entries,borderSize);
		
		for (int i = 0; i < rects.Length; i++) {
			//convert rect from Atlas(which has 0->1 values, 0.5 means center to pixel values)			
			Rect newRect=new Rect(rects[i].x*width,rects[i].y*height,atlasEntries[i].canvasRect.width,atlasEntries[i].canvasRect.height);			
			atlasEntries[i].canvasRect=newRect;			
		}		
		DestroyImmediate(tmpTexture,true);

        foreach(UFTAtlasEntry ae in atlasEntries)
        {
            
            foreach(UFTAtlasEntry other in atlasEntries)
            {
                if (ae != other && ae.canvasRect.Overlaps(other.canvasRect))
                {
                    return false;
                }
            }            
        }

        return true;
	}
	
	
	// save all entries textures to one Texture2d, this function doesn't store result texture to asset or to the file
	public Texture2D buildAtlasTexture2d(){
		Texture2D atlasTexture=new Texture2D((int)atlasWidth, (int)atlasHeight, TextureFormat.RGBA32, true);

        Color32[] atlasColors = new Color32[(int)atlasWidth * (int)atlasHeight];
        for (int i = 0; i < atlasColors.Length; i++ )
        {
            atlasColors[i] = Color.clear;
        }

		foreach (UFTAtlasEntry entry in atlasEntries){
			Texture2D entryTexture=entry.texture;
			Color32[] entryColors=entryTexture.GetPixels32();
			
			int rowPosition=0;			
			int offset=(int)(atlasColors.Length-((entry.canvasRect.y+entry.canvasRect.height) * (int)atlasWidth)+entry.canvasRect.x);
			for (int i = 0; i < entryColors.Length; i++) {
				if (rowPosition==entry.canvasRect.width){		
					rowPosition=0;	
					offset=offset+ (int)atlasWidth;					
				}				
				atlasColors[offset+rowPosition]=entryColors[i];
				rowPosition++;
			}			
		}	
		atlasTexture.SetPixels32(atlasColors);
        atlasTexture.Apply();

		return atlasTexture;
	}
	
}
