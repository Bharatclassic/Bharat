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
using System.Web.UI.Design;

public partial class Sales_Report : System.Web.UI.Page
{
    ClsBLLGeneraldetails clsbd = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    string sqlFormattedDate = DateTime.Now.ToString();
    PharmacyName Hosp = new PharmacyName();
    protected static string strconn11 = Dbconn.conmenthod();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PanelDay.Visible = false;
            PanelBtw.Visible = false;
            PanelCust.Visible = false;
            PanelCust1.Visible = false;
        }
        lblerror.Visible = false;
        lblsuccess.Visible = false;
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmdcheck = new SqlCommand();
            ArrayList OalHospitalDetails = new ArrayList();
            DataSet dscheck = new DataSet();
            SqlDataAdapter dacheck = new SqlDataAdapter();
            string code = "";
            OalHospitalDetails = Hosp.HospitalReturns();
            DataSet check1 = new DataSet();
            SqlConnection concheck = new SqlConnection(strconn11);
            concheck.Open();
            if (rdDay.Checked == true)
            {
                if (txtDay.Text == "")
                {
                    Master.ShowModal("Please enter a date", "txtDay", 1);
                    txtDay.Focus();
                    return;
                }
                if (ddlGrp.Text == "Select a group")
                {
                    Master.ShowModal("Please select an option for group", "ddlGrp", 1);
                    return;
                }
                DateTime trdate = Convert.ToDateTime(txtDay.Text);
                string trdate1 = trdate.ToString("yyyy-MM-dd");
                if (ddlGrp.Text != "ALL")
                {
                    check1 = clsbd.GetcondDataSet("*", "tblGroup", "g_name", ddlGrp.Text);
                    code = check1.Tables[0].Rows[0]["g_code"].ToString();
                    cmdcheck = new SqlCommand("select a.Invoiceno,b.ProductName,a.Quantity,a.Rate,a.Taxamount,a.taxrate,a.Stockinhand,a.Pro_Amount,a.Total_Pro_Amount from tblProductsale a left join tblProductMaster b on a.Productcode=b.Productcode where a.Trdate='" + trdate1 + "' and b.g_code='" + code + "' ", concheck);
                }
                else 
                {
                    cmdcheck = new SqlCommand("select a.Invoiceno,b.ProductName,a.Quantity,a.Rate,a.Taxamount,a.taxrate,a.Stockinhand,a.Pro_Amount,a.Total_Pro_Amount from tblProductsale a left join tblProductMaster b on a.Productcode=b.Productcode where a.Trdate='" + trdate1 + "' ", concheck);
                }
                
            }
            else if (rdBtw.Checked == true)
            {
                if (txtbtwDate1.Text == "")
                {
                    Master.ShowModal("Please enter From date", "txtbtwDate1", 1);
                    txtbtwDate1.Focus();
                    return;
                }
                if (txtbtwDate2.Text == "")
                {
                    Master.ShowModal("Please enter To date", "txtbtwDate2", 1);
                    txtbtwDate2.Focus();
                    return;
                }
                if (ddlGrp1.Text == "Select a group")
                {
                    Master.ShowModal("Please select an option for group", "ddlGrp1", 1);
                    return;
                }
                DateTime trdate = Convert.ToDateTime(txtbtwDate1.Text);
                string trdate1 = trdate.ToString("yyyy-MM-dd");
                DateTime trdate2 = Convert.ToDateTime(txtbtwDate2.Text);
                string trdate3 = trdate2.ToString("yyyy-MM-dd");
                if (ddlGrp1.Text == "ALL")
                {
                    cmdcheck = new SqlCommand("select a.Trdate,a.Invoiceno,b.ProductName,a.Quantity,a.Rate,a.Taxamount,a.taxrate,a.Stockinhand,a.Pro_Amount,a.Total_Pro_Amount from tblProductsale a left join tblProductMaster b on a.Productcode=b.Productcode where a.Trdate>='" + trdate1 + "' and a.Trdate<='" + trdate3 + "'", concheck);
                }
                else
                {
                    check1 = clsbd.GetcondDataSet("*", "tblGroup", "g_name", ddlGrp1.Text);
                    code = check1.Tables[0].Rows[0]["g_code"].ToString();
                    cmdcheck = new SqlCommand("select a.Trdate,a.Invoiceno,b.ProductName,a.Quantity,a.Rate,a.Taxamount,a.taxrate,a.Stockinhand,a.Pro_Amount,a.Total_Pro_Amount from tblProductsale a left join tblProductMaster b on a.Productcode=b.Productcode where a.Trdate>='" + trdate1 + "' and a.Trdate<='" + trdate3 + "' and b.g_code='" + code + "'", concheck);
                }
                }
            else if (rdCust.Checked == true)
            {
                if (txtCust1.Text == "")
                {
                    Master.ShowModal("Please enter From date", "txtCust1", 1);
                    txtCust1.Focus();
                    return;
                }
                if (txtCust2.Text == "")
                {
                    Master.ShowModal("Please enter From date", "txtCust2", 1);
                    txtCust2.Focus();
                    return;
                }
                if (chkAdvCust.Checked == false && chkCredCust.Checked == false)
                {
                    Master.ShowModal("Please select either of the CheckBoxes Advanced or Credit", "chkCredCust", 1);
                    chkCredCust.Focus();
                    return;
                }
                DateTime trdate = Convert.ToDateTime(txtCust1.Text);
                string trdate1 = trdate.ToString("yyyy-MM-dd");
                DateTime trdate2 = Convert.ToDateTime(txtCust2.Text);
                string trdate3 = trdate2.ToString("yyyy-MM-dd");
                if (chkAdvCust.Checked == true)
                {
                    cmdcheck = new SqlCommand("select a.Trdate,c.CA_name,a.Invoiceno,b.ProductName,a.Quantity,a.Rate,a.Taxamount,a.taxrate,a.Stockinhand,a.Pro_Amount,a.Total_Pro_Amount from tblProductsale a left join tblProductMaster b on a.Productcode=b.Productcode left join tblCustomer c on c.CA_code=a.CA_code where a.Trdate>='" + trdate1 + "' and a.Trdate<='" + trdate3 + "' and c.Credit_amount>'0' ", concheck);
                }
                else if (chkCredCust.Checked == true)
                {
                    cmdcheck = new SqlCommand("select a.Trdate,c.CA_name,a.Invoiceno,b.ProductName,a.Quantity,a.Rate,a.Taxamount,a.taxrate,a.Stockinhand,a.Pro_Amount,a.Total_Pro_Amount from tblProductsale a left join tblProductMaster b on a.Productcode=b.Productcode left join tblCustomer c on c.CA_code=a.CA_code where a.Trdate>='" + trdate1 + "' and a.Trdate<='" + trdate3 + "' and c.Credit_used>'0' ", concheck);
                }
            }
            else
            {
                if (txtCustCode.Text == "")
                {
                    Master.ShowModal("Please Enter a customer code", "txtCustCode", 1);
                    return;
                }
                if (txtCustName.Text == "")
                {
                    Master.ShowModal("Please Enter a customer name", "txtCustName", 1);
                    return;
                }
                string custcode=txtCustCode.Text;
                cmdcheck = new SqlCommand("select a.Trdate,a.Invoiceno,b.ProductName,a.Quantity,a.Rate,a.Taxamount,a.taxrate,a.Stockinhand,a.Pro_Amount,a.Total_Pro_Amount from tblProductsale a left join tblProductMaster b on a.Productcode=b.Productcode where a.CA_code='" + custcode + "'", concheck);
            }
            cmdcheck.ExecuteNonQuery();
            dacheck = new SqlDataAdapter(cmdcheck);
            dacheck.Fill(dscheck);
            if (dscheck.Tables[0].Rows.Count > 0)
            {
                Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                PdfWriter.GetInstance(document, Response.OutputStream);
                Document document1 = new Document();
                Font Normalfont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);

                MemoryStream memorystream = new System.IO.MemoryStream();
                PdfWriter.GetInstance(document, Response.OutputStream);
                PdfWriter writer = PdfWriter.GetInstance(document, memorystream);
                document.Open();

                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable tblsale = null;
                PdfPTable table1 = null;
                PdfPTable table2 = null;
                PdfPTable tbldt = null;
                PdfPTable table3 = null;
                PdfPTable table4 = null;

                PdfPCell GridCell = null;

                /*tblsale = new PdfPTable(1);
                tblsale.TotalWidth = 490f;
                tblsale.LockedWidth = true;
                tblsale.SetWidths(new float[] { 1f });

                table1 = new PdfPTable(8);
                table1.TotalWidth = 490f;
                table1.LockedWidth = true;
                table1.SetWidths(new float[] { 0.5f, 1.5f, 1.5f, 1f, 1f, 1f,1f,1f });

                tbldt = new PdfPTable(2);
                tbldt.TotalWidth = 490f;
                tbldt.LockedWidth = true;
                tbldt.SetWidths(new float[] { 1.4f, 1f });

                table2 = new PdfPTable(1);
                table2.TotalWidth = 490f;
                table2.LockedWidth = true;
                table2.SetWidths(new float[] { 1.4f });
                tblsale.AddCell(PhraseCell(new Phrase("Sales Report\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                cell.PaddingBottom = 30f;
                tblsale.AddCell(cell);*/
                if (rdDay.Checked == true)
                {
                    tblsale = new PdfPTable(1);
                    tblsale.TotalWidth = 490f;
                    tblsale.LockedWidth = true;
                    tblsale.SetWidths(new float[] { 1f });

                    table1 = new PdfPTable(10);
                    table1.TotalWidth = 490f;
                    table1.LockedWidth = true;
                    table1.SetWidths(new float[] { 0.25f, 0.65f, 1f, 0.4f, 0.5f, 0.35f, 0.5f,0.5f,0.5f,0.5f });

                    table3 = new PdfPTable(1);
                    table3.TotalWidth = 490f;
                    // table2.HorizontalAlignment = Element.ALIGN_LEFT;
                    table3.LockedWidth = true;
                    table3.SetWidths(new float[] { 1f });

                    table4 = new PdfPTable(2);
                    table4.TotalWidth = 490f;
                    table4.LockedWidth = true;
                    table4.SetWidths(new float[] { 1f, 1.4f });
                    
                    tbldt = new PdfPTable(2);
                    tbldt.TotalWidth = 490f;
                    tbldt.LockedWidth = true;
                    tbldt.SetWidths(new float[] { 1.4f, 1f });

                    table2 = new PdfPTable(1);
                    table2.TotalWidth = 490f;
                    table2.LockedWidth = true;
                    table2.SetWidths(new float[] { 1.4f });
                    tblsale.AddCell(PhraseCell(new Phrase("Sales Report\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                    cell.Colspan = 2;
                    cell.PaddingBottom = 30f;
                    tblsale.AddCell(cell);

                    table2 = new PdfPTable(1);
                    table2.TotalWidth = 490f;
                    table2.LockedWidth = true;
                    table2.SetWidths(new float[] { 1.4f });
                    tblsale.AddCell(PhraseCell(new Phrase("Date:" + txtDay.Text, FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                    cell.Colspan = 2;
                    cell.PaddingBottom = 30f;
                    tblsale.AddCell(cell);

                    //table2 = new PdfPTable(1);
                    //table2.TotalWidth = 490f;
                    //table2.LockedWidth = true;
                    //table2.SetWidths(new float[] { 1.4f });
                    if (ddlGrp.Text != "ALL")
                    {
                        tblsale.AddCell(PhraseCell(new Phrase("Group:" + ddlGrp.Text, FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                        cell.Colspan = 4;
                        cell.PaddingBottom = 30f;
                        tblsale.AddCell(cell);
                    }
                    GridCell = new PdfPCell(new Phrase(new Chunk("Sl No.", FontFactory.GetFont("Times", 6, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    // GridCell = new PdfPCell(new Phrase(new Chunk("Productcode", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    // GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Invoice No.", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Product Name", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Qty", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Prod Rate(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Tax @", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Tax Val", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("No. of Pieces", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Prod Amt(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //GridCell.BorderColorRight = BaseColor.BLACK;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Invc Amt(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //GridCell.BorderColorRight = BaseColor.BLACK;
                    table1.AddCell(GridCell);
                    table1.SpacingAfter = 15f;
                    SqlConnection con = new SqlConnection(strconn11);
                    con.Open();
                    DateTime trdate = Convert.ToDateTime(txtDay.Text);
                    string trdate1 = trdate.ToString("yyyy-MM-dd");
                    SqlCommand cmd = new SqlCommand();
                    if (ddlGrp.Text == "ALL")
                    {
                        cmd = new SqlCommand("select a.Invoiceno,b.ProductName,a.Quantity,a.Rate,a.Taxamount,a.taxrate,a.Stockinhand,a.Pro_Amount,a.Total_Pro_Amount from tblProductsale a left join tblProductMaster b on a.Productcode=b.Productcode where a.Trdate='" + trdate1 + "' ", con);
                    }
                    else
                    {
                        DataSet dsgrp = clsbd.GetcondDataSet("*", "tblGroup", "g_name", ddlGrp.Text);
                        string grpcode = dsgrp.Tables[0].Rows[0]["g_code"].ToString();
                        cmd=new SqlCommand("select a.Invoiceno,b.ProductName,a.Quantity,a.Rate,a.Taxamount,a.taxrate,a.Stockinhand,a.Pro_Amount,a.Total_Pro_Amount from tblProductsale a left join tblProductMaster b on a.Productcode=b.Productcode where a.Trdate='" + trdate1 + "' and b.g_code='" +grpcode+ "' ",con);
                    }
                    cmd.ExecuteNonQuery();
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    int slno = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            slno++;
                            GridCell = new PdfPCell(new Phrase(new Chunk(slno.ToString(), FontFactory.GetFont("Times", 6, Font.NORMAL, BaseColor.BLACK))));
                            GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            GridCell.VerticalAlignment = 15;
                            //GridCell.BorderColor = BaseColor.WHITE;
                            GridCell.PaddingBottom = 5f;
                            table1.AddCell(GridCell);
                            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                            {
                                if (j == 2)
                                {
                                    GridCell = new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[i][j].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                    GridCell.VerticalAlignment = 15;
                                    //GridCell.BorderColor = BaseColor.WHITE;
                                    GridCell.PaddingBottom = 5f;
                                    table1.AddCell(GridCell);
                                }
                                else 
                                {
                                    GridCell = new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[i][j].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    GridCell.VerticalAlignment = 15;
                                    //GridCell.BorderColor = BaseColor.WHITE;
                                    GridCell.PaddingBottom = 5f;
                                    table1.AddCell(GridCell);
                                }
                            }
                        }
                    }
                    else
                    {
                        Master.ShowModal("Hello..!!! There are no transactions", "txtDay", 1);
                        return;
                    }
                }
                else if (rdBtw.Checked == true)
                {
                    tblsale = new PdfPTable(1);
                    tblsale.TotalWidth = 490f;
                    tblsale.LockedWidth = true;
                    tblsale.SetWidths(new float[] { 1f });

                    table1 = new PdfPTable(11);
                    table1.TotalWidth = 490f;
                    table1.LockedWidth = true;
                    table1.SetWidths(new float[] { 0.25f, 0.65f, 0.8f,1f, 0.4f, 0.5f, 0.35f, 0.5f, 0.5f, 0.5f, 0.5f });

                    table3 = new PdfPTable(1);
                    table3.TotalWidth = 490f;
                    // table2.HorizontalAlignment = Element.ALIGN_LEFT;
                    table3.LockedWidth = true;
                    table3.SetWidths(new float[] { 1f });

                    table4 = new PdfPTable(2);
                    table4.TotalWidth = 490f;
                    table4.LockedWidth = true;
                    table4.SetWidths(new float[] { 1f, 1.4f });
                    
                    tbldt = new PdfPTable(2);
                    tbldt.TotalWidth = 490f;
                    tbldt.LockedWidth = true;
                    tbldt.SetWidths(new float[] { 1.4f, 1f });

                    table2 = new PdfPTable(1);
                    table2.TotalWidth = 490f;
                    table2.LockedWidth = true;
                    table2.SetWidths(new float[] { 1.4f });
                    tblsale.AddCell(PhraseCell(new Phrase("Sales Report\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                    cell.Colspan = 2;
                    cell.PaddingBottom = 30f;
                    tblsale.AddCell(cell);

                    if (ddlGrp1.Text != "ALL")
                    {
                        tblsale.AddCell(PhraseCell(new Phrase("Group:" + ddlGrp1.Text, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                        cell.Colspan = 2;
                        cell.PaddingBottom = 30f;
                        tblsale.AddCell(cell);
                    }
                    GridCell = new PdfPCell(new Phrase(new Chunk("Sl No.", FontFactory.GetFont("Times", 7, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Tr dates", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Invoice No.", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Product Name", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Qty", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Prod Rate(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Tax @", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Tax Val", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("No. of Pieces", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Prod Amt(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //GridCell.BorderColorRight = BaseColor.BLACK;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Invc Amt(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //GridCell.BorderColorRight = BaseColor.BLACK;
                    table1.AddCell(GridCell);
                    table1.SpacingAfter = 15f;

                    SqlConnection con = new SqlConnection(strconn11);
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    DateTime trdate = Convert.ToDateTime(txtbtwDate1.Text);
                    string trdate1 = trdate.ToString("yyyy-MM-dd");
                    DateTime trdate2 = Convert.ToDateTime(txtbtwDate2.Text);
                    string trdate3 = trdate2.ToString("yyyy-MM-dd");
                    //SqlCommand cmd = new SqlCommand("select a.Trdate,b.ProductName,b.Quantity,Rate,Taxamount,Stockinhand,Total_Amount from tblProductsale where Trdate>='" + trdate1 + "' and Trdate<='" + trdate3 + "' ", con);
                    if (ddlGrp1.Text == "ALL")
                    {
                        cmd = new SqlCommand("select a.Trdate,a.Invoiceno,b.ProductName,a.Quantity,a.Rate,a.Taxamount,a.taxrate,a.Stockinhand,a.Pro_Amount,a.Total_Pro_Amount from tblProductsale a left join tblProductMaster b on a.Productcode=b.Productcode where Trdate>='" + trdate1 + "' and Trdate<='" + trdate3 + "' ", con);
                    }
                    else
                    {
                        DataSet chkgrp = clsbd.GetcondDataSet("*", "tblGroup", "g_name", ddlGrp1.Text);
                        string grpcode=chkgrp.Tables[0].Rows[0]["g_code"].ToString();
                        cmd = new SqlCommand("select a.Trdate,a.Invoiceno,b.ProductName,a.Quantity,a.Rate,a.Taxamount,a.taxrate,a.Stockinhand,a.Pro_Amount,a.Total_Pro_Amount from tblProductsale a left join tblProductMaster b on a.Productcode=b.Productcode where Trdate>='" + trdate1 + "' and Trdate<='" + trdate3 + "' and b.g_code='" + grpcode + "' ", con);
                    }
                    cmd.ExecuteNonQuery();
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    int slno = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            slno++;
                            GridCell = new PdfPCell(new Phrase(new Chunk(slno.ToString(), FontFactory.GetFont("Times", 6, Font.NORMAL, BaseColor.BLACK))));
                            GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            GridCell.VerticalAlignment = 15;
                            //GridCell.BorderColor = BaseColor.WHITE;
                            GridCell.PaddingBottom = 5f;
                            table1.AddCell(GridCell);
                            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                            {
                                if (j == 0)
                                {
                                    DateTime transaction = Convert.ToDateTime(ds.Tables[0].Rows[i][j].ToString());
                                    string transaction1 = transaction.ToString("yyyy-MM-dd");
                                    GridCell = new PdfPCell(new Phrase(new Chunk(transaction1, FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    GridCell.VerticalAlignment = 15;
                                    //GridCell.BorderColor = BaseColor.WHITE;
                                    GridCell.PaddingBottom = 5f;
                                    table1.AddCell(GridCell);
                                }
                                else if (j == 2)
                                {
                                    GridCell = new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[i][j].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                    GridCell.VerticalAlignment = 15;
                                    //GridCell.BorderColor = BaseColor.WHITE;
                                    GridCell.PaddingBottom = 5f;
                                    table1.AddCell(GridCell);
                                }
                                else
                                {
                                    GridCell = new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[i][j].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    GridCell.VerticalAlignment = 15;
                                    //GridCell.BorderColor = BaseColor.WHITE;
                                    GridCell.PaddingBottom = 5f;
                                    table1.AddCell(GridCell);
                                }
                            }
                        }
                    }
                    else
                    {
                        Master.ShowModal("Hello..!!! There are no transactions", "txtbtwDate1", 1);
                        return;
                    }
                }
                else if(rdCust.Checked==true)
                {
                    if (txtCust1.Text == "")
                    {
                        Master.ShowModal("Please enter From date", "txtCust1", 1);
                        txtCust1.Focus();
                        return;
                    }
                    if (txtCust2.Text == "")
                    {
                        Master.ShowModal("Please enter From date", "txtCust2", 1);
                        txtCust2.Focus();
                        return;
                    }
                    if (chkAdvCust.Checked == false && chkCredCust.Checked == false)
                    {
                        Master.ShowModal("Please select either of the CheckBoxes Advanced or Credit", "chkCredCust", 1);
                        chkCredCust.Focus();
                        return;
                    }
                    if (chkCredCust.Checked == true)
                    {
                        tblsale = new PdfPTable(1);
                        tblsale.TotalWidth = 490f;
                        tblsale.LockedWidth = true;
                        tblsale.SetWidths(new float[] { 1f });

                        table1 = new PdfPTable(12);
                        table1.TotalWidth = 490f;
                        table1.LockedWidth = true;
                        table1.SetWidths(new float[] { 0.25f, 0.65f,1f, 0.8f, 1f, 0.4f, 0.5f, 0.35f, 0.5f, 0.5f, 0.5f, 0.5f });

                        table3 = new PdfPTable(1);
                        table3.TotalWidth = 490f;
                        // table2.HorizontalAlignment = Element.ALIGN_LEFT;
                        table3.LockedWidth = true;
                        table3.SetWidths(new float[] { 1f });

                        table4 = new PdfPTable(2);
                        table4.TotalWidth = 490f;
                        table4.LockedWidth = true;
                        table4.SetWidths(new float[] { 1f, 1.4f });


                        tbldt = new PdfPTable(2);
                        tbldt.TotalWidth = 490f;
                        tbldt.LockedWidth = true;
                        tbldt.SetWidths(new float[] { 1.4f, 1f });

                        table2 = new PdfPTable(1);
                        table2.TotalWidth = 490f;
                        table2.LockedWidth = true;
                        table2.SetWidths(new float[] { 1.4f });
                        tblsale.AddCell(PhraseCell(new Phrase("Sales Report\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                        cell.Colspan = 2;
                        cell.PaddingBottom = 30f;
                        tblsale.AddCell(cell);

                        table2 = new PdfPTable(1);
                        table2.TotalWidth = 490f;
                        table2.LockedWidth = true;
                        table2.SetWidths(new float[] { 1.4f });
                        tblsale.AddCell(PhraseCell(new Phrase("Credit Customers", FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                        cell.Colspan = 2;
                        cell.PaddingBottom = 30f;
                        tblsale.AddCell(cell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Sl No.", FontFactory.GetFont("Times", 7, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Tr dates", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Customer Name", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Invoice No.", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Product Name", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Qty", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Prod Rate(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Tax @", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Tax Val", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("No. of Pieces", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Prod Amt(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //GridCell.BorderColorRight = BaseColor.BLACK;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Invc Amt(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //GridCell.BorderColorRight = BaseColor.BLACK;
                        table1.AddCell(GridCell);
                        table1.SpacingAfter = 15f;

                        SqlConnection con = new SqlConnection(strconn11);
                        con.Open();

                        DateTime trdate = Convert.ToDateTime(txtCust1.Text);
                        string trdate1 = trdate.ToString("yyyy-MM-dd");
                        DateTime trdate2 = Convert.ToDateTime(txtCust2.Text);
                        string trdate3 = trdate2.ToString("yyyy-MM-dd");
                        //SqlCommand cmd = new SqlCommand("select b.CA_name,a.ProductName,a.Quantity,a.Rate,a.Taxamount,a.Stockinhand,a.Total_Amount from tblProductsale a inner join tblCustomer b on b.CA_code=a.CA_code where a.Trdate>='" + trdate1 + "' and a.Trdate<='" + trdate3 + "' and b.Credit_used>'0' ", con);
                        SqlCommand cmd = new SqlCommand("select a.Trdate,c.CA_name,a.Invoiceno,b.ProductName,a.Quantity,a.Rate,a.Taxamount,a.taxrate,a.Stockinhand,a.Pro_Amount,a.Total_Pro_Amount from tblProductsale a left join tblProductMaster b on a.Productcode=b.Productcode left join tblCustomer c on c.CA_code=a.CA_code where a.Trdate>='" + trdate1 + "' and a.Trdate<='" + trdate3 + "' and c.Credit_used>'0' ", con);
                        cmd.ExecuteNonQuery();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        int slno = 0;
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                slno++;
                                GridCell = new PdfPCell(new Phrase(new Chunk(slno.ToString(), FontFactory.GetFont("Times", 6, Font.NORMAL, BaseColor.BLACK))));
                                GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                GridCell.VerticalAlignment = 15;
                                //GridCell.BorderColor = BaseColor.WHITE;
                                GridCell.PaddingBottom = 5f;
                                table1.AddCell(GridCell);
                                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                {
                                    if (j == 0)
                                    {
                                        DateTime transaction = Convert.ToDateTime(ds.Tables[0].Rows[i][j].ToString());
                                        string transaction1 = transaction.ToString("yyyy-MM-dd");
                                        GridCell = new PdfPCell(new Phrase(new Chunk(transaction1, FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                        GridCell.VerticalAlignment = 15;
                                        //GridCell.BorderColor = BaseColor.WHITE;
                                        GridCell.PaddingBottom = 5f;
                                        table1.AddCell(GridCell);
                                    }
                                        else if(j==1 || j==3)
                                        {
                                            GridCell = new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[i][j].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                            GridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                            GridCell.VerticalAlignment = 15;
                                            //GridCell.BorderColor = BaseColor.WHITE;
                                            GridCell.PaddingBottom = 5f;
                                            table1.AddCell(GridCell);
                                        }
                                    else
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[i][j].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                        GridCell.VerticalAlignment = 15;
                                        //GridCell.BorderColor = BaseColor.WHITE;
                                        GridCell.PaddingBottom = 5f;
                                        table1.AddCell(GridCell);
                                    }
                                    }
                            }
                        }
                        else
                        {
                            Master.ShowModal("Hello..!!! There are no transactions", "txtCust1", 1);
                            txtCust1.Focus();
                            return;
                        }
                    }
                    else if (chkAdvCust.Checked == true)
                    {
                        tblsale = new PdfPTable(1);
                        tblsale.TotalWidth = 490f;
                        tblsale.LockedWidth = true;
                        tblsale.SetWidths(new float[] { 1f });

                        table1 = new PdfPTable(12);
                        table1.TotalWidth = 490f;
                        table1.LockedWidth = true;
                        table1.SetWidths(new float[] { 0.25f, 0.65f, 1f, 0.8f, 1f, 0.4f, 0.5f, 0.35f, 0.5f, 0.5f, 0.5f, 0.5f });

                        tbldt = new PdfPTable(2);
                        tbldt.TotalWidth = 490f;
                        tbldt.LockedWidth = true;
                        tbldt.SetWidths(new float[] { 1.4f, 1f });

                        table3 = new PdfPTable(1);
                        table3.TotalWidth = 490f;
                        // table2.HorizontalAlignment = Element.ALIGN_LEFT;
                        table3.LockedWidth = true;
                        table3.SetWidths(new float[] { 1f });

                        table4 = new PdfPTable(2);
                        table4.TotalWidth = 490f;
                        table4.LockedWidth = true;
                        table4.SetWidths(new float[] { 1f, 1.4f });

                        table2 = new PdfPTable(1);
                        table2.TotalWidth = 490f;
                        table2.LockedWidth = true;
                        table2.SetWidths(new float[] { 1.4f });
                        tblsale.AddCell(PhraseCell(new Phrase("Sales Report\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                        cell.Colspan = 2;
                        cell.PaddingBottom = 30f;
                        tblsale.AddCell(cell);
                        if (txtCust1.Text == "")
                        {
                            Master.ShowModal("Please enter From date", "txtCust1", 1);
                            txtCust1.Focus();
                            return;
                        }
                        if (txtCust2.Text == "")
                        {
                            Master.ShowModal("Please enter From date", "txtCust2", 1);
                            txtCust2.Focus();
                            return;
                        }
                        table2 = new PdfPTable(1);
                        table2.TotalWidth = 490f;
                        table2.LockedWidth = true;
                        table2.SetWidths(new float[] { 1.4f });
                        tblsale.AddCell(PhraseCell(new Phrase("Advance Customers", FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                        cell.Colspan = 2;
                        cell.PaddingBottom = 30f;
                        tblsale.AddCell(cell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Sl No.", FontFactory.GetFont("Times", 7, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Tr dates", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Customer Name", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Invoice No.", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Product Name", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Qty", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Prod Rate(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Tax @", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Tax Val", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("No. of Pieces", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Prod Amt(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //GridCell.BorderColorRight = BaseColor.BLACK;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Invc Amt(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //GridCell.BorderColorRight = BaseColor.BLACK;
                        table1.AddCell(GridCell);
                        table1.SpacingAfter = 15f;

                        SqlConnection con = new SqlConnection(strconn11);
                        con.Open();

                        DateTime trdate = Convert.ToDateTime(txtCust1.Text);
                        string trdate1 = trdate.ToString("yyyy-MM-dd");
                        DateTime trdate2 = Convert.ToDateTime(txtCust2.Text);
                        string trdate3 = trdate2.ToString("yyyy-MM-dd");
                        //SqlCommand cmd = new SqlCommand("select b.CA_name,a.ProductName,a.Quantity,a.Rate,a.Taxamount,a.Stockinhand,a.Total_Amount from tblProductsale a inner join tblCustomer b on b.CA_code=a.CA_code innerjoin  where a.Trdate>='" + trdate1 + "' and a.Trdate<='" + trdate3 + "' and b.Credit_amount>'0' ", con);
                        SqlCommand cmd = new SqlCommand("select a.Trdate,c.CA_name,a.Invoiceno,b.ProductName,a.Quantity,a.Rate,a.Taxamount,a.taxrate,a.Stockinhand,a.Pro_Amount,a.Total_Pro_Amount from tblProductsale a left join tblProductMaster b on a.Productcode=b.Productcode left join tblCustomer c on c.CA_code=a.CA_code where a.Trdate>='" + trdate1 + "' and a.Trdate<='" + trdate3 + "' and c.Credit_amount>'0' ", con);
                        cmd.ExecuteNonQuery();
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        int slno = 0;
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                slno++;
                                GridCell = new PdfPCell(new Phrase(new Chunk(slno.ToString(), FontFactory.GetFont("Times", 6, Font.NORMAL, BaseColor.BLACK))));
                                GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                GridCell.VerticalAlignment = 15;
                                //GridCell.BorderColor = BaseColor.WHITE;
                                GridCell.PaddingBottom = 5f;
                                table1.AddCell(GridCell);
                                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                {
                                    if (j == 0)
                                    {
                                        DateTime transaction = Convert.ToDateTime(ds.Tables[0].Rows[i][j].ToString());
                                        string transaction1 = transaction.ToString("yyyy-MM-dd");
                                        GridCell = new PdfPCell(new Phrase(new Chunk(transaction1, FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                        GridCell.VerticalAlignment = 15;
                                        //GridCell.BorderColor = BaseColor.WHITE;
                                        GridCell.PaddingBottom = 5f;
                                        table1.AddCell(GridCell);
                                    }
                                    else if (j == 1 || j == 3)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[i][j].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                        GridCell.VerticalAlignment = 15;
                                        //GridCell.BorderColor = BaseColor.WHITE;
                                        GridCell.PaddingBottom = 5f;
                                        table1.AddCell(GridCell);
                                    }
                                    else
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[i][j].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                        GridCell.VerticalAlignment = 15;
                                        //GridCell.BorderColor = BaseColor.WHITE;
                                        GridCell.PaddingBottom = 5f;
                                        table1.AddCell(GridCell);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Master.ShowModal("Hello..!!! There are no transactions", "txtCust1", 1);
                            txtCust1.Focus();
                            return;
                        }
                    }
                }
                else if (rdCust1.Checked == true)
                {
                    string custcode = txtCustCode.Text;
                    string custname = txtCustName.Text;
                    tblsale = new PdfPTable(1);
                    tblsale.TotalWidth = 490f;
                    tblsale.LockedWidth = true;
                    tblsale.SetWidths(new float[] { 1f });

                    table1 = new PdfPTable(11);
                    table1.TotalWidth = 490f;
                    table1.LockedWidth = true;
                    table1.SetWidths(new float[] { 0.25f, 0.65f, 0.8f, 1f, 0.4f, 0.5f, 0.35f, 0.5f, 0.5f, 0.5f, 0.5f });

                    tbldt = new PdfPTable(2);
                    tbldt.TotalWidth = 490f;
                    tbldt.LockedWidth = true;
                    tbldt.SetWidths(new float[] { 1.4f, 1f });

                    table4 = new PdfPTable(2);
                    table4.TotalWidth = 490f;
                    table4.LockedWidth = true;
                    table4.SetWidths(new float[] { 1.4f, 1f });

                    table3 = new PdfPTable(1);
                    table3.TotalWidth = 490f;
                    table3.LockedWidth = true;
                    table3.SetWidths(new float[] { 1f });
                    
                    table2 = new PdfPTable(1);
                    table2.TotalWidth = 490f;
                    table2.LockedWidth = true;
                    table2.SetWidths(new float[] { 1.4f });
                    tblsale.AddCell(PhraseCell(new Phrase("Sales Report\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                    cell.Colspan = 2;
                    cell.PaddingBottom = 30f;
                    tblsale.AddCell(cell);

                    /*tbldt.AddCell(PhraseCell(new Phrase(" Customer Code:" + custcode, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                    tbldt.AddCell(PhraseCell(new Phrase("Customer Name :" + custname, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                    cell.Colspan = 2;
                    cell.PaddingBottom = 30f;
                    tbldt.AddCell(cell);
                    tbldt.SpacingAfter = 15f;*/

                    DataSet dsinfo = clsbd.GetcondDataSet("*", "tblCustomer", "CA_code", custcode);
                    double  type = Convert.ToDouble(dsinfo.Tables[0].Rows[0]["Credit_used"].ToString());
                    double type1 = Convert.ToDouble(dsinfo.Tables[0].Rows[0]["Credit_amount"].ToString());
                    string address1 = dsinfo.Tables[0].Rows[0]["Address1"].ToString();
                    string address2 = dsinfo.Tables[0].Rows[0]["Address2"].ToString();
                    string Hobli = dsinfo.Tables[0].Rows[0]["Hobli"].ToString();
                    string Taluk = dsinfo.Tables[0].Rows[0]["Taluk"].ToString();
                    string District = dsinfo.Tables[0].Rows[0]["District"].ToString();
                    string State = dsinfo.Tables[0].Rows[0]["State"].ToString();
                    if (type > 0)
                    {
                        tbldt.AddCell(PhraseCell(new Phrase(" Customer Code:" + custcode, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                        tbldt.AddCell(PhraseCell(new Phrase("Customer Name :" + custname, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                        tbldt.AddCell(PhraseCell(new Phrase("Type :" + "Credit", FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                        cell.Colspan = 2;
                        cell.PaddingBottom = 30f;
                        tbldt.AddCell(cell);
                        //tbldt.SpacingAfter = 15f;
                    }
                    else if (type1 > 0)
                    {
                        tbldt.AddCell(PhraseCell(new Phrase(" Customer Code:" + custcode, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                        tbldt.AddCell(PhraseCell(new Phrase("Customer Name :" + custname, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                        tbldt.AddCell(PhraseCell(new Phrase("Type :" + "Advance", FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                        cell.Colspan = 2;
                        cell.PaddingBottom = 30f;
                        tbldt.AddCell(cell);
                        //tbldt.SpacingAfter = 15f;
                    }
                    tbldt.AddCell(PhraseCell(new Phrase(" ADDRESS:" + address1 + " , " + address2 + " , " + Hobli + "\n" + "\t" + Taluk + "\n" + "\t" + District + "\n" + "\t" + State + "\n", FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                    // tbldt.AddCell(PhraseCell(new Phrase("Patient Name :" + pname, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                    cell.Colspan = 2;
                    cell.PaddingBottom = 30f;
                    tbldt.AddCell(cell);
                    //tbldt.SpacingAfter = 15f;



                    /*table2 = new PdfPTable(1);
                    table2.TotalWidth = 490f;
                    table2.LockedWidth = true;
                    table2.SetWidths(new float[] { 1.4f });
                    tblsale.AddCell(PhraseCell(new Phrase(txtCustCode.Text, FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                    cell.Colspan = 2;
                    cell.PaddingBottom = 30f;
                    tblsale.AddCell(cell);*/

                    GridCell = new PdfPCell(new Phrase(new Chunk("Sl No.", FontFactory.GetFont("Times", 7, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Tr dates", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    /*GridCell = new PdfPCell(new Phrase(new Chunk("Customer Name", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);*/

                    GridCell = new PdfPCell(new Phrase(new Chunk("Invoice No.", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Product Name", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Qty", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Prod Rate(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Tax @", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Tax Val", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("No. of Pieces", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Prod Amt(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //GridCell.BorderColorRight = BaseColor.BLACK;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Invc Amt(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //GridCell.BorderColorRight = BaseColor.BLACK;
                    table1.AddCell(GridCell);
                    table1.SpacingAfter = 15f;

                    SqlConnection con = new SqlConnection(strconn11);
                    con.Open();

                    //DateTime trdate = Convert.ToDateTime(txtCust1.Text);
                    //string trdate1 = trdate.ToString("yyyy-MM-dd");
                    //DateTime trdate2 = Convert.ToDateTime(txtCust2.Text);
                    //string trdate3 = trdate2.ToString("yyyy-MM-dd");
                    //SqlCommand cmd = new SqlCommand("select b.CA_name,a.ProductName,a.Quantity,a.Rate,a.Taxamount,a.Stockinhand,a.Total_Amount from tblProductsale a inner join tblCustomer b on b.CA_code=a.CA_code innerjoin  where a.Trdate>='" + trdate1 + "' and a.Trdate<='" + trdate3 + "' and b.Credit_amount>'0' ", con);
                    SqlCommand cmd = new SqlCommand("select a.Trdate,a.Invoiceno,b.ProductName,a.Quantity,a.Rate,a.Taxamount,a.taxrate,a.Stockinhand,a.Pro_Amount,a.Total_Pro_Amount from tblProductsale a left join tblProductMaster b on a.Productcode=b.Productcode where a.CA_code='" + custcode + "' ", con);
                    cmd.ExecuteNonQuery();
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    int slno = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            slno++;
                            GridCell = new PdfPCell(new Phrase(new Chunk(slno.ToString(), FontFactory.GetFont("Times", 6, Font.NORMAL, BaseColor.BLACK))));
                            GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            GridCell.VerticalAlignment = 15;
                            //GridCell.BorderColor = BaseColor.WHITE;
                            GridCell.PaddingBottom = 5f;
                            table1.AddCell(GridCell);
                            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                            {
                                if (j == 0)
                                {
                                    DateTime transaction = Convert.ToDateTime(ds.Tables[0].Rows[i][j].ToString());
                                    string transaction1 = transaction.ToString("yyyy-MM-dd");
                                    GridCell = new PdfPCell(new Phrase(new Chunk(transaction1, FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    GridCell.VerticalAlignment = 15;
                                    //GridCell.BorderColor = BaseColor.WHITE;
                                    GridCell.PaddingBottom = 5f;
                                    table1.AddCell(GridCell);
                                }
                                else if (j == 2)
                                {
                                    GridCell = new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[i][j].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                    GridCell.VerticalAlignment = 15;
                                    //GridCell.BorderColor = BaseColor.WHITE;
                                    GridCell.PaddingBottom = 5f;
                                    table1.AddCell(GridCell);
                                }
                                else
                                {
                                    GridCell = new PdfPCell(new Phrase(new Chunk(ds.Tables[0].Rows[i][j].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    GridCell.VerticalAlignment = 15;
                                    //GridCell.BorderColor = BaseColor.WHITE;
                                    GridCell.PaddingBottom = 5f;
                                    table1.AddCell(GridCell);
                                }
                            }
                        }
                    }
                    else
                    {
                        Master.ShowModal("Hello..!!! There are no transactions", "txtCustCode", 1);
                        return;
                    }
                }

                phrase = new Phrase();
                phrase.Add(new Chunk(OalHospitalDetails[0].ToString() + "\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)));
                phrase.Add(new Chunk(OalHospitalDetails[1].ToString() + "\n" + OalHospitalDetails[2].ToString() + "\n" + OalHospitalDetails[3].ToString() + "\n\n", FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                cell.HorizontalAlignment = 0;
                table3.AddCell(cell);

                DataSet dslogin = clsbd.GetcondDataSet("*", "tblLogin", "username", Session["username"].ToString());
                table4.AddCell(PhraseCell(new Phrase("\n\n" + "Generated On:" + sqlFormattedDate, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                table4.AddCell(PhraseCell(new Phrase("\n\n" + "Generated By:" + "(" + dslogin.Tables[0].Rows[0]["UserName"].ToString() + ")", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                cell.Colspan = 4;
                cell.PaddingBottom = 30f;
                table4.AddCell(cell);
                
                
                    document.Add(tblsale);
                    document.Add(tbldt);
                    document.Add(table3);
                    document.Add(table1);
                    document.Add(table4);
                    document.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=SalesReport.pdf");

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
                Master.ShowModal("Hello..!!! There are no transactions", "txtCust1", 1);
                txtDay.Text = string.Empty;
                txtbtwDate1.Text = string.Empty;
                txtbtwDate2.Text = string.Empty;
                txtCust1.Text = string.Empty;
                txtCust2.Text = string.Empty;
                txtCustCode.Text = string.Empty;
                txtCustName.Text = string.Empty;
                return;
            }
            txtDay.Text = string.Empty;
            txtbtwDate1.Text = string.Empty;
            txtbtwDate2.Text = string.Empty;
            txtCust1.Text = string.Empty;
            txtCust2.Text = string.Empty;
            txtCustCode.Text = string.Empty;
            txtCustName.Text = string.Empty;
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
        
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }
    protected void rdDay_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdDay.Checked == true)
            {
                rdCust.Checked = false;
                rdBtw.Checked = false;
                rdCust1.Checked = false;
                txtDay.Text = string.Empty;
                fillGroup();
            }
            PanelDay.Visible = true;
            PanelBtw.Visible = false;
            PanelCust1.Visible = false;
            PanelCust.Visible = false;
            txtDay.Focus();
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
    }
    protected void rdBtw_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdBtw.Checked == true)
            {
                rdCust.Checked = false;
                rdDay.Checked = false;
                rdCust1.Checked = false;
                fillGroup();
            }
            PanelBtw.Visible = true;
            PanelCust.Visible = false;
            PanelDay.Visible = false;
            PanelCust1.Visible = false;
            txtbtwDate1.Focus();
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
    }
    protected void rdCust_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdCust.Checked == true)
            {
                rdBtw.Checked = false;
                rdDay.Checked = false;
                rdCust1.Checked = false;
                PanelCust.Visible = true;
                PanelBtw.Visible = false;
                PanelDay.Visible = false;
                PanelCust1.Visible = false;
                txtCust1.Focus();
                lblsuccess.Visible = true;
                lblsuccess.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0000ff");
                lblsuccess.Text = "Please select a Credit or Advanced type";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }
            if (chkAdvCust.Checked == true)
            {
                chkCredCust.Checked = false;
            }
            if (chkCredCust.Checked == true)
            {
                chkAdvCust.Checked = false;
            }
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
    }
    protected void txtbtwDate2_TextChanged(object sender, EventArgs e)
    {
        ddlGrp1.Focus();
    }
    protected void txtCust2_TextChanged(object sender, EventArgs e)
    {
        btnsave.Focus();
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
     protected void chkCredCust_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCredCust.Checked == true)
        {
            chkAdvCust.Checked= false;
        }
        lblsuccess.Visible = false;
        txtCust1.Focus();
    }
    protected void chkAdvCust_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAdvCust.Checked == true)
        {
            chkCredCust.Checked = false;
        }
        lblsuccess.Visible = false;
        txtCust1.Focus();
    }
    protected void rdCust1_CheckedChanged(object sender, EventArgs e)
    {
        if (rdCust1.Checked == true)
        {
            //ClearTextBoxes();
            CleartextBoxes2();
            //lblerror.Visible = true;
            //lblerror.Text = "Development still in Progress will be ready by 29th Aug afternoon.";
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel()", true);
            PanelCust1.Visible = true;
            rdCust.Checked = false;
            rdBtw.Checked = false;
            rdDay.Checked = false;
            PanelDay.Visible = false;
            PanelBtw.Visible = false;
            PanelCust.Visible = false;
            txtCustCode.Focus();
        }
        if (rdCust1.Checked == false)
        {
            txtCustCode.Text = string.Empty;
            txtCustName.Text = string.Empty;
        }
    }

 
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Customername(string prefixText)
    {
        SqlConnection con = new SqlConnection(strconn11);
        con.Open();
        SqlCommand cmd = new SqlCommand("Select CA_name from tblCustomer where CA_name like @1+'%' ", con);
        cmd.Parameters.AddWithValue("@1", prefixText);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        List<string> Customername = new List<string>();
        for(int i=0;i<dt.Rows.Count;i++)
        {
            Customername.Add(dt.Rows[i][0].ToString());
        }
        return Customername;
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Customercode(string prefixText)
    {
        string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            //string oConn = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(strconn11);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select CA_code from tblCustomer where CA_code like @1+'%'", conn);
            cmd.Parameters.AddWithValue("@1", prefixText);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<string> Customercode = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Customercode.Add(dt.Rows[i][0].ToString());
            }
            return Customercode;
        }
        else
        {
            string strconn11 = Dbconn.conmenthod();
            OleDbConnection conn = new OleDbConnection(strconn11);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select CA_code from tblCustomer where CA_code like @1+'%'", conn);
            cmd.Parameters.AddWithValue("@1", prefixText);
            OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            List<string> Customercode = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Customercode.Add(dt.Rows[i][0].ToString());
            }

            return Customercode;
        }
    }
    protected void txtCustCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custcode = txtCustCode.Text;
            DataSet dscode = clsbd.GetcondDataSet("*", "tblCustomer", "CA_code", custcode);
            if (dscode.Tables[0].Rows.Count > 0)
            {
                txtCustName.Text = dscode.Tables[0].Rows[0]["CA_name"].ToString();
                btnsave.Focus();
            }
            else 
            {
                Master.ShowModal("This Customer does not exist", "txtCustCode", 1);
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
    protected void txtCust1_TextChanged(object sender, EventArgs e)
    {
        txtCust2.Focus();
    }
    protected void txtbtwDate1_TextChanged(object sender, EventArgs e)
    {
        txtbtwDate2.Focus();
    }
    protected void txtDay_TextChanged(object sender, EventArgs e)
    {
        ddlGrp.Focus();
    }
    public void fillGroup()
    {
        ArrayList arr1 = new ArrayList();
        ArrayList arr2 = new ArrayList();
        DataSet dsfill = clsbd.GetDataSet("Distinct g_name", "tblGroup");
        if (dsfill.Tables[0].Rows.Count > 0)
        {
            for(int i=0;i<dsfill.Tables[0].Rows.Count;i++)
            {
                arr1.Add(dsfill.Tables[0].Rows[i]["g_name"].ToString());
            }
            arr2.Sort();
            arr2.Add("Select a group");
            arr2.Add("ALL");
            for (int i = 0; i < arr1.Count; i++)
            {
                arr2.Add(arr1[i].ToString());
            }
            ddlGrp.DataSource = arr2;
            ddlGrp.DataBind();
            ddlGrp1.DataSource = arr2;
            ddlGrp1.DataBind();
        }
    }
    protected void ddlGrp1_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnsave.Focus();
    }
    protected void ddlGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnsave.Focus();
    }
    protected void txtCustName_TextChanged(object sender, EventArgs e)
    {
        string custname = txtCustName.Text;
        DataSet dscust = clsbd.GetcondDataSet("*", "tblCustomer", "CA_name", custname);
        if (dscust.Tables[0].Rows.Count > 0)
        {
            txtCustCode.Text = dscust.Tables[0].Rows[0]["CA_code"].ToString();
        }
        else
        {
            Master.ShowModal("Not a relevant customer", "txtCustName", 1);
            return;
        }
    }
    public void CleartextBoxes2()
    {
        foreach (var control in this.Controls)
        {
            TextBox tb = control as TextBox;
            if (tb != null)
            {
                tb.Text = string.Empty;
            }
        }

    }
    
}