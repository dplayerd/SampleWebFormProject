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
    public partial class ProductList : System.Web.UI.Page
    {
        const int _pageSize = 10;


        internal class PagingLink
        {
            public string Name { get; set; }
            public string Link { get; set; }
            public string Title { get; set; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.LoadGridView();
                this.RestoreParameters();
            }
        }

        private void RestoreParameters()
        {
            string caption = Request.QueryString["caption"];
            string productTypeText = Request.QueryString["productType"];
            string minPriceText = Request.QueryString["minPrice"];
            string maxPriceText = Request.QueryString["maxPrice"];

            if (!string.IsNullOrEmpty(caption))
                this.txtCaption.Text = caption;

            if (!string.IsNullOrEmpty(productTypeText))
                this.ddlProductType.SelectedValue = productTypeText;

            if (!string.IsNullOrEmpty(minPriceText))
                this.txtPrice1.Text = minPriceText;

            if (!string.IsNullOrEmpty(maxPriceText))
                this.txtPrice2.Text = maxPriceText;

        }

        private string GetQueryString(bool includePage, int? pageIndex)
        {
            return string.Empty;
            ////----- Get Query string parameters -----
            //string page = Request.QueryString["Page"];
            //string name = Request.QueryString["name"];
            //string levelText = Request.QueryString["level"];
            ////----- Get Query string parameters -----


            //List<string> conditions = new List<string>();

            //if (!string.IsNullOrEmpty(page) && includePage)
            //    conditions.Add("Page=" + page);

            //if (!string.IsNullOrEmpty(name))
            //    conditions.Add("Name=" + name);

            //if (!string.IsNullOrEmpty(levelText))
            //    conditions.Add("Level=" + levelText);

            //if (pageIndex.HasValue)
            //    conditions.Add("Page=" + pageIndex.Value);

            //string retText =
            //    (conditions.Count > 0)
            //        ? "?" + string.Join("&", conditions)
            //        : string.Empty;

            //return retText;
        }


        private void LoadGridView()
        {
            ////----- Get Query string parameters -----
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

            string caption = Request.QueryString["caption"];
            string productTypeText = Request.QueryString["productType"];
            string minPriceText = Request.QueryString["minPrice"];
            string maxPriceText = Request.QueryString["maxPrice"];


            int? productType = null;
            if (!string.IsNullOrEmpty(productTypeText))
            {
                int temp;
                if (int.TryParse(productTypeText, out temp))
                    productType = temp;
            }

            decimal? minPrice = null;
            decimal? maxPrice = null;
            if (!string.IsNullOrEmpty(minPriceText))
            {
                int temp;
                if (int.TryParse(minPriceText, out temp))
                    minPrice = temp;
            }

            if (!string.IsNullOrEmpty(maxPriceText))
            {
                int temp;
                if (int.TryParse(maxPriceText, out temp))
                    maxPrice = temp;
            }
            //----- Get Query string parameters -----


            int totalSize = 0;

            var manager = new ProductManager();
            var list = manager.GetProducts(caption, productType, minPrice, maxPrice, out totalSize, pIndex, _pageSize);
            int pages = PagingHelper.CalculatePages(totalSize, _pageSize);

            List<PagingLink> pagingList = new List<PagingLink>();
            for (var i = 1; i <= pages; i++)
            {
                pagingList.Add(new PagingLink()
                {
                    Link = $"MamberList.aspx{this.GetQueryString(false, i)}",
                    Name = $"{i}",
                    Title = $"前往第 {i} 頁"
                });
            }

            //this.repPaging.DataSource = pagingList;
            //this.repPaging.DataBind();

            this.GridView1.DataSource = list;
            this.GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //string cmdName = e.CommandName;
            //string arg = e.CommandArgument.ToString();

            //if (cmdName == "DeleteItem")
            //{
            //    Guid id;
            //    if (Guid.TryParse(arg, out id))
            //    {
            //        var manager = new AccountManager();
            //        manager.DeleteAccountViewModel(id);

            //        this.LoadGridView();
            //        this.lblMsg.Text = "已刪除。";
            //    }
            //}
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string caption = this.txtCaption.Text;
            string productType = this.ddlProductType.Text;
            string minPrice = this.txtPrice1.Text;
            string maxPrice = this.txtPrice2.Text;

            string template = "?Page=1";

            if (!string.IsNullOrEmpty(caption))
                template += "&caption=" + caption;

            if (!string.IsNullOrEmpty(productType))
                template += "&productType=" + productType;

            if (!string.IsNullOrEmpty(minPrice))
                template += "&minPrice=" + minPrice;

            if (!string.IsNullOrEmpty(maxPrice))
                template += "&maxPrice=" + maxPrice;

            Response.Redirect("ProductList.aspx" + template);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ProductManager manager = new ProductManager();

            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Separator)
            {
                ProductModel mode = e.Row.DataItem as ProductModel;
                Literal ltProductType = e.Row.FindControl("ltProductType") as Literal;

                string val = manager.GetProductName(mode.ProductType);
                ltProductType.Text = val;
            }
        }
    }
}