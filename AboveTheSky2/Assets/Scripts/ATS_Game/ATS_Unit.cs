
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using UnityEngine;

namespace ATS
{
    public class ATS_Unit : ATS_Attackable
    {
        /// <summary>
        /// 攻擊目標
        /// </summary>
        public ATS_Attackable m_Target;

        public float m_Vel = 0.01f;

        public int m_Damage = 10;

        protected override void Awake()
        {
            base.Awake();

        }
        public void Spawn(int faction, Transform parent)
        {
            
            this.m_Faction = faction;
            
            this.transform.SetParent(parent, false);
            this.transform.position = parent.position;
            this.gameObject.SetActive(true);

            m_Target = ATS_Attackable.FindEnemy(m_Faction);
            Debug.LogError($"m_Target:{m_Target.name},m_Faction:{m_Faction},m_Target.Faction:{m_Target.m_Faction}");
        }
        // Update is called once per frame
        void Update()
        {
            if (m_Target == null)
            {
                GameObject.Destroy(gameObject);
                return;
            }
            var targetPos = m_Target.Center;
            Vector3 del = targetPos - transform.position;
            if (del.magnitude <= m_Vel)//hit
            {
                //m_AudioEvent.PlayOneShot();
                transform.position = targetPos;
                m_Target.OnHit(m_Damage);
                GameObject.Destroy(gameObject);
                return;
            }

            transform.position += m_Vel * del.normalized;
        }
        //private void OnCollisionEnter(Collision collision)
        //{
        //    Debug.LogError($"OnCollisionEnter collision:{collision.collider.name}");
        //}
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.LogError($"OnCollisionEnter2D collision:{collision.collider.name}");
        }
    }
}
