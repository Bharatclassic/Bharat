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
using System.Web.Services;
using System.Data.OleDb;
using System.Text;
using System.Net.NetworkInformation;
using System.Management;
using System.Runtime.InteropServices; 

public partial class Manufacture : System.Web.UI.Page
{
    ClsBALManufactmaster ClsBLGP = new ClsBALManufactmaster();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string button_select;
    protected static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
    DataTable tblmanufacture = new DataTable();
    DataTable dt = new DataTable();
    DataRow drrw;
   // string result = "";
    String sMacAddress ="";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Visible = false;
        lblsuccess.Visible = false;
        if (!IsPostBack)
        {
            txtmanufactmaster.Focus();
            Table2.Visible = false;
            lblcode.Visible = false;
            btndelete.Enabled = false;
            Bind();
           
        }
         if (Session["username"] != null)
        {

        }
        else
        {
            Response.Redirect("Index.aspx");
        }
      
         GetMACAddress();
        ClientScript.RegisterStartupScript(this.GetType(), "SetInitialFocus", "<script>document.getElementById('" + txtmanufactmaster.ClientID + "').focus();</script>");
        Button2.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtmanufactmaster.ClientID + "').focus();return false;}} else {return true}; ");       
    }

    public void Bind()
    {
       // string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            try
            {
                Gridmanuf.DataSource = null;
                Gridmanuf.DataBind();
                tblmanufacture.Rows.Clear();
                SqlConnection con = new SqlConnection(strconn11);
                SqlCommand cmd = new SqlCommand("select * from tblmanufacture order by ManufactureName", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblmanufacture.Columns.Add(col);
                    tblmanufacture.Columns.Add("MANUFACTURE NAME");

                    Session["Manufacture"] = tblmanufacture;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tblmanufacture = (DataTable)Session["Manufacture"];
                        drrw = tblmanufacture.NewRow();

                        drrw["MANUFACTURE NAME"] = ds.Tables[0].Rows[i]["ManufactureName"].ToString();

                        tblmanufacture.Rows.Add(drrw);
                        //Gridmanuf.DataSource = tblmanufacture;
                        //Gridmanuf.DataBind();
                    }
                    DataView dw = tblmanufacture.DefaultView;

                    dw.Sort = "Manufacture Name ASC";
                    Gridmanuf.DataSource = tblmanufacture;
                    Gridmanuf.DataBind();
                }
            }
            catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Enabled = true;
                lblerror.Text = asd;
            }
        }
        else
        {
            try
            {
                Gridmanuf.DataSource = null;
                Gridmanuf.DataBind();
                tblmanufacture.Rows.Clear();
                OleDbConnection conn10 = new OleDbConnection(strconn11);
                conn10.Open();
                OleDbCommand cmd1 = new OleDbCommand("select * from tblmanufacture order by ManufactureName", conn10);

                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblmanufacture.Columns.Add(col);
                    tblmanufacture.Columns.Add("MANUFACTURE NAME");

                    Session["Manufacture"] = tblmanufacture;

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        tblmanufacture = (DataTable)Session["Manufacture"];
                        drrw = tblmanufacture.NewRow();

                        drrw["MANUFACTURE NAME"] = ds1.Tables[0].Rows[i]["ManufactureName"].ToString();

                        tblmanufacture.Rows.Add(drrw);
                        //Gridmanuf.DataSource = tblmanufacture;
                        //Gridmanuf.DataBind();
                    }
                    DataView dw = tblmanufacture.DefaultView;

                    dw.Sort = "Manufacture Name ASC";
                    Gridmanuf.DataSource = tblmanufacture;
                    Gridmanuf.DataBind();
                }
            }
            catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Enabled = true;
                lblerror.Text = asd;
            }
        }
    }
    protected void Gridmanuf_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Gridmanuf.PageIndex = e.NewPageIndex;
        Bind();

    }

    public string GetMACAddress()
   {
    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
    //String sMacAddress = string.Empty;
    foreach (NetworkInterface adapter in nics)
    {
        if (sMacAddress == String.Empty)// only return MAC Address from first card  
        {
            IPInterfaceProperties properties = adapter.GetIPProperties();
            sMacAddress = adapter.GetPhysicalAddress().ToString();
        }
        sMacAddress = sMacAddress.Replace(":", "");
    } return sMacAddress;
 }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        
        if (button_select == "Modify")
        {
            try
            {
                
                string Sysdatetime=DateTime.Now.ToString();
                string gennam = txtmanufactmaster.Text;
                string cod = lblcode.Text;
                int c = Convert.ToInt32(cod);

                if (!File.Exists(filename))
                {
                  try{
                    ClsBLGP.updateManufacture("UPDATE_MANUFACTURE", gennam, c);
                   }
                  catch (Exception ex)
                   {
                      string asd = ex.Message;
                      lblerror.Visible = true;
                      lblerror.Text = asd;
                   }// ClsBLGD.UpdateRecords("tblmanufacture", "ManufactureName='" + gennam + "'", "ManufactureCode='" + lblcode.Text + "'");
                }
                else
                {
                    try{
                    OleDbConnection conn10 = new OleDbConnection(strconn11);
                    conn10.Open();

                    OleDbCommand cmd1 = new OleDbCommand("update tblmanufacture set ManufactureName='" + gennam + "' where ManufactureCode=" + c + "", conn10);
                    cmd1.ExecuteNonQuery();
                    conn10.Close();
                    }
                  catch (Exception ex)
                   {
                      string asd = ex.Message;
                      lblerror.Visible = true;
                      lblerror.Text = asd;
                   }
                }

                //ClsBLGD.UpdateRecords(""tblGeneric", "GN_name='" + gennam + "'", "GN_code='" + lblcode.Text + "'");
               
                lblsuccess.Visible = true;
                lblsuccess.Text = "Modified Successfully";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                Bind();
                button_select = string.Empty;
                txtmanufactmaster.Text = string.Empty;
                txtmanufactmaster.Enabled = true;
                txtmanufactmaster.Focus();
                //Response.Redirect("Generic.aspx");
            }
            catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Enabled = true;
                lblerror.Text = asd;
            }
        }
        else if (button_select != "Modify")
        {
            try
            {
               // GetHDDSerialNo() ;
                string Sysdatetime=DateTime.Now.ToString();
                string mname = txtmanufactmaster.Text.TrimStart();
                string strCaps1 = Regex.Replace(mname, "[^a-zA-Z + \\s]", "");
                string strEdited = Regex.Replace(strCaps1, @"\s+", " ");
                if (strEdited == "")
                {

                    Master.ShowModal("Manufacture Name mandotory", "txtmanufactmaster", 0);
                    return;

                }
                DataSet dsgrp = ClsBLGD.GetcondDataSet("*", "tblmanufacture", "ManufactureName", mname);
                if (dsgrp.Tables[0].Rows.Count > 0)
                {
                    try{
                    lblmod.Text = "Manufacture Name Already Exists";
                    int code = Convert.ToInt32(dsgrp.Tables[0].Rows[0]["ManufactureCode"].ToString());
                    lblcode.Text = Convert.ToString(code);
                    Table2.Visible = true;
                    btn.Enabled = true;
                    btn.Focus();
                    return;
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
                    //Gridmanuf.DataSource = null;
                    //Bind();
                    //string filename = Dbconn.Mymenthod();
                    if (!File.Exists(filename))
                    {
                        try{
                        ClsBLGP.Manufacture("INSERT_MANUFACTURE", mname,Session["username"].ToString(), sMacAddress,Sysdatetime);
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
                       // String strconn11 = Dbconn.conmenthod();
                      try{
                        OleDbConnection con = new OleDbConnection(strconn11);
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("insert into tblmanufacture(ManufactureName,LoginName,Mac_id,Sysdatetime) values('" + mname + "', '" + Session["username"].ToString() + "','" +  sMacAddress + "','" + Sysdatetime + "')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                      }
                      catch (Exception ex)
                     {
                      string asd = ex.Message;
                      lblerror.Visible = true;
                      lblerror.Text = asd;
                      }

                    }

                    lblsuccess.Visible = true;
                    lblsuccess.Text = "inserted successfully";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    Bind();
                    txtmanufactmaster.Text = string.Empty;
                    txtmanufactmaster.Enabled = true;
                    txtmanufactmaster.Focus();

                    //Response.Redirect("Manufacture.aspx");

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
    protected void Button2_Click(object sender, EventArgs e)
    {
        button_select = string.Empty;
        Response.Redirect("Home.aspx");
    }

    [WebMethod]

    public static string[] GetCustomers(string prefix)
    {

       // string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection(strconn11))
            {
                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

               // conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "select ManufactureName from tblmanufacture where ManufactureName like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["ManufactureName"]));
                        }
                    }
                    conn.Close();
                }
            }
            return customers.ToArray();
        }
        else
        {
            List<string> customers = new List<string>();
           // string strconn1 = Dbconn.conmenthod();
            using (OleDbConnection conn = new OleDbConnection(strconn11))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {

                    cmd.CommandText = "select ManufactureName from tblmanufacture where ManufactureName like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (OleDbDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["ManufactureName"]));
                        }
                    }
                    conn.Close();
                }

            }
            return customers.ToArray();

        }
    }

    protected void btnmodify_Click(object sender, EventArgs e)
    {
        mod();
        Table2.Visible = false;
        txtmanufactmaster.Enabled = true;
        txtmanufactmaster.Focus();
    }
    public string mod()
    {
        button_select = dbcon.modify();

        if (button_select == "Modify")
        {
            txtmanufactmaster.Enabled = true;
        }
        return button_select;
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        no();
    }
    public void no()
    {
        Table2.Visible = false;
        txtmanufactmaster.Text = string.Empty;
        txtmanufactmaster.Enabled = true;
        txtmanufactmaster.Focus();
    }
}