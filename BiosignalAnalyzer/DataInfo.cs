using System;
using System.Collections.Generic;

namespace BiosignalAnalyzer
{
    /// <summary>
    /// 読み込んだ生理指標データに関する情報を保存するクラスです．
    /// </summary>
    public static class DataInfo
    {
        #region プロパティ

        /// <summary>CDM データ csv のファイル名．</summary>
        public static Dictionary<Enums.FileTypes, string> FileNames { get; set; }

        /// <summary>読み込んでいるディレクトリのパス．</summary>
        public static string DirectoryPath { get; set; }

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

        /// <summary>CDM データに記された LF データの時間・値のペア</summary>
        public static List<KeyValuePair<int, float>> LFData;

        /// <summary>CDM データに記された HF データの時間・値のペア</summary>
        public static List<KeyValuePair<int, float>> HFData;

        /// <summary>CDM データに記された LF/HF データの時間・値のペア</summary>
        public static List<KeyValuePair<int, float>> LFHFData;

        /// <summary>ECG データの時間・値のペア</summary>
        public static List<KeyValuePair<int, float>> ECGData;

        /// <summary>SCR データの時間・値のペア</summary>
        public static List<KeyValuePair<int, float>> SCRData;

        /// <summary>RRI データの時間・値のペア</summary>
        public static List<KeyValuePair<int, float>> RRIData;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// 静的コンストラクタです．
        /// </summary>
        static DataInfo()
        {
            Reset();
        }

        #endregion

        #region public メソッド

        /// <summary>
        /// 読み込んだデータ内容をリセットします．
        /// </summary>
        public static void Reset()
        {
            FileNames = new Dictionary<Enums.FileTypes, string>();
            DirectoryPath = "";
            RecordDate = "";
            ParticipantID = "";
            ParticipantName = "";
            LFData = new List<KeyValuePair<int, float>>();
            HFData = new List<KeyValuePair<int, float>>();
            LFHFData = new List<KeyValuePair<int, float>>();
            ECGData = new List<KeyValuePair<int, float>>();
            SCRData = new List<KeyValuePair<int, float>>();
            RRIData = new List<KeyValuePair<int, float>>();
        }

        /// <summary>
        /// xxx:xx:xx.xxx 形式の時間文字列を整数値に変換します．
        /// </summary>
        /// <param name="timeStr">変換対象の時間文字列．</param>
        /// <returns>変換された整数値．負の数の場合，例外が発生している．</returns>
        public static int TimeToInt(string timeStr)
        {
            var s = timeStr.Split(':');
            try
            {
                return int.Parse(s[0]) * 3600000 + int.Parse(s[1]) * 60000 + (int)(float.Parse(s[2]) * 1000);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 整数値を x:xx:xx.xxx 形式の時間文字列に変換します．
        /// </summary>
        /// <param name="time">変換対象の整数値．</param>
        /// <returns>変換された時間文字列．空文字列の場合，例外が発生している．</returns>
        public static string IntToTime(int time)
        {
            return time < 0 ? "" : String.Format("{0}:{1:00}:{2:00.000}", time / 3600000, (time / 60000) % 60, time % 60000 / 1000.0);
        }

        #endregion
    }
}
