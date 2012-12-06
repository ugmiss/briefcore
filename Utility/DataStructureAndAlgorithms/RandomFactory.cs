using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.DataStructureAndAlgorithms
{
    public class RandomFactory
    {
        public static int Next(int max)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return random.Next(max);
        }
        public static int Next(int min, int max)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return random.Next(min, max);
        }
        public static double NextDouble()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return random.NextDouble();
        }
    }
}
