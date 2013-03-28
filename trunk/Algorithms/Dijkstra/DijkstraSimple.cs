﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Dijkstra
{
    public class Dijkstra
    {
        int[,] L1 ={
                {-1,  5, -1, -1, -1,  3, -1, -1}, 
                { 5, -1,  2, -1, -1, -1,  3, -1}, 
                {-1,  2, -1,  6, -1, -1, -1, 10}, 
                {-1, -1,  6, -1,  3, -1, -1, -1},
                {-1, -1, -1,  3, -1,  8, -1,  5}, 
                { 3, -1, -1, -1,  8, -1,  7, -1}, 
                {-1,  3, -1, -1, -1,  7, -1,  2}, 
                {-1, -1, 10, -1,  5, -1,  2, -1} 
            };
        private int rank = 0;
        private int[,] L;
        private int[] C;
        public int[] D;
        private int trank = 0;
        public Dijkstra(int paramRank, int[,] paramArray)
        {
            L = new int[paramRank, paramRank];
            C = new int[paramRank];
            D = new int[paramRank];
            rank = paramRank;
            for (int i = 0; i < rank; i++)
            {
                for (int j = 0; j < rank; j++)
                {
                    L[i, j] = paramArray[i, j];
                }
            }

            for (int i = 0; i < rank; i++)
            {
                C[i] = i;
            }
            C[0] = -1;
            for (int i = 1; i < rank; i++)
                D[i] = L[0, i];
        }
        public void DijkstraSolving()
        {
            int minValue = Int32.MaxValue;
            int minNode = 0;
            for (int i = 0; i < rank; i++)
            {
                if (C[i] == -1)
                    continue;
                if (D[i] > 0 && D[i] < minValue)
                {
                    minValue = D[i];
                    minNode = i;
                }
            }
            C[minNode] = -1;
            for (int i = 0; i < rank; i++)
            {
                if (L[minNode, i] < 0)
                    continue;
                if (D[i] < 0)
                {
                    D[i] = minValue + L[minNode, i];
                    continue;
                }
                if ((D[minNode] + L[minNode, i]) < D[i])
                    D[i] = minValue + L[minNode, i];
            }
        }
        public void Run()
        {
            for (trank = 1; trank <rank; trank++)
            {
                DijkstraSolving();
                Console.WriteLine("iteration" + trank);
                for (int i = 0; i < rank; i++)
                    Console.Write(D[i] + " ");
                Console.WriteLine("");
                for (int i = 0; i < rank; i++)
                    Console.Write(C[i] + " ");
                Console.WriteLine("");
            }
        }
    }
}
