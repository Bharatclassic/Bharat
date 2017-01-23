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
public partial class Packsize : System.Web.UI.Page
{
    ClsBALPacksize ClsBLGP = new ClsBALPacksize();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string button_select;
    protected static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
    DataTable tblPacksize = new DataTable();
    DataTable dt = new DataTable();
    DataRow drrw;
    string sMacAddress="";
//    string result = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Visible = false;
        lblsuccess.Visible = false;
        if (!IsPostBack)
        {
            txtpackname.Focus();
            Table2.Visible = false;
            lblcode.Visible = false;
            btndelete.Enabled = false;
            //DataColumn col = new DataColumn("slno", typeof(int));
            //col.AutoIncrement = true;
            //col.AutoIncrementSeed = 1;
            //col.AutoIncrementStep = 1;
            ////tblPacksize.Columns.Add(col);
            //tblPacksize.Columns.Add("Packsize");

            //Session["Packsize"] = tblPacksize;
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
        ClientScript.RegisterStartupScript(this.GetType(), "SetInitialFocus", "<script>document.getElementById('" + txtpackname.ClientID + "').focus();</script>");
        btnexit.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtpackname.ClientID + "').focus();return false;}} else {return true}; ");       

    }
    public void Bind()
    {
        //string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            try
            {
                Gridpacksize.DataSource = null;
                Gridpacksize.DataBind();
                tblPacksize.Rows.Clear();
                SqlConnection con = new SqlConnection(strconn11);
                SqlCommand cmd = new SqlCommand("select * from tblPacksize order by Pack_name", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblPacksize.Columns.Add(col);
                    tblPacksize.Columns.Add("PACKSIZE");

                    Session["Packsize"] = tblPacksize;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tblPacksize = (DataTable)Session["Packsize"];
                        drrw = tblPacksize.NewRow();

                        drrw["PACKSIZE"] = ds.Tables[0].Rows[i]["Pack_name"].ToString();

                        tblPacksize.Rows.Add(drrw);
                        //Gridpacksize.DataSource = tblPacksize;
                        //Gridpacksize.DataBind();
                    }
                    DataView dw = tblPacksize.DefaultView;
                    dw.Sort = "SLNO ASC";
                   // dw.Sort = "Packsize ASC";
                    Gridpacksize.DataSource = tblPacksize;
                    Gridpacksize.DataBind();
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
                Gridpacksize.DataSource = null;
                Gridpacksize.DataBind();
                tblPacksize.Rows.Clear();
                OleDbConnection conn10 = new OleDbConnection(strconn11);
                conn10.Open();
                OleDbCommand cmd1 = new OleDbCommand("select * from tblPacksize order by Pack_name", conn10);

                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblPacksize.Columns.Add(col);
                    tblPacksize.Columns.Add("PACKSIZE");

                    Session["Packsize"] = tblPacksize;

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        tblPacksize = (DataTable)Session["Packsize"];
                        drrw = tblPacksize.NewRow();

                        drrw["PACKSIZE"] = ds1.Tables[0].Rows[i]["Pack_name"].ToString();

                        tblPacksize.Rows.Add(drrw);
                        //Gridpacksize.DataSource = tblPacksize;
                        //Gridpacksize.DataBind();
                    }
                    DataView dw = tblPacksize.DefaultView;
                    dw.Sort = "SLNO ASC";
                    // dw.Sort = "Packsize ASC";
                    Gridpacksize.DataSource = tblPacksize;
                    Gridpacksize.DataBind();
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
     }  return sMacAddress;
   }
    protected void Gridpacksize_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Gridpacksize.PageIndex = e.NewPageIndex;
        Bind();

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (button_select == "Modify")
        {
            try
            {
                
               string Sysdatetime=DateTime.Now.ToString();
                string gennam = txtpackname.Text;
                string cod = lblcode.Text;
                int c = Convert.ToInt32(cod);
                
                //ClsBLGD.UpdateRecords(""tblGeneric", "GN_name='" + gennam + "'", "GN_code='" + lblcode.Text + "'");
                if (!File.Exists(filename))
                {
                    try{
                    ClsBLGP.updatePacksize("UPDATE_PACKSIZE", gennam, c);
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
                    try{
                    OleDbConnection conn10 = new OleDbConnection(strconn11);
                    conn10.Open();

                    OleDbCommand cmd1 = new OleDbCommand("update tblPacksize set Pack_name='" + gennam + "' where Pack_code=" + c + "", conn10);
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
                //Response.Redirect("Generic.aspx");
                txtpackname.Text = string.Empty;
                txtpackname.Enabled = true;
                txtpackname.Focus();
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
             
                string Sysdatetime=DateTime.Now.ToString();
                string packname = txtpackname.Text.TrimStart();
                if (packname == "")
                {
                    Master.ShowModal("PackName is mandatory", "txtpackname", 0);
                    return;
                }
                
                DataSet dspack = ClsBLGD.GetcondDataSet("*", "tblPacksize", "Pack_name", packname);
                if (dspack.Tables[0].Rows.Count > 0)
                {
                    try{
                    lblmod.Text = "Pack Name Already Exists";
                    int code = Convert.ToInt32(dspack.Tables[0].Rows[0]["Pack_code"].ToString());
                    lblcode.Text = Convert.ToString(code);
                    Table2.Visible = true;
                    btn.Enabled = true;
                    btn.Focus();
                    //Gridpacksize.DataSource = null;
                  //  Bind();
                    // Master.ShowModal("Generic Name already exists", "txtgenename", 1);
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
                        ClsBLGP.Packsize("INSERT_PACKSIZE", packname, Session["username"].ToString(),sMacAddress,Sysdatetime);
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
                      //  String strconn11 = Dbconn.conmenthod();
                        try{
                        OleDbConnection con = new OleDbConnection(strconn11);
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("insert into tblPacksize(Pack_name,LoginName,Mac_id,Sysdatetime ) values('" + packname + "','" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con);
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
                    txtpackname.Text = string.Empty;
                    txtpackname.Enabled = true;
                    txtpackname.Focus();
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
    protected void btnexit_Click(object sender, EventArgs e)
    {
        button_select = string.Empty;
        Response.Redirect("Home.aspx");
    }
    [WebMethod]

    public static string[] GetCustomers(string prefix)
    {

        //string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection(strconn11))
            {
                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

              //  conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "select Pack_name from tblPacksize where Pack_name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["Pack_name"]));
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
            //string strconn1 = Dbconn.conmenthod();
            using (OleDbConnection conn = new OleDbConnection(strconn11))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {

                    cmd.CommandText = "select Pack_name from tblPacksize where Pack_name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (OleDbDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["Pack_name"]));
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
        txtpackname.Focus();
    }
    public string mod()
    {
        button_select = dbcon.modify();

        if (button_select == "Modify")
        {
            txtpackname.Enabled = true;
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
        txtpackname.Text = string.Empty;
        txtpackname.Enabled = true;
        txtpackname.Focus();
    }
}