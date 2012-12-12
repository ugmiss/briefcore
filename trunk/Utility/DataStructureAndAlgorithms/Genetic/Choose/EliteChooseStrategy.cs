using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.DataStructureAndAlgorithms.Genetic.Choose
{
    /// <summary>
    /// 精英选择策略
    /// </summary>
    public class EliteChooseStrategy:IChooseStrategy
    {
        public Individual[] GetNewChoose(Individual[] population)
        {
            return population;
        }
    }
}
