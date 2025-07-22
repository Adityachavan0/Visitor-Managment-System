using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace VMS
{
    
    public partial class Home : System.Web.UI.Page
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
            SELECT VisitorId, FullName, IDType, IDNumber, Pax, Vehicle, ValidFrom, ValidTill, Remarks, 
                   CASE 
                       WHEN Status = 1 THEN 'Checked Out' 
                       ELSE 'Not Checked Out' 
                   END AS Status
            FROM tblVisitorCheckIn WHERE Status = 0 OR CAST(ValidTill AS DATE) = CAST(GETDATE() AS DATE);";

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
                string status = DataBinder.Eval(e.Row.DataItem, "Status") != null
                                ? DataBinder.Eval(e.Row.DataItem, "Status").ToString().Trim()
                                : "";

                // Debugging: Check status in tooltip
                e.Row.ToolTip = "Status: " + status;

                // Change row color based on status
                if (status == "Checked Out")
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen; // Green for checked-out visitors
                }
                else if (status == "Not Checked Out")
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral; // Red for visitors still inside
                }
            }
        }

        protected void rbCheckIn_CheckedChanged(object sender, EventArgs e)
        {
            divCheckIn.Visible = true;
            divCheckOut.Visible = false;

        }

        protected void rbCheckOut_CheckedChanged(object sender, EventArgs e)
        {
            divCheckIn.Visible =false;
            divCheckOut.Visible = true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertVisitorCheckIn", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    string gender;
                    if (rdogendermale.Checked)
                    {
                        gender = rdogendermale.Text;
                    }
                    else
                    {
                        gender = rdogenderfemale.Text;
                    }
                    // ✅ Add regular text parameters
                    cmd.Parameters.AddWithValue("@IDType", slcIdType.Text.Trim());
                    cmd.Parameters.AddWithValue("@IDNumber", txtidno.Text.Trim());
                    cmd.Parameters.AddWithValue("@FullName", txtvisitorName.Text.Trim());

                    cmd.Parameters.AddWithValue("@Company", txtcompany.Text.Trim());
                    cmd.Parameters.AddWithValue("@MobileNo", txtmobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@Age", string.IsNullOrEmpty(txtAge.Text) ? (object)DBNull.Value : int.Parse(txtAge.Text));
                    cmd.Parameters.AddWithValue("@Purpose", txtpurpose.Text.Trim());
                    cmd.Parameters.AddWithValue("@Pax", string.IsNullOrEmpty(txtpax.Text) ? (object)DBNull.Value : int.Parse(txtpax.Text));
                    cmd.Parameters.AddWithValue("@Vehicle", txtvehical.Text.Trim());
                    
                    cmd.Parameters.AddWithValue("@ToMeet", txttomeet.Text.Trim());
                    cmd.Parameters.AddWithValue("@ToAddress", txtTolocation.Text.Trim());
                    cmd.Parameters.AddWithValue("@ApprovedBy", txtapproved.Text.Trim());
                    cmd.Parameters.AddWithValue("@EntryBy", Session["UserName"].ToString());


                    // ✅ Handle Date Values
                    DateTime validFrom, validTill;
                    cmd.Parameters.AddWithValue("@ValidFrom", DateTime.TryParse(dtfromDate.Text, out validFrom) ? validFrom : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ValidTill", DateTime.TryParse(dttoDate.Text, out validTill) ? validTill : (object)DBNull.Value);

                    cmd.Parameters.AddWithValue("@Remarks", txtRemark.Text.Trim());

                    // ✅ Get Image Data



                    byte[] imageBytes = GetImageBytes();
                    cmd.Parameters.Add("@VisitorImage", SqlDbType.VarBinary, -1).Value = (imageBytes != null) ? (object)imageBytes : DBNull.Value;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    Response.Write("<script>alert('Record inserted successfully');</script>");
                     
                    ClearFields();
                }
            }
        }

        private byte[] GetImageBytes()
        {
            if (fileCapture.HasFile) // If an image file is uploaded
            {
                using (BinaryReader br = new BinaryReader(fileCapture.PostedFile.InputStream))
                {
                    return br.ReadBytes(fileCapture.PostedFile.ContentLength);
                }
            }
            else if (!string.IsNullOrEmpty(capturedImageData.Value)) // If image is captured via camera (Base64)
            {
                string base64 = capturedImageData.Value.Split(',')[1]; // Remove "data:image/png;base64,"
                return Convert.FromBase64String(base64); // Convert Base64 to byte array
            }
            return null;
        }
        private void ClearFields()
        {
            slcIdType.SelectedIndex = 0;
            txtidno.Text = "";
            txtvisitorName.Text = "";
            txtcompany.Text = "";
            txtmobile.Text = "";
            rdogenderfemale.Checked = false;
            rdogendermale.Checked  = false;

            txtAge.Text = "";
            txtpurpose.Text = "";
            txtpax.Text = "";
            txtvehical.Text = "";
            txttomeet.Text = "";
            txtTolocation.Text = "";
            txtapproved.Text = "";
            dtfromDate.Text = "";
            dttoDate.Text = "";
            txtRemark.Text = "";
            capturedImageData.Value = "";
        }

        protected void gvVisitors_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "checkout")
            {
                string visitorId = e.CommandArgument.ToString();

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "UPDATE tblVisitorCheckIn SET Status = 1 WHERE VisitorId = @VisitorId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@VisitorId", visitorId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Checkout successful!');", true);
                LoadTodayVisitors();


            }

        }
    }
}