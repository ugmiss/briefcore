﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace System
{
    public class ConfigManager
    {
        public static T GetConfig<T>() where T : new()
        {
            string filename = typeof(T).Name + ".config";
            if (!File.Exists(filename))
            {
                T t = new T();
                t.XmlSerialize().WriteToFile(filename);
            }
            return "".ReadFromFile(filename).XmlDeserialize<T>();
        }
        public static T GetConfig<T>(string filename) where T : new()
        {
            if (!File.Exists(filename))
            {
                T t = new T(); t.XmlSerialize().WriteToFile(filename);
            } return "".ReadFromFile(filename).XmlDeserialize<T>();
        }
        public static void SaveConfig<T>(T t) where T : new()
        {
            string filename = typeof(T).Name + ".config";
            t.XmlSerialize().WriteToFile(filename);
        }
        public static void SaveConfig<T>(T t, string filename) where T : new()
        {
            t.XmlSerialize().WriteToFile(filename);
        }
    }
}