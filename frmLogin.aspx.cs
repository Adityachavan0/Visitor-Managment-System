using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VMS
{
    public partial class test : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

       

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM tblUserMaster WHERE Username=@username AND Password=@password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    Session["Username"] = txtUsername.Text;
                    Response.Redirect("Dash.aspx");
                }
                else
                {
                    lblMessage.Text = "Invalid username or password!";
                }
            }
        }
    }
}