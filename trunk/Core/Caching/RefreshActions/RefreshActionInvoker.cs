using System;
using System.Threading;

namespace Caching
{
    public static class RefreshActionInvoker
    {
        public static void InvokeRefreshAction(CacheItem removedCacheItem, EnumRemovedReason removalReason)
        {
            if (removedCacheItem == null) throw new ArgumentNullException("removedCacheItem");
            if (removedCacheItem.RefreshAction == null)
            {
                return;
            }
            try
            {
                RefreshActionData refreshActionData =
                    new RefreshActionData(removedCacheItem.RefreshAction, removedCacheItem.Key, removedCacheItem.Value, removalReason);
                refreshActionData.InvokeOnThreadPoolThread();
            }
            catch
            {
            }
        }
        private class RefreshActionData
        {
            private ICacheItemRefreshAction refreshAction;
            private string keyToRefresh;
            private object removedData;
            private EnumRemovedReason removalReason;
            public RefreshActionData(ICacheItemRefreshAction refreshAction, string keyToRefresh, object removedData, EnumRemovedReason removalReason)
            {
                this.refreshAction = refreshAction;
                this.keyToRefresh = keyToRefresh;
                this.removalReason = removalReason;
                this.removedData = removedData;
            }
            public ICacheItemRefreshAction RefreshAction
            {
                get { return refreshAction; }
            }
            public string KeyToRefresh
            {
                get { return keyToRefresh; }
            }
            public EnumRemovedReason RemovalReason
            {
                get { return removalReason; }
            }
            public object RemovedData
            {
                get { return removedData; }
            }
            public void InvokeOnThreadPoolThread()
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPoolRefreshActionInvoker));
            }
            private void ThreadPoolRefreshActionInvoker(object notUsed)
            {
                try
                {
                    RefreshAction.Refresh(KeyToRefresh, RemovedData, RemovalReason);
                }
                catch 
                {
                }
            }
        }
    }
}
