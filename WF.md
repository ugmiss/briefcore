### CodeActivity与NativeActivity区别 ###
> CodeActivity为CodeActivityContext，它只是一个简化版的上下文环境，对Runtime没有完全的访问权限。
> 而NativeActivity的上下文环境，ActivityExecutionContext，ActivityExecutionContext支持scheduling，取消子活动的执行，持久化的支持，书签的支持等等对runtime有完全的访问权限。
### Variable与Argument的区别 ###
> Variable是为了方便可视化流程设计，他的作用域是Activity内部
> Argument 分输入输出
> > 输入参数：外部参数传入。
> > 输出参数：参数Activity外部获取。
### 常用Activity ###
> > 控制流:`DoWhile ForEach<T> If Parallel ParallelForEach<T> Pick PickBranch Sequence Switch<T> While`


> 流程图：`FlowChart FlowDecision FlowSwitch<T>`
## 自定义活动输入参数为集合 ##
`public InArgument<ICollection<T>> Collection { get; set; }ICollection<T> underlyingCollection = this.Collection.Get<ICollection<T>>(context);`
初始化Collection变量 eg： `New System.Collections.ObjectModel.Collection(Of Integer) From {1, 2, 3, 4}`
### InstanceView 类 ###
表示一个实例视图。例如，Execute(InstanceHandle, InstancePersistenceCommand, TimeSpan) 方法实现可返回一个 InstanceView 对象，该对象提供显示永久性存储区中的实例数据的视图。
每个 InstanceView 对象均与 InstanceHandle 关联。 对于从 InstanceStore.Execute 返回的 InstanceView，关联句柄是传递给 Execute 的句柄。 对于从 InstancePersistenceContext.InstanceView 返回的 InstanceView，关联句柄是传递给 TryCommand 的句柄。
在任何给定时间，InstanceHandle 可以具有 0 个或多个与之关联的 InstanceView 对象。 从 InstanceStore.Execute 返回的 InstanceView 对象是不可变的，并且表示成功执行此命令后已知实例状态的快照。 从 InstancePersistenceContext 返回的 InstanceView 对象是可变的，并且表示当前已知的实例状态，因为它会在执行此命令期间不断更新。 （如果此命令最终失败，则会丢弃此中间状态）。
public InstanceView Execute( InstanceHandle handle, InstancePersistenceCommand command, TimeSpan timeout)
handle
类型：System.Runtime.DurableInstancing.InstanceHandle
一个实例句柄。
command
类型：System.Runtime.DurableInstancing.InstancePersistenceCommand
要执行的命令。
timeout
类型：System.TimeSpan
此操作的超时值。
返回值类型：System.Runtime.DurableInstancing.InstanceView
一个 InstanceView 对象，表示成功完成此命令后此实例的已知状态。如果在事务下调用了 Execute，则此状态可以包含未提交的数据。一旦成功提交此事务，InstanceView 对象中的数据就被认为是已提交eg:
`static InstanceStore instanceStore;instanceStore =  new SqlWorkflowInstanceStore(@"Data Source=.;Initial Catalog=WF4;uid=sa;pwd=123456;Asynchronous Processing=True"); InstanceHandle handle = instanceStore.CreateInstanceHandle();             InstanceView view = instanceStore.Execute(handle, new CreateWorkflowOwnerCommand(), TimeSpan.FromSeconds(30));           handle.Free();instanceStore.DefaultInstanceOwner = view.InstanceOwner;...........application.InstanceStore = instanceStore; `
### 对于BookMark的理解 ###
> Activity如果是方法 BookMark就是断点 断点的作用就是当流程中需要让流程停止在某个点上 在条件或是发生某些事件的时候，再去掉这个断点，让流程进行下去。