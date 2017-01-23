using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text.RegularExpressions;
using System.Configuration;
using AlertMessageName;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Web.Services;
using System.Data.OleDb;
using System.Text;
using System.Net.NetworkInformation;
using System.Management;
using System.Runtime.InteropServices;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.Mail;
using iTextSharp.text.pdf.parser;

public partial class Custsalerpt : System.Web.UI.Page
{
    DataTable tblstockinward = new DataTable();
    DataRow dr2;
   
    
  
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    protected static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
    protected string button_select;
    DataTable tblwarehouse = new DataTable();
    DataTable dt = new DataTable();
    DataRow drrw;
    string sMacAddress = "";
    string Wh_Code;
    int count;

    ArrayList arryno = new ArrayList();

    ArrayList arryname = new ArrayList();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSalesRegPrint_Click(object sender, EventArgs e)
    {
          try
        {
            SqlConnection con = new SqlConnection(strconn11);
            SqlCommand cmd = new SqlCommand("select * from tblProductsale", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

           
            DataTable tbl1 = new DataTable();
            if (grdTransDetails.HeaderRow != null)
            {
                for (int i = 0; i < grdTransDetails.HeaderRow.Cells.Count; i++)
                {
                    tbl1.Columns.Add(grdTransDetails.HeaderRow.Cells[i].Text);
                }
            }
            foreach (GridViewRow grdrow in grdTransDetails.Rows)
            {
                DataRow drrow;
                drrow = tbl1.NewRow();
                for (int i = 0; i < grdTransDetails.HeaderRow.Cells.Count; i++)
                {
                    drrow[i] = grdrow.Cells[i].Text;
                }
                tbl1.Rows.Add(drrow);
            }
            MemoryStream memStream = new MemoryStream();
            Document pdfreports = new Document(PageSize.A4, 10f, 10f, 10f, 10f);

            PdfWriter pdfwriter = PdfWriter.GetInstance(pdfreports, memStream);
            PdfWriterEvents pdfevents = new PdfWriterEvents("Result");
            pdfwriter.PageEvent = pdfevents;

            PdfPCell pdfcell = null;
            PdfPTable tblHeader = null;
            PdfPTable tblsubHeader = null;
            PdfPTable tblnameID = null;
            PdfPTable tbldetails = null;
            PdfPTable tblprintedby = null;
            PdfPTable tblmainHeader = null;
            PdfPTable tbladd = null;
            //PdfPCell header = null;
            PdfPTable tblTotalAmount = null;

            pdfreports.Open();

            tblHeader = new PdfPTable(1);
            tblHeader.LockedWidth = true;
            tblHeader.TotalWidth = 580f;
            tblHeader.SetWidths(new float[] { 1f });

            tblmainHeader = new PdfPTable(1);
            tblmainHeader.LockedWidth = true;
            tblmainHeader.TotalWidth = 550f;
            tblmainHeader.SetWidths(new float[] { 1f });

            tblsubHeader = new PdfPTable(1);
            tblsubHeader.LockedWidth = true;
            tblsubHeader.TotalWidth = 580f;
            tblsubHeader.SetWidths(new float[] { 1f });

            tblnameID = new PdfPTable(6);
            tblnameID.LockedWidth = true;
            tblnameID.TotalWidth = 580f;
            tblnameID.SetWidths(new float[] { 1f, 0.1f, 2f, 1f, 0.1f, 1f });

            tbldetails = new PdfPTable(7);
            tbldetails.LockedWidth = true;
            tbldetails.TotalWidth = 580f;
            tbldetails.SetWidths(new float[] { 0.5f, 0.8f, 1.2f, 1.2f, 1.2f, 1.2f, 1.2f });

            tblTotalAmount = new PdfPTable(1);
            tblTotalAmount.LockedWidth = true;
            tblTotalAmount.TotalWidth = 580f;
            tblTotalAmount.SetWidths(new float[] { 1f });

            tblprintedby = new PdfPTable(1);
            tblprintedby.LockedWidth = true;
            tblprintedby.TotalWidth = 580f;
            tblprintedby.SetWidths(new float[] { 1f });

            tbladd = new PdfPTable(1);
            tbladd.LockedWidth = true;
            tbladd.TotalWidth = 550f;
            tbladd.SetWidths(new float[] { 1f });

            tblHeader.AddCell(PhraseCell(new Phrase("Brandwise  Result", FontFactory.GetFont("Times", 22, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
            tblHeader.SpacingAfter = 15f;

            tblsubHeader.AddCell(PhraseCell(new Phrase("Brandcode", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
            tblsubHeader.SpacingAfter = 15f;

            tblsubHeader.AddCell(PhraseCell(new Phrase("Productcode", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
            tblsubHeader.SpacingAfter = 15f;


            tblsubHeader.AddCell(PhraseCell(new Phrase("pname", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
            tblsubHeader.SpacingAfter = 15f;



           

            tblnameID.AddCell(PhraseCell(new Phrase("" + "", FontFactory.GetFont("Times", 12, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
            tblnameID.AddCell(PhraseCell(new Phrase("" + "", FontFactory.GetFont("Times", 12, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
            tblnameID.AddCell(PhraseCell(new Phrase("" + "", FontFactory.GetFont("Times", 12, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
            tblnameID.SpacingAfter = 20f;

            //pdfcell = new PdfPCell(new Phrase(new Chunk("slno", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
            // pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            //tbldetails.AddCell(pdfcell);

            // pdfcell = new PdfPCell(new Phrase(new Chunk("USN", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
            // pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            // tbldetails.AddCell(pdfcell);

            //pdfcell = new PdfPCell(new Phrase(new Chunk("Name", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
            //pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            //tbldetails.AddCell(pdfcell);

          

            for (int row = 0; row < tbl1.Rows.Count; row++)
            {
                for (int column = 0; column < tbl1.Columns.Count; column++)
                {
                    if (column == 2 || column == 3 || column == 5)
                    {
                        pdfcell = new PdfPCell(new Phrase(new Chunk(tbl1.Rows[row][column].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                        pdfcell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        tbldetails.AddCell(pdfcell);
                    }
                    else
                    {
                        pdfcell = new PdfPCell(new Phrase(new Chunk(tbl1.Rows[row][column].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                        pdfcell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        tbldetails.AddCell(pdfcell);
                    }
                }
            }
            tbldetails.SpacingAfter = 15f;


            pdfreports.Add(tblHeader);
            pdfreports.Add(tblsubHeader);
            pdfreports.Add(tblnameID);
            pdfreports.Add(tbldetails);
            pdfreports.Add(tblTotalAmount);
            pdfreports.Add(tblprintedby);

            //Add all Tables Ends

            pdfreports.Close();

            byte[] bytes = memStream.ToArray();
            memStream.Close();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-disposition", "attachment: filename=DailyCollectionRpt.pdf");
            // Response.AddHeader("Contect-Disposition", "attachment; filename=" + ddlCustId.SelectedItem.Text + "-" + ddlCustName.SelectedItem.Text + ".pdf");
            Response.ContentType = "application/pdf";

            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);

            grdTransDetails.DataSource = null;
            grdTransDetails.DataBind();
            tblstockinward.Rows.Clear();

            Response.End();
            Response.Close();

        }
        catch (Exception ee)
        {
            string asd = ee.Message;
            //lblError.Visible = true;
           // lblError.Text = asd;
        }
    }

    private static PdfPCell PhraseCell(Phrase phrase, int align)
    {
        PdfPCell cell = new PdfPCell(phrase);
        cell.BorderColor = BaseColor.WHITE;
        cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 2f;
        cell.PaddingTop = 0f;
        return cell;
    }

    
}