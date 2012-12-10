using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.DataStructureAndAlgorithms.Genetic
{
    /// <summary>
    /// 突变
    /// </summary>
    public class Mutation
    {
        /// <summary>
        /// 新的突变。
        /// </summary>
        /// <param name="chromosome"></param>
        /// <returns></returns>
        public static Chromosome NewMutation(Chromosome chromosome)
        {
            // 突变位
            int index = RandomFactory.Next(Environment.ChromosomeLength);
            chromosome.GeneArray[index] = chromosome.GeneArray[index] == 0 ? 1 : 0;
            return chromosome;
        }
    }
}
