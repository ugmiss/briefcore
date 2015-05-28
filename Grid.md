# 数据绑定和刷新 #
当GridControl绑定一个List或其他集合时
通过遍历集合元素，更新元素的属性后，可以通过刷新GridControl关联的GridView来刷新显示数据
```
     this.gridView1.RefreshData();
```

# 添加单元格按钮 #
```
添加ButtonEdit到ColumnEdit 并隐藏Text
其中要设置整体AllowEdit=true，其他列设成false
只保留此一列
```

# 改变单元格的显示颜色 #
```
private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
{
    if (e.DisplayText == "异常")
        e.Appearance.ForeColor = Color.Red;
    else
        e.Appearance.ForeColor = Color.Black;
}
```
```
今天碰到一新的修改需求，在DevExpress.XtraGrid.Views.Grid.GridView界面中，根据当前的行的数据状态，改变  RepositoryItemButtonEdit的Caption数据，折腾了很久，要么
 
不起作用，要么包界面中的所有Button的Caption的都改变了 ,要么点击按钮后，按钮消失了，留下一个空白的单元格子在哪里。经过一番折腾和查阅各种资料，终于解决了。
 
方法如下:
 
第一步: 增加一下三个类 
 
    public class MyRepositoryItemButtonEdit : RepositoryItemButtonEdit
     {
         public override DevExpress.XtraEditors.ViewInfo.BaseEditViewInfo CreateViewInfo()
         {
             return new MyRepositoryItemButtonEditViewInfo(this);
         }
     }
     public class MyRepositoryItemButtonEditViewInfo : ButtonEditViewInfo
     {
         public MyRepositoryItemButtonEditViewInfo(RepositoryItem item) : base(item) { }
 

        protected override DevExpress.XtraEditors.Drawing.EditorButtonObjectInfoArgs CreateButtonInfo(EditorButton button, int index)
         {
             return base.CreateButtonInfo(new MyEditorButton(), index);
         }
     }
     public class MyEditorButton : EditorButton
     {
         public MyEditorButton() : this(string.Empty) { }
         public MyEditorButton(string myCaption)
         {
             this.myCaption = myCaption;
             Kind = ButtonPredefines.Glyph;
         }
         string myCaption = "";
         public override string Caption
         {
             get
             {
                 return myCaption;
             }
             set
             {
                 myCaption = value;
             }
         }
     }
 

第二步： 在WinForm的构造函数中 初始化Button类以及注册相应的事件
 
public FrmView1()
         {
             InitializeComponent();
       
             MyRepositoryItemButtonEdit ri = new MyRepositoryItemButtonEdit();
             ri.Buttons[0].Kind = ButtonPredefines.Glyph;
             ri.Buttons[0].Caption = "发货";
             ri.TextEditStyle = TextEditStyles.HideTextEditor;
             this.gridControl.RepositoryItems.Add(ri);
             this.gridView.Columns["F_BTN_BEIHUO"].ColumnEdit = ri;                        
             ri.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(repositoryItemButtonEdit1_ButtonClick);
         }
 




第三步: 在GridView中注册一下两个事件，并在事件中根据条件设置Button Caption 的值
 


        private void GridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
         {
             if (e.Column.FieldName.Equals("F_BTN_BEIHUO"))
             {
                 if (!this.GridView.GetRowCellValue(e.RowHandle, "STATE_CODE").Equals("02"))
                 {
                     ButtonEditViewInfo editInfo = (ButtonEditViewInfo)((DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo)e.Cell).ViewInfo;
                     int i = editInfo.RightButtons.Count;
                     if (i > 0)
                     {
                         editInfo.RightButtons[0].Button.Caption = "备货";
                     }
                 }
                 else
                 {
                     ButtonEditViewInfo editInfo = (ButtonEditViewInfo)((DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo)e.Cell).ViewInfo;
                     int i = editInfo.RightButtons.Count;
                     if (i > 0)
                     {
                         editInfo.RightButtons[0].Button.Caption = "发货";
                     }
                 }
             }
         }
 

        private void GridView_ShownEditor(object sender, EventArgs e)
         {
             GridView view = sender as GridView;
             if (view.FocusedColumn.FieldName == "F_BTN_BEIHUO")
             {
                 ButtonEdit ed = (ButtonEdit)view.ActiveEditor;
 

                DataRow drCurFocus = this.gdvSender.GetFocusedDataRow();
                 if (drCurFocus["STATE_CODE"].ToString().Trim().Equals("02"))
                 {
                     ed.Properties.Buttons[0].Caption = "发货";
                 }
                 else
                 {
                     ed.Properties.Buttons[0].Caption = "备货";
                 }
             }
         }
 

完成以上三个步骤，并可实现根据条件动态修改button 的Caption 的值了。
 
值得注意的是，如果repositoryItemButtonEdit所在的列的宽度不够时，即Button 上的字不能完全显示时，点击按钮可能不会触发事件，后者点击按钮之后按钮消失了，产生一个或者多个的空白格子，这是只要调整按钮所在列的宽度，是Button上的字充分显示，即可解决问题。

```

# 打印 添加标题(默认的打印无标题) #
```
/// <summary>
/// 打印
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
{
    PrintingSystem ps = new DevExpress.XtraPrinting.PrintingSystem();
    DevExpress.XtraPrinting.PrintableComponentLink link = null;
    link = new DevExpress.XtraPrinting.PrintableComponentLink(ps);
    ps.Links.Add(link);
    link.Component = gridMineEnter;//PrintableComponentLink的组件指向对应的Grid控件
    string strPrintHeader = "井下人员进出查询";
    PageHeaderFooter phf = link.PageHeaderFooter as PageHeaderFooter;
    phf.Header.Content.Clear();
    phf.Header.Content.AddRange(new string[] { "", strPrintHeader, "" });
    phf.Header.Font = new System.Drawing.Font("宋体", 14, System.Drawing.FontStyle.Bold);
    phf.Header.LineAlignment = BrickAlignment.Center;
    link.CreateDocument();
    ps.PreviewFormEx.Show();
    //this.gridMineEnter.ShowPrintPreview();//默认的打印
}

```