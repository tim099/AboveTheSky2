#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

using HoneyFramework;
using UnityEditor;


public class UFTTextureUtil : MonoBehaviour {
	public static Color bgColor1=Color.white;
	public static Color bgColor2=new Color(0.8f,0.8f,0.8f,1f);
	

	private static GUIStyle _borderStyle;  

	public static GUIStyle borderStyle {
		get {
			if (_borderStyle == null){
				_borderStyle=new GUIStyle();
				_borderStyle.normal.background=UFTTextureUtil.createOnePxBorderTexture();
				_borderStyle.border=new RectOffset(1,1,1,1);
				_borderStyle.alignment=TextAnchor.MiddleCenter;
			}			
			return _borderStyle;
		}
		set {
			_borderStyle = value;
		}
	}
	
	private static Texture2D _atlasCanvasBGTile;

	public static Texture2D atlasCanvasBGTile {
		get {
			if (_atlasCanvasBGTile==null)
				_atlasCanvasBGTile=getCheckerBoardTile();
			return _atlasCanvasBGTile;
		}
		set {
			_atlasCanvasBGTile = value;
		}
	}	

	

	public static Dictionary<UFTTextureState,Color> borderColorDict=new Dictionary<UFTTextureState, Color>(){
		{UFTTextureState.passive, GUI.color},
		{UFTTextureState.onDrag, Color.green},
		{UFTTextureState.showBorder, Color.red}		
	};
	
	
	
	
	
	
	
	
	
	
	public static Texture2D createTexture(int width, int height, Color color){
		Texture2D tex = new Texture2D(width,height,TextureFormat.ARGB32,false);
		Color32[] pixels=tex.GetPixels32();
		for (int i=0;i<pixels.Length;i++){
			pixels[i]=color;	
		}			
		tex.SetPixels32(pixels);
		tex.Apply(false);		
	    return tex;
	}
	
	public static Texture2D createTexture(int width, int height, Color32[] colors){
		Texture2D tex = new Texture2D(width,height,TextureFormat.ARGB32,false);				
		tex.SetPixels32(colors);
		tex.Apply(false);		
	    return tex;
	}
	
	
	//this function create folder if no exists and store texture to the file
	public static void saveTextureToFile(Texture2D texture, string folderName, string fileName){
		
		if (!Directory.Exists(folderName))
			Directory.CreateDirectory(folderName);
		
		
		
		string filePath=folderName+"/" +fileName + ".png";
		
		saveTextureToFile (texture, filePath);
	}

	public static void saveTextureToFile (Texture2D texture, string filePath)
	{
		FileStream file=File.Open(filePath,FileMode.Create);
		byte[] byteArray=texture.EncodeToPNG();	
		BinaryWriter bw=new BinaryWriter(file);
		bw.Write(byteArray);
		file.Close();
	}

	public static Texture2D createOnePxBorderTexture(){
		string assetPath="Assets/HoneyFramework/3rd party/UFTAtlasEditor/Editor/Texture/onePxBorder.png";
		

		Texture2D texture = (Texture2D)AssetDatabase.LoadAssetAtPath(assetPath,typeof(Texture2D));
		if (texture==null){
			texture=new Texture2D(3,3);
			Color[] c=new Color[9];
			for (int i=0;i<9;i++){
				c[i]=new Color(1,1,1,1);	
			}
			c[4]=new Color(1,1,1,0); //center alpha is empty
			texture.SetPixels(c);
			texture.Apply();
			
			//save to files an then import
			saveTexture2DToAssets ( texture, assetPath);			
			
		}
		return texture;
	}

	public static Texture2D saveTexture2DToAssets (Texture2D texture, string assetPath)
	{		
#if UNITY_WEBPLAYER
	Debug.LogWarning("texture can't be saved in webplayer build mode, please switch to standalone version");	
#endif
		
		if (!assetPath.StartsWith("Assets/"))
			assetPath="Assets/"+assetPath;
		if (!assetPath.EndsWith("png"))
			assetPath=assetPath+".png";
		
		byte[] bytes = texture.EncodeToPNG();
		if (bytes != null){
#if !UNITY_WEBPLAYER
			File.WriteAllBytes(assetPath, bytes);			
#endif			
		}
		int maxTextureSize=texture.width>texture.height ? texture.width : texture.height;
		//to prevent leaking, remove this texture and import it again
		Object.DestroyImmediate((Object) texture);        
		AssetDatabase.ImportAsset(assetPath);

        //now when texture is ready modify its base to compressed
        TextureUtils.ChangeTextureSettings(assetPath, false, true, FilterMode.Bilinear, TextureImporterFormat.DXT5, TextureImporterType.Default, true, maxTextureSize);
		
        return (Texture2D)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Texture2D));
	}
/*
	public static Texture2D importTexture (string assetPath,int maxTextureSize=1024)
	{
		Texture2D texture = (Texture2D)AssetDatabase.LoadAssetAtPath(assetPath,typeof(Texture2D));
		if (texture==null)
			return null;
		TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
		textureImporter.textureFormat=TextureImporterFormat.ARGB32;
		textureImporter.textureType=TextureImporterType.Advanced;
		textureImporter.mipmapEnabled=false;
		textureImporter.wrapMode=TextureWrapMode.Clamp;
		textureImporter.filterMode=FilterMode.Point;
		textureImporter.npotScale=TextureImporterNPOTScale.None;
		textureImporter.maxTextureSize=maxTextureSize;
		AssetDatabase.ImportAsset(assetPath);
		return (Texture2D) AssetDatabase.LoadAssetAtPath(assetPath, typeof (Texture2D));
	}*/
	
	
	public static Texture2D getCheckerBoardTile(){
		int squareWidth=1;
		int textureWidth=2;

        string assetPath = "Assets/HoneyFramework/3rd party/UFTAtlasEditor/Editor/Texture/AtlasCanvasBGTile.png";
		Texture2D texture = (Texture2D)AssetDatabase.LoadAssetAtPath(assetPath,typeof(Texture2D));
		if (texture==null){
			texture=createCheckerBoard(textureWidth,textureWidth,bgColor1,bgColor2, squareWidth);
#if !UNITY_WEBPLAYER
			byte[] bytes = texture.EncodeToPNG();
			if (bytes != null)
		      File.WriteAllBytes(assetPath, bytes);
#endif
			 AssetDatabase.ImportAsset(assetPath);
		    TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
		     textureImporter.textureFormat=TextureImporterFormat.ARGB32;
		    textureImporter.textureType=TextureImporterType.Default;
			textureImporter.mipmapEnabled=false;
			textureImporter.wrapMode=TextureWrapMode.Repeat;
			textureImporter.filterMode=FilterMode.Point;
			textureImporter.npotScale=TextureImporterNPOTScale.None;
		    AssetDatabase.ImportAsset(assetPath);
		    texture= (Texture2D) AssetDatabase.LoadAssetAtPath(assetPath, typeof (Texture2D));
		}
		return texture;
	}

	//here we just generate Texture2d
	 static Texture2D createCheckerBoard(int width, int height, Color bgColor1,Color bgColor2, int squareWidth){
	
		
		Texture2D texture=new Texture2D(width,height);
		
			
		Color[] pixels=new Color[width*height];
		Color rowFirstColor=bgColor1;
		
		Color currentColor=bgColor1;
		for (int textHeight = 0; textHeight < height; textHeight+=squareWidth) {
			rowFirstColor=(rowFirstColor==bgColor1)?bgColor2:bgColor1;
			currentColor=rowFirstColor;
			for (int textWidth = 0; textWidth < width; textWidth+=squareWidth) {			
				int initPosition=textWidth+(textHeight*width);
				
				// paint pixels on this and nex squareWidth row
				for (int cubeWidth = 0; cubeWidth < squareWidth; cubeWidth++) {
					for (int cubeHeight = 0; cubeHeight < squareWidth; cubeHeight++) {						
						pixels[initPosition+cubeWidth+(width*cubeHeight)]=currentColor;	
					}					
				}
				currentColor=(currentColor==bgColor1)?bgColor2:bgColor1;
			}
		}
		texture.SetPixels(pixels);	
		texture.Apply();
		
		return texture;
	}
	
	
	
	//remove odd alpha stripes on texture borders
	public static Texture2D trimTextureAlpha(Texture2D texture){		
		int width=texture.width;
		int height=texture.height;
		int minU=width;
		int maxU=0;
		int minV=height;
		int maxV=0;
		Color32[] originalColor=texture.GetPixels32();
		
		

		bool previousRowWasEmpty=true;
		for (int v = 0; v < height; v++) {
			bool previousColumnWasEmpty=true;

			bool isRowEmpty=true;
			int maxRightU=0;
			for (int u = 0; u < width; u++) {
				float alpha=originalColor[(v*width) +u].a;				
				if (alpha!=0){
					if (u<minU)
						minU=u;			
					
					if (!previousColumnWasEmpty){
						maxRightU=u;
					}
					previousColumnWasEmpty=false;
					isRowEmpty=false;					
				} else {
					previousColumnWasEmpty=true;
				}
			}
			
			if (!isRowEmpty && maxRightU>maxU){				
				maxU=maxRightU;			
			}
			
			
			if (!isRowEmpty){
				if (v<minV)
					minV=v;
				if (!previousRowWasEmpty){
					maxV=v;
				}				
				previousRowWasEmpty=false;
			}else{
				previousRowWasEmpty=true;
			}
			
			
		}
		
		int i=0;
		int newWidth=maxU-minU+1;
		int newHeight=maxV-minV+1;
		if (newWidth!=width || newHeight!=height){
			
			int newColorSize=(newWidth*newHeight);			
			Color32[] newColor=new Color32[newColorSize];
			if(newColor.Length==0)
				throw new System.Exception("trimmed texture has a zerro size");
			for (int v = minV; v <= maxV; v++) {
				for (int u = minU; u <= maxU; u++) {
					newColor[i]=originalColor[v*width+u];					
					i++;
				}
			}
			Texture2D result=new Texture2D(newWidth,newHeight);
			
			result.SetPixels32(newColor);		
			result.Apply();
			return result;
		} 
		return texture;
	}
	
}
#endif