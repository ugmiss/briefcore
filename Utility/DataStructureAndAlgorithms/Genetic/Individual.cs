using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.DataStructureAndAlgorithms.Genetic
{
    /// <summary>
    /// 个体
    /// </summary>
    public class Individual : ICloneable
    {
        /// <summary>
        /// 染色体。
        /// </summary>
        public Chromosome Chromosome { get; set; }
        /// <summary>
        /// 个体适应度。
        /// </summary>
        public double Fitness { get; set; }
        /// <summary>
        /// 选择概率。
        /// </summary>
        public double ProbabilityOfSelect { get; set; }

        public string GetChromosomeString
        {
            get
            {
                if (Chromosome == null) return "";
                return string.Join("", (from c in Chromosome.GeneArray select c.ToString()).ToArray());
            }
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
