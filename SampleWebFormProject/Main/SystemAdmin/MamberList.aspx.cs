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
            if (!this.IsPostBack)
            {
                this.LoadGridView();
            }
        }

        private void LoadGridView()
        {
            var manager = new AccountManager();
            var list = manager.GetAccountViewModels();
            this.GridView1.DataSource = list;
            this.GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cmdName = e.CommandName;
            string arg = e.CommandArgument.ToString();

            if (cmdName == "DeleteItem")
            {
                Guid id;
                if (Guid.TryParse(arg, out id))
                {
                    var manager = new AccountManager();
                    manager.DeleteAccountViewModel(id);

                    this.LoadGridView();
                    this.lblMsg.Text = "已刪除。";
                }
            }
        }
    }
}