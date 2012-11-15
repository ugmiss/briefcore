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
        public static ICar CreateCar(string styleName)
        {
            switch (styleName)
            {
                case "BusinessCar":
                    return new BusinessCar();
                case "FamilyCar":
                    return new FamilyCar();
            }
            return null;
        }
    }
    public class SimpleFactoryDemo
    {
        public static void Show()
        {
            ICar car = SimpleFactory.CreateCar("BusinessCar");
            Console.WriteLine(car.GetCarName());
            Console.ReadKey();
        }
    }
}
