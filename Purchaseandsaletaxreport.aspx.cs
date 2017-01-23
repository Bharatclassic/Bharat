using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data.SqlClient;
using AlertMessageName;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using AllHospitalNames;


public partial class Purchaseandsaletaxreport : System.Web.UI.Page
{
    protected static string strconn11 = Dbconn.conmenthod();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();

    DataTable dtpurchasetax = new DataTable();
    DataRow drpurchasetax;
    ArrayList arryno = new ArrayList();
    PharmacyName Hosp = new PharmacyName();
    DataTable dtsaletax = new DataTable();
    DataRow drsaletax;

    string sqlFormattedDate = DateTime.Now.ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Visible = false;
        lblsuccess.Visible = false;
        grdpurchasetax.Visible = true;
        grdsaletax.Visible = true;
        btnexit.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtbtwDate1.ClientID + "').focus();return false;}} else {return true}; ");       

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
    private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, BaseColor color)
    {
        PdfContentByte contentByte = writer.DirectContent;
        contentByte.SetColorStroke(color);
        contentByte.MoveTo(x1, y1);
        contentByte.LineTo(x2, y2);
        contentByte.Stroke();
    }
    public void purchasetax()
    {
        dtpurchasetax.Columns.Add("Invoice Date");
        dtpurchasetax.Columns.Add("Tax@");
        dtpurchasetax.Columns.Add("Tax amount");
        Session["Purchase"] = dtpurchasetax;
    }
    public void saletax()
    {
        dtsaletax.Columns.Add("Invoice Date");
        dtsaletax.Columns.Add("Tax@");
        dtsaletax.Columns.Add("Tax amount");
        Session["sale"] = dtsaletax;
    }

    protected void btnreport_Click(object sender, EventArgs e)
        {
        try
        {
             DateTime trdate = Convert.ToDateTime(txtbtwDate1.Text);
            string trdate1 = trdate.ToString("yyyy-MM-dd");
            DateTime trdate2 = Convert.ToDateTime(txtbtwDate2.Text);
            string trdate3 = trdate2.ToString("yyyy-MM-dd");
            SqlConnection con1 = new SqlConnection(strconn11);
            SqlCommand cmd1 = new SqlCommand("select Invoicedate,ptax,ptaxamount from tblPurchasetax  where Invoicedate >= '" + trdate1 + "' and Invoicedate <= '" + trdate3 + "'", con1);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            purchasetax();

            if (ds1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    
                {
                    DateTime indate = Convert.ToDateTime(ds1.Tables[0].Rows[i]["Invoicedate"].ToString());
                    string date = indate.ToString("yyyy-MM-dd");
                    double ptax = Convert.ToDouble(ds1.Tables[0].Rows[i]["ptax"].ToString());
                    double ptaxamount1 = Convert.ToDouble(ds1.Tables[0].Rows[i]["ptaxamount"].ToString());
                    string ptaxamount = ptaxamount1.ToString("F");
                    dtpurchasetax = (DataTable)Session["Purchase"];
                    drpurchasetax = dtpurchasetax.NewRow();
                    drpurchasetax["Invoice Date"] = date;
                    drpurchasetax["Tax@"] = ptax;
                    drpurchasetax["Tax amount"] = ptaxamount;
                    dtpurchasetax.Rows.Add(drpurchasetax);
                    grdpurchasetax.DataSource = dtpurchasetax;
                    grdpurchasetax.DataBind();
                }
            }

            DateTime trdate6 = Convert.ToDateTime(txtbtwDate1.Text);
            string trdate7 = trdate.ToString("yyyy-MM-dd");
            DateTime trdate8 = Convert.ToDateTime(txtbtwDate2.Text);
            string trdate9 = trdate2.ToString("yyyy-MM-dd");
            SqlConnection con2 = new SqlConnection(strconn11);
            SqlCommand cmd2 = new SqlCommand("select Invoicedate,stax,staxamount from tblSalestax  where Invoicedate >= '" + trdate7 + "' and Invoicedate <= '" + trdate9 + "'", con2);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            saletax();
            if (ds2.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < ds2.Tables[0].Rows.Count; j++)
                {
                    DateTime indate1 = Convert.ToDateTime(ds2.Tables[0].Rows[j]["Invoicedate"].ToString());
                    string date1 = indate1.ToString("yyyy-MM-dd");
                    double stax = Convert.ToDouble(ds2.Tables[0].Rows[j]["stax"].ToString());
                    double staxamount1 = Convert.ToDouble(ds2.Tables[0].Rows[j]["staxamount"].ToString());
                    string staxamount = staxamount1.ToString("F");

                    dtsaletax = (DataTable)Session["sale"];
                    drsaletax = dtsaletax.NewRow();
                    drsaletax["Invoice Date"] = date1;
                    drsaletax["Tax@"]=stax;
                    drsaletax["Tax amount"] = staxamount;
                    dtsaletax.Rows.Add(drsaletax);
                    grdsaletax.DataSource = dtsaletax;
                    grdsaletax.DataBind();
                }
            }
                
            DataTable dtptax = new DataTable();
            DataTable dtstax = new DataTable();

            if (grdpurchasetax.HeaderRow != null)
            {
                for (int i = 0; i < grdpurchasetax.HeaderRow.Cells.Count; i++)
                {
                    dtptax.Columns.Add(grdpurchasetax.HeaderRow.Cells[i].Text);
                }
            }
            foreach (GridViewRow row in grdpurchasetax.Rows)
            {
                DataRow drptax;
                drptax = dtptax.NewRow();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    drptax[i] = row.Cells[i].Text;
                }
                dtptax.Rows.Add(drptax);
            }
            if (grdsaletax.HeaderRow != null)
            {
                for (int i = 0; i < grdsaletax.HeaderRow.Cells.Count; i++)
                {
                    dtstax.Columns.Add(grdsaletax.HeaderRow.Cells[i].Text);
                }
            }
            foreach (GridViewRow row in grdsaletax.Rows)
            {
                DataRow drstax;
                drstax = dtstax.NewRow();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    drstax[i] = row.Cells[i].Text;
                }
                dtstax.Rows.Add(drstax);
            }

            MemoryStream memorystream = new MemoryStream();
            Document pdocument = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            ArrayList oALHospDetails = Hosp.HospitalReturns();
           // PdfWriter.GetInstance(document, Response.OutputStream);
           // Document document1 = new Document();
           // Font Normalfont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);

            
           // PdfWriter.GetInstance(document, Response.OutputStream);
            PdfWriter writer = PdfWriter.GetInstance(pdocument, memorystream);
            //PdfWriterEvents pdfEvents = new PdfWriterEvents("HHHHHHH");
            //writer.PageEvent = pdfEvents;

            //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path + "/TablesSideBySide.pdf", FileMode.Create));
            //DataTable dtPdfstock = new DataTable();

            Phrase phrase = null;   
            //PdfPCell cell = null;
            PdfPTable tblsale = null;
            PdfPTable table1 = null;
            PdfPTable table2 = null;
            PdfPTable table3 = null;
            PdfPTable table4 = null;
            PdfPCell cell = null;
           // PdfPTable date = null;
            PdfPCell Gridcell = null;
            BaseColor color = null;
            PdfPTable tblCollectionDetails = null;
            PdfPTable tbltotaldetails = null;
            PdfPTable tbltotalsale = null;
            PdfPTable tbltotalpurchase = null;
            PdfPTable tblpharmacyname = null;


            pdocument.Open();

            tblsale = new PdfPTable(1);
            tblsale.TotalWidth = 590f;
            tblsale.LockedWidth = true;
            tblsale.SetWidths(new float[] { 1f });


           // date = new PdfPTable(1);
           // date.TotalWidth = 590f;
          //  date.LockedWidth = true;
          //  date.SetWidths(new float[] { 1f });

            table1 = new PdfPTable(2);
            table1.TotalWidth = 500f;
            table1.LockedWidth = true;
            table1.SetWidths(new float[] { 1f, 1f });

            tblpharmacyname = new PdfPTable(1);
            tblpharmacyname.TotalWidth = 500f;
            tblpharmacyname.LockedWidth = true;
            tblpharmacyname.SetWidths(new float[] { 1f });


            table2 = new PdfPTable(3);
            table2.TotalWidth = 250f;
            table2.LockedWidth = true;
            table2.SetWidths(new float[] { 1f, 1f, 1f });


            table3 = new PdfPTable(3);
            table3.TotalWidth = 250f;
            table3.LockedWidth = true;
            table3.SetWidths(new float[] { 1f, 1f, 1f });

            table4 = new PdfPTable(2);
            table4.TotalWidth = 500f;
            table4.LockedWidth = true;
            table4.SetWidths(new float[] { 1f ,1f});

            tbltotalpurchase = new PdfPTable(3);
            tbltotalpurchase.TotalWidth = 250f;
            tbltotalpurchase.LockedWidth = true;
            tbltotalpurchase.SetWidths(new float[] {1f,1f,1f});



            tbltotalsale = new PdfPTable(3);
            tbltotalsale.TotalWidth = 250f;
            tbltotalsale.LockedWidth = true;
            tbltotalsale.SetWidths(new float[] {1f,1f,1f});


            tbltotaldetails = new PdfPTable(2);
            tbltotaldetails.TotalWidth = 500f;
            tbltotaldetails.LockedWidth = true;
            tbltotaldetails.SetWidths(new float[] { 1f, 1f });

            if (dtptax.Columns.Count > 0)
            {
                table2 = new PdfPTable(dtptax.Columns.Count);
                table2.TotalWidth = 250f;
                table2.LockedWidth = true;
                table2.SetWidths(new float[] { 1f, 1f, 1f });
            }
            else
            {
                table2 = new PdfPTable(3);
                table2.TotalWidth = 250f;
                table2.LockedWidth = true;
                table2.SetWidths(new float[] { 1f, 1f, 1f });
            }
            if (dtstax.Columns.Count > 0)
            {
                table3 = new PdfPTable(dtstax.Columns.Count);
                table3.TotalWidth = 250f;
                table3.LockedWidth = true;
                table3.SetWidths(new float[] { 1f, 1f, 1f });
            }
            else
            {
                table3 = new PdfPTable(3);
                table3.TotalWidth = 250f;
                table3.LockedWidth = true;
                table3.SetWidths(new float[] { 1f, 1f, 1f });
            }

            tblCollectionDetails = new PdfPTable(2);
            tblCollectionDetails.LockedWidth = true;
            tblCollectionDetails.TotalWidth = 500f;
            tblCollectionDetails.SetWidths(new float[] { 1f, 1f });

            tblsale.AddCell(PhraseCell(new Phrase("Purchase and Sales Tax Report\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
            tblsale.SpacingAfter = 15f;

            Gridcell = new PdfPCell(new Phrase(new Chunk("Purchase Tax", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK))));
            Gridcell.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(Gridcell);

            Gridcell = new PdfPCell(new Phrase(new Chunk("Sales Tax", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK))));
            Gridcell.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(Gridcell);

            // Gridcell = new PdfPCell(new Phrase(new Chunk("SlNo.",FontFactory.GetFont("Times",10,Font.BOLD,BaseColor.BLACK))));
            // Gridcell.HorizontalAlignment = Element.ALIGN_CENTER;
            // table2.AddCell(Gridcell);

            Gridcell = new PdfPCell(new Phrase(new Chunk("Invoice Date", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            Gridcell.HorizontalAlignment = Element.ALIGN_CENTER;
            table2.AddCell(Gridcell);

            Gridcell = new PdfPCell(new Phrase(new Chunk("Tax@", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            Gridcell.HorizontalAlignment = Element.ALIGN_CENTER;
            table2.AddCell(Gridcell);

            Gridcell = new PdfPCell(new Phrase(new Chunk("Tax amount", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            Gridcell.HorizontalAlignment = Element.ALIGN_CENTER;
            table2.AddCell(Gridcell);
            table2.SpacingAfter = 15f;
            
            if (dtptax.Rows.Count > 0)
            {
                for (int i = 0; i < dtptax.Rows.Count; i++)
                {
                    for (int j = 0; j < dtptax.Columns.Count; j++)
                    {
                        Gridcell = new PdfPCell(new Phrase(new Chunk(dtptax.Rows[i][j].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                        Gridcell.HorizontalAlignment = Element.ALIGN_RIGHT;
                       // Gridcell.PaddingBottom = 5f;
                        table2.AddCell(Gridcell);
                    }


                }
            }

            Gridcell = new PdfPCell(new Phrase(new Chunk("Invoice Date", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            Gridcell.HorizontalAlignment = Element.ALIGN_CENTER;           
            table3.AddCell(Gridcell);

            Gridcell = new PdfPCell(new Phrase(new Chunk("Tax@", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            Gridcell.HorizontalAlignment = Element.ALIGN_CENTER;
            table3.AddCell(Gridcell);

            Gridcell = new PdfPCell(new Phrase(new Chunk("Tax amount", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            Gridcell.HorizontalAlignment = Element.ALIGN_CENTER;
            table3.AddCell(Gridcell);
            table3.SpacingAfter = 15f;

            if (dtstax.Rows.Count > 0)
            {
                for (int i = 0; i < dtstax.Rows.Count; i++)
                {
                    for (int j = 0; j < dtstax.Columns.Count; j++)
                    {
                        Gridcell = new PdfPCell(new Phrase(new Chunk(dtstax.Rows[i][j].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                        Gridcell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        //Gridcell.PaddingBottom = 5f;
                        table3.AddCell(Gridcell);
                    }


                }
            }
            //dtpurchasetax.Rows.Clear();
            //dtsaletax.Rows.Clear();
            decimal totalptax = 0;
            decimal totalstax = 0;
            for (int i = 0; i < grdpurchasetax.Rows.Count; i++)
            {
                decimal totptax = 0;
                totptax = Convert.ToDecimal(grdpurchasetax.Rows[i].Cells[2].Text);
                totalptax += totptax;
            }
            for (int j = 0; j < grdsaletax.Rows.Count; j++)
            {
                decimal totstax = 0;
                totstax = Convert.ToDecimal(grdsaletax.Rows[j].Cells[2].Text);
                totalstax += totstax;
            }

           

            tbltotaldetails.DefaultCell.Border = 0;
            Gridcell = new PdfPCell(new Phrase(new Chunk("Total", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            Gridcell.Colspan = 2;
            Gridcell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tbltotalpurchase.AddCell(Gridcell);
            Gridcell = new PdfPCell(new Phrase(new Chunk(totalptax.ToString("f"), FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            Gridcell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tbltotalpurchase.AddCell(Gridcell);

            //tbltotaldetails.DefaultCell.Border = 0;
            Gridcell = new PdfPCell(new Phrase(new Chunk("Total", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            Gridcell.Colspan = 2;
            Gridcell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tbltotalsale.AddCell(Gridcell);
            Gridcell = new PdfPCell(new Phrase(new Chunk(totalstax.ToString("f"), FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            Gridcell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tbltotalsale.AddCell(Gridcell);

            DataSet dslogin = ClsBLGD.GetcondDataSet("*", "tblLogin", "username", Session["username"].ToString());
            table4.AddCell(PhraseCell(new Phrase("\n\n\n\n" + "Generated On:" + sqlFormattedDate, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
            table4.AddCell(PhraseCell(new Phrase("\n\n\n\n" + "Generated By:" + "(" + dslogin.Tables[0].Rows[0]["UserName"].ToString() + ")", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
            cell.Colspan = 2;
            cell.PaddingBottom = 30f;
            //table4.AddCell(cell);

            phrase = new Phrase();
            phrase.Add(new Chunk(oALHospDetails[0].ToString() + "\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)));
            phrase.Add(new Chunk(oALHospDetails[1].ToString() + "\n" + oALHospDetails[2].ToString() + "\n" + oALHospDetails[3].ToString() + "\n\n", FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)));
            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
            cell.HorizontalAlignment = 0;
            tblpharmacyname.AddCell(cell);


            tbltotaldetails.AddCell(tbltotalpurchase);
            tbltotaldetails.AddCell(tbltotalsale);


            tblCollectionDetails.AddCell(table2);          
            tblCollectionDetails.AddCell(table3);


            pdocument.Add(tblsale);
            pdocument.Add(tblpharmacyname);
            pdocument.Add(table1);
            pdocument.Add(tblCollectionDetails);
            pdocument.Add(tbltotaldetails);
            pdocument.Add(table4);
            pdocument.Close();

            byte[] bytes = memorystream.ToArray();
            memorystream.Close();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=Taxreport.pdf");
            Response.ContentType = "application/pdf";

            

            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();


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