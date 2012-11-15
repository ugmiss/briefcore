using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternDemo
{
    public interface IPen
    {
        string WriteMyName();
    }
    public class Pen : IPen
    {
        public string WriteMyName() { return "钢笔"; }
    }
    public class BallPen : IPen
    {
        public string WriteMyName() { return "圆珠笔"; }
    }
    public class Pencil : IPen
    {
        public string WriteMyName() { return "铅笔"; }
    }

    public interface IPaper
    {
        string ReadMyName();
    }
    public class Magazine : IPaper
    {
        public string ReadMyName() { return "杂志"; }
    }
    public class Newspaper : IPaper
    {
        public string ReadMyName() { return "报纸"; }
    }
    public class Book : IPaper
    {
        public string ReadMyName() { return "书"; }
    }

    public interface AbstractFactory
    {
        IPen ProducePen();
        IPaper ProducePaper();
    }

    public class ConcreteFactoryA : AbstractFactory
    {
        public IPen ProducePen() { return new Pen(); }
        public IPaper ProducePaper() { return new Magazine(); }
    }
    public class ConcreteFactoryB : AbstractFactory
    {
        public IPen ProducePen() { return new Pencil(); }
        public IPaper ProducePaper() { return new Book(); }
    }
    public class ConcreteFactoryC : AbstractFactory
    {
        public IPen ProducePen() { return new BallPen(); }
        public IPaper ProducePaper() { return new Newspaper(); }
    }

    public class AbstractFactoryDemo
    {
        public static void Show()
        {
            AbstractFactory factoryA = new ConcreteFactoryA();
            AbstractFactory factoryB = new ConcreteFactoryB();
            AbstractFactory factoryC = new ConcreteFactoryC();
            IPen pen = factoryA.ProducePen();
            IPaper paper = factoryA.ProducePaper();
            Console.WriteLine("工厂A生产：{0}和{1}", pen.WriteMyName(), paper.ReadMyName());
            pen = factoryB.ProducePen();
            paper = factoryB.ProducePaper();
            Console.WriteLine("工厂B生产：{0}和{1}", pen.WriteMyName(), paper.ReadMyName());
            pen = factoryC.ProducePen();
            paper = factoryC.ProducePaper();
            Console.WriteLine("工厂C生产：{0}和{1}", pen.WriteMyName(), paper.ReadMyName());
            Console.ReadKey();
        }
    }

}
