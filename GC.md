### SOH与LOH ###
  * 小于85000byte进SOH
  * 大于85000byte进LOH
  * SOH 小对象堆，会做碎片压缩
  * LOH 大对象堆，不做碎片压缩，成本较高
### GC ###
```
GC.Collect();
GC.SuppressFinalize(this);
```