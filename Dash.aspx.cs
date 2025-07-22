using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace VMS
{
    public partial class Dash : System.Web.UI.Page
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
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"SELECT TOP 1000 VisitorId, FullName, Company, MobileNo, Purpose, ValidFrom, ValidTill 
                                 FROM tblVisitorCheckIn ORDER BY entryDate DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvVisitors.DataSource = dt;
                        gvVisitors.DataBind();
                    }
                }
            }
        }

        protected void gvVisitors_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "PrintPDF")
            {
                string visitorId = e.CommandArgument.ToString();
                GeneratePDF(visitorId);
            }
        }

        private void GeneratePDF(string visitorId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT VisitorId,FullName,IDType,IDNumber,Pax,Vehicle,ValidFrom,ValidTill,
                                Remarks,toMeet,Company,toAddress,ApprovedBy,EntryBy
FROM tblVisitorCheckIn WHERE VisitorId = @VisitorId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VisitorId", visitorId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Document pdfDoc = new Document(PageSize.A4, 25f, 25f, 25f, 25f);
                        MemoryStream stream = new MemoryStream();
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();

                        // Logo
                        string imagePath = Server.MapPath("~/img/aambylogo.jpg");
                        if (File.Exists(imagePath))
                        {
                            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagePath);
                            logo.ScaleToFit(200f, 200f);
                            logo.Alignment = Element.ALIGN_CENTER;
                            pdfDoc.Add(logo);
                        }

                        // Title
                        Paragraph title = new Paragraph("Visitor Pass", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20));
                        title.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(title);
                        pdfDoc.Add(new Paragraph("\n"));

                        // Table
                        PdfPTable table = new PdfPTable(2);
                        table.WidthPercentage = 50;
                        table.SetWidths(new float[] { 60f, 70f });
                        table.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.PaddingTop = 20f;
                        table.DefaultCell.Border = Rectangle.BOX;

                        AddRow(table, "Visitor ID:", reader["VisitorId"].ToString());
                        AddRow(table, "Name:", reader["FullName"].ToString());
                        AddRow(table, "ID Type:", reader["IDType"].ToString());
                        AddRow(table, "ID Number:", reader["IDNumber"].ToString());
                        AddRow(table, "Pax:", reader["Pax"].ToString());
                        AddRow(table, "Vehicle:", reader["Vehicle"].ToString());
                        AddRow(table, "Valid From:", Convert.ToDateTime(reader["ValidFrom"]).ToString("yyyy-MM-dd HH:mm"));
                        AddRow(table, "Valid Till:", Convert.ToDateTime(reader["ValidTill"]).ToString("yyyy-MM-dd HH:mm"));
                        AddRow(table, "Remarks:", reader["Remarks"].ToString());
                        AddRow(table, "Meet To:", reader["toMeet"].ToString());
                        AddRow(table, "Company:", reader["Company"].ToString());
                        AddRow(table, "To Address:", reader["toAddress"].ToString());
                        AddRow(table, "Approved By:", reader["ApprovedBy"].ToString());
                        AddRow(table, "Entry By:", reader["EntryBy"].ToString());

                        pdfDoc.Add(table);
                        pdfDoc.Close();

                        

                        byte[] bytes = stream.ToArray();
                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=VisitorPass_" + visitorId + ".pdf");
                        Response.BinaryWrite(bytes);
                        Response.End();
                    }
                    reader.Close();
                }
            }
        }

        private void AddRow(PdfPTable table, string label, string value)
        {
            Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            PdfPCell cell1 = new PdfPCell(new Phrase(label, boldFont));
            cell1.Border = Rectangle.BOX;
            cell1.Padding = 10f;

            PdfPCell cell2 = new PdfPCell(new Phrase(value));
            cell2.Border = Rectangle.BOX;
            cell2.Padding = 10f;

            table.AddCell(cell1);
            table.AddCell(cell2);
        }

    }
}
