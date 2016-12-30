using System;
using System.Windows.Forms;

namespace BiosignalAnalyzer
{
    public partial class Main : Form
    {
        #region フィールド

        #endregion

        #region プロパティ

        /// <summary>読み込んでいるディレクトリのパス．</summary>
        public string DirectoryPath { get; private set; }
        
        /// <summary>CDM データ csv のファイル名．</summary>
        public string CDMFileName { get; private set; }

        /// <summary>ECG データ csv のファイル名．</summary>
        public string ECGFileName { get; private set; }

        /// <summary>SCR データ csv のファイル名．</summary>
        public string SCRFileName { get; private set; }

        /// <summary>収録日時．</summary>
        public string RecordDate { get; private set; }

        /// <summary>被験者 ID．</summary>
        public string ParticipantID { get; private set; }

        /// <summary>被験者名．</summary>
        public string ParticipantName { get; private set; }

        /// <summary>CDM Analyzer の LF 周波数最小値．</summary>
        public float LFFrequencyMin { get; private set; }

        /// <summary>CDM Analyzer の LF 周波数最大値．</summary>
        public float LFFrequencyMax { get; private set; }

        /// <summary>CDM Analyzer の HF 周波数最小値．</summary>
        public float HFFrequencyMin { get; private set; }

        /// <summary>CDM Analyzer の HF 周波数最大値．</summary>
        public float HFFrequencyMax { get; private set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタです．
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }

        #endregion

        #region private メソッド

        /// <summary>
        /// ディレクトリを選択し，生理指標データを読み込みます．
        /// </summary>
        private void LoadFiles()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "生理指標データが保存されているフォルダを選択してください．";
                dialog.SelectedPath = Environment.CurrentDirectory;
                dialog.ShowNewFolderButton = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show(dialog.SelectedPath);
                }
            }
        }

        #endregion

        #region イベントハンドラ

        private void Main_Load(object sender, System.EventArgs e)
        {
            ActiveControl = null;
        }

        private void バージョン情報ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            using (var dialog = new VersionInfo())
            {
                dialog.StartPosition = FormStartPosition.CenterScreen;
                dialog.ShowDialog();
            }
        }

        private void 開くOToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            LoadFiles();
        }

        #endregion
    }
}
