using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ConsoleTest
{
    class Program
    {
        public const int rowCount = 10;
        public const int colCount = 10;
        static void Main(string[] args)
        {

            int[][] Matrix = new int[rowCount][];
            for (int i = 0; i < rowCount; i++)
            {
                Matrix[i] = Enumerable.Repeat(0, 10).ToArray();
            }
            PrintMatrix(Matrix);
        }

        public static void PrintMatrix(int[][] Matrix)
        {
            for (int i = 0; i < Matrix.Length; i++)
            {
                Console.Write("|");
                Console.Write(string.Join(",",Matrix[i]));
                Console.Write("|");
                Console.WriteLine();
            }
        }
    }
}
