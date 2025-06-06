using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

namespace HoneyFramework
{
    /*
     *  Formation class to manage group of the character actors within single hex and their movement
     */
    public class Formation : MonoBehaviour
    {
        static public List<Formation> formations = new List<Formation>();

        public Vector3i position;
        public float formationSpeed = 0.5f;
        List<CharacterActor> characters = new List<CharacterActor>();
        public Vector3 direction = Vector3.back;
        public float formationLazines = 1.0f;

        #region FormationManagement
        /// <summary>
        /// Creates formation at specified position (hex space coordinate)
        /// </summary>
        /// <param name="name"> name for formation </param>
        /// <param name="position"> hex position of the formation </param>
        /// <returns> returns instance of the formation </returns>
        static public Formation CreateFormation(string name, Vector3i position)
        {
            GameObject go = new GameObject(name);
            go.transform.parent = World.instance.transform;
            go.transform.localPosition = Vector3.zero;
            Formation f = go.AddComponent<Formation>();
            formations.Add(f);
            f.position = position;

            return f;
        }


        /// <summary>
        /// Destroy single or more formations where you may specify either of name and position (or both), each formation passing requirements would be destroyed.
        /// If no requirements specified ALL formations would be cleared
        /// </summary>
        /// <param name="name">option to provide name of the formations considered for destruction</param>
        /// <param name="position">option to provide position of formations considered for destruction</param>
        /// <returns></returns>
        static public void DestroyFormations(string name, Vector3i? position)
        {

            List<Formation> formationGroup = formations;

            if (position != null)
            {
                formationGroup = formationGroup.FindAll(o => o.position == position);
            }

            if (name.Length > 0)
            {
                string n = name.ToString();
                formationGroup = formationGroup.FindAll(o => o.gameObject.name == n);
            }

            foreach (Formation f in formationGroup)
            {
                formations.Remove(f);
                Destroy(f);
            }
        }
        #endregion

        /// <summary>
        /// adds single character to the formation
        /// </summary>
        /// <param name="a">character instance to join formation</param>
        /// <returns></returns>
        public void AddCharacter(CharacterActor a)
        {
            Vector3 offset = GetFormationOffset(characters.Count, direction);
            a.transform.parent = transform;
            
            a.SetWorldPosition(HexCoordinates.HexToWorld3D(position) + offset);

            characters.Add(a);
            a.parent = this;
        }
       
        /// <summary>
        /// Finds position within formation for the character (an offset from hex center)
        /// </summary>
        /// <param name="index"> index within formation </param>
        /// <param name="dir"> direction at which formation looks at </param>
        /// <returns></returns>
        public Vector3 GetFormationOffset(int index, Vector3 dir)
        {
            Vector3 right = Vector3.right * 0.3f;
            Vector3 left = new Vector3(-right.x, 0, right.z);
            Vector3 back = Vector3.back * 0.3f;
            int perRowCount = 5;

            int row = (int)(index / perRowCount);
            index -= row * perRowCount;

            int k = index % 2;
            int p = (int)(index / 2);

            p += k;
            Vector3 offset = p * (k > 0 ? right : left);
            offset += row * back;

            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, dir);
            return rot * offset;
        }

        #region Movement
        /// <summary>
        /// Requests movement of the formation to the specified position
        /// </summary>
        /// <param name="position"> position we want animate to </param>
        /// <returns></returns>
        public void GoTo(Vector3i position)
        {

            //vector3 position is hex vector3i position transformed to world 3d position
            Vector3 startPosition = (Vector3)this.position;
            Vector3 endPosition = (Vector3)position;

            var pathDelegates = new OnPathDelegate(path =>
            {
                if (path.error) { OnPathFailed(path); }
                else { OnPathSuccess(path); }
            });

            Path pathDesign = ABPath.Construct(startPosition, endPosition, pathDelegates);
            AstarPath.StartPath(pathDesign);
        }

        /// <summary>
        /// Event in case path have failed to be found!
        /// </summary>
        /// <param name="p">A* path data to be processed</param>
        /// <returns></returns>
        void OnPathFailed(Path p)
        {
            Debug.Log("Pathfinding failed");
        }

        /// <summary>
        /// Event when path have been found and can be processed and distributed to actors
        /// </summary>
        /// <param name="p">A* path data to be processed</param>
        /// <returns></returns>
        void OnPathSuccess(Path p)
        {
            if (p.path.Count < 2) return;

            // convert path to hex space
            List<Vector3> path = new List<Vector3>();
            List<Vector3> pathDirections = new List<Vector3>();
            for (int i = 0; i < p.path.Count; i++)
            {
                path.Add(HexCoordinates.HexToWorld3D((Vector3)p.path[i].position));
            }

            //note final path position. We will consider formation position to be at the last point of the path from now on.
            position = new Vector3i(p.path[p.path.Count - 1].position);

            //produce directions formation should look at for each step in path
            for (int i = 0; i < path.Count; i++)
            {
                if (i == path.Count - 1)
                {
                    pathDirections.Add(path[path.Count - 1] - path[path.Count - 2]);
                }
                else
                {
                    pathDirections.Add(path[i + 1] - path[i]);
                }
            }

            //store final direction of the formation to which it look at from now on (but it will be applied only after animations are done)
            direction = pathDirections[pathDirections.Count - 1];

            //produces unique path for each actor taking into account their position in formation at each step
            for (int i = 0; i < characters.Count; i++)
            {
                List<Vector3> personalizedPath = new List<Vector3>();
                for (int j = 0; j < path.Count; j++)
                {
                    Vector3 offset = GetFormationOffset(i, pathDirections[j]);
                    personalizedPath.Add(path[j] + offset);
                }

                characters[i].AnimateViaPath(personalizedPath, Random.Range(0f, formationLazines));
            }
        }
        #endregion
    }
}