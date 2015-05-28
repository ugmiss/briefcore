# 'Image.FromFile()' #
当使用FromFile时，有文件占用的问题，即选中的图片被程序打开占用，原文件不能修改和删除
```
OpenFileDialog file = new OpenFileDialog();
file.Filter = "*.jpg|*.jpg|*.png|*png|*.bmp|*.bmp";
file.ShowDialog();
if (!string.IsNullOrEmpty(file.FileName))
{
    try
    {
             this.picPerson.Image= Image.FromFile(file.FileName);
    }
    catch (OutOfMemoryException ex)
    {
        MessageBox.Show("图片太大或非法的图片格式。");
        return;
    }
}
```
# 'Image.FromStream()' #
使用文件流到内存流转换的方式，释放了对文件的占用。
```
OpenFileDialog file = new OpenFileDialog();
file.Filter = "*.gif|*.gif|*.jpg|*.jpg|*.png|*png|*.bmp|*.bmp";

file.ShowDialog();
if (!string.IsNullOrEmpty(file.FileName))
{
    try
    {
        FileStream fs = new FileStream(file.FileName, FileMode.Open, FileAccess.Read);
        int byteLength = (int)fs.Length;
        byte[] fileBytes = new byte[byteLength];
        fs.Read(fileBytes, 0, byteLength);
        //文件流关闭,文件解除锁定
        fs.Close();
        this.pictureBox1.Image = Image.FromStream(new MemoryStream(fileBytes));
    }
    catch (OutOfMemoryException ex)
    {
        MessageBox.Show("图片太大或非法的图片格式。");
        return;
    }
}
```