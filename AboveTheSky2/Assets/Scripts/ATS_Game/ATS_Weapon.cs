
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using UnityEngine;

namespace ATS
{
    public class ATS_Weapon : MonoBehaviour
    {
        public ATS_Bullet m_Bullet;
        public float m_CoolDown = 1.5f;
        public ATS_Attackable Owner { get; private set; }
        private bool m_Inited = false;
        public float m_CurCoolDown = 0;

        public int Faction => Owner.m_Faction;
        virtual public void Init(ATS_Attackable owner)
        {
            this.Owner = owner;
            m_Inited = true;
        }
        private void Update()
        {
            if (!m_Inited)
            {
                return;
            }
            if(m_CurCoolDown > 0)
            {
                m_CurCoolDown -= Time.deltaTime;
                return;
            }
            var target = ATS_Attackable.FindEnemy(Faction);
            if (target != null)
            {
                //Fire
                m_CurCoolDown = m_CoolDown;
                var bullet = Instantiate(m_Bullet);
                bullet.Init(this, target);
            }
            //foreach (var target in ATS_Attackable.s_Attackables)
            //{
            //    if(target.m_Faction != Faction)
            //    {
            //        //Fire
            //        m_CurCoolDown = m_CoolDown;
            //        var bullet = Instantiate(m_Bullet);
            //        bullet.Init(this, target);
            //    }
            //}


        }
    }
}
