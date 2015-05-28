### 反射对象中的结合属性 ###
```
foreach (PropertyInfo p in typeof(T).GetProperties())
{
    if (p.Name != "point")
    {
        object paramValue = p.FastGetValue(t);
        if (paramValue == null)
            paramValue = "";
        data.SetAttribute(p.Name, paramValue.ToString());
    }
    else
    {
        IList lt = p.FastGetValue(t) as IList;
        Type ty = p.PropertyType.GetGenericArguments()[0];
        foreach (object o in lt)
        {
            XmlElement point = doc.CreateElement("point");
            foreach (PropertyInfo p2 in ty.GetProperties())
            {
                object paramValue2 = p2.FastGetValue(o);
                if (paramValue2 == null)
                    paramValue2 = "";
                point.SetAttribute(p2.Name, paramValue2.ToString());
            }
            data.AppendChild(point);
        }
    }
}
```

### 字符串格式化 ###
```
string str="{0,-8} | {1} | {2}| {3,-8}  | {4,-8}";
```

### 十进制，16进制转换 ###
```

```