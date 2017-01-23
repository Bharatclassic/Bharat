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

public partial class Sales_Tax : System.Web.UI.Page
{
     ClsBALSaletax clsbal = new ClsBALSaletax();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string button_select;
    DataTable dt = new DataTable();
    DataRow drrw;
    //string mac = "";
    protected static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
    //  string result = "";
    string sMacAddress = "";

    ArrayList arryno = new ArrayList();
    ArrayList arryname = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Visible = false;
        lblsuccess.Visible = false;
        Table2.Visible=false;
        lblcode.Visible=false;
        lblcode1.Visible=false;
        if (!IsPostBack)
        {
            ddlsalestax.Focus();
            //txtdate.Focus();
            salestax();
            System.DateTime Dtnow = DateTime.Now;
            txtdate.Text = Dtnow.ToString("dd/MM/yyyy");
            
        }
        txtrateoftax.Attributes.Add("autocomplete", "off");
        if (Session["username"] != null)
        {

        }
        else
        {
            Response.Redirect("Index.aspx");
        }


       
        
        GetMACAddress();

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

      public void salestax()
    {
        DataSet dsgroup = ClsBLGD.GetDataSet("distinct g_name", "tblGroup");
        for (int i = 0; i < dsgroup.Tables[0].Rows.Count; i++)
        {
            DataSet dsgroup1 = ClsBLGD.GetcondDataSet("*", "tblGroup", "g_name", dsgroup.Tables[0].Rows[i]["g_name"].ToString());
            arryname.Add(dsgroup1.Tables[0].Rows[0]["g_name"].ToString());
            

        }

        arryname.Sort();
        arryno.Add("-Select-");
       // arryno.Add("Add New");
        for (int i = 0; i < arryname.Count; i++)
        {
            arryno.Add(arryname[i].ToString());
        }
        ddlsalestax.DataSource = arryno;
        ddlsalestax.DataBind();
       
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
         string filename = Dbconn.Mymenthod();
         string categorycode=lblcode.Text;
        try
        {

            //string mainhead = txtmainhead.Text;

            string Tax_Rate = txtrateoftax.Text;
            string Category_code = ddlsalestax.Text;

           
            
            System.DateTime Dtnow1 = DateTime.Now;
            string From_Date=Dtnow1.ToString("yyyy/MM/dd");
           // From_Date="1900/01/01";
            
            DateTime dt=Convert.ToDateTime(From_Date);

            // System.DateTime Dtnow2 = DateTime.Now;
            string To_Date="1900/01/01";
              

            string Login_name = Session["username"].ToString();
            System.DateTime Dtnow = DateTime.Now;
            
            string Sysdatetime = Dtnow.ToString("dd/MM/yyyy");
            string Mac_id = sMacAddress;
            //string Headercode = "9000";



            if (Tax_Rate == "")
            {
                Master.ShowModal("Tax Rate is mandatory", "txtrateoftax", 0);
                return;
            }

            if (Category_code == "-Select-")
            {
                Master.ShowModal("Please select a group", "ddlsalestax", 0);
                return;
            }

             DateTime dt1 = Convert.ToDateTime(From_Date);

              DataSet dsgrp = ClsBLGD.GetcondDataSet("*", "tblTax_Rate", "g_code", lblcode.Text);
                if (dsgrp.Tables[0].Rows.Count > 0)
                {
                    try{
                        SqlConnection conn22 = new SqlConnection(strconn11);
                        conn22.Open();
                       // SqlCommand cmd22 = new SqlCommand("UPDATE tblTax_Rate SET   Close_flag ='Y'  WHERE  g_code= '"+ lblcode.Text +"'", conn22);
                        SqlCommand cmd22 = new SqlCommand("UPDATE tblTax_Rate set To_Date ='"+  From_Date +"',Close_flag ='Y'  WHERE  g_code= '"+ lblcode.Text +"'", conn22);
                        cmd22.ExecuteNonQuery();
                    

                   

                    
                

                 if (!File.Exists(filename))
            {
                clsbal.Saletax("INSERT_TAX_RATE", categorycode,Tax_Rate,From_Date,To_Date,"N",Login_name,Mac_id,Sysdatetime);
            }
            else
            {
               // OleDbConnection conn12 = new OleDbConnection(strconn11);
                //conn12.Open();
                //OleDbCommand cmd5 = new OleDbCommand("Insert into tblSaletype(Saletype,Extraamount, Login_name, Mac_id,Sysdatetime)values('" + Saletype + "','" + Amount + "','" + Login_name + "','" + Sysdatetime + "','" + Mac_id + "')", conn12);
                //cmd5.ExecuteNonQuery();
                //conn12.Close();
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

                     clsbal.Saletax("INSERT_TAX_RATE", categorycode,Tax_Rate,From_Date,To_Date,"N",Login_name,Mac_id,Sysdatetime);

                }

            lblsuccess.Visible = true;
            lblsuccess.Text = "inserted successfully";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

             //txtsale.Text=string .Empty;
            txtrateoftax.Text = string.Empty;
            ddlsalestax.ClearSelection();
            ddlsalestax.Focus();
        }

        
             catch (Exception ex)
                   {
                      string asd = ex.Message;
                      lblerror.Visible = true;
                      lblerror.Text = asd;
                   }

       

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
         Response.Redirect("Home.aspx");
    }

      protected void txtdate_TextChanged(object sender, EventArgs e)
     {
          SqlConnection con = new SqlConnection(strconn11);
                 SqlCommand cmd1 = new SqlCommand("select fin_from_date as fin_from_date,fin_to_date as fin_to_date from tblFin_year", con);
              SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
              DataSet ds1 = new DataSet();
              da1.Fill(ds1);
              string  from_date = Convert.ToString(ds1.Tables[0].Rows[0]["fin_from_date"].ToString());
                DateTime from_date1=Convert.ToDateTime(from_date);
              string to_date= Convert.ToString(ds1.Tables[0].Rows[0]["fin_to_date"].ToString());
                 DateTime to_date1=Convert.ToDateTime(to_date);
               

               // System.DateTime Dtnow1 = DateTime.Now;
               // var current_date = Dtnow1.ToString("dd/MM/yyyy");
                
                 // DateTime current_date1=Convert.ToDateTime(current_date);

                string date1=txtdate.Text;
                DateTime date2=Convert.ToDateTime(date1);
                 
               if(from_date1<date2 && to_date1>date2)
               {
                 ddlsalestax.Focus();
               }
               else
               {
                   Master.ShowModal("Enter data within  fin year", "txtmainhead", 0);
                   return;
               }
          ddlsalestax.Focus();
        
     }

    protected void ddlsalestax_SelectedIndexChanged(object sender, EventArgs e)
      {
        DataSet dsgroup2 = ClsBLGD.GetcondDataSet("*", "tblGroup", "g_name", ddlsalestax.SelectedItem.Text);
        if(dsgroup2.Tables[0].Rows.Count>0)
        {
                int code = Convert.ToInt32(dsgroup2.Tables[0].Rows[0]["g_code"].ToString());
                lblcode.Text = Convert.ToString(code);
        }
        txtrateoftax.Focus();
      }

     protected void txtrateoftax_TextChanged(object sender, EventArgs e)
     {
       Button1.Focus();
     }


}