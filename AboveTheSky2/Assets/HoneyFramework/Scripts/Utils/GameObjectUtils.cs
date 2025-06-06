using UnityEngine;
using System.Collections;

namespace HoneyFramework
{
    /*
     *  Helper class supporting searches of game objects
     */
    public class GameObjectUtils
    {

        /// <summary>
        /// Finds gameobject on scene by name. It doesn't matter where it is located although it will return only first matching
        /// Note! this function uses reasonably heavy unity features and should not be called extensively in loops!
        /// </summary>
        /// <param name="name"> name to search for </param>
        /// <returns></returns>
        static public GameObject FindByName(string name)
        {
            Object[] objects = GameObject.FindObjectsOfType(typeof(GameObject));
            foreach (Object o in objects)
            {
                if (o is GameObject && o.name == name)
                {
                    return o as GameObject;
                }
            }

            return null;
        }

        /// <summary>
        /// Cheaper function to find gameobject by name, but requires root to start from.
        /// It will find disabled objects as well
        /// </summary>
        /// <param name="root"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public GameObject FindByName(GameObject root, string name)
        {
            Transform[] children = root.GetComponentsInChildren<Transform>(true);
            foreach (Transform o in children)
            {
                if (o is Transform && o.name == name)
                {
                    return o.gameObject;
                }
            }

            return null;
        }

    }
}