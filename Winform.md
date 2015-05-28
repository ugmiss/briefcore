### 嵌入的资源文件 ###
```
//根据assembly名称加载程序集到当前的Appdomain
System.Reflection.Assembly ass = System.Reflection.Assembly.Load("MyAssembly");
//按照 命名空间+目录的全路径+文件名 获取嵌入文件的流
using (StreamReader reader = new StreamReader(ass.GetManifestResourceStream("MyAssembly.Xml.objects.xml")))
{
    xml = reader.ReadToEnd();
}
```
### 一般输入的文本框需要控制其输入法（IME） ###
TextBox的ImeMode属性
|Off|当切换到此控件时，默认将输入法切换成英文，但仍然可以手动切换成中文|
|:--|:--------------------------------------------------------------------------------------------------|
|Disable|无论什么时候切换到此控件上，都只能输入英文，不能切换输入法            |
|NoControl|默认不控制                                                                                    |
### DialogResult ###
```
if (DialogResult.OK == XtraMessageBox.Show("是否确定删除此监测点计划", "提示", MessageBoxButtons.OKCancel))
{
    BusinessManager.DeleteVideoRecordPlan(plan);
}
```

### ToolTip ###
```
C#Winform中ToolTip的简单用法 
ToolTip信息提示框的作用就不用说了吧，我也没去细研究，只是学习了一下怎么去用，简单记录一下：

C#中提供了信息提示框，这有很多用处，可以提示控件或者用户自定义的属性信息，而且可以自动弹出或者用户指定弹出，也可以动画效果弹出。

使用方法：
①鼠标移动到控件或指定的位置自动显示：ToolTip.SetToolTip 方法 ，注意相关参数的设置。
②动画效果：参数：AutoPopDelay InitialDelay ReshowDelay
例如：

?// Create the ToolTip and associate with the Form container. 
ToolTip toolTip1 = new ToolTip(); 
 
// Set up the delays for the ToolTip. 
toolTip1.AutoPopDelay = 5000; 
toolTip1.InitialDelay = 1000; 
toolTip1.ReshowDelay = 500; 
// Force the ToolTip text to be displayed whether or not the form is active. 
toolTip1.ShowAlways = true; 
     
// Set up the ToolTip text for the Button and Checkbox. 
toolTip1.SetToolTip(this.button1, "My button1"); 
toolTip1.SetToolTip(this.checkBox1, "My checkBox1"); 

③提示图片设置：
ToolTipIcon属性
Error 错误图标 
Info 信息图标。 
None 不是标准图标。 
Warning 警告图标。 
④气泡样式：ToolTip.IsBalloon 属性 设置为True
⑤自定义位置和触发事件显示：ToolTip.Show 方法，注意相关参数的设置。
例如：

?m_ToolTip.Show(pStringBuilder.ToString(),  
m_HookHelperJP.FormObjects.Win32Window,e.x + r.Left, e.y + r.Top); 

使用技巧：
①ToolTip.Show时显示不能隐藏问题
可做以下处理：在再次触发Show前进行Hide操作
②m_ToolTip信息不能及时更新问题
这是因为其内存清理存在问题，处理方法：在显示前清楚内存，重新实例化。
例如：

?m_ToolTip.Dispose(); 
m_ToolTip = new ToolTip(); 
m_ToolTip.ToolTipIcon = ToolTipIcon.Info; 
//m_ToolTip.IsBalloon = true; 
m_ToolTip.ShowAlways = true; 
m_ToolTip.ToolTipTitle = sName; 
m_ToolTip.Show(pStringBuilder.ToString(),  
m_HookHelperJP.FormObjects.Win32Window, e.x + r.Left, e.y + r.Top); 

③固定宽度设置
ToolTip没有直接提供Width属性，找了很久可用以下方法设置：
在显示前ToolTip.Popup 事件 中通过参数PopupEventArgs.Size进行设置。但是这只能覆盖多出的信息。这种处理不合理。
建议另外一种方法：设置每一行固定字符，多出的则换行显示
```

### 右键菜单 ###
```
void gridPointJoinPlan_MouseUp(object sender, MouseEventArgs e)
{
    if (e.Button == System.Windows.Forms.MouseButtons.Right)
    {
         popupMenuPoint.ShowPopup(Control.MousePosition);
    }
}
```