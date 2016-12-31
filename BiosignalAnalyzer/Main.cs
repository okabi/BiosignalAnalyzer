using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

        /// <summary>前回のディレクトリ選択で選択されたディレクトリ(確定された選択は DataInfo のものです)．</summary>
        private string selectedDirectory;

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
            while (true)
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
                    if (dialog.Finished)
                    {
                        UpdateTextBox();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// テキストボックスの内容を更新します．
        /// </summary>
        private void UpdateTextBox()
        {
            textBoxParticipantName.Text = DataInfo.ParticipantName;
            textBoxParticipantID.Text = DataInfo.ParticipantID;
            textBoxRecordDate.Text = DataInfo.RecordDate;
            var s = DataInfo.DirectoryPath.Split('\\');
            textBoxDirectoryPath.Text = s[s.Length - 1];
        }

        /// <summary>
        /// ディレクトリを選択します．
        /// </summary>
        /// <returns>正しいディレクトリを選択したか．false ならキャンセルされた．</returns>
        private bool SelectTargetDirectory()
        {
            while (true)
            {
                var dialog = new FolderBrowserDialog();
                dialog.Description = "生理指標データが保存されているフォルダーを選択してください．";
                dialog.SelectedPath = selectedDirectory;
                dialog.ShowNewFolderButton = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    selectedDirectory = dialog.SelectedPath;
                    if (!IdentifyBiosignalFiles(selectedDirectory))
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
            // 総ファイルサイズの取得
            long totalByteSize = 0;
            var fileByteSize = new Dictionary<Enums.FileTypes, long>();
            foreach (Enums.FileTypes type in Enum.GetValues(typeof(Enums.FileTypes)))
            {
                if (type == Enums.FileTypes.Undefined)
                {
                    continue;
                }
                var path = String.Join("\\", new[] { DataInfo.DirectoryPath, DataInfo.FileNames[type] });
                fileByteSize[type] = new FileInfo(path).Length;
                totalByteSize += fileByteSize[type];
            }

            // 順にファイルを読み込む
            long readByteSize = 0;
            long readFileByteSize = 0;
            foreach (Enums.FileTypes type in Enum.GetValues(typeof(Enums.FileTypes)))
            {
                if (type == Enums.FileTypes.Undefined)
                {
                    continue;
                }
                var path = String.Join("\\", new[] { DataInfo.DirectoryPath, DataInfo.FileNames[type] });
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    using (var sr = new StreamReader(fs, Encoding.GetEncoding("Shift_JIS")))
                    {
                        var s = "start";
                        while (s != null)
                        {
                            if (token.IsCancellationRequested)
                            {
                                // 中断命令が出された
                                return;
                            }
                            try
                            {
                                s = await sr.ReadLineAsync();
                            }
                            catch (OutOfMemoryException)
                            {
                                MessageBox.Show("メモリが不足しています．", "読み込みエラー",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            if (s != null)
                            {
                                LoadOneLineString(s, type);
                                readByteSize += Encoding.GetEncoding("Shift_JIS").GetByteCount(s) + 2;
                                if (readByteSize > readFileByteSize + fileByteSize[type])
                                {
                                    readByteSize = readFileByteSize + fileByteSize[type];
                                }
                            }
                            Invoke(
                                (MethodInvoker)(() =>
                                {
                                    form.Progress = (int)(100f * readByteSize / totalByteSize);
                                    form.Message = type.ToString() + "ファイルを読み込んでいます...";
                                }));
                        }
                        readFileByteSize += fileByteSize[type];
                        readByteSize = readFileByteSize;
                    }
                }
            }
            Invoke(
                (MethodInvoker)(() =>
                {
                    form.Finished = true;
                }));
        }

        /// <summary>
        /// 生理指標データの1行分の文字列から DataInfo に情報を読み込みます．
        /// </summary>
        /// <param name="str">1行分の生理指標データ文字列．</param>
        /// <param name="type">現在読み込んでいるファイルの種類．</param>
        private void LoadOneLineString(string str, Enums.FileTypes type)
        {
            var s = str.Split(',');
            if (type == Enums.FileTypes.CDM)
            {
                if (Regex.IsMatch(s[0], @"収録日時"))
                {
                    DataInfo.RecordDate = s[1] + s[2];
                }
                else if (Regex.IsMatch(s[0], @"被験者ID"))
                {
                    DataInfo.ParticipantID = s[1];
                }
                else if (Regex.IsMatch(s[0], @"被験者名"))
                {
                    DataInfo.ParticipantName = s[1];
                }
                else if (Regex.IsMatch(s[0], @"LF"))
                {
                    var lf = Regex.Matches(s[0], @"([.\d]+)～([.\d]+)");
                    DataInfo.LFFrequencyMin = float.Parse(lf[0].Groups[1].Value);
                    DataInfo.LFFrequencyMax = float.Parse(lf[0].Groups[2].Value);
                    var hf = Regex.Matches(s[1], @"([.\d]+)～([.\d]+)");
                    DataInfo.HFFrequencyMin = float.Parse(hf[0].Groups[1].Value);
                    DataInfo.HFFrequencyMax = float.Parse(hf[0].Groups[2].Value);
                }
            }
            if (Regex.IsMatch(s[0], @"^[:\d]+$"))
            {
                switch (type)
                {
                    case Enums.FileTypes.CDM:
                        DataInfo.LFData.Add(new KeyValuePair<int, float>(DataInfo.TimeToInt(s[1]), float.Parse(s[4])));
                        DataInfo.HFData.Add(new KeyValuePair<int, float>(DataInfo.TimeToInt(s[1]), float.Parse(s[5])));
                        DataInfo.LFHFData.Add(new KeyValuePair<int, float>(DataInfo.TimeToInt(s[1]), float.Parse(s[6])));
                        break;
                    case Enums.FileTypes.RR:
                        DataInfo.RRIData.Add(new KeyValuePair<int, float>(DataInfo.TimeToInt(s[1]), float.Parse(s[3])));
                        break;
                    case Enums.FileTypes.ECG:
                        DataInfo.ECGData.Add(new KeyValuePair<int, float>(DataInfo.TimeToInt(s[0]), float.Parse(s[1])));
                        break;
                    case Enums.FileTypes.SCR:
                        DataInfo.SCRData.Add(new KeyValuePair<int, float>(DataInfo.TimeToInt(s[0]), float.Parse(s[1])));
                        break;
                }
            }
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
            selectedDirectory = Environment.CurrentDirectory;
            IntervalSecond = 20;
            UpdateTextBox();
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
