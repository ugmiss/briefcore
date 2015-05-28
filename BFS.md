### 广度优先遍历磁盘目录 ###
无论广度还是深度都是串行计算。
```
/// <summary>
/// 广度优先搜索算法。
/// </summary>
public class BreadthFirstSearch
{
    public static int BFS(DirectoryInfo dir)
    {
        int fileCount = 0;
        int dirCount = 0;
        //文件队列
        Queue<DirectoryInfo> dirList = new Queue<DirectoryInfo>();
        dirList.Enqueue(dir);
        //是否包含有子文件夹
        bool hasChild = true;
        while (hasChild)
        {
            DirectoryInfo currentDir = dirList.Dequeue();
            dirCount++;
            try
            {
                FileInfo[] fiList = currentDir.GetFiles();
                foreach (FileInfo fileinfo in fiList)
                {
                    if (fileinfo.Exists)
                    {
                        fileCount++;
                    }
                }
            }
            catch { }
            try
            {
                DirectoryInfo[] diList = currentDir.GetDirectories();
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
                hasChild = false;
            }
        }
        return fileCount;
    }
}
```