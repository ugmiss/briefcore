using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupportCenter.Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            string zstr = "ababcabababdc";
            string mstr = "babdc";
            var index = KMP(zstr, mstr);
            if (index == -1)
                Console.WriteLine("没有匹配的字符串！");
            else
                Console.WriteLine("哈哈，找到字符啦，位置为：" + index);
            Console.Read();
        }
        static int KMP(string bigstr, string smallstr)
        {
            int i = 0;
            int j = 0;
            //计算“前缀串”和“后缀串“的next  
            int[] next = GetNextVal(smallstr);
            while (i < bigstr.Length && j < smallstr.Length)
            {
                if (j == -1 || bigstr[i] == smallstr[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    j = next[j];
                }
            }
            if (j == smallstr.Length)
                return i - smallstr.Length;
            return -1;
        }

        /// <summary>  
        /// p0,p1....pk-1         （前缀串）  
        /// pj-k,pj-k+1....pj-1   （后缀串)  
        /// </summary>  
        /// <param name="match"></param>  
        /// <returns></returns>  
        static int[] GetNextVal(string smallstr)
        {
            //前缀串起始位置("-1"是方便计算）  
            int k = -1;
            //后缀串起始位置（"-1"是方便计算）  
            int j = 0;
            int[] next = new int[smallstr.Length];
            //根据公式： j=0时，next[j]=-1  
            next[j] = -1;
            while (j < smallstr.Length - 1)
            {
                if (k == -1 || smallstr[k] == smallstr[j])
                {
                    //pk=pj的情况: next[j+1]=k+1 => next[j+1]=next[j]+1  
                    next[++j] = ++k;
                }
                else
                {
                    //pk != pj 的情况:我们递推 k=next[k];  
                    //要么找到，要么k=-1中止  
                    k = next[k];
                }
            }
            return next;
        }
    }
}