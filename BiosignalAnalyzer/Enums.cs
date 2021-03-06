﻿namespace BiosignalAnalyzer
{
    /// <summary>
    /// 列挙型が宣言されているクラスです．
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// 生理指標データのファイル・タイプです．
        /// </summary>
        public enum FileTypes
        {
            /// <summary>ECG データ</summary>
            ECG,
            /// <summary>SCR データ</summary>
            SCR,
            /// <summary>CDM データ</summary>
            CDM,
            /// <summary>RR データ</summary>
            RR,
            /// <summary>不明なファイル・タイプ</summary>
            Undefined
        }
    }
}
