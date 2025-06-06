using UnityEngine;
using System.Collections;

public class NoDepthWaterLevel : MonoBehaviour
{
#if !UNITY_EDITOR_WIN && !UNITY_STANDALONE_WIN
	// Use this for initialization
	void Start () {
        transform.localPosition = new Vector3(0f, -0.031f, 0f);
	}	
#endif
}
