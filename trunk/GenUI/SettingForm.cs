using System;

namespace GenUI
{
    public partial class SettingForm : DevExpress.XtraEditors.XtraForm
    {
        string path = "conn.setting".AppPath();
        public SettingForm()
        {
            InitializeComponent();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            txtConnectString.Text.WriteToFile(path);
            Close();
        }
        private void SettingForm_Load(object sender, EventArgs e)
        {
            txtConnectString.Text = "".ReadFromFile(path);
        }
    }
}