using UnityEngine;
using System.Collections;

public class UFTMeshUtil : MonoBehaviour {

	public static GameObject createPlane(float width, float height){
		GameObject go=new GameObject();		
		go.AddComponent(typeof(MeshRenderer));
		MeshFilter meshFilter=(MeshFilter)go.AddComponent(typeof(MeshFilter));
		
		Mesh mesh=new Mesh();
		Vector3 point=Vector3.zero;
		
		Vector3[] vertices=new Vector3[6];
		int[] triangles=new int[6]{0,1,2,3,5,4};
		Vector2[] uvs=new Vector2[6]{
			new Vector2(0,0),
			new Vector2(0,1),
			new Vector2(1,0),
			new Vector2(1,0),
			new Vector2(1,1),
			new Vector2(0,1)			
		};
		point.x-=width/2;
		point.y-=height/2;
		vertices[0]=point;
		point.y+=height;
		vertices[1]=point;
		point.x+=width;
		point.y-=height;
		vertices[2]=point;
		vertices[3]=point;
		point.y+=height;
		vertices[4]=point;
		point.x-=width;
		vertices[5]=point;
		
		
		mesh.vertices = vertices;
    	mesh.uv = uvs;
		mesh.triangles = triangles;
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		meshFilter.sharedMesh=mesh;
		return go;
	}
	
	
	public static Mesh getObjectMesh (GameObject gameObject)
	{
		MeshFilter mf= gameObject.GetComponent<MeshFilter>();
		Mesh mesh;
		if (Application.isEditor){
			mesh=mf.sharedMesh;
		} else {
			mesh=mf.mesh;	
		}
		return mesh;
	}
}
