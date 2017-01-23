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

public partial class Chemical : System.Web.UI.Page
{
    ClsBALChemical ClsBLGP = new ClsBALChemical();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string button_select;
    DataTable tblChemical = new DataTable();
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
            txtchem.Focus();
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
        ClientScript.RegisterStartupScript(this.GetType(), "SetInitialFocus", "<script>document.getElementById('" + txtchem.ClientID + "').focus();</script>");
        btnExit.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtchem.ClientID + "').focus();return false;}} else {return true}; ");
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
     }  return sMacAddress;
   }
  

    public void Bind()
    {
        //gridbinding();
       // string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            try
            {
                Gridchemical.DataSource = null;
                Gridchemical.DataBind();
                tblChemical.Rows.Clear();
                SqlConnection con = new SqlConnection(strconn11);
                SqlCommand cmd = new SqlCommand("select * from tblChemical order by CC_name", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblChemical.Columns.Add(col);
                    tblChemical.Columns.Add("CHEMICAL NAME");

                    Session["Chemical"] = tblChemical;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tblChemical = (DataTable)Session["Chemical"];
                        drrw = tblChemical.NewRow();

                        drrw["CHEMICAL NAME"] = ds.Tables[0].Rows[i]["CC_name"].ToString();

                        tblChemical.Rows.Add(drrw);
                        //Gridchemical.DataSource = tblChemical;
                        //Gridchemical.DataBind();


                    }
                    DataView dw = tblChemical.DefaultView;

                    dw.Sort = "SLNO ASC";
                    Gridchemical.DataSource = tblChemical;
                    Gridchemical.DataBind();
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
                Gridchemical.DataSource = null;
                Gridchemical.DataBind();
                tblChemical.Rows.Clear();
                OleDbConnection conn10 = new OleDbConnection(strconn11);
                conn10.Open();
                OleDbCommand cmd1 = new OleDbCommand("select * from tblChemical order by CC_name ", conn10);

                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblChemical.Columns.Add(col);
                    tblChemical.Columns.Add("CHEMICAL NAME");

                    Session["Chemical"] = tblChemical;

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        tblChemical = (DataTable)Session["Chemical"];
                        drrw = tblChemical.NewRow();

                        drrw["CHEMICAL NAME"] = ds1.Tables[0].Rows[i]["CC_name"].ToString();

                        tblChemical.Rows.Add(drrw);
                        //Gridchemical.DataSource = tblChemical;
                        //Gridchemical.DataBind();
                    }
                    DataView dw = tblChemical.DefaultView;

                    dw.Sort = "SLNO ASC";
                    Gridchemical.DataSource = tblChemical;
                    Gridchemical.DataBind();
                    //DataView dws = tblChemical.DefaultView;
                    //dws.Sort = "slno ASC";
                    //Gridchemical.DataSource = tblChemical;
                    //Gridchemical.DataBind();
                }
            }
            catch (Exception e)
            {
                string asd = e.Message;
                lblerror.Enabled = true;
                lblerror.Text = asd;
            }
        }
    }
   
    protected void Gridchemical_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      
            Gridchemical.PageIndex = e.NewPageIndex;
            Bind();
            
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        
        if (button_select == "Modify")
        {
            try
            {

                string Sysdatetime=DateTime.Now.ToString();
                string gennam = txtchem.Text;
                string cod = lblcode.Text;
                int c = Convert.ToInt32(cod);
                if (!File.Exists(filename))
                {
                    try
                    {
                       ClsBLGP.updateChemical("UPDATE_CHEMICAL", gennam, c);
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
                    OleDbConnection conn10 = new OleDbConnection(Dbconn.conmenthod());
                    conn10.Open();
                    OleDbCommand cmd1 = new OleDbCommand("update tblChemical set CC_name='" + gennam + "' where CC_code=" + c + "", conn10);
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
            txtchem.Text = string.Empty;
            txtchem.Enabled = true;
            txtchem.Focus();
               
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

                string chemname = txtchem.Text.TrimStart();
                string Sysdatetime=DateTime.Now.ToString();
                string strCaps1 = Regex.Replace(chemname, "[^a-zA-Z + \\s]", "");
                string strEdited = Regex.Replace(strCaps1, @"\s+", " ");

                if (strEdited == "")
                {
                    Master.ShowModal("Chemical name Mandatory", "txtchem", 0);
                    return;

                }

                DataSet dschm = ClsBLGD.GetcondDataSet9("*", "tblChemical", "CC_name", chemname);
                if (dschm.Tables[0].Rows.Count > 0)
                {
                 try
                  {

                    lblmod.Text = "Chemical Name Already Exists";
                    int code = Convert.ToInt32(dschm.Tables[0].Rows[0]["CC_code"].ToString());
                    lblcode.Text = Convert.ToString(code);
                    Table2.Visible = true;
                    btn.Enabled = true;
                    btn.Focus();
                   // Gridchemical.DataSource = null;
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
                
                    if (!File.Exists(filename))
                    {
                       try
                       {
                           ClsBLGP.Chemical("INSERT_CHEMICAL", chemname,Session["username"].ToString(),sMacAddress,Sysdatetime);
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
                        OleDbCommand cmd = new OleDbCommand("insert into tblChemical(CC_name,LoginName,Mac_id,Sysdatetime) values('" + chemname + "','" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con);
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
                    txtchem.Text = string.Empty;
                    txtchem.Enabled = true;
                    txtchem.Focus();
                    
                  //Gridchemical.Columns.Clear();
                }    //else
            }
           catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Visible = true;
                lblerror.Text = asd;
            }
            
        }

    }
    
    protected void btnExit_Click(object sender, EventArgs e)
    {
        txtchem.Enabled = true;
        txtchem.Focus();
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
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "select CC_name from tblChemical where CC_name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["CC_name"]));
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
                    cmd.CommandText = "select CC_name from tblChemical where CC_name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (OleDbDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["CC_name"]));
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
        txtchem.Enabled = true;
        txtchem.Focus();
    }
    public string mod()
    {
        button_select = dbcon.modify();

        if (button_select == "Modify")
        {
            txtchem.Enabled = true;
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
        txtchem.Text = "";
        txtchem.Enabled = true;
        txtchem.Focus();
    }

    protected void txtchem_TextChanged(object sender, EventArgs e)
    {

        btnsave.Enabled = true;
    }
   
}