using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public class ArrayHelper
    {
        public static T[] Shuffle<T>(T[] arr)
        {
            for (int i = arr.Length; i >= 1; i--)
            {
                int x = RandomFactory.Next(i);
                T temp = arr[i - 1];
                arr[i - 1] = arr[x];
                arr[x] = temp;
            }
            return arr;
        }
    }
}
