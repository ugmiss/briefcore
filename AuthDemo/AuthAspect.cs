using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aop;

namespace AuthDemo
{
    public class AuthAttribute : PostAspectAttribute
    {
        public override void Action(InvokeContext context)
        {
            switch (Environment.CurrrentUser)
            {
                case "admin":
                    context.SetResult("all");
                    break;
                case "eo":
                    context.SetResult("onlyeo");
                    break;
                case "mo":
                    context.SetResult("onlymo");
                    break;
                default:
                    return;
            }
        }
    }
}
