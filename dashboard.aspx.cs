using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VMS
{
    public partial class dashboard : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadVisitorData();
            }
        }

        private void LoadVisitorData()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT VisitorId, FullName, IDType, ValidFrom, ValidTill, Status FROM tblVisitorCheckIn";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvVisitors.DataSource = dt;
                gvVisitors.DataBind();
            }
        }
        private DataTable GetVisitorDetailsById(int visitorId)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM tblVisitors WHERE VisitorId = @VisitorId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@VisitorId", visitorId);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        protected void gvVisitors_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "PrintPDF")
            {
                int visitorId = Convert.ToInt32(e.CommandArgument);

                // Get data from DB
                DataTable dt = GetVisitorDetailsById(visitorId);

                if (dt.Rows.Count > 0)
                {
                    GenerateVisitorPDF(dt.Rows[0]); // Generate and export PDF
                }
            }
        }

        private void GenerateVisitorPDF(DataRow visitor)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document();
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                doc.Add(new Paragraph("Visitor Pass"));
                doc.Add(new Paragraph(" ")); // line break

                doc.Add(new Paragraph("Name: " + visitor["FullName"].ToString()));
                doc.Add(new Paragraph("ID Type: " + visitor["IDType"].ToString()));
                doc.Add(new Paragraph("ID Number: " + visitor["IDNumber"].ToString()));
                doc.Add(new Paragraph("Pax: " + visitor["Pax"].ToString()));
                doc.Add(new Paragraph("Vehicle: " + visitor["Vehicle"].ToString()));
                doc.Add(new Paragraph("Valid From: " + Convert.ToDateTime(visitor["ValidFrom"]).ToString("yyyy-MM-dd HH:mm")));
                doc.Add(new Paragraph("Valid Till: " + Convert.ToDateTime(visitor["ValidTill"]).ToString("yyyy-MM-dd HH:mm")));
                doc.Add(new Paragraph("Remarks: " + visitor["Remarks"].ToString()));
                doc.Add(new Paragraph("Status: " + visitor["Status"].ToString()));

                doc.Close();

                byte[] bytes = ms.ToArray();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=VisitorPass_" + visitor["VisitorId"] + ".pdf");
                Response.BinaryWrite(bytes);
                Response.End();
            }
        }

        private void GeneratePDF(string visitorId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT VisitorId, FullName, IDType, IDNumber, Pax, Vehicle, ValidFrom, ValidTill, Remarks FROM tblVisitorCheckIn WHERE VisitorId = @VisitorId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VisitorId", visitorId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 50f, 50f);
                        MemoryStream stream = new MemoryStream();
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();

                        string imagePath = Server.MapPath("~/assets/images/logo/file.jpg");
                        if (File.Exists(imagePath))
                        {
                            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagePath);
                            logo.ScaleToFit(100f, 100f);
                            logo.Alignment = Element.ALIGN_CENTER;
                            pdfDoc.Add(logo);
                        }

                        Paragraph title = new Paragraph("Visitor Pass", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20));
                        title.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(title);
                        pdfDoc.Add(new Paragraph("\n"));

                        PdfPTable table = new PdfPTable(2);
                        table.WidthPercentage = 100;
                        table.SetWidths(new float[] { 30f, 70f });

                        AddRow(table, "Visitor ID:", reader["VisitorId"].ToString());
                        AddRow(table, "Name:", reader["FullName"].ToString());
                        AddRow(table, "ID Type:", reader["IDType"].ToString());
                        AddRow(table, "ID Number:", reader["IDNumber"].ToString());
                        AddRow(table, "Pax:", reader["Pax"].ToString());
                        AddRow(table, "Vehicle:", reader["Vehicle"].ToString());
                        AddRow(table, "Valid From:", Convert.ToDateTime(reader["ValidFrom"]).ToString("yyyy-MM-dd HH:mm"));
                        AddRow(table, "Valid Till:", Convert.ToDateTime(reader["ValidTill"]).ToString("yyyy-MM-dd HH:mm"));
                        AddRow(table, "Remarks:", reader["Remarks"].ToString());

                        pdfDoc.Add(table);
                        pdfDoc.Close();

                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=VisitorPass_" + visitorId + ".pdf");
                        Response.BinaryWrite(stream.ToArray());
                        Response.End();
                    }
                    reader.Close();
                }
            }
        }

        private void AddRow(PdfPTable table, string label, string value)
        {
            PdfPCell cell1 = new PdfPCell(new Phrase(label, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
            PdfPCell cell2 = new PdfPCell(new Phrase(value, FontFactory.GetFont(FontFactory.HELVETICA, 12)));
            cell1.Border = cell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
            table.AddCell(cell1);
            table.AddCell(cell2);
        }
    }
}
