using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Utility;
using System.Threading.Tasks;

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
            List<person> li = new List<person>();
            Parallel.For(1, 10, p =>
            {
                li.Add(new person() { Id = Guid.NewGuid().ToString(), Name = NameFactory.GetBoyName(), PersonNumber = 1001 + li.Count, Sex = RandomFactory.Next(2) == 1 ? "男" : "女" });
            });
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Id", "ID");
            dic.Add("PersonNumber", "编号");
            dic.Add("Name", "姓名");
            dic.Add("Sex", "性别");
            multiLookupControl1.Init(li, "Id", "Name", dic);
        }
        class person
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Sex { get; set; }
            public int PersonNumber { get; set; }

        }

       
    }

   
}
