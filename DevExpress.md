### CheckedListBoxControl ###
用到了Dev中的CheckedListBoxControl，我使用DataSource属性，给其绑定了一个List对象。界面显示都挺正常的，当若干个项的复选框被选中的后，它的checkedListBoxControl1.CheckedItems也是正常的。
唯独的问题是在代码中得到的checkedListBoxControl1.Items.Count 为0，自然就不能遍历控件的中的所有项。checkedListBoxControl1能够正确获得某个Item对象的方法就剩下GetItemValue(idx)、SetItemChecked等。
如果你也遇到这种情况，请使用下面的方法来遍历所有的Item，并做出相应的处理：
第一个例子是全不选：
```
public void ClearCheckState()
{
    int idx = 0;
    while (checkedListBoxControl1.GetItemValue(idx) != null)
    {
        checkedListBoxControl1.SetItemChecked(idx, false);
        idx++;
    }
}
```
第二个例子是遍历每个选项的Value
```
int idx = 0;
while (checkedListBoxControl1.GetItemValue(idx) != null)
{
    object obj = checkedListBoxControl1.GetItemValue(idx);
    // 你要做的处理逻辑
    idx++;
}
```
当然，如果你不是通过DataSource进行绑定，而是通过Items.add来添加的话，上面的问题就不会存在。s

### GridControl行改变事件 ###
```
void gridView1_FocusedRowChanged(object sender,DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
{
     VideoPlanTemplate template = gridView1.GetFocusedRow() as VideoPlanTemplate;
}
```

### CheckedComboBoxEdit勾选 ###
```
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

```