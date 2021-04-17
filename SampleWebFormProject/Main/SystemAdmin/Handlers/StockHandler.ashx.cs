using CoreProject.Helpers;
using CoreProject.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.SystemAdmin.Handlers
{
    /// <summary>
    /// StockHandler 的摘要描述
    /// </summary>
    public class StockHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if ("GET" == context.Request.HttpMethod.ToUpper())
            {
                string id = context.Request.QueryString["ID"];

                Guid temp;
                if (string.IsNullOrEmpty(id) ||
                    !Guid.TryParse(id, out temp))
                {
                    context.Response.StatusCode = 400;
                    context.Response.Write(" ID is required.");

                    return;
                }
                

                StocksManager manager = new StocksManager();
                var model = manager.GetStock(temp);

                if(model == null)
                {
                    context.Response.StatusCode = 404;
                    context.Response.Write($" Data is null (id={temp})");
                }

                string retText = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                context.Response.ContentType = "text/json";
                context.Response.Write(retText);
            }

            else if ("POST" == context.Request.HttpMethod.ToUpper())
            {
                string id = context.Request.QueryString["ID"];

                Guid temp;
                if (string.IsNullOrEmpty(id) ||
                    !Guid.TryParse(id, out temp))
                {
                    context.Response.StatusCode = 400;
                    context.Response.Write(" ID is required.");

                    return;
                }


                StocksManager manager = new StocksManager();
                var model = manager.GetStock(temp);

                if (model == null)
                {
                    context.Response.StatusCode = 404;
                    context.Response.Write($" Data is null (id={temp})");
                }

                //if(!LoginHelper.HasLogined())
                //{
                //    context.Response.StatusCode = 400;
                //    context.Response.Write($" not logined.");
                //}

                //var loginInfo = LoginHelper.GetCurrentUserInfo();


                // 取得前端的 post
                string cQtyText = context.Request.Form["CurrentQty"];
                string lQtyText = context.Request.Form["LockedQty"];

                int cQty = Convert.ToInt32(cQtyText);
                int lQty = Convert.ToInt32(lQtyText);

                model.CurrentQty = cQty;
                model.LockedQty = lQty;
                //model.Modifier = loginInfo.ID;
                //model.ModifyDate = DateTime.Now;

                // 寫入
                manager.UpdateStock(model);

                string retText = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                context.Response.ContentType = "text/json";
                context.Response.Write(retText);
            }
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