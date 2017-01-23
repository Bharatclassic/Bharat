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

public partial class Doctor : System.Web.UI.Page
{
    ClsBALDoctor ClsBLGP = new ClsBALDoctor();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string button_select;
    DataTable tbldoctor = new DataTable();
    DataTable dt = new DataTable();
    DataRow drrw;
    protected static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
    String sMacAddress = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        
        lblerror.Visible = false;
        lblsuccess.Visible = false;
        if (!IsPostBack)
        {
            Table2.Visible = false;
            lblcode.Visible = false;
           
            btndelete.Enabled = false;
            Bind();
            //txtdname.Text = "Dr.";
            //this.txtdname.Focus();
            }
        txtdspec.Attributes.Add("autocomplete", "off");
         if (Session["username"] != null)
        {

        }
        else
        {
            Response.Redirect("Index.aspx");
        }
        GetMACAddress();
        ClientScript.RegisterStartupScript(this.GetType(), "SetInitialFocus", "<script>document.getElementById('" + txtdname.ClientID + "').focus();</script>");
        btnexit.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtdname.ClientID + "').focus();return false;}} else {return true}; ");
       

    }


      public string GetMACAddress()
    {
       NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
      // String sMacAddress = string.Empty;
       foreach (NetworkInterface adapter in nics)
       {
         if (sMacAddress == String.Empty)// only return MAC Address from first card  
         {
            IPInterfaceProperties properties = adapter.GetIPProperties();
            sMacAddress = adapter.GetPhysicalAddress().ToString();
         }
       // sMacAddress = sMacAddress.Replace(":", "");
       }  return sMacAddress;
    }
    public void Bind()
    {
       // string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            try
            {
                Griddoctor.DataSource = null;
                Griddoctor.DataBind();
                tbldoctor.Rows.Clear();
                SqlConnection con = new SqlConnection(strconn11);
                SqlCommand cmd = new SqlCommand("select * from tblDoctor order by D_name", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tbldoctor.Columns.Add(col);
                    tbldoctor.Columns.Add("DOCTOR NAME");
                    tbldoctor.Columns.Add("DOCTOR SPECIFICATION");

                    Session["Doctor"] = tbldoctor;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbldoctor = (DataTable)Session["Doctor"];
                        drrw = tbldoctor.NewRow();

                        drrw["DOCTOR NAME"] = ds.Tables[0].Rows[i]["D_name"].ToString();
                        drrw["DOCTOR SPECIFICATION"] = ds.Tables[0].Rows[i]["D_spec"].ToString();

                        tbldoctor.Rows.Add(drrw);
                        //Griddoctor.DataSource = tbldoctor;
                        //Griddoctor.DataBind();
                    }
                    DataView dws = tbldoctor.DefaultView;
                    dws.Sort = "SLNO ASC";                  
                    Griddoctor.DataSource = tbldoctor;
                    Griddoctor.DataBind();
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
                Griddoctor.DataSource = null;
                Griddoctor.DataBind();
                tbldoctor.Rows.Clear();
                OleDbConnection conn10 = new OleDbConnection(strconn11);
                conn10.Open();
                OleDbCommand cmd1 = new OleDbCommand("select * from tblDoctor order by D_name", conn10);

                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tbldoctor.Columns.Add(col);
                    tbldoctor.Columns.Add("DOCTOR NAME");
                    tbldoctor.Columns.Add("DOCTOR SPECIFICATION");

                    Session["Doctor"] = tbldoctor;

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        tbldoctor = (DataTable)Session["Doctor"];
                        drrw = tbldoctor.NewRow();

                        drrw["DOCTOR NAME"] = ds1.Tables[0].Rows[i]["D_name"].ToString();
                        drrw["DOCTOR SPECIFICATION"] = ds1.Tables[0].Rows[i]["D_spec"].ToString();

                        tbldoctor.Rows.Add(drrw);
                        //Griddoctor.DataSource = tbldoctor;
                        //Griddoctor.DataBind();
                    }
                    DataView dws = tbldoctor.DefaultView;
                    dws.Sort = "SLNO ASC";  
                    Griddoctor.DataSource = tbldoctor;
                    Griddoctor.DataBind();
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
    protected void Griddoctor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Griddoctor.PageIndex = e.NewPageIndex;
        Bind();

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {

        
        if (button_select == "Modify")
        {
            //GetHDDSerialNo();
            string docname = "Dr." + txtdname.Text;
            string docspec = txtdspec.Text;
            if (docspec == "")
            {
                Master.ShowModal("Doc specficatiopn is empty", "txtdspec", 0);
                //txtdspec.Focus();
                return;
            }
            try
            {
                string cod = lblcode.Text;
                int c = Convert.ToInt32(cod);
                //string filename = Dbconn.Mymenthod();
                if (!File.Exists(filename))
                {
                    try
                    {
                    ClsBLGP.updateDoctor("UPDATE_DOCTOR", docname, docspec, c);
                    }
                  catch(Exception ex)
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
                    OleDbConnection conn10 = new OleDbConnection(strconn11);
                    conn10.Open();

                    OleDbCommand cmd1 = new OleDbCommand("update tblDoctor set D_spec='" + docspec + "' where D_code=" + c + "", conn10);
                    cmd1.ExecuteNonQuery();
                    conn10.Close();
                    }
                  catch(Exception ex)
                  {
                   string asd = ex.Message;
                   lblerror.Enabled = true;
                   lblerror.Text = asd;
                  }
                }
                lblsuccess.Visible = true;
                lblsuccess.Text = "Modified Successfully";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                Bind();
                button_select = string.Empty;
                txtdname.Enabled = true;
                txtdname.Text =string.Empty;
                txtdspec.Text = string.Empty;
                txtdname.Enabled = true;
                txtdname.Focus();

            }
            catch(Exception ex)
            {
                string asd = ex.Message;
                lblerror.Enabled = true;
                lblerror.Text = asd;
            }
        }
        else if (button_select != "Modify")
        {
            string filename = Dbconn.Mymenthod();
            try
            {

                string Sysdatetime=DateTime.Now.ToString();
                //GetHDDSerialNo();
                string dname ="Dr." + txtdname.Text;
                string dspec = txtdspec.Text;
                string gname = txtdname.Text.TrimStart();
                string g1name = txtdspec.Text.TrimStart();
                string strCaps1 = Regex.Replace(gname, "[^a-zA-Z + \\s]", "");
                string strCaps2 = Regex.Replace(g1name, "[^a-zA-Z + \\s]", "");
                string strEdited = Regex.Replace(strCaps1, @"\s+", " ");
                string strEdited2 = Regex.Replace(strCaps2, @"\s+", " ");
                if (strEdited == "")
                {
                    Master.ShowModal("Doctor  Name mandatory", "txtdname", 0);
                    txtdname.Focus();
                    return;
                }
                if (dspec == "")
                {
                    Master.ShowModal("Doctor  Specification mandatory", "txtdspec", 0);
                    txtdspec.Enabled = true;
                    txtdspec.Focus();
                    return;
                }
                DataSet dsdoc = ClsBLGD.GetcondDataSet("*", "tblDoctor", "D_name", dname);
                    if (!File.Exists(filename))
                    {
                        try
                        {
                        ClsBLGP.Doctor("INSERT_DOCTOR", dname, dspec,Session["username"].ToString(), sMacAddress,Sysdatetime);
                        }
                         catch(Exception ex)
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
                       // String strconn11 = Dbconn.conmenthod();
                        OleDbConnection con = new OleDbConnection(strconn11);
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("insert into tblDoctor(D_name,D_spec,LoginName,Mac_id,Sysdatetime) values('" + dname + "','" + dspec + "','" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        }
                          catch(Exception ex)
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
                     //txtdname.Text ="Dr.";
                    txtdspec.Text = string.Empty;
                    txtdname.Text=string.Empty;
                    txtdname.Enabled = true;
                    txtdname.Focus();

                }
            
            catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Visible = true;
                lblerror.Text = asd;
            }
        }

    }
    protected void btnexit_Click(object sender, EventArgs e)
    {
        button_select = string.Empty;
        Response.Redirect("Home.aspx");
    }


   [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Buyername(string prefixText)
    {
        string filename = Dbconn.Mymenthod();
         prefixText = "Dr." + prefixText;
        if (!File.Exists(filename))
        {
        //string oConn = ConfigurationManager.AppSettings["ConnectionString"];
        SqlConnection conn = new SqlConnection(strconn11);
        conn.Open();
        SqlCommand cmd = new SqlCommand("select D_name from tblDoctor where D_name like @1+'%'", conn);
        cmd.Parameters.AddWithValue("@1", prefixText);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        List<string> buyernames = new List<string>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            buyernames.Add(dt.Rows[i][0].ToString());
        }
        
        return buyernames;
        }
        else
        {
            // string strconn1 = Dbconn.conmenthod();
            //string strconn1 = Dbconn.conmenthod();
            OleDbConnection conn=new OleDbConnection(strconn11);
            conn.Open();
            OleDbCommand cmd=new OleDbCommand("select D_name from tblDoctor where D_name like @1+'%'", conn);
            cmd.Parameters.AddWithValue("@1", prefixText);
            OleDbDataAdapter oda=new OleDbDataAdapter(cmd);
            DataTable dt=new DataTable ();
            oda.Fill(dt);
            List<string>buyernames=new List<string> ();
            for(int i=0;i<dt.Rows.Count;i++)
            {
                buyernames.Add(dt.Rows[i][0].ToString());
            }
            return buyernames;
        }
    }



    protected void btnmodify_Click(object sender, EventArgs e)
    {
        string dname1 = txtdname.Text;
        string dspec1 = txtdspec.Text;
        try{
           DataSet dsgrp1 = ClsBLGD.GetcondDataSet("*", "tblDoctor", "D_name", dname1);
           lblmod.Text = "Doctor with below details already exists";
           int code = Convert.ToInt32(dsgrp1.Tables[0].Rows[0]["D_code"].ToString());
           lblcode.Text = Convert.ToString(code);
           Table2.Visible = true;
           ClsBLGD.GetcondDataSet("*", "tblDoctor", "D_name", dname1);
       // txtdname.Text = dsgrp1.Tables[0].Rows[0]["D_name"].ToString();
           txtdspec.Text = dsgrp1.Tables[0].Rows[0]["D_spec"].ToString();
           mod();
           Table2.Visible = false;
           txtdname.Enabled = false;
           txtdspec.Enabled = true;
        }
        catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Visible = true;
                lblerror.Text = asd;
            }

    }
    public string mod()
    {
        button_select = dbcon.modify();

        if (button_select == "Modify")
        {
            txtdname.Enabled = true;
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
        txtdname.Enabled = true;
        txtdspec.Enabled = true;
        txtdname.Text = string.Empty;
        txtdspec.Text = string.Empty;
        txtdname.Enabled = true;
        txtdname.Focus();
    }

    protected void txtdname_TextChanged(object sender, EventArgs e)
    {
        string name1 = txtdname.Text;
        try
        {
            DataSet dsgrp = ClsBLGD.GetcondDataSet("*", "tblDoctor", "D_name", name1);
            if (dsgrp.Tables[0].Rows.Count > 0)
            {
                lblmod.Text = "Doctor with below name already exists.Click Modify to edit details";
                int code = Convert.ToInt32(dsgrp.Tables[0].Rows[0]["D_code"].ToString());
                txtdspec.Text = dsgrp.Tables[0].Rows[0]["D_spec"].ToString();
                lblcode.Text = Convert.ToString(code);
                Table2.Visible = true;
                txtdspec.Enabled = false;
                btn.Enabled = true;
                btn.Focus();
                //Griddoctor.DataSource = null;
                //Bind();
                return;
            }
            else
            {
                txtdspec.Enabled = true;
                txtdspec.Focus();
            }
        }
        catch(Exception ex)
        {
            string asd = ex.Message;
            lblerror.Enabled = true;
            lblerror.Text = asd;
        }

    }

      
   
}