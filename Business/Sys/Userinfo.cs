using System;
using System.ComponentModel;

namespace Business
{
            [Description("IsTable")]

    public class Userinfo
    {
        [Description("IsPrimaryKey")]
        public string Id { set; get; }
        public DateTime? Registdate { set; get; }
        public DateTime? Lastlogindate { set; get; }
        public bool? Sex { set; get; }
        public string Name { set; get; }
        public string Password { set; get; }
        public string Cardnumber { set; get; }
        public string Email { set; get; }
    }
}