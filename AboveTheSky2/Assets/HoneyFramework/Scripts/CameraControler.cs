using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;

namespace HoneyFramework
{
    /*
     * Debug functionality which helps to make first steps in hexagonal world
     */
    public class CameraControler : MonoBehaviour
    {
        public float speed = 1;
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        void Start()
        {
            GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
                return;
            }

            if (World.instance == null) return;

            Vector3 v = transform.position;

            UpdateTeselationLevel(v.y);

            // camera up or down
            if (Input.GetKey("z") && v.y < 25.0f)
            {
                v.y += 0.1f;
                UpdateTeselationLevel(v.y);
            }
            else if (Input.GetKey("x") && v.y > 5.0f)
            {
                v.y -= 0.1f;
                UpdateTeselationLevel(v.y);
            }

            float vertical = Input.GetAxis("Vertical") * speed;
            float horizontal = Input.GetAxis("Horizontal") * speed;

            if (Input.touchCount > 0)
            {
                Vector2 move = Input.GetTouch(0).deltaPosition;
                vertical += -move.y;
                horizontal += -move.x;
            }
            vertical *= Time.deltaTime;
            horizontal *= Time.deltaTime;

            v.x += horizontal;
            v.z += vertical;

            transform.position = v;


            //part which allows to find "click" position in world
            if (World.GetInstance() != null && Input.GetMouseButtonDown(0) && World.GetInstance().status == World.Status.Ready)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                float ent = 100.0f;
                if (plane.Raycast(ray, out ent))
                {
                    Vector3 hitPoint = ray.GetPoint(ent);
                    hitPoint -= World.instance.transform.position;

                    Vector2 hexWorldPosition = VectorUtils.Vector3To2D(hitPoint);
                    Vector3i hexPos = HexCoordinates.GetHexCoordAt(hexWorldPosition);
                    Debug.Log("Selected hex" + hexPos);                    

                    if (GameManager.instance.formations.Count > 0)
                    {
                        GameManager.instance.formations[GameManager.instance.formations.Count - 1].GoTo(hexPos);
                    }

                    //EXAMPLE1: Color radius or trees
                    //World.PaintTrees(new Vector2(hitPoint.x, hitPoint.z), 3f, new Color(1f, 1f, 0f));

                    //EXAMPLE2: Point to marker example
                    //HexMarkers.SetMarkerType(hexPos, 11, HexMarkers.Layer.Borders, HexMarkers.directionZeroOneScale[Random.Range(0,6)]);

                    //EXAMPLE3: Click to rerandomise hex and rebake chunks linked to it
//                     if (World.GetInstance().hexes.ContainsKey(hexPos))
//                     {
//                         List<TerrainDefinition> tdList = TerrainDefinition.definitions.FindAll(o => o.source.mode == MHTerrain.Mode.normal);
//                         Hex h = World.GetHexDefinition(World.GeneratorMode.Random, hexPos, tdList); ;
//                         World.GetInstance().hexes[hexPos] = h;
//                         h.RebuildChunksOwningThisHex();
//                     }

                    //EXAMPLE4: Click to switch road status on pointed hex
                    Roads.SwitchRoadAt(hexPos);
                }
                else
                {
                    Debug.LogWarning("click outside world? e.g. horizontal");
                }
            }
        }

        void OnGUI()
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label("", GUILayout.Width(Screen.width - 150));

            GUILayout.BeginVertical();
            GUILayout.Label("Status: " + World.GetInstance().status.ToString());
            GUILayout.Label("DX Mode: " + (MHGameSettings.GetDx11Mode() ? "DX11" : "Non DX11"));

            //feedback for world status
            if (World.GetInstance().status == World.Status.NotReady)
            {
                if (GUILayout.Button("Generate World"))
                {
                    DataManager.Reload();
                    World.GetInstance().Initialize();
                    GameManager.instance.ActivatePathfinder();
                }

                if (GUILayout.Button("Load map"))
                {
                    DataManager.Reload();
                    World.GetInstance().InitializeFromSave();
                    GameManager.instance.ActivatePathfinder();
                }
            }
            else if (World.GetInstance().status == World.Status.Ready)
            {
                if (GUILayout.Button("Generate World"))
                {
                    DataManager.Reload();
                    World.GetInstance().Initialize();
                    GameManager.instance.ActivatePathfinder();
                }

                if (GUILayout.Button("Load map"))
                {
                    DataManager.Reload();
                    World.GetInstance().InitializeFromSave();
                    GameManager.instance.ActivatePathfinder();
                }

                if (GUILayout.Button("Spawn character"))
                {
                    Formation f = Formation.CreateFormation("Caps", Vector3i.zero);
                    for (int i = 0; i < 10; i++)
                    {
                        f.AddCharacter(CharacterActor.CreateCharacter("Characters/CapCharacter", 0.3f));
                    }
                    GameManager.instance.formations.Add(f);
                }

                if (GUILayout.Button("Save map"))
                {
                    SaveManager.Save(World.GetInstance(), false);
                }   
             
                if (GUILayout.Button("Mark River Neighbours"))
                {
                    HexMarkers.ClearAllMarkers();
                    foreach(KeyValuePair<Vector3i, Hex> pair in World.GetInstance().hexes)
                    {
                        if (pair.Value.directionsPassingRiver.Count > 0)
                        {
                            HexMarkers.SetMarkerType(pair.Key, HexMarkers.MarkerType.Friendly);
                        }
                    }                                                
                }

                if (GUILayout.Button("Random FoW mode"))
                {
                    int randomMode = Random.Range(0, 3);
                    foreach(KeyValuePair<Vector3i, Hex> pair in World.GetInstance().hexes)
                    {
                        if (randomMode == 0)
                        {
                            //clear
                            pair.Value.SetVisibility(Hex.Visibility.FullyVisible);
                        }
                        else if (randomMode == 1)
                        {
                            //random
                            float r = Random.Range(0f, 1f);
                            if (r < 0.33f)
                            {
                                pair.Value.SetVisibility(Hex.Visibility.FullyVisible);
                            }
                            else if (r < 0.67f)
                            {
                                pair.Value.SetVisibility(Hex.Visibility.Shadowed);
                            }
                            else
                            {
                                pair.Value.SetVisibility(Hex.Visibility.NotVisible);
                            }
                        }
                        else if (randomMode == 2)
                        {
                            // center

                            int dist = HexCoordinates.HexDistance( pair.Value.position, Vector3i.zero);

                            if (dist < 2)
                            {
                                pair.Value.SetVisibility(Hex.Visibility.FullyVisible);
                            }
                            else if (dist < 3)
                            {
                                pair.Value.SetVisibility(Hex.Visibility.Shadowed);
                            }
                            else
                            {
                                pair.Value.SetVisibility(Hex.Visibility.NotVisible);
                            }
                        }
                    }
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// allows to control quality level (tessellation is great but can get easily overkill if camera lift up)
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        void UpdateTeselationLevel(float height)
        {
            int tesselationLevel = (int)Mathf.Max(3.1f, Mathf.Min(16.0f, 25 - height));
            foreach (KeyValuePair<Vector2i, Chunk> pair in World.GetInstance().chunks)
            {
                if (pair.Value.chunkObject != null)
                {
                    MeshRenderer mr = pair.Value.chunkObject.GetComponent<MeshRenderer>();
                    mr.material.SetInt("_Tess", tesselationLevel);
                }
            }
        }

    }
}