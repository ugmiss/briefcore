## Facade ##
Façade设计模式并非一个集装箱，可以任意地放进任何多个对象。Façade模式中组件的内部应该是“相互耦合关系比较大的一系列组件”，而不是一个简单的功能集合。
## 原意是建筑的正面 ##
```
facade[英][fəˈsɑ:d]
```

## Facade模式的模型 ##
```
　　A系统有A1, A2, A3等类。客户端需要调用A系统的的A1.doSomething1();A2.doSomething2();A3.doSomething3()来完成某功能。
Facade模式的实现模型就是： 
A系统： 
class A1 {
    public void doSomething1();
} 
class A2 {
    public void doSomething2();
} 
class A3 {
    public void doSomething3();
} 
Facade：
public class Facade {
    public void doSomething() {
        A1 a1 = new A1();
        A1 a2 = new A2();
        A1 a3 = new A3(); 

        a1.doSomething1();
        a2.doSomething2();
        a3.doSomething3();
    }
} 
Test：
public class Client {
    public static void main(String []args) {
        Facade facade = new Facade();
        facade.doSomething();
    }
} 
```