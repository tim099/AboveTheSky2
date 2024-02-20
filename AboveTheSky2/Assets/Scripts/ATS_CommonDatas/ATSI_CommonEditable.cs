using System.Collections;
using System.Collections.Generic;
using UCL.Core.JsonLib;
using UnityEngine;

namespace ATS
{
    public interface ATSI_ID
    {
        /// <summary>
        /// 生成物品的唯一ID
        /// </summary>
        string ID { get; set; }
    }
    public interface ATSI_CommonEditable : IJsonSerializable, ATSI_ID
    {
        /// <summary>
        /// 路徑
        /// </summary>
        string SavePath { get; }
        /// <summary>
        /// 存檔
        /// </summary>
        JsonData Save();
        /// <summary>
        /// 根據ID刪除資料
        /// </summary>
        /// <param name="iID"></param>
        void Delete(string iID);
        /// <summary>
        /// 繪製編輯器
        /// </summary>
        void OnGUI(UCL.Core.UCL_ObjectDictionary iDataDic);
        /// <summary>
        /// 複製一份(避免編輯時改到原本的資料
        /// </summary>
        /// <returns></returns>
        ATSI_CommonEditable CloneInstance();

        /// <summary>
        /// 清除緩存
        /// </summary>
        void ClearCache();

        DataType DataType { get; set; }
    }
}
