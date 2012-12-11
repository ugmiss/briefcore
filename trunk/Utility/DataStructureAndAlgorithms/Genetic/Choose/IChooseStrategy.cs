﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.DataStructureAndAlgorithms.Genetic.Choose
{
    /// <summary>
    /// 选择策略接口
    /// </summary>
    public interface IChooseStrategy
    {
        int GetIndividualIndex(Population population);
    }
}
