
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.LocalizeLib;
using UCL.Core.Page;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    public class ATS_GridData : UCL.Core.JsonLib.UnityJsonSerializable, UCLI_FieldOnGUI
    {
        public int m_Width;
        public int m_Height;
        public int m_MaxIndex = 2;

        public List<int> m_GridIndexs = new List<int>();


        public int[,] m_Grid { get; private set; } = null;


        public override JsonData SerializeToJson()
        {
            if(m_Grid != null)
            {
                m_GridIndexs.Clear();
                for (int y = 0; y < m_Height; y++)//Save Grid to m_GridIndexs
                {
                    for (int x = 0; x < m_Width; x++)
                    {
                        m_GridIndexs.Add(m_Grid[x, y]);
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
                    if(m_GridIndexs.Count > aIndex)
                    {
                        aGridIndex = m_GridIndexs[aIndex];
                    }
                    m_Grid[x, y] = aGridIndex;
                }
            }
        }
        public void RefreshGrid()
        {
            if (m_Grid == null || m_Grid.GetLength(0) != m_Width || m_Grid.GetLength(1) != m_Height)
            {
                if (m_Width <= 1) m_Width = 1;
                if (m_Height <= 1) m_Height = 1;
                m_Grid = new int[m_Width, m_Height];
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

                using (var aScope = new GUILayout.VerticalScope())
                {
                    float aSize = UCL_GUIStyle.GetScaledSize(32);
                    GUILayoutOption aWidthOption = GUILayout.Width(aSize);
                    GUILayoutOption aHeightOption = GUILayout.Height(aSize);
                    var aButtonStyle = UCL_GUIStyle.GetButtonStyle(Color.white, 16);
                    for (int y = 0; y < m_Height; y++)
                    {
                        using (var aScope2 = new GUILayout.HorizontalScope())
                        {
                            for (int x = 0; x < m_Width; x++)
                            {
                                if (GUILayout.Button($"{m_Grid[x, y]}", aButtonStyle, aWidthOption, aHeightOption))
                                {
                                    int aVal = m_Grid[x, y] + 1;
                                    if (aVal >= m_MaxIndex)
                                    {
                                        aVal = 0;
                                    }
                                    m_Grid[x, y] = aVal;
                                }
                            }
                        }
                    }
                }
            };
            UCL_GUILayout.DrawField(this, iDataDic, iFieldName, iDrawObjExSetting: aDrawObjExSetting);

            //UCL_GUILayout.DrawableTexture



            return this;
        }
    }
    public class ATS_Building : UCL_Asset<ATS_Building>
    {
        /// <summary>
        /// 建設工作量 0或以下代表無須建造過程
        /// </summary>
        public int m_Work = 100;
        /// <summary>
        /// 建造材料
        /// </summary>
        public List<ATS_ResourceData> m_Consume = new List<ATS_ResourceData>();

        public List<ATS_RecipeEntry> m_Recipes = new ();

        public ATS_GridData m_GridData = new ATS_GridData();

        public override void OnGUI(UCL_ObjectDictionary iDataDic)
        {
            base.OnGUI(iDataDic);

        }
    }
}
