using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VMS
{
    public partial class userList : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTodayVisitors();

            }
        }
        private void LoadTodayVisitors()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
            SELECT id, UserName, EmpName, EmpId, Email, createdate, Password,  
                   CASE 
                       WHEN isActive = 1 THEN ' Active' 
                       ELSE 'Not Active' 
                   END AS isActive
            FROM tblUserMaster ";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvVisitors.DataSource = dt;
                gvVisitors.DataBind();
            }
        }
        protected void gvVisitors_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Fetch status safely without using '?.' (C# 4.0 compatible)
                string status = DataBinder.Eval(e.Row.DataItem, "isActive") != null
                                ? DataBinder.Eval(e.Row.DataItem, "isActive").ToString().Trim()
                                : "";

                // Debugging: Check status in tooltip
                e.Row.ToolTip = "isActive: " + status;

                // Change row color based on status
                if (status == "Active")
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen; // Green for checked-out visitors
                }
                else if (status == "Not Active")
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral; // Red for visitors still inside
                }
            }
        }
        
        string conString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
       

        

       

        

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtempid.Text.Trim() != "" && txtempName.Text.Trim() != "" && txtActive.Text.Trim() != "" && txtuserName.Text.Trim() != "" && txtemail.Text.Trim() != "")
            {
                using (SqlConnection conn = new SqlConnection(conString))
                {


                    
                    string query = "Update tblUserMaster Set UserName=@UserName,EmpName=@EmpName,EmpId=@EmpId,Email=@Email,isActive=@Active,Password=@Pass where id=" +Convert.ToInt32( ViewState["id"].ToString());
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        

                        cmd.Parameters.AddWithValue("@EmpId", txtempid.Text.Trim());
                        cmd.Parameters.AddWithValue("@EmpName", txtempName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Active", txtActive.Text.Trim());
                        cmd.Parameters.AddWithValue("@UserName", txtuserName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtemail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Pass", txtPass.Text.Trim());
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        Response.Write("<script>alert('Update successfully');</script>");
                        EditUser.Visible = false;
                        GridDate.Visible = true;
                        LoadTodayVisitors();
                    }
                }
            }
            else
            {
                EditUser.Visible = false;
                GridDate.Visible = true;
                Response.Write("<script>alert('Not Update ');</script>");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            
        }


        protected void Edit_Click(object sender, EventArgs e)
        {
            EditUser.Visible = true;
            GridDate.Visible = false;


            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.CommandArgument);
            ViewState["id"] = id.ToString();
            DataTable dt = new DataTable();
            

            string query = "select * from tblUserMaster where id=@id";

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                }
            }

            if (dt.Rows.Count > 0) { 
                txtemail.Text = dt.Rows[0]["Email"].ToString();
                txtActive.Text= dt.Rows[0]["isActive"].ToString();
                txtempName.Text = dt.Rows[0]["EmpName"].ToString();
                txtempid.Text = dt.Rows[0]["EmpId"].ToString();
                txtuserName.Text = dt.Rows[0]["UserName"].ToString();
                txtPass.Text = dt.Rows[0]["Password"].ToString();

            }

        }
    }
}