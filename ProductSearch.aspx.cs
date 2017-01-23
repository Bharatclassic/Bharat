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
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using CreatePDF;
using iTextSharp.text.pdf.parser;

public partial class Product_search : System.Web.UI.Page
{
    Dbconn dbcon = new Dbconn();
    protected static string button_select;
    protected string filename = Dbconn.Mymenthod();
    protected string strconn11 = Dbconn.conmenthod();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();

    PdfTableHead pdfHead = new PdfTableHead();
    DataTable dtsrch = new DataTable();
    DataRow drrow;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Visible = false;
        lblsuccess.Visible = false;
        lblerro.Visible = false;
        lblsucc.Visible = false;
        lblmerror.Visible = false;
        lblmsucc.Visible = false;

        if (!IsPostBack)
        {
            lblstock.Visible = false;
            lblshelf.Visible = false;
            lblrack.Visible = false;
            lblmrp.Visible = false;
            //Bind();
        }

        //txtmed.Text = "0";
        //Page.SetFocus(txtmed.Text);

        //(Panelmedicine.FindControl("txtmed") as TextBox).Focus();

        //(TabContainer1.FindControl("txtchem") as TextBox).Focus();

        //(tbpnlchemical.FindControl("txtchem") as TextBox).Focus();
        //var txtUserName = TabContainer1.FindControl("txtchem");
        //if (txtUserName == null)
        // {
        //Page.SetFocus(txtUserName);
        //ScriptManager.GetCurrent(Page).SetFocus(txtUserName);
        //txtUserName.Focus();
        //}

        //TextBox textBox = (TextBox)tbpnlchemical.FindControl("txtchem");
        //ScriptManager.GetCurrent(this).SetFocus(textBox); 
        ClientScript.RegisterStartupScript(this.GetType(), "SetInitialFocus", "<script>document.getElementById('" + txtchem.ClientID + "').focus();</script>");
        btnexit.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtchem.ClientID + "').focus();return false;}} else {return true}; ");


        ClientScript.RegisterStartupScript(this.GetType(), "SetInitialFocus", "<script>document.getElementById('" + txtgenename.ClientID + "').focus();</script>");
        btnexitgene.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtgenename.ClientID + "').focus();return false;}} else {return true}; ");

        ClientScript.RegisterStartupScript(this.GetType(), "SetInitialFocus", "<script>document.getElementById('" + txtmed.ClientID + "').focus();</script>");
        btnexitmed.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtmed.ClientID + "').focus();return false;}} else {return true}; ");
    }
    public void Bind()
    {
        if (!File.Exists(filename))
        {
            DataColumn col = new DataColumn("slno", typeof(int));
            col.AutoIncrement = true;
            col.AutoIncrementSeed = 1;
            col.AutoIncrementStep = 1;
            dtsrch.Columns.Add(col);
            dtsrch.Columns.Add("Medicine name");
            dtsrch.Columns.Add("Number of piece");
            dtsrch.Columns.Add("Manufacture Name");
            dtsrch.Columns.Add("Form");
            dtsrch.Columns.Add("MRP");
            Session["tblChemical"] = dtsrch;
        }
        else
        {
            DataColumn col = new DataColumn("slno", typeof(int));
            col.AutoIncrement = true;
            col.AutoIncrementSeed = 1;
            col.AutoIncrementStep = 1;
            dtsrch.Columns.Add(col);
            dtsrch.Columns.Add("Medicine name");
            dtsrch.Columns.Add("Number of piece");
            dtsrch.Columns.Add("Manufacture Name");
            dtsrch.Columns.Add("Form");
            dtsrch.Columns.Add("Price");
            Session["tblChemical"] = dtsrch;
        }
    }


    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        if (TabContainer1.ActiveTabIndex == 1)
        {
            txtchem.Enabled = true;
            txtchem.Focus();
        }
    }
    // protected void txtchem_TextChanged(object sender, EventArgs e)
    //{
    //   string chemical=txtchem.Text;
    //   string chemcode;
    //   int cmcode;

    // try
    // {
    //      DataSet dschem=ClsBLGD.GetcondDataSet("*","tblChemical","CC_name",chemical);
    //      if(dschem.Tables[0].Rows.Count>0)
    //      {
    //          chemcode=dschem.Tables[0].Rows[0]["CC_code"].ToString();
    //          cmcode=Convert.ToInt32(chemcode);
    //      }
    //      else
    //      {
    //          Master.ShowModal("Chemical Records not found","txtchem",0);
    //          return;
    //      }
    //      if (!File.Exists(filename))
    //      {
    //      SqlConnection conne=new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
    //      SqlCommand cmd=new SqlCommand ("SELECT c.FA_name,d.ManufactureName,e.formname,b.Pm_flag1,sum(f.MRP) as MRP FROM tblChemical as a JOIN tblProductMaster as b ON a.CC_code = b.Cemcode JOIN tblMedicine as c ON b.Medcode = c.FA_code join tblmanufacture as d ON b.Manufacturer=d.ManufactureCode join tblformmaster as e ON b.Form=e.formcode left join tblProductinward as f ON b.Productcode=f.Productcode WHERE a.CC_code='" + cmcode + "' group by FA_name,ManufactureName,formname,Pm_flag1",conne);
    //      SqlDataAdapter dachem = new SqlDataAdapter(cmd);
    //      DataSet ds = new DataSet();
    //      dachem.Fill(ds);
    //      if(ds.Tables[0].Rows.Count>0)
    //      {
    //          dtsrch=(DataTable)Session["tblChemical"];
    //          for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //          {
    //               drrow=dtsrch.NewRow();
    //               drrow["Medicine name"]=ds.Tables[0].Rows[0]["FA_name"].ToString();
    //               drrow["Number of piece"]=ds.Tables[0].Rows[0]["Pm_flag1"].ToString();
    //               drrow["Manufacture Name"]=ds.Tables[0].Rows[0]["ManufactureName"].ToString();
    //               drrow["Form"]=ds.Tables[0].Rows[0]["formname"].ToString();
    //               drrow["MRP"]=ds.Tables[0].Rows[0]["MRP"].ToString();
    //               dtsrch.Rows.Add(drrow);
    //          }
    //          chemdetails.DataSource=dtsrch;
    //          chemdetails.DataBind();
    //          GridDecorator.MergeRows(chemdetails);
    //      }
    //       else
    //       {
    //          Master.ShowModal("There are no Records to display. !!!", "txtBBillDate", 1);
    //          return;
    //       }

    //      }
    //      else
    //      {
    //      }
    // }

    //catch (Exception ex)
    //      {
    //          string asd = ex.Message;
    //          lblerror.Enabled = true;
    //          lblerror.Text = asd;
    //      }
    //  }
    protected void btnchem_Click(object sender, EventArgs e)
    {
        string chemical = txtchem.Text;
        string date = DateTime.Now.ToString("dd/MM/yyyy");
        if (chemdetails.Rows.Count > 0)
        {
            Document pdfBBill = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            MemoryStream memoryStream = new System.IO.MemoryStream();
            HTMLWorker htmlparser = new HTMLWorker(pdfBBill);
            PdfWriter.GetInstance(pdfBBill, Response.OutputStream);
            PdfWriter writer = PdfWriter.GetInstance(pdfBBill, memoryStream);
            PdfWriterEvents writerEvent = new PdfWriterEvents("chemical");
            writer.PageEvent = writerEvent;

            pdfBBill.Open();

            DataTable dtPdf = new DataTable();

            if (chemdetails.HeaderRow != null)
            {
                for (int i = 0; i < chemdetails.HeaderRow.Cells.Count; i++)
                {
                    dtPdf.Columns.Add(chemdetails.HeaderRow.Cells[i].Text);
                }
            }

            if (chemdetails.Rows.Count != 0)
            {
                foreach (GridViewRow row in chemdetails.Rows)
                {
                    DataRow drRow;
                    drRow = dtPdf.NewRow();
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        drRow[i] = row.Cells[i].Text;
                    }
                    dtPdf.Rows.Add(drRow);
                }
                Session["dtPdf"] = dtPdf;
            }
            PdfPCell GridCell = null;

            PdfPTable tblCChem = null;
            PdfPTable tblHead = null;
            PdfPTable tblDate = null;
            PdfPTable tblsubHeader = null;

            tblHead = new PdfPTable(1);
            tblHead.TotalWidth = 580f;
            tblHead.LockedWidth = true;
            tblHead.SetWidths(new float[] { 1f });

            tblCChem = new PdfPTable(chemdetails.HeaderRow.Cells.Count);
            tblCChem.TotalWidth = 580f;
            tblCChem.LockedWidth = true;
            tblCChem.SetWidths(new float[] { 0.4f, 1f, 0.6f, 0.6f, 1f, 1f });


            tblDate = new PdfPTable(1);
            tblDate.TotalWidth = 580f;
            tblDate.LockedWidth = true;
            tblDate.SetWidths(new float[] { 1f });

            tblsubHeader = new PdfPTable(1);
            tblsubHeader.LockedWidth = true;
            tblsubHeader.TotalWidth = 580f;
            tblsubHeader.SetWidths(new float[] { 1f });

            tblHead.AddCell(PhraseCell(new Phrase("Chemical composition", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
            tblHead.SpacingAfter = 10f;

            tblDate.AddCell(PhraseCell(new Phrase(date, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
            tblDate.SpacingAfter = 10f;

            GridCell = new PdfPCell(new Phrase(new Chunk("Sl No.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblCChem.AddCell(GridCell);

            GridCell = new PdfPCell(new Phrase(new Chunk("Medicine name", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblCChem.AddCell(GridCell);


            GridCell = new PdfPCell(new Phrase(new Chunk("Number of piece", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblCChem.AddCell(GridCell);


            GridCell = new PdfPCell(new Phrase(new Chunk("Manufacture Namer", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblCChem.AddCell(GridCell);


            GridCell = new PdfPCell(new Phrase(new Chunk("Form", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblCChem.AddCell(GridCell);


            GridCell = new PdfPCell(new Phrase(new Chunk("MRP", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
            GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblCChem.AddCell(GridCell);

            if (dtPdf != null)
            {
                for (int i = 0; i < dtPdf.Rows.Count; i++)
                {
                    for (int row = 0; row < dtPdf.Columns.Count; row++)
                    {
                        if (row == 3 || row == 4 || row == 5 || row == 6 || row == 7 || row == 8)
                        {
                            GridCell = new PdfPCell(new Phrase(new Chunk(dtPdf.Rows[i][row].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                            GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            tblCChem.AddCell(GridCell);
                        }
                        else
                        {
                            GridCell = new PdfPCell(new Phrase(new Chunk(dtPdf.Rows[i][row].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                            GridCell.HorizontalAlignment = 0;
                            tblCChem.AddCell(GridCell);
                        }
                    }
                }
            }
            DataSet dschemical = ClsBLGD.GetcondDataSet("*", "tblChemical", "CC_name", chemical);
            pdfBBill.Add(tblHead);

            tblsubHeader.AddCell(PhraseCell(new Phrase("Chemical Composition  :" + dschemical.Tables[0].Rows[0]["CC_name"].ToString(), FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
            tblsubHeader.SpacingAfter = 15f;
            // pdfBBill.Add(pdfHead.HeaderPart(new float[] { 1f }, 1, new string[] { dschemical.Tables[0].Rows[0]["CC_name"].ToString(), dschemical.Tables[0].Rows[0]["CC_code"].ToString() }, new Int32[] { 1 }));
            pdfBBill.Add(tblDate);
            pdfBBill.Add(tblsubHeader);

            pdfBBill.Add(tblCChem);

            pdfBBill.Close();

            chemdetails.DataSource = null;

            dtPdf.Rows.Clear();

            pdfBBill.Close();

            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=Search.pdf");
            Response.ContentType = "application/pdf";

            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);

            chemdetails.DataSource = null;
            chemdetails.DataBind();
            dtsrch.Rows.Clear();
            //ClsBLGD.ClearInputs(Page.Controls);
            //Bind();

            Response.End();
            Response.Close();

        }
        else
        {
            Master.ShowModal("There is nothing to print. !!!", "txtchemname", 1);
            return;
        }
        //tbpnlchemical.Enabled=true;
        txtchem.Focus();
    }


    //   protected void txtgenename_TextChanged(object sender, EventArgs e)
    // {
    //  string generic=txtgenename.Text;
    //  string genecode;
    //  int gncode;

    //try
    //{
    //     DataSet dsgene=ClsBLGD.GetcondDataSet("*","tblGeneric","GN_name",generic);
    //     if(dsgene.Tables[0].Rows.Count>0)
    //     {
    //         genecode=dsgene.Tables[0].Rows[0]["GN_code"].ToString();
    //         gncode=Convert.ToInt32(genecode);
    //     }
    //     else
    //     {
    //         Master.ShowModal("Generic Records not found","txtgenename",0);
    //         return;
    //     }
    //    SqlConnection conn=new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
    //    SqlCommand cmd1=new SqlCommand("SELECT c.FA_name,d.ManufactureName,e.formname,b.Pm_flag1,sum(f.MRP) as MRP FROM tblGeneric as a JOIN tblProductMaster as b ON a.GN_code = b.Genericcode JOIN tblMedicine as c ON b.Medcode = c.FA_code join tblmanufacture as d ON b.Manufacturer=d.ManufactureCode join tblformmaster as e ON b.Form=e.formcode left join tblProductinward as f ON b.Productcode=f.Productcode WHERE a.GN_code='" + gncode +"' group by FA_name,ManufactureName,formname,Pm_flag1",conn);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd1);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //       if(ds.Tables[0].Rows.Count>0)
    //       {
    //           dtsrch=(DataTable)Session["tblChemical"];
    //             for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //            {
    //                 drrow=dtsrch.NewRow();
    //             //      drBBills["Farmer Name"] = dsCustDet.Tables[0].Rows[0]["CustName"].ToString();
    //                 drrow["Medicine name"]=ds.Tables[0].Rows[0]["FA_name"].ToString();
    //                 drrow["Number of piece"]=ds.Tables[0].Rows[0]["Pm_flag1"].ToString();
    //                 drrow["Manufacture Name"]=ds.Tables[0].Rows[0]["ManufactureName"].ToString();
    //                 drrow["Form"]=ds.Tables[0].Rows[0]["formname"].ToString();
    //                 drrow["MRP"]=ds.Tables[0].Rows[0]["MRP"].ToString();
    //                 dtsrch.Rows.Add(drrow);
    //            }
    //           genedetails.DataSource=dtsrch;
    //           genedetails.DataBind();
    //            GridDecorator.MergeRows(chemdetails);
    //       }
    //     else
    //      {
    //         Master.ShowModal("There are no Records to display. !!!", "txtBBillDate", 1);
    //      }
    // }
    //    catch (Exception ex)
    //     {
    //         string asd = ex.Message;
    //         lblerro.Enabled = true;
    //         lblerro.Text = asd;
    //     }
    // }



    protected void btngene_Click(object sender, EventArgs e)
    {
        string generic = txtgenename.Text;
        string date = DateTime.Now.ToString("dd/MM/yyyy");
        if (genedetails.Rows.Count > 0)
        {
            Document pdfBBill = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            MemoryStream memoryStream = new System.IO.MemoryStream();
            HTMLWorker htmlparser = new HTMLWorker(pdfBBill);
            PdfWriter.GetInstance(pdfBBill, Response.OutputStream);
            PdfWriter writer = PdfWriter.GetInstance(pdfBBill, memoryStream);
            PdfWriterEvents writerEvent = new PdfWriterEvents("Generic");
            writer.PageEvent = writerEvent;

            pdfBBill.Open();

            DataTable dtPdf = new DataTable();

            if (genedetails.HeaderRow != null)
            {
                for (int i = 0; i < genedetails.HeaderRow.Cells.Count; i++)
                {
                    dtPdf.Columns.Add(genedetails.HeaderRow.Cells[i].Text);
                }
            }

            if (genedetails.Rows.Count != 0)
            {
                foreach (GridViewRow row in genedetails.Rows)
                {
                    DataRow drRow;
                    drRow = dtPdf.NewRow();
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        drRow[i] = row.Cells[i].Text;
                    }
                    dtPdf.Rows.Add(drRow);
                }
                Session["dtPdf"] = dtPdf;
            }
            PdfPCell GridCell = null;

            PdfPTable tblGGene = null;
            PdfPTable tblHead = null;
            PdfPTable tblDate = null;
            PdfPTable tblsubHeader = null;

            tblHead = new PdfPTable(1);
            tblHead.TotalWidth = 580f;
            tblHead.LockedWidth = true;
            tblHead.SetWidths(new float[] { 1f });

            tblGGene = new PdfPTable(genedetails.HeaderRow.Cells.Count);
            tblGGene.TotalWidth = 580f;
            tblGGene.LockedWidth = true;
            tblGGene.SetWidths(new float[] { 0.4f, 1f, 0.6f, 0.6f, 1f, 1f });


            tblDate = new PdfPTable(1);
            tblDate.TotalWidth = 580f;
            tblDate.LockedWidth = true;
            tblDate.SetWidths(new float[] { 1f });

            tblsubHeader = new PdfPTable(1);
            tblsubHeader.LockedWidth = true;
            tblsubHeader.TotalWidth = 580f;
            tblsubHeader.SetWidths(new float[] { 1f });
            if (!File.Exists(filename))
            {
                tblHead.AddCell(PhraseCell(new Phrase("Generic", FontFactory.GetFont("Times", 13, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                tblHead.SpacingAfter = 10f;

                tblDate.AddCell(PhraseCell(new Phrase(date, FontFactory.GetFont("Times", 12, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                tblDate.SpacingAfter = 10f;

                GridCell = new PdfPCell(new Phrase(new Chunk("Sl No.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tblGGene.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Medicine name", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tblGGene.AddCell(GridCell);


                GridCell = new PdfPCell(new Phrase(new Chunk("Number of piece", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tblGGene.AddCell(GridCell);


                GridCell = new PdfPCell(new Phrase(new Chunk("Manufacture Namer", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tblGGene.AddCell(GridCell);


                GridCell = new PdfPCell(new Phrase(new Chunk("Form", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tblGGene.AddCell(GridCell);


                GridCell = new PdfPCell(new Phrase(new Chunk("MRP", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tblGGene.AddCell(GridCell);
            }
            else
            {
                tblHead.AddCell(PhraseCell(new Phrase("Generic", FontFactory.GetFont("Times", 13, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                tblHead.SpacingAfter = 10f;

                tblDate.AddCell(PhraseCell(new Phrase(date, FontFactory.GetFont("Times", 12, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                tblDate.SpacingAfter = 10f;

                GridCell = new PdfPCell(new Phrase(new Chunk("Sl No.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tblGGene.AddCell(GridCell);

                GridCell = new PdfPCell(new Phrase(new Chunk("Medicine name", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tblGGene.AddCell(GridCell);


                GridCell = new PdfPCell(new Phrase(new Chunk("Number of piece", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tblGGene.AddCell(GridCell);


                GridCell = new PdfPCell(new Phrase(new Chunk("Manufacture Namer", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tblGGene.AddCell(GridCell);


                GridCell = new PdfPCell(new Phrase(new Chunk("Form", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tblGGene.AddCell(GridCell);


                GridCell = new PdfPCell(new Phrase(new Chunk("Price", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tblGGene.AddCell(GridCell);
            }

            if (dtPdf != null)
            {
                for (int i = 0; i < dtPdf.Rows.Count; i++)
                {
                    for (int row = 0; row < dtPdf.Columns.Count; row++)
                    {
                        if (row == 3 || row == 4 || row == 5 || row == 6 || row == 7 || row == 8)
                        {
                            GridCell = new PdfPCell(new Phrase(new Chunk(dtPdf.Rows[i][row].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                            GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            tblGGene.AddCell(GridCell);
                        }
                        else
                        {
                            GridCell = new PdfPCell(new Phrase(new Chunk(dtPdf.Rows[i][row].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                            GridCell.HorizontalAlignment = 0;
                            tblGGene.AddCell(GridCell);
                        }
                    }
                }
            }

            DataSet dsgeneric = ClsBLGD.GetcondDataSet("*", "tblGeneric", "GN_name", generic);
            pdfBBill.Add(tblHead);

            tblsubHeader.AddCell(PhraseCell(new Phrase("Generic  :" + dsgeneric.Tables[0].Rows[0]["GN_name"].ToString(), FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
            tblsubHeader.SpacingAfter = 15f;
            // pdfBBill.Add(pdfHead.HeaderPart(new float[] { 1f }, 1, new string[] { dschemical.Tables[0].Rows[0]["CC_name"].ToString(), dschemical.Tables[0].Rows[0]["CC_code"].ToString() }, new Int32[] { 1 }));
            pdfBBill.Add(tblDate);
            pdfBBill.Add(tblsubHeader);
            pdfBBill.Add(tblGGene);

            pdfBBill.Close();

            genedetails.DataSource = null;

            dtPdf.Rows.Clear();

            pdfBBill.Close();

            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=BBill.pdf");
            Response.ContentType = "application/pdf";

            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);


            ClsBLGD.ClearInputs(Page.Controls);
            Bind();

            Response.AddHeader("Refresh", "1;URL=ProductSearch.aspx");
            tbgeneric.Enabled = true;
            tbgeneric.Focus();
            Response.End();
        }
        else
        {
            Master.ShowModal("There is nothing to print. !!!", "txtchemname", 1);
        }
    }
    protected void btnmed_Click(object sender, EventArgs e)
    {
        string med = txtmed.Text;
        string stk = null;
        int stock;
        string rack = null;
        int rac;
        string shelf = null;
        string mrp = null;
        int mrprt;
        string medcode;
        int mdcod;
        try
        {
            DataSet ds6 = ClsBLGD.GetcondDataSet("*", "tblProductMaster", "Productname", med);
            if (ds6.Tables[0].Rows.Count > 0)
            {
                //string cod = lblcode.Text;
                //int c = Convert.ToInt32(cod);
                medcode = ds6.Tables[0].Rows[0]["Productcode"].ToString();
                mdcod = Convert.ToInt32(medcode);
            }
            else
            {
                Master.ShowModal("Medicine Records not found", "txtmed", 0);
                return;
            }
            if (!File.Exists(filename))
            {
                SqlConnection cone = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
                SqlCommand cmd = new SqlCommand("select b.Stockinhand,sum(b.MRP) as MRP,c.Se_name,c.srcount from tblProductMaster as a join tblProductinward as b on a.Productcode=(CASE WHEN (isnumeric(b.Productcode) = 1) THEN CAST( b.Productcode AS bigint ) ELSE 0 END) join tblShelf as c on (CASE WHEN (isnumeric(b.se_code) = 1) THEN CAST( b.se_code AS bigint ) ELSE 0 END)=c.Se_code where a.Productcode='" + mdcod + "' group by Stockinhand,Se_name,srcount", cone);
                //  SqlCommand cmd=new SqlCommand("select a.FA_name,c.srcount,c.Se_name,b.Stockinhand,b.MRP FROM tblMedicine as a join tblProductinward as b on a.FA_code=b.FA_code join tblShelf as c on b.se_code=c.Se_code where a.FA_code='" + mdcod + "'",cone);
                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
                DataSet dataset1 = new DataSet();
                adapter1.Fill(dataset1);
                if (dataset1.Tables[0].Rows.Count > 0)
                {
                    stk = dataset1.Tables[0].Rows[0]["Stockinhand"].ToString();
                    stock = Convert.ToInt32(stk);
                    rack = dataset1.Tables[0].Rows[0]["srcount"].ToString();
                    rac = Convert.ToInt32(rack);
                    shelf = dataset1.Tables[0].Rows[0]["Se_name"].ToString();
                    mrp = dataset1.Tables[0].Rows[0]["MRP"].ToString();
                    mrprt = Convert.ToInt32(mrp);

                    lblstock.Text = Convert.ToInt32(stk).ToString();
                    lblshelf.Text = shelf;
                    if (lblshelf.Text == "")
                    {
                        lblshelf.Text = null;
                    }
                    lblrack.Text = Convert.ToInt32(rack).ToString();
                    lblmrp.Text = Convert.ToInt32(mrp).ToString();
                    lblstock.Visible = true;
                    lblshelf.Visible = true;
                    lblrack.Visible = true;
                    lblmrp.Visible = true;
                }
                else
                {
                    Master.ShowModal("No records available", "txtmed", 0);
                    return;
                }
            }

            else
            {
                OleDbConnection conn1 = new OleDbConnection(strconn11);
                conn1.Open();
                OleDbCommand cmd1 = new OleDbCommand("select Stockinhand,Se_name,srcount,sum(MRP) as Price from((tblMedicine inner join tblProductInward on CStr(tblMedicine.FA_code)=tblProductinward.FA_code) inner join tblShelf on CStr(tblShelf.Se_code)=tblProductinward.se_code) where tblMedicine.FA_code=" + mdcod + " group by Stockinhand,Se_name,srcount", conn1);

                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    stk = ds1.Tables[0].Rows[0]["Stockinhand"].ToString();
                    stock = Convert.ToInt32(stk);
                    rack = ds1.Tables[0].Rows[0]["srcount"].ToString();
                    rac = Convert.ToInt32(rack);
                    shelf = ds1.Tables[0].Rows[0]["Se_name"].ToString();
                    mrp = ds1.Tables[0].Rows[0]["Price"].ToString();
                    mrprt = Convert.ToInt32(mrp);

                    lblstock.Text = Convert.ToInt32(stk).ToString();
                    lblshelf.Text = shelf;
                    if (lblshelf.Text == "")
                    {
                        lblshelf.Text = null;
                    }
                    lblrack.Text = Convert.ToInt32(rack).ToString();
                    lblmrp.Text = Convert.ToInt32(mrp).ToString();
                    lblstock.Visible = true;
                    lblshelf.Visible = true;
                    lblrack.Visible = true;
                    lblmrp.Visible = true;
                }
                else
                {
                    Master.ShowModal("No records available", "txtmed", 0);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Enabled = true;
            lblerror.Text = asd;
        }

    }
    protected void btnexitmed_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void btnexit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void btnexitgene_Click(object sender, EventArgs e)
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

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Buyername(string prefixText)
    {
        string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            string oConn = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(oConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select CC_name from tblChemical where CC_name like @1+'%'", conn);
            cmd.Parameters.AddWithValue("@1", prefixText);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<string> buyernames = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                buyernames.Add(dt.Rows[i][0].ToString());
            }
            return buyernames;
        }
        else
        {
            string strconn1 = Dbconn.conmenthod();
            OleDbConnection conn = new OleDbConnection(strconn1);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select CC_name from tblChemical where CC_name like @1+'%'", conn);
            cmd.Parameters.AddWithValue("@1", prefixText);
            OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            List<string> buyernames = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                buyernames.Add(dt.Rows[i][0].ToString());
            }
            return buyernames;
        }
    }



    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Buyername10(string prefixText)
    {
        string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            string oConn = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(oConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select  Productname from tblProductMaster where Productname like @1+'%'", conn);
            cmd.Parameters.AddWithValue("@1", prefixText);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<string> buyernames = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                buyernames.Add(dt.Rows[i][0].ToString());
            }
            return buyernames;
        }
        else
        {
            string strconn1 = Dbconn.conmenthod();
            OleDbConnection conn = new OleDbConnection(strconn1);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select CC_name from tblChemical where CC_name like @1+'%'", conn);
            cmd.Parameters.AddWithValue("@1", prefixText);
            OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            List<string> buyernames = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                buyernames.Add(dt.Rows[i][0].ToString());
            }
            return buyernames;
        }
    }














    [WebMethod]
    public static string[] Getmedicine(string prefix)
    {

        string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                // conn.ConnectionString = @"Data Source=VAGI-7-PC;Initial Catalog=Pharmacy;Integrated Security=False;User ID=sa;Password=vagi0903";
                conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select FA_name from tblMedicine where FA_name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["FA_name"]));
                        }
                    }
                    conn.Close();
                }
            }
            return customers.ToArray();
        }
        else
        {
            List<string> customers = new List<string>();
            string strconn1 = Dbconn.conmenthod();
            using (OleDbConnection conn = new OleDbConnection(strconn1))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {

                    cmd.CommandText = "select FA_name from tblMedicine where FA_name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (OleDbDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["FA_name"]));
                        }
                    }
                    conn.Close();
                }

            }
            return customers.ToArray();

        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Buyername1(string prefixText)
    {
        string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            string oConn = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(oConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select GN_name from tblGeneric where GN_name like @1+'%'", conn);
            cmd.Parameters.AddWithValue("@1", prefixText);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<string> buyernames = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                buyernames.Add(dt.Rows[i][0].ToString());
            }
            return buyernames;
        }
        else
        {
            string strconn1 = Dbconn.conmenthod();
            OleDbConnection conn = new OleDbConnection(strconn1);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select GN_name from tblGeneric where GN_name like @1+'%'", conn);
            cmd.Parameters.AddWithValue("@1", prefixText);
            OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            List<string> buyernames = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                buyernames.Add(dt.Rows[i][0].ToString());
            }
            return buyernames;
        }
    }

    protected void btnchserch_Click(object sender, EventArgs e)
    {
        Bind();
        string chemical = txtchem.Text;
        string chemcode;
        int cmcode;
        try
        {
            DataSet dschem = ClsBLGD.GetcondDataSet("*", "tblChemical", "CC_name", chemical);
            if (dschem.Tables[0].Rows.Count > 0)
            {
                chemcode = dschem.Tables[0].Rows[0]["CC_code"].ToString();
                cmcode = Convert.ToInt32(chemcode);
            }
            else
            {
                Master.ShowModal("Chemical Records not found", "txtchem", 0);
                return;
            }
            if (!File.Exists(filename))
            {

                SqlConnection conne = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
                SqlCommand cmd = new SqlCommand("SELECT c.FA_name,d.ManufactureName,e.formname,b.Pm_flag1,sum(f.MRP) as MRP FROM tblChemical as a JOIN tblProductMaster as b ON a.CC_code = b.Cemcode JOIN tblMedicine as c ON b.Medcode = c.FA_code join tblmanufacture as d ON b.Manufacturer=d.ManufactureCode join tblformmaster as e ON b.Form=e.formcode left join tblProductinward as f ON b.Productcode=f.Productcode WHERE a.CC_code='" + cmcode + "' group by FA_name,ManufactureName,formname,Pm_flag1", conne);
                SqlDataAdapter dachem = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dachem.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtsrch = (DataTable)Session["tblChemical"];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        drrow = dtsrch.NewRow();

                        drrow["Medicine name"] = ds.Tables[0].Rows[0]["FA_name"].ToString();
                        drrow["Number of piece"] = ds.Tables[0].Rows[0]["Pm_flag1"].ToString();
                        drrow["Manufacture Name"] = ds.Tables[0].Rows[0]["ManufactureName"].ToString();
                        drrow["Form"] = ds.Tables[0].Rows[0]["formname"].ToString();
                        drrow["MRP"] = ds.Tables[0].Rows[0]["MRP"].ToString();
                        dtsrch.Rows.Add(drrow);
                    }
                    // DataView dw = dtsrch.DefaultView;
                    chemdetails.DataSource = dtsrch;
                    chemdetails.DataBind();
                }
                else
                {
                    Master.ShowModal("There are no Records to display. !!!", "txtchem", 1);
                    chemdetails.Visible = false;
                    return;
                }

            }
            else
            {

                OleDbConnection conn1 = new OleDbConnection(Dbconn.conmenthod());
                conn1.Open();

                OleDbCommand cmd1 = new OleDbCommand("Select FA_name,ManufactureName,formname,Pm_flag1,sum(MRP) as Price  from (((((tblMedicine  inner join tblProductMaster  on CStr(tblMedicine.FA_code)=tblProductMaster.Medcode) inner join tblmanufacture on CStr(tblmanufacture.ManufactureCode)=tblProductMaster.Manufacturer)inner join tblformmaster on CStr(tblformmaster.formcode)=tblProductMaster.Form)left join  tblProductinward on  tblProductinward.Productcode=tblProductMaster.Productcode)inner join tblChemical on CStr(tblChemical.CC_code)=tblProductMaster.Cemcode)where tblChemical.CC_code=" + cmcode + " group by FA_name,ManufactureName,formname,Pm_flag1", conn1);
                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    dtsrch = (DataTable)Session["tblChemical"];
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        drrow = dtsrch.NewRow();

                        drrow["Medicine name"] = ds1.Tables[0].Rows[0]["FA_name"].ToString();
                        drrow["Number of piece"] = ds1.Tables[0].Rows[0]["Pm_flag1"].ToString();
                        drrow["Manufacture Name"] = ds1.Tables[0].Rows[0]["ManufactureName"].ToString();
                        drrow["Form"] = ds1.Tables[0].Rows[0]["formname"].ToString();
                        drrow["Price"] = ds1.Tables[0].Rows[0]["Price"].ToString();
                        dtsrch.Rows.Add(drrow);
                    }
                    // DataView dw = dtsrch.DefaultView;
                    chemdetails.DataSource = dtsrch;
                    chemdetails.DataBind();

                    //drrow["Medicine name"]=string.Empty;
                    //drrow["Number of piece"]=string.Empty;
                    //drrow["Manufacture Name"]=string.Empty;
                    //drrow["Form"]=string.Empty;
                    //drrow["Price"]=string.Empty;
                    //  DataColumn col = new DataColumn("slno", typeof(int));
                    //chemdetails.DataSource=dtsrch;
                    //chemdetails.DataBind();
                    dtsrch.Rows.Clear();

                }
                else
                {
                    Master.ShowModal("There are no Records to display. !!!", "txtchem", 1);
                    dtsrch.Rows.Clear();
                    return;
                }
            }
        }

        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Enabled = true;
            lblerror.Text = asd;
        }
    }
    protected void btngesrch_Click(object sender, EventArgs e)
    {
        Bind();
        string generic = txtgenename.Text;
        string genecode;
        int gncode;

        try
        {
            DataSet dsgene = ClsBLGD.GetcondDataSet("*", "tblGeneric", "GN_name", generic);
            if (dsgene.Tables[0].Rows.Count > 0)
            {
                genecode = dsgene.Tables[0].Rows[0]["GN_code"].ToString();
                gncode = Convert.ToInt32(genecode);
            }
            else
            {
                Master.ShowModal("Generic Records not found", "txtgenename", 0);
                return;
            }
            if (!File.Exists(filename))
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
                SqlCommand cmd1 = new SqlCommand("SELECT c.FA_name,d.ManufactureName,e.formname,b.Pm_flag1,sum(f.MRP) as MRP FROM tblGeneric as a JOIN tblProductMaster as b ON a.GN_code = b.Genericcode JOIN tblMedicine as c ON b.Medcode = c.FA_code join tblmanufacture as d ON b.Manufacturer=d.ManufactureCode join tblformmaster as e ON b.Form=e.formcode left join tblProductinward as f ON b.Productcode=f.Productcode WHERE a.GN_code='" + gncode + "' group by FA_name,ManufactureName,formname,Pm_flag1", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtsrch = (DataTable)Session["tblChemical"];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        drrow = dtsrch.NewRow();
                        // drrow["slno"] = ds.Tables[0].Rows[0]["CustName"].ToString();
                        drrow["Medicine name"] = ds.Tables[0].Rows[0]["FA_name"].ToString();
                        drrow["Number of piece"] = ds.Tables[0].Rows[0]["Pm_flag1"].ToString();
                        drrow["Manufacture Name"] = ds.Tables[0].Rows[0]["ManufactureName"].ToString();
                        drrow["Form"] = ds.Tables[0].Rows[0]["formname"].ToString();
                        drrow["MRP"] = ds.Tables[0].Rows[0]["MRP"].ToString();
                        dtsrch.Rows.Add(drrow);
                    }
                    genedetails.DataSource = dtsrch;
                    genedetails.DataBind();
                    dtsrch.Rows.Clear();
                    //drrow["Medicine name"]=string.Empty;
                    //drrow["Number of piece"]=string.Empty;
                    //drrow["Manufacture Name"]=string.Empty;
                    //drrow["Form"]=string.Empty;
                    //drrow["MRP"]=string.Empty;
                    ////DataColumn col = new DataColumn("slno", typeof(int));
                    //drrow["slno"]=1;
                    //genedetails.DataSource=dtsrch;
                    //genedetails.DataBind();
                    //dtsrch.Rows.Clear();
                }
                else
                {
                    Master.ShowModal("There are no Records to display", "txtgenename", 1);
                    genedetails.Visible = false;
                    return;
                }
            }

            else
            {
                OleDbConnection conn1 = new OleDbConnection(Dbconn.conmenthod());
                conn1.Open();

                OleDbCommand cmd1 = new OleDbCommand("Select FA_name,ManufactureName,formname,Pm_flag1,sum(MRP) as Price from (((((tblMedicine  inner join tblProductMaster  on CStr(tblMedicine.FA_code)=tblProductMaster.Medcode) inner join tblmanufacture on CStr(tblmanufacture.ManufactureCode)=tblProductMaster.Manufacturer)inner join tblformmaster on CStr(tblformmaster.formcode)=tblProductMaster.Form)left join  tblProductinward on  tblProductinward.Productcode=tblProductMaster.Productcode)inner join tblGeneric on CStr(tblGeneric.GN_code)=tblProductMaster.Genericcode)where tblGeneric.GN_code=" + gncode + " group by FA_name,ManufactureName,formname,Pm_flag1", conn1);
                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    dtsrch = (DataTable)Session["tblChemical"];
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        drrow = dtsrch.NewRow();
                        // drrow["slno"] = ds.Tables[0].Rows[0]["CustName"].ToString();
                        drrow["Medicine name"] = ds1.Tables[0].Rows[0]["FA_name"].ToString();
                        drrow["Number of piece"] = ds1.Tables[0].Rows[0]["Pm_flag1"].ToString();
                        drrow["Manufacture Name"] = ds1.Tables[0].Rows[0]["ManufactureName"].ToString();
                        drrow["Form"] = ds1.Tables[0].Rows[0]["formname"].ToString();
                        drrow["Price"] = ds1.Tables[0].Rows[0]["Price"].ToString();
                        dtsrch.Rows.Add(drrow);
                    }
                    // DataView dw = dtsrch.DefaultView;
                    genedetails.DataSource = dtsrch;
                    genedetails.DataBind();
                    dtsrch.Rows.Clear();
                    //  drrow["Medicine name"]=string.Empty;
                    //  drrow["Number of piece"]=string.Empty;
                    //  drrow["Manufacture Name"]=string.Empty;
                    //  drrow["Form"]=string.Empty;
                    //  drrow["Price"]=string.Empty;
                    ////  DataColumn col = new DataColumn("slno", typeof(int));

                }

                else
                {
                    Master.ShowModal("Records not available", "txtgenename", 1);
                    return;
                }
            }
        }


        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Enabled = true;
            lblerror.Text = asd;
        }
    }

}


// protected void btnchem_Click(object sender, EventArgs e)
//{
//    string chem=txtchem.Text;
//    string cname=null;
//    string code=null;
//    int ccode;
//    string medname=null;
//     string medicine;
//     int medcode;
//     string form=null;
//     int formcode;
//     string formname;
//     string manu=null;
//     string manufacturer;
//     int manucode;
//     string MRP=null;
//     string nupiece=null;
//     try
//    {
//        // SqlConnection con=new SqlConnection 
//         DataSet dss = ClsBLGD.GetcondDataSet("*", "tblChemical", "CC_name", chem);
//         for(int i=0;i<dss.Tables[0].Rows.Count;i++)
//         {
//             if(dss.Tables[0].Rows.Count>0)
//             {
//             code=dss.Tables[0].Rows[0]["CC_code"].ToString();
//             ccode=Convert.ToInt32(code);
//             }
//             else
//             {
//                 Master.ShowModal("Chemical not found","txtchem",0);
//                 return;
//             }
//         }

//         DataSet ds1=ClsBLGD.GetcondDataSet("*","tblProductMaster","CC_code",code);
//         //for(int j=0;j<ds1.Tables[0].Rows.Count;j++)
//         //{
//             if(ds1.Tables[0].Rows.Count>0)
//             {
//             medname=ds1.Tables[0].Rows[0]["Medcode"].ToString();
//             medcode=Convert.ToInt32(medname);
//             form=ds1.Tables[0].Rows[0]["Form"].ToString();
//             formcode=Convert.ToInt32(form);
//             manu=ds1.Tables[0].Rows[0]["Manufacturer"].ToString();
//             manucode=Convert.ToInt32(manu);
//             }
//             else
//             {
//                 Master.ShowModal("Product not found","txtchem",0);
//                 return;
//             }
//        // }
//         DataSet ds2=ClsBLGD.GetcondDataSet("*","tblMedicine","FA_code",medname);
//         {
//             if(ds2.Tables[0].Rows.Count>0)
//             {
//             medicine=ds2.Tables[0].Rows[0]["FA_name"].ToString();
//             }
//             else
//             {
//                  Master.ShowModal("Medicine not found","txtchem",0);
//                 return;
//             }
//         }
//         DataSet ds3=ClsBLGD.GetcondDataSet("*","tblformmaster","formcode",form);
//         {
//             if(ds3.Tables[0].Rows.Count>0)
//             {
//              formname=ds3.Tables[0].Rows[0]["formname"].ToString();
//             }
//             else
//             {
//                  Master.ShowModal("form not found","txtchem",0);
//                  return;
//             }
//         }
//         DataSet ds4=ClsBLGD.GetcondDataSet("*","tblmanufacture","ManufactureCode",manu);
//         {
//             if(ds4.Tables[0].Rows.Count>0)
//             {
//             manufacturer=ds4.Tables[0].Rows[0]["ManufactureName"].ToString();
//             }
//             else
//             {
//                    Master.ShowModal("Manufacture not found","txtchem",0);
//                  return;
//             }
//         }
//         DataSet ds5=ClsBLGD.GetcondDataSet("*","tblProductinward","CC_code",code);
//         if(ds5.Tables[0].Rows.Count>0)
//         { 
//             MRP=ds5.Tables[0].Rows[0]["MRP"].ToString();
//             nupiece=ds5.Tables[0].Rows[0]["Stockinward"].ToString();
//         }
//         else
//         {
//             Master.ShowModal("MRP or num of piece not found","txtchem",0);
//             return;
//         }
//     }
//          catch(Exception ex)
//     {
//     }
// }

//   Gridchemical.DataSource = null;
//   Gridchemical.DataBind();
//   tblgridchem.Rows.Clear();

//    DataColumn col = new DataColumn("slno", typeof(int));
//    col.AutoIncrement = true;
//    col.AutoIncrementSeed = 1;
//    col.AutoIncrementStep = 1;
//    tblgridchem.Columns.Add(col);
//    tblgridchem.Columns.Add("MEDICINE NAME");
//    tblgridchem.Columns.Add("NUMBER OF PIECE");
//    tblgridchem.Columns.Add("MANUFACTURE NAME");
//    tblgridchem.Columns.Add("FORM NAME");
//    tblgridchem.Columns.Add("MRP");
//    Session["Chemical"] = tblgridchem;
//     for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
//     {
//       tblgridchem = (DataTable)Session["Chemical"];
//        drrw = tblgridchem.NewRow();

//        drrw["MEDICINE NAME"] = ds2.Tables[0].Rows[i]["FA_name"].ToString();
//        drrw["NUMBER OF PIECE"]=ds5.Tables[0].Rows[i]["Stockinward"].ToString();
//        drrw["MANUFACTURE NAME"] = ds4.Tables[0].Rows[i]["ManufactureName"].ToString();
//        drrw["FORM NAME"] = ds3.Tables[0].Rows[i]["formname"].ToString();
//        drrw["MRP"]=ds5.Tables[0].Rows[i]["MRP"].ToString();
//        tblgridchem.Rows.Add(drrw);

//     }         
//        DataView dw = tblgridchem.DefaultView;
//        dw.Sort = "slno ASC";
//        Gridchemical.DataSource = tblgridchem;
//        Gridchemical.DataBind();
//    // addins();
//}


//    catch(Exception ex)
//    {
//    }
//}
//public void addins()
//{
//    string medname=Gridchemical.Rows[0].Cells[0].Text;
//    string numpiece=Gridchemical.Rows[0].Cells[1].Text;
//    string manufac=Gridchemical.Rows[0].Cells[2].Text;
//    string formnm=Gridchemical.Rows[0].Cells[3].Text;
//    string mp=Gridchemical.Rows[0].Cells[4].Text;


//}



// protected void Gridchemical_PageIndexChanging(object sender, GridViewPageEventArgs e)
//{

//        Gridchemical.PageIndex = e.NewPageIndex;
//        Bind();

//}










