using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.DataStructureAndAlgorithms.Genetic
{
    /// <summary>
    /// 染色体
    /// </summary>
    public class Chromosome
    {
        public int[] GeneArray = new int[Environment.ChromosomeLength];
        public Chromosome()
        { 
           for(int i=0;i<Environment.ChromosomeLength;i++)
           {
               GeneArray[i] = RandomFactory.Next(1);
           }
        }
    }
}
