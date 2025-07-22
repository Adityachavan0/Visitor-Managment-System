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
    public partial class resetpass : System.Web.UI.Page
    {
        string connstr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                // Load the user dropdown list only once when the page is first loaded
                loadUserddl();
            }
            

        }

        private void loadUserddl()
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                string query = @"SELECT EmpName FROM tblUserMaster ";
                

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ddlUser.DataSource = dt;
                    ddlUser.DataTextField = "EmpName";
                    ddlUser.DataValueField = "EmpName";
                    ddlUser.DataBind();
                    ddlUser.Items.Insert(0, new ListItem("-- Select Username --", ""));

                }
                else
                {
                    ddlUser.Items.Clear();
                    ddlUser.Items.Add(new ListItem("No Users Found", ""));
                }
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                string query = "update tblUserMaster set Password = 'pass' where EmpName='" + ddlUser.Text.Trim()+"'"; ;
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                lblMessage.Style["color"] = "green";
                lblMessage.Text = "Password Changed Successfully<br>Password is:-pass";

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            
        }
    }
}