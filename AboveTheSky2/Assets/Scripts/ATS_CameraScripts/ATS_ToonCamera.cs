
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
// Create time : 03/06 2024 13:00
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATS
{
    public class ATS_ToonCamera : MonoBehaviour
    {
        public static ATS_ToonCamera Ins { get; private set; } = null;

        public Camera m_Camera;
        public GameObject m_RayCastIndicator;
        public float m_MaxRayDistance = 500f;
        private void Awake()
        {
            Ins = this;
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if(m_Camera == null)
            {
                Debug.LogError("ATS_ToonCamera m_Camera == null");
                return;
            }
            RaycastHit hit;
            var mousePos = Input.mousePosition;
            mousePos.x *=((float)m_Camera.pixelWidth / Screen.width);
            mousePos.y *=((float)m_Camera.pixelHeight / Screen.height);

            //Debug.LogError($"Input.mousePosition:{mousePos}");
            Ray ray = m_Camera.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out hit, m_MaxRayDistance))
            {
                //Debug.DrawRay(ray.origin, ray.direction * 500, Color.green, 0.5f);
                if(hit.collider != null)
                {
                    // The gameobject hit
                    //GameObject hitGameObject = hit.collider.gameObject;


                    //Debug.LogWarning("hit  " + hit.collider.name);
                }

                
                m_RayCastIndicator.transform.position = hit.point;

            }


            //const float Speed = 0.05f;
            //if (Input.GetKey(KeyCode.W))
            //{
            //    transform.position += new Vector3(Speed, 0, Speed);
            //}
            //if (Input.GetKey(KeyCode.S))
            //{
            //    transform.position -= new Vector3(Speed, 0, Speed);
            //}
            //if (Input.GetKey(KeyCode.A))
            //{
            //    transform.position += new Vector3(-Speed, 0, Speed);
            //}
            //if (Input.GetKey(KeyCode.D))
            //{
            //    transform.position += new Vector3(Speed, 0, -Speed);
            //}
        }
    }
}
