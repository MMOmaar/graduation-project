using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace usedBooks
{
    public partial class loginPage : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["usedBooks"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
               
                CRUD myCrud = new CRUD();
                string mySql = @"select * from [user] where (userEmail='" + txtEmail.Text.Trim() + "' OR userName='" + txtEmail.Text.Trim() + "') AND userPassword='" + txtPassword.Text.Trim() + "'";
                SqlDataReader dr = myCrud.getDrPassSql(mySql);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Session["userId"] = dr.GetValue(0).ToString();
                        Session["userEmail"] = dr.GetValue(3).ToString();
                        Session["userFullName"] = dr.GetValue(1).ToString();
                        Session["userPassword"] = dr.GetValue(5).ToString();
                        Session["role"] = "user";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                   " alert('Logged in successfully'); window.open('Home.aspx');", true);
                    
                }
                else
                {
                    
                    PanelError.Visible = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                //
            }


            

        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("signupPage.aspx");
        }

        protected void lbtnForgotPass_Click(object sender, EventArgs e)
        {
            Response.Redirect("forgotPasswordPage.aspx");
        }

    }
}