using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Person> list = new List<Person>();
            list.Add(new Person() { Name = "AAA", Age = "22213123123123" });
            list.Add(new Person() { Name = "bbb", Age = "22" });
            list.Add(new Person() { Name = "ddd", Age = "22" });
            list.Add(new Person() { Name = "ddw", Age = "223" });
            list.Add(new Person() { Name = "wwa", Age = "12" });

            this.gridControl1.DataSource = list;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "(*.txt)|*.txt|(*.xlsx|*.xlsx";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FileName.Contains(".txt"))
                {
                    StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                    ExportData(this.gridView1, sw);
                }
            }
        }

        private void ExportData(DevExpress.XtraGrid.Views.Grid.GridView dgv, StreamWriter w)
        {
            try
            {
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    if (dgv.Columns[i].Visible)//可见
                    {
                        if (dgv.Columns[i].Caption.NotNullOrEmpty())
                            w.Write(dgv.Columns[i].Caption);//HeaderText
                        else
                            w.Write(dgv.Columns[i].FieldName);
                        w.Write("\t|");
                    }
                }
                w.WriteLine("\n");
                object[] values = new object[dgv.Columns.Count];//ColumnCount
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        if (dgv.Columns[j].Visible)
                        {
                            //dgv.GetRowCellValue(1, 1);
                            w.Write(dgv.GetRowCellValue(i, dgv.Columns[j].FieldName));
                            w.Write("\t|");
                        }
                    }
                    w.WriteLine("\n");
                }
                w.Flush();
                w.Close();
                MessageBox.Show("用户文件导出成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                w.Close();
                MessageBox.Show("用户文件导出失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

    public class Person
    {

        public string Name { get; set; }
        public string Age { get; set; }
    }
}
