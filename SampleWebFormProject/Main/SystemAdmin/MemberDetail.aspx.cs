using CoreProject.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main.SystemAdmin
{
    public partial class MemberDetail : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            string qsID = Request.QueryString["ID"];

            if (string.IsNullOrEmpty(qsID))
                Response.Redirect("~/SystemAdmin/MemberList.aspx");

            Guid temp;
            if (!Guid.TryParse(qsID, out temp))
                Response.Redirect("~/SystemAdmin/MemberList.aspx");


            var manager = new AccountManager();
            var model = manager.GetAccountViewModel(temp);

            if (model == null)
                Response.Redirect("~/SystemAdmin/MemberList.aspx");


            this.txtAccount.Text = model.Account;
            this.txtName.Text = model.Name;
            this.txtEmail.Text = model.Email;
            this.txtTitle.Text = model.Title;
            this.rdblUserLevel.SelectedValue = model.UserLevel.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}