using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace HoneyFramework
{
    /*
     * single foreground data used for foreground generation. 
     * Note that single foreground data always belong only to single chunk and to single hex, even if hex contains multiple foreground data shared with possibly multiple chunks
     */
    [Serializable()]
    public class ForegroundData
    {
        public float scale;
        public Vector3Serializable position;
        public Vector4Serializable color;
        public Vector4Serializable colorFinal;
        public string name;

        public bool horizontalInverse = false;
    }
}
