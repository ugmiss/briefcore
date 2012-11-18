namespace DataTransfer
{
    partial class InitForm
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
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.txtDir = new Telerik.WinControls.UI.RadTextBox();
            this.radButton2 = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(12, 3);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(89, 21);
            this.radButton1.TabIndex = 30;
            this.radButton1.Text = "默认生成路径";
            this.radButton1.ThemeName = "Breeze";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // txtDir
            // 
            this.txtDir.Location = new System.Drawing.Point(12, 30);
            this.txtDir.Name = "txtDir";
            this.txtDir.ReadOnly = true;
            this.txtDir.Size = new System.Drawing.Size(352, 20);
            this.txtDir.TabIndex = 31;
            this.txtDir.TabStop = false;
            this.txtDir.ThemeName = "Breeze";
            // 
            // radButton2
            // 
            this.radButton2.Location = new System.Drawing.Point(275, 56);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(89, 21);
            this.radButton2.TabIndex = 32;
            this.radButton2.Text = "确定";
            this.radButton2.ThemeName = "Breeze";
            this.radButton2.Click += new System.EventHandler(this.radButton2_Click);
            // 
            // InitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 91);
            this.Controls.Add(this.radButton2);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.txtDir);
            this.Name = "InitForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.ShowIcon = false;
            this.Text = "设置默认路径，保存数据备份。";
            this.ThemeName = "ControlDefault";
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadTextBox txtDir;
        private Telerik.WinControls.UI.RadButton radButton2;
    }
}

