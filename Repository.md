## 如何理解Repository ##
在DDD领域驱动模型中，介于Domain层和数据访问层之间，以非泛型的方式，为Domain层提供表对象对Domain对象的映射和转换，按Domain的需要，实现查找，添加，修改，删除等方法。
使用该模式的最大好处就是将领域模型从客户代码和数据映射层之间解耦出来。
### LinqToSql中的应用 ###
  1. 我们将对实体的公共操作部分，提取为IRepository接口，比如常见的增加，删除等方法。如下代码：
```
interface IRepository<T> where T : class
{
    IEnumerable<T> FindAll(Func<T, bool> exp);
    void Add(T entity);
    void Delete(T entity);
    void Save();
}
```
  1. 下面我们实现一个泛型的类来具体实现上面的接口的方法。
```
public class Repository<T> : IRepository<T> where T : class
{
    public DataContext context;
    public Repository(DataContext context)
    {
        this.context = context;
    }
    public IEnumerable<T> FindAll(Func<T, bool> exp)
    {
        return context.GetTable<T>().Where(exp);
    }
    public void Add(T entity)
    {
        context.GetTable<T>().InsertOnSubmit(entity);
    }
    public void Delete(T entity)
    {
        context.GetTable<T>().DeleteOnSubmit(entity);
    }
    public void Save()
    {
        context.SubmitChanges();
    }
}
```
  1. 上面我们实现是每个实体公共的操作，但是实际中每个实体都有符合自己业务的逻辑。我们单独定义另外一个接口，例如：
```
interface IBookRepository : IRepository<Book>
{
    IList<Book> GetAllByBookId(int id);
}
```
  1. 最后该实体的Repository类实现如下：
```
public class BookRepository : Repository<Book>, IBookRepository
{
    public BookRepository(DataContext dc)
        : base(dc)
    { }
    public IList<Book> GetAllByBookId(int id)
    {
        var listbook = from c in context.GetTable<Book>()
                       where c.BookId == id
                       select c;
        return listbook.ToList();
    }
} 
```