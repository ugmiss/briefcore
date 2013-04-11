using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace Test
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            List<Person> list = new List<Person>();
            list.Add(new Person() { ID = "1", Name = "AAA" });
            list.Add(new Person() { ID = "2", Name = "BBB" });
            list.Add(new Person() { ID = "3", Name = "CCC" });
            list.Add(new Person() { ID = "4", Name = "DDD" });
            list.Add(new Person() { ID = "5", Name = "EEE" });
          

            checkedComboBoxEdit1.Properties.Items.AddRange(list.ToArray());

            //checkedComboBoxEdit1.EditValue = "1,3,4";
            foreach (CheckedListBoxItem item in checkedComboBoxEdit1.Properties.Items)
            {
                Person p = item.Value as Person;
                if (p.ID == "1" || p.ID == "3" || p.ID == "5")
                {
                    item.CheckState = CheckState.Checked;
                }
                else
                {
                    item.CheckState = CheckState.Unchecked;
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            List<string> ids = new List<string>();
            foreach (CheckedListBoxItem item in checkedComboBoxEdit1.Properties.Items)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    ids.Add((item.Value as Person).ID);
                }
            }
            MessageBox.Show(string.Join(",", ids.ToArray()));
        }
    }
    public class Person
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
