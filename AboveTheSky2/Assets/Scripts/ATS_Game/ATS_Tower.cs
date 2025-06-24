
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs

using System.Collections.Generic;
using UnityEngine;

namespace ATS
{
    public class ATS_Tower : ATS_Attackable
    {
        public List<ATS_Weapon> m_Weapons = new();

        public override void Init()
        {
            base.Init();
            foreach(var weapon in m_Weapons)
            {
                weapon.Init(this);
            }
        }
    }
}
