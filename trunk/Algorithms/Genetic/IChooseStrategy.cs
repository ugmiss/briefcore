using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Genetic
{
    /// <summary>
    /// 选择策略接口
    /// </summary>
    public interface IChooseStrategy
    {
        int[][] GetNewChoose(int[][] population);
    }
}
