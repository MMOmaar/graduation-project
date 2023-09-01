using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace usedBooks
{
    public partial class doners : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["role"] == "user")
                {

                }
                else
                    Response.Redirect("Loginpage.aspx");
            }
        }
      
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtbookname.Text = "";
            txtAuthorName.Text = "";
            txtbookinfo.Text = "";
            txtbookDef.Text = "";
            txtbookPrice.Text = "";

        }

        protected void UploadFile(object sender, EventArgs e)
        {
           

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if(FileUpload1.HasFile)
            {
                string folderPath = Server.MapPath("~/BookImage/");

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists Create it.
                    Directory.CreateDirectory(folderPath);
                }

                //Save the File to the Directory (Folder).
                FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));

                //Display the Picture in Image control.

                Image1.ImageUrl = "~/BookImage/" + Path.GetFileName(FileUpload1.FileName);
            }
            CRUD myCrud = new CRUD();
            string mySql = @"  INSERT INTO book(bookName,bookAuthor,bookInfo,bookContact,bookPrice,[bookPic],[userId],bookDef)
                           VALUES(@bookName,@bookAuthor,@bookInfo,@bookContact,@bookPrice,@bookPic,@userId,@bookDef)";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@bookName ", txtbookname.Text);
            myPara.Add("@bookAuthor", txtAuthorName.Text);
            myPara.Add("@bookInfo", txtbookinfo.Text);
            myPara.Add("@bookPic", FileUpload1.FileName);
            myPara.Add("@bookDef", txtbookDef.Text);
            myPara.Add("@bookContact", "");
            myPara.Add("@bookPrice", txtbookPrice.Text);
            myPara.Add("@userId", Session["userId"]);
            //myPara.Add("@durationOfUseId", ddlDurationOfUse.SelectedValue);

            int rtn = myCrud.InsertUpdateDeleteViaSqlDic(mySql, myPara);
            if (rtn >= 1)
            {
                Panelsuccess.Visible=true;
                PanelError.Visible = false;

            }
            else
            {
                Panelsuccess.Visible = false;
                PanelError.Visible = true;
            }

        }

    }
}