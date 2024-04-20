
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
// Create time : 03/06 2024 13:00
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace ATS
{
    public class ATS_ToonCamera : MonoBehaviour
    {
        public static ATS_ToonCamera Ins { get; private set; } = null;

        public PixelPerfectCamera m_PixelPerfectCamera;
        public Camera m_Camera;
        public GameObject m_RayCastIndicator;
        public float m_MaxRayDistance = 500f;

        public float m_Test = 1f;
        public float m_Acc = 0.03f;
        public float m_VelDec = 0.95f;

        private bool m_Inited = false;

        /// <summary>
        /// 實際座標 因為移動後相機座標要根據像素做校正 所以會有誤差
        /// </summary>
        private Vector3 m_Pos = Vector3.zero;
        private Vector3 m_Origin = Vector3.zero;
        private Vector3 m_Vel = Vector3.zero;
        private Vector2Int m_PrePos = Vector2Int.zero;

        /// <summary>
        /// 在Camera平面上的投影點
        /// </summary>
        private Vector3 m_CastPoint = Vector3.zero;
        /// <summary>
        /// 在Camera平面上的投影向量(Up)
        /// </summary>
        private Vector3 m_CastUpVec = Vector3.zero;
        /// <summary>
        /// 在Camera平面上的投影向量(Right)
        /// </summary>
        private Vector3 m_CastRightVec = Vector3.zero;

        private Vector3 m_XCastPoint = Vector3.zero;
        private Vector3 m_YCastPoint = Vector3.zero;
        private Vector3 m_FinalCastPoint = Vector3.zero;

        private Vector3 m_FowardVec = Vector3.zero;
        private Vector3 m_UpVec = Vector3.zero;
        private Vector3 m_RightVec = Vector3.zero;

        private Vector3 m_A = Vector3.zero;
        /// <summary>
        /// 1f / m_PixelPerfectCamera.assetsPPU
        /// Assets pixel per unit
        /// </summary>
        private float m_DivPPU = 1f;
        /// <summary>
        /// camera pixel width
        /// </summary>
        private int m_Width = 1;
        /// <summary>
        /// camera pixel height
        /// </summary>
        private int m_Height = 1;
        private float m_PixelWidth = 1;
        private float m_PixelHeight = 1;
        private List<Vector3> m_VisitedPoints = new List<Vector3>();
        private List<Vector3> m_FinalCastPoints = new List<Vector3>();
        private void Awake()
        {
            Ins = this;
            Init();
        }
        private void Init()
        {
            if(m_Inited) return;
            m_Inited = true;
            m_Origin = m_Pos = m_Camera.transform.position;
            m_Width = m_Camera.pixelWidth;
            m_Height = m_Camera.pixelHeight;



            UpdateSetting();

            Debug.LogError($"m_DivPPU:{m_DivPPU},m_PixelWidth:{m_PixelWidth},m_PixelHeight:{m_PixelHeight}");
        }
        // Start is called before the first frame update
        void Start()
        {

            
        }
        private void MoveCam()
        {
            m_Vel *= m_VelDec;
            Vector3 up = m_Camera.transform.up;
            Vector3 right = m_Camera.transform.right;
            up.y = 0;
            right.y = 0;
            up.Normalize();
            right.Normalize();
            if (Input.GetKey(KeyCode.W))
            {
                m_Vel += m_Acc * up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                m_Vel -= m_Acc * up;
            }
            if (Input.GetKey(KeyCode.A))
            {
                m_Vel -= m_Acc * right;
            }
            if (Input.GetKey(KeyCode.D))
            {
                m_Vel += m_Acc * right;
            }
            m_Pos += m_Vel;
        }
        private void RaycastTest()
        {
            RaycastHit hit;
            int width = m_Camera.pixelWidth;
            int height = m_Camera.pixelHeight;
            var mousePos = Input.mousePosition;
            mousePos.x *= ((float)width / Screen.width);
            mousePos.y *= ((float)height / Screen.height);
            //Debug.LogError($"Input.mousePosition:{mousePos},m_PixelPerfectCamera.assetsPPU:{m_PixelPerfectCamera.assetsPPU},pixelSize:{PPU}");
            Ray ray = m_Camera.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out hit, m_MaxRayDistance))
            {
                //Debug.DrawRay(ray.origin, ray.direction * 500, Color.green, 0.5f);
                if (hit.collider != null)
                {
                    // The gameobject hit
                    //GameObject hitGameObject = hit.collider.gameObject;


                    //Debug.LogWarning("hit  " + hit.collider.name);
                }


                m_RayCastIndicator.transform.position = hit.point;

            }
        }
        private void UpdateSetting()
        {
            m_FowardVec = m_Camera.transform.forward;
            m_RightVec = m_Camera.transform.right;
            m_UpVec = m_Camera.transform.up;

            m_DivPPU = 1f / m_PixelPerfectCamera.assetsPPU;
            m_PixelWidth = m_DivPPU;//m_Camera.orthographicSize/ m_Width;//Mathf.Sqrt(2f) * //upFlatDotDivSQ
            m_PixelHeight = m_DivPPU;// m_Camera.orthographicSize/ m_Height;
        }

        void UpdatePos()
        {
            //const float ScaleFactor = Mathf.Sqrt(2f);
            UpdateSetting();

            Vector3 moveOffset = m_Pos - m_Origin;//camera離起始點的位移
            Vector3 camSpacePos = moveOffset - Vector3.Dot(moveOffset, m_FowardVec) * m_FowardVec;//轉換到攝影機平面座標




            float rightDot = Vector3.Dot(camSpacePos, m_RightVec);
            float upDot = Vector3.Dot(camSpacePos, m_UpVec);
            m_CastUpVec = upDot * m_UpVec;
            m_CastRightVec = rightDot * m_RightVec;
            m_CastPoint = m_CastRightVec + m_CastUpVec;

            Vector2 posVal = new Vector2(rightDot / m_PixelWidth, upDot / m_PixelHeight);

            int xPixel = Mathf.RoundToInt(posVal.x);
            int yPixel = Mathf.RoundToInt(posVal.y);
            float pX = xPixel * m_PixelWidth;
            float pY = yPixel * m_PixelHeight;
            //Debug.LogError($"rightVec:{m_RightVec},upVec:{m_UpVec},m_PixelWidth:{m_PixelWidth},m_PixelHeight:{m_PixelHeight},x:{xPixel},z:{yPixel}");
            m_YCastPoint = m_UpVec * pY;
            m_XCastPoint = m_RightVec * pX;
            m_FinalCastPoint = m_XCastPoint + m_YCastPoint;
            Vector3 castDel = m_FinalCastPoint - camSpacePos;

            //castPoint是在Camera平面的像素座標投影點
            //m_Camera.WorldToScreenPoint(castPoint);
            //Vector3 p = new Vector3(pX, m_Pos.y, pZ);
            float len = m_FinalCastPoint.y / m_FowardVec.y; //Vector3.Dot(castPoint, foward);
            Vector3 resultMoveOffset = m_FinalCastPoint - len * m_FowardVec;//轉換回原本的平面(y = 0)
            Vector3 moveOffsetDel = resultMoveOffset - moveOffset;
            //Debug.LogError($"camSpacePos:{camSpacePos.ToStringDetailValue()}" +
            //    $"castPoint:{m_FinalCastPoint.ToStringDetailValue()},castDel:{castDel.ToStringDetailValue()}" +
            //    $",resultMoveOffset:{resultMoveOffset.ToStringDetailValue()},moveOffset:{moveOffset.ToStringDetailValue()},moveOffsetDel:{moveOffsetDel.ToStringDetailValue()}");

            //resultMoveOffset.y = 0;// m_Pos.y;
            Vector3 finalPos = resultMoveOffset + m_Origin;


            Vector2Int pos = new Vector2Int(xPixel, yPixel);
            if (m_PrePos != pos)
            {
                if (m_VisitedPoints.Count > 0)
                {
                    var prev = m_VisitedPoints.LastElement();
                    var del = finalPos - prev;
                    Debug.LogError($"prev:{prev.ToStringDetailValue()},cur:{finalPos.ToStringDetailValue()},del:{del.ToStringDetailValue()},del.magnitude:{del.magnitude}" +
                        $",m_PrePos:{m_PrePos},pos:{pos}");
                }
                if (m_FinalCastPoints.Count > 0)
                {
                    var prev = m_FinalCastPoints.LastElement();
                    var del = m_FinalCastPoint - prev;
                    Debug.LogError($"prevFinalCastPoint:{prev.ToStringDetailValue()},curFinalCastPoint:{m_FinalCastPoint.ToStringDetailValue()},del:{del.ToStringDetailValue()},del.magnitude:{del.magnitude}" +
                        $",m_PrePos:{m_PrePos},pos:{pos}");
                }
                m_FinalCastPoints.Add(m_FinalCastPoint);

                m_PrePos = pos;
                m_VisitedPoints.Add(finalPos);

                Debug.LogError($"posVal:{posVal},rightDot:{rightDot},upDot:{upDot}");
                Debug.LogError($"m_Pos:{m_Pos},len:{len}, castPoint:{m_FinalCastPoint},p:{resultMoveOffset},finalPos:{finalPos},camPos.x:{moveOffset.x},camPos.y:{moveOffset.y},x:{xPixel},z:{yPixel},pX:{pX},pZ:{pY},p:{resultMoveOffset},xVec:{m_RightVec},yVec:{m_UpVec}");
            }
            
            transform.position = finalPos;//m_Pos

            Debug.LogError($"m_Pos:{m_Pos},finalPos:{finalPos},del:{m_Pos - finalPos}");
        }

        [SerializeField] private bool m_DrawCameraVec = true;
        [SerializeField] private bool m_DrawVisitedPoints = true;
        private void OnDrawGizmos()
        {
            if (!m_Inited)
            {
                Init();
            }
            if (m_DrawVisitedPoints)
            {
                
                Vector3 prevPoint = Vector3.negativeInfinity;
                foreach(var point in m_VisitedPoints) {
                    Gizmos.color = Color.green;
                    if (!prevPoint.Equals(Vector3.negativeInfinity))
                    {
                        Gizmos.DrawLine(prevPoint, point);
                    }
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(point, 0.01f);

                    prevPoint = point;
                }
            }

            {
                Vector3 prevPoint = Vector3.negativeInfinity;
                foreach (var point in m_FinalCastPoints)
                {
                    Gizmos.color = Color.blue;
                    if (!prevPoint.Equals(Vector3.negativeInfinity))
                    {
                        Gizmos.DrawLine(m_Origin + prevPoint, m_Origin + point);
                    }
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireSphere(m_Origin + point, 0.01f);

                    prevPoint = point;
                }
            }
            //m_VisitedPoints


            Gizmos.color = Color.green;
            //Gizmos.DrawSphere(m_Origin, 0.03f);
            Gizmos.DrawIcon(m_Origin, "plus_4.png", false);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 0.03f);
            //Gizmos.DrawIcon(transform.position, "plus_3.png", false);


            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(m_Pos, 0.04f);
            //Gizmos.DrawIcon(m_Pos, "plus_2.png", false);

            Gizmos.color = Color.cyan;
            var finalCastPoint = m_Origin + m_FinalCastPoint;
            var yCastPoint = m_Origin + m_YCastPoint;

            Gizmos.DrawSphere(finalCastPoint, 0.01f);
            Gizmos.DrawSphere(yCastPoint, 0.01f);
            Gizmos.DrawLine(yCastPoint, finalCastPoint);

            //Gizmos.DrawIcon(finalCastPoint, "plus.png", false);


            Gizmos.color = Color.red;
            Gizmos.DrawLine(m_Origin, yCastPoint);
            Gizmos.DrawLine(m_Origin, finalCastPoint);

            Vector3 castPoint = m_Origin + m_CastPoint;
            Vector3 castUp = m_Origin + m_CastUpVec;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(castUp, 0.02f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(castUp, castPoint);
            Gizmos.DrawWireSphere(castPoint, 0.02f);
            //Gizmos.DrawIcon(castPoint, "plus_1.png", false);
            

            Gizmos.color = Color.green;
            Gizmos.DrawLine(m_Origin, m_Pos);
            Gizmos.DrawLine(finalCastPoint, m_Pos);

            if (m_DrawCameraVec)//繪製Camera的Up跟Right
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(m_Pos, m_Pos + m_UpVec);
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(m_Pos, m_Pos + m_RightVec);
            }

            Vector3 sa = Vector3.zero, sb = Vector3.zero, sc = Vector3.zero, sd = Vector3.zero;
            Vector3 b = Vector3.zero, c = Vector3.zero, d = Vector3.zero;
            RaycastHit hit;
            const float MaxRayDis = 1000f;
            {
                Ray ray = m_Camera.ScreenPointToRay(Vector3.zero);
                sa = ray.origin;// ray.GetPoint(0);
                if (Physics.Raycast(ray, out hit, MaxRayDis))
                {
                    m_A = hit.point;
                }
            }
            {
                Ray ray = m_Camera.ScreenPointToRay(new Vector3(m_Width, 0 , 0));
                sb = ray.origin;//ray.GetPoint(0);
                if (Physics.Raycast(ray, out hit, MaxRayDis))
                {
                    b = hit.point;
                }
            }
            {
                Ray ray = m_Camera.ScreenPointToRay(new Vector3(m_Width, m_Height, 0));
                sc = ray.origin;//ray.GetPoint(0);
                if (Physics.Raycast(ray, out hit, MaxRayDis))
                {
                    c = hit.point;
                }
            }
            {
                Ray ray = m_Camera.ScreenPointToRay(new Vector3(0, m_Height, 0));
                sd = ray.origin;
                if (Physics.Raycast(ray, out hit, MaxRayDis))
                {
                    d = hit.point;
                }
            }
            {
                Gizmos.color = Color.yellow;
                Ray ray = new Ray();//m_Camera.ScreenPointToRay(new Vector3(0.5f * m_Width, 0.5f * m_Height, 0));
                ray.direction = m_Camera.transform.forward;
                ray.origin = m_Camera.transform.position;
                var s = ray.origin;
                if (Physics.Raycast(ray, out hit, MaxRayDis))
                {
                    var e = hit.point;
                    Gizmos.DrawLine(s, e);
                }
            }
            const float Size = 0.1f;
            Gizmos.color = Color.red;
            //Gizmos.DrawLineList(new Vector3[] { a, b, c, d });
            Gizmos.DrawLine(m_A, b);
            Gizmos.DrawLine(b, c);
            Gizmos.DrawLine(c, d);
            Gizmos.DrawLine(d, m_A);

            Gizmos.DrawSphere(m_A, Size);
            Gizmos.DrawSphere(b, Size);
            Gizmos.DrawSphere(c, Size);
            Gizmos.DrawSphere(d, Size);

            Gizmos.DrawLine(m_A, sa);
            Gizmos.DrawLine(b, sb);
            Gizmos.DrawLine(c, sc);
            Gizmos.DrawLine(d, sd);

            Vector3 delX = sb - sa;
            delX.Normalize();
            delX *= m_PixelWidth;
            Vector3 delY = sd - sa;
            delY.Normalize();
            delY *= m_PixelHeight;
            Gizmos.color = Color.blue;
            Gizmos.DrawLineList(new Vector3[] { sa, sb, sb, sc, sc, sd, sd, sa });

            Gizmos.color = Color.yellow;
            for (int i = 1; i < m_Width; i++)
            {
                Gizmos.DrawSphere(sa + i * delX, 0.01f);
            }
            Gizmos.color = Color.green;
            for (int i = 1; i < m_Height; i++)
            {
                Gizmos.DrawSphere(sa + i * delY, 0.01f);
            }

            Gizmos.color = Color.red;
            Vector3 upVec = 0.001f * Vector3.up;
            for (int i = 1; i < m_Height; i++)
            {
                float val = (float)i / m_Height;
                Gizmos.DrawLine(Vector3.Lerp(m_A, b, val)+ upVec, Vector3.Lerp(d, c, val) + upVec);
            }
            for (int i = 1; i < m_Width; i++)
            {
                float val = (float)i / m_Width;
                Gizmos.DrawLine(Vector3.Lerp(m_A, d, val) + upVec, Vector3.Lerp(b, c, val) + upVec);
            }
            Debug.LogError($"a:{m_A},b:{b},c:{c},d:{d}");
        }
        void UpdatePos2()
        {
            //const float ScaleFactor = Mathf.Sqrt(2f);


            //Vector3 rightVec = m_Camera.transform.right;
            //Vector3 upVec = m_Camera.transform.up;

            //Vector3 foward = m_Camera.transform.forward;
            //Debug.LogError($"upVec:{upVec},rightVec:{rightVec},foward:{foward}");

            m_Camera.transform.position = m_Origin;

            Vector3 moveOffset = m_Pos - m_Origin;//camera離起始點的位移

            Vector3 screenPoint = m_Camera.WorldToScreenPoint(moveOffset);//
            Vector3 pixelSreenPoint = screenPoint;
            pixelSreenPoint.x = Mathf.RoundToInt(pixelSreenPoint.x);
            pixelSreenPoint.y = Mathf.RoundToInt(pixelSreenPoint.y);
            pixelSreenPoint.z = Mathf.RoundToInt(pixelSreenPoint.z);
            Vector3 pixelMoveOffset = m_Camera.ScreenToWorldPoint(pixelSreenPoint);
            Vector3 finalPos = pixelMoveOffset + m_Origin;

            Debug.LogError($"m_Pos:{m_Pos.ToStringDetailValue()},screenPoint:{screenPoint.ToStringDetailValue()},moveOffset:{moveOffset.ToStringDetailValue()}" +
                $",pixelSreenPoint:{pixelSreenPoint.ToStringDetailValue()},pixelMoveOffset:{pixelMoveOffset.ToStringDetailValue()}" +
                $",finalPos:{finalPos.ToStringDetailValue()}");

            
            transform.position = finalPos;//m_Pos
        }
        // Update is called once per frame
        void Update()
        {
            if(m_Camera == null)
            {
                Debug.LogError("ATS_ToonCamera m_Camera == null");
                return;
            }

            MoveCam();
            RaycastTest();

            UpdatePos();
        }
    }
}
