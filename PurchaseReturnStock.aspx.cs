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

public partial class PurchaseReturnStock : System.Web.UI.Page
{
    ClsBLLGeneraldetails clsgd = new ClsBLLGeneraldetails();
    ClsBALPurchaseReturn ClsBLGP = new ClsBALPurchaseReturn();
    //ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
    DataTable tblProductinward = new DataTable();
    protected static string button_select;
    string sMacAddress = "";
    DataRow drrw;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           

        }

    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("PurchaseReturn.aspx");
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (!File.Exists(filename))
        {

        string Stockinhand = txtstockhand.Text;

        //lblstockhand.Text = Request.QueryString["transno"];

        string Transno = Session["Transno"].ToString();

        SqlConnection conn = new SqlConnection(strconn11);
        conn.Open();

        //SqlCommand cmd = new SqlCommand("SELECT * FROM detail", conn);

        SqlCommand cmd = new SqlCommand("update  tblProductinward  set  Stockinhand='" + Stockinhand + "' where TransNo='" + Transno + "'", conn);

        cmd.ExecuteNonQuery();

        conn.Close();

        lblsuccess.Visible = true;
        lblsuccess.Text = "Modified successfully";

        txtstockhand.Text = string.Empty;
    }
    else
        {

            string Stockinhand = txtstockhand.Text;

            //lblstockhand.Text = Request.QueryString["transno"];

            string Transno = Session["Transno"].ToString();

            OleDbConnection conn = new OleDbConnection(strconn11);
            conn.Open();

            //SqlCommand cmd = new SqlCommand("SELECT * FROM detail", conn);

            OleDbCommand cmd = new OleDbCommand("update  tblProductinward  set  Stockinhand='" + Stockinhand + "' where TransNo='" + Transno + "'", conn);

            cmd.ExecuteNonQuery();

            conn.Close();

            lblsuccess.Visible = true;
            lblsuccess.Text = "Modified successfully";

            txtstockhand.Text = string.Empty;



        }
    }
    


}