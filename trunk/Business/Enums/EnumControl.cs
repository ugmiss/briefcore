using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Business
{
    public enum EnumControl
    {
        [Description("单行文本")]
        TextBox = 0,
        [Description("多行文本")]
        TextArea = 1,
        [Description("下拉单选")]
        DropDownList = 2,
        [Description("复选框")]
        CheckBox = 3,
        [Description("日期选择")]
        DateEdit = 4,
        //[Description("单人选择")]
        //UserSelectSingle = 5,
        //[Description("单部门选择")]
        //OrgSelectSingle = 6,
        //[Description("多人选择")]
        //UserSelectMulti = 7,
        //[Description("多部门选择")]
        //OrgSelectMulti = 8,
        //[Description("单项目选择")]
        //ProjectSelectSingle = 9,
        [Description("文件上传")]
        UploadEdit = 10,
        //[Description("分类标签")]
        //LabelEdit = 11,
        [Description("隐藏控件")]
        NONE = 12,
        [Description("整数控件")]
        NumBox = 13,
        [Description("小数控件")]
        NumBoxSmall = 14
    }
}
