using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternDemo
{

    public interface ICoat
    {
        void GetYourCoat();
    }
    public class BusinessCoat : ICoat
    {
        public void GetYourCoat()
        {
            Console.WriteLine("商务上衣");
        }
    }
    public class FashionCoat : ICoat
    {
        public void GetYourCoat()
        {
            Console.WriteLine("时尚上衣");
        }
    }
    public class SimpleFactory
    {
        public static ICoat CreateCoat(string styleName)
        {
            switch (styleName.Trim().ToLower())
            {
                case "business":
                    return new BusinessCoat();
                case "fashion":
                    return new FashionCoat();
                default:
                    throw new Exception("还没有你要的那种衣服");
            }
        }
    }

}
