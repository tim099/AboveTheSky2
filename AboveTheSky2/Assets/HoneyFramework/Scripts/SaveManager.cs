using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HoneyFramework
{
    [Serializable()]
    public class SaveStructure
    {
        public int worldHexRadius;
        public List<Chunk> chunks;
        public List<Hex> hexes;
        public List<RiverData> riverData;
    }

    /*
     * Class used to save / load map data from/to drive
     */
    public class SaveManager
    {
        static string saveName = Application.persistentDataPath + "HoneySave";

        /// <summary>
        /// Saves current state of the world
        /// </summary>
        /// <param name="w"></param>
        /// <param name="withTextures"></param>
        /// <returns></returns>
        static public void Save(World w, bool withTextures)
        {
            SaveStructure save = new SaveStructure();
            save.worldHexRadius = w.hexRadius;
            save.riverData = w.riversStart;

            save.hexes = new List<Hex>();
            foreach(KeyValuePair<Vector3i, Hex> pair in w.hexes)
            {
                save.hexes.Add(pair.Value);
            }

            if (withTextures)
            {

            }

            Stream stream = File.Open(saveName, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, save);
            stream.Close();            
        }

        /// <summary>
        /// Load world from save file
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        static public bool Load(World w)
        {
            SaveStructure save;
			try
			{
	            Stream stream = File.Open(saveName, FileMode.Open);
	            BinaryFormatter bFormatter = new BinaryFormatter();
	            save = (SaveStructure)bFormatter.Deserialize(stream);
	            stream.Close();

	            if (save == null) return false;

	            w.hexRadius = save.worldHexRadius;
	            w.riversStart = save.riverData;

	            foreach (Hex hex in save.hexes)
	            {
	                w.hexes[hex.position] = hex;
	            }

	            if (save.chunks != null)
	            {

	            }

	            return true;
			}
			catch 
			{
				return false;
			}
        }


        /*static private Savemanager instance;

        static public Savemanager GetInstance()
        {
            if (instance == null)
            {
                instance = new Savemanager();
            }

            return instance;
        }*/


    }
}