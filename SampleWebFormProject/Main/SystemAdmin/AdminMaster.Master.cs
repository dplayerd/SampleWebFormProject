using CoreProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main.SystemAdmin
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!LoginHelper.HasLogined())
            {
                Response.Redirect("~/SystemAdmin/index.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LoginHelper.Logout();
            Response.Redirect("~/SystemAdmin/index.aspx");
        }
    }
}