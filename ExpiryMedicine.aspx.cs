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
//using System.Drawing;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.Mail;
using iTextSharp.text.pdf.parser;
using System.Globalization;
using custom.util;
using AllHospitalNames; 

public partial class ExpiryMedicine : System.Web.UI.Page
{
    DataTable tblexpirymedicine = new DataTable();

    ClsBALExpirydate ClsBLGP = new ClsBALExpirydate();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    ClsBALTransaction ClsBLGP3 = new ClsBALTransaction();
    PharmacyName Hosp = new PharmacyName();
    Dbconn dbcon = new Dbconn();
    protected static string button_select;
    DataTable tblChemical = new DataTable();
    DataTable dt = new DataTable();
    DataRow drrw;
    //string mac = "";
    protected static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
    DataTable tblProductinward = new DataTable();
    //  string result = "";
    string sMacAddress = "";
    string transno;
    string transno1;
    protected void Page_Load(object sender, EventArgs e)
    {
         if (!IsPostBack)
               {
                  System.DateTime Dtnow = DateTime.Now;
            //txtdate.Text = Dtnow.ToString("dd/MM/yyyy");
            txtdate1.Text = Dtnow.ToString("dd/MM/yyyy");

             //string expdate=txtdate1.Text;

             DateTime expdate= Convert.ToDateTime(txtdate1.Text);
             btnprint.Enabled = false;
 
             //DateTime expdate1=
             
                 BindUserDetails(); 
                }

         GetMACAddress();

    }


    public string GetMACAddress()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        // String sMacAddress = string.Empty;
        foreach (NetworkInterface adapter in nics)
        {
            if (sMacAddress == String.Empty)// only return MAC Address from first card  
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMacAddress = adapter.GetPhysicalAddress().ToString();
            }
            // sMacAddress = sMacAddress.Replace(":", "");
        } return sMacAddress;
    }

    private void BindUserDetails()
    {
        

        DateTime dtEntered = Convert.ToDateTime(txtdate1.Text);
        string strEnteredDate = dtEntered.ToString("MM/dd/yyyy");

        

        if (!File.Exists(filename))
        {
            try
            {


                gvDetails.DataSource = null;
                gvDetails.DataBind();
                tblProductinward.Rows.Clear();
                SqlConnection con = new SqlConnection(strconn11);
                string In_falg4 = "N";
               // SqlCommand cmd = new SqlCommand("select TransNo,Productcode,ProductName,Batchid,Expiredate,Totalvalues from tblProductinward a where In_falg4 ='" + In_falg4 + "' and Expiredate <'" + strEnteredDate + "'", con);

               // SqlCommand cmd = new SqlCommand("select TransNo,a.Productcode,a.ProductName,Batchid,Expiredate,Totalvalues from tblProductinward a INNER JOIN tblProductMaster b on a.g_code=b.g_code  where In_falg4 ='N' and Expiredate <'" + strEnteredDate + "' and Pharmflag='Y'", con);
                DateTime checkdate = DateTime.Now;
                checkdate = checkdate.AddDays(90);
                String checkdate1 = checkdate.ToString("yyyy/MM/dd");
                SqlCommand cmd = new SqlCommand("Select TransNo,a.Productcode,a.ProductName,a.Batchid,a.Expiredate,a.Totalvalues,a.Stockinhand as stockinhand,b.Pharmflag as PROD from tblProductinward a left join tblProductMaster b on a.Productcode=b.Productcode where a.Expiredate <'" + checkdate1 + "' and a.Stockinhand>'0' and b.Pharmflag !='N' and a.In_falg4='Y' group by a.TransNo,a.Stockinhand,a.ProductName,b.Pharmflag,a.Productcode,a.Batchid,a.Expiredate,a.Totalvalues", con);

              //  SqlCommand cmd = new SqlCommand("", con);
               
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblProductinward.Columns.Add(col);
                    tblProductinward.Columns.Add("TransNo");
                    tblProductinward.Columns.Add("Productcode");
                    tblProductinward.Columns.Add("ProductName");
                    tblProductinward.Columns.Add("Batchid");
                    tblProductinward.Columns.Add("Expiredate");
                    tblProductinward.Columns.Add("Totalvalues");

                    Session["Group"] = tblProductinward;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tblProductinward = (DataTable)Session["Group"];
                        drrw = tblProductinward.NewRow();
                        drrw["TransNo"] = ds.Tables[0].Rows[i]["TransNo"].ToString();
                        drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                        drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                        drrw["Batchid"] = ds.Tables[0].Rows[i]["Batchid"].ToString();
                        drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                        drrw["Totalvalues"] = ds.Tables[0].Rows[i]["Totalvalues"].ToString();

                        tblProductinward.Rows.Add(drrw);
                    }
                    DataView dw = tblProductinward.DefaultView;
                    dw.Sort = "SLNO ASC";
                    gvDetails.DataSource = tblProductinward;
                    gvDetails.DataBind();
                }
            }
            catch (Exception e)
            {
                string asd = e.Message;
                lblerror.Enabled = true;
                lblerror.Text = asd;
            }

        }
        else
        {

            try
            {
                var dtEntered1 = txtdate1.Text;
                var strEnteredDate1 = dtEntered.ToString("#dd/MM/yyyy#");

                gvDetails.DataSource = null;
                gvDetails.DataBind();
                tblProductinward.Rows.Clear();
                OleDbConnection con = new OleDbConnection(strconn11);
                string In_flag1 = "Y";
                OleDbCommand cmd = new OleDbCommand("select TransNo,Productcode,ProductName,Batchid,Expiredate,Totalvalues from tblProductinward where In_falg1='" + In_flag1 + "' and Expiredate < " + strEnteredDate1 + "", con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblProductinward.Columns.Add(col);
                    tblProductinward.Columns.Add("TransNo");
                    tblProductinward.Columns.Add("Productcode");
                    tblProductinward.Columns.Add("ProductName");
                    tblProductinward.Columns.Add("Batchid");
                    tblProductinward.Columns.Add("Expiredate");
                    tblProductinward.Columns.Add("Totalvalues");

                    Session["Group"] = tblProductinward;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tblProductinward = (DataTable)Session["Group"];
                        drrw = tblProductinward.NewRow();
                        drrw["TransNo"] = ds.Tables[0].Rows[i]["TransNo"].ToString();
                        drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                        drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                        drrw["Batchid"] = ds.Tables[0].Rows[i]["Batchid"].ToString();
                        drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                        drrw["Totalvalues"] = ds.Tables[0].Rows[i]["Totalvalues"].ToString();

                        tblProductinward.Rows.Add(drrw);
                    }
                    DataView dw = tblProductinward.DefaultView;
                    dw.Sort = "SLNO ASC";
                    gvDetails.DataSource = tblProductinward;
                    gvDetails.DataBind();
                }
            }
            catch (Exception e)
            {
                string asd = e.Message;
                lblerror.Enabled = true;
                lblerror.Text = asd;
            }

           

        }
    }
 

    
     protected void Gridview1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].BackColor = System.Drawing.Color.Yellow;
                e.Row.Cells[2].BackColor = System.Drawing.Color.Yellow;
                e.Row.Cells[3].BackColor = System.Drawing.Color.Yellow;
                e.Row.Cells[4].BackColor = System.Drawing.Color.Yellow;
                e.Row.Cells[5].BackColor = System.Drawing.Color.Yellow;
                //  DropDownList ddll = (DropDownList)e.Row.FindControl("ddlproductcode");



                //ddll.Items.Insert(0, new ListItem("--Select--", "0"));


                conn.Close();

            }
        }
    }
   

     protected void btnDelete_Click(object sender, EventArgs e)
       {
           if (!File.Exists(filename))
           {
               foreach (GridViewRow gvrow in gvDetails.Rows)
               {
                   
                   CheckBox chkdelete = (CheckBox)gvrow.FindControl("chkSelect");
                  
                   if (chkdelete.Checked)
                   {
                       
                       //int TransNo1 = Convert.ToInt32(gvDetails.DataKeys[gvrow.RowIndex].Value);
                      
                       using (SqlConnection con = new SqlConnection(strconn11))
                       {
                           con.Open();
                           //string Batchid1 = Convert.ToString(gvDetails.DataKeys[gvrow.RowIndex].Value);
                           string Productcode1 = Convert.ToString(gvDetails.DataKeys[gvrow.RowIndex].Value);
                           DataSet dsgroup21 = ClsBLGD.GetcondDataSet("*", "tblProductinward", "Productcode", Productcode1);
                           string Invoiceno = dsgroup21.Tables[0].Rows[0]["Invoiceno"].ToString();
                           string Invoicedate = dsgroup21.Tables[0].Rows[0]["Invoicedate"].ToString();
                           string Paymenttype = dsgroup21.Tables[0].Rows[0]["Paymenttype"].ToString();
                           string Paymentflag = dsgroup21.Tables[0].Rows[0]["Paymentflag"].ToString();
                           string SuppplierCode = dsgroup21.Tables[0].Rows[0]["SuppplierCode"].ToString();
                           string Indate = dsgroup21.Tables[0].Rows[0]["Indate"].ToString();
                           string Productcode = dsgroup21.Tables[0].Rows[0]["Productcode"].ToString();
                           string ProductName = dsgroup21.Tables[0].Rows[0]["ProductName"].ToString();
                           string g_code = dsgroup21.Tables[0].Rows[0]["g_code"].ToString();
                           string GN_code = dsgroup21.Tables[0].Rows[0]["GN_code"].ToString();
                           string CC_code = dsgroup21.Tables[0].Rows[0]["CC_code"].ToString();
                           string FA_code = dsgroup21.Tables[0].Rows[0]["FA_code"].ToString();
                           string unitcode = dsgroup21.Tables[0].Rows[0]["unitcode"].ToString();
                           string formcode = dsgroup21.Tables[0].Rows[0]["formcode"].ToString();
                           string ManufactureCode = dsgroup21.Tables[0].Rows[0]["ManufactureCode"].ToString();
                           string se_code = dsgroup21.Tables[0].Rows[0]["se_code"].ToString();
                           string Rack = dsgroup21.Tables[0].Rows[0]["Rack"].ToString();
                           string Supliercode = dsgroup21.Tables[0].Rows[0]["Supliercode"].ToString();
                           string Freesupply = dsgroup21.Tables[0].Rows[0]["Freesupply"].ToString();
                           string Tax = dsgroup21.Tables[0].Rows[0]["Tax"].ToString();
                           string Stockinward = dsgroup21.Tables[0].Rows[0]["Stockinward"].ToString();
                           string Stockinhand = dsgroup21.Tables[0].Rows[0]["Stockinhand"].ToString();
                           string Batchid = dsgroup21.Tables[0].Rows[0]["Batchid"].ToString();
                           string Expiredate = dsgroup21.Tables[0].Rows[0]["Expiredate"].ToString();
                           string Purchaseprice = dsgroup21.Tables[0].Rows[0]["Purchaseprice"].ToString();
                           string MRP = dsgroup21.Tables[0].Rows[0]["MRP"].ToString();
                           string Totalvalues = dsgroup21.Tables[0].Rows[0]["Totalvalues"].ToString();
                           string Taxamount = dsgroup21.Tables[0].Rows[0]["Taxamount"].ToString();
                           string Narration = dsgroup21.Tables[0].Rows[0]["Narration"].ToString();
                           string Sellprice = dsgroup21.Tables[0].Rows[0]["Sellprice"].ToString();
                           string In_falg1 = dsgroup21.Tables[0].Rows[0]["taxable"].ToString();
                           string In_falg2 = dsgroup21.Tables[0].Rows[0]["In_falg2"].ToString();
                           string In_falg3 = dsgroup21.Tables[0].Rows[0]["In_falg3"].ToString();
                           string In_falg4 = dsgroup21.Tables[0].Rows[0]["In_falg4"].ToString();
                           string In_falg5 = dsgroup21.Tables[0].Rows[0]["In_falg5"].ToString();
                           string In_falg6 = dsgroup21.Tables[0].Rows[0]["In_falg6"].ToString();
                           string In_falg7 = dsgroup21.Tables[0].Rows[0]["In_falg7"].ToString();
                           string In_falg8 = dsgroup21.Tables[0].Rows[0]["In_falg8"].ToString();
                           string In_falg9 = dsgroup21.Tables[0].Rows[0]["In_falg9"].ToString();
                           string In_falg10 = dsgroup21.Tables[0].Rows[0]["In_falg10"].ToString();

                           System.DateTime Dtnow = DateTime.Now;
                           string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");

                           ClsBLGP.Expirydate("INSERT_EXPIRYMEDICINE", Invoiceno, Invoicedate, Paymenttype, Paymentflag, SuppplierCode, Indate, Productcode, ProductName, g_code, GN_code, CC_code, FA_code, unitcode, formcode, ManufactureCode, se_code, Rack, Supliercode, Freesupply, Tax, Stockinward, Stockinhand, Batchid, Expiredate, Purchaseprice, MRP, Totalvalues, Taxamount, Narration, Sellprice, Session["username"].ToString(), sqlFormattedDate, sMacAddress, In_falg1, In_falg1, In_falg1, In_falg1, In_falg1, In_falg1, In_falg1, In_falg1, In_falg1, In_falg1);



                           string falg4 = "N";
                           SqlCommand cmd20 = new SqlCommand("UPDATE tblProductinward SET  In_falg4 ='" + falg4 + "' WHERE  Batchid ='" + Batchid + "' AND Productcode='" + Productcode + "'", con);
                           cmd20.ExecuteNonQuery();

                           lblsuccess.Visible = true;
                           lblsuccess.Text = "inserted successfully";
                           ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                          
                           con.Close();
                       }
                   }
               }
               System.DateTime Dtnow1 = DateTime.Now;
               string sqlFormattedDate1 = Dtnow1.ToString("dd/MM/yyyy");
               transno1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
               transno = transno1 + "/" + "EXP";

               string vreceptno = ClsBLGD.base64Encode("0000.00");

               ClsBLGP3.Transaction("INSERT_TRANSACTION", transno, txtdate1.Text, "0000", "0000", "9994", "N", "0000", vreceptno, "0000.00", "0000.00", "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate1, sMacAddress);
           }
           else
           {
               foreach (GridViewRow gvrow in gvDetails.Rows)
               {
                   //Finiding checkbox control in gridview for particular row
                   CheckBox chkdelete = (CheckBox)gvrow.FindControl("chkSelect");
                   //Condition to check checkbox selected or not
                   if (chkdelete.Checked)
                   {
                       //Getting UserId of particular row using datakey value
                       int TransNo1 = Convert.ToInt32(gvDetails.DataKeys[gvrow.RowIndex].Value);
                       string TransNo2 = Convert.ToString(gvDetails.DataKeys[gvrow.RowIndex].Value);
                       using (OleDbConnection con = new OleDbConnection(strconn11))
                       {
                           con.Open();
                           DataSet dsgroup21 = ClsBLGD.GetcondDataSet("*", "tblProductinward", "TransNo", TransNo2);
                           string Invoiceno = dsgroup21.Tables[0].Rows[0]["Invoiceno"].ToString();
                           string Invoicedate = dsgroup21.Tables[0].Rows[0]["Invoicedate"].ToString();
                           string Paymenttype = dsgroup21.Tables[0].Rows[0]["Paymenttype"].ToString();
                           string Paymentflag = dsgroup21.Tables[0].Rows[0]["Paymentflag"].ToString();
                           string SuppplierCode = dsgroup21.Tables[0].Rows[0]["SuppplierCode"].ToString();
                           string Indate = dsgroup21.Tables[0].Rows[0]["Indate"].ToString();
                           string Productcode = dsgroup21.Tables[0].Rows[0]["Productcode"].ToString();
                           string ProductName = dsgroup21.Tables[0].Rows[0]["ProductName"].ToString();
                           string g_code = dsgroup21.Tables[0].Rows[0]["g_code"].ToString();
                           string GN_code = dsgroup21.Tables[0].Rows[0]["GN_code"].ToString();
                           string CC_code = dsgroup21.Tables[0].Rows[0]["CC_code"].ToString();
                           string FA_code = dsgroup21.Tables[0].Rows[0]["FA_code"].ToString();
                           string unitcode = dsgroup21.Tables[0].Rows[0]["unitcode"].ToString();
                           string formcode = dsgroup21.Tables[0].Rows[0]["formcode"].ToString();
                           string ManufactureCode = dsgroup21.Tables[0].Rows[0]["ManufactureCode"].ToString();
                           string se_code = dsgroup21.Tables[0].Rows[0]["se_code"].ToString();
                           string Rack = dsgroup21.Tables[0].Rows[0]["Rack"].ToString();
                           string Supliercode = dsgroup21.Tables[0].Rows[0]["Supliercode"].ToString();
                           string Freesupply = dsgroup21.Tables[0].Rows[0]["Freesupply"].ToString();
                           string Tax = dsgroup21.Tables[0].Rows[0]["Tax"].ToString();
                           string Stockinward = dsgroup21.Tables[0].Rows[0]["Stockinward"].ToString();
                           string Stockinhand = dsgroup21.Tables[0].Rows[0]["Stockinhand"].ToString();
                           string Batchid = dsgroup21.Tables[0].Rows[0]["Batchid"].ToString();
                           string Expiredate = dsgroup21.Tables[0].Rows[0]["Expiredate"].ToString();
                           string Purchaseprice = dsgroup21.Tables[0].Rows[0]["Purchaseprice"].ToString();
                           string MRP = dsgroup21.Tables[0].Rows[0]["MRP"].ToString();
                           string Totalvalues = dsgroup21.Tables[0].Rows[0]["Totalvalues"].ToString();
                           string Taxamount = dsgroup21.Tables[0].Rows[0]["Taxamount"].ToString();
                           string Narration = dsgroup21.Tables[0].Rows[0]["Narration"].ToString();
                           string Sellprice = dsgroup21.Tables[0].Rows[0]["Sellprice"].ToString();
                           string In_falg1 = dsgroup21.Tables[0].Rows[0]["In_falg1"].ToString();
                           string In_falg2 = dsgroup21.Tables[0].Rows[0]["In_falg2"].ToString();
                           string In_falg3 = dsgroup21.Tables[0].Rows[0]["In_falg3"].ToString();
                           string In_falg4 = dsgroup21.Tables[0].Rows[0]["In_falg4"].ToString();
                           string In_falg5 = dsgroup21.Tables[0].Rows[0]["In_falg5"].ToString();
                           string In_falg6 = dsgroup21.Tables[0].Rows[0]["In_falg6"].ToString();
                           string In_falg7 = dsgroup21.Tables[0].Rows[0]["In_falg7"].ToString();
                           string In_falg8 = dsgroup21.Tables[0].Rows[0]["In_falg8"].ToString();
                           string In_falg9 = dsgroup21.Tables[0].Rows[0]["In_falg9"].ToString();
                           string In_falg10 = dsgroup21.Tables[0].Rows[0]["In_falg10"].ToString();

                           OleDbConnection con11 = new OleDbConnection(strconn11);
                           con.Open();
                           
                           System.DateTime Dtnow = DateTime.Now;
                           string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                           OleDbCommand cmd = new OleDbCommand("insert into tblExpiryMedicine(Invoiceno,Invoicedate,Paymenttype,Paymentflag,Suppliername,Indate,Productcode,ProductName,g_code,GN_code,CC_code,FA_code,unitcode,formcode,ManufactureCode,se_code,Rack,Suppliercode,Freesupply,Tax,Stockinward,Stockinhand,Batchid,Expiredate,Purchaseprice,MRP,Taxamount,Totalvalues,Login_name,Sysdatetime,Mac_id,In_falg1,In_falg2,In_falg3,In_falg4,In_falg5,In_falg6,In_falg7,In_falg8,In_falg9,In_falg10) values('" + Invoiceno + "','" + Invoicedate + "','" + Paymenttype + "','" + Paymentflag + "','" + SuppplierCode + "','" + Indate + "','" + Productcode + "','" + ProductName + "','" + g_code + "','" + GN_code + "','" + CC_code + "','" + FA_code + "'," + unitcode + ",'" + formcode + "','" + ManufactureCode + "','" + se_code + "','" + Rack + "'," + Supliercode + "," + Freesupply + ",'" + Tax + "','" + Stockinward + "','" + Stockinhand + "','" + Batchid + "','" + Expiredate + "','" + Purchaseprice + "','" + MRP + "','" + Totalvalues + "','" + Taxamount + "','" + Session["username"].ToString() + "','" + sqlFormattedDate + "','" + sMacAddress + "',In_falg1,In_falg2,In_falg3,In_falg4,In_falg5,In_falg6,In_falg7,In_falg8,In_falg9,In_falg10)", con11);
                           cmd.ExecuteNonQuery();
                           con.Close();


                           
                           string In_flag1 = "N";
                           OleDbCommand cmd20 = new OleDbCommand("UPDATE tblProductinward SET  In_falg1='" + In_flag1 + "' WHERE  TransNo =" + TransNo1 + "", con);
                           cmd20.ExecuteNonQuery();
                           con.Close();
                       }
                   }
               }

           }
            BindUserDetails();
            btnprint.Enabled = true;
        }


  

   

  
   
    protected void btnsave_Click(object sender, EventArgs e)
    {

    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
       Response.Redirect("Home.aspx");
    }
    protected void txtdate1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime startdate1111 = Convert.ToDateTime(txtdate1.Text);
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            Master.ShowModal("Invalid date format...", "txtdate1", 1);
            return;
        }
        DateTime startDate;
        if (DateTime.TryParse(txtdate1.Text, out startDate))
        {
            lblerrordate.Text = string.Empty;

            

                DateTime dtEntered = Convert.ToDateTime(txtdate1.Text);
                string strEnteredDate = dtEntered.ToString("MM/dd/yyyy");
           

            // txtdate1.Text = calender1.Value.ToString("dd/MM/yyyy");

            if (!File.Exists(filename))
            {
                try
                {


                    gvDetails.DataSource = null;
                    gvDetails.DataBind();
                    tblProductinward.Rows.Clear();
                    SqlConnection con = new SqlConnection(strconn11);
                    string In_flag1 = "Y";
                    SqlCommand cmd = new SqlCommand("select TransNo,Productcode,ProductName,Batchid,Expiredate,Totalvalues from tblProductinward where In_falg1='" + In_flag1 + "' and Expiredate <'" + strEnteredDate + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        DataColumn col = new DataColumn("SLNO", typeof(int));
                        col.AutoIncrement = true;
                        col.AutoIncrementSeed = 1;
                        col.AutoIncrementStep = 1;
                        tblProductinward.Columns.Add(col);
                        tblProductinward.Columns.Add("TransNo");
                        tblProductinward.Columns.Add("Productcode");
                        tblProductinward.Columns.Add("ProductName");
                        tblProductinward.Columns.Add("Batchid");
                        tblProductinward.Columns.Add("Expiredate");
                        tblProductinward.Columns.Add("Totalvalues");

                        Session["Group"] = tblProductinward;

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            tblProductinward = (DataTable)Session["Group"];
                            drrw = tblProductinward.NewRow();
                            drrw["TransNo"] = ds.Tables[0].Rows[i]["TransNo"].ToString();
                            drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                            drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                            drrw["Batchid"] = ds.Tables[0].Rows[i]["Batchid"].ToString();
                            drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                            drrw["Totalvalues"] = ds.Tables[0].Rows[i]["Totalvalues"].ToString();

                            tblProductinward.Rows.Add(drrw);
                        }
                        DataView dw = tblProductinward.DefaultView;
                        dw.Sort = "SLNO ASC";
                        gvDetails.DataSource = tblProductinward;
                        gvDetails.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    string asd = ex.Message;
                    lblerror.Enabled = true;
                    lblerror.Text = asd;
                }

            }
            else
            {

                try
                {
                    var dtEntered1 = txtdate1.Text;
                    var strEnteredDate1 = dtEntered.ToString("#dd/MM/yyyy#");

                    gvDetails.DataSource = null;
                    gvDetails.DataBind();
                    tblProductinward.Rows.Clear();
                    OleDbConnection con = new OleDbConnection(strconn11);
                    string In_flag1 = "Y";
                    OleDbCommand cmd = new OleDbCommand("select TransNo,Productcode,ProductName,Batchid,Expiredate,Totalvalues from tblProductinward where In_falg1='" + In_flag1 + "' and Expiredate < " + strEnteredDate1 + "", con);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        DataColumn col = new DataColumn("SLNO", typeof(int));
                        col.AutoIncrement = true;
                        col.AutoIncrementSeed = 1;
                        col.AutoIncrementStep = 1;
                        tblProductinward.Columns.Add(col);
                        tblProductinward.Columns.Add("TransNo");
                        tblProductinward.Columns.Add("Productcode");
                        tblProductinward.Columns.Add("ProductName");
                        tblProductinward.Columns.Add("Batchid");
                        tblProductinward.Columns.Add("Expiredate");
                        tblProductinward.Columns.Add("Totalvalues");

                        Session["Group"] = tblProductinward;

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            tblProductinward = (DataTable)Session["Group"];
                            drrw = tblProductinward.NewRow();
                            drrw["TransNo"] = ds.Tables[0].Rows[i]["TransNo"].ToString();
                            drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                            drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                            drrw["Batchid"] = ds.Tables[0].Rows[i]["Batchid"].ToString();
                            drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                            drrw["Totalvalues"] = ds.Tables[0].Rows[i]["Totalvalues"].ToString();

                            tblProductinward.Rows.Add(drrw);
                        }
                        DataView dw = tblProductinward.DefaultView;
                        dw.Sort = "SLNO ASC";
                        gvDetails.DataSource = tblProductinward;
                        gvDetails.DataBind();
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
        else
        {
            lblerrordate.Text = " <span style='font-size:12px;color: red;'>Invalid date Format</span>";

        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Bind();




        ArrayList oALHospDetails = Hosp.HospitalReturns();
        SqlConnection con50 = new SqlConnection(strconn11);
        SqlCommand cmd50 = new SqlCommand("select * from tblProductsale", con50);
        SqlDataAdapter da50 = new SqlDataAdapter(cmd50);
        DataSet ds50 = new DataSet();
        da50.Fill(ds50);




      

      

        SqlConnection con52 = new SqlConnection(strconn11);
        SqlCommand cmd52 = new SqlCommand("select * from tblBankname", con52);
        SqlDataAdapter da52 = new SqlDataAdapter(cmd52);
        DataSet ds52 = new DataSet();
        da52.Fill(ds52);

        string ShopAbbreviation = ds52.Tables[0].Rows[0]["ShopAbbreviation"].ToString();
        string PharmacyName = ds52.Tables[0].Rows[0]["PharmacyName"].ToString();
        string Place = ds52.Tables[0].Rows[0]["Place"].ToString();
        string Pincode = ds52.Tables[0].Rows[0]["Pincode"].ToString();

        string State = ds52.Tables[0].Rows[0]["State"].ToString();


        // PDF Report generation
        // Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
        Document document = new Document(new iTextSharp.text.Rectangle(500f, 400f), 0f, 0f, 0f, 0f);
        PdfWriter.GetInstance(document, Response.OutputStream);
        Document document1 = new Document();
        Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);

        MemoryStream memoryStream = new System.IO.MemoryStream();

        PdfWriter.GetInstance(document, Response.OutputStream);
        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
        PdfWriterEvents1 writerEvent = new PdfWriterEvents1(ShopAbbreviation.ToString());
        writer.PageEvent = writerEvent;


        DataTable dtPdfstock = new DataTable();
        if (grpexpiredetails.HeaderRow != null)
        {
            for (int i = 0; i < grpexpiredetails.HeaderRow.Cells.Count; i++)
            {
                dtPdfstock.Columns.Add(grpexpiredetails.HeaderRow.Cells[i].Text);
            }
        }

        //  add each of the data rows to the table

        foreach (GridViewRow row in grpexpiredetails.Rows)
        {
            DataRow datarow;
            datarow = dtPdfstock.NewRow();

            for (int i = 0; i < row.Cells.Count; i++)
            {
                datarow[i] = row.Cells[i].Text;
            }
            dtPdfstock.Rows.Add(datarow);
        }
        Session["dtPdfstock"] = dtPdfstock;


        Phrase phrase = null;
        PdfPCell cell = null;
        PdfPTable tblstock = null;
        PdfPTable table1 = null;
        PdfPTable table2 = null;

        PdfPTable tbldt = null;
        dtPdfstock = (DataTable)Session["dtPdfstock"];
        if (Session["dtPdfstock"] != null)
        {
            table2 = new PdfPTable(dtPdfstock.Columns.Count);
        }

        PdfPTable tblNoteSign = null;
      
        PdfPCell GridCell = null;
        BaseColor color = null;


        document.Open();

        //Header Table




        tblstock = new PdfPTable(1);
        tblstock.TotalWidth = 490f;
        tblstock.LockedWidth = true;
        tblstock.SetWidths(new float[] { 1f });

        tbldt = new PdfPTable(2);
        tbldt.TotalWidth = 490f;
        tbldt.LockedWidth = true;
        tbldt.SetWidths(new float[] { 1.4f, 0.6f });

        table1 = new PdfPTable(6);
        table1.TotalWidth = 490f;
        table1.LockedWidth = true;
        table1.SetWidths(new float[] { 0.5f, 1.0f, 0.7f, 0.7f, 0.7f, 0.7f});



        table2 = new PdfPTable(2);
        table2.TotalWidth = 490f;
        table2.LockedWidth = true;
        table2.SetWidths(new float[] { 1.4f, 0.6f });



        tblNoteSign = new PdfPTable(2);
        tblNoteSign.TotalWidth = 490f;
        tblNoteSign.LockedWidth = true;
        tblNoteSign.SetWidths(new float[] { 0.8f, 0.4f });






        GridCell = new PdfPCell(new Phrase(new Chunk("TransNo", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table1.AddCell(GridCell);

        // GridCell = new PdfPCell(new Phrase(new Chunk("Productcode", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        // GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Productcode.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("ProductName.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Batchid.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Expiredate.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Totalvalues.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table1.AddCell(GridCell);
        table1.SpacingAfter = 15f;

       


        //******************************************************************************************************************************************************************

        if (dtPdfstock != null)
        {
            for (int i = 0; i < dtPdfstock.Rows.Count; i++)
            {


                for (int row1 = 0; row1 < dtPdfstock.Columns.Count; row1++)
                {

                    GridCell = new PdfPCell(new Phrase(new Chunk(dtPdfstock.Rows[i][row1].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                   // GridCell.HorizontalAlignment = 0;
                    GridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    GridCell.PaddingBottom = 5f;
                    table1.AddCell(GridCell);

                }
            }
        }



        DateTime dtstrDate2 = DateTime.Now;

        DataSet dslogin = ClsBLGD.GetcondDataSet("*", "tblLogin", "username", Session["username"].ToString());
        // DataSet dsbcode = Clsbllgeneral.GetcondDataSet("*", "emp_det", "emp_code", dslogin.Tables[0].Rows[0]["emp_code"].ToString());

        // DataSet dsBranchDetails1 = Clsbllgeneral.GetcondDataSet("*", "branch_det", "branch_code", dsbcode.Tables[0].Rows[0]["branch_code"].ToString());

        tblstock.AddCell(PhraseCell(new Phrase("ExpiryMedicine Report\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
        cell.Colspan = 2;
        cell.PaddingBottom = 30f;
        tblstock.AddCell(cell);



       

        /* tbldt.AddCell(PhraseCell(new Phrase("Patient Name :" + pname, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
         cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_RIGHT);
         cell.Colspan = 2;
         cell.PaddingBottom = 28f;
         tbldt.AddCell(cell);*/






        phrase = new Phrase();
        phrase.Add(new Chunk(PharmacyName.ToString() + "\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)));
        phrase.Add(new Chunk(Place.ToString() + "\n" + Pincode.ToString() + "\n" + State.ToString() + "\n\n", FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)));
        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
        cell.HorizontalAlignment = 0;
        table2.AddCell(cell);

     



        // tblNoteSign.AddCell(PhraseCell(new Phrase("\n\n" + "Printed By " + "\n" + "(" + dsbcode.Tables[0].Rows[0]["emp_name"].ToString() + ")" + "\n\n", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
        // cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
        // cell.PaddingBottom = 30f;
        // tblNoteSign.AddCell(cell);



      


      


      

      


        tblNoteSign.AddCell(PhraseCell(new Phrase("\n\n" + "Printed By " + "\n" + "(" + dslogin.Tables[0].Rows[0]["UserName"].ToString() + ")" + "\n\n", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
        cell.Colspan = 2;
        cell.PaddingBottom = 30f;
        tblNoteSign.AddCell(cell);

        tblNoteSign.AddCell(PhraseCell(new Phrase("E & OE" + "\n\n", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
        cell.Colspan = 2;
        cell.PaddingBottom = 30f;
        tblNoteSign.AddCell(cell);










        // StringReader sr = new StringReader(sw.ToString());

        // ****************Drawing Line Horizontally*********************
        color = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#A9A9A9"));
        //DrawLine(writer, 0f, document.Top - 360f, document.PageSize.Width - 25f, document.Top - 360f, color);

        // ****************Drawing Line Vertically*********************
        //DrawLine(writer, 30f, 80f, 30f, 660f, color);
        //DrawLine(writer, 65f, 80f, 65f, 660f, color);
        int cntdtPdfstock = 0; ;
        if (dtPdfstock != null)
        {
            cntdtPdfstock = dtPdfstock.Rows.Count;
        }

        document.Add(tblstock);
        document.Add(table2);
        document.Add(tbldt);
        document.Add(table1);
        document.Add(tblNoteSign);
        grpexpiredetails.DataSource = null;
        dtPdfstock.Rows.Clear();
        document.Close();
        //Response.Clear();

        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", "attachment; filename=Expiremedicine.pdf");

        byte[] bytes = memoryStream.ToArray();
        memoryStream.Close();
        Response.Clear();
        //Response.Write(document);
        // Clsbllgeneral.ClearInputs(Page.Controls);

        Response.Buffer = true;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.BinaryWrite(bytes);
        Response.End();
        Response.Close();

    }

    public void Bind()
    {
        // string filename = Dbconn.Mymenthod();

        try
        {
            //string bname = ddlbname.SelectedItem.Text;
            grpexpiredetails.DataSource = null;
            grpexpiredetails.DataBind();
            tblexpirymedicine.Rows.Clear();
            SqlConnection con = new SqlConnection(strconn11);
            SqlCommand cmd = new SqlCommand("select TransNo,Productcode,ProductName,Batchid,Expiredate,Totalvalues from tblExpiryMedicine", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataColumn col = new DataColumn("SLNO", typeof(int));
                col.AutoIncrement = true;
                col.AutoIncrementSeed = 1;
                col.AutoIncrementStep = 1;
                tblexpirymedicine.Columns.Add(col);
                // tblpurchasesale.Columns.Add("Productcode");
                tblexpirymedicine.Columns.Add("TransNo");
                tblexpirymedicine.Columns.Add("Productcode");
                tblexpirymedicine.Columns.Add("ProductName");
                tblexpirymedicine.Columns.Add("Batchid");
                tblexpirymedicine.Columns.Add("Expiredate");
                tblexpirymedicine.Columns.Add("Totalvalues");
               


                Session["customer"] = tblexpirymedicine;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    tblexpirymedicine = (DataTable)Session["customer"];
                    drrw = tblexpirymedicine.NewRow();

                    //  drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                    drrw["TransNo"] = ds.Tables[0].Rows[i]["TransNo"].ToString();
                    drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                    drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                    drrw["Batchid"] = ds.Tables[0].Rows[i]["Batchid"].ToString();
                    drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                    drrw["Totalvalues"] = ds.Tables[0].Rows[i]["Totalvalues"].ToString();
                  




                    tblexpirymedicine.Rows.Add(drrw);
                    //Griddoctor.DataSource = tbldoctor;
                    //Griddoctor.DataBind();
                }
                DataView dws = tblexpirymedicine.DefaultView;
                dws.Sort = "SLNO ASC";
                grpexpiredetails.DataSource = tblexpirymedicine;
                grpexpiredetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Enabled = true;
            lblerror.Text = asd;
        }

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



}