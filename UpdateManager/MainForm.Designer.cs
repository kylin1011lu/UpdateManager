namespace UpdateManager
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            SaveConfig();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.BtnSelectPlatform = new System.Windows.Forms.Button();
            this.BtnSelectRelease = new System.Windows.Forms.Button();
            this.TbRelease = new System.Windows.Forms.TextBox();
            this.TbPlatform = new System.Windows.Forms.TextBox();
            this.BtnUpdateRelease = new System.Windows.Forms.Button();
            this.BtnUpdatePlat = new System.Windows.Forms.Button();
            this.CBVersionSelect = new System.Windows.Forms.ComboBox();
            this.LBPlatforms = new System.Windows.Forms.ListBox();
            this.BtnAddPlat = new System.Windows.Forms.Button();
            this.BtnDeletePlat = new System.Windows.Forms.Button();
            this.BtnCopy = new System.Windows.Forms.Button();
            this.PBCopy = new System.Windows.Forms.ProgressBar();
            this.TBLog = new System.Windows.Forms.TextBox();
            this.CBCopyPush = new System.Windows.Forms.CheckBox();
            this.LVGames = new System.Windows.Forms.ListView();
            this.loadwork = new System.ComponentModel.BackgroundWorker();
            this.CopyCheckWork = new System.ComponentModel.BackgroundWorker();
            this.CommitWork = new System.ComponentModel.BackgroundWorker();
            this.BtnUpdateFrame = new System.Windows.Forms.Button();
            this.TbFrame = new System.Windows.Forms.TextBox();
            this.BtnSelectFrame = new System.Windows.Forms.Button();
            this.CBFrameSource = new System.Windows.Forms.CheckBox();
            this.CBFrameCode = new System.Windows.Forms.CheckBox();
            this.BtnAddGame = new System.Windows.Forms.Button();
            this.BtnDeleteGame = new System.Windows.Forms.Button();
            this.LVPlatGames = new UpdateManager.MyListView();
            this.SuspendLayout();
            // 
            // BtnSelectPlatform
            // 
            this.BtnSelectPlatform.Location = new System.Drawing.Point(11, 414);
            this.BtnSelectPlatform.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSelectPlatform.Name = "BtnSelectPlatform";
            this.BtnSelectPlatform.Size = new System.Drawing.Size(90, 30);
            this.BtnSelectPlatform.TabIndex = 0;
            this.BtnSelectPlatform.Text = "选择平台";
            this.BtnSelectPlatform.Click += new System.EventHandler(this.BtnSelectPlatform_Click);
            // 
            // BtnSelectRelease
            // 
            this.BtnSelectRelease.Location = new System.Drawing.Point(11, 12);
            this.BtnSelectRelease.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSelectRelease.Name = "BtnSelectRelease";
            this.BtnSelectRelease.Size = new System.Drawing.Size(90, 30);
            this.BtnSelectRelease.TabIndex = 1;
            this.BtnSelectRelease.Text = "发布目录";
            this.BtnSelectRelease.Click += new System.EventHandler(this.BtnSelectRelease_Click);
            // 
            // TbRelease
            // 
            this.TbRelease.Location = new System.Drawing.Point(108, 17);
            this.TbRelease.Margin = new System.Windows.Forms.Padding(2);
            this.TbRelease.Name = "TbRelease";
            this.TbRelease.Size = new System.Drawing.Size(258, 25);
            this.TbRelease.TabIndex = 4;
            this.TbRelease.Text = "请选择发布目录";
            this.TbRelease.TextChanged += new System.EventHandler(this.TbRelease_TextChanged);
            // 
            // TbPlatform
            // 
            this.TbPlatform.Location = new System.Drawing.Point(105, 420);
            this.TbPlatform.Margin = new System.Windows.Forms.Padding(2);
            this.TbPlatform.Name = "TbPlatform";
            this.TbPlatform.Size = new System.Drawing.Size(259, 25);
            this.TbPlatform.TabIndex = 5;
            this.TbPlatform.Text = "请选择平台目录";
            this.TbPlatform.TextChanged += new System.EventHandler(this.TbPlatform_TextChanged);
            // 
            // BtnUpdateRelease
            // 
            this.BtnUpdateRelease.Location = new System.Drawing.Point(370, 12);
            this.BtnUpdateRelease.Margin = new System.Windows.Forms.Padding(2);
            this.BtnUpdateRelease.Name = "BtnUpdateRelease";
            this.BtnUpdateRelease.Size = new System.Drawing.Size(90, 30);
            this.BtnUpdateRelease.TabIndex = 6;
            this.BtnUpdateRelease.Text = "更新目录";
            this.BtnUpdateRelease.Click += new System.EventHandler(this.BtnUpdateRelease_Click);
            // 
            // BtnUpdatePlat
            // 
            this.BtnUpdatePlat.Location = new System.Drawing.Point(368, 414);
            this.BtnUpdatePlat.Margin = new System.Windows.Forms.Padding(2);
            this.BtnUpdatePlat.Name = "BtnUpdatePlat";
            this.BtnUpdatePlat.Size = new System.Drawing.Size(90, 30);
            this.BtnUpdatePlat.TabIndex = 7;
            this.BtnUpdatePlat.Text = "更新平台";
            this.BtnUpdatePlat.Click += new System.EventHandler(this.BtnUpdatePlat_Click);
            // 
            // CBVersionSelect
            // 
            this.CBVersionSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBVersionSelect.FormattingEnabled = true;
            this.CBVersionSelect.Location = new System.Drawing.Point(-1, 689);
            this.CBVersionSelect.Name = "CBVersionSelect";
            this.CBVersionSelect.Size = new System.Drawing.Size(121, 23);
            this.CBVersionSelect.TabIndex = 9;
            this.CBVersionSelect.Visible = false;
            // 
            // LBPlatforms
            // 
            this.LBPlatforms.FormattingEnabled = true;
            this.LBPlatforms.ItemHeight = 15;
            this.LBPlatforms.Location = new System.Drawing.Point(11, 486);
            this.LBPlatforms.Name = "LBPlatforms";
            this.LBPlatforms.Size = new System.Drawing.Size(449, 214);
            this.LBPlatforms.TabIndex = 10;
            this.LBPlatforms.SelectedIndexChanged += new System.EventHandler(this.LBPlatforms_SelectedIndexChanged);
            // 
            // BtnAddPlat
            // 
            this.BtnAddPlat.Location = new System.Drawing.Point(105, 451);
            this.BtnAddPlat.Margin = new System.Windows.Forms.Padding(2);
            this.BtnAddPlat.Name = "BtnAddPlat";
            this.BtnAddPlat.Size = new System.Drawing.Size(90, 30);
            this.BtnAddPlat.TabIndex = 11;
            this.BtnAddPlat.Text = "添加";
            this.BtnAddPlat.Click += new System.EventHandler(this.BtnAddPlat_Click);
            // 
            // BtnDeletePlat
            // 
            this.BtnDeletePlat.Location = new System.Drawing.Point(224, 451);
            this.BtnDeletePlat.Margin = new System.Windows.Forms.Padding(2);
            this.BtnDeletePlat.Name = "BtnDeletePlat";
            this.BtnDeletePlat.Size = new System.Drawing.Size(90, 30);
            this.BtnDeletePlat.TabIndex = 12;
            this.BtnDeletePlat.Text = "删除";
            this.BtnDeletePlat.Click += new System.EventHandler(this.BtnDeletePlat_Click);
            // 
            // BtnCopy
            // 
            this.BtnCopy.Location = new System.Drawing.Point(537, 371);
            this.BtnCopy.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCopy.Name = "BtnCopy";
            this.BtnCopy.Size = new System.Drawing.Size(90, 30);
            this.BtnCopy.TabIndex = 13;
            this.BtnCopy.Text = "开始拷贝";
            this.BtnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
            // 
            // PBCopy
            // 
            this.PBCopy.Location = new System.Drawing.Point(642, 371);
            this.PBCopy.Name = "PBCopy";
            this.PBCopy.Size = new System.Drawing.Size(435, 30);
            this.PBCopy.TabIndex = 14;
            // 
            // TBLog
            // 
            this.TBLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBLog.Location = new System.Drawing.Point(537, 446);
            this.TBLog.Multiline = true;
            this.TBLog.Name = "TBLog";
            this.TBLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TBLog.Size = new System.Drawing.Size(613, 252);
            this.TBLog.TabIndex = 15;
            // 
            // CBCopyPush
            // 
            this.CBCopyPush.AutoSize = true;
            this.CBCopyPush.Location = new System.Drawing.Point(537, 419);
            this.CBCopyPush.Name = "CBCopyPush";
            this.CBCopyPush.Size = new System.Drawing.Size(149, 19);
            this.CBCopyPush.TabIndex = 16;
            this.CBCopyPush.Text = "拷贝完成自动提交";
            this.CBCopyPush.UseVisualStyleBackColor = true;
            // 
            // LVGames
            // 
            this.LVGames.FullRowSelect = true;
            this.LVGames.GridLines = true;
            this.LVGames.Location = new System.Drawing.Point(15, 55);
            this.LVGames.MultiSelect = false;
            this.LVGames.Name = "LVGames";
            this.LVGames.Size = new System.Drawing.Size(445, 300);
            this.LVGames.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.LVGames.TabIndex = 17;
            this.LVGames.UseCompatibleStateImageBehavior = false;
            this.LVGames.View = System.Windows.Forms.View.Details;
            // 
            // BtnUpdateFrame
            // 
            this.BtnUpdateFrame.Location = new System.Drawing.Point(368, 371);
            this.BtnUpdateFrame.Margin = new System.Windows.Forms.Padding(2);
            this.BtnUpdateFrame.Name = "BtnUpdateFrame";
            this.BtnUpdateFrame.Size = new System.Drawing.Size(90, 30);
            this.BtnUpdateFrame.TabIndex = 21;
            this.BtnUpdateFrame.Text = "更新框架";
            this.BtnUpdateFrame.Click += new System.EventHandler(this.BtnUpdateFrame_Click);
            // 
            // TbFrame
            // 
            this.TbFrame.Location = new System.Drawing.Point(105, 377);
            this.TbFrame.Margin = new System.Windows.Forms.Padding(2);
            this.TbFrame.Name = "TbFrame";
            this.TbFrame.Size = new System.Drawing.Size(259, 25);
            this.TbFrame.TabIndex = 20;
            this.TbFrame.Text = "请选择框架目录";
            // 
            // BtnSelectFrame
            // 
            this.BtnSelectFrame.Location = new System.Drawing.Point(11, 371);
            this.BtnSelectFrame.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSelectFrame.Name = "BtnSelectFrame";
            this.BtnSelectFrame.Size = new System.Drawing.Size(90, 30);
            this.BtnSelectFrame.TabIndex = 19;
            this.BtnSelectFrame.Text = "框架目录";
            this.BtnSelectFrame.Click += new System.EventHandler(this.BtnSelectFrame_Click);
            // 
            // CBFrameSource
            // 
            this.CBFrameSource.AutoSize = true;
            this.CBFrameSource.Location = new System.Drawing.Point(707, 418);
            this.CBFrameSource.Name = "CBFrameSource";
            this.CBFrameSource.Size = new System.Drawing.Size(152, 19);
            this.CBFrameSource.TabIndex = 22;
            this.CBFrameSource.Text = "更新框架res和src";
            this.CBFrameSource.UseVisualStyleBackColor = true;
            this.CBFrameSource.CheckedChanged += new System.EventHandler(this.CBFrameSource_CheckedChanged);
            // 
            // CBFrameCode
            // 
            this.CBFrameCode.AutoSize = true;
            this.CBFrameCode.Location = new System.Drawing.Point(865, 418);
            this.CBFrameCode.Name = "CBFrameCode";
            this.CBFrameCode.Size = new System.Drawing.Size(113, 19);
            this.CBFrameCode.TabIndex = 23;
            this.CBFrameCode.Text = "更新框架c++";
            this.CBFrameCode.UseVisualStyleBackColor = true;
            this.CBFrameCode.CheckedChanged += new System.EventHandler(this.CBFrameCode_CheckedChanged);
            // 
            // BtnAddGame
            // 
            this.BtnAddGame.Location = new System.Drawing.Point(468, 194);
            this.BtnAddGame.Margin = new System.Windows.Forms.Padding(2);
            this.BtnAddGame.Name = "BtnAddGame";
            this.BtnAddGame.Size = new System.Drawing.Size(60, 30);
            this.BtnAddGame.TabIndex = 24;
            this.BtnAddGame.Text = "->";
            this.BtnAddGame.Click += new System.EventHandler(this.BtnAddGame_Click);
            // 
            // BtnDeleteGame
            // 
            this.BtnDeleteGame.Location = new System.Drawing.Point(468, 239);
            this.BtnDeleteGame.Margin = new System.Windows.Forms.Padding(2);
            this.BtnDeleteGame.Name = "BtnDeleteGame";
            this.BtnDeleteGame.Size = new System.Drawing.Size(60, 30);
            this.BtnDeleteGame.TabIndex = 25;
            this.BtnDeleteGame.Text = "删除";
            this.BtnDeleteGame.Click += new System.EventHandler(this.BtnDeleteGame_Click);
            // 
            // LVPlatGames
            // 
            this.LVPlatGames.CheckBoxes = true;
            this.LVPlatGames.FullRowSelect = true;
            this.LVPlatGames.GridLines = true;
            this.LVPlatGames.Location = new System.Drawing.Point(537, 55);
            this.LVPlatGames.Name = "LVPlatGames";
            this.LVPlatGames.Size = new System.Drawing.Size(620, 300);
            this.LVPlatGames.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.LVPlatGames.TabIndex = 18;
            this.LVPlatGames.UseCompatibleStateImageBehavior = false;
            this.LVPlatGames.View = System.Windows.Forms.View.Details;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1168, 713);
            this.Controls.Add(this.BtnDeleteGame);
            this.Controls.Add(this.BtnAddGame);
            this.Controls.Add(this.CBFrameCode);
            this.Controls.Add(this.CBFrameSource);
            this.Controls.Add(this.BtnUpdateFrame);
            this.Controls.Add(this.TbFrame);
            this.Controls.Add(this.BtnSelectFrame);
            this.Controls.Add(this.LVPlatGames);
            this.Controls.Add(this.LVGames);
            this.Controls.Add(this.CBCopyPush);
            this.Controls.Add(this.TBLog);
            this.Controls.Add(this.PBCopy);
            this.Controls.Add(this.BtnCopy);
            this.Controls.Add(this.BtnDeletePlat);
            this.Controls.Add(this.BtnAddPlat);
            this.Controls.Add(this.LBPlatforms);
            this.Controls.Add(this.CBVersionSelect);
            this.Controls.Add(this.BtnUpdatePlat);
            this.Controls.Add(this.BtnUpdateRelease);
            this.Controls.Add(this.TbPlatform);
            this.Controls.Add(this.TbRelease);
            this.Controls.Add(this.BtnSelectRelease);
            this.Controls.Add(this.BtnSelectPlatform);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "平台拷贝管理工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnSelectPlatform;
        private System.Windows.Forms.Button BtnSelectRelease;
        private System.Windows.Forms.TextBox TbRelease;
        private System.Windows.Forms.TextBox TbPlatform;
        private System.Windows.Forms.Button BtnUpdateRelease;
        private System.Windows.Forms.Button BtnUpdatePlat;
        private System.Windows.Forms.ComboBox CBVersionSelect;
        private System.Windows.Forms.ListBox LBPlatforms;
        private System.Windows.Forms.Button BtnAddPlat;
        private System.Windows.Forms.Button BtnDeletePlat;
        private System.Windows.Forms.Button BtnCopy;
        private System.Windows.Forms.ProgressBar PBCopy;
        private System.Windows.Forms.TextBox TBLog;
        private System.Windows.Forms.CheckBox CBCopyPush;
        private System.Windows.Forms.ListView LVGames;
        private MyListView LVPlatGames;
        private System.ComponentModel.BackgroundWorker loadwork;
        private System.ComponentModel.BackgroundWorker CopyCheckWork;
        private System.ComponentModel.BackgroundWorker CommitWork;
        private System.Windows.Forms.Button BtnUpdateFrame;
        private System.Windows.Forms.TextBox TbFrame;
        private System.Windows.Forms.Button BtnSelectFrame;
        private System.Windows.Forms.CheckBox CBFrameSource;
        private System.Windows.Forms.CheckBox CBFrameCode;
        private System.Windows.Forms.Button BtnAddGame;
        private System.Windows.Forms.Button BtnDeleteGame;
    }
}

