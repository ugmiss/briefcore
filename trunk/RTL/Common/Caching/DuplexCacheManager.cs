using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace Common
{
    /// <summary>
    /// 过期代理。
    /// </summary>
    /// <param name="typename"></param>
    public delegate void TimeOutChanged(string typename);
    /// <summary>
    /// 基于WCF通知的客户端与服务器双向缓存管理
    /// </summary>
    public static class DuplexCacheManager
    {
        /// <summary>
        /// 过期事件。
        /// </summary>
        public static event TimeOutChanged OnTimeOutChanged;
        /// <summary>
        /// 过期标志字典。
        /// </summary>
        static ConcurrentDictionary<string, bool> TimeOutMap = new ConcurrentDictionary<string, bool>();
        /// <summary>
        /// 是否过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsTimeOut<T>()
        {
            if (!TimeOutMap.ContainsKey(typeof(T).ToString()))
                return true;
            return TimeOutMap[typeof(T).ToString()];
        }
        /// <summary>
        /// 是否过期
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsTimeOut(Type t)
        {
            if (!TimeOutMap.ContainsKey(t.ToString()))
                return true;
            return TimeOutMap[t.ToString()];
        }
        /// <summary>
        /// 通知过期
        /// </summary>
        /// <param name="typename"></param>
        public static void NotifyTimeOut(string typename)
        {
            TimeOutMap[typename] = true;
            if (OnTimeOutChanged != null)
            {
                OnTimeOutChanged(typename);
            }
        }
        /// <summary>
        /// 通知过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void NotifyTimeOut<T>()
        {
            bool temp;
            TimeOutMap.TryGetValue(typeof(T).ToString(), out temp);
            if (temp) return;
            TimeOutMap[typeof(T).ToString()] = true;
            if (OnTimeOutChanged != null)
            {
                OnTimeOutChanged(typeof(T).ToString());
            }
        }
        /// <summary>
        /// 通知过期
        /// </summary>
        /// <param name="t"></param>
        public static void NotifyTimeOut(Type t)
        {
            bool temp;
            TimeOutMap.TryGetValue(t.ToString(), out temp);
            if (temp) return;
            TimeOutMap[t.ToString()] = true;
            if (OnTimeOutChanged != null)
            {
                OnTimeOutChanged(t.ToString());
            }
        }
        /// <summary>
        /// 添加过期监视
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void AddNotify<T>()
        {
            if (!TimeOutMap.ContainsKey(typeof(T).ToString()))
                TimeOutMap.TryAdd(typeof(T).ToString(), false);
            else
            {
                bool obj;
                TimeOutMap.TryRemove(typeof(T).ToString(), out obj);
                TimeOutMap.TryAdd(typeof(T).ToString(), false);
            }
        }
        /// <summary>
        /// 添加过期监视
        /// </summary>
        /// <param name="t"></param>
        public static void AddNotify(Type t)
        {
            if (!TimeOutMap.ContainsKey(t.ToString()))
                TimeOutMap.TryAdd(t.ToString(), false);
            else
            {
                bool obj;
                TimeOutMap.TryRemove(t.ToString(), out obj);
                TimeOutMap.TryAdd(t.ToString(), false);
            }
        }
    }
}
