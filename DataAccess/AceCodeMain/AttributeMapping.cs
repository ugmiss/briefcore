using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AceCodeMain
{
    /// <summary>
    /// 类型匹配，
    /// </summary>
    public class AttributeMapping
    {
        public static Dictionary<string, string> Mapping = new Dictionary<string, string>();

        public static Dictionary<string, string> ConvertMapping = new Dictionary<string, string>();

        static AttributeMapping()
        {
            Mapping.Add("datetime", "DateTime");
            Mapping.Add("smalldatetime", "DateTime");
            Mapping.Add("bit", "bool");
            Mapping.Add("int", "int");
            Mapping.Add("money", "decimal");
            Mapping.Add("float", "float");
            Mapping.Add("bigint", "long");
            Mapping.Add("smallint", "short");
            Mapping.Add("tinyint", "Int8");
            Mapping.Add("decimal", "decimal");

            Mapping.Add("text", "byte[]");
            Mapping.Add("varchar", "string");
            Mapping.Add("char", "string");
            Mapping.Add("nvarchar", "string");
            Mapping.Add("xml", "string");
            Mapping.Add("uniqueidentifier", "string");

            ConvertMapping.Add("string", "ToString");
            ConvertMapping.Add("int", "ToInt32");
            ConvertMapping.Add("float", "ToSingle");
            ConvertMapping.Add("decimal", "ToDecimal");
            ConvertMapping.Add("DateTime", "ToDateTime");
            ConvertMapping.Add("short", "ToInt16");
            ConvertMapping.Add("long", "ToInt64");
            ConvertMapping.Add("double", "ToDouble");
            ConvertMapping.Add("bool", "ToBoolean");
            ConvertMapping.Add("byte[]", "FromBase64String");
        }
    }
}
