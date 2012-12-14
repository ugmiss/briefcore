using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Genetic
{
    public class AbstractGeneticHelper
    {
        public Func<int[], double> FitnessFunc = p => { return 0; };
        public Func<int[]> ChromosomeFunc = () => { return null; };
        public Func<int[]> ChooseFunc = () => { return null; };
        public Func<int[], int[], int[][]> CrossFunc = (a, b) => { return null; };
        public Action<int[]> MutationAction = (p) => { };
    }
}
