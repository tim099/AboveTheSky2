using UnityEngine;
using System.Collections;

public delegate void DragInProgress();
public delegate void StopDragging(UFTAtlasEntry uftAtlasEntry);
public delegate void AtlasSizeChanged(int width, int height);
public delegate void StartDragging(UFTAtlasEntry uftAtlasEntry);
public delegate void NeedToRepaint();
public delegate void AtlasChange();
public delegate void AddNewEntry(UFTAtlasEntry uftAtlasEntry);
public delegate void RemoveEntry(UFTAtlasEntry uftAtlasEntry);
public delegate void TextureSizeChanged(UFTAtlasEntry uftAtlasEntry);
public delegate void AtlasMetadataObjectChanged(UFTAtlasMetadata atlasMetadata);

public class UFTAtlasEditorEventManager {
	public static DragInProgress onDragInProgress;
	public static StopDragging onStopDragging;
	public static AtlasSizeChanged onAtlasSizeChanged;
	public static StartDragging onStartDragging;
	public static NeedToRepaint onNeedToRepaint; 
	public static AtlasChange onAtlasChange;
	public static AddNewEntry onAddNewEntry;
	public static RemoveEntry onRemoveEntry;
	public static TextureSizeChanged onTextureSizeChanged;
	public static AtlasMetadataObjectChanged onAtlasMetadataObjectChanged;
}
