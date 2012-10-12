using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.Mac
{
    public class MacAddressGenerator
    {
        const string _allPossibleCharacters = "0123456789ABCDEF";
        readonly Random _randomGenerator = new Random();
        public MacAddressGenerator(int number)
        {
            MacAddresses = new List<string>();
            for (int i = 0; i < number; i++)
            {
                StringBuilder macAddress = new StringBuilder();
                for (int i2 = 0; i2 < 12; i2++)
                {
                    macAddress.Append(_allPossibleCharacters[_randomGenerator.Next(16)]);
                }
                MacAddresses.Add(Format(macAddress.ToString()));
            }
        }
        public List<string> MacAddresses { get; private set; }
        static string Format(string strToFormat)
        {
            string r = string.Empty;
            for (int i = 0; i < 12; i += 2)
            {
                r += strToFormat.Substring(i, 2) + ":";
            }
            r = r.Remove(r.Length - 1, 1);
            return r;
        }
    }
}
