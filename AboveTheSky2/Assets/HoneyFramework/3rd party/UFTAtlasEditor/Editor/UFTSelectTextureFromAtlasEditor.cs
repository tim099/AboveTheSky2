using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(UFTSelectTextureFromAtlas))]
public class UFTSelectTextureFromAtlasEditor : Editor {
	SerializedProperty textureIndex;
	SerializedProperty atlasMetadata;
	
	private bool debug=false;
	
	void OnEnable () {
        // Setup the SerializedProperties
        textureIndex = serializedObject.FindProperty ("textureIndex");  
		atlasMetadata= serializedObject.FindProperty("atlasMetadata");
    }
	
	public override void OnInspectorGUI(){
		
		UFTSelectTextureFromAtlas script=((UFTSelectTextureFromAtlas) target);
		if (script.atlasMetadata!=null){
			
			EditorGUILayout.IntSlider(textureIndex,0,script.atlasMetadata.entries.Length-1);		
		}
		
		EditorGUILayout.PropertyField(atlasMetadata);
		serializedObject.ApplyModifiedProperties ();
		
		if (GUI.changed){
			((UFTSelectTextureFromAtlas)target).updateUV();			
		}
				
		if (!script.isUV2Empty()){			
			if (GUILayout.Button("restore original uv")){
				script.restoreOriginalUVS();
			}
		}
		
		//debug
		debug=EditorGUILayout.Toggle("UV Debug mode" ,debug);
		
		if (debug){
			EditorGUILayout.LabelField("==========================================");
			EditorGUILayout.LabelField("uv (actual uv):");
			Vector2[] uv=UFTMeshUtil.getObjectMesh(script.gameObject).uv;
			foreach(Vector2 v in uv){
				EditorGUILayout.Vector2Field("",v);	
			}
			
			EditorGUILayout.LabelField("==========================================");
			EditorGUILayout.LabelField("uv2 (original uv):");
			Vector2[] uv2=UFTMeshUtil.getObjectMesh(script.gameObject).uv2;
			foreach(Vector2 v in uv2){
				EditorGUILayout.Vector2Field("",v);	
			}			
		}
    }
	
}
