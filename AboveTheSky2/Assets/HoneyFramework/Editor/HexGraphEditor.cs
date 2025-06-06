#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_4_8 || UNITY_4_9
#define UNITY_4
#endif

#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_3_5 || UNITY_3_4 || UNITY_3_3
#define UNITY_LE_4_3
#endif

using UnityEngine;
using UnityEditor;
using System.Collections;
using Pathfinding;
using HoneyFramework;

/*
 * Editor for pathfinder modified for hexagonal graph 
 * 
 */
[CustomGraphEditor(typeof(HexGraph), "HexGraph")]
public class HexGraphEditor : GraphEditor
{

    public override void OnInspectorGUI(NavGraph target)
    {
        PointGraph graph = target as PointGraph;

        graph.root = ObjectField(new GUIContent("Root", "All children of this object will be used as nodes, if it is not set, a tag search will be used instead (see below)"), graph.root, typeof(Transform), true) as Transform;

        graph.recursive = EditorGUILayout.Toggle(new GUIContent("Recursive", "Should children of the children in the root GameObject be searched"), graph.recursive);
        graph.searchTag = EditorGUILayout.TagField(new GUIContent("Tag", "If root is not set, all objects with this tag will be used as nodes"), graph.searchTag);

        if (graph.root != null)
        {
            EditorGUILayout.HelpBox("All children " + (graph.recursive ? "and sub-children " : "") + "of 'root' will be used as nodes\nSet root to null to use a tag search instead", MessageType.None);
        }
        else
        {
            EditorGUILayout.HelpBox("All object with the tag '" + graph.searchTag + "' will be used as nodes" + (graph.searchTag == "Untagged" ? "\nNote: the tag 'Untagged' cannot be used" : ""), MessageType.None);
        }

        graph.maxDistance = EditorGUILayout.FloatField(new GUIContent("Max Distance", "The max distance in world space for a connection to be valid. A zero counts as infinity"), graph.maxDistance);

        graph.limits = EditorGUILayout.Vector3Field("Max Distance (axis aligned)", graph.limits);
        graph.raycast = EditorGUILayout.Toggle(new GUIContent("Raycast", "Use raycasting to check if connections are valid between each pair of nodes"), graph.raycast);
        
        if (graph.raycast)
        {
            EditorGUI.indentLevel++;

            graph.thickRaycast = EditorGUILayout.Toggle(new GUIContent("Thick Raycast", "A thick raycast checks along a thick line with radius instead of just along a line"), graph.thickRaycast);
            
            if (graph.thickRaycast)
            {
                graph.thickRaycastRadius = EditorGUILayout.FloatField(new GUIContent("Raycast Radius", "The radius in world units for the thick raycast"), graph.thickRaycastRadius);
            }
            
            graph.mask = EditorGUILayoutx.LayerMaskField(/*new GUIContent (*/"Mask"/*,"Used to mask which layers should be checked")*/, graph.mask);
            EditorGUI.indentLevel--;
        }       
    }

    public void DrawChildren(PointGraph graph, Transform tr)
    {
        foreach (Transform child in tr)
        {
            Gizmos.DrawCube(child.position, Vector3.one * HandleUtility.GetHandleSize(child.position) * 0.1F);
            //Handles.CubeCap (-1,graph.nodes[i].position,Quaternion.identity,HandleUtility.GetHandleSize(graph.nodes[i].position)*0.1F);
            //Gizmos.DrawCube (nodes[i].position,Vector3.one);
            if (graph.recursive) DrawChildren(graph, child);
        }
    }
}
