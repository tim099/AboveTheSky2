
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UnityEngine;

namespace ATS
{
    public static partial class ATS_AssetGroup
    {
        public const string ATS = "ATS";
        public enum AssetType : int
        {
            ATS_TileData = 1,
            ATS_Test,
        }
    }

    public enum TileType
    {
        /// <summary>
        /// 不可建造
        /// </summary>
        None,
        /// <summary>
        /// 可建築的空格
        /// </summary>
        BuildingSlot,
    }


    [UCL.Core.ATTR.UCL_GroupIDAttribute(ATS_AssetGroup.ATS)]
    [UCL.Core.ATTR.UCL_Sort((int)ATS_AssetGroup.AssetType.ATS_TileData)]
    public class ATS_TileData : UCL_Asset<ATS_TileData>
    {

        /// <summary>
        /// 圖示Icon
        /// </summary>
        public UCL_SpriteAssetEntry m_Sprite = new UCL_SpriteAssetEntry();
        public TileType m_TileType = TileType.None;
        /// <summary>
        /// 是否要顯示(false則此地塊隱藏)
        /// </summary>
        public bool m_Show = true;

        /// <summary>
        /// 地塊的通行狀態
        /// </summary>
        public TilePathState m_TilePathState = new TilePathState();


        /// <summary>
        /// 是否能建造
        /// </summary>
        public bool CanBuild => m_TilePathState.GetPathState(PathState.CanBuild);//m_TileType == TileType.BuildingSlot;

        public Texture2D Texture => m_Sprite.Texture;
    }

    public class ATS_TileDataEntry : UCL_AssetEntryDefault<ATS_TileData>
    {
        public const string DefaultID = "Grass";


        public ATS_TileDataEntry() { m_ID = DefaultID; }
        public ATS_TileDataEntry(string iID) { m_ID = iID; }
    }
}
