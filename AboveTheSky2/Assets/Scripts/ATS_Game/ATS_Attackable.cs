
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections.Generic;
using System.Linq;
using UCL.Core.MathLib;
using UnityEngine;

namespace ATS
{
    /// <summary>
    /// 可被作為攻擊目標
    /// </summary>
    public class ATS_Attackable : MonoBehaviour
    {
        #region static
        public static List<ATS_Attackable> s_Attackables = new();

        public static ATS_Attackable FindEnemy(int faction)
        {
            return UCL_Random.Instance.RandomPick(ATS_Attackable.s_Attackables.Where(target => target.m_Faction != faction).ToList());
            //foreach (var target in ATS_Attackable.s_Attackables)
            //{
            //    if (target.m_Faction != faction)
            //    {
            //        return target;
            //    }
            //}
            //return null;
        }
        #endregion



        public ATS_HPBar m_HPBar;

        public Transform m_Center;


        public Vector3 Center
        {
            get
            {
                if (m_Center != null) return m_Center.position;
                return transform.position;
            }
        }
        /// <summary>
        /// 陣營
        /// </summary>
        public int m_Faction = 0;
        public int m_HP;
        public int m_MaxHP = 100;
        /// <summary>
        /// 安裝的武器
        /// </summary>
        public List<ATS_Weapon> m_Weapons = new();
        virtual public void Init()
        {
            m_HP = m_MaxHP;
            foreach (var weapon in m_Weapons)
            {
                weapon.Init(this);
            }
        }

        virtual protected void Awake()
        {
            s_Attackables.Add(this);
            Init();
        }
        virtual protected void OnDestroy()
        {
            s_Attackables.Remove(this);
        }

        virtual public void OnHit(int damage)
        {
            m_HP -= damage;
            if (m_HP <= 0)//die
            {
                m_HP = 0;
                GameObject.Destroy(gameObject);
                return;
            }
            if (m_HPBar != null)
            {
                m_HPBar.UpdateHP(m_HP, m_MaxHP);
            }
        }
    }
}
