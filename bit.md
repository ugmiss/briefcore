# Introduction #
1.获得int型最大值


int  getMaxInt(){
> return  (1<<31) - 1; //2147483647， 由于优先级关系，括号不可省略


int  getMaxInt(){
> return  -(1<<-1) - 1; //2147483647
}
另一种写法

int  getMaxInt(){
> return  ~(1<<31); //2147483647
}
C语言中不知道int占几个字节时候

int  getMaxInt(){
> return  ((unsigned  int )- 1 ) >>  1 ; //2147483647
}
2.获得int型最小值


int  getMinInt(){
> return  1<<31; //-2147483648
> }
另一种写法

int  getMinInt(){
> return  1 << -1; //-2147483648
}
3.获得long类型的最大值


C语言版

long  getMaxLong(){
> return  ((unsigned  long )-1) >> 1; //2147483647
}
JAVA版
[java](java.md) view plain copy
long  getMaxLong(){
> return  (( long ) 1 << 127 )- 1 ; //9223372036854775807
}
获得long最小值，和其他类型的最大值，最小值同理.

4.乘以2运算


int  mulTwo( int  n){ //计算n\*2
> return  n<<1;
}
5.除以2运算


int  divTwo( int  n){ //负奇数的运算不可用
> return  n>>1; //除以2
}
6.乘以2的m次方


int  mulTwoPower( int  n, int  m){ //计算n**(2^m)
> return  n<<m;
}
7.除以2的m次方**


int  divTwoPower( int  n, int  m){ //计算n/(2^m)
> return  n>>m;
}
8.判断一个数的奇偶性


boolean  isOddNumber( int  n){
> return  (n &  1 ) ==  1 ;
}
9.不用临时变量交换两个数（面试常考）


C语言 版

void  swap( int  **a, int**b){
> (**a)<sup>=(*b)</sup>=(**a)^=(**b);
}
通用版（一些语言中得分开写）**

a ^= b;
b ^= a;
a ^= b;
1 0.取绝对值（某些机器上 ， 效率比n>0  ?  n:-n 高）

int  abs( int  n){
return  (n ^ (n >> 31)) - (n >> 31);
/**n>>31 取得n的符号，若n为正数，n>>31等于0，若n为负数，n>>31等于-1
若n为正数 n<sup>0=0,数不变，若n为负数有n</sup>-1 需要计算n和-1的补码，然后进行异或运算，
结果n变号并且为n的绝对值减1，再减去-1就是绝对值**/
}
11.取两个数的最大值（某些机器上， 效率比a>b ? a:b高）


通用版
[cpp](cpp.md) view plain copy
int  max( int  a, int  b){
> return  b&((a-b)>>31) | a&(~(a-b)>>31);
> /**如果a>=b,(a-b)>>31为0，否则为-1**/
}
C语言版

int  max( int  x, int  y){
> return  x <sup> ((x </sup> y) & -(x < y));
> /**如果x<y x<y返回1，否则返回0，
、 与0做与运算结果为0，与-1做与运算结果不变**/
}
12.取两个数的最小值（某些机器上， 效率比a>b ? b:a高）


通用版
[cpp](cpp.md) view plain copy
int  min( int  a, int  b){
> return  a&((a-b)>>31) | b&(~(a-b)>>31);
> /**如果a>=b,(a-b)>>31为0，否则为-1**/
}
C语言版

int  min( int  x, int  y){
> return  y <sup> ((x </sup> y) & -(x < y));
> > /**如果x<y x<y返回1，否则返回0，
> > > 与0做与运算结果为0，与-1做与运算结果不变**/
}
13.判断符号是否相同


boolean  isSameSign( int  x,  int  y){

> return  (x ^ y) >  0 ;  // true 表示 x和y有相同的符号， false表示x，y有相反的符号。
}
14.计算2的n次方


int  getFactorialofTwo( int  n){ //n > 0
> return  2<<(n-1); //2的n次方
}
15.判断一个数是不是2的幂

[java](java.md) view plain copy
boolean  isFactorialofTwo( int  n){
> return  (n & (n -  1 )) ==  0 ;
> /**如果是2的幂，n一定是100... n-1就是1111....
> > 所以做与运算结果为0**/
}
16.对2的n次方取余


int  quyu( int  m, int  n){ //n为2的次方

> return  m & (n -  1 );
> /**如果是2的幂，n一定是100... n-1就是1111....
> > 所以做与运算结果保留m在n范围的非0的位**/
}
17.求两个整数的平均值

[java](java.md) view plain copy
int  getAverage( int  x,  int  y){

> return  (x+y) >>  1 ;
｝
另一种写法

int  getAverage( int  x,  int  y){
> return  ((x^y) >>  1 ) + (x&y);
> /**(x^y) >> 1得到x，y其中一个为1的位并除以2，
> > x&y得到x，y都为1的部分，加一起就是平均数了**/

}

下面是三个最基本对二进制位的操作

18.从低位到高位,取n的第m位


int  getBit( int  n,  int  m){

> return  (n >> (m- 1 )) &  1 ;
}
19.从低位到高位.将n的第m位置1


int  setBitToOne( int  n,  int  m){
> return  n | ( 1 <<(m- 1 ));
> /**将1左移m-1位找到第m位，得到000...1...000
> > n在和这个数做或运算**/
}
20.从低位到高位,将n的第m位置0


int  setBitToZero( int  n,  int  m){

> return  n & ~( 1 <<(m- 1 ));
> /**将1左移m-1位找到第m位，取反后变成111...0...1111
> > n再和这个数做与运算**/
}