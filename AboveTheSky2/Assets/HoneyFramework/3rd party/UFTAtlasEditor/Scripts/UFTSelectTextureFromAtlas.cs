using UnityEngine;
using System.Collections;


// In this script we will store original uv's to mesh.uv2
// and then we will apply texture from attlas, we will multiply original uv2 to atlas position

public class UFTSelectTextureFromAtlas : MonoBehaviour {	
	public int textureIndex;	
	public UFTAtlasMetadata atlasMetadata;
	
	public void Reset(){
		storeOriginalUV();
	}
	
	/// <summary>
	/// In this function we will check if we have uv2 coordinates we will copy uv2 to uv1 (restore original mesh.uv)
	/// if uv2==null then we will store original uv to it
	/// </summary>
	public void OnEnable(){		
		storeOriginalUV ();		
	}

	public void storeOriginalUV ()
	{
		Mesh mesh=UFTMeshUtil.getObjectMesh(gameObject);		
		if (isUV2Empty()){
			mesh.uv2=(Vector2[]) mesh.uv.Clone();		
		} else {
			mesh.uv=(Vector2[]) mesh.uv2.Clone();	
		}
	}	
	
	/// <summary>
	/// Updates mesh uv, we will take original mesh.uv from uv2 coordinates and then multiply to atlas position
	/// </summary>		
	public void updateUV(){
		Rect rect=atlasMetadata.entries[textureIndex].uvRect;
		Mesh mesh=UFTMeshUtil.getObjectMesh(gameObject);
		Vector2[] uvs=new Vector2[mesh.uv2.Length];
		for (int i=0; i<uvs.Length; i++){
			uvs[i].x = mesh.uv2[i].x * rect.width + rect.x;
			uvs[i].y = mesh.uv2[i].y * rect.height + rect.y;			
		}
		mesh.uv=uvs;		
	}	

	public bool isUV2Empty ()
	{
		Mesh mesh=UFTMeshUtil.getObjectMesh(gameObject);
		return mesh.uv2.Length==0 || mesh.uv2==null;
	}
	
	
	
	
	public void restoreOriginalUVS(){
		Mesh mesh=UFTMeshUtil.getObjectMesh(gameObject);
		if (isUV2Empty ())
			throw new System.Exception("mesh.uv2 of your object is wrong, can't restore original values");
		
		mesh.uv=(Vector2[]) mesh.uv2.Clone();
		
	}
}
