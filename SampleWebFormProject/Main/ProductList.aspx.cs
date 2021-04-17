using CoreProject.Managers;
using CoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main
{
    public partial class ProductList : System.Web.UI.Page
    {
        private string _saveFolder = "~/FileDownload/";

        protected void Page_Load(object sender, EventArgs e)
        {
            var manager = new ProductManager();

            this.repList.DataSource = manager.GetProducts(null, null, null, null);
            this.repList.DataBind();
        }

        protected void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var model = e.Item.DataItem as ProductModel;
                var img = e.Item.FindControl("img") as Image;

                if (!string.IsNullOrWhiteSpace(model.Pic1))
                {
                    img.ImageUrl = this._saveFolder + model.Pic1;
                    img.Visible = true;
                }

                if (!string.IsNullOrWhiteSpace(model.Pic2))
                {
                    img.ImageUrl = this._saveFolder + model.Pic2;
                    img.Visible = true;
                }
            }
        }
    }
}