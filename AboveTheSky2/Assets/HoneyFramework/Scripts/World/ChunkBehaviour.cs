using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;
using System.Collections;

namespace HoneyFramework
{
    /*
     * Script which is attached to chunk object and allows to add events and local functionality (especially that hexes itself are virtual/drawn but without physical representation)
     */
    public class ChunkBehaviour : MonoBehaviour
    {
        public Chunk owner = null;
        
        void OnBecameVisible()
        {
            FogOfWar.ChunkUpdate(owner, true);
        }

        void OnBecameInvisible()
        {
            FogOfWar.ChunkUpdate(owner, false);
        }
    }
}