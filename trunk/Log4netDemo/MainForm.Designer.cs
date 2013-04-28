namespace Log4netDemo
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
            this.btnLogInfo = new System.Windows.Forms.Button();
            this.btnLogError = new System.Windows.Forms.Button();
            this.btnLogDebug = new System.Windows.Forms.Button();
            this.btnLogFatal = new System.Windows.Forms.Button();
            this.btLogWarn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLogInfo
            // 
            this.btnLogInfo.Location = new System.Drawing.Point(38, 42);
            this.btnLogInfo.Name = "btnLogInfo";
            this.btnLogInfo.Size = new System.Drawing.Size(75, 23);
            this.btnLogInfo.TabIndex = 0;
            this.btnLogInfo.Text = "LogInfo";
            this.btnLogInfo.UseVisualStyleBackColor = true;
            this.btnLogInfo.Click += new System.EventHandler(this.btnLogInfo_Click);
            // 
            // btnLogError
            // 
            this.btnLogError.Location = new System.Drawing.Point(38, 145);
            this.btnLogError.Name = "btnLogError";
            this.btnLogError.Size = new System.Drawing.Size(75, 23);
            this.btnLogError.TabIndex = 1;
            this.btnLogError.Text = "LogError";
            this.btnLogError.UseVisualStyleBackColor = true;
            this.btnLogError.Click += new System.EventHandler(this.btnLogError_Click);
            // 
            // btnLogDebug
            // 
            this.btnLogDebug.Location = new System.Drawing.Point(38, 13);
            this.btnLogDebug.Name = "btnLogDebug";
            this.btnLogDebug.Size = new System.Drawing.Size(75, 23);
            this.btnLogDebug.TabIndex = 2;
            this.btnLogDebug.Text = "LogDebug";
            this.btnLogDebug.UseVisualStyleBackColor = true;
            this.btnLogDebug.Click += new System.EventHandler(this.btnLogDebug_Click);
            // 
            // btnLogFatal
            // 
            this.btnLogFatal.Location = new System.Drawing.Point(38, 174);
            this.btnLogFatal.Name = "btnLogFatal";
            this.btnLogFatal.Size = new System.Drawing.Size(75, 23);
            this.btnLogFatal.TabIndex = 3;
            this.btnLogFatal.Text = "LogFatal";
            this.btnLogFatal.UseVisualStyleBackColor = true;
            this.btnLogFatal.Click += new System.EventHandler(this.btnLogFatal_Click);
            // 
            // btLogWarn
            // 
            this.btLogWarn.Location = new System.Drawing.Point(38, 116);
            this.btLogWarn.Name = "btLogWarn";
            this.btLogWarn.Size = new System.Drawing.Size(75, 23);
            this.btLogWarn.TabIndex = 4;
            this.btLogWarn.Text = "LogWarn";
            this.btLogWarn.UseVisualStyleBackColor = true;
            this.btLogWarn.Click += new System.EventHandler(this.btLogWarn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 388);
            this.Controls.Add(this.btLogWarn);
            this.Controls.Add(this.btnLogFatal);
            this.Controls.Add(this.btnLogDebug);
            this.Controls.Add(this.btnLogError);
            this.Controls.Add(this.btnLogInfo);
            this.Name = "MainForm";
            this.Text = "日志Demo";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLogInfo;
        private System.Windows.Forms.Button btnLogError;
        private System.Windows.Forms.Button btnLogDebug;
        private System.Windows.Forms.Button btnLogFatal;
        private System.Windows.Forms.Button btLogWarn;
    }
}

