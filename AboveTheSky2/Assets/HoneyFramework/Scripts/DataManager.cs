using System;
using System.Collections.Generic;
using UnityEngine;

namespace HoneyFramework
{
    /*
     * Class which ensures all data tables are loaded. Allows to reload them as well in case any change is done. 
     */
    public class DataManager
    {
        static public bool isInitialized = false;
        static public DataManager instance;

        protected DataManager() { }

        /// <summary>
        /// Returns instance of the class. Creates new one if non exists
        /// </summary>
        /// <returns></returns>
        static public DataManager GetInstance()
        {
            if (instance == null)
            {
                instance = new DataManager();
                instance.LoadResources();
            }

            return instance;
        }

        /// <summary>
        /// Functionality for reloading resources. 
        /// Note! When instance is is first created it will load resources itself to ensure that anything accessing data manager 
        /// </summary>
        /// <returns></returns>
        static public void Reload()
        {
            if (isInitialized || instance != null)
            {
                GetInstance().LoadResources();
            }
            else
            {
                GetInstance();
            }
        }

        /// <summary>
        /// Loads all data resources.
        /// </summary>
        /// <returns></returns>
        protected virtual void LoadResources()
        {
            //first we have to load settings to ensure we work existing data
            MHDatabase.LoadDB<MHGameSettings>();
            MHDatabase.LoadDB<MHTerrain>();

            isInitialized = true;
        }
    }

}