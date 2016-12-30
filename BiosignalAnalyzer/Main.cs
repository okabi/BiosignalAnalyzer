using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace BiosignalAnalyzer
{
    /// <summary>
    /// メインフォームのクラスです．
    /// </summary>
    public partial class Main : Form
    {
        #region フィールド

        /// <summary>表示区間の秒数．</summary>
        private int __intervalSecond;

        #endregion

        #region プロパティ

        /// <summary>表示区間の秒数．</summary>
        public int IntervalSecond
        {
            get
            {
                return __intervalSecond;
            }
            set
            {
                __intervalSecond = value;
                textBoxIntervalSecond.Text = __intervalSecond.ToString();
            }
        }

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
        /// 「開く」を押したときの処理です．
        /// </summary>
        private void Open()
        {
            if (!SelectTargetDirectory())
            {
                return;
            }
            using (var dialog = new ProgressForm())
            {
                dialog.AsyncTask = LoadFilesAsync;
                dialog.StartPosition = FormStartPosition.CenterScreen;
                dialog.ShowDialog();
                MessageBox.Show(dialog.Finished.ToString());
            }
        }

        /// <summary>
        /// ディレクトリを選択します．
        /// </summary>
        /// <returns>正しいディレクトリを選択したか．</returns>
        private bool SelectTargetDirectory()
        {
            var selectedPath = Environment.CurrentDirectory;
            while (true)
            {
                var dialog = new FolderBrowserDialog();
                dialog.Description = "生理指標データが保存されているフォルダを選択してください．";
                dialog.SelectedPath = selectedPath;
                dialog.ShowNewFolderButton = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    selectedPath = dialog.SelectedPath;
                    if (!IdentifyBiosignalFiles(selectedPath))
                    {
                        // 必要なファイルが揃っていなかったらやり直し
                        continue;
                    }
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// DataInfo のパス情報から生理指標データを非同期に読み込みます．
        /// </summary>
        /// <param name="form">進捗状況を表すフォーム．</param>
        /// <param name="token">処理キャンセルを監視するトークン．</param>
        private async void LoadFilesAsync(ProgressForm form, CancellationToken token)
        {
            // テスト用
            for (int i = 0; i < 100; i++)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                await System.Threading.Tasks.Task.Delay(100);
                Invoke(
                    (MethodInvoker)(() =>
                    {
                        form.Progress = i + 1;
                        form.Message = "ほえ";
                    }));
            }
            Invoke(
                (MethodInvoker)(() =>
                {
                    form.Finished = true;
                }));
        }

        /// <summary>
        /// csv データのファイル・タイプ(CDM など)を判断します．
        /// </summary>
        /// <param name="path">判断したいファイルのパス．</param>
        /// <returns>ファイル・タイプ．</returns>
        private Enums.FileTypes JudgeFileType(string path)
        {
            using (var parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                try
                {
                    var column = parser.ReadFields();
                    return
                        Regex.IsMatch(column[0], @"CDM") ? Enums.FileTypes.CDM :
                        Regex.IsMatch(column[0], @"RR") ? Enums.FileTypes.RR :
                        Regex.IsMatch(column[1], @"ECG") ? Enums.FileTypes.ECG :
                        Regex.IsMatch(column[1], @"SCR") ? Enums.FileTypes.SCR :
                        Enums.FileTypes.Undefined;
                }
                catch
                {
                    return Enums.FileTypes.Undefined;
                }
            }
        }

        /// <summary>
        /// 生理指標データのファイル名を同定し，DataInfo にファイル名を保存します．
        /// </summary>
        /// <remarks>
        /// 必要なすべてのファイルが揃っていない場合，メッセージボックスに
        /// エラー内容を出力します．
        /// </remarks>
        /// <param name="path">ファイルを探索するディレクトリ．</param>
        /// <returns>必要なすべてのファイルが揃っているか．</returns>
        private bool IdentifyBiosignalFiles(string path)
        {
            var filenames = new Dictionary<Enums.FileTypes, string>();
            var errors = new List<string>();
            foreach (var f in Directory.GetFiles(path, "*.csv"))
            {
                var type = JudgeFileType(f);
                var name = Path.GetFileName(f);
                if (filenames.ContainsKey(type))
                {
                    errors.Add(String.Format("{0}: 他の{1}データファイルが存在します．",
                        name,
                        type.ToString()));
                }
                else if (type != Enums.FileTypes.Undefined)
                {
                    filenames[type] = name;
                }
            }
            foreach (Enums.FileTypes type in Enum.GetValues(typeof(Enums.FileTypes)))
            {
                if (type == Enums.FileTypes.Undefined)
                {
                    continue;
                }
                if (!filenames.ContainsKey(type))
                {
                    errors.Add(String.Format("{0}データファイルが存在しません．", type.ToString()));
                }
            }
            if(errors.Count > 0)
            {
                MessageBox.Show(String.Join("\n", errors), "読み込みエラー", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            DataInfo.FileNames = filenames;
            DataInfo.DirectoryPath = path;
            return true;
        }

        #endregion

        #region イベントハンドラ

        private void Main_Load(object sender, System.EventArgs e)
        {
            ActiveControl = buttonDetect;
            IntervalSecond = 20;
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (ActiveControl == textBoxIntervalSecond)
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            Open();
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
            Open();
        }

        private void textBoxIntervalSecond_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var error = "";
            int second = IntervalSecond;
            if (!Regex.IsMatch(textBoxIntervalSecond.Text, @"^\d*[1-9]+\d*$"))
            {
                e.Cancel = true;
                error = "正の整数を指定してください．";
            }
            else
            {
                try
                {
                    second = Int32.Parse(textBoxIntervalSecond.Text);
                    if (second > 99999)
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    e.Cancel = true;
                    error = "10万秒以上は指定できません．";
                }
            }
            if (e.Cancel)
            {
                MessageBox.Show(error, "入力エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxIntervalSecond.Text = IntervalSecond.ToString();
                return;
            }
            IntervalSecond = second;
        }

        private void textBoxIntervalSecond_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void textBoxIntervalSecond_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != '\b';
        }

        #endregion
    }
}
