using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GenUI
{
    public enum EnumFieldType
    {
        [Description("文本")]
        NVARCHAR,
        [Description("GUID")]
        UNIQUEIDENTIFIER,
        [Description("数字")]
        DECIMAL,
        [Description("时间")]
        DATETIME,
        [Description("整数")]
        INT,
        [Description("是否")]
        BIT,
        [Description("文件")]
        TEXT
    }
}
