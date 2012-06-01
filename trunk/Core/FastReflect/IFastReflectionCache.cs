﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public interface IFastReflectionCache<TKey, TValue>
    {
        TValue Get(TKey key);
    }
}
