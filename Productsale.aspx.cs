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
//using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Net.NetworkInformation;
using System.Management;
//using System.Drawing;
using System.Threading;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.Mail;
using iTextSharp.text.pdf.parser;
using System.Globalization;
using custom.util;
using AllHospitalNames;






public partial class _Default : System.Web.UI.Page
{
    DataTable tblpurchasesale = new DataTable();
    DataRow dr2;

    DataTable tblpurchasesale1 = new DataTable();

    ClsBALCustomeraccount ClsBLGP1 = new ClsBALCustomeraccount();
    ClsBLLGeneraldetails clsgd = new ClsBLLGeneraldetails();
    ClsBALTempproductsale ClsBLGP = new ClsBALTempproductsale();
    ClsBALTempProductaccount ClsBLGP2 = new ClsBALTempProductaccount();
    ClsBALSupplieraccount clsSup = new ClsBALSupplieraccount();
    ClsBALProductsale Clsprdinw = new ClsBALProductsale();
    ClsBALCardinfo Clscard = new ClsBALCardinfo();
    ClsBALSALESTAX Clstax = new ClsBALSALESTAX();
    ClsBALTEMPTAX Clstemptax=new ClsBALTEMPTAX();
    NumberToEnglish NumToEng = new NumberToEnglish();
    ClsBALCustomeraccount ClsBLCA = new ClsBALCustomeraccount();
    ClsBALCreditcustomer Clscrcust = new ClsBALCreditcustomer();
    ClsBALTransaction ClsBLGP3 = new ClsBALTransaction();
    PharmacyName Hosp = new PharmacyName();
    double calc;
    double g, h, g10, a1;
    double m;
    DataRow drrw;
    //ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    protected static string g1;
    protected static string balance1;
    Dbconn dbcon = new Dbconn();
    protected static string button_select;
    protected static  string strconn  = Dbconn.conmenthod();
    protected static string strconn1 = Dbconn.conmenthod();

    ArrayList arryno10 = new ArrayList();

    ArrayList arryname10 = new ArrayList();
    

    string filename = Dbconn.Mymenthod();
    ArrayList arryno = new ArrayList();

    ArrayList arryname = new ArrayList();

    ArrayList arryno20 = new ArrayList();

    ArrayList arryname20 = new ArrayList();

    string sMacAddress = "";
    string sqlFormattedDate = DateTime.Now.ToString();

    int count;
    int hedcd;

   // string invce1;

string invoiceno;
string transno;
string transno1;
string invoiceno1;
    string codecode;
    string headcode = "";
     protected static string name = "";
   protected static string a = "";


    
    
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Productcode", typeof(string)));
        dt.Columns.Add(new DataColumn("ProductName", typeof(string)));
        dt.Columns.Add(new DataColumn("Expiredate", typeof(string)));
        dt.Columns.Add(new DataColumn("Batchno", typeof(string)));
        dt.Columns.Add(new DataColumn("Stockinhand", typeof(string)));
        dt.Columns.Add(new DataColumn("Rate", typeof(string)));
        dt.Columns.Add(new DataColumn("Taxamount", typeof(string)));
        dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
        dt.Columns.Add(new DataColumn("Taxrate", typeof(string)));
        dt.Columns.Add(new DataColumn("D_Rate", typeof(string)));
        dt.Columns.Add(new DataColumn("Pro_Amount", typeof(string)));
        dt.Columns.Add(new DataColumn("g_name", typeof(string)));
        


        dr = dt.NewRow();
        
        dr["RowNumber"] = 1;
        dr["Productcode"] = string.Empty;
        dr["ProductName"] = string.Empty;
        dr["Expiredate"] = string.Empty;
        dr["Batchno"] = string.Empty;
        dr["Stockinhand"] = string.Empty;
        dr["Rate"] = string.Empty;
        dr["Taxamount"] = string.Empty;
        dr["Quantity"] = string.Empty;
        dr["Taxrate"] = string.Empty;
        dr["D_Rate"] = string.Empty;
        dr["Pro_Amount"] = string.Empty;
        dr["g_name"] = string.Empty;
       

        dt.Rows.Add(dr);
        //dr = dt.NewRow();

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;

        Gridview1.DataSource = dt;
        Gridview1.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtstock.BackColor = Color.LightBlue;
        txtstock.Attributes["style"] = "color:red; font-weight:bold;";
        lblsuccess.Visible = false;
        lblerror.Visible = false;
        //lblpayment.Visible = false;
        lblinvoicenor.Visible = false;
        txtinvoicenor.Visible = false;
      
        txtstock.Enabled = false;
        lblcardamount.Visible = false;

        txtdoctorname.Focus();
          GetMACAddress();
        
         System.DateTime Dtnow = DateTime.Now;
        string Sysdatetime= Dtnow.ToString("dd/MM/yyyy");
        txtdate.Text=Sysdatetime;
        if (!Page.IsPostBack)
        {
            SqlConnection con1 = new SqlConnection(strconn1);
            con1.Open();
            SqlCommand cmddel = new SqlCommand("delete FROM tbltempprodsale where LoginName = '"+ Session["username"] +"'", con1);
            cmddel.ExecuteNonQuery();
            con1.Close();

            //Bind();
            payment();
            //BindData();
            SetInitialRow();
            autoincrement();

            Panel3.Visible = false;
            Panel4.Visible = false;
            lblinvoicenor.Visible = false;
            txtinvoicenor.Visible = false;
            btnprint.Enabled = false;

            SqlConnection con = new SqlConnection(strconn1);
            con.Open();
            SqlCommand cmd = new SqlCommand("select Max(Tempid) as Tempid from tblTempProductsale", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string Tempid = ds.Tables[0].Rows[0]["Tempid"].ToString();
            //lbltrno.Text = Tempid;

            
               System.DateTime Dtnow1 = DateTime.Now;
               string sqlFormattedDate = Dtnow1.ToString("dd/MM/yyyy");

             // ClsBLGP.Tempproductsale("INSERT_TEMPPRODUCTSALE", Session["username"].ToString(), sMacAddress, sqlFormattedDate);

                     SqlConnection con12 = new SqlConnection(strconn1);
                    con12.Open();
          SqlCommand cmd12 = new SqlCommand("SELECT max(Displaynor) FROM tblTempProductsale", con12);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            string  Displaynor="0";
            string Invoicenor="0";
               if(ds1.Tables.Count>0)
                 {
                    //ClsBLGP.Tempproductsale("INSERT_TEMPPRODUCTSALE",Displaynor,Invoicenor, Session["username"].ToString(), sMacAddress, sqlFormattedDate);
                 }

              // SqlConnection con1 = new SqlConnection(strconn1);
              // con1.Open();
               //SqlCommand cmd1 = new SqlCommand("delete from tbltempprodsale where LoginName='" + Session["username"].ToString() + "'", con1);
               //cmd1.ExecuteNonQuery();

           }

        
       

           

       

    }

    public string GetMACAddress()
      {
          NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
          //  String sMacAddress = string.Empty;
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

    private void AddNewRowToGrid()
    {
        
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");

                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtexpiredate");
                    //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                    DropDownList box3 = (DropDownList)Gridview1.Rows[rowIndex].Cells[4].FindControl("ddl_Batch");
                    TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtStockinhand");
                    TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtrate");
                    TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                    TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtquantity");
                    TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("txttaxrate");
                    TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[10].FindControl("txtdiscount");
                    TextBox box10 = (TextBox)Gridview1.Rows[rowIndex].Cells[11].FindControl("txtproamount");
                    TextBox box11 = (TextBox)Gridview1.Rows[rowIndex].Cells[12].FindControl("txtgroupname");


                    if (box9.Text == "")
                    {
                        ShowPopupMessage("Enter Discount amount or zero", PopupMessageType.txtdiscount);
                        return;
                    }

                    if (box7.Text == "")
                    {

                        ShowPopupMessage("Enter quantity", PopupMessageType.txtquantity);
                        return;
                    }

                    //System.DateTime Dtnow = DateTime.Now;
                    //string Sysdatetime = Dtnow.ToString("dd/MM/yyyy");
                    //DataSet ds = clsgd.GetcondDataSet3("*", "tbltempprodsale", "Productcode", box0.Text, "ProductName", box1.Text, "Batchid", box3.Text);
                    //if (ds.Tables[0].Rows.Count==0)
                    //{

                    //    Clsprdinw.tempproductsale("INSERT_TEMPPRODUCTSALEbatch", box0.Text, box1.Text, box3.SelectedItem.Text, box7.Text, box4.Text,Session["username"].ToString(), sMacAddress, Sysdatetime);
                    //}
                    

                   
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["Productcode"] = box0.Text;
                    dtCurrentTable.Rows[i - 1]["ProductName"] = box1.Text;
                    dtCurrentTable.Rows[i - 1]["Expiredate"] = box2.Text;
                    //***************************

                    dtCurrentTable.Rows[i - 1]["Batchno"] = box3.Text;
                    dtCurrentTable.Rows[i - 1]["Stockinhand"] = box4.Text;
                    dtCurrentTable.Rows[i - 1]["Rate"] = box5.Text;
                    dtCurrentTable.Rows[i - 1]["Taxamount"] = box6.Text;
                    dtCurrentTable.Rows[i - 1]["Quantity"] = box7.Text;
                    dtCurrentTable.Rows[i - 1]["taxrate"] = box8.Text;
                    dtCurrentTable.Rows[i - 1]["D_Rate"] = box9.Text;
                    dtCurrentTable.Rows[i - 1]["Pro_Amount"] = box10.Text;
                    dtCurrentTable.Rows[i - 1]["g_name"] = box11.Text;
                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;

                Gridview1.DataSource = dtCurrentTable;
                Gridview1.DataBind();
            }
            
        }
        else
        {
            Response.Write("ViewState is null");
        }
       
        //Set Previous Data on Postbacks
        
        SetPreviousData();
    }



    protected void ddpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddpaymenttype.SelectedItem.Text == "CASH")
        {

            Panel3.Visible = true;
            ddlpaytype.Focus();
            cardtype();

            //txtcardno.Text = "xxxx-xxxx-xxxx-xxxx-";

            Double sum = 0;
            Double add = 0.0;
            Double discount1 = 0.0;
            Double sumdisc = 0.0;
            Double taxrate1 = 0.0;
            Double addtax = 0.0;

            for (int k = 0; k < Gridview1.Rows.Count; k++)
            {

                if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text))
                {
                    add = 0.0;

                }
                else
                {

                    add = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text);
                    sum = sum + add;
                }


                if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text))
                {
                    taxrate1 = 0.0;

                }
                else
                {

                    taxrate1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text);
                    addtax = addtax + taxrate1;
                }






                txtpramount.Text = (sum).ToString();

                double ctax = Convert.ToDouble(txttax.Text);
                double cpamount = Convert.ToDouble(txtpramount.Text);


                if (txtdiscount.Text == "0")
                {
                    double csamount = (cpamount + ctax);
                    txttotalamount.Text = Convert.ToString(csamount);
                   
                }
                else
                {
                    double cdisc = Convert.ToDouble(txtdiscount.Text);
                    txttotalamount.Text = Convert.ToString(cpamount + ctax - cdisc);
                   
                }

               // string amttot = txttotalamount.Text;
              //  txtcramount.Text = amttot;

            }
        }
        else
        {
            //lblbillnor.Enabled = false;
            //lblbillnor.Text = invoiceno;
            lblvbillno.Enabled = false;
            lblvbillno.Text = invoiceno;
            Panel3.Visible = false;
        }


        if (ddpaymenttype.SelectedItem.Text == "CARD")
        {

            if (txtcardno.Text != "")
            {
                txtcardno.Text = string.Empty;
            }

            if (txtcramount.Text != "")
            {
                txtcramount.Text = "0";
            }

            if (txttransno.Text != "")
            {
                txttransno.Text = string.Empty;
            }



           
            Panel3.Visible = true;
            ddlpaytype.Focus();
            cardtype();

           // txtcardno.Text = "xxxx-xxxx-xxxx-xxxx-";

            Double sum = 0;
            Double add = 0.0;
            Double discount1 = 0.0;
            Double sumdisc = 0.0;
            Double taxrate1 = 0.0;
            Double addtax = 0.0;

            for (int k = 0; k < Gridview1.Rows.Count; k++)
            {

                if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text))
                {
                    add = 0.0;

                }
                else
                {

                    add = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text);
                    sum = sum + add;
                }


                if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text))
                {
                    taxrate1 = 0.0;

                }
                else
                {

                    taxrate1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text);
                    addtax = addtax + taxrate1;
                }






                txtpramount.Text = (sum).ToString();


                //if (txtdiscount.Text == "")
                //{
                //    txttotalamount.Text = (sum).ToString();
                //    txtpramount.Text = (sum).ToString();
                //    Double sumpramount=Convert.ToDouble(txtpramount.Text);
                //    Double sumdiscount=sumpramount * sumdisc/100;
                //    txtdiscount.Text=(sumdiscount).ToString();
                //    txttax.Text=(addtax).ToString();
                //}
                //else
                //{
                //    double ttpramount = Convert.ToDouble(txtpramount.Text);
                //    double ttdiscount = Convert.ToDouble(txtdiscount.Text);
                //    txttotalamount.Text = (sum - ttdiscount).ToString();
                //    txtamount.Text=(sum - ttdiscount).ToString();
                //    txttax.Text=(addtax).ToString();
                //}

                string amttot = txttotalamount.Text;
                txtcramount.Text = amttot;

            }
        }
        else
        {
            //lblbillnor.Enabled = false;
            //lblbillnor.Text = invoiceno;
            lblvbillno.Enabled = false;
            lblvbillno.Text = invoiceno;
            Panel3.Visible = false;
        }

        if (ddpaymenttype.SelectedItem.Text == "CUSTOMER")
        {
            Panel4.Visible = true;
            txtcustomercode.Focus();
            Double sum = 0;
            Double add = 0.0;
            Double discount1 = 0.0;
            Double sumdisc = 0.0;
            Double taxrate1 = 0.0;
            Double addtax= 0.0;

            for (int k = 0; k < Gridview1.Rows.Count; k++)
            {

                if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text))
                {
                    add = 0.0;

                }
                else
                {

                    add = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text);
                    sum = sum + add;
                }


                if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text))
                   {
                       taxrate1 = 0.0;

                   }
                  else
                   {

                      taxrate1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text);
                      addtax = addtax + taxrate1;
                   }




               

                txtpramount.Text = (sum).ToString();


                //if (txtdiscount.Text == "")
                //{
                //    txttotalamount.Text = (sum).ToString();
                //    txtpramount.Text = (sum).ToString();
                //    Double sumpramount=Convert.ToDouble(txtpramount.Text);
                //    Double sumdiscount=sumpramount * sumdisc/100;
                //    txtdiscount.Text=(sumdiscount).ToString();
                //    txttax.Text=(addtax).ToString();
                //}
                //else
                //{
                //    double ttpramount = Convert.ToDouble(txtpramount.Text);
                //    double ttdiscount = Convert.ToDouble(txtdiscount.Text);
                //    txttotalamount.Text = (sum - ttdiscount).ToString();
                //    txtamount.Text=(sum - ttdiscount).ToString();
                //    txttax.Text=(addtax).ToString();
                //}

                string amttot = txttotalamount.Text;
                txtamount.Text = amttot;

            }




            //invoiceno1 = clsgd.FetchMaximumInvoiceNo("Select_Max_Invoiceno");
            //invoiceno = invoiceno1 + "/" + "SAL";
            lblbillnor.Enabled = false;
            lblbillnor.Text = invoiceno;
           

        }
        else
        {
            Panel4.Visible = false;
        }

        
       
    }
    // ddchem.Enabled = true;
    //ddchem.Focus();

    public void supplier()
    {
        DataSet dsgroup = clsgd.GetDataSet("distinct SupplierName", "tblsuppliermaster");
        for (int i = 0; i < dsgroup.Tables[0].Rows.Count; i++)
        {
            DataSet dsgroup1 = clsgd.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", dsgroup.Tables[0].Rows[i]["SupplierName"].ToString());
            arryname.Add(dsgroup1.Tables[0].Rows[0]["SupplierName"].ToString());


        }

        arryname.Sort();
        arryno.Add("-Select-");
        //arryno.Add("Add New");
        for (int i = 0; i < arryname.Count; i++)
        {
            arryno.Add(arryname[i].ToString());
        }
        //ddlsupplier.DataSource = arryno;
        //ddlsupplier.DataBind();
        //ddGecode.Focus();

    }

    

   

    protected void Button1_Click(object sender, EventArgs e)
    {
       
        
        string code=txtcustomercode.Text;
         
        string productcode = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Text);
        string productname = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txtproductname") as TextBox).Text);
       
        string quantity = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txtquantity") as TextBox).Text);

        string doctorname = txtdoctorname.Text;

        string patientname = txtpatientname.Text;


        if (doctorname == "")
        {
            Master.ShowModal("Enter doctorname. !!!", "txtquantity", 1);
            return;

           // ShowPopupMessage("Enter doctorname. !!!", PopupMessageType.txtdoctorname);
               // return;
               

        }

        if (patientname == "")
        {
            Master.ShowModal("Enter patientname. !!!", "txtpatientname", 1);
            return;

             // ShowPopupMessage("Enter patientname. !!!", PopupMessageType.txtpatientname);
              //  return;

        }

        if (productcode == "" || productname == "")
        {
            Master.ShowModal("Enter productcode or productname. !!!", "txtstockarrival", 1);
            return;
        }

        if (quantity == "")
        {
            Master.ShowModal("Enter quantity. !!!", "txtquantity", 1);
            return;
        }
       
       
        int rowIndex = 0;
        StringCollection sc = new StringCollection();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");
                   // TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtquantity");
                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                    //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                    DropDownList box4 = (DropDownList)Gridview1.Rows[rowIndex].Cells[5].FindControl("ddl_Batch");
                    TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtStockinhand");
                    TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtrate");
                    TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                    TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtquantity");
                    TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttaxrate");
                    TextBox box10 = (TextBox)Gridview1.Rows[rowIndex].Cells[10].FindControl("txtdiscount");
                    TextBox box11 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("txtproamount");
                    TextBox box12 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("txtpurchamountt");
                    TextBox box13 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("txtgroupname");

                        System.DateTime Dtnow = DateTime.Now;
                        string Sysdatetime = Dtnow.ToString("dd/MM/yyyy");

                        sc.Add(txtdoctorname.Text + "," + txtpatientname.Text + "," + txtinvoicenor.Text + "," + ddpaymenttype.SelectedItem.Text + "," + txtdate.Text + "," + box0.Text + "," + box1.Text + "," + box3.Text + "," + box4.Text + "," + box5.Text + "," + box6.Text + "," + box7.Text + "," + box8.Text + "," + box9.Text + "," + box10.Text + "," + box11.Text + "," + box13.Text + "," + Session["username"].ToString() + "," + Sysdatetime + "," + sMacAddress);
                        rowIndex++;

                   



                   

                }
                InsertRecords(sc);

                SqlConnection con1 = new SqlConnection(strconn1);
                con1.Open();
                SqlCommand cmd1 = new SqlCommand("delete from tbltempprodsale", con1);
                cmd1.ExecuteNonQuery();
            }
        }
    }

    private void InsertRecords(StringCollection sc)
    {

        try
        {

            string filename = Dbconn.Mymenthod();
            if (!File.Exists(filename))
            {



                string indate = txtdate.Text;

                DateTime indate10 = Convert.ToDateTime(indate);
                string indate11 = indate10.ToString("yyyy-MM-dd");

                string pramount = txtpramount.Text;

                string discount = txtdiscount.Text;

                string totamount = txttotalamount.Text;

                string sumtaxrate = txttax.Text;

                string doctorname = txtdoctorname.Text;

                string close_flag = "Y";
                DataSet dschm = clsgd.GetcondDataSet9("*", "tblProductsale", "Sale_falg5", close_flag);
                /*if (dschm.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        string cl_flag = "N";
                        SqlConnection conn52 = new SqlConnection(strconn1);
                        conn52.Open();
                        SqlCommand cmd52 = new SqlCommand("UPDATE tblProductsale SET  Sale_falg5='" + cl_flag + "' WHERE Sale_falg5= 'Y'", conn52);
                        cmd52.ExecuteNonQuery();


                    }
                    catch (Exception ex)
                    {
                        string asd = ex.Message;
                        lblerror.Visible = true;
                        lblerror.Text = asd;
                    }
                }*/


                //SqlConnection con = new SqlConnection(strconn1);
                //con.Open();
                //SqlCommand cmd = new SqlCommand("select Max(Sale_falg3) as Sale_falg3 from tblProductsale", con);
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //da.Fill(ds);
                //if (ds.Tables[0].Rows.Count > 0)
                //{

                //    string Tempid = ds.Tables[0].Rows[0]["Sale_falg3"].ToString();
                //}

                //else
                //{
                //    string Tempid = "1";
                //}





                transno1 = clsgd.FetchMaximumTransNo("Select_Max_Transno");
                invoiceno1 = clsgd.FetchMaximumInvoiceNo("Select_Max_Invoiceno");
                transno = transno1 + "/" + "SAL";
                
                invoiceno = invoiceno1 + "/" + "SAL";
                lblinvoicenor.Visible = true;
                txtinvoicenor.Visible = true;
                txtinvoicenor.Enabled = false;
                txtinvoicenor.Text = invoiceno;

               // Session["invoice10"] = invoiceno;
              //  invce1 = Session["invoice10"].ToString();
                //invce1 = txtinvoicenor.Text;
             


                int rowIndex = 0;
                //StringCollection sc = new StringCollection();
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                            TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");
                            TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtquantity");
                            TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                            //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                            DropDownList box4 = (DropDownList)Gridview1.Rows[rowIndex].Cells[5].FindControl("ddl_Batch");
                            TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtStockinhand");
                            TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtrate");
                            TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                            TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtquantity");
                            TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("txttaxrate");
                            TextBox box11 = (TextBox)Gridview1.Rows[rowIndex].Cells[11].FindControl("txtdiscount");
                            TextBox box10 = (TextBox)Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproamount");
                            TextBox box12 = (TextBox)Gridview1.Rows[rowIndex].Cells[11].FindControl("txttaxamount");
                            TextBox box13 = (TextBox)Gridview1.Rows[rowIndex].Cells[11].FindControl("txtpurchamount");
                            TextBox box14 = (TextBox)Gridview1.Rows[rowIndex].Cells[11].FindControl("txtgroupname");


                            if (box11.Text == "")
                            {
                                ShowPopupMessage("Enter Discount amount or zero", PopupMessageType.txtdiscount);
                                return;
                            }

                            SqlConnection inv = new SqlConnection(strconn1);
                            SqlCommand cmdd = new SqlCommand(" Select Invoiceno from tblProductinward where Productcode = '" + box0.Text + "' AND ProductName = '" + box1.Text + "' AND Batchid = '" + box4.Text + "'", inv);
                            SqlDataAdapter daa = new SqlDataAdapter(cmdd);
                            DataSet dss = new DataSet();
                            daa.Fill(dss);

                            string invno = dss.Tables[0].Rows[0]["Invoiceno"].ToString();






                            System.DateTime Dtnow = DateTime.Now;
                            string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                            String strconn11 = Dbconn.conmenthod();

                            double mrp = Convert.ToDouble(box6.Text);
                            double tax = Convert.ToDouble(box7.Text);
                            double qty = Convert.ToDouble(box8.Text);
                            double ratetax = Convert.ToDouble((mrp * tax) / (100 + tax));
                            double selprice = Convert.ToDouble(mrp * tax) / 100;

                            double selprice10 = mrp + selprice;

                            double rselprice = Math.Round(selprice10, 2);
                            string selprice1 = Convert.ToString(rselprice);




                            string fnselprice = Convert.ToString(selprice1);

                            string disc1 = Convert.ToString(box10.Text);


                            // double disc20=Convert.ToDouble(disc1);


                            Double drate = Convert.ToDouble(box11.Text);


                            double dvalue = (mrp * qty * drate) / 100;

                            string dvalue1 = Convert.ToString(dvalue);







                            if (ddpaymenttype.SelectedItem.Text == "CASH")
                            {
                                if (disc1 == "")
                                {
                                    cal();

                                    DataSet dschm10 = clsgd.GetcondDataSet9("*", "tblDoctor", "D_name", doctorname);
                                    if (dschm10.Tables[0].Rows.Count > 0)
                                    {
                                        string D_code = dschm10.Tables[0].Rows[0]["D_code"].ToString();
                                        DateTime boxdate = Convert.ToDateTime(box3.Text);
                                        string boxdate1 = boxdate.ToString("yyyy-MM-dd");


                                        DataSet dschm20 = clsgd.GetcondDataSet2("*", "tblProductinward", "Productcode", box0.Text, "Batchid", box4.Text);

                                        string suppliercode = dschm20.Tables[0].Rows[0]["Supliercode"].ToString();


                                        if (suppliercode == "0000")
                                        {


                                            string suppliercode1 = " ";
                                            string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                            DataSet dschm40 = clsgd.GetcondDataSet9("*", "tblmanufacture", "ManufactureCode", manufacturecode);
                                            string manufacturename = dschm40.Tables[0].Rows[0]["ManufactureName"].ToString();

                                            Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, "0", "0", fnselprice, pramount, discount, sumtaxrate, totamount, "0", box13.Text, suppliercode1, manufacturename, "Y", invno, box14.Text, "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                        }
                                        else
                                        {
                                            DataSet dschm30 = clsgd.GetcondDataSet9("*", "tblsuppliermaster", "SupplierCode", suppliercode);

                                            string suppliename = dschm30.Tables[0].Rows[0]["SupplierName"].ToString();

                                            // string suppliercode1 = dschm20.Tables[0].Rows[0]["Supliercode"].ToString();
                                            string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();

                                            DataSet dschm40 = clsgd.GetcondDataSet9("*", "tblmanufacture", "ManufactureCode", manufacturecode);
                                            string manufacturename = dschm40.Tables[0].Rows[0]["ManufactureName"].ToString();

                                            Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, "0", "0", fnselprice, pramount, discount, sumtaxrate, totamount, "0", box13.Text, suppliename, manufacturename, "Y", invno, box14.Text, "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                        }


                                    }
                                    else
                                    {
                                        string D_code = "0";
                                        DateTime boxdate = Convert.ToDateTime(box3.Text);
                                        string boxdate1 = boxdate.ToString("yyyy-MM-dd");

                                        DataSet dschm20 = clsgd.GetcondDataSet2("*", "tblProductinward", "Productcode", box0.Text, "Batchid", box4.Text);

                                        string suppliercode = dschm20.Tables[0].Rows[0]["Supliercode"].ToString();

                                        if (suppliercode == "0000")
                                        {
                                            string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                            if (manufacturecode == "0")
                                            {
                                                string manufacturecode1 = " ";
                                                string suppliercode1 = " ";
                                                Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, "0", "0", fnselprice, pramount, discount, sumtaxrate, totamount, "0", box13.Text, suppliercode1, manufacturecode1, "Y", invno, box14.Text, "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                            }
                                            else
                                            {
                                                string manufacturecode1 = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();

                                                DataSet dschm40 = clsgd.GetcondDataSet9("*", "tblmanufacture", "ManufactureCode", manufacturecode);
                                                string manufacturename = dschm40.Tables[0].Rows[0]["ManufactureName"].ToString();

                                                string suppliercode1 = " ";
                                                Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, "0", "0", fnselprice, pramount, discount, sumtaxrate, totamount, "0", box13.Text, suppliercode1, manufacturename, "Y", invno, box14.Text, "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                            }

                                        }
                                        else
                                        {
                                            //string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                            // string suppliercode1 = dschm20.Tables[0].Rows[0]["Supliercode"].ToString();

                                            DataSet dschm30 = clsgd.GetcondDataSet9("*", "tblsuppliermaster", "SupplierCode", suppliercode);

                                            string suppliename = dschm30.Tables[0].Rows[0]["SupplierName"].ToString();

                                            // string suppliercode1 = dschm20.Tables[0].Rows[0]["Supliercode"].ToString();
                                            string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();

                                            if (manufacturecode == "0")
                                            {

                                                string manufacturecode1 = " ";
                                                Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, "0", "0", fnselprice, pramount, discount, sumtaxrate, totamount, "0", box13.Text, suppliename, manufacturecode1, "Y", invno, box14.Text, "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                            }
                                            else
                                            {

                                                string manufacturecode1 = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                DataSet dschm40 = clsgd.GetcondDataSet9("*", "tblmanufacture", "ManufactureCode", manufacturecode);
                                                string manufacturename = dschm40.Tables[0].Rows[0]["ManufactureName"].ToString();
                                                Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, "0", "0", fnselprice, pramount, discount, sumtaxrate, totamount, "0", box13.Text, suppliename, manufacturename, "Y", invno, box14.Text, "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                            }

                                        }


                                    }



                                    //Clsprdinw.PdfProductsale("INSERT_PDF_PRODUCTSALE", txtdoctorname.Text, txtdoctorname.Text, txtpatientname.Text, txtinvoicenor.Text, ddpaymenttype.SelectedItem.Text, txtdate.Text, box0.Text, box1.Text, box3.Text, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, box11.Text, fnselprice, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                    //string productcode1 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductcode") as TextBox).Text);
                                    //SqlConnection con58 = new SqlConnection(strconn1);
                                    //SqlCommand cmd58 = new SqlCommand("select sum(Quantity) as Quantity  from tblProductsale where  Productcode = " + box0.Text + " AND Batchid = " + box4.Text + "", con58);
                                    //SqlDataAdapter da58 = new SqlDataAdapter(cmd58);
                                    //DataSet ds58 = new DataSet();

                                    //da58.Fill(ds58);

                                    //int Quantity = Convert.ToInt32(ds58.Tables[0].Rows[0]["Quantity"].ToString());

                                    //int sthand = Convert.ToInt32(box5.Text);

                                    // TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtquantity");

                                    double Quantity = Convert.ToDouble(box8.Text);

                                    SqlConnection con59 = new SqlConnection(strconn1);
                                    SqlCommand cmd59 = new SqlCommand("select  Stockinhand  from tblProductinward where  Productcode = " + box0.Text + " AND ProductName = '" + box1.Text + "' AND Batchid = '" + box4.Text + "'", con59);
                                    SqlDataAdapter da59 = new SqlDataAdapter(cmd59);
                                    DataSet ds59 = new DataSet();

                                    da59.Fill(ds59);

                                    double sthand = Convert.ToDouble(ds59.Tables[0].Rows[0]["Stockinhand"].ToString());


                                    string sthand1 = Convert.ToString(sthand - Quantity);



                                    //SqlConnection conn25 = new SqlConnection(strconn1);
                                    //conn25.Open();
                                    //SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "'", conn25);
                                    //cmd25.ExecuteNonQuery();
                                    //rowIndex++;


                                    DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblProductMaster", "Productcode", box0.Text);
                                    string flag2 = dsgroup10.Tables[0].Rows[0]["Pharmflag"].ToString();
                                    if (flag2 == "Y")
                                    {



                                        SqlConnection conn25 = new SqlConnection(strconn1);
                                        conn25.Open();
                                        SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "'", conn25);
                                        cmd25.ExecuteNonQuery();
                                        rowIndex++;
                                    }
                                    else
                                    {
                                        SqlConnection con60 = new SqlConnection(strconn1);
                                        SqlCommand cmd60 = new SqlCommand("select  * from tblProductinward where  Productcode = " + box0.Text + " order by Invoicedate", con60);
                                        SqlDataAdapter da60 = new SqlDataAdapter(cmd60);
                                        DataSet ds60 = new DataSet();

                                        da60.Fill(ds60);
                                        for (int k = 0; k < ds60.Tables[0].Rows.Count; k++)
                                        {
                                            if (Quantity != 0)
                                            {
                                                sthand1 = ds60.Tables[0].Rows[k]["Stockinhand"].ToString();
                                                double sthand10 = Convert.ToDouble(sthand1);
                                                SqlConnection conn255 = new SqlConnection(strconn11);
                                                conn255.Open();
                                                if (Quantity > sthand10)
                                                {
                                                    Quantity = Quantity - sthand10;
                                                    sthand10 = 0;
                                                    SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand10 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                    cmd255.ExecuteNonQuery();
                                                    rowIndex++;
                                                }
                                                else
                                                {
                                                    sthand10 = sthand10 - Quantity;
                                                    Quantity = 0;
                                                    SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand10 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                    cmd255.ExecuteNonQuery();
                                                    rowIndex++;
                                                }
                                            }
                                        }
                                    }



                                }

                                else
                                {
                                    cal();
                                    DataSet dschm10 = clsgd.GetcondDataSet9("*", "tblDoctor", "D_name", doctorname);
                                    if (dschm10.Tables[0].Rows.Count > 0)
                                    {
                                        string D_code = dschm10.Tables[0].Rows[0]["D_code"].ToString();
                                        DateTime boxdate = Convert.ToDateTime(box3.Text);
                                        string boxdate1 = boxdate.ToString("yyyy-MM-dd");

                                        DataSet dschm20 = clsgd.GetcondDataSet2("*", "tblProductinward", "Productcode", box0.Text, "Batchid", box4.Text);

                                        string suppliercode = dschm20.Tables[0].Rows[0]["SuppplierCode"].ToString();
                                        if (suppliercode == "0000")
                                        {
                                            string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();

                                            if (manufacturecode == "0")
                                            {

                                                string suppliercode1 = " ";
                                                string manufacturecode1 = " ";

                                                Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, dvalue1, fnselprice, pramount, discount, sumtaxrate, totamount, "0", box13.Text, suppliercode1, manufacturecode1, "Y", invno, box14.Text, "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                                double Quantity = Convert.ToDouble(box8.Text);

                                                SqlConnection con59 = new SqlConnection(strconn1);
                                                SqlCommand cmd59 = new SqlCommand("select  Stockinhand  from tblProductinward where  Productcode = " + box0.Text + " AND ProductName = '" + box1.Text + "' AND Batchid = '" + box4.Text + "'", con59);
                                                SqlDataAdapter da59 = new SqlDataAdapter(cmd59);
                                                DataSet ds59 = new DataSet();

                                                da59.Fill(ds59);

                                                int sthand = Convert.ToInt32(ds59.Tables[0].Rows[0]["Stockinhand"].ToString());


                                                string sthand1 = Convert.ToString(sthand - Quantity);



                                                //SqlConnection conn25 = new SqlConnection(strconn1);
                                                //conn25.Open();
                                                //SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "'", conn25);
                                                //cmd25.ExecuteNonQuery();
                                                //rowIndex++;


                                                DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblProductMaster", "Productcode", box0.Text);
                                                string flag2 = dsgroup10.Tables[0].Rows[0]["Pharmflag"].ToString();
                                                if (flag2 == "Y")
                                                {



                                                    SqlConnection conn25 = new SqlConnection(strconn1);
                                                    conn25.Open();
                                                    SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "'", conn25);
                                                    cmd25.ExecuteNonQuery();
                                                    rowIndex++;
                                                }
                                                else
                                                {
                                                    SqlConnection con60 = new SqlConnection(strconn1);
                                                    SqlCommand cmd60 = new SqlCommand("select  * from tblProductinward where  Productcode = " + box0.Text + " order by Invoicedate", con60);
                                                    SqlDataAdapter da60 = new SqlDataAdapter(cmd60);
                                                    DataSet ds60 = new DataSet();

                                                    da60.Fill(ds60);
                                                    for (int k = 0; k < ds60.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (Quantity != 0)
                                                        {
                                                            sthand1 = ds60.Tables[0].Rows[k]["Stockinhand"].ToString();
                                                            double sthand10 = Convert.ToDouble(sthand1);
                                                            SqlConnection conn255 = new SqlConnection(strconn11);
                                                            conn255.Open();
                                                            if (Quantity > sthand10)
                                                            {
                                                                Quantity = Quantity - sthand10;
                                                                sthand10 = 0;
                                                                SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand10 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                                cmd255.ExecuteNonQuery();
                                                                rowIndex++;
                                                            }
                                                            else
                                                            {
                                                                sthand10 = sthand10 - Quantity;
                                                                Quantity = 0;
                                                                SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand10 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                                cmd255.ExecuteNonQuery();
                                                                rowIndex++;
                                                            }
                                                        }
                                                    }




                                                }



                                            }
                                            else
                                            {
                                                string manufacturecode1 = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();

                                                DataSet dschm40 = clsgd.GetcondDataSet9("*", "tblmanufacture", "ManufactureCode", manufacturecode);
                                                string manufacturename = dschm40.Tables[0].Rows[0]["ManufactureName"].ToString();

                                                string suppliercode1 = " ";
                                                Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, dvalue1, fnselprice, pramount, discount, sumtaxrate, totamount, "0", box13.Text, suppliercode1, manufacturename, "Y", "Y", box14.Text, "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                                double Quantity = Convert.ToDouble(box8.Text);

                                                SqlConnection con59 = new SqlConnection(strconn1);
                                                SqlCommand cmd59 = new SqlCommand("select  Stockinhand  from tblProductinward where  Productcode = " + box0.Text + " AND ProductName = '" + box1.Text + "' AND Batchid = '" + box4.Text + "'", con59);
                                                SqlDataAdapter da59 = new SqlDataAdapter(cmd59);
                                                DataSet ds59 = new DataSet();

                                                da59.Fill(ds59);

                                                int sthand = Convert.ToInt32(ds59.Tables[0].Rows[0]["Stockinhand"].ToString());


                                                string sthand1 = Convert.ToString(sthand - Quantity);



                                                //SqlConnection conn25 = new SqlConnection(strconn1);
                                                //conn25.Open();
                                                //SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "'", conn25);
                                                //cmd25.ExecuteNonQuery();
                                                //rowIndex++;


                                                DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblProductMaster", "Productcode", box0.Text);
                                                string flag2 = dsgroup10.Tables[0].Rows[0]["Pharmflag"].ToString();
                                                if (flag2 == "Y")
                                                {



                                                    SqlConnection conn25 = new SqlConnection(strconn1);
                                                    conn25.Open();
                                                    SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "'", conn25);
                                                    cmd25.ExecuteNonQuery();
                                                    rowIndex++;
                                                }
                                                else
                                                {
                                                    SqlConnection con60 = new SqlConnection(strconn1);
                                                    SqlCommand cmd60 = new SqlCommand("select  * from tblProductinward where  Productcode = " + box0.Text + " order by Invoicedate", con60);
                                                    SqlDataAdapter da60 = new SqlDataAdapter(cmd60);
                                                    DataSet ds60 = new DataSet();

                                                    da60.Fill(ds60);
                                                    for (int k = 0; k < ds60.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (Quantity != 0)
                                                        {
                                                            sthand1 = ds60.Tables[0].Rows[k]["Stockinhand"].ToString();
                                                            double sthand10 = Convert.ToDouble(sthand1);
                                                            SqlConnection conn255 = new SqlConnection(strconn11);
                                                            conn255.Open();
                                                            if (Quantity > sthand10)
                                                            {
                                                                Quantity = Quantity - sthand10;
                                                                sthand10 = 0;
                                                                SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand10 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                                cmd255.ExecuteNonQuery();
                                                                rowIndex++;
                                                            }
                                                            else
                                                            {
                                                                sthand10 = sthand10 - Quantity;
                                                                Quantity = 0;
                                                                SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand10 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                                cmd255.ExecuteNonQuery();
                                                                rowIndex++;
                                                            }
                                                        }
                                                    }




                                                }

                                            }

                                        }

                                        else
                                        {

                                            string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();

                                            if (manufacturecode == "0")
                                            {
                                                string manufacturecode1 = " ";

                                                DataSet dschm30 = clsgd.GetcondDataSet9("*", "tblsuppliermaster", "SupplierCode", suppliercode);

                                                string suppliename = dschm30.Tables[0].Rows[0]["SupplierName"].ToString();

                                                // string suppliercode1 = dschm20.Tables[0].Rows[0]["SuppplierCode"].ToString();

                                                Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, dvalue1, fnselprice, pramount, discount, sumtaxrate, totamount, "0", box13.Text, suppliename, manufacturecode1, "Y", invno, box14.Text, "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                                double Quantity = Convert.ToDouble(box8.Text);

                                                SqlConnection con59 = new SqlConnection(strconn1);
                                                SqlCommand cmd59 = new SqlCommand("select  Stockinhand  from tblProductinward where  Productcode = " + box0.Text + " AND ProductName = '" + box1.Text + "' AND Batchid = '" + box4.Text + "'", con59);
                                                SqlDataAdapter da59 = new SqlDataAdapter(cmd59);
                                                DataSet ds59 = new DataSet();

                                                da59.Fill(ds59);

                                                int sthand = Convert.ToInt32(ds59.Tables[0].Rows[0]["Stockinhand"].ToString());


                                                string sthand1 = Convert.ToString(sthand - Quantity);



                                                //SqlConnection conn25 = new SqlConnection(strconn1);
                                                //conn25.Open();
                                                //SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "'", conn25);
                                                //cmd25.ExecuteNonQuery();
                                                //rowIndex++;



                                                DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblProductMaster", "Productcode", box0.Text);
                                                string flag2 = dsgroup10.Tables[0].Rows[0]["Pharmflag"].ToString();
                                                if (flag2 == "Y")
                                                {



                                                    SqlConnection conn25 = new SqlConnection(strconn1);
                                                    conn25.Open();
                                                    SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "'", conn25);
                                                    cmd25.ExecuteNonQuery();
                                                    rowIndex++;
                                                }
                                                else
                                                {
                                                    SqlConnection con60 = new SqlConnection(strconn1);
                                                    SqlCommand cmd60 = new SqlCommand("select  * from tblProductinward where  Productcode = " + box0.Text + " order by Invoicedate", con60);
                                                    SqlDataAdapter da60 = new SqlDataAdapter(cmd60);
                                                    DataSet ds60 = new DataSet();

                                                    da60.Fill(ds60);
                                                    for (int k = 0; k < ds60.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (Quantity != 0)
                                                        {
                                                            sthand1 = ds60.Tables[0].Rows[k]["Stockinhand"].ToString();
                                                            double sthand10 = Convert.ToDouble(sthand1);
                                                            SqlConnection conn255 = new SqlConnection(strconn11);
                                                            conn255.Open();
                                                            if (Quantity > sthand10)
                                                            {
                                                                Quantity = Quantity - sthand10;
                                                                sthand10 = 0;
                                                                SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand10 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                                cmd255.ExecuteNonQuery();
                                                                rowIndex++;
                                                            }
                                                            else
                                                            {
                                                                sthand10 = sthand10 - Quantity;
                                                                Quantity = 0;
                                                                SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand10 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                                cmd255.ExecuteNonQuery();
                                                                rowIndex++;
                                                            }
                                                        }
                                                    }




                                                }
                                            }
                                            else
                                            {
                                                string manufacturecode1 = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();

                                                DataSet dschm30 = clsgd.GetcondDataSet9("*", "tblsuppliermaster", "SupplierCode", suppliercode);

                                                string suppliename = dschm30.Tables[0].Rows[0]["SupplierName"].ToString();

                                                DataSet dschm40 = clsgd.GetcondDataSet9("*", "tblmanufacture", "ManufactureCode", manufacturecode);
                                                string manufacturename = dschm40.Tables[0].Rows[0]["ManufactureName"].ToString();

                                                // string suppliercode1 = dschm20.Tables[0].Rows[0]["SuppplierCode"].ToString();
                                                ///bh
                                                Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, dvalue1, fnselprice, pramount, discount, sumtaxrate, totamount, "0", box13.Text, suppliename, manufacturename, "Y", invno, box14.Text, "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                                double Quantity = Convert.ToDouble(box8.Text);

                                                SqlConnection con59 = new SqlConnection(strconn1);
                                                SqlCommand cmd59 = new SqlCommand("select  Stockinhand from tblProductinward where  Productcode = " + box0.Text + " AND ProductName = '" + box1.Text + "' AND Batchid = '" + box4.Text + "'", con59);
                                                SqlDataAdapter da59 = new SqlDataAdapter(cmd59);
                                                DataSet ds59 = new DataSet();

                                                da59.Fill(ds59);

                                                int sthand = Convert.ToInt32(ds59.Tables[0].Rows[0]["Stockinhand"].ToString());


                                                string sthand1 = Convert.ToString(sthand - Quantity);



                                                //SqlConnection conn25 = new SqlConnection(strconn1);
                                                //conn25.Open();
                                                //SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "'", conn25);
                                                //cmd25.ExecuteNonQuery();
                                                //rowIndex++;


                                                DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblProductMaster", "Productcode", box0.Text);
                                                string flag2 = dsgroup10.Tables[0].Rows[0]["Pharmflag"].ToString();
                                                if (flag2 == "Y")
                                                {



                                                    SqlConnection conn25 = new SqlConnection(strconn1);
                                                    conn25.Open();
                                                    SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "'", conn25);
                                                    cmd25.ExecuteNonQuery();
                                                    rowIndex++;
                                                }
                                                else
                                                {
                                                    SqlConnection con60 = new SqlConnection(strconn1);
                                                    SqlCommand cmd60 = new SqlCommand("select  * from tblProductinward where  Productcode = " + box0.Text + " order by Invoicedate", con60);
                                                    SqlDataAdapter da60 = new SqlDataAdapter(cmd60);
                                                    DataSet ds60 = new DataSet();

                                                    da60.Fill(ds60);
                                                    for (int k = 0; k < ds60.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (Quantity != 0)
                                                        {
                                                            sthand1 = ds60.Tables[0].Rows[k]["Stockinhand"].ToString();
                                                            double sthand10 = Convert.ToDouble(sthand1);
                                                            SqlConnection conn255 = new SqlConnection(strconn11);
                                                            conn255.Open();
                                                            if (Quantity > sthand10)
                                                            {
                                                                Quantity = Quantity - sthand10;
                                                                sthand10 = 0;
                                                                SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand10 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                                cmd255.ExecuteNonQuery();
                                                                rowIndex++;
                                                            }
                                                            else
                                                            {
                                                                sthand10 = sthand10 - Quantity;
                                                                Quantity = 0;
                                                                SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand10 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                                cmd255.ExecuteNonQuery();
                                                                rowIndex++;
                                                            }
                                                        }
                                                    }




                                                }

                                            }

                                        }



                                    }
                                    else
                                    {
                                        string D_code = "0";
                                        DateTime boxdate = Convert.ToDateTime(box3.Text);
                                        string boxdate1 = boxdate.ToString("yyyy-MM-dd");

                                        DataSet dschm20 = clsgd.GetcondDataSet2("*", "tblProductinward", "Productcode", box0.Text, "Batchid", box4.Text);

                                        string suppliercode = dschm20.Tables[0].Rows[0]["SuppplierCode"].ToString();
                                        if (suppliercode == "0000")
                                        {
                                            string suppliercode1 = " ";
                                            string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                            if (manufacturecode == "0")
                                            {
                                                string manufacturecode1 = " ";

                                                Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, dvalue1, fnselprice, pramount, discount, sumtaxrate, totamount, "0", box13.Text, suppliercode1, manufacturecode1, "Y", invno, box14.Text, "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                                double Quantity = Convert.ToDouble(box8.Text);

                                                SqlConnection con59 = new SqlConnection(strconn1);
                                                SqlCommand cmd59 = new SqlCommand("select  Stockinhand  from tblProductinward where  Productcode = " + box0.Text + " AND ProductName = '" + box1.Text + "' AND Batchid = '" + box4.Text + "'", con59);
                                                SqlDataAdapter da59 = new SqlDataAdapter(cmd59);
                                                DataSet ds59 = new DataSet();

                                                da59.Fill(ds59);

                                                int sthand = Convert.ToInt32(ds59.Tables[0].Rows[0]["Stockinhand"].ToString());


                                                string sthand1 = Convert.ToString(sthand - Quantity);



                                                //SqlConnection conn25 = new SqlConnection(strconn1);
                                                //conn25.Open();
                                                //SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "'", conn25);
                                                //cmd25.ExecuteNonQuery();
                                                //rowIndex++;



                                                DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblProductMaster", "Productcode", box0.Text);
                                                string flag2 = dsgroup10.Tables[0].Rows[0]["Pharmflag"].ToString();
                                                if (flag2 == "Y")
                                                {



                                                    SqlConnection conn25 = new SqlConnection(strconn1);
                                                    conn25.Open();
                                                    SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "'", conn25);
                                                    cmd25.ExecuteNonQuery();
                                                    rowIndex++;
                                                }
                                                else
                                                {
                                                    SqlConnection con60 = new SqlConnection(strconn1);
                                                    SqlCommand cmd60 = new SqlCommand("select  * from tblProductinward where  Productcode = " + box0.Text + " order by Invoicedate", con60);
                                                    SqlDataAdapter da60 = new SqlDataAdapter(cmd60);
                                                    DataSet ds60 = new DataSet();

                                                    da60.Fill(ds60);
                                                    for (int k = 0; k < ds60.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (Quantity != 0)
                                                        {
                                                            sthand1 = ds60.Tables[0].Rows[k]["Stockinhand"].ToString();
                                                            double sthand10 = Convert.ToDouble(sthand1);
                                                            SqlConnection conn255 = new SqlConnection(strconn11);
                                                            conn255.Open();
                                                            if (Quantity > sthand10)
                                                            {
                                                                Quantity = Quantity - sthand10;
                                                                sthand10 = 0;
                                                                SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand10 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                                cmd255.ExecuteNonQuery();
                                                                rowIndex++;
                                                            }
                                                            else
                                                            {
                                                                sthand10 = sthand10 - Quantity;
                                                                Quantity = 0;
                                                                SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand10 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                                cmd255.ExecuteNonQuery();
                                                                rowIndex++;
                                                            }
                                                        }
                                                    }




                                                }





                                            }
                                            else
                                            {

                                                string manufacturecode1 = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();

                                                DataSet dschm40 = clsgd.GetcondDataSet9("*", "tblmanufacture", "ManufactureCode", manufacturecode);
                                                string manufacturename = dschm40.Tables[0].Rows[0]["ManufactureName"].ToString();

                                                Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, dvalue1, fnselprice, pramount, discount, sumtaxrate, totamount, "0", box13.Text, suppliercode1, manufacturename, "Y", "Y", box14.Text, "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                                double Quantity = Convert.ToDouble(box8.Text);

                                                SqlConnection con59 = new SqlConnection(strconn1);
                                                SqlCommand cmd59 = new SqlCommand("select  Stockinhand  from tblProductinward where  Productcode = " + box0.Text + " AND ProductName = '" + box1.Text + "' AND Batchid = '" + box4.Text + "'", con59);
                                                SqlDataAdapter da59 = new SqlDataAdapter(cmd59);
                                                DataSet ds59 = new DataSet();

                                                da59.Fill(ds59);

                                                int sthand = Convert.ToInt32(ds59.Tables[0].Rows[0]["Stockinhand"].ToString());


                                                string sthand1 = Convert.ToString(sthand - Quantity);



                                                //SqlConnection conn25 = new SqlConnection(strconn1);
                                                //conn25.Open();
                                                //SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "'", conn25);
                                                //cmd25.ExecuteNonQuery();
                                                //rowIndex++;



                                                DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblProductMaster", "Productcode", box0.Text);
                                                string flag2 = dsgroup10.Tables[0].Rows[0]["Pharmflag"].ToString();
                                                if (flag2 == "Y")
                                                {



                                                    SqlConnection conn25 = new SqlConnection(strconn1);
                                                    conn25.Open();
                                                    SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "'", conn25);
                                                    cmd25.ExecuteNonQuery();
                                                    rowIndex++;
                                                }
                                                else
                                                {
                                                    SqlConnection con60 = new SqlConnection(strconn1);
                                                    SqlCommand cmd60 = new SqlCommand("select  * from tblProductinward where  Productcode = " + box0.Text + " order by Invoicedate", con60);
                                                    SqlDataAdapter da60 = new SqlDataAdapter(cmd60);
                                                    DataSet ds60 = new DataSet();

                                                    da60.Fill(ds60);
                                                    for (int k = 0; k < ds60.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (Quantity != 0)
                                                        {
                                                            sthand1 = ds60.Tables[0].Rows[k]["Stockinhand"].ToString();
                                                            double sthand10 = Convert.ToDouble(sthand1);
                                                            SqlConnection conn255 = new SqlConnection(strconn11);
                                                            conn255.Open();
                                                            if (Quantity > sthand10)
                                                            {
                                                                Quantity = Quantity - sthand10;
                                                                sthand10 = 0;
                                                                SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand10 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                                cmd255.ExecuteNonQuery();
                                                                rowIndex++;
                                                            }
                                                            else
                                                            {
                                                                sthand10 = sthand10 - Quantity;
                                                                Quantity = 0;
                                                                SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand10 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                                cmd255.ExecuteNonQuery();
                                                                rowIndex++;
                                                            }
                                                        }
                                                    }




                                                }

                                            }
                                        }

                                        else
                                        {

                                            // string suppliercode1 = dschm20.Tables[0].Rows[0]["SuppplierCode"].ToString();
                                            string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();

                                            if (manufacturecode == "0")
                                            {
                                                string manufacturecode1 = " ";
                                                DataSet dschm30 = clsgd.GetcondDataSet9("*", "tblsuppliermaster", "SupplierCode", suppliercode);

                                                string suppliename = dschm30.Tables[0].Rows[0]["SupplierName"].ToString();



                                                Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, dvalue1, fnselprice, pramount, discount, sumtaxrate, totamount, "0", box13.Text, suppliename, manufacturecode1, "Y", invno, box14.Text, "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                                double Quantity = Convert.ToDouble(box8.Text);

                                                SqlConnection con59 = new SqlConnection(strconn1);
                                                SqlCommand cmd59 = new SqlCommand("select  Stockinhand  from tblProductinward where  Productcode = " + box0.Text + " AND ProductName = '" + box1.Text + "' AND Batchid = '" + box4.Text + "'", con59);
                                                SqlDataAdapter da59 = new SqlDataAdapter(cmd59);
                                                DataSet ds59 = new DataSet();

                                                da59.Fill(ds59);

                                                int sthand = Convert.ToInt32(ds59.Tables[0].Rows[0]["Stockinhand"].ToString());


                                                string sthand1 = Convert.ToString(sthand - Quantity);

                                                //kiran


                                                DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblProductMaster", "Productcode", box0.Text);
                                                string flag2 = dsgroup10.Tables[0].Rows[0]["Pharmflag"].ToString();
                                                if (flag2 == "Y")
                                                {



                                                    SqlConnection conn25 = new SqlConnection(strconn1);
                                                    conn25.Open();
                                                    SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "'", conn25);
                                                    cmd25.ExecuteNonQuery();
                                                    rowIndex++;
                                                }
                                                else
                                                {
                                                    SqlConnection con60 = new SqlConnection(strconn1);
                                                    SqlCommand cmd60 = new SqlCommand("select  * from tblProductinward where  Productcode = " + box0.Text + " order by Invoicedate", con60);
                                                    SqlDataAdapter da60 = new SqlDataAdapter(cmd60);
                                                    DataSet ds60 = new DataSet();

                                                    da60.Fill(ds60);
                                                    for (int k = 0; k < ds60.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (Quantity != 0)
                                                        {
                                                            sthand1 = ds60.Tables[0].Rows[k]["Stockinhand"].ToString();
                                                            double sthand10 = Convert.ToDouble(sthand1);
                                                            SqlConnection conn255 = new SqlConnection(strconn11);
                                                            conn255.Open();
                                                            if (Quantity > sthand10)
                                                            {
                                                                Quantity = Quantity - sthand10;
                                                                sthand10 = 0;
                                                                SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand10 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                                cmd255.ExecuteNonQuery();
                                                                rowIndex++;
                                                            }
                                                            else
                                                            {
                                                                sthand10 = sthand10 - Quantity;
                                                                Quantity = 0;
                                                                SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand10 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                                cmd255.ExecuteNonQuery();
                                                                rowIndex++;
                                                            }
                                                        }
                                                    }




                                                }

                                            }
                                            else
                                            {
                                                //string manufacturecode1 = "No manufacture";
                                                string manufacturecode1 = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                DataSet dschm30 = clsgd.GetcondDataSet9("*", "tblsuppliermaster", "SupplierCode", suppliercode);

                                                DataSet dschm40 = clsgd.GetcondDataSet9("*", "tblmanufacture", "ManufactureCode", manufacturecode);
                                                string manufacturename = dschm40.Tables[0].Rows[0]["ManufactureName"].ToString();


                                                string suppliename = dschm30.Tables[0].Rows[0]["SupplierName"].ToString();

                                                Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, dvalue1, fnselprice, pramount, discount, sumtaxrate, totamount, "0", box13.Text, suppliename, manufacturename, "Y", invno, box14.Text, "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                                double Quantity = Convert.ToDouble(box8.Text);

                                                SqlConnection con59 = new SqlConnection(strconn1);
                                                SqlCommand cmd59 = new SqlCommand("select  Stockinhand  from tblProductinward where  Productcode = " + box0.Text + " AND ProductName = '" + box1.Text + "' AND Batchid = '" + box4.Text + "'", con59);
                                                SqlDataAdapter da59 = new SqlDataAdapter(cmd59);
                                                DataSet ds59 = new DataSet();

                                                da59.Fill(ds59);

                                                int sthand = Convert.ToInt32(ds59.Tables[0].Rows[0]["Stockinhand"].ToString());


                                                string sthand1 = Convert.ToString(sthand - Quantity);


                                                DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblProductMaster", "Productcode", box0.Text);
                                                string flag2 = dsgroup10.Tables[0].Rows[0]["Pharmflag"].ToString();
                                                if (flag2 == "Y")
                                                {



                                                    SqlConnection conn25 = new SqlConnection(strconn1);
                                                    conn25.Open();
                                                    SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "'", conn25);
                                                    cmd25.ExecuteNonQuery();
                                                    rowIndex++;
                                                }
                                                else
                                                {
                                                    SqlConnection con60 = new SqlConnection(strconn1);
                                                    SqlCommand cmd60 = new SqlCommand("select  * from tblProductinward where  Productcode = " + box0.Text + " order by Invoicedate", con60);
                                                    SqlDataAdapter da60 = new SqlDataAdapter(cmd60);
                                                    DataSet ds60 = new DataSet();

                                                    da60.Fill(ds60);
                                                    for (int k = 0; k < ds60.Tables[0].Rows.Count; k++)
                                                    {
                                                        if (Quantity != 0)
                                                        {
                                                            sthand1 = ds60.Tables[0].Rows[k]["Stockinhand"].ToString();
                                                            double sthand10 = Convert.ToDouble(sthand1);
                                                            SqlConnection conn255 = new SqlConnection(strconn11);
                                                            conn255.Open();
                                                            if (Quantity > sthand10)
                                                            {
                                                                Quantity = Quantity - sthand10;
                                                                sthand10 = 0;
                                                                SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand10 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                                cmd255.ExecuteNonQuery();
                                                                rowIndex++;
                                                            }
                                                            else
                                                            {
                                                                sthand10 = sthand10 - Quantity;
                                                                Quantity = 0;
                                                                SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand10 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                                cmd255.ExecuteNonQuery();
                                                                rowIndex++;
                                                            }
                                                        }
                                                    }




                                                }








                                            }
                                        }


                                        //Clsprdinw.PdfProductsale("INSERT_PDF_PRODUCTSALE", txtdoctorname.Text, txtdoctorname.Text, txtpatientname.Text, txtinvoicenor.Text, ddpaymenttype.SelectedItem.Text, txtdate.Text, box0.Text, box1.Text, box3.Text, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, box11.Text, fnselprice, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                        //  Clsprdinw.PdfProductsale("INSERT_PDF_PRODUCTSALE", txtdoctorname.Text, txtdoctorname.Text, txtpatientname.Text, txtinvoicenor.Text, ddpaymenttype.SelectedItem.Text, txtdate.Text, box0.Text, box1.Text, box3.Text, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, box11.Text, fnselprice, pramount, discount, sumtaxrate, totamount, "0", box13.Text, Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                        //int Quantity = Convert.ToInt32(box8.Text);

                                        //SqlConnection con59 = new SqlConnection(strconn1);
                                        //SqlCommand cmd59 = new SqlCommand("select  Stockinhand  from tblProductinward where  Productcode = " + box0.Text + " AND ProductName = '" + box1.Text + "' AND Batchid = '" + box4.Text + "'", con59);
                                        //SqlDataAdapter da59 = new SqlDataAdapter(cmd59);
                                        //DataSet ds59 = new DataSet();

                                        //da59.Fill(ds59);

                                        //int sthand = Convert.ToInt32(ds59.Tables[0].Rows[0]["Stockinhand"].ToString());


                                        //string sthand1 = Convert.ToString(sthand - Quantity);



                                        //SqlConnection conn25 = new SqlConnection(strconn1);
                                        //conn25.Open();
                                        //SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "'", conn25);
                                        //cmd25.ExecuteNonQuery();
                                        //// }
                                        //rowIndex++;

                                    }


                                }
                            }
                        }





                            // Clstax.SALESTAX("INSERT_SALESTAX", txtinvoicenor.Text, box12.Text,  box9.Text,box7.Text, Session["username"].ToString(),sMacAddress, sqlFormattedDate);




                            //SqlConnection con10 = new SqlConnection(strconn1);
                            //con10.Open();
                            //SqlCommand cmd = new SqlCommand("select Doctorname from tblProductsale", con10);
                            //SqlDataAdapter da = new SqlDataAdapter(cmd);
                            //DataSet ds = new DataSet();
                            //da.Fill(ds);
                            //string Doctorname = ds.Tables[0].Rows[0]["Doctorname"].ToString();
                            ////con10.Close();

                            //System.DateTime Dtnow1 = DateTime.Now;
                            //string sqlFormattedDate1 = Dtnow1.ToString("dd/MM/yyyy");

                            //ClsBLGP2.TempProductaccount("INSERT_TempPRODUCTACCOUNT", Doctorname, Session["username"].ToString(), sMacAddress, sqlFormattedDate);
                            //Thread.Sleep(5000);

                            //SqlConnection con16 = new SqlConnection(strconn1);
                            //con16.Open();
                            //SqlCommand cmd10 = new SqlCommand("select Accountid from tblTempProductaccount", con16);
                            //SqlDataAdapter da10 = new SqlDataAdapter(cmd10);
                            //DataSet ds10 = new DataSet();
                            //da10.Fill(ds10);
                            //string Accountid = ds10.Tables[0].Rows[0]["Accountid"].ToString();



                            //SqlConnection con15 = new SqlConnection(strconn1);
                            //con15.Open();
                            //SqlCommand cmd16 = new SqlCommand("delete FROM tblTempProductaccount where Accountid='" + Accountid + "'", con15);
                            //cmd16.ExecuteNonQuery();











                            if (ddpaymenttype.SelectedItem.Text == "CARD")
                            {

                                if (txtcardno.Text == "")
                                {
                                    Master.ShowModal("Enter CARD No. !!!", "txttransno", 1);
                                    return;
                                }

                                if (txtcramount.Text == "")
                                {
                                    Master.ShowModal("Enter Amount. !!!", " txtcramount", 1);
                                    return;
                                }

                                if (ddlpaytype.SelectedItem.Text == "Select")
                                {
                                    Master.ShowModal("Enter Card type. !!!", "ddlpaytype", 1);
                                    return;

                                }

                                if (txttransno.Text == " ")
                                {
                                    Master.ShowModal("Enter transno. !!!", "txttransno", 1);
                                    return;

                                }




                                lblvbillno.Text = invoiceno;

                                string card = txtcardno.Text;
                                string card20 = "xxxx-xxxx-xxxx-" + card;


                                Clscard.Cardinfo("INSERT_CARDINFO", ddlpaytype.SelectedItem.Text, lblvbillno.Text, card20, txtcramount.Text, "N", txttransno.Text, txtdate.Text, Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                int rowIndex2 = 0;
                                //StringCollection sc = new StringCollection();
                                if (ViewState["CurrentTable"] != null)
                                {
                                    DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable"];
                                    DataRow drCurrentRow1 = null;
                                    if (dtCurrentTable.Rows.Count > 0)
                                    {
                                        for (int l = 1; l <= dtCurrentTable1.Rows.Count; l++)
                                        {
                                            TextBox box20 = (TextBox)Gridview1.Rows[rowIndex2].Cells[1].FindControl("txtproductcode");
                                            TextBox box21 = (TextBox)Gridview1.Rows[rowIndex2].Cells[2].FindControl("txtproductname");
                                            TextBox box22 = (TextBox)Gridview1.Rows[rowIndex2].Cells[3].FindControl("txtquantity");
                                            TextBox box23 = (TextBox)Gridview1.Rows[rowIndex2].Cells[4].FindControl("txtexpiredate");
                                            //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                                            DropDownList box24 = (DropDownList)Gridview1.Rows[rowIndex2].Cells[5].FindControl("ddl_Batch");
                                            TextBox box25 = (TextBox)Gridview1.Rows[rowIndex2].Cells[5].FindControl("txtStockinhand");
                                            TextBox box26 = (TextBox)Gridview1.Rows[rowIndex2].Cells[6].FindControl("txtrate");
                                            TextBox box27 = (TextBox)Gridview1.Rows[rowIndex2].Cells[7].FindControl("txttax");
                                            TextBox box28 = (TextBox)Gridview1.Rows[rowIndex2].Cells[8].FindControl("txtquantity");
                                            TextBox box29 = (TextBox)Gridview1.Rows[rowIndex2].Cells[9].FindControl("txttaxrate");
                                            TextBox box30 = (TextBox)Gridview1.Rows[rowIndex2].Cells[11].FindControl("txtdiscount");
                                            TextBox box31 = (TextBox)Gridview1.Rows[rowIndex2].Cells[10].FindControl("txtproamount");
                                            TextBox box32 = (TextBox)Gridview1.Rows[rowIndex2].Cells[11].FindControl("txttaxamount");
                                            TextBox box33 = (TextBox)Gridview1.Rows[rowIndex2].Cells[11].FindControl("txtpurchamount");
                                            //Clscrcust.Creditcustomer("INSERT_CUSTOMERACCOUNT", txtcustomercode.Text, txtcustname.Text, txtbillnor.Text, txtamount.Text, txtdate.Text, Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                            //ClsBLGP1.Customeraccno("INSERT_CUSTOMERACCOUNT", '0' txttransno.Text, txtinvoicenor.Text, txtdate.Text, "CR", "Y", txtcustomercode.Text, txtdate.Text, "D", "0", txtamount.Text, "0", "0", txtdate.Text, "Y", txtamount.Text, "DR", '0', Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                            double mrp10 = Convert.ToDouble(box26.Text);
                                            double tax10 = Convert.ToDouble(box27.Text);
                                            double ratetax10 = Convert.ToDouble((mrp10 * tax10) / (100 + tax10));
                                            double selprice20 = Convert.ToDouble(mrp10 - ratetax10);
                                            double rselprice20 = Math.Round(selprice20, 2);
                                            string selprice21 = Convert.ToString(rselprice20);
                                            string fnselprice22 = Convert.ToString(selprice21);
                                            string code = txtcustomercode.Text;
                                            cal();
                                           // lblbillnor.Text = invoiceno;

                                           

                                            SqlConnection inv10 = new SqlConnection(strconn1);
                                            SqlCommand cmdd10 = new SqlCommand(" Select Invoiceno from tblProductinward where Productcode = '" + box20.Text + "' AND ProductName = '" + box21.Text + "' AND Batchid = '" + box24.Text + "'", inv10);
                                            SqlDataAdapter daa10 = new SqlDataAdapter(cmdd10);
                                            DataSet dss10 = new DataSet();
                                            daa10.Fill(dss10);

                                            string invno10 = dss10.Tables[0].Rows[0]["Invoiceno"].ToString();

                                            DataSet dschm10 = clsgd.GetcondDataSet9("*", "tblDoctor", "D_name", doctorname);
                                            if (dschm10.Tables[0].Rows.Count > 0)
                                            {

                                                string D_code = dschm10.Tables[0].Rows[0]["D_code"].ToString();
                                                DateTime boxdate = Convert.ToDateTime(box23.Text);
                                                string boxdate1 = boxdate.ToString("yyyy-MM-dd");

                                                DataSet dschm20 = clsgd.GetcondDataSet2("*", "tblProductinward", "Productcode", box20.Text, "Batchid", box24.Text);

                                                string suppliercode = dschm20.Tables[0].Rows[0]["SuppplierCode"].ToString();

                                                if (suppliercode == "0000")
                                                {
                                                    string suppliercode1 = " ";
                                                    string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                    if (manufacturecode == "0")
                                                    {
                                                        string manufacturecode1 = " ";
                                                        // string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                        Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box20.Text, box21.Text, boxdate1, box24.Text, box25.Text, box26.Text, box27.Text, box28.Text, box29.Text, box30.Text, box31.Text, box31.Text, fnselprice22, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, box33.Text, suppliercode1, manufacturecode1, "Y", invno10, "Y", "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                                    }
                                                    else
                                                    {
                                                        string manufacturecode1 = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                        // string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                        Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box20.Text, box21.Text, boxdate1, box24.Text, box25.Text, box26.Text, box27.Text, box28.Text, box29.Text, box30.Text, box31.Text, box31.Text, fnselprice22, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, box33.Text, suppliercode1, manufacturecode1, "Y", invno10, "Y", "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                                    }
                                                }
                                                else
                                                {
                                                    string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();

                                                    if (manufacturecode == "0")
                                                    {
                                                        string manufacturecode1 = " ";
                                                        DataSet dschm30 = clsgd.GetcondDataSet9("*", "tblsuppliermaster", "SupplierCode", suppliercode);

                                                        string suppliename = dschm30.Tables[0].Rows[0]["SupplierName"].ToString();

                                                        Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box20.Text, box21.Text, boxdate1, box24.Text, box25.Text, box26.Text, box27.Text, box28.Text, box29.Text, box30.Text, box31.Text, box31.Text, fnselprice22, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, box33.Text, suppliename, manufacturecode1, "Y", invno10, "Y", "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                                    }
                                                    else
                                                    {
                                                        string manufacturecode1 = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                        DataSet dschm30 = clsgd.GetcondDataSet9("*", "tblsuppliermaster", "SupplierCode", suppliercode);

                                                        string suppliename = dschm30.Tables[0].Rows[0]["SupplierName"].ToString();

                                                        DataSet dschm40 = clsgd.GetcondDataSet9("*", "tblmanufacture", "ManufactureCode", manufacturecode);
                                                        string manufacturename = dschm40.Tables[0].Rows[0]["ManufactureName"].ToString();

                                                        Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box20.Text, box21.Text, boxdate1, box24.Text, box25.Text, box26.Text, box27.Text, box28.Text, box29.Text, box30.Text, box31.Text, box31.Text, fnselprice22, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, box33.Text, suppliename, manufacturename, "Y", invno10, "Y", "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                                    }
                                                }


                                                // Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, txtdoctorname.Text, txtpatientname.Text, txtbillnor.Text, ddpaymenttype.SelectedItem.Text, txtdate.Text, box0.Text, box1.Text, box3.Text, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, box11.Text, fnselprice, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, "Y", "Y", "Y", "Y", "Y", "Y", "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                                // ClsBLGP1.Customeraccno("INSERT_CUSTOMERACCOUNT", "1", txttransno.Text, txtinvoicenor.Text, txtdate.Text, "CR", "Y", txtcustomercode.Text, txtdate.Text, "D", "0", txtamount.Text, "0", "0", txtdate.Text, "Y", "0", "D", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                            }
                                            else
                                            {
                                                string D_code = "0";
                                                DateTime boxdate = Convert.ToDateTime(box23.Text);
                                                string boxdate1 = boxdate.ToString("yyyy-MM-dd");
                                                DataSet dschm20 = clsgd.GetcondDataSet2("*", "tblProductinward", "Productcode", box20.Text, "Batchid", box24.Text);

                                                string suppliercode = dschm20.Tables[0].Rows[0]["SuppplierCode"].ToString();
                                                if (suppliercode == "0000")
                                                {
                                                    string suppliercode1 = "No Supplier";
                                                    //string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();

                                                    string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                    if (manufacturecode == "0")
                                                    {
                                                        string manufacturecode1 = " ";
                                                        Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box20.Text, box21.Text, boxdate1, box24.Text, box25.Text, box26.Text, box27.Text, box28.Text, box29.Text, box30.Text, box31.Text, box31.Text, fnselprice22, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, box33.Text, suppliercode1, manufacturecode1, "Y", invno10, "Y", "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                                    }
                                                    else
                                                    {
                                                        string manufacturecode1 = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                        Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box20.Text, box21.Text, boxdate1, box24.Text, box25.Text, box26.Text, box27.Text, box28.Text, box29.Text, box30.Text, box31.Text, box31.Text, fnselprice22, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, box33.Text, suppliercode1, manufacturecode1, "Y", invno10, "Y", "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                                    }


                                                }

                                                else
                                                {
                                                    string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                    if (manufacturecode == "0")
                                                    {

                                                        string manufacturecode1 = " ";
                                                        DataSet dschm30 = clsgd.GetcondDataSet9("*", "tblsuppliermaster", "SupplierCode", suppliercode);

                                                        string suppliename = dschm30.Tables[0].Rows[0]["SupplierName"].ToString();

                                                        Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box20.Text, box21.Text, boxdate1, box24.Text, box25.Text, box26.Text, box27.Text, box28.Text, box29.Text, box30.Text, box31.Text, box31.Text, fnselprice22, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, box33.Text, suppliename, manufacturecode1, "Y", invno10, "Y", "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                                    }
                                                    else
                                                    {
                                                        string manufacturecode1 = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                        DataSet dschm30 = clsgd.GetcondDataSet9("*", "tblsuppliermaster", "SupplierCode", suppliercode);

                                                        string suppliename = dschm30.Tables[0].Rows[0]["SupplierName"].ToString();
                                                          DataSet dschm40 = clsgd.GetcondDataSet9("*", "tblmanufacture", "ManufactureCode", manufacturecode1);
                                                        string manufacturename = dschm40.Tables[0].Rows[0]["ManufactureName"].ToString();

                                                        Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box20.Text, box21.Text, boxdate1, box24.Text, box25.Text, box26.Text, box27.Text, box28.Text, box29.Text, box30.Text, box31.Text, box31.Text, fnselprice22, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, box33.Text, suppliename, manufacturename, "Y", invno10, "Y", "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                                    }

                                                }
                                            }



                                            SqlConnection con58 = new SqlConnection(strconn1);
                                            SqlCommand cmd58 = new SqlCommand("select Stockinhand  from tbltempprodsale where  Productcode = '" + box20.Text + "' AND  Batchid = '" + box24.Text + "'", con58);
                                            SqlDataAdapter da58 = new SqlDataAdapter(cmd58);
                                            DataSet ds58 = new DataSet();

                                            da58.Fill(ds58);

                                            double Quantity = Convert.ToDouble(ds58.Tables[0].Rows[0]["Stockinhand"].ToString());

                                            //int sthand = Convert.ToInt32(box5.Text);

                                            SqlConnection con59 = new SqlConnection(strconn1);
                                            SqlCommand cmd59 = new SqlCommand("select  Stockinhand  from tblProductinward where  Productcode = '" + box20.Text + "' AND ProductName = '" + box21.Text + "' AND Batchid = '" + box24.Text + "'", con59);
                                            SqlDataAdapter da59 = new SqlDataAdapter(cmd59);
                                            DataSet ds59 = new DataSet();

                                            da59.Fill(ds59);

                                            // int sthand = Convert.ToInt32(ds59.Tables[0].Rows[0]["Stockinhand"].ToString());

                                            string sthand10 = ds59.Tables[0].Rows[0]["Stockinhand"].ToString();

                                            int sthand = Convert.ToInt32(sthand10);

                                            //if (Quantity > sthand)
                                            //{
                                            //    Master.ShowModal("Stock in not available for this product. !!!", "txtproductcode", 1);
                                            //    return;

                                            //}



                                            string sthand15 = Convert.ToString(sthand - Quantity);

                                            int sthand1 = Convert.ToInt32(sthand15);



                                            //    SqlConnection conn25 = new SqlConnection(strconn1);
                                            //    conn25.Open();
                                            //    SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "' ", conn25);
                                            //    cmd25.ExecuteNonQuery();



                                            //rowIndex1++;



                                            DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblProductMaster", "Productcode", box20.Text);
                                            string flag2 = dsgroup10.Tables[0].Rows[0]["Pharmflag"].ToString();
                                            if (flag2 == "Y")
                                            {



                                                SqlConnection conn25 = new SqlConnection(strconn1);
                                                conn25.Open();
                                                SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box20.Text + " and ProductName='" + box21.Text + "' and Batchid ='" + box24.Text + "'", conn25);
                                                cmd25.ExecuteNonQuery();
                                                rowIndex2++;
                                            }
                                            else
                                            {
                                                SqlConnection con60 = new SqlConnection(strconn1);
                                                SqlCommand cmd60 = new SqlCommand("select  * from tblProductinward where  Productcode = " + box20.Text + " order by Invoicedate", con60);
                                                SqlDataAdapter da60 = new SqlDataAdapter(cmd60);
                                                DataSet ds60 = new DataSet();

                                                da60.Fill(ds60);
                                                for (int k = 0; k < ds60.Tables[0].Rows.Count; k++)
                                                {
                                                    if (Quantity != 0)
                                                    {
                                                        sthand15 = ds60.Tables[0].Rows[k]["Stockinhand"].ToString();
                                                        double sthand20 = Convert.ToDouble(sthand15);
                                                        SqlConnection conn255 = new SqlConnection(strconn1);
                                                        conn255.Open();
                                                        if (Quantity > sthand20)
                                                        {
                                                            Quantity = Quantity - sthand20;
                                                            sthand20 = 0;
                                                            SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand20 + "' WHERE Productcode= " + box20.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                            cmd255.ExecuteNonQuery();
                                                            rowIndex2++;
                                                        }
                                                        else
                                                        {
                                                            sthand20 = sthand20 - Quantity;
                                                            Quantity = 0;
                                                            SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand20 + "' WHERE Productcode= " + box20.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                            cmd255.ExecuteNonQuery();
                                                            rowIndex2++;
                                                        }
                                                    }
                                                }




                                            }

                                        }



                                    }
                                }

                            }

                            ////*****************Bharat*****************************************
                           /* SqlConnection inv = new SqlConnection(strconn1);
                            SqlCommand cmdd = new SqlCommand(" Select Invoiceno from tblProductinward where Productcode = '" + box0.Text + "' AND ProductName = '" + box1.Text + "' AND Batchid = '" + box4.Text + "'", inv);
                            SqlDataAdapter daa = new SqlDataAdapter(cmdd);
                            DataSet dss = new DataSet();
                            daa.Fill(dss);

                            string invno = dss.Tables[0].Rows[0]["Invoiceno"].ToString();

                            SqlConnection conn251 = new SqlConnection(strconn1);
                            conn251.Open();
                            SqlCommand cmd251 = new SqlCommand("UPDATE tblProductsale SET  Sale_falg6='" + invno + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchno ='" + box4.Text + "'", conn251);
                            cmd251.ExecuteNonQuery();
                            rowIndex++;*/


                        



                        if (ddpaymenttype.SelectedItem.Text == "CUSTOMER")
                        {
                            if (txtcustomercode.Text == "" || txtcustname.Text == "")
                            {
                                Master.ShowModal("Enter Customer Code or Customer Name. !!!", "txttransno", 1);
                                return;
                            }

                            if (txtcustname.Text == "")
                            {
                                Master.ShowModal("Enter Customer Name. !!!", " txtcramount", 1);
                                return;
                            }

                            SqlConnection con = new SqlConnection(strconn1);
                            con.Open();
                            SqlCommand cmd = new SqlCommand("select Credit_amount as Credit_amount,Credit_limit as Credit_limit, Credit_used as Credit_used from tblCustomer where CA_code='" + txtcustomercode.Text + "'", con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            double ca=0;
                            double cl=0;
                            double cu=0;
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0].IsNull("Credit_amount"))
                                {
                                    ca = 0;
                                }
                                else
                                {
                                    ca = Convert.ToDouble(ds.Tables[0].Rows[0]["Credit_amount"].ToString());
                                }
                                if (ds.Tables[0].Rows[0].IsNull("Credit_limit"))
                                {
                                    cl = 0;
                                }
                                else
                                {
                                    cl = Convert.ToDouble(ds.Tables[0].Rows[0]["Credit_limit"].ToString());
                                }

                                if (ds.Tables[0].Rows[0].IsNull("Credit_used"))
                                {
                                    cu = 0;
                                }
                                else
                                {
                                    cu = Convert.ToDouble(ds.Tables[0].Rows[0]["Credit_used"].ToString());
                                }
                            }
                            double credlimit = Convert.ToDouble(txtbal.Text);
                            double baltt = Convert.ToDouble(txtbal.Text) + cl;
                            double famount = Convert.ToDouble(txttotalamount.Text);
                            double crdused=Math.Round(Convert.ToDouble(txttotalamount.Text)-ca);


                            if (baltt < famount)
                            {
                                Master.ShowModal("Product amount acess the balance amount. !!!", " txtcramount", 1);
                                return;
                            }
                            else
                            {
                                if (credlimit < famount)
                                {
                                SqlConnection con1 = new SqlConnection(strconn1);
                                con1.Open();
                                SqlCommand cmd1 = new SqlCommand("UPDATE tblCustomer SET  Credit_used='" + crdused + "',Credit_amount='0' WHERE  CA_code='" + txtcustomercode.Text + "'", con1);
                                cmd1.ExecuteNonQuery();

                                }
                            }
                            maxno();
                            int rowIndex1 = 0;
                            //StringCollection sc = new StringCollection();
                            if (ViewState["CurrentTable"] != null)
                            {
                                DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable"];
                                DataRow drCurrentRow1 = null;
                                if (dtCurrentTable.Rows.Count > 0)
                                {
                                    for (int i = 1; i <= dtCurrentTable1.Rows.Count; i++)
                                    {
                                        TextBox box0 = (TextBox)Gridview1.Rows[rowIndex1].Cells[1].FindControl("txtproductcode");
                                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex1].Cells[2].FindControl("txtproductname");
                                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex1].Cells[3].FindControl("txtquantity");
                                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex1].Cells[4].FindControl("txtexpiredate");
                                        //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                                        DropDownList box4 = (DropDownList)Gridview1.Rows[rowIndex1].Cells[5].FindControl("ddl_Batch");
                                        TextBox box5 = (TextBox)Gridview1.Rows[rowIndex1].Cells[5].FindControl("txtStockinhand");
                                        TextBox box6 = (TextBox)Gridview1.Rows[rowIndex1].Cells[6].FindControl("txtrate");
                                        TextBox box7 = (TextBox)Gridview1.Rows[rowIndex1].Cells[7].FindControl("txttax");
                                        TextBox box8 = (TextBox)Gridview1.Rows[rowIndex1].Cells[8].FindControl("txtquantity");
                                        TextBox box9 = (TextBox)Gridview1.Rows[rowIndex1].Cells[9].FindControl("txttaxrate");
                                        TextBox box11 = (TextBox)Gridview1.Rows[rowIndex1].Cells[11].FindControl("txtdiscount");
                                        TextBox box10 = (TextBox)Gridview1.Rows[rowIndex1].Cells[10].FindControl("txtproamount");
                                        TextBox box12 = (TextBox)Gridview1.Rows[rowIndex1].Cells[11].FindControl("txttaxamount");
                                        TextBox box13 = (TextBox)Gridview1.Rows[rowIndex1].Cells[11].FindControl("txtpurchamount");
                                        //Clscrcust.Creditcustomer("INSERT_CUSTOMERACCOUNT", txtcustomercode.Text, txtcustname.Text, txtbillnor.Text, txtamount.Text, txtdate.Text, Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                        //ClsBLGP1.Customeraccno("INSERT_CUSTOMERACCOUNT", '0' txttransno.Text, txtinvoicenor.Text, txtdate.Text, "CR", "Y", txtcustomercode.Text, txtdate.Text, "D", "0", txtamount.Text, "0", "0", txtdate.Text, "Y", txtamount.Text, "DR", '0', Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                        double mrp = Convert.ToDouble(box6.Text);
                                        double tax = Convert.ToDouble(box7.Text);
                                        double ratetax = Convert.ToDouble((mrp * tax) / (100 + tax));
                                        double selprice = Convert.ToDouble(mrp - ratetax);
                                        double rselprice = Math.Round(selprice, 2);
                                        string selprice1 = Convert.ToString(rselprice);
                                        string fnselprice = Convert.ToString(selprice1);
                                        string code = txtcustomercode.Text;
                                        cal();
                                        lblbillnor.Text = invoiceno;

                                        SqlConnection inv = new SqlConnection(strconn1);
                                        SqlCommand cmdd = new SqlCommand(" Select Invoiceno from tblProductinward where Productcode = '" + box0.Text + "' AND ProductName = '" + box1.Text + "' AND Batchid = '" + box4.Text + "'", inv);
                                        SqlDataAdapter daa = new SqlDataAdapter(cmdd);
                                        DataSet dss = new DataSet();
                                        daa.Fill(dss);

                                        string invno = dss.Tables[0].Rows[0]["Invoiceno"].ToString();

                                        DataSet dschm10 = clsgd.GetcondDataSet9("*", "tblDoctor", "D_name", doctorname);
                                        if (dschm10.Tables[0].Rows.Count > 0)
                                        {

                                            string D_code = dschm10.Tables[0].Rows[0]["D_code"].ToString();
                                            DateTime boxdate = Convert.ToDateTime(box3.Text);
                                            string boxdate1 = boxdate.ToString("yyyy-MM-dd");

                                            DataSet dschm20 = clsgd.GetcondDataSet2("*", "tblProductinward", "Productcode", box0.Text, "Batchid", box4.Text);

                                            string suppliercode = dschm20.Tables[0].Rows[0]["SuppplierCode"].ToString();

                                            if (suppliercode == "0000")
                                            {
                                                string suppliercode1 = " ";
                                                string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                if (manufacturecode == "0")
                                                {
                                                    string manufacturecode1 = " ";
                                                    // string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                    Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, box11.Text, fnselprice, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, box13.Text, suppliercode1, manufacturecode1, "Y", invno, "Y", "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                                }
                                                else
                                                {
                                                    string manufacturecode1 = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                    // string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                    Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, box11.Text, fnselprice, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, box13.Text, suppliercode1, manufacturecode1, "Y", invno, "Y", "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                                }
                                            }
                                            else
                                            {
                                                string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();

                                                if (manufacturecode == "0")
                                                {
                                                    string manufacturecode1 = " ";
                                                    DataSet dschm30 = clsgd.GetcondDataSet9("*", "tblsuppliermaster", "SupplierCode", suppliercode);

                                                    string suppliename = dschm30.Tables[0].Rows[0]["SupplierName"].ToString();

                                                    Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, box11.Text, fnselprice, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, box13.Text, suppliename, manufacturecode1, "Y", invno, "Y", "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                                }
                                                else
                                                {
                                                    string manufacturecode1 = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                    DataSet dschm30 = clsgd.GetcondDataSet9("*", "tblsuppliermaster", "SupplierCode", suppliercode);

                                                    string suppliename = dschm30.Tables[0].Rows[0]["SupplierName"].ToString();

                                                    DataSet dschm40 = clsgd.GetcondDataSet9("*", "tblmanufacture", "ManufactureCode", manufacturecode);
                                                    string manufacturename = dschm40.Tables[0].Rows[0]["ManufactureName"].ToString();

                                                    Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, box11.Text, fnselprice, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, box13.Text, suppliename, manufacturename, "Y", invno, "Y", "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                                }
                                            }


                                            // Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, txtdoctorname.Text, txtpatientname.Text, txtbillnor.Text, ddpaymenttype.SelectedItem.Text, txtdate.Text, box0.Text, box1.Text, box3.Text, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, box11.Text, fnselprice, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, "Y", "Y", "Y", "Y", "Y", "Y", "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                            // ClsBLGP1.Customeraccno("INSERT_CUSTOMERACCOUNT", "1", txttransno.Text, txtinvoicenor.Text, txtdate.Text, "CR", "Y", txtcustomercode.Text, txtdate.Text, "D", "0", txtamount.Text, "0", "0", txtdate.Text, "Y", "0", "D", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                        }
                                        else
                                        {
                                            string D_code = "0";
                                            DateTime boxdate = Convert.ToDateTime(box3.Text);
                                            string boxdate1 = boxdate.ToString("yyyy-MM-dd");
                                            DataSet dschm20 = clsgd.GetcondDataSet2("*", "tblProductinward", "Productcode", box0.Text, "Batchid", box4.Text);

                                            string suppliercode = dschm20.Tables[0].Rows[0]["SuppplierCode"].ToString();
                                            if (suppliercode == "0000")
                                            {
                                                string suppliercode1 = "No Supplier";
                                                //string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();

                                                string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                if (manufacturecode == "0")
                                                {
                                                    string manufacturecode1 = " ";
                                                    Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, box11.Text, fnselprice, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, box13.Text, suppliercode1, manufacturecode1, "Y", invno, "Y", "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                                }
                                                else
                                                {
                                                    string manufacturecode1 = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                    Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, box11.Text, fnselprice, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, box13.Text, suppliercode1, manufacturecode1, "Y", invno, "Y", "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                                }


                                            }

                                            else
                                            {
                                                string manufacturecode = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                if (manufacturecode == "0")
                                                {

                                                    string manufacturecode1 = " ";
                                                    DataSet dschm30 = clsgd.GetcondDataSet9("*", "tblsuppliermaster", "SupplierCode", suppliercode);

                                                    string suppliename = dschm30.Tables[0].Rows[0]["SupplierName"].ToString();

                                                    Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, box11.Text, fnselprice, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, box13.Text, suppliename, manufacturecode1, "Y", invno, "Y", "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                                }
                                                else
                                                {
                                                    string manufacturecode1 = dschm20.Tables[0].Rows[0]["ManufactureCode"].ToString();
                                                    DataSet dschm30 = clsgd.GetcondDataSet9("*", "tblsuppliermaster", "SupplierCode", suppliercode);

                                                    string suppliename = dschm30.Tables[0].Rows[0]["SupplierName"].ToString();
                                                    DataSet dschm40 = clsgd.GetcondDataSet9("*", "tblmanufacture", "ManufactureCode", manufacturecode1);
                                                    string manufacturename = dschm40.Tables[0].Rows[0]["ManufactureName"].ToString();

                                                    Clsprdinw.Productsale("INSERT_PRODUCTSALE", transno, txtdoctorname.Text, D_code, txtpatientname.Text, invoiceno, ddpaymenttype.SelectedItem.Text, indate11, box0.Text, box1.Text, boxdate1, box4.Text, box5.Text, box6.Text, box7.Text, box8.Text, box9.Text, box10.Text, box11.Text, box11.Text, fnselprice, pramount, discount, sumtaxrate, totamount, txtcustomercode.Text, box13.Text, suppliename, manufacturename, "Y", invno, "Y", "Y", "Y", "Y", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                                }

                                            }
                                        }
                                       


                                            SqlConnection con58 = new SqlConnection(strconn1);
                                            SqlCommand cmd58 = new SqlCommand("select Stockinhand  from tbltempprodsale where  Productcode = '" + box0.Text + "' AND  Batchid = '" + box4.Text + "'", con58);
                                            SqlDataAdapter da58 = new SqlDataAdapter(cmd58);
                                            DataSet ds58 = new DataSet();

                                            da58.Fill(ds58);

                                            double Quantity = Convert.ToDouble(ds58.Tables[0].Rows[0]["Stockinhand"].ToString());

                                            //int sthand = Convert.ToInt32(box5.Text);

                                            SqlConnection con59 = new SqlConnection(strconn1);
                                            SqlCommand cmd59 = new SqlCommand("select  Stockinhand  from tblProductinward where  Productcode = '" + box0.Text + "' AND ProductName = '" + box1.Text + "' AND Batchid = '" + box4.Text + "'", con59);
                                            SqlDataAdapter da59 = new SqlDataAdapter(cmd59);
                                            DataSet ds59 = new DataSet();

                                            da59.Fill(ds59);

                                           // int sthand = Convert.ToInt32(ds59.Tables[0].Rows[0]["Stockinhand"].ToString());

                                            string sthand10 = ds59.Tables[0].Rows[0]["Stockinhand"].ToString();

                                            int sthand = Convert.ToInt32(sthand10);

                                            //if (Quantity > sthand)
                                            //{
                                            //    Master.ShowModal("Stock in not available for this product. !!!", "txtproductcode", 1);
                                            //    return;

                                            //}



                                            string sthand15 = Convert.ToString(sthand - Quantity);

                                            int sthand1 = Convert.ToInt32(sthand15);



                                        //    SqlConnection conn25 = new SqlConnection(strconn1);
                                        //    conn25.Open();
                                        //    SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "' ", conn25);
                                        //    cmd25.ExecuteNonQuery();
                                           
                                        

                                        //rowIndex1++;



                                            DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblProductMaster", "Productcode", box0.Text);
                                            string flag2 = dsgroup10.Tables[0].Rows[0]["Pharmflag"].ToString();
                                            if (flag2 == "Y")
                                            {



                                                SqlConnection conn25 = new SqlConnection(strconn1);
                                                conn25.Open();
                                                SqlCommand cmd25 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand1 + "' WHERE Productcode= " + box0.Text + " and ProductName='" + box1.Text + "' and Batchid ='" + box4.Text + "'", conn25);
                                                cmd25.ExecuteNonQuery();
                                                rowIndex1++;
                                            }
                                            else
                                            {
                                                SqlConnection con60 = new SqlConnection(strconn1);
                                                SqlCommand cmd60 = new SqlCommand("select  * from tblProductinward where  Productcode = " + box0.Text + " order by Invoicedate", con60);
                                                SqlDataAdapter da60 = new SqlDataAdapter(cmd60);
                                                DataSet ds60 = new DataSet();

                                                da60.Fill(ds60);
                                                for (int k = 0; k < ds60.Tables[0].Rows.Count; k++)
                                                {
                                                    if (Quantity != 0)
                                                    {
                                                        sthand15 = ds60.Tables[0].Rows[k]["Stockinhand"].ToString();
                                                        double sthand20 = Convert.ToDouble(sthand15);
                                                        SqlConnection conn255 = new SqlConnection(strconn1);
                                                        conn255.Open();
                                                        if (Quantity > sthand20)
                                                        {
                                                            Quantity = Quantity - sthand20;
                                                            sthand20 = 0;
                                                            SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand20 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                            cmd255.ExecuteNonQuery();
                                                            rowIndex1++;
                                                        }
                                                        else
                                                        {
                                                            sthand20 = sthand20 - Quantity;
                                                            Quantity = 0;
                                                            SqlCommand cmd255 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand='" + sthand20 + "' WHERE Productcode= " + box0.Text + " and Invoiceno='" + ds60.Tables[0].Rows[k]["Invoiceno"].ToString() + "'", conn255);
                                                            cmd255.ExecuteNonQuery();
                                                            rowIndex1++;
                                                        }
                                                    }
                                                }




                                            }

                                    }

                                  

                                }
                            }

                            if (ddpaymenttype.SelectedItem.Text == "CUSTOMER")
                            {
                                ClsBLGP1.Customeraccno("INSERT_CUSTOMERACCOUNT", "1", transno, txtinvoicenor.Text, txtdate.Text, "D", "N", txtcustomercode.Text, txtdate.Text, "AD", "0", txtamount.Text, "0", "0", txtdate.Text, "Y", "0", "D", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                            }
                            else 
                            {
                                ClsBLGP1.Customeraccno("INSERT_CUSTOMERACCOUNT", "1", transno, txtinvoicenor.Text, txtdate.Text, "D", "Y", txtcustomercode.Text, txtdate.Text, "AD", "0", txtamount.Text, "0", "0", txtdate.Text, "Y", "0", "D", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                            }

                            SqlConnection con7 = new SqlConnection(strconn1);
                            SqlCommand cmd21 = new SqlCommand("select * from tblCustomer where CA_code='" + txtcustomercode.Text + "'", con7);
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd21);
                            DataSet ds1 = new DataSet();

                            da1.Fill(ds1);
                            string camount1 = ds1.Tables[0].Rows[0]["Credit_amount"].ToString();
                            double camount2 = Convert.ToDouble(camount1);
                            string cuse = ds1.Tables[0].Rows[0]["Credit_used"].ToString();

                            double cuse1 = Convert.ToDouble(cuse);
                            double totamount10 = Convert.ToDouble(totamount);
                            double amt10 = (totamount10 + cuse1);
                            if (credlimit < famount)
                            {
                            }
                            else
                            {

                                if (camount2 == 0)
                                {
                                    SqlConnection conn20 = new SqlConnection(strconn1);
                                    conn20.Open();
                                    SqlCommand cmd20 = new SqlCommand("UPDATE tblCustomer SET  Credit_used='" + amt10 + "' WHERE  CA_code='" + txtcustomercode.Text + "'", conn20);
                                    cmd20.ExecuteNonQuery();


                                }
                                else
                                {
                                    string camount = ds1.Tables[0].Rows[0]["Credit_amount"].ToString();
                                    double camont1 = Convert.ToDouble(camount);
                                    double ttamount = Convert.ToDouble(totamount);
                                    double ucamount = Convert.ToDouble(camont1 - ttamount);
                                    SqlConnection conn20 = new SqlConnection(strconn1);
                                    conn20.Open();
                                    SqlCommand cmd20 = new SqlCommand("UPDATE tblCustomer SET  Credit_amount='" + ucamount + "' WHERE  CA_code='" + txtcustomercode.Text + "'", conn20);
                                    cmd20.ExecuteNonQuery();


                                }
                            }


                        }


                        //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Records Successfuly Saved!');", true);
                        //Response.Redirect("GridViewWithTextBoxes.aspx");
                        lblsuccess.Visible = true;


                        // lblsuccess.Text = "inserted successfully";
                        cal();

                        SqlConnection con12 = new SqlConnection(strconn1);
                        con12.Open();
                        SqlCommand cmd12 = new SqlCommand("SELECT DISTINCT Taxamount FROM tblProductsale", con12);
                        SqlDataReader reader = cmd12.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //int taxamount=reader["Taxamount"];
                                string tax10 = (string)reader["Taxamount"];
                                SqlConnection con14 = new SqlConnection(strconn1);
                                con14.Open();
                                SqlCommand cmd14 = new SqlCommand("SELECT SUM(taxrate) AS taxrate,sum(taxable1) as taxable1 FROM tblProductsale where Taxamount='" + tax10 + "'", con14);
                                SqlDataReader reader10 = cmd14.ExecuteReader();
                                if (reader10.HasRows)
                                {
                                    while (reader10.Read())
                                    {
                                        double Taxamount = Convert.ToDouble(reader10["taxrate"]);
                                        double taxable = Convert.ToDouble(reader10["taxable1"]);

                                        string taxable1 = taxable.ToString();

                                        string taxamount1 = Taxamount.ToString();
                                        string staxamount = tax10.ToString();

                                        // string invoiceno =txtinvoicenor.Text;
                                        string invoicedate = txtdate.Text;





                                        Clsprdinw.SALESTAX("INSERT_SALETAX", invoiceno, invoicedate, tax10, taxamount1, taxable1, Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                        SqlConnection con55 = new SqlConnection(strconn1);
                                        con55.Open();
                                        SqlCommand cmd55 = new SqlCommand("delete FROM tbltempprodsale", con55);
                                        cmd55.ExecuteNonQuery();

                                    }
                                }
                            }
                        }



                    }
                  
                }

                if (ddpaymenttype.SelectedItem.Text == "CASH")
                {
                    string proamt = txttotalamount.Text;
                    //string Chequeno1 = ClsBLGD.base64Encode(Chequeno5);

                    string incno1 = clsgd.base64Encode(txtinvoicenor.Text);

                    ClsBLGP3.Transaction("INSERT_TRANSACTION", transno, txtdate.Text, "0000", "0000", "9997", "N", "0000", incno1, "0000.00", "0000.00", proamt, "0000.00", txttax.Text, "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                }
                else if (ddpaymenttype.SelectedItem.Text == "CARD")
                {
                    string proamt = txttotalamount.Text;
                    string incno1 = clsgd.base64Encode(lblbillnor.Text);
                    string transcard = txttransno.Text;
                    if (ddlpaytype.SelectedItem.Text == "Credit Card")
                    {
                        ClsBLGP3.Transaction("INSERT_TRANSACTION", transno, txtdate.Text, "0000", "0000", "9985", "N", "0000", lblbillnor.Text, proamt, "0000.00", "0000.00", "0000.00", txttax.Text, "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                        ClsBLGP3.Transaction("INSERT_TRANSACTION", transcard, txtdate.Text, "0000", "0000", "9997", "N", "0000", lblbillnor.Text, "0000.00", proamt, "0000.00", "0000.00", txttax.Text, "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                        ClsBLGP3.Transaction("INSERT_TRANSACTION", transcard, txtdate.Text, "0000", "0000", "9982", "N", "0000", lblbillnor.Text, "0000.00", lblcardamount.Text, "0000.00", "0000.00", txttax.Text, "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                    }

                    if (ddlpaytype.SelectedItem.Text == "Debit card")
                    {
                        ClsBLGP3.Transaction("INSERT_TRANSACTION", transno, txtdate.Text, "0000", "0000", "9984", "N", "0000", lblbillnor.Text, proamt, "0000.00", "0000.00", "0000.00", txttax.Text, "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                        ClsBLGP3.Transaction("INSERT_TRANSACTION", transcard, txtdate.Text, "0000", "0000", "9997", "N", "0000", lblbillnor.Text, "0000.00", proamt, "0000.00", "0000.00", txttax.Text, "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                        ClsBLGP3.Transaction("INSERT_TRANSACTION", transcard, txtdate.Text, "0000", "0000", "9982", "N", "0000", lblbillnor.Text, "0000.00", lblcardamount.Text, "0000.00", "0000.00", txttax.Text, "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                    }



                }
                else if (ddpaymenttype.SelectedItem.Text == "CUSTOMER")
                {
                    string proamt = txttotalamount.Text;
                   
                    string incno1 = clsgd.base64Encode(lblbillnor.Text);
                    ClsBLGP3.Transaction("INSERT_TRANSACTION", transno, txtdate.Text, "0000", "0000", "9983", "N", "0000", incno1, proamt, "0000.00", "0000.00", "0000.00", txttax.Text, "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                    ClsBLGP3.Transaction("INSERT_TRANSACTION", transno, txtdate.Text, txtcustomercode.Text, "0000", "9994", "N", "0000", incno1, "0000.00", proamt, "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                   

                }

                txtdoctorname.Text = string.Empty;
                txtpatientname.Text = string.Empty;
                // txtinvoicenor.Text = string.Empty;
                txtpramount.Text = string.Empty;
                txtdiscount.Text = string.Empty;
                txttotalamount.Text = string.Empty;
               // txttransno.Text = string.Empty;

                txtcardno.Text = string.Empty;

                txtcramount.Text = string.Empty;
                lblvbillno.Text = string.Empty;
                txtcustomercode.Text = string.Empty;
                txtcustname.Text = string.Empty;
                 lblbillnor.Text = string.Empty;
                txtamount.Text = string.Empty;
                txttax.Text = string.Empty;
                txtbal.Text = string.Empty;
                txtstock.Text = string.Empty;
                txttransno.Text = string.Empty;

                ddlpaytype.ClearSelection();
                /* txtdoctorname.Enabled = false;
                txtpatientname.Enabled = false;
                // txtinvoicenor.Text = string.Empty;
                txtpramount.Enabled = false;
                txtdiscount.Enabled = false;
                txttotalamount.Enabled = false;
                txttransno.Enabled = false;
                txtcramount.Enabled = false;
                txtbillno.Enabled = false;
                txtcustomercode.Enabled = false;
                txtcustname.Enabled = false;
                 lblbillnor.Enabled = false;
                txtamount.Enabled = false;
                txttax.Enabled = false;
                txtbal.Enabled = false;
                txtstock.Enabled = false;*/
                

               
                ddpaymenttype.ClearSelection();
                if (ddpaymenttype.SelectedItem.Text == "CASH")
                {
                    Panel3.Visible = false;
                }
                SetInitialRow();

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

                // Bind();


                // ArrayList oALHospDetails = Hosp.HospitalReturns();

                // string dcname = txtdoctorname.Text;
                // string pname = txtpatientname.Text;
                // string pamount = txtpramount.Text;
                // string discount1 = txtdiscount.Text;
                // string tax1 = txttax.Text;
                // string ttamount = txttotalamount.Text;



                // // PDF Report generation
                //// Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                // Document document = new Document(new iTextSharp.text.Rectangle(500f, 400f), 0f, 0f, 0f, 0f);
                // PdfWriter.GetInstance(document, Response.OutputStream);
                // Document document1 = new Document();
                // Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);

                // MemoryStream memoryStream = new System.IO.MemoryStream();

                // PdfWriter.GetInstance(document, Response.OutputStream);
                // PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                // PdfWriterEvents1 writerEvent = new PdfWriterEvents1(oALHospDetails[4].ToString());
                //  writer.PageEvent = writerEvent;


                // DataTable dtPdfstock = new DataTable();
                // if (grprodsaledetails.HeaderRow != null)
                // {
                //     for (int i = 0; i < grprodsaledetails.HeaderRow.Cells.Count; i++)
                //     {
                //         dtPdfstock.Columns.Add(grprodsaledetails.HeaderRow.Cells[i].Text);
                //     }
                // }

                // //  add each of the data rows to the table

                // foreach (GridViewRow row in grprodsaledetails.Rows)
                // {
                //     DataRow datarow;
                //     datarow = dtPdfstock.NewRow();

                //     for (int i = 0; i < row.Cells.Count; i++)
                //     {
                //         datarow[i] = row.Cells[i].Text;
                //     }
                //     dtPdfstock.Rows.Add(datarow);
                // }
                // Session["dtPdfstock"] = dtPdfstock;


                // Phrase phrase = null;
                // PdfPCell cell = null;
                // PdfPTable tblstock = null;
                // PdfPTable table1 = null;
                // PdfPTable table2 = null;

                // PdfPTable tbldt = null;
                // dtPdfstock = (DataTable)Session["dtPdfstock"];
                // if (Session["dtPdfstock"] != null)
                // {
                //     table2 = new PdfPTable(dtPdfstock.Columns.Count);
                // }

                // PdfPTable tblNoteSign = null;
                // PdfPTable tblTotBillAmt = null;
                // PdfPTable tblinwords = null;
                // PdfPTable tblpay = null;
                // PdfPCell GridCell = null;
                // BaseColor color = null;


                // document.Open();

                // //Header Table




                // tblstock = new PdfPTable(1);
                // tblstock.TotalWidth = 490f;
                // tblstock.LockedWidth = true;
                // tblstock.SetWidths(new float[] { 1f });

                // tbldt = new PdfPTable(2);
                // tbldt.TotalWidth = 490f;
                // tbldt.LockedWidth = true;
                // tbldt.SetWidths(new float[] { 1.4f, 0.6f });

                // table1 = new PdfPTable(8);
                // table1.TotalWidth = 490f;
                // table1.LockedWidth = true;
                // table1.SetWidths(new float[] { 0.5f, 1.5f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f});



                // table2 = new PdfPTable(2);
                // table2.TotalWidth = 490f;
                // table2.LockedWidth = true;
                // table2.SetWidths(new float[] { 1.4f, 0.6f});



                // tblNoteSign = new PdfPTable(2);
                // tblNoteSign.TotalWidth = 490f;
                // tblNoteSign.LockedWidth = true;
                // tblNoteSign.SetWidths(new float[] { 0.8f, 0.4f });

                // tblTotBillAmt = new PdfPTable(1);
                // tblTotBillAmt.TotalWidth = 490f;
                // tblTotBillAmt.LockedWidth = true;
                // tblTotBillAmt.SetWidths(new float[] { 1f });

                // tblpay = new PdfPTable(1);
                // tblpay.TotalWidth = 490f;
                // tblpay.LockedWidth = true;
                // tblpay.SetWidths(new float[] { 1f });

                // tblinwords = new PdfPTable(1);
                // tblinwords.TotalWidth = 490f;
                // tblinwords.LockedWidth = true;
                // tblinwords.SetWidths(new float[] { 1f });





                // GridCell = new PdfPCell(new Phrase(new Chunk("SLNO", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                // GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                // table1.AddCell(GridCell);

                //// GridCell = new PdfPCell(new Phrase(new Chunk("Productcode", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                //// GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                // //table1.AddCell(GridCell);

                // GridCell = new PdfPCell(new Phrase(new Chunk("ProductName.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                // GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                // table1.AddCell(GridCell);

                // GridCell = new PdfPCell(new Phrase(new Chunk("Expiredate.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                // GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                // table1.AddCell(GridCell);

                // GridCell = new PdfPCell(new Phrase(new Chunk("Batchno.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                // GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                // table1.AddCell(GridCell);

                // GridCell = new PdfPCell(new Phrase(new Chunk("Rate.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                // GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                // table1.AddCell(GridCell);

                // GridCell = new PdfPCell(new Phrase(new Chunk("Taxamount.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                // GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                // table1.AddCell(GridCell);

                // GridCell = new PdfPCell(new Phrase(new Chunk("Quantity.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                // GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                // table1.AddCell(GridCell);

                // GridCell = new PdfPCell(new Phrase(new Chunk("Amount.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
                // GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
                // table1.AddCell(GridCell);
                // table1.SpacingAfter = 15f;


                // //******************************************************************************************************************************************************************

                // if (dtPdfstock != null)
                // {
                //     for (int i = 0; i < dtPdfstock.Rows.Count; i++)
                //     {


                //         for (int row1 = 0; row1 < dtPdfstock.Columns.Count; row1++)
                //         {

                //                 GridCell = new PdfPCell(new Phrase(new Chunk(dtPdfstock.Rows[i][row1].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                //                 GridCell.HorizontalAlignment = 0;
                //                 GridCell.PaddingBottom = 5f;
                //                 table1.AddCell(GridCell);

                //         }
                //     }
                // }



                // DateTime dtstrDate2 = DateTime.Now;

                // DataSet dslogin = clsgd.GetcondDataSet("*", "tblLogin", "username", Session["username"].ToString());
                //// DataSet dsbcode = Clsbllgeneral.GetcondDataSet("*", "emp_det", "emp_code", dslogin.Tables[0].Rows[0]["emp_code"].ToString());

                //// DataSet dsBranchDetails1 = Clsbllgeneral.GetcondDataSet("*", "branch_det", "branch_code", dsbcode.Tables[0].Rows[0]["branch_code"].ToString());

                // tblstock.AddCell(PhraseCell(new Phrase("Sale Report\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                // cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                // cell.Colspan = 2;
                // cell.PaddingBottom = 30f;
                // tblstock.AddCell(cell);



                // tbldt.AddCell(PhraseCell(new Phrase("Doctor Name :" + dcname, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                // tbldt.AddCell(PhraseCell(new Phrase("Patient Name :" + pname, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                // cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                // cell.Colspan = 2;
                // cell.PaddingBottom = 30f;
                // tbldt.AddCell(cell);
                // tbldt.SpacingAfter = 15f;

                ///* tbldt.AddCell(PhraseCell(new Phrase("Patient Name :" + pname, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                // cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_RIGHT);
                // cell.Colspan = 2;
                // cell.PaddingBottom = 28f;
                // tbldt.AddCell(cell);*/






                // phrase = new Phrase();
                // phrase.Add(new Chunk(oALHospDetails[0].ToString() + "\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)));
                // phrase.Add(new Chunk(oALHospDetails[1].ToString() + "\n" + oALHospDetails[2].ToString() + "\n" + oALHospDetails[3].ToString() + "\n\n", FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)));
                // cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
                // cell.HorizontalAlignment = 0;
                // table2.AddCell(cell);

                // phrase = new Phrase();
                // phrase.Add(new Chunk("Bill No. :" + txtinvoicenor.Text + "\n" + "Date :" + sqlFormattedDate + "\n", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)));
                // cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
                // cell.HorizontalAlignment = 0;
                // table2.AddCell(cell);



                //// tblNoteSign.AddCell(PhraseCell(new Phrase("\n\n" + "Printed By " + "\n" + "(" + dsbcode.Tables[0].Rows[0]["emp_name"].ToString() + ")" + "\n\n", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                //// cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                //// cell.PaddingBottom = 30f;
                //// tblNoteSign.AddCell(cell);



                // tblTotBillAmt.AddCell(PhraseCell(new Phrase("Product Amount:" + "Rs." + pamount + "\n", FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                // tblTotBillAmt.AddCell(PhraseCell(new Phrase("Discount Amount : " + "Rs." + discount1 + "\n", FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                //// tblTotBillAmt.AddCell(PhraseCell(new Phrase("Tax Amount : " + "Rs." + tax1 + "\n", FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                //// tblTotBillAmt.AddCell(PhraseCell(new Phrase("Total Final Amount:" + "Rs." + ttamount + "\n", FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                // cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_RIGHT);
                // cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                // cell.Border = 0;
                // cell.Colspan = 9;
                // cell.PaddingBottom = 30f;
                // tblTotBillAmt.AddCell(cell);


                //  string bill = "Amount Paid";
                //  tblpay.AddCell(PhraseCell(new Phrase("\n" + bill + " Rs. " + ttamount, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
                // cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_RIGHT);
                // cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                // cell.Border = 0;
                // cell.Colspan = 9;
                // cell.PaddingBottom = 200f;
                // tblpay.AddCell(cell);


                // double doubTotal = Convert.ToDouble(ttamount);
                // string strNumToEng = NumToEng.changeNumericToWords(doubTotal);

                // tblinwords.AddCell(PhraseCell(new Phrase("\n" + "Amount In Words :   " + strNumToEng + " Only.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                // cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
                // cell.Colspan = 2;
                // cell.PaddingBottom = 200f;
                // tblinwords.AddCell(cell);


                // tblNoteSign.AddCell(PhraseCell(new Phrase("\n\n" + "Printed By " + "\n" + "(" + dslogin.Tables[0].Rows[0]["UserName"].ToString() + ")" + "\n\n", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
                // cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                // cell.Colspan = 2;
                // cell.PaddingBottom = 30f;
                // tblNoteSign.AddCell(cell);

                // tblNoteSign.AddCell(PhraseCell(new Phrase("E & OE" + "\n\n", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
                // cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                // cell.Colspan = 2;
                // cell.PaddingBottom = 30f;
                // tblNoteSign.AddCell(cell);










                // // StringReader sr = new StringReader(sw.ToString());

                // // ****************Drawing Line Horizontally*********************
                // color = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#A9A9A9"));
                // //DrawLine(writer, 0f, document.Top - 360f, document.PageSize.Width - 25f, document.Top - 360f, color);

                // // ****************Drawing Line Vertically*********************
                // //DrawLine(writer, 30f, 80f, 30f, 660f, color);
                // //DrawLine(writer, 65f, 80f, 65f, 660f, color);
                // int cntdtPdfstock = 0; ;
                // if (dtPdfstock != null)
                // {
                //     cntdtPdfstock = dtPdfstock.Rows.Count;
                // } 

                // document.Add(tblstock);
                // document.Add(table2);
                // document.Add(tbldt);
                // document.Add(table1);
                // document.Add(tblTotBillAmt);
                // document.Add(tblpay);
                // document.Add(tblinwords);
                // document.Add(tblNoteSign);
                // grprodsaledetails.DataSource = null;
                // dtPdfstock.Rows.Clear();
                // document.Close();
                // //Response.Clear();

                // Response.ContentType = "application/pdf";
                // Response.AddHeader("Content-Disposition", "attachment; filename=Productsale.pdf");

                // byte[] bytes = memoryStream.ToArray();
                // memoryStream.Close();
                // Response.Clear();
                // //Response.Write(document);
                //// Clsbllgeneral.ClearInputs(Page.Controls);

                // Response.Buffer = true;
                // Response.Cache.SetCacheability(HttpCacheability.NoCache);
                // Response.BinaryWrite(bytes);
                // Response.End();
                // Response.Close();

            }



            else
            {

                //System.DateTime Dtnow = DateTime.Now;
                //string Sysdatetime = Dtnow.ToString("dd/MM/yyyy hh:mm:ss");
                //txtdate.Text = Sysdatetime;


                string indate = txtdate.Text;

                //string indate = Convert.ToString(txtdate.Text);

                int rowIndex = 0;
                //StringCollection sc = new StringCollection();
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                            TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");
                            TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtquantity");
                            TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                            //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                            TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtbatchno");
                            TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtrate");
                            TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtdiscount");
                            TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                            TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtproamount");
                            String strconn11 = Dbconn.conmenthod();
                            OleDbConnection con = new OleDbConnection(strconn11);
                            con.Open();
                            OleDbCommand cmd1 = new OleDbCommand("insert into tblProductsale (Doctorname,Doctorcode,Patientcode,Invoiceno,Typeoftrans,Trdate,Productcode,ProductName,Quantity,Expiredate,Batchno,Rate,D_Rate,D_Value,Taxamount,Pro_Amount,Total_Pro_Amount,Total_Discount,Total_Amount,Login_name,Sysdatetime,Mac_id,Sale_falg1,Sale_falg2,Sale_falg3,Sale_falg4,Sale_falg5,Sale_falg6,Sale_falg7,Sale_falg8,Sale_falg9,Sale_falg10) values('" + txtdoctorname.Text + "','" + txtdoctorname.Text + "','" + txtpatientname.Text + "','" + txtinvoicenor.Text + "','" + ddpaymenttype.SelectedItem.Text + "','" + txtdate.Text + "','" + box0.Text + "','" + box1.Text + "','" + box2.Text + "','" + box3.Text + "','" + box4.Text + "','" + box5.Text + "','" + box5.Text + "','" + box6.Text + "'," + box7.Text + ",'" + box8.Text + "','" + txtpramount.Text + "','" + txtdiscount.Text + "','" + txttotalamount.Text + "','" + Session["username"].ToString() + "','" + sqlFormattedDate + "','" + sMacAddress + "','Y','Y','Y','Y','Y','Y','Y','Y','Y','Y')", con);
                            cmd1.ExecuteNonQuery();
                            con.Close();

                            if (ddpaymenttype.SelectedItem.Text == "CARD")
                            {
                                if (txtcardno.Text == "")
                                {
                                    Master.ShowModal("Enter Trans No. !!!", "txttransno", 1);
                                    return;
                                }

                                if (txtcramount.Text == "")
                                {
                                    Master.ShowModal("Enter Amount. !!!", " txtcramount", 1);
                                    return;
                                }
                                //String strconn12 = Dbconn.conmenthod();
                                OleDbConnection con1 = new OleDbConnection(strconn11);
                                con1.Open();
                                OleDbCommand cmd2 = new OleDbCommand("insert into tblCardinfo (TrNo,Amount,Bankrec,Billno,Trdate,Login_name,Sysdatetime,Mac_id) values('" + txtcardno.Text + "','" + txtcramount.Text + "','N','" + lblvbillno.Text + "','" + txtdate.Text + "','" + Session["username"].ToString() + "','" + sqlFormattedDate + "','" + sMacAddress + "')", con1);
                                cmd2.ExecuteNonQuery();
                                con.Close();

                            }


                            if (ddpaymenttype.SelectedItem.Text == "CUSTOMER")
                            {
                                if (txtcustomercode.Text == "" || txtcustname.Text == "")
                                {
                                    Master.ShowModal("Enter Customer Code or Customer Name. !!!", "txttransno", 1);
                                    return;
                                }

                                if (txtcustname.Text == "")
                                {
                                    Master.ShowModal("Enter Customer Name. !!!", " txtcramount", 1);
                                    return;
                                }
                                //String strconn12 = Dbconn.conmenthod();
                                OleDbConnection con2 = new OleDbConnection(strconn11);
                                con2.Open();
                                OleDbCommand cmd3 = new OleDbCommand("insert into tblCreditcustomer(Custcode,Custname,Amount,Billno,Trdate,Login_name,Sysdatetime,Mac_id) values('" + txtcustomercode.Text + "','" + txtcustname.Text + "','" + lblbillnor.Text + "','" + txtamount.Text + "','" + txtdate.Text + "','" + Session["username"].ToString() + "','" + sqlFormattedDate + "','" + sMacAddress + "')", con2);
                                cmd3.ExecuteNonQuery();
                                con.Close();

                            }

                        }

                        //this.Gridview1.Refresh();
                        //this.Gridview1.DataSource = null;
                        //box1.text= string.Empty;
                        //Gridview1.Refresh();
                        // ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

                    }
                }
            }
        }

        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }

        btnprint.Enabled = true;

    }

    private string GetConnectionString()
    {
        //"DBConnection" is the name of the Connection String
        //that was set up from the web.config file
        return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
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

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        

        string indate = txtdate.Text;


        
        string batchno = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("ddl_Batch") as DropDownList).Text);
        string rate = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txtrate") as TextBox).Text);
        string discount = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txtdiscount") as TextBox).Text);
        string tax = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txttax") as TextBox).Text);

        string productcode = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Text);

        //string tax1 = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("ddltax") as DropDownList).SelectedValue);

      if (batchno == "")
        {
            Master.ShowModal("Batch No Mandatory", "txtexpiredate", 0);
            return;

        }

        if (rate == "")
        {
            Master.ShowModal("Rate Mandatory", "txtstockarrival", 0);
            return;

        }

        


        if (tax == "")
        {
            Master.ShowModal("Tax Mandatory", "txtstockarrival", 0);
            return;

        }

      

      if (productcode == "")
        {
            Master.ShowModal("Product Code is Mandatory", "txtinvoicedate", 0);
            return;

        }

      Panel4.Visible = false;
      payment();
     

         AddNewRowToGrid();

          int rowIndex = 0;

          if (ViewState["CurrentTable"] != null)
          {
              DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
              DataRow drCurrentRow = null;
              if (dtCurrentTable.Rows.Count > 0)
              {
                  for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                  {
                      //extract the TextBox values
                      TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                      rowIndex++;
                      box0.Focus();
                  }
              }
          }
                   
      
        

        
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");
                    
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtexpiredate");
                    //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                    DropDownList box3 = (DropDownList)Gridview1.Rows[rowIndex].Cells[4].FindControl("ddl_Batch");
                    TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtStockinhand");
                    TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtrate");
                    TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                    TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtquantity");
                    TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("txttaxrate");
                    TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[10].FindControl("txtdiscount");
                    TextBox box10 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("txtproamount");
                    TextBox box11 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("txtgroupname");





                    

                        box0.Text = dt.Rows[i]["Productcode"].ToString();
                        box1.Text = dt.Rows[i]["ProductName"].ToString();
                        box2.Text = dt.Rows[i]["Expiredate"].ToString();
                        //************************
                       // box3.ClearSelection();
                       // box3.Items.FindByText(dt.Rows[i]["Batchno"].ToString()).Selected = true;
                        //box3.SelectedItem.Text = dt.Rows[i]["Batchno"].ToString();
                        box3.Items.Add(dt.Rows[i]["Batchno"].ToString());
                        //box3.Text = "500";
                       // box3.Text = dt.Rows[i]["Batchno"].ToString();
                        box4.Text = dt.Rows[i]["Stockinhand"].ToString();
                        box5.Text = dt.Rows[i]["Rate"].ToString();
                        box6.Text = dt.Rows[i]["Taxamount"].ToString();
                        box7.Text = dt.Rows[i]["Quantity"].ToString();
                        box8.Text = dt.Rows[i]["taxrate"].ToString();
                        box9.Text = dt.Rows[i]["D_Rate"].ToString();
                        box10.Text = dt.Rows[i]["Pro_Amount"].ToString();
                        box11.Text = dt.Rows[i]["g_name"].ToString();
                    
                   
                   


                    rowIndex++;

                }
            }
            // ViewState["CurrentTable"] = dt;

        }
    }

   



    protected void txtproductcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = (TextBox)sender;
            GridViewRow row = (GridViewRow)txt.NamingContainer;




            string productcode10 = Convert.ToString((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtproductcode") as TextBox).Text);


            string filename = Dbconn.Mymenthod();
            if (!File.Exists(filename))
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text);

                    using (SqlConnection conn = new SqlConnection(strconn1))
                    {


                        string commandString = "SELECT Productname,Productcode,Expiredate,Batchid,Tax FROM tblProductinward " +
                                                              String.Format("WHERE (Productcode = '{0}')", ID);

                        SqlCommand cmd = new SqlCommand(commandString, conn);
                        conn.Open();


                        SqlDataReader dr = cmd.ExecuteReader();



                        if (dr.HasRows)
                        {

                            dr.Read();


                            ((Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text) = dr["ProductName"].ToString();
                            ((Gridview1.Rows[i].Cells[1].FindControl("txtexpiredate") as TextBox).Text) = dr["Expiredate"].ToString();
                            //((Gridview1.Rows[i].Cells[1].FindControl("ddl_Batch") as DropDownList).SelectedItem.Text) = dr["Batchid"].ToString();




                            // SqlConnection con51 = new SqlConnection(strconn1);
                            // SqlCommand cmd51 = new SqlCommand("select Tax_Rate from tblTax_Rate where Close_flag='N'", con51);
                            // SqlDataAdapter da51 = new SqlDataAdapter(cmd51);
                            // DataSet ds51 = new DataSet();
                            // da51.Fill(ds51);

                            // g = Convert.ToInt32(ds51.Tables[0].Rows[0]["Tax_Rate"].ToString());
                            //((Gridview1.Rows[i].Cells[1].FindControl("txttax") as TextBox).Text) = g.ToString();

                            DataSet dsprodin = clsgd.GetcondDataSet("*", "tblProductMaster", "Productcode", ID);

                            // DataSet dsprodin = clsgd.GetcondDataSet("*", "tblProductMaster", "g_code", lblgroupname.Text);
                            string g_code = Convert.ToString(dsprodin.Tables[0].Rows[0]["g_code"].ToString());
                            DataSet dsprodin10 = clsgd.GetcondDataSet("*", "tblGroup", "g_code", g_code);
                            string gcode = Convert.ToString(dsprodin10.Tables[0].Rows[0]["g_code"].ToString());

                            string Close_flag = "N";

                            DataSet dsprodin12 = clsgd.GetcondDataSet2("*", "tblTax_Rate", "g_code", gcode, "Close_flag", Close_flag);
                            string Tax_Rate = Convert.ToString(dsprodin12.Tables[0].Rows[0]["Tax_Rate"].ToString());

                            if (dsprodin12.Tables[0].Rows.Count > 0)
                            {

                                ((Gridview1.Rows[i].Cells[1].FindControl("txttax") as TextBox).Text) = Tax_Rate.ToString();

                            }
                            else
                            {
                                Master.ShowModal("Enter the tax for this product in sales tax", "txtproductcode", 0);
                                return;
                            }

                            DataSet dsprodin20 = clsgd.GetcondDataSet("*", "tblGroup", "g_name", g_code);
                            string gname = Convert.ToString(dsprodin10.Tables[0].Rows[0]["g_name"].ToString());

                            ((Gridview1.Rows[i].Cells[1].FindControl("txtgroupname") as TextBox).Text) = gname.ToString();



                            conn.Close();

                        }
                        else
                        {

                            // Do something if no user is found
                            // OR do nothing
                            ShowPopupMessage("Product Name does not exist", PopupMessageType.txtproductcode);
                            (Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text = string.Empty;
                            (Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Focus();
                            return;

                        }

                        DateTime dtEntered = Convert.ToDateTime(txtdate.Text);
                        string strEnteredDate = dtEntered.ToString("MM/dd/yyyy");

                        for (int j = 0; j < Gridview1.Rows.Count; j++)
                        {

                            using (SqlConnection conn10 = new SqlConnection(strconn1))
                            {
                                conn10.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

                                // string ProductName = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text);

                                string Productcode1 = Convert.ToString((Gridview1.Rows[j].Cells[1].FindControl("txtproductcode") as TextBox).Text);
                                string productname = Convert.ToString((Gridview1.Rows[j].Cells[1].FindControl("txtproductname") as TextBox).Text);

                                //DropDownList ddll = (DropDownList)(Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Text);

                                DropDownList ddll = (DropDownList)Gridview1.Rows[j].Cells[7].FindControl("ddl_Batch");

                                DataSet ds = new DataSet();
                                conn10.Open();
                                 string pharmflag = "Y";

                                 string ID1 = Convert.ToString((Gridview1.Rows[j].Cells[1].FindControl("txtproductcode") as TextBox).Text);

                                 DataSet pharm1 = clsgd.GetcondDataSet2("*", "tblProductMaster", "Pharmflag", pharmflag, "Productcode", Productcode1);

                                 if (pharm1.Tables[0].Rows.Count > 0)
                                 {



                                     //string cmdstr = "Select Batchid from tblProductinward where productcode ="'+ productcode +'"";
                                     string cmdstr = "SELECT  * FROM tblProductinward WHERE tblProductinward.Expiredate >= '" + strEnteredDate + "' AND Productcode = '" + Productcode1 + "' AND Stockinhand > 0   ORDER BY tblProductinward.Expiredate ASC";

                                     SqlCommand cmd10 = new SqlCommand(cmdstr, conn10);

                                     SqlDataAdapter dda = new SqlDataAdapter(cmd10);

                                     DataSet ds1 = new DataSet();

                                     dda.Fill(ds1);


                                     //int  sqlresult = cmd10.ExecuteNonQuery();

                                     if (ds1.Tables[0].Rows.Count > 0)
                                     {

                                         SqlDataAdapter adp = new SqlDataAdapter(cmd10);
                                         DataSet dsgrp2011 = clsgd.GetcondDataSet3("Batchid", "tbltempprodsale", "Productcode", Productcode1, "ProductName", productname, "Batchid", ddll.Text);
                                         if (dsgrp2011.Tables[0].Rows.Count == 0)
                                         {

                                             adp.Fill(ds);

                                             ddll.DataSource = ds.Tables[0];

                                             ddll.DataTextField = "Batchid";

                                             ddll.DataBind();
                                         }

                                         //ddll.Items.Insert(0, new ListItem("--Select--", "0"));
                                         DropDownList ddl = (DropDownList)row.FindControl("ddl_Batch");
                                         //DropDownList ddl = (DropDownList)Gridview1.FooterRow.FindControl("ddl_Batch");
                                         DataSet dsgrp201 = clsgd.GetcondDataSet("*", "tbltempprodsale", "Productcode", productcode10);

                                         // if (dsgrp201.Tables[0].Rows.Count > 0)
                                         for (int l = 0; l < dsgrp201.Tables[0].Rows.Count; l++)
                                         {
                                             string batchnumber1 = dsgrp201.Tables[0].Rows[l]["Batchid"].ToString();
                                             //  ShowModal1("Different Batch Id", "txtbatchno", 0);
                                             ddl.Items.Remove(batchnumber1);

                                         }
                                     }
                                     else
                                     {
                                         ShowPopupMessage("Product code date is expiry", PopupMessageType.txtproductcode);
                                         (Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Focus();
                                         return;

                                     }
                                 }

                                 else
                                 {









                                     string cmdstr = "SELECT  * FROM tblProductinward WHERE  Productcode = '" + Productcode1 + "' AND Stockinhand > 0   ORDER BY tblProductinward.Expiredate ASC";

                                     SqlCommand cmd10 = new SqlCommand(cmdstr, conn10);

                                     SqlDataAdapter dda = new SqlDataAdapter(cmd10);

                                     DataSet ds1 = new DataSet();

                                     dda.Fill(ds1);


                                     //int  sqlresult = cmd10.ExecuteNonQuery();

                                     if (ds1.Tables[0].Rows.Count > 0)
                                     {

                                         SqlDataAdapter adp = new SqlDataAdapter(cmd10);
                                         DataSet dsgrp2011 = clsgd.GetcondDataSet3("Batchid", "tbltempprodsale", "Productcode", Productcode1, "ProductName", productname, "Batchid", ddll.Text);
                                         if (dsgrp2011.Tables[0].Rows.Count == 0)
                                         {

                                             adp.Fill(ds);

                                             ddll.DataSource = ds.Tables[0];

                                             ddll.DataTextField = "Batchid";

                                             ddll.DataBind();
                                         }

                                         //ddll.Items.Insert(0, new ListItem("--Select--", "0"));
                                         DropDownList ddl = (DropDownList)row.FindControl("ddl_Batch");
                                         //DropDownList ddl = (DropDownList)Gridview1.FooterRow.FindControl("ddl_Batch");
                                         DataSet dsgrp201 = clsgd.GetcondDataSet("*", "tbltempprodsale", "Productcode", productcode10);

                                         // if (dsgrp201.Tables[0].Rows.Count > 0)
                                         for (int l = 0; l < dsgrp201.Tables[0].Rows.Count; l++)
                                         {
                                             string batchnumber1 = dsgrp201.Tables[0].Rows[l]["Batchid"].ToString();
                                             //  ShowModal1("Different Batch Id", "txtbatchno", 0);
                                             ddl.Items.Remove(batchnumber1);

                                         }
                                     }


                                 }
                                conn10.Close();

                            }




                            //(Gridview1.Rows[j].Cells[1].FindControl("ddl_Batch") as DropDownList).Focus();

                            // Close connections


                        }
                    }





                    using (SqlConnection conn15 = new SqlConnection(strconn))
                    {
                        //conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];



                        SqlCommand cmd15 = new SqlCommand("select * from tblGroup where p_flag='Y'", conn15);
                        conn15.Open();

                        // Execute SQL and get returned Reader
                        SqlDataReader dr15 = cmd15.ExecuteReader();


                        // Test for values in DataReader
                        if (dr15.HasRows)
                        {
                            // Setup DataReader
                            dr15.Read();
                            string pharm_flag = dr15["p_flag"].ToString();

                            if (pharm_flag == "Y")
                            {


                                using (SqlConnection conn10 = new SqlConnection(strconn1))
                                {
                                    conn10.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

                                    for (int m = 0; m < Gridview1.Rows.Count; m++)
                                    {
                                        string productcode1 = Convert.ToString((Gridview1.Rows[m].Cells[1].FindControl("txtproductcode") as TextBox).Text);

                                        string productname1 = Convert.ToString((Gridview1.Rows[m].Cells[1].FindControl("txtproductname") as TextBox).Text);

                                        string Batch1 = Convert.ToString((Gridview1.Rows[m].Cells[1].FindControl("ddl_Batch") as DropDownList).Text);


                                        //DropDownList ddll = (DropDownList)(Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Text);





                                    }







                                    // Close connections
                                }






                            }


                        }
                    }



                }



            }
            else
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text);
                    //OleDbCommand com;


                    using (OleDbConnection conn = new OleDbConnection(strconn1))
                    {



                        //conn.Open();
                        string commandString = "SELECT Productname,Productcode,Expiredate FROM tblProductinward " +
                                                               String.Format("WHERE (Productcode = '{0}')", ID);
                        OleDbCommand cmd = new OleDbCommand(commandString, conn);
                        conn.Open();
                        OleDbDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            // Setup DataReader
                            dr.Read();

                            // Set DR values to Text fields
                            ((Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text) = dr["Productcode"].ToString();
                            ((Gridview1.Rows[i].Cells[1].FindControl("txtexpiredate") as TextBox).Text) = dr["Expiredate"].ToString();
                            //((Gridview1.Rows[i].Cells[1].FindControl("ddl_Batch") as TextBox).Text) = dr["Batchid"].ToString();
                           

                        }
                        else
                        {

                        }

                        using (OleDbConnection conn10 = new OleDbConnection(strconn1))
                        {
                            conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

                            string productcode = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Text);

                            //DropDownList ddll = (DropDownList)(Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Text);

                            DropDownList ddll = (DropDownList)Gridview1.Rows[0].Cells[7].FindControl("ddl_Batch");

                            DataSet ds = new DataSet();
                            conn10.Open();

                            //string cmdstr = "Select Batchid from tblProductinward where productcode ="'+ productcode +'"";
                            string cmdstr = "Select  Batchid from tblProductinward  where productcode='" + productcode + "' GROUP BY Batchid ORDER BY Batchid*1 ASC";

                            OleDbCommand cmd10 = new OleDbCommand(cmdstr, conn10);

                            OleDbDataAdapter adp = new OleDbDataAdapter(cmd10);

                            adp.Fill(ds);

                            ddll.DataSource = ds.Tables[0];

                            ddll.DataTextField = "Batchid";

                            //ddlproductcode.DataValueField = "id";

                            ddll.DataBind();

                            //ddll.Items.Insert(0, new ListItem("--Select--", "0"));

                            conn.Close();

                        }



                        (Gridview1.Rows[0].Cells[1].FindControl("txtquantity") as TextBox).Focus();




                    }
                }
            }


            if (!File.Exists(filename))
            {

                 TextBox txt30 = (TextBox)sender;
                GridViewRow row30 = (GridViewRow)txt30.NamingContainer;
                // Gridview1.Rows[row1.RowIndex].FindControl("txtdiscount").Focus();

                string productname30 = ((Gridview1.Rows[row30.RowIndex].FindControl("txtproductname") as TextBox).Text);

                DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblProductMaster", "Productname", productname30);
                string flag2 = dsgroup10.Tables[0].Rows[0]["Pharmflag"].ToString();
                if (flag2 == "Y")
                {
                    for (int i = 0; i < Gridview1.Rows.Count; i++)
                    {
                        string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("ddl_Batch") as DropDownList).Text);
                        string productcode28 = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text);

                        using (SqlConnection conn = new SqlConnection(strconn))
                        {
                            string commandString = "SELECT Stockinhand FROM tblProductinward where Batchid='" + ID + "' and productcode='" + productcode28 + "'";
                            //SqlConnection cnn = new SqlConnection(connectionString);
                            SqlCommand cmd = new SqlCommand(commandString, conn);
                            conn.Open();

                            // Execute SQL and get returned Reader
                            SqlDataReader dr = cmd.ExecuteReader();


                            // Test for values in DataReader
                            if (dr.HasRows)
                            {
                                // Setup DataReader
                                dr.Read();

                                // Set DR values to Text fields
                                //((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text) = dr["Productcode"].ToString();
                               // ((Gridview1.Rows[i].Cells[1].FindControl("txtStockinhand") as TextBox).Text) = dr["Stockinhand"].ToString();

                                ((Gridview1.Rows[row30.RowIndex].FindControl("txtStockinhand") as TextBox).Text) = dr["Stockinhand"].ToString();







                            }



                            else
                            {
                                // Do something if no user is found
                                // OR do nothing
                            }

                            // Close connections
                            dr.Close();
                            conn.Close();
                        }

                    }


                    for (int k = 0; k < Gridview1.Rows.Count; k++)
                    {

                        string productcode = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductcode") as TextBox).Text);
                        string productname = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtproductname") as TextBox).Text);
                        string Batch = Convert.ToString((Gridview1.Rows[k].Cells[3].FindControl("ddl_Batch") as DropDownList).Text);




                        using (SqlConnection conn = new SqlConnection(strconn))
                        {
                            //conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

                            //string commandString = "SELECT ProductName,Productcode,Sellprice,Batchid FROM tblProductinward " +
                            //String.Format("WHERE (Batchid = '{0}')", ID);
                            // string commandString = "SELECT ProductName,Productcode,Sellprice,MRP,Batchid FROM tblProductinward WHERE Productcode = " + productcode + " AND productname = '" + productname + "' AND Batchid = " + Batch;
                            //SqlConnection cnn = new SqlConnection(connectionString);
                            // SqlCommand cmd = new SqlCommand(commandString, conn);
                            // conn.Open();

                            // Execute SQL and get returned Reader
                            //SqlDataReader dr = cmd.ExecuteReader();

                            DataSet dschm = clsgd.GetcondDataSet3("*", "tblProductinward", "Productcode", productcode, "productname", productname, "Batchid", Batch);
                            if (dschm.Tables[0].Rows.Count > 0)
                            {




                                // Set DR values to Text fields
                                //((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text) = dr["Productcode"].ToString();
                                double tax1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txttax") as TextBox).Text);
                                //string mrp = dr["MRP"].ToString();
                                string mrp = dschm.Tables[0].Rows[0]["MRP"].ToString();
                                Double mrp1 = Convert.ToDouble(mrp);

                                Double amt = (mrp1 * tax1) / (100 + tax1);
                                Double fnamt = mrp1 - amt;
                                double fnamt10 = Math.Round(fnamt, 2);

                                ((Gridview1.Rows[k].Cells[1].FindControl("txtrate") as TextBox).Text) = Convert.ToString(fnamt10);





                            }




                            else
                            {
                                //Master.ShowModal("Same batch id exists and delete the new row.", "txtcustcode", 1);
                                //string name = (GridView1.FooterRow.FindControl("txtName") as TextBox).Text;

                                //return;
                            }

                            // Close connections
                            //dr.Close();
                            conn.Close();
                        }
                        string productcode1 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductcode") as TextBox).Text);
                        SqlConnection con58 = new SqlConnection(strconn1);
                        SqlCommand cmd58 = new SqlCommand("select sum(Stockinhand) as Stockinhand  from tblProductinward where  Productcode = '" + productcode1 + "' and Batchid = '" + Batch + "'", con58);
                        SqlDataAdapter da58 = new SqlDataAdapter(cmd58);
                        DataSet ds58 = new DataSet();

                        da58.Fill(ds58);
                        string stock = ds58.Tables[0].Rows[0]["Stockinhand"].ToString();

                        // int stock = Convert.ToInt32(ds58.Tables[0].Rows[0]["Stockinhand"].ToString());


                        //txtstock.Text = Convert.ToString(stock);
                        if (stock == "")
                        {
                            ShowPopupMessage("Pls chosse different Product Code", PopupMessageType.txtproductcode);

                            TextBox txt2 = (TextBox)sender;
                            GridViewRow row2 = (GridViewRow)txt2.NamingContainer;

                            // Gridview1.Rows[row1.RowIndex].FindControl("txtdiscount").Focus();

                            (Gridview1.Rows[row2.RowIndex].FindControl("txtproductcode") as TextBox).Text = string.Empty;
                            (Gridview1.Rows[row2.RowIndex].FindControl("txtproductname") as TextBox).Text = string.Empty;
                            (Gridview1.Rows[row2.RowIndex].FindControl("txtexpiredate") as TextBox).Text = string.Empty;
                            //(Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Focus();
                            return;
                        }


                        string productcode12 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductcode") as TextBox).Text);
                        SqlConnection con60 = new SqlConnection(strconn1);
                        SqlCommand cmd60 = new SqlCommand("select sum(Stockinhand) as Stockinhand  from tblProductinward where  Productcode = '" + productcode12 + "'", con60);
                        SqlDataAdapter da60 = new SqlDataAdapter(cmd60);
                        DataSet ds60 = new DataSet();

                        da60.Fill(ds60);
                        string stock10 = ds60.Tables[0].Rows[0]["Stockinhand"].ToString();
                        int stock11 = Convert.ToInt32(stock10);







                        // txtstock.Text = totstock;

                        DataSet dsgrp16 = clsgd.GetcondDataSet("*", "tbltempprodsale", "Productcode", productcode1);
                        if (dsgrp16.Tables[0].Rows.Count > 0)
                        {
                            string productcode24 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductcode") as TextBox).Text);
                            SqlConnection con65 = new SqlConnection(strconn1);
                            SqlCommand cmd65 = new SqlCommand("select Stockinhand  from tbltempprodsale where  Productcode = '" + productcode12 + "'", con58);
                            SqlDataAdapter da65 = new SqlDataAdapter(cmd65);
                            DataSet ds65 = new DataSet();

                            da65.Fill(ds65);
                            string stock15 = ds65.Tables[0].Rows[0]["Stockinhand"].ToString();
                            int stock18 = Convert.ToInt32(stock15);

                            string productcode2 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductcode") as TextBox).Text);
                            SqlConnection con59 = new SqlConnection(strconn1);
                            SqlCommand cmd59 = new SqlCommand("select sum(Stockinhand) as Stockinhand   from tblProductinward where  Productcode = '" + productcode2 + "'", con58);
                            SqlDataAdapter da59 = new SqlDataAdapter(cmd59);
                            DataSet ds59 = new DataSet();

                            da59.Fill(ds59);
                            string quantity = ds59.Tables[0].Rows[0]["Stockinhand"].ToString();
                            int quantity10 = Convert.ToInt32(quantity);

                            string totstock = Convert.ToString(quantity10 - stock18);
                            txtstock.Text = totstock;

                        }
                        else
                        {
                            txtstock.Text = stock10;

                        }


                    }
                }

                else
                {
                    for (int i = 0; i < Gridview1.Rows.Count; i++)
                    {
                        string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("ddl_Batch") as DropDownList).Text);
                        string productname28 = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text);


                        TextBox txt38 = (TextBox)sender;
                        GridViewRow row38 = (GridViewRow)txt38.NamingContainer;
                        // Gridview1.Rows[row1.RowIndex].FindControl("txtdiscount").Focus();

                        string productname38 = ((Gridview1.Rows[row38.RowIndex].FindControl("txtproductname") as TextBox).Text);


                        DataSet dsgrp = clsgd.GetcondDataSet("*", "tbltempprodsale", "ProductName", productname30);
                        if (dsgrp.Tables[0].Rows.Count > 0)
                        {
                            //Table2.Visible = true;
                            ShowPopupMessage("Pls chosse different Product Name", PopupMessageType.txtproductname);
                            (Gridview1.Rows[0].Cells[1].FindControl("txtproductname") as TextBox).Focus();
                            return;
                        }





                        using (SqlConnection conn = new SqlConnection(strconn))
                        {
                            //string commandString = "SELECT  SUM(Stockinhand) as Stockinhand  FROM tblProductinward where Batchid='" + ID + "'";

                            string commandString = "SELECT   sum(Stockinhand) as Stockinhand   FROM tblProductinward where  ProductName='" + productname28 + "'";
                            //SqlConnection cnn = new SqlConnection(connectionString);
                            SqlCommand cmd = new SqlCommand(commandString, conn);
                            conn.Open();

                            // Execute SQL and get returned Reader
                            SqlDataReader dr = cmd.ExecuteReader();


                            // Test for values in DataReader
                            if (dr.HasRows)
                            {
                                // Setup DataReader
                                dr.Read();

                                // Set DR values to Text fields
                                //((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text) = dr["Productcode"].ToString();
                                // ((Gridview1.Rows[i].Cells[1].FindControl("txtStockinhand") as TextBox).Text) = dr["Stockinhand"].ToString();

                                // ((Gridview1.Rows[i].Cells[1].FindControl("txtdiscount") as TextBox).Text) = "0";

                                ((Gridview1.Rows[row38.RowIndex].FindControl("txtStockinhand") as TextBox).Text) = dr["Stockinhand"].ToString();




                            }



                            else
                            {
                                // Do something if no user is found
                                // OR do nothing
                            }

                            // Close connections
                            dr.Close();
                            conn.Close();
                        }

                    }






                    for (int k = 0; k < Gridview1.Rows.Count; k++)
                    {

                        string productcode = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductcode") as TextBox).Text);
                        string productname = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtproductname") as TextBox).Text);
                        string Batch = Convert.ToString((Gridview1.Rows[k].Cells[3].FindControl("ddl_Batch") as DropDownList).Text);

                        using (SqlConnection conn = new SqlConnection(strconn))
                        {
                            //conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

                            //string commandString = "SELECT ProductName,Productcode,Sellprice,Batchid FROM tblProductinward " +
                            //String.Format("WHERE (Batchid = '{0}')", ID);
                            string commandString = "SELECT ProductName,Productcode,Sellprice,MRP,Batchid FROM tblProductinward WHERE Productcode = " + productcode + " AND productname = '" + productname + "' AND Batchid = '" + Batch + "'";
                            //SqlConnection cnn = new SqlConnection(connectionString);
                            SqlCommand cmd = new SqlCommand(commandString, conn);
                            conn.Open();

                            // Execute SQL and get returned Reader
                            SqlDataReader dr = cmd.ExecuteReader();


                            // Test for values in DataReader
                            if (dr.HasRows)
                            {
                                // Setup DataReader
                                dr.Read();

                                // Set DR values to Text fields
                                //((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text) = dr["Productcode"].ToString();
                                double tax1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txttax") as TextBox).Text);
                                string mrp = dr["MRP"].ToString();
                                Double mrp1 = Convert.ToDouble(mrp);

                                Double amt = (mrp1 * tax1) / (100 + tax1);
                                Double fnamt = mrp1 - amt;
                                double fnamt10 = Math.Round(fnamt, 2);

                                ((Gridview1.Rows[k].Cells[1].FindControl("txtrate") as TextBox).Text) = Convert.ToString(fnamt10);


                            }







                            else
                            {
                                // Do something if no user is found
                                // OR do nothing
                            }

                            // Close connections
                            dr.Close();
                            conn.Close();
                        }

                        string productname1 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductname") as TextBox).Text);
                        SqlConnection con58 = new SqlConnection(strconn1);
                        SqlCommand cmd58 = new SqlCommand("select sum(Stockinhand) as Stockinhand  from tblProductinward where  ProductName = '" + productname1 + "' and Batchid = '" + Batch + "'", con58);
                        SqlDataAdapter da58 = new SqlDataAdapter(cmd58);
                        DataSet ds58 = new DataSet();

                        da58.Fill(ds58);
                        string stock = ds58.Tables[0].Rows[0]["Stockinhand"].ToString();

                        // int stock = Convert.ToInt32(ds58.Tables[0].Rows[0]["Stockinhand"].ToString());


                        //txtstock.Text = Convert.ToString(stock);
                        if (stock == "")
                        {
                            ShowPopupMessage("Pls chosse different Product Name", PopupMessageType.txtproductname);
                            (Gridview1.Rows[0].Cells[1].FindControl("txtproductname") as TextBox).Focus();
                            return;
                        }


                        string productname12 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductname") as TextBox).Text);
                        SqlConnection con60 = new SqlConnection(strconn1);
                        SqlCommand cmd60 = new SqlCommand("select sum(Stockinhand) as Stockinhand  from tblProductinward where   ProductName = '" + productname12 + "'", con58);
                        SqlDataAdapter da60 = new SqlDataAdapter(cmd60);
                        DataSet ds60 = new DataSet();

                        da60.Fill(ds60);
                        string stock10 = ds60.Tables[0].Rows[0]["Stockinhand"].ToString();
                        int stock11 = Convert.ToInt32(stock10);







                        // txtstock.Text = totstock;

                        DataSet dsgrp16 = clsgd.GetcondDataSet("*", "tbltempprodsale", "ProductName", productname1);
                        if (dsgrp16.Tables[0].Rows.Count > 0)
                        {
                            string productcode24 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductname") as TextBox).Text);
                            SqlConnection con65 = new SqlConnection(strconn1);
                            SqlCommand cmd65 = new SqlCommand("select Stockinhand from tbltempprodsale where ProductName = '" + productcode24 + "'", con58);
                            SqlDataAdapter da65 = new SqlDataAdapter(cmd65);
                            DataSet ds65 = new DataSet();

                            da65.Fill(ds65);
                            string stock15 = ds65.Tables[0].Rows[0]["Stockinhand"].ToString();
                            int stock18 = Convert.ToInt32(stock15);

                            string productcode2 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductname") as TextBox).Text);
                            SqlConnection con59 = new SqlConnection(strconn1);
                            SqlCommand cmd59 = new SqlCommand("select sum(Stockinhand) as Stockinhand   from tbltempprodsale where  ProductName = '" + productcode2 + "'", con59);
                            SqlDataAdapter da59 = new SqlDataAdapter(cmd59);
                            DataSet ds59 = new DataSet();

                            da59.Fill(ds59);
                            string quantity = ds59.Tables[0].Rows[0]["Stockinhand"].ToString();
                            int quantity10 = Convert.ToInt32(quantity);

                            string totstock = Convert.ToString(quantity10 -stock18);
                            txtstock.Text = totstock;

                        }
                        else
                        {
                            txtstock.Text = stock10;

                        }



                    }




                }





            }
            else
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtbatchno") as TextBox).Text);
                    //OleDbCommand com;
                    string strconn1 = Dbconn.conmenthod();

                    using (OleDbConnection conn = new OleDbConnection(strconn1))
                    {



                        //conn.Open();
                        string commandString = "SELECT ProductName,Productcode,Sellprice,Batchid FROM tblProductinward " +
                                                                 String.Format("WHERE (Batchid = '{0}')", ID);
                        OleDbCommand cmd = new OleDbCommand(commandString, conn);
                        conn.Open();
                        OleDbDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            // Setup DataReader
                            dr.Read();

                            // Set DR values to Text fields
                            ((Gridview1.Rows[i].Cells[1].FindControl("txtrate") as TextBox).Text) = dr["Sellprice"].ToString();


                        }
                        else
                        {
                            // Do something if no user is found
                            // OR do nothing
                        }
                    }
                }
            }

            if (!File.Exists(filename))
            {








            }

            else
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtbatchno") as TextBox).Text);
                    //OleDbCommand com;
                    string strconn1 = Dbconn.conmenthod();

                    using (OleDbConnection conn = new OleDbConnection(strconn1))
                    {



                        //conn.Open();
                        string commandString = "SELECT Tax,Batchid,MRP FROM tblProductinward " +
                                                                  String.Format("WHERE (Batchid  = '{0}')", ID);
                        OleDbCommand cmd = new OleDbCommand(commandString, conn);
                        conn.Open();
                        OleDbDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            // Setup DataReader
                            dr.Read();

                            // Set DR values to Text fields
                            ((Gridview1.Rows[i].Cells[1].FindControl("txttax") as TextBox).Text) = dr["Tax"].ToString();

                            double mrp = Convert.ToDouble(dr["MRP"]);

                            int rowIndex = 0;
                            //StringCollection sc = new StringCollection();
                            if (ViewState["CurrentTable"] != null)
                            {
                                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                                DataRow drCurrentRow = null;
                                if (dtCurrentTable.Rows.Count > 0)
                                {
                                    for (int j = 1; j <= dtCurrentTable.Rows.Count; j++)
                                    {
                                        TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");
                                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtquantity");
                                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                                        //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                                        TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtbatchno");
                                        TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtrate");
                                        TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtdiscount");
                                        TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                                        TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtproamount");

                                       // ((Gridview1.Rows[i].Cells[1].FindControl("txtdiscount") as TextBox).Text) = "0";

                                    

                                        double quantity = Convert.ToDouble(box2.Text);

                                        double rate = Convert.ToDouble(box5.Text);

                                        double tax = Convert.ToDouble(box7.Text);

                                        double taxrate = (mrp * tax) / (100 + tax);

                                        double productamount = (quantity * rate) + taxrate;
                                        double productamount1 = Math.Round(productamount, 2);
                                        string productamount2 = Convert.ToString(productamount1);

                                        ((Gridview1.Rows[i].Cells[1].FindControl("txtproamount") as TextBox).Text) = Convert.ToString(productamount2);

                                        double selprice = Convert.ToDouble(mrp - taxrate);

                                        double rselprice = Math.Round(selprice, 2);
                                        string selprice1 = Convert.ToString(rselprice);
                                        System.DateTime Dtnow = DateTime.Now;
                                        string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                                        String strconn11 = Dbconn.conmenthod();

                                        Double sum = 0;
                                        Double add = 0.0;

                                        for (int k = 0; k < Gridview1.Rows.Count; k++)
                                        {

                                            if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text))
                                            {
                                                add = 0.0;

                                            }
                                            else
                                            {

                                                add = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text);
                                                sum = sum + add;
                                            }


                                            txtpramount.Text = (sum).ToString();


                                            if (txttotalamount.Text == "")
                                            {
                                                txttotalamount.Text = (sum).ToString();
                                            }
                                            else
                                            {
                                                double totamount = Convert.ToDouble(txttotalamount.Text);
                                                txttotalamount.Text = (sum - totamount).ToString();
                                            }

                                        }




                                    }


                                    //(Gridview1.Rows[0].Cells[1].FindControl("txtbatchno") as TextBox).Focus();
                                }

                            }


                        }
                        else
                        {
                            // Do something if no user is found
                            // OR do nothing
                        }
                    }
                }
            }


            TextBox txt1 = (TextBox)sender;
            GridViewRow row1 = (GridViewRow)txt1.NamingContainer;
           // Gridview1.Rows[row1.RowIndex].FindControl("txtdiscount").Focus();

            ((Gridview1.Rows[row1.RowIndex].FindControl("txtdiscount") as TextBox).Text) = "0";




        // (Gridview1.Rows[0].Cells[1].FindControl("txtquantity") as TextBox).Focus();
        
        Gridview1.Rows[row.RowIndex].FindControl("txtquantity").Focus();

        }

        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
       
       

    }


    protected void txtproductname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = (TextBox)sender;
            GridViewRow row = (GridViewRow)txt.NamingContainer;

            string productname10 = Convert.ToString((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtproductname") as TextBox).Text);

            string filename = Dbconn.Mymenthod();
            if (!File.Exists(filename))
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text);

                    using (SqlConnection conn = new SqlConnection(strconn1))
                    {
                        //conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

                        string commandString = "SELECT Productname,Productcode,Expiredate,Batchid,Tax FROM tblProductinward " +
                                                              String.Format("WHERE (Productname = '{0}')", ID);
                        //SqlConnection cnn = new SqlConnection(connectionString);
                        SqlCommand cmd = new SqlCommand(commandString, conn);
                        conn.Open();

                        // Execute SQL and get returned Reader
                        SqlDataReader dr = cmd.ExecuteReader();


                        // Test for values in DataReader
                        if (dr.HasRows)
                        {
                            // Setup DataReader
                            dr.Read();

                            // Set DR values to Text fields
                            ((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text) = dr["Productcode"].ToString();
                            ((Gridview1.Rows[i].Cells[1].FindControl("txtexpiredate") as TextBox).Text) = dr["Expiredate"].ToString();
                            //((Gridview1.Rows[i].Cells[1].FindControl("ddl_Batch") as DropDownList).SelectedItem.Text) = dr["Batchid"].ToString();

                            //SqlConnection con50 = new SqlConnection(strconn1);
                            //SqlCommand cmd50 = new SqlCommand("select From_Date as From_Date,To_Date as To_Date from tblTax_Rate where Close_flag='Y'", con50);
                            //SqlDataAdapter da50 = new SqlDataAdapter(cmd50);
                            //DataSet ds50 = new DataSet();

                            //da50.Fill(ds50);

                            //string frmdate = Convert.ToString(ds50.Tables[0].Rows[0]["From_Date"].ToString());
                            //DateTime from_date1 = Convert.ToDateTime(frmdate);
                            //string todate = Convert.ToString(ds50.Tables[0].Rows[0]["To_Date"].ToString());
                            //DateTime to_date1 = Convert.ToDateTime(todate);

                            //string sdate = txtdate.Text;
                            //DateTime sdate1 = Convert.ToDateTime(sdate);

                            //if (sdate1 > from_date1 && sdate1 < to_date1)
                            //{

                            //    ((Gridview1.Rows[i].Cells[1].FindControl("txttax") as TextBox).Text) = dr["Tax"].ToString();
                            //}
                            //else
                            //{
                            //    SqlConnection con51 = new SqlConnection(strconn1);
                            //    SqlCommand cmd51 = new SqlCommand("select Tax_Rate from tblTax_Rate where Close_flag='N'", con51);
                            //    SqlDataAdapter da51 = new SqlDataAdapter(cmd51);
                            //    DataSet ds51 = new DataSet();
                            //    da51.Fill(ds51);

                            //    g = Convert.ToDouble(ds51.Tables[0].Rows[0]["Tax_Rate"].ToString());
                            //    ((Gridview1.Rows[i].Cells[1].FindControl("txttax") as TextBox).Text) = g.ToString();


                            //}

                            DataSet dsprodin = clsgd.GetcondDataSet("*", "tblProductMaster", "Productname", ID);

                            // DataSet dsprodin = clsgd.GetcondDataSet("*", "tblProductMaster", "g_code", lblgroupname.Text);
                            string g_code = Convert.ToString(dsprodin.Tables[0].Rows[0]["g_code"].ToString());
                            DataSet dsprodin10 = clsgd.GetcondDataSet("*", "tblGroup", "g_code", g_code);
                            string gcode = Convert.ToString(dsprodin10.Tables[0].Rows[0]["g_code"].ToString());

                            string Close_flag = "N";

                            DataSet dsprodin12 = clsgd.GetcondDataSet2("*", "tblTax_Rate", "g_code", gcode, "Close_flag", Close_flag);
                            string Tax_Rate = Convert.ToString(dsprodin12.Tables[0].Rows[0]["Tax_Rate"].ToString());

                            if (dsprodin12.Tables[0].Rows.Count > 0)
                            {

                                ((Gridview1.Rows[i].Cells[1].FindControl("txttax") as TextBox).Text) = Tax_Rate.ToString();


                            }
                            else
                            {
                                Master.ShowModal("Enter the tax for this product in sales tax", "txtproductcode", 0);
                                return;
                            }


                            DataSet dsprodin20 = clsgd.GetcondDataSet("*", "tblGroup", "g_name", g_code);
                            string gname = Convert.ToString(dsprodin10.Tables[0].Rows[0]["g_name"].ToString());

                            ((Gridview1.Rows[i].Cells[1].FindControl("txtgroupname") as TextBox).Text) = gname.ToString();

                            conn.Close();

                        }
                        else
                        {

                            // Do something if no user is found
                            // OR do nothing
                            ShowPopupMessage("Product Name does not exist", PopupMessageType.txtproductname);
                            (Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text = string.Empty;
                            (Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Focus();

                            return;

                        }
                    }


                    DateTime dtEntered = Convert.ToDateTime(txtdate.Text);
                    string strEnteredDate = dtEntered.ToString("MM/dd/yyyy");

                      for (int j = 0; j < Gridview1.Rows.Count; j++)
                                     {

                                         using (SqlConnection conn10 = new SqlConnection(strconn1))
                                         {


                      string pharmflag = "Y";
                    
                                 string ID1 = Convert.ToString((Gridview1.Rows[j].Cells[1].FindControl("txtproductname") as TextBox).Text);

                                 DataSet pharm1 = clsgd.GetcondDataSet2("*", "tblProductMaster", "Pharmflag", pharmflag, "Productname", ID1);

                                 if (pharm1.Tables[0].Rows.Count > 0)
                                 {





                                     string Productcode1 = Convert.ToString((Gridview1.Rows[j].Cells[1].FindControl("txtproductcode") as TextBox).Text);
                                     string productname = Convert.ToString((Gridview1.Rows[j].Cells[1].FindControl("txtproductname") as TextBox).Text);

                                     //DropDownList ddll = (DropDownList)(Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Text);

                                     DropDownList ddll = (DropDownList)Gridview1.Rows[j].Cells[7].FindControl("ddl_Batch");

                                     DataSet ds = new DataSet();
                                     conn10.Open();

                                     //string cmdstr = "Select Batchid from tblProductinward where productcode ="'+ productcode +'"";
                                     string cmdstr = "SELECT  * FROM tblProductinward WHERE tblProductinward.Expiredate > '" + strEnteredDate + "' AND ProductName = '" + productname + "'  AND  Stockinhand > 0    ORDER BY tblProductinward.Expiredate ASC";

                                     // SqlCommand cmd10 = new SqlCommand(cmdstr, conn10);
                                     SqlCommand cmd10 = new SqlCommand(cmdstr, conn10);

                                     SqlDataAdapter dda = new SqlDataAdapter(cmd10);

                                     DataSet ds1 = new DataSet();

                                     dda.Fill(ds1);

                                     if (ds1.Tables[0].Rows.Count > 0)
                                     {

                                         SqlDataAdapter adp = new SqlDataAdapter(cmd10);
                                         DataSet dsgrp2011 = clsgd.GetcondDataSet3("Batchid", "tbltempprodsale", "Productcode", Productcode1, "ProductName", productname, "Batchid", ddll.Text);
                                         if (dsgrp2011.Tables[0].Rows.Count == 0)
                                         {

                                             adp.Fill(ds);

                                             ddll.DataSource = ds.Tables[0];

                                             ddll.DataTextField = "Batchid";

                                             ddll.DataBind();
                                         }

                                         DropDownList ddl = (DropDownList)row.FindControl("ddl_Batch");
                                         //DropDownList ddl = (DropDownList)Gridview1.FooterRow.FindControl("ddl_Batch");
                                         DataSet dsgrp201 = clsgd.GetcondDataSet("*", "tbltempprodsale", "ProductName", productname10);

                                         // if (dsgrp201.Tables[0].Rows.Count > 0)
                                         for (int l = 0; l < dsgrp201.Tables[0].Rows.Count; l++)
                                         {
                                             string batchnumber1 = dsgrp201.Tables[0].Rows[l]["Batchid"].ToString();
                                             //  ShowModal1("Different Batch Id", "txtbatchno", 0);
                                             ddl.Items.Remove(batchnumber1);

                                         }
                                     }
                                     else
                                     {
                                         ShowPopupMessage("Product  Name is expiry", PopupMessageType.txtproductname);
                                         (Gridview1.Rows[0].Cells[1].FindControl("txtproductname") as TextBox).Focus();
                                         return;

                                     }
                                     conn10.Close();

                                 }



                                        // (Gridview1.Rows[j].Cells[1].FindControl("ddl_Batch") as DropDownList).Focus();

                                         // Close connections




                                 else
                                 {


                                     string Productcode1 = Convert.ToString((Gridview1.Rows[j].Cells[1].FindControl("txtproductcode") as TextBox).Text);
                                     string productname = Convert.ToString((Gridview1.Rows[j].Cells[1].FindControl("txtproductname") as TextBox).Text);

                                     //DropDownList ddll = (DropDownList)(Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Text);

                                     DropDownList ddll = (DropDownList)Gridview1.Rows[j].Cells[7].FindControl("ddl_Batch");

                                     DataSet ds = new DataSet();
                                     conn10.Open();

                                     //string cmdstr = "Select Batchid from tblProductinward where productcode ="'+ productcode +'"";
                                     string cmdstr = "SELECT  * FROM tblProductinward WHERE ProductName = '" + productname + "'  AND  Stockinhand > 0";

                                     // SqlCommand cmd10 = new SqlCommand(cmdstr, conn10);
                                     SqlCommand cmd10 = new SqlCommand(cmdstr, conn10);

                                     SqlDataAdapter dda = new SqlDataAdapter(cmd10);

                                     DataSet ds1 = new DataSet();

                                     dda.Fill(ds1);

                                     if (ds1.Tables[0].Rows.Count > 0)
                                     {

                                         SqlDataAdapter adp = new SqlDataAdapter(cmd10);
                                         DataSet dsgrp2011 = clsgd.GetcondDataSet3("Batchid", "tbltempprodsale", "Productcode", Productcode1, "ProductName", productname, "Batchid", ddll.Text);
                                         if (dsgrp2011.Tables[0].Rows.Count == 0)
                                         {

                                             adp.Fill(ds);

                                             ddll.DataSource = ds.Tables[0];

                                             ddll.DataTextField = "Batchid";

                                             ddll.DataBind();
                                         }

                                         DropDownList ddl = (DropDownList)row.FindControl("ddl_Batch");
                                         //DropDownList ddl = (DropDownList)Gridview1.FooterRow.FindControl("ddl_Batch");
                                         DataSet dsgrp201 = clsgd.GetcondDataSet("*", "tbltempprodsale", "ProductName", productname10);

                                         // if (dsgrp201.Tables[0].Rows.Count > 0)
                                         for (int l = 0; l < dsgrp201.Tables[0].Rows.Count; l++)
                                         {
                                             string batchnumber1 = dsgrp201.Tables[0].Rows[l]["Batchid"].ToString();
                                             //  ShowModal1("Different Batch Id", "txtbatchno", 0);
                                             ddl.Items.Remove(batchnumber1);

                                         }

                                         TextBox txt25 = (TextBox)sender;
                                         GridViewRow row25 = (GridViewRow)txt25.NamingContainer;

                                         System.DateTime Dtnow1 = DateTime.Now;
                                         string Sysdatetime1 = Dtnow1.ToString("dd/MM/yyyy");


                                         // ((Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtproductname") as TextBox).Text) = Sysdatetime1;

                                         //  string invoiceno = Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtinvoiceno");

                                         string txt1 = ((Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtproductname") as TextBox).Text);

                                         // double invoiceno10 = Convert.ToDouble(invoiceno);


                                         for (int k = 0; k < Gridview1.Rows.Count; k++)
                                         {
                                             string ID10 = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtproductname") as TextBox).Text);

                                             //string date10 = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtproductname") as TextBox).Text);


                                             //double id20 = Convert.ToDouble(ID1);

                                             if (ID10 == txt1)
                                             {

                                                 // ((Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtinvoicedate") as TextBox).Text) = date10;



                                             }
                                         }



                                         //else
                                         //{
                                         //    ShowPopupMessage("Product  Name is expiry", PopupMessageType.txtproductname);
                                         //    (Gridview1.Rows[0].Cells[1].FindControl("txtproductname") as TextBox).Focus();
                                         //    return;

                                         //}
                                         conn10.Close();

                                     }
                                 }



                                         (Gridview1.Rows[j].Cells[1].FindControl("ddl_Batch") as DropDownList).Focus();

                                         // Close connections


                                     }

                                   







                                 }

                                   


                    using (SqlConnection conn15 = new SqlConnection(strconn))
                    {
                        //conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];



                        SqlCommand cmd15 = new SqlCommand("select * from tblGroup where p_flag='Y'", conn15);
                        conn15.Open();

                        // Execute SQL and get returned Reader
                        SqlDataReader dr15 = cmd15.ExecuteReader();


                        // Test for values in DataReader
                        if (dr15.HasRows)
                        {
                            // Setup DataReader
                            dr15.Read();
                            string pharm_flag = dr15["p_flag"].ToString();

                            if (pharm_flag == "Y")
                            {


                                using (SqlConnection conn10 = new SqlConnection(strconn1))
                                {
                                    conn10.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

                                    for (int m = 0; m < Gridview1.Rows.Count; m++)
                                    {
                                        string productcode1 = Convert.ToString((Gridview1.Rows[m].Cells[1].FindControl("txtproductcode") as TextBox).Text);

                                        string productname1 = Convert.ToString((Gridview1.Rows[m].Cells[1].FindControl("txtproductname") as TextBox).Text);

                                        string Batch1 = Convert.ToString((Gridview1.Rows[m].Cells[1].FindControl("ddl_Batch") as DropDownList).Text);


                                        //DropDownList ddll = (DropDownList)(Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Text);



                                    }







                                    // Close connections
                                }








                            }


                        }
                    }

                }

            }
            else
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text);
                    //OleDbCommand com;


                    using (OleDbConnection conn = new OleDbConnection(strconn1))
                    {



                        //conn.Open();
                        string commandString = "SELECT Productname,Productcode,Expiredate FROM tblProductinward " +
                                                               String.Format("WHERE (Productcode = '{0}')", ID);
                        OleDbCommand cmd = new OleDbCommand(commandString, conn);
                        conn.Open();
                        OleDbDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            // Setup DataReader
                            dr.Read();

                            // Set DR values to Text fields
                            ((Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text) = dr["Productname"].ToString();
                            ((Gridview1.Rows[i].Cells[1].FindControl("txtexpiredate") as TextBox).Text) = dr["Expiredate"].ToString();
                            //((Gridview1.Rows[i].Cells[1].FindControl("ddl_Batch") as TextBox).Text) = dr["Batchid"].ToString();


                        }
                        else
                        {

                        }

                        using (OleDbConnection conn10 = new OleDbConnection(strconn1))
                        {
                            conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

                            string productcode = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Text);

                            //DropDownList ddll = (DropDownList)(Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Text);

                            DropDownList ddll = (DropDownList)Gridview1.Rows[0].Cells[7].FindControl("ddl_Batch");

                            DataSet ds = new DataSet();
                            conn10.Open();

                            //string cmdstr = "Select Batchid from tblProductinward where productcode ="'+ productcode +'"";
                            string cmdstr = "Select  Batchid from tblProductinward  where productcode='" + productcode + "' GROUP BY Batchid ORDER BY Batchid*1 ASC";

                            OleDbCommand cmd10 = new OleDbCommand(cmdstr, conn10);

                            OleDbDataAdapter adp = new OleDbDataAdapter(cmd10);

                            adp.Fill(ds);

                            ddll.DataSource = ds.Tables[0];

                            ddll.DataTextField = "Batchid";

                            //ddlproductcode.DataValueField = "id";

                            ddll.DataBind();

                            // ddll.Items.Insert(0, new ListItem("--Select--", "0"));

                            conn.Close();

                        }



                        (Gridview1.Rows[0].Cells[1].FindControl("txtquantity") as TextBox).Focus();




                    }
                }
            }


            if (!File.Exists(filename))
            {
                TextBox txt30 = (TextBox)sender;
                GridViewRow row30 = (GridViewRow)txt30.NamingContainer;
                // Gridview1.Rows[row1.RowIndex].FindControl("txtdiscount").Focus();

                string productname30 = ((Gridview1.Rows[row30.RowIndex].FindControl("txtproductname") as TextBox).Text);

                DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblProductMaster", "Productname", productname30);
                string flag2 = dsgroup10.Tables[0].Rows[0]["Pharmflag"].ToString();
                if (flag2 == "Y")
                {










                    for (int i = 0; i < Gridview1.Rows.Count; i++)
                    {
                        string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("ddl_Batch") as DropDownList).Text);
                        string productname28 = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text);

                        TextBox txt35 = (TextBox)sender;
                        GridViewRow row35 = (GridViewRow)txt35.NamingContainer;
                        // Gridview1.Rows[row1.RowIndex].FindControl("txtdiscount").Focus();

                        string productname35 = ((Gridview1.Rows[row35.RowIndex].FindControl("txtproductname") as TextBox).Text);
                        using (SqlConnection conn = new SqlConnection(strconn))
                        {
                            //string commandString = "SELECT  SUM(Stockinhand) as Stockinhand  FROM tblProductinward where Batchid='" + ID + "'";

                            string commandString = "SELECT   Stockinhand  FROM tblProductinward where Batchid='" + ID + "' and ProductName='" + productname28 + "'";
                            //SqlConnection cnn = new SqlConnection(connectionString);
                            SqlCommand cmd = new SqlCommand(commandString, conn);
                            conn.Open();

                            // Execute SQL and get returned Reader
                            SqlDataReader dr = cmd.ExecuteReader();


                            // Test for values in DataReader
                            if (dr.HasRows)
                            {
                                // Setup DataReader
                                dr.Read();

                                // Set DR values to Text fields
                                //((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text) = dr["Productcode"].ToString();
                               // ((Gridview1.Rows[i].Cells[1].FindControl("txtStockinhand") as TextBox).Text) = dr["Stockinhand"].ToString();
                                ((Gridview1.Rows[row35.RowIndex].FindControl("txtStockinhand") as TextBox).Text) = dr["Stockinhand"].ToString();

                                // ((Gridview1.Rows[i].Cells[1].FindControl("txtdiscount") as TextBox).Text) = "0";




                            }



                            else
                            {
                                // Do something if no user is found
                                // OR do nothing
                            }

                            // Close connections
                            dr.Close();
                            conn.Close();
                        }

                    }






                    for (int k = 0; k < Gridview1.Rows.Count; k++)
                    {

                        string productcode = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductcode") as TextBox).Text);
                        string productname = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtproductname") as TextBox).Text);
                        string Batch = Convert.ToString((Gridview1.Rows[k].Cells[3].FindControl("ddl_Batch") as DropDownList).Text);

                        using (SqlConnection conn = new SqlConnection(strconn))
                        {
                            //conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

                            //string commandString = "SELECT ProductName,Productcode,Sellprice,Batchid FROM tblProductinward " +
                            //String.Format("WHERE (Batchid = '{0}')", ID);
                            string commandString = "SELECT ProductName,Productcode,Sellprice,MRP,Batchid FROM tblProductinward WHERE Productcode = " + productcode + " AND productname = '" + productname + "' AND Batchid = '" + Batch + "'";
                            //SqlConnection cnn = new SqlConnection(connectionString);
                            SqlCommand cmd = new SqlCommand(commandString, conn);
                            conn.Open();

                            // Execute SQL and get returned Reader
                            SqlDataReader dr = cmd.ExecuteReader();


                            // Test for values in DataReader
                            if (dr.HasRows)
                            {
                                // Setup DataReader
                                dr.Read();

                                // Set DR values to Text fields
                                //((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text) = dr["Productcode"].ToString();
                                double tax1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txttax") as TextBox).Text);
                                string mrp = dr["MRP"].ToString();
                                Double mrp1 = Convert.ToDouble(mrp);

                                Double amt = (mrp1 * tax1) / (100 + tax1);
                                Double fnamt = mrp1 - amt;
                                double fnamt10 = Math.Round(fnamt, 2);

                                ((Gridview1.Rows[k].Cells[1].FindControl("txtrate") as TextBox).Text) = Convert.ToString(fnamt10);


                            }







                            else
                            {
                                // Do something if no user is found
                                // OR do nothing
                            }

                            // Close connections
                            dr.Close();
                            conn.Close();
                        }







                        string productname1 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductname") as TextBox).Text);
                        SqlConnection con58 = new SqlConnection(strconn1);
                        SqlCommand cmd58 = new SqlCommand("select sum(Stockinhand) as Stockinhand  from tblProductinward where  ProductName = '" + productname1 + "' and Batchid = '" + Batch + "'", con58);
                        SqlDataAdapter da58 = new SqlDataAdapter(cmd58);
                        DataSet ds58 = new DataSet();

                        da58.Fill(ds58);
                        string stock = ds58.Tables[0].Rows[0]["Stockinhand"].ToString();

                        // int stock = Convert.ToInt32(ds58.Tables[0].Rows[0]["Stockinhand"].ToString());


                        //txtstock.Text = Convert.ToString(stock);
                        if (stock == "")
                        {
                            ShowPopupMessage("Pls chosse different Product Name", PopupMessageType.txtproductname);
                            (Gridview1.Rows[0].Cells[1].FindControl("txtproductname") as TextBox).Focus();
                            return;
                        }


                        string productname12 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductname") as TextBox).Text);
                        SqlConnection con60 = new SqlConnection(strconn1);
                        SqlCommand cmd60 = new SqlCommand("select sum(Stockinhand) as Stockinhand  from tblProductinward where   ProductName = '" + productname12 + "'", con58);
                        SqlDataAdapter da60 = new SqlDataAdapter(cmd60);
                        DataSet ds60 = new DataSet();

                        da60.Fill(ds60);
                        string stock10 = ds60.Tables[0].Rows[0]["Stockinhand"].ToString();
                        int stock11 = Convert.ToInt32(stock10);







                        // txtstock.Text = totstock;

                        DataSet dsgrp16 = clsgd.GetcondDataSet("*", "tbltempprodsale", "ProductName", productname1);
                        if (dsgrp16.Tables[0].Rows.Count > 0)
                        {
                            string productcode24 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductname") as TextBox).Text);
                            SqlConnection con65 = new SqlConnection(strconn1);
                            SqlCommand cmd65 = new SqlCommand("select Stockinhand  from tbltempprodsale where ProductName = '" + productcode24 + "'", con58);
                            SqlDataAdapter da65 = new SqlDataAdapter(cmd65);
                            DataSet ds65 = new DataSet();

                            da65.Fill(ds65);
                            string stock15 = ds65.Tables[0].Rows[0]["Stockinhand"].ToString();
                            int stock18 = Convert.ToInt32(stock15);

                            string productcode2 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductname") as TextBox).Text);
                            SqlConnection con59 = new SqlConnection(strconn1);
                            SqlCommand cmd59 = new SqlCommand("select sum(Stockinhand) as Stockinhand   from  tblProductinward where  ProductName = '" + productcode2 + "'", con59);
                            SqlDataAdapter da59 = new SqlDataAdapter(cmd59);
                            DataSet ds59 = new DataSet();

                            da59.Fill(ds59);
                            string quantity = ds59.Tables[0].Rows[0]["Stockinhand"].ToString();
                            int quantity10 = Convert.ToInt32(quantity);

                            string totstock = Convert.ToString(quantity10 - stock18);
                            txtstock.Text = totstock;

                        }
                        else
                        {
                            txtstock.Text = stock10;

                        }

                    }
                }
                else
                {


                   
                   




 for (int i = 0; i < Gridview1.Rows.Count; i++)
                    {
                        string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("ddl_Batch") as DropDownList).Text);
                        string productname28 = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text);


                        TextBox txt38 = (TextBox)sender;
                        GridViewRow row38 = (GridViewRow)txt38.NamingContainer;
                        // Gridview1.Rows[row1.RowIndex].FindControl("txtdiscount").Focus();

                        string productname38 = ((Gridview1.Rows[row38.RowIndex].FindControl("txtproductname") as TextBox).Text);


                      DataSet dsgrp = clsgd.GetcondDataSet("*", "tbltempprodsale", "ProductName", productname30);
        if (dsgrp.Tables[0].Rows.Count > 0)
        {
            //Table2.Visible = true;
            ShowPopupMessage("Pls chosse different Product Name", PopupMessageType.txtproductname);
            (Gridview1.Rows[0].Cells[1].FindControl("txtproductname") as TextBox).Focus();
            return;
        }





                        using (SqlConnection conn = new SqlConnection(strconn))
                        {
                            //string commandString = "SELECT  SUM(Stockinhand) as Stockinhand  FROM tblProductinward where Batchid='" + ID + "'";

                            string commandString = "SELECT   sum(Stockinhand) as Stockinhand   FROM tblProductinward where  ProductName='" + productname28 + "'";
                            //SqlConnection cnn = new SqlConnection(connectionString);
                            SqlCommand cmd = new SqlCommand(commandString, conn);
                            conn.Open();

                            // Execute SQL and get returned Reader
                            SqlDataReader dr = cmd.ExecuteReader();


                            // Test for values in DataReader
                            if (dr.HasRows)
                            {
                                // Setup DataReader
                                dr.Read();

                                // Set DR values to Text fields
                                //((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text) = dr["Productcode"].ToString();
                               // ((Gridview1.Rows[i].Cells[1].FindControl("txtStockinhand") as TextBox).Text) = dr["Stockinhand"].ToString();

                                // ((Gridview1.Rows[i].Cells[1].FindControl("txtdiscount") as TextBox).Text) = "0";

                                ((Gridview1.Rows[row38.RowIndex].FindControl("txtStockinhand") as TextBox).Text) = dr["Stockinhand"].ToString();




                            }



                            else
                            {
                                // Do something if no user is found
                                // OR do nothing
                            }

                            // Close connections
                            dr.Close();
                            conn.Close();
                        }

                    }






                    for (int k = 0; k < Gridview1.Rows.Count; k++)
                    {

                        string productcode = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductcode") as TextBox).Text);
                        string productname = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtproductname") as TextBox).Text);
                        string Batch = Convert.ToString((Gridview1.Rows[k].Cells[3].FindControl("ddl_Batch") as DropDownList).Text);

                        using (SqlConnection conn = new SqlConnection(strconn))
                        {
                            //conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

                            //string commandString = "SELECT ProductName,Productcode,Sellprice,Batchid FROM tblProductinward " +
                            //String.Format("WHERE (Batchid = '{0}')", ID);
                            string commandString = "SELECT ProductName,Productcode,Sellprice,MRP,Batchid FROM tblProductinward WHERE Productcode = " + productcode + " AND productname = '" + productname + "' AND Batchid = '" + Batch + "'";
                            //SqlConnection cnn = new SqlConnection(connectionString);
                            SqlCommand cmd = new SqlCommand(commandString, conn);
                            conn.Open();

                            // Execute SQL and get returned Reader
                            SqlDataReader dr = cmd.ExecuteReader();


                            // Test for values in DataReader
                            if (dr.HasRows)
                            {
                                // Setup DataReader
                                dr.Read();

                                // Set DR values to Text fields
                                //((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text) = dr["Productcode"].ToString();
                                double tax1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txttax") as TextBox).Text);
                                string mrp = dr["MRP"].ToString();
                                Double mrp1 = Convert.ToDouble(mrp);

                                Double amt = (mrp1 * tax1) / (100 + tax1);
                                Double fnamt = mrp1 - amt;
                                double fnamt10 = Math.Round(fnamt, 2);

                                ((Gridview1.Rows[k].Cells[1].FindControl("txtrate") as TextBox).Text) = Convert.ToString(fnamt10);


                            }







                            else
                            {
                                // Do something if no user is found
                                // OR do nothing
                            }

                            // Close connections
                            dr.Close();
                            conn.Close();
                        }

                        string productname1 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductname") as TextBox).Text);
                        SqlConnection con58 = new SqlConnection(strconn1);
                        SqlCommand cmd58 = new SqlCommand("select sum(Stockinhand) as Stockinhand  from tblProductinward where  ProductName = '" + productname1 + "' and Batchid = '" + Batch + "'", con58);
                        SqlDataAdapter da58 = new SqlDataAdapter(cmd58);
                        DataSet ds58 = new DataSet();

                        da58.Fill(ds58);
                        string stock = ds58.Tables[0].Rows[0]["Stockinhand"].ToString();

                        // int stock = Convert.ToInt32(ds58.Tables[0].Rows[0]["Stockinhand"].ToString());


                        //txtstock.Text = Convert.ToString(stock);
                        if (stock == "")
                        {
                            ShowPopupMessage("Pls chosse different Product Name", PopupMessageType.txtproductname);
                            (Gridview1.Rows[0].Cells[1].FindControl("txtproductname") as TextBox).Focus();
                            return;
                        }


                        string productname12 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductname") as TextBox).Text);
                        SqlConnection con60 = new SqlConnection(strconn1);
                        SqlCommand cmd60 = new SqlCommand("select sum(Stockinhand) as Stockinhand  from tblProductinward where   ProductName = '" + productname12 + "'", con58);
                        SqlDataAdapter da60 = new SqlDataAdapter(cmd60);
                        DataSet ds60 = new DataSet();

                        da60.Fill(ds60);
                        string stock10 = ds60.Tables[0].Rows[0]["Stockinhand"].ToString();
                        int stock11 = Convert.ToInt32(stock10);







                        // txtstock.Text = totstock;

                        DataSet dsgrp16 = clsgd.GetcondDataSet("*", "tbltempprodsale", "ProductName", productname1);
                        if (dsgrp16.Tables[0].Rows.Count > 0)
                        {
                            string productcode24 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductname") as TextBox).Text);
                            SqlConnection con65 = new SqlConnection(strconn1);
                            SqlCommand cmd65 = new SqlCommand("select Stockinhand  from tbltempprodsale where ProductName = '" + productcode24 + "'", con58);
                            SqlDataAdapter da65 = new SqlDataAdapter(cmd65);
                            DataSet ds65 = new DataSet();

                            da65.Fill(ds65);
                            string stock15 = ds65.Tables[0].Rows[0]["Stockinhand"].ToString();
                            int stock18 = Convert.ToInt32(stock15);

                            string productcode2 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductname") as TextBox).Text);
                            SqlConnection con59 = new SqlConnection(strconn1);
                            SqlCommand cmd59 = new SqlCommand("select sum(Stockinhand) as Stockinhand   from tblProductinward where  ProductName = '" + productcode2 + "'", con59);
                            SqlDataAdapter da59 = new SqlDataAdapter(cmd59);
                            DataSet ds59 = new DataSet();

                            da59.Fill(ds59);
                            string quantity = ds59.Tables[0].Rows[0]["Stockinhand"].ToString();
                            int quantity10 = Convert.ToInt32(quantity);

                            string totstock = Convert.ToString(quantity10 - stock18);
                            txtstock.Text = totstock;

                        }
                        else
                        {
                            txtstock.Text = stock10;

                        }



                    }







                }
            }
            else
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtbatchno") as TextBox).Text);
                    //OleDbCommand com;
                    string strconn1 = Dbconn.conmenthod();

                    using (OleDbConnection conn = new OleDbConnection(strconn1))
                    {



                        //conn.Open();
                        string commandString = "SELECT ProductName,Productcode,Sellprice,Batchid FROM tblProductinward " +
                                                                 String.Format("WHERE (Batchid = '{0}')", ID);
                        OleDbCommand cmd = new OleDbCommand(commandString, conn);
                        conn.Open();
                        OleDbDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            // Setup DataReader
                            dr.Read();

                            // Set DR values to Text fields
                            ((Gridview1.Rows[i].Cells[1].FindControl("txtrate") as TextBox).Text) = dr["Sellprice"].ToString();


                        }
                        else
                        {
                            // Do something if no user is found
                            // OR do nothing
                        }
                    }
                }
            }

            if (!File.Exists(filename))
            {








            }

            else
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtbatchno") as TextBox).Text);
                    //OleDbCommand com;
                    string strconn1 = Dbconn.conmenthod();

                    using (OleDbConnection conn = new OleDbConnection(strconn1))
                    {



                        //conn.Open();
                        string commandString = "SELECT Tax,Batchid,MRP FROM tblProductinward " +
                                                                  String.Format("WHERE (Batchid  = '{0}')", ID);
                        OleDbCommand cmd = new OleDbCommand(commandString, conn);
                        conn.Open();
                        OleDbDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            // Setup DataReader
                            dr.Read();

                            // Set DR values to Text fields
                            ((Gridview1.Rows[i].Cells[1].FindControl("txttax") as TextBox).Text) = dr["Tax"].ToString();

                            double mrp = Convert.ToDouble(dr["MRP"]);

                            int rowIndex = 0;
                            //StringCollection sc = new StringCollection();
                            if (ViewState["CurrentTable"] != null)
                            {
                                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                                DataRow drCurrentRow = null;
                                if (dtCurrentTable.Rows.Count > 0)
                                {
                                    for (int j = 1; j <= dtCurrentTable.Rows.Count; j++)
                                    {
                                        TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");
                                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtquantity");
                                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                                        //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                                        TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtbatchno");
                                        TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtrate");
                                        TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtdiscount");
                                        TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                                        TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtproamount");


                                        double quantity = Convert.ToDouble(box2.Text);

                                        double rate = Convert.ToDouble(box5.Text);

                                        double tax = Convert.ToDouble(box7.Text);

                                        double taxrate = (mrp * tax) / (100 + tax);

                                        double productamount = (quantity * rate) + taxrate;
                                        double productamount1 = Math.Round(productamount, 2);
                                        string productamount2 = Convert.ToString(productamount1);

                                        ((Gridview1.Rows[i].Cells[1].FindControl("txtproamount") as TextBox).Text) = Convert.ToString(productamount2);

                                        double selprice = Convert.ToDouble(mrp - taxrate);

                                        double rselprice = Math.Round(selprice, 2);
                                        string selprice1 = Convert.ToString(rselprice);
                                        System.DateTime Dtnow = DateTime.Now;
                                        string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                                        String strconn11 = Dbconn.conmenthod();

                                        Double sum = 0;
                                        Double add = 0.0;

                                        for (int k = 0; k < Gridview1.Rows.Count; k++)
                                        {

                                            if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text))
                                            {
                                                add = 0.0;

                                            }
                                            else
                                            {

                                                add = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text);
                                                sum = sum + add;
                                            }


                                            txtpramount.Text = (sum).ToString();


                                            if (txttotalamount.Text == "")
                                            {
                                                txttotalamount.Text = (sum).ToString();
                                            }
                                            else
                                            {
                                                double totamount = Convert.ToDouble(txttotalamount.Text);
                                                txttotalamount.Text = (sum - totamount).ToString();
                                            }

                                        }




                                    }


                                    //(Gridview1.Rows[0].Cells[1].FindControl("txtbatchno") as TextBox).Focus();
                                }

                            }


                        }
                        else
                        {
                            // Do something if no user is found
                            // OR do nothing
                        }
                    }
                }
            }

            TextBox txt20 = (TextBox)sender;
            GridViewRow row20 = (GridViewRow)txt20.NamingContainer;
            // Gridview1.Rows[row1.RowIndex].FindControl("txtdiscount").Focus();

            ((Gridview1.Rows[row20.RowIndex].FindControl("txtdiscount") as TextBox).Text) = "0";

            // (Gridview1.Rows[0].Cells[1].FindControl("txtquantity") as TextBox).Focus();
            //TextBox txt = (TextBox)sender;
            // GridViewRow row = (GridViewRow)txt.NamingContainer;
            Gridview1.Rows[row.RowIndex].FindControl("txtquantity").Focus();
        }
    
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
       


    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Buyername(string prefixText)
    {
        string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            // string oConn = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(strconn1);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Productname from tblProductMaster where Productname like @1+'%'", conn);
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
            // string strconn1 = Dbconn.conmenthod();
            OleDbConnection conn = new OleDbConnection(strconn1);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select Productname from tblProductMaster where Productname like @1+'%'", conn);
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
      
   protected void btnExit_Click(object sender, EventArgs e)
    {
        SqlConnection con58 = new SqlConnection(strconn1);
        con58.Open();
        SqlCommand cmd58 = new SqlCommand("delete FROM tbltempprodsale where LoginName = '" + Session["username"] + "'", con58);
        cmd58.ExecuteNonQuery();
        Response.Redirect("Home.aspx");
    }

    protected void Gridview1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].BackColor = System.Drawing.Color.White;


              //  DropDownList ddll = (DropDownList)e.Row.FindControl("ddlproductcode");



                //ddll.Items.Insert(0, new ListItem("--Select--", "0"));


                conn.Close();

            }
        }

      


    }

    protected void txtinvoicedate_TextChanged(object sender, EventArgs e)
    {
        //txtinvoiceamount.Focus();

    }
    protected void txtinvoiceamount_TextChanged(object sender, EventArgs e)
    {
        (Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Focus();
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
            SqlCommand cmd = new SqlCommand("select distinct Productcode from tblProductinward where Stockinhand>0 and Productcode like @1+'%'", conn);
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
            // string strconn1 = Dbconn.conmenthod();
            string strconn1 = Dbconn.conmenthod();
            OleDbConnection conn = new OleDbConnection(strconn1);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select distinct Productcode from tblProductinward where Stockinhand>0 and Productcode like @1+'%'", conn);
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
    public static List<string> Buyername2(string prefixText)
    {
        string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            string oConn = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(oConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select distinct ProductName from tblProductinward where Stockinhand>0 and ProductName like @1+'%'", conn);
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
            // string strconn1 = Dbconn.conmenthod();
            string strconn1 = Dbconn.conmenthod();
            OleDbConnection conn = new OleDbConnection(strconn1);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select distinct ProductName from tblProductinward where Stockinhand>0 and ProductName like @1+'%'", conn);
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
    public static List<string> Customercode(string prefixText)
    {


         string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            //string oConn = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(strconn1);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select CA_code from tblCustomer where CA_code like @1+'%'", conn);
            cmd.Parameters.AddWithValue("@1", prefixText);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<string> CA_code = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CA_code.Add(dt.Rows[i][0].ToString());
            }
            return CA_code;
        }
        else
        {
            //string strconn1 = Dbconn.conmenthod();
            OleDbConnection conn = new OleDbConnection(strconn1);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select CA_code from tblCustomer where CA_code like @1+'%'", conn);
            cmd.Parameters.AddWithValue("@1", prefixText);
            OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            List<string> CA_code = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CA_code.Add(dt.Rows[i][0].ToString());
            }

            return CA_code;
        }


    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Customername(string prefixText)
    {

        string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {

            //string oConn = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(strconn1);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select CA_name from tblCustomer where CA_name like @1+'%'", conn);
            cmd.Parameters.AddWithValue("@1", prefixText);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<string> CA_name = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CA_name.Add(dt.Rows[i][0].ToString());
            }

            return CA_name;
        }

        else
        {
            //string strconn1 = Dbconn.conmenthod();
            OleDbConnection conn = new OleDbConnection(strconn1);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select CA_name from tblCustomer where CA_name like @1+'%'", conn);
            cmd.Parameters.AddWithValue("@1", prefixText);
            OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            List<string> CA_name = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CA_name.Add(dt.Rows[i][0].ToString());
            }
            return CA_name;
        }


    }
   

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Buyername3(string prefixText)
    {
        string filename = Dbconn.Mymenthod();
        prefixText = "Dr." + prefixText;
        if (!File.Exists(filename))
        {
            string oConn = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(oConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select D_name,D_code from tblDoctor where D_name like @1+'%'", conn);
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
            // string strconn1 = Dbconn.conmenthod();
            string strconn1 = Dbconn.conmenthod();
            OleDbConnection conn = new OleDbConnection(strconn1);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select D_name from tblDoctor where D_name  like +@prefixText+ '%'", conn);
            cmd.Parameters.AddWithValue("@prefixText", prefixText);
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



    protected void txtcustomercode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //txtcustcode.BackColor = Color.LightBlue; 
            string CA_code = txtcustomercode.Text;
            // DataSet ds1=ClsBLGD.GetcondDataSet("*","tblCustomer","CA_code",cuscd);
            DataSet ds2 = clsgd.GetcondDataSet("*", "tblCustomer", "CA_code", CA_code);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                string cunm = ds2.Tables[0].Rows[0]["CA_name"].ToString();
                txtcustname.Text = cunm;

               /* SqlConnection con505 = new SqlConnection(strconn1);
                SqlCommand cmd505 = new SqlCommand("select * from tblCustomer where CA_code='" + txtcustomercode.Text + "'", con505);
                SqlDataAdapter da505 = new SqlDataAdapter(cmd505);
                DataSet ds505 = new DataSet();

                da505.Fill(ds505);
                double amount = Convert.ToDouble(txtamount.Text);
                double credit = Convert.ToDouble(ds505.Tables[0].Rows[0]["Credit_used"].ToString());
                double creditamount = amount + credit;
                double creditlimit = Convert.ToDouble(ds505.Tables[0].Rows[0]["Credit_limit"].ToString());

                if (creditamount > creditlimit)
                {
                    Master.ShowModal("Credit Limit Exceeded for this Customer", "txtcustomercode", 1);
                    txtcustomercode.Text = string.Empty;
                    return;

                }*/


                SqlConnection con50 = new SqlConnection(strconn1);
                SqlCommand cmd50 = new SqlCommand("select Credit_amount as Credit_amount from tblCustomer where CA_code='" + txtcustomercode.Text + "'", con50);
                SqlDataAdapter da50 = new SqlDataAdapter(cmd50);
                DataSet ds50 = new DataSet();

                da50.Fill(ds50);

                if (ds50.Tables[0].Rows.Count > 0)
                {
                    if (ds50.Tables[0].Rows[0].IsNull("Credit_amount"))
                    {
                        g = 0;
                    }
                    else
                    {
                        g = Convert.ToDouble(ds50.Tables[0].Rows[0]["Credit_amount"].ToString());
                    }
                }

                if (g > 0)
                {
                    txtbal.BackColor = System.Drawing.Color.Green;
                    txtbal.Text = Convert.ToString(g);
                    string balance = txtbal.Text;
                    // txtcredit.Text = "0";


                }
                else
                {
                    SqlConnection con30 = new SqlConnection(strconn1);
                    SqlCommand cmd30 = new SqlCommand("select Credit_limit as Credit_limit from tblCustomer where CA_code='" + txtcustomercode.Text + "'", con30);
                    SqlDataAdapter da30 = new SqlDataAdapter(cmd30);
                    DataSet ds30 = new DataSet();

                    da30.Fill(ds30);

                    if (ds30.Tables[0].Rows.Count > 0)
                    {
                        if (ds30.Tables[0].Rows[0].IsNull("Credit_limit"))
                        {
                            a1 = 0;
                        }
                        else
                        {
                            a1 = Convert.ToDouble(ds30.Tables[0].Rows[0]["Credit_limit"].ToString());


                            SqlConnection con25 = new SqlConnection(strconn1);
                            SqlCommand cmd15 = new SqlCommand("select Credit_used as Credit_used from tblCustomer where CA_code='" + txtcustomercode.Text + "'", con25);
                            SqlDataAdapter da15 = new SqlDataAdapter(cmd15);
                            DataSet ds15 = new DataSet();

                            da15.Fill(ds15);

                            if (ds15.Tables[0].Rows.Count > 0)
                            {
                                if (ds15.Tables[0].Rows[0].IsNull("Credit_used"))
                                {
                                    m = 0;
                                }
                                else
                                {
                                    m = Convert.ToDouble(ds15.Tables[0].Rows[0]["Credit_used"].ToString());
                                }

                                string bal = Convert.ToString(a1 - m);
                                txtbal.BackColor = System.Drawing.Color.Yellow;
                                txtbal.Text = Convert.ToString(bal);
                                string balance = txtbal.Text;



                            }

                        }
                    }
                }

            }


            else
            {

                Master.ShowModal("Customer code does not exist", "txtcustomercode", 1);
                txtcustomercode.Text = string.Empty;
                return;


            }
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
        SqlConnection con505 = new SqlConnection(strconn1);
               SqlCommand cmd505 = new SqlCommand("select * from tblCustomer where CA_code='" + txtcustomercode.Text + "'", con505);
               SqlDataAdapter da505 = new SqlDataAdapter(cmd505);
               DataSet ds505 = new DataSet();

               da505.Fill(ds505);
               double amount = Convert.ToDouble(txtamount.Text);
               double credit = Convert.ToDouble(ds505.Tables[0].Rows[0]["Credit_used"].ToString());
               double creditamount = amount + credit;
               double creditlimit = Convert.ToDouble(ds505.Tables[0].Rows[0]["Credit_limit"].ToString());

               if (creditamount > creditlimit)
               {
                   Master.ShowModal("Credit Limit Exceeded for this Customer", "txtcustomercode", 1);
                   txtcustomercode.Text = string.Empty;
                   return;

               }
        double balamount = Convert.ToDouble(txtbal.Text);
        if (balamount <= 0)
        {
            Master.ShowModal("Sale not possible if balance is zero", "txtcustomercode", 1);
            txtcustomercode.Text = string.Empty;
            return;

        }

        else
        {
            btnsave.Focus();
        }




      

       
        
    }
    protected void txtcustname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //txtcustcode.BackColor = Color.LightBlue; 
            string CA_name = txtcustname.Text;
            // DataSet ds1=ClsBLGD.GetcondDataSet("*","tblCustomer","CA_code",cuscd);
            DataSet ds2 = clsgd.GetcondDataSet("*", "tblCustomer", "CA_name", CA_name);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                string cunm = ds2.Tables[0].Rows[0]["CA_code"].ToString();
                txtcustomercode.Text = cunm;

              /*  SqlConnection con505 = new SqlConnection(strconn1);
                SqlCommand cmd505 = new SqlCommand("select * from tblCustomer where CA_code='" + txtcustomercode.Text + "'", con505);
                SqlDataAdapter da505 = new SqlDataAdapter(cmd505);
                DataSet ds505 = new DataSet();

                da505.Fill(ds505);
                double amount = Convert.ToDouble(txtamount.Text);
                double credit = Convert.ToDouble(ds505.Tables[0].Rows[0]["Credit_used"].ToString());
                double creditamount = amount + credit;
                double creditlimit = Convert.ToDouble(ds505.Tables[0].Rows[0]["Credit_limit"].ToString());

                if (creditamount > creditlimit)
                {
                    Master.ShowModal("Credit Limit Exceeded for this Customer", "txtcustomercode", 1);
                    txtcustomercode.Text = string.Empty;
                    return;

                }*/

                SqlConnection con50 = new SqlConnection(strconn1);
                SqlCommand cmd50 = new SqlCommand("select Credit_amount as Credit_amount from tblCustomer where CA_name='" + txtcustname.Text + "'", con50);
                SqlDataAdapter da50 = new SqlDataAdapter(cmd50);
                DataSet ds50 = new DataSet();

                da50.Fill(ds50);

                if (ds50.Tables[0].Rows.Count > 0)
                {
                    if (ds50.Tables[0].Rows[0].IsNull("Credit_amount"))
                    {
                        g = 0;
                    }
                    else
                    {
                        g = Convert.ToDouble(ds50.Tables[0].Rows[0]["Credit_amount"].ToString());
                    }
                }

                if (g > 0)
                {
                    txtbal.BackColor = System.Drawing.Color.Green;
                    txtbal.Text = Convert.ToString(g);
                    string balance = txtbal.Text;
                    // txtcredit.Text = "0";


                }
                else
                {
                    SqlConnection con30 = new SqlConnection(strconn1);
                    SqlCommand cmd30 = new SqlCommand("select Credit_limit as Credit_limit from tblCustomer where CA_name='" + txtcustname.Text + "'", con30);
                    SqlDataAdapter da30 = new SqlDataAdapter(cmd30);
                    DataSet ds30 = new DataSet();

                    da30.Fill(ds30);

                    if (ds30.Tables[0].Rows.Count > 0)
                    {
                        if (ds30.Tables[0].Rows[0].IsNull("Credit_limit"))
                        {
                            a1 = 0;
                        }
                        else
                        {
                            a1 = Convert.ToDouble(ds30.Tables[0].Rows[0]["Credit_limit"].ToString());


                            SqlConnection con25 = new SqlConnection(strconn1);
                            SqlCommand cmd15 = new SqlCommand("select Credit_used as Credit_used from tblCustomer where CA_name='" + txtcustname.Text + "'", con25);
                            SqlDataAdapter da15 = new SqlDataAdapter(cmd15);
                            DataSet ds15 = new DataSet();

                            da15.Fill(ds15);

                            if (ds15.Tables[0].Rows.Count > 0)
                            {
                                if (ds15.Tables[0].Rows[0].IsNull("Credit_used"))
                                {
                                    m = 0;
                                }
                                else
                                {
                                    m = Convert.ToDouble(ds15.Tables[0].Rows[0]["Credit_used"].ToString());
                                }

                                string bal = Convert.ToString(a1 - m);
                                txtbal.BackColor = System.Drawing.Color.Yellow;
                                txtbal.Text = Convert.ToString(bal);
                                string balance = txtbal.Text;



                            }

                        }
                    }
                }

            }


            else
            {

                Master.ShowModal("Customer name does not exist", "txtcustname", 1);
                txtcustname.Text = string.Empty;
                txtcustomercode.Text = string.Empty;
                return;


            }
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }

       // btnsave.Focus();
          SqlConnection con505 = new SqlConnection(strconn1);
              SqlCommand cmd505 = new SqlCommand("select * from tblCustomer where CA_code='" + txtcustomercode.Text + "'", con505);
              SqlDataAdapter da505 = new SqlDataAdapter(cmd505);
              DataSet ds505 = new DataSet();

              da505.Fill(ds505);
              double amount = Convert.ToDouble(txtamount.Text);
              double credit = Convert.ToDouble(ds505.Tables[0].Rows[0]["Credit_used"].ToString());
              double creditamount = amount + credit;
              double creditlimit = Convert.ToDouble(ds505.Tables[0].Rows[0]["Credit_limit"].ToString());

              if (creditamount > creditlimit)
              {
                  Master.ShowModal("Credit Limit Exceeded for this Customer", "txtcustomercode", 1);
                  txtcustomercode.Text = string.Empty;
                  return;

              }

        double balamount = Convert.ToDouble(txtbal.Text);
        if (balamount <= 0)
        {
            Master.ShowModal("Sale not possible if balance is zero", "txtcustomercode", 1);
            txtcustomercode.Text = string.Empty;
            return;

        }
        else
        {
            btnsave.Focus();
        }
        
    }


   


    

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Buyername6(string prefixText)
    {
        string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            string oConn = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(oConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Tax from tblProductinward where Stockinhand>0 and Tax like @1+'%'", conn);
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
            // string strconn1 = Dbconn.conmenthod();
            string strconn1 = Dbconn.conmenthod();
            OleDbConnection conn = new OleDbConnection(strconn1);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select Tax from tblProductinward where Stockinhand>0 and Batchid like @1+'%'", conn);
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

    protected void txtothers_TextChanged(object sender, EventArgs e)
    {
        

    }

    public void payment()
    {
        DataSet dsgroup = clsgd.GetDataSet("distinct Saletype", "tblSaletype");
        for (int i = 0; i < dsgroup.Tables[0].Rows.Count; i++)
        {
            DataSet dsgroup1 = clsgd.GetcondDataSet("*", "tblSaletype", "Saletype", dsgroup.Tables[0].Rows[i]["Saletype"].ToString());
            arryname.Add(dsgroup1.Tables[0].Rows[0]["Saletype"].ToString());


        }

        arryname.Sort();
        arryno.Add("CASH");
        //arryno.Add("Add New");
        for (int i = 0; i < arryname.Count; i++)
        {
            arryno.Add(arryname[i].ToString());
        }
        ddpaymenttype.DataSource = arryno;
        ddpaymenttype.DataBind();
        //ddGecode.Focus();

    }

    public void cardtype()
    {
        DataSet dsgroup20 = clsgd.GetDataSet("distinct Saletype", "tblSalecardtype");
        for (int i = 0; i < dsgroup20.Tables[0].Rows.Count; i++)
        {
            DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblSalecardtype", "Saletype", dsgroup20.Tables[0].Rows[i]["Saletype"].ToString());
            arryname20.Add(dsgroup10.Tables[0].Rows[0]["Saletype"].ToString());


        }

        arryname20.Sort();
        arryno20.Add("Select");
        //arryno.Add("Add New");
        for (int i = 0; i < arryname20.Count; i++)
        {
            arryno20.Add(arryname20[i].ToString());
        }
        ddlpaytype.DataSource = arryno20;
        ddlpaytype.DataBind();
        //ddGecode.Focus();

    }


    public void autoincrement()
    {
        try
        {
            if (!File.Exists(filename))
            {
                SqlConnection con = new SqlConnection(strconn1);
                con.Open();
                SqlCommand cmd = new SqlCommand("select Max(Invoiceno) as STransno from tblProductsale", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    headcode = ds.Tables[0].Rows[0]["STransno"].ToString();
                    if (headcode == "")
                    {
                        hedcd = 0;
                        codecode = Convert.ToString(hedcd);
                       // txtinvoicenor.Text = codecode;
                    }
                    else
                    {
                        // hedcd = Convert.ToInt32(headcode);
                        codecode = Convert.ToString(hedcd);
                       // lbltrno.Text = codecode;
                      //  txtinvoicenor.Text = codecode;

                    }
                }
            }
            else
            {
                OleDbConnection conn = new OleDbConnection(strconn1);
                conn.Open();
                OleDbCommand cmd1 = new OleDbCommand("select Max(STransno) as STransno from tblProductsale", conn);
                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    headcode = ds1.Tables[0].Rows[0]["STransno"].ToString();
                    //Int32 headcode = Convert.ToInt32(ds1.Tables[0].Rows[0]["Headercode"].ToString());
                    if (headcode == "")
                    {
                        hedcd = 0;
                    }
                    else
                    {
                        hedcd = Convert.ToInt32(headcode);
                    }
                    if (hedcd == 0 && hedcd < 9999)
                    {
                        if (hedcd == 9000 && hedcd < 9999)
                        {
                            count = Convert.ToInt16(cmd1.ExecuteScalar()) + 1;
                            hedcd = count;
                        }
                        else
                        {
                            hedcd = 001;
                            codecode = Convert.ToString(hedcd);
                           // lbltrno.Text = codecode;
                           // txtinvoicenor.Text = codecode;
                            lblvbillno.Text = codecode;
                        }
                    }
                    else
                    {
                        count = Convert.ToInt16(cmd1.ExecuteScalar()) + 1;
                        hedcd = count;
                        // hedcd="000"+count;
                        codecode = Convert.ToString(hedcd);
                       // lbltrno.Text = codecode;
                        //txtinvoicenor.Text = codecode;
                        lblvbillno.Text = codecode;
                    }

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

    public void cal()
    {
        SqlConnection con = new SqlConnection(strconn1);
                    SqlCommand cmd1 = new SqlCommand("select max(STransno) as STransno from tblProductsale", con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();

                    da1.Fill(ds1);

                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0].IsNull("STransno"))
                        {
                            g = 0;
                           // txtinvoicenor.Text=g.ToString();
                        }
                        else
                        {
                            g = 0;
                            // txtinvoicenor.Text=g.ToString();
                        }
                    }
                    else
                    {
                        g = 0;
                    }
                }

            
                  
                  

              


           
       

  



    public string maxno()
    {
        if (!File.Exists(filename))
        {
            try
            {


                SqlConnection con = new SqlConnection(strconn1);
                SqlCommand cmd1 = new SqlCommand("select max(Columnno) as Columnno from tblCustomeraccount where CA_code='" + txtcustomercode.Text + "'", con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    if (ds1.Tables[0].Rows[0].IsNull("Columnno"))
                    {
                        h = 1;
                    }
                    else
                    {
                        g = Convert.ToInt32(ds1.Tables[0].Rows[0]["Columnno"].ToString());
                        h = g + 1;
                    }
                    g1 = Convert.ToString(h);
                }
            }
            catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Visible = true;
                lblerror.Text = asd;
            } return g1;
        }
        else
        {
            try
            {
                OleDbConnection con = new OleDbConnection(strconn1);
                OleDbCommand cmd1 = new OleDbCommand("select max(Columnno) as Columnno from tblCustomeraccount where CA_code='" + txtcustomercode.Text + "'", con);
                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    if (ds1.Tables[0].Rows[0].IsNull("Columnno"))
                    {
                        h = 1;
                    }
                    else
                    {
                        g = Convert.ToInt32(ds1.Tables[0].Rows[0]["Columnno"].ToString());
                        h = g + 1;
                    }
                    g1 = Convert.ToString(h);
                }
            }
            catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Visible = true;
                lblerror.Text = asd;
            } return g1;
        }
    }


  
    

   protected void txtdoctorname_TextChanged(object sender, EventArgs e)
    {
        txtpatientname.Focus();
    }
    protected void txtpatientname_TextChanged(object sender, EventArgs e)
    {
        (Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Focus();
    }

    protected void txtquantity_TextChanged(object sender, EventArgs e)
    {
        try
        {

            if (!File.Exists(filename))
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text);
                    using (SqlConnection conn = new SqlConnection(strconn))
                    {
                        //conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

                        string commandString = "SELECT Tax,Batchid,MRP,Productcode,Productname FROM tblProductinward " +
                                                                  String.Format("WHERE (Productname  = '{0}')", ID);
                        //SqlConnection cnn = new SqlConnection(connectionString);
                        SqlCommand cmd = new SqlCommand(commandString, conn);
                        conn.Open();

                        // Execute SQL and get returned Reader
                        SqlDataReader dr = cmd.ExecuteReader();


                        // Test for values in DataReader
                        if (dr.HasRows)
                        {
                            // Setup DataReader
                            dr.Read();
                            double mrp = Convert.ToDouble(dr["MRP"]);



                            int rowIndex = 0;
                            //StringCollection sc = new StringCollection();
                            if (ViewState["CurrentTable"] != null)
                            {
                                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                                DataRow drCurrentRow = null;
                                if (dtCurrentTable.Rows.Count > 0)
                                {
                                    for (int j = 0; j < dtCurrentTable.Rows.Count; j++)
                                    {
                                        //TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                                        //TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");
                                        //TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtquantity");
                                        //TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                                        ////float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                                        //TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtbatchno");
                                        //TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtrate");
                                        //TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtdiscount");
                                        //TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                                        //TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtproamount");
                                        //TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txttaxamount");

                                        TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");
                                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtquantity");
                                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                                        //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                                        DropDownList box4 = (DropDownList)Gridview1.Rows[rowIndex].Cells[5].FindControl("ddl_Batch");
                                        TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtStockinhand");
                                        TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txtrate");
                                        TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtdiscount");
                                        TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("txttax");
                                        TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproamount");


                                        string disccount = Convert.ToString(box7.Text);

                                        if (disccount == "")
                                        {

                                            Master.ShowModal("Enter Zero or discount amount", "txtdiscount", 0);
                                            return;

                                        }

                                        double stock = Convert.ToDouble(box5.Text);
                                        double quantity10 = Convert.ToDouble(box2.Text);

                                        string quantity2 = Convert.ToString(box2.Text);

                                        if (quantity2 == "0")
                                        {
                                            Master.ShowModal("Dont enter zero quantity . !!!", "txtquantity", 1);
                                            return;

                                        }

                                        if (stock < quantity10)
                                        {
                                           // Master.ShowModal("It should be equal to stock . !!!", "txtquantity", 1);
                                            ShowPopupMessage("It should be equal to stock . !!!", PopupMessageType.txtquantity);
                                           // (Gridview1.Rows[i].Cells[1].FindControl("txtquantity") as TextBox).Text = string.Empty;

                                            TextBox txt10 = (TextBox)sender;
                                            GridViewRow row10 = (GridViewRow)txt10.NamingContainer;

                                           // (Gridview1.Rows[row10.RowIndex].Cells[1].FindControl("txtquantity") as TextBox).Focus();

                                            (Gridview1.Rows[row10.RowIndex].FindControl("txtquantity") as TextBox).Text = string.Empty;
                                           
                                            return;

                                        }







                                        Double quantity = 0.0;
                                        Double rate = 0.0;
                                        Double tax = 0.0;
                                        Double disc = 0.0;


                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtquantity") as TextBox).Text))
                                        {
                                            quantity = 0.0;
                                        }
                                        else
                                        {

                                            quantity = Convert.ToDouble(box2.Text);
                                        }


                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtrate") as TextBox).Text))
                                        {
                                            rate = 0.0;
                                        }
                                        else
                                        {

                                            rate = Convert.ToDouble(box6.Text);
                                        }


                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txttax") as TextBox).Text))
                                        {
                                            tax = 0.0;
                                        }
                                        else
                                        {

                                            tax = Convert.ToDouble(box8.Text);
                                        }


                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtdiscount") as TextBox).Text))
                                        {
                                            disc = 0.0;
                                        }
                                        else
                                        {

                                            disc = Convert.ToDouble(box7.Text);
                                        }



                                        double productamount12 = 0.0;


                                        if (rate == 0.0)
                                        {
                                            double productamount = 0;

                                        }
                                        else
                                        {


                                            // double ratetax = quantity * taxrate;
                                            if (disc == '0')
                                            {
                                                double productamount = (quantity * rate);
                                            }
                                            else
                                            {
                                                //double taxrate = (tax * rate) / (100 + tax);
                                                //double ratetax = quantity * taxrate;
                                                //double fnratetax= Math.Round(ratetax, 2);
                                                double disc10 = (rate * disc) / 100;

                                                double amt10 = rate - disc10;
                                                double amt15 = (amt10 * tax) / 100;
                                                double taxrate = (amt15 * quantity);
                                                double taxrate2 = Math.Round(taxrate, 2);

                                                double productamount = (quantity * rate);
                                                Double sumdiscount = productamount * disc / 100;
                                                double proamount1 = ((productamount) - sumdiscount + taxrate);
                                                double productamount1 = Math.Round(proamount1, 2);
                                                string productamount2 = Convert.ToString(productamount1);

                                                double taxamount10 = rate - disc10;
                                                double taxamount15 = taxamount10 * quantity;


                                                ((Gridview1.Rows[rowIndex].Cells[1].FindControl("txttaxrate") as TextBox).Text) = Convert.ToString(taxrate2);
                                                ((Gridview1.Rows[rowIndex].Cells[1].FindControl("txtpurchamount") as TextBox).Text) = Convert.ToString(productamount);
                                                ((Gridview1.Rows[rowIndex].Cells[1].FindControl("txttaxamount") as TextBox).Text) = Convert.ToString(taxamount15);
                                                ((Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproamount") as TextBox).Text) = Convert.ToString(productamount);

                                                System.DateTime Dtnow = DateTime.Now;
                                                string Sysdatetime = Dtnow.ToString("dd/MM/yyyy");
                                                DataSet ds = clsgd.GetcondDataSet3("*", "tbltempprodsale", "Productcode", box0.Text, "ProductName", box1.Text, "Batchid", box3.Text);
                                                if (ds.Tables[0].Rows.Count == 0)
                                                {

                                                    Clsprdinw.tempproductsale("INSERT_TEMPPRODUCTSALEbatch", box0.Text, box1.Text, box4.SelectedItem.Text, box2.Text, box5.Text, Session["username"].ToString(), sMacAddress, Sysdatetime);
                                                }
                                                rowIndex++;
                                            }





                                        }





                                    }



                                }
                                TextBox txt = (TextBox)sender;
                                GridViewRow row = (GridViewRow)txt.NamingContainer;


                                (Gridview1.Rows[row.RowIndex].Cells[1].FindControl("ButtonAdd") as Button).Focus();


                            }








                        }





                        else
                        {
                            // Do something if no user is found
                            // OR do nothing
                        }

                        // Close connections
                        dr.Close();
                        conn.Close();
                    }

                }

                Double sum = 0;

                Double proamount = 0.0;
                Double discount1 = 0.0;
                Double sumdisc = 0.0;
                Double quantity1 = 0.0;
                Double rate1 = 0.0;
                Double tax1 = 0.0;
                Double taxrate1 = 0.0;
                Double addtax = 0.0;
                Double discsumamt = 0.0;
                Double purchamount = 0.0;
                Double adpurchamount = 0.0;


                for (int k = 0; k < Gridview1.Rows.Count; k++)
                {
                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtquantity") as TextBox).Text))
                    {
                        quantity1 = 0.0;
                    }
                    else
                    {

                        quantity1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtquantity") as TextBox).Text);
                    }


                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtrate") as TextBox).Text))
                    {
                        rate1 = 0.0;
                    }
                    else
                    {

                        rate1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtrate") as TextBox).Text);
                    }



                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[5].FindControl("txttax") as TextBox).Text))
                    {
                        tax1 = 0.0;
                    }
                    else
                    {

                        tax1 = Convert.ToDouble((Gridview1.Rows[k].Cells[5].FindControl("txttax") as TextBox).Text);
                    }


                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text))
                    {
                        taxrate1 = 0.0;

                    }
                    else
                    {

                        taxrate1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text);
                        addtax = addtax + taxrate1;
                    }




                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text))
                    {
                        proamount = 0.0;

                    }
                    else
                    {

                        proamount = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text);
                        sum = sum + proamount;
                        string str = sum.ToString("0.00");
                    }


                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtpurchamount") as TextBox).Text))
                    {
                        purchamount = 0.0;

                    }
                    else
                    {

                        purchamount = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtpurchamount") as TextBox).Text);
                        adpurchamount = adpurchamount + purchamount;
                    }









                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtdiscount") as TextBox).Text))
                    {
                        discount1 = 0.0;

                    }
                    else
                    {

                        double sumpramount = 0.0;
                        discount1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtdiscount") as TextBox).Text);
                        sumdisc = sumdisc + discount1;
                        proamount = quantity1 * rate1;
                        sumpramount = (quantity1 * rate1);
                        // Double sumpramount=Convert.ToDouble(txtpramount.Text);

                        // Double sumpramount1=sumpramount + (quantity1*rate1);
                        // txtpramount.Text=(sumpramount1).ToString();
                        Double sumdiscount = proamount * discount1 / 100;
                        discsumamt = discsumamt + sumdiscount;
                        double discsumamt1 = Convert.ToDouble(discsumamt);
                        double discsumamt2 = Math.Round(discsumamt1, 2);
                        txtdiscount.Text = (discsumamt2).ToString();

                        txtpramount.Text = (adpurchamount).ToString();
                        string tax10 = (addtax).ToString();
                        double tax15 = Convert.ToDouble(tax10);
                        double tax20 = Math.Round(tax15, 2);
                        txttax.Text = (tax20).ToString();

                        double taxrate = (tax1 * quantity1 * rate1) / 100;
                        double productamount = (quantity1 * rate1) + taxrate;
                        double proamount1 = (quantity1 * rate1);
                        double productamount1 = Math.Round(productamount, 2);
                        string productamount2 = Convert.ToString(productamount1);


                    }






                    double sumproductamount = 0.0;


                    //if (txttotalamount.Text == "")
                    //{
                    //    txttotalamount.Text = (sum).ToString();
                    //}
                    //else
                    //{
                        double taxrate20 = (tax1 * quantity1 * rate1) / 100;
                        double productamount20 = (quantity1 * rate1);

                        //sumproductamount=sumproductamount+productamount;

                        Double sumpramount20 = Convert.ToDouble(txtpramount.Text);
                        double totfinal = Convert.ToDouble(txtdiscount.Text);
                        double tttax = Convert.ToDouble(txttax.Text);
                        txttotalamount.Text = (sumpramount20 - totfinal + tttax).ToString();
                    //}

                }













            }








            else
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtbatchno") as TextBox).Text);
                    //OleDbCommand com;
                    string strconn1 = Dbconn.conmenthod();

                    using (OleDbConnection conn = new OleDbConnection(strconn1))
                    {



                        //conn.Open();
                        string commandString = "SELECT Tax,Batchid,MRP FROM tblProductinward " +
                                                                  String.Format("WHERE (Batchid  = '{0}')", ID);
                        OleDbCommand cmd = new OleDbCommand(commandString, conn);
                        conn.Open();
                        OleDbDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            // Setup DataReader
                            dr.Read();

                            // Set DR values to Text fields
                            ((Gridview1.Rows[i].Cells[1].FindControl("txttax") as TextBox).Text) = dr["Tax"].ToString();

                            double mrp = Convert.ToDouble(dr["MRP"]);

                            int rowIndex = 0;
                            //StringCollection sc = new StringCollection();
                            if (ViewState["CurrentTable"] != null)
                            {
                                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                                DataRow drCurrentRow = null;
                                if (dtCurrentTable.Rows.Count > 0)
                                {
                                    for (int j = 1; j <= dtCurrentTable.Rows.Count; j++)
                                    {
                                        TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");
                                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtquantity");
                                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                                        //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                                        TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtbatchno");
                                        TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtrate");
                                        TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtdiscount");
                                        TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                                        TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtproamount");


                                        //double quantity = Convert.ToDouble(box2.Text);
                                        double quantity = 0.0;

                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtquantity") as TextBox).Text))
                                        {
                                            quantity = 0.0;
                                        }
                                        else
                                        {

                                            quantity = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtquantity") as TextBox).Text);
                                        }


                                        double rate = Convert.ToDouble(box5.Text);

                                        double tax = Convert.ToDouble(box7.Text);

                                        double disc = Convert.ToDouble(box6.Text);

                                        //double taxrate = (mrp * tax) / (100 + tax);

                                        double taxrate = (tax * quantity * rate) / 100;

                                        double productamount = (quantity * rate) - (quantity * disc);
                                        double productamount1 = Math.Round(productamount, 2);
                                        string productamount2 = Convert.ToString(productamount1);

                                        ((Gridview1.Rows[i].Cells[1].FindControl("txtproamount") as TextBox).Text) = Convert.ToString(productamount2);

                                        double selprice = Convert.ToDouble(mrp - taxrate);

                                        double rselprice = Math.Round(selprice, 2);
                                        string selprice1 = Convert.ToString(rselprice);
                                        System.DateTime Dtnow = DateTime.Now;
                                        string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                                        String strconn11 = Dbconn.conmenthod();

                                        Double sum = 0;
                                        Double add = 0.0;

                                        for (int k = 0; k < Gridview1.Rows.Count; k++)
                                        {

                                            if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text))
                                            {
                                                add = 0.0;

                                            }
                                            else
                                            {

                                                add = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text);
                                                sum = sum + add;
                                            }


                                            txtpramount.Text = (sum).ToString();


                                            if (txttotalamount.Text == "")
                                            {
                                                txttotalamount.Text = (sum).ToString();
                                            }
                                            else
                                            {
                                                double totamount = Convert.ToDouble(txttotalamount.Text);
                                                txttotalamount.Text = (sum - totamount).ToString();
                                            }

                                        }




                                    }





                                }

                            }


                        }
                        else
                        {
                            // Do something if no user is found
                            // OR do nothing
                        }
                    }
                }
            }
        }

        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }


        if (ddpaymenttype.SelectedItem.Text == "CARD")
        {
            Panel3.Visible = true;
            Double sum = 0;
            Double add = 0.0;
            Double discount1 = 0.0;
            Double sumdisc = 0.0;
            Double taxrate1 = 0.0;
            Double addtax = 0.0;

            for (int k = 0; k < Gridview1.Rows.Count; k++)
            {

                if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text))
                {
                    add = 0.0;

                }
                else
                {

                    add = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text);
                    sum = sum + add;
                }


                if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text))
                {
                    taxrate1 = 0.0;

                }
                else
                {

                    taxrate1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text);
                    addtax = addtax + taxrate1;
                }






                txtpramount.Text = (sum).ToString();


                //if (txtdiscount.Text == "")
                //{
                //    txttotalamount.Text = (sum).ToString();
                //    txtpramount.Text = (sum).ToString();
                //    Double sumpramount=Convert.ToDouble(txtpramount.Text);
                //    Double sumdiscount=sumpramount * sumdisc/100;
                //    txtdiscount.Text=(sumdiscount).ToString();
                //    txttax.Text=(addtax).ToString();
                //}
                //else
                //{
                //    double ttpramount = Convert.ToDouble(txtpramount.Text);
                //    double ttdiscount = Convert.ToDouble(txtdiscount.Text);
                //    txttotalamount.Text = (sum - ttdiscount).ToString();
                //    txtamount.Text=(sum - ttdiscount).ToString();
                //    txttax.Text=(addtax).ToString();
                //}

                string amttot = txttotalamount.Text;
                txtcramount.Text = amttot;

                
             if (ddlpaytype.SelectedItem.Text == "Credit Card")
             {
                    txtpramount.Text = (sum).ToString();

                string p_flag2 = ddlpaytype.SelectedItem.Text;

                DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblSalecardtype", "Saletype", p_flag2);

                double flag2 = Convert.ToDouble(dsgroup10.Tables[0].Rows[0]["Extraamount"].ToString());


                double amttot20 = Convert.ToDouble(txttotalamount.Text);

                double camount = amttot20 + (amttot20 * flag2) / 100;

                  string  camount10 =  camount.ToString("F");

                  txtcramount.Text = Convert.ToString(camount10);

                  txttotalamount.Text = Convert.ToString(camount10);
             }

            

              if (ddlpaytype.SelectedItem.Text == "Debit card")
             {

                  txtpramount.Text = (sum).ToString();

                string p_flag2 = ddlpaytype.SelectedItem.Text;

                DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblSalecardtype", "Saletype", p_flag2);

                double flag2 = Convert.ToDouble(dsgroup10.Tables[0].Rows[0]["Extraamount"].ToString());


                double amttot40 = Convert.ToDouble(txttotalamount.Text);

                double camount = amttot40 + (amttot40 * flag2) / 100;

                 string  camount10 =  camount.ToString("F");

                  txtcramount.Text = Convert.ToString(camount10);

                  txttotalamount.Text = Convert.ToString(camount10);
              }


              


        //else
        //{
        //    //lblbillnor.Enabled = false;
        //    //lblbillnor.Text = invoiceno;
        //    lblvbillno.Enabled = false;
        //    lblvbillno.Text = invoiceno;
        //    Panel3.Visible = false;
        //}

      

       
        //else
        //{
        //    //lblbillnor.Enabled = false;
        //    //lblbillnor.Text = invoiceno;
        //    lblvbillno.Enabled = false;
        //    lblvbillno.Text = invoiceno;
        //    Panel3.Visible = false;
        //}
    }


        }










        if (ddpaymenttype.SelectedItem.Text == "CUSTOMER")
        {
            Panel4.Visible = true;
            Double sum = 0;
            Double add = 0.0;
            Double discount1 = 0.0;
            Double sumdisc = 0.0;
            Double taxrate1 = 0.0;
            Double addtax = 0.0;

            for (int k = 0; k < Gridview1.Rows.Count; k++)
            {

                if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text))
                {
                    add = 0.0;

                }
                else
                {

                    add = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text);
                    sum = sum + add;
                }


                if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text))
                {
                    taxrate1 = 0.0;

                }
                else
                {

                    taxrate1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text);
                    addtax = addtax + taxrate1;
                }






                txtpramount.Text = (sum).ToString();


                //if (txtdiscount.Text == "")
                //{
                //    txttotalamount.Text = (sum).ToString();
                //    txtpramount.Text = (sum).ToString();
                //    Double sumpramount=Convert.ToDouble(txtpramount.Text);
                //    Double sumdiscount=sumpramount * sumdisc/100;
                //    txtdiscount.Text=(sumdiscount).ToString();
                //    txttax.Text=(addtax).ToString();
                //}
                //else
                //{
                //    double ttpramount = Convert.ToDouble(txtpramount.Text);
                //    double ttdiscount = Convert.ToDouble(txtdiscount.Text);
                //    txttotalamount.Text = (sum - ttdiscount).ToString();
                //    txtamount.Text=(sum - ttdiscount).ToString();
                //    txttax.Text=(addtax).ToString();
                //}

                string amttot = txttotalamount.Text;
                txtamount.Text = amttot;

            }
        }
















        TextBox txt1 = (TextBox)sender;
        GridViewRow row1 = (GridViewRow)txt1.NamingContainer;
        Gridview1.Rows[row1.RowIndex].FindControl("txtdiscount").Focus();


      




      


    }


    protected void ddl_Batch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("ddl_Batch") as DropDownList).Text);
                string productcode29 = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text);

                using (SqlConnection conn = new SqlConnection(strconn))
                {
                   // string commandString = "SELECT  SUM(Stockinhand) as Stockinhand  FROM tblProductinward where Batchid='" + ID + "'";
                    //SqlConnection cnn = new SqlConnection(connectionString);

                    string commandString = "SELECT   Stockinhand  FROM tblProductinward where Batchid='" + ID + "' and productcode='" + productcode29 + "'";
                    SqlCommand cmd = new SqlCommand(commandString, conn);
                    conn.Open();

                    // Execute SQL and get returned Reader
                    SqlDataReader dr = cmd.ExecuteReader();


                    // Test for values in DataReader
                    if (dr.HasRows)
                    {
                        // Setup DataReader
                        dr.Read();

                        // Set DR values to Text fields
                        //((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text) = dr["Productcode"].ToString();
                        ((Gridview1.Rows[i].Cells[1].FindControl("txtStockinhand") as TextBox).Text) = dr["Stockinhand"].ToString();






                    }



                    else
                    {
                        // Do something if no user is found
                        // OR do nothing
                    }

                    // Close connections
                    dr.Close();
                    conn.Close();
                }




                for (int k = 0; k < Gridview1.Rows.Count; k++)
                {

                    string productcode = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductcode") as TextBox).Text);
                    string productname = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtproductname") as TextBox).Text);
                    string Batch = Convert.ToString((Gridview1.Rows[k].Cells[3].FindControl("ddl_Batch") as DropDownList).Text);




                    using (SqlConnection conn = new SqlConnection(strconn))
                    {
                        //conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

                        //string commandString = "SELECT ProductName,Productcode,Sellprice,Batchid FROM tblProductinward " +
                        //String.Format("WHERE (Batchid = '{0}')", ID);
                        // string commandString = "SELECT ProductName,Productcode,Sellprice,MRP,Batchid FROM tblProductinward WHERE Productcode = " + productcode + " AND productname = '" + productname + "' AND Batchid = " + Batch;
                        //SqlConnection cnn = new SqlConnection(connectionString);
                        // SqlCommand cmd = new SqlCommand(commandString, conn);
                        // conn.Open();

                        // Execute SQL and get returned Reader
                        //SqlDataReader dr = cmd.ExecuteReader();

                        DataSet dschm = clsgd.GetcondDataSet3("*", "tblProductinward", "Productcode", productcode, "productname", productname, "Batchid", Batch);
                        if (dschm.Tables[0].Rows.Count > 0)
                        {




                            // Set DR values to Text fields
                            //((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text) = dr["Productcode"].ToString();
                            double tax1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txttax") as TextBox).Text);
                            //string mrp = dr["MRP"].ToString();
                            string mrp = dschm.Tables[0].Rows[0]["MRP"].ToString();
                            Double mrp1 = Convert.ToDouble(mrp);

                            Double amt = (mrp1 * tax1) / (100 + tax1);
                            Double fnamt = mrp1 - amt;
                            double fnamt10 = Math.Round(fnamt, 2);

                            ((Gridview1.Rows[k].Cells[1].FindControl("txtrate") as TextBox).Text) = Convert.ToString(fnamt10);





                        }




                        else
                        {
                            //Master.ShowModal("Same batch id exists and delete the new row.", "txtcustcode", 1);
                            //string name = (GridView1.FooterRow.FindControl("txtName") as TextBox).Text;

                            //return;
                        }

                        // Close connections
                        //dr.Close();
                        conn.Close();
                    }
                    string productcode1 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductcode") as TextBox).Text);
                    SqlConnection con58 = new SqlConnection(strconn1);
                    SqlCommand cmd58 = new SqlCommand("select sum(Stockinhand) as Stockinhand  from tblProductinward where  Productcode = '" + productcode1 + "' and Batchid = '" + Batch + "'", con58);
                    SqlDataAdapter da58 = new SqlDataAdapter(cmd58);
                    DataSet ds58 = new DataSet();

                    da58.Fill(ds58);
                    string stock = ds58.Tables[0].Rows[0]["Stockinhand"].ToString();

                    // int stock = Convert.ToInt32(ds58.Tables[0].Rows[0]["Stockinhand"].ToString());


                    //txtstock.Text = Convert.ToString(stock);
                    if (stock == "")
                    {
                        ShowPopupMessage("Pls chosse different Product Code", PopupMessageType.txtproductcode);


                       // (Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Focus();
                        return;
                    }


                    string productcode12 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductcode") as TextBox).Text);
                    SqlConnection con60 = new SqlConnection(strconn1);
                    SqlCommand cmd60 = new SqlCommand("select sum(Stockinhand) as Stockinhand  from tblProductinward where  Productcode = '" + productcode12 + "'", con60);
                    SqlDataAdapter da60 = new SqlDataAdapter(cmd60);
                    DataSet ds60 = new DataSet();

                    da60.Fill(ds60);
                    string stock10 = ds60.Tables[0].Rows[0]["Stockinhand"].ToString();
                    int stock11 = Convert.ToInt32(stock10);







                    // txtstock.Text = totstock;

                    DataSet dsgrp16 = clsgd.GetcondDataSet("*", "tbltempprodsale", "Productcode", productcode1);
                    if (dsgrp16.Tables[0].Rows.Count > 0)
                    {
                        string productcode24 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductcode") as TextBox).Text);
                        SqlConnection con65 = new SqlConnection(strconn1);
                        SqlCommand cmd65 = new SqlCommand("select Stock  from tbltempprodsale where  Productcode = '" + productcode12 + "'", con58);
                        SqlDataAdapter da65 = new SqlDataAdapter(cmd65);
                        DataSet ds65 = new DataSet();

                        da65.Fill(ds65);
                        string stock15 = ds65.Tables[0].Rows[0]["Stock"].ToString();
                        int stock18 = Convert.ToInt32(stock15);

                        string productcode2 = Convert.ToString((Gridview1.Rows[k].Cells[0].FindControl("txtproductcode") as TextBox).Text);
                        SqlConnection con59 = new SqlConnection(strconn1);
                        SqlCommand cmd59 = new SqlCommand("select sum(Stockinhand) as Stockinhand   from tbltempprodsale where  Productcode = '" + productcode2 + "'", con58);
                        SqlDataAdapter da59 = new SqlDataAdapter(cmd59);
                        DataSet ds59 = new DataSet();

                        da59.Fill(ds59);
                        string quantity = ds59.Tables[0].Rows[0]["Stockinhand"].ToString();
                        int quantity10 = Convert.ToInt32(quantity);

                        string totstock = Convert.ToString(stock18 - quantity10);
                        txtstock.Text = totstock;

                    }
                    else
                    {
                        txtstock.Text = stock10;

                    }


                }







            }



            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;

            Gridview1.Rows[row.RowIndex].FindControl("txtquantity").Focus();

        }

        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }

      


    }



    protected void txtdiscount_TextChanged(object sender, EventArgs e)
    {
        try
        {

            if (!File.Exists(filename))
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text);
                    using (SqlConnection conn = new SqlConnection(strconn))
                    {
                        //conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

                        string commandString = "SELECT Tax,Batchid,MRP,Productcode,Productname FROM tblProductinward " +
                                                                  String.Format("WHERE (Productname  = '{0}')", ID);
                        //SqlConnection cnn = new SqlConnection(connectionString);
                        SqlCommand cmd = new SqlCommand(commandString, conn);
                        conn.Open();

                        // Execute SQL and get returned Reader
                        SqlDataReader dr = cmd.ExecuteReader();


                        // Test for values in DataReader
                        if (dr.HasRows)
                        {
                            // Setup DataReader
                            dr.Read();
                            double mrp = Convert.ToDouble(dr["MRP"]);



                            int rowIndex = 0;
                            //StringCollection sc = new StringCollection();
                            if (ViewState["CurrentTable"] != null)
                            {
                                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                                DataRow drCurrentRow = null;
                                if (dtCurrentTable.Rows.Count > 0)
                                {
                                    for (int j = 0; j < dtCurrentTable.Rows.Count; j++)
                                    {
                                        TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");
                                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtquantity");
                                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                                        //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                                        TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtbatchno");
                                        TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtrate");
                                        TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtdiscount");
                                        TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                                        TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtproamount");
                                        TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txttaxamount");

                                        string disccount = Convert.ToString(box6.Text);

                                        if (disccount == "")
                                        {

                                            Master.ShowModal("Enter Zero or discount amount", "txtdiscount", 0);
                                            return;

                                        }






                                        Double quantity = 0.0;
                                        Double rate = 0.0;
                                        Double tax = 0.0;
                                        Double disc = 0.0;


                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtquantity") as TextBox).Text))
                                        {
                                            quantity = 0.0;
                                        }
                                        else
                                        {

                                            quantity = Convert.ToDouble(box2.Text);
                                        }


                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtrate") as TextBox).Text))
                                        {
                                            rate = 0.0;
                                        }
                                        else
                                        {

                                            rate = Convert.ToDouble(box5.Text);
                                        }


                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txttax") as TextBox).Text))
                                        {
                                            tax = 0.0;
                                        }
                                        else
                                        {

                                            tax = Convert.ToDouble(box7.Text);
                                        }


                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtdiscount") as TextBox).Text))
                                        {
                                            disc = 0.0;
                                        }
                                        else
                                        {

                                            disc = Convert.ToDouble(box6.Text);
                                        }



                                        double productamount12 = 0.0;


                                        if (rate == 0.0)
                                        {
                                            double productamount = 0;

                                        }
                                        else
                                        {


                                            // double ratetax = quantity * taxrate;
                                            if (disc == '0')
                                            {
                                                double productamount = (quantity * rate);
                                            }
                                            else
                                            {
                                                //double taxrate = (tax * rate) / (100 + tax);
                                                //double ratetax = quantity * taxrate;
                                                //double fnratetax= Math.Round(ratetax, 2);
                                                double disc10 = (rate * disc) / 100;

                                                double amt10 = rate - disc10;
                                                double amt15 = (amt10 * tax) / 100;
                                                double taxrate = (amt15 * quantity);
                                                double taxrate2 = Math.Round(taxrate, 2);

                                                double productamount = (quantity * rate);
                                                Double sumdiscount = productamount * disc / 100;
                                                double proamount1 = ((productamount) - sumdiscount + taxrate);
                                                double productamount1 = Math.Round(proamount1, 2);
                                                string productamount2 = Convert.ToString(productamount1);

                                                double taxamount10 = rate - disc10;
                                                double taxamount15 = taxamount10 * quantity;


                                                ((Gridview1.Rows[rowIndex].Cells[1].FindControl("txttaxrate") as TextBox).Text) = Convert.ToString(taxrate2);
                                                ((Gridview1.Rows[rowIndex].Cells[1].FindControl("txtpurchamount") as TextBox).Text) = Convert.ToString(productamount);
                                                ((Gridview1.Rows[rowIndex].Cells[1].FindControl("txttaxamount") as TextBox).Text) = Convert.ToString(taxamount15);
                                                ((Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproamount") as TextBox).Text) = Convert.ToString(productamount);
                                                rowIndex++;
                                            }





                                        }





                                    }



                                }
                                TextBox txt = (TextBox)sender;
                                GridViewRow row = (GridViewRow)txt.NamingContainer;


                                (Gridview1.Rows[row.RowIndex].Cells[1].FindControl("ButtonAdd") as Button).Focus();


                            }








                        }





                        else
                        {
                            // Do something if no user is found
                            // OR do nothing
                        }

                        // Close connections
                        dr.Close();
                        conn.Close();
                    }

                }

                Double sum = 0;
                Double proamount = 0.0;
                Double discount1 = 0.0;
                Double sumdisc = 0.0;
                Double quantity1 = 0.0;
                Double rate1 = 0.0;
                Double tax1 = 0.0;
                Double taxrate1 = 0.0;
                Double addtax = 0.0;
                Double discsumamt = 0.0;
                Double purchamount = 0.0;
                Double adpurchamount = 0.0;


                for (int k = 0; k < Gridview1.Rows.Count; k++)
                {
                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtquantity") as TextBox).Text))
                    {
                        quantity1 = 0.0;
                    }
                    else
                    {

                        quantity1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtquantity") as TextBox).Text);
                    }


                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtrate") as TextBox).Text))
                    {
                        rate1 = 0.0;
                    }
                    else
                    {

                        rate1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtrate") as TextBox).Text);
                    }



                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[5].FindControl("txttax") as TextBox).Text))
                    {
                        tax1 = 0.0;
                    }
                    else
                    {

                        tax1 = Convert.ToDouble((Gridview1.Rows[k].Cells[5].FindControl("txttax") as TextBox).Text);
                    }


                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text))
                    {
                        taxrate1 = 0.0;

                    }
                    else
                    {

                        taxrate1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text);
                        addtax = addtax + taxrate1;
                    }




                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text))
                    {
                        proamount = 0.0;

                    }
                    else
                    {

                        proamount = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text);
                        sum = sum + proamount;
                    }


                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtpurchamount") as TextBox).Text))
                    {
                        purchamount = 0.0;

                    }
                    else
                    {

                        purchamount = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtpurchamount") as TextBox).Text);
                        adpurchamount = adpurchamount + purchamount;
                    }









                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtdiscount") as TextBox).Text))
                    {
                        discount1 = 0.0;

                    }
                    else
                    {

                        double sumpramount = 0.0;
                        discount1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtdiscount") as TextBox).Text);
                        sumdisc = sumdisc + discount1;
                        proamount = quantity1 * rate1;
                        sumpramount = (quantity1 * rate1);
                        // Double sumpramount=Convert.ToDouble(txtpramount.Text);

                        // Double sumpramount1=sumpramount + (quantity1*rate1);
                        // txtpramount.Text=(sumpramount1).ToString();
                        Double sumdiscount = proamount * discount1 / 100;
                        discsumamt = discsumamt + sumdiscount;
                        double discsumamt1 = Convert.ToDouble(discsumamt);
                        double discsumamt2 = Math.Round(discsumamt1, 2);
                        txtdiscount.Text = (discsumamt2).ToString();

                        txtpramount.Text = (adpurchamount).ToString();
                        string tax10 = (addtax).ToString();
                        double tax15 = Convert.ToDouble(tax10);
                        double tax20 = Math.Round(tax15, 2);
                        txttax.Text = (tax20).ToString();

                        double taxrate = (tax1 * quantity1 * rate1) / 100;
                        double productamount = (quantity1 * rate1) + taxrate;
                        double proamount1 = (quantity1 * rate1);
                        double productamount1 = Math.Round(productamount, 2);
                        string productamount2 = Convert.ToString(productamount1);


                    }






                    double sumproductamount = 0.0;


                    if (txttotalamount.Text == "")
                    {
                        txttotalamount.Text = (sum).ToString();
                    }
                    else
                    {
                        double taxrate = (tax1 * quantity1 * rate1) / 100;
                        double productamount = (quantity1 * rate1);

                        //sumproductamount=sumproductamount+productamount;

                        Double sumpramount = Convert.ToDouble(txtpramount.Text);
                        double totfinal = Convert.ToDouble(txtdiscount.Text);
                        double tttax = Convert.ToDouble(txttax.Text);
                        txttotalamount.Text = (sumpramount - totfinal + tttax).ToString();
                    }

                }













            }








            else
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtbatchno") as TextBox).Text);
                    //OleDbCommand com;
                    string strconn1 = Dbconn.conmenthod();

                    using (OleDbConnection conn = new OleDbConnection(strconn1))
                    {



                        //conn.Open();
                        string commandString = "SELECT Tax,Batchid,MRP FROM tblProductinward " +
                                                                  String.Format("WHERE (Batchid  = '{0}')", ID);
                        OleDbCommand cmd = new OleDbCommand(commandString, conn);
                        conn.Open();
                        OleDbDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            // Setup DataReader
                            dr.Read();

                            // Set DR values to Text fields
                            ((Gridview1.Rows[i].Cells[1].FindControl("txttax") as TextBox).Text) = dr["Tax"].ToString();

                            double mrp = Convert.ToDouble(dr["MRP"]);

                            int rowIndex = 0;
                            //StringCollection sc = new StringCollection();
                            if (ViewState["CurrentTable"] != null)
                            {
                                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                                DataRow drCurrentRow = null;
                                if (dtCurrentTable.Rows.Count > 0)
                                {
                                    for (int j = 1; j <= dtCurrentTable.Rows.Count; j++)
                                    {
                                        TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");
                                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtquantity");
                                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                                        //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                                        TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtbatchno");
                                        TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtrate");
                                        TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtdiscount");
                                        TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                                        TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtproamount");


                                        //double quantity = Convert.ToDouble(box2.Text);
                                        double quantity = 0.0;

                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtquantity") as TextBox).Text))
                                        {
                                            quantity = 0.0;
                                        }
                                        else
                                        {

                                            quantity = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtquantity") as TextBox).Text);
                                        }


                                        double rate = Convert.ToDouble(box5.Text);

                                        double tax = Convert.ToDouble(box7.Text);

                                        double disc = Convert.ToDouble(box6.Text);

                                        //double taxrate = (mrp * tax) / (100 + tax);

                                        double taxrate = (tax * quantity * rate) / 100;

                                        double productamount = (quantity * rate) - (quantity * disc);
                                        double productamount1 = Math.Round(productamount, 2);
                                        string productamount2 = Convert.ToString(productamount1);

                                        ((Gridview1.Rows[i].Cells[1].FindControl("txtproamount") as TextBox).Text) = Convert.ToString(productamount2);

                                        double selprice = Convert.ToDouble(mrp - taxrate);

                                        double rselprice = Math.Round(selprice, 2);
                                        string selprice1 = Convert.ToString(rselprice);
                                        System.DateTime Dtnow = DateTime.Now;
                                        string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                                        String strconn11 = Dbconn.conmenthod();

                                        Double sum = 0;
                                        Double add = 0.0;

                                        for (int k = 0; k < Gridview1.Rows.Count; k++)
                                        {

                                            if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text))
                                            {
                                                add = 0.0;

                                            }
                                            else
                                            {

                                                add = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text);
                                                sum = sum + add;
                                            }


                                            txtpramount.Text = (sum).ToString();


                                            if (txttotalamount.Text == "")
                                            {
                                                txttotalamount.Text = (sum).ToString();
                                            }
                                            else
                                            {
                                                double totamount = Convert.ToDouble(txttotalamount.Text);
                                                txttotalamount.Text = (sum - totamount).ToString();
                                            }

                                        }




                                    }





                                }

                            }


                        }
                        else
                        {
                            // Do something if no user is found
                            // OR do nothing
                        }
                    }
                }
            }
        }

        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }


        if (ddpaymenttype.SelectedItem.Text == "CUSTOMER")
        {
            Panel4.Visible = true;
            Double sum = 0;
            Double add = 0.0;
            Double discount1 = 0.0;
            Double sumdisc = 0.0;
            Double taxrate1 = 0.0;
            Double addtax = 0.0;

            for (int k = 0; k < Gridview1.Rows.Count; k++)
            {

                if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text))
                {
                    add = 0.0;

                }
                else
                {

                    add = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text);
                    sum = sum + add;
                }


                if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text))
                {
                    taxrate1 = 0.0;

                }
                else
                {

                    taxrate1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text);
                    addtax = addtax + taxrate1;
                }






                txtpramount.Text = (sum).ToString();


                //if (txtdiscount.Text == "")
                //{
                //    txttotalamount.Text = (sum).ToString();
                //    txtpramount.Text = (sum).ToString();
                //    Double sumpramount=Convert.ToDouble(txtpramount.Text);
                //    Double sumdiscount=sumpramount * sumdisc/100;
                //    txtdiscount.Text=(sumdiscount).ToString();
                //    txttax.Text=(addtax).ToString();
                //}
                //else
                //{
                //    double ttpramount = Convert.ToDouble(txtpramount.Text);
                //    double ttdiscount = Convert.ToDouble(txtdiscount.Text);
                //    txttotalamount.Text = (sum - ttdiscount).ToString();
                //    txtamount.Text=(sum - ttdiscount).ToString();
                //    txttax.Text=(addtax).ToString();
                //}

                string amttot = txttotalamount.Text;
                txtamount.Text = amttot;

            }
        }

      



       // (Gridview1.Rows[0].Cells[8].FindControl("ButtonAdd") as Button).Focus();

        


    }

     protected void txttax_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!File.Exists(filename))
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text);
                    using (SqlConnection conn = new SqlConnection(strconn))
                    {
                        //conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

                        string commandString = "SELECT Tax,Batchid,MRP,Productcode,Productname FROM tblProductinward " +
                                                                  String.Format("WHERE (Productname  = '{0}')", ID);
                        //SqlConnection cnn = new SqlConnection(connectionString);
                        SqlCommand cmd = new SqlCommand(commandString, conn);
                        conn.Open();

                        // Execute SQL and get returned Reader
                        SqlDataReader dr = cmd.ExecuteReader();


                        // Test for values in DataReader
                        if (dr.HasRows)
                        {
                            // Setup DataReader
                            dr.Read();

                            // Set DR values to Text fields
                            //((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text) = dr["Productcode"].ToString();
                            //((Gridview1.Rows[i].Cells[1].FindControl("txttax") as TextBox).Text) = dr["Tax"].ToString();

                            double mrp = Convert.ToDouble(dr["MRP"]);



                            int rowIndex = 0;
                            //StringCollection sc = new StringCollection();
                            if (ViewState["CurrentTable"] != null)
                            {
                                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                                DataRow drCurrentRow = null;
                                if (dtCurrentTable.Rows.Count > 0)
                                {
                                    for (int j = 0; j < dtCurrentTable.Rows.Count; j++)
                                    {
                                        TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");
                                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtquantity");
                                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                                        //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                                        TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtbatchno");
                                        TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtStockinhand");
                                        TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txtrate");
                                        TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtdiscount");
                                        DropDownList box8 = (DropDownList)Gridview1.Rows[rowIndex].Cells[9].FindControl("ddlTAX");
                                        TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproamount");








                                        Double quantity = 0.0;
                                        Double rate = 0.0;
                                        Double tax = 0.0;
                                        Double discount10 = 0.0;


                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtquantity") as TextBox).Text))
                                        {
                                            quantity = 0.0;
                                        }
                                        else
                                        {

                                            quantity = Convert.ToDouble(box2.Text);
                                        }


                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtrate") as TextBox).Text))
                                        {
                                            rate = 0.0;
                                        }
                                        else
                                        {

                                            rate = Convert.ToDouble(box6.Text);
                                        }


                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("ddlTAX") as DropDownList).SelectedValue))
                                        {
                                            tax = 0.0;
                                        }
                                        else
                                        {

                                            tax = Convert.ToDouble(box8.Text);
                                        }

                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[1].FindControl("txtdiscount") as TextBox).Text))
                                        {
                                            discount10 = 0.0;
                                        }
                                        else
                                        {
                                            discount10 = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[1].FindControl("txtdiscount") as TextBox).Text);
                                        }




                                        if (rate == 0.0)
                                        {
                                            double productamount = 0;

                                        }
                                        else
                                        {

                                            // double taxrate = (tax * rate) / (100 + tax);
                                            // double ratetax = quantity * taxrate;
                                            //double fnratetax= Math.Round(ratetax, 2)
                                            string discount20 = Convert.ToString(discount10);
                                            if (discount20 == "")
                                            {
                                                discount10 = (rate * discount10) / 100;
                                                double amt10 = rate - discount10;
                                                double amt15 = (amt10 * tax) / 100;
                                                double taxrate = (amt15 * quantity);
                                                double taxrate10 = Math.Round(taxrate, 2);


                                                //double productamount = (quantity * rate) + taxrate;
                                                double productamount = (quantity * rate);
                                                //double productamount = (quantity * rate) - (quantity * disc);
                                                double productamount1 = Math.Round(productamount, 2);
                                                string productamount2 = Convert.ToString(productamount1);
                                                ((Gridview1.Rows[rowIndex].Cells[1].FindControl("txttaxrate") as TextBox).Text) = Convert.ToString(0);
                                                ((Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproamount") as TextBox).Text) = Convert.ToString(productamount2);
                                                rowIndex++;

                                            }

                                            else
                                            {

                                                discount10 = (rate * discount10) / 100;
                                                double amt10 = rate - discount10;
                                                double amt15 = (amt10 * tax) / 100;
                                                double taxrate = (amt15 * quantity);
                                                double taxrate10 = Math.Round(taxrate, 2);


                                                //double productamount = (quantity * rate) + taxrate;
                                                double productamount = (quantity * rate);
                                                //double productamount = (quantity * rate) - (quantity * disc);
                                                double productamount1 = Math.Round(productamount, 2);
                                                string productamount2 = Convert.ToString(productamount1);
                                                ((Gridview1.Rows[rowIndex].Cells[1].FindControl("txttaxrate") as TextBox).Text) = Convert.ToString(taxrate10);
                                                ((Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproamount") as TextBox).Text) = Convert.ToString(productamount2);
                                                rowIndex++;
                                            }

                                        }







                                    }


                                    //(Gridview1.Rows[0].Cells[1].FindControl("txtbatchno") as TextBox).Focus();
                                }

                            }








                        }





                        else
                        {
                            // Do something if no user is found
                            // OR do nothing
                        }

                        // Close connections
                        dr.Close();
                        conn.Close();
                    }

                }

                Double sum = 0;
                Double add = 0.0;
                Double discount1 = 0.0;
                Double sumdisc = 0.0;
                Double taxrate1 = 0.0;
                Double addtax = 0.0;

                for (int k = 0; k < Gridview1.Rows.Count; k++)
                {

                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text))
                    {
                        add = 0.0;

                    }
                    else
                    {

                        add = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text);
                        sum = sum + add;
                    }


                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text))
                    {
                        taxrate1 = 0.0;

                    }
                    else
                    {

                        taxrate1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text);
                        addtax = addtax + taxrate1;
                    }






                    txtpramount.Text = (sum).ToString();


                    if (txtdiscount.Text == "")
                    {
                        txttotalamount.Text = (sum).ToString();
                        txtpramount.Text = (sum).ToString();
                        Double sumpramount = Convert.ToDouble(txtpramount.Text);
                        Double sumdiscount = sumpramount * sumdisc / 100;
                        txtdiscount.Text = (sumdiscount).ToString();
                        txttax.Text = (addtax).ToString();
                    }
                    else
                    {
                        double ttpramount = Convert.ToDouble(txtpramount.Text);
                        double ttdiscount = Convert.ToDouble(txtdiscount.Text);
                        double tttax = Convert.ToDouble(txttax.Text);
                        txttotalamount.Text = (sum - ttdiscount + tttax).ToString();
                        txttax.Text = (addtax).ToString();
                    }

                }








            }









            else
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtbatchno") as TextBox).Text);
                    //OleDbCommand com;
                    string strconn1 = Dbconn.conmenthod();

                    using (OleDbConnection conn = new OleDbConnection(strconn1))
                    {



                        //conn.Open();
                        string commandString = "SELECT Tax,Batchid,MRP FROM tblProductinward " +
                                                                  String.Format("WHERE (Batchid  = '{0}')", ID);
                        OleDbCommand cmd = new OleDbCommand(commandString, conn);
                        conn.Open();
                        OleDbDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            // Setup DataReader
                            dr.Read();

                            // Set DR values to Text fields
                            ((Gridview1.Rows[i].Cells[1].FindControl("txttax") as TextBox).Text) = dr["Tax"].ToString();

                            double mrp = Convert.ToDouble(dr["MRP"]);

                            int rowIndex = 0;
                            //StringCollection sc = new StringCollection();
                            if (ViewState["CurrentTable"] != null)
                            {
                                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                                DataRow drCurrentRow = null;
                                if (dtCurrentTable.Rows.Count > 0)
                                {
                                    for (int j = 1; j <= dtCurrentTable.Rows.Count; j++)
                                    {
                                        TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");
                                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtquantity");
                                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                                        //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                                        TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtbatchno");
                                        TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtrate");
                                        TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtdiscount");
                                        TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                                        TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtproamount");


                                        //double quantity = Convert.ToDouble(box2.Text);
                                        double quantity = 0.0;

                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtquantity") as TextBox).Text))
                                        {
                                            quantity = 0.0;
                                        }
                                        else
                                        {

                                            quantity = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtquantity") as TextBox).Text);
                                        }


                                        double rate = Convert.ToDouble(box5.Text);

                                        double tax = Convert.ToDouble(box7.Text);

                                        double disc = Convert.ToDouble(box6.Text);

                                        //double taxrate = (mrp * tax) / (100 + tax);

                                        double taxrate = (tax * quantity * rate) / 100;

                                        double productamount = (quantity * rate) - (quantity * disc);
                                        double productamount1 = Math.Round(productamount, 2);
                                        string productamount2 = Convert.ToString(productamount1);

                                        ((Gridview1.Rows[i].Cells[1].FindControl("txtproamount") as TextBox).Text) = Convert.ToString(productamount2);

                                        double selprice = Convert.ToDouble(mrp - taxrate);

                                        double rselprice = Math.Round(selprice, 2);
                                        string selprice1 = Convert.ToString(rselprice);
                                        System.DateTime Dtnow = DateTime.Now;
                                        string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                                        String strconn11 = Dbconn.conmenthod();

                                        Double sum = 0;
                                        Double add = 0.0;

                                        for (int k = 0; k < Gridview1.Rows.Count; k++)
                                        {

                                            if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text))
                                            {
                                                add = 0.0;

                                            }
                                            else
                                            {

                                                add = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text);
                                                sum = sum + add;
                                            }


                                            txtpramount.Text = (sum).ToString();


                                            if (txttotalamount.Text == "")
                                            {
                                                txttotalamount.Text = (sum).ToString();
                                            }
                                            else
                                            {
                                                double totamount = Convert.ToDouble(txttotalamount.Text);
                                                txttotalamount.Text = (sum - totamount).ToString();
                                            }

                                        }




                                    }


                                    //(Gridview1.Rows[0].Cells[1].FindControl("txtbatchno") as TextBox).Focus();
                                }

                            }


                        }
                        else
                        {
                            // Do something if no user is found
                            // OR do nothing
                        }
                    }
                }
            }

            TextBox txt = (TextBox)sender;
            GridViewRow row = (GridViewRow)txt.NamingContainer;
            Gridview1.Rows[row.RowIndex].FindControl("txtquantity").Focus();
        }

        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }


     }


      private string ShowPopupMessage(string message, PopupMessageType messageType)
    {
        switch (messageType)
        {
            case PopupMessageType.txtdoctorname:
                lblMessagePopupHeading.Text = "Error";
                a = "txtdoctorname";
                break;

            case PopupMessageType.txtpatientname:
                lblMessagePopupHeading.Text = "Error";
                a = "txtpatientname";
                break;


            case PopupMessageType.txtproductcode:
                lblMessagePopupHeading.Text = "Error";
                //Render image in literal control
                
                a = "txtproductcode";
               // int b = Convert.ToInt16(a);


                break;
               
            case PopupMessageType.txtproductname:
                lblMessagePopupHeading.Text = "Error";
                a = "txtproductname";
                break;
                
            case PopupMessageType.txtexpiredate:
                lblMessagePopupHeading.Text = "Error";
                a = "txtexpiredate";
                break;
          

            case PopupMessageType.txtstockarrival:
                lblMessagePopupHeading.Text = "Error";
                a = "txtstockarrival";

                break;

            case PopupMessageType.txtrate:
                lblMessagePopupHeading.Text = "Error";
                a = "txtrate";

                break;

            case PopupMessageType.txttax:
                lblMessagePopupHeading.Text = "Error";
                a = "txttax";

                break;
            case PopupMessageType.txtquantity:
                lblMessagePopupHeading.Text = "Error";
                a = "txtquantity";

                break;
            case PopupMessageType.txtdiscount:
                lblMessagePopupHeading.Text = "Error";
                a = "txtdiscount";

                break;

            case PopupMessageType.txttaxrate:
                lblMessagePopupHeading.Text = "Error";
                a = "txttaxrate";

                break;
            case PopupMessageType.txtpurchamount:
                lblMessagePopupHeading.Text = "Error";
                a = "txtpurchamount";

                break;
            default:
                lblMessagePopupHeading.Text = "Information";

                break;
               
        }
        lblErrorMessage.Text = message;
        mpeMessagePopup.Show();
        
        return a;
        
        //(Gridview1.Rows[0].Cells[2].FindControl("txtbatchno") as TextBox).Focus();
    }
    public enum PopupMessageType
    {
        txtdoctorname,
        txtpatientname,
        txtproductcode,
        txtproductname,
        txtexpiredate,
        txtstockarrival,
        txtrate,
        txttax,
        txtquantity,
        txtdiscount,
        txttaxrate,
        txtpurchamount
       
    }

    protected void btnMessagePopupTargetButton_Click(object sender, EventArgs e)
    {

        if (a == "txtproductcode")
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        (Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode") as TextBox).Focus();
                        rowIndex++;
                    }
                   
                }
            }

        }
        if (a == "txtproductname")
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        (Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname") as TextBox).Focus();
                        rowIndex++;
                    }

                }
            }

            

        }

        if (a == "txtexpiredate")
        {


            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        (Gridview1.Rows[rowIndex].Cells[3].FindControl("txtexpiredate") as TextBox).Focus();
                        rowIndex++;
                    }

                }
            }

        }

        if (a == " txtstockarrival")
        {


            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        (Gridview1.Rows[rowIndex].Cells[4].FindControl("txtstockarrival") as TextBox).Focus();
                        rowIndex++;
                    }

                }
            }

        }

        if (a == "txtrate")
        {


            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        (Gridview1.Rows[rowIndex].Cells[5].FindControl("txtrate") as TextBox).Focus();
                        rowIndex++;
                    }

                }
            }

        }

        if (a == "txttax")
        {


            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        (Gridview1.Rows[rowIndex].Cells[6].FindControl("txttax") as TextBox).Focus();
                        rowIndex++;
                    }

                }
            }

        }

        if (a == "txtquantity")
        {

            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        (Gridview1.Rows[rowIndex].Cells[7].FindControl("txtquantity") as TextBox).Focus();
                        rowIndex++;
                    }

                }
            }
            

        }

        if (a == "txtdiscount")
        {


            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        (Gridview1.Rows[rowIndex].Cells[8].FindControl("txtdiscount") as TextBox).Focus();
                        rowIndex++;
                    }

                }
            }
            

        }

        if (a == "txttaxrate")
        {


            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        (Gridview1.Rows[rowIndex].Cells[9].FindControl("txttaxrate") as TextBox).Focus();
                        rowIndex++;
                    }

                }
            }

        }

        if (a == "txttaxamount")
        {


            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        (Gridview1.Rows[rowIndex].Cells[10].FindControl("txttaxamount") as TextBox).Focus();
                        rowIndex++;
                    }

                }
            }

        }

        if (a == "txtpurchamount")
        {


            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        (Gridview1.Rows[rowIndex].Cells[11].FindControl("txtpurchamount") as TextBox).Focus();
                        rowIndex++;
                    }

                }
            }

        }

        name = "";
        a = "";
    }
    protected void Gridview1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable"] = dt;
                Gridview1.DataSource = dt;
                Gridview1.DataBind();


                for (int i = 0; i < Gridview1.Rows.Count - 1; i++)
                {
                    Gridview1.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }

                SetPreviousData();

            }

            int rowIndex2 = rowIndex + 1;

            
            SqlConnection con58 = new SqlConnection(strconn1);
            con58.Open();
            SqlCommand cmd58 = new SqlCommand("delete FROM tbltempprodsale where Inwardcode='" + rowIndex2 + "'", con58);
            cmd58.ExecuteNonQuery();

        }

      



       

        try

        {


         if (!File.Exists(filename))
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text);
                    using (SqlConnection conn = new SqlConnection(strconn))
                    {
                        //conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

                        string commandString = "SELECT Tax,Batchid,MRP,Productcode,Productname FROM tblProductinward " +
                                                                  String.Format("WHERE (Productname  = '{0}')", ID);
                        //SqlConnection cnn = new SqlConnection(connectionString);
                        SqlCommand cmd = new SqlCommand(commandString, conn);
                        conn.Open();

                        // Execute SQL and get returned Reader
                        SqlDataReader dr = cmd.ExecuteReader();


                        // Test for values in DataReader
                        if (dr.HasRows)
                        {
                            // Setup DataReader
                            dr.Read();
                            double mrp = Convert.ToDouble(dr["MRP"]);



                            int rowIndex = 0;
                            //StringCollection sc = new StringCollection();
                            if (ViewState["CurrentTable"] != null)
                            {
                                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                                DataRow drCurrentRow = null;
                                if (dtCurrentTable.Rows.Count > 0)
                                {
                                    for (int j = 0; j < dtCurrentTable.Rows.Count; j++)
                                    {
                                        TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");
                                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtquantity");
                                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                                        //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                                        TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtbatchno");
                                        TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtrate");
                                        TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtdiscount");
                                        TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                                        TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtproamount");
                                        TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txttaxamount");

                                        string disccount = Convert.ToString(box6.Text);

                                      





                                        Double quantity = 0.0;
                                        Double rate = 0.0;
                                        Double tax = 0.0;
                                        Double disc = 0.0;


                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtquantity") as TextBox).Text))
                                        {
                                            quantity = 0.0;
                                        }
                                        else
                                        {

                                            quantity = Convert.ToDouble(box2.Text);
                                        }


                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtrate") as TextBox).Text))
                                        {
                                            rate = 0.0;
                                        }
                                        else
                                        {

                                            rate = Convert.ToDouble(box5.Text);
                                        }


                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txttax") as TextBox).Text))
                                        {
                                            tax = 0.0;
                                        }
                                        else
                                        {

                                            tax = Convert.ToDouble(box7.Text);
                                        }


                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtdiscount") as TextBox).Text))
                                        {
                                            disc = 0.0;
                                        }
                                        else
                                        {

                                            disc = Convert.ToDouble(box6.Text);
                                        }



                                        double productamount12 = 0.0;


                                        if (rate == 0.0)
                                        {
                                            double productamount = 0;

                                        }
                                        else
                                        {


                                            // double ratetax = quantity * taxrate;
                                            if (disc == '0')
                                            {
                                                double productamount = (quantity * rate);
                                            }
                                            else
                                            {
                                                //double taxrate = (tax * rate) / (100 + tax);
                                                //double ratetax = quantity * taxrate;
                                                //double fnratetax= Math.Round(ratetax, 2);
                                                double disc10 = (rate * disc) / 100;

                                                double amt10 = rate - disc10;
                                                double amt15 = (amt10 * tax) / 100;
                                                double taxrate = (amt15 * quantity);
                                                double taxrate2 = Math.Round(taxrate, 2);

                                                double productamount = (quantity * rate);
                                                Double sumdiscount = productamount * disc / 100;
                                                double proamount1 = ((productamount) - sumdiscount + taxrate);
                                                double productamount1 = Math.Round(proamount1, 2);
                                                string productamount2 = Convert.ToString(productamount1);

                                                double taxamount10 = rate - disc10;
                                                double taxamount15 = taxamount10 * quantity;


                                                ((Gridview1.Rows[rowIndex].Cells[1].FindControl("txttaxrate") as TextBox).Text) = Convert.ToString(taxrate2);
                                                ((Gridview1.Rows[rowIndex].Cells[1].FindControl("txtpurchamount") as TextBox).Text) = Convert.ToString(productamount);
                                                ((Gridview1.Rows[rowIndex].Cells[1].FindControl("txttaxamount") as TextBox).Text) = Convert.ToString(taxamount15);
                                                ((Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproamount") as TextBox).Text) = Convert.ToString(productamount);
                                                rowIndex++;
                                            }





                                        }





                                    }



                                }
                               // TextBox txt = (TextBox)sender;
                                //GridViewRow row = (GridViewRow)txt.NamingContainer;


                                //(Gridview1.Rows[row.RowIndex].Cells[1].FindControl("ButtonAdd") as Button).Focus();


                            }








                        }





                        else
                        {
                            // Do something if no user is found
                            // OR do nothing
                        }

                        // Close connections
                        dr.Close();
                        conn.Close();
                    }

                }

                Double sum = 0;
                Double proamount = 0.0;
                Double discount1 = 0.0;
                Double sumdisc = 0.0;
                Double quantity1 = 0.0;
                Double rate1 = 0.0;
                Double tax1 = 0.0;
                Double taxrate1 = 0.0;
                Double addtax = 0.0;
                Double discsumamt = 0.0;
                Double purchamount = 0.0;
                Double adpurchamount = 0.0;


                for (int k = 0; k < Gridview1.Rows.Count; k++)
                {
                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtquantity") as TextBox).Text))
                    {
                        quantity1 = 0.0;
                    }
                    else
                    {

                        quantity1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtquantity") as TextBox).Text);
                    }


                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtrate") as TextBox).Text))
                    {
                        rate1 = 0.0;
                    }
                    else
                    {

                        rate1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtrate") as TextBox).Text);
                    }



                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[5].FindControl("txttax") as TextBox).Text))
                    {
                        tax1 = 0.0;
                    }
                    else
                    {

                        tax1 = Convert.ToDouble((Gridview1.Rows[k].Cells[5].FindControl("txttax") as TextBox).Text);
                    }


                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text))
                    {
                        taxrate1 = 0.0;

                    }
                    else
                    {

                        taxrate1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text);
                        addtax = addtax + taxrate1;
                    }




                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text))
                    {
                        proamount = 0.0;

                    }
                    else
                    {

                        proamount = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text);
                        sum = sum + proamount;
                    }


                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtpurchamount") as TextBox).Text))
                    {
                        purchamount = 0.0;

                    }
                    else
                    {

                        purchamount = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtpurchamount") as TextBox).Text);
                        adpurchamount = adpurchamount + purchamount;
                    }









                    if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtdiscount") as TextBox).Text))
                    {
                        discount1 = 0.0;

                    }
                    else
                    {

                        double sumpramount = 0.0;
                        discount1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtdiscount") as TextBox).Text);
                        sumdisc = sumdisc + discount1;
                        proamount = quantity1 * rate1;
                        sumpramount = (quantity1 * rate1);
                        // Double sumpramount=Convert.ToDouble(txtpramount.Text);

                        // Double sumpramount1=sumpramount + (quantity1*rate1);
                        // txtpramount.Text=(sumpramount1).ToString();
                        Double sumdiscount = proamount * discount1 / 100;
                        discsumamt = discsumamt + sumdiscount;
                        double discsumamt1 = Convert.ToDouble(discsumamt);
                        double discsumamt2 = Math.Round(discsumamt1, 2);
                        txtdiscount.Text = (discsumamt2).ToString();

                        txtpramount.Text = (adpurchamount).ToString();
                        string tax10 = (addtax).ToString();
                        double tax15 = Convert.ToDouble(tax10);
                        double tax20 = Math.Round(tax15, 2);
                        txttax.Text = (tax20).ToString();

                        double taxrate = (tax1 * quantity1 * rate1) / 100;
                        double productamount = (quantity1 * rate1) + taxrate;
                        double proamount1 = (quantity1 * rate1);
                        double productamount1 = Math.Round(productamount, 2);
                        string productamount2 = Convert.ToString(productamount1);


                    }






                    double sumproductamount = 0.0;


                    if (txttotalamount.Text == "")
                    {
                        txttotalamount.Text = (sum).ToString();
                    }
                    else
                    {
                        double taxrate = (tax1 * quantity1 * rate1) / 100;
                        double productamount = (quantity1 * rate1);

                        //sumproductamount=sumproductamount+productamount;

                        Double sumpramount = Convert.ToDouble(txtpramount.Text);
                        double totfinal = Convert.ToDouble(txtdiscount.Text);
                        double tttax = Convert.ToDouble(txttax.Text);
                        txttotalamount.Text = (sumpramount - totfinal + tttax).ToString();
                    }

                }













            }








            else
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtbatchno") as TextBox).Text);
                    //OleDbCommand com;
                    string strconn10 = Dbconn.conmenthod();

                    using (OleDbConnection conn = new OleDbConnection(strconn10))
                    {



                        //conn.Open();
                        string commandString = "SELECT Tax,Batchid,MRP FROM tblProductinward " +
                                                                  String.Format("WHERE (Batchid  = '{0}')", ID);
                        OleDbCommand cmd = new OleDbCommand(commandString, conn);
                        conn.Open();
                        OleDbDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            // Setup DataReader
                            dr.Read();

                            // Set DR values to Text fields
                            ((Gridview1.Rows[i].Cells[1].FindControl("txttax") as TextBox).Text) = dr["Tax"].ToString();

                            double mrp = Convert.ToDouble(dr["MRP"]);

                            int rowIndex = 0;
                            //StringCollection sc = new StringCollection();
                            if (ViewState["CurrentTable"] != null)
                            {
                                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                                DataRow drCurrentRow = null;
                                if (dtCurrentTable.Rows.Count > 0)
                                {
                                    for (int j = 1; j <= dtCurrentTable.Rows.Count; j++)
                                    {
                                        TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");
                                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtquantity");
                                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                                        //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                                        TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtbatchno");
                                        TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtrate");
                                        TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtdiscount");
                                        TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                                        TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtproamount");


                                        //double quantity = Convert.ToDouble(box2.Text);
                                        double quantity = 0.0;

                                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtquantity") as TextBox).Text))
                                        {
                                            quantity = 0.0;
                                        }
                                        else
                                        {

                                            quantity = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtquantity") as TextBox).Text);
                                        }


                                        double rate = Convert.ToDouble(box5.Text);

                                        double tax = Convert.ToDouble(box7.Text);

                                        double disc = Convert.ToDouble(box6.Text);

                                        //double taxrate = (mrp * tax) / (100 + tax);

                                        double taxrate = (tax * quantity * rate) / 100;

                                        double productamount = (quantity * rate) - (quantity * disc);
                                        double productamount1 = Math.Round(productamount, 2);
                                        string productamount2 = Convert.ToString(productamount1);

                                        ((Gridview1.Rows[i].Cells[1].FindControl("txtproamount") as TextBox).Text) = Convert.ToString(productamount2);

                                        double selprice = Convert.ToDouble(mrp - taxrate);

                                        double rselprice = Math.Round(selprice, 2);
                                        string selprice1 = Convert.ToString(rselprice);
                                        System.DateTime Dtnow = DateTime.Now;
                                        string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                                        String strconn11 = Dbconn.conmenthod();

                                        Double sum = 0;
                                        Double add = 0.0;

                                        for (int k = 0; k < Gridview1.Rows.Count; k++)
                                        {

                                            if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text))
                                            {
                                                add = 0.0;

                                            }
                                            else
                                            {

                                                add = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text);
                                                sum = sum + add;
                                            }


                                            txtpramount.Text = (sum).ToString();


                                            if (txttotalamount.Text == "")
                                            {
                                                txttotalamount.Text = (sum).ToString();
                                            }
                                            else
                                            {
                                                double totamount = Convert.ToDouble(txttotalamount.Text);
                                                txttotalamount.Text = (sum - totamount).ToString();
                                            }

                                        }




                                    }





                                }

                            }


                        }
                        else
                        {
                            // Do something if no user is found
                            // OR do nothing
                        }
                    }
                }
            }
        }

        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }

    }
    public void Bind()
    {
        // string filename = Dbconn.Mymenthod();
        string invce1 = txtinvoicenor.Text;

        try
        {

            SqlConnection con10 = new SqlConnection(strconn1);
            SqlCommand cmd21 = new SqlCommand("select * from tblProductsale  where Invoiceno ='" + invce1 + "'", con10);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd21);
            DataSet ds1 = new DataSet();

            da1.Fill(ds1);

            string STransno = ds1.Tables[0].Rows[0]["Invoiceno"].ToString();

            //string bname = ddlbname.SelectedItem.Text;
            grprodsaledetails.DataSource = null;
            grprodsaledetails.DataBind();
            tblpurchasesale.Rows.Clear();
            SqlConnection con = new SqlConnection(strconn1);
            //SqlCommand cmd = new SqlCommand("Select ProductName,Expiredate,Batchno,Rate,Taxamount,Quantity,D_Rate,Pro_Amount,a.SupplierName as SupplierName,c.ManufactureName as ManufactureName  from tblsuppliermaster b RIGHT JOIN tblProductsale a on b.SupplierName =a.SupplierName inner join tblmanufacture c on c.ManufactureCode=a.ManufactureCode where a.Sale_falg5='Y' and a.STransno='" + STransno + "'", con);
            SqlCommand cmd = new SqlCommand("Select ProductName,Expiredate,Batchno,Rate,Taxamount,Quantity,D_Rate,Pro_Amount,a.g_name as g_name,a.SupplierName as SupplierName,a.ManufactureName as ManufactureName  from tblsuppliermaster b RIGHT JOIN tblProductsale a on b.SupplierName =a.SupplierName LEFT JOIN  tblmanufacture c on c.ManufactureName=a.ManufactureName Inner JOIN tblGroup g on g.g_name=a.g_name where  a.Invoiceno='" + txtinvoicenor.Text + "' ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
               DataColumn col = new DataColumn("SLNO", typeof(int));
                col.AutoIncrement = true;
                col.AutoIncrementSeed = 1;
                col.AutoIncrementStep = 1;
                tblpurchasesale.Columns.Add(col);
               // tblpurchasesale.Columns.Add("Productcode");
                tblpurchasesale.Columns.Add("ProductName");
                tblpurchasesale.Columns.Add("Expiredate");
                tblpurchasesale.Columns.Add("ManufactureName");
                tblpurchasesale.Columns.Add("g_name");
                tblpurchasesale.Columns.Add("Batchno");
                tblpurchasesale.Columns.Add("Rate");
                tblpurchasesale.Columns.Add("Taxamount");
                tblpurchasesale.Columns.Add("Quantity");
               // tblpurchasesale.Columns.Add("D_Rate");
                tblpurchasesale.Columns.Add("Pro_Amount");
               
              Session["customer"] = tblpurchasesale;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    tblpurchasesale = (DataTable)Session["customer"];
                    drrw = tblpurchasesale.NewRow();

                  //  drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                    drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                    DateTime expire1 = Convert.ToDateTime(ds.Tables[0].Rows[i]["Expiredate"].ToString());
                    string expire2 = expire1.ToString("yyyy-MM-dd");
                    drrw["Expiredate"] = expire2;
                    drrw["ManufactureName"] = ds.Tables[0].Rows[i]["ManufactureName"].ToString();
                    drrw["g_name"] = ds.Tables[0].Rows[i]["g_name"].ToString();
                    drrw["Batchno"] = ds.Tables[0].Rows[i]["Batchno"].ToString();
                    drrw["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();
                    drrw["Taxamount"] = ds.Tables[0].Rows[i]["Taxamount"].ToString();
                    drrw["Quantity"] = ds.Tables[0].Rows[i]["Quantity"].ToString();
                   // drrw["D_Rate"] = ds.Tables[0].Rows[i]["D_Rate"].ToString();
                    drrw["Pro_Amount"] = ds.Tables[0].Rows[i]["Pro_Amount"].ToString();
                   
                    

                    //if (SupplierCode10 == "0000")
                    //{
                    //    drrw["SupplierName"] = "No Supplier";
                    //}
                    //else
                    //{
                    //   drrw["SupplierName"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();
                    //}



                    tblpurchasesale.Rows.Add(drrw);
                    //Griddoctor.DataSource = tbldoctor;
                    //Griddoctor.DataBind();
                }
                DataView dws = tblpurchasesale.DefaultView;
                dws.Sort = "SLNO ASC";
                grprodsaledetails.DataSource = tblpurchasesale;
                grprodsaledetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Enabled = true;
            lblerror.Text = asd;
        }

    }


    public void Bind1()
    {
        // string filename = Dbconn.Mymenthod();
        string invce1 = txtinvoicenor.Text;

        try
        {

            SqlConnection con10 = new SqlConnection(strconn1);
            SqlCommand cmd21 = new SqlCommand("select * from tblProductsale where Invoiceno ='" + invce1 + "'", con10);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd21);
            DataSet ds20 = new DataSet();

            da1.Fill(ds20);

            string STransno = ds20.Tables[0].Rows[0]["Invoiceno"].ToString();

            //string bname = ddlbname.SelectedItem.Text;
            grprodsaledetails1.DataSource = null;
            grprodsaledetails1.DataBind();
            tblpurchasesale1.Rows.Clear();
            SqlConnection con = new SqlConnection(strconn1);
           // SqlCommand cmd10 = new SqlCommand("Select ProductName,Expiredate,Batchno,Rate,Taxamount,Quantity,a.D_Rate as D_Rate,Pro_Amount,a.SupplierName as SupplierName,a.ManufactureName as ManufactureName  from tblsuppliermaster b RIGHT JOIN tblProductsale a on b.SupplierName =a.SupplierName LEFT JOIN tblmanufacture c on c.ManufactureName=a.ManufactureName where  a.Invoiceno='" + txtinvoicenor.Text + "'", con);
            SqlCommand cmd10 = new SqlCommand("Select ProductName,Expiredate,Batchno,Rate,Taxamount,Quantity,a.D_Rate as D_Rate,Pro_Amount,a.g_name as g_name,a.ManufactureName as ManufactureName  from tblsuppliermaster b RIGHT JOIN tblProductsale a on b.SupplierName =a.SupplierName LEFT JOIN tblmanufacture c on c.ManufactureName=a.ManufactureName Inner JOIN tblGroup g on g.g_name=a.g_name where  a.Invoiceno='" + txtinvoicenor.Text + "'", con);
            SqlDataAdapter da10 = new SqlDataAdapter(cmd10);
            DataSet ds10 = new DataSet();
            da10.Fill(ds10);

            if (ds10.Tables[0].Rows.Count > 0)
            {
                DataColumn col = new DataColumn("SLNO", typeof(int));
                col.AutoIncrement = true;
                col.AutoIncrementSeed = 1;
                col.AutoIncrementStep = 1;
                tblpurchasesale1.Columns.Add(col);
                // tblpurchasesale.Columns.Add("Productcode");
                tblpurchasesale1.Columns.Add("ProductName");
                tblpurchasesale1.Columns.Add("Expiredate");
                tblpurchasesale1.Columns.Add("ManufactureName");
                tblpurchasesale1.Columns.Add("g_name");
                tblpurchasesale1.Columns.Add("Batchno");
                tblpurchasesale1.Columns.Add("Rate");
                tblpurchasesale1.Columns.Add("Taxamount");
                tblpurchasesale1.Columns.Add("Quantity");
                 tblpurchasesale1.Columns.Add("D_Rate");
                tblpurchasesale1.Columns.Add("Pro_Amount");




                Session["customer"] = tblpurchasesale1;

                for (int i = 0; i < ds10.Tables[0].Rows.Count; i++)
                {
                    tblpurchasesale1 = (DataTable)Session["customer"];
                    drrw = tblpurchasesale1.NewRow();

                    //  drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                    drrw["ProductName"] = ds10.Tables[0].Rows[i]["ProductName"].ToString();
                    DateTime expire1 = Convert.ToDateTime(ds10.Tables[0].Rows[i]["Expiredate"].ToString());
                    string expire2 = expire1.ToString("yyyy-MM-dd");
                    drrw["Expiredate"] = expire2;
                    drrw["ManufactureName"] = ds10.Tables[0].Rows[i]["ManufactureName"].ToString();
                    drrw["g_name"] = ds10.Tables[0].Rows[i]["g_name"].ToString();
                    drrw["Batchno"] = ds10.Tables[0].Rows[i]["Batchno"].ToString();
                    drrw["Rate"] = ds10.Tables[0].Rows[i]["Rate"].ToString();
                    drrw["Taxamount"] = ds10.Tables[0].Rows[i]["Taxamount"].ToString();
                    drrw["Quantity"] = ds10.Tables[0].Rows[i]["Quantity"].ToString();
                     drrw["D_Rate"] = ds10.Tables[0].Rows[i]["D_Rate"].ToString();
                    drrw["Pro_Amount"] = ds10.Tables[0].Rows[i]["Pro_Amount"].ToString();



                    //if (SupplierCode10 == "0000")
                    //{
                    //    drrw["SupplierName"] = "No Supplier";
                    //}
                    //else
                    //{
                    //   drrw["SupplierName"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();
                    //}



                    tblpurchasesale1.Rows.Add(drrw);
                    //Griddoctor.DataSource = tbldoctor;
                    //Griddoctor.DataBind();
                }
                DataView dws = tblpurchasesale1.DefaultView;
                dws.Sort = "SLNO ASC";
                grprodsaledetails1.DataSource = tblpurchasesale1;
                grprodsaledetails1.DataBind();
            }
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Enabled = true;
            lblerror.Text = asd;
        }

    }



    protected void btnprint_Click(object sender, EventArgs e)
    {

        string invce1 = txtinvoicenor.Text;
        Bind();

        Bind1();

       // txtdoctorname.Enabled = true;


       
               /* txtpatientname.Text = string.Empty;
                // txtinvoicenor.Text = string.Empty;
                txtpramount.Text = string.Empty;
                txtdiscount.Text = string.Empty;
                txttotalamount.Text = string.Empty;
                txttransno.Text = string.Empty;
                txtcramount.Text = string.Empty;
                txtbillno.Text = string.Empty;
                txtcustomercode.Text = string.Empty;
                txtcustname.Text = string.Empty;
                 lblbillnor.Text = string.Empty;
                txtamount.Text = string.Empty;
                txttax.Text = string.Empty;
                txtbal.Text = string.Empty;
                txtstock.Text = string.Empty;
                txtinvoicenor.Visible = false;*/

        /*SqlConnection con = new SqlConnection(strconn1);
        SqlCommand cmd21 = new SqlCommand("select max(STransno) as STransno from tblProductsale", con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd21);
        DataSet ds1 = new DataSet();

        da1.Fill(ds1);

        string STransno = ds1.Tables[0].Rows[0]["STransno"].ToString();*/



        ArrayList oALHospDetails = Hosp.HospitalReturns();
        SqlConnection con50 = new SqlConnection(strconn1);
        SqlCommand cmd50 = new SqlCommand("select * from tblProductsale where Invoiceno ='" + txtinvoicenor.Text + "'", con50);
        SqlDataAdapter da50 = new SqlDataAdapter(cmd50);
        DataSet ds50 = new DataSet();
        da50.Fill(ds50);

        string dcname = ds50.Tables[0].Rows[0]["Doctorname"].ToString();
        string pname = ds50.Tables[0].Rows[0]["Patientcode"].ToString();
        string pamount = ds50.Tables[0].Rows[0]["Total_Pro_Amount"].ToString();
        string discount1 = ds50.Tables[0].Rows[0]["Total_Discount"].ToString();
        string tax1 = ds50.Tables[0].Rows[0]["sumtaxrate"].ToString();
        string ttamount = ds50.Tables[0].Rows[0]["Total_Amount"].ToString();



        // PDF Report generation
        // Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
        Document document = new Document(new iTextSharp.text.Rectangle(500f, 400f), 0f, 0f, 0f, 0f);
        PdfWriter.GetInstance(document, Response.OutputStream);
        Document document1 = new Document();
        Font NormalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);

        MemoryStream memoryStream = new System.IO.MemoryStream();

        PdfWriter.GetInstance(document, Response.OutputStream);
        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
        PdfWriterEvents1 writerEvent = new PdfWriterEvents1(oALHospDetails[4].ToString());
        writer.PageEvent = writerEvent;


        DataTable dtPdfstock = new DataTable();
        if (grprodsaledetails.HeaderRow != null)
        {
            for (int i = 0; i < grprodsaledetails.HeaderRow.Cells.Count; i++)
            {
                dtPdfstock.Columns.Add(grprodsaledetails.HeaderRow.Cells[i].Text);
            }
        }

        //  add each of the data rows to the table

        foreach (GridViewRow row in grprodsaledetails.Rows)
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



        DataTable dtPdfstock1 = new DataTable();
        if (grprodsaledetails1.HeaderRow != null)
        {
            for (int i = 0; i < grprodsaledetails1.HeaderRow.Cells.Count; i++)
            {
                dtPdfstock1.Columns.Add(grprodsaledetails1.HeaderRow.Cells[i].Text);
            }
        }

        //  add each of the data rows to the table

        foreach (GridViewRow row in grprodsaledetails1.Rows)
        {
            DataRow datarow1;
            datarow1 = dtPdfstock1.NewRow();

            for (int i = 0; i < row.Cells.Count; i++)
            {
                datarow1[i] = row.Cells[i].Text;
            }
            dtPdfstock1.Rows.Add(datarow1);
        }
        Session["dtPdfstock1"] = dtPdfstock1;

        Phrase phrase = null;
        PdfPCell cell = null;
        PdfPTable tblstock = null;
        PdfPTable table1 = null;
        PdfPTable table2 = null;
        PdfPTable table4 = null;

        PdfPTable tbldt = null;
        dtPdfstock = (DataTable)Session["dtPdfstock"];
        if (Session["dtPdfstock"] != null)
        {
            table2 = new PdfPTable(dtPdfstock.Columns.Count);
        }

        PdfPTable tblNoteSign = null;
        PdfPTable tblTotBillAmt = null;
        PdfPTable tblinwords = null;
        PdfPTable tblpay = null;
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

        table1 = new PdfPTable(10);
        table1.TotalWidth = 490f;
        table1.LockedWidth = true;
        table1.SetWidths(new float[] { 0.4f, 1.5f, 0.9f, 1.0f, 1.2f, 0.5f, 0.7f, 0.7f, 0.7f, 0.9f});

        table4 = new PdfPTable(11);
        table4.TotalWidth = 490f;
        table4.LockedWidth = true;
        table4.SetWidths(new float[] { 0.4f, 1.5f, 0.9f, 1.0f, 1.2f, 0.5f, 0.7f, 0.7f, 0.7f, 0.7f, 0.7f });



        table2 = new PdfPTable(2);
        table2.TotalWidth = 490f;
        table2.LockedWidth = true;
        table2.SetWidths(new float[] { 1.4f, 0.6f });



        tblNoteSign = new PdfPTable(2);
        tblNoteSign.TotalWidth = 490f;
        tblNoteSign.LockedWidth = true;
        tblNoteSign.SetWidths(new float[] { 0.8f, 0.4f });

        tblTotBillAmt = new PdfPTable(1);
        tblTotBillAmt.TotalWidth = 490f;
        tblTotBillAmt.LockedWidth = true;
        tblTotBillAmt.SetWidths(new float[] { 1f });

        tblpay = new PdfPTable(1);
        tblpay.TotalWidth = 490f;
        tblpay.LockedWidth = true;
        tblpay.SetWidths(new float[] { 1f });

        tblinwords = new PdfPTable(1);
        tblinwords.TotalWidth = 490f;
        tblinwords.LockedWidth = true;
        tblinwords.SetWidths(new float[] { 1f });


        SqlConnection con55 = new SqlConnection(strconn1);
        SqlCommand cmd55 = new SqlCommand("select sum(D_Rate) as D_Rate from tblProductsale where Invoiceno ='" + txtinvoicenor.Text + "'", con50);
        SqlDataAdapter da55 = new SqlDataAdapter(cmd50);
        DataSet ds55 = new DataSet();
        da50.Fill(ds55);

        string drate = ds55.Tables[0].Rows[0]["D_Rate"].ToString();

        double drate10 = Convert.ToDouble(drate);



        GridCell = new PdfPCell(new Phrase(new Chunk("SLNO", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table1.AddCell(GridCell);

        // GridCell = new PdfPCell(new Phrase(new Chunk("Productcode", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        // GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Product Name.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Expirydate.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Mf Name.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Group Name.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Batchno.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Rate.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Tax@%.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Qty.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Amt.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table1.AddCell(GridCell);
        table1.SpacingAfter = 5f;
       

       


        //******************************************************************************************************************************************************************

        GridCell = new PdfPCell(new Phrase(new Chunk("SLNO", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table4.AddCell(GridCell);

        // GridCell = new PdfPCell(new Phrase(new Chunk("Productcode", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        // GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Product Name.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table4.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Expirydate.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table4.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Mf Name.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table4.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Group Name.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table4.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Batchno.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table4.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Rate.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table4.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Tax@%.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table4.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Qty.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table4.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Discnt.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table4.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Amt.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table4.AddCell(GridCell);
        table4.SpacingAfter = 5f;

        //******************************************************************************************************************************************************************


        if (dtPdfstock != null)
        {
            for (int i = 0; i < dtPdfstock.Rows.Count; i++)
            {


                for (int row1 = 0; row1 < dtPdfstock.Columns.Count; row1++)
                {

                    GridCell = new PdfPCell(new Phrase(new Chunk(dtPdfstock.Rows[i][row1].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment =Element.ALIGN_LEFT;
                    GridCell.PaddingBottom = 5f;
                    table1.AddCell(GridCell);

                }
            }
        }


        if (dtPdfstock1 != null)
        {
            for (int i = 0; i < dtPdfstock1.Rows.Count; i++)
            {


                for (int row1 = 0; row1 < dtPdfstock1.Columns.Count; row1++)
                {

                    GridCell = new PdfPCell(new Phrase(new Chunk(dtPdfstock1.Rows[i][row1].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    GridCell.PaddingBottom = 5f;
                    table4.AddCell(GridCell);

                }
            }
        }


        //if (dtPdfstock != null)
        //{
        //    for (int i = 0; i < dtPdfstock.Rows.Count; i++)
        //    {


        //        for (int row1 = 0; row1 < dtPdfstock.Columns.Count; row1++)
        //        {

        //            GridCell = new PdfPCell(new Phrase(new Chunk(dtPdfstock.Rows[i][row1].ToString(), FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK))));
        //            GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
        //            GridCell.PaddingBottom = 5f;
        //            table4.AddCell(GridCell);

        //        }
        //    }
        //}



        DateTime dtstrDate2 = DateTime.Now;

        DataSet dslogin = clsgd.GetcondDataSet("*", "tblLogin", "username", Session["username"].ToString());
        // DataSet dsbcode = Clsbllgeneral.GetcondDataSet("*", "emp_det", "emp_code", dslogin.Tables[0].Rows[0]["emp_code"].ToString());

        // DataSet dsBranchDetails1 = Clsbllgeneral.GetcondDataSet("*", "branch_det", "branch_code", dsbcode.Tables[0].Rows[0]["branch_code"].ToString());

        tblstock.AddCell(PhraseCell(new Phrase("CASH/CREDIT BILL\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
        cell.Colspan = 2;
        cell.PaddingBottom = 30f;
        tblstock.AddCell(cell);



        tbldt.AddCell(PhraseCell(new Phrase("Doctor Name :" + dcname, FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
        tbldt.AddCell(PhraseCell(new Phrase("Patient Name :" + pname, FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
        cell.Colspan = 2;
        cell.PaddingBottom = 30f;
        tbldt.AddCell(cell);
        tbldt.SpacingAfter = 5f;

        /* tbldt.AddCell(PhraseCell(new Phrase("Patient Name :" + pname, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
         cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_RIGHT);
         cell.Colspan = 2;
         cell.PaddingBottom = 28f;
         tbldt.AddCell(cell);*/






        phrase = new Phrase();
        phrase.Add(new Chunk(oALHospDetails[0].ToString() + "\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)));
        phrase.Add(new Chunk(oALHospDetails[1].ToString() + "\n" + oALHospDetails[2].ToString() + "\n" + oALHospDetails[3].ToString() + "\n\n", FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK)));
        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
        cell.HorizontalAlignment = 0;
        table2.AddCell(cell);

        //sqlFormattedDate=

        DateTime sqlFormattedDate1 = Convert.ToDateTime(sqlFormattedDate);
        string sqlFormattedDate2 = sqlFormattedDate1.ToString("dd-MM-yyyy");

        SqlConnection con56 = new SqlConnection(strconn1);
        SqlCommand cmd56 = new SqlCommand("select Max(STransno) as STransno from tblProductsale where Login_name='" + Session["username"].ToString() + "'", con50);
        SqlDataAdapter da56 = new SqlDataAdapter(cmd56);
        DataSet ds56 = new DataSet();
        da56.Fill(ds56);

        string STransno1 = ds56.Tables[0].Rows[0]["STransno"].ToString();



        phrase = new Phrase();
        phrase.Add(new Chunk("Bill No. :" + txtinvoicenor.Text + "\n" + "Date :" + sqlFormattedDate2 + "\n", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)));
        cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
        cell.HorizontalAlignment = 0;
        table2.AddCell(cell);



        // tblNoteSign.AddCell(PhraseCell(new Phrase("\n\n" + "Printed By " + "\n" + "(" + dsbcode.Tables[0].Rows[0]["emp_name"].ToString() + ")" + "\n\n", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
        // cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
        // cell.PaddingBottom = 30f;
        // tblNoteSign.AddCell(cell);


        phrase = new Phrase();
        phrase.Add(new Chunk(oALHospDetails[0].ToString() + "\n", FontFactory.GetFont("Times", 14, Font.BOLD, BaseColor.BLACK)));
        phrase.Add(new Chunk(oALHospDetails[1].ToString() + "\n" + oALHospDetails[2].ToString() + "\n" + oALHospDetails[3].ToString() + "\n\n", FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)));
        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
        cell.HorizontalAlignment = 0;
        table2.AddCell(cell);



        tblTotBillAmt.AddCell(PhraseCell(new Phrase("Product Amount:" + "Rs." + pamount + "\n", FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
        tblTotBillAmt.AddCell(PhraseCell(new Phrase("Discount Amount : " + "Rs." + discount1 + "\n", FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
        tblTotBillAmt.AddCell(PhraseCell(new Phrase("Tax Amount : " + "Rs." + tax1 + "\n", FontFactory.GetFont("Times", 8, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
        // tblTotBillAmt.AddCell(PhraseCell(new Phrase("Total Final Amount:" + "Rs." + ttamount + "\n", FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_RIGHT);
        //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        //cell.Border = 0;
        //cell.Colspan = 9;
        // cell.PaddingBottom = 30f;
        tblTotBillAmt.AddCell(cell);


        string bill = "Amount Paid";
        tblTotBillAmt.AddCell(PhraseCell(new Phrase("\n" + bill + " Rs. " + ttamount, FontFactory.GetFont("Times", 8, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_RIGHT);
        // cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        // cell.Border = 0;
        // cell.Colspan = 9;
        //cell.PaddingBottom = 200f;
        tblTotBillAmt.AddCell(cell);









        double doubTotal = Convert.ToDouble(ttamount);
        string strNumToEng = NumToEng.changeNumericToWords(doubTotal);

        tblTotBillAmt.AddCell(PhraseCell(new Phrase("\n" + "Amount In Words :   " + strNumToEng + " Only.", FontFactory.GetFont("Times", 8, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
        cell.Colspan = 2;
        cell.PaddingBottom = 750f;
        tblTotBillAmt.AddCell(cell);


        tblTotBillAmt.AddCell(PhraseCell(new Phrase("\n\n" + "Printed By " + "\n" + "(" + oALHospDetails[0].ToString() + ")" + "\n\n", FontFactory.GetFont("Times", 8, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
        cell.Colspan = 2;
       cell.PaddingBottom = 700f;
        tblTotBillAmt.AddCell(cell);

        tblTotBillAmt.AddCell(PhraseCell(new Phrase("E & OE" + "\n\n", FontFactory.GetFont("Times", 8, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
        cell.Colspan = 2;
       // cell.PaddingBottom = 30f;
        tblTotBillAmt.AddCell(cell);










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

       
        if (drate10 > 0.0)
        {
            document.Add(table4);
        }
        else
        {
            document.Add(table1);

        }





       
        document.Add(tblTotBillAmt);
        document.Add(tblpay);
        document.Add(tblinwords);
        document.Add(tblNoteSign);
        grprodsaledetails.DataSource = null;
        dtPdfstock.Rows.Clear();
        document.Close();
        //Response.Clear();

        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", "attachment; filename=Productsale.pdf");

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

        txtinvoicenor.Visible = false;
    }

    protected void ddlpaytype_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlpaytype.SelectedItem.Text == "Credit Card")
        {

            Double sum = 0;
            Double add = 0.0;
            Double discount1 = 0.0;
            Double sumdisc = 0.0;
            Double taxrate1 = 0.0;
            Double addtax = 0.0;

            for (int k = 0; k < Gridview1.Rows.Count; k++)
            {

                if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text))
                {
                    add = 0.0;

                }
                else
                {

                    add = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text);
                    sum = sum + add;
                }


                if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text))
                {
                    taxrate1 = 0.0;

                }
                else
                {

                    taxrate1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text);
                    addtax = addtax + taxrate1;
                }






                txtpramount.Text = (sum).ToString();

                string p_flag2 = ddlpaytype.SelectedItem.Text;

                DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblSalecardtype", "Saletype", p_flag2);

                double flag2 = Convert.ToDouble(dsgroup10.Tables[0].Rows[0]["Extraamount"].ToString());

                if (txtdiscount.Text == "0")
                {
                    double ctax = Convert.ToDouble(txttax.Text);
                    double cpamount = Convert.ToDouble(txtpramount.Text);
                    double csamount = (cpamount + ctax);
                    txttotalamount.Text = Convert.ToString(csamount);

                    double amttot = Convert.ToDouble(txttotalamount.Text);

                    double cardamt = (amttot * flag2) / 100;

                    string scardant = Convert.ToString(cardamt);

                    double camount = amttot + (amttot * flag2) / 100;

                    lblcardamount.Text = Convert.ToString(cardamt);


                    string camount10 = camount.ToString("F");

                    txtcramount.Text = Convert.ToString(camount10);

                    txttotalamount.Text = Convert.ToString(camount10);


                }

                else
                {

                    double ctax = Convert.ToDouble(txttax.Text);
                    double cpamount = Convert.ToDouble(txtpramount.Text);

                    double cdisc = Convert.ToDouble(txtdiscount.Text);
                    txttotalamount.Text = Convert.ToString(cpamount + ctax - cdisc);
                   // double csamount = (cpamount + ctax);
                   // txttotalamount.Text = Convert.ToString(csamount);

                    double amttot = Convert.ToDouble(txttotalamount.Text);

                    double cardamt = (amttot * flag2) / 100;

                    string scardant = Convert.ToString(cardamt);

                    double camount = amttot + (amttot * flag2) / 100;

                    lblcardamount.Text = Convert.ToString(cardamt);


                    string camount10 = camount.ToString("F");

                    txtcramount.Text = Convert.ToString(camount10);

                    txttotalamount.Text = Convert.ToString(camount10);

                    





                }

            }

         
        }

     
        //else
        //{
        //    //lblbillnor.Enabled = false;
        //    //lblbillnor.Text = invoiceno;
        //    lblvbillno.Enabled = false;
        //    lblvbillno.Text = invoiceno;
        //    Panel3.Visible = false;
        //}

        if (ddlpaytype.SelectedItem.Text == "Debit card")
        {

            Double sum = 0;
            Double add = 0.0;
            Double discount1 = 0.0;
            Double sumdisc = 0.0;
            Double taxrate1 = 0.0;
            Double addtax = 0.0;

            for (int k = 0; k < Gridview1.Rows.Count; k++)
            {

                if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text))
                {
                    add = 0.0;

                }
                else
                {

                    add = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtproamount") as TextBox).Text);
                    sum = sum + add;
                }


                if (String.IsNullOrEmpty((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text))
                {
                    taxrate1 = 0.0;

                }
                else
                {

                    taxrate1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txttaxrate") as TextBox).Text);
                    addtax = addtax + taxrate1;
                }






                txtpramount.Text = (sum).ToString();

                string p_flag2 = ddlpaytype.SelectedItem.Text;

                DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblSalecardtype", "Saletype", p_flag2);

                double flag2 = Convert.ToDouble(dsgroup10.Tables[0].Rows[0]["Extraamount"].ToString());


                double amttot = Convert.ToDouble(txttotalamount.Text);

                double cardamt = (amttot * flag2) / 100;

                lblcardamount.Text = Convert.ToString(cardamt);

                string scardant = Convert.ToString(cardamt);

                double camount = amttot + (amttot * flag2) / 100;

                 string  camount10 =  camount.ToString("F");

                  txtcramount.Text = Convert.ToString(camount10);

                  txttotalamount.Text = Convert.ToString(camount10);

               // txtcramount.Text = Convert.ToString(camount);

               // txttotalamount.Text = Convert.ToString(camount);

            }

           
        }
        //else
        //{
        //    //lblbillnor.Enabled = false;
        //    //lblbillnor.Text = invoiceno;
        //    lblvbillno.Enabled = false;
        //    lblvbillno.Text = invoiceno;
        //    Panel3.Visible = false;
        //}

        txtcardno.Focus();


         



       
    }




    protected void txttransno_TextChanged(object sender, EventArgs e)
    {
        btnsave.Focus();
       
    }
}