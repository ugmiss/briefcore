using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Caching
{
    [Serializable]
    [ComVisible(false)]
    public class FileExpiration : ICacheItemExpiration
    {
        private readonly string dependencyFileName;
        private DateTime lastModifiedTime;
        public FileExpiration(string fullFileName)
        {
            if (string.IsNullOrEmpty(fullFileName))
            {
                throw new ArgumentException("fullFileName", "文件名不能为空");
            }
            dependencyFileName = Path.GetFullPath(fullFileName);
            EnsureTargetFileAccessible();
            if (!File.Exists(dependencyFileName))
            {
                throw new ArgumentException("fullFileName","非法的文件名");
            }
            this.lastModifiedTime = File.GetLastWriteTime(fullFileName);
        }
        public string FileName
        {
            get { return dependencyFileName; }
        }
        public DateTime LastModifiedTime
        {
            get { return lastModifiedTime; }
        }
        public bool HasExpired()
        {
            EnsureTargetFileAccessible();
            if (File.Exists(this.dependencyFileName) == false)
            {
                return true;
            }
            DateTime currentModifiedTime = File.GetLastWriteTime(dependencyFileName);
            if (DateTime.Compare(lastModifiedTime, currentModifiedTime) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Notify()
        {
        }
        public void Initialize(CacheItem owningCacheItem)
        {
        }
        private void EnsureTargetFileAccessible()
        {
            string file = dependencyFileName;
            FileIOPermission permission = new FileIOPermission(FileIOPermissionAccess.Read, file);
            permission.Demand();
        }
    }
}
