using System.Collections.Generic;

namespace BiosignalAnalyzer
{
    /// <summary>
    /// 読み込んだ生理指標データに関する情報を保存するクラスです．
    /// </summary>
    public static class DataInfo
    {
        #region プロパティ

        /// <summary>読み込んでいるディレクトリのパス．</summary>
        public static string DirectoryPath { get; set; }

        /// <summary>CDM データ csv のファイル名．</summary>
        public static Dictionary<Enums.FileTypes, string> FileNames { get; set; }

        /// <summary>収録日時．</summary>
        public static string RecordDate { get; set; }

        /// <summary>被験者 ID．</summary>
        public static string ParticipantID { get; set; }

        /// <summary>被験者名．</summary>
        public static string ParticipantName { get; set; }

        /// <summary>CDM Analyzer の LF 周波数最小値．</summary>
        public static float LFFrequencyMin { get; set; }

        /// <summary>CDM Analyzer の LF 周波数最大値．</summary>
        public static float LFFrequencyMax { get; set; }

        /// <summary>CDM Analyzer の HF 周波数最小値．</summary>
        public static float HFFrequencyMin { get; set; }

        /// <summary>CDM Analyzer の HF 周波数最大値．</summary>
        public static float HFFrequencyMax { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// 静的コンストラクタです．
        /// </summary>
        static DataInfo()
        {
            FileNames = new Dictionary<Enums.FileTypes, string>();
        }

        #endregion
    }
}
