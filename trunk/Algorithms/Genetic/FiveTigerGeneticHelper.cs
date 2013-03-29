using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace Algorithms.Genetic
{
    public class FiveTigerGeneticHelper
    {
        public static int[] Blacks;
        public static int[] Whites;
        public FiveTigerGeneticHelper(int[] blacks, int[] whites)
        {
            Blacks = blacks;
            Whites = whites;
        }

        /// <summary>
        /// 适应度函数。
        /// </summary>
        public static Func<int[], double> FitnessFunc = p =>
        {
            int[] b = new int[25];
            for (int i = 0; i < Blacks.Length; i++)
            {
                b[Blacks[i]] = 1;
            }
            for (int i = 0; i < Whites.Length; i++)
            {
                b[Whites[i]] = -1;
            }
            int index = 0;
            int pindex = 0;
            while (true)
            {
                if (index == 24)
                    break;
                if (b[index] == 0)
                {
                    b[index] = p[pindex]==1?1:-1;
                    pindex++;
                }
                else
                {
                    index++;
                }
            }
            double[] brights;
            for (int i = 0; i < 25; i++)
            {
                b[i] = b[i] == 1 ? 1 : 0;
            }
            int bpoint = GetBlackPoint(b, out brights);
            for (int i = 0; i < 25; i++)
            {
                b[i] = b[i] == 1 ? 0 : 1;
            }
            int wpoint = GetBlackPoint(b, out brights);

            return ((double)(bpoint/86.0));
        };
        /// <summary>
        /// 染色体生成函数。
        /// </summary>
        public static Func<int[]> ChromosomeFunc = () =>
        {
            int len = 25 - Blacks.Length - Whites.Length;
            int[] chromosome = new int[len];
            for (int i = 0; i < len; i++)
            {
                chromosome[i] = 0;
            }
            for (int i = 0; i < 13 - Blacks.Length; i++)
            {
                chromosome[i] = 1;
            }
            chromosome = ArrayHelper.Shuffle(chromosome);
            return chromosome;
        };
        /// <summary>
        /// 选择函数。
        /// </summary>
        public static Func<int[][], int[]> ChooseFunc = (p) =>
        {
            double[] fitness = new double[p.Length];
            double[] probabilities = new double[p.Length];
            double sumfitness = 0;
            for (int i = 0; i < p.Length; i++)
            {
                fitness[i] = FitnessFunc(p[i]);
                sumfitness += fitness[i];
            }
            for (int i = 0; i < p.Length; i++)
            {
                probabilities[i] = fitness[i] / sumfitness;
            }
            double m = 0;
            double r = RandomFactory.NextDouble();
            for (int i = 0; i < p.Length; i++)
            {
                m = m + probabilities[i];
                if (r <= m)
                    return p[i];
            }
            return null;
        };
        /// <summary>
        /// 交叉函数。
        /// </summary>
        public static Func<int[], int[], int[][]> CrossFunc = (a, b) =>
        {
            int index = RandomFactory.Next(a.Length);
            int[] ab = new int[a.Length];
            int[] ba = new int[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                if (i > index)
                {
                    ab[i] = b[i];
                    ba[i] = a[i];
                }
                else
                {
                    ab[i] = a[i];
                    ba[i] = b[i];
                }
            }

            int sumAB = 0;
            int sumBA = 0;
            do
            {
                for (int x = 0; x < ab.Length; x++)
                {
                    sumAB += ab[x];
                    sumBA += ba[x];
                }
                if (sumAB > sumBA)
                {
                    for (int x = 0; x < ab.Length; x++)
                    {
                        if (ab[x] > ba[x])
                        {
                            ab[x] = 0;
                            ba[x] = 1;
                            break;
                        }
                    }
                }
                if (sumBA > sumAB)
                {
                    for (int x = 0; x < ab.Length; x++)
                    {
                        if (ba[x] > ab[x])
                        {
                            ab[x] = 1;
                            ba[x] = 0;
                            break;
                        }
                    }
                }

            } while (sumAB != sumBA);
            return new int[][] { ab, ba };
        };
        /// <summary>
        /// 变异函数。
        /// </summary>
        public static Action<int[]> MutationAction = (p) =>
        {
            int indexA = RandomFactory.Next(p.Length);
            int indexB = RandomFactory.Next(p.Length);
            int temp = p[indexA];
            p[indexA] = p[indexB];
            p[indexB] = temp;
        };



        public static int GetBlackPoint(int[] blacksinAll, out double[] rights)
        {
            int sum = 0;
            rights = new double[25];
            for (int i = 0; i < rights.Length; i++)
            {
                rights[i] = 0;
            }
            int[] qi = Enumerable.Repeat(0, 25).ToArray();
            for (int i = 0; i < blacksinAll.Length; i++)
            {
                qi[i] = blacksinAll[i];
            }
            //通天2个
            if (GetLine(qi, 5, rights, 0, 6, 12, 18, 24))
                sum += 5;
            if (GetLine(qi, 5, rights, 4, 8, 12, 16, 20))
                sum += 5;
            //五虎横5
            int[] temp1 = Enumerable.Range(0, 5).ToArray();
            for (int x = 0; x < 5; x++)
            {
                var q = (from c in temp1 select c + 5 * x).ToArray();
                if (GetLine(qi, 4, rights, q))
                    sum += 4;
            }
            //五虎竖5
            int[] temp2 = Enumerable.Range(0, 5).ToArray();
            for (int x = 0; x < 5; x++)
            {
                var q = (from c in temp2 select c * 5 + x).ToArray();
                if (GetLine(qi, 4, rights, q))
                    sum += 4;
            }
            //四斜
            if (GetLine(qi, 3, rights, 1, 7, 13, 19))
                sum += 3;
            if (GetLine(qi, 3, rights, 5, 11, 17, 23))
                sum += 3;

            if (GetLine(qi, 3, rights, 3, 7, 11, 15))
                sum += 3;
            if (GetLine(qi, 3, rights, 9, 13, 17, 21))
                sum += 3;

            //三斜
            if (GetLine(qi, 2, rights, 2, 6, 10))
                sum += 2;
            if (GetLine(qi, 2, rights, 2, 8, 14))
                sum += 2;

            if (GetLine(qi, 2, rights, 10, 16, 22))
                sum += 2;
            if (GetLine(qi, 2, rights, 14, 18, 22))
                sum += 2;

            //井
            for (int x = 0; x < 19; x++)
            {
                if (x % 5 == 4) continue;
                if ((qi[x] + qi[x + 1] + qi[x + 5] + qi[x + 6]) == 4)
                {
                    sum += 1;
                    rights[x] += 0.25;
                    rights[x + 1] += 0.25;
                    rights[x + 5] += 0.25;
                    rights[x + 6] += 0.25;
                }
            }
            return sum;
        }

        public static bool GetLine(int[] qi, double fen, double[] rights, params int[] arr)
        {
            int sum = 0;
            foreach (int x in arr)
            {
                sum += qi[x];
            }
            if (sum == arr.Length)
            {
                foreach (int x in arr)
                {
                    rights[x] += fen / arr.Length;
                }
                return true;
            }
            return false;
        }

    }
}



