using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main
{
    public partial class TestFileUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.img1.Src = "/FileDownload/" + this.Session["NewFileName"];
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!this.FileUpload1.HasFile)
            {
                this.lbMsg.Text = "File is required.";
                return;
            }

            var uFile = this.FileUpload1.PostedFile;
            var fileName = uFile.FileName;
            string fileExt = System.IO.Path.GetExtension(fileName);

            string path = Server.MapPath("~/FileDownload/");
            string newFIleName = Guid.NewGuid().ToString() + fileExt;
            string fullPath = System.IO.Path.Combine(path, newFIleName);

            this.Session["NewFileName"] = newFIleName;

            uFile.SaveAs(fullPath);
            this.lbMsg.Text = "Upload success.";
        }
    }
}