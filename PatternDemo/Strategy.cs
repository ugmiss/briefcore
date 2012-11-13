using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternDemo
{
    // 策略接口
    public interface IStrategy { void DoOperation(double sum);}
    // 策略A
    public class StrategyA : IStrategy
    {
        public void DoOperation(double sum)
        {
            Console.WriteLine("策略A：满100送30，满200送70");
            Console.WriteLine("消费金额：{0}", sum);
            if (sum >= 200)
            {
                sum = sum - 70;
            }
            else if (sum >= 100)
            {
                sum = sum - 30;
            }
            Console.WriteLine("优惠后金额：{0}", sum);
        }
    }
    // 策略B
    public class StrategyB : IStrategy
    {
        public void DoOperation(double sum)
        {
            Console.WriteLine("策略B：全场八折销售");
            Console.WriteLine("消费金额：{0}", sum);
            sum = sum * .8d;
            Console.WriteLine("优惠后金额：{0}", sum);
        }
    }
    //环境角色
    public class StrategyContext
    {
        IStrategy strategy;
        public StrategyContext(IStrategy strategy)
        {
            this.strategy = strategy;
        }
        public void Operation(double sum)
        {
            strategy.DoOperation(sum);
        }
    }
    public class StrategyDemo
    {
        public static void Show()
        {
            StrategyContext contextA = new StrategyContext(new StrategyA());
            contextA.Operation(190.0);
            contextA.Operation(210.0);
            StrategyContext contextB = new StrategyContext(new StrategyB());
            contextB.Operation(190.0);
            contextB.Operation(210.0);
        }
    }
}
