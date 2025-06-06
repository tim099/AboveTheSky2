using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Reflection;
using UnityEngine;

namespace HoneyFramework
{
    /*
     *  Abstract base type used to construct other complex data (eg terrain data)
     */
    public abstract class MHType
    {
        [XmlAttribute]
        public int OID { get; set; }
        [XmlAttribute]
        public string name = "";

        public string GetName() { return name; }

        public override string ToString()
        {
            return name;
        }

        public int CompareTo(MHType instance)
        {
            // A null value means that this object is greater. 
            if (instance == null)
            {
                return 1;
            }
            else
            {
                return this.name.CompareTo(instance.name);
            }
        }

        /// <summary>
        /// Finds next available index for this list
        /// </summary>
        /// <returns></returns>
        public void CreateDBIndex()
        {
            int index = 0;

            Type t = GetType();
            FieldInfo fi = t.GetField("list", BindingFlags.Public | BindingFlags.Static);
            if (fi != null)
            {
                object obj = fi.GetValue(null);                
                //Indexing start at 1; 0 is default int value which may be indicator of OID override.
                index = GetTopIndex(obj as IEnumerable) + 1;                
            }

            OID = index;
        }
        
        /// <summary>
        /// Finds currently highest index in the list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private int GetTopIndex(IEnumerable list) 
        {
            if (list == null)
            {
                Debug.LogWarning("list index not found! Only new type should get this message!");
                return 0;
            }

            int index = 0;
            foreach (object l in list)
            {
                MHType tag = l as MHType;
                if (tag != null && tag.OID > index)
                {
                    index = tag.OID;
                }
            }

            return index;
        }

    }     
}
