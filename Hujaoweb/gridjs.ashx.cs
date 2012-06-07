using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hujaoweb
{
    /// <summary>
    /// grid 的摘要说明
    /// </summary>
    public class gridjs : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string js = "var manager;$(function () {window['g'] =manager = $(\"#maingrid\").ligerGrid({columns: [{ display: '用户名', name: 'UserName', width: 250, align: 'left', totalSummary:{type: 'count'} }], width: '100%', pageSizeOptions: [5, 10, 15, 20], height: '97%',url: 'grid.ashx',dataAction: 'local',usePager: true,alternatingRow: true,record:\"Total\",rownumbers:true });});";
            context.Response.Write(js);
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