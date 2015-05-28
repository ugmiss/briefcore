# Introduction #
volatile关键字是一种类型修饰符，用它声明的类型变量表示可以被某些编译器未知的因素更改，比如：操作系统、硬件或者其它线程等。遇到这个关键字声明的变量，编译器对访问该变量的代码就不再进行优化，从而可以提供对特殊地址的稳定访问。当要求使用volatile 声明的变量的值的时候，系统总是重新从它所在的内存读取数据，即使它前面的指令刚刚从该处读取过数据。而且读取的数据立刻被保存。

volatile指出变量是随时可能发生变化的，每次使用它的时候必须从i的地址中读取，因而编译器生成的汇编代码会重新从变量地址读取数据。而优化做法是，由于编译器发现两次读数据的代码之间没有对变量进行过操作，它会自动使用上次读的数据，而不是重新从变量地址里面读。这样一来，如果变量是一个寄存器或者表示一个端口数据就容易出错，所以说volatile可以保证对特殊地址的稳定访问。

典型的例子：
for (int i=0; i<100000; i++);
这个语句用来测试空循环的速度的，但是编译器肯定要把它优化掉，根本就不执行。如果你写成
for (volatile int i=0; i<100000; i++);
它就会执行了。

一般说来，volatile用在如下的几个地方：
1、中断服务程序中修改的供其它程序检测的变量需要加volatile；
2、多任务环境下各任务间共享的标志应该加volatile；
3、存储器映射的硬件寄存器通常也要加volatile说明，因为每次对它的读写都可能有不同意义；