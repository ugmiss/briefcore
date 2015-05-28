QRCode项目地址http://qrcodenet.codeplex.com/
应用示例：
```
string str = textBox1.Text;
QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
QrCode qrCode = new QrCode();
qrEncoder.TryEncode(str, out qrCode);
GraphicsRenderer renderer = new GraphicsRenderer(
    new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
using (FileStream stream = new FileStream(@"D:\Matrix.png", FileMode.Create))
{
    renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
}
this.pictureBox1.Image = Image.FromFile(@"D:\Matrix.png");
```