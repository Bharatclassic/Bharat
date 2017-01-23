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

public partial class Formmaster : System.Web.UI.Page
{
    ClsBALFormmaster ClsBLGP = new ClsBALFormmaster();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string button_select;
    DataTable tblformmaster = new DataTable();
     DataTable dt = new DataTable();
     protected static string filename = Dbconn.Mymenthod();
    protected  static string strconn11 = Dbconn.conmenthod();
     DataRow drrw;
     string sMacAddress = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Visible = false;
        lblsuccess.Visible = false;
        if (!IsPostBack)
        {
            txtformunitmaster.Focus();
            Table2.Visible = false;
            lblcode.Visible = false;
            btndelete.Enabled = false;
            Bind();
           // GetMACAddress();

        }
         if (Session["username"] != null)
        {

        }
        else
        {
            Response.Redirect("Index.aspx");
        }
        GetMACAddress();
        ClientScript.RegisterStartupScript(this.GetType(), "SetInitialFocus", "<script>document.getElementById('" + txtformunitmaster.ClientID + "').focus();</script>");
        Button2.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtformunitmaster.ClientID + "').focus();return false;}} else {return true}; ");       
    }
    public void Bind()
    {
        //string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            try
            {
                Gridform.DataSource = null;
                Gridform.DataBind();
                tblformmaster.Rows.Clear();
                SqlConnection con = new SqlConnection(strconn11);
                SqlCommand cmd = new SqlCommand("select * from tblformmaster order by formname", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblformmaster.Columns.Add(col);
                    tblformmaster.Columns.Add("FORM NAME");

                    Session["Formmaster"] = tblformmaster;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tblformmaster = (DataTable)Session["Formmaster"];
                        drrw = tblformmaster.NewRow();

                        drrw["FORM NAME"] = ds.Tables[0].Rows[i]["formname"].ToString();

                        tblformmaster.Rows.Add(drrw);
                        //Gridform.DataSource = tblformmaster;
                        //Gridform.DataBind();


                    }
                    DataView dw = tblformmaster.DefaultView;
                    dw.Sort = "SLNO ASC";
                   // dw.Sort = "form name ASC";
                    Gridform.DataSource = tblformmaster;
                    Gridform.DataBind();
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
                Gridform.DataSource = null;
                Gridform.DataBind();
                tblformmaster.Rows.Clear();
                OleDbConnection conn10 = new OleDbConnection(strconn11);
                conn10.Open();
                OleDbCommand cmd1 = new OleDbCommand("select * from tblformmaster order by formname", conn10);

                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblformmaster.Columns.Add(col);
                    tblformmaster.Columns.Add("FORM NAME");

                    Session["Formmaster"] = tblformmaster;

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        tblformmaster = (DataTable)Session["Formmaster"];
                        drrw = tblformmaster.NewRow();

                        drrw["FORM NAME"] = ds1.Tables[0].Rows[i]["formname"].ToString();

                        tblformmaster.Rows.Add(drrw);
                        //Gridform.DataSource = tblformmaster;
                        //Gridform.DataBind();


                    }
                    DataView dw = tblformmaster.DefaultView;
                    dw.Sort = "SLNO ASC";
                   // dw.Sort = " form name ASC";
                    Gridform.DataSource = tblformmaster;
                    Gridform.DataBind();
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
    protected void Gridform_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Gridform.PageIndex = e.NewPageIndex;
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
               // GetMACAddress();
                string Sysdatetime=DateTime.Now.ToString();
                string gennam = txtformunitmaster.Text;
                //string gennam = txtgenename.Text;
                string cod = lblcode.Text;
                int c = Convert.ToInt32(cod);

                if (!File.Exists(filename))
                {
                    ClsBLGP.updateForm("UPDATE_FORMMASTER", gennam, c);
                    //ClsBLGD.UpdateRecords("tblformmaster", "formname='" + gennam + "'", "formcode='" + lblcode.Text + "'");
                }
                else
                {
                    OleDbConnection conn10 = new OleDbConnection(strconn11);
                    conn10.Open();

                    OleDbCommand cmd1 = new OleDbCommand("update tblformmaster set formname='" + gennam + "' where formcode=" + c + "", conn10);
                    cmd1.ExecuteNonQuery();
                    conn10.Close();
                }
                
             
                lblsuccess.Visible = true;
                lblsuccess.Text = "Modified Successfully";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                Bind();
                button_select = string.Empty;
                txtformunitmaster.Text = string.Empty;
                txtformunitmaster.Enabled = true;
                txtformunitmaster.Focus();
                //  Response.Redirect("Chemical.aspx");
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
               // GetMACAddress();
                string Sysdatetime=DateTime.Now.ToString();
                string formname = txtformunitmaster.Text.TrimStart();
                string strCaps1 = Regex.Replace(formname, "[^a-zA-Z + \\s]", "");
                string strEdited = Regex.Replace(strCaps1, @"\s+", " ");

                if (strEdited == "")
                {

                    Master.ShowModal("Form  Name mandatory", "txtformunitmaster", 0);
                    return;

                }
                DataSet dsgrp = ClsBLGD.GetcondDataSet("*", "tblformmaster", "formname", formname);
                if (dsgrp.Tables[0].Rows.Count > 0)
                {
                    try{
                    lblmod.Text = "Form Name Already Exists";
                    int code = Convert.ToInt32(dsgrp.Tables[0].Rows[0]["formcode"].ToString());
                    lblcode.Text = Convert.ToString(code);
                    Table2.Visible = true;
                    btn.Enabled = true;
                    btn.Focus();
                    
                   // Bind();
                   // Master.ShowModal("Form Name Alraedy exists", "txtformunitmaster", 1);
                    return;
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

                    if (!File.Exists(filename))
                    {
                        try{
                        ClsBLGP.Form("INSERT_FORMMASTER", formname,Session["username"].ToString(),sMacAddress,Sysdatetime);
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
                        try{
                        //String strconn11 = Dbconn.conmenthod();
                        OleDbConnection con = new OleDbConnection(strconn11);
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("insert into tblformmaster(formname,LoginName,Mac_id,Sysdatetime) values('" + formname + "', '" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        }
                         catch (Exception ex)
                       {
                         string asd = ex.Message;
                         lblerror.Enabled = true;
                         lblerror.Text = asd;
                      }

                    }

                    lblsuccess.Visible = true;
                    lblsuccess.Text = "inserted successfully";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    Bind();
                    txtformunitmaster.Text = string.Empty;
                    txtformunitmaster.Enabled = true;
                    txtformunitmaster.Focus();

                    //Response.Redirect("Formmaster.aspx");

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

                    cmd.CommandText = "select formname from tblformmaster where formname like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["formname"]));
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

                    cmd.CommandText = "select formname from tblformmaster where formname like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (OleDbDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["formname"]));
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
        txtformunitmaster.Focus();
    }
    public string mod()
    {
        button_select = dbcon.modify();

        if (button_select == "Modify")
        {
            txtformunitmaster.Enabled = true;
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
        txtformunitmaster.Text = string.Empty;
        txtformunitmaster.Enabled = true;
        txtformunitmaster.Focus();
        
    }

}
