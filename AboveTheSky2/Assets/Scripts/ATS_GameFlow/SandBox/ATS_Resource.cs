
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UnityEngine;

namespace ATS
{

    /// <summary>
    /// Sandbox中的實體資源(可被搬運)
    /// </summary>
    public class ATS_Resource : SandBoxBase, UCLI_ShortName
    {
        /// <summary>
        /// 資源尺寸(基於房間尺寸的比例)
        /// </summary>
        public const float ResourceSize = 0.35f;

        public enum ResourceState
        {
            /// <summary>
            /// 掉落中(生成時 或是 搬運中取消)
            /// </summary>
            Dropping,
            /// <summary>
            /// 掉在地上(可被搬運狀態)
            /// </summary>
            Dropped,
            /// <summary>
            /// 準備被搬運中(搬運前標記 避免被重複搬運)
            /// </summary>
            PrepareToHaul,
            /// <summary>
            /// 被搬運中(此時不存在於任何Cell中)
            /// </summary>
            Hauling,
        }
        

        public Cell p_Cell = null;

        public ResourceAmount m_ResourceAmount = new ResourceAmount();
        /// <summary>
        /// 所在位置
        /// </summary>
        public ATS_Vector3 m_Pos = new ATS_Vector3();
        /// <summary>
        /// 當前速度
        /// </summary>
        public ATS_Vector3 m_Vel = new ATS_Vector3();

        public ResourceState m_State = ResourceState.Dropping;
        public Texture2D Texture => m_ResourceAmount.Texture;
        public string GetShortName() => $"{m_ResourceAmount},{m_Pos}({m_State})";
        public override string ToString() => GetShortName();
        public ATS_Resource() { }
        public ATS_Resource(string iID, int iAmount)
        {
            m_ResourceAmount.Init(iID, iAmount);
        }

        public override void ContentOnGUI(UCL_ObjectDictionary iDic)
        {
            DrawOnGrid(RegionGrid);
        }
        public void DrawOnGrid(ATS_RegionGrid iGrid)
        {
            var aTexture = Texture;
            if (aTexture == null)
            {
                return;
            }
            var aRect = iGrid.GetCellRect(m_Pos.x - 0.5f * ResourceSize, m_Pos.y, ResourceSize, ResourceSize);
            //GUI.DrawTexture(aRect, aTexture);
            GUI.DrawTexture(aRect, aTexture);
            //GUI.Label(aRect, $"{m_ResourceAmount.m_Amount}");
        }
        /// <summary>
        /// 先移除之前的掉落地塊紀錄
        /// </summary>
        public void ClearCell()
        {
            if (p_Cell != null)//先移除之前的紀錄
            {
                p_Cell.m_Resources.Remove(this);
            }
        }
        /// <summary>
        /// 更新當前所在的地塊
        /// </summary>
        public void UpdateCell()
        {
            ClearCell();

            var aPos = m_Pos.ToVector2Int;
            p_Cell = Region.Cells[aPos.x, aPos.y];
            p_Cell.m_Resources.Add(this);//紀錄掉落位置
        }
        /// <summary>
        /// 把散落在地上的資源放入倉庫
        /// </summary>
        /// <param name="iResource"></param>
        public void AddToStorage()
        {
            Region.Data.m_Resources.AddToStorage(this);
        }
        public void SetState(ResourceState iState)
        {
            switch (iState)
            {
                case ResourceState.Dropped:
                    {
                        UpdateCell();
                        break;
                    }
                case ResourceState.PrepareToHaul:
                case ResourceState.Hauling:
                    {
                        ClearCell();
                        break;
                    }
            }
            m_State = iState;
        }
        public override void GameUpdate()
        {
            base.GameUpdate();


            switch (m_State)
            {
                case ResourceState.Dropping:
                    {
                        float aY = m_Pos.y;
                        int aFY = Mathf.FloorToInt(aY);
                        float aDY = aY - aFY;
                        if (aDY > ATS_Const.GroundHeight)//Drop
                        {
                            m_Vel.y += ATS_Const.Gravity;
                            //Debug.LogError($"aDY:{aDY},m_Vel.y:{m_Vel.y}");
                        }
                        m_Vel *= ATS_Const.Fraction;
                        aDY += m_Vel.y;
                        float aMinHeight = 0.2f * ATS_Const.GroundHeight;
                        if (aDY < aMinHeight)//掉落到地上
                        {
                            m_Vel.Reset();
                            //if (m_Vel.y > 0)
                            //{
                            //    m_Vel.y *= -ATS_Const.Bounciness;
                            //}
                            //aDY = aMinHeight;
                            if (aDY < 0) aDY = 0;
                            SetState(ResourceState.Dropped);
                        }
                        m_Pos.y = aFY + aDY;
                        break;
                    }


            }



        }
    }
}
