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

public partial class SaleReturnStock : System.Web.UI.Page
{
    ClsBLLGeneraldetails clsgd = new ClsBLLGeneraldetails();
    ClsBALPurchaseReturn ClsBLGP = new ClsBALPurchaseReturn();
    //ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
    DataTable tblProductinward = new DataTable();
    protected static string button_select;
   

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (!File.Exists(filename))
        {

            string Quantity = txtquantity.Text;

            //lblstockhand.Text = Request.QueryString["transno"];

            string STransno = Session["STransno"].ToString();

            SqlConnection conn = new SqlConnection(strconn11);
            conn.Open();

            //SqlCommand cmd = new SqlCommand("SELECT * FROM detail", conn);

            SqlCommand cmd = new SqlCommand("update  tblProductsale  set  Quantity='" + Quantity + "' where STransno='" + STransno + "'", conn);

            cmd.ExecuteNonQuery();

            conn.Close();

            lblsuccess.Visible = true;
            lblsuccess.Text = "Modified successfully";

            txtquantity.Text = string.Empty;


        }
        else
        {
            string Quantity = txtquantity.Text;

            //lblstockhand.Text = Request.QueryString["transno"];

            string STransno = Session["STransno"].ToString();

            OleDbConnection conn = new OleDbConnection(strconn11);
            conn.Open();

            //SqlCommand cmd = new SqlCommand("SELECT * FROM detail", conn);

            OleDbCommand cmd = new OleDbCommand("update  tblProductsale  set  Quantity='" + Quantity + "' where STransno =" + STransno + "", conn);

            cmd.ExecuteNonQuery();

            conn.Close();

            lblsuccess.Visible = true;
            lblsuccess.Text = "Modified successfully";

            txtquantity.Text = string.Empty;


        }
    }
   
  


    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}