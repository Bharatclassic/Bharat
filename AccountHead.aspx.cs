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
using System.Runtime.InteropServices; 

public partial class AccountHead : System.Web.UI.Page
{
    ClsBALAccountHead ClsBLGP = new ClsBALAccountHead();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string button_select;
    protected static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
   // DataTable tblhead = new DataTable();
  //  DataTable dt = new DataTable();
   // DataRow drrw;
     int count;
     int hedcd;
     string codecode;
    string headcode="";
    string sMacAddress="";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Visible = false;
        lblsuccess.Visible = false;
        if (!IsPostBack)
        {
            txtmainhead.Focus();
            System.DateTime Sysdatetime = DateTime.Now;
            txtdate.Text = Sysdatetime.ToString("dd/MM/yyyy");
            //string Sysdatetime=DateTime.Now.ToString("dd/mm/yyyy");
            //txtdate.Text=Sysdatetime;
        }
       GetMACAddress();

    }

     public void autoincrement()
    {
       try
       {
         if (!File.Exists(filename))
          {
             SqlConnection con = new SqlConnection(strconn11);
             con.Open();
             SqlCommand cmd=new SqlCommand("select Max(Headercode) as Headercode from tblaccounthead",con);
             SqlDataAdapter da=new SqlDataAdapter(cmd);
             DataSet ds=new DataSet();
             da.Fill(ds);
             if(ds.Tables[0].Rows.Count>0)
             {
                 headcode=ds.Tables[0].Rows[0]["Headercode"].ToString();
                 if(headcode=="")
                 {
                     hedcd=0;
                 }
                 else
                 {
                  hedcd=Convert.ToInt32(headcode);
                 }
                  if(hedcd == 0 && hedcd < 9999)
                 {
                      if(hedcd==9000 && hedcd < 9999)
                      {
                          count=Convert.ToInt16(cmd.ExecuteScalar()) + 1;
                          hedcd=count;
                          codecode = Convert.ToString(hedcd);
                      }
                      else
                      {
                        hedcd=9000;
                        codecode = Convert.ToString(hedcd);
                      }
                 }
                 else
                 {
                     count=Convert.ToInt16(cmd.ExecuteScalar()) + 1;
                     hedcd=count;
                     codecode = Convert.ToString(hedcd);
                    // hedcd="000"+count;
                 }
             }
             else
             {
                 Master.ShowModal("no records fount","txtmainhead",1);
                 return;
             }
         }
             else
             {
                 OleDbConnection conn=new OleDbConnection (strconn11);
                 conn.Open();
                 OleDbCommand cmd1=new OleDbCommand ("Select Max(Headercode) from tblaccountHead",conn);
                 OleDbDataAdapter da1=new OleDbDataAdapter (cmd1);
                 DataSet ds1=new DataSet ();
                 da1.Fill(ds1);
             if(ds1.Tables[0].Rows.Count>0)
             {
                 headcode=ds1.Tables[0].Rows[0]["Headercode"].ToString();
                 if(headcode=="")
                 {
                     hedcd=0;
                 }
                 else
                 {
                  hedcd=Convert.ToInt32(headcode);
                 }
                  if(hedcd == 0 && hedcd < 9999)
                 {
                      if(hedcd==9000 && hedcd < 9999)
                      {
                          count=Convert.ToInt16(cmd1.ExecuteScalar()) + 1;
                          hedcd=count;
                      }
                      else
                      {
                        hedcd=9000;
                      }
                 }
                 else
                 {
                     count=Convert.ToInt16(cmd1.ExecuteScalar()) + 1;
                     hedcd=count;
                    // hedcd="000"+count;
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
         string filename = Dbconn.Mymenthod();
            try
            {
                ///string Headercode= "9000";
                string mainhead = txtmainhead.Text;
                string subhead = txtsubhead.Text;
                string bankaccount = chkbankaccount.Checked ? "Y" : "N";
                string others = Chkothers.Checked ? "Y" : "N";
                //string headercode=autoincrement();
               
                autoincrement();
                //string hedcd; 
                string Login_name = Session["username"].ToString();
                //System.DateTime Dtnow = DateTime.Now;
                string Effectivedate = txtdate.Text;
                //string Effectivedate = "01/01/1990";
                //string Sysdatetime=txtdate.Text;
                //System.DateTime Dtnow1 = DateTime.Now;
                //string Sys = Dtnow1.ToString("dd/MM/yyyy");
                string Sys = txtdate.Text;
                //string Sysdatetime =  
                //int incNumber = 0;

                //string nyNumber = "s" + incNumber.ToString("00");
                //incNumber++;

                if (mainhead == "")
                {

                    Master.ShowModal("Main Head mandatory", "txtmainhead", 0);
                    txtmainhead.Focus();
                    return;

                }
                   if (subhead == "")
                   {

                       Master.ShowModal("Sub head mandatory", "txtsubhead", 0);
                       txtsubhead.Focus();
                       return;

                   }
                if(bankaccount=="" && others=="")
                {
                    Master.ShowModal("Please select one bank","txtmainhead",0);
                    return;
                }

                  if (!File.Exists(filename))
                    {

                        //ClsBLGP.Accounthead("INSERT_ACCOUNTHEAD", hedcd, mainhead, subhead, Effectivedate, bankaccount, others, Login_name, sMacAddress, Sys);
                        ClsBLGP.Accounthead("INSERT_ACCOUNTHEAD", codecode, mainhead, subhead, Effectivedate, bankaccount, others, Login_name, Sys, sMacAddress);
                       //SqlConnection con=new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);      
                       //con.Open();
                       //SqlCommand cmd=new SqlCommand("update tblaccounthead set Headercode='" + headercode + "',where Slno=(select max(Slno-1)from tblTaxnameMaster)",con);
                       //cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        //String strconn11 = Dbconn.conmenthod();
                        OleDbConnection con = new OleDbConnection(strconn11);
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("insert into tblaccounthead(hedcd,Mainhead,Subhead,Effectivedate,Bankaccount,Others) values('" + mainhead + "','" + subhead + "','" + bankaccount + "','" + others + "','" + Login_name + "','" + sMacAddress + "','" + Sys + "')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    lblsuccess.Visible = true;
                    lblsuccess.Text = "inserted successfully";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    //Bind();

                }
            
            catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Visible = true;
                lblerror.Text = asd;
            }
        }

    protected void chkbankaccount_CheckedChanged(object sender, EventArgs e)
    {
        if (chkbankaccount.Checked == true)
        {
            //Chkothers.Checked=!chkbankaccount.Checked;
            Chkothers.Visible = false;
        }
        else
        {
            Chkothers.Visible = true;
        }
        //else if(Chkothers.Checked==true)
        //{
        //    chkbankaccount.Checked=false;
        //}
       
    }
    protected void Chkothers_CheckedChanged(object sender, EventArgs e)
    {
        if (Chkothers.Checked == true)
        {
            chkbankaccount.Visible = false;
        }
        else
        {
            chkbankaccount.Visible = true;
        }

        //else if(Chkothers.Checked==false)
        //{
        //   // chkbankaccount.Checked=true;
        //}
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}
