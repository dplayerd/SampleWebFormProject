using CoreProject.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main.SystemAdmin
{
    public partial class MamberList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var manager = new AccountManager();
            var list = manager.GetAccountViewModels();
            this.GridView1.DataSource = list;
            this.GridView1.DataBind();
        }
    }
}