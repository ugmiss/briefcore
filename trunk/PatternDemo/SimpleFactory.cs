using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternDemo
{
    public interface ICar
    {
        string GetCarName();
    }
    public class BusinessCar : ICar
    {
        public string GetCarName()
        {
            return "商务轿车";
        }
    }
    public class FamilyCar : ICar
    {
        public string GetCarName()
        {
            return "家用轿车";
        }
    }
    public class SimpleFactory
    {
        public static ICar CreateCoat(string styleName)
        {
            ICar car = null;
            Console.WriteLine("开始生产...");
            switch (styleName)
            {
                case "business":
                    car = new BusinessCar();
                    break;
                case "family":
                    car = new FamilyCar();
                    break;
                default:
                    throw new Exception("不生产这种车");
            }
            Console.WriteLine("生产完成。");
            return car;
        }
    }
}
