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

public partial class ProductList : System.Web.UI.Page
{
    DataTable tblProductinward = new DataTable();
    protected static string strconn11 = Dbconn.conmenthod();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    string sqlFormattedDate = DateTime.Now.ToString();
    DataRow drrw;
    PharmacyName Hosp = new PharmacyName();
    ArrayList arryno = new ArrayList();
    //lblerror.Visible = false;
        //lblsuccess.Visible = false;
        System.DateTime Dtnow = DateTime.Now;
       // string Sysdatetime = Dtnow.ToString("dd/MM/yyyy");
        //txtdate.Text = Sysdatetime;
       // txtdate.Enabled = false;
       // btnExit.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + chkexpiry.ClientID + "').focus();return false;}} else {return true}; ");
    protected void Page_Load(object sender, EventArgs e)
    {


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

                date = new PdfPTable(1);
                date.TotalWidth = 490f;
                date.LockedWidth = true;
                date.SetWidths(new float[] { 1f });

                table1 = new PdfPTable(5);
                table1.TotalWidth = 490f;
                table1.LockedWidth = true;
                table1.SetWidths(new float[] {0.5f, 1f,1f,1f,1f});

                table2 = new PdfPTable(2);
                table2.TotalWidth = 450f;
                //table2.HorizontalAlignment = Element.ALIGN_LEFT;
                table2.LockedWidth = true;
                table2.SetWidths(new float[] { 1f,1f });

                table3 = new PdfPTable(1);
                table3.TotalWidth = 490f;
                // table2.HorizontalAlignment = Element.ALIGN_LEFT;
                table3.LockedWidth = true;
                table3.SetWidths(new float[] { 1f });

              //  tblstock.AddCell(PhraseCell(new Phrase("Product List Report\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
              //  cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
               // cell.Colspan = 2;
             //   cell.PaddingBottom = 30f;
             //   tblstock.AddCell(cell);


                GridCell = new PdfPCell(new Phrase(new Chunk("SlNo", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table1.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("ProductCode", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table1.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("ProductName", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table1.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Shelf", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table1.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Rack", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table1.AddCell(GridCell);

                SqlConnection con5 = new SqlConnection(strconn11);
                    SqlCommand cmd5 = new SqlCommand("Select Productcode ,Productname,Shelf,Row from tblProductMaster", con5);
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
                            table1.AddCell(GridCell);
                            for (int row1 = 0; row1 < ds5.Tables[0].Columns.Count; row1++)
                            {
                                     
                                                         
                                
                                    GridCell = new PdfPCell(new Phrase(new Chunk(ds5.Tables[0].Rows[j][row1].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                                    GridCell.HorizontalAlignment = 0;
                                    GridCell.PaddingBottom = 5f;
                                    table1.AddCell(GridCell);
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
                    table2.AddCell(PhraseCell(new Phrase("\n\n" + "Generated On:" + sqlFormattedDate, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                    table2.AddCell(PhraseCell(new Phrase("\n\n" + "Generated By:" + "(" + dslogin.Tables[0].Rows[0]["UserName"].ToString() + ")", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                    cell.Colspan = 4;
                    cell.PaddingBottom = 30f;
                    table2.AddCell(cell);

                   // document.Add(tblstock);
                   // document.Add(date);
                    document.Add(table3);
                   // document.Add(tblsupplier);
                    
                    document.Add(table1);
                    //document.Add(table5);
                  //  document.Add(table6);
                   //// document.Add(tblsum);
                    // document.Add(tbltotal);

                  //  document.Add(table4);
                    document.Add(table2);
                    document.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=ProductlistReport.pdf");

                    byte[] bytes = memorystream.ToArray();
                    memorystream.Close();
                    Response.Clear();

                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(bytes);
                    Response.End();
                    Response.Close();
            

    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}