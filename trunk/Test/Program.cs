using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

namespace Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            long smallBlockSize = 90000;
            long largeBlockSize = 1 << 24;
            long count = 0;
            long a = 1024;
            var bigBlock = new byte[0];
            try
            {
                var smallBlocks = new List<byte[]>();
                while (true)
                {
                    GC.Collect();
                    bigBlock = new byte[largeBlockSize];
                    largeBlockSize++;
                    smallBlocks.Add(new byte[smallBlockSize]);
                    count++;
                    Console.WriteLine("{0} Mb allocated",
                    (count * smallBlockSize) / (a * a));
                }
            }
            catch (OutOfMemoryException)
            {
                bigBlock = null;
                GC.Collect();
                Console.WriteLine("OUTMEM {0} Mb allocated",
                     (count * smallBlockSize) / (a * a));
                Console.WriteLine(count);
            }

            Console.ReadLine();
        }
    }
}



