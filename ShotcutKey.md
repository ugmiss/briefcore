### 代码如下，注意'KeyPreview'设置为true ###
```
public BaseForm()
{
    InitializeComponent();
    this.KeyUp += new KeyEventHandler(BaseForm_KeyUp);
    this.KeyPreview = true;
}

void BaseForm_KeyUp(object sender, KeyEventArgs e)
{
    if (e.KeyData == Keys.F12 || e.KeyData == Keys.F4)
    {
       //相关操作
    }
}
```