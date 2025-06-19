
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using UnityEngine;

namespace ATS
{
    public class ATS_Bullet : MonoBehaviour
    {
        public float m_Vel = 0.1f;
        

        /// <summary>
        /// 攻擊目標
        /// </summary>
        public ATS_Attackable m_Target;

        public ATS_Weapon Weapon { get; private set; }

        public void Init(ATS_Weapon weapon, ATS_Attackable target)
        {
            Weapon = weapon;
            m_Target = target;

            transform.position = Weapon.transform.position;
            gameObject.SetActive(true);
        }



        private void Update()
        {
            if(m_Target == null)
            {
                return;
            }
            Vector3 del = m_Target.transform.position - transform.position;
            if (del.magnitude <= m_Vel)//hit
            {
                transform.position = m_Target.transform.position;

                return;
            }

            transform.position += m_Vel * del.normalized;

        }
    }
}
