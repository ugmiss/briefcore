using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Algorithms
{
    public class Search
    {
        public void BFS(DirectoryInfo dir, ref long nfile, ref long ndir)
        {
            try
            {
                IEnumerable<string> ieDir = Directory.EnumerateDirectories(dir.FullName);
                IEnumerator<string> iDir = ieDir.GetEnumerator();
                do
                {
                    if (iDir.Current != null)
                    {
                        Console.WriteLine(iDir.Current.ToString());
                        ndir++;
                        try
                        {
                            IEnumerable<string> ieFile = Directory.EnumerateFiles(iDir.Current.ToString());
                            IEnumerator<string> iFile = ieFile.GetEnumerator();
                            do
                            {
                                if (iFile.Current != null)
                                {
                                    Console.WriteLine(iFile.Current.ToString());
                                    nfile++;
                                }
                            }
                            while (iFile.MoveNext());
                        }
                        catch
                        {
                        }
                        BFS(new DirectoryInfo(iDir.Current.ToString()), ref nfile, ref ndir);
                    }
                }
                while (iDir.MoveNext());
            }
            catch
            {
            }
        }

        public void BFS2(DirectoryInfo dir, ref long nFile, ref long nDir)
        {
            try
            {
                if (dir.Exists)
                {
                    Console.WriteLine(dir.FullName);
                    nDir++;
                    try
                    {
                        FileInfo[] arrFile = dir.GetFiles();
                        foreach (FileInfo fiNode in arrFile)
                        {
                            if (fiNode.Exists)
                            {
                                Console.WriteLine(fiNode.FullName);
                                nFile++;
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                DirectoryInfo[] arrDir = dir.GetDirectories();
                foreach (DirectoryInfo node in arrDir)
                {
                    BFS2(node, ref nFile, ref nDir);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 广度优先遍历 ： 用队列
        /// </summary>
        /// <param name="dir"></param>
        public string BFS(DirectoryInfo dir)
        {
            long nFileCount = 0;
            long nDirCount = 0;
            //文件队列
            Queue<DirectoryInfo> dirList = new Queue<DirectoryInfo>();
            dirList.Enqueue(dir);
            //是否包含有子文件夹
            bool bIsHasChildDir = true;
            while (bIsHasChildDir)
            {
                //1. 文件夹出队  
                //取文件信息 
                //取子文件夹信息 
                //输出出队文件夹信息
                DirectoryInfo WriteDir = dirList.Dequeue();
                Console.WriteLine(WriteDir.FullName);
                nDirCount++;
                try
                {
                    //2.当前目录文件扫描（广度遍历）
                    FileInfo[] fiList = WriteDir.GetFiles();
                    foreach (FileInfo node in fiList)
                    {
                        if (node.Exists)
                        {
                            Console.WriteLine(node.FullName);
                            nFileCount++;
                        }
                    }
                }
                catch { }
                try
                {
                    //3.子文件夹入队
                    DirectoryInfo[] diList = WriteDir.GetDirectories();
                    foreach (DirectoryInfo node in diList)
                    {
                        if (node.Exists)
                        {
                            //入队,同时开始访问文件夹
                            dirList.Enqueue(node);
                        }
                    }
                }
                catch { }
                //队列中没有文件夹可访问
                if (dirList.Count == 0)
                {
                    bIsHasChildDir = false;
                }
            }
            return string.Format("FileCount:{0}, DirCount:{1}", nFileCount, nDirCount);
        }

        /// <summary>
        /// 深度优先遍历 : 用栈 =二叉树的前序遍历  （传统深度遍历）
        /// </summary>
        /// <param name="dir"></param>
        public string DFS(DirectoryInfo dir)
        {
            long nFileCount = 0;
            long nDirCount = 0;
            //1.把root 压入 栈中
            Stack<DirectoryInfo> dirStack = new Stack<DirectoryInfo>();
            dirStack.Push(dir);
            DirectoryInfo parent = null;
            while (dirStack.Count != 0)
            {
                //读顶栈
                DirectoryInfo current = dirStack.Pop();
                //输出当前目录
                Console.WriteLine(current.FullName);
                nDirCount++;
                try
                {
                    FileInfo[] fiList = current.GetFiles();
                    foreach (FileInfo node in fiList)
                    {
                        Console.WriteLine(node.FullName);
                        nFileCount++;
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
            return string.Format("FileCount:{0}, DirCount:{1}", nFileCount, nDirCount);
        }
        class Program
        {

            public static string GetTimeSpanFormatText(DateTime dateStart)
            {
                string strSpan = "span:{0}d{1}h{2}m{3}s{4}ms";
                TimeSpan tsSpan = DateTime.Now - dateStart;
                string strReturn = string.Format(strSpan, tsSpan.Days, tsSpan.Hours, tsSpan.Minutes, tsSpan.Seconds, tsSpan.Milliseconds);
                return strReturn;
            }

            static void Main(string[] args)
            {
                DirectoryInfo root = new DirectoryInfo("D:\\");

                Search lsFile = new Search();
                DateTime start = DateTime.Now;
                Console.WriteLine(start.ToLongTimeString());
                long nFileCount1 = 0;
                long nDirCount1 = 0;
                lsFile.BFS(root, ref nFileCount1, ref nDirCount1);
                Console.WriteLine(GetTimeSpanFormatText(start));
                Console.WriteLine(string.Format("FileCount:{0}, DirCount:{1}", nFileCount1, nDirCount1));
                Console.WriteLine("OK");
                Console.ReadLine();

                Search lsFile2 = new Search();
                DateTime start2 = DateTime.Now;
                long nFileCount = 0;
                long nDirCount = 0;
                lsFile2.BFS2(root, ref nFileCount, ref nDirCount);
                Console.WriteLine(GetTimeSpanFormatText(start2));
                Console.WriteLine(string.Format("FileCount:{0}, DirCount:{1}", nFileCount, nDirCount));
                Console.WriteLine("OK");

                Console.ReadLine();

                Search btree = new Search();
                DateTime start3 = DateTime.Now;
                string strBFS = btree.BFS(root);
                Console.WriteLine(GetTimeSpanFormatText(start3));
                Console.WriteLine(strBFS);
                Console.WriteLine("OK");

                Console.ReadLine();


                Search btree3 = new Search();
                DateTime start5 = DateTime.Now;
                string strResult = btree3.DFS(root);
                Console.WriteLine(GetTimeSpanFormatText(start5));
                Console.WriteLine(strResult);
                Console.WriteLine("OK");

                Console.ReadLine();

            }


        }
    }
}
