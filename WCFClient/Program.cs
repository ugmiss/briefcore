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
            Environment.MessageForm = new MessageForm();
            Application.Run(Environment.MessageForm);
        }
    }
}
