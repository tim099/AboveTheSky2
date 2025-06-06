using UnityEngine;
using System.Collections;
using UnityEditor;

public class UFTCreatePlane : ScriptableWizard {
	public string meshName="plane";
	public int width=256;
	public int height=128;
	public Material material;
	public UFTAtlasMetadata atlasMetadata;
	
	
	[MenuItem ("GameObject/UFT Create Plane")]
    static void CreateWizard () {
        ScriptableWizard.DisplayWizard<UFTCreatePlane>("UFT Create Plane", "Create");        
    }
	
	 void OnWizardCreate () {
        GameObject go = UFTMeshUtil.createPlane(width,height);
		go.AddComponent<UFTSelectTextureFromAtlas>().atlasMetadata=atlasMetadata;
		go.GetComponent<Renderer>().material=material;
		
		AssetDatabase.CreateAsset(go.GetComponent<MeshFilter>().sharedMesh, AssetDatabase.GenerateUniqueAssetPath("Assets/"+meshName+".asset") );
        AssetDatabase.SaveAssets();
		
    } 
}
