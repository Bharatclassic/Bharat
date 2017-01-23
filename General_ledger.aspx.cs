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
using System.Globalization;
using System.Net.NetworkInformation;
using System.Management;
using iTextSharp.text;
using iTextSharp;
using CreatePDF;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.Mail;
using iTextSharp.text.pdf.parser;
using AllHospitalNames;

public partial class General_ledger : System.Web.UI.Page
{
    protected static string strconn11 = Dbconn.conmenthod();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    ClsBLLGeneraldetails clsgd = new ClsBLLGeneraldetails();
    string sqlFormattedDate = DateTime.Now.ToString();
    Dbconn dbcon = new Dbconn();
    string day1, day2;
    double balance = 0;

    string trd, trd1;

    ArrayList oALMain = new ArrayList();
    ArrayList oALHospitalDetails = new ArrayList();
    PharmacyName Hosp = new PharmacyName();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateMainHead();
        }
    }
    protected void btnexit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    private static PdfPCell PhraseCell(Phrase phrase, int align)
    {
        PdfPCell cell = new PdfPCell(phrase);
        cell.BorderColor = BaseColor.WHITE;
        cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 2f;
        cell.PaddingTop = 2f;
        return cell;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt = Convert.ToDateTime(txtDay.Text);
            trd = dt.ToString("yyyy-MM-dd");

            DateTime dt1 = Convert.ToDateTime(TextBox1.Text);
            trd1 = dt1.ToString("yyyy-MM-dd");

            oALHospitalDetails = Hosp.HospitalReturns();
            if (txtDay.Text == "")
            {
                Master.ShowModal("Please Enter From Date", "txtday", 1);
                return;
            }
            if (TextBox1.Text == "")
            {
                Master.ShowModal("Please Enter To Date", "TextBox1", 1);
                return;
            }
            if (chkallhead.Checked == false)
            {
                if (ddlGrp.SelectedItem.Text == "Select" && chkallhead.Checked == false)
                {
                    Master.ShowModal("Please Select MainHead", "ddlGrp", 1);
                    return;
                }
                if (DropDownList1.SelectedItem.Text == "Select" && chkallhead.Checked == false)
                {
                    Master.ShowModal("Please Select MainHead", "DropDownList1", 1);
                    return;
                }
            }

            if (chkallhead.Checked == false)
            {
                SqlConnection con = new SqlConnection(strconn11);
                SqlCommand cmd = new SqlCommand("Select a.* from tbltransaction a join tblVoachermaster b on a.Accounthead = b.Headercode where b.Mainhead = '" + ddlGrp.SelectedItem.Text + "' and b.Subhead = '" + DropDownList1.SelectedItem.Text + "'and a.Trdate >='" + trd + "' and a.Trdate <='" + trd1 + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Document pdfdocument = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                    PdfWriter.GetInstance(pdfdocument, Response.OutputStream);
                    Document document1 = new Document();
                    Font Normalfont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);

                    MemoryStream memorystream = new System.IO.MemoryStream();
                    PdfWriter.GetInstance(pdfdocument, Response.OutputStream);
                    PdfWriter writer = PdfWriter.GetInstance(pdfdocument, memorystream);
                    ArrayList oALHospDetails = Hosp.HospitalReturns();

                    Phrase phrPhrase = null;
                    PdfPCell pdfCell = null;
                    PdfPTable tblHeading = null;
                    PdfPTable tblHeader = null;
                    PdfPTable tblDates = null;
                    PdfPTable tblHeadsDetails = null;
                    //PdfPTable tblCreditDebit = null;
                    PdfPTable tblSalesCreditDetails = null;
                    PdfPTable tblSalesDebitDetails = null;
                    PdfPTable tblTotalCreditAmount = null;
                    //PdfPTable tblTotalDebitAmount = null;
                    PdfPTable tblTotalAmountDetails = null;
                    PdfPTable tblPrintedBy = null;

                    pdfdocument.Open();

                    tblHeading = new PdfPTable(1);
                    tblHeading.LockedWidth = true;
                    tblHeading.TotalWidth = 580f;
                    tblHeading.SetWidths(new float[] { 1f });

                    tblHeader = new PdfPTable(1);
                    tblHeader.LockedWidth = true;
                    tblHeader.TotalWidth = 580f;
                    tblHeader.SetWidths(new float[] { 1f });

                    tblDates = new PdfPTable(2);
                    tblDates.LockedWidth = true;
                    tblDates.TotalWidth = 580f;
                    tblDates.SetWidths(new float[] { 1f, 1f });

                    tblHeadsDetails = new PdfPTable(2);
                    tblHeadsDetails.LockedWidth = true;
                    tblHeadsDetails.TotalWidth = 580f;
                    tblHeadsDetails.SetWidths(new float[] { 1f, 1f });

                    tblTotalCreditAmount = new PdfPTable(5);
                    tblTotalCreditAmount.LockedWidth = true;
                    tblTotalCreditAmount.TotalWidth = 580f;
                    tblTotalCreditAmount.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f });

                    tblTotalAmountDetails = new PdfPTable(2);
                    tblTotalAmountDetails.LockedWidth = true;
                    tblTotalAmountDetails.TotalWidth = 580f;
                    tblTotalAmountDetails.SetWidths(new float[] { 1f, 1f });

                    tblSalesCreditDetails = new PdfPTable(6);
                    tblSalesCreditDetails.LockedWidth = true;
                    tblSalesCreditDetails.TotalWidth = 580f;
                    tblSalesCreditDetails.SetWidths(new float[] { 0.3f, 0.6f, 1f, 1f, 1f, 1f });

                    tblPrintedBy = new PdfPTable(1);
                    tblPrintedBy.LockedWidth = true;
                    tblPrintedBy.TotalWidth = 580f;
                    tblPrintedBy.SetWidths(new float[] { 1f });

                    //Report Name

                    tblHeading.AddCell(PhraseCell(new Phrase("General Ledger", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                    tblHeading.SpacingAfter = 15f;

                    phrPhrase = new Phrase();
                    phrPhrase.Add(new Chunk(oALHospDetails[0].ToString() + "\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)));
                    phrPhrase.Add(new Chunk(oALHospDetails[1].ToString() + "\n" + oALHospDetails[2].ToString() + "\n" + oALHospDetails[3].ToString() + "\n" + oALHospDetails[4].ToString() + "\n\n", FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)));
                    pdfCell = PhraseCell(phrPhrase, PdfPCell.ALIGN_LEFT);
                    pdfCell.HorizontalAlignment = 0;
                    tblHeader.AddCell(pdfCell);

                    tblDates.AddCell(PhraseCell(new Phrase(txtDay.Text + "-", FontFactory.GetFont("Times", 12, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                    tblDates.AddCell(PhraseCell(new Phrase(TextBox1.Text, FontFactory.GetFont("Times", 12, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                    tblDates.SpacingAfter = 15f;

                    tblHeadsDetails.AddCell(PhraseCell(new Phrase("Main Head: " + ddlGrp.SelectedItem.Text, FontFactory.GetFont("Times", 12, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                    tblHeadsDetails.AddCell(PhraseCell(new Phrase("Sub Head : " + DropDownList1.SelectedItem.Text, FontFactory.GetFont("Times", 12, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                    tblHeadsDetails.SpacingAfter = 15f;

                    pdfCell = new PdfPCell(new Phrase(new Chunk("Sl No.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    // pdfCell.Rowspan = 2;
                    tblSalesCreditDetails.AddCell(pdfCell);

                    pdfCell = new PdfPCell(new Phrase(new Chunk("Date", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    //pdfCell.Rowspan = 2;
                    tblSalesCreditDetails.AddCell(pdfCell);

                    pdfCell = new PdfPCell(new Phrase(new Chunk("Receipt No. / \n " + "Voucher No.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    // pdfCell.Rowspan = 2;
                    tblSalesCreditDetails.AddCell(pdfCell);

                    pdfCell = new PdfPCell(new Phrase(new Chunk("Credit", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    // pdfCell.Colspan = 2;
                    tblSalesCreditDetails.AddCell(pdfCell);

                    pdfCell = new PdfPCell(new Phrase(new Chunk("Debit", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    // pdfCell.Colspan = 2;
                    tblSalesCreditDetails.AddCell(pdfCell);



                    pdfCell = new PdfPCell(new Phrase(new Chunk("Balance", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    // pdfCell.Rowspan = 2;
                    tblSalesCreditDetails.AddCell(pdfCell);

                    SqlConnection con1 = new SqlConnection(strconn11);
                    SqlCommand cmd1 = new SqlCommand("Select a.Trdate,a.Voureptno,(a.Cash_Credit + a.Adj_Card)as total,(a.Cash_Debit + a.Adj_Debit)as total1,a.Cash_Credit from tbltransaction a join tblVoachermaster b on a.Accounthead = b.Headercode where b.Mainhead = '" + ddlGrp.SelectedItem.Text + "' and b.Subhead = '" + DropDownList1.SelectedItem.Text + "' and a.Trdate >='" + trd + "' and a.Trdate <='" + trd1 + "'", con1);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);
                    int slno = 0;

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        slno++;
                        pdfCell = new PdfPCell(new Phrase(new Chunk(Convert.ToString(slno), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                        pdfCell.HorizontalAlignment = 0;
                        pdfCell.PaddingBottom = 5f;
                        tblSalesCreditDetails.AddCell(pdfCell);
                        tblSalesCreditDetails.SpacingAfter = 15f;
                        for (int j = 0; j < ds1.Tables[0].Columns.Count; j++)
                        {
                            if (j == 0)
                            {
                                DateTime day = Convert.ToDateTime(ds1.Tables[0].Rows[i][j].ToString());
                                string day1 = day.ToString("dd-MM-yyyy");
                                pdfCell = new PdfPCell(new Phrase(new Chunk(day1, FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                pdfCell.HorizontalAlignment = 0;
                                pdfCell.PaddingBottom = 5f;
                                tblSalesCreditDetails.AddCell(pdfCell);
                                tblSalesCreditDetails.SpacingAfter = 15f;

                            }
                            else if (j == 1)
                            {
                                pdfCell = new PdfPCell(new Phrase(new Chunk(ClsBLGD.base64Decode(ds1.Tables[0].Rows[i][j].ToString()), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                pdfCell.HorizontalAlignment = 0;
                                pdfCell.PaddingBottom = 5f;
                                tblSalesCreditDetails.AddCell(pdfCell);
                                tblSalesCreditDetails.SpacingAfter = 15f;
                            }
                            else if (j == 4)
                            {
                                if (balance > 0)
                                {
                                    pdfCell = new PdfPCell(new Phrase(new Chunk("Cr." + Convert.ToString(balance), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                    pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    pdfCell.PaddingBottom = 5f;
                                    tblSalesCreditDetails.AddCell(pdfCell);
                                    tblSalesCreditDetails.SpacingAfter = 15f;
                                }
                                else if (balance < 0)
                                {
                                    balance = balance * (-1);
                                    pdfCell = new PdfPCell(new Phrase(new Chunk("Dr." + Convert.ToString(balance), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                    pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    pdfCell.PaddingBottom = 5f;
                                    tblSalesCreditDetails.AddCell(pdfCell);
                                    tblSalesCreditDetails.SpacingAfter = 15f;
                                }

                            }

                            else if (j == 2)
                            {
                                pdfCell = new PdfPCell(new Phrase(new Chunk(ds1.Tables[0].Rows[i][j].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                pdfCell.PaddingBottom = 5f;
                                tblSalesCreditDetails.AddCell(pdfCell);
                                tblSalesCreditDetails.SpacingAfter = 15f;


                                balance = balance + Convert.ToDouble(ds1.Tables[0].Rows[i][j].ToString());

                            }
                            else if (j == 3)
                            {
                                pdfCell = new PdfPCell(new Phrase(new Chunk(ds1.Tables[0].Rows[i][j].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                pdfCell.PaddingBottom = 5f;
                                tblSalesCreditDetails.AddCell(pdfCell);
                                tblSalesCreditDetails.SpacingAfter = 15f;

                                balance = balance - Convert.ToDouble(ds1.Tables[0].Rows[i][j].ToString());

                            }

                        }
                    }

                    tblSalesCreditDetails.SpacingAfter = 15f;


                    pdfdocument.Add(tblHeading);
                    pdfdocument.Add(tblHeader);
                    pdfdocument.Add(tblDates);
                    pdfdocument.Add(tblHeadsDetails);
                    //pdfSales.Add(tblCreditDebit);
                    pdfdocument.Add(tblSalesCreditDetails);
                    pdfdocument.Add(tblTotalAmountDetails);
                    pdfdocument.Add(tblPrintedBy);

                    pdfdocument.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=GeneralLedger.pdf");

                    byte[] bytes = memorystream.ToArray();
                    memorystream.Close();
                    Response.Clear();

                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(bytes);
                    Response.End();
                    Response.Close();
                }
                else
                {
                    Master.ShowModal("There is No Transaction Between These Dates !!!", "txtday", 1);
                    return;

                }
            }
            if (chkallhead.Checked == true)
            {
                SqlConnection con = new SqlConnection(strconn11);
                SqlCommand cmd = new SqlCommand("Select a.* from tbltransaction a join tblVoachermaster b on a.Accounthead = b.Headercode where a.Trdate >='" + trd + "' and a.Trdate <='" + trd1 + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    Document pdfdocument = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                    PdfWriter.GetInstance(pdfdocument, Response.OutputStream);
                    Document document1 = new Document();
                    Font Normalfont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);

                    MemoryStream memorystream = new System.IO.MemoryStream();
                    PdfWriter.GetInstance(pdfdocument, Response.OutputStream);
                    PdfWriter writer = PdfWriter.GetInstance(pdfdocument, memorystream);
                    ArrayList oALHospDetails = Hosp.HospitalReturns();

                    Phrase phrPhrase = null;
                    PdfPCell pdfCell = null;
                    PdfPTable tblHeading = null;
                    PdfPTable tblHeader = null;
                    PdfPTable tblDates = null;
                    PdfPTable tblHeadsDetails = null;
                    //PdfPTable tblCreditDebit = null;
                    PdfPTable tblSalesCreditDetails = null;
                    PdfPTable tblSalesDebitDetails = null;
                    PdfPTable tblTotalCreditAmount = null;
                    //PdfPTable tblTotalDebitAmount = null;
                    PdfPTable tblTotalAmountDetails = null;
                    PdfPTable tblPrintedBy = null;

                    pdfdocument.Open();

                    tblHeading = new PdfPTable(1);
                    tblHeading.LockedWidth = true;
                    tblHeading.TotalWidth = 580f;
                    tblHeading.SetWidths(new float[] { 1f });

                    tblHeader = new PdfPTable(1);
                    tblHeader.LockedWidth = true;
                    tblHeader.TotalWidth = 580f;
                    tblHeader.SetWidths(new float[] { 1f });

                    tblDates = new PdfPTable(2);
                    tblDates.LockedWidth = true;
                    tblDates.TotalWidth = 580f;
                    tblDates.SetWidths(new float[] { 1f, 1f });

                    tblHeadsDetails = new PdfPTable(2);
                    tblHeadsDetails.LockedWidth = true;
                    tblHeadsDetails.TotalWidth = 580f;
                    tblHeadsDetails.SetWidths(new float[] { 1f, 1f });

                    tblTotalCreditAmount = new PdfPTable(5);
                    tblTotalCreditAmount.LockedWidth = true;
                    tblTotalCreditAmount.TotalWidth = 580f;
                    tblTotalCreditAmount.SetWidths(new float[] { 1f, 1f, 1f, 1f, 1f });

                    tblTotalAmountDetails = new PdfPTable(2);
                    tblTotalAmountDetails.LockedWidth = true;
                    tblTotalAmountDetails.TotalWidth = 580f;
                    tblTotalAmountDetails.SetWidths(new float[] { 1f, 1f });

                    tblSalesCreditDetails = new PdfPTable(6);
                    tblSalesCreditDetails.LockedWidth = true;
                    tblSalesCreditDetails.TotalWidth = 580f;
                    tblSalesCreditDetails.SetWidths(new float[] { 0.3f, 0.6f, 1f, 1f, 1f, 1f });

                    tblPrintedBy = new PdfPTable(1);
                    tblPrintedBy.LockedWidth = true;
                    tblPrintedBy.TotalWidth = 580f;
                    tblPrintedBy.SetWidths(new float[] { 1f });

                    //Report Name

                    tblHeading.AddCell(PhraseCell(new Phrase("General Ledger", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                    tblHeading.SpacingAfter = 15f;

                    phrPhrase = new Phrase();
                    phrPhrase.Add(new Chunk(oALHospDetails[0].ToString() + "\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)));
                    phrPhrase.Add(new Chunk(oALHospDetails[1].ToString() + "\n" + oALHospDetails[2].ToString() + "\n" + oALHospDetails[3].ToString() + "\n" + oALHospDetails[4].ToString() + "\n\n", FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)));
                    pdfCell = PhraseCell(phrPhrase, PdfPCell.ALIGN_LEFT);
                    pdfCell.HorizontalAlignment = 0;
                    tblHeader.AddCell(pdfCell);

                    tblDates.AddCell(PhraseCell(new Phrase(txtDay.Text + "-", FontFactory.GetFont("Times", 12, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                    tblDates.AddCell(PhraseCell(new Phrase(TextBox1.Text, FontFactory.GetFont("Times", 12, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                    tblDates.SpacingAfter = 15f;

                    pdfdocument.Add(tblHeading);
                    pdfdocument.Add(tblHeader);
                    pdfdocument.Add(tblDates);

                    SqlConnection con5 = new SqlConnection(strconn11);
                    SqlCommand cmd5 = new SqlCommand("Select distinct(b.Headercode), b.Mainhead,b.Subhead from tbltransaction a join tblVoachermaster b on a.Accounthead = b.Headercode where a.Trdate >='" + trd + "' and a.Trdate <='" + trd1 + "'", con5);
                    SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                    DataSet ds5 = new DataSet();
                    da5.Fill(ds5);

                    for (int k = 0; k < ds5.Tables[0].Rows.Count; k++)
                    {
                        

                        string mainhead = ds5.Tables[0].Rows[k]["Mainhead"].ToString();
                        string subhead = ds5.Tables[0].Rows[k]["Subhead"].ToString();
                        string headcode = ds5.Tables[0].Rows[k]["Headercode"].ToString();


                        tblHeadsDetails.AddCell(PhraseCell(new Phrase("Main Head: " + mainhead, FontFactory.GetFont("Times", 12, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                        tblHeadsDetails.AddCell(PhraseCell(new Phrase("Sub Head : " + subhead, FontFactory.GetFont("Times", 12, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                        tblHeadsDetails.SpacingAfter = 15f;

                        pdfCell = new PdfPCell(new Phrase(new Chunk("Sl No.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                        pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        // pdfCell.Rowspan = 2;
                        tblSalesCreditDetails.AddCell(pdfCell);

                        pdfCell = new PdfPCell(new Phrase(new Chunk("Date", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                        pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        //pdfCell.Rowspan = 2;
                        tblSalesCreditDetails.AddCell(pdfCell);

                        pdfCell = new PdfPCell(new Phrase(new Chunk("Receipt No. / \n " + "Voucher No.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                        pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        // pdfCell.Rowspan = 2;
                        tblSalesCreditDetails.AddCell(pdfCell);

                        pdfCell = new PdfPCell(new Phrase(new Chunk("Credit", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                        pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        // pdfCell.Colspan = 2;
                        tblSalesCreditDetails.AddCell(pdfCell);

                        pdfCell = new PdfPCell(new Phrase(new Chunk("Debit", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                        pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        // pdfCell.Colspan = 2;
                        tblSalesCreditDetails.AddCell(pdfCell);



                        pdfCell = new PdfPCell(new Phrase(new Chunk("Balance", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                        pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        // pdfCell.Rowspan = 2;
                        tblSalesCreditDetails.AddCell(pdfCell);

                        SqlConnection con1 = new SqlConnection(strconn11);
                        SqlCommand cmd1 = new SqlCommand("Select a.Trdate,a.Voureptno,(a.Cash_Credit + a.Adj_Card)as total,(a.Cash_Debit + a.Adj_Debit)as total1,a.Accounthead from tbltransaction a join tblVoachermaster b on a.Accounthead = b.Headercode where b.Headercode = '" + headcode + "' and a.Trdate >='" + trd + "' and a.Trdate <='" + trd1 + "'", con1);
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        int slno = 0;



                        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            slno++;
                            pdfCell = new PdfPCell(new Phrase(new Chunk(Convert.ToString(slno), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                            pdfCell.HorizontalAlignment = 0;
                            pdfCell.PaddingBottom = 5f;
                            tblSalesCreditDetails.AddCell(pdfCell);
                            tblSalesCreditDetails.SpacingAfter = 15f;


                            for (int j = 0; j < ds1.Tables[0].Columns.Count; j++)
                            {
                                if (j == 0)
                                {
                                    DateTime day = Convert.ToDateTime(ds1.Tables[0].Rows[i][j].ToString());
                                    string day1 = day.ToString("dd-MM-yyyy");
                                    pdfCell = new PdfPCell(new Phrase(new Chunk(day1, FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                    pdfCell.HorizontalAlignment = 0;
                                    pdfCell.PaddingBottom = 5f;
                                    tblSalesCreditDetails.AddCell(pdfCell);
                                    tblSalesCreditDetails.SpacingAfter = 15f;

                                }
                                else if (j == 1)
                                {
                                    pdfCell = new PdfPCell(new Phrase(new Chunk(ClsBLGD.base64Decode(ds1.Tables[0].Rows[i][j].ToString()), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                    pdfCell.HorizontalAlignment = 0;
                                    pdfCell.PaddingBottom = 5f;
                                    tblSalesCreditDetails.AddCell(pdfCell);
                                    tblSalesCreditDetails.SpacingAfter = 15f;
                                }
                                else if (j == 4)
                                {
                                    if (balance > 0)
                                    {
                                        pdfCell = new PdfPCell(new Phrase(new Chunk("Cr." + Convert.ToString(balance), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                        pdfCell.PaddingBottom = 5f;
                                        tblSalesCreditDetails.AddCell(pdfCell);
                                        tblSalesCreditDetails.SpacingAfter = 15f;
                                    }
                                    else if (balance < 0)
                                    {
                                        balance = balance * (-1);
                                        pdfCell = new PdfPCell(new Phrase(new Chunk("Dr." + Convert.ToString(balance), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                        pdfCell.PaddingBottom = 5f;
                                        tblSalesCreditDetails.AddCell(pdfCell);
                                        tblSalesCreditDetails.SpacingAfter = 15f;
                                    }

                                }

                                else if (j == 2)
                                {
                                    pdfCell = new PdfPCell(new Phrase(new Chunk(ds1.Tables[0].Rows[i][j].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                    pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    pdfCell.PaddingBottom = 5f;
                                    tblSalesCreditDetails.AddCell(pdfCell);
                                    tblSalesCreditDetails.SpacingAfter = 15f;


                                    balance = balance + Convert.ToDouble(ds1.Tables[0].Rows[i][j].ToString());

                                }
                                else if (j == 3)
                                {
                                    pdfCell = new PdfPCell(new Phrase(new Chunk(ds1.Tables[0].Rows[i][j].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                    pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    pdfCell.PaddingBottom = 5f;
                                    tblSalesCreditDetails.AddCell(pdfCell);
                                    tblSalesCreditDetails.SpacingAfter = 15f;

                                    balance = balance - Convert.ToDouble(ds1.Tables[0].Rows[i][j].ToString());

                                }

                            }
                        }


                        tblSalesCreditDetails.SpacingAfter = 15f;
                        pdfdocument.Add(tblHeadsDetails);
                        pdfdocument.Add(tblSalesCreditDetails);
                        balance = 0;

                        tblHeadsDetails = new PdfPTable(2);
                        tblHeadsDetails.LockedWidth = true;
                        tblHeadsDetails.TotalWidth = 580f;
                        tblHeadsDetails.SetWidths(new float[] { 1f, 1f });

                        tblSalesCreditDetails = new PdfPTable(6);
                        tblSalesCreditDetails.LockedWidth = true;
                        tblSalesCreditDetails.TotalWidth = 580f;
                        tblSalesCreditDetails.SetWidths(new float[] { 0.3f, 0.6f, 1f, 1f, 1f, 1f });
                       

                    }

                    DataSet dslogin = ClsBLGD.GetcondDataSet("*", "tblLogin", "username", Session["username"].ToString());
                    tblPrintedBy.AddCell(PhraseCell(new Phrase("\n\n" + "Generated On:" + sqlFormattedDate, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                    tblPrintedBy.AddCell(PhraseCell(new Phrase("\n\n" + "Generated By:" + "(" + dslogin.Tables[0].Rows[0]["UserName"].ToString() + ")", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                    pdfCell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                    pdfCell.Colspan = 4;
                    pdfCell.PaddingBottom = 30f;
                    tblPrintedBy.AddCell(pdfCell);

                    //pdfdocument.Add(tblHeading);
                    //pdfdocument.Add(tblHeader);
                    //pdfdocument.Add(tblDates);
                    //pdfdocument.Add(tblHeadsDetails);
                    //pdfSales.Add(tblCreditDebit);
                    //pdfdocument.Add(tblSalesCreditDetails);

                    pdfdocument.Add(tblTotalAmountDetails);
                    pdfdocument.Add(tblPrintedBy);

                    pdfdocument.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=GeneralLedger.pdf");

                    byte[] bytes = memorystream.ToArray();
                    memorystream.Close();
                    Response.Clear();

                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(bytes);
                    Response.End();
                    Response.Close();
                }
                else
                {
                    Master.ShowModal("There is No Transaction Between These Dates !!!", "txtday", 1);
                    return;

                }

            }

        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;

        }
        //Response.End();
        //Response.Close();
    }
    protected void txtDay_TextChanged(object sender, EventArgs e)
    {
        DataSet fin = clsgd.GetcondDataSet("*", "tblFin_year", "fin_year_close", "N");
        DateTime date = Convert.ToDateTime(fin.Tables[0].Rows[0]["fin_from_date"].ToString());
        DateTime date1 = Convert.ToDateTime(txtDay.Text);
       // DateTime date3 = Convert.ToDateTime(TextBox1.Text);
        DateTime date2 = Convert.ToDateTime(fin.Tables[0].Rows[0]["fin_to_date"].ToString());
        day1 = date.ToString("yyyy-MM-dd");
        day2 = date2.ToString("yyyy-MM-dd");

        DateTime test;
        if (DateTime.TryParseExact(txtDay.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out test) == true)
        {
            TextBox1.Focus();
        }
        else
        {
            Master.ShowModal("Entered Date is not in correct format !!!", "txtDay", 1);
        }
        if (date1 < date || date1 > date2)
        {
            txtDay.Text = string.Empty;
            Master.ShowModal("Date Should be in Financial Year from '"+day1+"' to '"+day2+"'", "txtDay", 1);
            //return;
        }     

    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        DataSet fin = clsgd.GetcondDataSet("*", "tblFin_year", "fin_year_close", "N");
        DateTime date = Convert.ToDateTime(fin.Tables[0].Rows[0]["fin_from_date"].ToString());
        DateTime date1 = Convert.ToDateTime(TextBox1.Text);
        DateTime date3 = Convert.ToDateTime(txtDay.Text);
        DateTime date2 = Convert.ToDateTime(fin.Tables[0].Rows[0]["fin_to_date"].ToString());
        day1 = date.ToString("yyyy-MM-dd");
        day2 = date2.ToString("yyyy-MM-dd");

        DateTime test;
        if (DateTime.TryParseExact(txtDay.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out test) == true)
        {
            chkallhead.Focus();
        }
        else
        {
            Master.ShowModal("Entered Date is not in correct format !!!", "TextBox1", 1);
        }
        if (date1 > date2 || date1 < date3)
        {
            TextBox1.Text = string.Empty;

            Master.ShowModal("Date Should be in Financial Year from '" + day1 + "' to '" + day2 + "'", "TextBox1", 1);
           // return;
        }     
    }
    public void PopulateMainHead()
    {
        DataSet mainhead = clsgd.GetDataSet("distinct Mainhead", "tblVoachermaster");
        oALMain.Sort();
        oALMain.Add("Select");
        for (int i = 0; i < mainhead.Tables[0].Rows.Count; i++)
        {
            oALMain.Add(mainhead.Tables[0].Rows[i]["Mainhead"].ToString());
        }
      
        ddlGrp.DataSource = oALMain;
        ddlGrp.DataBind();

    }
    protected void ddlGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet subhead = clsgd.GetcondDataSet("Subhead", "tblVoachermaster", "Mainhead", ddlGrp.SelectedItem.Text);
        oALMain.Sort();
        oALMain.Add("Select");
        for (int i = 0; i < subhead.Tables[0].Rows.Count; i++)
        {
            oALMain.Add(subhead.Tables[0].Rows[i]["Subhead"].ToString());
        }
        DropDownList1.DataSource = oALMain;
        DropDownList1.DataBind();

    }
    protected void chkallhead_CheckedChanged(object sender, EventArgs e)
    {
        if(chkallhead.Checked == true)
        {
            lblGrp.Visible = false;
            ddlGrp.Visible = false;
            Label2.Visible = false;
            DropDownList1.Visible = false;
        }
        if (chkallhead.Checked == false)
        {
            lblGrp.Visible = true;
            ddlGrp.Visible = true;
            Label2.Visible = true;
            DropDownList1.Visible = true;
        }
    }
}