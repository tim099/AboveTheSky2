using System;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace HoneyFramework
{
    /*
     * Example game manager class which controls world seed and pathfinder resources.
     */
    public class GameManager : MonoBehaviour
    {
        static public GameManager instance;
        public int currentSeed = 0;
        bool initializedSeed = false;

        public List<Formation> formations = new List<Formation>();
        AstarPath pathFinder;

        /// <summary>
        /// Prepares pathfinder component 
        /// </summary>
        /// <returns></returns>
        void Awake()
        {
            instance = this;
            pathFinder = gameObject.AddComponent<AstarPath>();
            AstarPath.active = pathFinder;
        }

        /// <summary>
        /// Prepares some basic gameplay resources (used mostly for demo/debug) 
        /// and sets seed from game settings or if they are 0 there then from scene
        /// if its 0 there a well it will leave seed randomized
        /// </summary>
        /// <returns></returns>
        void Start()
        {
            if (!initializedSeed)
            {
                DataManager.GetInstance();
                int settingsSeed = MHGameSettings.GetWorldSeed();
                if (settingsSeed != 0)
                {
                    UnityEngine.Random.seed = settingsSeed;
                }
                else if (World.GetInstance().seed != 0)
                {
                    UnityEngine.Random.seed = World.GetInstance().seed;
                }

                currentSeed = UnityEngine.Random.seed;
                initializedSeed = true;
            }            
        }

        /// <summary>
        /// Returns instance of the pathfinder
        /// </summary>
        /// <returns></returns>
        static public AstarPath GetPathfinder()
        {
            return instance.pathFinder;
        }

        /// <summary>
        /// Initializes pathfinder and its resources on current hex world
        /// </summary>
        /// <returns></returns>
        public void ActivatePathfinder()
        {
			pathFinder.data.AddGraph(typeof(HexGraph));
			pathFinder.Scan();
        }
    }
}

