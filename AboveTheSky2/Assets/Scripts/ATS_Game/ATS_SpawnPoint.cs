
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using UnityEngine;

namespace ATS
{
    public class ATS_SpawnPoint : MonoBehaviour
    {
        public int m_CoolDown = 200;
        public int m_CoolDownTimer = 0;
        /// <summary>
        /// 陣營
        /// </summary>
        public int m_Faction = 0;

        public ATS_Unit m_UnitTmp;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (m_CoolDownTimer > 0)
            {
                --m_CoolDownTimer;
                return;
            }
            m_CoolDownTimer = m_CoolDown;
            var unit = Instantiate(m_UnitTmp);
            unit.Spawn(m_Faction, transform);

            Debug.LogError($"Spawn:{unit.name}");
        }
    }
}
