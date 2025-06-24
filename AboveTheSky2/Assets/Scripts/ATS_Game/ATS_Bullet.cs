
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using FMODUnity;
using UnityEngine;

namespace ATS
{
    public class ATS_Bullet : MonoBehaviour
    {
        public float m_Vel = 0.1f;

        public int m_Damage = 10;
        /// <summary>
        /// 攻擊目標
        /// </summary>
        public ATS_Attackable m_Target;
        public AudioEvent m_AudioEvent = new();

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
                GameObject.Destroy(gameObject);
                return;
            }
            var targetPos = m_Target.Center;
            Vector3 del = targetPos - transform.position;
            if (del.magnitude <= m_Vel)//hit
            {
                m_AudioEvent.PlayOneShot();
                transform.position = targetPos;
                m_Target.OnHit(m_Damage);
                GameObject.Destroy(gameObject);
                return;
            }

            transform.position += m_Vel * del.normalized;

        }
    }



    public enum EmitterDestroyType//Emitter 甚麼狀態之下會進入銷毀程序
    {
        Never, WhenStop
    }

    [System.Serializable]
    public class AudioEvent
    {
        public EventReference m_EventReference;
        public Transform m_TargetPos;

        public void PlayOneShot()
        {
            Debug.LogError($"PlayOneShot:{m_EventReference.Path}");
            RuntimeManager.PlayOneShot(m_EventReference, m_TargetPos == null ? Vector3.zero : m_TargetPos.position);
        }
    }
}
