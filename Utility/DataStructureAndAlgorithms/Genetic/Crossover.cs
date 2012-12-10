using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.DataStructureAndAlgorithms.Genetic
{
    /// <summary>
    /// 交叉
    /// </summary>
    public class Crossover
    {
        public static Individual[] NewCrossover(Individual[] IndividualArray)
        {
            int CrossIndex = RandomFactory.Next(Environment.ChromosomeLength);
            Individual i1 = IndividualArray[0];
            Individual i2 = IndividualArray[1];
            for (int x = CrossIndex; x < Environment.ChromosomeLength; x++)
            {
                int temp = i1.Chromosome.GeneArray[x];
                i1.Chromosome.GeneArray[x] = i2.Chromosome.GeneArray[x];
                i2.Chromosome.GeneArray[x] = temp;
            }
            return new Individual[] { i1, i2 };
        }
    }
}
