using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.DataStructureAndAlgorithms.Genetic.Choose;

namespace Utility.DataStructureAndAlgorithms.Genetic
{
    /// <summary>
    /// 进化。
    /// </summary>
    public class Revolution
    {
        public static Individual EliteIndividual { get; set; }
        public static IChooseStrategy ChooseStrategy { get; set; }
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
                Individual indi = new Individual();
                indi.Chromosome = new Chromosome();
                population.IndividualList.Add(indi);
            }
            CurrentPopulation = population;
            return population;
        }

        public static Individual GetBestIndividual()
        {
            List<Individual> r = CurrentPopulation.IndividualList.OrderByDescending(p => p.Fitness).ToList();
            return r[0];
        }


        public static Population CurrentPopulation { get; set; }
        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Individual[] Choose(Func<Individual, double> func)
        {
            double SumFitness = 0;
            foreach (Individual individual in CurrentPopulation.IndividualList)
            {
                individual.Fitness = FitnessFunction.GetFitnessRate(individual, func);
                SumFitness += individual.Fitness;
            }
            foreach (Individual individual in CurrentPopulation.IndividualList)
            {
                if (SumFitness == 0)
                    individual.ProbabilityOfSelect = 0;
                else
                    individual.ProbabilityOfSelect = individual.Fitness / SumFitness;
            }
            return ChooseStrategy.GetNewChoose(CurrentPopulation.IndividualList.ToArray());
        }

        public static List<Individual> GetList()
        {
            return Environment.list;
        }

        public static void Begin(Func<Individual, double> func, int PopulationCount, int ChromosomeLength, int MaxGenarationCount)
        {
            Environment.ChromosomeLength = ChromosomeLength;
            Environment.MaxGenarationCount = MaxGenarationCount;
            Environment.PopulationCount = PopulationCount;
            GetFirstPopulation();
            for (int i = 0; i < Environment.MaxGenarationCount; i++)
            {
                //孵化池
                Individual[] MatingPool = Choose(func);
                //随机交配
                MatingPool = Crossover.NewCrossover(MatingPool);
                //随机突变
                if (RandomFactory.NextDouble() < .01)
                {
                    MatingPool[0].Chromosome = Mutation.NewMutation(MatingPool[0].Chromosome);
                }
                //新生代
                Population newPopulation = new Population();
                for (int x = 0; x < Environment.PopulationCount; x++)
                {
                    if (RandomFactory.NextDouble() < .5)
                    {
                        newPopulation.IndividualList.Add(MatingPool[RandomFactory.Next(MatingPool.Length)]);
                    }
                    else
                    {
                        if (EliteIndividual != null)
                        {
                            newPopulation.IndividualList.Add(EliteIndividual.Clone() as Individual);
                        }
                        else
                        {
                            Individual indi = new Individual();
                            indi.Chromosome = new Chromosome();
                            newPopulation.IndividualList.Add(indi);
                        }
                    }
                }
                if (EliteIndividual != null)
                {
                    newPopulation.IndividualList.RemoveAt(RandomFactory.Next(MatingPool.Length));
                    newPopulation.IndividualList.Add(EliteIndividual);
                }
                CurrentPopulation = newPopulation;
                if (EliteIndividual != null && GetBestIndividual().Fitness > EliteIndividual.Fitness)
                    EliteIndividual = GetBestIndividual();
            }
        }
    }
}
