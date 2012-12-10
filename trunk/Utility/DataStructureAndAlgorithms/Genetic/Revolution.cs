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
        Population GetFirstPopulation()
        {
            Population population = new Population();
            return population;
        }


    }
}
