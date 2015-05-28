### Code ###
```
//通过Form找BarManager 
DevExpress.XtraBars.BarManager myManager = null;
foreach (var childControl in parentform.Controls)
{

    if (childControl is DevExpress.XtraBars.BarDockControl)
    {
        myManager = (childControl as DevExpress.XtraBars.BarDockControl).Manager;
        if (myManager != null)
            break;
    }
}
//找主菜单条
Bar bar = myManager.MainMenu;
foreach (BarItem sbi in myManager.Items)
{
    //sbi.Enabled = sr != null;
    if (sbi is BarSubItem)
    {
        Set(sbi as BarSubItem, parentform);
    }
}

 public static void Set(BarSubItem item, BaseForm parentform)
        {
            foreach (var link in item.LinksPersistInfo)
            {

                BarItem sbi = (link as LinkPersistInfo).Item;
                //sbi.Enabled = sr != null;
                if (sbi is BarSubItem)
                {
                    Set(sbi as BarSubItem, parentform);
                }
            }
        }


```