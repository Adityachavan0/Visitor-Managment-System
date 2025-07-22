using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;


namespace VMS
{
    public partial class VMSPrintGatepass : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string visitorId = Request.QueryString["VisitorId"];
                if (!string.IsNullOrEmpty(visitorId))
                {
                    LoadVisitorDetails(visitorId);
                }
            }
        }

        private void LoadVisitorDetails(string visitorId)
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
                        lblVisitorId.Text = reader["VisitorId"].ToString();
                        lblVisitorName.Text = reader["FullName"].ToString();
                        lblIDType.Text = reader["IDType"].ToString();
                        lblIDNumber.Text = reader["IDNumber"].ToString();
                        lblPax.Text = reader["Pax"].ToString();
                        lblVehicle.Text = reader["Vehicle"].ToString();
                        lblValidFrom.Text = Convert.ToDateTime(reader["ValidFrom"]).ToString("yyyy-MM-dd HH:mm");
                        lblValidTill.Text = Convert.ToDateTime(reader["ValidTill"]).ToString("yyyy-MM-dd HH:mm");
                        lblRemarks.Text = reader["Remarks"].ToString();
                    }

                    reader.Close();
                }
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

                        AddRow(table, "ID:", reader["VisitorId"].ToString());
                        AddRow(table, "Name:", reader["FullName"].ToString());
                        AddRow(table, "ID Type:", reader["IDType"].ToString());
                        AddRow(table, "ID No:", reader["IDNumber"].ToString());
                        AddRow(table, "Pax:", reader["Pax"].ToString());
                        AddRow(table, "Vehicle:", reader["Vehicle"].ToString());
                        AddRow(table, "Valid From:", Convert.ToDateTime(reader["ValidFrom"]).ToString("yyyy-MM-dd HH:mm:ss"));
                        AddRow(table, "Valid Till:", Convert.ToDateTime(reader["ValidTill"]).ToString("yyyy-MM-dd HH:mm:ss"));
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
        protected void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            string visitorId = Request.QueryString["VisitorId"];
            if (!string.IsNullOrEmpty(visitorId))
            {
                GeneratePDF(visitorId);
            }
        }

    }
}
