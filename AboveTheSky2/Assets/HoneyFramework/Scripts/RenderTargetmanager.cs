using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace HoneyFramework
{
    class RenderTarget
    {
        public int width;
        public int height;
        public int depth;
        public RenderTextureFormat format;
        public RenderTextureReadWrite sampling;
        public int antiAliasing;
        public RenderTexture texture;
        public bool inUse;

        public RenderTarget(int w, int h) : this(w, h, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, 1) { }

        public RenderTarget(int w, int h, int depth) : this(w, h, depth, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, 1) { }

        public RenderTarget(int w, int h, int d, RenderTextureFormat f, RenderTextureReadWrite s, int aa)
        {
            texture = new RenderTexture(w, h, d, f, s);
            texture.antiAliasing = aa;
            width = w;
            height = h;
            depth = d;
            format = f;
            sampling = s;
            antiAliasing = aa;
        }
    }

    class RenderTargetManager
    {
        static private RenderTargetManager instance;        
        private List<RenderTarget> textures = new List<RenderTarget>();

        static private RenderTargetManager GetInstance()
        {
            if (instance == null)
            {
                instance = new RenderTargetManager();
            }

            return instance;
        }

        /// <summary>
        /// Prepares texture either by reusing unused one or preparing new texture
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        static public RenderTexture GetNewTexture(int w, int h)
        {
            return GetNewTexture(w, h, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, 1);
        }

        /// <summary>
        /// Prepares texture either by reusing unused one or preparing new texture
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="depth"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        static public RenderTexture GetNewTexture(int w, int h, int depth, RenderTextureFormat f)
        {
            return GetNewTexture(w, h, depth, f, RenderTextureReadWrite.Default, 1);
        }

        /// <summary>
        /// Prepares texture either by reusing unused one or preparing new texture
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="depth"></param>
        /// <param name="f"></param>
        /// <param name="s"></param>
        /// <param name="aa"></param>
        /// <returns></returns>
        static public RenderTexture GetNewTexture(int w, int h, int depth, RenderTextureFormat f, RenderTextureReadWrite s, int aa)
        {
            RenderTarget rt = GetInstance().textures.Find(o => (
                                                !o.inUse && 
                                                o.width == w && 
                                                o.height == h && 
                                                o.depth == depth &&
                                                o.format == f && 
                                                o.sampling == s && 
                                                o.antiAliasing == aa
                                                )
                                           );

            if (rt == null)
            {
                rt = new RenderTarget(w, h, depth, f, s, aa);
                GetInstance().textures.Add(rt);
            }

            rt.inUse = true;

            return rt.texture;
        }

        /// <summary>
        /// This method marks one texture as unused this way it can be destroyed later or kept in memory for future use
        /// </summary>
        /// <param name="rt"></param>
        /// <returns></returns>
        static public void ReleaseTexture(RenderTexture rt)
        {
            RenderTarget rTarget = GetInstance().textures.Find(o => o.texture == rt);
            if (rTarget != null)
            {
                rTarget.inUse = false;
            }
        }

        /// <summary>
        /// This method marks all textures as unused this way they can be destroyed later or kept in memory for future use
        /// </summary>
        /// <returns></returns>
        static public void ReleaseAll()
        {
            List<RenderTarget> targets = GetInstance().textures;
            foreach (RenderTarget rt in targets)
            {
                rt.inUse = false;
            }
        }

        /// <summary>
        /// Destroy single render texture, it doesnt care if its marked or not as unused
        /// </summary>
        /// <param name="rt"></param>
        /// <returns></returns>
        static public void DestroyTexture(RenderTexture rt)
        {
            RenderTarget rTarget = GetInstance().textures.Find(o => o.texture == rt);
            if (rTarget != null)
            {
                GameObject.Destroy(rTarget.texture);
                GetInstance().textures.Remove(rTarget);
            }
        }

        /// <summary>
        /// Makes global cleanup of all unused textures. Usually used jsut after "ReleaseAll" 
        /// </summary>
        /// <returns></returns>
        static public void DestroyAllUnusedTextures()
        {
            List<RenderTarget> targets = GetInstance().textures.FindAll(o => !o.inUse);
            foreach (RenderTarget rt in targets)
            {
                GameObject.Destroy(rt.texture);
            }

            GetInstance().textures.RemoveAll(o => !o.inUse);
            if (GetInstance().textures.Count == 0)
            {
                GetInstance().textures.Clear();
                instance = null;
            }
        }
    }
}