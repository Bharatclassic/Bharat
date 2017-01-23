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




public partial class _Default : System.Web.UI.Page
{
    ClsBLLGeneraldetails clsgd = new ClsBLLGeneraldetails();
    ClsBALProductMaster ClsBLGP = new ClsBALProductMaster();
     ClsBALtempproductinwrad ClsBLGP1 = new  ClsBALtempproductinwrad();

     ClsBALTransaction ClsBLGP2 = new ClsBALTransaction();

    ClsBALSupplieraccount clsSup = new ClsBALSupplieraccount();
    ClsBALProductinward Clsprdinw = new ClsBALProductinward();
    //ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string button_select;
    protected static  string strconn  = Dbconn.conmenthod();
    protected static string strconn1 = Dbconn.conmenthod();


   
    string filename = Dbconn.Mymenthod();
    ArrayList arryno = new ArrayList();

    ArrayList arryname = new ArrayList();

    string sMacAddress = "";

    string Voachrno = "00";

    string Bankaccno ="00";

    string Chequeno = "00";

    //DateTime Chequedate = 'dd/mm/yy';

    string Chequedate = "01/01/1900";

    string Narration = "00";

    string sqlFormattedDate = DateTime.Now.ToString();


    string Tr_no = "00";
    protected static string name = "";
   protected static string a = "";

   string invoiceno;
   string transno;
   string transno1;
   string invoiceno1;
    
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
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
        dt.Columns.Add(new DataColumn("g_name", typeof(string)));


        dr = dt.NewRow();
        
        dr["RowNumber"] = 1;
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
        ddlsupplier.BorderColor = System.Drawing.Color.Orange;
        ddlsupplier.BorderWidth = 1;
        ddlsupplier.BorderStyle = BorderStyle.Solid;


        ddpaymenttype.BorderColor = System.Drawing.Color.Orange;
        ddpaymenttype.BorderWidth = 1;
        ddpaymenttype.BorderStyle = BorderStyle.Solid;
        txtinvoiceno.Attributes.Add("autocomplete", "off");
        txtinvoiceamount.Attributes.Add("autocomplete", "off");
        
         System.DateTime Dtnow = DateTime.Now;
        string Sysdatetime= Dtnow.ToString("dd/MM/yyyy");
        txtdate.Text=Sysdatetime;
         lblsuccess.Visible = false;
            lblerror.Visible = false;
        if (!Page.IsPostBack)
        {

            txtadjustamount.Enabled = false;
            txtdate.Enabled = false;
            supplier();
            //BindData();
            SetInitialRow();
            //txtinvoiceno.Focus();
            ddpaymenttype.Enabled = true;
            ddpaymenttype.Focus();
            //ddpaymenttype.BorderColor = System.Drawing.Color.Black;
            //ddpaymenttype.BorderWidth = 1;
            //ddpaymenttype.BorderStyle = BorderStyle.Dotted;
           // txtinvoiceno.Enabled = true;
            //txtinvoiceno.Focus();
            System.DateTime Dtnow1 = DateTime.Now;

            txtinvoicedate.Text = Dtnow1.ToString("dd/MM/yyyy");



           
            chkpayment.Visible = false;
            //lblpayment.Visible = false;
            ddlsupplier.Visible = true;
            Table2.Visible = false;
            lblgroupname.Visible = false;
            lblgenericcode.Visible = false;
            lblchemcode.Visible = false;
            lblmedicine.Visible = false;
            lblunit.Visible = false;
            lblform.Visible = false;
            lblmanufacture.Visible = false;
            lblshelf.Visible = false;
            lblrock.Visible = false;
            lblsupplier.Visible = true;
            lblsuppliercode.Visible = false;
            btnsave.Enabled = false;
            
            txtnarrations.Visible = false;

        }
        GetMACAddress();

       
       
        for (int i = 0; i < Gridview1.Rows.Count; i++)
        {
            if (String.IsNullOrEmpty((Gridview1.Rows[i].Cells[1].FindControl("txtexpiredate") as TextBox).Text))
            {
                //System.DateTime Dtnow1 = DateTime.Now;
                //string Sysdatetime1 = Dtnow1.ToString("dd/MM/yyyy");
               // TextBox box = (TextBox)Gridview1.Rows[i].Cells[4].FindControl("txtexpiredate");
                //box.Text = Sysdatetime1;
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
          //  box100.Enabled = false;
            
            

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
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtbatchno");
                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                    TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival");
                    TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply");
                    TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                    TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtpurchaseprice"); 
                    TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("txtMRP");
                    TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[10].FindControl("txtTaxamount");
                    TextBox box10 = (TextBox)Gridview1.Rows[rowIndex].Cells[11].FindControl("txtproductvalue");
                    TextBox box11 = (TextBox)Gridview1.Rows[rowIndex].Cells[11].FindControl("txtgroupname");





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
                    dtCurrentTable.Rows[i - 1]["Productcode"] = box0.Text; 
                     dtCurrentTable.Rows[i - 1]["productname"] = box1.Text;
                    dtCurrentTable.Rows[i - 1]["Batchid"] = box2.Text;
                     dtCurrentTable.Rows[i - 1]["Expiredate"] = box3.Text;
                     dtCurrentTable.Rows[i - 1]["Stockinward"] = box4.Text;
                      dtCurrentTable.Rows[i - 1]["Freesupply"] = box5.Text;
                       dtCurrentTable.Rows[i - 1]["Tax"] = box6.Text;
                         dtCurrentTable.Rows[i - 1]["Purchaseprice"] = box7.Text;
                         dtCurrentTable.Rows[i - 1]["MRP"] = box8.Text;
                         dtCurrentTable.Rows[i - 1]["TaxAmount"] = box9.Text;
                          dtCurrentTable.Rows[i - 1]["Totalvalues"] = box10.Text;
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

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        string productcode = (Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Text;
        string productname = (Gridview1.Rows[0].Cells[1].FindControl("txtproductname") as TextBox).Text;
        string batcno = (Gridview1.Rows[0].Cells[1].FindControl("txtbatchno") as TextBox).Text;
        string expdate = (Gridview1.Rows[0].Cells[1].FindControl("txtexpiredate") as TextBox).Text;
        string stockarrival = (Gridview1.Rows[0].Cells[1].FindControl("txtstockarrival") as TextBox).Text;
        string freesupply=(Gridview1.Rows[0].Cells[1].FindControl("txtfreesupply") as TextBox).Text;
        string purchaseprice = (Gridview1.Rows[0].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text;
        string mrp = (Gridview1.Rows[0].Cells[1].FindControl("txtMRP") as TextBox).Text;

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

        //ShowPopupMessage("Enter Batch No. !!!", PopupMessageType.txtbatchno);
        //return;

        if (expdate == "")
        {
           
                    //ShowPopupMessage("Enter Batch No. !!!", PopupMessageType.txtbatchno);
                    ShowPopupMessage("Enter Expire Date. !!!", PopupMessageType.txtexpiredate);
                    return;
                
           
        }
       /* if (batcno == "")
        {
            ShowPopupMessage("Enter Batchno", PopupMessageType.txtbatchno);
            return;

        }

        if (expdate == "")
        {
            ShowPopupMessage("Enter Expire Date", PopupMessageType.txtexpiredate);
            return;

        }*/
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
    protected void ddpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddpaymenttype.SelectedItem.Text == "Credit")
        {
            lblsupplier.Visible = true;
            ddlsupplier.Visible = true;
            ddlsupplier.Focus();
            chkpayment.Visible = false;
            chkpayment.Enabled = false;
            //lblpayment.Visible = false;
            txtinvoiceno.Enabled = true;
            //txtinvoiceno.Focus();
        }
        else
        {
            //ddlsupplier.Visible = false;
            lblsupplier.Visible = true;
            ddlsupplier.Visible = true;

            chkpayment.Visible = false;
            chkpayment.Enabled = false;
           // chkpayment.Visible = true;
            //chkpayment.Focus();
            //lblpayment.Visible = true;
            //lblsupplier.Visible = false;
            txtinvoiceno.Enabled = true;
            txtinvoiceno.Focus();
        }

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

       

        if (ddpaymenttype.SelectedItem.Text == "Credit")
        {
            chkpayment.Visible = false;
            //lblpayment.Visible = false;
            //txtdate.Focus();
        }
        else
        {
            if (chkpayment.Checked == true)
            {
                ddlsupplier.Visible = true;
            }
            //chkpayment.Visible = true;
            //lblpayment.Visible = true;
            //txtdate.Focus();
        }

        (Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Focus(); 

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
        ddlsupplier.DataSource = arryno;
        ddlsupplier.DataBind();
        //ddGecode.Focus();

    }

    protected void chkpayment_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkpayment.Checked == true)//this is working
        //{
        //    ddlsupplier.Visible = true;
        //    lblsupplier.Visible = true;
        //    //chkpayment.Visible = true;
        //    //lblpayment.Visible = true;
        //    ddlsupplier.Enabled = true;
        //    ddlsupplier.Focus();
        //    ddlsupplier.BorderColor = System.Drawing.Color.Black;
        //    ddlsupplier.BorderWidth = 1;
        //    ddlsupplier.BorderStyle = BorderStyle.Dotted;
            
        //}
        //else
        //{
        //    ddlsupplier.Visible = false;
        //    lblsupplier.Visible = false;
        //   // chkpayment.Visible = false;
        //    //lblpayment.Visible = false;
        //   // txtdate.Enabled = true;
        //   // txtdate.Focus();
        //  //  txtinvoiceamount.Enabled = true;
        //    //txtinvoiceamount.Focus();
        //}
    }

    protected void Gridview1_RowCreated(object sender, GridViewRowEventArgs e)
    {
         //using (SqlConnection conn = new SqlConnection())
       //// {
           // conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
           // if (e.Row.RowType == DataControlRowType.DataRow)
           // {
                 



              //  DropDownList ddll = (DropDownList)e.Row.FindControl("ddltax");

               // DataSet ds = new DataSet();
               // conn.Open();

               // string cmdstr = "Select Rate_of_interest from tblTax_Rate order by Rate_of_interest";

               // SqlCommand cmd = new SqlCommand(cmdstr, conn);

               // SqlDataAdapter adp = new SqlDataAdapter(cmd);

               // adp.Fill(ds);

               // ddll.DataSource = ds.Tables[0];

               // ddll.DataTextField = "Rate_of_interest";

                //ddlproductcode.DataValueField = "id";

               // ddll.DataBind();

               // ddll.Items.Insert(0, new ListItem("--Select--", "0"));

               // conn.Close();
               //  }

            
       // }





       // if (e.Row.RowType == DataControlRowType.DataRow)
       // {
          ////  Label l = (Label)e.Row.FindControl("Label1");
          //  if (l != null)
           // {
              //  string script = "window.open('Default.aspx');";
               // l.Attributes.Add("onclick", script);
           // }
       // }
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
                    TextBox box0 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode"); 
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtproductname");
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtbatchno");
                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                    //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                    TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival");
                    TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival");
                    TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply");
                   TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                    TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtpurchaseprice");
                    TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("txtMRP");
                    TextBox box10 = (TextBox)Gridview1.Rows[rowIndex].Cells[10].FindControl("txtTaxamount");
                    TextBox box11 = (TextBox)Gridview1.Rows[rowIndex].Cells[11].FindControl("txtproductvalue");

                 

                   

                   


                    if (ddpaymenttype.SelectedItem.Text == "Cash")
                    {
                       
                            if (ddlsupplier.SelectedItem.Text == "-Select-")
                            {
                                Master.ShowModal("Please select a Supplier Name   . !!!", "ddpaymenttype", 1);
                                return;
                            }
                            System.DateTime Dtnow = DateTime.Now;
                            string Sysdatetime = Dtnow.ToString("dd/MM/yyyy");
                            //lblsuppliercode.Text = "0";
                            sc.Add(txtinvoiceno.Text + "," + txtinvoicedate.Text + "," + "CA" + "," + ddpaymenttype.SelectedItem.Text + "," + lblsuppliercode.Text + "," + txtdate.Text + "," + "C" + "," + Voachrno + "," + box11.Text + "," + Bankaccno + "," + Chequeno + "," + txtdate.Text + "," + Narration + "," + Session["username"].ToString() + "," + Sysdatetime + "," + sMacAddress);
                            rowIndex++;
                        

                        //else
                        //{
                        //    System.DateTime Dtnow = DateTime.Now;
                        //    string Sysdatetime = Dtnow.ToString("dd/MM/yyyy");
                        //    lblsuppliercode.Text = "0000";
                           
                        //    sc.Add(txtinvoiceno.Text + "," + txtinvoicedate.Text + "," + "CA" + "," + ddpaymenttype.SelectedItem.Text + "," + lblsuppliercode.Text + "," + txtdate.Text + "," + "C" + "," + Voachrno + "," + box11.Text + "," + Bankaccno + "," + Chequeno + "," + txtdate.Text + "," + Narration + "," + Session["username"].ToString() + "," + Sysdatetime + "," + sMacAddress);
                        //    rowIndex++;


                        //}
                    }
                    else
                    {
                        if (ddlsupplier.SelectedItem.Text == "-Select-")
                        {
                            Master.ShowModal("Please select a Supplier Name   . !!!", "ddpaymenttype", 1);
                            return;
                        }

                        //DateTime Sysdatetime = DateTime.Now;
                        string sqlFormattedDate = DateTime.Now.ToString();

                        sc.Add(txtinvoiceno.Text + "," + txtinvoicedate.Text + "," + "CR" + "," + ddpaymenttype.SelectedItem.Text + "," + lblsuppliercode.Text + "," + txtdate.Text + "," + "C" + "," + Voachrno + "," + box11.Text + "," + Bankaccno + "," + Chequeno + ",'" + Chequedate.ToString() + "'," + Narration + "," + Session["username"].ToString() + ",'" + sqlFormattedDate.ToString() + "'," + sMacAddress);
                        rowIndex++;

                    }



                   

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

                string indate = txtdate.Text;


                //string indate = Convert.ToString(txtdate.Text);

                transno1 = clsgd.FetchMaximumTransNo("Select_Max_Transno");
                invoiceno1 = clsgd.FetchMaximumInvoiceNo("Select_Max_Invoiceno");
                transno = transno1 + "/" + "PRS";

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
                            TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtbatchno");
                            TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                            //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                            TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival");
                            TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival");
                            TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply");
                            TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                            TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtpurchaseprice");
                            TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("txtMRP");
                            TextBox box10 = (TextBox)Gridview1.Rows[rowIndex].Cells[10].FindControl("txtTaxamount");
                            TextBox box11 = (TextBox)Gridview1.Rows[rowIndex].Cells[11].FindControl("txtproductvalue");

                            double txtmrp = Convert.ToDouble(box9.Text);

                            double ddltax = Convert.ToDouble(box7.Text);

                            double taxrate = (txtmrp * ddltax) / (100 + ddltax);

                            double selprice = Convert.ToDouble(txtmrp - taxrate);

                            //string expiredate = box3.Text.ToString("dd/MM/yyyy");

                            string close_flag = "Y";
                            DataSet dschm = clsgd.GetcondDataSet9("*", "tblProductMaster", "Productcode", box0.Text);
                            string gcode1 = dschm.Tables[0].Rows[0]["g_code"].ToString();

                            DataSet dschm10 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", gcode1, "p_flag", close_flag);


                            if (dschm10.Tables[0].Rows.Count > 0)
                            {


                                DataSet dschm20 = clsgd.GetcondDataSet9("*", "tblProductMaster", "Productcode", box0.Text);
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
                                DataSet dschm20 = clsgd.GetcondDataSet9("*", "tblProductMaster", "Productcode", box0.Text);
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

                            DateTime dtEntered = Convert.ToDateTime(box3.Text);
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

                            if (box9.Text == "")
                            {
                                // Master.ShowModal("Enter MRP. !!!", "txtMRP", 1);
                                //return;
                                ShowPopupMessage("Enter MRP. !!!", PopupMessageType.txtMRP);
                                return;



                                double rselprice = Math.Round(selprice, 2);
                                string selprice1 = Convert.ToString(rselprice);
                                System.DateTime Dtnow = DateTime.Now;
                                string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                                String strconn11 = Dbconn.conmenthod();
                                if (ddpaymenttype.SelectedItem.Text == "Cash")
                                {
                                    string Narration = txtnarrations.Text;
                                    if (Narration == "00")
                                    {

                                        Clsprdinw.Productinward("INSERT_PRODUCTINWARD", transno, txtinvoiceno.Text, txtinvoicedate.Text, "CA", "Y", lblsuppliercode.Text, indate, box0.Text, box1.Text, lblgroupname.Text, lblgenericcode.Text, lblchemcode.Text, lblmedicine.Text, lblunit.Text, lblform.Text, lblmanufacture.Text, lblshelf.Text, lblrock.Text, "0", box6.Text, box7.Text, box4.Text, box5.Text, box2.Text, expiredate, box8.Text, box9.Text, box11.Text, box10.Text, Narration, selprice1, Session["username"].ToString(), sqlFormattedDate, sMacAddress, taxable, "Y", txttotalamount.Text, "N", "N", "N", "N", "N", "N", "N");
                                        rowIndex++;
                                        //clsSup.Supplieraccno("INSERT_SUPPLIERACCOUNT", Tr_no, txtinvoiceno.Text, txtinvoicedate.Text, "CA", "Y", ddlsupplier.SelectedItem.Text, indate, "C", Voachrno, txttotalamount.Text, Bankaccno, Chequeno, Chequedate, "00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                    }
                                    else
                                    {
                                        Clsprdinw.Productinward("INSERT_PRODUCTINWARD", transno, txtinvoiceno.Text, txtinvoicedate.Text, "CA", "Y", lblsuppliercode.Text, indate, box0.Text, box1.Text, lblgroupname.Text, lblgenericcode.Text, lblchemcode.Text, lblmedicine.Text, lblunit.Text, lblform.Text, lblmanufacture.Text, lblshelf.Text, lblrock.Text, "0", box6.Text, box7.Text, box4.Text, box5.Text, box2.Text, expiredate, box8.Text, box9.Text, box11.Text, box10.Text, Narration, selprice1, Session["username"].ToString(), sqlFormattedDate, sMacAddress, taxable, "Y", txttotalamount.Text, "N", "N", "N", "N", "N", "N", "N");
                                        rowIndex++;
                                        //clsSup.Supplieraccno("INSERT_SUPPLIERACCOUNT", Tr_no, txtinvoiceno.Text, txtinvoicedate.Text, "CA", "Y", ddlsupplier.SelectedItem.Text, indate, "C", Voachrno, txttotalamount.Text, Bankaccno, Chequeno, Chequedate, "00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                    }
                                }
                            }

                            else
                            {
                                 double rselprice = Math.Round(selprice, 2);
                                 string selprice1 = Convert.ToString(rselprice);
                               // string Narration = txtnarrations.Text;
                                if (ddpaymenttype.SelectedItem.Text == "Cash")
                                {
                                    string Narration = txtnarrations.Text;
                                    if (Narration == "00")
                                    {
                                        Clsprdinw.Productinward("INSERT_PRODUCTINWARD", transno, txtinvoiceno.Text, txtinvoicedate.Text, "CA", "Y", lblsuppliercode.Text, indate, box0.Text, box1.Text, lblgroupname.Text, lblgenericcode.Text, lblchemcode.Text, lblmedicine.Text, lblunit.Text, lblform.Text, lblmanufacture.Text, lblshelf.Text, lblrock.Text, "0", box6.Text, box7.Text, box4.Text, box5.Text, box2.Text, expiredate, box8.Text, box9.Text, box11.Text, box10.Text, Narration, selprice1, Session["username"].ToString(), sqlFormattedDate, sMacAddress, taxable, taxableamount, txttotalamount.Text, "N", "N", "N", "N", "N", "N", "N");
                                        rowIndex++;
                                        //clsSup.Supplieraccno("INSERT_SUPPLIERACCOUNT", Tr_no, txtinvoiceno.Text, txtinvoicedate.Text, "CA", "Y", ddlsupplier.SelectedItem.Text, indate, "C", Voachrno, txttotalamount.Text, Bankaccno, Chequeno, Chequedate, "00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                    }
                                    else
                                    {
                                        //string Narration = txtnarrations.Text;
                                        Clsprdinw.Productinward("INSERT_PRODUCTINWARD", transno, txtinvoiceno.Text, txtinvoicedate.Text, "CA", "Y", lblsuppliercode.Text, indate, box0.Text, box1.Text, lblgroupname.Text, lblgenericcode.Text, lblchemcode.Text, lblmedicine.Text, lblunit.Text, lblform.Text, lblmanufacture.Text, lblshelf.Text, lblrock.Text, "0", box6.Text, box7.Text, box4.Text, box5.Text, box2.Text, expiredate, box8.Text, box9.Text, box11.Text, box10.Text, Narration, selprice1, Session["username"].ToString(), sqlFormattedDate, sMacAddress, taxable, taxableamount, txttotalamount.Text, "N", "N", "N", "N", "N", "N", "N");
                                        rowIndex++;
                                        //clsSup.Supplieraccno("INSERT_SUPPLIERACCOUNT", Tr_no, txtinvoiceno.Text, txtinvoicedate.Text, "CA", "Y", ddlsupplier.SelectedItem.Text, indate, "C", Voachrno, txttotalamount.Text, Bankaccno, Chequeno, Chequedate, "00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                    }
                                }
                                if (ddpaymenttype.SelectedItem.Text == "Credit")
                                {
                                    string Narration = txtnarrations.Text;
                                    if (Narration == "00")
                                    {

                                        Clsprdinw.Productinward("INSERT_PRODUCTINWARD", transno, txtinvoiceno.Text, txtinvoicedate.Text, "CR", "Y", lblsuppliercode.Text, indate, box0.Text, box1.Text, lblgroupname.Text, lblgenericcode.Text, lblchemcode.Text, lblmedicine.Text, lblunit.Text, lblform.Text, lblmanufacture.Text, lblshelf.Text, lblrock.Text, "0", box6.Text, box7.Text, box4.Text, box5.Text, box2.Text, expiredate, box8.Text, box9.Text, box11.Text, box10.Text, "00", selprice1, Session["username"].ToString(), sqlFormattedDate, sMacAddress, taxable, taxableamount, txttotalamount.Text, "N", "N", "N", "N", "N", "N", "N");
                                        rowIndex++;
                                        // clsSup.Supplieraccno("INSERT_SUPPLIERACCOUNT", Tr_no, txtinvoiceno.Text, txtinvoicedate.Text, "CR", "Y", ddlsupplier.SelectedItem.Text, indate, "C", Voachrno, txttotalamount.Text, Bankaccno, Chequeno, Chequedate, "00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                    }
                                    else
                                    {
                                        Clsprdinw.Productinward("INSERT_PRODUCTINWARD", transno, txtinvoiceno.Text, txtinvoicedate.Text, "CR", "Y", lblsuppliercode.Text, indate, box0.Text, box1.Text, lblgroupname.Text, lblgenericcode.Text, lblchemcode.Text, lblmedicine.Text, lblunit.Text, lblform.Text, lblmanufacture.Text, lblshelf.Text, lblrock.Text, "0", box6.Text, box7.Text, box4.Text, box5.Text, box2.Text, expiredate, box8.Text, box9.Text, box11.Text, box10.Text, Narration, selprice1, Session["username"].ToString(), sqlFormattedDate, sMacAddress, taxable, taxableamount, txttotalamount.Text, "N", "N", "N", "N", "N", "N", "N");
                                        rowIndex++;
                                        //clsSup.Supplieraccno("INSERT_SUPPLIERACCOUNT", Tr_no, txtinvoiceno.Text, txtinvoicedate.Text, "CR", "Y", ddlsupplier.SelectedItem.Text, indate, "C", Voachrno, txttotalamount.Text, Bankaccno, Chequeno, Chequedate, "00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                    }
                                }
                            }
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

                                    TextBox box0 = (TextBox)Gridview1.Rows[rowIndex1].Cells[1].FindControl("txtproductcode");



                                    SqlConnection con12 = new SqlConnection(strconn1);
                                    con12.Open();
                                    SqlCommand cmd12 = new SqlCommand("SELECT DISTINCT Tax FROM tblProductinward where productcode='" + box0.Text + "' AND Invoiceno = '" + txtinvoiceno.Text + "'", con12);
                                    // SqlDataReader reader = cmd12.ExecuteReader();
                                    SqlDataAdapter da1 = new SqlDataAdapter(cmd12);
                                    DataSet ds1 = new DataSet();
                                    da1.Fill(ds1);

                                    if (ds1.Tables[0].Rows.Count > 0)
                                    {

                                        string tax10 = Convert.ToString(ds1.Tables[0].Rows[0]["Tax"].ToString());
                                        SqlConnection con14 = new SqlConnection(strconn1);
                                        con14.Open();
                                        SqlCommand cmd14 = new SqlCommand("SELECT SUM(Taxamount) AS Taxamount,sum(taxable) as taxable FROM tblProductinward where Tax='" + tax10 + "' AND Invoiceno = '" + txtinvoiceno.Text + "'", con14);
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

                                        string invoiceno = txtinvoiceno.Text;
                                        string invoicedate = txtinvoicedate.Text;

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
                                            Clsprdinw.Purchasetax("INSERT_PURCHASETAX", invoiceno, invoicedate, tax10, taxamount1, taxable1, Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                            rowIndex1++;

                                            SqlConnection cone12 = new SqlConnection(strconn1);
                                            cone12.Open();
                                            SqlCommand cmd1121 = new SqlCommand("insert into tbltmptax(Productcode,Tax)values('" + box0.Text + "','" + tax10 + "')", cone12);
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

                if (ddpaymenttype.SelectedItem.Text == "Credit")
                {
                    clsSup.Supplieraccno("INSERT_SUPPLIERACCOUNT", Tr_no, txtinvoiceno.Text, txtinvoicedate.Text, "CR", "N", lblsuppliercode.Text, indate, "C", Voachrno, txttotalamount.Text, Bankaccno, Chequeno, Chequedate, "00", Session["username"].ToString(), sqlFormattedDate1, sMacAddress);
                }
                if (ddpaymenttype.SelectedItem.Text == "Cash")
                {
                    if (chkpayment.Checked == true)
                    {
                        clsSup.Supplieraccno("INSERT_SUPPLIERACCOUNT", Tr_no, txtinvoiceno.Text, txtinvoicedate.Text, "CA", "N", lblsuppliercode.Text, indate, "C", Voachrno, txttotalamount.Text, Bankaccno, Chequeno, Chequedate, "00", Session["username"].ToString(), sqlFormattedDate1, sMacAddress);
                    }
                }


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
                            TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtbatchno");
                            TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                            //float box3 = Convert.ToInt32((Gridview1.Rows[rowIndex].Cells[4].FindControl("TextBox3") as TextBox).Text);
                            TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival");
                            TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival");
                            TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply");
                            TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                            TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtpurchaseprice");
                            TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("txtMRP");
                            TextBox box10 = (TextBox)Gridview1.Rows[rowIndex].Cells[10].FindControl("txtTaxamount");
                            TextBox box11 = (TextBox)Gridview1.Rows[rowIndex].Cells[11].FindControl("txtproductvalue");
                            String strconn11 = Dbconn.conmenthod();
                            OleDbConnection con = new OleDbConnection(strconn11);
                            DateTime dtEntered1 = Convert.ToDateTime(box3.Text);
                            string expiredate1 = dtEntered1.ToString("dd/MM/yyyy");
                            con.Open();
                            OleDbCommand cmd = new OleDbCommand("insert into tblProductinward (Invoiceno,Invoicedate,Paymenttype,Paymentflag,Suppliername,Indate,Productcode,ProductName,g_code,GN_code,CC_code,FA_code,unitcode,formcode,ManufactureCode,se_code,Rack,Suppliercode,Freesupply,Tax,Stockinward,Stockinhand,Batchid,Expiredate,Purchaseprice,MRP,Taxamount,Totalvalues,Login_name,Sysdatetime,Mac_id,In_falg1,In_falg2,In_falg3,In_falg4,In_falg5,In_falg6,In_falg7,In_falg8,In_falg9,In_falg10) values('" + txtinvoiceno.Text + "','" + txtinvoicedate.Text + "','" + ddpaymenttype.SelectedItem.Text + "','" + ddpaymenttype.SelectedItem.Text + "','" + ddlsupplier.SelectedItem.Text + "','" + indate + "','" + box0.Text + "','" + box1.Text + "','" + lblgroupname.Text + "','" + lblgenericcode.Text + "','" + lblchemcode.Text + "','" + lblmedicine.Text + "'," + lblunit.Text + ",'" + lblform.Text + "','" + lblmanufacture.Text + "','" + lblshelf.Text + "','" + lblrock.Text + "'," + lblsuplier.Text + "," + box6.Text + ",'" + box7.Text + "','" + box4.Text + "','" + box5.Text + "','" + box2.Text + "','" + expiredate1 + "','" + box8.Text + "','" + box9.Text + "','" + box10.Text + "','" + box11.Text + "','" + Session["username"].ToString() + "','" + sqlFormattedDate + "','" + sMacAddress + "','Y','Y','Y','Y','Y','Y','Y','Y','Y','Y')", con);
                            cmd.ExecuteNonQuery();
                            con.Close();

                            OleDbConnection conn12 = new OleDbConnection(strconn11);
                            conn12.Open();
                            OleDbCommand cmd5 = new OleDbCommand("Insert into tblSupplieraccount(Tr_no,Invoiceno,Invoicedate,Paymenttype,Paymentflag,SupplierCode,Indate,Typeoftransaction,Vouchrno,Totalvalues,Bankaccno,Chequeno,Chequedate,Narration,Login_name,Sysdatetime,Mac_id)values('" + Tr_no + "','" + txtinvoiceno.Text + "','" + txtinvoicedate.Text + "','" + ddpaymenttype.SelectedItem.Text + "','Y','" + ddlsupplier.SelectedItem.Text + "','" + indate + "','C','" + Voachrno + "','" + box11.Text + "','" + Bankaccno + "','" + Chequeno + "','" + Chequedate + "','" + Narration + "','" + Session["username"].ToString() + "','" + sqlFormattedDate + "','" + sMacAddress + "')", conn12);
                            cmd5.ExecuteNonQuery();
                            conn12.Close();
                        }
                    }
                }

                // System.DateTime Dtnow = DateTime.Now;
                //string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");


            }

           

            string proamt = txttotalamount.Text;
            if (ddpaymenttype.SelectedItem.Text == "Credit" && ddlsupplier.SelectedItem.Text != "")
            {
                string suppliername = ddlsupplier.SelectedItem.Text;
               // string vreceptno = txtinvoiceno.Text;

                string vreceptno = clsgd.base64Encode(txtinvoiceno.Text);

                DataSet dsgroup1 = clsgd.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", suppliername);
                string scode = dsgroup1.Tables[0].Rows[0]["SupplierCode"].ToString();
               // string taxamoint = txtamount.Text;

                if (txtothers.Text != "")
                {
                    double others20 = Convert.ToDouble(txtothers.Text);
                    double pramt10 = Convert.ToDouble(txttotalamount.Text);

                    double rndoff = Convert.ToDouble(txtroundoff.Text);

                    if (txtroundoff.Text != "")
                    {

                           double rndoff20 = Convert.ToDouble(txtroundoff.Text);

                           if (rndoff20 > 0)
                           {
                               // double prmy25 = Convert.ToDouble(others20 - pramt10);

                               double prmy25 = Convert.ToDouble(pramt10 - (others20 + rndoff));

                               string prmy50 = Convert.ToString(prmy25);
                               ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9999", "N", "0000", vreceptno, "0000.00", prmy50, "0000.00", "0000.00", "0000.00", txtamount.Text, Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                               ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9988", "N", "0000", vreceptno, "0000.00", txtothers.Text, "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                               ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9988", "N", "0000", vreceptno, "0000.00", txtroundoff.Text, "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                           }
                           else
                           {
                               double prmy25 = Convert.ToDouble(pramt10 - (others20 + rndoff));

                               string prmy50 = Convert.ToString(prmy25);
                               ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9999", "N", "0000", vreceptno, "0000.00", prmy50, "0000.00", "0000.00", "0000.00", txtamount.Text, Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                               ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9988", "N", "0000", vreceptno, "0000.00", txtothers.Text, "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                               string rnd25 = Convert.ToString(rndoff20 * (-1));

                               ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9988", "N", "0000", vreceptno, rnd25, "0000.00", "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                             

                           }
                    }
                    else
                    {
                        double prmy25 = Convert.ToDouble(pramt10 - (others20));

                        string prmy50 = Convert.ToString(prmy25);
                        ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9999", "N", "0000", vreceptno, "0000.00", prmy50, "0000.00", "0000.00", "0000.00", txtamount.Text, Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                        ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9988", "N", "0000", vreceptno, "0000.00", txtothers.Text, "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                    }
                }
                else
                {

                    //ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9999", "N", "0000", vreceptno, "0000.00", proamt, "0000.00", "0000.00", "0000.00", txtamount.Text, Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                    if (txtroundoff.Text != "")
                    {
                        double rndoff20 = Convert.ToDouble(txtroundoff.Text);

                        if (rndoff20 > 0)
                        {
                            //double others20 = Convert.ToDouble(txtothers.Text);
                            double pramt10 = Convert.ToDouble(txttotalamount.Text);

                            double rndoff = Convert.ToDouble(txtroundoff.Text);
                            double prmy25 = Convert.ToDouble(pramt10 - (rndoff));

                            string prmy50 = Convert.ToString(prmy25);
                            ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9999", "N", "0000", vreceptno, "0000.00", prmy50, "0000.00", "0000.00", "0000.00", txtamount.Text, Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                            ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9988", "N", "0000", vreceptno, "0000.00", txtroundoff.Text, "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                        }
                        else
                        {
                           // double others20 = Convert.ToDouble(txtothers.Text);
                            double pramt10 = Convert.ToDouble(txttotalamount.Text);

                            double rndoff = Convert.ToDouble(txtroundoff.Text);
                            double prmy25 = Convert.ToDouble(pramt10 - (rndoff));

                            string prmy50 = Convert.ToString(prmy25);
                            ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9999", "N", "0000", vreceptno, "0000.00", prmy50, "0000.00", "0000.00", "0000.00", txtamount.Text, Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                            string rnd25 = Convert.ToString(rndoff20 * (-1));

                            ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9988", "N", "0000", vreceptno, rnd25, "0000.00", "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                        }

                    }

                }
                ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", scode, "9993", "N", "0000", vreceptno, proamt, "0000.00", "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
               
            }
            else if (ddpaymenttype.SelectedItem.Text == "Cash")
            {
                string suppliername = ddlsupplier.SelectedItem.Text;
                string vreceptno = clsgd.base64Encode(txtinvoiceno.Text);

                DataSet dsgroup1 = clsgd.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", suppliername);
                string scode = dsgroup1.Tables[0].Rows[0]["SupplierCode"].ToString();

                if (txtothers.Text != "")
                {
                    double others20 = Convert.ToDouble(txtothers.Text);
                    double pramt10 = Convert.ToDouble(txttotalamount.Text);

                     double rndoff = Convert.ToDouble(txtroundoff.Text);

                     if (txtroundoff.Text != "")
                     {
                          double rndoff20 = Convert.ToDouble(txtroundoff.Text);

                          if (rndoff20 > 0)
                          {

                              // double prmy25 = Convert.ToDouble(pramt10 - others20);

                              double prmy25 = Convert.ToDouble(pramt10 - (others20 + rndoff));

                              string prmy50 = Convert.ToString(prmy25);

                              ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", scode, "9999", "N", "0000", vreceptno, "0000.00", "0000.00", "0000.00", prmy50, "0000.00", txtamount.Text, Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                              ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9988", "N", "0000", vreceptno, "0000.00", "0000.00", "0000.00", txtothers.Text, "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                              ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9988", "N", "0000", vreceptno, "0000.00", "0000.00", "0000.00", txtroundoff.Text, "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                          }
                          else
                          {

                              double prmy25 = Convert.ToDouble(pramt10 - (others20 + rndoff));

                              string prmy50 = Convert.ToString(prmy25);

                              ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", scode, "9999", "N", "0000", vreceptno, "0000.00", "0000.00", "0000.00", prmy50, "0000.00", txtamount.Text, Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                              ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9988", "N", "0000", vreceptno, "0000.00", "0000.00", "0000.00", txtothers.Text, "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                              string rnd25 = Convert.ToString(rndoff20 * (-1));

                              ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9988", "N", "0000", vreceptno, "0000.00", "0000.00", "0000.00", rnd25, "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                          
                          }
                     }
                     else
                     {
                         double prmy25 = Convert.ToDouble(pramt10 - (others20));

                         string prmy50 = Convert.ToString(prmy25);

                         ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", scode, "9999", "N", "0000", vreceptno, "0000.00", "0000.00", "0000.00", prmy50, "0000.00", txtamount.Text, Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                         ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9988", "N", "0000", vreceptno, "0000.00", "0000.00", "0000.00", txtothers.Text, "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                     }
                }
                else
                {
                    if (txtroundoff.Text != "")
                    {
                        double rndoff20 = Convert.ToDouble(txtroundoff.Text);

                        if (rndoff20 > 0)
                        {

                            ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9988", "N", "0000", vreceptno, "0000.00", "0000.00", "0000.00", txtroundoff.Text, "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                        }
                        else
                        {

                            string rnd25 = Convert.ToString(rndoff20 * (-1));

                            ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9988", "N", "0000", vreceptno, "0000.00", "0000.00", rnd25, "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                        }

                    }

                    ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", scode, "9999", "N", "0000", vreceptno, "0000.00", "0000.00", proamt, "0000.00", "0000.00", txtamount.Text, Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                }

               

            }
            else
            {
                string vreceptno = clsgd.base64Encode(txtinvoiceno.Text);

                if (txtothers.Text != "")
                {

                    double others20 = Convert.ToDouble(txtothers.Text);
                    double pramt10 = Convert.ToDouble(txttotalamount.Text);

                    //double prmy25 = Convert.ToDouble(others20 - pramt10);

                    double prmy25 = Convert.ToDouble(pramt10 - others20);

                    string prmy50 = Convert.ToString(prmy25);


                    ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9999", "N", "0000", vreceptno, "0000.00", "0000.00", prmy50, "0000.00", "0000.00", txtamount.Text, Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                }
                else
                {
                    ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9999", "N", "0000", vreceptno, "0000.00", "0000.00", proamt, "0000.00", "0000.00", txtamount.Text, Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                }

                if (txtothers.Text != "")
                {
                    ClsBLGP2.Transaction("INSERT_TRANSACTION", transno, txtinvoicedate.Text, "0000", "0000", "9988", "N", "0000", vreceptno, "0000.00", txtothers.Text, "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                }

            }




            lblsuccess.Visible = true;
            lblsuccess.Text = "inserted successfully";
            txtinvoiceno.Text = string.Empty;
            txtinvoicedate.Text = string.Empty;
            txtinvoiceamount.Text = string.Empty;
            txtroundoff.Text = string.Empty;
            txttotalamount.Text = string.Empty;
            txtothers.Text = string.Empty;
            txtnarrations.Text = string.Empty;
            txtadjustamount.Text = string.Empty;
            txtothers.Enabled = true;

            supplier();

            System.DateTime Dtnow10 = DateTime.Now;

            txtinvoicedate.Text = Dtnow10.ToString("dd/MM/yyyy");




            txtnarrations.Visible = false;
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

    private string GetConnectionString()
    {
        //"DBConnection" is the name of the Connection String
        //that was set up from the web.config file
        return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
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
                    DateTime today = DateTime.Now;
                    DateTime answer = today.AddDays(90);
                    DateTime expdate1 = Convert.ToDateTime(expiredate1);

                    string close_flag = "Y";
                    DataSet dschm = clsgd.GetcondDataSet9("*", "tblProductMaster", "ProductName", productname1);
                    string gcode = dschm.Tables[0].Rows[0]["g_code"].ToString();

                    DataSet dschm10 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", gcode, "p_flag", close_flag);


                    if (dschm10.Tables[0].Rows.Count > 0)
                    {

                        if (expdate1 <= answer)
                        {
                            // Master.ShowModal("Expire Date minimum 90days greater than current  date", "txtexpiredate", 1);
                            ShowPopupMessage("Expire Date minimum 90days greater than current  date", PopupMessageType.txtexpiredate);
                            (Gridview1.Rows[0].Cells[1].FindControl("txtexpiredate") as TextBox).Enabled = true;
                            (Gridview1.Rows[0].Cells[1].FindControl("txtexpiredate") as TextBox).Focus();
                            return;

                        }
                    }



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

                      // TextBox batchidno = (TextBox)Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtbatchno");

                      // batchidno.Attributes["onfocus"] = "javascript:this.select();";
                       

                        // (Gridview1.Rows[row10.RowIndex].Cells[1].FindControl("txtquantity") as TextBox).Focus();
                       TextBox txt10 = (TextBox)sender;
                       GridViewRow row10 = (GridViewRow)txt10.NamingContainer;

                        (Gridview1.Rows[row10.RowIndex].FindControl("txtbatchno") as TextBox).Focus();

                        return;


                    }

                }
            }

            
            string invoiceamount1 = txtinvoiceamount.Text;
            if (invoiceamount1 == "")
            {
                Master.ShowModal("Enter invoice Amount", "txtinvoiceamount", 0);
                //ShowPopupMessage("Enter invoice Amount", PopupMessageType.);
                return;
            }
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

                DateTime today = DateTime.Now;
                DateTime answer = today.AddDays(90);
                DateTime expdate1 = Convert.ToDateTime(expiredate1);

                string close_flag = "Y";
                DataSet dschm = clsgd.GetcondDataSet9("*", "tblProductMaster", "ProductName", productname1);
                string gcode = dschm.Tables[0].Rows[0]["g_code"].ToString();

                DataSet dschm10 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", gcode, "p_flag", close_flag);


                if (dschm10.Tables[0].Rows.Count > 0)
                {

                    if (expdate1 <= answer)
                    {
                        TextBox txt10 = (TextBox)sender;
                        GridViewRow row10 = (GridViewRow)txt10.NamingContainer;
                        // Master.ShowModal("Expire Date minimum 90days greater than current  date", "txtexpiredate", 1);
                        ShowPopupMessage("Expire Date minimum 90days greater than current  date", PopupMessageType.txtexpiredate);

                        (Gridview1.Rows[row10.RowIndex].FindControl("txtexpiredate") as TextBox).Focus();

                        // (Gridview1.Rows[0].Cells[1].FindControl("txtexpiredate") as TextBox).Enabled = true;
                        // (Gridview1.Rows[0].Cells[1].FindControl("txtexpiredate") as TextBox).Focus();
                        return;

                    }
                }
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


                            double pvalue20 = Convert.ToDouble(balance * productprice * rateoftax / 100);
                            string pvalue30 = Math.Round(pvalue20, 2).ToString();



                            ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txttaxamount") as TextBox).Text) = Convert.ToString(pvalue30);

                            double productcost = Convert.ToDouble(balance * productprice * rateoftax / 100);

                            double cost = Convert.ToDouble(balance * productprice);


                            double pvalue = Convert.ToDouble(productcost + cost);
                            string pvalue10 = Math.Round(pvalue, 2).ToString();



                            ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproductvalue") as TextBox).Text) = Convert.ToString(pvalue10);

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
            double invoiceamount = Convert.ToDouble(txtinvoiceamount.Text);

            //double txtadjustamount=


            //double others = Convert.ToDouble(txtothers.Text);

            if (sum == invoiceamount)
            {
                txtadjustamount.Text = "0";
                txttotalamount.Text = Convert.ToString(sum);
                txtothers.Enabled = false;
                btnsave.Enabled = true;
                btnsave.Focus();
                return;

            }

            string others = txtothers.Text;
            string roundoff1 = txtroundoff.Text;

            if (others == "")
            {
                if (roundoff1 != "")
                {
                    txtnarrations.Visible = false;

                    txttotalamount.Text = (sum).ToString();
                    double roundoff = Convert.ToDouble(txtroundoff.Text);
                    //double others1 = Convert.ToDouble(txtothers.Text);

                    //double totalamount = invoiceamount + others1-;
                    double tamt1 = (sum - roundoff);
                    txttotalamount.Text = Math.Round(tamt1, 2).ToString();
                    double adjamt1 = (invoiceamount - (sum + roundoff));
                    txtadjustamount.Text = Math.Round(adjamt1, 2).ToString();
                }

                else
                {

                    //double totalamount = invoiceamount + others1-;
                    //txttotalamount.Text = (sum).ToString();
                    // txtadjustamount.Text = (invoiceamount - (sum)).ToString();
                    double tamt12 = sum;
                    txttotalamount.Text = Math.Round(tamt12, 2).ToString();
                    double adjamt2 = (invoiceamount - (sum));
                    txtadjustamount.Text = Math.Round(adjamt2, 2).ToString();

                }
            }
            else
            {
                txtnarrations.Visible = true;
                //txtothers.Text = "0";
                double others1 = Convert.ToDouble(txtothers.Text);
                //txtroundoff.Text = "0";
                //string roundoff1 = txtroundoff.Text;



                if (roundoff1 != "")
                {
                    double roundoff = Convert.ToDouble(txtroundoff.Text);
                    //txtadjustamount.Text = (invoiceamount - (sum + others1 + roundoff)).ToString();
                    double adjamt4 = (invoiceamount - (sum + others1 + roundoff));
                    txtadjustamount.Text = Math.Round(adjamt4, 2).ToString();
                    //double totalamount = invoiceamount + others1-;
                    //txttotalamount.Text = (sum + others1 - roundoff).ToString();
                    double ttamount = (sum + others1 - roundoff);
                    txttotalamount.Text = Math.Round(ttamount, 2).ToString();

                }
                else
                {
                    //txtadjustamount.Text = (invoiceamount - (sum + others1)).ToString();
                    // double calamount = invoiceamount + others1;
                    //txttotalamount.Text = (sum + others1).ToString();

                    double adjamt20 = (invoiceamount - (sum + others1));
                    txtadjustamount.Text = Math.Round(adjamt20, 2).ToString();
                    double calamount = invoiceamount + others1;
                    double ttamt21 = (sum + others1);
                    txttotalamount.Text = Math.Round(ttamt21, 2).ToString();
                }

            }

            double calamount1 = Convert.ToDouble(txttotalamount.Text);

            if (txtadjustamount.Text == "0")
            {
                btnsave.Enabled = true;
            }

           // else if (calamount1 < invoiceamount)
            // {
            //   AddNewRowToGrid();
            //  (Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Focus();
            // }
            else if (calamount1 <= invoiceamount)
            {
                btnsave.Enabled = true;
                btnsave.Focus();

            }
            else
            {
                Master.ShowModal("Adjust the invoice Amount", "txtinvoiceamount", 1);
                return;
            }

            // TextBox txt = (TextBox)sender;
            //GridViewRow row = (GridViewRow)txt.NamingContainer;
           // Gridview1.Rows[row.RowIndex].FindControl("txtfreesupply").Focus();

            TextBox batchidno = (TextBox)Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtfreesupply");

            // batchidno.Attributes.Add("onfocusin", "select();");
            (Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtfreesupply") as TextBox).Focus();
            batchidno.Attributes["onfocus"] = "javascript:this.select();";
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
                                        (Gridview1.Rows[rowIndex].Cells[1].FindControl("txtpurchaseprice") as TextBox).Focus();
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
                    double invoiceamount = Convert.ToDouble(txtinvoiceamount.Text);

                    //double txtadjustamount=


                    //double others = Convert.ToDouble(txtothers.Text);

                    if (sum == invoiceamount)
                    {
                        txtadjustamount.Text = "0";
                        txttotalamount.Text = Convert.ToString(sum);
                        txtothers.Enabled = false;
                        btnsave.Enabled = true;
                        btnsave.Focus();
                        return;

                    }

                    string others = txtothers.Text;
                    string roundoff1 = txtroundoff.Text;

                    if (others == "")
                    {
                        if (roundoff1 != "")
                        {
                            txtnarrations.Visible = false;

                            txttotalamount.Text = (sum).ToString();
                            double roundoff = Convert.ToDouble(txtroundoff.Text);
                            //double others1 = Convert.ToDouble(txtothers.Text);

                            //double totalamount = invoiceamount + others1-;
                            double tamt1 = (sum - roundoff);
                            txttotalamount.Text = Math.Round(tamt1, 2).ToString();
                            double adjamt1 = (invoiceamount - (sum + roundoff));
                            txtadjustamount.Text = Math.Round(adjamt1, 2).ToString();
                        }

                        else
                        {

                            //double totalamount = invoiceamount + others1-;
                            //txttotalamount.Text = (sum).ToString();
                            // txtadjustamount.Text = (invoiceamount - (sum)).ToString();
                            double tamt12 = sum;
                            txttotalamount.Text = Math.Round(tamt12, 2).ToString();
                            double adjamt2 = (invoiceamount - (sum));
                            txtadjustamount.Text = Math.Round(adjamt2, 2).ToString();

                        }
                    }
                    else
                    {
                        txtnarrations.Visible = true;
                        //txtothers.Text = "0";
                        double others1 = Convert.ToDouble(txtothers.Text);
                        //txtroundoff.Text = "0";
                        //string roundoff1 = txtroundoff.Text;



                        if (roundoff1 != "")
                        {
                            double roundoff = Convert.ToDouble(txtroundoff.Text);
                            //txtadjustamount.Text = (invoiceamount - (sum + others1 + roundoff)).ToString();
                            double adjamt4 = (invoiceamount - (sum + others1 + roundoff));
                            txtadjustamount.Text = Math.Round(adjamt4, 2).ToString();
                            //double totalamount = invoiceamount + others1-;
                            //txttotalamount.Text = (sum + others1 - roundoff).ToString();
                            double ttamount = (sum + others1 - roundoff);
                            txttotalamount.Text = Math.Round(ttamount, 2).ToString();

                        }
                        else
                        {
                            //txtadjustamount.Text = (invoiceamount - (sum + others1)).ToString();
                            // double calamount = invoiceamount + others1;
                            //txttotalamount.Text = (sum + others1).ToString();

                            double adjamt20 = (invoiceamount - (sum + others1));
                            txtadjustamount.Text = Math.Round(adjamt20, 2).ToString();
                            double calamount = invoiceamount + others1;
                            double ttamt21 = (sum + others1);
                            txttotalamount.Text = Math.Round(ttamt21, 2).ToString();
                        }

                    }


                    TextBox txt = (TextBox)sender;
                    GridViewRow row = (GridViewRow)txt.NamingContainer;
                    Gridview1.Rows[row.RowIndex].FindControl("txtpurchaseprice").Focus();


                   // TextBox box7 = (TextBox)Gridview1.Rows[row.RowIndex].Cells[7].FindControl("txttax");
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

    protected void txttax_TextChanged(object sender, EventArgs e)
    {



        string stockarrival = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txtstockarrival") as TextBox).Text);
        string freesupply = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txtfreesupply") as TextBox).Text);
        string tax = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txttax") as TextBox).Text);
        string purchaseprice = Convert.ToString((Gridview1.Rows[0].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text);





      


        if (purchaseprice == "")
        {
            // Master.ShowModal("Enter purchase price. !!!", "txtpurchaseprice", 1);
            // return;
        }




        if (tax != "0")
        {

        }

         if (purchaseprice  != "0")
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
                        if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[6].FindControl("txttax") as  TextBox).Text))
                        {
                            rateoftax = 0.0;
                        }
                        else
                        {
                            rateoftax = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[6].FindControl("txttax") as  TextBox).Text);
                        }

                        ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txttaxamount") as TextBox).Text) = Convert.ToString(balance * productprice * rateoftax / 100);

                        double productcost = Convert.ToDouble(balance * productprice * rateoftax / 100);

                        double cost = Convert.ToDouble(balance * productprice);

                        //((Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproductvalue") as TextBox).Text) = Convert.ToString(productcost + cost);
                        double pvalue = Convert.ToDouble(productcost + cost);
                        string pvalue10 = Math.Round(pvalue,2).ToString();
                        ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproductvalue") as TextBox).Text) = Convert.ToString(pvalue10);
                        (Gridview1.Rows[rowIndex].Cells[1].FindControl("txtpurchaseprice") as TextBox).Focus();

                        
                        rowIndex++;
                        
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
         double invoiceamount = Convert.ToDouble(txtinvoiceamount.Text);

         //double txtadjustamount=


         //double others = Convert.ToDouble(txtothers.Text);

        if (sum == invoiceamount)
        {
            txtadjustamount.Text = "0";
            txttotalamount.Text = Convert.ToString(sum);
            txtothers.Enabled = false;
            btnsave.Enabled = true;
            btnsave.Focus();
            return;

        }

        string others = txtothers.Text;
        string roundoff1 = txtroundoff.Text;

        if (others == "")
        {
            if (roundoff1 != "")
            {
                txtnarrations.Visible = false;

                txttotalamount.Text = (sum).ToString();
                double roundoff = Convert.ToDouble(txtroundoff.Text);
                //double others1 = Convert.ToDouble(txtothers.Text);

                //double totalamount = invoiceamount + others1-;
                double tamt1 = (sum - roundoff);
                txttotalamount.Text = Math.Round(tamt1, 2).ToString();
                double adjamt1 = (invoiceamount - (sum + roundoff));
                txtadjustamount.Text = Math.Round(adjamt1,2).ToString();
            }

            else
            {

                //double totalamount = invoiceamount + others1-;
                //txttotalamount.Text = (sum).ToString();
               // txtadjustamount.Text = (invoiceamount - (sum)).ToString();
                double tamt12 = sum;
                txttotalamount.Text = Math.Round(tamt12, 2).ToString();
                double adjamt2 = (invoiceamount - (sum));
                txtadjustamount.Text = Math.Round(adjamt2, 2).ToString();

            }
        }
        else
        {
            txtnarrations.Visible = true;
            //txtothers.Text = "0";
            double others1 = Convert.ToDouble(txtothers.Text);
            //txtroundoff.Text = "0";
            //string roundoff1 = txtroundoff.Text;



            if (roundoff1 != "")
            {
                double roundoff = Convert.ToDouble(txtroundoff.Text);
                //txtadjustamount.Text = (invoiceamount - (sum + others1 + roundoff)).ToString();
                double adjamt4 = (invoiceamount - (sum + others1 + roundoff));
                txtadjustamount.Text = Math.Round(adjamt4, 2).ToString();
                //double totalamount = invoiceamount + others1-;
                //txttotalamount.Text = (sum + others1 - roundoff).ToString();
                double ttamount = (sum + others1 - roundoff);
                txttotalamount.Text = Math.Round(ttamount, 2).ToString();

            }
            else
            {
                //txtadjustamount.Text = (invoiceamount - (sum + others1)).ToString();
               // double calamount = invoiceamount + others1;
                //txttotalamount.Text = (sum + others1).ToString();

                double adjamt20 = (invoiceamount - (sum + others1));
                txtadjustamount.Text=Math.Round(adjamt20,2).ToString();
                double calamount = invoiceamount + others1;
                double ttamt21 = (sum + others1);
                txttotalamount.Text = Math.Round(ttamt21, 2).ToString();
            }

        
         }


        
         

    }


    protected void txtpurchaseprice_TextChanged(object sender, EventArgs e)
    {
        try
        {
        string invoiceno = txtinvoiceno.Text;
        string invdate = txtinvoicedate.Text;
        string invoiveamount = txtinvoiceamount.Text;

        for (int k = 0; k < Gridview1.Rows.Count; k++)
        {
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

            if (invdate == "")
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


            //decimal purchaseprice10 = Convert.ToDecimal(purchaseprice);
            //decimal integral = Math.Truncate(purchaseprice10);
            //decimal fractional = purchaseprice10 - integral;
          

            string close_flag = "Y";
            DataSet dschm = clsgd.GetcondDataSet9("*", "tblProductMaster", "ProductName", productname1);
            string gcode = dschm.Tables[0].Rows[0]["g_code"].ToString();

            DataSet dschm10 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", gcode, "p_flag", close_flag);


            if (dschm10.Tables[0].Rows.Count > 0)
            {

                if (expdate1 <= answer)
                {
                    // Master.ShowModal("Expire Date minimum 90days greater than current  date", "txtexpiredate", 1);
                    ShowPopupMessage("Expire Date minimum 90days greater than current  date", PopupMessageType.txtexpiredate);
                    (Gridview1.Rows[0].Cells[1].FindControl("txtexpiredate") as TextBox).Enabled = true;
                    (Gridview1.Rows[0].Cells[1].FindControl("txtexpiredate") as TextBox).Focus();
                    return;

                }
            }

             string close_flag12="Y";    
           DataSet dschm20 = clsgd.GetcondDataSet9("*", "tblProductMaster", "ProductName", productname1);
           string gcode20 = dschm20.Tables[0].Rows[0]["g_code"].ToString();

           DataSet dschm25 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", gcode, "p_flag", close_flag12);


           if (dschm25.Tables[0].Rows.Count > 0)
           {

               if (expdate1 <= answer)
               {
                   // Master.ShowModal("Expire Date min 90 days greater than current date", "txtexpiredate", 0);
                   //return;
                   ShowPopupMessage("Expire Date min 90 days greater than current date", PopupMessageType.txtexpiredate);
                   return;

               }
           }

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

            if (invoiveamount == "")
            {
                Master.ShowModal("Invoice Amount is Mandatory", "txtinvoiceamount", 0);
                return;

            }


            //if (fractional == 0)
            //{
            //    string purchaseprice25 = (purchaseprice) + ".00";


            //    ((Gridview1.Rows[k].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text) = Convert.ToString(purchaseprice25);
               
            //}


            TextBox txt25 = (TextBox)sender;
            GridViewRow row25 = (GridViewRow)txt25.NamingContainer;

           //// Gridview1.Rows[row25.RowIndex].FindControl("txtpurchaseprice").te;


           // string prs = ((Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text);


           // decimal purchaseprice10 = Convert.ToDecimal(prs);
           // decimal integral = Math.Truncate(purchaseprice10);
           // decimal fractional = purchaseprice10 - integral;


           // if (fractional == 0)
           // {
           //     // string purchaseprice50 = Convert.ToString((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtpurchaseprice") as TextBox).Text);

           //     string purchaseprice25 = (prs) + ".00";

           //     ((Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text) = Convert.ToString(purchaseprice25);

           // }


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

                            double ttax= Convert.ToDouble(balance * productprice * rateoftax / 100); 
                              string taxvalue12 = Math.Round(ttax, 2).ToString();


                              ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txttaxamount") as TextBox).Text) = Convert.ToString(taxvalue12);

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
        double invoiceamount = Convert.ToDouble(txtinvoiceamount.Text);

        //double txtadjustamount=


        //double others = Convert.ToDouble(txtothers.Text);

        if (sum == invoiceamount)
        {
            txtadjustamount.Text = "0";
            txttotalamount.Text = Convert.ToString(sum);
            txtothers.Enabled = false;
            btnsave.Enabled = true;
            btnsave.Focus();
            return;

        }

        string others = txtothers.Text;
        string roundoff1 = txtroundoff.Text;

        if (others == "")
        {
            if (roundoff1 != "")
            {
                txtnarrations.Visible = false;

                txttotalamount.Text = (sum).ToString();
                double roundoff = Convert.ToDouble(txtroundoff.Text);
                //double others1 = Convert.ToDouble(txtothers.Text);

                //double totalamount = invoiceamount + others1-;
               

                double tmount10 = (sum - roundoff);
                txttotalamount.Text = Math.Round(tmount10, 2).ToString();

                double adjmnt20 = (invoiceamount - (sum + roundoff));
                txtadjustamount.Text = Math.Round(adjmnt20, 2).ToString();

            }

            else
            {

                //double totalamount = invoiceamount + others1-;
                //txttotalamount.Text = (sum).ToString();
                //txtadjustamount.Text = (invoiceamount - (sum)).ToString();


                double ttamt = sum;
                txttotalamount.Text = Math.Round(ttamt, 2).ToString();

                double adjamt10 = (invoiceamount - (sum));
                txtadjustamount.Text = Math.Round(adjamt10, 2).ToString();

            }
        }
        else
        {
            txtnarrations.Visible = true;
            //txtothers.Text = "0";
            double others1 = Convert.ToDouble(txtothers.Text);
            //txtroundoff.Text = "0";
            //string roundoff1 = txtroundoff.Text;



            if (roundoff1 != "")
            {
                double roundoff = Convert.ToDouble(txtroundoff.Text);
               // txtadjustamount.Text = (invoiceamount - (sum + others1 + roundoff)).ToString();
                //double totalamount = invoiceamount + others1-;
                double adjamt26 = (invoiceamount - (sum + others1 + roundoff));
                txtadjustamount.Text = Math.Round(adjamt26,2).ToString();
                //txttotalamount.Text = (sum + others1 - roundoff).ToString();
                double ttamt26 = (sum + others1 - roundoff);
                txttotalamount.Text = Math.Round(ttamt26,2).ToString();

            }
            else
            {
                //txtadjustamount.Text = (invoiceamount - (sum + others1)).ToString();
                double adjamt26 = (invoiceamount - (sum + others1));
                txtadjustamount.Text = Math.Round(adjamt26,2).ToString();
                double calamount = invoiceamount + others1;
                //txttotalamount.Text = (sum + others1).ToString();
                double ttamt26 = (sum + others1);
                txttotalamount.Text = Math.Round(ttamt26,2).ToString();
            }

        }



        // txtothers.Text = "0";
        //double others2 = Convert.ToDouble(txtothers.Text);
        double calamount1 = Convert.ToDouble(txttotalamount.Text);

        if (txtadjustamount.Text == "0")
        {
            btnsave.Enabled = true;
        }

       // else if (calamount1 < invoiceamount)
       // {
         //   AddNewRowToGrid();
          //  (Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Focus();
       // }
        else if (calamount1 <= invoiceamount)
        {
            btnsave.Enabled = true;
            btnsave.Focus();

        }
        else
        {
            Master.ShowModal("Adjust the invoice Amount", "txtinvoiceamount", 1);
            return;
        }

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
            string tax = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txttax")  as TextBox).Text);
            string purchaseprice = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text);
            string MRP = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtMRP") as TextBox).Text);
           // if (MRP == "")
        // {
           // Master.ShowModal("Enter MRP. !!!", "txtpurchaseprice", 1);
            //return;
            // ShowPopupMessage("Enter MRP. !!!", PopupMessageType.txtMRP);
            // return;
        // }
        if (txtinvoiceamount.Text == "")
        {
            Master.ShowModal("Enter Invoice Amount", "txtinvoiceamount", 0);
            return;
        }
          
              double purchaseprice1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text);
                    double  MRP1 = Convert.ToDouble((Gridview1.Rows[k].Cells[1].FindControl("txtMRP") as TextBox).Text);

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

        //// Gridview1.Rows[row25.RowIndex].FindControl("txtpurchaseprice").te;


        // string prs = ((Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text);


        // decimal purchaseprice10 = Convert.ToDecimal(prs);
        // decimal integral = Math.Truncate(purchaseprice10);
        // decimal fractional = purchaseprice10 - integral;


        // if (fractional == 0)
        // {
        //     // string purchaseprice50 = Convert.ToString((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtpurchaseprice") as TextBox).Text);

        //     string purchaseprice25 = (prs) + ".00";

        //     ((Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text) = Convert.ToString(purchaseprice25);

        // }


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



                    //((Gridview1.Rows[rowIndex].Cells[10].FindControl("txttaxamount") as TextBox).Text) = Convert.ToString(balance * productprice * rateoftax / 100);

                    double ttax = Convert.ToDouble(balance * productprice * rateoftax / 100);
                    string taxvalue12 = Math.Round(ttax, 2).ToString();


                    ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txttaxamount") as TextBox).Text) = Convert.ToString(taxvalue12);

                    double productcost = Convert.ToDouble(balance * productprice * rateoftax / 100);

                    double cost = Convert.ToDouble(balance * productprice);

                   // ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproductvalue") as TextBox).Text) = Convert.ToString(productcost + cost);

                    double pvalue = Convert.ToDouble(productcost + cost);
                    string pvalue10 = Math.Round(pvalue, 2).ToString();
                    ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproductvalue") as TextBox).Text) = Convert.ToString(pvalue10);

                   
                    rowIndex++;

                    string Sysdatetime=DateTime.Now.ToString();
                    string rateoftax1= rateoftax.ToString();


                    // ClsBLGP1.tempproductinwrad("INSERT_tempproductinwrad", Productcode,ProductName,batchno,rateoftax1,Session["username"].ToString(),sMacAddress,Sysdatetime);
                }

            }
        
      }


        if (((Gridview1.Rows[0].Cells[10].FindControl("txtproductvalue") as TextBox).Text) == txtinvoiceamount.Text)
        {
            //txtadjustamount.Text = "0";
            //btnsave.Enabled = true;
            //btnsave.Focus();
            //return;

            txtadjustamount.Text = "0";
            txtothers.Enabled = false;
            btnsave.Enabled = true;
            btnsave.Focus();
            string tax10 = ((Gridview1.Rows[0].Cells[9].FindControl("txttaxamount") as TextBox).Text);
            txtamount.Text = tax10.ToString();
            return;
        }


        double sum = 0;
        double taxamt = 0;

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

            if (String.IsNullOrEmpty((Gridview1.Rows[j].Cells[1].FindControl("txttaxamount") as TextBox).Text))
            {
                Double tax = 0.0;

            }
            else
            {

                Double tax = Convert.ToDouble((Gridview1.Rows[j].Cells[1].FindControl("txttaxamount") as TextBox).Text);
                taxamt = taxamt + tax;
            }




        }
        double invoiceamount = Convert.ToDouble(txtinvoiceamount.Text);
        txtamount.Text = Convert.ToString(taxamt);
       // txtamount.Text = taxamt.ToString();

        //double txtadjustamount=


        //double others = Convert.ToDouble(txtothers.Text);



        if (sum == invoiceamount)
        {
            txtadjustamount.Text = "0";
            txttotalamount.Text = Convert.ToString(sum);
            txtothers.Enabled = false;
            btnsave.Enabled = true;
            btnsave.Focus();
            return;

        }

        string others = txtothers.Text;
        string roundoff1 = txtroundoff.Text;

            if(txtadjustamount.Text != "0")
            {


        if (others == "")
        {
            if (roundoff1 != "")
            {
                txtnarrations.Visible = false;

                //txttotalamount.Text = (sum).ToString();
                double ttamt25 = (sum);
                txttotalamount.Text = Math.Round(ttamt25, 2).ToString();
                double roundoff = Convert.ToDouble(txtroundoff.Text);
                //double others1 = Convert.ToDouble(txtothers.Text);

                //double totalamount = invoiceamount + others1-;
                //txttotalamount.Text = (sum - roundoff).ToString();
                //txtadjustamount.Text = (invoiceamount - (sum + roundoff)).ToString();

                if (roundoff <= 1)
                {

                    double ttamt23 = (sum + roundoff);
                    txttotalamount.Text = Math.Round(ttamt23, 2).ToString();
                    double adaj23 = (invoiceamount - (sum + roundoff));
                    txtadjustamount.Text = Math.Round(adaj23, 2).ToString();
                   // btnsave.Enabled = true;
                    //btnsave.Focus();
                }
                else
                {
                    double ttamt23 = (sum - roundoff);
                    txttotalamount.Text = Math.Round(ttamt23, 2).ToString();
                    double adaj23 = (invoiceamount - (sum + roundoff));
                    txtadjustamount.Text = Math.Round(adaj23, 2).ToString();
                   // btnsave.Enabled = true;
                   // btnsave.Focus();

                }
               

            }

            else
            {

                //double totalamount = invoiceamount + others1-;
                //txttotalamount.Text = (sum).ToString();
                //txtadjustamount.Text = (invoiceamount - (sum)).ToString();

                double ttamt24 = (sum);
                txttotalamount.Text = Math.Round(ttamt24,2).ToString();

                double tadj24 = (invoiceamount - (sum));
                txtadjustamount.Text = Math.Round(tadj24, 2).ToString();

            }
        }
        else
        {
            txtnarrations.Visible = true;
            //txtothers.Text = "0";
            double others1 = Convert.ToDouble(txtothers.Text);
            //txtroundoff.Text = "0";
            //string roundoff1 = txtroundoff.Text;



            if (roundoff1 != "")
            {
                double roundoff = Convert.ToDouble(txtroundoff.Text);
                //txtadjustamount.Text = (invoiceamount - (sum + others1 + roundoff)).ToString();

                double adjamt25 = (invoiceamount - (sum + others1 + roundoff));
                txtadjustamount.Text = Math.Round(adjamt25, 2).ToString();
                //double totalamount = invoiceamount + others1-;
               // txttotalamount.Text = (sum + others1 - roundoff).ToString();
                double ttamt25 = (sum + others1 - roundoff);
                txttotalamount.Text = Math.Round(ttamt25,2).ToString();

            }
            else
            {
               // txtadjustamount.Text = (invoiceamount - (sum + others1)).ToString();
                double adjamt26 = (invoiceamount - (sum + others1));
                txtadjustamount.Text = Math.Round(adjamt26, 2).ToString();
                double calamount = invoiceamount + others1;
                //txttotalamount.Text = (sum + others1).ToString();
                double ttamt26 = (sum + others1);
                txttotalamount.Text = Math.Round(ttamt26, 2).ToString();
            }

        }

        }
      

        TextBox txt = (TextBox)sender;
        GridViewRow row = (GridViewRow)txt.NamingContainer;
        //Gridview1.Rows[row.RowIndex].FindControl("txtMRP").Focus();
        

        // txtothers.Text = "0";
        //double others2 = Convert.ToDouble(txtothers.Text);
       double calamount1 = Convert.ToDouble(txttotalamount.Text);

       double invamt = Convert.ToDouble(txtinvoiceamount.Text);

       double tbalance = Convert.ToDouble(invamt - calamount1);

       txtadjustamount.Text = Math.Round(tbalance, 2).ToString(); ;



        if (txtadjustamount.Text == "0")
        {
            btnsave.Enabled = true;
            btnsave.Focus();
        }

        else if (calamount1 < invoiceamount)
        {
            //AddNewRowToGrid();
           //(Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode") as TextBox).Focus();
            (Gridview1.Rows[row.RowIndex].Cells[1].FindControl("ButtonAdd") as Button).Focus();


        }
        else if (calamount1 <= invoiceamount)
        {
            btnsave.Enabled = true;
            btnsave.Focus();

        }
        else
        {
            Master.ShowModal("Adjust the invoice Amount", "txtinvoiceamount", 1);
            return;
        }
        
       // GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
        //row.Focus();

       //(Gridview1.Rows[rowIndex].FindControl("ButtonAdd") as Button).Focus();
       //Gridview1.Rows[rowIndex].Enabled = true;


       // (Gridview1.FindControl("ButtonAdd") as LinkButton).Focus();
        
        }

         catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
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
                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtbatchno");
                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate");
                    TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival");
                    TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply");
                    TextBox box6 = (TextBox)Gridview1.Rows[rowIndex].Cells[7].FindControl("txttax");
                    TextBox box7 = (TextBox)Gridview1.Rows[rowIndex].Cells[8].FindControl("txtpurchaseprice");
                    TextBox box8 = (TextBox)Gridview1.Rows[rowIndex].Cells[9].FindControl("txtMRP");
                    TextBox box9 = (TextBox)Gridview1.Rows[rowIndex].Cells[10].FindControl("txtTaxamount");
                    TextBox box10 = (TextBox)Gridview1.Rows[rowIndex].Cells[11].FindControl("txtproductvalue");
                    TextBox box11 = (TextBox)Gridview1.Rows[rowIndex].Cells[11].FindControl("txtgroupname");

                   // box6.Enabled = false;

                    box0.Text = dt.Rows[i]["Productcode"].ToString();
                    box1.Text = dt.Rows[i]["ProductName"].ToString();
                    box2.Text = dt.Rows[i]["Batchid"].ToString();
                    box3.Text = dt.Rows[i]["Expiredate"].ToString();
                    box4.Text = dt.Rows[i]["Stockinward"].ToString();
                    box5.Text = dt.Rows[i]["Freesupply"].ToString();
                    box6.Text = dt.Rows[i]["Tax"].ToString();
                    box7.Text = dt.Rows[i]["Purchaseprice"].ToString();
                    box8.Text = dt.Rows[i]["MRP"].ToString();
                    box9.Text = dt.Rows[i]["TaxAmount"].ToString();
                    box10.Text = dt.Rows[i]["Totalvalues"].ToString();
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

            if (ddlsupplier.SelectedItem.Text == "-Select-")
            {
                Master.ShowModal("Please select a Supplier Name   . !!!", "ddlsupplier", 1);
                return;
            }

            string filename = Dbconn.Mymenthod();

            TextBox txt10 = (TextBox)sender;
            GridViewRow row10 = (GridViewRow)txt10.NamingContainer;
            
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
                       // (Gridview1.Rows[i].Cells[1].FindControl("txtproductcode") as TextBox).Focus();

                        return;

                    }


                    //DataSet dsprodin = clsgd.GetcondDataSet("*", "tblProductMaster", "g_code", lblgroupname.Text);
                    //string g_code = Convert.ToString(dsprodin.Tables[0].Rows[0]["g_code"].ToString());
                    //DataSet dsprodin10 = clsgd.GetcondDataSet("*", "tblGroup", "g_code", g_code);
                    //string gcode = Convert.ToString(dsprodin10.Tables[0].Rows[0]["g_code"].ToString());

                    //DataSet dsprodin12 = clsgd.GetcondDataSet("*", "tblTax_Rate", "g_code", gcode);
                    

                    //if (dsprodin12.Tables[0].Rows.Count > 0)
                    //{
                    //    string Tax_Rate = Convert.ToString(dsprodin12.Tables[0].Rows[0]["Tax_Rate"].ToString());

                    //    ((Gridview1.Rows[i].Cells[1].FindControl("txttax") as TextBox).Text) = Tax_Rate.ToString();


                    //}

                    DataSet dsprodin = clsgd.GetcondDataSet("*", "tblProductMaster", "Productcode", ID);

                    // DataSet dsprodin = clsgd.GetcondDataSet("*", "tblProductMaster", "g_code", lblgroupname.Text);
                    string g_code = Convert.ToString(dsprodin.Tables[0].Rows[0]["g_code"].ToString());
                    DataSet dsprodin10 = clsgd.GetcondDataSet("*", "tblGroup", "g_code", g_code);
                    string gcode = Convert.ToString(dsprodin10.Tables[0].Rows[0]["g_code"].ToString());

                    string Close_flag = "N";

                    DataSet dsprodin12 = clsgd.GetcondDataSet2("*", "tblTax_Rate", "g_code", gcode, "Close_flag", Close_flag);
                   

                    if (dsprodin12.Tables[0].Rows.Count > 0)
                    {
                        string Tax_Rate = Convert.ToString(dsprodin12.Tables[0].Rows[0]["Tax_Rate"].ToString());
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
                            //mahesh bhat
                            for (int j = 0; j < Gridview1.Rows.Count - 1; j++)
                            {
                                //string productname1 = Convert.ToString((Gridview1.Rows[j].Cells[1].FindControl("txtproductname") as TextBox).Text);
                                string productcode1 = Convert.ToString((Gridview1.Rows[j].Cells[1].FindControl("txtproductcode") as TextBox).Text);
                                string close_flag1 = "N";
                                DataSet dschm = clsgd.GetcondDataSet2("*", "tblProductMaster", "Productcode", productcode1, "Pharmflag", close_flag1);
                                if (dschm.Tables[0].Rows.Count > 0)
                                {
                                    string productcode11 = dschm.Tables[0].Rows[0]["Productcode"].ToString();
                                    string g_code1 = dschm.Tables[0].Rows[0]["g_code"].ToString();
                                    DataSet dschm10 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", g_code1, "p_flag", close_flag1);
                                    if (dschm10.Tables[0].Rows.Count > 0)
                                    {
                                        string gcode1 = dschm.Tables[0].Rows[0]["g_code"].ToString();
                                        DataSet dschm100 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", gcode1, "p_flag", close_flag1);
                                    }
                                    string productcode12 = Convert.ToString((Gridview1.Rows[row10.RowIndex].FindControl("txtproductcode") as TextBox).Text);
                                    string productname12 = Convert.ToString((Gridview1.Rows[row10.RowIndex].FindControl("txtproductname") as TextBox).Text);
                                    if (productcode11 == productcode12)
                                    {
                                        ShowPopupMessage("Product Name Alredy Exists!!!", PopupMessageType.txtproductcode);
                                        ((Gridview1.Rows[row10.RowIndex].Cells[1].FindControl("txtproductcode") as TextBox).Text) = string.Empty;
                                        ((Gridview1.Rows[row10.RowIndex].Cells[1].FindControl("txtproductname") as TextBox).Text) = string.Empty;
                                        return;
                                    }
                                }
                            }
                            //mahesh bhat
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
      //  Gridview1.Rows[row1.RowIndex].FindControl("txtproductname").Focus();

        ((Gridview1.Rows[row1.RowIndex].FindControl("txtfreesupply") as TextBox).Text) = "0";






       
       
    }
       

    protected void btnExit_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("Home.aspx");
    }

    

    protected void Gridview1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //TextBox btn = (TextBox)e.Row.FindControl("txtbatchno"); // give property id of button form template field
        //btn.Enabled = false; 

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].BackColor = Color.White;
                //Button buttonId = (Button)e.Row.FindControl("ButtonAdd");
                //buttonId.Enabled = false;

               
                conn.Close();

            }
        }

       // using (SqlConnection conn = new SqlConnection())
      //  {
          //  conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
           // if (e.Row.RowType == DataControlRowType.DataRow)
           // {



              //  DropDownList ddll = (DropDownList)e.Row.FindControl("ddltax");

              //  ddll.Items.Insert(0, new ListItem("--Select--", "0"));

               // conn.Close();

          //  }
       // }


    }

    protected void txtinvoicedate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime startdate = Convert.ToDateTime(txtinvoicedate.Text);
            DateTime enddate = Convert.ToDateTime(txtdate.Text);
            if (startdate > enddate)
            {
                Master.ShowModal("Invoice Date cannot be greater than current date. !!!", "txtinvoicedate", 1);
                return;
            }


        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            Master.ShowModal("Invalid date format...", "txtinvoicedate", 1);
            return;
        }
        txtinvoiceamount.Enabled = true;
        txtinvoiceamount.Focus();

    }
    protected void txtinvoiceamount_TextChanged(object sender, EventArgs e)
    {
        string invoiceamount1 = txtinvoiceamount.Text;

        decimal invoiceamount15 = Convert.ToDecimal(invoiceamount1);
        txtinvoiceamount.Text = invoiceamount15.ToString("F");


       // string balamt20 = txtadjustamount.Text;
       // decimal balamt = Convert.ToDecimal(balamt20);

        //txtadjustamount.Text = balamt.ToString("F");

        if (invoiceamount1 == "")
        {
            Master.ShowModal("Enter invoice Amount", "txtinvoiceamount", 1);
            return;
        }

        if (ddpaymenttype.SelectedItem.Text == "Cash")
        {
           // chkpayment.Enabled = true;
           // chkpayment.Focus();

            ddlsupplier.Enabled = true;
            ddlsupplier.Focus();
            ddlsupplier.BorderColor = System.Drawing.Color.Black;
            ddlsupplier.BorderWidth = 1;
            ddlsupplier.BorderStyle = BorderStyle.Dotted;

        }
        else
        {
            ddlsupplier.Enabled = true;
            ddlsupplier.Focus();
            ddlsupplier.BorderColor = System.Drawing.Color.Black;
            ddlsupplier.BorderWidth = 1;
            ddlsupplier.BorderStyle = BorderStyle.Dotted;
        }

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


                    rowIndex++;

                }

            }

        }

        Double sum = 0;


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


            //TimelineID = ((TextBox)gvTimeline.Rows[i].FindControl("txtTimeline")).Text.Trim();

        }

        // txttotalamount.Text = (sum).ToString();

        double invoiceamount = Convert.ToDouble(txtinvoiceamount.Text);


        //double others = Convert.ToDouble(txtothers.Text);
        for (int jj = 0; jj < Gridview1.Rows.Count; jj++)
        {
            if (String.IsNullOrEmpty((Gridview1.Rows[jj].Cells[1].FindControl("txtproductcode") as TextBox).Text))
            {

            }
            else
            {
                if (String.IsNullOrEmpty((Gridview1.Rows[jj].Cells[1].FindControl("txtMRP") as TextBox).Text))
                {
                    ShowPopupMessage("Enter MRP", PopupMessageType.txtMRP);
                    return;

                }
                else
                {

                    //  Double add = Convert.ToDouble((Gridview1.Rows[jj].Cells[1].FindControl("txtproductvalue") as TextBox).Text);
                    // sum = sum + add;
                }

            }
        }

        if (sum == invoiceamount)
        {
            txtadjustamount.Text = "0";
            txtothers.Enabled = false;
            btnsave.Enabled = true;
            btnsave.Focus();
            return;

        }



        string others = txtothers.Text;
        string roundoff1 = txtroundoff.Text;

        if (others == "")
        {
            if (roundoff1 != "")
            {
                txtnarrations.Visible = false;

                txttotalamount.Text = (sum).ToString();
                double roundoff = Convert.ToDouble(txtroundoff.Text);
                //double others1 = Convert.ToDouble(txtothers.Text);

                //double totalamount = invoiceamount + others1-;
                txttotalamount.Text = (sum - roundoff).ToString();
                txtadjustamount.Text = (invoiceamount - (sum + roundoff)).ToString();
            }

            else
            {

                //double totalamount = invoiceamount + others1-;
                txttotalamount.Text = (sum).ToString();
                txtadjustamount.Text = (invoiceamount - (sum)).ToString();

                decimal adjamount15 = Convert.ToDecimal(txtadjustamount.Text);
                txtadjustamount.Text = adjamount15.ToString("F");
            }
        }
        else
        {
            txtnarrations.Visible = true;
            //txtothers.Text = "0";
            double others1 = Convert.ToDouble(txtothers.Text);
            //txtroundoff.Text = "0";
            //string roundoff1 = txtroundoff.Text;



            if (roundoff1 != "")
            {
                double roundoff = Convert.ToDouble(txtroundoff.Text);
                txtadjustamount.Text = (invoiceamount - (sum + others1 + roundoff)).ToString();
                //double totalamount = invoiceamount + others1-;
                txttotalamount.Text = (sum + others1 - roundoff).ToString();

            }
            else
            {
                txtadjustamount.Text = (invoiceamount - (sum + others1)).ToString();
                double calamount = invoiceamount + others1;
                txttotalamount.Text = (sum + others1).ToString();
            }

        }



        // txtothers.Text = "0";
        //double others2 = Convert.ToDouble(txtothers.Text);
        double calamount1 = Convert.ToDouble(txttotalamount.Text);
        //if (calamount1 > invoiceamount)
        //{
       // string mrp = Convert.ToString((Gridview1.Rows[rowIndex].Cells[1].FindControl("txtMRP") as TextBox).Text);

        //if (mrp == "")
        //{
          //  ShowPopupMessage("Enter MRP", PopupMessageType.txtMRP);
            //return;
        ///}
        //}

       //code  change

       // double invceamt = Convert.ToDouble(txtinvoiceamount.Text);

        //txtadjustamount.Text = (invceamt - calamount1).ToString();


        if (txtadjustamount.Text == "0")
        {
            txtothers.Enabled = false;
            btnsave.Enabled = true;
            btnsave.Focus();
            return;
        }
        else if (txttotalamount.Text == "0")
        {
            
           // txtdate.Enabled = true;
            //txtdate.Focus();
        }


        else if (calamount1 < invoiceamount)
        {
           // AddNewRowToGrid();
           // (Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode") as TextBox).Focus();

            TextBox txt25 = (TextBox)sender;
            GridViewRow row25 = (GridViewRow)txt25.NamingContainer;

            (Gridview1.Rows[row25.RowIndex].Cells[1].FindControl("ButtonAdd") as Button).Focus();


            
        }
        else if (calamount1 <= invoiceamount)
        {
            btnsave.Enabled = true;
            btnsave.Focus();

        }
        else
        {
            Master.ShowModal("Adjust the invoice Amount", "txtinvoiceamount", 1);
            return;
        }

       // TextBox txt = (TextBox)sender;
      //  GridViewRow row = (GridViewRow)txt.NamingContainer;

        
        //chkpayment.Visible = true;
        //lblpayment.Visible = true;
        if (txtinvoiceamount.Text == "")
        {
        }
        else
        {
            
        }
        if (ddpaymenttype.SelectedItem.Text == "Credit")
        {
            ddlsupplier.Enabled = true;
            ddlsupplier.Focus();
            chkpayment.Enabled = false;
        }
       else
        {

            
            (Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Focus();
            
         
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

    protected void txtproductname_TextChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlsupplier.SelectedItem.Text == "-Select-")
            {
                Master.ShowModal("Please select a Supplier Name   . !!!", "ddlsupplier", 1);
                return;
            }


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


                    //DataSet dsprodin = clsgd.GetcondDataSet("*", "tblProductMaster", "g_code", lblgroupname.Text);
                    //string g_code = Convert.ToString(dsprodin.Tables[0].Rows[0]["g_code"].ToString());
                    //DataSet dsprodin10 = clsgd.GetcondDataSet("*", "tblGroup", "g_code", g_code);
                    //string gcode = Convert.ToString(dsprodin10.Tables[0].Rows[0]["g_code"].ToString());

                    //DataSet dsprodin12 = clsgd.GetcondDataSet("*", "tblTax_Rate", "g_code", gcode);
                    //string Tax_Rate = Convert.ToString(dsprodin12.Tables[0].Rows[0]["Tax_Rate"].ToString());

                    //if (dsprodin12.Tables[0].Rows.Count > 0)
                    //{

                    //    ((Gridview1.Rows[i].Cells[1].FindControl("txttax") as TextBox).Text) = Tax_Rate.ToString();


                    //}

                    DataSet dsprodin = clsgd.GetcondDataSet("*", "tblProductMaster", "Productname", ID);

                    // DataSet dsprodin = clsgd.GetcondDataSet("*", "tblProductMaster", "g_code", lblgroupname.Text);
                    string g_code = Convert.ToString(dsprodin.Tables[0].Rows[0]["g_code"].ToString());
                    DataSet dsprodin10 = clsgd.GetcondDataSet("*", "tblGroup", "g_code", g_code);
                    string gcode = Convert.ToString(dsprodin10.Tables[0].Rows[0]["g_code"].ToString());

                    string Close_flag = "N";

                    DataSet dsprodin12 = clsgd.GetcondDataSet2("*", "tblTax_Rate", "g_code", gcode, "Close_flag", Close_flag);
                  

                    if (dsprodin12.Tables[0].Rows.Count > 0)
                    {
                        string Tax_Rate = Convert.ToString(dsprodin12.Tables[0].Rows[0]["Tax_Rate"].ToString());
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
                  




                    (Gridview1.Rows[i].Cells[1].FindControl("txtbatchno") as TextBox).Focus();
                }


                //TextBox txt = (TextBox)sender;
                //GridViewRow row = (GridViewRow)txt.NamingContainer;

                ////string productcode = Convert.ToString((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtproductcode") as TextBox).Text);
                //((Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtbatchno") as TextBox).Text) = "0";
                //TextBox batchidno = (TextBox)Gridview1.Rows[row.RowIndex].Cells[1].FindControl("txtbatchno");
                //batchidno.Attributes.Add("onfocusin", "select();");

                string pharmflag = "Y";
                //DataSet pharm = clsgd.GetcondDataSet("*", "tblProductMaster", "Pharmflag", pharmflag);
                for (int j = 0; j < Gridview1.Rows.Count; j++)
                {
                    string ID1 = Convert.ToString((Gridview1.Rows[j].Cells[1].FindControl("txtproductname") as TextBox).Text);

                    DataSet pharm1 = clsgd.GetcondDataSet2("*", "tblProductMaster", "Pharmflag", pharmflag, "Productname", ID1);

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
            //mahesh bhat
            for (int j = 0; j < Gridview1.Rows.Count - 1; j++)
            {
                TextBox txt10 = (TextBox)sender;
                GridViewRow row10 = (GridViewRow)txt10.NamingContainer;
                string productname1 = Convert.ToString((Gridview1.Rows[j].Cells[1].FindControl("txtproductname") as TextBox).Text);
                //string productcode1 = Convert.ToString((Gridview1.Rows[j].Cells[1].FindControl("txtproductcode") as TextBox).Text);
                string close_flag1 = "N";
                DataSet dschm = clsgd.GetcondDataSet2("*", "tblProductMaster", "Productname", productname1, "Pharmflag", close_flag1);
                if (dschm.Tables[0].Rows.Count > 0)
                {
                    string productcode11 = dschm.Tables[0].Rows[0]["Productcode"].ToString();
                    string g_code1 = dschm.Tables[0].Rows[0]["g_code"].ToString();
                    DataSet dschm10 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", g_code1, "p_flag", close_flag1);
                    if (dschm10.Tables[0].Rows.Count > 0)
                    {
                        string gcode1 = dschm.Tables[0].Rows[0]["g_code"].ToString();
                        DataSet dschm100 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", gcode1, "p_flag", close_flag1);
                    }
                    string productcode12 = Convert.ToString((Gridview1.Rows[row10.RowIndex].FindControl("txtproductcode") as TextBox).Text);
                    string productname12 = Convert.ToString((Gridview1.Rows[row10.RowIndex].FindControl("txtproductname") as TextBox).Text);
                    if (productcode11 == productcode12)
                    {
                        ShowPopupMessage("Product Name Alredy Exists!!!", PopupMessageType.txtproductcode);
                        ((Gridview1.Rows[row10.RowIndex].Cells[1].FindControl("txtproductcode") as TextBox).Text) = string.Empty;
                        ((Gridview1.Rows[row10.RowIndex].Cells[1].FindControl("txtproductname") as TextBox).Text) = string.Empty;
                        return;
                    }
                }
            }
            //mahesh bhat
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

    protected void txtothers_TextChanged(object sender, EventArgs e)
    {
        for (int k = 0; k < Gridview1.Rows.Count; k++)
        {

            string expdate = Convert.ToString((Gridview1.Rows[k].Cells[1].FindControl("txtproductcode") as TextBox).Text);
            if (expdate == "")
            {

                //Master.ShowModal("MRP is Mandatory", "txtMRP", 1);
                //return;
                ShowPopupMessage("Delete a blank row is Mandatory", PopupMessageType.txtMRP);
                txtothers.Text=string.Empty;
                SetPreviousData();
                return;
                 
            }


        }
       
        

        string others2 = txtothers.Text;
        if (others2 == "0")
        {

            txtnarrations.Visible = false;
        }
        else
        {
            txtnarrations.Visible = true;
            txtnarrations.Focus();
        }


        Double sum = 0;


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


            //TimelineID = ((TextBox)gvTimeline.Rows[i].FindControl("txtTimeline")).Text.Trim();

        }

        // txttotalamount.Text = (sum).ToString();

        double invoiceamount = Convert.ToDouble(txtinvoiceamount.Text);


        //double others = Convert.ToDouble(txtothers.Text);

        if (sum == invoiceamount)
        {
            txtadjustamount.Text = "0";
            btnsave.Enabled = true;
            btnsave.Focus();
            return;

        }

        string others = txtothers.Text;
        string roundoff1 = txtroundoff.Text;

        if (others == "")
        {
            if (roundoff1 != "")
            {
                txtnarrations.Visible = false;

                txttotalamount.Text = (sum).ToString();
                double roundoff = Convert.ToDouble(txtroundoff.Text);
                //double others1 = Convert.ToDouble(txtothers.Text);

                //double totalamount = invoiceamount + others1-;
                txttotalamount.Text = (sum - roundoff).ToString();
                txtadjustamount.Text = (invoiceamount - (sum + roundoff)).ToString();
            }

            else
            {

                //double totalamount = invoiceamount + others1-;
                txttotalamount.Text = (sum).ToString();
                txtadjustamount.Text = (invoiceamount - (sum)).ToString();
            }
        }
        else
        {
            txtnarrations.Visible = true;
            //txtothers.Text = "0";
            double others1 = Convert.ToDouble(txtothers.Text);
            //txtroundoff.Text = "0";
            //string roundoff1 = txtroundoff.Text;



            if (roundoff1 != "")
            {
                //double roundoff = Convert.ToDouble(txtroundoff.Text);
                //txtadjustamount.Text = (invoiceamount - (sum + others1 + roundoff)).ToString();
               // txttotalamount.Text = (sum + others1 - roundoff).ToString();
                double roundoff = Convert.ToDouble(txtroundoff.Text);
                // txtadjustamount.Text = (invoiceamount - (sum + others1 + roundoff)).ToString();
                //double totalamount = invoiceamount + others1-;
                double adjamt26 = (invoiceamount - (sum + others1 + roundoff));
                txtadjustamount.Text = Math.Round(adjamt26, 2).ToString();
                //txttotalamount.Text = (sum + others1 - roundoff).ToString();
                double ttamt26 = (sum + others1 - roundoff);
                txttotalamount.Text = Math.Round(ttamt26, 2).ToString();

            }
            else
            {
               // txtadjustamount.Text = (invoiceamount - (sum + others1)).ToString();
               // double calamount = invoiceamount + others1;
                //txttotalamount.Text = (sum + others1).ToString();
                //txtadjustamount.Text = (invoiceamount - (sum + others1)).ToString();
                double adjamt26 = (invoiceamount - (sum + others1));
                txtadjustamount.Text = Math.Round(adjamt26, 2).ToString();
                double calamount = invoiceamount + others1;
                //txttotalamount.Text = (sum + others1).ToString();
                double ttamt26 = (sum + others1);
                txttotalamount.Text = Math.Round(ttamt26, 2).ToString();
            }

        }



        // txtothers.Text = "0";
        //double others2 = Convert.ToDouble(txtothers.Text);
        double calamount1 = Convert.ToDouble(txttotalamount.Text);

        if (txtadjustamount.Text == "0")
        {
            txtroundoff.Text = "0";
            btnsave.Enabled = true;
            btnsave.Focus();

        }

        else if (calamount1 < invoiceamount)
        {
           // AddNewRowToGrid();
           // txtroundoff.Focus();

            double adjamt = Convert.ToDouble(txtadjustamount.Text);
            txtroundoff.Text = Convert.ToString(adjamt);

            double others10 = Convert.ToDouble(txtothers.Text);

            double rndoff1 = Convert.ToDouble(txtroundoff.Text);

           
                if (rndoff1 < 0)
                {
                    double adjamt26 = (invoiceamount - (sum + others10 - rndoff1));
                    txtadjustamount.Text = Convert.ToString(adjamt26);
                    txttotalamount.Text = Convert.ToString(sum + others10 - rndoff1);

                    if (adjamt26 == 0)
                    {
                        btnsave.Focus();
                    }

                }
                else
                {
                    double adjamt26 = (invoiceamount - (sum + others10 + rndoff1));
                    txtadjustamount.Text = Convert.ToString(adjamt26);
                    txttotalamount.Text = Convert.ToString(sum + others10 + rndoff1);

                    if (adjamt26 == 0)
                    {
                        btnsave.Focus();
                    }


                }

            
            
           // Master.ShowModal("Adjust the invoice Amount", "txtinvoiceamount", 1);
            //return;
        




        }
        else if (calamount1 <= invoiceamount)
        {
            btnsave.Enabled = true;
            btnsave.Focus();

        }
        else
        {
           // Master.ShowModal("Adjust the invoice Amount", "txtinvoiceamount", 1);
           // return;

             double adjamt = Convert.ToDouble(txtadjustamount.Text);
            txtroundoff.Text = Convert.ToString(adjamt);

            double others10 = Convert.ToDouble(txtothers.Text);

            double rndoff1 = Convert.ToDouble(txtroundoff.Text);

            if (rndoff1 < 1)
            {
                if (rndoff1 < 0)
                {
                    double adjamt26 = (invoiceamount - (sum + others10 + rndoff1));

                    txtadjustamount.Text = Convert.ToString(adjamt26);
                    txttotalamount.Text = Convert.ToString(sum + others10 + rndoff1);

                    if (adjamt26 == 0)
                    {
                        btnsave.Focus();
                    }

                }
                else
                {
                    double adjamt26 = (invoiceamount - (sum + others10 - rndoff1));

                    txtadjustamount.Text = Convert.ToString(adjamt26);
                    txttotalamount.Text = Convert.ToString(sum + others10 - rndoff1);

                    if (adjamt26 == 0)
                    {
                        btnsave.Focus();
                    }


                }

            }
            
           // Master.ShowModal("Adjust the invoice Amount", "txtinvoiceamount", 1);
            //return;
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
            string invoiceno = txtinvoiceno.Text;
            DataSet dscomp = clsgd.GetcondDataSet2("*", "tblProductinward", "Batchid", batchno1, "Invoiceno", invoiceno);
            //DataSet dspro = clsgd.GetcondDataSet("*", "tblProductinward", "Batchid", batchno1);
            if (dscomp.Tables[0].Rows.Count > 0)
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

    protected void btnMessagePopupTargetButton_Click(object sender, EventArgs e)
    {

        if (a == "txtbatchno")
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        (Gridview1.Rows[rowIndex].Cells[3].FindControl("txtbatchno") as TextBox).Focus();
                        rowIndex++;
                    }
                   
                }
            }

        }
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

                        (Gridview1.Rows[rowIndex].Cells[4].FindControl("txtexpiredate") as TextBox).Focus();
                        rowIndex++;
                    }

                }
            }

        }

        if (a == "txtstockarrival")
        {


            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        (Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Focus();
                        rowIndex++;
                    }

                }
            }

        }

        if (a == "txtfreesupply")
        {


            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        (Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply") as TextBox).Focus();
                        rowIndex++;
                    }

                }
            }

        }

        if (a == "ddltax")
        {

            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        (Gridview1.Rows[rowIndex].Cells[7].FindControl("ddltax") as DropDownList).Focus();
                        rowIndex++;
                    }

                }
            }
            

        }

        if (a == "txtpurchaseprice")
        {


            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        (Gridview1.Rows[rowIndex].Cells[8].FindControl("txtpurchaseprice") as TextBox).Focus();
                        rowIndex++;
                    }

                }
            }
            

        }

        if (a == "txtMRP")
        {


            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        (Gridview1.Rows[rowIndex].Cells[9].FindControl("txtMRP") as TextBox).Focus();
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

        if (a == "txtproductvalue")
        {


            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        (Gridview1.Rows[rowIndex].Cells[11].FindControl("txtproductvalue") as TextBox).Focus();
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
          }



          try
          {
              string invoiceno = txtinvoiceno.Text;
              string invdate = txtinvoicedate.Text;
              string invoiveamount = txtinvoiceamount.Text;

              for (int k = 0; k < Gridview1.Rows.Count; k++)
              {
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

                  if (invdate == "")
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


                  //decimal purchaseprice10 = Convert.ToDecimal(purchaseprice);
                  //decimal integral = Math.Truncate(purchaseprice10);
                  //decimal fractional = purchaseprice10 - integral;


                  string close_flag = "Y";
                  DataSet dschm = clsgd.GetcondDataSet9("*", "tblProductMaster", "ProductName", productname1);
                  string gcode = dschm.Tables[0].Rows[0]["g_code"].ToString();

                  DataSet dschm10 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", gcode, "p_flag", close_flag);


                  if (dschm10.Tables[0].Rows.Count > 0)
                  {

                      if (expdate1 <= answer)
                      {
                          // Master.ShowModal("Expire Date minimum 90days greater than current  date", "txtexpiredate", 1);
                          ShowPopupMessage("Expire Date minimum 90days greater than current  date", PopupMessageType.txtexpiredate);
                          (Gridview1.Rows[0].Cells[1].FindControl("txtexpiredate") as TextBox).Enabled = true;
                          (Gridview1.Rows[0].Cells[1].FindControl("txtexpiredate") as TextBox).Focus();
                          return;

                      }
                  }

                  string close_flag12 = "Y";
                  DataSet dschm20 = clsgd.GetcondDataSet9("*", "tblProductMaster", "ProductName", productname1);
                  string gcode20 = dschm20.Tables[0].Rows[0]["g_code"].ToString();

                  DataSet dschm25 = clsgd.GetcondDataSet2("*", "tblGroup", "g_code", gcode, "p_flag", close_flag12);


                  if (dschm25.Tables[0].Rows.Count > 0)
                  {

                      if (expdate1 <= answer)
                      {
                          // Master.ShowModal("Expire Date min 90 days greater than current date", "txtexpiredate", 0);
                          //return;
                          ShowPopupMessage("Expire Date min 90 days greater than current date", PopupMessageType.txtexpiredate);
                          return;

                      }
                  }

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

                  if (invoiveamount == "")
                  {
                      Master.ShowModal("Invoice Amount is Mandatory", "txtinvoiceamount", 0);
                      return;

                  }


                  //if (fractional == 0)
                  //{
                  //    string purchaseprice25 = (purchaseprice) + ".00";


                  //    ((Gridview1.Rows[k].Cells[1].FindControl("txtpurchaseprice") as TextBox).Text) = Convert.ToString(purchaseprice25);

                  //}


                 





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

                                  double ttax = Convert.ToDouble(balance * productprice * rateoftax / 100);
                                  string taxvalue12 = Math.Round(ttax, 2).ToString();


                                  ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txttaxamount") as TextBox).Text) = Convert.ToString(taxvalue12);

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
              double invoiceamount = Convert.ToDouble(txtinvoiceamount.Text);

              //double txtadjustamount=


              //double others = Convert.ToDouble(txtothers.Text);

              if (sum == invoiceamount)
              {
                  txtadjustamount.Text = "0";
                  txttotalamount.Text = Convert.ToString(sum);
                  txtothers.Enabled = false;
                  btnsave.Enabled = true;
                  btnsave.Focus();
                  return;

              }

              string others = txtothers.Text;
              string roundoff1 = txtroundoff.Text;

              if (others == "")
              {
                  if (roundoff1 != "")
                  {
                      txtnarrations.Visible = false;

                      txttotalamount.Text = (sum).ToString();
                      double roundoff = Convert.ToDouble(txtroundoff.Text);
                      //double others1 = Convert.ToDouble(txtothers.Text);

                      //double totalamount = invoiceamount + others1-;


                      double tmount10 = (sum - roundoff);
                      txttotalamount.Text = Math.Round(tmount10, 2).ToString();

                      double adjmnt20 = (invoiceamount - (sum + roundoff));
                      txtadjustamount.Text = Math.Round(adjmnt20, 2).ToString();

                  }

                  else
                  {

                      //double totalamount = invoiceamount + others1-;
                      //txttotalamount.Text = (sum).ToString();
                      //txtadjustamount.Text = (invoiceamount - (sum)).ToString();


                      double ttamt = sum;
                      txttotalamount.Text = Math.Round(ttamt, 2).ToString();

                      double adjamt10 = (invoiceamount - (sum));
                      txtadjustamount.Text = Math.Round(adjamt10, 2).ToString();

                  }
              }
              else
              {
                  txtnarrations.Visible = true;
                  //txtothers.Text = "0";
                  double others1 = Convert.ToDouble(txtothers.Text);
                  //txtroundoff.Text = "0";
                  //string roundoff1 = txtroundoff.Text;



                  if (roundoff1 != "")
                  {
                      double roundoff = Convert.ToDouble(txtroundoff.Text);
                      // txtadjustamount.Text = (invoiceamount - (sum + others1 + roundoff)).ToString();
                      //double totalamount = invoiceamount + others1-;
                      double adjamt26 = (invoiceamount - (sum + others1 + roundoff));
                      txtadjustamount.Text = Math.Round(adjamt26, 2).ToString();
                      //txttotalamount.Text = (sum + others1 - roundoff).ToString();
                      double ttamt26 = (sum + others1 - roundoff);
                      txttotalamount.Text = Math.Round(ttamt26, 2).ToString();

                  }
                  else
                  {
                      //txtadjustamount.Text = (invoiceamount - (sum + others1)).ToString();
                      double adjamt26 = (invoiceamount - (sum + others1));
                      txtadjustamount.Text = Math.Round(adjamt26, 2).ToString();
                      double calamount = invoiceamount + others1;
                      //txttotalamount.Text = (sum + others1).ToString();
                      double ttamt26 = (sum + others1);
                      txttotalamount.Text = Math.Round(ttamt26, 2).ToString();
                  }

              }



              // txtothers.Text = "0";
              //double others2 = Convert.ToDouble(txtothers.Text);
              double calamount1 = Convert.ToDouble(txttotalamount.Text);

              if (txtadjustamount.Text == "0")
              {
                  btnsave.Enabled = true;
              }

             // else if (calamount1 < invoiceamount)
              // {
              //   AddNewRowToGrid();
              //  (Gridview1.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Focus();
              // }
              else if (calamount1 <= invoiceamount)
              {
                  btnsave.Enabled = true;
                  btnsave.Focus();

              }
              else
              {
                  Master.ShowModal("Adjust the invoice Amount", "txtinvoiceamount", 1);
                  return;
              }

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

     protected void txtnarrations_TextChanged(object sender, EventArgs e)
     {
        // string invoiceamount1 = txtinvoiceamount.Text;

        //if (invoiceamount1 == "")
        //{
        //    Master.ShowModal("Enter invoice Amount", "txtinvoiceamount", 1);
        //    return;
        //}

        //if (ddpaymenttype.SelectedItem.Text == "Cash")
        //{
        //    chkpayment.Enabled = true;
        //    chkpayment.Focus();

        //}
        //else
        //{
        //    ddlsupplier.Enabled = true;
        //    ddlsupplier.Focus();
        //    ddlsupplier.BorderColor = System.Drawing.Color.Black;
        //    ddlsupplier.BorderWidth = 1;
        //    ddlsupplier.BorderStyle = BorderStyle.Dotted;
        //}

        //Double scv = 0.0;
        //Double sllv = 0.0;
        //Double productprice = 0.0;
        //Double rateoftax = 0.0;


        //int rowIndex = 0;
        //if (ViewState["CurrentTable"] != null)
        //{
        //    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        //    if (dtCurrentTable.Rows.Count > 0)
        //    {
        //        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
        //        {
        //            //extract the TextBox values

        //            //sllv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text);

        //            if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text))
        //            {
        //                sllv = 0.0;
        //            }
        //            else
        //            {

        //                sllv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtstockarrival") as TextBox).Text);
        //            }


        //            if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply") as TextBox).Text))
        //            {
        //                scv = 0.0;
        //            }
        //            else
        //            {

        //                scv = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[6].FindControl("txtfreesupply") as TextBox).Text);
        //            }
        //            Double balance = Convert.ToDouble(sllv - scv);
        //            if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtpurchaseprice") as TextBox).Text))
        //            {
        //                productprice = 0.0;
        //            }
        //            else
        //            {
        //                productprice = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[5].FindControl("txtpurchaseprice") as TextBox).Text);
        //            }
        //            if (String.IsNullOrEmpty((Gridview1.Rows[rowIndex].Cells[6].FindControl("txttax") as TextBox).Text))
        //            {
        //                rateoftax = 0.0;
        //            }
        //            else
        //            {
        //                rateoftax = Convert.ToDouble((Gridview1.Rows[rowIndex].Cells[6].FindControl("txttax") as TextBox).Text);
        //            }

        //            ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txttaxamount") as TextBox).Text) = Convert.ToString(balance * productprice * rateoftax / 100);

        //            double productcost = Convert.ToDouble(balance * productprice * rateoftax / 100);

        //            double cost = Convert.ToDouble(balance * productprice);

        //            ((Gridview1.Rows[rowIndex].Cells[10].FindControl("txtproductvalue") as TextBox).Text) = Convert.ToString(productcost + cost);


        //            rowIndex++;

        //        }

        //    }

        //}

        //Double sum = 0;


        //for (int j = 0; j < Gridview1.Rows.Count; j++)
        //{

        //    if (String.IsNullOrEmpty((Gridview1.Rows[j].Cells[1].FindControl("txtproductvalue") as TextBox).Text))
        //    {
        //        Double add = 0.0;

        //    }
        //    else
        //    {

        //        Double add = Convert.ToDouble((Gridview1.Rows[j].Cells[1].FindControl("txtproductvalue") as TextBox).Text);
        //        sum = sum + add;
        //    }


        //    //TimelineID = ((TextBox)gvTimeline.Rows[i].FindControl("txtTimeline")).Text.Trim();

        //}

        //// txttotalamount.Text = (sum).ToString();

        //double invoiceamount = Convert.ToDouble(txtinvoiceamount.Text);


        ////double others = Convert.ToDouble(txtothers.Text);

        //if (sum == invoiceamount)
        //{
        //    txtadjustamount.Text = "0";
        //    btnsave.Enabled = true;
        //    btnsave.Focus();
        //    return;

        //}



        //string others = txtothers.Text;
        //string roundoff1 = txtroundoff.Text;

        //if (others == "")
        //{
        //    if (roundoff1 != "")
        //    {
        //        txtnarrations.Visible = false;

        //        txttotalamount.Text = (sum).ToString();
        //        double roundoff = Convert.ToDouble(txtroundoff.Text);
        //        //double others1 = Convert.ToDouble(txtothers.Text);

        //        //double totalamount = invoiceamount + others1-;
        //        txttotalamount.Text = (sum - roundoff).ToString();
        //        txtadjustamount.Text = (invoiceamount - (sum + roundoff)).ToString();
        //    }

        //    else
        //    {

        //        //double totalamount = invoiceamount + others1-;
        //        txttotalamount.Text = (sum).ToString();
        //        txtadjustamount.Text = (invoiceamount - (sum)).ToString();
        //    }
        //}
        //else
        //{
        //    txtnarrations.Visible = true;
        //    //txtothers.Text = "0";
        //    double others1 = Convert.ToDouble(txtothers.Text);
        //    //txtroundoff.Text = "0";
        //    //string roundoff1 = txtroundoff.Text;



        //    if (roundoff1 != "")
        //    {
        //        double roundoff = Convert.ToDouble(txtroundoff.Text);
        //        txtadjustamount.Text = (invoiceamount - (sum + others1 + roundoff)).ToString();
        //        //double totalamount = invoiceamount + others1-;


        //        txttotalamount.Text = (sum + others1 - roundoff).ToString();

        //    }
        //    else
        //    {
        //        txtadjustamount.Text = (invoiceamount - (sum + others1)).ToString();
        //        double calamount = invoiceamount + others1;
        //        txttotalamount.Text = (sum + others1).ToString();
        //    }

        //}



        //// txtothers.Text = "0";
        ////double others2 = Convert.ToDouble(txtothers.Text);
        //double calamount1 = Convert.ToDouble(txttotalamount.Text);

        //if (txtadjustamount.Text == "0")
        //{
        //    btnsave.Enabled = true;
        //    btnsave.Focus();
        //    return;
        //}

         txtothers.Enabled = false;

         btnsave.Focus();
      


     }

     protected void txtroundoff_TextChanged(object sender, EventArgs e)
     {
          string invoiceamount1 = txtinvoiceamount.Text;

        if (invoiceamount1 == "")
        {
            Master.ShowModal("Enter invoice Amount", "txtinvoiceamount", 1);
            return;
        }

        if (ddpaymenttype.SelectedItem.Text == "Cash")
        {
            chkpayment.Enabled = true;
            chkpayment.Focus();

        }
        else
        {
            ddlsupplier.Enabled = true;
            ddlsupplier.Focus();
            ddlsupplier.BorderColor = System.Drawing.Color.Black;
            ddlsupplier.BorderWidth = 1;
            ddlsupplier.BorderStyle = BorderStyle.Dotted;
        }


        for (int jj = 0; jj < Gridview1.Rows.Count; jj++)
        {

            if (String.IsNullOrEmpty((Gridview1.Rows[jj].Cells[1].FindControl("txtMRP") as TextBox).Text))
            {
                ShowPopupMessage("Enter MRP", PopupMessageType.txtMRP);
                return;

            }
        }

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


                    rowIndex++;

                }

            }

        }

        Double sum = 0;


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


            //TimelineID = ((TextBox)gvTimeline.Rows[i].FindControl("txtTimeline")).Text.Trim();

        }

        // txttotalamount.Text = (sum).ToString();

        double invoiceamount = Convert.ToDouble(txtinvoiceamount.Text);


        //double others = Convert.ToDouble(txtothers.Text);

        if (sum == invoiceamount)
        {
            txtadjustamount.Text = "0";
            txtothers.Enabled = false;
            btnsave.Enabled = true;
            btnsave.Focus();
            return;

        }

      

        string others = txtothers.Text;
        string roundoff1 = txtroundoff.Text;

        if (others == "")
        {
            if (roundoff1 != "")
            {
                txtnarrations.Visible = false;

                txttotalamount.Text = (sum).ToString();
                double roundoff = Convert.ToDouble(txtroundoff.Text);
                //double others1 = Convert.ToDouble(txtothers.Text);

                double sum15 = Math.Round(sum, 2);

                //double totalamount = invoiceamount + others1-;
                txttotalamount.Text = Convert.ToString(sum15 + (roundoff));
                double tamt = Convert.ToDouble(txttotalamount.Text);

                double ainvamt = Convert.ToDouble(txtinvoiceamount.Text);
                if (roundoff > 0)
                {

                    double sum10 = Math.Round(sum, 2);
                    txtadjustamount.Text = Convert.ToString(invoiceamount - (sum10 + (roundoff)));


                 


                }
                else
                {
                    //double subtract = sum + (roundoff);
                    roundoff = roundoff * (-1);
                    sum = Math.Round(sum - roundoff);
                    txtadjustamount.Text = Convert.ToString(invoiceamount - sum);
                    //double value = invoiceamount - subtract;
                    //txtadjustamount.Text = Convert.ToString(value);
                }
               
            }

            else
            {

                //double totalamount = invoiceamount + others1-;
                txttotalamount.Text = (sum).ToString();
                txtadjustamount.Text = (invoiceamount - (sum)).ToString();
            }
        }
        else
        {
            txtnarrations.Visible = true;
            //txtothers.Text = "0";
            double others1 = Convert.ToDouble(txtothers.Text);
            //txtroundoff.Text = "0";
            //string roundoff1 = txtroundoff.Text;



            if (roundoff1 != "")
            {
                double roundoff = Convert.ToDouble(txtroundoff.Text);

                if (roundoff > 0)
                {
                    double sum1 = Math.Round(sum,2);
                    txtadjustamount.Text = Convert.ToString(invoiceamount - (sum1 + others1 + (roundoff)));
                }
                else
                {
                    double sum1 = Math.Round(sum, 2);
                    txtadjustamount.Text = Convert.ToString(invoiceamount - (sum1 + others1 + (roundoff)));
                }

                double adjust =Convert.ToDouble(txtadjustamount.Text);


                double sum2 = Math.Round(sum, 2);
                    //double totalamount = invoiceamount + others1-;
                    txttotalamount.Text = Convert.ToString(sum2 + others1 + (roundoff));
               

            }
            else
            {
                double sum2 = Math.Round(sum, 2);
                txtadjustamount.Text = (invoiceamount - (sum2 + others1)).ToString();
                double calamount = invoiceamount + others1;
                txttotalamount.Text = (sum + others1).ToString();
            }

        }



        // txtothers.Text = "0";
        //double others2 = Convert.ToDouble(txtothers.Text);
        double calamount1 = Convert.ToDouble(txttotalamount.Text);

        if (txtadjustamount.Text == "0")
        {
            txtothers.Enabled = false;
            btnsave.Enabled = true;
            btnsave.Focus();
            return;
        }
        else if (txttotalamount.Text == "0")
        {
            
           // txtdate.Enabled = true;
            //txtdate.Focus();
        }


        else if (calamount1 < invoiceamount)
        {
           // AddNewRowToGrid();
            //(Gridview1.Rows[rowIndex].Cells[1].FindControl("txtproductcode") as TextBox).Focus();
            Master.ShowModal("Roound off amount make it equal to invoice amount", "txtroundoff", 1);
            return;
        }
        else if (calamount1 <= invoiceamount)
        {
            btnsave.Enabled = true;
            btnsave.Focus();

        }
        else
        {
            Master.ShowModal("Adjust the invoice Amount", "txtinvoiceamount", 1);
            return;
        }


     }


   
}