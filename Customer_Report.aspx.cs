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
//using System.Drawing;


public partial class Customer_Report : System.Web.UI.Page
{
    ClsBLLGeneraldetails clsbd = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string strconn11 = Dbconn.conmenthod();
    DataRow dr;
    DataTable dtrpt = new DataTable();
    String sqlFormattedDate = DateTime.Now.ToString();
    GridView grcustrpt = new GridView();
    double credit;
    double debit;
    PharmacyName hosp = new PharmacyName();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtdate.Focus();
            lblerror.Visible = false;
            lblsuccess.Visible = false;
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
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
            if (txtdate.Text == "")
            {
                Master.ShowModal("Please enter a date", "txtdate", 1);
                return;
            }
            bind();
            //arraylist  oALHospDetails = Hosp.HospitalReturns();
            ArrayList oALHospitalDetails = hosp.HospitalReturns();
            SqlConnection con = new SqlConnection(strconn11);
            DateTime invoicedate = Convert.ToDateTime(txtdate.Text);
            //string.Format("yyyy-mm-dd", invoicedate);
            string indate = invoicedate.ToString("yyyy-MM-dd");
            SqlCommand cmd10 = new SqlCommand("select * from tblCustomeraccount where Invoicedate<='" + indate + "'", con);
            SqlDataAdapter da10 = new SqlDataAdapter(cmd10);
            DataSet ds10 = new DataSet();
            da10.Fill(ds10);
            if (ds10.Tables[0].Rows.Count > 0)
            {

                   
                    SqlCommand cmd3 = new SqlCommand("Select CA_name from tblCustomer where CA_code='" + ds10.Tables[0].Rows[0]["CA_code"] + "'", con);
                    SqlCommand cmd1 = new SqlCommand("Select sum(Totalvalues) as Totalvalues from tblCustomeraccount where CA_code='" + ds10.Tables[0].Rows[0]["CA_code"] + "' and Bal_type='C' ", con);
                    SqlCommand cmd2 = new SqlCommand("Select sum(Totalvalues) as Totalvalues from tblCustomeraccount where CA_code='" + ds10.Tables[0].Rows[0]["CA_code"] + "' and Bal_type='D' ", con);
                    DataSet ds3 = new DataSet();
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                    da3.Fill(ds3);
                    DataSet ds1 = new DataSet();
                    DataSet ds2 = new DataSet();
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    da1.Fill(ds1);
                    da2.Fill(ds2);
                    string cred = ds1.Tables[0].Rows[0]["Totalvalues"].ToString();
                    string deb = ds2.Tables[0].Rows[0]["Totalvalues"].ToString();
                    if (cred!="")
                    {
                        credit = Convert.ToDouble(cred);
                    }
                    else
                    {
                        credit = 0;
                    }
                    if (deb!="")
                    {
                        debit = Convert.ToDouble(deb);
                    }
                    else
                    {
                        debit = 0;
                    }
                    double balamt = credit - debit;
            string customercode = ds10.Tables[0].Rows[0]["CA_code"].ToString();
            string customername = ds3.Tables[0].Rows[0]["CA_name"].ToString();
            
           // string invoicedate1 = ds10.Tables[0].Rows[0]["Invoicedate"].ToString();

            

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
            if (grcustrpt.HeaderRow != null)
            {
                for (int i = 0; i < grcustrpt.HeaderRow.Cells.Count; i++)
                {
                    dtPdfcustomer.Columns.Add(grcustrpt.HeaderRow.Cells[i].Text);
                }
            }

            //  add each of the data rows to the table

            foreach (GridViewRow row in grcustrpt.Rows)
            {
                DataRow datarow;
                datarow = dtPdfcustomer.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    datarow[i] = row.Cells[i].Text;
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

            // PdfPTable tbldt = null;
            dtPdfcustomer = (DataTable)Session["dtPdfstock"];
            if (Session["dtPdfstock"] != null)
            {
                table2 = new PdfPTable(dtPdfcustomer.Columns.Count);
            }
            PdfPCell GridCell = null;
            BaseColor color = BaseColor.YELLOW;
            

            document.Open();

            tblstock = new PdfPTable(1);
            tblstock.TotalWidth = 490f;
            tblstock.LockedWidth = true;
            tblstock.SetWidths(new float[] { 1f });

            table1 = new PdfPTable(7);
            table1.TotalWidth = 500f;
            table1.LockedWidth = true;
            table1.SetWidths(new float[] { 0.5f, 1.5f,2f,1.5f,1.5f,1.5f,1.5f});

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
            tblstock.AddCell(PhraseCell(new Phrase("Customer Report\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
            cell.Colspan = 2;
            cell.PaddingBottom = 30f;
            tblstock.AddCell(cell);

            /*tbldt.AddCell(PhraseCell(new Phrase(" Customer Code:" + customercode, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
            tbldt.AddCell(PhraseCell(new Phrase("Customer Name :" + customername, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
            cell.Colspan = 2;
            cell.PaddingBottom = 30f;
            tbldt.AddCell(cell);
            tbldt.SpacingAfter = 15f;

             

            tbldt.AddCell(PhraseCell(new Phrase(" Credit:" + credit, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
            // tbldt.AddCell(PhraseCell(new Phrase("Patient Name :" + pname, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
            cell.Colspan = 2;
            cell.PaddingBottom = 30f;
            tbldt.AddCell(cell);
            tbldt.SpacingAfter = 15f;

            tbldt.AddCell(PhraseCell(new Phrase(" Debit:" + debit, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
            // tbldt.AddCell(PhraseCell(new Phrase("Patient Name :" + pname, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
            cell.Colspan = 2;
            cell.PaddingBottom = 30f;
            tbldt.AddCell(cell);
            tbldt.SpacingAfter = 15f;

            tbldt.AddCell(PhraseCell(new Phrase(" Bal Amt:" + balamt, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
            // tbldt.AddCell(PhraseCell(new Phrase("Patient Name :" + pname, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
            cell.Colspan = 2;
            cell.PaddingBottom = 30f;
            tbldt.AddCell(cell);
            tbldt.SpacingAfter = 15f; */

            GridCell = new PdfPCell(new Phrase(new Chunk("Sl No.", FontFactory.GetFont("Times", 7, Font.BOLD, BaseColor.BLACK))));
            GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(GridCell);

            // GridCell = new PdfPCell(new Phrase(new Chunk("Productcode", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            // GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //table1.AddCell(GridCell);

            GridCell = new PdfPCell(new Phrase(new Chunk("Customer Code", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(GridCell);

            GridCell = new PdfPCell(new Phrase(new Chunk("Customer Name", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(GridCell);

            GridCell = new PdfPCell(new Phrase(new Chunk("Invoice Date(latest)", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(GridCell);

            GridCell = new PdfPCell(new Phrase(new Chunk("Credit(Rs.)", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(GridCell);

            GridCell = new PdfPCell(new Phrase(new Chunk("Debit(Rs.)", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
             GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
             table1.AddCell(GridCell);

             GridCell = new PdfPCell(new Phrase(new Chunk("Bal Amt(Rs.)", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
             GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
             //GridCell.BorderColorRight = BaseColor.BLACK;
             table1.AddCell(GridCell);
             table1.SpacingAfter = 15f;

             /*GridCell = new PdfPCell(new Phrase(new Chunk("", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
             GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
             //GridCell.BorderColor=BaseColor.WHITE;
             //GridCell.BorderColorRight = BaseColor.BLACK;
             table1.AddCell(GridCell);
             table1.DefaultCell.Border = Rectangle.NO_BORDER;
             table1.SpacingAfter = 15f;*/

            if (dtPdfcustomer != null)
            {
                for (int i = 0; i < dtPdfcustomer.Rows.Count; i++)
                {


                    for (int row1 = 0; row1 < dtPdfcustomer.Columns.Count; row1++)
                    {

                        if (row1 == 2)
                        {
                            GridCell = new PdfPCell(new Phrase(dtPdfcustomer.Rows[i][row1].ToString(), FontFactory.GetFont("Times", 8,Font.NORMAL,BaseColor.BLACK)));
                            GridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            GridCell.VerticalAlignment = 15;
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

            DataSet dslogin =  clsbd.GetcondDataSet("*", "tblLogin", "username", Session["username"].ToString());
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
            Response.AddHeader("Content-Disposition", "attachment; filename=CustomerReport.pdf");

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
                Master.ShowModal("There is no transaction to show", "txtdate", 1);
                txtdate.Focus();
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
    public void bind()
    {
        try
        {
            grcustrpt.BorderColor = System.Drawing.Color.White;
            grcustrpt.DataSource = null;
            grcustrpt.DataBind();
            dtrpt.Rows.Clear();
            SqlConnection con = new SqlConnection(strconn11);
            /*string invoicedate = DateTime.Now.ToString
            DateTime invoicedate = DateTime.ParseExact(txtdate.Text, "yyyy-mm-dd",CultureInfo.InvariantCulture);*/
            DateTime invoicedate = Convert.ToDateTime(txtdate.Text);
            //string.Format("yyyy-mm-dd", invoicedate);
            string indate = invoicedate.ToString("yyyy-MM-dd");
            SqlCommand cmd = new SqlCommand("select distinct(CA_code) from tblCustomeraccount where Invoicedate <='" + indate + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataColumn col = new DataColumn("SlNo", typeof(int));
                col.AutoIncrement = true;
                col.AutoIncrementSeed = 1;
                col.AutoIncrementStep = 1;
                dtrpt.Columns.Add(col);
                dtrpt.Columns.Add("Customer Code");
                dtrpt.Columns.Add("Customer Name");
                dtrpt.Columns.Add("Invoice date");
                dtrpt.Columns.Add("Credit");
                dtrpt.Columns.Add("Debit");
                dtrpt.Columns.Add("Balance Amount");
                //dtrpt.Columns.Add("Balance Type");
                Session["CustomerReport"] = dtrpt;
                double credit;
                double debit;
                
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dtrpt = (DataTable)Session["CustomerReport"];
                    string ccode = ds.Tables[0].Rows[i]["CA_code"].ToString();
                    SqlCommand cmd4 = new SqlCommand("Select * from tblCustomer where CA_code='" + ccode + "'", con);
                    SqlCommand cmd1 = new SqlCommand("Select sum(Totalvalues) as Totalvalues from tblCustomeraccount where CA_code='" + ccode + "' and Paymenttype='C' ", con);
                    SqlCommand cmd5 = new SqlCommand("Select max(Invoicedate) as Invoicedate from tblCustomeraccount where CA_code='" + ccode + "'", con);
                    SqlCommand cmd2 = new SqlCommand("Select sum(Totalvalues) as Totalvalues from tblCustomeraccount where CA_code='" + ccode + "' and Paymenttype='D' ", con);
                    DataSet ds1 = new DataSet();
                    DataSet ds2 = new DataSet();
                    DataSet ds3 = new DataSet();
                    DataSet ds4 = new DataSet();
                    SqlDataAdapter da4 = new SqlDataAdapter(cmd5);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd4);
                    da1.Fill(ds1);
                    da2.Fill(ds2);
                    da3.Fill(ds3);
                    da4.Fill(ds4);
                    //string cname = ds3.Tables[0].Rows[0]["CA_name"].ToString();
                    string cre = ds1.Tables[0].Rows[0]["Totalvalues"].ToString();
                        if (cre != string.Empty)
                        {
                            credit = Convert.ToDouble(cre);
                            //credit = cre.ToString("F");
                        }
                        else
                        {
                            credit = 0;
                        }
                        string deb = ds2.Tables[0].Rows[0]["Totalvalues"].ToString();
                        if (deb!=string.Empty)
                    {
                       
                        debit = Convert.ToDouble(deb);
                    }
                    else
                    {
                        debit = 0;
                    }
                    double balamt = credit - debit;
                    dr = dtrpt.NewRow();
                    dr["Customer Code"] = ccode;
                    dr["Customer Name"] = ds3.Tables[0].Rows[0]["CA_name"].ToString();
                    DateTime invo = Convert.ToDateTime(ds4.Tables[0].Rows[0]["Invoicedate"].ToString());
                    string invo1 = invo.ToString("yyyy-MM-dd");
                    dr["Invoice date"] = invo1;
                    dr["Credit"] = credit.ToString("F");
                    dr["Debit"] = debit.ToString("F");
                    if (balamt < 0)
                    {
                        balamt = balamt * (-1);

                        dr["Balance Amount"] = "Debit"+ "  " + balamt.ToString("F");
                        //dr["Balance Amount"] = balamt;
                        //dr["Balance Type"]="Debit";
                        //dr["Balance Type"] = BorderStyle.None;
                    }
                    else
                    {
                        dr["Balance Amount"] = "Credit" + "  " + balamt.ToString("F");
                        //dr["Balance Amount"] = balamt;
                        //dr["Balance Type"] = "Credit";
                        //dr["Balance Type"] = BorderStyle.None;
                    }
                    dtrpt.Rows.Add(dr);

                }
                DataView dwcs = dtrpt.DefaultView;
                dwcs.Sort = "SlNo ASC";
                grcustrpt.DataSource = dtrpt;
                grcustrpt.DataBind();
            }
            else
            {
                Master.ShowModal("Sorry there is nothing to print within the entered date", "txtdate", 1);
                txtdate.Focus();
                return;
            }
            //txtdate.Text = string.Empty;
        }
         catch (Exception e)
        {
            string asd = e.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
    }
}
