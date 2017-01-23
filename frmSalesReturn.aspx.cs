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
using System.Drawing; 

public partial class frmSalesReturn : System.Web.UI.Page
{
    ClsBLLGeneraldetails clsgd = new ClsBLLGeneraldetails();
    ClsBALSalesReturn ClsBLGP = new ClsBALSalesReturn();
    ClsBALProductinward Clsprdinw = new ClsBALProductinward();
    ClsBALTransaction ClsBLGP2 = new ClsBALTransaction();
    //ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
    DataTable tblsalereturn = new DataTable();
    protected static string button_select;
    string sMacAddress = "";
    DataRow drrw;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtbillno.Focus();
        }

    }
    protected void txtbillno_TextChanged(object sender, EventArgs e)
    {
        try{
        GridView1.DataSource = null;
                GridView1.DataBind();
                tblsalereturn.Rows.Clear();
                SqlConnection con = new SqlConnection(strconn11);
                SqlCommand cmd = new SqlCommand("SELECT * from tblProductsale where Invoiceno='" + txtbillno.Text + "' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblsalereturn.Columns.Add(col);
                    tblsalereturn.Columns.Add("Product Name");
                    tblsalereturn.Columns.Add("Batch No");
                    tblsalereturn.Columns.Add("Expiry Date");
                    tblsalereturn.Columns.Add("Sales_Qty");
                    tblsalereturn.Columns.Add("Sales_Price");
                    tblsalereturn.Columns.Add("Return_Qty");
                    tblsalereturn.Columns.Add("Totalamt");
                    //tblsalereturn.Columns.Add("Rate");
                   

                    Session["Group"] = tblsalereturn;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        tblsalereturn = (DataTable)Session["Group"];
                        drrw = tblsalereturn.NewRow();
                        drrw["Product Name"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                        drrw["Batch No"] = ds.Tables[0].Rows[i]["Batchno"].ToString();
                        DateTime date1 =Convert.ToDateTime(ds.Tables[0].Rows[i]["Expiredate"].ToString());
                        drrw["Expiry Date"] = date1.ToString("dd/MM/yyyy");
                        drrw["Sales_Qty"] = ds.Tables[0].Rows[i]["Quantity"].ToString();
                        drrw["Sales_Price"] = ds.Tables[0].Rows[i]["selprice"].ToString();
                        drrw["Return_Qty"] = "";
                        drrw["Totalamt"] = "";
                        //drrw["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();
                         tblsalereturn.Rows.Add(drrw);
                    }
                    DataView dw = tblsalereturn.DefaultView;
                    dw.Sort = "SLNO ASC";
                    GridView1.DataSource = tblsalereturn;
                    GridView1.DataBind();
                    (GridView1.Rows[0].Cells[4].FindControl("Return_Qty") as TextBox).Focus();
                }
                //TextBox txt = (TextBox)sender;
                //GridViewRow row = (GridViewRow)txt.NamingContainer;
                
            }
            catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Enabled = true;
                lblerror.Text = asd;
            }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string invoiceno;
                string name = GridView1.Rows[i].Cells[0].Text;
                string batchid = GridView1.Rows[i].Cells[1].Text;
                DataSet ds10 = clsgd.GetcondDataSet3("*", "tblProductsale", "ProductName", name, "Batchno", batchid, "Invoiceno", txtbillno.Text);
               
                invoiceno = ds10.Tables[0].Rows[0]["Sale_falg6"].ToString(); 
                
                double rqty1;
                double tot11;
                rqty1 = Convert.ToDouble((GridView1.Rows[i].Cells[4].FindControl("Return_Qty") as TextBox).Text);
                //tot11 = Convert.ToDouble((GridView1.Rows[i].Cells[5].FindControl("Totalamt") as TextBox).Text);
                tot11 = Convert.ToDouble(GridView1.Rows[i].Cells[6].Text);

                //DataSet dsproin = clsgd.GetcondDataSet4("*", "tblProductinward", "ProductName", name, "Batchid", batchid, "Invoiceno", invoiceno);
                DataSet dsproin = clsgd.GetcondDataSet3("*", "tblProductinward", "ProductName", name, "Batchid", batchid, "Invoiceno", invoiceno);
                if (dsproin.Tables[0].Rows.Count > 0)
                {
                    string transno = dsproin.Tables[0].Rows[0]["TransNo"].ToString();
                    string invoicedate = dsproin.Tables[0].Rows[0]["Invoicedate"].ToString();
                    string ca = dsproin.Tables[0].Rows[0]["Paymenttype"].ToString();
                    string paymflag = dsproin.Tables[0].Rows[0]["Paymentflag"].ToString();
                    string indate = dsproin.Tables[0].Rows[0]["Indate"].ToString();
                    string prodcode = dsproin.Tables[0].Rows[0]["Productcode"].ToString();
                    string gcode = dsproin.Tables[0].Rows[0]["g_code"].ToString();
                    string gncode = dsproin.Tables[0].Rows[0]["GN_code"].ToString();
                    string cccode = dsproin.Tables[0].Rows[0]["CC_code"].ToString();
                    string facode = dsproin.Tables[0].Rows[0]["FA_code"].ToString();
                    string uintc = dsproin.Tables[0].Rows[0]["unitcode"].ToString();
                    string fcode = dsproin.Tables[0].Rows[0]["formcode"].ToString();
                    string mcode = dsproin.Tables[0].Rows[0]["ManufactureCode"].ToString();
                    string secode = dsproin.Tables[0].Rows[0]["se_code"].ToString();
                    string rackno = dsproin.Tables[0].Rows[0]["Rack"].ToString();
                    string free = dsproin.Tables[0].Rows[0]["Freesupply"].ToString();
                    string Tax = dsproin.Tables[0].Rows[0]["Tax"].ToString();
                    string stocki = dsproin.Tables[0].Rows[0]["Stockinward"].ToString();
                    string exdate = dsproin.Tables[0].Rows[0]["Expiredate"].ToString();
                    string purchase = dsproin.Tables[0].Rows[0]["Purchaseprice"].ToString();
                    string mrp = dsproin.Tables[0].Rows[0]["MRP"].ToString();
                    string Totalvalues = dsproin.Tables[0].Rows[0]["Totalvalues"].ToString();
                    string Taxamount = dsproin.Tables[0].Rows[0]["Taxamount"].ToString();
                    string Narration = dsproin.Tables[0].Rows[0]["Narration"].ToString();
                    string Sellprice = dsproin.Tables[0].Rows[0]["Sellprice"].ToString();
                    string Sysdatetime = dsproin.Tables[0].Rows[0]["Sysdatetime"].ToString();
                    string Mac_id = dsproin.Tables[0].Rows[0]["Mac_id"].ToString();
                    string taxable = dsproin.Tables[0].Rows[0]["taxable"].ToString();
                    string invoiceno1 = txtbillno.Text;
                    string qtyin = Convert.ToString(rqty1);
                    string total = Convert.ToString(tot11);

                    System.DateTime dnow = DateTime.Now;
                    string trdate = dnow.ToString("dd/MM/yyyy");




                    //Clsprdinw.Productinward("INSERT_PRODUCTINWARD", transno, txtbillno.Text, invoicedate, ca, paymflag, "0000", indate, prodcode, name, gcode, gncode, cccode, facode, uintc, fcode, mcode, secode, rackno, "0000", free, Tax, stocki, rqty1, batchid, exdate, purchase, mrp, Totalvalues, Taxamount, Narration, Sellprice, Session["username"].ToString(), Sysdatetime, Mac_id, taxable, "Y", tot11, "Y", "Y", "Y", "Y", "Y", "Y","Y");
                    Clsprdinw.Productinward("INSERT_PRODUCTINWARD", transno, invoiceno1, invoicedate, ca, paymflag, "0000", indate, prodcode, name, gcode, gncode, cccode, facode, uintc, fcode, mcode, secode, rackno, "0000", free, Tax, stocki, qtyin, batchid, exdate, purchase, mrp, Totalvalues, Taxamount, Narration, Sellprice, Session["username"].ToString(), Sysdatetime, Mac_id, taxable, "Y", total, "Y", "Y", "Y", "Y", "Y", "Y", "Y");

                    ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, trdate, "0000", "0000", "9996", "N", "0000", invoiceno1, "0", "0", "0", "0", total, "0", Session["username"].ToString(),trdate, Mac_id);

                }


            }

            GridView1.Visible = false;
            txtbillno.Text = string.Empty;
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Enabled = true;
            lblerror.Text = asd;
        }

        //DataSet dsproin=clsgd.GetcondDataSet3("*","tblProductinward","ProductName",
        
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void Return_Qty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int countgno=0;
            //int rcountno;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                countgno= countgno + 1;
            }
            TextBox txt = (TextBox)sender;
            GridViewRow row = (GridViewRow)txt.NamingContainer;

            
            int index=row.RowIndex;
            string proname = GridView1.Rows[index].Cells[0].Text;
            DataSet dsprosale = clsgd.GetcondDataSet2("*", "tblProductsale", "Invoiceno", txtbillno.Text,"ProductName",proname);
            if (dsprosale.Tables[0].Rows.Count > 0)
            {

                ///GridViewRow row = GridView1.SelectedRow;
                double div1 = 10;
                double total = 0;
                double price = 0;
                double rqty = 0;
                double div = 0;
                double dvalue=0;
               
                // int price = Convert.ToInt16((TextBox)row.FindControl("Sales_Price"));
                //price = Convert.ToDouble((GridView1.Rows[row.RowIndex].Cells[3].FindControl("Sales_Price") as TextBox).Text);
                price=Convert.ToDouble((dsprosale.Tables[0].Rows[0]["Rate"].ToString()));
                dvalue=Convert.ToDouble((dsprosale.Tables[0].Rows[0]["D_Value"].ToString()));
                rqty = Convert.ToDouble((GridView1.Rows[row.RowIndex].Cells[5].FindControl("Return_Qty") as TextBox).Text);
                div = div1 / 100;
                total = (price - dvalue) * rqty;
                //int total = 0;
                GridView1.Rows[index].Cells[6].Text = Convert.ToString(total);

                int cellno = row.RowIndex + 1;
                if (countgno > cellno)
                {
                    (GridView1.Rows[cellno].Cells[6].FindControl("Return_Qty") as TextBox).Focus();
                }
                else
                {
                    btnsave.Focus();
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}