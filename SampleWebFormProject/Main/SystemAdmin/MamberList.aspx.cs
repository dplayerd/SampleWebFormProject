using CoreProject.Helpers;
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
        const int _pageSize = 10;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.LoadGridView();
            }
        }

        private void LoadGridView()
        {
            string page = Request.QueryString["Page"];
            int pIndex = 0;
            if (string.IsNullOrEmpty(page))
                pIndex = 1;
            else
            {
                int.TryParse(page, out pIndex);

                if (pIndex <= 0)
                    pIndex = 1;
            }

            int totalSize = 0;

            var manager = new AccountManager();
            var list = manager.GetAccountViewModels(out totalSize, pIndex, _pageSize);
            int pages = PagingHelper.CalculatePages(totalSize, _pageSize);

            this.ltPages.Text = string.Empty;
            for (var i = 1; i <= pages; i++)
            {
                string template = $@"<a href=""MamberList.aspx?Page={i}"">Page {i}</a> &nbsp;&nbsp;";
                this.ltPages.Text += template;
            }

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