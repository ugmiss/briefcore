namespace Caching
{
    public interface ICacheItemRefreshAction
    {
        void Refresh(string removedKey, object expiredValue, EnumRemovedReason removalReason);
    }
}
