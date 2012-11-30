using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Windows.Forms;

namespace WCFClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //InstanceContext instanceContext = new InstanceContext(new Callback());
            //  using(DuplexChannelFactory<ICalculator> channelFactory = new  DuplexChannelFactory<ICalculator>(instanceContext,"CalculatorService"))
            //  {
            //      ICalculator proxy = channelFactory.CreateChannel();
            //      using (proxy as IDisposable)
            //      }
            Environment.MessageForm = new MessageForm();
            Application.Run(Environment.MessageForm);
        }
    }
}
