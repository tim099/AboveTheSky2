
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
using System.Collections;
using System.Collections.Generic;
using UCL.Core.JsonLib;
using UnityEngine;

namespace ATS
{
    public class ATS_SandBoxRef<T> : IJsonSerializable
        where T : class, ATSI_SandBox, new()
    {
        /// <summary>
        /// 不能重複的Index
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

        private T m_Value = null;

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
                    m_Value = aSandBox.GetSandBoxItemByIndex<T>(aIndex);
                }
            }

            //JsonConvert.LoadFieldFromJsonUnityVer(this, iJson);
        }
    }
}
