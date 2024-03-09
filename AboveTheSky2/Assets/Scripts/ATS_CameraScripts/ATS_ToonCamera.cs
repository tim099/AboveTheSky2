
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
