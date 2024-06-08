
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.MathLib;
using UnityEngine;

namespace ATS
{
    public enum MinionState
    {
        /// <summary>
        /// 閒晃
        /// </summary>
        Idle,

    }

    /// <summary>
    /// 基礎單位(船員或其他動物)
    /// </summary>
    public class ATS_Minion : SandBoxBase
    {
        public const float GroundHeight = 0.1f;



        /// <summary>
        /// 單位類型
        /// </summary>
        public ATS_CreatureDataEntry m_CreatureDataEntry = new ATS_CreatureDataEntry();
        /// <summary>
        /// 座標對應到飛船實際的格子 例如3.5代表在X=3格子的0.5位置(中間)
        /// </summary>
        public float m_X;
        public float m_Y;
        public float m_Z;
        public bool m_Moved = false;
        /// <summary>
        /// 目前在哪個Cell(X座標)
        /// </summary>
        public int PosX => Mathf.FloorToInt(m_X);
        /// <summary>
        /// 目前在哪個Cell(Y座標)
        /// </summary>
        public int PosY => Mathf.FloorToInt(m_Y);

        public MinionState m_MinionState = MinionState.Idle;


        public ATS_Minion() { }
        public ATS_Minion(string iID, float iX, float iY) {
            m_CreatureDataEntry.ID = iID;
            m_X = iX;
            m_Y = iY;
            m_Z = 0;
        }
        #region Getter
        public ATS_CreatureData CreatureData => m_CreatureDataEntry.GetData();
        //{
        //    get
        //    {
        //        if(m_CreatureData == null)
        //        {
        //            m_CreatureData = m_CreatureDataEntry.GetData();
        //        }
        //        return m_CreatureData;
        //    }
        //}

        private ATS_CreatureData m_CreatureData = null;

        public Texture2D Texture => CreatureData.Texture;
        public float Width => CreatureData.m_Width;
        public float Height => CreatureData.m_Height;
        #endregion




        public List<(PathState, ATS_Vector2Int)> m_DebugPaths = new ();
        public PathState m_Dir = PathState.None;
        public float m_VelX = 0f;
        public float m_VelY = 0f;
        public override void ContentOnGUI(UCL_ObjectDictionary iDic)
        {
            DrawOnGrid(RegionGrid);
        }
        public override void GameUpdate()
        {
            base.GameUpdate();

            switch (m_MinionState)
            {
                case MinionState.Idle:
                    {
                        IdleUpdate();
                        break;
                    }
            }

        }

        private ATS_Path m_Path = null;
        private int m_Timer = 0;
        private int m_At = 0;
        private Vector2? m_Pos = null;
        private Vector2? m_NextPos = null;
        virtual protected void IdleUpdate()
        {
            const int Counter = 30;
            if(m_Path == null)
            {
                int aTargetDistance = UCL_Random.Instance.Range(2, 5);
                //return true if find target
                bool CheckNode(Cell iCell, PathNode iNode)
                {
                    if(iNode.m_Distance >= aTargetDistance)
                    {
                        return true;
                    }
                    return false;
                }
                m_Path = PathFinder.SearchPath(m_X, m_Y, CheckNode);
                //Debug.LogError($"({m_X},{m_Y}), m_Path:{m_Path.m_Path.ConcatString(iPos => $"{iPos.m_X},{iPos.m_Y}")}");
                m_Timer = Counter;
                m_At = 0;
                m_NextPos = null;
                m_Pos = new Vector2(m_X, m_Y);
            }
            else
            {
                m_Timer++;
                if (m_Timer >= Counter)
                {
                    m_Timer = 0;
                    var aPath = m_Path.m_Path;
                    if (m_At < aPath.Count)
                    {
                        if(m_NextPos.HasValue)
                        {
                            m_Pos = m_NextPos.Value;
                        }
                        

                        var aPos = aPath[m_At];
                        m_NextPos = new Vector2(aPos.m_X + 0.5f, aPos.m_Y + Random.Range(0.01f, GroundHeight));

                        //m_X = aPos.m_X + 0.5f;
                        //m_Y = aPos.m_Y;
                    }
                    else//End
                    {
                        m_Path = null;
                    }

                    m_At++;
                }
                else
                {
                    if (m_Pos.HasValue && m_NextPos.HasValue)
                    {
                        var aPos = Vector2.Lerp(m_Pos.Value, m_NextPos.Value, ((float)m_Timer / Counter));
                        m_X = aPos.x;
                        m_Y = aPos.y;
                    }
                }
            }



            //PathFinder.FindPath(m_X, m_Y);
            //int aCurX = PosX;
            //int aCurY = PosY;

            

            //if (m_Dir == PathState.None)
            //{
            //    var aPaths = PathFinder.GetPaths(aCurX, aCurY);
            //    if (!aPaths.IsNullOrEmpty())
            //    {
            //        m_Dir = UCL_Random.Instance.RandomPick(aPaths);
            //        //m_DebugPaths.Add((m_Dir, new ATS_Vector2Int(aCurX, aCurY)));
            //        Debug.LogError($"IdleUpdate m_Dir:{m_Dir},({aCurX},{aCurY})");
            //    }
            //    else
            //    {
            //        Debug.LogError($"IdleUpdate aPaths.IsNullOrEmpty(),({aCurX},{aCurY})");
            //    }
            //}
            //const float Acc = 0.003f;
            //const float Dec = 0.98f;
            //float aX = m_X - aCurX;
            //bool aInCenter = aX <= 0.6f && aX >= 0.4f;
            //switch (m_Dir)
            //{
            //    case PathState.Right:
            //        {
            //            m_VelX += Acc;
            //            m_VelY = 0;
            //            break;
            //        }
            //    case PathState.Left:
            //        {
            //            m_VelX -= Acc;
            //            m_VelY = 0;
            //            break;
            //        }
            //    case PathState.Up:
            //        {
            //            //必須先移動到中央
            //            if(aX < 0.4f)
            //            {
            //                m_VelX += Acc;
            //                m_VelY = 0;
            //            }
            //            else if(aX > 0.6f)
            //            {
            //                m_VelX -= Acc;
            //                m_VelY = 0;
            //            }
            //            else//上下移動
            //            {
            //                m_VelX = 0;
            //                m_VelY += Acc;
            //            }
            //            break;
            //        }
            //    case PathState.Down:
            //        {
            //            //必須先移動到中央
            //            if (aX < 0.4f)
            //            {
            //                m_VelX += Acc;
            //                m_VelY = 0;
            //            }
            //            else if (aX > 0.6f)
            //            {
            //                m_VelX -= Acc;
            //                m_VelY = 0;
            //            }
            //            else//上下移動
            //            {
            //                m_VelX = 0;
            //                m_VelY -= Acc;
            //            }
            //            break;
            //        }
            //}
            //m_VelY *= Dec;
            //m_VelX *= Dec;
            //m_X += m_VelX;
            //m_Y += m_VelY;

            //if (PosX != aCurX || PosY != aCurY)//已經移動到下一格
            //{
            //    m_Moved = true;
            //    //m_VelX = 0;
            //    //m_VelY = 0;
            //    //m_Dir = PathState.None;
            //}
            //if (aInCenter && m_Moved)
            //{
            //    m_VelX = 0;
            //    m_VelY = 0;
            //    m_Dir = PathState.None;
            //    m_Moved = false;
            //}
        }
        public void DrawOnGrid(ATS_RegionGrid iGrid)
        {
            var aTexture = Texture;
            if (aTexture == null)
            {
                return;
            }
            var aRect = iGrid.GetCellRect(m_X - 0.5f * Width, m_Y, Width, Height);
            //GUI.DrawTexture(aRect, aTexture);
            if (m_VelX < 0)
            {
                GUI.DrawTextureWithTexCoords(aRect, aTexture, new Rect(1, 0, -1, 1));//Inverse
            }
            else
            {
                GUI.DrawTexture(aRect, aTexture);
            }
        }
    }
}
