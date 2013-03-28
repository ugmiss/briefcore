using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Algorithms
{
    public class TreeParallelSearch
    {
        public static int TraverseTreeParallelForEach(DirectoryInfo root)//, Action<string> action)
        {
            int fileCount = 0;
            int procCount = System.Environment.ProcessorCount;
            Stack<DirectoryInfo> dirStack = new Stack<DirectoryInfo>();
            dirStack.Push(root);
            while (dirStack.Count > 0)
            {
                DirectoryInfo currentDir = dirStack.Pop();
                DirectoryInfo[] subDirs = null;
                FileInfo[] files = null;
                try
                {
                    subDirs = currentDir.GetDirectories();
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                catch (DirectoryNotFoundException)
                {
                    continue;
                }

                try
                {
                    files = currentDir.GetFiles();
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                catch (DirectoryNotFoundException)
                {
                    continue;
                }
                try
                {
                    if (files.Length < procCount)
                    {
                        foreach (var file in files)
                        {
                            fileCount++;
                        }
                    }
                    else
                    {
                        Parallel.ForEach(files, () => 0, (file, loopState, localCount) =>
                        {
                            return (int)++localCount;
                        },
                        (c) =>
                        {
                            Interlocked.Exchange(ref fileCount, fileCount + c);
                        });
                    }
                }
                catch (AggregateException ae)
                {
                    ae.Handle((ex) =>
                        {
                            if (ex is UnauthorizedAccessException)
                            {
                                return true;
                            }
                            return false;
                        });
                }
                foreach (var str in subDirs)
                    dirStack.Push(str);
            }
            return fileCount;
        }
    }
}
