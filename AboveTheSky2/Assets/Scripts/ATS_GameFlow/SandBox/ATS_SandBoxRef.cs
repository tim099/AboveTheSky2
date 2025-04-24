
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core;
using UCL.Core.JsonLib;
using UCL.Core.UI;
using UnityEngine;

namespace ATS
{
    public class ATS_SandBoxRef<T> : IJsonSerializable, UCLI_FieldOnGUI
        where T : class, ATSI_SandBox, new()
    {
        /// <summary>
        /// 不重複的Index
        /// Unique Index
        /// </summary>
        virtual public int Index
        {
            get
            {
                if (Value == null)
                {
                    return -1;
                }
                return Value.Index;
            }
        }
        virtual public T Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
            }
        }
        virtual public string TypeName => typeof(T).Name;

        private T m_Value = null;
        public override string ToString() => $"{base.ToString()}[{Index}]";
        virtual public JsonData SerializeToJson()
        {
            //m_Index = Index;
            return new JsonData(Index);
            //return JsonConvert.SaveFieldsToJsonUnityVer(this);
        }
        virtual public void DeserializeFromJson(JsonData iJson)
        {
            if (iJson == null)
            {
                return;
            }
            var aIndex = iJson.GetInt(-1);
            var aSandBox = ATS_SandBox.s_CurSaveSandBox;
            if(aIndex >= 0)
            {
                aSandBox.AddOnLoadEndAction(OnLoadEnd);//必須在全部讀檔結束時才能恢復Reference
                void OnLoadEnd()
                {
                    m_Value = aSandBox.GetSandBoxItemByIndex<T>(aIndex, TypeName);
                }
            }

            //JsonConvert.LoadFieldFromJsonUnityVer(this, iJson);
        }
        virtual public string GetDisplayName(string iFieldName)
        {
            return $"[{Index}]{iFieldName}";
        }
        /// <summary>
        /// return new data if the data of field altered
        /// </summary>
        /// <param name="iFieldName"></param>
        /// <param name="iEditTmpDatas"></param>
        /// <returns></returns>
        virtual public object OnGUI(string iFieldName, UCL_ObjectDictionary iDataDic, UCL_GUILayout.DrawObjectParams iParams)
        {
            //GUILayout.Label($"{iFieldName}:{Index}", UCL_GUIStyle.LabelStyle);
            if(m_Value != null)
            {
                UCL_GUILayout.DrawObjectData(m_Value, iDataDic.GetSubDic("Value"), GetDisplayName(iFieldName));
            }
            else
            {
                GUILayout.Label(GetDisplayName(iFieldName), UCL_GUIStyle.LabelStyle);
            }
            return this;
        }
    }
}
