using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml.Serialization;
using UnityEngine;

namespace HoneyFramework
{
    /*
     *  Small class used for serializing link of string and count for it
     */
    public class MHSimpleCounter
    {        
        [XmlAttribute]
        public string name;

        [XmlAttribute]
        public int count;
    
        public MHSimpleCounter() { }        
     
    }
}
