namespace BiosignalAnalyzer
{
    partial class Main
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.開くOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.バージョン情報ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxParticipantID = new System.Windows.Forms.TextBox();
            this.textBoxParticipantName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRecordDate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.textBoxDirectoryPath = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.textBoxIntervalTime = new System.Windows.Forms.TextBox();
            this.buttonRight = new System.Windows.Forms.Button();
            this.buttonDetect = new System.Windows.Forms.Button();
            this.textBoxIntervalSecond = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルToolStripMenuItem,
            this.バージョン情報ToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // ファイルToolStripMenuItem
            // 
            this.ファイルToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開くOToolStripMenuItem,
            this.保存SToolStripMenuItem,
            this.toolStripMenuItem1,
            this.終了XToolStripMenuItem});
            this.ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            resources.ApplyResources(this.ファイルToolStripMenuItem, "ファイルToolStripMenuItem");
            // 
            // 開くOToolStripMenuItem
            // 
            this.開くOToolStripMenuItem.Name = "開くOToolStripMenuItem";
            resources.ApplyResources(this.開くOToolStripMenuItem, "開くOToolStripMenuItem");
            this.開くOToolStripMenuItem.Click += new System.EventHandler(this.開くOToolStripMenuItem_Click);
            // 
            // 保存SToolStripMenuItem
            // 
            resources.ApplyResources(this.保存SToolStripMenuItem, "保存SToolStripMenuItem");
            this.保存SToolStripMenuItem.Name = "保存SToolStripMenuItem";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // 終了XToolStripMenuItem
            // 
            this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
            resources.ApplyResources(this.終了XToolStripMenuItem, "終了XToolStripMenuItem");
            // 
            // バージョン情報ToolStripMenuItem
            // 
            this.バージョン情報ToolStripMenuItem.Name = "バージョン情報ToolStripMenuItem";
            resources.ApplyResources(this.バージョン情報ToolStripMenuItem, "バージョン情報ToolStripMenuItem");
            this.バージョン情報ToolStripMenuItem.Click += new System.EventHandler(this.バージョン情報ToolStripMenuItem_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxParticipantID
            // 
            resources.ApplyResources(this.textBoxParticipantID, "textBoxParticipantID");
            this.textBoxParticipantID.Name = "textBoxParticipantID";
            this.textBoxParticipantID.ReadOnly = true;
            // 
            // textBoxParticipantName
            // 
            resources.ApplyResources(this.textBoxParticipantName, "textBoxParticipantName");
            this.textBoxParticipantName.Name = "textBoxParticipantName";
            this.textBoxParticipantName.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBoxRecordDate
            // 
            resources.ApplyResources(this.textBoxRecordDate, "textBoxRecordDate");
            this.textBoxRecordDate.Name = "textBoxRecordDate";
            this.textBoxRecordDate.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // statusStrip
            // 
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // textBoxDirectoryPath
            // 
            resources.ApplyResources(this.textBoxDirectoryPath, "textBoxDirectoryPath");
            this.textBoxDirectoryPath.Name = "textBoxDirectoryPath";
            this.textBoxDirectoryPath.ReadOnly = true;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            resources.ApplyResources(this.chart1, "chart1");
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // buttonLeft
            // 
            resources.ApplyResources(this.buttonLeft, "buttonLeft");
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.UseVisualStyleBackColor = true;
            // 
            // textBoxIntervalTime
            // 
            resources.ApplyResources(this.textBoxIntervalTime, "textBoxIntervalTime");
            this.textBoxIntervalTime.Name = "textBoxIntervalTime";
            this.textBoxIntervalTime.ReadOnly = true;
            // 
            // buttonRight
            // 
            resources.ApplyResources(this.buttonRight, "buttonRight");
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.UseVisualStyleBackColor = true;
            // 
            // buttonDetect
            // 
            resources.ApplyResources(this.buttonDetect, "buttonDetect");
            this.buttonDetect.Name = "buttonDetect";
            this.buttonDetect.UseVisualStyleBackColor = true;
            // 
            // textBoxIntervalSecond
            // 
            resources.ApplyResources(this.textBoxIntervalSecond, "textBoxIntervalSecond");
            this.textBoxIntervalSecond.Name = "textBoxIntervalSecond";
            this.textBoxIntervalSecond.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxIntervalSecond_KeyDown);
            this.textBoxIntervalSecond.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxIntervalSecond_KeyPress);
            this.textBoxIntervalSecond.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxIntervalSecond_Validating);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxIntervalSecond);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonDetect);
            this.Controls.Add(this.buttonRight);
            this.Controls.Add(this.textBoxIntervalTime);
            this.Controls.Add(this.buttonLeft);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.textBoxDirectoryPath);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxRecordDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxParticipantName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxParticipantID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ファイルToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 開くOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存SToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem バージョン情報ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxParticipantID;
        private System.Windows.Forms.TextBox textBoxParticipantName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRecordDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.TextBox textBoxDirectoryPath;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonLeft;
        private System.Windows.Forms.TextBox textBoxIntervalTime;
        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Button buttonDetect;
        private System.Windows.Forms.TextBox textBoxIntervalSecond;
        private System.Windows.Forms.Label label6;
    }
}

