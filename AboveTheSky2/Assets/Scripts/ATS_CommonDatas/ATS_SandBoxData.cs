
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.LocalizeLib;
using UCL.Core.Page;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    [UCL.Core.ATTR.UCL_GroupIDAttribute(ATS_AssetGroup.ATS)]
    public class ATS_SandBoxData : UCL_Asset<ATS_SandBoxData>
    {
        public static ATS_SandBoxData Default => Util.GetData(ATS_SandBoxEntry.DefaultID);


        /// <summary>
        /// 起始飛船
        /// </summary>
        public ATS_AirshipDataEntry m_InitAirship = new();


        public ATS_SandBox Create(bool isLoadGame = false)
        {
            ATS_SandBox sandBox = new ATS_SandBox();
            sandBox.Data = this;

            sandBox.Init();
            if (!isLoadGame)
            {
                sandBox.SetAirShip(m_InitAirship.GetData().Create());
                sandBox.GameInit();
            }
            
            return sandBox;
        }
    }
    public class ATS_SandBoxEntry : UCL_AssetEntryDefault<ATS_SandBoxData>
    {
        public const string DefaultID = "SandBox_01";


        public ATS_SandBoxEntry() { m_ID = DefaultID; }
        public ATS_SandBoxEntry(string iID) { m_ID = iID; }
    }
}
