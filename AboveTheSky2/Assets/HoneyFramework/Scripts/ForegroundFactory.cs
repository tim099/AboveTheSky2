using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HoneyFramework
{

    public class ForegroundFactory
    {
        static public List<string> warnings;
        /// <summary>
        /// Request to build foreground for specified chunk
        /// </summary>
        /// <param name="chunk"> chunk requesting foregorund build </param>
        /// <param name="atlas"> atlas which should be used for foreground creation </param>
        /// <param name="viewAngle"> foreground is rotated to the angle of the camera. It works well for camera lookign from fixed angle </param>
        /// <param name="worldPosition"> world position </param>
        /// <returns> mesh containing block of the foreground covering single chunk </returns>
        static public Mesh BuildForeground(Chunk chunk, UFTAtlasMetadata atlas, float viewAngle, Vector3 worldPosition)
        {
            //build mesh using sorted chunk data
            MeshPreparationData mpd = new MeshPreparationData();
            Quaternion qAngle = Quaternion.Euler(viewAngle, 0.0f, 0.0f);

            for (int i = 0; i < chunk.foregroundData.Count; i++)
            {
                AddSingleSprite(mpd, chunk.foregroundData[i], qAngle, atlas, worldPosition);
            }

            //check if anything have been produced
            if (mpd.vertexList.Count == 0) return null;

            //fill data to mesh
            Mesh m = null;
            m = new Mesh();
            m.vertices = mpd.vertexList.ToArray();
            m.uv = mpd.uvList.ToArray();
            m.triangles = mpd.indexList.ToArray();
            m.colors = mpd.colorList.ToArray();

            return m;
        }

        /// <summary>
        /// Adds single sprite (4 vertices) to the mesh data prepared with foreground instance.
        /// </summary>
        /// <param name="data"> data block of the mesh to which we add data for new sprite</param>
        /// <param name="source"> foreground definition</param>
        /// <param name="viewAngle"> rotation for the sprite to look at</param>
        /// <param name="atlas"> atlas data to find sprite uvs in </param>
        /// <param name="worldPosition"> world position </param>
        /// <returns></returns>
        static private void AddSingleSprite(MeshPreparationData data, ForegroundData source, Quaternion viewAngle, UFTAtlasMetadata atlas, Vector3 worldPosition)
        {
            UFTAtlasEntryMetadata spriteData = atlas.GetByName(source.name);
            int startingIndex = data.vertexList.Count;
            /*
             Vertices
             */
            if (spriteData == null)
            {
                if (warnings == null) warnings = new List<string>();

                if (!warnings.Contains(source.name))
                {
                    Debug.Log("Foreground texture not found for: " + source.name);
                    warnings.Add(source.name);
                }
                return;
            }
            float height = spriteData.pixelRect.height * source.scale * 2f;
            float width = spriteData.pixelRect.width * source.scale * 2f;

            Vector2 uv = new Vector2(spriteData._pivot.x / spriteData._pixelRect.width, spriteData._pivot.y / spriteData._pixelRect.height);
            uv.x = 1f - uv.x;

            Vector3 topLeft     = viewAngle * new Vector3(-(1f - uv.x) * width  , uv.y * height         , 0f);
            Vector3 bottomLeft  = viewAngle * new Vector3(-(1f - uv.x) * width  , -(1f - uv.y) * height , 0f);
            Vector3 bottomRight = viewAngle * new Vector3(uv.x * width          , -(1f - uv.y) * height , 0f);
            Vector3 topRight    = viewAngle * new Vector3(uv.x * width          , uv.y * height         , 0f);

            data.vertexList.Add(topLeft + source.position + worldPosition);
            data.vertexList.Add(bottomLeft + source.position + worldPosition);
            data.vertexList.Add(bottomRight + source.position + worldPosition);
            data.vertexList.Add(topRight + source.position + worldPosition);

            /* 
             UVs
             */
            float topUV = spriteData.uvRect.yMin;
            float leftUV = spriteData.uvRect.xMin;
            float bottomUV = spriteData.uvRect.yMax;
            float rightUV = spriteData.uvRect.xMax;

            float left = source.horizontalInverse ? rightUV : leftUV;
            float right = source.horizontalInverse ? leftUV : rightUV;

            //texture uv
            data.uvList.Add(new Vector2(left, bottomUV));
            data.uvList.Add(new Vector2(left, topUV));
            data.uvList.Add(new Vector2(right, topUV));
            data.uvList.Add(new Vector2(right, bottomUV));

            /*
             Index
             */
            data.indexList.Add(startingIndex + 2);
            data.indexList.Add(startingIndex + 1);
            data.indexList.Add(startingIndex + 0);

            data.indexList.Add(startingIndex + 0);
            data.indexList.Add(startingIndex + 3);
            data.indexList.Add(startingIndex + 2);

            /*
             Color
             */
            float lightStrength = 0.8f;
            data.colorList.Add(source.colorFinal.GetColor());
            data.colorList.Add(source.colorFinal.GetColor() * (1 - lightStrength));//use extra shadow on side of the foreground        
            data.colorList.Add(source.colorFinal.GetColor() * (1 + lightStrength));//use extra light on side of the foreground
            data.colorList.Add(source.colorFinal.GetColor());
        }

    }
}