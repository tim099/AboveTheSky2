
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UnityEngine;

namespace ATS
{
    public static class ATS_StaticTextures
    {
        public static Texture2D TileFrame => UCL_SpriteAsset.Util.GetData("TileFrame").Texture;
        //public static Texture2D White
        //{
        //    get
        //    {
        //        if(s_White == null)
        //        {
        //            s_White = new Texture2D(1, 1);
        //            s_White.SetPixel(0, 0, Color.white);
        //            s_White.Apply();
        //        }
        //        return s_White;
        //    }
        //}
        //public static Texture2D s_White = null;
            //UCL_SpriteAsset.Util.GetData("White").Texture;

    }
}
