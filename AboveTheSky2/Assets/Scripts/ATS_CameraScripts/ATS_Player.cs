
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
// Create time : 03/06 2024 19:51
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATS
{
    public class ATS_Player : MonoBehaviour
    {
        public Rigidbody m_Rigidbody;
        public GameObject m_PlayerObj;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            const float Speed = 10.05f;
            if (Input.GetKey(KeyCode.W))
            {
                m_Rigidbody.AddForce((Vector3.forward + Vector3.right) * Speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                m_Rigidbody.AddForce((Vector3.back + Vector3.left) * Speed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                m_Rigidbody.AddForce((Vector3.forward + Vector3.left) * Speed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                m_Rigidbody.AddForce((Vector3.back + Vector3.right) * Speed);
            }
            if(m_Rigidbody.linearVelocity.magnitude > 0)
            {
                m_PlayerObj.transform.LookAt(m_PlayerObj.transform.position + m_Rigidbody.linearVelocity);
            }
        }
    }
}
