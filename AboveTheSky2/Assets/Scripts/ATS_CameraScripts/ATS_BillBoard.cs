
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
// Create time : 03/06 2024 13:03
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATS
{
    public class ATS_BillBoard : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (ATS_ToonCamera.Ins != null)
            {
                transform.rotation = ATS_ToonCamera.Ins.m_Camera.transform.rotation;
            }
        }
    }
}
