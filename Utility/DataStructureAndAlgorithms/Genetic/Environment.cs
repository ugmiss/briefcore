using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.DataStructureAndAlgorithms.Genetic
{
    /// <summary>
    /// 环境
    /// </summary>
    public  class Environment
    {
        /// <summary>
        /// 种群个体数量。
        /// </summary>
        public static int PopulationCount = 10;
        /// <summary>
        /// 染色体长度。
        /// </summary>
        public static int ChromosomeLength = 4;
        /// <summary>
        /// 总代数。
        /// </summary>
        public static int MaxGenarationCount = 50;

        public static List<Individual> list = new List<Individual>();

    }
}
