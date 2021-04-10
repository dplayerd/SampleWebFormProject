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
            this.txtPhone.Text = model.Phone;
            this.rdblUserLevel.SelectedValue = model.UserLevel.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string qsID = Request.QueryString["ID"];

            Guid temp;
            if (!Guid.TryParse(qsID, out temp))
                return;

            var manager = new AccountManager();
            var model = manager.GetAccountViewModel(temp);

            if (!string.IsNullOrEmpty(this.txtPWD.Text) &&
                !string.IsNullOrEmpty(this.txtNewPWD.Text))
            {
                if (model.PWD == this.txtPWD.Text)
                {
                    model.PWD = this.txtNewPWD.Text.Trim();
                }
                else
                {
                    this.lblMsg.Text = "密碼和原密碼不一致";
                    return;
                }
            }

            model.Title = this.txtTitle.Text.Trim();
            model.Name = this.txtName.Text.Trim();
            model.Email = this.txtEmail.Text.Trim();
            model.Phone = this.txtPhone.Text.Trim();

            int userLever = 0;

            if (int.TryParse(this.rdblUserLevel.SelectedValue, out userLever))
            {
                try
                {
                    var item = (UserLevel)userLever;
                }
                catch
                {
                    throw;
                }

                model.UserLevel = userLever;
            }

            manager.UpdateAccountViewModel(model);
            this.lblMsg.Text = "存檔成功";
        }
    }
}