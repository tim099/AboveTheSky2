
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.MathLib;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    public enum MinionState
    {
        /// <summary>
        /// 閒晃
        /// </summary>
        Idle,

        /// <summary>
        /// 執行所有的m_Jobs
        /// </summary>
        Working,

        /// <summary>
        /// 睡眠
        /// </summary>
        Sleep,
    }

    /// <summary>
    /// 基礎單位(船員或其他動物)
    /// </summary>
    public class ATS_Minion : ATS_SandBoxBase
    {
        /// <summary>
        /// 移動相關資料
        /// </summary>
        public class MoveData
        {
            /// <summary>
            /// 當前路徑
            /// </summary>
            public ATS_Path m_Path = null;
            public ATS_Vector3 m_TargetPos = null;

            public void Clear()
            {
                m_Path = null;
                m_TargetPos = null;
            }
        }
        /// <summary>
        /// 單位類型
        /// </summary>
        public ATS_CreatureDataEntry m_CreatureDataEntry = new ATS_CreatureDataEntry();
        /// <summary>
        /// 座標對應到飛船實際的格子 例如3.5代表在X=3格子的0.5位置(中間)
        /// </summary>
        public ATS_Vector3 m_Pos = new ATS_Vector3();
        /// <summary>
        /// 移動相關資料
        /// </summary>
        public MoveData m_MoveData = new MoveData();
        public MinionState m_State = MinionState.Idle;
        /// <summary>
        /// 當前所有的Job
        /// </summary>
        public List<ATS_JobRef> m_Jobs = new List<ATS_JobRef>();
        /// <summary>
        /// 目前在哪個Cell(X,Y)
        /// </summary>
        public ATS_Vector2Int PosInt => m_Pos.ToVector2Int;

        


        public ATS_Minion() { }
        public ATS_Minion(string iID, float iX, float iY) {
            m_CreatureDataEntry.ID = iID;
            m_Pos.x = iX;
            m_Pos.y = iY;
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

        //private ATS_CreatureData m_CreatureData = null;

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

            switch (m_State)
            {
                case MinionState.Idle:
                    {
                        IdleUpdate();
                        break;
                    }
                case MinionState.Working:
                    {
                        WorkingUpdate();
                        break;
                    }
            }

        }
        public void SetState(MinionState iState)
        {
            switch (iState)
            {
                //case MinionState.Working:
                //    {
                //        m_MoveData.Clear();
                //        break;
                //    }
                default:
                    {
                        m_MoveData.Clear();
                        break;
                    }
            }
            m_State = iState;
        }
        protected void WorkingUpdate()
        {
            if (m_Jobs.IsNullOrEmpty())
            {
                SetState(MinionState.Idle);
                return;
            }
            ATS_Job aCurJob = m_Jobs[0].Value;
            if(aCurJob == null)//Bug!!
            {
                Debug.LogError("WorkingUpdate aCurJob == null");
                m_Jobs.RemoveAt(0);
                return;
            }
            if(aCurJob.m_JobState == ATS_Job.JobState.Pending)
            {
                aCurJob.Start();
            }
            aCurJob.WorkingUpdate(this);
            if(aCurJob.Complete || aCurJob.Cancel)//工作已完成 或 取消
            {
                m_Jobs.RemoveAt(0);
                aCurJob.End();
            }
        }
        protected void IdleUpdate()
        {
            
            if(m_MoveData.m_Path == null)
            {
                {
                    bool SearchJob(Cell iCell, PathNode iPathNode)
                    {
                        if (!iCell.m_Jobs.IsNullOrEmpty())
                        {
                            return true;
                        }
                        return false;
                    }
                    //先嘗試找工作
                    var aResult = PathFinder.Search(m_Pos.x, m_Pos.y, SearchJob);
                    if (!aResult.IsNullOrEmpty())
                    {
                        var aCell = aResult[0].Item1;
                        if(!aCell.m_Jobs.IsNullOrEmpty())//找到工作
                        {
                            var aJob = aCell.m_Jobs[0];
                            aCell.m_Jobs.RemoveAt(0);

                            m_Jobs.Add(aJob);
                            SetState(MinionState.Working);
                            return;
                        }
                    }
                }

                int aTargetDistance = UCL_Random.Instance.Range(2, 8);
                //return true if find target
                int CheckNode(Cell iCell, PathNode iNode)
                {
                    if (iNode.m_Distance >= aTargetDistance)//距離滿足aTargetDistance 找到目標位置
                    {
                        return 0;
                    }
                    return aTargetDistance - iNode.m_Distance;//尚未找到目標 回傳與目標的距離
                }
                m_MoveData.m_Path = PathFinder.SearchPath(m_Pos.x, m_Pos.y, CheckNode);
                //Debug.LogError($"({m_Pos.x},{m_Pos.y}), m_Path:{m_Path.m_Path.ConcatString(iPos => $"{iPos.m_Pos.x},{iPos.m_Pos.y}")}");
            }
            else
            {
                MoveUpdate();
            }

        }
        /// <summary>
        /// 若走到終點則回傳true
        /// </summary>
        /// <returns></returns>
        public bool MoveUpdate()
        {
            var aCurPath = m_MoveData.m_Path;
            if (aCurPath == null)
            {
                return true;
            }
            var aPath = aCurPath.m_Path;
            const float Vel = 0.02f;
            const float Offset = 1.5f * Vel;
            //已經到達當前目標位置 尋找下一個位置
            if (m_MoveData.m_TargetPos == null)
            {
                if (aPath.IsNullOrEmpty())//已經走整個路徑
                {
                    m_MoveData.Clear();
                    return true;
                }

                var aNextPos = aPath[0];
                aPath.RemoveAt(0);
                m_MoveData.m_TargetPos = aNextPos;
            }


            var aTargetPos = m_MoveData.m_TargetPos;//目標位置(下一格)
            float aDx = aTargetPos.x - m_Pos.x;
            if (Mathf.Abs(aDx) <= Offset)
            {//水平位置已到達 判斷是否需要上下移動
                m_Pos.x = aTargetPos.x;

                float aDy = aTargetPos.y - m_Pos.y;
                if (Mathf.Abs(aDy) <= Offset)//抵達目標
                {
                    m_Pos.y = aTargetPos.y;
                    m_MoveData.m_TargetPos = null;
                }
                else//先進行上下移動
                {
                    if (aDy > 0)
                    {
                        m_Pos.y += Vel;
                    }
                    else
                    {
                        m_Pos.y -= Vel;
                    }
                }

            }
            else//先進行水平移動
            {
                if(aDx > 0)
                {
                    m_Pos.x += Vel;
                }
                else
                {
                    m_Pos.x -= Vel;
                }
            }
            return false;
        }
        public void DrawOnGrid(ATS_RegionGrid iGrid)
        {
            var aTexture = Texture;
            if (aTexture == null)
            {
                return;
            }
            var aRect = iGrid.GetCenterCellRect(m_Pos.x, m_Pos.y, Width, Height);
            //GUI.DrawTexture(aRect, aTexture);
            if (m_VelX < 0)
            {
                GUI.DrawTextureWithTexCoords(aRect, aTexture, new Rect(1, 0, -1, 1));//Inverse
            }
            else
            {
                GUI.DrawTexture(aRect, aTexture);
            }

            var aTargetPos = m_MoveData.m_TargetPos;//目標位置(下一格)
            if (aTargetPos != null)
            {
                aRect = iGrid.GetCenterCellRect(aTargetPos.x, aTargetPos.y, 0.3f, 0.3f);
                UCL_GUIStyle.PushGUIColor(UCL_Color.Half.Green);
                GUI.DrawTexture(aRect, ATS_StaticTextures.White);
                UCL_GUIStyle.PopGUIColor();
            }
        }
    }
}
