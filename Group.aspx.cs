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
using Microsoft.VisualBasic;
using System.Net.NetworkInformation;
using System.Management;
using System.Runtime.InteropServices; 

public partial class Group : System.Web.UI.Page
{
    ClsBALGroup ClsBLGP = new ClsBALGroup();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string button_select;
    protected  static string filename = Dbconn.Mymenthod();
    protected  static string strconn11 = Dbconn.conmenthod();
    DataTable tblGroup = new DataTable();
    DataTable dt = new DataTable();
    DataRow drrw;
    string sMacAddress = "";
    int g;
     //string result = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        
        lblerror.Visible = false;
        lblsuccess.Visible = false;
        if (!IsPostBack)
        {
            txtgroup.Enabled = true;
            txtgroup.Focus();
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
        ClientScript.RegisterStartupScript(this.GetType(), "SetInitialFocus", "<script>document.getElementById('" + txtgroup.ClientID + "').focus();</script>");
        Button2.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtgroup.ClientID + "').focus();return false;}} else {return true}; ");       
       
    }

    public void Bind()
    {
        if (!File.Exists(filename))
        {
            try
            {
                Gridgroup.DataSource = null;
                Gridgroup.DataBind();
                tblGroup.Rows.Clear();
                SqlConnection con = new SqlConnection(strconn11);
                SqlCommand cmd = new SqlCommand("select * from tblGroup order by g_name", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblGroup.Columns.Add(col);
                    tblGroup.Columns.Add("GROUP NAME");

                    Session["Group"] = tblGroup;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tblGroup = (DataTable)Session["Group"];
                        drrw = tblGroup.NewRow();

                        drrw["GROUP NAME"] = ds.Tables[0].Rows[i]["g_name"].ToString();

                        tblGroup.Rows.Add(drrw);
                    }
                    DataView dw = tblGroup.DefaultView;
                    dw.Sort = "SLNO ASC";
                    Gridgroup.DataSource = tblGroup;
                    Gridgroup.DataBind();
                }
            }
            catch (Exception e)
            {
                string asd = e.Message;
                lblerror.Enabled = true;
                lblerror.Text = asd;
            }
        }
        else
        {
            try
            {
                Gridgroup.DataSource = null;
                Gridgroup.DataBind();
                tblGroup.Rows.Clear();
                OleDbConnection conn10 = new OleDbConnection(strconn11);
                conn10.Open();
                OleDbCommand cmd1 = new OleDbCommand("select * from tblGroup order by g_name", conn10);

                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblGroup.Columns.Add(col);
                    tblGroup.Columns.Add("GROUP NAME");

                    Session["Group"] = tblGroup;
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        tblGroup = (DataTable)Session["Group"];
                        drrw = tblGroup.NewRow();
                        drrw["GROUP NAME"] = ds1.Tables[0].Rows[i]["g_name"].ToString();

                        tblGroup.Rows.Add(drrw);
                    }
                    DataView dw = tblGroup.DefaultView;
                    dw.Sort = "SLNO ASC";
                    Gridgroup.DataSource = tblGroup;
                    Gridgroup.DataBind();
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
    protected void Gridgroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Gridgroup.PageIndex = e.NewPageIndex;
        Bind();

    }

   public string GetMACAddress()
   {
    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
     foreach (NetworkInterface adapter in nics)
     {
        if (sMacAddress == String.Empty)// only return MAC Address from first card  
        {
            IPInterfaceProperties properties = adapter.GetIPProperties();
            sMacAddress = adapter.GetPhysicalAddress().ToString();
        }
        sMacAddress = sMacAddress.Replace(":", "");
     }
       return sMacAddress;
   }

    protected void Button1_Click1(object sender, EventArgs e)
    {

       // SqlConnection conn = new SqlConnection(strconn11);
         // SqlCommand cmd10 = new SqlCommand("create table tblTempTable(Id int, SomeColumn varchar(50))", conn);
         // conn.Open();
       // cmd10.ExecuteNonQuery();
        // SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);
       // bulkCopy.DestinationTableName = "tblTempTable";
        // bulkCopy.WriteToServer(dt);
         // SqlDataAdapter da55 = new SqlDataAdapter(cmd10);
        // DataSet ds55 = new DataSet();
        // da55.Fill(ds55);
        // g = Convert.ToInt32(ds55.Tables[0].Rows[0]["TABLE_NAME"].ToString());

       // conn.Close();

        //SqlConnection conn12 = new SqlConnection(strconn11);
       // SqlCommand cmd11 = new SqlCommand("SELECT TABLE_NAME FROM information_schema.tables", conn12);
        // SqlDataAdapter da50 = new SqlDataAdapter(cmd11);
        // DataSet ds50 = new DataSet();
        // da50.Fill(ds50);
        // g = Convert.ToInt32(ds50.Tables[0].Rows[0]["TABLE_NAME"].ToString());
         
         

        if (button_select == "Modify")
        {
            try
            {
                string Sysdatetime=DateTime.Now.ToString();
                string gennam = txtgroup.Text;
                string gflag = chkgroup.Checked ? "Y" : "N";
                string cod = lblcode.Text;
                int c = Convert.ToInt32(cod);
                if (!File.Exists(filename))
                {
                    try
                    {
                     ClsBLGP.updateGroup("UPDATE_GROUP",gennam,gflag,c);
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
                    OleDbConnection conn10 = new OleDbConnection(strconn11);
                    conn10.Open();

                    OleDbCommand cmd1 = new OleDbCommand("update tblGroup set g_name='" + gennam + "',p_flag='" + gflag + "' where g_code=" + c + "", conn10);
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
                lblsuccess.Visible = true;
                lblsuccess.Text = "Modified Successfully";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                Bind();
                button_select = string.Empty;
                txtgroup.Text = string.Empty;
                txtgroup.Enabled = true;
                txtgroup.Focus();
                chkgroup.Checked = false;
                //Gridgroup.DataSource = null;
                //Gridgroup.Columns.Clear();
            }
            catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Visible = true;
                lblerror.Text = asd;
            }

        }
        else if (button_select != "Modify")
        {
            try
            {
                 //GetHDDSerialNo() ;
                string Sysdatetime=DateTime.Now.ToString();
                string gname = txtgroup.Text.TrimStart();
                string gflag = chkgroup.Checked ? "Y" : "N";
                string strCaps1 = Regex.Replace(gname, "[^a-zA-Z + \\s]", "");
                string strEdited = Regex.Replace(strCaps1, @"\s+", " ");
                //string gname = txtgroup.Text;
                if (strEdited == "")
                {
                   // Microsoft.VisualBasic.Interaction.MsgBox("GroupName is Mandatory");
                    Master.ShowModal("GroupName is Mandatory", "txtgroup", 0);
                    return;

                }
                DataSet dsgrp = ClsBLGD.GetcondDataSet("*", "tblGROUP", "g_name", gname);
                if (dsgrp.Tables[0].Rows.Count > 0)
                {
                    try{
                    lblmod.Text = "Group Name Already Exists";
                    int code = Convert.ToInt32(dsgrp.Tables[0].Rows[0]["g_code"].ToString());
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
                    if (!File.Exists(filename))
                    {
                      try
                      {
                       ClsBLGP.Group("INSERT_GROUP", gname, gflag,Session["username"].ToString(),sMacAddress,Sysdatetime);
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
                        OleDbConnection con = new OleDbConnection(strconn11);
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("insert into tblGroup(g_name,p_flag,LoginName,Mac_id,Sysdatetime) values('" + gname + "','" + gflag + "','" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con);
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
                    chkgroup.Checked = false;
                    txtgroup.Text = string.Empty;
                    txtgroup.Enabled = true;
                    txtgroup.Focus();
                    txtgroup.Text = string.Empty;

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
    protected void Button4_Click(object sender, EventArgs e)
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
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select g_name from tblGroup where g_name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["g_name"]));
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
                    cmd.CommandText = "select g_name from tblGroup where g_name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (OleDbDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["g_name"]));
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
        txtgroup.Enabled = true;
        txtgroup.Focus();
    }
    public string mod()
    {
        button_select = dbcon.modify();
        if (button_select == "Modify")
        {
            txtgroup.Enabled = true;
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
        txtgroup.Text = string.Empty;
        txtgroup.Enabled = true;
        txtgroup.Focus();
        return;
    }

    protected void txtgroup_TextChanged(object sender, EventArgs e)
    {
        HttpCookie deleteCookie = new HttpCookie("txtinvoicedate");
        Response.Cookies.Add(deleteCookie);
        deleteCookie.Expires = DateTime.Now.AddDays(-1);
        chkgroup.Focus();
    }
}


   
