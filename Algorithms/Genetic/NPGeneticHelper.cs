using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace Algorithms.Genetic
{

    /// <summary>
    /// 货物
    /// </summary>
    public class Deal
    {
        public double Weight { get; set; }
        public double Value { get; set; }
    }

    public class NPGeneticHelper
    {
        public static List<Deal> List { get; set; }
        public static double MaxWeight { get; set; }
        public static double MaxGuess { get; set; }
        public NPGeneticHelper(double maxWeight, List<Deal> list)
        {
            List = list;
            MaxWeight = maxWeight;
            Deal best = list.OrderByDescending(p => p.Value / p.Weight).ToArray()[0];
            MaxGuess = MaxWeight / best.Weight * best.Value;
        }
        public static Func<int[], double> FitnessFunc = p =>
        {
            double SumWeight = 0;
            double SumValue = 0;
            for (int x = 0; x < p.Length; x++)
            {
                if (p[x] == 1)
                {
                    Deal d = List[x];
                    SumWeight += d.Weight;
                    SumValue += d.Value;
                }
            }
            if (SumWeight > MaxWeight)
                return 0;
            else
                return (double)1 / (MaxGuess - SumValue);
        };
        public static Func<int[]> ChromosomeFunc = () =>
        {
            int[] chromosome = new int[List.Count];
            for (int i = 0; i < List.Count; i++)
            {
                chromosome[i] = RandomFactory.Next(2);
            }
            return chromosome;
        };
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
        public static Func<int[], int[], int[][]> CrossFunc = (a, b) =>
        {
            int index = RandomFactory.Next(a.Length);
            int[] ab = new int[a.Length];
            int[] ba = new int[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                if (i < index)
                {
                    ab[i] = a[i];
                    ba[i] = b[i];
                }
                else
                {
                    ab[i] = b[i];
                    ba[i] = a[i];
                }
            }
            return new int[][] { ab, ba };
        };
        public static Action<int[]> MutationAction = (p) =>
        {
            int index = RandomFactory.Next(p.Length);
            p[index] = p[index] == 1 ? 0 : 1;
        };
    }
}
