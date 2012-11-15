namespace SlideWindowDemo
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
            this.btnInit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.txtDBPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPartitionCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(16, 291);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(90, 23);
            this.btnInit.TabIndex = 1;
            this.btnInit.Text = "初始化";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "库名";
            // 
            // txtDBName
            // 
            this.txtDBName.Location = new System.Drawing.Point(16, 27);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(120, 21);
            this.txtDBName.TabIndex = 3;
            this.txtDBName.Text = "DataCenter";
            // 
            // txtDBPath
            // 
            this.txtDBPath.Location = new System.Drawing.Point(16, 73);
            this.txtDBPath.Name = "txtDBPath";
            this.txtDBPath.Size = new System.Drawing.Size(120, 21);
            this.txtDBPath.TabIndex = 5;
            this.txtDBPath.Text = "D:\\Data\\";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "地址";
            // 
            // txtPartitionCount
            // 
            this.txtPartitionCount.Location = new System.Drawing.Point(16, 114);
            this.txtPartitionCount.Name = "txtPartitionCount";
            this.txtPartitionCount.Size = new System.Drawing.Size(120, 21);
            this.txtPartitionCount.TabIndex = 7;
            this.txtPartitionCount.Text = "6";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "分区数";
            // 
            // txtStartTime
            // 
            this.txtStartTime.Location = new System.Drawing.Point(16, 161);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(120, 21);
            this.txtStartTime.TabIndex = 9;
            this.txtStartTime.Text = "2012-11-15 15:00:00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "开始时间";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(16, 239);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(120, 21);
            this.txtInterval.TabIndex = 11;
            this.txtInterval.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "间隔";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "月",
            "日",
            "时",
            "分"});
            this.comboBox1.Location = new System.Drawing.Point(17, 207);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(119, 20);
            this.comboBox1.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnInit);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.txtDBName);
            this.panel1.Controls.Add(this.txtInterval);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtDBPath);
            this.panel1.Controls.Add(this.txtStartTime);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtPartitionCount);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(166, 483);
            this.panel1.TabIndex = 14;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(166, 0);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(720, 483);
            this.txtLog.TabIndex = 15;
            this.txtLog.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 483);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "数据库归档解决方案（分区-滑动窗口）";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDBName;
        private System.Windows.Forms.TextBox txtDBPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPartitionCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStartTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox txtLog;
    }
}

