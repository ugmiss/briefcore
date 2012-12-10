using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.DataStructureAndAlgorithms.Genetic
{
    /// <summary>
    /// 种群
    /// </summary>
    public class Population
    {
        public int Count
        {
            get
            {
                return IndividualList.Count;
            }
        }
        /// <summary>
        /// 个体集合。
        /// </summary>
        public List<Individual> IndividualList = new List<Individual>();

    }
}
