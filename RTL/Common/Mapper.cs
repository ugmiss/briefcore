using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.ObjectModel;

namespace System
{
    /// <summary>
    /// 模型属性映射器(实现A对象或集合到T对象或集合的转换，属性一致的进行赋值)。
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// 对象映射。
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T MapToModel<A, T>(this A data) where T : new()
        {
            T t = new T();
            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo[] dataprop = typeof(A).GetProperties();
            foreach (PropertyInfo p in properties)
            {
                foreach (PropertyInfo pp in dataprop)
                {
                    if (pp.Name == p.Name)
                    {
                        p.FastSetValue(t, pp.FastGetValue(data));
                        break;
                    }
                }
            }
            return t;
        }
        /// <summary>
        /// ObservableCollection集合映射
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ObservableCollection<T> MapToObservableCollection<A, T>(this IEnumerable<A> data) where T : new()
        {
            ObservableCollection<T> list = new ObservableCollection<T>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo[] dataprop = typeof(A).GetProperties();
            foreach (A o in data)
            {
                T t = new T();
                foreach (PropertyInfo p in properties)
                {
                    var a = dataprop.FirstOrDefault(p1 => p1.Name == p.Name);
                    if (a != null)
                        p.FastSetValue(t, a.FastGetValue(o));
                }
                list.Add(t);
            }
            return list;
        }
        /// <summary>
        /// List集合映射。
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<T> MapToList<A, T>(this IEnumerable<A> data) where T : new()
        {
            List<T> list = new List<T>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo[] dataprop = typeof(A).GetProperties();
            foreach (A o in data)
            {
                T t = new T();
                foreach (PropertyInfo p in properties)
                {
                    var a = dataprop.FirstOrDefault(p1 => p1.Name == p.Name);
                    if (a != null)
                        p.FastSetValue(t, a.FastGetValue(o));
                }
                list.Add(t);
            }
            return list;
        }
    }
}
