多线程计时器

1：System.Threading.Timer

2：System.Timers.Timer



特殊目的的单线程计时器：

1：System.Windows.Forms.Timer（Windows Forms Timer）

2：System.Windows.Threading.DispatcherTimer(WPF timer);



多线程计时器比较强大，精确，而且可扩展性强；

单线程计时器比较安全,对于更新 Windows Forms controls或者WPF这种简单任务来说更方便。