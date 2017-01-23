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
using System.Web.UI.WebControls.WebParts;
using System.Web.Services;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Collections.Specialized;
using System.Net.NetworkInformation;
using System.Management;
using System.Drawing;

public partial class Stockinhandentry : System.Web.UI.Page
{
    ClsBLLGeneraldetails clsgd = new ClsBLLGeneraldetails();
    ClsBALProductMaster ClsBLGP = new ClsBALProductMaster();
    ClsBALtempproductinwrad ClsBLGP1 = new ClsBALtempproductinwrad();

    ClsBALTransaction ClsBLGP2 = new ClsBALTransaction();

    ClsBALSupplieraccount clsSup = new ClsBALSupplieraccount();
    ClsBALProductinward Clsprdinw = new ClsBALProductinward();
    //ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string button_select;
    protected static string strconn = Dbconn.conmenthod();
    protected static string strconn1 = Dbconn.conmenthod();

    string sMacAddress = "";

    protected static string a = "";

    ArrayList arryno = new ArrayList();

    ArrayList arryname = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblsuccess.Visible = false;
        lblerror.Visible = false;
        if (!Page.IsPostBack)
        {

            supplier();

            //BindData();
            SetInitialRow();
            //txtinvoiceno.Focus();

            ddlsupplier.Focus();

            //ddpaymenttype.BorderColor = System.Drawing.Color.Black;
            //ddpaymenttype.BorderWidth = 1;
            //ddpaymenttype.BorderStyle = BorderStyle.Dotted;
            // txtinvoiceno.Enabled = true;
            //txtinvoiceno.Focus();
            System.DateTime Dtnow1 = DateTime.Now;





            lblgroupname.Visible = false;
            lblgenericcode.Visible = false;
            lblchemcode.Visible = false;
            lblmedicine.Visible = false;
            lblunit.Visible = false;
            lblform.Visible = false;
            lblmanufacture.Visible = false;
            lblshelf.Visible = false;
            lblrock.Visible = false;

            btnsave.Enabled = true;

            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                if (String.IsNullOrEmpty((Gridview1.Rows[i].Cells[1].FindControl("txtexpiredate") as TextBox).Text))
                {
                    System.DateTime Dtnow = DateTime.Now;
                    string Sysdatetime1 = Dtnow1.ToString("dd/MM/yyyy");
                    TextBox box = (TextBox)Gridview1.Rows[i].Cells[5].FindControl("txtexpiredate");
                   //TextBox box10 = (TextBox)Gridview1.Rows[i].Cells[4].FindControl("txtinvoicedate");
                    box.Text = Sysdatetime1;
                    //box10.Text = Sysdatetime1;
                }
                TextBox box1 = (TextBox)Gridview1.Rows[i].Cells[1].FindControl("txtproductcode");
                box1.BackColor = Color.DarkOliveGreen;
                box1.ForeColor = Color.DarkOliveGreen;

                TextBox box2 = (TextBox)Gridview1.Rows[i].Cells[1].FindControl("txtbatchno");
                box2.BackColor = Color.DarkSlateGray;
                box2.ForeColor = Color.DarkSlateGray;

                TextBox box100 = (TextBox)Gridview1.Rows[i].Cells[7].FindControl("txttax");
                TextBox box101 = (TextBox)Gridview1.Rows[i].Cells[9].FindControl("txttaxamount");
                TextBox box102 = (TextBox)Gridview1.Rows[i].Cells[10].FindControl("txtproductvalue");
                //box100.BorderColor = System.Drawing.Color.Orange;
                //box100.BorderWidth = 1;
                //box100.BorderStyle = BorderStyle.Solid;
                box101.Enabled = false;
                box102.Enabled = false;
                box100.Enabled = false;



            }



        }
        GetMACAddress();

    }

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
        ddlsupplier.DataSource = arryno;
        ddlsupplier.DataBind();
        //ddGecode.Focus();

    }

    protected void ddlsupplier_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (ddlsupplier.SelectedItem.Text == "-Select-")
        {
            Master.ShowModal("Please select a Supplier Name . !!!", "ddlsupplier", 1);
            return;
        }

        DataSet dsgroup2 = clsgd.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", ddlsupplier.SelectedItem.Text);
        // int code = Convert.ToInt32(dsgroup2.Tables[0].Rows[0]["SupplierCode"].ToString());
        //lblsuppliercode.Text = Convert.ToString(code);
        lblsuppliercode.Text = dsgroup2.Tables[0].Rows[0]["SupplierCode"].ToString();



        (Gridview1.Rows[0].Cells[1].FindControl("txtinvoiceno") as TextBox).Focus();

    }

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("InvoiceNo", typeof(string)));
        dt.Columns.Add(new DataColumn("InvoiceDate", typeof(string)));
        dt.Columns.Add(new DataColumn("Productcode", typeof(string)));
        dt.Columns.Add(new DataColumn("ProductName", typeof(string)));
        dt.Columns.Add(new DataColumn("Batchid", typeof(string)));
        dt.Columns.Add(new DataColumn("Expiredate", typeof(string)));
        dt.Columns.Add(new DataColumn("Stockinward", typeof(string)));
        dt.Columns.Add(new DataColumn("Freesupply", typeof(string)));
        dt.Columns.Add(new DataColumn("Tax", typeof(string)));
        dt.Columns.Add(new DataColumn("Purchaseprice", typeof(string)));
        dt.Columns.Add(new DataColumn("MRP", typeof(string)));
        dt.Columns.Add(new DataColumn("TaxAmount", typeof(string)));
        dt.Columns.Add(new DataColumn("Totalvalues", typeof(string)));


        dr = dt.NewRow();

        dr["RowNumber"] = 1;
        dr["InvoiceNo"] = string.Empty;
        dr["InvoiceDate"] = string.Empty;
        dr["Productcode"] = string.Empty;
        dr["ProductName"] = string.Empty;
        dr["Batchid"] = string.Empty;
        dr["Expiredate"] = string.Empty;
        dr["Stockinward"] = string.Empty;
        dr["Freesupply"] = string.Empty;
        dr["Tax"] = string.Empty;
        dr["Purchaseprice"] = string.Empty;
        dr["MRP"] = string.Empty;
        dr["TaxAmount"] = string.Empty;
        dr["Totalvalues"] = string.Empty;

        dt.Rows.Add(dr);
        //dr = dt.NewRow();

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;

        Gridview1.DataSource = dt;
        Gridview1.DataBind();

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
                 TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtinvoiceno");
                 TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtinvoicedate");
                 TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                 TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");
                 TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtbatchno");
                 TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                 TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival");
                 TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply");
                 TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                 TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtpurchaseprice");
                 TextBox box10 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("txtMRP");
                 TextBox box11 = (TextBox)Gridview1.Rows[rowIndex].Cells[10].FindControl("txtTaxamount");
                 TextBox box12 = (TextBox)Gridview1.Rows[rowIndex].Cells[11].FindControl("txtproductvalue");

                 
                 box0.Text = dt.Rows[i]["InvoiceNo"].ToString();
                 box1.Text = dt.Rows[i]["InvoiceDate"].ToString();
                 box2.Text = dt.Rows[i]["Productcode"].ToString();
                 box3.Text = dt.Rows[i]["ProductName"].ToString();
                 box4.Text = dt.Rows[i]["Batchid"].ToString();
                 box5.Text = dt.Rows[i]["Expiredate"].ToString();
                 box6.Text = dt.Rows[i]["Stockinward"].ToString();
                 box7.Text = dt.Rows[i]["Freesupply"].ToString();
                 box8.Text = dt.Rows[i]["Tax"].ToString();
                 box9.Text = dt.Rows[i]["Purchaseprice"].ToString();
                 box10.Text = dt.Rows[i]["MRP"].ToString();
                 box11.Text = dt.Rows[i]["TaxAmount"].ToString();
                 box12.Text = dt.Rows[i]["Totalvalues"].ToString();


                 rowIndex++;

             }
         }
         // ViewState["CurrentTable"] = dt;

     }
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
                    TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtinvoiceno");
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtinvoicedate");
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode");
                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");
                    TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtbatchno");
                    TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                    TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival");
                    TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply");
                    TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                    TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtpurchaseprice"); 
                    TextBox box10 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("txtMRP");
                    TextBox box11 = (TextBox)Gridview1.Rows[rowIndex].Cells[10].FindControl("txtTaxamount");
                    TextBox box12 = (TextBox)Gridview1.Rows[rowIndex].Cells[11].FindControl("txtproductvalue");

                    string invn = box0.Text;




                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;
                     //dtCurrentTable.Rows[i - 1]["productname"] = box0.Text;
                   // drCurrentRow["Productcode"] = box0.Text;
                   // drCurrentRow["ProductName"] = box1.Text;
                   // drCurrentRow["Batchid"] = box2.Text;
                   // drCurrentRow["Expiredate"] = box3.Text;
                   // drCurrentRow["Stockinward"] = box4.Text;
                  //  drCurrentRow["Freesupply"] = box5.Text;
                  //  drCurrentRow["Tax"] = box6.Text;
                   // drCurrentRow["Purchaseprice"] = box7.Text;
                   // drCurrentRow["MRP"] = box8.Text;
                   // drCurrentRow["TaxAmount"] = box9.Text;
                   // drCurrentRow["Totalvalues"] = box10.Text;
                    dtCurrentTable.Rows[i - 1]["InvoiceNo"] = invn;
                    dtCurrentTable.Rows[i - 1]["InvoiceDate"] = box1.Text;
                    dtCurrentTable.Rows[i - 1]["Productcode"] = box2.Text; 
                     dtCurrentTable.Rows[i - 1]["productname"] = box3.Text;
                    dtCurrentTable.Rows[i - 1]["Batchid"] = box4.Text;
                     dtCurrentTable.Rows[i - 1]["Expiredate"] = box5.Text;
                     dtCurrentTable.Rows[i - 1]["Stockinward"] = box6.Text;
                      dtCurrentTable.Rows[i - 1]["Freesupply"] = box7.Text;
                       dtCurrentTable.Rows[i - 1]["Tax"] = box8.Text;
                         dtCurrentTable.Rows[i - 1]["Purchaseprice"] = box9.Text;
                         dtCurrentTable.Rows[i - 1]["MRP"] = box10.Text;
                         dtCurrentTable.Rows[i - 1]["TaxAmount"] = box11.Text;
                          dtCurrentTable.Rows[i - 1]["Totalvalues"] = box12.Text;
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


    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        string invoiceno = (Gridview1.Rows[0].Cells[1].FindControl("txtinvoiceno") as TextBox).Text;
        string invoicedate = (Gridview1.Rows[0].Cells[1].FindControl("txtinvoicedate") as TextBox).Text;
        string productcode = (Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Text;
        string productname = (Gridview1.Rows[0].Cells[1].FindControl("txtproductname") as TextBox).Text;
        string batcno = (Gridview1.Rows[0].Cells[1].FindControl("txtbatchno") as TextBox).Text;
        string expdate = (Gridview1.Rows[0].Cells[1].FindControl("txtexpiredate") as TextBox).Text;
        string stockarrival = (Gridview1.Rows[0].Cells[1].FindControl("txtstockarrival") as TextBox).Text;
        string freesupply = (Gridview1.Rows[0].Cells[1].FindControl("txtfreesupply") as TextBox).Text;
        string purchaseprice = (Gridview1.Rows[0].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text;
        string mrp = (Gridview1.Rows[0].Cells[1].FindControl("txtMRP") as TextBox).Text;

        if (ddlsupplier.SelectedItem.Text == "-Select-")
        {
            Master.ShowModal("Please select a supplier. !!!", "ddlsupplier", 1);
            return;
        }


        if (productcode == "")
        {
            ShowPopupMessage("Enter Product Code", PopupMessageType.txtproductcode);
            return;
        }
        if (productname == "")
        {
            ShowPopupMessage("Enter Product Name", PopupMessageType.txtproductname);
            return;

        }
        //////classic/////////
        if (batcno == "")
        {
            ShowPopupMessage("Enter Batch No. !!!", PopupMessageType.txtbatchno);
            return;


        }

       

        if (expdate == "")
        {

            //ShowPopupMessage("Enter Batch No. !!!", PopupMessageType.txtbatchno);
            ShowPopupMessage("Enter Expire Date. !!!", PopupMessageType.txtexpiredate);
            return;


        }
         if (batcno == "")
         {
             ShowPopupMessage("Enter Batchno", PopupMessageType.txtbatchno);
             return;

         }

         if (expdate == "")
         {
             ShowPopupMessage("Enter Expire Date", PopupMessageType.txtexpiredate);
             return;

         }
        if (stockarrival == "")
        {
            ShowPopupMessage("Enter Stock Arrival", PopupMessageType.txtstockarrival);
            return;

        }

        if (freesupply == "")
        {
             ShowPopupMessage("Enter Free Supply", PopupMessageType.txtfreesupply);
            return;
        }

        if (mrp == "")
        {
            ShowPopupMessage("Enter MRP", PopupMessageType.txtMRP);
            return;
        }
        Button txt = (Button)sender;
        GridViewRow row = (GridViewRow)txt.NamingContainer;

       // (Gridview1.Rows[row.RowIndex].Cells[1].FindControl("Buttonsave") as Button).Visible = false;


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
                    TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtinvoiceno");
                    rowIndex++;
                    box0.Focus();
                }
            }
        }
    }

    private string ShowPopupMessage(string message, PopupMessageType messageType)
    {
        switch (messageType)
        {
            /* case PopupMessageType.txtbatchno:
                 lblMessagePopupHeading.Text = "Error";
                 //Render image in literal control
                
                 a = "txtbatchno";
                // int b = Convert.ToInt16(a);


                 break;*/

            case PopupMessageType.txtproductname:
                lblMessagePopupHeading.Text = "Error";
                a = "txtproductname";
                break;

            case PopupMessageType.txtproductcode:
                lblMessagePopupHeading.Text = "Error";
                a = "txtproductcode";
                break;
            /*  case PopupMessageType.txtexpiredate:
                  lblMessagePopupHeading.Text = "Error";
                  a = "txtexpiredate";*/

            // break;

            case PopupMessageType.txtstockarrival:
                lblMessagePopupHeading.Text = "Error";
                a = "txtstockarrival";

                break;

            case PopupMessageType.txtfreesupply:
                lblMessagePopupHeading.Text = "Error";
                a = "txtfreesupply";

                break;

            case PopupMessageType.ddltax:
                lblMessagePopupHeading.Text = "Error";
                a = "ddltax";

                break;
            case PopupMessageType.txtpurchaseprice:
                lblMessagePopupHeading.Text = "Error";
                a = "txtpurchaseprice";

                break;
            case PopupMessageType.txtMRP:
                lblMessagePopupHeading.Text = "Error";
                a = "txtMRP";

                break;

            case PopupMessageType.txttaxamount:
                lblMessagePopupHeading.Text = "Error";
                a = "txttaxamount";

                break;
            case PopupMessageType.txtproductvalue:
                lblMessagePopupHeading.Text = "Error";
                a = "txttaxamount";

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
        txtproductcode,
        txtproductname,
        txtbatchno,
        txtexpiredate,
        txtstockarrival,
        txtfreesupply,
        ddltax,
        txtpurchaseprice,
        txtMRP,
        txttaxamount,
        txtproductvalue
    }

    protected void txtproductcode_TextChanged(object sender, EventArgs e)
    {
        try
        {

            string filename = Dbconn.Mymenthod();
            if (!File.Exists(filename))
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text);

                    string close_flag = "Y";
                    DataSet dschm = clsgd.GetcondDataSet9("*", "tblProductMaster", "Productcode", ID);
                    if (dschm.Tables[0].Rows.Count > 0)
                    {
                        string gcode1 = dschm.Tables[0].Rows[0]["g_code"].ToString();

                        DataSet dschm10 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", gcode1, "p_flag", close_flag);


                        if (dschm10.Tables[0].Rows.Count > 0)
                        {


                            DataSet dschm20 = clsgd.GetcondDataSet9("*", "tblProductMaster", "Productcode", ID);
                            ((Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text) = Convert.ToString(dschm20.Tables[0].Rows[0]["Productname"].ToString());
                            lblgroupname.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["g_code"].ToString());
                            lblgenericcode.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Genericcode"].ToString());
                            lblchemcode.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Cemcode"].ToString());
                            lblmedicine.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Medcode"].ToString());
                            lblunit.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Unit"].ToString());
                            lblform.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Form"].ToString());
                            lblmanufacture.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Manufacturer"].ToString());
                            lblshelf.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Shelf"].ToString());
                            lblrock.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Row"].ToString());
                            lblsuplier.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Suppname"].ToString());
                        }
                        else
                        {
                            DataSet dschm20 = clsgd.GetcondDataSet9("*", "tblProductMaster", "Productcode", ID);
                            ((Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text) = Convert.ToString(dschm20.Tables[0].Rows[0]["Productname"].ToString());
                            lblgroupname.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["g_code"].ToString()); ;
                            lblgenericcode.Text = "0";
                            lblchemcode.Text = "0";
                            lblmedicine.Text = "0";
                            lblunit.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Unit"].ToString());
                            lblform.Text = "0";
                            lblmanufacture.Text = "0";
                            lblshelf.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Shelf"].ToString());
                            lblrock.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Row"].ToString());
                            lblsuplier.Text = "0";

                        }
                    }
                    else
                    {
                        // OR do nothing
                        ShowPopupMessage("Product name does not exist", PopupMessageType.txtproductcode);
                        (Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text = string.Empty;
                        (Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Focus();

                        return;

                    }


                   /* DataSet dsprodin = clsgd.GetcondDataSet("*", "tblProductMaster", "g_code", lblgroupname.Text);
                    string g_code = Convert.ToString(dsprodin.Tables[0].Rows[0]["g_code"].ToString());
                    DataSet dsprodin10 = clsgd.GetcondDataSet("*", "tblGroup", "g_code", g_code);
                    string gcode = Convert.ToString(dsprodin10.Tables[0].Rows[0]["g_code"].ToString());

                    DataSet dsprodin12 = clsgd.GetcondDataSet("*", "tblTax_Rate", "g_code", gcode);
                    string Tax_Rate = Convert.ToString(dsprodin12.Tables[0].Rows[0]["Tax_Rate"].ToString());

                    if (dsprodin12.Tables[0].Rows.Count > 0)
                    {

                        ((Gridview1.Rows[i].Cells[1].FindControl("txttax") as TextBox).Text) = Tax_Rate.ToString();


                    }*/
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


                    // (Gridview1.Rows[i].Cells[1].FindControl("txtbatchno") as TextBox).Focus();


                }
                string pharmflag = "Y";
                //DataSet pharm = clsgd.GetcondDataSet("*", "tblProductMaster", "Pharmflag", pharmflag);
                for (int j = 0; j < Gridview1.Rows.Count; j++)
                {
                    string ID1 = Convert.ToString((Gridview1.Rows[j].Cells[1].FindControl("txtproductcode") as TextBox).Text);

                    DataSet pharm1 = clsgd.GetcondDataSet2("*", "tblProductMaster", "Pharmflag", pharmflag, "Productcode", ID1);

                    if (pharm1.Tables[0].Rows.Count > 0)
                    {


                        TextBox txt = (TextBox)sender;
                        GridViewRow row = (GridViewRow)txt.NamingContainer;

                        //string productcode = Convert.ToString((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtproductcode") as TextBox).Text);
                        ((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtbatchno") as TextBox).Text) = Convert.ToString("0");

                        TextBox batchidno = (TextBox)Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtbatchno");

                        // batchidno.Attributes.Add("onfocusin", "select();");
                        (Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtbatchno") as TextBox).Focus();
                        batchidno.Attributes["onfocus"] = "javascript:this.select();";

                        System.DateTime Dtnow1 = DateTime.Now;
                        string Sysdatetime1 = Dtnow1.ToString("dd/MM/yyyy");
                        ((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtexpiredate") as TextBox).Text) = Sysdatetime1;
                        (Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtexpiredate") as TextBox).Enabled = true;


                    }
                    else
                    {
                        TextBox txt = (TextBox)sender;
                        GridViewRow row = (GridViewRow)txt.NamingContainer;
                        ((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtbatchno") as TextBox).Text) = Convert.ToString("0");
                        ((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtexpiredate") as TextBox).Text) = "01/01/1900";
                        (Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtexpiredate") as TextBox).Enabled = false;
                        (Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtstockarrival") as TextBox).Focus();
                    }
                    //SetPreviousData();


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
                        string commandString = "SELECT Productname,Groupname,Genericcode,Cemcode,Medcode,Unit,Form,Manufacturer,Shelf,Row,Suppname FROM tblProductMaster " +
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
                            lblgroupname.Text = dr["Groupname"].ToString();
                            lblgenericcode.Text = dr["Genericcode"].ToString();
                            lblchemcode.Text = dr["Cemcode"].ToString();
                            lblmedicine.Text = dr["Medcode"].ToString();
                            lblunit.Text = dr["Unit"].ToString();
                            lblform.Text = dr["Form"].ToString();
                            lblmanufacture.Text = dr["Manufacturer"].ToString();
                            lblshelf.Text = dr["Shelf"].ToString();
                            lblrock.Text = dr["Row"].ToString();
                            lblsuplier.Text = dr["Suppname"].ToString();

                        }
                        else
                        {
                            // Do something if no user is found
                            // OR do nothing
                            ShowPopupMessage("Product code does not exist", PopupMessageType.txtproductcode);
                            return;
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


        TextBox txt1 = (TextBox)sender;
        GridViewRow row1 = (GridViewRow)txt1.NamingContainer;
        // Gridview1.Rows[row1.RowIndex].FindControl("txtdiscount").Focus();

     
        Gridview1.Rows[row1.RowIndex].FindControl("txtproductname").Focus();
        ((Gridview1.Rows[row1.RowIndex].FindControl("txtfreesupply") as TextBox).Text) = "0";




  }


    protected void txtproductname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string filename = Dbconn.Mymenthod();
            if (!File.Exists(filename))
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text);

                    string close_flag = "Y";
                    DataSet dschm = clsgd.GetcondDataSet9("*", "tblProductMaster", "Productname", ID);
                    if (dschm.Tables[0].Rows.Count > 0)
                    {
                        string gcode1 = dschm.Tables[0].Rows[0]["g_code"].ToString();

                        DataSet dschm10 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", gcode1, "p_flag", close_flag);


                        if (dschm10.Tables[0].Rows.Count > 0)
                        {


                            DataSet dschm20 = clsgd.GetcondDataSet9("*", "tblProductMaster", "Productname", ID);
                            ((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text) = Convert.ToString(dschm20.Tables[0].Rows[0]["Productcode"].ToString());
                            lblgroupname.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["g_code"].ToString());
                            lblgenericcode.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Genericcode"].ToString());
                            lblchemcode.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Cemcode"].ToString());
                            lblmedicine.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Medcode"].ToString());
                            lblunit.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Unit"].ToString());
                            lblform.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Form"].ToString());
                            lblmanufacture.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Manufacturer"].ToString());
                            lblshelf.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Shelf"].ToString());
                            lblrock.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Row"].ToString());
                            lblsuplier.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Suppname"].ToString());
                        }
                        else
                        {
                            DataSet dschm20 = clsgd.GetcondDataSet9("*", "tblProductMaster", "Productname", ID);
                            ((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text) = Convert.ToString(dschm20.Tables[0].Rows[0]["Productcode"].ToString());
                            lblgroupname.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["g_code"].ToString()); ;
                            lblgenericcode.Text = "0";
                            lblchemcode.Text = "0";
                            lblmedicine.Text = "0";
                            lblunit.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Unit"].ToString());
                            lblform.Text = "0";
                            lblmanufacture.Text = "0";
                            lblshelf.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Shelf"].ToString());
                            lblrock.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Row"].ToString());
                            lblsuplier.Text = "0";

                        }
                    }
                    else
                    {
                        // OR do nothing
                        ShowPopupMessage("Product name does not exist", PopupMessageType.txtproductcode);
                        (Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text = string.Empty;
                        (Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Focus();

                        return;

                    }


                   // DataSet dsprodin = clsgd.GetcondDataSet("*", "tblProductMaster", "g_code", lblgroupname.Text);
                   // string g_code = Convert.ToString(dsprodin.Tables[0].Rows[0]["g_code"].ToString());
                   // DataSet dsprodin10 = clsgd.GetcondDataSet("*", "tblGroup", "g_code", g_code);
                   // string gcode = Convert.ToString(dsprodin10.Tables[0].Rows[0]["g_code"].ToString());

                   // DataSet dsprodin12 = clsgd.GetcondDataSet("*", "tblTax_Rate", "g_code", gcode);
                    //string Tax_Rate = Convert.ToString(dsprodin12.Tables[0].Rows[0]["Tax_Rate"].ToString());

                    //if (dsprodin12.Tables[0].Rows.Count > 0)
                   // {

                       // ((Gridview1.Rows[i].Cells[1].FindControl("txttax") as TextBox).Text) = Tax_Rate.ToString();


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


                    (Gridview1.Rows[i].Cells[1].FindControl("txtbatchno") as TextBox).Focus();
                }


                string pharmflag = "Y";
                //DataSet pharm = clsgd.GetcondDataSet("*", "tblProductMaster", "Pharmflag", pharmflag);
                for (int j = 0; j < Gridview1.Rows.Count; j++)
                {
                    string ID1 = Convert.ToString((Gridview1.Rows[j].Cells[1].FindControl("txtproductcode") as TextBox).Text);

                    DataSet pharm1 = clsgd.GetcondDataSet2("*", "tblProductMaster", "Pharmflag", pharmflag, "Productcode", ID1);

                    if (pharm1.Tables[0].Rows.Count > 0)
                    {


                        TextBox txt = (TextBox)sender;
                        GridViewRow row = (GridViewRow)txt.NamingContainer;

                        //string productcode = Convert.ToString((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtproductcode") as TextBox).Text);
                        ((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtbatchno") as TextBox).Text) = Convert.ToString("0");

                        TextBox batchidno = (TextBox)Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtbatchno");

                        // batchidno.Attributes.Add("onfocusin", "select();");
                        (Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtbatchno") as TextBox).Focus();
                        batchidno.Attributes["onfocus"] = "javascript:this.select();";

                        System.DateTime Dtnow1 = DateTime.Now;
                        string Sysdatetime1 = Dtnow1.ToString("dd/MM/yyyy");
                        ((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtexpiredate") as TextBox).Text) = Sysdatetime1;
                        (Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtexpiredate") as TextBox).Enabled = true;


                    }
                    else
                    {
                        TextBox txt = (TextBox)sender;
                        GridViewRow row = (GridViewRow)txt.NamingContainer;
                        ((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtbatchno") as TextBox).Text) = Convert.ToString("0");
                        ((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtexpiredate") as TextBox).Text) = "01/01/1900";
                        (Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtexpiredate") as TextBox).Enabled = false;
                        (Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtstockarrival") as TextBox).Focus();
                    }
                    //SetPreviousData();


                }


               // TextBox txt = (TextBox)sender;
               // GridViewRow row = (GridViewRow)txt.NamingContainer;

                //string productcode = Convert.ToString((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtproductcode") as TextBox).Text);
                //((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtbatchno") as TextBox).Text) = "0";
               // TextBox batchidno = (TextBox)Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtbatchno");
                //batchidno.Attributes.Add("onfocusin", "select();");
            }
            else
            {
                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    string ID = Convert.ToString((Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text);
                    //OleDbCommand com;
                    string strconn1 = Dbconn.conmenthod();

                    using (OleDbConnection conn = new OleDbConnection(strconn1))
                    {



                        //conn.Open();
                        string commandString = "SELECT Productname,Productcode,Groupname,Genericcode,Cemcode,Medcode,Unit,Form,Manufacturer,Shelf,Row,Suppname FROM tblProductMaster " +
                                                                 String.Format("WHERE (Productname = '{0}')", ID);
                        OleDbCommand cmd = new OleDbCommand(commandString, conn);
                        conn.Open();
                        OleDbDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            // Setup DataReader
                            dr.Read();

                            // Set DR values to Text fields
                            ((Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Text) = dr["Productcode"].ToString();
                            lblgroupname.Text = dr["Groupname"].ToString();
                            lblgenericcode.Text = dr["Genericcode"].ToString();
                            lblchemcode.Text = dr["Cemcode"].ToString();
                            lblmedicine.Text = dr["Medcode"].ToString();
                            lblunit.Text = dr["Unit"].ToString();
                            lblform.Text = dr["Form"].ToString();
                            lblmanufacture.Text = dr["Manufacturer"].ToString();
                            lblshelf.Text = dr["Shelf"].ToString();
                            lblrock.Text = dr["Row"].ToString();
                            lblsuplier.Text = dr["Suppname"].ToString();

                        }
                        else
                        {
                            // Do something if no user is found
                            // OR do nothing
                            ShowPopupMessage("Product name does not exist ", PopupMessageType.txtproductname);
                            return;
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


        TextBox txt1 = (TextBox)sender;
        GridViewRow row1 = (GridViewRow)txt1.NamingContainer;
        // Gridview1.Rows[row1.RowIndex].FindControl("txtdiscount").Focus();

        ((Gridview1.Rows[row1.RowIndex].FindControl("txtfreesupply") as TextBox).Text) = "0";



        // (Gridview1.Rows[0].Cells[1].FindControl("txtbatchno") as TextBox).Focus();
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
            string strconn1 = Dbconn.conmenthod();
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

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Buyername(string prefixText)
    {
        string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            string oConn = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(oConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Productcode from tblProductMaster where Productcode like @1+'%'", conn);
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
            OleDbCommand cmd = new OleDbCommand("select Productcode from tblProductMaster where Productcode like @1+'%'", conn);
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

        Response.Redirect("Home.aspx");
    }

    protected void txtstockarrival_TextChanged(object sender, EventArgs e)
    {

        try
        {

            TextBox txt = (TextBox)sender;
            GridViewRow row = (GridViewRow)txt.NamingContainer;

            string productcode = Convert.ToString((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtproductcode") as TextBox).Text);
            string Batchid = Convert.ToString((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtbatchno") as TextBox).Text);

            /* DataSet dsprodin = clsgd.GetcondDataSet2("*", "tblProductinward", "Productcode", productcode, "Batchid", Batchid);
             if (dsprodin.Tables[0].Rows.Count > 0)
             {
                 //Gridview1.Rows[row.RowIndex].FindControl("txtbatchno").Focus();
                 //Master.ShowModal("Different Batch Id", "txtbatchno", 1);
                 //Gridview1.Rows[row.RowIndex].FindControl("txtbatchno").Focus();
                 //return;
                 //Gridview1.Rows[row.RowIndex].FindControl("txtbatchno").Focus();
                 ShowPopupMessage("Different Batch Id", PopupMessageType.txtbatchno);
                 return;
                 //lblErrorMessage.Text = message;



             }*/



            /*DataSet dsgrp = clsgd.GetcondDataSet2("*", "tbltempproductinward", "productcode", productcode, "Batchid", Batchid);

            if (dsgrp.Tables[0].Rows.Count > 0)
            {
                // Master.ShowModal("Different Batch Id", "txtbatchno", 0);

                // return;
                ShowPopupMessage("Different Batch Id", PopupMessageType.txtbatchno);
                return;

                //  ShowModal1("Different Batch Id", "txtbatchno", 0);

            }*/


            for (int k = 0; k < Gridview1.Rows.Count; k++)
            {
                string productname1 = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtproductname") as TextBox).Text);
                string productcode1 = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtproductcode") as TextBox).Text);
                string productname2 = Convert.ToString((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtproductname") as TextBox).Text);


                DataSet dschm = clsgd.GetcondDataSet9("*", "tblProductMaster", "ProductName", productname2);
                string gcode = dschm.Tables[0].Rows[0]["g_code"].ToString();

                DataSet dschm10 = clsgd.GetcondDataSet9("*", "tblGroup", "g_code", gcode);
                string p_flag = dschm10.Tables[0].Rows[0]["p_flag"].ToString();

                // DataSet dschm11 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", gcode, "p_flag", p_flag);


                if (p_flag == "Y")
                {
                    string Batchid1 = Convert.ToString((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtbatchno") as TextBox).Text);
                    //int batchid12 = Convert.ToInt64(Batchid1);
                    if (Batchid1 == "0")
                    {
                        // Master.ShowModal("Pharmacy product batch id mandotory", "txtexpiredate", 1);
                        ShowPopupMessage("Pharmacy product batch id mandotory", PopupMessageType.txtbatchno);
                        return;
                    }

                }
            }

            //string invoiceamount1 = txtinvoiceamount.Text;
            //if (invoiceamount1 == "")
            //{
            //    Master.ShowModal("Enter invoice Amount", "txtinvoiceamount", 0);
            //    //ShowPopupMessage("Enter invoice Amount", PopupMessageType.);
            //    return;
            //}
            for (int k = 0; k < Gridview1.Rows.Count; k++)
            {
                string productname1 = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtproductname") as TextBox).Text);
                string stockarrival = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtstockarrival") as TextBox).Text);
                string freesupply = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtfreesupply") as TextBox).Text);
                string tax = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txttax") as TextBox).Text);
                string purchaseprice = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text);
                string MRP = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtMRP") as TextBox).Text);
                string expiredate1 = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtexpiredate") as TextBox).Text);

                if (freesupply != "" && purchaseprice != "" && MRP == "")
                {

                    //Master.ShowModal("MRP is Mandatory", "txtMRP", 1);
                    //return;
                    ShowPopupMessage("MRP is Mandatory", PopupMessageType.txtMRP);
                    return;
                }

                //DateTime today = DateTime.Now;
                //DateTime answer = today.AddDays(90);
                //DateTime expdate1 = Convert.ToDateTime(expiredate1);

                //string close_flag = "Y";
                //DataSet dschm = clsgd.GetcondDataSet9("*", "tblProductMaster", "ProductName", productname1);
                //string gcode = dschm.Tables[0].Rows[0]["g_code"].ToString();

                //DataSet dschm10 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", gcode, "p_flag", close_flag);


                //if (dschm10.Tables[0].Rows.Count > 0)
                //{

                //    if (expdate1 <= answer)
                //    {
                //        // Master.ShowModal("Expire Date minimum 90days greater than current  date", "txtexpiredate", 1);
                //        ShowPopupMessage("Expire Date minimum 90days greater than current  date", PopupMessageType.txtexpiredate);

                //        // (Gridview1.Rows[0].Cells[1].FindControl("txtexpiredate") as TextBox).Enabled = true;
                //        // (Gridview1.Rows[0].Cells[1].FindControl("txtexpiredate") as TextBox).Focus();
                //        return;

                //    }
                //}
            }


            string freesupply1 = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txtfreesupply") as TextBox).Text);
            // if (stockarrival != "" || freesupply != "" || tax != "" || purchaseprice != "")
            if (freesupply1 != "")
            {


                Double scv = 0.0;
                Double sllv = 0.0;
                Double productprice = 0.0;
                Double rateoftax = 0.0;
                Double mrp = 0.0;
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

                            //sllv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text);

                            if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text))
                            {
                                sllv = 0.0;
                            }
                            else
                            {

                                sllv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text);
                            }


                            if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply") as TextBox).Text))
                            {
                                scv = 0.0;
                            }
                            else
                            {

                                scv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply") as TextBox).Text);
                            }
                            Double balance = Convert.ToDouble(sllv - scv);
                            if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtpurchaseprice") as TextBox).Text))
                            {
                                productprice = 0.0;
                            }
                            else
                            {
                                productprice = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtpurchaseprice") as TextBox).Text);
                            }
                            if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[6].FindControl("txttax") as TextBox).Text))
                            {
                                rateoftax = 0.0;
                            }
                            else
                            {
                                rateoftax = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[6].FindControl("txttax") as TextBox).Text);
                            }

                            if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtMRP") as TextBox).Text))
                            {
                                mrp = 0.0;
                            }
                            else
                            {
                                mrp = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtMRP") as TextBox).Text);
                            }



                            ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txttaxamount") as TextBox).Text) = Convert.ToString(balance * productprice * rateoftax / 100);

                            double productcost = Convert.ToDouble(balance * productprice * rateoftax / 100);

                            double cost = Convert.ToDouble(balance * productprice);



                            ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproductvalue") as TextBox).Text) = Convert.ToString(productcost + cost);

                            if (rowIndex == '1')
                            {
                                (Gridview1.Rows[0].Cells[1].FindControl("txtfreesupply") as TextBox).Focus();
                            }
                            else
                            {

                                (Gridview1.Rows[rowIndex].Cells[1].FindControl("txtfreesupply") as TextBox).Focus();
                                rowIndex++;
                            }
                        }

                    }
                }

            }


            double sum = 0;

            for (int j = 0; j < Gridview1.Rows.Count; j++)
            {
                if (String.IsNullOrEmpty((Gridview1.Rows[j].Cells[1].FindControl("txtproductvalue") as TextBox).Text))
                {
                    Double add = 0.0;

                }
                else
                {

                    Double add = Convert.ToDouble((Gridview1.Rows[j].Cells[1].FindControl("txtproductvalue") as TextBox).Text);
                    sum = sum + add;
                }


            }
           
            // TextBox txt = (TextBox)sender;
            //GridViewRow row = (GridViewRow)txt.NamingContainer;
            Gridview1.Rows[row.RowIndex].FindControl("txtfreesupply").Focus();
            // txtothers.Text = "0";
            //double others2 = Convert.ToDouble(txtothers.Text);

        }

        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }

    }

    protected void txtfreesupply_TextChanged(object sender, EventArgs e)
    {

        try
        {





            for (int k = 0; k < Gridview1.Rows.Count; k++)
            {


                double stockarrival1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtstockarrival") as TextBox).Text);
                double freesupply1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtfreesupply") as TextBox).Text);



                if (stockarrival1 <= freesupply1)
                {
                    //Master.ShowModal("stockarrival1 must be graeter than freesupply1", "txtstockarrival", 0);
                    //return;
                    ShowPopupMessage("stockarrival1 must be graeter than freesupply1", PopupMessageType.txtstockarrival);
                    return;
                }



            }

            string stockarrival = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txtstockarrival") as TextBox).Text);
            string freesupply = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txtfreesupply") as TextBox).Text);
            string tax = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txttax") as TextBox).Text);
            string purchaseprice = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text);



            if (tax == "--Select--")
            {
                //Master.ShowModal("Enter tax. !!!", "ddltax", 1);
                // return;
            }


            if (purchaseprice == "")
            {
                // Master.ShowModal("Enter purchase price. !!!", "txtpurchaseprice", 1);
                // return;
            }




            if (tax != "0")
            {

                Double scv = 0.0;
                Double sllv = 0.0;
                Double productprice = 0.0;
                Double rateoftax = 0.0;


                int rowIndex = 0;
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            //extract the TextBox values

                            //sllv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text);

                            if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text))
                            {
                                sllv = 0.0;
                            }
                            else
                            {

                                sllv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text);
                            }


                            if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply") as TextBox).Text))
                            {
                                scv = 0.0;
                            }
                            else
                            {

                                scv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply") as TextBox).Text);
                            }
                            Double balance = Convert.ToDouble(sllv - scv);
                            if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtpurchaseprice") as TextBox).Text))
                            {
                                productprice = 0.0;
                            }
                            else
                            {
                                productprice = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtpurchaseprice") as TextBox).Text);
                            }
                            if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[6].FindControl("txttax") as TextBox).Text))
                            {
                                rateoftax = 0.0;
                            }
                            else
                            {
                                rateoftax = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[6].FindControl("txttax") as TextBox).Text);
                            }

                            ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txttaxamount") as TextBox).Text) = Convert.ToString(balance * productprice * rateoftax / 100);

                            double productcost = Convert.ToDouble(balance * productprice * rateoftax / 100);

                            double cost = Convert.ToDouble(balance * productprice);

                            ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproductvalue") as TextBox).Text) = Convert.ToString(productcost + cost);
                            if (rowIndex == '1')
                            {
                                (Gridview1.Rows[0].Cells[1].FindControl("txtstockarrival") as TextBox).Focus();
                            }
                            else
                            {
                                (Gridview1.Rows[rowIndex].Cells[1].FindControl("txttax") as TextBox).Focus();
                                rowIndex++;
                            }


                        }

                    }

                }

            }


            double sum = 0;

            for (int j = 0; j < Gridview1.Rows.Count; j++)
            {
                if (String.IsNullOrEmpty((Gridview1.Rows[j].Cells[1].FindControl("txtproductvalue") as TextBox).Text))
                {
                    Double add = 0.0;

                }
                else
                {

                    Double add = Convert.ToDouble((Gridview1.Rows[j].Cells[1].FindControl("txtproductvalue") as TextBox).Text);
                    sum = sum + add;
                }


            }
       


            TextBox txt = (TextBox)sender;
            GridViewRow row = (GridViewRow)txt.NamingContainer;
            Gridview1.Rows[row.RowIndex].FindControl("txtpurchaseprice").Focus();


            TextBox box7 = (TextBox)Gridview1.Rows[row.RowIndex].Cells[7].FindControl("txttax");
            //box7.BorderColor = System.Drawing.Color.Black;
            // box7.BorderWidth = 1;
            // box7.BorderStyle = BorderStyle.Dotted;
        }

        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
    }


    protected void txtpurchaseprice_TextChanged(object sender, EventArgs e)
    {
        try
        {
           // string invoiceno = txtinvoiceno.Text;
            //string invdate = txtinvoicedate.Text;
           // string invoiveamount = txtinvoiceamount.Text;

            for (int k = 0; k < Gridview1.Rows.Count; k++)
            {
                string invoiceno = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtinvoiceno") as TextBox).Text);
                string invoicedate = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtinvoicedate") as TextBox).Text);
                string productname1 = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtproductname") as TextBox).Text);
                string expdate = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtexpiredate") as TextBox).Text);
                string stockarrival = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtstockarrival") as TextBox).Text);
                string freesupply = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtfreesupply") as TextBox).Text);
                string tax = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txttax") as TextBox).Text);
                string purchaseprice = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text);
                string MRP = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtMRP") as TextBox).Text);

                if (invoiceno == "")
                {
                    Master.ShowModal("Enter invoice nor. !!!", "txtinvoiceno", 1);
                    return;
                }


                if (purchaseprice == "")
                {
                    // Master.ShowModal("Enter purchase price. !!!", "txtpurchaseprice", 1);
                    //  return;
                    ShowPopupMessage("Enter purchase price. !!!", PopupMessageType.txtpurchaseprice);
                    return;
                }

                if (invoicedate == "")
                {
                    Master.ShowModal("Invoive Date Mandatory", "txtinvoicedate", 0);
                    return;

                }
                DateTime today = DateTime.Now;
                DateTime answer = today.AddDays(90);
                DateTime expdate1 = Convert.ToDateTime(expdate);

                if (expdate == "")
                {
                    // Master.ShowModal("Expire Date Mandatory", "txtexpiredate", 0);
                    //return;
                    ShowPopupMessage("Expire Date Mandatory", PopupMessageType.txtexpiredate);
                    return;

                }

                //string close_flag = "Y";
                //DataSet dschm = clsgd.GetcondDataSet9("*", "tblProductMaster", "ProductName", productname1);
                //string gcode = dschm.Tables[0].Rows[0]["g_code"].ToString();

                //DataSet dschm10 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", gcode, "p_flag", close_flag);


                //if (dschm10.Tables[0].Rows.Count > 0)
                //{

                //    if (expdate1 <= answer)
                //    {
                //        // Master.ShowModal("Expire Date min 90 days greater than current date", "txtexpiredate", 0);
                //        //return;
                //        ShowPopupMessage("Expire Date min 90 days greater than current date", PopupMessageType.txtexpiredate);
                //        return;

                //    }
                //}

                if (stockarrival == "")
                {
                    // Master.ShowModal("Stock Arrival Mandatory", "txtstockarrival", 0);
                    //return;
                    ShowPopupMessage("Stock Arrival Mandatory", PopupMessageType.txtstockarrival);
                    return;

                }

                if (freesupply == "")
                {
                    // Master.ShowModal("Free Supply Mandatory", "txtfreesupply", 0);
                    //return;
                    ShowPopupMessage("Free Supply Mandatory", PopupMessageType.txtfreesupply);
                    return;

                }


                if (tax == "")
                {
                    //Master.ShowModal("Tax Mandatory", "ddltax", 0);
                    //return;
                    ShowPopupMessage("Tax Mandatory", PopupMessageType.ddltax);
                    return;

                }

                if (purchaseprice == "")
                {
                    // Master.ShowModal("Purchase Price Mandatory", "txtpurchaseprice", 0);
                    //return;
                    ShowPopupMessage("Purchase Price Mandatory", PopupMessageType.txtpurchaseprice);
                    return;

                }

                //if (MRP == "")
                //{
                // Master.ShowModal("MRP is Mandatory", "txtMRP", 0);
                //return;
                //  ShowPopupMessage("MRP is Mandatory", PopupMessageType.txtMRP);
                // return;
                //}


                TextBox txt25 = (TextBox)sender;
                GridViewRow row25 = (GridViewRow)txt25.NamingContainer;



                decimal txt1 = Convert.ToDecimal((Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text);

                ((Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text) = txt1.ToString("F");

              







                string purchaseprice1 = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text);
                if (purchaseprice1 != "")
                {
                    Double scv = 0.0;
                    Double sllv = 0.0;
                    Double productprice = 0.0;
                    Double rateoftax = 0.0;

                    int rowIndex = 0;
                    if (ViewState["CurrentTable"] != null)
                    {
                        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                        if (dtCurrentTable.Rows.Count > 0)
                        {
                            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                            {
                                //extract the TextBox values

                                //sllv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text);

                                if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text))
                                {
                                    sllv = 0.0;
                                }
                                else
                                {

                                    sllv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text);
                                }


                                if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply") as TextBox).Text))
                                {
                                    scv = 0.0;
                                }
                                else
                                {

                                    scv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply") as TextBox).Text);
                                }
                                Double balance = Convert.ToDouble(sllv - scv);
                                if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtpurchaseprice") as TextBox).Text))
                                {
                                    productprice = 0.0;
                                }
                                else
                                {
                                    productprice = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtpurchaseprice") as TextBox).Text);
                                }
                                if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[6].FindControl("txttax") as TextBox).Text))
                                {
                                    rateoftax = 0.0;
                                }
                                else
                                {
                                    rateoftax = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[6].FindControl("txttax") as TextBox).Text);
                                }

                                ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txttaxamount") as TextBox).Text) = Convert.ToString(balance * productprice * rateoftax / 100);

                                double productcost = Convert.ToDouble(balance * productprice * rateoftax / 100);

                                double cost = Convert.ToDouble(balance * productprice);

                                double pvalue = Convert.ToDouble(productcost + cost);
                                string pvalue10 = Math.Round(pvalue, 2).ToString();
                                ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproductvalue") as TextBox).Text) = Convert.ToString(pvalue10);

                                // ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproductvalue") as TextBox).Text) = Convert.ToString(productcost + cost);

                                (Gridview1.Rows[0].Cells[1].FindControl("txtMRP") as TextBox).Focus();
                                rowIndex++;




                            }

                        }

                    }

                }

            }





            double sum = 0;

            for (int j = 0; j < Gridview1.Rows.Count; j++)
            {
                if (String.IsNullOrEmpty((Gridview1.Rows[j].Cells[1].FindControl("txtproductvalue") as TextBox).Text))
                {
                    Double add = 0.0;

                }
                else
                {

                    Double add = Convert.ToDouble((Gridview1.Rows[j].Cells[1].FindControl("txtproductvalue") as TextBox).Text);
                    sum = sum + add;
                }


            }


            txttotalamount.Text = Convert.ToString(sum);


          

            TextBox txt = (TextBox)sender;
            GridViewRow row = (GridViewRow)txt.NamingContainer;
            Gridview1.Rows[row.RowIndex].FindControl("txtMRP").Focus();
        }

        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }


    }


    protected void txtMRP_TextChanged(object sender, EventArgs e)
    {

        try
        {

            for (int k = 0; k < Gridview1.Rows.Count; k++)
            {

                string expdate = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtexpiredate") as TextBox).Text);
                string stockarrival = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtstockarrival") as TextBox).Text);
                string freesupply = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtfreesupply") as TextBox).Text);
                string tax = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txttax") as TextBox).Text);
                string purchaseprice = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text);
                string MRP = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtMRP") as TextBox).Text);
                // if (MRP == "")
                // {
                // Master.ShowModal("Enter MRP. !!!", "txtpurchaseprice", 1);
                //return;
                // ShowPopupMessage("Enter MRP. !!!", PopupMessageType.txtMRP);
                // return;
                // }
               

                double purchaseprice1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text);
                double MRP1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtMRP") as TextBox).Text);

                if (MRP1 <= purchaseprice1)
                {
                    //Master.ShowModal("MRP must be graeter than Purchase prize", "txtstockarrival", 0);
                    //return;
                    ShowPopupMessage("MRP must be graeter than Purchase prize", PopupMessageType.txtMRP);
                    return;
                }


            }

            TextBox txt25 = (TextBox)sender;
            GridViewRow row25 = (GridViewRow)txt25.NamingContainer;


            decimal txt1 = Convert.ToDecimal((Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtMRP") as TextBox).Text);

            ((Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtMRP") as TextBox).Text) = txt1.ToString("F");


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


                        string Productcode = Convert.ToString((Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode") as TextBox).Text);
                        string ProductName = Convert.ToString((Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductname") as TextBox).Text);
                        string batchno = Convert.ToString((Gridview1.Rows[rowIndex].Cells[1].FindControl("txtbatchno") as TextBox).Text);

                        Double sllv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text);
                        Double scv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply") as TextBox).Text);
                        Double balance = Convert.ToDouble(sllv - scv);

                        Double productprice = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtpurchaseprice") as TextBox).Text);
                        Double rateoftax = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[6].FindControl("txttax") as TextBox).Text);

                        ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txttaxamount") as TextBox).Text) = Convert.ToString(balance * productprice * rateoftax / 100);

                        double productcost = Convert.ToDouble(balance * productprice * rateoftax / 100);

                        double cost = Convert.ToDouble(balance * productprice);

                        // ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproductvalue") as TextBox).Text) = Convert.ToString(productcost + cost);

                        double pvalue = Convert.ToDouble(productcost + cost);
                        string pvalue10 = Math.Round(pvalue, 2).ToString();
                        ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproductvalue") as TextBox).Text) = Convert.ToString(pvalue10);


                        rowIndex++;

                        string Sysdatetime = DateTime.Now.ToString();
                        string rateoftax1 = rateoftax.ToString();


                        // ClsBLGP1.tempproductinwrad("INSERT_tempproductinwrad", Productcode,ProductName,batchno,rateoftax1,Session["username"].ToString(),sMacAddress,Sysdatetime);
                    }

                }

            }


           


            double sum = 0;

            for (int j = 0; j < Gridview1.Rows.Count; j++)
            {
                if (String.IsNullOrEmpty((Gridview1.Rows[j].Cells[1].FindControl("txtproductvalue") as TextBox).Text))
                {
                    Double add = 0.0;

                }
                else
                {

                    Double add = Convert.ToDouble((Gridview1.Rows[j].Cells[1].FindControl("txtproductvalue") as TextBox).Text);
                    sum = sum + add;
                }


            }
           

            // (Gridview1.FindControl("ButtonAdd") as LinkButton).Focus();
           // (Gridview1.Rows[row.RowIndex].Cells[1].FindControl("ButtonAdd") as Button).Focus();
        }

        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }







    }

    protected void txtinvoiceno_TextChanged(object sender, EventArgs e)
    {

        TextBox txt25 = (TextBox)sender;
        GridViewRow row25 = (GridViewRow)txt25.NamingContainer;

        System.DateTime Dtnow1 = DateTime.Now;
        string Sysdatetime1 = Dtnow1.ToString("dd/MM/yyyy");


        ((Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtinvoicedate") as TextBox).Text) = Sysdatetime1;

      //  string invoiceno = Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtinvoiceno");

        double txt1 = Convert.ToDouble((Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtinvoiceno") as TextBox).Text);

       // double invoiceno10 = Convert.ToDouble(invoiceno);


        for (int j = 0; j < Gridview1.Rows.Count; j++)
        {
            string ID1 = Convert.ToString((Gridview1.Rows[j].Cells[1].FindControl("txtinvoiceno") as TextBox).Text);

            string date10 = Convert.ToString((Gridview1.Rows[j].Cells[1].FindControl("txtinvoicedate") as TextBox).Text);


            double id20 = Convert.ToDouble(ID1);

            if (id20 == txt1)
            {

                ((Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtinvoicedate") as TextBox).Text) = date10;
               


            }
        }


       // (Gridview1.Rows[row25.RowIndex].Cells[2].FindControl("txtinvoicedate") as TextBox).Focus();

        Gridview1.Rows[row25.RowIndex].FindControl("txtinvoicedate").Focus();

        //TextBox batchidno = (TextBox)Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtinvoicedate");

        //// batchidno.Attributes.Add("onfocusin", "select();");
        //(Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtinvoicedate") as TextBox).Focus();
        //batchidno.Attributes["onfocus"] = "javascript:this.select();";








       // TextBox txt1 = (TextBox)sender;
       // GridViewRow row1 = (GridViewRow)txt1.NamingContainer;
        //Gridview1.Rows[row1.RowIndex].FindControl("txtinvoicedate").Focus();

       // ((Gridview1.Rows[row1.RowIndex].FindControl("txtfreesupply") as TextBox).Text) = "0";

    }


    protected void Button1_Click(object sender, EventArgs e)
    {

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
                    TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtinvoiceno");
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtinvoicedate");
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtproductcode");
                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtproductname");
                    TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtbatchno");
                    TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtexpiredate");
                    //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                    TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txtstockarrival");
                    TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtstockarrival");
                    TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("txtfreesupply");
                    TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[10].FindControl("txttax");
                    TextBox box10 = (TextBox)Gridview1.Rows[rowIndex].Cells[11].FindControl("txtpurchaseprice");
                    TextBox box11 = (TextBox)Gridview1.Rows[rowIndex].Cells[12].FindControl("txtMRP");
                    TextBox box12 = (TextBox)Gridview1.Rows[rowIndex].Cells[13].FindControl("txtTaxamount");
                    TextBox box13 = (TextBox)Gridview1.Rows[rowIndex].Cells[14].FindControl("txtproductvalue");

                            System.DateTime Dtnow = DateTime.Now;
                            string Sysdatetime = Dtnow.ToString("dd/MM/yyyy");
                            //lblsuppliercode.Text = "0";
                            sc.Add(box0.Text + "," + box1.Text + "," + "CA" + ",'0'," + lblsuppliercode.Text + "," + box1.Text + "," + "C" + ",'0'," + box1.Text + ",'0','0'," + box1.Text + ",'0'," + Session["username"].ToString() + "," + Sysdatetime + "," + sMacAddress);
                            rowIndex++;
                       
                  




                }
                InsertRecords(sc);
                SqlConnection con1 = new SqlConnection(strconn1);
                con1.Open();
                SqlCommand cmd1 = new SqlCommand("delete from tbltempproductinward", con1);
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

                //SqlConnection conn = new SqlConnection(GetConnectionString());

               

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
                            TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtinvoiceno");
                            TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtinvoicedate");
                            TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtproductcode");
                            TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtproductname");
                            TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtbatchno");
                            TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtexpiredate");
                            //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                            TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txtstockarrival");
                            TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtstockarrival");
                            TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("txtfreesupply");
                            TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[10].FindControl("txttax");
                            TextBox box10 = (TextBox)Gridview1.Rows[rowIndex].Cells[11].FindControl("txtpurchaseprice");
                            TextBox box11 = (TextBox)Gridview1.Rows[rowIndex].Cells[12].FindControl("txtMRP");
                            TextBox box12 = (TextBox)Gridview1.Rows[rowIndex].Cells[13].FindControl("txtTaxamount");
                            TextBox box13 = (TextBox)Gridview1.Rows[rowIndex].Cells[14].FindControl("txtproductvalue");

                            double txtmrp = Convert.ToDouble(box11.Text);

                            double ddltax = Convert.ToDouble(box9.Text);

                            double taxrate = (txtmrp * ddltax) / (100 + ddltax);

                            double selprice = Convert.ToDouble(txtmrp - taxrate);

                            //string expiredate = box3.Text.ToString("dd/MM/yyyy");

                            string close_flag = "Y";
                            DataSet dschm = clsgd.GetcondDataSet9("*", "tblProductMaster", "Productcode", box2.Text);
                            string gcode1 = dschm.Tables[0].Rows[0]["g_code"].ToString();

                            DataSet dschm10 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", gcode1, "p_flag", close_flag);


                            if (dschm10.Tables[0].Rows.Count > 0)
                            {


                                DataSet dschm20 = clsgd.GetcondDataSet9("*", "tblProductMaster", "Productcode", box2.Text);
                                //((Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text) = Convert.ToString(dschm20.Tables[0].Rows[0]["Productname"].ToString());
                                lblgroupname.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["g_code"].ToString());
                                lblgenericcode.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Genericcode"].ToString());
                                lblchemcode.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Cemcode"].ToString());
                                lblmedicine.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Medcode"].ToString());
                                lblunit.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Unit"].ToString());
                                lblform.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Form"].ToString());
                                lblmanufacture.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Manufacturer"].ToString());
                                lblshelf.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Shelf"].ToString());
                                lblrock.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Row"].ToString());
                                lblsuplier.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Suppname"].ToString());
                            }
                            else
                            {
                                DataSet dschm20 = clsgd.GetcondDataSet9("*", "tblProductMaster", "Productcode", box2.Text);
                                // ((Gridview1.Rows[i].Cells[1].FindControl("txtproductname") as TextBox).Text) = Convert.ToString(dschm20.Tables[0].Rows[0]["Productname"].ToString());
                                lblgroupname.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["g_code"].ToString()); ;
                                lblgenericcode.Text = "0";
                                lblchemcode.Text = "0";
                                lblmedicine.Text = "0";
                                lblunit.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Unit"].ToString());
                                lblform.Text = "0";
                                lblmanufacture.Text = "0";
                                lblshelf.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Shelf"].ToString());
                                lblrock.Text = Convert.ToString(dschm20.Tables[0].Rows[0]["Row"].ToString());
                                lblsuplier.Text = "0";

                            }




                            Double sllv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text);
                            Double scv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply") as TextBox).Text);
                            Double balance = Convert.ToDouble(sllv - scv);

                            Double productprice = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtpurchaseprice") as TextBox).Text);
                            Double rateoftax = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[6].FindControl("txttax") as TextBox).Text);

                            ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txttaxamount") as TextBox).Text) = Convert.ToString(balance * productprice * rateoftax / 100);

                            double productcost = Convert.ToDouble(balance * productprice * rateoftax / 100);

                            double cost = Convert.ToDouble(balance * productprice);

                            string taxable = cost.ToString();

                            string taxableamount = cost.ToString();

                            DateTime dtEntered = Convert.ToDateTime(box5.Text);
                            string expiredate = dtEntered.ToString("dd/MM/yyyy");



                            if (box0.Text == "")
                            {
                                //Master.ShowModal("Enter Product Code. !!!", "txtproductcode", 1);
                                //return;
                                ShowPopupMessage("Enter Product Code. !!!", PopupMessageType.txtproductcode);
                                return;

                            }

                            if (box1.Text == "")
                            {
                                //Master.ShowModal("Enter Product Name. !!!", "txtproductname", 1);
                                //return;
                                ShowPopupMessage("Enter Product Name. !!!", PopupMessageType.txtproductname);
                                return;

                            }
                            ////bharat////
                            if (box2.Text == "")
                            {

                                ShowPopupMessage("Enter Batch No. !!!", PopupMessageType.txtbatchno);
                                return;


                            }

                            //ShowPopupMessage("Enter Batch No. !!!", PopupMessageType.txtbatchno);
                            //return;

                            if (box3.Text == "")
                            {
                                // Master.ShowModal("Enter Batch No. !!!", "txtbatchno", 1);
                                //return;

                                //ShowPopupMessage("Enter Batch No. !!!", PopupMessageType.txtbatchno);
                                ShowPopupMessage("Enter Expire Date. !!!", PopupMessageType.txtexpiredate);
                                return;


                            }


                            if (box4.Text == "")
                            {
                                // Master.ShowModal("Enter Stock Arrival. !!!", "txtstockarrival", 1);
                                //return;

                                ShowPopupMessage("Enter Stock Arrival. !!!", PopupMessageType.txtstockarrival);
                                return;

                            }

                            if (box6.Text == "")
                            {
                                //Master.ShowModal("Enter Free Supply. !!!", "txtfreesupply", 1);
                                //return;

                                ShowPopupMessage("Enter Free Supply. !!!", PopupMessageType.txtfreesupply);
                                return;

                            }

                            if (box8.Text == "")
                            {
                                // Master.ShowModal("Enter Purchase Price. !!!", "txtpurchaseprice", 1);
                                //return;
                                ShowPopupMessage("Enter Purchase Price. !!!", PopupMessageType.txtpurchaseprice);
                                return;

                            }

                           



                                double rselprice = Math.Round(selprice, 2);
                                string selprice1 = Convert.ToString(rselprice);
                                System.DateTime Dtnow = DateTime.Now;
                                string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                                String strconn11 = Dbconn.conmenthod();


                                Clsprdinw.Productinward("INSERT_PRODUCTINWARD", "0", box0.Text, box1.Text, "CA", "Y", lblsuppliercode.Text, box1.Text, box2.Text, box3.Text, lblgroupname.Text, lblgenericcode.Text, lblchemcode.Text, lblmedicine.Text, lblunit.Text, lblform.Text, lblmanufacture.Text, lblshelf.Text, lblrock.Text, "0", box8.Text, box9.Text, box6.Text, box7.Text, box4.Text, expiredate, box10.Text, box11.Text, box13.Text, box12.Text, "0", selprice1, Session["username"].ToString(), sqlFormattedDate, sMacAddress, taxable, "Y", "0", "Y", "Y", "Y", "Y", "Y", "Y", "Y");
                                        rowIndex++;
                                        //clsSup.Supplieraccno("INSERT_SUPPLIERACCOUNT", Tr_no, txtinvoiceno.Text, txtinvoicedate.Text, "CA", "Y", ddlsupplier.SelectedItem.Text, indate, "C", Voachrno, txttotalamount.Text, Bankaccno, Chequeno, Chequedate, "00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                    
                                
                            
                            
                            
                            
                        }




                        int rowIndex1 = 0;
                        if (ViewState["CurrentTable"] != null)
                        {
                            DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable"];
                            DataRow drCurrentRow1 = null;
                            if (dtCurrentTable.Rows.Count > 0)
                            {
                                for (int i = 1; i <= dtCurrentTable1.Rows.Count; i++)
                                {
                                    TextBox box0 = (TextBox)Gridview1.Rows[rowIndex1].Cells[1].FindControl("txtinvoiceno");
                                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex1].Cells[1].FindControl("txtinvoicedate");
                                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex1].Cells[1].FindControl("txtproductcode");



                                    SqlConnection con12 = new SqlConnection(strconn1);
                                    con12.Open();
                                    SqlCommand cmd12 = new SqlCommand("SELECT DISTINCT Tax FROM tblProductinward where productcode='" + box2.Text + "'", con12);
                                    // SqlDataReader reader = cmd12.ExecuteReader();
                                    SqlDataAdapter da1 = new SqlDataAdapter(cmd12);
                                    DataSet ds1 = new DataSet();
                                    da1.Fill(ds1);

                                    if (ds1.Tables[0].Rows.Count > 0)
                                    {

                                        string tax10 = Convert.ToString(ds1.Tables[0].Rows[0]["Tax"].ToString());
                                        SqlConnection con14 = new SqlConnection(strconn1);
                                        con14.Open();
                                        SqlCommand cmd14 = new SqlCommand("SELECT SUM(Taxamount) AS Taxamount,sum(taxable) as taxable FROM tblProductinward where Tax='" + tax10 + "' AND Invoiceno = " + box0.Text + "", con14);
                                        //SqlDataReader reader10 = cmd14.ExecuteReader();
                                        SqlDataAdapter da = new SqlDataAdapter(cmd14);
                                        DataSet ds = new DataSet();
                                        da.Fill(ds);



                                        // double Taxamount = Convert.ToDouble(reader10["Taxamount"]);
                                        //double taxable = Convert.ToDouble(reader10["taxable"]);

                                        string Taxamount1 = ds.Tables[0].Rows[0]["Taxamount"].ToString();
                                        double Taxamount = Convert.ToDouble(Taxamount1);
                                        string taxable1 = ds.Tables[0].Rows[0]["taxable"].ToString();
                                        double taxable = Convert.ToDouble(taxable1);
                                        //string taxable1 = taxable.ToString();

                                        string taxamount1 = Taxamount.ToString();
                                        string staxamount = tax10.ToString();

                                       

                                        System.DateTime Dtnow = DateTime.Now;
                                        string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                                        SqlConnection cone = new SqlConnection(strconn1);
                                        cone.Open();
                                        SqlCommand cmd = new SqlCommand("Select * from tbltmptax where Productcode='" + box0.Text + "' and Tax='" + tax10 + "'", cone);
                                        SqlDataAdapter da12 = new SqlDataAdapter(cmd);
                                        DataSet ds12 = new DataSet();
                                        da12.Fill(ds12);

                                        if (ds12.Tables[0].Rows.Count > 0)
                                        {






                                        }
                                        else
                                        {
                                            Clsprdinw.Purchasetax("INSERT_PURCHASETAX", box0.Text, box1.Text, tax10, taxamount1, taxable1, Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                            rowIndex1++;

                                            SqlConnection cone12 = new SqlConnection(strconn1);
                                            cone12.Open();
                                            SqlCommand cmd1121 = new SqlCommand("insert into tbltmptax(Productcode,Tax)values('" + box2.Text + "','" + tax10 + "')", cone12);
                                            cmd1121.ExecuteNonQuery();
                                        }
                                    }



                                }
                                SqlConnection con101 = new SqlConnection(strconn1);
                                con101.Open();
                                SqlCommand cmd121 = new SqlCommand("delete from tbltmptax", con101);
                                cmd121.ExecuteNonQuery();
                            }
                        }




                        //SqlConnection con = new SqlConnection(strconn11);
                        //con.Open();
                        //SqlCommand cmd = new SqlCommand("insert into tblSupplieraccount (Invoiceno,Invoicedate,Paymenttype,Paymentflag,SupplierCode,Indate,Typeoftransaction,Vouchrno,Totalvalues,Bankaccno,Chequeno,Chequedate,Narration,Tr_no,Login_name,Sysdatetime,Mac_id) values('" + txtinvoiceno.Text + "','" + txtinvoicedate.Text + "','" + "CA" + "','" + "Y" + "','" + ddlsupplier.SelectedItem.Text + "','" + indate + "','" + "C" + "','" + Voachrno + "','" + box11.Text + "','" + Bankaccno + "','" + Chequeno + "','" + Chequedate + "','" + Narration + "','" + "0" + "','" + Session["username"].ToString() + "','" + sqlFormattedDate + "','" + sMacAddress + "')", con);
                        //cmd.ExecuteNonQuery();
                        //con.Close();
                    }

                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Records Successfuly Saved!');", true);
                    //Response.Redirect("GridViewWithTextBoxes.aspx");


                }
                /////bharat///////
                System.DateTime Dtnow1 = DateTime.Now;
                string sqlFormattedDate1 = Dtnow1.ToString("dd/MM/yyyy");

               


            }

            lblsuccess.Visible = true;
            lblsuccess.Text = "inserted successfully";
            txttotalamount.Text = string.Empty;

            supplier();


           
            SetInitialRow();
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }




    }

     protected void txtbatchno_TextChanged(object sender, EventArgs e)
    {

        try
        {

            // int rowIndex = 0;
            //if (ViewState["CurrentTable"] != null)
            //{
            //  DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            //if (dtCurrentTable.Rows.Count > 0)
            //{
            //  for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            //{
            TextBox txt = (TextBox)sender;
            GridViewRow row = (GridViewRow)txt.NamingContainer;

            //            string productcode = Convert.ToString((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtproductcode") as TextBox).Text);
            //            string Batchid = Convert.ToString((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtbatchno") as TextBox).Text);

            //            DataSet dsprodin = clsgd.GetcondDataSet2("*", "tblProductinward", "Productcode", productcode, "Batchid", Batchid);
            //            if (dsprodin.Tables[0].Rows.Count > 0)
            //            {
            //                //Gridview1.Rows[row.RowIndex].FindControl("txtbatchno").Focus();
            //                //Master.ShowModal("Different Batch Id", "txtbatchno", 1);
            //                //Gridview1.Rows[row.RowIndex].FindControl("txtbatchno").Focus();
            //                //return;
            //                //Gridview1.Rows[row.RowIndex].FindControl("txtbatchno").Focus();
            //                ShowPopupMessage("Different Batch Id", PopupMessageType.txtbatchno);
            //                return;
            //                //lblErrorMessage.Text = message;



            //            }



            //DataSet dsgrp=clsgd.GetcondDataSet2("*","tbltempproductinward","productcode",productcode,"Batchid",Batchid);

            //    if (dsgrp.Tables[0].Rows.Count>0)
            //    {
            //       // Master.ShowModal("Different Batch Id", "txtbatchno", 0);

            //       // return;
            //        ShowPopupMessage("Different Batch Id", PopupMessageType.txtbatchno);
            //        return;

            //      //  ShowModal1("Different Batch Id", "txtbatchno", 0);

            //    }


            for (int k = 0; k < Gridview1.Rows.Count - 1; k++)
            {
                string productname1 = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtproductname") as TextBox).Text);
                string productcode1 = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtproductcode") as TextBox).Text);

                string loopbatchno = (Gridview1.Rows[k].Cells[1].FindControl("txtbatchno") as TextBox).Text;


                string close_flag = "Y";
                DataSet dschm = clsgd.GetcondDataSet9("*", "tblProductMaster", "ProductName", productname1);
                string gcode = dschm.Tables[0].Rows[0]["g_code"].ToString();

                DataSet dschm10 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", gcode, "p_flag", close_flag);


                if (dschm10.Tables[0].Rows.Count < 0)
                {
                    // Master.ShowModal("Pharmacy product batch id mandotory", "txtexpiredate", 1);
                    ShowPopupMessage("Pharmacy product batch id mandotory", PopupMessageType.txtbatchno);
                    return;

                }

                string batchno = (Gridview1.Rows[row.RowIndex].FindControl("txtbatchno") as TextBox).Text;

                string productcode12 = Convert.ToString((Gridview1.Rows[row.RowIndex].FindControl("txtproductcode") as TextBox).Text);
                string productname12 = Convert.ToString((Gridview1.Rows[row.RowIndex].FindControl("txtproductname") as TextBox).Text);
                //string stock = Convert.ToString((Gridview1.Rows[row.RowIndex].FindControl("txtstockarrival") as TextBox).Text);
                // int lblbatch1 = Convert.ToInt16(lblbatch);

                // int loppbatchno = Convert.ToInt16(loopbatchno);
                if (loopbatchno == "0")
                {
                }
                if (batchno == loopbatchno && productcode12 == productcode1 && productname12 == productname1)
                {
                    ShowPopupMessage("Different Batch ID", PopupMessageType.txtbatchno);
                    return;
                }


            }
            string batchno1 = (Gridview1.Rows[row.RowIndex].FindControl("txtbatchno") as TextBox).Text;
            DataSet dspro = clsgd.GetcondDataSet("*", "tblProductinward", "Batchid", batchno1);
            if (dspro.Tables[0].Rows.Count > 0)
            {
                ShowPopupMessage("Different Batch ID", PopupMessageType.txtbatchno);
                return;
            }

            Gridview1.Rows[row.RowIndex].FindControl("txtexpiredate").Focus();
        }

        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }

                  
        
    }


     protected void txtexpiredate_TextChanged(object sender, EventArgs e)
     {

         try
         {

             for (int k = 0; k < Gridview1.Rows.Count; k++)
             {
                 string productname1 = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtproductname") as TextBox).Text);
                 string productcode1 = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtproductcode") as TextBox).Text);
                 string stockarrival = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtstockarrival") as TextBox).Text);
                 string freesupply = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtfreesupply") as TextBox).Text);
                 string tax = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txttax") as TextBox).Text);
                 string purchaseprice = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text);
                 string MRP = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtMRP") as TextBox).Text);
                 string expiredate1 = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtexpiredate") as TextBox).Text);



                 if (freesupply == "")
                 {
                     //Master.ShowModal("Enter Free Supply. !!!", "txtfreesupply", 1);
                     // return;
                 }

                 if (tax == "")
                 {
                     // Master.ShowModal("Enter tax. !!!", "ddltax", 1);
                     // return;
                 }


                 if (purchaseprice == "")
                 {
                     //Master.ShowModal("Enter purchase price. !!!", "txtpurchaseprice", 1);
                     // return;
                 }
                 try
                 {
                     //DateTime today = DateTime.Now;
                     //DateTime answer = today.AddDays(90);
                     //DateTime expdate1 = Convert.ToDateTime(expiredate1);

                     //string close_flag = "Y";
                     //DataSet dschm = clsgd.GetcondDataSet9("*", "tblProductMaster", "ProductName", productname1);
                     //string gcode = dschm.Tables[0].Rows[0]["g_code"].ToString();

                     //DataSet dschm10 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", gcode, "p_flag", close_flag);


                     //if (dschm10.Tables[0].Rows.Count > 0)
                     //{

                     //    if (expdate1 <= answer)
                     //    {
                     //        // Master.ShowModal("Expire Date minimum 90days greater than current  date", "txtexpiredate", 1);
                     //        ShowPopupMessage("Expire Date minimum 90days greater than current  date", PopupMessageType.txtexpiredate);
                     //        (Gridview1.Rows[0].Cells[1].FindControl("txtexpiredate") as TextBox).Enabled = true;
                     //        (Gridview1.Rows[0].Cells[1].FindControl("txtexpiredate") as TextBox).Focus();
                     //        return;

                     //    }
                     //}



                     if (expiredate1 == "")
                     {
                         //Master.ShowModal("Expire Date Mandatory", "txtexpiredate", 1);
                         //return;
                         ShowPopupMessage("Expire Date Mandatory", PopupMessageType.txtexpiredate);
                         return;

                     }






                 }
                 catch (Exception ex)
                 {
                     string asd = ex.Message;
                     //Master.ShowModal("Invalid date format", "txtexpiredate", 1);
                     //return;
                     ShowPopupMessage("Invalid date format", PopupMessageType.txtexpiredate);
                     return;
                 }

                 if (stockarrival != "" && freesupply != "" && purchaseprice != "" && MRP == "")
                 {

                     //Master.ShowModal("MRP is Mandatory", "txtMRP", 1);
                     //return;
                     ShowPopupMessage("MRP is Mandatory", PopupMessageType.txtMRP);
                     return;
                 }

             }



             string expiredate2 = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txtexpiredate") as TextBox).Text);


             if (expiredate2 != "")
             {


                 Double scv = 0.0;
                 Double sllv = 0.0;
                 Double productprice = 0.0;
                 Double rateoftax = 0.0;
                 Double mrp = 0.0;
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

                             //sllv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text);

                             if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text))
                             {
                                 sllv = 0.0;
                             }
                             else
                             {

                                 sllv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text);
                             }


                             if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply") as TextBox).Text))
                             {
                                 scv = 0.0;
                             }
                             else
                             {

                                 scv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply") as TextBox).Text);
                             }
                             Double balance = Convert.ToDouble(sllv - scv);
                             if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtpurchaseprice") as TextBox).Text))
                             {
                                 productprice = 0.0;
                             }
                             else
                             {
                                 productprice = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtpurchaseprice") as TextBox).Text);
                             }
                             if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[6].FindControl("txttax") as TextBox).Text))
                             {
                                 rateoftax = 0.0;
                             }
                             else
                             {
                                 rateoftax = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[6].FindControl("txttax") as TextBox).Text);
                             }

                             if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtMRP") as TextBox).Text))
                             {
                                 mrp = 0.0;
                             }
                             else
                             {
                                 mrp = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtMRP") as TextBox).Text);
                             }



                             //((Gridview1.Rows[rowIndex].Cells[10].FindControl("txttaxamount") as TextBox).Text) = Convert.ToString(balance * productprice * rateoftax / 100);

                             //double productcost = Convert.ToDouble(balance * productprice * rateoftax / 100);

                             //double cost = Convert.ToDouble(balance * productprice);



                             //((Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproductvalue") as TextBox).Text) = Convert.ToString(productcost + cost);

                             double ttax = Convert.ToDouble(balance * productprice * rateoftax / 100);
                             string taxvalue12 = Math.Round(ttax, 2).ToString();


                             ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txttaxamount") as TextBox).Text) = Convert.ToString(taxvalue12);

                             double productcost = Convert.ToDouble(balance * productprice * rateoftax / 100);

                             double cost = Convert.ToDouble(balance * productprice);

                             double pvalue = Convert.ToDouble(productcost + cost);
                             string pvalue10 = Math.Round(pvalue, 2).ToString();
                             ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproductvalue") as TextBox).Text) = Convert.ToString(pvalue10);
                             if (rowIndex == '1')
                             {
                                 (Gridview1.Rows[0].Cells[1].FindControl("txtstockarrival") as TextBox).Focus();
                             }
                             else
                             {

                                 (Gridview1.Rows[rowIndex].Cells[1].FindControl("txtstockarrival") as TextBox).Focus();
                                 rowIndex++;
                             }

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
         }


         try
         {
             // string invoiceno = txtinvoiceno.Text;
             //string invdate = txtinvoicedate.Text;
             // string invoiveamount = txtinvoiceamount.Text;

             for (int k = 0; k < Gridview1.Rows.Count; k++)
             {
                 string invoiceno = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtinvoiceno") as TextBox).Text);
                 string invoicedate = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtinvoicedate") as TextBox).Text);
                 string productname1 = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtproductname") as TextBox).Text);
                 string expdate = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtexpiredate") as TextBox).Text);
                 string stockarrival = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtstockarrival") as TextBox).Text);
                 string freesupply = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtfreesupply") as TextBox).Text);
                 string tax = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txttax") as TextBox).Text);
                 string purchaseprice = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text);
                 string MRP = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtMRP") as TextBox).Text);

                 if (invoiceno == "")
                 {
                     Master.ShowModal("Enter invoice nor. !!!", "txtinvoiceno", 1);
                     return;
                 }


                 if (purchaseprice == "")
                 {
                     // Master.ShowModal("Enter purchase price. !!!", "txtpurchaseprice", 1);
                     //  return;
                     ShowPopupMessage("Enter purchase price. !!!", PopupMessageType.txtpurchaseprice);
                     return;
                 }

                 if (invoicedate == "")
                 {
                     Master.ShowModal("Invoive Date Mandatory", "txtinvoicedate", 0);
                     return;

                 }
                 DateTime today = DateTime.Now;
                 DateTime answer = today.AddDays(90);
                 DateTime expdate1 = Convert.ToDateTime(expdate);

                 if (expdate == "")
                 {
                     // Master.ShowModal("Expire Date Mandatory", "txtexpiredate", 0);
                     //return;
                     ShowPopupMessage("Expire Date Mandatory", PopupMessageType.txtexpiredate);
                     return;

                 }

                 //string close_flag = "Y";
                 //DataSet dschm = clsgd.GetcondDataSet9("*", "tblProductMaster", "ProductName", productname1);
                 //string gcode = dschm.Tables[0].Rows[0]["g_code"].ToString();

                 //DataSet dschm10 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", gcode, "p_flag", close_flag);


                 //if (dschm10.Tables[0].Rows.Count > 0)
                 //{

                 //    if (expdate1 <= answer)
                 //    {
                 //        // Master.ShowModal("Expire Date min 90 days greater than current date", "txtexpiredate", 0);
                 //        //return;
                 //        ShowPopupMessage("Expire Date min 90 days greater than current date", PopupMessageType.txtexpiredate);
                 //        return;

                 //    }
                 //}

                 if (stockarrival == "")
                 {
                     // Master.ShowModal("Stock Arrival Mandatory", "txtstockarrival", 0);
                     //return;
                     ShowPopupMessage("Stock Arrival Mandatory", PopupMessageType.txtstockarrival);
                     return;

                 }

                 if (freesupply == "")
                 {
                     // Master.ShowModal("Free Supply Mandatory", "txtfreesupply", 0);
                     //return;
                     ShowPopupMessage("Free Supply Mandatory", PopupMessageType.txtfreesupply);
                     return;

                 }


                 if (tax == "")
                 {
                     //Master.ShowModal("Tax Mandatory", "ddltax", 0);
                     //return;
                     ShowPopupMessage("Tax Mandatory", PopupMessageType.ddltax);
                     return;

                 }

                 if (purchaseprice == "")
                 {
                     // Master.ShowModal("Purchase Price Mandatory", "txtpurchaseprice", 0);
                     //return;
                     ShowPopupMessage("Purchase Price Mandatory", PopupMessageType.txtpurchaseprice);
                     return;

                 }

                 //if (MRP == "")
                 //{
                 // Master.ShowModal("MRP is Mandatory", "txtMRP", 0);
                 //return;
                 //  ShowPopupMessage("MRP is Mandatory", PopupMessageType.txtMRP);
                 // return;
                 //}


                 //TextBox txt25 = (TextBox)sender;
                 //GridViewRow row25 = (GridViewRow)txt25.NamingContainer;



                 //decimal txt1 = Convert.ToDecimal((Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text);

                 //((Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text) = txt1.ToString("F");









                 string purchaseprice1 = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text);
                 if (purchaseprice1 != "")
                 {
                     Double scv = 0.0;
                     Double sllv = 0.0;
                     Double productprice = 0.0;
                     Double rateoftax = 0.0;

                     int rowIndex = 0;
                     if (ViewState["CurrentTable"] != null)
                     {
                         DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                         if (dtCurrentTable.Rows.Count > 0)
                         {
                             for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                             {
                                 //extract the TextBox values

                                 //sllv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text);

                                 if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text))
                                 {
                                     sllv = 0.0;
                                 }
                                 else
                                 {

                                     sllv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text);
                                 }


                                 if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply") as TextBox).Text))
                                 {
                                     scv = 0.0;
                                 }
                                 else
                                 {

                                     scv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply") as TextBox).Text);
                                 }
                                 Double balance = Convert.ToDouble(sllv - scv);
                                 if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtpurchaseprice") as TextBox).Text))
                                 {
                                     productprice = 0.0;
                                 }
                                 else
                                 {
                                     productprice = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtpurchaseprice") as TextBox).Text);
                                 }
                                 if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[6].FindControl("txttax") as TextBox).Text))
                                 {
                                     rateoftax = 0.0;
                                 }
                                 else
                                 {
                                     rateoftax = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[6].FindControl("txttax") as TextBox).Text);
                                 }

                                 ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txttaxamount") as TextBox).Text) = Convert.ToString(balance * productprice * rateoftax / 100);

                                 double productcost = Convert.ToDouble(balance * productprice * rateoftax / 100);

                                 double cost = Convert.ToDouble(balance * productprice);

                                 double pvalue = Convert.ToDouble(productcost + cost);
                                 string pvalue10 = Math.Round(pvalue, 2).ToString();
                                 ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproductvalue") as TextBox).Text) = Convert.ToString(pvalue10);

                                 // ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproductvalue") as TextBox).Text) = Convert.ToString(productcost + cost);

                                 (Gridview1.Rows[0].Cells[1].FindControl("txtMRP") as TextBox).Focus();
                                 rowIndex++;




                             }

                         }

                     }

                 }

             }





             double sum = 0;

             for (int j = 0; j < Gridview1.Rows.Count; j++)
             {
                 if (String.IsNullOrEmpty((Gridview1.Rows[j].Cells[1].FindControl("txtproductvalue") as TextBox).Text))
                 {
                     Double add = 0.0;

                 }
                 else
                 {

                     Double add = Convert.ToDouble((Gridview1.Rows[j].Cells[1].FindControl("txtproductvalue") as TextBox).Text);
                     sum = sum + add;
                 }


             }


             txttotalamount.Text = Convert.ToString(sum);




             //TextBox txt = (TextBox)sender;
            // GridViewRow row = (GridViewRow)txt.NamingContainer;
            // Gridview1.Rows[row.RowIndex].FindControl("txtMRP").Focus();
         }

         catch (Exception ex)
         {
             string asd = ex.Message;
             lblerror.Visible = true;
             lblerror.Text = asd;
         }







     }


     protected void txtinvoicedate_TextChanged(object sender, EventArgs e)
     {
         TextBox txt = (TextBox)sender;
         GridViewRow row = (GridViewRow)txt.NamingContainer;

         (Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtproductcode") as TextBox).Focus();

     }


   
      

        

     




    







}