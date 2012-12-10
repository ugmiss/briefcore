using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.DataStructureAndAlgorithms.Genetic
{
    /// <summary>
    /// 适应函数
    /// </summary>
    public class FitnessFunction
    {
        public static double GetFitnessRate(Individual individual, Func<Individual, double> func)
        {
            return func(individual);
        }
    }
}
