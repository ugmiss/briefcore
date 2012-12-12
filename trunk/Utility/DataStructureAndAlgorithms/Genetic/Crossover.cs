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
            List<Individual> r = new List<Individual>();
            for (int j = 0; j < IndividualArray.Length / 2; j++)
            {
                int CrossIndex = RandomFactory.Next(Environment.ChromosomeLength);
                Individual i1 = IndividualArray[j].Clone() as Individual;
                Individual i2 = IndividualArray[IndividualArray.Length - 1 - j].Clone() as Individual;
                for (int x = CrossIndex; x < Environment.ChromosomeLength; x++)
                {
                    int temp = i1.Chromosome.GeneArray[x];
                    i1.Chromosome.GeneArray[x] = i2.Chromosome.GeneArray[x];
                    i2.Chromosome.GeneArray[x] = temp;
                }
                r.Add(i1);
                r.Add(i2);
            }
            if (IndividualArray.Length % 2 == 1)
            {
                r.Add(IndividualArray[IndividualArray.Length / 2].Clone() as Individual);
            }
            return r.ToArray();
        }
    }
}
