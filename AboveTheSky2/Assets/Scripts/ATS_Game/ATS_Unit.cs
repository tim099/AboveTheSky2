
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using UnityEngine;

namespace ATS
{
    public class ATS_Unit : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
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
