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
//using System.Drawing;
using System.Runtime.InteropServices;
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


public partial class Supplier_accno : System.Web.UI.Page
{
    DataTable tblSupplieraccount = new DataTable();
    ClsBALSupplieraccount clsbal = new ClsBALSupplieraccount();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    ClsBALTransaction ClsBLGP3 = new ClsBALTransaction();
    PharmacyName Hosp = new PharmacyName();
    Dbconn dbcon = new Dbconn();
    protected static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
    string sMacAddress = "";
    double calc;
    double g;
    double m;
    DataRow drrw;
    double closing = 0;
    string sqlFormattedDate = DateTime.Now.ToString();


    string Tr_no;
    string Tr_no1;

    string invoiceno1;
    string invoiceno;
    double balance = 0;

    
   
  
    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Visible = false;
        lblsuccess.Visible = false;
        lblDR.Visible=false;
        lblCR.Visible=false;
        txtdate.Enabled = false;
        rdtrans.Items[0].Enabled = false;
        txtbal.Visible = false;
        txtamt.Attributes.Add("autocomplete", "off");
        txtvou.Attributes.Add("autocomplete", "off");
        //txtaccno.Attributes.Add("autocomplete", "off");
        txtbal.Attributes.Add("autocomplete", "off");

     

       
        if(!Page.IsPostBack)
        {
            txtsupcode.Enabled = true;
            txtsupcode.Focus();
            System.DateTime Dtnow = DateTime.Now;
            txtdate.Text = Dtnow.ToString("dd/MM/yyyy");
            txtdate1.Text = Dtnow.ToString("dd/MM/yyyy");
            rdtrans.SelectedIndex = 1;
            rdpay.SelectedIndex = 0;
            Button1.Visible = false;

            
        }

       
        GetMACAddress();

          if(rdpay.SelectedIndex==0)
          {
              ddlaccno.Visible = false;
               lblaccno1.Visible=false;
                lblchqno1.Visible=false;
            lblaccno.Visible = false;
            txtchqno.Visible = false;
            lblchqno.Visible = false;
            txtdate1.Visible = false;
            lbldate1.Visible = false;
          //  ImageButton1.Visible = false;

          }
          else if(rdpay.SelectedIndex==1)
          {
            //txtaccno.Visible = true;
            lblaccno1.Visible=true;
            lblaccno.Visible = true;
            lblchqno1.Visible=true;
            txtchqno.Visible = true;
            lblchqno.Visible = true;
            txtdate1.Visible = true;
            lbldate1.Visible = true;
           // ImageButton1.Visible = true;
          }
        //cal();
        

        ClientScript.RegisterStartupScript(this.GetType(), "SetInitialFocus", "<script>document.getElementById('" + txtsupcode.ClientID + "').focus();</script>");
        btnexit.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtsupcode.ClientID + "').focus();return false;}} else {return true}; ");

    }
     [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Suppliercode(string prefixText)
    {
        

            // string filename = Dbconn.Mymenthod();
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
                List<string> Supliercode = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Supliercode.Add(dt.Rows[i][0].ToString());
                }
                return Supliercode;
            }
            else
            {
                //string strconn1 = Dbconn.conmenthod();
                OleDbConnection conn = new OleDbConnection(strconn11);
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("select SupplierCode from tblsuppliermaster where SupplierCode like @1+'%'", conn);
                cmd.Parameters.AddWithValue("@1", prefixText);
                OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                List<string> Supliercode = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Supliercode.Add(dt.Rows[i][0].ToString());
                }

                return Supliercode;
            }
        
        
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Suppliername(string prefixText)
    {
        
        
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
                //string strconn1 = Dbconn.conmenthod();
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

    protected void Bindbankdetails()
    {
        //conenction path for database

        SqlConnection conn = new SqlConnection(strconn11);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Subhead from tblVoachermaster where Mainhead='BANK ACCOUNT' and Subhead !='BANK ACCOUNT'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ddlaccno.DataSource = ds;
            ddlaccno.DataTextField = "Subhead";
            ddlaccno.DataValueField = "Subhead";
            ddlaccno.DataBind();
            ddlaccno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
       
            conn.Close();
        
    }


     protected void txtsupcode_TextChanged(object sender, EventArgs e)
        {
        try
        {
            txtsupcode.BackColor = System.Drawing.Color.LightBlue;
            string supcode = txtsupcode.Text;
            //int supcod=Convert.ToInt32(supcode);
            DataSet ds1 = ClsBLGD.GetcondDataSet("*", "tblsuppliermaster", "SupplierCode", supcode);
          //  DataSet ds1 = ClsBLGD.GetcondDataSet("*", "tblsuppliermaster", "Suppliercode", supcode);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                string name = ds1.Tables[0].Rows[0]["SupplierName"].ToString();
                txtsupname.Text = name;
            }
            else
            {
                Master.ShowModal("Supplier code does not exist", "txtsupcode", 1);
                return;
            }
            rdtrans.Enabled = true;
            rdtrans.Focus();
          
            
            
            //lblerror.Text=string.Empty;

             SqlConnection con50 = new SqlConnection(strconn11);
             SqlCommand cmd50 = new SqlCommand("select sum(Totalvalues) as Totalvalues from tblSupplieraccount where SupplierCode='" + txtsupcode.Text + "' and Typeoftransaction='C'", con50);
             SqlDataAdapter da50 = new SqlDataAdapter(cmd50);
             DataSet ds50 = new DataSet();
                                     da50.Fill(ds50);

                                   if (ds50.Tables[0].Rows.Count > 0)
                                   {
                                       if (ds50.Tables[0].Rows[0].IsNull("Totalvalues"))
                                   {
                                     g = 0;
                                   }
                                   else
                                   {
                                       g = Convert.ToDouble(ds50.Tables[0].Rows[0]["Totalvalues"].ToString());
                                   }
                                  }
                                  // string bal = Convert.ToString(g);
                                  // txtbal.Text = bal;

              SqlConnection con = new SqlConnection(strconn11);
              SqlCommand cmd1 = new SqlCommand("select  sum(Totalvalues) as Totalvalues from tblSupplieraccount where SupplierCode='" + txtsupcode.Text + "'and Typeoftransaction='D'", con);
              SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
              DataSet ds10 = new DataSet();
                da1.Fill(ds10);
                if (ds50.Tables[0].Rows.Count > 0)
                {
                    if (ds10.Tables[0].Rows[0].IsNull("Totalvalues"))
                    {
                        m = 0;
                    }
                    else
                    {
                        m = Convert.ToDouble(ds10.Tables[0].Rows[0]["Totalvalues"].ToString());
                    }
                }


              double balamount = g - m;



              txtbal.BackColor = System.Drawing.Color.Red;
             txtbal.Text = Convert.ToString(balamount);

              decimal txt1 = Convert.ToDecimal(txtbal.Text);
              txtbal.Text = txt1.ToString("F");

              

              string balance = txtbal.Text;
              double balance1 = Convert.ToDouble(balance);

              if (balance1 <= 0)
              {
                  rdtrans.Enabled = false;
                  rdpay.Enabled = false;
                  txtamt.Enabled = false;
                  txtvou.Enabled = false;
                  ddlaccno.Enabled = false;
                  txtchqno.Enabled = false;
                  txtaddress.Enabled = false;
                  btnexit.Focus();
              }
              else
              {
                  txtamt.Focus();

              }

              DataSet ds20 = ClsBLGD.GetcondDataSet("*", "tblSupplieraccount", "SupplierCode", supcode);
              if (ds20.Tables[0].Rows.Count > 0)
              {


                  Button1.Visible = true;

              }
              else
              {
                  Button1.Visible = false;
              }

              string supcode1 = txtsupcode.Text;
              //int supcod=Convert.ToInt32(supcode);
              DataSet dss = ClsBLGD.GetcondDataSet("*", "tblsuppliermaster", "SupplierCode", supcode1);

              if (dss.Tables[0].Rows[0].IsNull("Credit_Limit"))
              {
                  decimal limit = 0;
                  Label2.Text = "0";
                  Label4.Text = "0";

              }
              else
              {
                  decimal limit = Convert.ToDecimal(dss.Tables[0].Rows[0]["Credit_Limit"].ToString());
                  string creditlimit = limit.ToString("F");
                  txtbal1.Text = creditlimit;






                  SqlConnection conn = new SqlConnection(strconn11);
                  conn.Open();
                  SqlCommand cmdd = new SqlCommand("Select sum(Totalvalues) as Totalvalues from tblSupplieraccount where SupplierCode = '" + supcode1 + "' and Typeoftransaction = 'C'", conn);
                  SqlCommand cmdd1 = new SqlCommand("Select sum(Totalvalues) as Totalvalues from tblSupplieraccount where SupplierCode = '" + supcode1 + "' and Typeoftransaction = 'D'", conn);
                  SqlDataAdapter daaa = new SqlDataAdapter(cmdd);
                  SqlDataAdapter daaa1 = new SqlDataAdapter(cmdd1);
                  DataSet dsss = new DataSet();
                  DataSet dsss1 = new DataSet();
                  daaa.Fill(dsss);
                  daaa1.Fill(dsss1);

                  decimal CD = 0;
                  decimal credit = 0;
                  decimal debit = 0;

                  if (dsss.Tables[0].Rows.Count > 0 || dsss1.Tables[0].Rows.Count > 0)
                  {
                      if (dsss.Tables[0].Rows[0].IsNull("Totalvalues"))
                      {
                          credit = 0;

                      }
                      else
                      {
                          credit = Convert.ToDecimal(dsss.Tables[0].Rows[0]["Totalvalues"].ToString());

                      }
                      if (dsss1.Tables[0].Rows[0].IsNull("Totalvalues"))
                      {
                          // debit = Convert.ToDecimal(dsss1.Tables[0].Rows[0]["Totalvalues"].ToString());
                          debit = 0;
                      }
                      else
                      {
                          debit = Convert.ToDecimal(dsss1.Tables[0].Rows[0]["Totalvalues"].ToString());
                          // debit = 0;
                      }

                      CD = credit - debit;

                  }
                  if (CD > 0)
                  {
                      string credeb = CD.ToString("F");
                      Label2.Text = credeb;
                  }
                  else
                  {
                      Label2.Text = "0.00";
                  }

                  decimal ball = limit - CD;
                  string balancee = ball.ToString("F");
                  Label4.Text = balancee;
              }
            
        }
           
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
        
    }
     protected void txtsupname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string supname = txtsupname.Text;
            DataSet ds1 = ClsBLGD.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", supname);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                string code = ds1.Tables[0].Rows[0]["SupplierCode"].ToString();
                txtsupcode.Text = code;
            }
            else
            {
                Master.ShowModal("Supplier name does not exist", "txtsupname", 1);
                return;
            }
            SqlConnection con50 = new SqlConnection(strconn11);
            SqlCommand cmd50 = new SqlCommand("select sum(Totalvalues) as Totalvalues from tblSupplieraccount where SupplierCode='" + txtsupcode.Text + "' and Paymenttype = 'CR' and Typeoftransaction='C'", con50);
            SqlDataAdapter da50 = new SqlDataAdapter(cmd50);
            DataSet ds50 = new DataSet();

            da50.Fill(ds50);

            if (ds50.Tables[0].Rows.Count > 0)
            {
                if (ds50.Tables[0].Rows[0].IsNull("Totalvalues"))
                {
                    g = 0;
                }
                else
                {
                    g = Convert.ToDouble(ds50.Tables[0].Rows[0]["Totalvalues"].ToString());
                }
            }




            SqlConnection con = new SqlConnection(strconn11);
            SqlCommand cmd1 = new SqlCommand("select  sum(Totalvalues) as Totalvalues from tblSupplieraccount where SupplierCode='" + txtsupcode.Text + "' and Typeoftransaction='D'", con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds10 = new DataSet();
            da1.Fill(ds10);
            if (ds50.Tables[0].Rows.Count > 0)
            {
                if (ds10.Tables[0].Rows[0].IsNull("Totalvalues"))
                {
                    m = 0;
                }
                else
                {
                    m = Convert.ToDouble(ds10.Tables[0].Rows[0]["Totalvalues"].ToString());
                }
            }


            double balamount = g - m;



           // txtbal.BackColor = System.Drawing.Color.Green;
            txtbal.Text = Convert.ToString(g);
            string balance = txtbal.Text;

            decimal txt1 = Convert.ToDecimal(txtbal.Text);
            txtbal.Text = txt1.ToString("F");

            rdtrans.Enabled = true;
            rdtrans.Focus();

            string supname1 = txtsupname.Text;
           // DataSet dss = ClsBLGD.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", supname1);
            DataSet dss = ClsBLGD.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", supname);

            if (dss.Tables[0].Rows[0].IsNull("Credit_Limit"))
            {
                decimal limit = 0;
                Label2.Text = "0";
                Label4.Text = "0";

            }
            else
            {


                decimal limit = Convert.ToDecimal(dss.Tables[0].Rows[0]["Credit_Limit"].ToString());
                string creditlimit = limit.ToString("F");
                txtbal1.Text = creditlimit;

                SqlConnection conn = new SqlConnection(strconn11);
                conn.Open();
                SqlCommand cmdd = new SqlCommand("Select sum(Totalvalues) as Totalvalues from tblSupplieraccount where SupplierCode = '" + txtsupcode.Text + "' and Typeoftransaction = 'C'", conn);
                SqlCommand cmdd1 = new SqlCommand("Select sum(Totalvalues) as Totalvalues from tblSupplieraccount where SupplierCode = '" + txtsupcode.Text + "' and Typeoftransaction = 'D'", conn);
                SqlDataAdapter daaa = new SqlDataAdapter(cmdd);
                SqlDataAdapter daaa1 = new SqlDataAdapter(cmdd1);
                DataSet dsss = new DataSet();
                DataSet dsss1 = new DataSet();
                daaa.Fill(dsss);
                daaa1.Fill(dsss1);

                decimal CD = 0;
                decimal credit = 0;
                decimal debit = 0;

                if (dsss.Tables[0].Rows.Count > 0 || dsss1.Tables[0].Rows.Count > 0)
                {
                    if (dsss.Tables[0].Rows[0].IsNull("Totalvalues"))
                    {
                        // credit = Convert.ToDecimal(dsss.Tables[0].Rows[0]["Totalvalues"].ToString());
                        credit = 0;

                    }
                    else
                    {
                        credit = Convert.ToDecimal(dsss.Tables[0].Rows[0]["Totalvalues"].ToString());
                        //credit = 0;
                    }
                    if (dsss1.Tables[0].Rows[0].IsNull("Totalvalues"))
                    {
                        //debit = Convert.ToDecimal(dsss1.Tables[0].Rows[0]["Totalvalues"].ToString());
                        debit = 0;

                    }
                    else
                    {
                        debit = Convert.ToDecimal(dsss1.Tables[0].Rows[0]["Totalvalues"].ToString());
                        // debit = 0;
                    }

                    CD = credit - debit;
                }
                if (CD > 0)
                {
                    string credeb = CD.ToString("F");
                    Label2.Text = credeb;
                }
                else
                {
                    Label2.Text = "0.00";
                }

                decimal ball = limit - CD;
                string balancee = ball.ToString("F");
                Label4.Text = balancee;
            }
            

            //txtamt.Focus();
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }

        DataSet ds20 = ClsBLGD.GetcondDataSet("*", "tblSupplieraccount", "SupplierCode", txtsupcode.Text);
        if (ds20.Tables[0].Rows.Count > 0)
        {


            Button1.Visible = true;

        }
        else
        {
            Button1.Visible = false;
        }
         rdtrans.Enabled = true;
         rdtrans.Focus();
    }
    public void cal()
    {
     if (!File.Exists(filename))
     {
        try
        {
            double g;
            double h;
            double a;
            double gh;
            //string dc=txtbal.Text;
            //int decre=Convert.ToInt32(dc);
            SqlConnection con = new SqlConnection(strconn11);
            SqlCommand cmd = new SqlCommand("select * from tblSupplieraccount where SupplierCode='" + txtsupcode.Text + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                SqlCommand cmd1 = new SqlCommand("select sum(Totalvalues) as Totalvalues from tblSupplieraccount where SupplierCode='" + txtsupcode.Text + "' and Paymenttype='CA' and Typeoftransaction='D'", con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                
                da1.Fill(ds1);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    if (ds1.Tables[0].Rows[0].IsNull("Totalvalues"))
                    {
                        g = 0;
                    }
                    else
                    {
                        g = Convert.ToDouble(ds1.Tables[0].Rows[0]["Totalvalues"].ToString());
                    }
                }
                else
                {
                    g = 0;
                }
               
                SqlCommand cmd11 = new SqlCommand("select sum(Totalvalues) as Totalvalues from tblSupplieraccount where SupplierCode='" + txtsupcode.Text + "' and Paymenttype='CR' and Typeoftransaction='D'", con);
                SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
                DataSet ds11 = new DataSet();
                da11.Fill(ds11);
                if (ds11.Tables[0].Rows.Count > 0)
                {
                    if (ds11.Tables[0].Rows[0].IsNull("Totalvalues"))
                    {
                        h = 0;
                    }
                    else
                    {
                        h = Convert.ToDouble(ds11.Tables[0].Rows[0]["Totalvalues"].ToString());
                    }
                }
                else
                {
                    h = 0;
                }
                gh = g + h;
                SqlCommand cmd112 = new SqlCommand("select sum(Totalvalues) as Totalvalues from tblSupplieraccount where SupplierCode='" + txtsupcode.Text + "' and Paymenttype='CR' and Typeoftransaction='C'", con);
                SqlDataAdapter da112 = new SqlDataAdapter(cmd112);
                DataSet ds112 = new DataSet();
                da112.Fill(ds112);
                if (ds112.Tables[0].Rows.Count > 0)
                {
                    if (ds112.Tables[0].Rows[0].IsNull("Totalvalues"))
                    {
                        a = 0;
                    }
                    else
                    {
                        a = Convert.ToDouble(ds112.Tables[0].Rows[0]["Totalvalues"].ToString());
                    }
                }
                else
                {
                    a = 0;
                }
                calc = gh - a;
                if (calc > 0)
                {
                   // txtbal.BackColor = System.Drawing.Color.Yellow;
                    txtbal.Text = Convert.ToString(calc);
                }
                else
                {
                    //txtbal.BackColor = System.Drawing.Color.Red;
                    txtbal.Text = Convert.ToString(calc);
                }
            }
             else
            {
                txtbal.Text=string.Empty;
                return;
            }
                string dc=txtbal.Text;
                int decre=Convert.ToInt32(dc);
                if(decre>0)
                {
                    //lblCR.Visible=true;
                }
                else if(decre<0)
                {
                   // lblDR.Visible=true;
                }
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
     }
     else
     {
          try
         {
           double g;
           double h;
           double a;
           double gh;
            //string dc=txtbal.Text;
            //  int decre=Convert.ToInt32(dc);
             OleDbConnection conn11=new OleDbConnection (strconn11);
             OleDbCommand cmd1=new OleDbCommand ("select * from tblSupplieraccount where SupplierCode='" + txtsupcode.Text + "'", conn11);
             OleDbDataAdapter da=new OleDbDataAdapter (cmd1);
             DataSet ds1 = new DataSet();
             da.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
               OleDbCommand cmd2=new OleDbCommand ("select sum(Totalvalues) as Totalvalues1 from tblSupplieraccount where SupplierCode='" + txtsupcode.Text + "' and Paymenttype='CA' and Typeoftransaction='D'", conn11);
               OleDbDataAdapter da1=new OleDbDataAdapter(cmd2);
               DataSet ds2=new DataSet();
               da1.Fill(ds2);

               if (ds2.Tables[0].Rows.Count > 0)
                {
                   if (ds2.Tables[0].Rows[0].IsNull("Totalvalues1"))
                    {
                        g = 0;
                    }
                    else
                    {
                        g = Convert.ToInt32(ds2.Tables[0].Rows[0]["Totalvalues1"].ToString());
                    }
                }
               else
                {
                    g = 0;
                }
               OleDbCommand cmd3=new OleDbCommand("select sum(Totalvalues) as Totalvalues1 from tblSupplieraccount where SupplierCode='" + txtsupcode.Text + "' and Paymenttype='CR' and Typeoftransaction='D'", conn11);
               OleDbDataAdapter da2=new OleDbDataAdapter(cmd3);
               DataSet ds3 = new DataSet();
               da2.Fill(ds3);
               if (ds3.Tables[0].Rows.Count > 0)
                {
                    if (ds3.Tables[0].Rows[0].IsNull("Totalvalues1"))
                    {
                        h = 0;
                    }
                    else
                    {
                        h = Convert.ToInt32(ds3.Tables[0].Rows[0]["Totalvalues1"].ToString());
                    }
                }
               else
                {
                    h = 0;
                }
                gh = g + h;
                OleDbCommand cmd4=new OleDbCommand("select sum(Totalvalues) as Totalvalues1 from tblSupplieraccount where SupplierCode='" + txtsupcode.Text + "' and Paymenttype='CR' and Typeoftransaction='C'", conn11);
                OleDbDataAdapter da3=new OleDbDataAdapter (cmd4);
                DataSet ds4 = new DataSet();
                da3.Fill(ds4);
                if (ds4.Tables[0].Rows.Count > 0)
                {
                    if (ds4.Tables[0].Rows[0].IsNull("Totalvalues1"))
                    {
                        a = 0;
                    }
                    else
                    {
                        a = Convert.ToInt32(ds4.Tables[0].Rows[0]["Totalvalues1"].ToString());
                    }
                }
                else
                {
                    a = 0;
                }
               calc = gh - a;
               if (calc > 0)
                {
                   // txtbal.BackColor = System.Drawing.Color.Yellow;
                    txtbal.Text = Convert.ToString(calc);
                }
                else
                {
                    //txtbal.BackColor = System.Drawing.Color.Red;
                    txtbal.Text = Convert.ToString(calc);
                }
            }
            else
            {
                txtbal.Text=string.Empty;
                return;
            }
            string dc=txtbal.Text;
            int decre=Convert.ToInt32(dc);
               if(decre > 0)
               {
                    //lblCR.Visible=true;
                }
                else if(decre <= 0)
                {
                    //lblDR.Visible=true;
                }
         }

        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
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
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            string Paymenttype;
            string Typeoftransaction;
            string invoiceno = "0";
            string invoicedate = "01/01/1990";
            if (rdpay.SelectedIndex == 0)
            {
                 Paymenttype = "CA";
            }
            else
            {
                 Paymenttype = "AD";
            }
            string Paymentflag = "Y";
            string SupplierCode = txtsupcode.Text;
            string Indate = txtdate.Text;
            if (rdtrans.SelectedIndex == 0)
            {
                 Typeoftransaction = "C";
            }
            else
            {
                 Typeoftransaction ="D";
            }
            
            string Vouchrno = txtvou.Text;
            string Totalvalues1 = txtamt.Text;
            decimal Totalvalues2 = Convert.ToDecimal(Totalvalues1);
            string Totalvalues = Totalvalues2.ToString("F");

           // string Bankaccno = ddlaccno.SelectedItem.Text;
            string Chequeno = txtchqno.Text;
            string Chequedate = txtdate1.Text;
            string Narration = txtaddress.Text;
           // string Tr_no = "0";
            string Login_name = Session["username"].ToString();
            System.DateTime Dtnow = DateTime.Now;
            string Sysdatetime = Dtnow.ToString("dd/MM/yyyy hh:mm:ss");
            string Mac_id = sMacAddress;
            
            
            if (rdpay.SelectedIndex == 0)
            {
               
                if (txtchqno.Text == "")
                {
                    Chequeno = "0";
                }
                if (txtdate1.Text == "")
                {
                    Chequedate = ("01/01/1990");
                }
                
                if (txtsupcode.Text == "")
                {
                    Master.ShowModal("Supplier Code mandatory", "txtdoorno", 0);
                    return;
                }

                if (txtsupname.Text == "")
                {
                    Master.ShowModal("Supplier Name mandatory", "txtdoorno", 0);
                    return;
                }
                if (rdtrans.Text == "")
                {
                    Master.ShowModal("Transation Type  mandatory", "txtdoorno", 0);
                    return;
                }
                if (rdpay.Text == "")
                {
                    Master.ShowModal("Payment Type  mandatory", "txtdoorno", 0);
                    return;
                }
                if (txtamt.Text == "")
                {
                    Master.ShowModal("Amount mandatory", "txtamt", 0);
                    return;
                }
                if (txtvou.Text == "")
                {
                    Master.ShowModal("Voucher No mandatory", "txtvou", 0);
                    return;
                }
               /* if (ddlaccno.Text == "--Select--" ||  ddlaccno.Text == "")
                {
                    Master.ShowModal("Select a bank account number", "ddlaccno", 0);
                    return;
                }*/
                Tr_no1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                Tr_no = Tr_no1 + "/" + "SUPACC";
                invoiceno1 = ClsBLGD.FetchMaximumInvoiceNo("Select_Max_Invoiceno");
                invoiceno = invoiceno1 + "/" + "SUPACC";

                if (!File.Exists(filename))
                {
                    string Bankaccno = "0";
                clsbal.Supplieraccno("INSERT_SUPPLIERACCOUNT",Tr_no, invoiceno, invoicedate, Paymenttype, Paymentflag, SupplierCode, Indate, Typeoftransaction, Vouchrno, Totalvalues, Bankaccno, Chequeno, Chequedate, Narration,Login_name,Sysdatetime,Mac_id);
                if (rdpay.SelectedIndex == 1)
                {

                    //Tr_no1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                    //Tr_no = Tr_no1 + "/" + "SUPACC";
                    System.DateTime Dtnow1 = DateTime.Now;
                    string sqlFormattedDate = Dtnow1.ToString("dd/MM/yyyy");
                    string supcode = txtsupcode.Text;
                    string proamt = txtamt.Text;
                    string Chequeno1 = txtchqno.Text;
                   // string Vouchrno1 = txtvou.Text;

                    string Vouchrno1 = ClsBLGD.base64Encode(txtvou.Text);

                    ClsBLGP3.Transaction("INSERT_TRANSACTION", Tr_no, txtdate.Text, "0000", supcode, "9993", "N", Chequeno1, Vouchrno1, "0000.00", proamt, "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                }
                else if (rdpay.SelectedIndex == 0)
                {
                    //Tr_no1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                    //Tr_no = Tr_no1 + "/" + "SUPACC";
                    System.DateTime Dtnow1 = DateTime.Now;
                    string sqlFormattedDate = Dtnow1.ToString("dd/MM/yyyy");
                    string supcode = txtsupcode.Text;
                    string proamt = txtamt.Text;
                    string Chequeno1 = txtchqno.Text;
                   // string Vouchrno1 = txtvou.Text;

                    string Vouchrno1 = ClsBLGD.base64Encode(txtvou.Text);

                    ClsBLGP3.Transaction("INSERT_TRANSACTION", Tr_no, txtdate.Text, "0000", supcode, "9993", "N", "0000", Vouchrno1, "0000.00", "0000.00", "0000.00", proamt, "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                }
                }
                else
                 {
                     string Bankaccno = "0";
                     OleDbConnection conn12=new OleDbConnection(strconn11);
                     conn12.Open();
                     OleDbCommand cmd5=new OleDbCommand("Insert into tblSupplieraccount(Tr_no,Invoiceno,Invoicedate,Paymenttype,Paymentflag,SupplierCode,Indate,Typeoftransaction,Vouchrno,Totalvalues,Bankaccno,Chequeno,Chequedate,Narration,Login_name,Sysdatetime,Mac_id)values('" + Tr_no + "','" + invoiceno + "','" + invoicedate + "','" + Paymenttype + "','" + Paymentflag + "','" + SupplierCode + "','" + Indate + "','" + Typeoftransaction + "','" + Vouchrno + "','" + Totalvalues + "','" + Bankaccno + "','" + Chequeno + "','" + Chequedate + "','" + Narration + "','" + Login_name + "','" + Sysdatetime + "','" + Mac_id +"')",conn12);
                     cmd5.ExecuteNonQuery();
                     conn12.Close();
                  }
            }
            if (rdpay.SelectedIndex == 1)
            {
                
                if (txtsupcode.Text == "")
                {
                    Master.ShowModal("Supplier Code mandatory", "txtdoorno", 0);
                    return;
                }

                if (txtsupname.Text == "")
                {
                    Master.ShowModal("Supplier Name mandatory", "txtdoorno", 0);
                    return;
                }
                if (rdtrans.Text == "")
                {
                    Master.ShowModal("Transation Type  mandatory", "txtdoorno", 0);
                    return;
                }
                if (rdpay.Text == "")
                {
                    Master.ShowModal("Payment Type  mandatory", "txtdoorno", 0);
                    return;
                }
                if (txtamt.Text == "")
                {
                    Master.ShowModal("Amount mandatory", "txtdoorno", 0);
                    return;
                }
                if (txtvou.Text == "")
                {
                    Master.ShowModal("Voucher No mandatory", "txtdoorno", 0);
                    return;
                }
                if (ddlaccno.SelectedItem.Text == "" || ddlaccno.SelectedItem.Text == "--Select--")
                {
                    Master.ShowModal("Account No mandatory", "txtdoorno", 0);
                    return;
                }
                if (txtchqno.Text == "")
                {
                    Master.ShowModal("Chquee No mandatory", "txtdoorno", 0);
                    return;
                }
               if (!File.Exists(filename))
               {
                   Tr_no1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                   Tr_no = Tr_no1 + "/" + "SUPACC";
                   string Bankaccno = ddlaccno.SelectedItem.Text;
                clsbal.Supplieraccno("INSERT_SUPPLIERACCOUNT", Tr_no,invoiceno, invoicedate, Paymenttype, Paymentflag, SupplierCode, Indate, Typeoftransaction, Vouchrno, Totalvalues, Bankaccno, Chequeno, Chequedate, Narration, Login_name, Sysdatetime, Mac_id);
                if (rdpay.SelectedIndex == 1)
                {

                    //Tr_no1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                    //Tr_no = Tr_no1 + "/" + "SUPACC";
                    System.DateTime Dtnow1 = DateTime.Now;
                    string sqlFormattedDate = Dtnow1.ToString("dd/MM/yyyy");
                    string supcode = txtsupcode.Text; 
                    string proamt = txtamt.Text;
                    string Chequeno1 = txtchqno.Text;
                    //string Vouchrno1 = txtvou.Text;

                    string Vouchrno1 = ClsBLGD.base64Encode(txtvou.Text);


                    ClsBLGP3.Transaction("INSERT_TRANSACTION", Tr_no, txtdate.Text, "0000", supcode, "9993", "N", Chequeno1, Vouchrno1, proamt, "0000.00", "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                    string subhead=ddlaccno.SelectedItem.Text;

                    string Bankaccountflag = "Y";

                    DataSet dsgroup1 = ClsBLGD.GetcondDataSet2("*", "tblVoachermaster", "Subhead", subhead, "Bankaccount", Bankaccountflag);

                    if (dsgroup1.Tables[0].Rows.Count > 0)
                    {

                        string Headercode = dsgroup1.Tables[0].Rows[0]["Headercode"].ToString();
                        ClsBLGP3.Transaction("INSERT_TRANSACTION", Tr_no, txtdate.Text, "0000", supcode, Headercode, "N", Chequeno1, Vouchrno1, "0000.00", proamt, "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                    }
                  

                }
                else if (rdpay.SelectedIndex == 0)
                {
                    //Tr_no1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                    //Tr_no = Tr_no1 + "/" + "SUPACC";
                    System.DateTime Dtnow1 = DateTime.Now;
                    string sqlFormattedDate = Dtnow1.ToString("dd/MM/yyyy");
                    string supcode = txtsupcode.Text;
                    string proamt = txtamt.Text;
                    string Chequeno1 = txtchqno.Text;
                    //string Vouchrno1 = txtvou.Text;

                    string Vouchrno1 = ClsBLGD.base64Encode(txtvou.Text);

                    ClsBLGP3.Transaction("INSERT_TRANSACTION", Tr_no, txtdate.Text, "0000", supcode, "9993", "N", "0000", Vouchrno1, "0000.00", "0000.00", "0000.00", proamt, "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                }
               }
               else
               {
                   string Bankaccno = "0";
                   OleDbConnection conn12=new OleDbConnection(strconn11);
                   conn12.Open();
                   OleDbCommand cmd5=new OleDbCommand("Insert into tblSupplieraccount(Tr_no,Invoiceno,Invoicedate,Paymenttype,Paymentflag,SupplierCode,Indate,Typeoftransaction,Vouchrno,Totalvalues,Bankaccno,Chequeno,Chequedate,Narration,Login_name,Sysdatetime,Mac_id)values('" + Tr_no + "','" + invoiceno + "','" + invoicedate + "','" + Paymenttype + "','" + Paymentflag + "','" + SupplierCode + "','" + Indate + "','" + Typeoftransaction + "','" + Vouchrno + "','" + Totalvalues + "','" + Bankaccno + "','" + Chequeno + "','" + Chequedate + "','" + Narration + "','" + Login_name + "','" + Sysdatetime + "','" + Mac_id +"')",conn12);
                   cmd5.ExecuteNonQuery();
                   conn12.Close();
               }
            }

          
            lblsuccess.Visible = true;
            lblsuccess.Text = "inserted successfully";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            //cal();
            txtsupcode.Text = string.Empty;
            txtsupname.Text = string.Empty;
            txtamt.Text = string.Empty;
            txtvou.Text = string.Empty;
            ddlaccno.ClearSelection();
            txtchqno.Text=string.Empty;
            //txtdate1.Text=string.Empty;
            txtaddress.Text=string.Empty;
            txtbal.Text = string.Empty;
            lblDR.Text = string.Empty;
            lblCR.Text = string.Empty;
            txtbal1.Text = string.Empty;
            Label2.Text = string.Empty;
            Label4.Text = string.Empty;

            ddlaccno.Visible=false;
            txtchqno.Visible=false;
            txtdate1.Visible=false;
            lblaccno.Visible = false;
            lblchqno.Visible = false;
            lbldate1.Visible = false;
            txtdate1.Text = Dtnow.ToString("dd/MM/yyyy");
            
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
    }
    protected void rdtrans_SelectedIndexChanged(object sender, EventArgs e)
    {
        rdpay.Enabled = true;
        rdpay.Focus();
    }
    protected void rdpay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdpay.SelectedIndex == 0)
        {
            ddlaccno.Visible = false;
            lblaccno.Visible = false;
            txtchqno.Visible = false;
            lblchqno.Visible = false;
            txtdate1.Visible = false;
            lbldate1.Visible = false;
           // ImageButton1.Visible = false;
        }
        if (rdpay.SelectedIndex == 1)
        {
            ddlaccno.Visible = true;
            lblaccno.Visible = true;
            txtchqno.Visible = true;
            lblchqno.Visible = true;
            txtdate1.Visible = true;
            lbldate1.Visible = true;
            //ImageButton1.Visible = true;
        }
        txtamt.Enabled = true;
        txtamt.Focus();
    }
    protected void txtdate1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime startdate = Convert.ToDateTime(txtdate1.Text);
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            Master.ShowModal("Invalid date format...", "txtdate1", 1);
            return;
        }
        txtaddress.Enabled = true;
        txtaddress.Focus();
    }
    protected void btnexit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void ddlaccno_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtchqno.Focus();
    }
    protected void txtvou_TextChanged(object sender, EventArgs e)
    {
        if (rdpay.SelectedIndex == 0)
        {
            txtaddress.Focus();
        }
        else
        {

            ddlaccno.Focus();
            Bindbankdetails();
        }
    }

    protected void txtamt_TextChanged(object sender, EventArgs e)
    {
        double balamt = 0;
        if (txtbal.Text != "")
        {
            balamt = Convert.ToDouble(txtbal.Text);
        }
       // double bal =Convert.ToDouble(txtbal.Text);
        double amt = Convert.ToDouble(txtamt.Text);
        if (balamt < amt)
        {
            Master.ShowModal("Enter amount lesser than balance amount ...", "txtamt", 1);
            txtamt.Text = string.Empty;
            txtamt.Focus();
            return;

        }
        txtvou.Focus();

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
    public void Bind()
    {
        // string filename = Dbconn.Mymenthod();

        try
        {
            //string bname = ddlbname.SelectedItem.Text;
            grcustomerdetails.DataSource = null;
            grcustomerdetails.DataBind();
            tblSupplieraccount.Rows.Clear();

            SqlConnection con = new SqlConnection(strconn11);
            SqlCommand cmd = new SqlCommand("select * from tblSupplieraccount where SupplierCode = '" + txtsupcode.Text + "' order by Indate ASC", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            int sum = 0;
            double Balance = 0;
            double value = 0;
           // var Balance = ""; 

            if (ds.Tables[0].Rows.Count > 0)
            {

                DataColumn col = new DataColumn("SLNO", typeof(int));
                col.AutoIncrement = true;
                col.AutoIncrementSeed = 1;
                col.AutoIncrementStep = 1;
                tblSupplieraccount.Columns.Add(col);
                // tblpurchasesale.Columns.Add("Productcode");

                tblSupplieraccount.Columns.Add("Indate");
                tblSupplieraccount.Columns.Add("Invoiceno");
                tblSupplieraccount.Columns.Add("Credit");
                tblSupplieraccount.Columns.Add("Debit");
                tblSupplieraccount.Columns.Add("Balance");
                // tblSupplieraccount.Columns.Add("Balance");


                Session["Supplier"] = tblSupplieraccount;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {


                    tblSupplieraccount = (DataTable)Session["Supplier"];
                    drrw = tblSupplieraccount.NewRow();
                    // SqlCommand cmd1 = new SqlCommand("select * from tblSupplieraccount where CA_code = '" + txtcustcode.Text + "'", con);
                    //SqlCommand cmd2 = new SqlCommand("select  Totalvalues  from tblSupplieraccount where Bal_type = 'D' and CA_code = '" + txtcustcode.Text + "'", con);
                    //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    //SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    // DataSet ds1 = new DataSet();
                    // DataSet ds2 = new DataSet();
                    // da1.Fill(ds1);
                    //da2.Fill(ds2);
                    string Paymenttype = ds.Tables[0].Rows[i]["Typeoftransaction"].ToString();
                    string credit6 = ds.Tables[0].Rows[i]["Totalvalues"].ToString();
                    decimal credit7 = Convert.ToDecimal(credit6);
                    string credit = credit7.ToString("F");
                    //int debit = Convert.ToInt32(ds2.Tables[0].Rows[i]["Totalvalues"].ToString());


                    //  drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                    DateTime indate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Indate"].ToString());
                    string date = indate.ToString("yyyy-MM-dd");
                    drrw["Indate"] = date;
                     drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                   
                   /* if (ds.Tables[0].Rows[i]["Invoiceno"].ToString() == "0")
                    {
                        drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["Vouchrno"].ToString() == "0")
                    {

                        drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                    }*/
                    ////////////////Bharat////////////
                    if (Paymenttype == "C")
                    {
                        drrw["Credit"] = credit;
                        drrw["Debit"] = "0";
                        double Ccredit = Convert.ToDouble(credit);
                        Balance = Balance + Ccredit;
                      //  Balance1 = Balance1 + Ccredit;
                       //  Balance = Balance1.ToString("F");
                       // Balance = Convert.ToDouble(Balance.ToString("F"));
                      //  Balance = Math.Truncate(Balance * 100) / 100;
                       // Balance = Math.Round(Balance, 2);
                        
                       // string bal = Convert.ToString(Balance);
                        //decimal Balance1 = Convert.ToDecimal(bal);

                       // Balance = Convert.ToDouble(Balance1.ToString("F"));
                        
                        //drrw["Balance"] = Balance;

                    }
                    else
                    {
                        drrw["Credit"] = "0";
                        drrw["Debit"] = credit;
                        double Ccredit = Convert.ToDouble(credit);
                        Balance = Balance - Ccredit;
                      //  Balance1 = Balance1 - Ccredit;
                      //   Balance = Balance1.ToString("F");
                        //Balance = Convert.ToDouble(Balance.ToString("F"));
                       // Balance = Math.Truncate(Balance * 100) / 100;
                       // Balance = Math.Round(Balance, 2);
                        
                      //  decimal Balance1 = Convert.ToDecimal(Balance);

                       // Balance = Convert.ToDouble(Balance1.ToString("F"));
                        //Balance = Convert.ToDouble(Balance.ToString("F"));
                        //drrw["Balance"] = Balance;
                    }
                    var bal = "";
                    if (Balance < 0)
                    {
                        value = Balance;
                        Balance = Balance * (-1);
                         bal = Balance.ToString("F");
                        drrw["Balance"] = bal + "" + "Dr.";
                        Balance = value;
                    }
                    else
                    {
                         bal = Balance.ToString("F");
                        drrw["Balance"] = bal + "" + "Cr.";
                    }


                    closing = Convert.ToDouble(bal);
                    

                    tblSupplieraccount.Rows.Add(drrw);
                    //Griddoctor.DataSource = tbldoctor;
                    //Griddoctor.DataBind();


                }




                DataView dws = tblSupplieraccount.DefaultView;
                dws.Sort = "SLNO ASC";
                grcustomerdetails.DataSource = tblSupplieraccount;
                grcustomerdetails.DataBind();

            }
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Enabled = true;
            lblerror.Text = asd;
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Bind();
        //arraylist  oALHospDetails = Hosp.HospitalReturns();
        SqlConnection con = new SqlConnection(strconn11);
        SqlCommand cmd10 = new SqlCommand("select * from tblsuppliermaster where SupplierCode = '" + txtsupcode.Text + "'", con);
        SqlDataAdapter da10 = new SqlDataAdapter(cmd10);
        DataSet ds10 = new DataSet();
        da10.Fill(ds10);

        ArrayList oALHospDetails = Hosp.HospitalReturns();

        string Suppliercode = ds10.Tables[0].Rows[0]["SupplierCode"].ToString();
        string Suppliername = ds10.Tables[0].Rows[0]["SupplierName"].ToString();
        string address1 = ds10.Tables[0].Rows[0]["add1"].ToString();
        // string address2 = ds10.Tables[0].Rows[0]["Address2"].ToString();
        string Hobli = ds10.Tables[0].Rows[0]["add2"].ToString();
        string Taluk = ds10.Tables[0].Rows[0]["add3"].ToString();
        // string District = ds10.Tables[0].Rows[0]["District"].ToString();
        //   string State = ds10.Tables[0].Rows[0]["State"].ToString();


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
        if (grcustomerdetails.HeaderRow != null)
        {
            for (int i = 0; i < grcustomerdetails.HeaderRow.Cells.Count; i++)
            {
                dtPdfcustomer.Columns.Add(grcustomerdetails.HeaderRow.Cells[i].Text);
            }
        }

        //  add each of the data rows to the table

        foreach (GridViewRow row in grcustomerdetails.Rows)
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
        PdfPTable table5 = null;
        PdfPTable table7 = null;

        PdfPTable tbldt = null;

        // PdfPTable tbldt = null;
        dtPdfcustomer = (DataTable)Session["dtPdfstock"];
        if (Session["dtPdfstock"] != null)
        {
            table2 = new PdfPTable(dtPdfcustomer.Columns.Count);
        }
        PdfPCell GridCell = null;
        BaseColor color = null;

        document.Open();

        tblstock = new PdfPTable(1);
        tblstock.TotalWidth = 490f;
        tblstock.LockedWidth = true;
        tblstock.SetWidths(new float[] { 1f });

        table1 = new PdfPTable(6);
        table1.TotalWidth = 490f;
        table1.LockedWidth = true;
        table1.SetWidths(new float[] { 0.3f, 1f, 1f, 1f, 1f, 1f });

        tbldt = new PdfPTable(2);
        tbldt.TotalWidth = 500f;
        tbldt.LockedWidth = true;
        tbldt.SetWidths(new float[] { 1.4f, 1.4f });

        table2 = new PdfPTable(1);
        table2.TotalWidth = 490f;
        table2.LockedWidth = true;
        table2.SetWidths(new float[] { 1.4f });

        table3 = new PdfPTable(4);
        table3.TotalWidth = 490f;
        table3.LockedWidth = true;
        table3.SetWidths(new float[] { 1.4f, 1.4f, 1.4f, 1.4f });

        table4 = new PdfPTable(2);
        table4.TotalWidth = 490f;
        table4.LockedWidth = true;
        table4.SetWidths(new float[] { 1.4f, 1.4f });

        table5 = new PdfPTable(1);
        table5.TotalWidth = 490f;
        table5.LockedWidth = true;
        table5.SetWidths(new float[] { 1.4f });

        table7 = new PdfPTable(1);
        table7.TotalWidth = 490f;
        // table2.HorizontalAlignment = Element.ALIGN_LEFT;
        table7.LockedWidth = true;
        table7.SetWidths(new float[] { 1f });


        tblstock.AddCell(PhraseCell(new Phrase("Supplier Report\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
        cell.Colspan = 2;
        cell.PaddingBottom = 30f;
        tblstock.AddCell(cell);


        tbldt.AddCell(PhraseCell(new Phrase("Supplier Name :" + Suppliername, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
        tbldt.AddCell(PhraseCell(new Phrase("Supplier Code:" + Suppliercode, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
        cell.Colspan = 2;
        cell.PaddingBottom = 30f;
        tbldt.AddCell(cell);
        tbldt.SpacingAfter = 15f;

        tbldt.AddCell(PhraseCell(new Phrase(" Address:" + address1 + "," + Hobli + "\n" + Taluk + ",", FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
        // tbldt.AddCell(PhraseCell(new Phrase("Patient Name :" + pname, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
        cell.Colspan = 2;
        cell.PaddingBottom = 30f;
        tbldt.AddCell(cell);
        tbldt.SpacingAfter = 15f;


        table2.AddCell(PhraseCell(new Phrase("Statement Summary :-\n", FontFactory.GetFont("Times", 12, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
        cell.Colspan = 2;
        cell.PaddingBottom = 30f;
        table2.AddCell(cell);

        SqlConnection con1 = new SqlConnection(strconn11);
        SqlCommand cmd11 = new SqlCommand("select  MAX(Totalvalues) as Totalvalues from tblSupplieraccount where SupplierCode = '" + txtsupcode.Text + "'", con1);
        SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
        DataSet ds11 = new DataSet();
        da11.Fill(ds11);
        string openbal = ds11.Tables[0].Rows[0]["Totalvalues"].ToString();

        SqlConnection con2 = new SqlConnection(strconn11);
        SqlCommand cmd12 = new SqlCommand("select  SUM(Totalvalues) as Totalvalues from tblSupplieraccount where SupplierCode = '" + txtsupcode.Text + "' and Typeoftransaction = 'C'", con2);
        SqlCommand cmd13 = new SqlCommand("select  SUM(Totalvalues) as Totalvalues from tblSupplieraccount where SupplierCode = '" + txtsupcode.Text + "'and Typeoftransaction = 'D'", con2);
        SqlDataAdapter da12 = new SqlDataAdapter(cmd12);
        SqlDataAdapter da13 = new SqlDataAdapter(cmd13);
        DataSet ds12 = new DataSet();
        DataSet ds13 = new DataSet();
        da12.Fill(ds12);
        da13.Fill(ds13);
        /////////////////Bharat/////////

        string Debit = "";
        string Credit = "";
        string Credit10 = ds12.Tables[0].Rows[0]["Totalvalues"].ToString();
        string Debit10 = ds13.Tables[0].Rows[0]["Totalvalues"].ToString();

        if (Credit10 != "")
        {
            
            decimal credit20 = Convert.ToDecimal(Credit10);
             Credit = credit20.ToString("F");
        }
        //string Credit10 = ds12.Tables[0].Rows[0]["Totalvalues"].ToString();
       // double Credit = Convert.ToDouble(Credit10.ToString("F"));
        if (Debit10 != "")
        {
            
            decimal debit20 = Convert.ToDecimal(Debit10);
            Debit = debit20.ToString("F");
        }
       



        //table3.AddCell(PhraseCell(new Phrase("Opening Balance:" +"Rs."+ openbal  , FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
        table3.AddCell(PhraseCell(new Phrase("Credits:" + "Rs." + Credit, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
        if (Debit == "")
        {
            table3.AddCell(PhraseCell(new Phrase("Debits:" + "Rs." + 0, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
        }
        else
        {
            table3.AddCell(PhraseCell(new Phrase("Debits:" + "Rs." + Debit, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
        }
        if (closing < 0)
        {
            closing = closing * (-1);
            string closing1 = closing.ToString("F");
            table3.AddCell(PhraseCell(new Phrase("Closing Balance:" + "Rs." + closing1 + "Dr", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
            cell.Colspan = 3;
            cell.PaddingBottom = 30f;
            table3.AddCell(cell);
        }
        else
        {
            string closing1 = closing.ToString("F");
            table3.AddCell(PhraseCell(new Phrase("Closing Balance:" + "Rs." + closing1 + "Cr", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
            cell.Colspan = 3;
            cell.PaddingBottom = 30f;
            table3.AddCell(cell);
        }

        DateTime dtstrDate2 = DateTime.Now;

        // string creditflag = "C";

        DataSet dslogin = ClsBLGD.GetcondDataSet("*", "tblLogin", "username", Session["username"].ToString());
        //DataSet dsmax = ClsBLGD.GetcondDataSet2("", "tblCustomeraccount", "Bal_type", creditflag, "CA_code", txtcustcode.Text);

        table4.AddCell(PhraseCell(new Phrase("\n\n\n\n" + "Generated On:" + sqlFormattedDate, FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
        table4.AddCell(PhraseCell(new Phrase("\n\n\n\n" + "Generated By:" + "(" + dslogin.Tables[0].Rows[0]["UserName"].ToString() + ")", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
        cell.Colspan = 4;
        cell.PaddingBottom = 30f;
        table4.AddCell(cell);

        table5.AddCell(PhraseCell(new Phrase("\n\n\n\n" + "This is a computer generated statement and does not require signature", FontFactory.GetFont("Times", 7, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_RIGHT);
        cell.Colspan = 2;
        cell.PaddingBottom = 30f;
        table5.AddCell(cell);



        GridCell = new PdfPCell(new Phrase(new Chunk("SlNo.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //GridCell.BorderColor = BaseColor.WHITE;
        table1.AddCell(GridCell);

        // GridCell = new PdfPCell(new Phrase(new Chunk("Productcode", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        // GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Date", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        // GridCell.BorderColor = BaseColor.WHITE;
        table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Invoice No.", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        // GridCell.BorderColor = BaseColor.WHITE;
        table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Credit(Rs)", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        table1.AddCell(GridCell);
        GridCell.BorderColor = BaseColor.WHITE;


        GridCell = new PdfPCell(new Phrase(new Chunk("Debit(Rs)", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        // GridCell.BorderColor = BaseColor.WHITE;
        table1.AddCell(GridCell);

        GridCell = new PdfPCell(new Phrase(new Chunk("Balance Amt(Rs)", FontFactory.GetFont("Times", 10, Font.BOLD, BaseColor.BLACK))));
        GridCell.HorizontalAlignment = Element.ALIGN_CENTER;
        // GridCell.BorderColor = BaseColor.WHITE;
        table1.AddCell(GridCell);
        table1.SpacingAfter = 15f;

        if (dtPdfcustomer != null)
        {
            for (int i = 0; i < dtPdfcustomer.Rows.Count; i++)
            {


                for (int row1 = 0; row1 < dtPdfcustomer.Columns.Count; row1++)
                {

                    GridCell = new PdfPCell(new Phrase(new Chunk(dtPdfcustomer.Rows[i][row1].ToString(), FontFactory.GetFont("Times", 10, Font.NORMAL, BaseColor.BLACK))));
                    GridCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    // GridCell.BorderColor = BaseColor.WHITE;
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
        table7.AddCell(cell);




        document.Add(table7);
        document.Add(tblstock);
        document.Add(tbldt);
        document.Add(table1);
        document.Add(table2);
        document.Add(table3);
        document.Add(table4);
        document.Add(table5);
        document.Close();

        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", "attachment; filename=SupplierReport.pdf");


        byte[] bytes = memorystream.ToArray();
        memorystream.Close();
        Response.Clear();

        Response.Buffer = true;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.BinaryWrite(bytes);
        Response.End();
        Response.Close();

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Supplier_accno.aspx");
    }
}
