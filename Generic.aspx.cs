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

public partial class Generic : System.Web.UI.Page
{
    ClsBALGeneric ClsBLGP = new ClsBALGeneric();
    //ClsBALGroup ClsBLGP = new ClsBALGroup();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string button_select;
    DataTable tblGeneric = new DataTable();
    DataTable dt = new DataTable();
    DataRow drrw;
    protected  static string filename = Dbconn.Mymenthod();
    protected  static string strconn11 = Dbconn.conmenthod();
     string sMacAddress = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Visible = false;
        lblsuccess.Visible = false;
        if (!IsPostBack)
        {
            txtgenename.Focus();
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
        ClientScript.RegisterStartupScript(this.GetType(), "SetInitialFocus", "<script>document.getElementById('" + txtgenename.ClientID + "').focus();</script>");
        btnexit.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtgenename.ClientID + "').focus();return false;}} else {return true}; ");       
       
    }
    public void Bind()
    {
      //  string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            try
            {
                Gridgeneric.DataSource = null;
                Gridgeneric.DataBind();
                tblGeneric.Rows.Clear();
              //  SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
                 SqlConnection con = new SqlConnection(strconn11);
                SqlCommand cmd = new SqlCommand("select * from tblGeneric order by GN_name", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblGeneric.Columns.Add(col);
                    tblGeneric.Columns.Add("GENERIC NAME");

                    Session["Generic"] = tblGeneric;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tblGeneric = (DataTable)Session["Generic"];
                        drrw = tblGeneric.NewRow();

                        drrw["GENERIC NAME"] = ds.Tables[0].Rows[i]["GN_name"].ToString();

                        tblGeneric.Rows.Add(drrw);
                        //Gridgeneric.DataSource = tblGeneric;
                        //Gridgeneric.DataBind();
                    }
                    DataView dw = tblGeneric.DefaultView;
                    dw.Sort = "SLNO ASC";
                   // dw.Sort = "Generic name ASC";
                    Gridgeneric.DataSource = tblGeneric;
                    Gridgeneric.DataBind();
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
                Gridgeneric.DataSource = null;
                Gridgeneric.DataBind();
                tblGeneric.Rows.Clear();
                OleDbConnection conn10 = new OleDbConnection(strconn11);
                conn10.Open();
                OleDbCommand cmd1 = new OleDbCommand("select * from tblGeneric order by GN_name", conn10);

                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblGeneric.Columns.Add(col);
                    tblGeneric.Columns.Add("GENERIC NAME");

                    Session["Generic"] = tblGeneric;

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        tblGeneric = (DataTable)Session["Generic"];
                        drrw = tblGeneric.NewRow();

                        drrw["GENERIC NAME"] = ds1.Tables[0].Rows[i]["GN_name"].ToString();

                        tblGeneric.Rows.Add(drrw);
                        //Gridgeneric.DataSource = tblGeneric;
                        //Gridgeneric.DataBind();
                    }
                    DataView dw = tblGeneric.DefaultView;
                    dw.Sort = "SLNO ASC";
                    // dw.Sort = "Generic name ASC";
                    Gridgeneric.DataSource = tblGeneric;
                    Gridgeneric.DataBind();
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
    protected void Gridgeneric_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Gridgeneric.PageIndex = e.NewPageIndex;
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
       
        if (button_select == "Modify")
        {
            
            try
            {
                
                string Sysdatetime=DateTime.Now.ToString();
                string gennam = txtgenename.Text;
                string cod = lblcode.Text;
                int c = Convert.ToInt32(cod);
                if (!File.Exists(filename))
                {
                  try
                  {
                    ClsBLGP.updateGeneric("UPDATE_GENERIC", gennam, c);
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

                    OleDbCommand cmd1 = new OleDbCommand("update tblGeneric set GN_name='" + gennam + "' where GN_code=" + c + "", conn10);
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
              txtgenename.Text = "";
              txtgenename.Enabled = true;
              txtgenename.Focus();
              Gridgeneric.DataSource = null;
              Gridgeneric.Columns.Clear(); 
          }
          catch (Exception ex)
          {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
         }
       }
        else if(button_select !="Modify")
        {
        try
        {
            //GetMACAddress();
            string Sysdatetime=DateTime.Now.ToString();
            string genename = txtgenename.Text.TrimStart(); ;
            string strCaps1 = Regex.Replace(genename, "[^a-zA-Z + \\s]", "");
            string strEdited = Regex.Replace(strCaps1, @"\s+", " ");
            if (strEdited == "")
            {
                Master.ShowModal("Generic Name is mandatory","txtgenename",0);
                return;
            }
                

                DataSet dsgene = ClsBLGD.GetcondDataSet("*", "tblGeneric", "GN_name", genename);
                if (dsgene.Tables[0].Rows.Count > 0)
                {
                    try
                   {
                     lblmod.Text = "Generic Name Already Exists";
                     int code = Convert.ToInt32(dsgene.Tables[0].Rows[0]["GN_code"].ToString());
                     lblcode.Text = Convert.ToString(code);
                     Table2.Visible = true;
                     btn.Enabled = true;
                     btn.Focus();
                     Gridgeneric.DataSource = null;
                   // Bind();
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
                   
                   // string filename = Dbconn.Mymenthod();
                    if (!File.Exists(filename))
                    {
                        try
                        {
                        ClsBLGP.Generic("INSERT_GENERIC", genename,Session["username"].ToString(),sMacAddress,Sysdatetime);
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
                        //String strconn11 = Dbconn.conmenthod();
                        OleDbConnection con = new OleDbConnection(strconn11);
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("insert into tblGeneric(GN_name,LoginName,Mac_id,Sysdatetime) values('" + genename + "','" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con);
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
                    //Microsoft.VisualBasic.Interaction.MsgBox("Record inserted successfully");
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    Bind();               
                    txtgenename.Text = string.Empty;
                    txtgenename.Enabled = true;
                    txtgenename.Focus();
                    txtgenename.Text = string.Empty;

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

                    cmd.CommandText = "select GN_name from tblGeneric where GN_name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["GN_name"]));
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
            string strconn1 = Dbconn.conmenthod();
            using (OleDbConnection conn = new OleDbConnection(strconn11))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {

                    cmd.CommandText = "select GN_name from tblGeneric where GN_name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (OleDbDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["GN_name"]));
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
        txtgenename.Enabled = true;
        txtgenename.Focus();
    }
    public string mod()
    {
        button_select = dbcon.modify();

        if (button_select == "Modify")
        {
            txtgenename.Enabled = true;
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
        txtgenename.Text = string.Empty;
        txtgenename.Enabled = true;
        txtgenename.Focus();
    }

    protected void txtgenename_TextChanged(object sender, EventArgs e)
    {
        btnsave.Enabled = true;
    }
}