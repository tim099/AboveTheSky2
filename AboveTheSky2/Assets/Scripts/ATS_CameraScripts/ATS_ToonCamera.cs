
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
// Create time : 03/06 2024 13:00
using System;
using System.Collections;
using System.Collections.Generic;
using UCL.Core.UI;
using UnityEngine;


namespace ATS
{
    public class ATS_ToonCamera : MonoBehaviour
    {
        public static ATS_ToonCamera Ins { get; private set; } = null;

        public UnityEngine.Rendering.Universal.PixelPerfectCamera m_PixelPerfectCamera;
        public Camera m_Camera;
        public GameObject m_RayCastIndicator;
        public float m_MaxRayDistance = 500f;

        public float m_Test = 1f;
        /// <summary>
        /// -0.0366 , 0.05178 //0.08838
        /// </summary>
        public float m_TestY = 0;
        public float m_Acc = 0.03f;
        public float m_VelDec = 0.95f;

        private bool m_Inited = false;
        private float m_Y = 20;
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
        /// <summary>
        /// Camera的UpVector
        /// </summary>
        private Vector3 m_UpVec = Vector3.zero;
        private Vector3 m_UpVecN = Vector3.zero;
        private Vector3 m_RightVec = Vector3.zero;
        /// <summary>
        /// UpVec的位移投影到y=0平面後的值
        /// </summary>
        private Vector3 m_DelUp = Vector3.zero;
        private Vector3 m_DelFoward = Vector3.zero;

        private Vector3 m_A = Vector3.zero;

        private Vector2Int m_IntPos = Vector2Int.zero;

        /// <summary>
        /// m_PixelPerfectCamera.assetsPPU
        /// </summary>
        private float m_PPU = 1f;
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
        /// <summary>
        /// m_UpVecN投影到y = 0平面上的長度(單位長度投影)
        /// </summary>
        private float m_Uplen = 1f;

        private float m_Cos = 1f;

        private System.Text.StringBuilder m_OnGUILog = new System.Text.StringBuilder();

        private List<Vector3> m_VisitedPoints = new List<Vector3>();
        private List<Vector3> m_FinalCastPoints = new List<Vector3>();
        /// <summary>
        /// 垂直方向的像素長度
        /// </summary>
        float m_UpPixelSize = 0;
        private void Awake()
        {
            Ins = this;
            Init();
        }
        private void Init()
        {
            if(m_Inited) return;
            m_Inited = true;
            m_Y = transform.position.y;
            m_Origin = m_Pos = m_Camera.transform.position;
            m_Width = m_Camera.pixelWidth;
            m_Height = m_Camera.pixelHeight;



            UpdateSetting();

            //Debug.LogError($"m_DivPPU:{m_DivPPU},m_PixelWidth:{m_PixelWidth},m_PixelHeight:{m_PixelHeight}");
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

            if (Input.GetKeyDown(KeyCode.W))
            {
                ++m_IntPos.y;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                --m_IntPos.y;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                ++m_IntPos.x;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                --m_IntPos.x;
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
            m_Width = m_Camera.pixelWidth;
            m_Height = m_Camera.pixelHeight;

            m_FowardVec = m_Camera.transform.forward;
            m_RightVec = m_Camera.transform.right;
            m_UpVec = m_Camera.transform.up;

            m_PPU = m_PixelPerfectCamera.assetsPPU;
            m_DivPPU = 1f / m_PPU;
            m_PixelWidth = m_DivPPU;//m_Camera.orthographicSize/ m_Width;//Mathf.Sqrt(2f) * //upFlatDotDivSQ
            m_PixelHeight = m_DivPPU;// m_Camera.orthographicSize/ m_Height;

            m_UpVecN = m_UpVec;
            m_UpVecN.y = 0;
            m_Uplen = m_UpVecN.magnitude;
            m_UpVecN.Normalize();
            m_Cos = (1f / m_Uplen);

            m_UpPixelSize = m_DivPPU * m_Cos;
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
        [SerializeField] private bool m_DrawCameraGrid = true;
        [SerializeField] private bool m_DrawPreviewGrid = true;
        [SerializeField] private bool m_DrawPreviewRay = true;
        private List<Vector3> m_CornerPos = new List<Vector3>();
        private void OnDrawGizmos()
        {
            if (!m_Inited)
            {
                Init();
            }
            UpdateSetting();


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





            if (m_DrawCameraVec)//繪製Camera的Up跟Right
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(m_Origin, m_Pos);
                Gizmos.DrawLine(finalCastPoint, m_Pos);

                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(m_Pos, m_Pos + m_UpVec);
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(m_Pos, m_Pos + m_RightVec);
            }

            Vector3 sa = Vector3.zero, sb = Vector3.zero, sc = Vector3.zero, sd = Vector3.zero;
            Vector3 b = Vector3.zero, c = Vector3.zero, d = Vector3.zero;
            RaycastHit hit;
            const float Size = 0.06f;
            const float SizeSmall = 0.03f;
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
                    if (m_CornerPos.Count == 0 || (m_CornerPos.LastElement() - d).magnitude > 0.001f)
                    {
                        m_CornerPos.Add(d);
                    }
                }
            }

            {
                Ray ray = new Ray();
                Vector3 origin = new Vector3(0, m_Y, 0);//原點
                ray.origin = origin;
                ray.direction = m_FowardVec;
                //先找到往上的向量長度
                float height = m_DivPPU * m_Height;//單位高度(像素高度 * (1/pixel per unit))
                float width = m_DivPPU * m_Width;//單位高度(像素高度 * (1/pixel per unit))
                Vector3 upVec = 0.5f * height * m_UpVec;
                Vector3 rightVec = 0.5f * width * m_RightVec;
                if (m_DrawPreviewRay)
                {
                    if (Physics.Raycast(ray, out hit, MaxRayDis))
                    {
                        Gizmos.color = Color.red;

                        Vector3 end = hit.point;
                        Gizmos.DrawSphere(origin, Size);
                        Gizmos.DrawSphere(end, Size);

                        Gizmos.DrawLine(origin, end);
                        Gizmos.color = Color.green;
                        if (m_IntPos.y > 0)
                        {
                            for (int i = 1; i <= m_IntPos.y; i++)
                            {
                                Vector3 aDel = m_UpVecN * i * m_UpPixelSize;
                                Vector3 aS = origin + aDel;
                                Vector3 aE = end + aDel;

                                Gizmos.DrawSphere(aS, Size);
                                Gizmos.DrawSphere(aE, Size);

                                Gizmos.DrawLine(aS, aE);
                            }
                        }
                    }
                }


                if (m_DrawPreviewGrid)
                {
                    Vector3 LU = origin + upVec - rightVec;
                    Vector3 RU = origin + upVec + rightVec;
                    Vector3 LD = origin - upVec - rightVec;
                    Vector3 RD = origin - upVec + rightVec;

                    DrawGrid(LU, RU, LD, RD, Size, m_Width, m_Height, Vector3.zero, Color.yellow);
                    Vector3 LUE = LU;
                    Vector3 RUE = RU;
                    Vector3 LDE = LD;
                    Vector3 RDE = RD;
                    ray.origin = LU;
                    if (Physics.Raycast(ray, out hit, MaxRayDis)) LUE = hit.point;

                    ray.origin = RU;
                    if (Physics.Raycast(ray, out hit, MaxRayDis)) RUE = hit.point;

                    ray.origin = LD;
                    if (Physics.Raycast(ray, out hit, MaxRayDis)) LDE = hit.point;

                    ray.origin = RD;
                    if (Physics.Raycast(ray, out hit, MaxRayDis)) RDE = hit.point;

                    DrawGrid(LUE, RUE, LDE, RDE, Size, m_Width, m_Height, 0.001f * Vector3.up, Color.yellow);

                    //if(m_IntPos != Vector2Int.zero)
                    {
                        Vector2Int gridPos = m_IntPos;
                        gridPos.x += (m_Width + 1) / 2;
                        gridPos.y += (m_Height + 1) / 2;
                        Vector3 gridWorldSpacePos = GetGridPos(LU, RU, LD, RD, m_Width, m_Height, gridPos.x, gridPos.y);
                        Vector3 pos = origin + m_DelUp;
                        Vector3 pos2 = pos + m_DelFoward;

                        Gizmos.color = Color.red;
                        Gizmos.DrawLine(gridWorldSpacePos, pos);

                        Gizmos.color = new Color(0.5f, 1, 0.3f);
                        

                        Gizmos.DrawSphere(pos, SizeSmall);
                        Gizmos.DrawSphere(pos2, SizeSmall);
                        Gizmos.DrawLine(origin, pos);
                        Gizmos.DrawLine(pos, pos2);
                    }
                }


            }

            if (m_CornerPos.Count > 0)
            {
                foreach (var pos in m_CornerPos)
                {
                    Gizmos.color = Color.grey;

                    Gizmos.DrawSphere(pos, Size);
                }
            }
            
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
            if (m_DrawCameraGrid)
            {
                Gizmos.color = Color.red;
                Vector3 upVec = 0.001f * Vector3.up;

                DrawGrid(m_A, b, d, c, Size, m_Width, m_Height, upVec, Color.red);

                //for (int i = 1; i < m_Height; i++)
                //{
                //    float val = (float)i / m_Height;
                //    Gizmos.DrawLine(Vector3.Lerp(m_A, b, val) + upVec, Vector3.Lerp(d, c, val) + upVec);
                //}
                //for (int i = 1; i < m_Width; i++)
                //{
                //    float val = (float)i / m_Width;
                //    Gizmos.DrawLine(Vector3.Lerp(m_A, d, val) + upVec, Vector3.Lerp(b, c, val) + upVec);
                //}
            }


            //繪製Camera位置
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(m_Camera.transform.position, 0.03f);
                //Gizmos.DrawIcon(transform.position, "plus_3.png", false);
                Ray ray = new Ray();
                ray.origin = m_Camera.transform.position;
                ray.direction = m_Camera.transform.forward;
                if (Physics.Raycast(ray, out hit, MaxRayDis))//繪製Camera到地面的投射線
                {
                    Gizmos.DrawLine(ray.origin, hit.point);
                }
                int MidX = m_Width / 2;
                int MidY = m_Height / 2;
                m_OnGUILog.AppendLine($"MidX:{MidX},MidY:{MidY}");
                {//On pixel Origin
                    Gizmos.color = Color.gray;
                    ray = m_Camera.ScreenPointToRay(new Vector3(MidX, MidY, 0));
                    if (Physics.Raycast(ray, out hit, MaxRayDis))
                    {
                        Gizmos.DrawLine(ray.origin, hit.point);
                    }
                }
                {//On pixel Up
                    Gizmos.color = Color.green;
                    ray = m_Camera.ScreenPointToRay(new Vector3(MidX, MidY + 1,0));
                    if (Physics.Raycast(ray, out hit, MaxRayDis))
                    {
                        Gizmos.DrawLine(ray.origin, hit.point);
                    }
                }
                {//On pixel Down
                    Gizmos.color = Color.green;
                    ray = m_Camera.ScreenPointToRay(new Vector3(MidX, MidY - 1, 0));
                    if (Physics.Raycast(ray, out hit, MaxRayDis))
                    {
                        Gizmos.DrawLine(ray.origin, hit.point);
                    }
                }
            }
            //Debug.LogError($"a:{m_A},b:{b},c:{c},d:{d}");
        }
        static Vector3 GetGridPos(Vector3 LU, Vector3 RU, Vector3 LD, Vector3 RD, int widthSeg, int heightSeg, int x, int y)
        {
            float xVal = (float)x / widthSeg;
            float yVal = (float)y / heightSeg;

            Vector3 s = Vector3.Lerp(LD, LU, yVal);//左側起點
            Vector3 e = Vector3.Lerp(RD, RU, yVal);//右側終點
            return Vector3.Lerp(s, e, xVal);
        }
        static void DrawGrid(Vector3 LU, Vector3 RU, Vector3 LD, Vector3 RD, float sphereSize,int widthSeg, int heightSeg, Vector3 offsetVec, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(LU, sphereSize);
            Gizmos.DrawSphere(RU, sphereSize);
            Gizmos.DrawSphere(LD, sphereSize);
            Gizmos.DrawSphere(RD, sphereSize);

            for (int i = 0; i <= heightSeg; i++)
            {
                float val = (float)i / heightSeg;
                Gizmos.DrawLine(Vector3.Lerp(LU, LD, val) + offsetVec, Vector3.Lerp(RU, RD, val) + offsetVec);
            }
            for (int i = 0; i <= widthSeg; i++)
            {
                float val = (float)i / widthSeg;
                Gizmos.DrawLine(Vector3.Lerp(LU, RU, val) + offsetVec, Vector3.Lerp(LD, RD, val) + offsetVec);
            }
        }
        void UpdatePos2()
        {
            UpdateSetting();
            //float aY = transform.position.y;
            Vector3 finalPos = new Vector3(0, m_Y,0);
            Vector3 aDel = Vector3.zero;
            if (m_IntPos.y != 0)
            {
                float aCosVal = Vector3.Dot(m_UpVecN, m_UpVec);
                m_DelUp = m_UpVecN * m_IntPos.y * m_UpPixelSize;// * m_Test
                aDel += m_DelUp;
                //投影回Camera的Up
                Vector3 aOriginUP = m_UpVec * Vector3.Dot(m_DelUp, m_UpVec); //m_UpVec * m_IntPos.y * m_DivPPU;
                Vector3 aOriginFoward = m_DelUp - aOriginUP;
                float aValUp = Mathf.Sqrt(m_DelUp.magnitude) * m_DivPPU;

                Vector3 aDelVec = aOriginUP - m_DelUp;
                float aTestFoward = m_TestY;
                float aDelY = m_IntPos.y * aTestFoward;



                float aSegFoward = aCosVal / m_PixelPerfectCamera.assetsPPU;//0.08838f;Mathf.Sqrt(1/2f)
                float aDY = aDelY / aSegFoward;
                aDY -= Mathf.Floor(aDY);
                aDY *= aSegFoward;
                m_DelFoward = aDY * m_FowardVec;//測試用m_IntPos.y * m_TestY *

                int aFowardSegCount = Mathf.FloorToInt(aOriginFoward.magnitude / aSegFoward);//計算長度有多少像素
                float aFowardLen = aFowardSegCount * aSegFoward;
                Debug.LogError($"aTestF:{aTestFoward},m_IntPos.y:{m_IntPos.y},aDelY:{aDelY},aDY:{aDY},SegFoward:{aSegFoward},aCosVal:{aCosVal}" +
                    $", m_DelUp:{m_DelUp.ToStringDetailValue()}, aOriginUP:{aOriginUP.ToStringDetailValue()},aValUp:{aValUp}" +
                    $"\naOriginFoward.magnitude:{aOriginFoward.magnitude},aFowardSegCount:{aFowardSegCount},aFowardLen:{aFowardLen},m_DelFoward.magnitude:{m_DelFoward.magnitude}");


                
                aDel += m_DelFoward;//測試用
            }
            
            finalPos += aDel;

            Debug.LogError($"aUpVec:{m_UpVecN.ToStringDetailValue()},m_IntPos:{m_IntPos},m_UpPixelSize:{m_UpPixelSize},Cos:{m_Cos},m_Uplen:{m_Uplen},m_DivPPU:{m_DivPPU},aDel:{aDel.ToStringDetailValue()}" +
                $",m_Test:{m_Test}");



            transform.position = finalPos;//m_Pos
        }

        void UpdatePos3()
        {
            const float HalfSqrt = 0.70710678118f;//0.70710678118f = Mathf.Sqrt(1/2)
            m_OnGUILog.Clear();
            UpdateSetting();
            //float aY = transform.position.y;
            Vector3 finalPos = new Vector3(0, m_Y, 0);
            Vector3 aDel = Vector3.zero;
            if (m_IntPos.y != 0)//朝著CameraUp方向位移
            {
                float aCosVal = Vector3.Dot(m_UpVecN, m_UpVec);
                float aSinVal = Mathf.Sqrt(1f - aCosVal * aCosVal);
                float aDivCosVal = 1f / aCosVal;
                float aDivSinVal = 1f / aSinVal;
                
                Vector3 aUp = m_IntPos.y * m_UpVec * m_DivPPU;//往上位移的距離
                Vector3 aUpCast = Vector3.Dot(aUp, m_UpVecN) * aDivCosVal * aDivCosVal * m_UpVecN;
                Vector3 aFoward = aUpCast - aUp;

                m_DelUp = aUp;
                //https://www.rapidtables.com/calc/math/Arctan_Calculator.html

                float aFowardCosVal = Mathf.Abs(Vector3.Dot(m_FowardVec, Vector3.forward));
                if (aFowardCosVal <= HalfSqrt)//0.70710678118f = Mathf.Sqrt(1/2)
                {
                    aFowardCosVal = Mathf.Abs(Vector3.Dot(m_FowardVec, Vector3.right));
                }
                //float aFowardCosVal = Vector3.Dot(m_FowardVec, m_UpVecN);
                float aFowardSinVal = Mathf.Sqrt(1f - aFowardCosVal * aFowardCosVal);

                //Mathf.Sqrt(1 / 2f) *
                float aFVal = m_Test * 2f * aFowardCosVal;//Mathf.Sqrt(2f) * aFowardCosVal * aDivSinVal 
                float aFPPU = m_PPU * aFVal;//aCosVal;//m_TestY
                m_OnGUILog.AppendLine($"aFowardCosVal:{aFowardCosVal},aFowardSinVal:{aFowardSinVal},aFVal:{aFVal},aFoward:{aFoward.ToStringDetailValue()}");
                m_OnGUILog.AppendLine($"aCosVal:{aCosVal},aSinVal:{aSinVal},aDivCosVal:{aDivCosVal},aDivSinVal:{aDivSinVal}");
                int aFowardPixel = Mathf.RoundToInt(Vector3.Dot(aFoward, m_FowardVec) * aFPPU);

                Vector3 aFowardOffset = aFowardPixel * (1f/ aFPPU) * m_FowardVec;

                m_DelFoward = aFowardOffset;
                Debug.LogError($"m_PPU:{m_PPU},m_IntPos.y:{m_IntPos.y},aUp:{aUp.ToStringDetailValue()},aUpCast:{aUpCast.ToStringDetailValue()},aFoward:{aFoward.ToStringDetailValue()},m_DelUp:{m_DelUp.ToStringDetailValue()}" +
                    $"aFoward.magnitude:{aFoward.magnitude},aFowardOffset:{aFowardOffset.ToStringDetailValue()},aFowardPixel:{aFowardPixel},aFPPU:{aFPPU},aCosVal:{aCosVal},aSinVal:{aSinVal}");

                aDel += m_DelUp;
                aDel += m_DelFoward;


                //aDel += m_UpVec * m_DivPPU * aUpPixel;//aOriginUP;
                //aDel += m_FowardVec * m_DivPPU * aFowardPixel;
                //aDel += m_RightVec * m_DivPPU * aRightPixel;//aOriginRight;
            }

            finalPos += aDel;

            transform.position = finalPos;//m_Pos
        }
        private void OnGUI()
        {
            GUILayout.Label($"PPU:{m_PPU}", UCL_GUIStyle.LabelStyle);
            GUILayout.Label($"IntPos:{m_IntPos}", UCL_GUIStyle.LabelStyle);
            GUILayout.Label($"DelUp:{m_DelUp.ToStringDetailValue()}", UCL_GUIStyle.LabelStyle);
            GUILayout.Label($"DelFoward:{m_DelFoward.ToStringDetailValue()}", UCL_GUIStyle.LabelStyle);
            GUILayout.Label(m_OnGUILog.ToString(), UCL_GUIStyle.LabelStyle);
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

            //UpdatePos();
            UpdatePos3();
        }
    }
}
