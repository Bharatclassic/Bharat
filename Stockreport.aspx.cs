using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls.WebParts;
using System.Web.Services;
using System.Net.NetworkInformation;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.Mail;
using iTextSharp.text.pdf.parser;
using System.Globalization;
using custom.util;
using AllHospitalNames;
using System.IO;
using System.Collections;

public partial class Stockreport : System.Web.UI.Page
{
    DataTable tblProductinward = new DataTable();
    protected static string strconn11 = Dbconn.conmenthod();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    string sqlFormattedDate = DateTime.Now.ToString();
     DataRow drrw;
     PharmacyName Hosp = new PharmacyName();
     ArrayList arryno = new ArrayList();
     decimal sum = 0;
     decimal totalvalue = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Visible = false;
        lblsuccess.Visible = false;
        System.DateTime Dtnow = DateTime.Now;
        string Sysdatetime = Dtnow.ToString("dd/MM/yyyy");
        //txtdate.Text = Sysdatetime;
       // txtdate.Enabled = false;
        btnexit.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + chkexpiry.ClientID + "').focus();return false;}} else {return true}; ");
        if (!Page.IsPostBack)
        {
            txtdate.Text = Sysdatetime;
            PanelSupp.Visible = false;
            Panel1.Visible = false;
           // rdsup.Checked = false;
        }
      /*  if ((rdsup.Checked = true) || (rdprod.Checked = true))
        {
            lblexpire.Visible = false;
            chkexpiry.Visible = false;
        }*/
        lblexpire.Visible = false;
        chkexpiry1.Visible = false;
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
    protected void btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkexpiry.Checked == true)
            {
            SqlConnection con5 = new SqlConnection(strconn11);
            DateTime expdate1 = Convert.ToDateTime(txtdate.Text);
            string date2 = expdate1.ToString("yyyy-MM-dd");
            SqlCommand cmd5 = new SqlCommand("Select a.ProductName,SUM(a.Stockinhand) as Stockinhand,b.SupplierName,a.Invoiceno,a.Invoicedate,a.Batchid,a.Expiredate, SUM(a.Purchaseprice) as pp from tblProductinward a left join tblsuppliermaster b on a.SuppplierCode = b.SupplierCode  where a.Stockinhand >'0' and a.Expiredate != '1900-01-01 00:00:00.000'and a.Expiredate <= '" + date2 + "'   group by a.ProductName,a.Purchaseprice,b.SupplierName,a.Invoiceno,a.Invoicedate,a.Batchid,a.Expiredate,a.Purchaseprice ORDER BY a.ProductName ASC", con5);
            SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
            DataSet ds5 = new DataSet();
            da5.Fill(ds5);
            
                if (ds5.Tables[0].Rows.Count == 0)
                {
                    Master.ShowModal("There is No Expire Medicine!!!", "txtdate", 1);
                    return;
                }
            }         
            
            
            SqlConnection con2 = new SqlConnection(strconn11);
            SqlCommand cmd2 = new SqlCommand("Select ProductName,Stockinhand,MRP from tblProductinward where Stockinhand >0  ORDER BY ProductName ASC;", con2);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ArrayList oALHospDetails = Hosp.HospitalReturns();
                Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                PdfWriter.GetInstance(document, Response.OutputStream);
                Document document1 = new Document();
                Font Normalfont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);

                MemoryStream memorystream = new System.IO.MemoryStream();
                PdfWriter.GetInstance(document, Response.OutputStream);
                PdfWriter writer = PdfWriter.GetInstance(document, memorystream);
                DataTable dtPdfstock = new DataTable();

                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable tblsupplier = null;
                PdfPTable tblstock = null;
                PdfPTable table1 = null;
                PdfPTable table2 = null;
                PdfPTable table3 = null;
                PdfPTable table4 = null;
                PdfPTable table5 = null;
                PdfPTable table6 = null;
                PdfPTable tblsum = null;
                PdfPTable tbltotal = null;
                PdfPTable date = null;
                PdfPCell GridCell = null;
                BaseColor color = null;

                document.Open();

                tblstock = new PdfPTable(1);
                tblstock.TotalWidth = 490f;
                tblstock.LockedWidth = true;
                tblstock.SetWidths(new float[] { 1f });

                tblsupplier = new PdfPTable(1);
                tblsupplier.TotalWidth  = 460f;
                tblsupplier.LockedWidth = true;
                tblsupplier.SetWidths(new float[] { 1f});

                date = new PdfPTable(1);
                date.TotalWidth = 490f;
                date.LockedWidth = true;
                date.SetWidths(new float[] { 1f });

                table1 = new PdfPTable(9);
                table1.TotalWidth = 490f;
                table1.LockedWidth = true;
                table1.SetWidths(new float[] { 0.3f, 1.2f, 0.3f, 1.2f, 0.6f, 0.6f,0.4f,0.6f, 0.5f});

                table2 = new PdfPTable(7);
                table2.TotalWidth = 450f;
                //table2.HorizontalAlignment = Element.ALIGN_LEFT;
                table2.LockedWidth = true;
                table2.SetWidths(new float[] { 0.3f, 1.2f, 0.3f, 1.2f, 0.6f, 0.6f, 0.5f});

                table3 = new PdfPTable(1);
                table3.TotalWidth = 490f;
               // table2.HorizontalAlignment = Element.ALIGN_LEFT;
                table3.LockedWidth = true;
                table3.SetWidths(new float[] { 1f });

                table4 = new PdfPTable(2);
                table4.TotalWidth = 490f;
                table4.LockedWidth = true;
                table4.SetWidths(new float[] { 1f, 1.4f });

                table5 = new PdfPTable(6);
                table5.TotalWidth = 460f;
                table5.LockedWidth = true;
                table5.SetWidths(new float[] { 0.3f,2f,0.7f,0.7f,1f,0.9f});

                table6 = new PdfPTable(6);
                table6.TotalWidth = 460f;
                table6.LockedWidth = true;
                table6.SetWidths(new float[] { 0.3f, 2f, 0.7f, 0.7f, 1f, 0.9f });

                tblsum = new PdfPTable(1);
                tblsum.TotalWidth = 450f;
                tblsum.LockedWidth = true;
                tblsum.SetWidths(new float[] { 1f });

                tbltotal = new PdfPTable(1);
                tbltotal.TotalWidth = 450f;
                tbltotal.LockedWidth = true;
                tbltotal.SetWidths(new float[] { 1f });



                date.AddCell(PhraseCell(new Phrase("Date:" + txtdate.Text, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_RIGHT);
                cell.Colspan = 2;
                cell.PaddingBottom = 30f;
                if (rdsup.Checked == true)
                {
                    tblstock.AddCell(PhraseCell(new Phrase("Stock Report on Supplier\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                    cell.Colspan = 2;
                    cell.PaddingBottom = 30f;
                    tblstock.AddCell(cell);

                    //tblsupplier.AddCell(PhraseCell(new Phrase("Supplier Code:" + txtSuppCode.Text, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                    tblsupplier.AddCell(PhraseCell(new Phrase("Supplier Name:" + txtSuppName.Text, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                    cell.Colspan = 2;
                    cell.PaddingBottom = 30f;
                    tblsupplier.AddCell(cell);
                    tblsupplier.SpacingAfter = 5f;

                    GridCell = new PdfPCell(new Phrase(new Chunk("SlNo", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table5.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("ProductName", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table5.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Stock", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table5.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Inv No.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table5.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Inv Date", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table5.AddCell(GridCell);

                    //GridCell = new PdfPCell(new Phrase(new Chunk("", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                  //  GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                  //  table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("PP(Rs.)", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table5.AddCell(GridCell);
                    table5.SpacingAfter = 15f;

                    SqlConnection con5 = new SqlConnection(strconn11);
                    SqlCommand cmd5 = new SqlCommand("Select ProductName ,SUM(Stockinhand) as Stockinhand,Invoiceno,Invoicedate, SUM(Purchaseprice) as pp from tblProductinward  where Stockinhand >'0' and SuppplierCode = '" + txtSuppCode.Text + "' group by ProductName ,Stockinhand ,Stockinhand,Invoiceno,Invoicedate, Purchaseprice ORDER BY ProductName ASC", con5);
                    SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                    DataSet ds5 = new DataSet();
                    da5.Fill(ds5);

                     int no = 0;

                    if (ds5.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds5.Tables[0].Rows.Count; j++)
                        {
                            no++;
                            GridCell = new PdfPCell(new Phrase(new Chunk(no.ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                            GridCell.HorizontalAlignment = 0;
                            GridCell.PaddingBottom = 5f;
                            table5.AddCell(GridCell);
                            for (int row1 = 0; row1 < ds5.Tables[0].Columns.Count; row1++)
                            {
                                if (row1 == 3)
                                {
                                    DateTime inv = Convert.ToDateTime(ds5.Tables[0].Rows[j][row1].ToString());
                                    string invdate = inv.ToString("dd-MM-yyyy");
                                    GridCell = new PdfPCell(new Phrase(new Chunk(invdate, FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = 0;
                                    GridCell.PaddingBottom = 5f;
                                    table5.AddCell(GridCell);
                                }
                             
                                else if (row1 == 4)
                                {
                                    decimal tsh = Convert.ToDecimal(ds5.Tables[0].Rows[j][1].ToString());
                                    decimal pp = Convert.ToDecimal(ds5.Tables[0].Rows[j][row1].ToString());
                                    decimal total = (tsh * pp);
                                    sum = total + sum;
                                    string product = total.ToString("F");
                                    //string product = Convert.ToString(total);
                                    GridCell = new PdfPCell(new Phrase(new Chunk(product, FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    GridCell.PaddingBottom = 5f;
                                    table5.AddCell(GridCell);
                                }
                                else
                                {
                                    GridCell = new PdfPCell(new Phrase(new Chunk(ds5.Tables[0].Rows[j][row1].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = 0;
                                    GridCell.PaddingBottom = 5f;
                                    table5.AddCell(GridCell);
                                }
                            }
                        }
                    }
                    //document.Add(tblsupplier);
                }

                else if (rdprod.Checked == true)
                {
                    tblstock.AddCell(PhraseCell(new Phrase("Stock Report on Product\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                    cell.Colspan = 2;
                    cell.PaddingBottom = 30f;
                    tblstock.AddCell(cell);

                    //tblsupplier.AddCell(PhraseCell(new Phrase("Supplier Code:" + txtSuppCode.Text, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                    tblsupplier.AddCell(PhraseCell(new Phrase("Product Name:" + TextBox6.Text, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                    cell.Colspan = 2;
                    cell.PaddingBottom = 30f;
                    tblsupplier.AddCell(cell);
                    tblsupplier.SpacingAfter = 5f;

                    GridCell = new PdfPCell(new Phrase(new Chunk("SlNo", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table6.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("SupplierName", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table6.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Stock", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table6.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Inv No.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table6.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Inv Date", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table6.AddCell(GridCell);

                    //GridCell = new PdfPCell(new Phrase(new Chunk("", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    //  GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //  table1.AddCell(GridCell);
                    GridCell = new PdfPCell(new Phrase(new Chunk("PP(Rs.)", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table6.AddCell(GridCell);
                    table6.SpacingAfter = 15f;

                    SqlConnection con6 = new SqlConnection(strconn11);
                    SqlCommand cmd6 = new SqlCommand("Select b.SupplierName ,SUM(a.Stockinhand) as Stockinhand,a.Invoiceno,a.Invoicedate, SUM(a.Purchaseprice) as pp from tblProductinward a left join tblsuppliermaster b on a.SuppplierCode = b.SupplierCode  where a.Stockinhand >'0' and a.ProductName = '" + TextBox6.Text + "' and a.Productcode = '" + TextBox5.Text + "' group by b.SupplierName, a.Purchaseprice,a.Batchid,a.Invoiceno,a.Invoicedate ORDER BY b.SupplierName ASC", con6);
                    SqlDataAdapter da6 = new SqlDataAdapter(cmd6);
                    DataSet ds6 = new DataSet();
                    da6.Fill(ds6);

                    int no = 0;

                    if (ds6.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds6.Tables[0].Rows.Count; j++)
                        {
                            no++;
                            GridCell = new PdfPCell(new Phrase(new Chunk(no.ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                            GridCell.HorizontalAlignment = 0;
                            GridCell.PaddingBottom = 5f;
                            table6.AddCell(GridCell);
                            for (int row1 = 0; row1 < ds6.Tables[0].Columns.Count; row1++)
                            {
                                if (row1 == 3)
                                {
                                    DateTime inv = Convert.ToDateTime(ds6.Tables[0].Rows[j][row1].ToString());
                                    string invdate = inv.ToString("dd-MM-yyyy");
                                    GridCell = new PdfPCell(new Phrase(new Chunk(invdate, FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = 0;
                                    GridCell.PaddingBottom = 5f;
                                    table6.AddCell(GridCell);
                                }

                                else if (row1 == 4)
                                {
                                    decimal tsh = Convert.ToDecimal(ds6.Tables[0].Rows[j][1].ToString());
                                    decimal pp = Convert.ToDecimal(ds6.Tables[0].Rows[j][row1].ToString());
                                    decimal total = (tsh * pp);
                                    sum = total + sum;
                                    string product = total.ToString("F");
                                    //string product = Convert.ToString(total);
                                    GridCell = new PdfPCell(new Phrase(new Chunk(product, FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    GridCell.PaddingBottom = 5f;
                                    table6.AddCell(GridCell);
                                }
                                else
                                {
                                    GridCell = new PdfPCell(new Phrase(new Chunk(ds6.Tables[0].Rows[j][row1].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = 0;
                                    GridCell.PaddingBottom = 5f;
                                    table6.AddCell(GridCell);
                                }
                            }
                        }
                    }

                }


                else if (chkexpiry.Checked == true)
                {
                    tblstock.AddCell(PhraseCell(new Phrase("Stock Report on Expiry Date\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                    cell.Colspan = 2;
                    cell.PaddingBottom = 30f;
                    tblstock.AddCell(cell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("SlNo", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("ProductName", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("TSH", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("SupplierName", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Inv No.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Inv Date", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Batch Id.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Exp Date", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("PP(Rs.)", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);
                    table1.SpacingAfter = 15f;

                    SqlConnection con1 = new SqlConnection(strconn11);

                    //  SqlCommand cmd1 = new SqlCommand("Select ProductName,Stockinhand,Batchid,Expiredate,Purchaseprice,MRP from tblProductinward where Stockinhand >0  ORDER BY ProductName ASC; ", con1);
                    //SqlCommand cmd1 = new SqlCommand("Select a.ProductName,a.Stockinhand,b.SupplierName,a.Batchid,a.Expiredate,a.Purchaseprice from tblProductinward a inner join tblsuppliermaster b on a.SuppplierCode=b.SupplierCode where a.Stockinhand >'0' ORDER BY a.ProductName ASC", con1);
                    DateTime expdate = Convert.ToDateTime(txtdate.Text);
                    string date1 = expdate.ToString("yyyy-MM-dd");
                    SqlCommand cmd1 = new SqlCommand("Select a.ProductName,SUM(a.Stockinhand) as Stockinhand,b.SupplierName,a.Invoiceno,a.Invoicedate,a.Batchid,a.Expiredate, SUM(a.Purchaseprice) as pp from tblProductinward a left join tblsuppliermaster b on a.SuppplierCode = b.SupplierCode  where a.Stockinhand >'0' and a.Expiredate != '1900-01-01 00:00:00.000'and a.Expiredate <= '" + date1 + "'   group by a.ProductName,a.Purchaseprice,b.SupplierName,a.Invoiceno,a.Invoicedate,a.Batchid,a.Expiredate,a.Purchaseprice ORDER BY a.ProductName ASC", con1);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);
                    int no = 0;

                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                        {
                            no++;
                            GridCell = new PdfPCell(new Phrase(new Chunk(no.ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                            GridCell.HorizontalAlignment = 0;
                            GridCell.PaddingBottom = 5f;
                            table1.AddCell(GridCell);
                            for (int row1 = 0; row1 < ds1.Tables[0].Columns.Count; row1++)
                            {
                                if (row1 == 4)
                                {
                                    DateTime inv = Convert.ToDateTime(ds1.Tables[0].Rows[j][row1].ToString());
                                    string invdate = inv.ToString("dd-MM-yyyy");
                                    GridCell = new PdfPCell(new Phrase(new Chunk(invdate, FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = 0;
                                    GridCell.PaddingBottom = 5f;
                                    table1.AddCell(GridCell);
                                }
                                else if (row1 == 6)
                                {

                                    DateTime expire1 = Convert.ToDateTime(ds1.Tables[0].Rows[j][row1].ToString());
                                    string expire2 = expire1.ToString("dd-MM-yyyy");
                                    /* if (expire2 == "01-01-1900")
                                     {

                                     }
                                     else
                                     {*/
                                    GridCell = new PdfPCell(new Phrase(new Chunk(expire2, FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = 0;
                                    GridCell.PaddingBottom = 5f;
                                    table1.AddCell(GridCell);


                                }
                                else if (row1 == 7)
                                {
                                    decimal tsh = Convert.ToDecimal(ds1.Tables[0].Rows[j][1].ToString());
                                    decimal pp = Convert.ToDecimal(ds1.Tables[0].Rows[j][row1].ToString());
                                    decimal total = (tsh * pp);
                                    sum = total + sum;
                                    string product = total.ToString("F");
                                   // string product = Convert.ToString(total);
                                    GridCell = new PdfPCell(new Phrase(new Chunk(product, FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    GridCell.PaddingBottom = 5f;
                                    table1.AddCell(GridCell);
                                }
                                else
                                {
                                    GridCell = new PdfPCell(new Phrase(new Chunk(ds1.Tables[0].Rows[j][row1].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = 0;
                                    GridCell.PaddingBottom = 5f;
                                    table1.AddCell(GridCell);
                                }
                            }
                        }
                    }
                }
                // }               
                /* else
                {
                    Master.ShowModal("There is No Expire Medicine", "txtdate", 1);
                    return;
                }*/
                //if(chkexpiry.Checked == false)
                else if (rdtotal.Checked == true)
                {
                    tblstock.AddCell(PhraseCell(new Phrase("Stock Report\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                    cell.Colspan = 2;
                    cell.PaddingBottom = 30f;
                    tblstock.AddCell(cell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("SlNo", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    // GridCell.Width = 1f;
                    table2.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("ProductName", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //GridCell.Width = 1f;
                    table2.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("TSH", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    // GridCell.Width = 1f;
                    table2.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("SupplierName", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table2.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Inv No.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table2.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Inv Date", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table2.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("PP(Rs.)", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table2.AddCell(GridCell);
                    table2.SpacingAfter = 15f;

                    SqlConnection con = new SqlConnection(strconn11);
                    // SqlCommand cmd = new SqlCommand("Select a.ProductName,a.Stockinhand,MRP from tblProductinward where Stockinhand >0  ORDER BY ProductName ASC;", con);
                    SqlCommand cmd = new SqlCommand("Select a.ProductName ,SUM(a.Stockinhand) as Stockinhand,b.SupplierName,a.Invoiceno,a.Invoicedate, SUM(a.Purchaseprice) as pp from tblProductinward a left join tblsuppliermaster b on a.SuppplierCode = b.SupplierCode  where a.Stockinhand >'0'group by a.ProductName,a.Purchaseprice,a.Batchid,b.SupplierName,a.Invoiceno,a.Invoicedate ORDER BY a.ProductName ASC", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    int slno = 0;
                    //int tsh = 0;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            slno++;
                            GridCell = new PdfPCell(new Phrase(new Chunk(slno.ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                            GridCell.HorizontalAlignment = 0;
                            GridCell.PaddingBottom = 5f;
                            table2.AddCell(GridCell);


                            for (int row = 0; row < ds.Tables[0].Columns.Count; row++)
                            {
                                if (row == 4)
                                {
                                    DateTime expire1 = Convert.ToDateTime(ds.Tables[0].Rows[i][row].ToString());
                                    string expire2 = expire1.ToString("dd-MM-yyyy");
                                    GridCell = new PdfPCell(new Phrase(new Chunk(expire2, FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = 0;
                                    GridCell.PaddingBottom = 5f;
                                    table2.AddCell(GridCell);
                                }
                                else if (row == 5)
                                {
                                    decimal tsh = Convert.ToDecimal(ds.Tables[0].Rows[i][1].ToString());
                                    decimal pp = Convert.ToDecimal(ds.Tables[0].Rows[i][row].ToString());
                                    decimal total = (tsh * pp);
                                    sum = total + sum;
                                   // string product = Convert.ToString(total);
                                    string product = total.ToString("F");
                                    GridCell = new PdfPCell(new Phrase(new Chunk(product, FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    GridCell.PaddingBottom = 5f;
                                    table2.AddCell(GridCell);
                                }

                                else
                                {

                                    GridCell = new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[i][row].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = 0;
                                    GridCell.PaddingBottom = 5f;
                                    table2.AddCell(GridCell);
                                }

                            }
                        }

                    }

                }
                phrase = new Phrase();
                phrase.Add(new Chunk(oALHospDetails[0].ToString() + "\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)));
                phrase.Add(new Chunk(oALHospDetails[1].ToString() + "\n" + oALHospDetails[2].ToString() + "\n" + oALHospDetails[3].ToString() + "\n\n", FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.HorizontalAlignment = 0;
                table3.AddCell(cell);

                DataSet dslogin = ClsBLGD.GetcondDataSet("*", "tblLogin", "username", Session["username"].ToString());
                table4.AddCell(PhraseCell(new Phrase("\n\n" + "Generated On:" + sqlFormattedDate, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                table4.AddCell(PhraseCell(new Phrase("\n\n" + "Generated By:" + "(" + dslogin.Tables[0].Rows[0]["UserName"].ToString() + ")", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 4;
                cell.PaddingBottom = 30f;
                table4.AddCell(cell);

                tblsum.AddCell(PhraseCell(new Phrase("Total:" + sum, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_RIGHT);
                cell.Colspan = 4;
                cell.PaddingBottom = 30f;
                tblsum.AddCell(cell);

                /*tbltotal.AddCell(PhraseCell(new Phrase("Total:" + totalvalue, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_RIGHT);
                cell.Colspan = 4;
                cell.PaddingBottom = 30f;
                tbltotal.AddCell(cell);*/
               /* if (rdsup.Checked = true)
                {
                    document.Add(tblstock);
                    document.Add(date);
                    document.Add(table3);
                    document.Add
                    
                }*/

                document.Add(tblstock);
                document.Add(date);
                document.Add(table3);
                document.Add(tblsupplier);
                document.Add(table2);
                document.Add(table1);
                document.Add(table5);
                document.Add(table6);
                document.Add(tblsum);
               // document.Add(tbltotal);
                
                document.Add(table4);
               
                document.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=StockReport.pdf");

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
                Master.ShowModal("There is Nothing to print", "txtdate", 1);
                return;
            }

        }

        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;

        }
    }


    protected void btnexit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void rdsup_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdsup.Checked == true)
            {
                rdprod.Checked = false;
                rdtotal.Checked = false;
                chkexpiry.Checked = false;
                PanelSupp.Visible = true;
                Panel1.Visible = false;               
                 
                 txtSuppCode.Focus();
        

            }
        }
        catch(Exception ex)
        {
            string msg = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = msg;

        }
    }
    protected void rdprod_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdprod.Checked == true)
            {
                rdsup.Checked = false;
                rdtotal.Checked = false;
                PanelSupp.Visible = false;
                Panel1.Visible = true;
                chkexpiry.Checked = false;
                TextBox5.Focus();

            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = msg;

        }

    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Suppliercode (string prefixText)
    {
        SqlConnection con7 = new SqlConnection(strconn11);
        con7.Open();
        SqlCommand cmd7 = new SqlCommand("Select SupplierCode from tblsuppliermaster where SupplierCode like @1+'%'", con7);
        cmd7.Parameters.AddWithValue("@1", prefixText);
        SqlDataAdapter da = new SqlDataAdapter(cmd7);
        DataTable dt = new DataTable();
        da.Fill(dt);
        List<string> Suppliercode = new List<string>();
        for(int i = 0;i<dt.Rows.Count;i++)
        {
            Suppliercode.Add(dt.Rows[i][0].ToString());
        }

        return Suppliercode;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Suppliername(string prefixText)
    {
        SqlConnection con7 = new SqlConnection(strconn11);
        con7.Open();
        SqlCommand cmd7 = new SqlCommand("Select SupplierName from tblsuppliermaster where SupplierName like @1+'%'", con7);
        cmd7.Parameters.AddWithValue("@1", prefixText);
        SqlDataAdapter da = new SqlDataAdapter(cmd7);
        DataTable dt = new DataTable();
        da.Fill(dt);
        List<string> Suppliername = new List<string>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Suppliername.Add(dt.Rows[i][0].ToString());
        }

        return Suppliername;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> productcode(string prefixText)
    {
        SqlConnection con7 = new SqlConnection(strconn11);
        con7.Open();
        SqlCommand cmd7 = new SqlCommand("Select Productcode from tblProductMaster where Productcode like @1+'%'", con7);
        cmd7.Parameters.AddWithValue("@1", prefixText);
        SqlDataAdapter da = new SqlDataAdapter(cmd7);
        DataTable dt = new DataTable();
        da.Fill(dt);
        List<string> productcode = new List<string>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            productcode.Add(dt.Rows[i][0].ToString());
        }

        return productcode;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> productname(string prefixText)
    {   
        SqlConnection con7 = new SqlConnection(strconn11);
        con7.Open();
        SqlCommand cmd7 = new SqlCommand("Select Productname from tblProductMaster where Productname like @1+'%'", con7);
        cmd7.Parameters.AddWithValue("@1", prefixText);
        SqlDataAdapter da = new SqlDataAdapter(cmd7);
        DataTable dt = new DataTable();
        da.Fill(dt);
        List<string> productname = new List<string>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            productname.Add(dt.Rows[i][0].ToString());
        }

        return productname;
    }

    protected void txtSuppCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custcode = txtSuppCode.Text;
            DataSet dscode = ClsBLGD.GetcondDataSet("*", "tblsuppliermaster", "SupplierCode", custcode);
            if (dscode.Tables[0].Rows.Count > 0)
            {
                txtSuppName.Text = dscode.Tables[0].Rows[0]["SupplierName"].ToString();
                btnreport.Focus();
            }
            else
            {
                Master.ShowModal("This Supplier does not exist", "txtSuppCode", 1);
                return;
            }
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
    }
    protected void txtSuppName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custcode = txtSuppName.Text;
            DataSet dscode = ClsBLGD.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", custcode);
            if (dscode.Tables[0].Rows.Count > 0)
            {
                txtSuppName.Text = dscode.Tables[0].Rows[0]["SupplierCode"].ToString();
                btnreport.Focus();
            }
            else
            {
                Master.ShowModal("This Supplier does not exist", "txtSuppName", 1);
                return;
            }
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }

    }
    protected void TextBox5_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custcode = TextBox5.Text;
            DataSet dscode = ClsBLGD.GetcondDataSet("*", "tblProductMaster", "Productcode", custcode);
            if (dscode.Tables[0].Rows.Count > 0)
            {
                TextBox6.Text = dscode.Tables[0].Rows[0]["Productname"].ToString();
                btnreport.Focus();
            }
            else
            {
                Master.ShowModal("This Product does not exist", "TextBox5", 1);
                return;                                                                                                                                                                                       
            }
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }

    }
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custcode = TextBox6.Text;
            DataSet dscode = ClsBLGD.GetcondDataSet("*", "tblProductMaster", "Productname", custcode);
            if (dscode.Tables[0].Rows.Count > 0)
            {
                TextBox5.Text = dscode.Tables[0].Rows[0]["Productcode"].ToString();
                btnreport.Focus();
            }
            else
            {
                Master.ShowModal("This Product does not exist", "TextBox6", 1);
                return;
            }
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }

    }
    protected void chkexpiry_CheckedChanged(object sender, EventArgs e)
    {
        if (chkexpiry.Checked == true)
        {
            rdprod.Checked = false;
            rdsup.Checked = false;
            rdtotal.Checked = false;
            PanelSupp.Visible = false;
            Panel1.Visible = false;
            btnreport.Focus();
        }
    }
    protected void rdtotal_CheckedChanged(object sender, EventArgs e)
    {

        if (chkexpiry.Checked == true)
        {
            rdprod.Checked = false;
            rdsup.Checked = false;
            chkexpiry.Checked = false;
            PanelSupp.Visible = false;
            Panel1.Visible = false;
            btnreport.Focus();
        }
    }
}