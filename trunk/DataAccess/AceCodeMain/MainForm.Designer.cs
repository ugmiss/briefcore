namespace AceCodeMain
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnGen = new System.Windows.Forms.Button();
            this.ckLogic = new System.Windows.Forms.CheckBox();
            this.ckObj = new System.Windows.Forms.CheckBox();
            this.ckInf = new System.Windows.Forms.CheckBox();
            this.ckAdp = new System.Windows.Forms.CheckBox();
            this.ckFix = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(356, 412);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnGen
            // 
            this.btnGen.Location = new System.Drawing.Point(385, 401);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(75, 23);
            this.btnGen.TabIndex = 1;
            this.btnGen.Text = "生成";
            this.btnGen.UseVisualStyleBackColor = true;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // ckLogic
            // 
            this.ckLogic.AutoSize = true;
            this.ckLogic.Location = new System.Drawing.Point(388, 16);
            this.ckLogic.Name = "ckLogic";
            this.ckLogic.Size = new System.Drawing.Size(60, 16);
            this.ckLogic.TabIndex = 2;
            this.ckLogic.Text = "逻辑层";
            this.ckLogic.UseVisualStyleBackColor = true;
            // 
            // ckObj
            // 
            this.ckObj.AutoSize = true;
            this.ckObj.Location = new System.Drawing.Point(388, 38);
            this.ckObj.Name = "ckObj";
            this.ckObj.Size = new System.Drawing.Size(60, 16);
            this.ckObj.TabIndex = 3;
            this.ckObj.Text = "对象层";
            this.ckObj.UseVisualStyleBackColor = true;
            // 
            // ckInf
            // 
            this.ckInf.AutoSize = true;
            this.ckInf.Location = new System.Drawing.Point(388, 60);
            this.ckInf.Name = "ckInf";
            this.ckInf.Size = new System.Drawing.Size(60, 16);
            this.ckInf.TabIndex = 4;
            this.ckInf.Text = "接口层";
            this.ckInf.UseVisualStyleBackColor = true;
            // 
            // ckAdp
            // 
            this.ckAdp.AutoSize = true;
            this.ckAdp.Location = new System.Drawing.Point(388, 82);
            this.ckAdp.Name = "ckAdp";
            this.ckAdp.Size = new System.Drawing.Size(60, 16);
            this.ckAdp.TabIndex = 5;
            this.ckAdp.Text = "代理层";
            this.ckAdp.UseVisualStyleBackColor = true;
            // 
            // ckFix
            // 
            this.ckFix.AutoSize = true;
            this.ckFix.Location = new System.Drawing.Point(388, 104);
            this.ckFix.Name = "ckFix";
            this.ckFix.Size = new System.Drawing.Size(72, 16);
            this.ckFix.TabIndex = 6;
            this.ckFix.Text = "固定文件";
            this.ckFix.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(379, 209);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(81, 20);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.Enabled = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(379, 247);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(81, 20);
            this.comboBox2.TabIndex = 8;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboBox3
            // 
            this.comboBox3.Enabled = false;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(379, 285);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(81, 20);
            this.comboBox3.TabIndex = 9;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(379, 187);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 16);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "生成用户表";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(377, 232);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "UID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(377, 270);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "UPASS";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(379, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "全消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(481, 16);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(240, 367);
            this.textBox1.TabIndex = 14;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(388, 126);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(72, 16);
            this.checkBox2.TabIndex = 15;
            this.checkBox2.Text = "静态属性";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(646, 389);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "cOPy";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 436);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.ckFix);
            this.Controls.Add(this.ckAdp);
            this.Controls.Add(this.ckInf);
            this.Controls.Add(this.ckObj);
            this.Controls.Add(this.ckLogic);
            this.Controls.Add(this.btnGen);
            this.Controls.Add(this.dataGridView1);
            this.Name = "MainForm";
            this.Text = "生成文件";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnGen;
        private System.Windows.Forms.CheckBox ckLogic;
        private System.Windows.Forms.CheckBox ckObj;
        private System.Windows.Forms.CheckBox ckInf;
        private System.Windows.Forms.CheckBox ckAdp;
        private System.Windows.Forms.CheckBox ckFix;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button button2;
    }
}