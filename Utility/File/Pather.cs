using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Utility
{
    public class Pather
    {
        public static string GetLastName(string absolutepath)
        {
            if (absolutepath.IndexOf(".") >= 0)
                return absolutepath.Substring(absolutepath.LastIndexOf("\\"), (absolutepath.LastIndexOf(".") - absolutepath.LastIndexOf("\\"))).Replace("\\", "");
            else
                return absolutepath.Substring(absolutepath.LastIndexOf("\\")).Replace("\\", "");
        }
    }
}
