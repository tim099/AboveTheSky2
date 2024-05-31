
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
    }
}
