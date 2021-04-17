using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Utility
{
    public class PageHelper
    {
        public static void Goto404Page(HttpContext context)
        {
            context.Response.Clear();

            context.Response.StatusCode = 404;
            context.Response.Write(" Data doesn't existed. ");
            context.Response.Flush();
            context.Response.End();
        }
    }
}