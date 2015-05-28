### 零散的拖拽代码 ###
```
private void gridView4_MouseDown(object sender, MouseEventArgs e)
{
    GridView view = sender as GridView;
    downHitInfo = null;
    GridHitInfo hitInfo = view.CalcHitInfo(new System.Drawing.Point(e.X, e.Y));
    if (Control.ModifierKeys != Keys.None) return;
    if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
    {
        downHitInfo = hitInfo;
    }
    MyClass.videoDragFlag = false;
    MyClass.rectangle = new Rectangle();
}

private void gridView4_MouseMove(object sender, MouseEventArgs e)
{
    GridView view = sender as GridView;
    if (e.Button == MouseButtons.Left && downHitInfo != null)
    {
        Size dragSize = SystemInformation.DragSize;
        Rectangle dragRect = new Rectangle(new System.Drawing.Point(downHitInfo.HitPoint.X - dragSize.Width / 2,
            downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);
        if (!dragRect.Contains(new System.Drawing.Point(e.X, e.Y)))
        {
            object row = view.GetRow(downHitInfo.RowHandle);
            view.GridControl.DoDragDrop(row, DragDropEffects.Move);
            downHitInfo = null;
            DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
        }
    }
}



private void XXX_DragOver(object sender, DragEventArgs e)
{
    e.Effect = DragDropEffects.Move;
}
        
private void XXX_DragDrop(object sender, DragEventArgs e)
{
    MyClass myClass= e.Data.GetData(typeof(MyClass)) as MyClass;
}
```