using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Caching;

namespace System
{
    // 配置管理器
    public class ConfigManager
    {
        public static OrientConfig GetConfig()
        {
            if (CacheManager.GetData(SystemKeys.ConfigFileCache) == null)
            {
                OrientConfig config = string.Empty.ReadFromFile("Config.xml".AppPath()).XmlDeserialize<OrientConfig>();
                //物理文件缓存策略
                CacheManager.Add(SystemKeys.ConfigFileCache, config, null, new FileExpiration("Config.xml".AppPath()));
                return config;
            }
            else
            {
                return (OrientConfig)CacheManager.GetData(SystemKeys.ConfigFileCache);
            }
        }
        public static void SaveConfig(OrientConfig config)
        {
            config.XmlSerialize().WriteToFile8("Config.xml".AppPath());
        }
        public static string Get(string key)
        {
            //生成默认的配置文件
            //OrientConfig configtemp = new OrientConfig();
            //configtemp.Items = new List<Item>();
            //configtemp.Items.Add(new Item() { Key = SystemKeys.MinePositionDatabase, Value = "server=.;uid=sa;pwd=123456;database=GISData" });
            //SaveConfig(configtemp);
            OrientConfig config = string.Empty.ReadFromFile("Config.xml".AppPath()).XmlDeserialize<OrientConfig>();
            foreach (var item in config.Items)
            {
                if (item.Key == key)
                {
                    return item.Value;
                }
            }
            return null;
        }
        public static int GetInt(string key)
        {
            OrientConfig config = string.Empty.ReadFromFile("Config.xml".AppPath()).XmlDeserialize<OrientConfig>();
            foreach (var item in config.Items)
            {
                if (item.Key == key)
                {
                    return Convert.ToInt32(item.Value);
                }
            }
            return 0;
        }
    }
    // 配置类
    public class OrientConfig
    {
        public List<Item> Items { get; set; }
    }
    // 配置项
    public class Item
    {
        [XmlAttribute("key")]
        public string Key { get; set; }
        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}
