namespace MyCutter
{
    partial class DetailForm
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
            this.txtX = new Telerik.WinControls.UI.RadTextBox();
            this.txtSpeed = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.txtResource = new Telerik.WinControls.UI.RadTextBox();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.radButton2 = new Telerik.WinControls.UI.RadButton();
            this.radButton3 = new Telerik.WinControls.UI.RadButton();
            this.ddlBlockType = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.txtY = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
            this.txtAngle = new Telerik.WinControls.UI.RadTextBox();
            this.chkRePlay = new Telerik.WinControls.UI.RadCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlBlockType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRePlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(70, 40);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(125, 20);
            this.txtX.TabIndex = 0;
            this.txtX.TabStop = false;
            // 
            // txtSpeed
            // 
            this.txtSpeed.Location = new System.Drawing.Point(70, 68);
            this.txtSpeed.Name = "txtSpeed";
            this.txtSpeed.Size = new System.Drawing.Size(125, 20);
            this.txtSpeed.TabIndex = 1;
            this.txtSpeed.TabStop = false;
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(12, 12);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(52, 18);
            this.radLabel1.TabIndex = 2;
            this.radLabel1.Text = "动作类型";
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(12, 40);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(31, 18);
            this.radLabel2.TabIndex = 3;
            this.radLabel2.Text = "TagX";
            // 
            // radLabel4
            // 
            this.radLabel4.Location = new System.Drawing.Point(14, 96);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(29, 18);
            this.radLabel4.TabIndex = 5;
            this.radLabel4.Text = "资源";
            // 
            // txtResource
            // 
            this.txtResource.Location = new System.Drawing.Point(70, 94);
            this.txtResource.Name = "txtResource";
            this.txtResource.Size = new System.Drawing.Size(270, 20);
            this.txtResource.TabIndex = 4;
            this.txtResource.TabStop = false;
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(346, 94);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(45, 20);
            this.radButton1.TabIndex = 6;
            this.radButton1.Text = "选择";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // radButton2
            // 
            this.radButton2.Location = new System.Drawing.Point(311, 120);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(80, 20);
            this.radButton2.TabIndex = 7;
            this.radButton2.Text = "取消";
            this.radButton2.Click += new System.EventHandler(this.radButton2_Click);
            // 
            // radButton3
            // 
            this.radButton3.Location = new System.Drawing.Point(225, 120);
            this.radButton3.Name = "radButton3";
            this.radButton3.Size = new System.Drawing.Size(80, 20);
            this.radButton3.TabIndex = 8;
            this.radButton3.Text = "确定";
            this.radButton3.Click += new System.EventHandler(this.radButton3_Click);
            // 
            // ddlBlockType
            // 
            this.ddlBlockType.DropDownAnimationEnabled = true;
            this.ddlBlockType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            radListDataItem1.Selected = true;
            radListDataItem1.Text = "移动";
            radListDataItem1.TextWrap = true;
            radListDataItem2.Text = "播放视频";
            radListDataItem2.TextWrap = true;
            radListDataItem3.Text = "播放音频";
            radListDataItem3.TextWrap = true;
            this.ddlBlockType.Items.Add(radListDataItem1);
            this.ddlBlockType.Items.Add(radListDataItem2);
            this.ddlBlockType.Items.Add(radListDataItem3);
            this.ddlBlockType.Location = new System.Drawing.Point(70, 10);
            this.ddlBlockType.Name = "ddlBlockType";
            this.ddlBlockType.ShowImageInEditorArea = true;
            this.ddlBlockType.Size = new System.Drawing.Size(125, 20);
            this.ddlBlockType.TabIndex = 14;
            this.ddlBlockType.Text = "移动";
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(206, 40);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(31, 18);
            this.radLabel3.TabIndex = 16;
            this.radLabel3.Text = "TagY";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(264, 40);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(125, 20);
            this.txtY.TabIndex = 15;
            this.txtY.TabStop = false;
            // 
            // radLabel5
            // 
            this.radLabel5.Location = new System.Drawing.Point(14, 68);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(29, 18);
            this.radLabel5.TabIndex = 17;
            this.radLabel5.Text = "速度";
            // 
            // radLabel6
            // 
            this.radLabel6.Location = new System.Drawing.Point(208, 68);
            this.radLabel6.Name = "radLabel6";
            this.radLabel6.Size = new System.Drawing.Size(29, 18);
            this.radLabel6.TabIndex = 19;
            this.radLabel6.Text = "角度";
            // 
            // txtAngle
            // 
            this.txtAngle.Location = new System.Drawing.Point(264, 68);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new System.Drawing.Size(125, 20);
            this.txtAngle.TabIndex = 18;
            this.txtAngle.TabStop = false;
            // 
            // chkRePlay
            // 
            this.chkRePlay.Location = new System.Drawing.Point(264, 10);
            this.chkRePlay.Name = "chkRePlay";
            this.chkRePlay.Size = new System.Drawing.Size(66, 18);
            this.chkRePlay.TabIndex = 20;
            this.chkRePlay.Text = "是否循环";
            // 
            // DetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 149);
            this.Controls.Add(this.chkRePlay);
            this.Controls.Add(this.radLabel6);
            this.Controls.Add(this.txtAngle);
            this.Controls.Add(this.radLabel5);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.ddlBlockType);
            this.Controls.Add(this.radButton3);
            this.Controls.Add(this.radButton2);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.radLabel4);
            this.Controls.Add(this.txtResource);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.txtSpeed);
            this.Controls.Add(this.txtX);
            this.Name = "DetailForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "动作详细配置";
            this.Load += new System.EventHandler(this.PageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlBlockType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRePlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox txtX;
        private Telerik.WinControls.UI.RadTextBox txtSpeed;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadTextBox txtResource;
        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadButton radButton2;
        private Telerik.WinControls.UI.RadButton radButton3;
        private Telerik.WinControls.UI.RadDropDownList ddlBlockType;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadTextBox txtY;
        private Telerik.WinControls.UI.RadTextBox txtAngle;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadLabel radLabel6;
        private Telerik.WinControls.UI.RadCheckBox chkRePlay;
    }
}