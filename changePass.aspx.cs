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

    public partial class changePass : System.Web.UI.Page
    {
        string connstr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnchange_Click(object sender, EventArgs e)
        {
            if (checkCurrentPass())
            {
                if (txtNewPass.Text == txtConfirm.Text)
                {
                    
                    
                    using (SqlConnection conn = new SqlConnection(connstr))
                    {
                        conn.Open();
                        string query = "update tblUserMaster set Password = @NewPass where Password= @Pass";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Pass", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@NewPass", txtConfirm.Text);
                        cmd.ExecuteNonQuery();
                        lblMessage.Style["color"] = "green";
                        lblMessage.InnerText = "Password Changed Successfully";

                    }
                }
                else
                {
                    lblMessage.Style["color"] = "red";
                    lblMessage.InnerText = "Passwords do not match.";
                }
            }
            
        }

        private bool checkCurrentPass()
        {
            string username = Session["Username"].ToString();
            
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                
                conn.Open();
                string query = "SELECT Password FROM tblUserMaster WHERE UserName = @username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", Session["Username"]); // Assuming UserId is stored in session
                string currentPass = cmd.ExecuteScalar().ToString();
                if (currentPass == txtPassword.Text)
                {
                    //lblMessage.Style["color"] = "green";
                    //lblMessage.InnerText = "Current password is correct.";

                    return true;
                    
                }
                else
                {
                    lblMessage.Style["color"] = "red";
                    lblMessage.InnerText = "Current password is incorrect.";
                    return false;
                }
            }
        }
    }
}