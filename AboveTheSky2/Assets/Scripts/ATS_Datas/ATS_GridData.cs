
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.MathLib;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    public class ATS_GridData : UCL.Core.JsonLib.UnityJsonSerializable, UCLI_FieldOnGUI, ISandBox
    {
        public const float CellSize = 32f;
        public class RuntimeData
        {
            public int m_CurIndex = 0;
        }

        public int m_Width;
        public int m_Height;
        

        public List<int> m_GridIndexs = new List<int>();


        public int[,] Grid { get; private set; } = null;

        protected RuntimeData RTData { get; set; } = null;
        /// <summary>
        /// DrawGrid時GUILayoutUtility.GetRect回傳的Rect
        /// </summary>
        protected Rect GridRect { get; set; } = Rect.zero;//new Rect(0, 0, 1, 1);
        protected IList<string> GridIDs { get; set; }//IReadOnlyList
        public virtual int MaxIndex => 2;

        public override JsonData SerializeToJson()
        {
            if (Grid != null)
            {
                m_GridIndexs.Clear();
                for (int y = 0; y < m_Height; y++)//Save Grid to m_GridIndexs
                {
                    for (int x = 0; x < m_Width; x++)
                    {
                        m_GridIndexs.Add(Grid[x, y]);
                    }
                }
            }

            return base.SerializeToJson();
        }
        public override void DeserializeFromJson(JsonData iJson)
        {
            base.DeserializeFromJson(iJson);
            RefreshGrid();
            for (int y = 0; y < m_Height; y++)//Load Grid from m_GridIndexs
            {
                for (int x = 0; x < m_Width; x++)
                {
                    int aIndex = x + y * m_Width;
                    int aGridIndex = 0;
                    if (m_GridIndexs.Count > aIndex)
                    {
                        aGridIndex = m_GridIndexs[aIndex];
                    }
                    Grid[x, y] = aGridIndex;
                }
            }
        }
        public void RefreshGrid()
        {
            if (Grid == null || Grid.GetLength(0) != m_Width || Grid.GetLength(1) != m_Height)
            {
                if (m_Width <= 1) m_Width = 1;
                if (m_Height <= 1) m_Height = 1;
                Grid = new int[m_Width, m_Height];
            }

        }
        /// <summary>
        /// return new data if the data of field altered
        /// </summary>
        /// <param name="iFieldName"></param>
        /// <param name="iDataDic"></param>
        /// <returns></returns>
        virtual public object OnGUI(string iFieldName, UCL_ObjectDictionary iDataDic)
        {
            UCL_GUILayout.DrawObjExSetting aDrawObjExSetting = new UCL_GUILayout.DrawObjExSetting();
            aDrawObjExSetting.OnShowField = () =>
            {
                RefreshGrid();
                EditGrid(iDataDic.GetSubDic("Grid"));
            };
            UCL_GUILayout.DrawField(this, iDataDic, iFieldName, iDrawObjExSetting: aDrawObjExSetting);

            //UCL_GUILayout.DrawableTexture



            return this;
        }
        virtual public Rect GetCellRect(int x, int y, float width, float height)
        {
            float aCellWidth = GridRect.width / m_Width;//單位寬度
            float aCellHeight = GridRect.height / m_Height;//單位高度
            //Debug.LogError($"aCellWidth:{aCellWidth},GridRect.width:{GridRect.width},m_Width:{m_Width}" +
                //$",aCellHeight:{aCellHeight},GridRect.height:{GridRect.height},m_Height:{m_Height}");
            return new Rect(GridRect.position.x + x * aCellWidth,//X
                GridRect.position.y + GridRect.height - (y + height) * aCellHeight,//Y

                width * aCellWidth,//Width
                height * aCellHeight);//Height
        }
        virtual protected void EditGrid(UCL_ObjectDictionary iDataDic)
        {
            DrawGrid(iDataDic);
        }

        virtual protected void GetGridRect(float iCellSize)
        {
            float aWidth = m_Width * iCellSize;
            float aHeight = m_Height * iCellSize;
            GridRect = GUILayoutUtility.GetRect(aWidth, aHeight, GUILayout.ExpandWidth(false));
        }
        virtual protected void DrawCells()
        {
            GUIStyle aButtonStyle = UCL_GUIStyle.GetButtonStyle(Color.white, 16);
            for (int y = 0; y < m_Height; y++)
            {
                for (int x = 0; x < m_Width; x++)
                {
                    //aCellRect.position = GridRect.position + new Vector2(x * aGridSize, y * aGridSize);
                    var aCellRect = GetCellRect(x, y, 1, 1);
                    DrawCell(GridRect, aCellRect, x, y, aButtonStyle);
                }
            }
        }
        virtual public void DrawGrid(UCL_ObjectDictionary iDataDic)
        {
            GetGridRect(UCL_GUIStyle.GetScaledSize(CellSize));
            DrawCells();
        }
        virtual protected void DrawCell(Rect iRect, Rect iCellRect, int x, int y, GUIStyle iButtonStyle)
        {
            if (GUI.Button(iCellRect, $"{Grid[x, y]}", iButtonStyle))//if (GUILayout.Button($"{Grid[x, y]}", aButtonStyle, aWidthOption, aHeightOption))
            {
                int aVal = Grid[x, y] + 1;
                if (aVal >= MaxIndex)
                {
                    aVal = 0;
                }
                Grid[x, y] = aVal;
            }
        }
        #region SandBox
        public void Init(ATS_SandBox iSandBox)
        {
            RTData = new RuntimeData();
        }
        /// <summary>
        /// Logic base update
        /// </summary>
        public void GameUpdate()
        {
            ++RTData.m_CurIndex;
        }
        /// <summary>
        /// 用在ATS_SandboxPage
        /// </summary>
        public void ContentOnGUI(UCL_ObjectDictionary iDic)
        {
            //if (RTData == null)
            //{
            //    return;
            //}

            DrawGrid(iDic.GetSubDic("DrawGrid"));
        }
        #endregion
    }


}
