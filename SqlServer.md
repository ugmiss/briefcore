### 刷新视图 ###
视图保存了元数据，当基础表结构变化时，视图并不会更新结构所以需要执行下面的sql
```
EXEC sp_refreshview V_Name
```


### Raid ###
  1. RAID 0：通过并行读取来提高数据I/O, 读取操作效率很高, 但是不提供数据容错及保护. 不推荐作为SQL Server使用.
  1. RAID 1：镜像保护,有两个驱动器,一个做主驱动器,一个做镜像, 所以是实际需要两倍的驱动器,第二个所为冗余使用. 使用RAID 1我们的存储容量应该是（n`*`s/2）。一次写操作写入到两个磁盘, 所以虽写入速度会稍微有影响, 但是读取速度几乎是大多数情况下的两倍. 因为在读取操作过程中驱动器可以并行地进行访问，从而提高了吞吐量。RAID 1限制于两个驱动器。
  1. RAID 5:带校验的磁盘条带。在这种类型的RAID中，数据以复杂条带的形式写入到阵列中的所有驱动器中，同时所有驱动器中都有分布数校验块。这样RAID 5就可能使用三个或者更多磁盘组成的任意大小的阵列，只牺牲相当于一个磁盘的存储容量用于校验。但是这种校验是分布式的，并不单独存在于任何一个物理磁盘中.RAID 5由于在大型阵列中牺牲的存储容量较少，所以它具有成本效益的特点，从而被人们所广泛使用。与镜像不同的是，带有校验的条带要求必须在磁盘之间进行针对每个写入条带的计算，这造成了一部分的开销。因此，吞吐量并不总是一个容易计算的项目，它在很大程度上取决于系统在做校验计算时候的计算能力。计算RAID 5的容量非常简单：就是（n-1）`*`s）。RAID 5阵列可以避免这列中任何单个磁盘的丢失.对RAID5的每一次写操作, 都会涉及到多个读用于计算并且存储. 对SQL Server有很多的写操作,并且要求很高效率的时, RAID 5并不是一个很好的选择.
  1. RAID 6：带双重校验的磁盘条带。RAID 6与RAID 5非常相似，但它的每个条带使用两个校验块，而不是一个，这加强了应对磁盘故障的保护能力。RAID 6是RAID家族中的新成员。RAID 6是其他几个RAID类型实现标准化几年之后增加的。RAID 6比较特殊，因为它可以承受阵列中任意两个驱动器的故障，同时防止数据丢失。但是为了配合额外的冗余度，RAID 6阵列需要牺牲阵列中相当于两个驱动器的容量，并要求真列中最少有四个驱动器。RAID 6的容量可以用（（n-2）`*`s）来计算。
  1. RAID 10:带条带的镜像。从技术上来说，RAID 10是一种混合的RAID，包括存在于一个非校验条带（RAID 0）中的一对RAID镜像。当一个阵列中只有两个驱动器的时候，很多厂商会称其为RAID 10（或者RAID 10+），但从技术上来说这应该是RAID 1，因为阵列中至少有四个驱动器才会发生条带化。对于RAID 10来说，驱动器必须是一对一对添加的，因此阵列中的驱动器数量只可能是偶数。RAID 10可以在丢失近半数驱动器组的情况下正常运转，同时最多只能承受每个驱动器中一个驱动器发生故障或者丢失。RAID 10不包含校验计算，这使得它相对RAID 5和RAID 6来说具有一定的性能优势，而且阵列对计算能力的要求也更低。RAID 10提供了超过任何一种常见类型RAID的读取性能，因为在读取操作中阵列中的所有驱动器都可同时使用。但是RAID 10的写入性能要低很多。RAID 10的容量计算方法和RAID 1相同，都是（n`*`s/2）。
```
读效率: 因为是并行读取, 读取效率都很高.
写效率: RAID 0 > RAID 1 > RAID 10 > RAID 5
磁盘利用率: RAID 0 > RAID 5 > RAID 1 = RAID 10
容错能力:  RAID 10 = RAID 1 > RAID 5 > RAID 0
作为SQL Server 的DB Server建议使用RAID 1 或RAID10.
```

### 跨库 ###
同server跨库查询与同库查询性能相同，linked server跨库查询性能差;所以同server上的跨库存储过程和跨库视图都没有性能问题。