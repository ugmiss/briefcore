using System;
using System.ComponentModel;

namespace Business
{
            [Description("IsTable")]

    public class Article
    {
        public string Content { set; get; }
        [Description("IsPrimaryKey")]
        public string Id { set; get; }
        public string Authorid { set; get; }
        public string Categoryid { set; get; }
        public DateTime? Publishdate { set; get; }
        public DateTime? Modifydate { set; get; }
        public string Title { set; get; }
        public string Summary { set; get; }
        public string Status { set; get; }
    }
}