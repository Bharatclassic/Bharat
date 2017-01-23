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

public partial class Medicine : System.Web.UI.Page
{
    ClsBALMedicine ClsBLGP = new ClsBALMedicine();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string button_select;
    protected static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
    DataTable tblMedicine = new DataTable();
    DataTable dt = new DataTable();
    DataRow drrw;
    String sMacAddress = "";
   //  string result = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Visible = false;
        lblsuccess.Visible = false;
        if (!IsPostBack)
        {
            txtfamily.Focus();
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
        ClientScript.RegisterStartupScript(this.GetType(), "SetInitialFocus", "<script>document.getElementById('" + txtfamily.ClientID + "').focus();</script>");
        btnexit.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtfamily.ClientID + "').focus();return false;}} else {return true}; ");       
    }
    public void Bind()
    {
       // string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            try
            {
                Gridmedicine.DataSource = null;
                Gridmedicine.DataBind();
                tblMedicine.Rows.Clear();
                SqlConnection con = new SqlConnection(strconn11);
                SqlCommand cmd = new SqlCommand("select * from tblMedicine order by FA_name", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblMedicine.Columns.Add(col);
                    tblMedicine.Columns.Add("MEDICINE NAME");

                    Session["Medicine"] = tblMedicine;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tblMedicine = (DataTable)Session["Medicine"];
                        drrw = tblMedicine.NewRow();

                        drrw["MEDICINE NAME"] = ds.Tables[0].Rows[i]["FA_name"].ToString();

                        tblMedicine.Rows.Add(drrw);
                        //Gridmedicine.DataSource = tblMedicine;
                        //Gridmedicine.DataBind();
                    }
                    DataView dw = tblMedicine.DefaultView;
                    dw.Sort = "SLNO ASC";
                    //dw.Sort = "Medicine Name ASC";
                    Gridmedicine.DataSource = tblMedicine;
                    Gridmedicine.DataBind();
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
                Gridmedicine.DataSource = null;
                Gridmedicine.DataBind();
                tblMedicine.Rows.Clear();
                OleDbConnection conn10 = new OleDbConnection(strconn11);
                conn10.Open();
                OleDbCommand cmd1 = new OleDbCommand("select * from tblMedicine order by FA_name", conn10);

                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblMedicine.Columns.Add(col);
                    tblMedicine.Columns.Add("MEDICINE NAME");

                    Session["Medicine"] = tblMedicine;

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        tblMedicine = (DataTable)Session["Medicine"];
                        drrw = tblMedicine.NewRow();

                        drrw["MEDICINE NAME"] = ds1.Tables[0].Rows[i]["FA_name"].ToString();
                        tblMedicine.Rows.Add(drrw);
                        //Gridmedicine.DataSource = tblMedicine;
                        //Gridmedicine.DataBind();
                    }
                    DataView dw = tblMedicine.DefaultView;
                    dw.Sort = "SLNO ASC";
                   // dw.Sort = "Medicine Name ASC";
                    Gridmedicine.DataSource = tblMedicine;
                    Gridmedicine.DataBind();
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
    protected void Gridmedicine_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Gridmedicine.PageIndex = e.NewPageIndex;
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
    protected void btnsave_Click(object sender, EventArgs e)
    {
       // Gridmedicine.DataSource = null;
        //Bind();
        if (button_select == "Modify")
        {
            try
            {
             
                string Sysdatetime=DateTime.Now.ToString();
                string gennam = txtfamily.Text;
                string cod = lblcode.Text;
                int c = Convert.ToInt32(cod);
                if (!File.Exists(filename))
                {
                    try
                    {
                    ClsBLGP.updateMedicine("UPDATE_MEDICINE", gennam, c);
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

                    OleDbCommand cmd1 = new OleDbCommand("update tblMedicine set FA_name='" + gennam + "' where FA_code=" + c + "", conn10);
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
                txtfamily.Text = string.Empty;
                txtfamily.Enabled = true;
                txtfamily.Focus();
               // Response.Redirect("Generic.aspx");
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
                string gname = txtfamily.Text.TrimStart();
                string medname = txtfamily.Text;
                string strCaps1 = Regex.Replace(gname, "[^a-zA-Z + \\s]", "");
                string strEdited = Regex.Replace(strCaps1, @"\s+", " ");
                if (strEdited == "")
                {

                    Master.ShowModal("Medicine Name mandotory", "txtformunitmaster", 0);
                    return;

                }
                DataSet dsmed = ClsBLGD.GetcondDataSet("*", "tblMedicine", "FA_name", medname);
                if (dsmed.Tables[0].Rows.Count > 0)
                {
                    lblmod.Text = "Medicine Name already exists";
                    //Master.ShowModal("Medicine Name already exists", "txtfamily", 1);
                    int code = Convert.ToInt32(dsmed.Tables[0].Rows[0]["FA_code"].ToString());
                    lblcode.Text = Convert.ToString(code);
                    Table2.Visible = true;
                    btn.Enabled = true;
                    btn.Focus();
                   //  Gridmedicine.DataSource = null;
                   // Bind();
                    return;
                }
                else
                {
                    
                   // string filename = Dbconn.Mymenthod();
                    if (!File.Exists(filename))
                    {
                      try
                      {
                        ClsBLGP.Medicine("INSERT_MEDICINE", medname,Session["username"].ToString(),sMacAddress,Sysdatetime);
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
                        try
                        {
                        OleDbConnection con = new OleDbConnection(strconn11);
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("insert into tblMedicine(FA_name,LoginName,Mac_id,Sysdatetime) values('" + medname + "','" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con);
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
                    txtfamily.Text = string.Empty;
                    txtfamily.Enabled = true;
                    txtfamily.Focus();
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
                // conn.ConnectionString = @"Data Source=VAGI-7-PC;Initial Catalog=Pharmacy;Integrated Security=False;User ID=sa;Password=vagi0903"; 
                //conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select FA_name from tblMedicine where FA_name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["FA_name"]));
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

                    cmd.CommandText = "select FA_name from tblMedicine where FA_name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (OleDbDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["FA_name"]));
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
        txtfamily.Focus();
    }
    public string mod()
    {
        button_select = dbcon.modify();

        if (button_select == "Modify")
        {
            txtfamily.Enabled = true;
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
        txtfamily.Text = string.Empty;
        txtfamily.Enabled = true;
        txtfamily.Focus();
    }
}