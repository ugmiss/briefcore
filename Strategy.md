## 抽象策略角色 ##
> 策略类，通常由一个接口或者抽象类实现。
```
// 策略接口
public interface IStrategy
{
    void DoOperation();
}
```
## 具体策略角色 ##
> 包装了相关的算法和行为。
```
// 策略A
public class StrategyA:IStrategy
{
    public void DoOperation()
    {
    }
}
// 策略B
public class StrategyB:IStrategy
{
    public void DoOperation()
    {
    }
}
```

## 环境角色 ##
持有一个策略类的引用，最终给客户端调用。
```
public class Context
{
     IStrategy strategy;    
     public Context(IStrategy strategy)
     {
         this.strategy=strategy;
     }
     public void Operation()
     {
         strategy.DoOperation();
     }
}
```