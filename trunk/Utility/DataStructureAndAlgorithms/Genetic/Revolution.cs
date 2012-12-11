using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.DataStructureAndAlgorithms.Genetic
{
    /// <summary>
    /// 进化。
    /// </summary>
    public class Revolution
    {
        /// <summary>
        /// 每代
        /// </summary>
        Dictionary<int, Population> List = new Dictionary<int, Population>();
        /// <summary>
        /// 第一代。
        /// </summary>
        /// <returns></returns>
        public static Population GetFirstPopulation()
        {
            Population population = new Population();
            for (int i = 0; i < Environment.PopulationCount; i++)
            {
                Individual indi= new Individual();
                indi.Chromosome = new Chromosome();
                population.IndividualList.Add(indi);
            }
            CurrentPopulation = population;
            return population;
        }
        public static Population CurrentPopulation { get; set; }
        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Individual[] Choose(Func<Individual, double> func)
        {
            List<Individual> li = CurrentPopulation.IndividualList.OrderBy(p => FitnessFunction.GetFitnessRate(p, func)).ToList();
            return new Individual[] { li[0], li[1] };
        }

        public static List<Individual> GetList()
        {
            return Environment.list;
        }

        public static void Begin(Func<Individual, double> func, int PopulationCount,int ChromosomeLength,int MaxGenarationCount )
        {
            Environment.ChromosomeLength = ChromosomeLength;
            Environment.MaxGenarationCount = MaxGenarationCount;
            Environment.PopulationCount = PopulationCount;
            GetFirstPopulation();
            for (int i = 0; i < Environment.MaxGenarationCount; i++)
            {

                Individual[] temp2 = Choose(func);
               
                foreach (var t in temp2)
                {
                    if (FitnessFunction.GetFitnessRate(t, func) > 0)
                    {
                        Environment.list.Add(t);
                    }
                }

                //孵化池
                Individual[] MatingPool = new Individual[Environment.PopulationCount];
                //随机交配
                if (RandomFactory.NextDouble() < .5)
                {
                    temp2 = Crossover.NewCrossover(temp2);
                }
                //随机突变
                if (RandomFactory.NextDouble() < .05)
                {
                    temp2[0].Chromosome = Mutation.NewMutation(temp2[0].Chromosome);
                }
                //新生代
                Population newPopulation = new Population();
                newPopulation.IndividualList.AddRange(temp2);
                for (int k= 2; k < Environment.PopulationCount; k++)
                { 
                    Individual indi=new Individual();
                    indi.Chromosome=new Chromosome();
                    newPopulation.IndividualList.Add(indi);
                }
                CurrentPopulation = newPopulation;
            }
        }
    }
}
