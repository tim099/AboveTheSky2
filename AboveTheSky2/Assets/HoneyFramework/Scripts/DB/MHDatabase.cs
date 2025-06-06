using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using UnityEngine;

namespace HoneyFramework
{

    public class MHDatabase
    {
        /// <summary>
        /// saves proper database file based on example instance provided. Used when Save is located in generic place shared by multiple valid types. 
        /// Example instance is usually instance which were modified
        /// </summary>
        /// <param name="exampleInstance"></param>
        /// <returns></returns>
        static public void SaveDB(MHType exampleInstance)
        {
            Type t = exampleInstance.GetType();
            FieldInfo fi = t.GetField("list", BindingFlags.Public | BindingFlags.Static);
            if (fi != null)
            {
                object obj = fi.GetValue(null);
                Type listType = obj.GetType();
                // listType list = obj as listType;

                if (obj != null)
                {
                    XmlSerializer serializer = new XmlSerializer(listType);

                    string path = Application.streamingAssetsPath;
                    using (TextWriter writer = new StreamWriter(path + "/" + t.ToString() + @".xml"))
                    {
                        serializer.Serialize(writer, obj);
                    }
                }
            }
        }

        /// <summary>
        /// Saves database based on type provided
        /// </summary>
        /// <returns></returns>
        static public void SaveDB<T>() where T : MHType
        {
            FieldInfo fi = typeof(T).GetField("list", BindingFlags.Public | BindingFlags.Static);
            if (fi != null)
            {
                object obj = fi.GetValue(null);

                List<T> list = obj as List<T>;

                if (list != null)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

                    string path = Application.streamingAssetsPath;
                    using (TextWriter writer = new StreamWriter(path + "/" + typeof(T).ToString() + @".xml"))
                    {
                        serializer.Serialize(writer, list);
                    }
                }
            }
        }

        /// <summary>
        /// Loads database type list based on type provided
        /// </summary>
        /// <returns></returns>
        static public void LoadDB<T>() where T : MHType
        {
            try
            {
                //Try to load it from streaming assets
                Load<T>();
            }
            catch
            {
                try
                {
                    //Not in streaming assets so try from resources
                    LoadFromResources<T>();
                }
                catch
                {
                    //not in resources neither in which case create empty list of this type and save it to file
                    try
                    {
                        string path = Application.streamingAssetsPath;

                        bool isExists = System.IO.Directory.Exists(path);

                        if (!isExists)
                        {
                            System.IO.Directory.CreateDirectory(path);
                        }

                        using (TextWriter writer = new StreamWriter(path + "/" + typeof(T).ToString() + @".xml"))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                            serializer.Serialize(writer, new List<T>());
                        }
                    }
                    finally
                    {
                        //try to load it again to get final result of this operation
                        Load<T>();
                    }
                }
            }
        }

        static private void LoadFromResources<T>()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<T>));
            string path = typeof(T).ToString();

            TextAsset asset = Resources.Load<TextAsset>(path);

            using (TextReader reader = new StreamReader(new MemoryStream(asset.bytes)))
            {
                object obj = deserializer.Deserialize(reader);
                List<T> XmlData = (List<T>)obj;

                FieldInfo fi = typeof(T).GetField("list", BindingFlags.Public | BindingFlags.Static);
                if (fi != null)
                {
                    fi.SetValue(null, XmlData);
                }
            }
        }

        /// <summary>
        /// Do the heavy work for the loading for defined type
        /// </summary>
        /// <returns></returns>
        static private void Load<T>()
        {            
            XmlSerializer deserializer = new XmlSerializer(typeof(List<T>));
            string path = Application.streamingAssetsPath;
            path += "/" + typeof(T).ToString() + @".xml";
            
            using (TextReader reader = new StreamReader(new MemoryStream(LoadBytes(path))))
            {

                object obj = deserializer.Deserialize(reader);
                List<T> XmlData = (List<T>)obj;

                FieldInfo fi = typeof(T).GetField("list", BindingFlags.Public | BindingFlags.Static);
                if (fi != null)
                {
                    fi.SetValue(null, XmlData);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static private byte[] LoadBytes(string path)
        {
            if(Application.platform == RuntimePlatform.Android || path.Contains("://"))
            {             
                WWW file = new WWW(path);
                while (!file.isDone) 
                { }
                return file.bytes;
            }
            else
            {                
                return System.IO.File.ReadAllBytes(path);
            }                
        }
    }
}

