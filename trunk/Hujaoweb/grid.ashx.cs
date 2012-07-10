using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business;

namespace Hujaoweb
{
    /// <summary>
    /// grid 的摘要说明
    /// </summary>
    public class grid : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string json = "";
            Logic logic = new Logic("server=.;uid=sa;pwd=sa;database=hujaodata");
            var li= logic.GetAll<Userinfo>();
            json = @"{""Rows"":" + li.JsonSerialize() + @",""Total"":""" + li.Count + @"""}";
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}