using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


[Serializable]
public class UFTAtlasEntryMetadata{
	[SerializeField]
	public string _name;
	
	[SerializeField]
	public string _assetPath;
	
	[SerializeField]
	public Rect _pixelRect;
	
	[SerializeField]
	public Rect _uvRect;

    [SerializeField]
    public Vector2 _pivot;
	
	[SerializeField]
	public bool _isTrimmed;
	
	public UFTAtlasEntryMetadata (string name, string assetPath, Rect pixelRect, Rect uvRect, bool isTrimmed, Vector2 pivot)
	{
		this._name = name;
		this._assetPath = assetPath;
		this._pixelRect = pixelRect;
		this._uvRect = uvRect;
		this._isTrimmed = isTrimmed;
        this._pivot = pivot;
	}

	
	
	public string assetPath {
		get {
			return this._assetPath;
		}		
	}

	public bool isTrimmed {
		get {
			return this._isTrimmed;
		}		
	}

	public string name {
		get {
			return this._name;
		}		
	}

	public Rect pixelRect {
		get {
			return this._pixelRect;
		}		
	}

	public Rect uvRect {
		get {
			return this._uvRect;
		}		
	}
}


[Serializable]
public class UFTAtlasMetadata : ScriptableObject {
	[SerializeField]
	public UFTAtlasEntryMetadata[] entries;
	
	[SerializeField]
	public Texture2D texture;
	
	[SerializeField]
	public string atlasName;

    [NonSerialized]
    private Dictionary<string, int> nameMap;

    public UFTAtlasEntryMetadata GetByName(string name)
    {
        if (nameMap == null)
        {
            nameMap = new Dictionary<string, int>();
            for(int i=0; i< entries.Length; i++ )
            {
                nameMap[entries[i].name] = i;
            }
        }

        if (nameMap.ContainsKey(name))
        {
            return entries[nameMap[name]];
        }

        return null;
    }
	
}
