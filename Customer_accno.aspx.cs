using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls.WebParts;
using System.Web.Services;
using System.Net.NetworkInformation;
using System.Management;
using System.Runtime.InteropServices;
using AlertMessageName;
using System.Text;
//  using System.Collections.Specialized;
// using System.Drawing;
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


public partial class Customer_accno : System.Web.UI.Page
{
    DataTable tblCustomeraccount = new DataTable();
    DataRow dr2;
    ClsBALCustomeraccount ClsBLGP=new ClsBALCustomeraccount ();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    ClsBALTransaction ClsBLGP3 = new ClsBALTransaction();
    PharmacyName Hosp = new PharmacyName();
    Dbconn dbcon = new Dbconn();
    protected static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
    string sMacAddress = "";
    double calc;
    double g, h,g10,a;
    double m,p;
    protected static string g1;
    protected static string balance1;
    
    string transno;
    string transno1;

    string TIN_No;

    string PAN_No;
    string City;
    string Hobli;

    string invoiceno1;
    string invoiceno;
    DataRow drrw;
    string sqlFormattedDate = DateTime.Now.ToString();
    double closing = 0;

    ArrayList arryno = new ArrayList();

    ArrayList arryname = new ArrayList();

    ArrayList arryno1 = new ArrayList();

    ArrayList arryname1 = new ArrayList();
   

    protected void Page_Load(object sender, EventArgs e)
    {
        txtdate.Enabled = false;
        lblerror.Visible = false;
        lblsuccess.Visible = false;
        
        report.Visible = false;
        txtamt.Attributes.Add("autocomplete", "off");
        txtvou.Attributes.Add("autocomplete", "off");
        txtreference.Attributes.Add("autocomplete", "off");
        
        if (!Page.IsPostBack)
        {
            //txtsupcode.Enabled = true;
            //txtsupcode.Focus();
            System.DateTime Dtnow = DateTime.Now;
            txtdate.Text = Dtnow.ToString("dd/MM/yyyy");
            txtdate1.Text = Dtnow.ToString("dd/MM/yyyy");
            rdtrans.SelectedIndex = 0;
            rdpay.SelectedIndex = 0;
            lblreference.Visible=false;
            txtreference.Visible=false;
            lblbankaccount.Visible = false;
            dddepbankacc.Visible = false;
            txtaccno.Visible = false;
            lbldate1.Visible = false;
            txtdate1.Visible = false;
            lblchqno.Visible = false;
            txtchqno.Visible = false;
            lblaccno1.Visible = false;
           // lblCB.Visible = false;
           // lblAB.Visible = false;
            lblaccno.Visible = false;
            ddlaccno.Visible = false;
            bindaccno();
            deposit();
            PanelInvc.Visible = false;
        }
        if ((rdtrans.SelectedIndex == 0) && (rdpay.SelectedIndex == 0))
        {
            lblamt.Visible = true;
            txtamt.Visible = true;
            lblvou.Visible = true;
            txtvou.Visible = true;
            lblnarr.Visible = true;
            txtaddress.Visible = true;
            lblaccno.Visible = false;
            ddlaccno.Visible = false;
            txtaccno.Visible = false;
            lblreference.Visible = false;
            txtreference.Visible = false;
            lblchqno.Visible = false;
            txtchqno.Visible = false;
            lblbankaccount.Visible = false;
            dddepbankacc.Visible = false;
            lbldate1.Visible = false;
            txtdate1.Visible = false;
            lblaccno1.Visible = false;
        }
        if ((rdtrans.SelectedIndex == 0) && (rdpay.SelectedIndex == 1))
        {
            lblamt.Visible = true;
            txtamt.Visible = true;
            lblvou.Visible = true;
            txtvou.Visible = true;
            lblreference.Visible = false;
            txtreference.Visible = false;
            lblaccno.Visible = false;
            lblaccno1.Visible = true;
            txtaccno.Visible = true;
            lbldate1.Visible = true;
            txtdate1.Visible = true;
            lblnarr.Visible = true;
            txtaddress.Visible = true;
            lblbankaccount.Visible = true;
            dddepbankacc.Visible = true;
            ddlaccno.Visible = false;
            lblchqno.Visible = true;
            txtchqno.Visible = true;
        }
        if ((rdtrans.SelectedIndex == 1) && (rdpay.SelectedIndex == 0))
        {
            lblamt.Visible = true;
            txtamt.Visible = true;
            lblvou.Visible = false;
            txtvou.Visible = false;
            lblreference.Visible = true;
            txtreference.Visible = true;
            lblaccno.Visible = false;
            txtaccno.Visible = false;
            lbldate1.Visible = false;
            txtdate1.Visible = false;
            lblnarr.Visible = true;
            txtaddress.Visible = true;
            ddlaccno.Visible = false;
            lblchqno.Visible = false;
            txtchqno.Visible = false;
            lblaccno1.Visible = false;
            lblbankaccount.Visible = false;
            dddepbankacc.Visible = false;
        }
        if ((rdtrans.SelectedIndex == 1) && (rdpay.SelectedIndex == 1))
        {
            lblamt.Visible = true;
            txtamt.Visible = true;
            lblvou.Visible = false;
            txtvou.Visible = false;
            lblreference.Visible = true;
            txtreference.Visible = true;
            lblaccno.Visible = true;
            txtaccno.Visible = false;
            lbldate1.Visible = true;
            txtdate1.Visible = true;
            lblnarr.Visible = true;
            txtaddress.Visible = true;
            ddlaccno.Visible = true;
            lblchqno.Visible = true;
            txtchqno.Visible = true;
            lblaccno1.Visible = false;
            lblbankaccount.Visible = false;
            dddepbankacc.Visible = false;
        }
        var ctrlName = Request.Params[Page.postEventSourceID];
        var args = Request.Params[Page.postEventArgumentID];
        if (txtdate1.Text != "" || !IsPostBack)
            HandleCustomPostbackEvent(ctrlName, args);
        if (txtamt.Text == "" || !IsPostBack)
            HandleCustomPostbackEvent(ctrlName, args);
        GetMACAddress();
        //cal();
        
         

       

        ClientScript.RegisterStartupScript(this.GetType(), "SetInitialFocus", "<script>document.getElementById('" + txtcustcode.ClientID + "').focus();</script>");
        btnexit.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtcustcode.ClientID + "').focus();return false;}} else {return true}; ");

    }
    private void HandleCustomPostbackEvent(string ctrlName, string args)
    {
        if (ctrlName == txtdate1.UniqueID && args == "OnBlur")
        {
            if (dddepbankacc.Visible == true)
            {
                dddepbankacc.Focus();
            }
        }
        if (ctrlName == txtamt.UniqueID && args == "OnBlur")
        {
            if (txtamt.Text != "")
            {
                {
                    //Master.ShowModal("Please enter amount", "txtamt", 1);
                    //txtamt.Focus();
                    txtvou.Focus();
                    return;
                }
            }
        }
    }

    public void bindaccno()
    {
        string bankname="BANK ACCOUNT";
        DataSet ds = ClsBLGD.GetcondDataSet("Subhead", "tblVoachermaster", "Mainhead", bankname);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            arryname.Add(ds.Tables[0].Rows[i]["Subhead"].ToString());
        }

        arryname.Sort();
        arryno.Add("-Select-");
        for (int i = 0; i < arryname.Count; i++)
        {
            arryno.Add(arryname[i].ToString());
        }
        ddlaccno.DataSource = arryno;
        ddlaccno.DataBind();

    }
    protected void txtamt_TextChanged(object sender, EventArgs e)
    {
        double balamt = 0;
        double amtamount = 0;
        if (txtbal.Text != "")
        {
            balamt = Convert.ToDouble(txtbal.Text);
        }
        if (txtamt.Text != "")
        {
            amtamount = Convert.ToDouble(txtamt.Text);
        }
        /* if (balamt > 0 && amtamount > 0)
         {
             Master.ShowModal("Debit not possible...", "txtdate1", 1);
             return;
         }
         else
         {

         }*/
       
        if (rdtrans.SelectedIndex == 0 && rdpay.SelectedIndex == 0)
        {
            txtvou.Focus();
        }

        if (rdtrans.SelectedIndex == 0 && rdpay.SelectedIndex == 1)
        {
            

            txtvou.Focus();
        }

        if (rdtrans.SelectedIndex == 1 && rdpay.SelectedIndex == 0)
        {
            if (balamt == 0)
            {
                return;
            }

            if (balamt < amtamount)
            {
                Master.ShowModal("Debit not possible...", "txtamt", 1);
                return;

            }

            txtreference.Focus();
        }
        if (rdtrans.SelectedIndex == 1 && rdpay.SelectedIndex == 1)
        {
            if (balamt == 0)
            {
                return;
            }

            if (balamt < amtamount)
            {
                Master.ShowModal("Debit not possible...", "txtamt", 1);
                return;

            }


            txtreference.Focus();
        }

    }

    protected void Page_Init(object sender, EventArgs e)
    {
        var onBlurScript = Page.ClientScript.GetPostBackEventReference(txtdate1, "OnBlur");
        txtdate1.Attributes.Add("OnBlur", onBlurScript);
        var onBlurScript1 = Page.ClientScript.GetPostBackEventReference(txtdate1, "OnBlur");
        txtdate1.Attributes.Add("OnBlur", onBlurScript1);
        var onBlurScript2 = Page.ClientScript.GetPostBackEventReference(txtamt, "OnBlur");
        txtamt.Attributes.Add("OnBlur", onBlurScript2);
    }


    public void deposit()
    {
        //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
     
        /*using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlCommand cmd = new SqlCommand("SELECT Subhead FROM tblVoachermaster where Bankaccount='Y'"))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                dddepbankacc.DataSource = cmd.ExecuteReader();
                dddepbankacc.DataTextField = "Subhead";
                dddepbankacc.DataValueField = "Subhead";
                dddepbankacc.DataBind();
               // dddepbankacc.Items.Insert(0, new ListItem("--Select--", "0"));
                dddepbankacc.Items.Add("--Select--");
                con.Close();
            }
        }*/

        string bankname = "Y";
        DataSet ds = ClsBLGD.GetcondDataSet("Subhead", "tblVoachermaster", "Bankaccount", bankname);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            arryname1.Add(ds.Tables[0].Rows[i]["Subhead"].ToString());
            string namw1 = ds.Tables[0].Rows[i]["Subhead"].ToString();
        }

        arryname1.Sort();
        arryno1.Add("-Select-");
        for (int i = 0; i < arryname1.Count; i++)
        {
            arryno1.Add(arryname1[i].ToString());
        }
        dddepbankacc.DataSource = arryno1;
        dddepbankacc.DataBind();

       // ddGecode.Focus();

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

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Customercode(string prefixText)
    {
        

            string filename = Dbconn.Mymenthod();
            if (!File.Exists(filename))
            {
                //string oConn = ConfigurationManager.AppSettings["ConnectionString"];
                SqlConnection conn = new SqlConnection(strconn11);
                conn.Open();
                SqlCommand cmd = new SqlCommand("select CA_code from tblCustomer where CA_code like @1+'%'", conn);
                cmd.Parameters.AddWithValue("@1", prefixText);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<string> Customercode = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Customercode.Add(dt.Rows[i][0].ToString());
                }
                return Customercode;
            }
            else
            {
                string strconn11 = Dbconn.conmenthod();
                OleDbConnection conn = new OleDbConnection(strconn11);
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("select CA_code from tblCustomer where CA_code like @1+'%'", conn);
                cmd.Parameters.AddWithValue("@1", prefixText);
                OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                List<string> Customercode = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Customercode.Add(dt.Rows[i][0].ToString());
                }

                return Customercode;
            }
        
        
    }

      [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Customername(string prefixText)
    {
        

             string filename = Dbconn.Mymenthod();
            if (!File.Exists(filename))
            {
                //string oConn = ConfigurationManager.AppSettings["ConnectionString"];
                SqlConnection conn = new SqlConnection(strconn11);
                conn.Open();
                SqlCommand cmd = new SqlCommand("select CA_name from tblCustomer where CA_name like @1+'%'", conn);
                cmd.Parameters.AddWithValue("@1", prefixText);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<string> Customercode = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Customercode.Add(dt.Rows[i][0].ToString());
                }
                return Customercode;
            }
            else
            {
                //string strconn1 = Dbconn.conmenthod();
                OleDbConnection conn = new OleDbConnection(strconn11);
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("select CA_name from tblCustomer where CA_name like @1+'%'", conn);
                cmd.Parameters.AddWithValue("@1", prefixText);
                OleDbDataAdapter oda = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                List<string> Customercode = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Customercode.Add(dt.Rows[i][0].ToString());
                }

                return Customercode;
            }
        
        
    }
      protected void txtcustcode_TextChanged(object sender, EventArgs e)
      {
           try
          {
              //txtcustcode.BackColor = Color.LightBlue; 
              string cuscd=txtcustcode.Text;
              DataSet ds1=ClsBLGD.GetcondDataSet("*","tblCustomer","CA_code",cuscd);
              if (ds1.Tables[0].Rows.Count > 0)
              {
                  string cusnm = ds1.Tables[0].Rows[0]["CA_name"].ToString();
                  txtcustnm.Text = cusnm;

                  report.Visible = true;


                  SqlConnection con40 = new SqlConnection(strconn11);
                  SqlCommand cmd40 = new SqlCommand("select Mobileno as Mobileno,Email as Email,City as City,Hobli as Hobli  from tblCustomer where CA_code='" + txtcustcode.Text + "'", con40);
                  SqlDataAdapter da40 = new SqlDataAdapter(cmd40);
                  DataSet ds40 = new DataSet();

                  da40.Fill(ds40);

                  if (ds40.Tables[0].Rows.Count > 0)
                  {
                      if (ds40.Tables[0].Rows[0].IsNull("Mobileno"))
                      {
                          PAN_No = "0";
                      }
                      else
                      {
                          PAN_No = Convert.ToString(ds40.Tables[0].Rows[0]["Mobileno"].ToString());
                      }


                      if (ds40.Tables[0].Rows[0].IsNull("Email"))
                      {
                          TIN_No = "0";
                      }
                      else
                      {
                          TIN_No = Convert.ToString(ds40.Tables[0].Rows[0]["Email"].ToString());
                      }

                      if (ds40.Tables[0].Rows[0].IsNull("City"))
                      {
                          City = "0";
                      }
                      else
                      {
                          City = Convert.ToString(ds40.Tables[0].Rows[0]["City"].ToString());
                      }


                      if (ds40.Tables[0].Rows[0].IsNull("Hobli"))
                      {
                          Hobli = "0";
                      }
                      else
                      {
                          Hobli = Convert.ToString(ds40.Tables[0].Rows[0]["Hobli"].ToString());
                      }

                  }



                  if (PAN_No != "0")
                  {

                      lblpanid.Text = Convert.ToString(PAN_No);
                     // lbltnnor.Text = Convert.ToString(TIN_No);

                  }

                  if (TIN_No != "0")
                  {
                     lbltnnor.Text = Convert.ToString(TIN_No);
                  }

                  if (City != "0")
                  {
                      lbltaluk.Text = Convert.ToString(City);
                  }

                  if (Hobli != "0")
                  {
                      lblhobli.Text = Convert.ToString(Hobli);
                  }


                  

                    SqlConnection con50 = new SqlConnection(strconn11);
                    SqlCommand cmd50 = new SqlCommand("select Credit_limit as Credit_limit from tblCustomer where CA_code='" + txtcustcode.Text + "'", con50);
                                     SqlDataAdapter da50 = new SqlDataAdapter(cmd50);
                                     DataSet ds50 = new DataSet();

                                     da50.Fill(ds50);

                                   if (ds50.Tables[0].Rows.Count > 0)
                                   {
                                       if (ds50.Tables[0].Rows[0].IsNull("Credit_limit"))
                                   {
                                     g = 0;
                                   }
                                   else
                                   {
                                       g = Convert.ToDouble(ds50.Tables[0].Rows[0]["Credit_limit"].ToString());
                                   }
                                  }

                                       if(g>=0)
                                       {
                                          // txtbal.BackColor = System.Drawing.Color.Green;

                                          // decimal bal10 = Convert.ToDecimal(g);
                                          // txtbal.Text = bal10.ToString("F");
                                         // txtbal.Text = Convert.ToString(g);
                                         // string balance = txtbal.Text;
                                         // txtcredit.Text = "0";
                                         // lblAB.Visible = true;

                                           decimal bal20 = Convert.ToDecimal(g);
                                           lblcreditlimit.Text = bal20.ToString("F");

                                       }



                                       SqlConnection con60 = new SqlConnection(strconn11);
                                       SqlCommand cmd60 = new SqlCommand("select Credit_amount as Credit_amount from tblCustomer where CA_code='" + txtcustcode.Text + "'", con50);
                                       SqlDataAdapter da60 = new SqlDataAdapter(cmd60);
                                       DataSet ds60 = new DataSet();

                                       da60.Fill(ds60);

                                       if (ds60.Tables[0].Rows.Count > 0)
                                       {
                                           if (ds60.Tables[0].Rows[0].IsNull("Credit_amount"))
                                           {
                                               p = 0;
                                           }
                                           else
                                           {
                                               p = Convert.ToDouble(ds60.Tables[0].Rows[0]["Credit_amount"].ToString());
                                           }
                                       }

                                       if (p >= 0)
                                       {
                                           // txtbal.BackColor = System.Drawing.Color.Green;

                                           // decimal bal10 = Convert.ToDecimal(g);
                                           // txtbal.Text = bal10.ToString("F");
                                           // txtbal.Text = Convert.ToString(g);
                                           // string balance = txtbal.Text;
                                           // txtcredit.Text = "0";
                                           // lblAB.Visible = true;

                                           decimal bal25 = Convert.ToDecimal(p);
                                           txtcredit.Text = bal25.ToString("F");

                                       }


















                                     
                                            SqlConnection con30 = new SqlConnection(strconn11);
                                      SqlCommand cmd30 = new SqlCommand("select Credit_limit as Credit_limit from tblCustomer where CA_code='" + txtcustcode.Text + "'", con30);
                                     SqlDataAdapter da30 = new SqlDataAdapter(cmd30);
                                     DataSet ds30 = new DataSet();

                                     da30.Fill(ds30);

                                   if (ds30.Tables[0].Rows.Count > 0)
                                   {
                                   if (ds30.Tables[0].Rows[0].IsNull("Credit_limit"))
                                   {
                                     a = 0;
                                   }
                                   else
                                   {
                                       a = Convert.ToDouble(ds30.Tables[0].Rows[0]["Credit_limit"].ToString());


                                            SqlConnection con25 = new SqlConnection(strconn11);
                                      SqlCommand cmd15 = new SqlCommand("select Credit_used as Credit_used from tblCustomer where CA_code='" + txtcustcode.Text + "'", con25);
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

                                       string bal = Convert.ToString(a - m + p);
                                        txtbal.BackColor = System.Drawing.Color.Yellow;



                                        txtbal.Text = Convert.ToString(bal);
                                        string balance = txtbal.Text;
                                      //  lblCB.Visible = true;

                                        decimal cuse = Convert.ToDecimal(m);
                                        lblcredituseid.Text = cuse.ToString("F");

                                        decimal bal10 = Convert.ToDecimal(txtbal.Text);
                                        txtbal.Text = bal10.ToString("F");

                                        double camt = Convert.ToDouble(lblcreditlimit.Text);

                                        double cbal = Convert.ToDouble(txtbal.Text);

                                       // txtcredit.BackColor = System.Drawing.Color.Red;
                                       // txtcredit.Text = Convert.ToString(camt - cbal);
                                       string credit = txtcredit.Text;

                                      

                                       }
                                     
                                   }


                                 }
              

              }
                 
              
              else
              {
                  
                  Master.ShowModal("Customer code does not exist", "txtcustcode", 1);
                   txtcustcode.Text=string.Empty;
                   return;
                 
                 
              }


                DataSet ds10=ClsBLGD.GetcondDataSet("*","tblCustomeraccount","CA_code",cuscd);
                if (ds10.Tables[0].Rows.Count > 0)
                {


                    report.Visible = true;

                }
                else
                {
                    report.Visible = false;
                }








              SqlConnection con = new SqlConnection(strconn11);
              SqlCommand cmd1 = new SqlCommand("select Credit_amount as Credit_amount from tblCustomer where CA_code='" + txtcustcode.Text + "'", con);
              SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
              DataSet ds20 = new DataSet();
                da1.Fill(ds20);
               g = Convert.ToDouble(ds20.Tables[0].Rows[0]["Credit_amount"].ToString());
              if(g>0)
              {
                rdtrans.Items[1].Enabled = true;
              }
              else{
                  rdtrans.Items[1].Enabled = false;

              }
              
              

              rdtrans.Enabled = true;
              rdtrans.Focus();
             // cal();
          }
          catch (Exception ex)
         {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
      
          
      }
      protected void txtcustnm_TextChanged(object sender, EventArgs e)
      {

          try
          {
              //txtcustcode.BackColor = Color.LightBlue; 
              string cuscd = txtcustnm.Text;
              DataSet ds1 = ClsBLGD.GetcondDataSet("*", "tblCustomer", "CA_name", cuscd);
              if (ds1.Tables[0].Rows.Count > 0)
              {
                  string cusnm = ds1.Tables[0].Rows[0]["CA_code"].ToString();
                  txtcustcode.Text = cusnm;

                  report.Visible = true;

                  SqlConnection con40 = new SqlConnection(strconn11);
                  SqlCommand cmd40 = new SqlCommand("select Mobileno as Mobileno,Email as Email,City as City,Hobli as Hobli  from tblCustomer where CA_name='" + txtcustnm.Text + "'", con40);
                  SqlDataAdapter da40 = new SqlDataAdapter(cmd40);
                  DataSet ds40 = new DataSet();

                  da40.Fill(ds40);

                  if (ds40.Tables[0].Rows.Count > 0)
                  {
                      if (ds40.Tables[0].Rows[0].IsNull("Mobileno"))
                      {
                          PAN_No = "0";
                      }
                      else
                      {
                          PAN_No = Convert.ToString(ds40.Tables[0].Rows[0]["Mobileno"].ToString());
                      }


                      if (ds40.Tables[0].Rows[0].IsNull("Email"))
                      {
                          TIN_No = "0";
                      }
                      else
                      {
                          TIN_No = Convert.ToString(ds40.Tables[0].Rows[0]["Email"].ToString());
                      }

                      if (ds40.Tables[0].Rows[0].IsNull("City"))
                      {
                          City = "0";
                      }
                      else
                      {
                          City = Convert.ToString(ds40.Tables[0].Rows[0]["City"].ToString());
                      }


                      if (ds40.Tables[0].Rows[0].IsNull("Hobli"))
                      {
                          Hobli = "0";
                      }
                      else
                      {
                          Hobli = Convert.ToString(ds40.Tables[0].Rows[0]["Hobli"].ToString());
                      }

                  }



                  if (PAN_No != "0")
                  {

                      lblpanid.Text = Convert.ToString(PAN_No);
                      // lbltnnor.Text = Convert.ToString(TIN_No);

                  }

                  if (TIN_No != "0")
                  {
                      lbltnnor.Text = Convert.ToString(TIN_No);
                  }

                  if (City != "0")
                  {
                      lbltaluk.Text = Convert.ToString(City);
                  }

                  if (Hobli != "0")
                  {
                      lblhobli.Text = Convert.ToString(Hobli);
                  }




                  SqlConnection con50 = new SqlConnection(strconn11);
                  SqlCommand cmd50 = new SqlCommand("select Credit_limit as Credit_limit from tblCustomer where CA_code='" + txtcustcode.Text + "'", con50);
                  SqlDataAdapter da50 = new SqlDataAdapter(cmd50);
                  DataSet ds50 = new DataSet();

                  da50.Fill(ds50);

                  if (ds50.Tables[0].Rows.Count > 0)
                  {
                      if (ds50.Tables[0].Rows[0].IsNull("Credit_limit"))
                      {
                          g = 0;
                      }
                      else
                      {
                          g = Convert.ToDouble(ds50.Tables[0].Rows[0]["Credit_limit"].ToString());
                      }
                  }

                  if (g >= 0)
                  {
                      // txtbal.BackColor = System.Drawing.Color.Green;

                      // decimal bal10 = Convert.ToDecimal(g);
                      // txtbal.Text = bal10.ToString("F");
                      // txtbal.Text = Convert.ToString(g);
                      // string balance = txtbal.Text;
                      // txtcredit.Text = "0";
                      // lblAB.Visible = true;

                      decimal bal20 = Convert.ToDecimal(g);
                      lblcreditlimit.Text = bal20.ToString("F");

                  }



                  SqlConnection con60 = new SqlConnection(strconn11);
                  SqlCommand cmd60 = new SqlCommand("select Credit_amount as Credit_amount from tblCustomer where CA_code='" + txtcustcode.Text + "'", con50);
                  SqlDataAdapter da60 = new SqlDataAdapter(cmd60);
                  DataSet ds60 = new DataSet();

                  da60.Fill(ds60);

                  if (ds60.Tables[0].Rows.Count > 0)
                  {
                      if (ds60.Tables[0].Rows[0].IsNull("Credit_amount"))
                      {
                          p = 0;
                      }
                      else
                      {
                          p = Convert.ToDouble(ds60.Tables[0].Rows[0]["Credit_amount"].ToString());
                      }
                  }

                  if (p >= 0)
                  {
                      // txtbal.BackColor = System.Drawing.Color.Green;

                      // decimal bal10 = Convert.ToDecimal(g);
                      // txtbal.Text = bal10.ToString("F");
                      // txtbal.Text = Convert.ToString(g);
                      // string balance = txtbal.Text;
                      // txtcredit.Text = "0";
                      // lblAB.Visible = true;

                      decimal bal25 = Convert.ToDecimal(p);
                      txtcredit.Text = bal25.ToString("F");

                  }



















                  SqlConnection con30 = new SqlConnection(strconn11);
                  SqlCommand cmd30 = new SqlCommand("select Credit_limit as Credit_limit from tblCustomer where CA_code='" + txtcustcode.Text + "'", con30);
                  SqlDataAdapter da30 = new SqlDataAdapter(cmd30);
                  DataSet ds30 = new DataSet();

                  da30.Fill(ds30);

                  if (ds30.Tables[0].Rows.Count > 0)
                  {
                      if (ds30.Tables[0].Rows[0].IsNull("Credit_limit"))
                      {
                          a = 0;
                      }
                      else
                      {
                          a = Convert.ToDouble(ds30.Tables[0].Rows[0]["Credit_limit"].ToString());


                          SqlConnection con25 = new SqlConnection(strconn11);
                          SqlCommand cmd15 = new SqlCommand("select Credit_used as Credit_used from tblCustomer where CA_code='" + txtcustcode.Text + "'", con25);
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

                              string bal = Convert.ToString(a - m + p);
                              txtbal.BackColor = System.Drawing.Color.Yellow;



                              txtbal.Text = Convert.ToString(bal);
                              string balance = txtbal.Text;
                              //  lblCB.Visible = true;

                              decimal cuse = Convert.ToDecimal(m);
                              lblcredituseid.Text = cuse.ToString("F");

                              decimal bal10 = Convert.ToDecimal(txtbal.Text);
                              txtbal.Text = bal10.ToString("F");

                              double camt = Convert.ToDouble(lblcreditlimit.Text);

                              double cbal = Convert.ToDouble(txtbal.Text);

                              // txtcredit.BackColor = System.Drawing.Color.Red;
                             // txtcredit.Text = Convert.ToString(camt - cbal);
                              string credit = txtcredit.Text;



                          }

                      }


                  }


              }


              else
              {

                  Master.ShowModal("Customer code does not exist", "txtcustcode", 1);
                  txtcustcode.Text = string.Empty;
                  return;


              }


              DataSet ds10 = ClsBLGD.GetcondDataSet("*", "tblCustomeraccount", "CA_code", cuscd);
              if (ds10.Tables[0].Rows.Count > 0)
              {


                  report.Visible = true;

              }
              else
              {
                  report.Visible = false;
              }








              SqlConnection con = new SqlConnection(strconn11);
              SqlCommand cmd1 = new SqlCommand("select Credit_amount as Credit_amount from tblCustomer where CA_code='" + txtcustcode.Text + "'", con);
              SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
              DataSet ds20 = new DataSet();
              da1.Fill(ds20);
              g = Convert.ToDouble(ds20.Tables[0].Rows[0]["Credit_amount"].ToString());
              if (g > 0)
              {
                  rdtrans.Items[1].Enabled = true;
              }
              else
              {
                  rdtrans.Items[1].Enabled = false;

              }



              rdtrans.Enabled = true;
              rdtrans.Focus();
              // cal();
          }
          catch (Exception ex)
          {
              string asd = ex.Message;
              lblerror.Visible = true;
              lblerror.Text = asd;
          }
      
        
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
          
          SqlConnection con = new SqlConnection(strconn11);
          SqlCommand cmd = new SqlCommand("select * from tblCustomeraccount where CA_code='" + txtcustcode.Text + "'", con);
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          DataSet ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables[0].Rows.Count > 0)
          {
              SqlCommand cmd1 = new SqlCommand("select Credit_amount as Credit_amount from tblCustomer where CA_code='" + txtcustcode.Text + "'", con);
              SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
              DataSet ds1 = new DataSet();

              da1.Fill(ds1);

               if (ds1.Tables[0].Rows.Count > 0)
                {
                   if (ds1.Tables[0].Rows[0].IsNull("Credit_amount"))
                    {
                        g = 0;
                    }
                    else
                    {
                        g = Convert.ToDouble(ds1.Tables[0].Rows[0]["Credit_amount"].ToString());
                    }
                }
               else
                {
                    g = 0;
                }
              
               calc=g;
               if (calc > 0)
                {
                    txtbal.BackColor = System.Drawing.Color.Yellow;
                    txtbal.Text = Convert.ToString(calc);
                    string balance = txtbal.Text;
                   // lblCB.Visible = true;

                }
                else
                {
                    txtbal.BackColor = System.Drawing.Color.Red;
                    txtbal.Text = Convert.ToString(calc);
                    string balance = txtbal.Text;
                }

           }
           else
           {
              txtbal.Text=string.Empty;
              return;
           }
               string dc=txtbal.Text;
               double decre=Convert.ToDouble(dc);
               if(decre>0)
                {
                   // lblCR.Visible=true;
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
           
             OleDbConnection conn11=new OleDbConnection (strconn11);
             OleDbCommand cmd1=new OleDbCommand ("select * from tblCustomeraccount where CA_code='" + txtcustcode.Text + "'", conn11);
             OleDbDataAdapter da=new OleDbDataAdapter (cmd1);
             DataSet ds1 = new DataSet();
             da.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
               OleDbCommand cmd2=new OleDbCommand ("select sum(Totalvalues) as Totalvalues1 from tblCustomeraccount where CA_code='" + txtcustcode.Text + "' and Paymenttype='CA' and Typeoftransaction='D'", conn11);
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
               OleDbCommand cmd3=new OleDbCommand("select sum(Totalvalues) as Totalvalues1 from tblCustomeraccount where CA_code='" + txtcustcode.Text + "' and Paymenttype='CR' and Typeoftransaction='D'", conn11);
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
                OleDbCommand cmd4=new OleDbCommand("select sum(Totalvalues) as Totalvalues1 from tblCustomeraccount where CA_code='" + txtcustcode.Text + "' and Paymenttype='CR' and Typeoftransaction='C'", conn11);
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
                    txtbal.BackColor = System.Drawing.Color.Yellow;
                    txtbal.Text = Convert.ToString(calc);
                   // lblCB.Visible = true;
                }
                else
                {
                    txtbal.BackColor = System.Drawing.Color.Red;
                    txtbal.Text = Convert.ToString(calc);
                }
            }
           else
             {
                txtbal.Text=string.Empty;
                 rdpay.Enabled=true;
                rdpay.Focus();
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
    
      protected void rdpay_SelectedIndexChanged(object sender, EventArgs e)
      {
          if ((rdtrans.SelectedIndex == 0) && (rdpay.SelectedIndex == 0))
          {
              lblamt.Visible = true;
              txtamt.Visible = true;
              lblvou.Visible = true;
              txtvou.Visible = true;
              lblnarr.Visible = true;
              txtaddress.Visible = true;
              lblaccno.Visible = false;
              ddlaccno.Visible = false;
              txtaccno.Visible = false;
              lblreference.Visible = false;
              txtreference.Visible = false;
              lblchqno.Visible = false;
              txtchqno.Visible = false;
              lblbankaccount.Visible = false;
              dddepbankacc.Visible = false;
              lbldate1.Visible = false;
              txtdate1.Visible = false;
              lblaccno1.Visible = false;
          }
          if ((rdtrans.SelectedIndex == 0) && (rdpay.SelectedIndex == 1))
          {
              lblamt.Visible = true;
              txtamt.Visible = true;
              lblvou.Visible = true;
              txtvou.Visible = true;
              lblreference.Visible = false;
              txtreference.Visible = false;
              lblaccno.Visible = false;
              lblaccno1.Visible = true;
              txtaccno.Visible = true;
              lbldate1.Visible = true;
              txtdate1.Visible = true;
              lblnarr.Visible = true;
              txtaddress.Visible = true;
              lblbankaccount.Visible = true;
              dddepbankacc.Visible = true;
              ddlaccno.Visible = false;
              lblchqno.Visible = true;
              txtchqno.Visible = true;
          }
          if ((rdtrans.SelectedIndex == 1) && (rdpay.SelectedIndex == 0))
          {
              lblamt.Visible = true;
              txtamt.Visible = true;
              lblvou.Visible = false;
              txtvou.Visible = false;
              lblreference.Visible = true;
              txtreference.Visible = true;
              lblaccno.Visible = false;
              txtaccno.Visible = false;
              lbldate1.Visible = false;
              txtdate1.Visible = false;
              lblnarr.Visible = true;
              txtaddress.Visible = true;
              ddlaccno.Visible = false;
              lblchqno.Visible = false;
              txtchqno.Visible = false;
              lblaccno1.Visible = false;
              lblbankaccount.Visible = false;
              dddepbankacc.Visible = false;
          }
          if ((rdtrans.SelectedIndex == 1) && (rdpay.SelectedIndex == 1))
          {
              lblamt.Visible = true;
              txtamt.Visible = true;
              lblvou.Visible = false;
              txtvou.Visible = false;
              lblreference.Visible = true;
              txtreference.Visible = true;
              lblaccno.Visible = true;
              txtaccno.Visible = false;
              lbldate1.Visible = true;
              txtdate1.Visible = true;
              lblnarr.Visible = true;
              txtaddress.Visible = true;
              ddlaccno.Visible = true;
              lblchqno.Visible = true;
              txtchqno.Visible = true;
              lblaccno1.Visible = false;
              lblbankaccount.Visible = false;
              dddepbankacc.Visible = false;
          }

         txtamt.Enabled = true;
         txtamt.Focus();
      }
      protected void btnsave_Click(object sender, EventArgs e)
      {
          try
          {
              string Paymenttype="";
              string Typeoftransaction="";
              string invoiceno="0";
              string invoicedate = txtdate.Text;
              if (rdpay.SelectedIndex == 0 && rdtrans.SelectedIndex == 0)
              {
                  Paymenttype="C";
                  Typeoftransaction = "CA";
              }
              else if (rdpay.SelectedIndex == 1 && rdtrans.SelectedIndex == 1)
              {
                  Paymenttype="D";
                  Typeoftransaction = "AD";
              }
              else if (rdpay.SelectedIndex == 0 && rdtrans.SelectedIndex == 1)
              {
                  Paymenttype = "D";
                  Typeoftransaction = "CA";
              }
              else if (rdpay.SelectedIndex == 1 && rdtrans.SelectedIndex == 0)
              {
                  Paymenttype = "C";
                  Typeoftransaction = "AD";
              }

              string Paymentflag="Y";
              string CA_code=txtcustcode.Text;
              string Indate=txtdate.Text;
              //if(rdtrans.SelectedIndex==0)
              //{
              //    Typeoftransaction = "CA";
              //}
              //else if(rdtrans.SelectedIndex==1)
              //{
              //    Typeoftransaction = "AD";
              //}
              string  Vouchrno=txtreference.Text;
              string Totalvalues=txtamt.Text;
              
              string Bankaccno2;
              string Bankaccno;
              if ((rdtrans.SelectedIndex == 0) && (rdpay.SelectedIndex == 1))
              {
                  Bankaccno2 = txtaccno.Text;
                  Bankaccno = ClsBLGD.base64Encode(Bankaccno2);
              }
              else
              {
                  Bankaccno2 = ddlaccno.SelectedItem.Text;
                  Bankaccno = ClsBLGD.base64Encode(Bankaccno2);
              }

              string Chequeno2=txtchqno.Text;

              string Chequeno = ClsBLGD.base64Encode(Chequeno2);

              string Chequedate=txtdate1.Text;
              string Narration=txtaddress.Text;
              string Tr_no="0";
              // string referno=txtvou.Text;
              string Login_name = Session["username"].ToString();
             // System.DateTime Dtnow = DateTime.Now;
             // string Sysdatetime = Dtnow.ToString("dd/MM/yyyy hh:mm:ss");
               string Sysdatetime=DateTime.Now.ToString();
              string Mac_id = sMacAddress;

              if(rdpay.SelectedIndex==0)
              {
                  if (ddlaccno.SelectedItem.Text == "")
                  {
                      Bankaccno="0";
                  }
                  if(txtchqno.Text=="")
                  {
                      Chequeno="0";
                  }
                  if (txtdate1.Text == "")
                  {
                    Chequedate = ("01/01/1990");
                  }
                  if(txtcustcode.Text=="")
                  {
                      Master.ShowModal("Please enter Customer code","txtcustcode",0);
                      return;
                  }
                  if(txtcustnm.Text=="")
                  {
                      Master.ShowModal("Please enter Customer name","txtcustnm",0);
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
                 if(txtamt.Text=="")
                  {
                    Master.ShowModal("Please fill amount", "txtamt", 0);
                    return;
                  }
                  if(rdtrans.SelectedIndex==0)
                  {
                  if(txtvou.Text=="")
                  {
                    Master.ShowModal("Please fill recept details", "txtvou", 0);
                    return;
                  }
                  }
                  else{
                       if(txtreference.Text=="") 
                  {
                    Master.ShowModal("Please fill voucher details", "txtvou", 0);
                    return;
                  }

                  }
                 if (!File.Exists(filename))
                   {
                       try
                       {
                          maxno();
                       //bal();
                          string Bal_type = "C";


                          transno1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                          transno = transno1 + "/" + "CSTACC";
                         // invoiceno1 = ClsBLGD.FetchMaximumInvoiceNo("Select_Max_Invoiceno");
                          //invoiceno = invoiceno1 + "/" + "CSTACC";


                             if (rdtrans.SelectedIndex==1 && rdpay.SelectedIndex==1)
                             {
                                 string referno = txtvou.Text;
                                 double chqno=Convert.ToDouble(txtchqno.Text);
                                  if(chqno==0)
                               {
                                   ClsBLGP.Customeraccno("INSERT_CUSTOMERACCOUNT", "1", transno, invoiceno, invoicedate, Paymenttype, Paymentflag, CA_code, Indate, Typeoftransaction, referno, Totalvalues, Bankaccno, Chequeno, "01/01/1900", Narration, "0", Bal_type, Login_name, Sysdatetime, Mac_id);
                               }
                                  else
                                  {
                                      ClsBLGP.Customeraccno("INSERT_CUSTOMERACCOUNT", "1", transno, invoiceno, invoicedate, Paymenttype, Paymentflag, CA_code, Indate, Typeoftransaction, referno, Totalvalues, Bankaccno, Chequeno, Chequedate, Narration, "0", Bal_type, Login_name, Sysdatetime, Mac_id);
                                  if(rdtrans.SelectedIndex==0)
                                 {
                                      SqlConnection con10 = new SqlConnection(strconn11);
                                      SqlCommand cmd10 = new SqlCommand("select Credit_used as Credit_amount from tblCustomer where CA_code='" + txtcustcode.Text + "'", con10);
                                     SqlDataAdapter da10 = new SqlDataAdapter(cmd10);
                                     DataSet ds10 = new DataSet();

                                     da10.Fill(ds10);

                               if (ds10.Tables[0].Rows.Count > 0)
                                {
                                  if (ds10.Tables[0].Rows[0].IsNull("Credit_amount"))
                                  {
                                     g = 0;
                                  }
                                else
                                {
                                  g = Convert.ToDouble(ds10.Tables[0].Rows[0]["Credit_amount"].ToString());
                                }
                             
                          
                                    // double amt=Convert.ToDouble(txtamt.Text);
                                    // double cramount=g+amt;
                              // SqlConnection conn22 = new SqlConnection(strconn11);
                              // conn22.Open();
                              // SqlCommand cmd22 = new SqlCommand("UPDATE tblCustomer SET  Credit_amount='" + cramount + "' WHERE  CA_code='" + txtcustcode.Text + "'", conn22);
                              // cmd22.ExecuteNonQuery();
                                 }
                             }
                                  else
                                  {
                                       SqlConnection con20 = new SqlConnection(strconn11);
                                      SqlCommand cmd10 = new SqlCommand("select Credit_used as Credit_amount from tblCustomer where CA_code='" + txtcustcode.Text + "'", con20);
                                     SqlDataAdapter da10 = new SqlDataAdapter(cmd10);
                                     DataSet ds10 = new DataSet();

                                     da10.Fill(ds10);

                               if (ds10.Tables[0].Rows.Count > 0)
                                {
                                  if (ds10.Tables[0].Rows[0].IsNull("Credit_amount"))
                                  {
                                     g = 0;
                                  }
                                else
                                {
                                  g = Convert.ToInt32(ds10.Tables[0].Rows[0]["Credit_amount"].ToString());
                                }
                             
                           
                                     double amt=Convert.ToDouble(txtamt.Text);
                                     double cramount=g-amt;
                               SqlConnection conn22 = new SqlConnection(strconn11);
                               conn22.Open();
                               SqlCommand cmd22 = new SqlCommand("UPDATE tblCustomer SET  Credit_amount='" + txtamt.Text + "',Credit_used='" + cramount + "' WHERE  CA_code='" + txtcustcode.Text + "'", conn22);
                               cmd22.ExecuteNonQuery();
                                 }


                                  }
                                      }

                             }
                             else
                             {
                                 if (rdtrans.SelectedIndex == 0 && rdpay.SelectedIndex == 0)
                                 {
                                     string Vouchrno1 = txtvou.Text;


                                     ClsBLGP.Customeraccno("INSERT_CUSTOMERACCOUNT", "1", transno, invoiceno, invoicedate, Paymenttype, Paymentflag, CA_code, Indate, Typeoftransaction, Vouchrno1, Totalvalues, Bankaccno, Chequeno, Chequedate, Narration, "0", Bal_type, Login_name, Sysdatetime, Mac_id);

                                 }

                                 if (rdtrans.SelectedIndex == 1 && rdpay.SelectedIndex == 0)
                                 {
                                     string Vouchrno1 = txtreference.Text;


                                     ClsBLGP.Customeraccno("INSERT_CUSTOMERACCOUNT", "1", transno, invoiceno, invoicedate, Paymenttype, Paymentflag, CA_code, Indate, Typeoftransaction, Vouchrno1, Totalvalues, Bankaccno, Chequeno, Chequedate, Narration, "0", Bal_type, Login_name, Sysdatetime, Mac_id);

                                 }


                                 if (rdtrans.SelectedIndex == 0 && rdpay.SelectedIndex == 1)
                                 {
                                     string Vouchrno1 = txtreference.Text;


                                     ClsBLGP.Customeraccno("INSERT_CUSTOMERACCOUNT", "1", transno, invoiceno, invoicedate, Paymenttype, Paymentflag, CA_code, Indate, Typeoftransaction, Vouchrno1, Totalvalues, Bankaccno, Chequeno, Chequedate, Narration, "0", Bal_type, Login_name, Sysdatetime, Mac_id);

                                 }

                               


                           }

                          
                              bal();
                               cal();

                               SqlConnection con = new SqlConnection(strconn11);
                               SqlCommand cmd21 = new SqlCommand("select max(Columnno) as Columnno from tblCustomeraccount where CA_code='" + txtcustcode.Text + "'", con);
                               SqlDataAdapter da1 = new SqlDataAdapter(cmd21);
                               DataSet ds1 = new DataSet();

                               da1.Fill(ds1);

                               if (ds1.Tables[0].Rows.Count > 0)
                               {
                                   if (ds1.Tables[0].Rows[0].IsNull("Columnno"))
                                   {
                                       g10 = 0;
                                   }
                                   else
                                   {
                                       g10 = Convert.ToDouble(ds1.Tables[0].Rows[0]["Columnno"].ToString());
                                   }
                               }
                               else
                               {
                                   g10 = 0;
                               }


                               //ClsBLGP.updatecustomeraccount("UPDATE_CUSTEMERACCOUNT", Tr_no,"0",invoiceno, invoicedate, Paymenttype, Paymentflag, CA_code, Indate, Typeoftransaction, Vouchrno, Totalvalues, Bankaccno, Chequeno, Chequedate, Narration, balance1, Bal_type, Login_name, Sysdatetime, Mac_id, c);
                               SqlConnection conn20 = new SqlConnection(strconn11);
                               conn20.Open();
                               SqlCommand cmd20 = new SqlCommand("UPDATE tblCustomeraccount SET  Bal_amt='" + balance1 + "',Columnno= '" + g1 + "'" + " WHERE Columnno= " + g10 + " and CA_code='" + txtcustcode.Text + "'", conn20);
                               cmd20.ExecuteNonQuery();

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
                      string Bal_type = "D";

                      maxno();
                      OleDbConnection conn13 = new OleDbConnection(strconn11);
                      conn13.Open();
                      OleDbCommand cmd5 = new OleDbCommand("Insert into tblCustomeraccount(Columnno,Tr_no,Invoiceno,Invoicedate,Paymenttype,Paymentflag,CA_code,Indate,Typeoftransaction,Vouchrno,Totalvalues,Bankaccno,Chequeno,Chequedate,Narration,Bal_amt,Login_name,Sysdatetime,Mac_id)values('1','" + Tr_no + "','" + invoiceno + "','" + invoicedate + "','" + Paymenttype + "','" + Paymentflag + "','" + CA_code + "','" + Indate + "','" + Typeoftransaction + "','" + Vouchrno + "','" + Totalvalues + "','" + Bankaccno + "','" + Chequeno + "','" + Chequedate + "','" + Narration + "','0','" + Login_name + "','" + Sysdatetime + "','" + Mac_id + "')", conn13);
                      cmd5.ExecuteNonQuery();
                      conn13.Close();
                      bal();
                      cal();
                      OleDbConnection conn14 = new OleDbConnection(strconn11);
                      conn14.Open();
                      OleDbCommand cmd21 = new OleDbCommand("select MAX(Columnno) as Columnno1 from tblCustomeraccount where CA_code='" + txtcustcode.Text + "'", conn14);
                      OleDbDataAdapter da1 = new OleDbDataAdapter(cmd21);
                      DataSet ds1 = new DataSet();

                      da1.Fill(ds1);

                      if (ds1.Tables[0].Rows.Count > 0)
                      {
                          if (ds1.Tables[0].Rows[0].IsNull("Columnno1"))
                          {
                              g10 = 0;
                          }
                          else
                          {
                              g10 = Convert.ToInt32(ds1.Tables[0].Rows[0]["Columnno1"].ToString());
                          }
                      }
                      else
                      {
                          g10 = 0;
                      }

                      OleDbConnection conn25 = new OleDbConnection(strconn11);
                      conn25.Open();
                      OleDbCommand cmd25 = new OleDbCommand("UPDATE tblCustomeraccount SET  Bal_amt='" + balance1 + "',Columnno= '" + g1 + "' WHERE Columnno= " + g10 + " AND CA_code='" + txtcustcode.Text + "'", conn25);
                      //cmd20.ExecuteNonQuery();
                  }
              }
              if (rdpay.SelectedIndex == 1)
              {
                  double Bal_amt1 = Convert.ToDouble(txtamt.Text);
                  string Bal_amt = Convert.ToString(Bal_amt1);

                if (txtcustcode.Text == "")
                {
                    Master.ShowModal("Customer Code mandatory", "txtdoorno", 0);
                    return;
                }

                if (txtcustnm.Text == "")
                {
                    Master.ShowModal("Customer Name mandatory", "txtdoorno", 0);
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

                  if(rdtrans.SelectedIndex==0)
                {
                 
                if (txtvou.Text == "")
                {
                    Master.ShowModal("Recept  No mandatory", "txtdoorno", 0);
                    return;
                }

                if (rdtrans.SelectedIndex == 0 && rdpay.SelectedIndex == 1)
                {
                    if (dddepbankacc.SelectedItem.Text == "--Select--")
                    {
                        Master.ShowModal("Select Bank Account", "dddepbankacc", 0);
                        return;
                    }
                    
                }

              }
                  else{

                      if (txtreference.Text == "")
                {
                    Master.ShowModal("Voucher No mandatory", "txtdoorno", 0);
                    return;
                }
                  }
                  if (ddlaccno.Text == "")
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
                       DataSet dsgene = ClsBLGD.GetcondDataSet("*", "tblCustomeraccount", "CA_code", CA_code);
                       if (dsgene.Tables[0].Rows.Count > 0)
                       {
                           try
                           {
                               transno1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                               transno = transno1 + "/" + "CSTACC";

                             
                               //string Columnno = "0";
                               string Bal_type = "D";
                               maxno();
                               string Vouchrno1 = txtvou.Text;
                               ClsBLGP.Customeraccno("INSERT_CUSTOMERACCOUNT", "1", transno, invoiceno, invoicedate, Paymenttype, Paymentflag, CA_code, Indate, Typeoftransaction, Vouchrno1, Totalvalues, Bankaccno, Chequeno, Chequedate, Narration, "0", Bal_type, Login_name, Sysdatetime, Mac_id);
                               bal();
                               cal();
                              

                                 

                               SqlConnection con = new SqlConnection(strconn11);
                               SqlCommand cmd21 = new SqlCommand("select max(Columnno) as Columnno from tblCustomeraccount where CA_code='" + txtcustcode.Text + "'", con);
                               SqlDataAdapter da1 = new SqlDataAdapter(cmd21);
                               DataSet ds1 = new DataSet();

                               da1.Fill(ds1);

                               if (ds1.Tables[0].Rows.Count > 0)
                               {
                                   if (ds1.Tables[0].Rows[0].IsNull("Columnno"))
                                   {
                                       g10 = 0;
                                   }
                                   else
                                   {
                                       g10 = Convert.ToDouble(ds1.Tables[0].Rows[0]["Columnno"].ToString());
                                   }
                               }
                               else
                               {
                                   g10 = 0;
                               }


                               //ClsBLGP.updatecustomeraccount("UPDATE_CUSTEMERACCOUNT", Tr_no,"0",invoiceno, invoicedate, Paymenttype, Paymentflag, CA_code, Indate, Typeoftransaction, Vouchrno, Totalvalues, Bankaccno, Chequeno, Chequedate, Narration, balance1, Bal_type, Login_name, Sysdatetime, Mac_id, c);
                               SqlConnection conn20 = new SqlConnection(strconn11);
                               conn20.Open();
                               SqlCommand cmd20 = new SqlCommand("UPDATE tblCustomeraccount SET  Bal_amt='" + balance1 + "',Columnno= '" + g1 + "'" + " WHERE Columnno= " + g10 + " and CA_code='" + txtcustcode.Text + "'", conn20);
                               cmd20.ExecuteNonQuery();

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

                           string Bal_type = "D";

                           transno1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                           transno = transno1 + "/" + "CSTACC";
                           string Vouchrno1 = txtreference.Text;
                                  
                                   maxno();
                                   ClsBLGP.Customeraccno("INSERT_CUSTOMERACCOUNT", "1", transno, invoiceno, invoicedate, Paymenttype, Paymentflag, CA_code, Indate, Typeoftransaction, Vouchrno1, Totalvalues, Bankaccno, Chequeno, Chequedate, Narration, "0", Bal_type, Login_name, Sysdatetime, Mac_id);
                                   

                                   bal();
                                   cal();
                                   //ClsBLGP.updatecustomeraccount("UPDATE_CUSTEMERACCOUNT", g1, Tr_no, invoiceno, invoicedate, Paymenttype, Paymentflag, CA_code, Indate, Typeoftransaction, Vouchrno, Totalvalues, Bankaccno, Chequeno, Chequedate, Narration, balance1, Bal_type, Login_name, Sysdatetime, Mac_id, c);

                                   SqlConnection con = new SqlConnection(strconn11);
                                   SqlCommand cmd21 = new SqlCommand("select max(Columnno) as Columnno from tblCustomeraccount where CA_code='" + txtcustcode.Text + "'", con);
                                   SqlDataAdapter da1 = new SqlDataAdapter(cmd21);
                                   DataSet ds1 = new DataSet();

                                   da1.Fill(ds1);

                                   if (ds1.Tables[0].Rows.Count > 0)
                                   {
                                       if (ds1.Tables[0].Rows[0].IsNull("Columnno"))
                                       {
                                           g10 = 0;
                                       }
                                       else
                                       {
                                           g10 = Convert.ToInt32(ds1.Tables[0].Rows[0]["Columnno"].ToString());
                                       }
                                   }
                                   else
                                   {
                                       g10 = 0;
                                   }
                                 
                                  

                                   SqlConnection conn20 = new SqlConnection(strconn11);
                                   conn20.Open();
                                   SqlCommand cmd20 = new SqlCommand("UPDATE tblCustomeraccount SET  Bal_amt='" + balance1 + "',Columnno= '" + g1 + "' WHERE Columnno= " + g10 + " AND CA_code='" + txtcustcode.Text + "'", conn20);
                                   cmd20.ExecuteNonQuery();
                                   //cmd20.Close();


                             
                               
                           }

                       
                   }
                   else
                   {
                       string Bal_type = "D";

                       maxno();
                     OleDbConnection conn13=new OleDbConnection(strconn11);
                     conn13.Open();
                     OleDbCommand cmd5 = new OleDbCommand("Insert into tblCustomeraccount(Columnno,Tr_no,Invoiceno,Invoicedate,Paymenttype,Paymentflag,CA_code,Indate,Typeoftransaction,Vouchrno,Totalvalues,Bankaccno,Chequeno,Chequedate,Narration,Bal_amt,Login_name,Sysdatetime,Mac_id)values('1','" + Tr_no + "','" + invoiceno + "','" + invoicedate + "','" + Paymenttype + "','" + Paymentflag + "','" + CA_code + "','" + Indate + "','" + Typeoftransaction + "','" + Vouchrno + "','" + Totalvalues + "','" + Bankaccno + "','" + Chequeno + "','" + Chequedate + "','" + Narration + "','0','" + Login_name + "','" + Sysdatetime + "','" + Mac_id + "')", conn13);
                     cmd5.ExecuteNonQuery();
                     conn13.Close();
                     bal();
                     cal();
                     OleDbConnection conn14 = new OleDbConnection(strconn11);
                     conn14.Open();
                     OleDbCommand cmd21 = new OleDbCommand("select MAX(Columnno) as Columnno1 from tblCustomeraccount where CA_code='" + txtcustcode.Text + "'", conn14);
                     OleDbDataAdapter da1 = new OleDbDataAdapter(cmd21);
                     DataSet ds1 = new DataSet();

                     da1.Fill(ds1);

                     if (ds1.Tables[0].Rows.Count > 0)
                     {
                         if (ds1.Tables[0].Rows[0].IsNull("Columnno1"))
                         {
                             g10 = 0;
                         }
                         else
                         {
                             g10 = Convert.ToInt32(ds1.Tables[0].Rows[0]["Columnno1"].ToString());
                         }
                     }
                     else
                     {
                         g10 = 0;
                     }

                     OleDbConnection conn25 = new OleDbConnection(strconn11);
                     conn25.Open();
                     OleDbCommand cmd25 = new OleDbCommand("UPDATE tblCustomeraccount SET  Bal_amt='" + balance1 + "',Columnno= '" + g1 + "' WHERE Columnno= " + g10 + " AND CA_code='" + txtcustcode.Text + "'", conn25);
                     //cmd20.ExecuteNonQuery();
                     //OleDbCommand cmd1 = new OleDbCommand("update tblChemical set CC_name='" + gennam + "' where CC_code=" + c + "", conn10);
                     cmd25.ExecuteNonQuery();
                     conn25.Close();

                   }
               }
           
              
                if(rdtrans.SelectedIndex==0)
                                 {
                                     SqlConnection con20 = new SqlConnection(strconn11);
                                      SqlCommand cmd10 = new SqlCommand("select Credit_used as Credit_used,Credit_amount as Credit_amount  from tblCustomer where CA_code='" + txtcustcode.Text + "'", con20);
                                     SqlDataAdapter da10 = new SqlDataAdapter(cmd10);
                                     DataSet ds10 = new DataSet();

                                     da10.Fill(ds10);
                                     double gc;

                               if (ds10.Tables[0].Rows.Count > 0)
                                {
                                    if (ds10.Tables[0].Rows[0].IsNull("Credit_used"))
                                  {
                                     g = 0;
                                  }
                                else
                                {
                                  g = Convert.ToDouble(ds10.Tables[0].Rows[0]["Credit_used"].ToString());
                                }
                                    if (ds10.Tables[0].Rows.Count > 0)
                                    {
                                        if (ds10.Tables[0].Rows[0].IsNull("Credit_amount"))
                                        {
                                            gc = 0;

                                        }
                                        else
                                        {
                                            gc = Convert.ToDouble(ds10.Tables[0].Rows[0]["Credit_amount"].ToString());
                                        }
                                    }
                           
                                     double amt=Convert.ToDouble(txtamt.Text);
                                     double cramount = amt - g;
                                         
                               SqlConnection conn22 = new SqlConnection(strconn11);
                               conn22.Open();
                               if (cramount == 0)
                               {
                                   SqlCommand cmd22 = new SqlCommand("UPDATE tblCustomer SET  Credit_used='" + cramount + "',Credit_amount=Credit_amount+'" + txtamt.Text + "' WHERE  CA_code='" + txtcustcode.Text + "'", conn22);
                                   cmd22.ExecuteNonQuery();
                               }
                               if (cramount > 0)
                               {
                                   SqlCommand cmd221 = new SqlCommand("UPDATE tblCustomer SET  Credit_amount=Credit_amount+'" + cramount + "',Credit_used='0'  WHERE  CA_code='" + txtcustcode.Text + "'", conn22);
                                   cmd221.ExecuteNonQuery();
                               }

                               if (cramount < 0)
                               {
                                   double cramt = 0;
                                   cramt = System.Math.Abs(cramount);
                                   SqlCommand cmd222 = new SqlCommand("UPDATE tblCustomer SET  Credit_used=Credit_used-'" + cramt + "',Credit_amount=Credit_amount+'" + txtamt.Text + "' WHERE  CA_code='" + txtcustcode.Text + "'", conn22);
                                   cmd222.ExecuteNonQuery();
                               }

                               if ((rdtrans.SelectedIndex == 0) && (rdpay.SelectedIndex == 1))
                               {
                                   transno1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                                   transno = transno1 + "/" + "CSTACC";

                                   System.DateTime Dtnow = DateTime.Now;
                                   string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                                   string custcode = txtcustcode.Text;
                                   string proamt = txtamt.Text;

                                   string Chequeno5 = txtchqno.Text;
                                   string Chequeno1 = ClsBLGD.base64Encode(Chequeno5);


                                   string Vouchrno5 = txtvou.Text;
                                   string Vouchrno1 = ClsBLGD.base64Encode(Vouchrno5);

                                   SqlConnection con = new SqlConnection(strconn11);
                                   con.Open();
                                   SqlCommand cmd = new SqlCommand("select * from tblVoachermaster where Subhead='" + dddepbankacc.SelectedItem.Text + "'", con);
                                   SqlDataAdapter da = new SqlDataAdapter(cmd);
                                   DataSet ds = new DataSet();
                                   da.Fill(ds);
                                   if (ds.Tables[0].Rows.Count > 0)
                                   {
                                       string headercode = ds.Tables[0].Rows[0]["Headercode"].ToString();

                                       transno1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                                       transno = transno1 + "/" + "CSTACC";
                                       ClsBLGP3.Transaction("INSERT_TRANSACTION", transno, txtdate.Text, custcode, "0000", headercode, "N", Chequeno1, Vouchrno1,"0000.00", proamt , "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                   }
                                   transno1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                                   transno = transno1 + "/" + "CSTACC";
                                   ClsBLGP3.Transaction("INSERT_TRANSACTION", transno, txtdate.Text, custcode, "0000", "9994", "N", Chequeno1, Vouchrno1, proamt, "0000.00", "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                   
                               }

                               if ((rdtrans.SelectedIndex == 0) && (rdpay.SelectedIndex == 0))
                               {
                                   transno1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                                   transno = transno1 + "/" + "CSTACC";

                                   System.DateTime Dtnow = DateTime.Now;
                                   string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                                   string custcode = txtcustcode.Text;
                                   string proamt = txtamt.Text;

                                   //string Chequeno5 = txtchqno.Text;
                                   string Chequeno1 = "0";


                                   string Vouchrno5 = txtvou.Text;
                                   string Vouchrno1 = ClsBLGD.base64Encode(Vouchrno5);

                                   ClsBLGP3.Transaction("INSERT_TRANSACTION", transno, txtdate.Text, custcode, "0000", "9994", "N", Chequeno1, Vouchrno1, "0000.00", "0000.00", proamt, "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                               }
                               if ((rdtrans.SelectedIndex == 1) && (rdpay.SelectedIndex == 0))
                               {
                                   transno1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                                   transno = transno1 + "/" + "CSTACC";

                                   System.DateTime Dtnow = DateTime.Now;
                                   string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                                   string custcode = txtcustcode.Text;
                                   string proamt = txtamt.Text;

                                   //string Chequeno5 = txtchqno.Text;
                                   string Chequeno1 = "0";


                                   string Vouchrno5 = txtvou.Text;
                                   string Vouchrno1 = ClsBLGD.base64Encode(Vouchrno5);

                                   ClsBLGP3.Transaction("INSERT_TRANSACTION", transno, txtdate.Text, custcode, "0000", "9994", "N", Chequeno1, Vouchrno1, "0000.00", "0000.00", "0000.00", proamt, "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                   
                               }

                               if ((rdtrans.SelectedIndex == 1) && (rdpay.SelectedIndex == 1))
                               {
                                   transno1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                                   transno = transno1 + "/" + "CSTACC";

                                   System.DateTime Dtnow = DateTime.Now;
                                   string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                                   string custcode = txtcustcode.Text;
                                   string proamt = txtamt.Text;

                                   string Chequeno5 = txtchqno.Text;
                                   string Chequeno1 = ClsBLGD.base64Encode(Chequeno5);



                                   string Vouchrno1 = "0";

                                   ClsBLGP3.Transaction("INSERT_TRANSACTION", transno, txtdate.Text, custcode, "0000", "9994", "N", Chequeno1, Vouchrno1, "0000.00", proamt, "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
 
                               }
                              
                               
                              

                                     lblsuccess.Visible = true;
            //lblsuccess.Text = "inserted successfully" ID = " + calc.ToString() ;
                                lblsuccess.Text = "Record inserted successfully. Balance = " + cramount.ToString() ;
                                 ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                                cal();
                                rdtrans.SelectedIndex = 0;
                                rdpay.SelectedIndex = 0;
                                txtcustcode.Text=string.Empty;
                                txtcustnm.Text=string.Empty;
                                txtamt.Text=string.Empty;
                                ddlaccno.ClearSelection();
                                txtaddress.Text=string.Empty;
                                txtreference.Text=string.Empty;
                                txtchqno.Text=string.Empty;
                                txtvou.Text= string.Empty;
                                txtbal.Text=string.Empty;
                                txtcredit.Text = string.Empty;
                                deposit();
                                //lblAB.Visible = false;
                               // lblCB.Visible = false;

                                lblcreditlimit.Text = string.Empty;
                                lblcredituseid.Text = string.Empty;


                                if (rdtrans.SelectedIndex == 0 && rdpay.SelectedIndex == 0)
                                {
                                    deposit();
                                    lblbankaccount.Visible = false;
                                    dddepbankacc.Visible = false;
                                }
                                 }
                            
                               
                             }

                           else{

                                      SqlConnection con20 = new SqlConnection(strconn11);
                                      SqlCommand cmd10 = new SqlCommand("select Credit_amount as Credit_amount from tblCustomer where CA_code='" + txtcustcode.Text + "'", con20);
                                     SqlDataAdapter da10 = new SqlDataAdapter(cmd10);
                                     DataSet ds10 = new DataSet();

                                     da10.Fill(ds10);

                               if (ds10.Tables[0].Rows.Count > 0)
                                {
                                  if (ds10.Tables[0].Rows[0].IsNull("Credit_amount"))
                                  {
                                     g = 0;
                                  }
                                else
                                {
                                  g = Convert.ToDouble(ds10.Tables[0].Rows[0]["Credit_amount"].ToString());
                                }
                             
                           
                                     double amt=Convert.ToDouble(txtamt.Text);
                                     double cramount=g-amt;
                               SqlConnection conn22 = new SqlConnection(strconn11);
                               conn22.Open();
                               SqlCommand cmd22 = new SqlCommand("UPDATE tblCustomer SET  Credit_amount='" + cramount + "' WHERE  CA_code='" + txtcustcode.Text + "'", conn22);
                               cmd22.ExecuteNonQuery();

                               if ((rdtrans.SelectedIndex == 0) && (rdpay.SelectedIndex == 1))
                               {
                                   transno1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                                   transno = transno1 + "/" + "CSTACC";

                                   System.DateTime Dtnow = DateTime.Now;
                                   string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                                   string custcode = txtcustcode.Text;
                                   string proamt = txtamt.Text;

                                   string Chequeno5 = txtchqno.Text;
                                   string Chequeno1 = ClsBLGD.base64Encode(Chequeno5);


                                   string Vouchrno5 = txtvou.Text;
                                   string Vouchrno1 = ClsBLGD.base64Encode(Vouchrno5);

                                   SqlConnection con = new SqlConnection(strconn11);
                                   con.Open();
                                   SqlCommand cmd = new SqlCommand("select * from tblVoachermaster where Subhead='" + dddepbankacc.SelectedItem.Text + "'", con);
                                   SqlDataAdapter da = new SqlDataAdapter(cmd);
                                   DataSet ds = new DataSet();
                                   da.Fill(ds);
                                   if (ds.Tables[0].Rows.Count > 0)
                                   {
                                       string headercode = ds.Tables[0].Rows[0]["Headercode"].ToString();

                                       transno1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                                       transno = transno1 + "/" + "CSTACC";
                                       ClsBLGP3.Transaction("INSERT_TRANSACTION", transno, txtdate.Text, custcode, "0000", headercode, "N", Chequeno1, Vouchrno1, "0000.00", proamt, "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                                   }
                                   transno1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                                   transno = transno1 + "/" + "CSTACC";
                                   ClsBLGP3.Transaction("INSERT_TRANSACTION", transno, txtdate.Text, custcode, "0000", "9994", "N", Chequeno1, Vouchrno1, proamt, "0000.00", "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                               }

                               if ((rdtrans.SelectedIndex == 0) && (rdpay.SelectedIndex == 0))
                               {
                                   transno1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                                   transno = transno1 + "/" + "CSTACC";

                                   System.DateTime Dtnow = DateTime.Now;
                                   string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                                   string custcode = txtcustcode.Text;
                                   string proamt = txtamt.Text;

                                   string Chequeno5 = txtchqno.Text;
                                   string Chequeno1 = ClsBLGD.base64Encode(Chequeno5);


                                   string Vouchrno5 = txtvou.Text;
                                   string Vouchrno1 = ClsBLGD.base64Encode(Vouchrno5);

                                   ClsBLGP3.Transaction("INSERT_TRANSACTION", transno, txtdate.Text, custcode, "0000", "9994", "N", Chequeno1, Vouchrno1, "0000.00", "0000.00", proamt, "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                               }
                               if ((rdtrans.SelectedIndex == 1) && (rdpay.SelectedIndex == 0))
                               {
                                   transno1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                                   transno = transno1 + "/" + "CSTACC";

                                   System.DateTime Dtnow = DateTime.Now;
                                   string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                                   string custcode = txtcustcode.Text;
                                   string proamt = txtamt.Text;

                                   string Chequeno5 = txtchqno.Text;
                                   string Chequeno1 = ClsBLGD.base64Encode(Chequeno5);


                                   string Vouchrno5 = txtvou.Text;
                                   string Vouchrno1 = ClsBLGD.base64Encode(Vouchrno5);

                                   ClsBLGP3.Transaction("INSERT_TRANSACTION", transno, txtdate.Text, custcode, "0000", "9994", "N", Chequeno1, Vouchrno1, "0000.00", "0000.00", "0000.00", proamt, "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                               }

                               if ((rdtrans.SelectedIndex == 1) && (rdpay.SelectedIndex == 1))
                               {
                                   transno1 = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
                                   transno = transno1 + "/" + "CSTACC";

                                   System.DateTime Dtnow = DateTime.Now;
                                   string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                                   string custcode = txtcustcode.Text;
                                   string proamt = txtamt.Text;

                                   string Chequeno5 = txtchqno.Text;
                                   string Chequeno1 = ClsBLGD.base64Encode(Chequeno5);


                                   string Vouchrno5 = txtvou.Text;
                                   string Vouchrno1 = ClsBLGD.base64Encode(Vouchrno5);

                                   ClsBLGP3.Transaction("INSERT_TRANSACTION", transno, txtdate.Text, custcode, "0000", "9994", "N", Chequeno1, Vouchrno1, "0000.00", proamt, "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                   string subhead = ddlaccno.SelectedItem.Text;
                                   DataSet dsvoucher= ClsBLGD.GetcondDataSet("*", "tblVoachermaster", "Subhead", subhead);
                                   if (dsvoucher.Tables[0].Rows.Count > 0)
                                   {
                                       string header = dsvoucher.Tables[0].Rows[0]["Headercode"].ToString();
                                       ClsBLGP3.Transaction("INSERT_TRANSACTION", transno, txtdate.Text, custcode, "0000", header, "N", Chequeno1, Vouchrno1, proamt,"0000.00","0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                                   }
                               }
                              
                                 
                                
             
             lblsuccess.Visible = true;
            //lblsuccess.Text = "inserted successfully" ID = " + calc.ToString() ;
              lblsuccess.Text = "Record inserted successfully. Balance = " + cramount.ToString() ;
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
              cal();
            txtcustcode.Text=string.Empty;
            txtcustnm.Text=string.Empty;
            txtamt.Text=string.Empty;
            ddlaccno.ClearSelection();
            txtaddress.Text=string.Empty;
           txtreference.Text=string.Empty;
            txtchqno.Text=string.Empty;
            txtvou.Text= string.Empty;
             txtbal.Text=string.Empty;
             txtcredit.Text = string.Empty;
            // lblAB.Visible = false;
            // lblCB.Visible = false;
             lblcreditlimit.Text = string.Empty;
             lblcredituseid.Text = string.Empty;
             //txtcredit.Text = string.Empty;
             //txtbal.Text = string.Empty;


                     }
                }
                if ((rdtrans.SelectedIndex == 0) && (rdpay.SelectedIndex == 0))
                {
                    lblamt.Visible = true;
                    txtamt.Visible = true;
                    lblvou.Visible = true;
                    txtvou.Visible = true;
                    lblnarr.Visible = true;
                    txtaddress.Visible = true;
                    lblaccno.Visible = false;
                    ddlaccno.Visible = false;
                    txtaccno.Visible = false;
                    lblreference.Visible = false;
                    txtreference.Visible = false;
                    lblchqno.Visible = false;
                    txtchqno.Visible = false;
                    lblbankaccount.Visible = false;
                    dddepbankacc.Visible = false;
                    lbldate1.Visible = false;
                    txtdate1.Visible = false;
                    lblaccno1.Visible = false;
                }
                if ((rdtrans.SelectedIndex == 0) && (rdpay.SelectedIndex == 1))
                {
                    lblamt.Visible = true;
                    txtamt.Visible = true;
                    lblvou.Visible = true;
                    txtvou.Visible = true;
                    lblreference.Visible = false;
                    txtreference.Visible = false;
                    lblaccno.Visible = false;
                    lblaccno1.Visible = true;
                    txtaccno.Visible = true;
                    lbldate1.Visible = true;
                    txtdate1.Visible = true;
                    lblnarr.Visible = true;
                    txtaddress.Visible = true;
                    lblbankaccount.Visible = true;
                    dddepbankacc.Visible = true;
                    ddlaccno.Visible = false;
                    lblchqno.Visible = true;
                    txtchqno.Visible = true;
                }
                if ((rdtrans.SelectedIndex == 1) && (rdpay.SelectedIndex == 0))
                {
                    lblamt.Visible = true;
                    txtamt.Visible = true;
                    lblvou.Visible = false;
                    txtvou.Visible = false;
                    lblreference.Visible = true;
                    txtreference.Visible = true;
                    lblaccno.Visible = false;
                    txtaccno.Visible = false;
                    lbldate1.Visible = false;
                    txtdate1.Visible = false;
                    lblnarr.Visible = true;
                    txtaddress.Visible = true;
                    ddlaccno.Visible = false;
                    lblchqno.Visible = false;
                    txtchqno.Visible = false;
                    lblaccno1.Visible = false;
                    lblbankaccount.Visible = false;
                    dddepbankacc.Visible = false;
                }
                if ((rdtrans.SelectedIndex == 1) && (rdpay.SelectedIndex == 1))
                {
                    lblamt.Visible = true;
                    txtamt.Visible = true;
                    lblvou.Visible = false;
                    txtvou.Visible = false;
                    lblreference.Visible = true;
                    txtreference.Visible = true;
                    lblaccno.Visible = true;
                    txtaccno.Visible = false;
                    lbldate1.Visible = true;
                    txtdate1.Visible = true;
                    lblnarr.Visible = true;
                    txtaddress.Visible = true;
                    ddlaccno.Visible = true;
                    lblchqno.Visible = true;
                    txtchqno.Visible = true;
                    lblaccno1.Visible = false;
                    lblbankaccount.Visible = false;
                    dddepbankacc.Visible = false;
                }
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }

    }

      protected void txtdate1_TextChanged(object sender, EventArgs e)
      {
           double chqno=Convert.ToDouble(txtchqno.Text);
         if(chqno==0)
         {
             txtdate1.Text="00/00/0000";
         }
         else{
             
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
         }
          txtaddress.Enabled=true;

          if (rdtrans.SelectedIndex == 0 && rdpay.SelectedIndex == 1)
          {
              dddepbankacc.Focus();
          }
          else
          {
              txtaddress.Focus();
          }


         
      }
      protected void btnexit_Click(object sender, EventArgs e)
      {
          Response.Redirect("Home.aspx");
      }



      public string bal()
      {
          if (!File.Exists(filename))
          {
              try
              {
                  double g;
                  double h;
                  double a;
                  double gh;

                  SqlConnection con = new SqlConnection(strconn11);
                  SqlCommand cmd = new SqlCommand("select * from tblCustomeraccount where CA_code='" + txtcustcode.Text + "'", con);
                  SqlDataAdapter da = new SqlDataAdapter(cmd);
                  DataSet ds = new DataSet();
                  da.Fill(ds);

                  if (ds.Tables[0].Rows.Count > 0)
                  {
                      SqlCommand cmd1 = new SqlCommand("select sum(Totalvalues) as Totalvalues from tblCustomeraccount where CA_code='" + txtcustcode.Text + "' and Paymenttype='CA' and Typeoftransaction='D'", con);
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
                      SqlCommand cmd11 = new SqlCommand("select sum(Totalvalues) as Totalvalues from tblCustomeraccount where CA_code='" + txtcustcode.Text + "' and Paymenttype='CR' and Typeoftransaction='D'", con);
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
                      SqlCommand cmd112 = new SqlCommand("select sum(Totalvalues) as Totalvalues from tblCustomeraccount where CA_code='" + txtcustcode.Text + "' and Paymenttype='CR' and Typeoftransaction='C'", con);
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
                      balance1 = Convert.ToString(calc);

                      if (calc > 0)
                      {
                          txtbal.BackColor = System.Drawing.Color.Yellow;
                          txtbal.Text = Convert.ToString(calc);
                          string balance = txtbal.Text;
                         // lblCB.Visible = true;


                      }
                      else
                      {
                          txtbal.BackColor = System.Drawing.Color.Red;
                          txtbal.Text = Convert.ToString(calc);
                          string balance = txtbal.Text;
                      }

                  }
                  else
                  {

                      txtbal.Text = string.Empty;
                      // return ;
                  }
                  string dc = txtbal.Text;
                  int decre = Convert.ToInt32(dc);
                  if (decre > 0)
                  {
                     // lblCR.Visible = true;
                  }
                  else if (decre < 0)
                  {
                     // lblDR.Visible = true;
                  }

              }
              catch (Exception ex)
              {
                  string asd = ex.Message;
                  lblerror.Visible = true;
                  lblerror.Text = asd;
              } return balance1;


          }
          else
          {
              try
              {
                  double g;
                  double h;
                  double a;
                  double gh;

                  OleDbConnection conn11 = new OleDbConnection(strconn11);
                  OleDbCommand cmd1 = new OleDbCommand("select * from tblCustomeraccount where CA_code='" + txtcustcode.Text + "'", conn11);
                  OleDbDataAdapter da = new OleDbDataAdapter(cmd1);
                  DataSet ds1 = new DataSet();
                  da.Fill(ds1);
                  if (ds1.Tables[0].Rows.Count > 0)
                  {
                      OleDbCommand cmd2 = new OleDbCommand("select sum(Totalvalues) as Totalvalues1 from tblCustomeraccount where CA_code='" + txtcustcode.Text + "' and Paymenttype='CA' and Typeoftransaction='D'", conn11);
                      OleDbDataAdapter da1 = new OleDbDataAdapter(cmd2);
                      DataSet ds2 = new DataSet();
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
                      OleDbCommand cmd3 = new OleDbCommand("select sum(Totalvalues) as Totalvalues1 from tblCustomeraccount where CA_code='" + txtcustcode.Text + "' and Paymenttype='CR' and Typeoftransaction='D'", conn11);
                      OleDbDataAdapter da2 = new OleDbDataAdapter(cmd3);
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
                      OleDbCommand cmd4 = new OleDbCommand("select sum(Totalvalues) as Totalvalues1 from tblCustomeraccount where CA_code='" + txtcustcode.Text + "' and Paymenttype='CR' and Typeoftransaction='C'", conn11);
                      OleDbDataAdapter da3 = new OleDbDataAdapter(cmd4);
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
                      balance1 = Convert.ToString(calc);

                      if (calc > 0)
                      {
                          txtbal.BackColor = System.Drawing.Color.Yellow;
                          txtbal.Text = Convert.ToString(calc);
                         // lblCB.Visible = true;
                      }
                      else
                      {
                          txtbal.BackColor = System.Drawing.Color.Red;
                          txtbal.Text = Convert.ToString(calc);
                      }
                  }
                  else
                  {
                      txtbal.Text = string.Empty;
                      rdpay.Enabled = true;
                      rdpay.Focus();
                      //return;

                  }
                  string dc = txtbal.Text;
                  int decre = Convert.ToInt32(dc);
                  if (decre > 0)
                  {
                      //lblCR.Visible = true;
                  }
                  else if (decre < 0)
                  {
                      //lblDR.Visible = true;
                  }


              }

              catch (Exception ex)
              {
                  string asd = ex.Message;
                  lblerror.Visible = true;
                  lblerror.Text = asd;
              } return balance1;
          }


          
      }

      public string maxno()
      {
          if (!File.Exists(filename))
          {
              try
              {


                  SqlConnection con = new SqlConnection(strconn11);
                  SqlCommand cmd1 = new SqlCommand("select max(Columnno) as Columnno from tblCustomeraccount where CA_code='" + txtcustcode.Text + "'", con);
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
                          g = Convert.ToDouble(ds1.Tables[0].Rows[0]["Columnno"].ToString());
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
                  OleDbConnection con = new OleDbConnection(strconn11);
                  OleDbCommand cmd1 = new OleDbCommand("select max(Columnno) as Columnno from tblCustomeraccount where CA_code='" + txtcustcode.Text + "'", con);
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

     protected void txtchqno_TextChanged(object sender, EventArgs e)
      {
         double chqno=Convert.ToDouble(txtchqno.Text);
         if(chqno==0)
         {
            // string no=Convert.ToString("00/00/0000");
             //txtdate1.Visible=false;
             //lbldate1.Visible=false;
             //txtaddress.Enabled=true;
             //txtaddress.Focus();
         }
         else{
               //System.DateTime Dtnow = DateTime.Now;
              //txtdate1.Text = Dtnow.ToString("dd/MM/yyyy");
               //txtaddress.Enabled=true;
               //txtdate1.Focus();
         }

     }

     protected void dddepbankacc_SelectedIndexChanged(object sender, EventArgs e)
     {
         txtaddress.Focus();

     }

     protected void rdtrans_SelectedIndexChanged(object sender, EventArgs e)
      {
         if((rdtrans.SelectedIndex==0) && (rdpay.SelectedIndex==0))
         {
             lblamt.Visible = true;
             txtamt.Visible = true;
             lblvou.Visible = true;
             txtvou.Visible = true;
             lblnarr.Visible = true;
             txtaddress.Visible = true;
             lblaccno.Visible = false;
             ddlaccno.Visible = false;
             txtaccno.Visible = false;
             lblreference.Visible = false;
             txtreference.Visible = false;
             lblchqno.Visible = false;
             txtchqno.Visible = false;
             lblbankaccount.Visible = false;
             dddepbankacc.Visible = false;
             lbldate1.Visible = false;
             txtdate1.Visible = false;
             lblaccno1.Visible = false;
         }
         if ((rdtrans.SelectedIndex == 0) && (rdpay.SelectedIndex == 1))
         {
             lblamt.Visible = true;
             txtamt.Visible = true;
             lblvou.Visible = true;
             txtvou.Visible = true;
             lblreference.Visible = false;
             txtreference.Visible = false;
             lblaccno.Visible = false;
             lblaccno1.Visible = true;
             txtaccno.Visible = true;
             lbldate1.Visible = true;
             txtdate1.Visible = true;
             lblnarr.Visible = true;
             txtaddress.Visible = true;
             lblbankaccount.Visible = true;
             dddepbankacc.Visible = true;
             ddlaccno.Visible = false;
             lblchqno.Visible = true;
             txtchqno.Visible = true;
         }
         if ((rdtrans.SelectedIndex == 1) && (rdpay.SelectedIndex == 0))
         {
             lblamt.Visible = true;
             txtamt.Visible = true;
             lblvou.Visible = false;
             txtvou.Visible = false;
             lblreference.Visible = true;
             txtreference.Visible = true;
             lblaccno.Visible = false;
             txtaccno.Visible = false;
             lbldate1.Visible = false;
             txtdate1.Visible = false;
             lblnarr.Visible = true;
             txtaddress.Visible = true;
             ddlaccno.Visible = false;
             lblchqno.Visible = false;
             txtchqno.Visible = false;
             lblaccno1.Visible = false;
             lblbankaccount.Visible = false;
             dddepbankacc.Visible = false;
         }
         if ((rdtrans.SelectedIndex == 1) && (rdpay.SelectedIndex == 1))
         {
             lblamt.Visible = true;
             txtamt.Visible = true;
             lblvou.Visible = false;
             txtvou.Visible = false;
             lblreference.Visible = true;
             txtreference.Visible = true;
             lblaccno.Visible = true;
             txtaccno.Visible = false;
             lbldate1.Visible = true;
             txtdate1.Visible = true;
             lblnarr.Visible = true;
             txtaddress.Visible = true;
             ddlaccno.Visible = true;
             lblchqno.Visible = true;
             txtchqno.Visible = true;
             lblaccno1.Visible = false;
             lblbankaccount.Visible = false;
             dddepbankacc.Visible = false;
         }

         rdpay.Focus();


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


     protected void report_Click(object sender, EventArgs e)
     {
         PanelInvc.Visible = true;
         rdList.Focus();
     }
     public void Bind()
     {
         // string filename = Dbconn.Mymenthod();

         try
         {
             //string bname = ddlbname.SelectedItem.Text;
             grcustomerdetails.DataSource = null;
             grcustomerdetails.DataBind();
             tblCustomeraccount.Rows.Clear();
             SqlConnection con = new SqlConnection(strconn11);
             SqlCommand cmd = new SqlCommand();
             if (rdList.SelectedItem.Value == "all")
             {
                 cmd = new SqlCommand("select * from tblCustomeraccount where CA_code = '" + txtcustcode.Text + "'", con);
             }
             else
             {
                 cmd = new SqlCommand("select * from tblCustomeraccount where CA_code = '" + txtcustcode.Text + "' and Invoiceno='" + ddlInvo.SelectedItem.Text + "'", con);
             }
             SqlDataAdapter da = new SqlDataAdapter(cmd);
             DataSet ds = new DataSet();
             da.Fill(ds);
             int sum = 0;
             double Balance = 0;
             double value = 0;

             if (ds.Tables[0].Rows.Count > 0)
             {

                 DataColumn col = new DataColumn("SLNO", typeof(int));
                 col.AutoIncrement = true;
                 col.AutoIncrementSeed = 1;
                 col.AutoIncrementStep = 1;
                 tblCustomeraccount.Columns.Add(col);
                 // tblpurchasesale.Columns.Add("Productcode");
                 
                 tblCustomeraccount.Columns.Add("Invoicedate");
                 tblCustomeraccount.Columns.Add("Invoiceno");
                 tblCustomeraccount.Columns.Add("Credit");
                 tblCustomeraccount.Columns.Add("Debit");
                 tblCustomeraccount.Columns.Add("Balance");
                 // tblCustomeraccount.Columns.Add("Balance");


                 Session["customer"] = tblCustomeraccount;

                 for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                 {


                     tblCustomeraccount = (DataTable)Session["customer"];
                     drrw = tblCustomeraccount.NewRow();
                     // SqlCommand cmd1 = new SqlCommand("select * from tblCustomeraccount where CA_code = '" + txtcustcode.Text + "'", con);
                     //SqlCommand cmd2 = new SqlCommand("select  Totalvalues  from tblCustomeraccount where Bal_type = 'D' and CA_code = '" + txtcustcode.Text + "'", con);
                     //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                     //SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                     // DataSet ds1 = new DataSet();
                     // DataSet ds2 = new DataSet();
                     // da1.Fill(ds1);
                     //da2.Fill(ds2);
                     string Paymenttype = ds.Tables[0].Rows[i]["Paymenttype"].ToString();
                    // double credit = Convert.ToDouble(ds.Tables[0].Rows[i]["Totalvalues"].ToString());
                     string credit6 = ds.Tables[0].Rows[i]["Totalvalues"].ToString();
                     decimal credit7 = Convert.ToDecimal(credit6);
                     string credit = credit7.ToString("F");
                     //int debit = Convert.ToInt32(ds2.Tables[0].Rows[i]["Totalvalues"].ToString());


                     //  drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                     DateTime indate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Invoicedate"].ToString());
                     string date = indate.ToString("yyyy-MM-dd");
                     drrw["Invoicedate"] = date;
                    // drrw["Invoiceno1"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                     //drrw["Invoiceno2"] = ds.Tables[0].Rows[i]["Vouchrno"].ToString();
                     if (ds.Tables[0].Rows[i]["Invoiceno"].ToString() == "0")
                     {
                         drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Vouchrno"].ToString();
                     }
                     if (ds.Tables[0].Rows[i]["Vouchrno"].ToString() == "0")
                     {
                        
                         drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                     }

                     if (Paymenttype == "C")
                     {
                         drrw["Credit"] = credit;
                         drrw["Debit"] = "0";
                         double Ccredit = Convert.ToDouble(credit);
                         Balance = Balance + Ccredit;
                        // Balance = Balance + Ccredit;
                         //drrw["Balance"] = Balance;

                     }
                     else
                     {
                         drrw["Credit"] = "0";
                         drrw["Debit"] = credit;
                         double Ccredit = Convert.ToDouble(credit);
                         Balance = Balance - Ccredit;
                         //Balance = Balance + Ccredit;
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

                     tblCustomeraccount.Rows.Add(drrw);
                     //Griddoctor.DataSource = tbldoctor;
                     //Griddoctor.DataBind();


                 }




                 DataView dws = tblCustomeraccount.DefaultView;
                 dws.Sort = "SLNO ASC";
                 grcustomerdetails.DataSource = tblCustomeraccount;
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
     protected void grcustomerdetails_SelectedIndexChanged(object sender, EventArgs e)
     {

     }
     protected void ddlaccno_SelectedIndexChanged(object sender, EventArgs e)
     {
         txtchqno.Focus();
     }
     protected void Button1_Click(object sender, EventArgs e)
     {
         Response.Redirect("Customer_accno.aspx");
     }
     protected void btnPrnt_Click(object sender, EventArgs e)
     {
         if (rdList.SelectedItem.Value== "")
         {
             Master.ShowModal("Please select either all or invoice", "rdlist", 1);
             return;
         }
         if (rdList.SelectedItem.Value == "invc")
         {
             if (ddlInvo.SelectedItem.Text == "Select Invoice")
             {
                 Master.ShowModal("Please select an invoice", "ddlInvo", 1);
                 return;
             }
         }
         if (rdList.SelectedValue == "all")
         {
             Bind();
             //arraylist  oALHospDetails = Hosp.HospitalReturns();
             SqlConnection con = new SqlConnection(strconn11);
             SqlCommand cmd10 = new SqlCommand("select * from tblCustomer where CA_code = '" + txtcustcode.Text + "'", con);
             SqlDataAdapter da10 = new SqlDataAdapter(cmd10);
             DataSet ds10 = new DataSet();
             da10.Fill(ds10);

             ArrayList oALHospDetails = Hosp.HospitalReturns();

             string customercode = ds10.Tables[0].Rows[0]["CA_code"].ToString();
             string customername = ds10.Tables[0].Rows[0]["CA_name"].ToString();
             string address1 = ds10.Tables[0].Rows[0]["Address1"].ToString();
             // string address2 = ds10.Tables[0].Rows[0]["Address2"].ToString();
             string Hobli = ds10.Tables[0].Rows[0]["Hobli"].ToString();
             string Taluk = ds10.Tables[0].Rows[0]["Taluk"].ToString();
             string District = ds10.Tables[0].Rows[0]["District"].ToString();
             string State = ds10.Tables[0].Rows[0]["State"].ToString();


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


             tblstock.AddCell(PhraseCell(new Phrase("Customer Report\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
             cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
             cell.Colspan = 2;
             cell.PaddingBottom = 30f;
             tblstock.AddCell(cell);


             tbldt.AddCell(PhraseCell(new Phrase("Customer Name :" + customername, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
             tbldt.AddCell(PhraseCell(new Phrase(" Customer Code:" + customercode, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
             cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
             cell.Colspan = 2;
             cell.PaddingBottom = 30f;
             tbldt.AddCell(cell);
             tbldt.SpacingAfter = 15f;

             tbldt.AddCell(PhraseCell(new Phrase(" Address:" + address1 + "," + Hobli + "\n" + Taluk + "," + District + "\n" + State + "\n", FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
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
             SqlCommand cmd11 = new SqlCommand("select  MAX(Totalvalues) as Totalvalues from tblCustomeraccount where CA_code = '" + txtcustcode.Text + "'", con1);
             SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
             DataSet ds11 = new DataSet();
             da11.Fill(ds11);
             string openbal = ds11.Tables[0].Rows[0]["Totalvalues"].ToString();

             SqlConnection con2 = new SqlConnection(strconn11);
             SqlCommand cmd12 = new SqlCommand("select  SUM(Totalvalues) as Totalvalues from tblCustomeraccount where CA_code = '" + txtcustcode.Text + "' and Paymenttype = 'C'", con2);
             SqlCommand cmd13 = new SqlCommand("select  SUM(Totalvalues) as Totalvalues from tblCustomeraccount where CA_code = '" + txtcustcode.Text + "'and Paymenttype = 'D'", con2);
             SqlDataAdapter da12 = new SqlDataAdapter(cmd12);
             SqlDataAdapter da13 = new SqlDataAdapter(cmd13);
             DataSet ds12 = new DataSet();
             DataSet ds13 = new DataSet();
             da12.Fill(ds12);
             da13.Fill(ds13);

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

             table5.AddCell(PhraseCell(new Phrase("\n\n\n\n" + "This is a computer generated ststement and does not require signature", FontFactory.GetFont("Times", 7, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
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
             Response.AddHeader("Content-Disposition", "attachment; filename=CustomerReport.pdf");
             PanelInvc.Visible = false;
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
             Bind();
             //arraylist  oALHospDetails = Hosp.HospitalReturns();
             SqlConnection con = new SqlConnection(strconn11);
             SqlCommand cmd10 = new SqlCommand("select * from tblCustomer where CA_code = '" + txtcustcode.Text + "'", con);
             SqlDataAdapter da10 = new SqlDataAdapter(cmd10);
             DataSet ds10 = new DataSet();
             da10.Fill(ds10);

             ArrayList oALHospDetails = Hosp.HospitalReturns();

             string customercode = ds10.Tables[0].Rows[0]["CA_code"].ToString();
             string customername = ds10.Tables[0].Rows[0]["CA_name"].ToString();
             string address1 = ds10.Tables[0].Rows[0]["Address1"].ToString();
             // string address2 = ds10.Tables[0].Rows[0]["Address2"].ToString();
             string Hobli = ds10.Tables[0].Rows[0]["Hobli"].ToString();
             string Taluk = ds10.Tables[0].Rows[0]["Taluk"].ToString();
             string District = ds10.Tables[0].Rows[0]["District"].ToString();
             string State = ds10.Tables[0].Rows[0]["State"].ToString();


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


             tblstock.AddCell(PhraseCell(new Phrase("Customer Report\n", FontFactory.GetFont("Times", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER));
             cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
             cell.Colspan = 2;
             cell.PaddingBottom = 30f;
             tblstock.AddCell(cell);


             tbldt.AddCell(PhraseCell(new Phrase("Customer Name :" + customername, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
             tbldt.AddCell(PhraseCell(new Phrase(" Customer Code:" + customercode, FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
             cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
             cell.Colspan = 2;
             cell.PaddingBottom = 30f;
             tbldt.AddCell(cell);
             tbldt.SpacingAfter = 15f;

             tbldt.AddCell(PhraseCell(new Phrase(" Address:" + address1 + "," + Hobli + "\n" + Taluk + "," + District + "\n" + State + "\n", FontFactory.GetFont("Times", 12, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_LEFT));
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
             SqlCommand cmd11 = new SqlCommand("select  MAX(Totalvalues) as Totalvalues from tblCustomeraccount where CA_code = '" + txtcustcode.Text + "' and Invoiceno='" + ddlInvo.SelectedItem.Text + "'", con1);
             SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
             DataSet ds11 = new DataSet();
             da11.Fill(ds11);
             string openbal = ds11.Tables[0].Rows[0]["Totalvalues"].ToString();

             SqlConnection con2 = new SqlConnection(strconn11);
             SqlCommand cmd12 = new SqlCommand("select  SUM(Totalvalues) as Totalvalues from tblCustomeraccount where CA_code = '" + txtcustcode.Text + "' and Invoiceno='" + ddlInvo.SelectedItem.Text + "' and Paymenttype = 'C'", con2);
             SqlCommand cmd13 = new SqlCommand("select  SUM(Totalvalues) as Totalvalues from tblCustomeraccount where CA_code = '" + txtcustcode.Text + "' and Invoiceno='" + ddlInvo.SelectedItem.Text + "' and Paymenttype = 'D'", con2);
             SqlDataAdapter da12 = new SqlDataAdapter(cmd12);
             SqlDataAdapter da13 = new SqlDataAdapter(cmd13);
             DataSet ds12 = new DataSet();
             DataSet ds13 = new DataSet();
             da12.Fill(ds12);
             da13.Fill(ds13);

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

             table5.AddCell(PhraseCell(new Phrase("\n\n\n\n" + "This is a computer generated ststement and does not require signature", FontFactory.GetFont("Times", 7, Font.NORMAL, BaseColor.BLACK)), PdfPCell.ALIGN_RIGHT));
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
             Response.AddHeader("Content-Disposition", "attachment; filename=CustomerReport.pdf");
             PanelInvc.Visible = false;
             byte[] bytes = memorystream.ToArray();
             memorystream.Close();
             Response.Clear();
             Response.Buffer = true;
             Response.Cache.SetCacheability(HttpCacheability.NoCache);
             Response.BinaryWrite(bytes);
             Response.End();
             Response.Close();
             
         }
}
     protected void rdList_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (rdList.SelectedValue == "all")
         {
             lblInvo.Visible = false;
             ddlInvo.Visible = false;
             btnPrnt.Focus();
         }
         if (rdList.SelectedValue == "invc")
         {
             lblInvo.Visible = true;
             ddlInvo.Visible = true;
             SqlConnection con11 = new SqlConnection(strconn11);
             using (con11)
             {
                 using (SqlCommand cmd = new SqlCommand("select * from tblCustomeraccount where CA_code='" + txtcustcode.Text + "' and Invoiceno!='0'"))
                 {
                     cmd.CommandType = CommandType.Text;
                     cmd.Connection = con11;
                     con11.Open();
                     ddlInvo.DataSource = cmd.ExecuteReader();
                     ddlInvo.DataTextField = "Invoiceno";
                     ddlInvo.DataBind();
                     con11.Close();
                 }

             }
             ddlInvo.Items.Insert(0, "Select Invoice");
             ddlInvo.Focus();
         }
     }
     protected void ddlInvo_SelectedIndexChanged(object sender, EventArgs e)
     {
         btnPrnt.Focus();
     }
}