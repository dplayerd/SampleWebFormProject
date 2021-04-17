using CoreProject.Helpers;
using CoreProject.Managers;
using CoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main.SystemAdmin
{

    public partial class ProductDetail : System.Web.UI.Page
    {
        private string[] _allowExts = { ".jpg", ".png", ".bmp", ".gif" };
        private string _saveFolder = "~/FileDownload/";



        protected void Page_Init(object sender, EventArgs e)
        {
            if (this.IsUpdateMode())
            {
                Guid temp;
                Guid.TryParse(Request.QueryString["ID"], out temp);

                //this.txtAccount.Enabled = false;
                //this.txtAccount.BackColor = System.Drawing.Color.DarkGray;
                this.LoadProduct(temp);
            }
            else
            {
                //this.txtPWD.Enabled = false;
                //this.txtPWD.BackColor = System.Drawing.Color.DarkGray;
            }

            //LoginInfo loginInfo = LoginHelper.GetCurrentUserInfo();
            //if (loginInfo.Level == UserLevel.Admin)
            //{
            //    this.rdblUserLevel.Enabled = true;
            //}
        }

        private bool IsUpdateMode()
        {
            string qsID = Request.QueryString["ID"];

            Guid temp;
            if (Guid.TryParse(qsID, out temp))
                return true;

            return false;
        }

        private void LoadProduct(Guid id)
        {
            var manager = new ProductManager();
            var model = manager.GetProduct(id);

            if (model == null)
                Response.Redirect("~/SystemAdmin/ProductList.aspx");

            this.rdblProductType.SelectedValue = model.ProductType.ToString();
            this.txtCaption.Text = model.Caption;
            this.txtPrice.Text = model.Price.ToString();
            this.txtBody.Text = model.Body;
            this.ckbIsEnabled.Checked = model.IsEnabled;

            if (!string.IsNullOrEmpty(model.Pic1))
            {
                this.imgPic1.ImageUrl = _saveFolder + model.Pic1;
                this.imgPic1.Visible = true;
            }

            if (!string.IsNullOrEmpty(model.Pic2))
            {
                this.imgPic2.ImageUrl = _saveFolder + model.Pic2;
                this.imgPic2.Visible = true;
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            var manager = new ProductManager();
            ProductModel model = null;

            if (this.IsUpdateMode())
            {
                string qsID = Request.QueryString["ID"];

                Guid temp;
                if (!Guid.TryParse(qsID, out temp))
                    return;

                model = manager.GetProduct(temp);
            }
            else
            {
                model = new ProductModel();
            }

            model.Caption = this.txtCaption.Text.Trim();
            model.Body = this.txtBody.Text.Trim();
            model.ProductType = Convert.ToInt32(this.rdblProductType.Text);
            model.IsEnabled = this.ckbIsEnabled.Checked;
            model.Price = Convert.ToDecimal(this.txtPrice.Text);

            string pic1 = this.GetNewFileName(this.fuPic1);
            if (!string.IsNullOrEmpty(pic1))
                model.Pic1 = pic1;

            string pic2 = this.GetNewFileName(this.fuPic2);
            if (!string.IsNullOrEmpty(pic2))
                model.Pic2 = pic2;

            try
            {
                var loginInfo = LoginHelper.GetCurrentUserInfo();
                if (this.IsUpdateMode())
                {
                    model.Modifier = loginInfo.ID;
                    model.ModifyDate = DateTime.Now;

                    manager.UpdateProduct(model);
                }
                else
                {
                    model.Creator = loginInfo.ID;
                    model.CreateDate = DateTime.Now;

                    manager.CreateProduct(model);
                }

                this.lblMsg.Text = "存檔成功";
            }
            catch (Exception ex)
            {
                this.lblMsg.Text = ex.ToString();
                return;
            }
        }


        private string GetNewFileName(FileUpload fu)
        {
            if (!fu.HasFile)
                return string.Empty;


            var uFile = fu.PostedFile;
            var fileName = uFile.FileName;
            string fileExt = System.IO.Path.GetExtension(fileName);

            if (!_allowExts.Contains(fileExt.ToLower()))
                return string.Empty;


            string path = Server.MapPath(_saveFolder);
            string newFileName = Guid.NewGuid().ToString() + fileExt;
            string fullPath = System.IO.Path.Combine(path, newFileName);

            uFile.SaveAs(fullPath);
            return newFileName;
        }
    }
}