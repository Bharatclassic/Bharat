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

public partial class DayBook : System.Web.UI.Page
{
    protected static string strconn11 = Dbconn.conmenthod();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    string sqlFormattedDate = DateTime.Now.ToString();
    ArrayList arryno = new ArrayList();
    PharmacyName Hosp = new PharmacyName();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Visible = false;
        lblsuccess.Visible = false;
        btnexit.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtdate.ClientID + "').focus();return false;}} else {return true}; ");       
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
    protected void btnrpt_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime date5 = Convert.ToDateTime(txtdate.Text);
            string date6 = date5.ToString("yyyy-MM-dd");
            DataSet check = ClsBLGD.GetcondDataSet("*", "tbltransaction", "Trdate", date6);
            if (check.Tables[0].Rows.Count > 0)
            {
                Document document = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 10f);
                PdfWriter.GetInstance(document, Response.OutputStream);
                Document document1 = new Document();
                Font Normalfont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);

                MemoryStream memorystream = new System.IO.MemoryStream();
                PdfWriter.GetInstance(document, Response.OutputStream);
                PdfWriter writer = PdfWriter.GetInstance(document, memorystream);
                ArrayList oALHospDetails = Hosp.HospitalReturns();

                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable tblpharmacyname = null;
                PdfPTable tblheading = null;
                PdfPTable tblsubheading = null;
                PdfPTable tblcreditdetails = null;
                PdfPTable tbldebitdetails = null;
                PdfPTable tblcollection = null;
                PdfPTable tbltotalcredit = null;
                PdfPTable tbltotaldebit = null;
                PdfPTable tblTotalDetails = null;
                PdfPTable tblopeningbalance = null;
                PdfPTable tbllogin = null;
                PdfPTable tbldate = null;
                PdfPCell GridCell = null;

                document.Open();

                tblheading = new PdfPTable(1);
                tblheading.TotalWidth = 800f;
                tblheading.LockedWidth = true;
                tblheading.SetWidths(new float[] { 1f });

                tbldate = new PdfPTable(1);
                tbldate.TotalWidth = 800f;
                tbldate.LockedWidth = true;
                tbldate.SetWidths(new float[] { 1f });
               

                tblsubheading = new PdfPTable(2);
                tblsubheading.TotalWidth = 800f;
                tblsubheading.LockedWidth = true;
                tblsubheading.SetWidths(new float[] { 1f, 1f });

                tblcreditdetails = new PdfPTable(4);
                tblcreditdetails.TotalWidth = 400f;
                tblcreditdetails.LockedWidth = true;
                tblcreditdetails.SetWidths(new float[] { 1f, 3f, 1f, 1f });

                tbldebitdetails = new PdfPTable(4);
                tbldebitdetails.TotalWidth = 400f;
                tbldebitdetails.LockedWidth = true;
                tbldebitdetails.SetWidths(new float[] { 1f, 3f, 1f, 1f });

                tbltotalcredit = new PdfPTable(4);
                tbltotalcredit.TotalWidth = 400f;
                tbltotalcredit.LockedWidth = true;
                tbltotalcredit.SetWidths(new float[] { 1f, 3f, 1f, 1f });

                tbllogin = new PdfPTable(2);
                tbllogin.TotalWidth = 800f;
                tbllogin.LockedWidth = true;
                tbllogin.SetWidths(new float[] { 1f, 1f });


                tbltotaldebit = new PdfPTable(4);
                tbltotaldebit.TotalWidth = 400f;
                tbltotaldebit.LockedWidth = true;
                tbltotaldebit.SetWidths(new float[] { 1f, 3f, 1f, 1f });

                tblcollection = new PdfPTable(2);
                tblcollection.TotalWidth = 800f;
                tblcollection.LockedWidth = true;
                tblcollection.SetWidths(new float[] { 1f, 1f });

                tblpharmacyname = new PdfPTable(1);
                tblpharmacyname.TotalWidth = 800f;
                tblpharmacyname.LockedWidth = true;
                tblpharmacyname.SetWidths(new float[] { 1f});

                tblTotalDetails = new PdfPTable(2);
                tblTotalDetails.LockedWidth = true;
                tblTotalDetails.TotalWidth = 800f;
                tblTotalDetails.SetWidths(new float[] { 1f, 1f });

                tblopeningbalance = new PdfPTable(2);
                tblopeningbalance.LockedWidth = true;
                tblopeningbalance.TotalWidth = 800f;
                tblopeningbalance.SetWidths(new float[] { 1f, 1f });


                GridCell = new PdfPCell(new Phrase(new Chunk("Credit", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                tblsubheading.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Debit", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tblsubheading.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Receipt No.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                tblcreditdetails.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Particulars", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_MIDDLE;
                tblcreditdetails.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Cash", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                tblcreditdetails.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Adjustment", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                tblcreditdetails.AddCell(GridCell);
                tblcreditdetails.SpacingAfter = 15f;

                DateTime date01 = Convert.ToDateTime(txtdate.Text);
                string date02 = date01.ToString("yyyy-MM-dd");
                DataSet cacode = ClsBLGD.GetcondDataSet("*", "tbltransaction", "Trdate", date02);
                for (int k = 0; k < cacode.Tables[0].Rows.Count; k++)
                {
                    //for (int m = 0; m < cacode.Tables[0].Columns.Count; m++)
                    {

                        string ca_code = cacode.Tables[0].Rows[k]["Customercode"].ToString();
                        string sup_code = cacode.Tables[0].Rows[k]["Suppliercode"].ToString();
                        string trans = cacode.Tables[0].Rows[k]["SNo"].ToString();
                       // int trans = Convert.ToInt16(cacode.Tables[0].Rows[k]["SNo"].ToString());
                        if (ca_code == "0000" && sup_code == "0000")
                        {
                            DateTime day = Convert.ToDateTime(txtdate.Text);
                            string day1 = day.ToString("yyyy-MM-dd");
                            SqlConnection con10 = new SqlConnection(strconn11);
                            SqlCommand cmd10 = new SqlCommand("Select a.Voureptno,b.Subhead,a.Cash_Credit,a.Adj_Card from tbltransaction a inner join tblVoachermaster b on a.Accounthead = b.Headercode where Trdate ='" + day1 + "' and SNo = '" + trans + "' and (a.Cash_Credit>0 or a.Adj_Card > 0)", con10);
                            //SqlCommand cmd = new SqlCommand("Select * from tblProductsale where Trdate ='" + date1 + "'", con);
                            SqlDataAdapter da10 = new SqlDataAdapter(cmd10);
                            DataSet ds10 = new DataSet();
                            da10.Fill(ds10);
                            for (int i = 0; i < ds10.Tables[0].Rows.Count; i++)
                            {
                                for (int j = 0; j < ds10.Tables[0].Columns.Count; j++)
                                {
                                    if (j == 2 || j == 3)
                                    {
                                        double credit2 = Convert.ToDouble(ds10.Tables[0].Rows[i][j].ToString());
                                       // string credit1 = Convert.ToString(Math.Round(credit2,2));
                                        string credit1 = credit2.ToString("F");
                                        GridCell = new PdfPCell(new Phrase(new Chunk(credit1, FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                        GridCell.PaddingBottom = 5f;                                        
                                        tblcreditdetails.AddCell(GridCell);
                                        tblcreditdetails.SpacingAfter = 15f;
                                    }

                                    if (j == 1)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ds10.Tables[0].Rows[i][j].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment =0;
                                        GridCell.PaddingBottom = 5f;
                                        tblcreditdetails.AddCell(GridCell);
                                    }

                                   
                                    if (j == 0)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ClsBLGD.base64Decode(ds10.Tables[0].Rows[i][j].ToString()), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = 0;
                                        GridCell.PaddingBottom = 5f;
                                        tblcreditdetails.AddCell(GridCell);
                                    }
                                }
                             }
                            
                        }

                        else if (ca_code == "0000")
                        {
                            DateTime date = Convert.ToDateTime(txtdate.Text);
                            string date1 = date.ToString("yyyy-MM-dd");
                            SqlConnection con = new SqlConnection(strconn11);
                            SqlCommand cmd = new SqlCommand("Select a.Voureptno,c.Subhead,b.SupplierName,a.Cash_Credit,a.Adj_Card from tbltransaction a inner join tblsuppliermaster b on a.Suppliercode = b.SupplierCode inner join tblVoachermaster c on a.Accounthead = c.Headercode where a.Trdate ='" + date1 + "' and a.Customercode = '" + ca_code + "' and a.Suppliercode = '" + sup_code + "' and a.SNo= '" + trans + "'and (a.Cash_Credit>0 or a.Adj_Card > 0)", con);
                            //SqlCommand cmd = new SqlCommand("Select * from tblProductsale where Trdate ='" + date1 + "'", con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                {
                                    if (j == 3 || j == 4)
                                    {
                                        double credit2 = Convert.ToDouble(ds.Tables[0].Rows[i][j].ToString());
                                       // string credit1 = Convert.ToString(Math.Round(credit2, 2));
                                        string credit1 = credit2.ToString("F");
                                        GridCell = new PdfPCell(new Phrase(new Chunk(credit1, FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                        GridCell.PaddingBottom = 5f;
                                        tblcreditdetails.AddCell(GridCell);
                                        tblcreditdetails.SpacingAfter = 15f;
                                    }
                                    if (j == 1)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[i][j].ToString() + "\n" + ds.Tables[0].Rows[i][j + 1].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = 0;
                                        GridCell.PaddingBottom = 5f;
                                        tblcreditdetails.AddCell(GridCell);
                                    }
                                  
                                    if (j == 0)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ClsBLGD.base64Decode(ds.Tables[0].Rows[i][j].ToString()), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = 0;
                                        GridCell.PaddingBottom = 5f;
                                        tblcreditdetails.AddCell(GridCell);
                                    }
                                }
                            }
                        }
                        else if (sup_code == "0000")
                        {
                            DateTime date = Convert.ToDateTime(txtdate.Text);
                            string date1 = date.ToString("yyyy-MM-dd");
                            SqlConnection con = new SqlConnection(strconn11);
                            // SqlCommand cmd = new SqlCommand("Select Voureptno,Accounthead,Cash_Credit,Adj_Card from tbltransaction where Trdate ='" + date1 + "'", con);
                            SqlCommand cmd = new SqlCommand("Select a.Voureptno,c.Subhead,b.CA_name,a.Cash_Credit,a.Adj_Card from tbltransaction a inner join tblCustomer b on a.Customercode = b.CA_code inner join tblVoachermaster c on a.Accounthead = c.Headercode where a.Trdate ='" + date1 + "' and a.Customercode = '" + ca_code + "' and a.Suppliercode = '" + sup_code + "' and a.SNo = '" + trans + "'and (a.Cash_Credit>0 or a.Adj_Card > 0)", con);
                            //SqlCommand cmd = new SqlCommand("Select * from tblProductsale where Trdate ='" + date1 + "'", con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                {
                                    if (j == 3 || j == 4)
                                    {
                                        double credit2 = Convert.ToDouble(ds.Tables[0].Rows[i][j].ToString());
                                       // string credit1 = Convert.ToString(Math.Round(credit2, 2));
                                        string credit1 = credit2.ToString("F");
                                        GridCell = new PdfPCell(new Phrase(new Chunk(credit1, FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                        GridCell.PaddingBottom = 5f;
                                        tblcreditdetails.AddCell(GridCell);
                                        tblcreditdetails.SpacingAfter = 15f;
                                    }

                                    if (j == 1)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[i][j].ToString() + "\n" + ds.Tables[0].Rows[i][j + 1].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = 0;
                                        GridCell.PaddingBottom = 5f;
                                        tblcreditdetails.AddCell(GridCell);
                                    }

                                    if (j == 0)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ClsBLGD.base64Decode(ds.Tables[0].Rows[i][j].ToString()), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = 0;
                                        GridCell.PaddingBottom = 5f;
                                        tblcreditdetails.AddCell(GridCell);
                                    }
                                }
                            }
                        }
                        else
                        {
                            DateTime date = Convert.ToDateTime(txtdate.Text);
                            string date1 = date.ToString("yyyy-MM-dd");
                            SqlConnection con = new SqlConnection(strconn11);
                            SqlCommand cmd = new SqlCommand("Select a.Voureptno,d.Subhead,b.CA_name,c.SupplierName,a.Cash_Credit,a.Adj_Card from tbltransaction a inner join tblCustomer b on a.Customercode = b.CA_code inner join tblsuppliermaster c on a.Suppliercode = c.SupplierCode inner join tblVoachermaster d on a.Accounthead = d.Headercode where a.Trdate ='" + date1 + "' and a.Suppliercode = '" + sup_code + "' and a.Customercode = '" + ca_code + "' and a.SNo = '" + trans + "'and (a.Cash_Credit>0 or a.Adj_Card > 0)", con);
                            //SqlCommand cmd = new SqlCommand("Select * from tblProductsale where Trdate ='" + date1 + "'", con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                {
                                    if (j == 4 || j == 5)
                                    {
                                        double credit2 = Convert.ToDouble(ds.Tables[0].Rows[i][j].ToString());
                                        //string credit1 = Convert.ToString(Math.Round(credit2, 2));
                                        string credit1 = credit2.ToString("F");
                                        GridCell = new PdfPCell(new Phrase(new Chunk(credit1, FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                        GridCell.PaddingBottom = 5f;
                                        tblcreditdetails.AddCell(GridCell);
                                        tblcreditdetails.SpacingAfter = 15f;
                                    }

                                    if (j == 1)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[i][j].ToString() + "\n" + ds.Tables[0].Rows[i][j + 1].ToString() + "\n" + ds.Tables[0].Rows[i][j + 2].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = 0;
                                        GridCell.PaddingBottom = 5f;
                                        tblcreditdetails.AddCell(GridCell);
                                    }
                                   
                                    if (j == 0)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ClsBLGD.base64Decode(ds.Tables[0].Rows[i][j].ToString()), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = 0;
                                        GridCell.PaddingBottom = 5f;
                                        tblcreditdetails.AddCell(GridCell);
                                    }
                                }
                            }
                        }
                    }

                }
            

              
                GridCell = new PdfPCell(new Phrase(new Chunk("Voucher No.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                tbldebitdetails.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Particulars", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_MIDDLE;
                tbldebitdetails.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Cash", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                tbldebitdetails.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Adjustment", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                tbldebitdetails.AddCell(GridCell);
                tbldebitdetails.SpacingAfter = 15f;

                DateTime dayy = Convert.ToDateTime(txtdate.Text);
                string dayy1 = dayy.ToString("yyyy-MM-dd");
                DataSet dbcode = ClsBLGD.GetcondDataSet("*", "tbltransaction", "Trdate", dayy1);
                for (int k = 0; k < cacode.Tables[0].Rows.Count; k++)
                {
                    //for (int m = 0; m < cacode.Tables[0].Columns.Count; m++)
                    {

                        string ca_code = dbcode.Tables[0].Rows[k]["Customercode"].ToString();
                        string sup_code = dbcode.Tables[0].Rows[k]["Suppliercode"].ToString();
                        string trans = dbcode.Tables[0].Rows[k]["SNo"].ToString();
                        if (ca_code == "0000" && sup_code == "0000")
                        {
                            DateTime day = Convert.ToDateTime(txtdate.Text);
                            string day1 = day.ToString("yyyy-MM-dd");
                            SqlConnection con10 = new SqlConnection(strconn11);
                            SqlCommand cmd10 = new SqlCommand("Select a.Voureptno,b.Subhead,a.Cash_Debit,a.Adj_Debit from tbltransaction a inner join tblVoachermaster b on a.Accounthead = b.Headercode where Trdate ='" + day1 + "' and SNo = '" + trans + "' and (a.Cash_Debit>0 or a.Adj_Debit>0)", con10);
                            //SqlCommand cmd = new SqlCommand("Select * from tblProductsale where Trdate ='" + date1 + "'", con);
                            SqlDataAdapter da10 = new SqlDataAdapter(cmd10);
                            DataSet ds10 = new DataSet();
                            da10.Fill(ds10);
                            for (int i = 0; i < ds10.Tables[0].Rows.Count; i++)
                            {
                                for (int j = 0; j < ds10.Tables[0].Columns.Count; j++)
                                {
                                    if (j == 2 || j == 3)
                                    {
                                        double credit2 = Convert.ToDouble(ds10.Tables[0].Rows[i][j].ToString());
                                       // string credit1 = Convert.ToString(Math.Round(credit2, 2));
                                        string credit1 = credit2.ToString("F");
                                        GridCell = new PdfPCell(new Phrase(new Chunk(credit1, FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                        GridCell.PaddingBottom = 5f;
                                        tbldebitdetails.AddCell(GridCell);
                                        tbldebitdetails.SpacingAfter = 15f;
                                    }
                                    if (j == 1)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ds10.Tables[0].Rows[i][j].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = 0;
                                        GridCell.PaddingBottom = 5f;
                                        tbldebitdetails.AddCell(GridCell);
                                    }

                                   
                                    if (j == 0)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ClsBLGD.base64Decode(ds10.Tables[0].Rows[i][j].ToString()), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = 0;
                                        GridCell.PaddingBottom = 5f;
                                        tbldebitdetails.AddCell(GridCell);
                                    }
                                }
                            }

                        }

                        else if (ca_code == "0000")
                        {
                            DateTime date = Convert.ToDateTime(txtdate.Text);
                            string date1 = date.ToString("yyyy-MM-dd");
                            SqlConnection con = new SqlConnection(strconn11);
                            SqlCommand cmd = new SqlCommand("Select a.Voureptno,c.Subhead,b.SupplierName,a.Cash_Debit,a.Adj_Debit from tbltransaction a inner join tblsuppliermaster b on a.Suppliercode = b.SupplierCode inner join tblVoachermaster c on a.Accounthead = c.Headercode where a.Trdate ='" + date1 + "' and a.Customercode = '" + ca_code + "' and a.Suppliercode = '" + sup_code + "' and a.SNo = '" + trans + "'and (a.Cash_Debit>0 or a.Adj_Debit>0)", con);
                            //SqlCommand cmd = new SqlCommand("Select * from tblProductsale where Trdate ='" + date1 + "'", con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                {
                                    if (j == 3 || j == 4)
                                    {
                                        double credit2 = Convert.ToDouble(ds.Tables[0].Rows[i][j].ToString());
                                       // string credit1 = Convert.ToString(Math.Round(credit2, 2));
                                        string credit1 = credit2.ToString("F");
                                        GridCell = new PdfPCell(new Phrase(new Chunk(credit1, FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                        GridCell.PaddingBottom = 5f;
                                        tbldebitdetails.AddCell(GridCell);
                                        tbldebitdetails.SpacingAfter = 15f;
                                    }

                                    if (j == 1)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[i][j].ToString() + "\n" + ds.Tables[0].Rows[i][j + 1].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = 0;
                                        GridCell.PaddingBottom = 5f;
                                        tbldebitdetails.AddCell(GridCell);
                                    }
                                   
                                    if (j == 0)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ClsBLGD.base64Decode(ds.Tables[0].Rows[i][j].ToString()), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment =0;
                                        GridCell.PaddingBottom = 5f;
                                        tbldebitdetails.AddCell(GridCell);
                                    }
                                }
                            }
                        }
                        else if (sup_code == "0000")
                        {
                            DateTime date = Convert.ToDateTime(txtdate.Text);
                            string date1 = date.ToString("yyyy-MM-dd");
                            SqlConnection con = new SqlConnection(strconn11);
                            // SqlCommand cmd = new SqlCommand("Select Voureptno,Accounthead,Cash_Credit,Adj_Card from tbltransaction where Trdate ='" + date1 + "'", con);
                            SqlCommand cmd = new SqlCommand("Select a.Voureptno,c.Subhead,b.CA_name,a.Cash_Debit,a.Adj_Debit from tbltransaction a inner join tblCustomer b on a.Customercode = b.CA_code inner join tblVoachermaster c on a.Accounthead = c.Headercode where a.Trdate ='" + date1 + "' and a.Customercode = '" + ca_code + "' and a.Suppliercode = '" + sup_code + "' and a.SNo = '" + trans + "'and (a.Cash_Debit>0 or a.Adj_Debit>0) ", con);
                            //SqlCommand cmd = new SqlCommand("Select * from tblProductsale where Trdate ='" + date1 + "'", con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                {
                                    if (j == 3 || j == 4)
                                    {
                                        double credit2 = Convert.ToDouble(ds.Tables[0].Rows[i][j].ToString());
                                       // string credit1 = Convert.ToString(Math.Round(credit2, 2));
                                        string credit1 = credit2.ToString("F");
                                        GridCell = new PdfPCell(new Phrase(new Chunk(credit1, FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                        GridCell.PaddingBottom = 5f;
                                        tbldebitdetails.AddCell(GridCell);
                                        tbldebitdetails.SpacingAfter = 15f;
                                    }

                                    if (j == 1)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[i][j].ToString() + "\n" + ds.Tables[0].Rows[i][j + 1].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = 0;
                                        GridCell.PaddingBottom = 5f;
                                        tbldebitdetails.AddCell(GridCell);
                                    }
                                  
                                    if (j == 0)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ClsBLGD.base64Decode(ds.Tables[0].Rows[i][j].ToString()), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));                                     
                                        GridCell.HorizontalAlignment = 0;
                                        GridCell.PaddingBottom = 5f;
                                        tbldebitdetails.AddCell(GridCell);
                                    }
                                }
                            }
                        }
                        else
                        {
                            DateTime date = Convert.ToDateTime(txtdate.Text);
                            string date1 = date.ToString("yyyy-MM-dd");
                            SqlConnection con = new SqlConnection(strconn11);
                            SqlCommand cmd = new SqlCommand("Select a.Voureptno,d.Subhead,b.CA_name,c.SupplierName,a.Cash_Debit,a.Adj_Debit from tbltransaction a inner join tblCustomer b on a.Customercode = b.CA_code inner join tblsuppliermaster c on a.Suppliercode = c.SupplierCode inner join tblVoachermaster d on a.Accounthead = d.Headercode where a.Trdate ='" + date1 + "' and a.Suppliercode = '" + sup_code + "' and a.Customercode = '" + ca_code + "' and a.SNo = '" + trans + "'and (a.Cash_Debit>0 or a.Adj_Debit>0)", con);
                            //SqlCommand cmd = new SqlCommand("Select * from tblProductsale where Trdate ='" + date1 + "'", con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                {
                                    if (j == 4 || j == 5)
                                    {
                                        double credit2 = Convert.ToDouble(ds.Tables[0].Rows[i][j].ToString());
                                       //string credit1 = Convert.ToString(Math.Round(credit2, 2));
                                        string credit1 = credit2.ToString("F");
                                        GridCell = new PdfPCell(new Phrase(new Chunk(credit1, FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                        GridCell.PaddingBottom = 5f;
                                        tbldebitdetails.AddCell(GridCell);
                                        tbldebitdetails.SpacingAfter = 15f;
                                    }

                                    if (j == 1)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[i][j].ToString() + "\n" + ds.Tables[0].Rows[i][j + 1].ToString() + "\n" + ds.Tables[0].Rows[i][j + 2].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = 0;
                                        GridCell.PaddingBottom = 5f;
                                        tbldebitdetails.AddCell(GridCell);
                                    }
                                   
                                    if (j == 0)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ClsBLGD.base64Decode(ds.Tables[0].Rows[i][j].ToString()), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = 0;
                                        GridCell.PaddingBottom = 5f;
                                        tbldebitdetails.AddCell(GridCell);
                                    }
                                }
                            }
                        }
                    }

                }

                tblheading.AddCell(PhraseCell(new Phrase("DAY BOOK\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                cell.PaddingBottom = 30f;
                tblheading.AddCell(cell);

                DateTime value = Convert.ToDateTime(txtdate.Text);
                string datevalue = value.ToString("dd-MM-yyyy");
                tbldate.AddCell(PhraseCell(new Phrase("Date:" + datevalue, FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                cell.PaddingBottom = 30f;

                //DateTime dt = Convert.ToDateTime(txtdate.Text);
                //string dt1 = dt.ToString("yyyy-MM-dd");
                DateTime dt = Convert.ToDateTime(txtdate.Text);
                string dt1 = dt.ToString("yyyy-MM-dd");
                SqlConnection con2 = new SqlConnection(strconn11);
                SqlCommand cmd2 = new SqlCommand("select SUM(Adj_Card) as adjcredit,SUM(Adj_Debit) as adjdebit,SUM(Cash_Credit) as credit,SUM(Cash_Debit) as debit from tbltransaction where Trdate = '" + dt1 + "'", con2);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);

                double Adjcredit1 = Convert.ToDouble(ds2.Tables[0].Rows[0]["adjcredit"].ToString());
               // string Adjcredit = Convert.ToString(Math.Round(Adjcredit1, 2));
                string Adjcredit = Adjcredit1.ToString("F");
                double Adjdebit1 = Convert.ToDouble(ds2.Tables[0].Rows[0]["adjdebit"].ToString());
               // string Adjdebit = Convert.ToString(Math.Round(Adjdebit1, 2));
                string Adjdebit = Adjdebit1.ToString("F");
                double credit3 = Convert.ToDouble(ds2.Tables[0].Rows[0]["credit"].ToString());
                string credit = credit3.ToString("F");
                //string credit = Convert.ToString(Math.Round(credit3, 2));
                double debit3 = Convert.ToDouble(ds2.Tables[0].Rows[0]["debit"].ToString());
                string debit = debit3.ToString("F");
               // string debit = Convert.ToString(Math.Round(debit3, 2));

                tblTotalDetails.DefaultCell.Border = 0;
                GridCell = new PdfPCell(new Phrase(new Chunk("Total", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.Colspan = 2;
                GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbltotalcredit.AddCell(GridCell);
                GridCell = new PdfPCell(new Phrase(new Chunk(credit.ToString(), FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbltotalcredit.AddCell(GridCell);
                GridCell = new PdfPCell(new Phrase(new Chunk(Adjcredit.ToString(), FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbltotalcredit.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Total", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.Colspan = 2;
                GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbltotaldebit.AddCell(GridCell);
                GridCell = new PdfPCell(new Phrase(new Chunk(debit.ToString(), FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbltotaldebit.AddCell(GridCell);
                GridCell = new PdfPCell(new Phrase(new Chunk(Adjdebit.ToString(), FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbltotaldebit.AddCell(GridCell);

                SqlConnection con3 = new SqlConnection(strconn11);
                DateTime dt2 = Convert.ToDateTime(txtdate.Text);
               // dt2 = dt2.AddDays(-1);
                string dt3 = dt2.ToString("yyyy-MM-dd");
                SqlCommand cmd3 = new SqlCommand("Select SUM(Cash_Credit) as cashcredit,Sum(Cash_Debit) as cashdebit from tbltransaction where Trdate < '" + dt3 + "'", con3);
                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);

                if (ds3.Tables[0].Rows.Count > 0)
                {
                    double openc = 0, opend = 0, opening = 0;

                 
                    if (ds3.Tables[0].Rows[0]["cashcredit"].ToString() == "" && ds3.Tables[0].Rows[0]["cashdebit"].ToString() == "")
                    {
                        opening = 0;
                        openc = 0;
                        opend = 0;
                    }
                    else
                    {
                        
                        openc = Convert.ToDouble(ds3.Tables[0].Rows[0]["cashcredit"].ToString());
                        opend = Convert.ToDouble(ds3.Tables[0].Rows[0]["cashdebit"].ToString());
                    }

                    opening = openc - opend;
                   // Math.Round(opening, 2) 
                
                    if (opening == 0)
                    {
                        tblopeningbalance.AddCell(PhraseCell(new Phrase("\t\t\t\t\t"+"Opening Balance:" + "Rs." + 0, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));

                    }
                    else
                    {
                        tblopeningbalance.AddCell(PhraseCell(new Phrase("Opening Balance:" + "Rs." + Math.Round(opening, 2), FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));

                    }

                }
                else
                {
                    tblopeningbalance.AddCell(PhraseCell(new Phrase("Opening Balance:" + "Rs." + 0, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                }
                SqlConnection con4 = new SqlConnection(strconn11);
                SqlCommand cmd4 = new SqlCommand("Select SUM(Cash_Credit) as opcashcredit,Sum(Cash_Debit) as clcashdebit from tbltransaction where Trdate <= '" + dt3 + "'", con4);
                SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                DataSet ds4 = new DataSet();
                da4.Fill(ds4);

                
                if (ds4.Tables[0].Rows.Count > 0)
                {

                    double closec = Convert.ToDouble(ds4.Tables[0].Rows[0]["opcashcredit"].ToString());
                    double closed = Convert.ToDouble(ds4.Tables[0].Rows[0]["clcashdebit"].ToString());

                    double close = closec - closed;


                    if (close == 0)
                    {
                        tblopeningbalance.AddCell(PhraseCell(new Phrase("Closing Balance:" + "Rs." + 0, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_RIGHT);
                        cell.Colspan = 4;
                        cell.PaddingBottom = 30f;
                        tblopeningbalance.AddCell(cell);
                    }
                    else
                    {
                        tblopeningbalance.AddCell(PhraseCell(new Phrase("Closing Balance:" + "Rs." + Math.Round(close,2), FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_RIGHT);
                        cell.Colspan = 4;
                        cell.PaddingBottom = 30f;
                        tblopeningbalance.AddCell(cell);
                    }
                }
                else
                {
                    tblopeningbalance.AddCell(PhraseCell(new Phrase("Closing Balance:" + "Rs." + 0, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_RIGHT);
                     cell.Colspan = 4;
                        cell.PaddingBottom = 30f;
                        tblopeningbalance.AddCell(cell);
                    
                }
                phrase = new Phrase();
                phrase.Add(new Chunk(oALHospDetails[0].ToString() + "\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)));
                phrase.Add(new Chunk(oALHospDetails[1].ToString() + "\n" + oALHospDetails[2].ToString() + "\n" + oALHospDetails[3].ToString() + "\n\n", FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.HorizontalAlignment = 0;
                tblpharmacyname.AddCell(cell);

                DataSet dslogin = ClsBLGD.GetcondDataSet("*", "tblLogin", "username", Session["username"].ToString());
                tbllogin.AddCell(PhraseCell(new Phrase("\n\n\n\n" + "Generated On:" + sqlFormattedDate, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                tbllogin.AddCell(PhraseCell(new Phrase("\n\n\n\n" + "Generated By:" + "(" + dslogin.Tables[0].Rows[0]["UserName"].ToString() + ")", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 4;
                cell.PaddingBottom = 30f;
                tbllogin.AddCell(cell);

                

                tblcollection.AddCell(tblcreditdetails);
                tblcollection.AddCell(tbldebitdetails);

                tblTotalDetails.AddCell(tbltotalcredit);
                tblTotalDetails.AddCell(tbltotaldebit);

                     
                document.Add(tblheading);
                document.Add(tblpharmacyname);
                document.Add(tbldate);
                document.Add(tblsubheading);
                document.Add(tblcollection);
                document.Add(tblTotalDetails);
                document.Add(tblopeningbalance);
                document.Add(tbllogin);

                document.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=Daybook.pdf");

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
                 Master.ShowModal("There is no Transaction!!!!","txtdate",0);
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
}