### Map ###
映射
### Reduce ###
化简
### 应用 ###
在Google，MapReduce用在非常广泛的应用程序中，包括“分布grep，分布排序，web连接图反转，每台机器的词矢量，web访问日志分析，反向索引构建，文档聚类,机器学习，基于统计的机器翻译...”值得注意的是，MapReduce实现以后，它被用来重新生成Google的整个索引，并取代老的ad hoc程序去更新索引。
MapReduce会生成大量的临时文件，为了提高效率，它利用Google文件系统来管理和访问这些文件。
其他实现
Nutch项目开发了一个实验性的MapReduce的实现，也即是后来大名鼎鼎的hadoop
Phoenix是斯坦福大学开发的基于多核/多处理器、共享内存的MapReduce实现。