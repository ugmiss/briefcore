using System;
using System.Collections.Generic;
using System.Text;

namespace BeepConsole
{
    class Program
    {
        enum Data
        {
            C = 256, D = 288, E = 320, F = 341, G = 384, A = 426, B = 480, Bm = 453

        }
        static void Main(string[] args)


//作者:L work studio --luan
        {

            int i = 1;
            while (true)
            {
                Console.Beep((int)Data.C, 600);
                Console.Beep((int)Data.D, 180);
                Console.Beep((int)Data.E, 500);
                Console.Beep((int)Data.C, 180);
                Console.Beep((int)Data.E, 400);
                Console.Beep((int)Data.C, 400);
                Console.Beep((int)Data.E, 800);
                Console.Beep((int)Data.D, 600);
                Console.Beep((int)Data.E, 180);
                Console.Beep((int)Data.F, 180);
                Console.Beep((int)Data.F, 180);
                Console.Beep((int)Data.E, 180);
                Console.Beep((int)Data.D, 180);
                Console.Beep((int)Data.F, 1600);
                //P1
                Console.Beep((int)Data.E, 600);
                Console.Beep((int)Data.F, 180);
                Console.Beep((int)Data.G, 580);
                Console.Beep((int)Data.E, 180);
                Console.Beep((int)Data.G, 400);
                Console.Beep((int)Data.E, 400);
                Console.Beep((int)Data.G, 800);
                //P2
                Console.Beep((int)Data.F, 600);
                Console.Beep((int)Data.G, 180);
                Console.Beep((int)Data.A, 180);
                Console.Beep((int)Data.A, 180);
                Console.Beep((int)Data.G, 180);
                Console.Beep((int)Data.F, 180);
                Console.Beep((int)Data.A, 1600);

                //
                Console.Beep((int)Data.G, 600);
                Console.Beep((int)Data.C, 180);
                Console.Beep((int)Data.D, 180);
                Console.Beep((int)Data.E, 180);
                Console.Beep((int)Data.F, 180);
                Console.Beep((int)Data.G, 180);
                Console.Beep((int)Data.A, 1600);
                //
                Console.Beep((int)Data.A, 600);
                Console.Beep((int)Data.D, 180);
                Console.Beep((int)Data.E, 180);
                Console.Beep((int)Data.F, 180);
                Console.Beep((int)Data.G, 180);
                Console.Beep((int)Data.A, 180);
                Console.Beep((int)Data.B, 1600);

                //
                Console.Beep((int)Data.B, 600);
                Console.Beep((int)Data.E, 180);
                Console.Beep((int)Data.F, 180);
                Console.Beep((int)Data.G, 180);
                Console.Beep((int)Data.A, 180);
                Console.Beep((int)Data.B, 180);
                Console.Beep((int)Data.C * 2, 1200);

                Console.Beep((int)Data.B, 180);
                Console.Beep((int)Data.Bm, 180);

                Console.Beep((int)Data.A, 350);
                Console.Beep((int)Data.F, 350);
                Console.Beep((int)Data.B, 350);
                Console.Beep((int)Data.G, 350);
                Console.Beep((int)Data.C * 2, 1000);

                System.Threading.Thread.Sleep(2000);
                //Console.Beep((int)Data.C * (i+1), 100);
            }
        }
    }
}
