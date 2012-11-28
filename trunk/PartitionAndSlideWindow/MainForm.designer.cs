﻿namespace PartitionAndSlideWindow
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel2 = new System.Windows.Forms.Panel();
            this.wizardControl1 = new DevExpress.XtraWizard.WizardControl();
            this.welcomeWizardPage1 = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.wizardPage1 = new DevExpress.XtraWizard.WizardPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtUid = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
            this.wizardPage2 = new DevExpress.XtraWizard.WizardPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPartitionCount = new System.Windows.Forms.TextBox();
            this.txtDBPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStartTime = new System.Windows.Forms.TextBox();
            this.wizardPage3 = new DevExpress.XtraWizard.WizardPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lisIndex = new System.Windows.Forms.ListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboCol = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboTable = new System.Windows.Forms.ComboBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardControl1.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.wizardPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.wizardControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(693, 436);
            this.panel2.TabIndex = 35;
            // 
            // wizardControl1
            // 
            this.wizardControl1.CancelText = "取消";
            this.wizardControl1.Controls.Add(this.welcomeWizardPage1);
            this.wizardControl1.Controls.Add(this.wizardPage1);
            this.wizardControl1.Controls.Add(this.completionWizardPage1);
            this.wizardControl1.Controls.Add(this.wizardPage2);
            this.wizardControl1.Controls.Add(this.wizardPage3);
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.FinishText = "完成";
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.LookAndFeel.SkinName = "Metropolis";
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.NextText = "下一步 >";
            this.wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.welcomeWizardPage1,
            this.wizardPage1,
            this.wizardPage2,
            this.wizardPage3,
            this.completionWizardPage1});
            this.wizardControl1.PreviousText = "< 上一步";
            this.wizardControl1.Size = new System.Drawing.Size(693, 436);
            this.wizardControl1.Text = "";
            this.wizardControl1.CancelClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_CancelClick);
            this.wizardControl1.FinishClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_FinishClick);
            // 
            // welcomeWizardPage1
            // 
            this.welcomeWizardPage1.IntroductionText = resources.GetString("welcomeWizardPage1.IntroductionText");
            this.welcomeWizardPage1.Name = "welcomeWizardPage1";
            this.welcomeWizardPage1.ProceedText = "继续,点击下一步";
            this.welcomeWizardPage1.Size = new System.Drawing.Size(476, 303);
            this.welcomeWizardPage1.Text = "滑动窗口方案-表分区配置向导";
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.groupBox1);
            this.wizardPage1.DescriptionText = "数据库连接设置";
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(661, 291);
            this.wizardPage1.Text = "第一步";
            this.wizardPage1.PageValidating += new DevExpress.XtraWizard.WizardPageValidatingEventHandler(this.wizardPage1_PageValidating);
            this.wizardPage1.PageCommit += new System.EventHandler(this.wizardPage1_PageCommit);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtPwd);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtUid);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDBName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(661, 291);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库设置";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(154, 14);
            this.label8.TabIndex = 23;
            this.label8.Text = "密码            （Password）";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(216, 86);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(139, 22);
            this.txtPwd.TabIndex = 24;
            this.txtPwd.Text = "123456";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 14);
            this.label7.TabIndex = 21;
            this.label7.Text = "用户名           （User ID）";
            // 
            // txtUid
            // 
            this.txtUid.Location = new System.Drawing.Point(216, 55);
            this.txtUid.Name = "txtUid";
            this.txtUid.Size = new System.Drawing.Size(139, 22);
            this.txtUid.TabIndex = 22;
            this.txtUid.Text = "sa";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(168, 14);
            this.label6.TabIndex = 19;
            this.label6.Text = "数据库服务    （SQL Server）";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(216, 23);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(139, 22);
            this.txtServer.TabIndex = 20;
            this.txtServer.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "数据库名称 （Database Name）";
            // 
            // txtDBName
            // 
            this.txtDBName.Location = new System.Drawing.Point(216, 118);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(139, 22);
            this.txtDBName.TabIndex = 3;
            this.txtDBName.Text = "DataCenter";
            // 
            // completionWizardPage1
            // 
            this.completionWizardPage1.AllowCancel = false;
            this.completionWizardPage1.FinishText = "表分区设置相关操作完成。";
            this.completionWizardPage1.Name = "completionWizardPage1";
            this.completionWizardPage1.ProceedText = "点击完成关闭";
            this.completionWizardPage1.Size = new System.Drawing.Size(476, 303);
            this.completionWizardPage1.Text = "完成向导";
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.groupBox2);
            this.wizardPage2.DescriptionText = "分区参数设置";
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Size = new System.Drawing.Size(661, 291);
            this.wizardPage2.Text = "第二步";
            this.wizardPage2.PageCommit += new System.EventHandler(this.wizardPage2_PageCommit);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtInterval);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtPartitionCount);
            this.groupBox2.Controls.Add(this.txtDBPath);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtStartTime);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(661, 291);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "分区设置";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(216, 118);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(59, 22);
            this.txtInterval.TabIndex = 11;
            this.txtInterval.Text = "10";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "月",
            "日",
            "时",
            "分",
            "秒"});
            this.comboBox1.Location = new System.Drawing.Point(296, 118);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(59, 22);
            this.comboBox1.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "文件组路径     （File Path）";
            // 
            // txtPartitionCount
            // 
            this.txtPartitionCount.Location = new System.Drawing.Point(216, 86);
            this.txtPartitionCount.Name = "txtPartitionCount";
            this.txtPartitionCount.Size = new System.Drawing.Size(139, 22);
            this.txtPartitionCount.TabIndex = 7;
            this.txtPartitionCount.Text = "6";
            // 
            // txtDBPath
            // 
            this.txtDBPath.Location = new System.Drawing.Point(216, 23);
            this.txtDBPath.Name = "txtDBPath";
            this.txtDBPath.Size = new System.Drawing.Size(139, 22);
            this.txtDBPath.TabIndex = 5;
            this.txtDBPath.Text = "D:\\Data\\";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "分区开始时间   (Start Time)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "分区数    (Partition Count)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 14);
            this.label5.TabIndex = 10;
            this.label5.Text = "分区间隔         (Interval)";
            // 
            // txtStartTime
            // 
            this.txtStartTime.Location = new System.Drawing.Point(216, 55);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(139, 22);
            this.txtStartTime.TabIndex = 9;
            this.txtStartTime.Text = "2012-11-15 15:00:00";
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.groupBox3);
            this.wizardPage3.DescriptionText = "分区表和分区列选择";
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.Size = new System.Drawing.Size(661, 291);
            this.wizardPage3.Text = "第三步";
            this.wizardPage3.PageCommit += new System.EventHandler(this.wizardPage3_PageCommit);
            this.wizardPage3.PageRollback += new System.EventHandler(this.wizardPage3_PageRollback);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.lisIndex);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.cboCol);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.cboTable);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(661, 291);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "分区表选择";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 82);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(169, 14);
            this.label11.TabIndex = 9;
            this.label11.Text = "索引            （Index Name）";
            // 
            // lisIndex
            // 
            this.lisIndex.FormattingEnabled = true;
            this.lisIndex.ItemHeight = 14;
            this.lisIndex.Location = new System.Drawing.Point(216, 82);
            this.lisIndex.Name = "lisIndex";
            this.lisIndex.Size = new System.Drawing.Size(140, 144);
            this.lisIndex.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(170, 14);
            this.label10.TabIndex = 7;
            this.label10.Text = "分区列       （Column Name）";
            // 
            // cboCol
            // 
            this.cboCol.FormattingEnabled = true;
            this.cboCol.Location = new System.Drawing.Point(216, 54);
            this.cboCol.Name = "cboCol";
            this.cboCol.Size = new System.Drawing.Size(140, 22);
            this.cboCol.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(168, 14);
            this.label9.TabIndex = 5;
            this.label9.Text = "分区表         （Table Name）";
            // 
            // cboTable
            // 
            this.cboTable.FormattingEnabled = true;
            this.cboTable.Location = new System.Drawing.Point(216, 23);
            this.cboTable.Name = "cboTable";
            this.cboTable.Size = new System.Drawing.Size(140, 22);
            this.cboTable.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 436);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " 数据库归档解决方案（分区-滑动窗口）";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardControl1.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.wizardPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.wizardPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraWizard.WizardControl wizardControl1;
        private DevExpress.XtraWizard.WelcomeWizardPage welcomeWizardPage1;
        private DevExpress.XtraWizard.WizardPage wizardPage1;
        private DevExpress.XtraWizard.CompletionWizardPage completionWizardPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtUid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDBName;
        private DevExpress.XtraWizard.WizardPage wizardPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPartitionCount;
        private System.Windows.Forms.TextBox txtDBPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStartTime;
        private DevExpress.XtraWizard.WizardPage wizardPage3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboCol;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboTable;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox lisIndex;
    }
}