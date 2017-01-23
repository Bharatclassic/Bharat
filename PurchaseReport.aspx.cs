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

public partial class PurchaseReport : System.Web.UI.Page
{
    ClsBLLGeneraldetails clsbd = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    String sqlFormattedDate = DateTime.Now.ToString();
    PharmacyName Hosp = new PharmacyName();
    protected static string strconn11 = Dbconn.conmenthod();
    GridView griddate = new GridView();
    ArrayList oALHospitalDetails = new ArrayList();
    DataRow dr;

    ArrayList arryno = new ArrayList();

    ArrayList arryname = new ArrayList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PanelBtw.Visible = false;
            PanelDay.Visible = false;
            PanelSupp.Visible = false;

            lblchkgrp.Visible = false;
            ddinvoiveno.Visible = false;
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }
    protected void rdDay_CheckedChanged(object sender, EventArgs e)
    {
        if (rdDay.Checked == true)
        {
            rdBtw.Checked = false;
            rdPurch.Checked = false;
            PanelDay.Visible = true;
            PanelBtw.Visible = false;
            PanelSupp.Visible = false;
            groupcode();
        }
        txtDay.Focus();
    }
    protected void rdBtw_CheckedChanged(object sender, EventArgs e)
    {
        if (rdBtw.Checked == true)
        {
            rdDay.Checked = false;
            rdPurch.Checked = false;
            PanelBtw.Visible = true;
            PanelDay.Visible = false;
            PanelSupp.Visible = false;
            groupcode();
        }
        txtbtwDate1.Focus();
    }
    protected void txtbtwDate1_TextChanged(object sender, EventArgs e)
    {
        txtbtwDate2.Focus();
    }
    protected void txtbtwDate2_TextChanged(object sender, EventArgs e)
    {
        btnsave.Focus();
        ddlgrp1.Focus();
    }
    protected void txtSuppCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string supcode = txtSuppCode.Text;
            SqlConnection con = new SqlConnection(strconn11);
            con.Open();
            DataSet ds = clsbd.GetcondDataSet("*", "tblsuppliermaster", "SupplierCode", supcode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtSuppName.Focus();
                txtSuppName.Text = ds.Tables[0].Rows[0]["SupplierName"].ToString();
            }
            else 
            {
                Master.ShowModal("Supplier with entered code does not exist", "txtSuppCode", 1);
                return;
            }
        }
        catch (Exception ex)
        {
            string error = ex.Message;
            lblerror.Visible = false;
            lblerror.Text = error;
        }
    }
    protected void txtSuppName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string supname=txtSuppName.Text;
            DataSet ds = clsbd.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", supname);
            if (ds.Tables[0].Rows.Count > 0)
            {
              txtSuppCode.Text = ds.Tables[0].Rows[0]["SupplierCode"].ToString();
              btnsave.Focus();
            }
            else
            {
                Master.ShowModal("Supplier with entered name does not exist", "txtSuppCode", 1);
                return;
            }
        }
        catch (Exception ex)
        { string a = ex.Message;
        lblerror.Visible = false;
        lblerror.Text = a;
        }
       
    }
    protected void rdPurch_CheckedChanged(object sender, EventArgs e)
    {
        if (rdPurch.Checked == true)
        {
            rdDay.Checked = false;
            rdBtw.Checked = false;
            PanelSupp.Visible = true;
            PanelDay.Visible = false;
            PanelBtw.Visible = false;
        }

        

        
       
       
        txtSuppCode.Focus();
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Suppliercode(string prefixText)
    {
        string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            //string oConn = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(strconn11);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select SupplierCode from tblsuppliermaster where SupplierCode like @1+'%'", conn);
            cmd.Parameters.AddWithValue("@1", prefixText);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<string> Suppliercode = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Suppliercode.Add(dt.Rows[i][0].ToString());
            }
            return Suppliercode;
        }
        else
        {
            string strconn11 = Dbconn.conmenthod();
            OleDbConnection conn = new OleDbConnection(strconn11);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select SupplierCode from tblsuppliermaster where SupplierCode like @1+'%'", conn);
            cmd.Parameters.AddWithValue("@1", prefixText);
            OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            List<string> Suppliercode = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Suppliercode.Add(dt.Rows[i][0].ToString());
            }

            return Suppliercode;
        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Suppliername(string prefixText)
    {
        string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            //string oConn = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(strconn11);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select SupplierName from tblsuppliermaster where SupplierName like @1+'%'", conn);
            cmd.Parameters.AddWithValue("@1", prefixText);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<string> Suppliername = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Suppliername.Add(dt.Rows[i][0].ToString());
            }
            return Suppliername;
        }
        else
        {
            string strconn11 = Dbconn.conmenthod();
            OleDbConnection conn = new OleDbConnection(strconn11);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select SupplierName from tblsuppliermaster where SupplierName like @1+'%'", conn);
            cmd.Parameters.AddWithValue("@1", prefixText);
            OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            List<string> Suppliername = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Suppliername.Add(dt.Rows[i][0].ToString());
            }

            return Suppliername;
        }
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
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            oALHospitalDetails = Hosp.HospitalReturns();
            if (rdDay.Checked == true)
            {
                if (txtDay.Text == "")
                {
                    Master.ShowModal("Please enter date", "txtday", 1);
                    return;
                }
                if (ddlGrp.Text == "Select a group")
                {
                    Master.ShowModal("Please select a group name or" + "'all'" + "option provided in the group list", "ddlGrp", 1);
                    return;
                }
                binddate();
                DataSet dschck = new DataSet();
                DateTime indate1 = Convert.ToDateTime(txtDay.Text);
            string indate = indate1.ToString("yyyy-MM-dd");
            if (ddlGrp.Text == "ALL")
            {
              dschck = clsbd.GetcondDataSet("*", "tblProductinward", "Indate", indate);
            }
            else
            {
                DataSet dsgrp = clsbd.GetcondDataSet("*", "tblGroup", "g_name", ddlGrp.Text);
                string grpcode = dsgrp.Tables[0].Rows[0]["g_code"].ToString();
                dschck = clsbd.GetcondDataSet2("*", "tblProductinward", "Indate", indate, "g_code", grpcode);
            }
                if (dschck.Tables[0].Rows.Count > 0)
            {
                Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                PdfWriter.GetInstance(document, Response.OutputStream);
                Document document1 = new Document();
                Font Normalfont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);

                MemoryStream memorystream = new System.IO.MemoryStream();
                PdfWriter.GetInstance(document, Response.OutputStream);
                PdfWriter writer = PdfWriter.GetInstance(document, memorystream);

                // PdfWriterEvents1 writerEvent = new PdfWriterEvents1(oALHospDetails[4].ToString());
                // writer.PageEvent = writerEvent;


                DataTable dtPdfcustomer = new DataTable();
                if (griddate.HeaderRow != null)
                {
                    for (int i = 0; i < griddate.HeaderRow.Cells.Count; i++)
                    {
                        dtPdfcustomer.Columns.Add(griddate.HeaderRow.Cells[i].Text);
                    }
                }
                foreach (GridViewRow row in griddate.Rows)
                {
                    DataRow datarow;
                    datarow = dtPdfcustomer.NewRow();

                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Text == "&nbsp;")
                        {
                            datarow[i] = "";
                        }
                        else { datarow[i] = row.Cells[i].Text; }
                    }
                    dtPdfcustomer.Rows.Add(datarow);
                }
                Session["dtPdfstock"] = dtPdfcustomer;
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable tblstock = null;
                PdfPTable table1 = null;
                PdfPTable table2 = null;
                PdfPTable table3 = null;
                PdfPTable table4 = null;

                PdfPTable tbldt = null;
                dtPdfcustomer = (DataTable)Session["dtPdfstock"];
                if (Session["dtPdfstock"] != null)
                {
                    table2 = new PdfPTable(dtPdfcustomer.Columns.Count);
                }
                PdfPCell GridCell = null;

                document.Open();

                tblstock = new PdfPTable(1);
                tblstock.TotalWidth = 450f;
                tblstock.LockedWidth = true;
                tblstock.SetWidths(new float[] { 1f });

                table1 = new PdfPTable(7);
                table1.TotalWidth = 450f;
                table1.LockedWidth = true;
                table1.SetWidths(new float[] { 0.5f, 1f, 1f, 1f, 0.5f, 0.5f, 0.5f });

                tbldt = new PdfPTable(2);
                tbldt.TotalWidth = 450f;
                tbldt.LockedWidth = true;
                tbldt.SetWidths(new float[] { 1.4f, 1f });

                table4= new PdfPTable(2);
                table4.TotalWidth = 450f;
                table4.LockedWidth = true;
                table4.SetWidths(new float[] { 1.4f, 1f });

                table3 = new PdfPTable(1);
                table3.TotalWidth = 450f;
                table3.LockedWidth = true;
                table3.SetWidths(new float[] { 1f });

                table2 = new PdfPTable(1);
                table2.TotalWidth = 490f;
                table2.LockedWidth = true;
                table2.SetWidths(new float[] { 1.4f });
                tblstock.AddCell(PhraseCell(new Phrase("Purchase Report\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                cell.PaddingBottom = 30f;
                tblstock.AddCell(cell);

                table2 = new PdfPTable(1);
                table2.TotalWidth = 490f;
                table2.LockedWidth = true;
                table2.SetWidths(new float[] { 1.4f });
                tblstock.AddCell(PhraseCell(new Phrase("Date:" + txtDay.Text, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                cell.PaddingBottom = 30f;
                tblstock.AddCell(cell);

                if (ddlGrp.Text == "ALL")
                {
                }
                else 
                {
                    table2 = new PdfPTable(1);
                    table2.TotalWidth = 490f;
                    table2.LockedWidth = true;
                    table2.SetWidths(new float[] { 1.4f });
                    tblstock.AddCell(PhraseCell(new Phrase("Group :" + ddlGrp.Text, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                    cell.Colspan = 2;
                    cell.PaddingBottom = 30f;
                    tblstock.AddCell(cell);
                }

                GridCell = new PdfPCell(new Phrase(new Chunk("Sl No.", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table1.AddCell(GridCell);

                // GridCell = new PdfPCell(new Phrase(new Chunk("Productcode", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                // GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                //table1.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Product Name", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table1.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Manufacturer", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table1.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Supplier", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table1.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Qty", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table1.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Tax(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table1.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Amt(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table1.AddCell(GridCell);
                table1.SpacingAfter = 15f;

                if (dtPdfcustomer != null)
                {
                    for (int i = 0; i < dtPdfcustomer.Rows.Count; i++)
                    {


                        for (int row1 = 0; row1 < dtPdfcustomer.Columns.Count; row1++)
                        {

                            if (row1 == 1 || row1 == 2 || row1 == 3)
                            {
                                GridCell = new PdfPCell(new Phrase(new Chunk(dtPdfcustomer.Rows[i][row1].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                GridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                GridCell.VerticalAlignment = 15;
                                //GridCell.BorderColor = BaseColor.WHITE;
                                GridCell.PaddingBottom = 5f;
                                table1.AddCell(GridCell);
                                //table1.SpacingAfter = 15f;
                            }
                            else
                            {
                                GridCell = new PdfPCell(new Phrase(new Chunk(dtPdfcustomer.Rows[i][row1].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                GridCell.VerticalAlignment = 15;
                                //GridCell.BorderColor = BaseColor.WHITE;
                                GridCell.PaddingBottom = 5f;
                                table1.AddCell(GridCell);
                                //table1.SpacingAfter = 15f;
                            }

                        }
                    }
                }
                phrase = new Phrase();
                phrase.Add(new Chunk(oALHospitalDetails[0].ToString() + "\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)));
                phrase.Add(new Chunk(oALHospitalDetails[1].ToString() + "\n" + oALHospitalDetails[2].ToString() + "\n" + oALHospitalDetails[3].ToString() + "\n\n", FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)));
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

                document.Add(tblstock);
                document.Add(table3);
                document.Add(table1);
                document.Add(tbldt);
                document.Add(table4);
                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=PurchaseReport.pdf");

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
                Master.ShowModal("Hello..!!! There are no transactions", "txtDay", 1);
                return;
            }
            }
            else if (rdBtw.Checked == true)
            {
                if (txtbtwDate1.Text == "")
                {
                    Master.ShowModal("Please enter from date", "txtbtwDate1", 1);
                    return;
                }
                if (txtbtwDate2.Text == "")
                {
                    Master.ShowModal("Please enter from date", "txtbtwDate2", 1);
                    return;
                }
                if (ddlgrp1.Text == "Select a group")
                {
                    Master.ShowModal("Please select a group name or all option provided in the group list", "ddlgrp1", 1);
                    return;
                }
                binddatebtw();
                DataSet dschck = new DataSet();
                DateTime indate1 = Convert.ToDateTime(txtbtwDate1.Text);
                string indate = indate1.ToString("yyyy-MM-dd");
                DateTime indate2 = Convert.ToDateTime(txtbtwDate2.Text);
                string indate3 = indate2.ToString("yyyy-MM-dd");
                SqlConnection con = new SqlConnection(strconn11);
                if (ddlgrp1.Text == "ALL")
                {
                    SqlCommand cmdchck = new SqlCommand("Select * from tblProductinward where Indate>='" + indate + "' and Indate<='" + indate3 + "' ", con);
                    SqlDataAdapter dachck = new SqlDataAdapter(cmdchck);
                    dachck.Fill(dschck);
                }
                else 
                {
                    string grpcode = "";
                    DataSet dsgrp = clsbd.GetcondDataSet("*", "tblGroup", "g_name", ddlgrp1.Text);
                    if (dsgrp.Tables[0].Rows.Count > 0)
                    {
                        grpcode = dsgrp.Tables[0].Rows[0]["g_code"].ToString();
                    }
                    else
                    {
                        grpcode="";
                    }
                    SqlCommand cmdchck = new SqlCommand("Select * from tblProductinward where Indate>='" + indate + "' and Indate<='" + indate3 + "' and g_code='" + grpcode + "' ", con);
                    SqlDataAdapter dachck = new SqlDataAdapter(cmdchck);
                    dachck.Fill(dschck);
                }
                    if (dschck.Tables[0].Rows.Count > 0)
                {
                    Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                    PdfWriter.GetInstance(document, Response.OutputStream);
                    Document document1 = new Document();
                    Font Normalfont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);

                    MemoryStream memorystream = new System.IO.MemoryStream();
                    PdfWriter.GetInstance(document, Response.OutputStream);
                    PdfWriter writer = PdfWriter.GetInstance(document, memorystream);

                    // PdfWriterEvents1 writerEvent = new PdfWriterEvents1(oALHospDetails[4].ToString());
                    // writer.PageEvent = writerEvent;


                    DataTable dtPdfcustomer = new DataTable();
                    if (griddate.HeaderRow != null)
                    {
                        for (int i = 0; i < griddate.HeaderRow.Cells.Count; i++)
                        {
                            dtPdfcustomer.Columns.Add(griddate.HeaderRow.Cells[i].Text);
                        }
                    }
                    foreach (GridViewRow row in griddate.Rows)
                    {
                        DataRow datarow;
                        datarow = dtPdfcustomer.NewRow();

                        for (int i = 0; i < row.Cells.Count; i++)
                        {
                            if (row.Cells[i].Text == "&nbsp;")
                            {
                                datarow[i] = "";
                            }
                            else
                            {
                                datarow[i] = row.Cells[i].Text;
                            }
                            }
                        dtPdfcustomer.Rows.Add(datarow);
                    }
                    Session["dtPdfstock"] = dtPdfcustomer;
                    Phrase phrase = null;
                    PdfPCell cell = null;
                    PdfPTable tblstock = null;
                    PdfPTable table1 = null;
                    PdfPTable table2 = null;
                    PdfPTable table3 = null;
                    PdfPTable table4 = null;

                    PdfPTable tbldt = null;
                    dtPdfcustomer = (DataTable)Session["dtPdfstock"];
                    if (Session["dtPdfstock"] != null)
                    {
                        table2 = new PdfPTable(dtPdfcustomer.Columns.Count);
                    }
                    PdfPCell GridCell = null;

                    document.Open();

                    tblstock = new PdfPTable(1);
                    tblstock.TotalWidth = 450f;
                    tblstock.LockedWidth = true;
                    tblstock.SetWidths(new float[] { 1f });

                    table1 = new PdfPTable(8);
                    table1.TotalWidth = 450f;
                    table1.LockedWidth = true;
                    table1.SetWidths(new float[] { 0.5f,0.8f,1f, 1f, 1f, 0.5f, 0.5f, 0.5f });

                    tbldt = new PdfPTable(2);
                    tbldt.TotalWidth = 450f;
                    tbldt.LockedWidth = true;
                    tbldt.SetWidths(new float[] { 1.4f, 1f });

                    table3 = new PdfPTable(1);
                    table3.TotalWidth = 450f;
                    table3.LockedWidth = true;
                    table3.SetWidths(new float[] { 1f });

                    table4 = new PdfPTable(2);
                    table4.TotalWidth = 450f;
                    table4.LockedWidth = true;
                    table4.SetWidths(new float[] { 1.4f,1f });

                    table2 = new PdfPTable(1);
                    table2.TotalWidth = 490f;
                    table2.LockedWidth = true;
                    table2.SetWidths(new float[] { 1.4f });
                    tblstock.AddCell(PhraseCell(new Phrase("Purchase Report\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                    cell.Colspan = 2;
                    cell.PaddingBottom = 30f;
                    tblstock.AddCell(cell);

                    if (ddlgrp1.Text == "ALL")
                    {
                    }
                    else
                    {
                        table2 = new PdfPTable(1);
                        table2.TotalWidth = 490f;
                        table2.LockedWidth = true;
                        table2.SetWidths(new float[] { 1.4f });
                        tblstock.AddCell(PhraseCell(new Phrase("Group :" + ddlgrp1.Text, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                        cell.Colspan = 2;
                        cell.PaddingBottom = 30f;
                        tblstock.AddCell(cell);
                    }

                    GridCell = new PdfPCell(new Phrase(new Chunk("Sl No.", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Indate", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Product Name", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Manufacturer", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Supplier", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Qty", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Tax(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);

                    GridCell = new PdfPCell(new Phrase(new Chunk("Amt(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(GridCell);
                    table1.SpacingAfter = 15f;

                    if (dtPdfcustomer != null)
                    {
                        for (int i = 0; i < dtPdfcustomer.Rows.Count; i++)
                        {


                            for (int row1 = 0; row1 < dtPdfcustomer.Columns.Count; row1++)
                            {

                                if (row1 == 2 || row1 == 3 || row1 == 4)
                                {
                                    GridCell = new PdfPCell(new Phrase(new Chunk(dtPdfcustomer.Rows[i][row1].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                    GridCell.VerticalAlignment = 15;
                                    //GridCell.BorderColor = BaseColor.WHITE;
                                    GridCell.PaddingBottom = 5f;
                                    table1.AddCell(GridCell);
                                }
                                else
                                {
                                    GridCell = new PdfPCell(new Phrase(new Chunk(dtPdfcustomer.Rows[i][row1].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                    GridCell.VerticalAlignment = 15;
                                    //GridCell.BorderColor = BaseColor.WHITE;
                                    GridCell.PaddingBottom = 5f;
                                    table1.AddCell(GridCell);
                                    //table1.SpacingAfter = 15f;

                                }
                            }
                        }
                    }

                    phrase = new Phrase();
                    phrase.Add(new Chunk(oALHospitalDetails[0].ToString() + "\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)));
                    phrase.Add(new Chunk(oALHospitalDetails[1].ToString() + "\n" + oALHospitalDetails[2].ToString() + "\n" + oALHospitalDetails[3].ToString() + "\n\n", FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)));
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
                    
                    document.Add(tblstock);
                    document.Add(table3);
                    document.Add(table1);
                    document.Add(tbldt);
                    document.Add(table4);
                    document.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=PurchaseReport.pdf");

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
                    Master.ShowModal("Hello..!!! There are no transactions", "txtbtwDate1", 1);
                    return;
                }
            }
            else if (rdPurch.Checked == true)
            {
                if (txtSuppCode.Text == "")
                {
                    Master.ShowModal("Please enter supplier code","txtSuppCode", 1);
                    return;
                }
                if (txtSuppName.Text == "")
                {
                    Master.ShowModal("Please enter supplier name","txtSuppName", 1);
                    return;
                }
                if (RadioButton1.Checked == true)
                {
                    bindsupp();
                }
                else
                {
                    bindsupp10();
                }

                if (RadioButton1.Checked == true)
                {


                    SqlConnection con = new SqlConnection(strconn11);
                    SqlCommand cmdchck = new SqlCommand("Select a.Indate as Indate,a.Stockinward as Stockinward,a.Taxamount as Taxamount,a.Purchaseprice as Purchaseprice,b.SupplierName as SupplierName,c.Productname as Productname,d.ManufactureName as ManufactureName from tblProductinward a left join tblsuppliermaster b on a.SuppplierCode=b.SupplierCode left join tblProductMaster c on a.Productcode=c.Productcode left join tblmanufacture d on d.ManufactureCode=a.ManufactureCode where a.SuppplierCode='" + txtSuppCode.Text + "' ", con);
                    DataSet dschck = new DataSet();
                    SqlDataAdapter dachck = new SqlDataAdapter(cmdchck);
                    dachck.Fill(dschck);
                    if (dschck.Tables[0].Rows.Count > 0)
                    {
                        Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                        PdfWriter.GetInstance(document, Response.OutputStream);
                        Document document1 = new Document();
                        Font Normalfont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);

                        MemoryStream memorystream = new System.IO.MemoryStream();
                        PdfWriter.GetInstance(document, Response.OutputStream);
                        PdfWriter writer = PdfWriter.GetInstance(document, memorystream);

                        // PdfWriterEvents1 writerEvent = new PdfWriterEvents1(oALHospDetails[4].ToString());
                        // writer.PageEvent = writerEvent;


                        DataTable dtPdfcustomer = new DataTable();
                        if (griddate.HeaderRow != null)
                        {
                            for (int i = 0; i < griddate.HeaderRow.Cells.Count; i++)
                            {
                                dtPdfcustomer.Columns.Add(griddate.HeaderRow.Cells[i].Text);
                            }
                        }
                        foreach (GridViewRow row in griddate.Rows)
                        {
                            DataRow datarow;
                            datarow = dtPdfcustomer.NewRow();

                            for (int i = 0; i < row.Cells.Count; i++)
                            {
                                if (row.Cells[i].Text == "&nbsp;")
                                {
                                    datarow[i] = "";
                                }
                                else
                                {
                                    datarow[i] = row.Cells[i].Text;
                                }
                            }
                            dtPdfcustomer.Rows.Add(datarow);
                        }
                        Session["dtPdfstock"] = dtPdfcustomer;
                        Phrase phrase = null;
                        PdfPCell cell = null;
                        PdfPTable tblstock = null;
                        PdfPTable table1 = null;
                        PdfPTable table2 = null;
                        PdfPTable table3 = null;
                        PdfPTable table4 = null;
                        PdfPTable tbldt = null;
                        dtPdfcustomer = (DataTable)Session["dtPdfstock"];
                        if (Session["dtPdfstock"] != null)
                        {
                            table2 = new PdfPTable(dtPdfcustomer.Columns.Count);
                        }
                        PdfPCell GridCell = null;

                        document.Open();

                        tblstock = new PdfPTable(1);
                        tblstock.TotalWidth = 450f;
                        tblstock.LockedWidth = true;
                        tblstock.SetWidths(new float[] { 1f });

                        table1 = new PdfPTable(8);
                        table1.TotalWidth = 450f;
                        table1.LockedWidth = true;
                        table1.SetWidths(new float[] { 0.5f, 0.8f, 1f, 1f, 1f, 0.5f, 0.5f, 0.5f });

                        table3 = new PdfPTable(1);
                        table3.TotalWidth = 450f;
                        table3.LockedWidth = true;
                        table3.SetWidths(new float[] { 1f });

                        table4 = new PdfPTable(2);
                        table4.TotalWidth = 450f;
                        table4.LockedWidth = true;
                        table4.SetWidths(new float[] { 1.4f, 1f });

                        tbldt = new PdfPTable(2);
                        tbldt.TotalWidth = 450f;
                        tbldt.LockedWidth = true;
                        tbldt.SetWidths(new float[] { 1.4f, 1f });

                        table2 = new PdfPTable(1);
                        table2.TotalWidth = 490f;
                        table2.LockedWidth = true;
                        table2.SetWidths(new float[] { 1.4f });
                        tblstock.AddCell(PhraseCell(new Phrase("Purchase Report\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                        cell.Colspan = 2;
                        cell.PaddingBottom = 30f;
                        tblstock.AddCell(cell);

                        DataSet dssupp = clsbd.GetcondDataSet("*", "tblsuppliermaster", "SupplierCode", txtSuppCode.Text);
                        string add1 = dssupp.Tables[0].Rows[0]["add1"].ToString();
                        string add2 = dssupp.Tables[0].Rows[0]["add2"].ToString();
                        string add3 = dssupp.Tables[0].Rows[0]["add3"].ToString();
                        string mobno = dssupp.Tables[0].Rows[0]["mobileNo"].ToString();
                        string mailid = dssupp.Tables[0].Rows[0]["email"].ToString();
                        string bal = dssupp.Tables[0].Rows[0]["Balamount"].ToString();

                        tbldt.AddCell(PhraseCell(new Phrase("Supplier Id:" + txtSuppCode.Text, FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                        tbldt.AddCell(PhraseCell(new Phrase("Supplier Name :" + txtSuppName.Text, FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                        cell.Colspan = 2;
                        cell.PaddingBottom = 20f;
                        tbldt.AddCell(cell);

                        tbldt.AddCell(PhraseCell(new Phrase(" Address:" + add1 + ", " + add2 + ", " + add3, FontFactory.GetFont("Times", 9, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                        tbldt.AddCell(PhraseCell(new Phrase("Mobile No :" + mobno, FontFactory.GetFont("Times", 9, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                        tbldt.AddCell(PhraseCell(new Phrase("Email Id:" + mailid, FontFactory.GetFont("Times", 9, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                        tbldt.AddCell(PhraseCell(new Phrase("Balance(Rs.) :" + bal, FontFactory.GetFont("Times", 9, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                        cell.Colspan = 2;
                        cell.PaddingBottom = 30f;
                        tbldt.AddCell(cell);
                        tbldt.SpacingAfter = 10f;

                        GridCell = new PdfPCell(new Phrase(new Chunk("Sl No.", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //GridCell.BorderColor = BaseColor.WHITE;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Indate", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //GridCell.BorderColor = BaseColor.WHITE;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Product Name", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //GridCell.BorderColor = BaseColor.WHITE;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Manufacturer", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //GridCell.BorderColor = BaseColor.WHITE;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Supplier", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        // GridCell.BorderColor = BaseColor.WHITE;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Qty", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        // GridCell.BorderColor = BaseColor.WHITE;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Tax(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Amt(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);
                        table1.SpacingAfter = 15f;

                        if (dtPdfcustomer != null)
                        {
                            for (int i = 0; i < dtPdfcustomer.Rows.Count; i++)
                            {


                                for (int row1 = 0; row1 < dtPdfcustomer.Columns.Count; row1++)
                                {

                                    if (row1 == 2 || row1 == 3 || row1 == 4)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(dtPdfcustomer.Rows[i][row1].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                        GridCell.VerticalAlignment = 15;
                                        //GridCell.BorderColor = BaseColor.WHITE;
                                        GridCell.PaddingBottom = 5f;
                                        table1.AddCell(GridCell);
                                        //table1.SpacingAfter = 15f;
                                    }
                                    else
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(dtPdfcustomer.Rows[i][row1].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                        GridCell.VerticalAlignment = 15;
                                        //GridCell.BorderColor = BaseColor.WHITE;
                                        GridCell.PaddingBottom = 5f;
                                        table1.AddCell(GridCell);
                                    }

                                }
                            }
                        }

                        phrase = new Phrase();
                        phrase.Add(new Chunk(oALHospitalDetails[0].ToString() + "\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)));
                        phrase.Add(new Chunk(oALHospitalDetails[1].ToString() + "\n" + oALHospitalDetails[2].ToString() + "\n" + oALHospitalDetails[3].ToString() + "\n\n", FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)));
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


                        document.Add(tblstock);
                        document.Add(table3);
                        document.Add(tbldt);
                        document.Add(table1);
                        document.Add(table4);
                        document.Close();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Disposition", "attachment; filename=PurchaseReport.pdf");

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
                        Master.ShowModal("Hello..!!! There are no transactions", "txtSuppCode", 1);
                        return;
                    }
                }



               /*---*/

                if (RadioButton2.Checked == true)
                {


                    SqlConnection con = new SqlConnection(strconn11);
                    SqlCommand cmdchck = new SqlCommand("Select a.Indate as Indate,a.Stockinward as Stockinward,a.Taxamount as Taxamount,a.Purchaseprice as Purchaseprice,b.SupplierName as SupplierName,c.Productname as Productname,d.ManufactureName as ManufactureName from tblProductinward a left join tblsuppliermaster b on a.SuppplierCode=b.SupplierCode left join tblProductMaster c on a.Productcode=c.Productcode left join tblmanufacture d on d.ManufactureCode=a.ManufactureCode where a.SuppplierCode='" + txtSuppCode.Text + "' and a.Invoiceno ='" + ddinvoiveno.SelectedItem.Text + "' ", con);
                    DataSet dschck = new DataSet();
                    SqlDataAdapter dachck = new SqlDataAdapter(cmdchck);
                    dachck.Fill(dschck);
                    if (dschck.Tables[0].Rows.Count > 0)
                    {
                        Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                        PdfWriter.GetInstance(document, Response.OutputStream);
                        Document document1 = new Document();
                        Font Normalfont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);

                        MemoryStream memorystream = new System.IO.MemoryStream();
                        PdfWriter.GetInstance(document, Response.OutputStream);
                        PdfWriter writer = PdfWriter.GetInstance(document, memorystream);

                        // PdfWriterEvents1 writerEvent = new PdfWriterEvents1(oALHospDetails[4].ToString());
                        // writer.PageEvent = writerEvent;


                        DataTable dtPdfcustomer = new DataTable();
                        if (griddate.HeaderRow != null)
                        {
                            for (int i = 0; i < griddate.HeaderRow.Cells.Count; i++)
                            {
                                dtPdfcustomer.Columns.Add(griddate.HeaderRow.Cells[i].Text);
                            }
                        }
                        foreach (GridViewRow row in griddate.Rows)
                        {
                            DataRow datarow;
                            datarow = dtPdfcustomer.NewRow();

                            for (int i = 0; i < row.Cells.Count; i++)
                            {
                                if (row.Cells[i].Text == "&nbsp;")
                                {
                                    datarow[i] = "";
                                }
                                else
                                {
                                    datarow[i] = row.Cells[i].Text;
                                }
                            }
                            dtPdfcustomer.Rows.Add(datarow);
                        }
                        Session["dtPdfstock"] = dtPdfcustomer;
                        Phrase phrase = null;
                        PdfPCell cell = null;
                        PdfPTable tblstock = null;
                        PdfPTable table1 = null;
                        PdfPTable table2 = null;
                        PdfPTable table3 = null;
                        PdfPTable table4 = null;
                        PdfPTable tbldt = null;
                        dtPdfcustomer = (DataTable)Session["dtPdfstock"];
                        if (Session["dtPdfstock"] != null)
                        {
                            table2 = new PdfPTable(dtPdfcustomer.Columns.Count);
                        }
                        PdfPCell GridCell = null;

                        document.Open();

                        tblstock = new PdfPTable(1);
                        tblstock.TotalWidth = 450f;
                        tblstock.LockedWidth = true;
                        tblstock.SetWidths(new float[] { 1f });

                        table1 = new PdfPTable(8);
                        table1.TotalWidth = 450f;
                        table1.LockedWidth = true;
                        table1.SetWidths(new float[] { 0.5f, 0.8f, 1f, 1f, 1f, 0.5f, 0.5f, 0.5f });

                        table3 = new PdfPTable(1);
                        table3.TotalWidth = 450f;
                        table3.LockedWidth = true;
                        table3.SetWidths(new float[] { 1f });

                        table4 = new PdfPTable(2);
                        table4.TotalWidth = 450f;
                        table4.LockedWidth = true;
                        table4.SetWidths(new float[] { 1.4f, 1f });

                        tbldt = new PdfPTable(2);
                        tbldt.TotalWidth = 450f;
                        tbldt.LockedWidth = true;
                        tbldt.SetWidths(new float[] { 1.4f, 1f });

                        table2 = new PdfPTable(1);
                        table2.TotalWidth = 490f;
                        table2.LockedWidth = true;
                        table2.SetWidths(new float[] { 1.4f });
                        tblstock.AddCell(PhraseCell(new Phrase("Purchase Report\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                        cell.Colspan = 2;
                        cell.PaddingBottom = 30f;
                        tblstock.AddCell(cell);

                        DataSet dssupp = clsbd.GetcondDataSet("*", "tblsuppliermaster", "SupplierCode", txtSuppCode.Text);
                        string add1 = dssupp.Tables[0].Rows[0]["add1"].ToString();
                        string add2 = dssupp.Tables[0].Rows[0]["add2"].ToString();
                        string add3 = dssupp.Tables[0].Rows[0]["add3"].ToString();
                        string mobno = dssupp.Tables[0].Rows[0]["mobileNo"].ToString();
                        string mailid = dssupp.Tables[0].Rows[0]["email"].ToString();
                        string bal = dssupp.Tables[0].Rows[0]["Balamount"].ToString();

                        tbldt.AddCell(PhraseCell(new Phrase("Supplier Id:" + txtSuppCode.Text, FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                        tbldt.AddCell(PhraseCell(new Phrase("Supplier Name :" + txtSuppName.Text, FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                        cell.Colspan = 2;
                        cell.PaddingBottom = 20f;
                        tbldt.AddCell(cell);

                        tbldt.AddCell(PhraseCell(new Phrase(" Address:" + add1 + ", " + add2 + ", " + add3, FontFactory.GetFont("Times", 9, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                        tbldt.AddCell(PhraseCell(new Phrase("Mobile No :" + mobno, FontFactory.GetFont("Times", 9, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                        tbldt.AddCell(PhraseCell(new Phrase("Email Id:" + mailid, FontFactory.GetFont("Times", 9, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                        tbldt.AddCell(PhraseCell(new Phrase("Balance(Rs.) :" + bal, FontFactory.GetFont("Times", 9, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                        cell.Colspan = 2;
                        cell.PaddingBottom = 30f;
                        tbldt.AddCell(cell);
                        tbldt.SpacingAfter = 10f;

                        GridCell = new PdfPCell(new Phrase(new Chunk("Sl No.", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //GridCell.BorderColor = BaseColor.WHITE;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Indate", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //GridCell.BorderColor = BaseColor.WHITE;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Product Name", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //GridCell.BorderColor = BaseColor.WHITE;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Manufacturer", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //GridCell.BorderColor = BaseColor.WHITE;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Supplier", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        // GridCell.BorderColor = BaseColor.WHITE;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Qty", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        // GridCell.BorderColor = BaseColor.WHITE;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Tax(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);

                        GridCell = new PdfPCell(new Phrase(new Chunk("Amt(Rs.)", FontFactory.GetFont("Times", 9, Font.BOLD, BaseColor.BLACK))));
                        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table1.AddCell(GridCell);
                        table1.SpacingAfter = 15f;

                        if (dtPdfcustomer != null)
                        {
                            for (int i = 0; i < dtPdfcustomer.Rows.Count; i++)
                            {


                                for (int row1 = 0; row1 < dtPdfcustomer.Columns.Count; row1++)
                                {

                                    if (row1 == 2 || row1 == 3 || row1 == 4)
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(dtPdfcustomer.Rows[i][row1].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                        GridCell.VerticalAlignment = 15;
                                        //GridCell.BorderColor = BaseColor.WHITE;
                                        GridCell.PaddingBottom = 5f;
                                        table1.AddCell(GridCell);
                                        //table1.SpacingAfter = 15f;
                                    }
                                    else
                                    {
                                        GridCell = new PdfPCell(new Phrase(new Chunk(dtPdfcustomer.Rows[i][row1].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                        GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                        GridCell.VerticalAlignment = 15;
                                        //GridCell.BorderColor = BaseColor.WHITE;
                                        GridCell.PaddingBottom = 5f;
                                        table1.AddCell(GridCell);
                                    }

                                }
                            }
                        }

                        phrase = new Phrase();
                        phrase.Add(new Chunk(oALHospitalDetails[0].ToString() + "\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)));
                        phrase.Add(new Chunk(oALHospitalDetails[1].ToString() + "\n" + oALHospitalDetails[2].ToString() + "\n" + oALHospitalDetails[3].ToString() + "\n\n", FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)));
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


                        document.Add(tblstock);
                        document.Add(table3);
                        document.Add(tbldt);
                        document.Add(table1);
                        document.Add(table4);
                        document.Close();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Disposition", "attachment; filename=PurchaseReport.pdf");

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
                        Master.ShowModal("Hello..!!! There are no transactions", "txtSuppCode", 1);
                        return;
                    }
                }

                PanelSupp.Visible = false;









            }
    }
        catch (Exception ex)
        {
            string error = ex.Message;
            //lblerror.Visible = true;
            lblerror.Text = error;
        }
    }
    public void binddate()
    {
        try
        {
            DataSet ds = new DataSet();
            DateTime indate1 = Convert.ToDateTime(txtDay.Text);
            string indate = indate1.ToString("yyyy-MM-dd");
            griddate.DataSource = null;
            griddate.DataBind();
            DataTable dtdayrpt = new DataTable();
            dtdayrpt.Rows.Clear();
            SqlConnection con = new SqlConnection(strconn11);
            con.Open();
            //SqlCommand cmd = new SqlCommand("Select * from tblProductinward where Indate='" + indate + "'", con);
            if (ddlGrp.Text == "ALL")
            {
                SqlCommand cmd = new SqlCommand("Select a.Stockinward as Stockinward,a.Taxamount as Taxamount,a.Purchaseprice as Purchaseprice,b.SupplierName as SupplierName,c.Productname as Productname,d.ManufactureName as ManufactureName from tblProductinward a left join tblsuppliermaster b on a.SuppplierCode=b.SupplierCode left join tblProductMaster c on a.Productcode=c.Productcode left join tblmanufacture d on d.ManufactureCode=a.ManufactureCode where a.Indate='" + indate + "' ", con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            else 
            {
                DataSet dsgrp = clsbd.GetcondDataSet("*", "tblGroup", "g_name", ddlGrp.Text);
                string groupcode = dsgrp.Tables[0].Rows[0]["g_code"].ToString();
                SqlCommand cmd = new SqlCommand("Select a.Stockinward as Stockinward,a.Taxamount as Taxamount,a.Purchaseprice as Purchaseprice,b.SupplierName as SupplierName,c.Productname as Productname,d.ManufactureName as ManufactureName from tblProductinward a left join tblsuppliermaster b on a.SuppplierCode=b.SupplierCode left join tblProductMaster c on a.Productcode=c.Productcode left join tblmanufacture d on d.ManufactureCode=a.ManufactureCode where a.Indate='" + indate + "' and a.g_code='" + groupcode + "' ", con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
           //DataSet ds = new DataSet();
           
           if (ds.Tables[0].Rows.Count > 0)
            {
                DataColumn col = new DataColumn("SlN0", typeof(int));
                col.AutoIncrement = true;
                col.AutoIncrementSeed = 1;
                col.AutoIncrementStep = 1;
                dtdayrpt.Columns.Add(col);
                dtdayrpt.Columns.Add("Product Name");
                dtdayrpt.Columns.Add("manufacturer");
                dtdayrpt.Columns.Add("Supplier");
                dtdayrpt.Columns.Add("Quantity");
                dtdayrpt.Columns.Add("Tax");
                dtdayrpt.Columns.Add("Amount");
                Session["PurchaseReport"] = dtdayrpt;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dtdayrpt = (DataTable)Session["PurchaseReport"];
                    /*string supcode = ds.Tables[0].Rows[i]["SuppplierCode"].ToString();
                    string prodcode = ds.Tables[0].Rows[i]["Productcode"].ToString();
                    string manufacturecode = ds.Tables[0].Rows[i]["ManufactureCode"].ToString();
                    DataSet dssup = clsbd.GetcondDataSet("*", "tblsuppliermaster", "SupplierCode", supcode);
                    DataSet dsprod = clsbd.GetcondDataSet("*", "tblProductMaster", "Productcode", prodcode);
                    DataSet dsman = clsbd.GetcondDataSet("*", "tblmanufacture", "ManufactureCode", manufacturecode);*/
                    dr = dtdayrpt.NewRow();
                    dr["Product Name"] = ds.Tables[0].Rows[i]["Productname"].ToString();
                    dr["manufacturer"] = ds.Tables[0].Rows[i]["ManufactureName"].ToString();
                    dr["Supplier"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();
                    dr["Quantity"] = ds.Tables[0].Rows[i]["Stockinward"].ToString();
                    decimal taxx = Convert.ToDecimal(ds.Tables[0].Rows[i]["Taxamount"].ToString());
                    string tax = taxx.ToString("F");
                    dr["Tax"] = tax;
                    decimal amtt = Convert.ToDecimal(ds.Tables[0].Rows[i]["Purchaseprice"].ToString());
                    string amt = amtt.ToString("F");
                    dr["Amount"] = amt;
                   // dr["Tax"] = ds.Tables[0].Rows[i]["Taxamount"].ToString();
                   // dr["Amount"] = ds.Tables[0].Rows[i]["Purchaseprice"].ToString();
                    dtdayrpt.Rows.Add(dr);
                }
                DataView dw = dtdayrpt.DefaultView;
                dw.Sort = "SlN0 ASC";
                griddate.DataSource = dtdayrpt;
                griddate.DataBind();

            }
            else
            {
                Master.ShowModal("Hello..!!! There are no transactions in the entered date", "txtDay", 1);
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
    public void binddatebtw()
    {
        try
        {
            DataSet ds = new DataSet();
            DateTime indate1 = Convert.ToDateTime(txtbtwDate1.Text);
            string indate = indate1.ToString("yyyy-MM-dd");
            DateTime indate2 = Convert.ToDateTime(txtbtwDate2.Text);
            string indate3=indate2.ToString("yyyy-MM-dd");
            griddate.DataSource = null;
            griddate.DataBind();
            DataTable dtdayrpt = new DataTable();
            dtdayrpt.Rows.Clear();
            SqlConnection con = new SqlConnection(strconn11);
            con.Open();
            //SqlCommand cmd = new SqlCommand("Select * from tblProductinward where Indate='" + indate + "'", con);
            if (ddlgrp1.Text == "ALL")
            {
                SqlCommand cmd = new SqlCommand("Select a.Indate as Indate,a.Stockinward as Stockinward,a.Taxamount as Taxamount,a.Purchaseprice as Purchaseprice,b.SupplierName as SupplierName,c.Productname as Productname,d.ManufactureName as ManufactureName from tblProductinward a left join tblsuppliermaster b on a.SuppplierCode=b.SupplierCode left join tblProductMaster c on a.Productcode=c.Productcode left join tblmanufacture d on d.ManufactureCode=a.ManufactureCode where a.Indate>='" + indate + "' and a.Indate<='" + indate3 + "' ", con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            else
            {
                DataSet dsgrp = clsbd.GetcondDataSet("*", "tblGroup", "g_name", ddlgrp1.Text);
                string groupcode = dsgrp.Tables[0].Rows[0]["g_code"].ToString();
                SqlCommand cmd = new SqlCommand("Select a.Indate as Indate,a.Stockinward as Stockinward,a.Taxamount as Taxamount,a.Purchaseprice as Purchaseprice,b.SupplierName as SupplierName,c.Productname as Productname,d.ManufactureName as ManufactureName from tblProductinward a left join tblsuppliermaster b on a.SuppplierCode=b.SupplierCode left join tblProductMaster c on a.Productcode=c.Productcode left join tblmanufacture d on d.ManufactureCode=a.ManufactureCode where a.Indate>='" + indate + "' and a.Indate<='" + indate3 + "' and a.g_code='" + groupcode + "' ", con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataColumn col = new DataColumn("SlN0", typeof(int));
                col.AutoIncrement = true;
                col.AutoIncrementSeed = 1;
                col.AutoIncrementStep = 1;
                dtdayrpt.Columns.Add(col);
                dtdayrpt.Columns.Add("Indate");
                dtdayrpt.Columns.Add("Product Name");
                dtdayrpt.Columns.Add("manufacturer");
                dtdayrpt.Columns.Add("Supplier");
                dtdayrpt.Columns.Add("Quantity");
                dtdayrpt.Columns.Add("Tax");
                dtdayrpt.Columns.Add("Amount");
                Session["PurchaseReport"] = dtdayrpt;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dtdayrpt = (DataTable)Session["PurchaseReport"];
                    /*string supcode = ds.Tables[0].Rows[i]["SuppplierCode"].ToString();
                    string prodcode = ds.Tables[0].Rows[i]["Productcode"].ToString();
                    string manufacturecode = ds.Tables[0].Rows[i]["ManufactureCode"].ToString();
                    DataSet dssup = clsbd.GetcondDataSet("*", "tblsuppliermaster", "SupplierCode", supcode);
                    DataSet dsprod = clsbd.GetcondDataSet("*", "tblProductMaster", "Productcode", prodcode);
                    DataSet dsman = clsbd.GetcondDataSet("*", "tblmanufacture", "ManufactureCode", manufacturecode);*/
                    dr = dtdayrpt.NewRow();
                    DateTime date1=Convert.ToDateTime(ds.Tables[0].Rows[i]["Indate"].ToString());
                    string date2 = date1.ToString("yyyy-MM-dd");
                    dr["Indate"] = date2;
                    dr["Product Name"] = ds.Tables[0].Rows[i]["Productname"].ToString();
                    dr["manufacturer"] = ds.Tables[0].Rows[i]["ManufactureName"].ToString();
                    dr["Supplier"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();
                    dr["Quantity"] = ds.Tables[0].Rows[i]["Stockinward"].ToString();
                    decimal taxx = Convert.ToDecimal(ds.Tables[0].Rows[i]["Taxamount"].ToString());
                    string tax = taxx.ToString("F");
                    dr["Tax"] = tax;
                    decimal amtt = Convert.ToDecimal(ds.Tables[0].Rows[i]["Purchaseprice"].ToString());
                    string amt = amtt.ToString("F");
                    dr["Amount"] = amt;
                    //dr["Tax"] = ds.Tables[0].Rows[i]["Taxamount"].ToString();
                   // dr["Amount"] = ds.Tables[0].Rows[i]["Purchaseprice"].ToString();
                    dtdayrpt.Rows.Add(dr);
                }
                DataView dw = dtdayrpt.DefaultView;
                dw.Sort = "SlN0 ASC";
                griddate.DataSource = dtdayrpt;
                griddate.DataBind();

            }
            else
            {
                Master.ShowModal("Hello..!!! There are no transactions in the entered date", "txtbtwDate1", 1);
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
    public void bindsupp()
    {
        try
        {
            string suppcode = txtSuppCode.Text;
            string suppname = txtSuppName.Text;
            griddate.DataSource = null;
            griddate.DataBind();
            DataTable dtdayrpt = new DataTable();
            dtdayrpt.Rows.Clear();
            SqlConnection con = new SqlConnection(strconn11);
            con.Open();
            //SqlCommand cmd = new SqlCommand("Select * from tblProductinward where Indate='" + indate + "'", con);
            SqlCommand cmd = new SqlCommand("Select a.Indate as Indate,a.Stockinward as Stockinward,a.Taxamount as Taxamount,a.Purchaseprice as Purchaseprice,b.SupplierName as SupplierName,c.Productname as Productname,d.ManufactureName as ManufactureName from tblProductinward a left join tblsuppliermaster b on a.SuppplierCode=b.SupplierCode left join tblProductMaster c on a.Productcode=c.Productcode left join tblmanufacture d on d.ManufactureCode=a.ManufactureCode where a.SuppplierCode='" + suppcode + "' ", con);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataColumn col = new DataColumn("SlN0", typeof(int));
                col.AutoIncrement = true;
                col.AutoIncrementSeed = 1;
                col.AutoIncrementStep = 1;
                dtdayrpt.Columns.Add(col);
                dtdayrpt.Columns.Add("Indate");
                dtdayrpt.Columns.Add("Product Name");
                dtdayrpt.Columns.Add("manufacturer");
                dtdayrpt.Columns.Add("Supplier");
                dtdayrpt.Columns.Add("Quantity");
                dtdayrpt.Columns.Add("Tax");
                dtdayrpt.Columns.Add("Amount");
                Session["PurchaseReport"] = dtdayrpt;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dtdayrpt = (DataTable)Session["PurchaseReport"];
                    /*string supcode = ds.Tables[0].Rows[i]["SuppplierCode"].ToString();
                    string prodcode = ds.Tables[0].Rows[i]["Productcode"].ToString();
                    string manufacturecode = ds.Tables[0].Rows[i]["ManufactureCode"].ToString();
                    DataSet dssup = clsbd.GetcondDataSet("*", "tblsuppliermaster", "SupplierCode", supcode);
                    DataSet dsprod = clsbd.GetcondDataSet("*", "tblProductMaster", "Productcode", prodcode);
                    DataSet dsman = clsbd.GetcondDataSet("*", "tblmanufacture", "ManufactureCode", manufacturecode);*/
                    dr = dtdayrpt.NewRow();
                    DateTime date1 = Convert.ToDateTime(ds.Tables[0].Rows[i]["Indate"].ToString());
                    string date2 = date1.ToString("yyyy-MM-dd");
                    dr["Indate"] = date2;
                    dr["Product Name"] = ds.Tables[0].Rows[i]["Productname"].ToString();
                    dr["manufacturer"] = ds.Tables[0].Rows[i]["ManufactureName"].ToString();
                    dr["Supplier"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();
                    dr["Quantity"] = ds.Tables[0].Rows[i]["Stockinward"].ToString();
                    decimal taxx = Convert.ToDecimal(ds.Tables[0].Rows[i]["Taxamount"].ToString());
                    string tax = taxx.ToString("F");
                    dr["Tax"] = tax;
                    decimal amtt = Convert.ToDecimal(ds.Tables[0].Rows[i]["Purchaseprice"].ToString());
                    string amt = amtt.ToString("F");
                    dr["Amount"] = amt;
                    dtdayrpt.Rows.Add(dr);
                }
                DataView dw = dtdayrpt.DefaultView;
                dw.Sort = "SlN0 ASC";
                griddate.DataSource = dtdayrpt;
                griddate.DataBind();

            }
            else
            {
                Master.ShowModal("Hello..!!! There are no transactions for entered supplier code", "txtSuppCode", 1);
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


    public void bindsupp10()
    {
        try
        {
            string suppcode = txtSuppCode.Text;
            string suppname = txtSuppName.Text;
            griddate.DataSource = null;
            griddate.DataBind();
            DataTable dtdayrpt = new DataTable();
            dtdayrpt.Rows.Clear();
            SqlConnection con = new SqlConnection(strconn11);
            con.Open();
            //SqlCommand cmd = new SqlCommand("Select * from tblProductinward where Indate='" + indate + "'", con);
            SqlCommand cmd = new SqlCommand("Select a.Indate as Indate,a.Stockinward as Stockinward,a.Taxamount as Taxamount,a.Purchaseprice as Purchaseprice,b.SupplierName as SupplierName,c.Productname as Productname,d.ManufactureName as ManufactureName from tblProductinward a left join tblsuppliermaster b on a.SuppplierCode=b.SupplierCode left join tblProductMaster c on a.Productcode=c.Productcode left join tblmanufacture d on d.ManufactureCode=a.ManufactureCode where a.SuppplierCode='" + suppcode + "' and  a.Invoiceno ='" + ddinvoiveno.SelectedItem.Text + "'", con);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataColumn col = new DataColumn("SlN0", typeof(int));
                col.AutoIncrement = true;
                col.AutoIncrementSeed = 1;
                col.AutoIncrementStep = 1;
                dtdayrpt.Columns.Add(col);
                dtdayrpt.Columns.Add("Indate");
                dtdayrpt.Columns.Add("Product Name");
                dtdayrpt.Columns.Add("manufacturer");
                dtdayrpt.Columns.Add("Supplier");
                dtdayrpt.Columns.Add("Quantity");
                dtdayrpt.Columns.Add("Tax");
                dtdayrpt.Columns.Add("Amount");
                Session["PurchaseReport"] = dtdayrpt;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dtdayrpt = (DataTable)Session["PurchaseReport"];
                    /*string supcode = ds.Tables[0].Rows[i]["SuppplierCode"].ToString();
                    string prodcode = ds.Tables[0].Rows[i]["Productcode"].ToString();
                    string manufacturecode = ds.Tables[0].Rows[i]["ManufactureCode"].ToString();
                    DataSet dssup = clsbd.GetcondDataSet("*", "tblsuppliermaster", "SupplierCode", supcode);
                    DataSet dsprod = clsbd.GetcondDataSet("*", "tblProductMaster", "Productcode", prodcode);
                    DataSet dsman = clsbd.GetcondDataSet("*", "tblmanufacture", "ManufactureCode", manufacturecode);*/
                    dr = dtdayrpt.NewRow();
                    DateTime date1 = Convert.ToDateTime(ds.Tables[0].Rows[i]["Indate"].ToString());
                    string date2 = date1.ToString("yyyy-MM-dd");
                    dr["Indate"] = date2;
                    dr["Product Name"] = ds.Tables[0].Rows[i]["Productname"].ToString();
                    dr["manufacturer"] = ds.Tables[0].Rows[i]["ManufactureName"].ToString();
                    dr["Supplier"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();
                    dr["Quantity"] = ds.Tables[0].Rows[i]["Stockinward"].ToString();
                    decimal taxx = Convert.ToDecimal(ds.Tables[0].Rows[i]["Taxamount"].ToString());
                    string tax = taxx.ToString("F");
                    dr["Tax"] = tax;
                    decimal amtt = Convert.ToDecimal(ds.Tables[0].Rows[i]["Purchaseprice"].ToString());
                    string amt = amtt.ToString("F");
                    dr["Amount"] = amt;
                    dtdayrpt.Rows.Add(dr);
                }
                DataView dw = dtdayrpt.DefaultView;
                dw.Sort = "SlN0 ASC";
                griddate.DataSource = dtdayrpt;
                griddate.DataBind();

            }
            else
            {
                Master.ShowModal("Hello..!!! There are no transactions for entered supplier code", "txtSuppCode", 1);
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
    protected void txtDay_TextChanged(object sender, EventArgs e)
    {
        ddlGrp.Focus();
    }
    protected void cmbGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnsave.Focus();
    }
    public void groupcode()
    {
        try
        {
            ArrayList arrgrp = new ArrayList();
            ArrayList arrgrp1 = new ArrayList();
            DataSet ds = clsbd.GetDataSet("distinct g_name", "tblGroup");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //DataSet ds1 = clsbd.GetcondDataSet("*", "tblGroup", "g_name", ds.Tables[0].Rows[i]["g_name"].ToString());
                arrgrp.Add(ds.Tables[0].Rows[i]["g_name"].ToString());
            }
            arrgrp1.Sort();
            arrgrp1.Add("Select a group");
            arrgrp1.Add("ALL");
            for (int j = 0; j < arrgrp.Count; j++)
            {
                arrgrp1.Add(arrgrp[j].ToString());
            }
            ddlgrp1.DataSource = arrgrp1;
            ddlGrp.DataSource = arrgrp1;
            ddlGrp.DataBind();
            ddlgrp1.DataBind();
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
    }
    protected void ddlGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnsave.Focus();
    }
    protected void ddlgrp1_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnsave.Focus();
    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton1.Checked = false;
        lblchkgrp.Visible = true;
        ddinvoiveno.Visible = true;
        groupcode1();
    }

    public void groupcode1()
    {
       // DataSet dsgroup = clsbd.GetDataSet("distinct Invoiceno", "tblProductinward");
       // for (int i = 0; i < dsgroup.Tables[0].Rows.Count; i++)
       // {
       //     DataSet dsgroup1 = clsbd.GetcondDataSet("*", "tblProductinward", "SuppplierCode", txtSuppCode.Text);
       //     arryname.Add(dsgroup1.Tables[0].Rows[0]["Invoiceno"].ToString());


       // }

       // arryname.Sort();
       // arryno.Add("-Select-");
       //// arryno.Add("Add New");
       // for (int i = 0; i < arryname.Count; i++)
       // {
       //     arryno.Add(arryname[i].ToString());
       // }
       // ddinvoiveno.DataSource = arryno;
       // ddinvoiveno.DataBind();

        //string bankname = "BANK ACCOUNT";
        DataSet ds = clsbd.GetcondDataSet("distinct Invoiceno", "tblProductinward", "SuppplierCode", txtSuppCode.Text);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            arryname.Add(ds.Tables[0].Rows[i]["Invoiceno"].ToString());
        }

        arryname.Sort();
        arryno.Add("-Select-");
        for (int i = 0; i < arryname.Count; i++)
        {
            arryno.Add(arryname[i].ToString());
        }
        ddinvoiveno.DataSource = arryno;
        ddinvoiveno.DataBind();



      

    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton2.Checked = false;
    }
}