using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VMS
{
    public partial class createUser : System.Web.UI.Page
    {
        DataTable dt;
        
        string conString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txtuserName_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                string query = "SELECT COUNT(*) FROM tblUserMaster WHERE UserName = @User";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@User", txtuserName.Text.Trim());

                    conn.Open(); // ✅ Always open the connection before executing
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        lblMessage.Style["color"] = "red";
                        lblMessage.Text = "Username already exists";
                        btnSubmit.Enabled = false;
                    }
                    else
                    {
                        lblMessage.Style["color"] = "green";
                        lblMessage.Text = "Username available";
                        btnSubmit.Enabled = true;
                    }
                }
            }
        }

        protected void txtempid_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                string query = "SELECT COUNT(*) FROM tblUserMaster WHERE EmpId = @EmpId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmpId", txtempid.Text.Trim());

                    conn.Open(); // ✅ Always open the connection before executing
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        lblMessage.Style["color"] = "red";
                        lblMessage.Text = "Emp ID already exists";
                        btnSubmit.Enabled = false;
                    }
                    else
                    {
                        lblMessage.Style["color"] = "green";
                        lblMessage.Text = "Emp Id available";
                        btnSubmit.Enabled = true;
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtempid.Text.Trim()!="" && txtempName.Text.Trim()!="" && txtActive.Text.Trim()!="" && txtuserName.Text.Trim()!="" && txtemail.Text.Trim()!="")
            {
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    string query = "Insert into tblUserMaster(UserName,EmpName,EmpId,Email,isActive,createdate,Password) Values(@UserName,@EmpName,@EmpId,@Email,@Active,GETDATE(),'pass')";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmpId", txtempid.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmpName", txtempName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Active", txtActive.Text.Trim());
                        cmd.Parameters.AddWithValue("@UserName", txtuserName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtemail.Text.Trim());

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        Response.Write("<script>alert('User created successfully');</script>");
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Please Enter All Value ');</script>");
            }
        }




        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtempid.Text = "";
            txtActive.Text = "";
            txtemail.Text = "";
            txtempName.Text = "";
            txtuserName.Text = "";

        }
    }
}