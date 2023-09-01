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
    public partial class AdminloginPage : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["usedBooks"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from admin where (adminEmail='" + txtEmail.Text.Trim() + "' OR adminUserName='" + txtEmail.Text.Trim() + "') AND adminPassword='" + txtPassword.Text.Trim() + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Session["adminEmail"] = dr.GetValue(1).ToString();
                        Session["adminPassword"] = dr.GetValue(2).ToString();
                        Session["role"] = "admin";
                    }
                    Response.Write("<script>alert('Logged in successfully');</script>");
                    Response.Redirect("Home.aspx");
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