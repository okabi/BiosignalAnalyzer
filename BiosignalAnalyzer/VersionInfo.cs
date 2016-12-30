using System.Diagnostics;
using System.Windows.Forms;

namespace BiosignalAnalyzer
{
    public partial class VersionInfo : Form
    {
        /// <summary>
        /// コンストラクタです．
        /// </summary>
        public VersionInfo()
        {
            InitializeComponent();

            // 製品情報の取得
            labelName.Text = Application.ProductName;
            labelVersion.Text = "Version  " + Application.ProductVersion;
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel.LinkVisited = true;
            Process.Start(linkLabel.Text);
        }
    }
}
