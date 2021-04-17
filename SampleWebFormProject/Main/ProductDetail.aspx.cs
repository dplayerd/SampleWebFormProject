using CoreProject.Managers;
using Main.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main
{
    public partial class ProductDetail : System.Web.UI.Page
    {
        private string _saveFolder = "~/FileDownload/";

        protected void Page_Load(object sender, EventArgs e)
        {
            string idText = Request.QueryString["ID"];

            if (string.IsNullOrEmpty(idText))
                PageHelper.Goto404Page(this.Context);

            Guid id;
            if (!Guid.TryParse(idText, out id))
                PageHelper.Goto404Page(this.Context);


            if (!this.IsPostBack)
                this.LoadProduct(id);
        }

        public void LoadProduct(Guid id)
        {
            var manager = new ProductManager();
            var model = manager.GetProduct(id);

            if(model == null)
                PageHelper.Goto404Page(this.Context);


            this.ltCaption.Text = model.Caption;
            this.ltBody.Text = model.Body.Replace(Environment.NewLine, "<br/>");
            this.ltType.Text = manager.GetProductName(model.ProductType);
            this.ltPrice.Text = model.Price.ToString("0");



            if (!string.IsNullOrEmpty(model.Pic1))
            {
                this.img1.ImageUrl = _saveFolder + model.Pic1;
                this.img1.Visible = true;
            }

            if (!string.IsNullOrEmpty(model.Pic2))
            {
                this.img2.ImageUrl = _saveFolder + model.Pic2;
                this.img2.Visible = true;
            }
        }
    }
}