using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HoneyFramework
{
    /*
     *  This "would be structure" data block which is used by mesh preparation data to construct single sprite.
     *  Note that this is not a structure because of its memory size (128 bytes = 1024 bits). 
     *  Structure will force to copy all of its data every time its assigned which will waste resources
     */
    public class QuadPreparation
    {
        public Vector3 v0;
        public Vector3 v1;
        public Vector3 v2;
        public Vector3 v3;

        public Vector2 minUV;
        public Vector2 maxUV;

        public Color c0;
        public Color c1;
        public Color c2;
        public Color c3;

    }

    /*
     *  Helper class for creation of the mesh data for sprite like or sprite sequence meshes
     */
    public class MeshPreparationData
    {
        public List<Vector3> vertexList = new List<Vector3>();
        public List<Vector3> normalsList = new List<Vector3>();
        public List<Vector2> uvList = new List<Vector2>();
        public List<int> indexList = new List<int>();
        public List<Color> colorList = new List<Color>();

        /*          maxUV
         *   v3 ----- v2
         *      |  /|
         *      | / |
         *      |/  |
         *   v0 ----- v1
         *   minUV
         */

        /// <summary>
        /// Function which adds single sprite to data block based on data provided
        /// </summary>
        /// <param name="data"></param>
        /// <param name="qData"></param>
        /// <returns></returns>
        static public void AddSingleSprite(MeshPreparationData data, QuadPreparation qData)
        {
            int startingIndex = data.vertexList.Count;
            /*
             Vertices
             */

            data.vertexList.Add(qData.v0);
            data.vertexList.Add(qData.v1);
            data.vertexList.Add(qData.v2);
            data.vertexList.Add(qData.v3);

            /* 
             UVs
             */

            data.uvList.Add(new Vector2(qData.minUV.x, qData.minUV.y));
            data.uvList.Add(new Vector2(qData.maxUV.x, qData.minUV.y));
            data.uvList.Add(new Vector2(qData.maxUV.x, qData.maxUV.y));
            data.uvList.Add(new Vector2(qData.minUV.x, qData.maxUV.y));

            /*
             Index
             */
            data.indexList.Add(startingIndex + 0);
            data.indexList.Add(startingIndex + 2);
            data.indexList.Add(startingIndex + 1);

            data.indexList.Add(startingIndex + 0);
            data.indexList.Add(startingIndex + 3);
            data.indexList.Add(startingIndex + 2);

            /*
             Color
             */
            data.colorList.Add(qData.c0);
            data.colorList.Add(qData.c1);
            data.colorList.Add(qData.c2);
            data.colorList.Add(qData.c3);
        }
    }   
}