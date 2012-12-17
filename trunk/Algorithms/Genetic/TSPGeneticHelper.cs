using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace Algorithms.Genetic
{
    public class City
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public class TSPGeneticHelper
    {
        public static List<City> List { get; set; }
        public static double[][] DistanceMap { get; set; }
        public TSPGeneticHelper(List<City> cityList)
        {
            List = cityList;
            int citycount = List.Count;
            //计算距离矩阵。
            DistanceMap = new double[citycount][];
            for (int i = 0; i < citycount; i++)
            {
                DistanceMap[i] = new double[citycount];
            }
            for (int i = 0; i < citycount; i++)
            {
                for (int k = 0; k < citycount; k++)
                {
                    if (i == k)
                        continue;
                    var CityA = cityList[i];
                    var CityB = cityList[k];
                    DistanceMap[i][k] = System.Math.Sqrt((CityA.X - CityB.X) * (CityA.X - CityB.X) + (CityA.Y - CityB.Y) * (CityA.Y - CityB.Y));
                }
            }
        }
        /// <summary>
        /// 适应度函数。
        /// </summary>
        public static Func<int[], double> FitnessFunc = p =>
        {
            double SumDistance = 0;
            for (int i = 0; i < p.Length - 1; i++)
            {
                SumDistance += DistanceMap[p[i]][p[i + 1]];
            }
            SumDistance += DistanceMap[0][p.Length - 1];
            return 1000 / SumDistance;//路径和越小，适应度越高
        };
        /// <summary>
        /// 染色体生成函数。
        /// </summary>
        public static Func<int[]> ChromosomeFunc = () =>
        {
            int[] chromosome = new int[List.Count];
            for (int i = 0; i < List.Count; i++)
            {
                chromosome[i] = i;
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
            int indexA = RandomFactory.Next(a.Length);
            int indexB = RandomFactory.Next(a.Length);
            if (indexA > indexB)
            {
                int temp = indexA;
                indexA = indexB;
                indexB = temp;
            }
            int[] ab = new int[a.Length];
            int[] ba = new int[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                if (i > indexA && i <= indexB)
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
            int[] intersect = ba.Intersect(ab).ToArray();
            while (intersect.Length != ba.Length)
            {
                int indexa = 0;
                int indexb = 0;
                int tempx;
                bool flag = false;
                for (int i = 0; i < ab.Length - 1; i++)
                {
                    if (flag) break;
                    tempx = ab[i];
                    indexa=i;
                  
                    for (int k = i + 1; k < ab.Length; k++)
                    {
                        if (tempx == ab[k])
                        {
                            int tp=ab[k];
                            ab[k]=ba[indexa];
                            ba[indexa]=tp;
                            flag = true;
                            break;
                        }
                    }
                }
                intersect = ba.Intersect(ab).ToArray();
            }
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
            p[indexB] =temp;
        };
    }
}
