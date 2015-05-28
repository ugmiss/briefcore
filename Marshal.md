Marshal 类提供了一个方法集，这些方法用于分配非托管内存、复制非托管内存块、将托管类型转换为非托管类型，此外还提供了在与非托管代码交互时使用的其他杂项方法。


---


类别 成员
高级封送处理 GetManagedThunkForUnmanagedMethodPtr、GetUnmanagedThunkForManagedMethodPtr、NumParamBytes


---


COM 库函数 BindToMoniker、GetActiveObject


---


COM 实用工具 ChangeWrapperHandleStrength、CreateWrapperOfType、GetComObjectData、GetComSlotForMethodInfo、GetEndComSlot、GetMethodInfoForComSlot、GetStartComSlot、ReleaseComObject、SetComObjectData

---


数据转换 托管到非托管：Copy、GetComInterfaceForObject、GetIDispatchForObject、GetIUnknownForObject、StringToBSTR、StringToCoTaskMemAnsi、StringToCoTaskMemAuto、StringToCoTaskMemUni、StringToHGlobalAnsi、StringToHGlobalAuto、StringToHGlobalUni、StructureToPtr、UnsafeAddrOfPinnedArrayElement
非托管到托管：Copy、GetObjectForIUnknown、GetObjectForNativeVariant、GetObjectsForNativeVariants、GetTypedObjectForIUnknown、GetTypeForITypeInfo、PtrToStringAnsi、PtrToStringAuto、PtrToStringBSTR、PtrToStringUni

属性：SystemDefaultCharSize、SystemMaxDBCSCharSize


---


直接读取和写入 ReadByte、ReadInt16、ReadInt32、ReadInt64、ReadIntPtr、WriteByte、WriteInt16、WriteInt32、WriteInt64、WriteIntPtr

---


错误处理 COM：GetHRForException、ThrowExceptionForHR
Win32：GetLastWin32Error、GetExceptionCode、GetExceptionPointers

两者：GetHRForLastWin32Error


---


承载实用工具 GetThreadFromFiberCookie

---


IUnknown AddRef、QueryInterface、Release

---


内存管理 COM：AllocCoTaskMem、ReAllocCoTaskMem、FreeCoTaskMem、FreeBSTR
Win32：AllocHGlobal、ReAllocHGlobal、FreeHGlobal

两者：DestroyStructure


---


平台调用实用工具 Prelink、PrelinkAll、GetHINSTANCE

---


结构检查 OffsetOf、SizeOf

---


类型信息 GenerateGuidForType、GenerateProgIdForType、GetTypeInfoName、GetTypeLibGuid、GetTypeLibGuidForAssembly、GetTypeLibLcid、GetTypeLibName、IsComObject、IsTypeVisibleFromCom

