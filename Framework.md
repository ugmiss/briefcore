### Marshal ###
```
int size = typeof(T).IsValueType ? Marshal.SizeOf(default(T)) : IntPtr.Size;
```
### object ###
```
bool flag = object.ReferenceEquals(A, B);
bool flag2 = A.Equals(B);
```