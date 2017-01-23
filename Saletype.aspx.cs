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

public partial class Saletype : System.Web.UI.Page
{
    ClsBALSaletype clsbal = new ClsBALSaletype();
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
    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Visible = false;
        lblsuccess.Visible = false;
        if (!IsPostBack)
        {
            ddpaymenttype.Focus();
        }
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

    protected void btnsave_Click(object sender, EventArgs e)
    {

        string filename = Dbconn.Mymenthod();
        try
        {

            //string mainhead = txtmainhead.Text;

            string Saletype = ddpaymenttype.SelectedItem.Text;
            string Amount = txtamount.Text;
           
            string Login_name = Session["username"].ToString();
            System.DateTime Dtnow = DateTime.Now;
            
            string Sysdatetime = Dtnow.ToString("dd/MM/yyyy hh:mm:ss");
            string Mac_id = sMacAddress;
            //string Headercode = "9000";



            if (Saletype == "")
            {
                Master.ShowModal("Sale Type is mandatory", "txtsaletype", 0);
                return;
            }

            if (Amount == "")
            {
                Master.ShowModal("Amount mandatory", "txtamount", 0);
                return;
            }

            if (!File.Exists(filename))
            {
                clsbal.Salecardtype("INSERT_SALECARDTYPE", Saletype, Amount, Login_name, Mac_id, Sysdatetime);
            }
            else
            {
                OleDbConnection conn12 = new OleDbConnection(strconn11);
                conn12.Open();
                OleDbCommand cmd5 = new OleDbCommand("Insert into tblSaletype(Saletype,Extraamount, Login_name, Mac_id,Sysdatetime)values('" + Saletype + "','" + Amount + "','" + Login_name + "','" + Sysdatetime + "','" + Mac_id + "')", conn12);
                cmd5.ExecuteNonQuery();
                conn12.Close();
            }

            lblsuccess.Visible = true;
            lblsuccess.Text = "inserted successfully";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            ddpaymenttype.ClearSelection();
            txtamount.Text = string.Empty;
            ddpaymenttype.Focus();
        }

        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }


    }

    protected void btnexit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}