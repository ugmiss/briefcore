using System.ComponentModel;

namespace Business
{
    public enum EnumOperation
    {
        [Description("增加")]
        Add = 0,
        [Description("修改")]
        Modify = 1,
        [Description("删除")]
        Delete = 2,
        [Description("查看")]
        View = 3,
        [Description("复制")]
        Copy = 4
    }
}
