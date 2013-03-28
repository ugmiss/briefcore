using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Algorithms
{
    public class DepthFirstSearch
    {
        /// <summary>
        /// 深度优先遍历 : 用栈 =二叉树的前序遍历  （传统深度遍历）
        /// </summary>
        /// <param name="root"></param>
        public static int DFS(DirectoryInfo root)
        {
            int fileCount = 0;
            long dirCount = 0;
            Stack<DirectoryInfo> dirStack = new Stack<DirectoryInfo>();
            dirStack.Push(root);
            while (dirStack.Count != 0)
            {
                DirectoryInfo current = dirStack.Pop();
                dirCount++;
                try
                {
                    FileInfo[] fiList = current.GetFiles();
                    foreach (FileInfo node in fiList)
                    {
                        fileCount++;
                    }
                }
                catch { }
                try
                {
                    DirectoryInfo[] dirList = current.GetDirectories();
                    foreach (DirectoryInfo node in dirList)
                    {
                        dirStack.Push(node);
                    }
                }
                catch { }
            }
            return fileCount;
        }
    }
}
