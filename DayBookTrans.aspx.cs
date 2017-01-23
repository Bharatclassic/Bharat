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
using System.Globalization;
using System.Net.NetworkInformation;
using System.Management;
//using iTextSharp.text;
//using iTextSharp;
using CreatePDF;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.Mail;
using iTextSharp.text.pdf.parser;
using AllHospitalNames;
using System.Web.UI.Design;

public partial class DayBookTrans : System.Web.UI.Page
{
    string sMacAddress = "";
    Dbconn dbcon = new Dbconn();
    protected static string strconn1 = Dbconn.conmenthod();
    ClsBALTransaction clstr=new ClsBALTransaction();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    PharmacyName Hosp = new PharmacyName();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (rdAdj.Checked == false)
        {
            txtCheque.Visible = false;
        }
        if (!IsPostBack)
        {
            System.DateTime Dtnow = DateTime.Now;
            string Sysdatetime = Dtnow.ToString("dd/MM/yyyy");
            txtTrDate.Text = Sysdatetime;
            mainhead();
            ddlMain.Focus();
            lblCheque.Visible = false;
            txtCheque.Visible = false;
            lblRec.Visible = false;
            txtRec.Visible = false;
        }
        lblerror.Visible = false;
        lblsuccess.Visible = false;
    }
    protected void btnexit_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }
    public void mainhead()
    {
        try
        {
            SqlConnection con = new SqlConnection(strconn1);
            using (con)
            {
                using (SqlCommand cmd = new SqlCommand("select Mainhead from tblVoachermaster WHERE Headercode NOT IN ('9999','9994','9997','9993','9998','9996','9995','9992','9985','9984','9983','9982','9986') group by MainHead"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    ddlMain.DataSource = cmd.ExecuteReader();
                    ddlMain.DataTextField = "Mainhead";
                    ddlMain.DataBind();
                    con.Close();
                }
            }
            ddlMain.Items.Insert(0, new ListItem("--Select mainhead--", "0"));
        }
        catch (Exception e)
        {
            lblerror.Visible = true;
            string msg = e.Message;
            lblerror.Text = msg;
        }
    }
    protected void ddlMain_SelectedIndexChanged(object sender, EventArgs e)
    {
        string mainhead = ddlMain.SelectedItem.ToString();
        subhead(mainhead);
    }
    public void subhead(string input)
    {
        try
        {
            SqlConnection con = new SqlConnection(strconn1);
            using (con)
            {
                using (SqlCommand cmd = new SqlCommand("select * from tblVoachermaster where MainHead='" + input + "' and Mainhead != Subhead"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    ddlSubHead.DataSource = cmd.ExecuteReader();
                    ddlSubHead.DataTextField = "SubHead";
                    ddlSubHead.DataBind();
                    con.Close();
                }

            }
            ddlSubHead.Items.Insert(0, new ListItem("--Select subhead--", "0"));
            ddlSubHead.Focus();
        }
        catch (Exception ex)
        {
            lblerror.Visible = true;
            string msg = ex.Message;
            lblerror.Text = msg;
        }
    }
    protected void rdCred_CheckedChanged(object sender, EventArgs e)
    {
        lblRec.Visible = true;
        txtRec.Visible = true;
        if (rdCred.Checked == true)
        {
            rdDeb.Checked = false;
            lblRec.Text = "Receipt No.";
        }
        rdDeb.Focus();
    }
    protected void rdDeb_CheckedChanged(object sender, EventArgs e)
    {
        lblRec.Visible = true;
        txtRec.Visible = true;
        if (rdDeb.Checked == true)
        {
            rdCred.Checked = false;
            lblRec.Text = "Voucher No.";
        }
        rdCash.Focus();
    }
    protected void rdAdj_CheckedChanged(object sender, EventArgs e)
    {
        if (rdAdj.Checked == true)
        {
            rdCash.Checked = false;
            lblCheque.Visible = true;
            txtCheque.Visible = true;
        }
        txtRec.Focus();
    }
    protected void txtRec_TextChanged(object sender, EventArgs e)
    {
        if (txtCheque.Visible == true)
        {
            txtCheque.Focus();
        }
        else 
        {
            txtAmt.Focus();
        }
    }
    protected void rdCash_CheckedChanged(object sender, EventArgs e)
    {
        if (rdCash.Checked == true)
        {
            rdAdj.Checked = false;
            txtCheque.Visible = false;
        }
        rdAdj.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlMain.SelectedItem.Text == "--Select mainhead--")
            {
                Master.ShowModal("Please select a mainhead", "ddlMain", 0);
                ddlMain.Focus();
                return;
            }
            if (ddlSubHead.SelectedItem.Text == "--Select subhead--")
            {
                Master.ShowModal("Please select a Subhead", "ddlSubHead", 0);
                ddlSubHead.Focus();
                return;
            }
            if (rdCred.Checked == false && rdDeb.Checked == false)
            {
                Master.ShowModal("Please check either credit or debit", "rdCred", 0);
                rdCred.Focus();
                return;
            }
            if (rdCash.Checked == false && rdAdj.Checked == false)
            {
                Master.ShowModal("Please check either cash or Adjustment", "rdCash", 0);
                rdAdj.Focus();
                return;
            }
            int amountenter = int.Parse(txtAmt.Text);
            if (txtAmt.Text == "" || amountenter<=0)
            {
                Master.ShowModal("Please enter valid amount in amount field", "txtAmt", 0);
                txtAmt.Focus();
                return;
            }
            if (txtRec.Text == "")
            {
                Master.ShowModal("Please enter valid Voucher/Receipt number", "txtRec", 0);
                txtRec.Focus();
                return;
            }
            
            System.DateTime Dtnow = DateTime.Now;
            string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
            string mac = GetMACAddress();
            string cheque = "0";
            string mainhead = ddlMain.SelectedItem.Text;
            string subhead = ddlSubHead.SelectedItem.Text;
            string head = gethead(subhead);
            string date = txtTrDate.Text;
            string vouch3 = txtRec.Text + "/DB";
            string vouch = ClsBLGD.base64Encode(vouch3); 
            string vouch1 = Regex.Match(txtRec.Text, @"\d+").Value;
            int vouch2=int.Parse(vouch1);
            if (vouch1.Length == txtRec.Text.Length && vouch2 == 0)
            {
                Master.ShowModal("Please enter valid Receipt number", "txtRec", 1);
                return;
            }
            string amt = txtAmt.Text;
            string transno = ClsBLGD.FetchMaximumTransNo("Select_Max_Transno");
            SqlConnection con = new SqlConnection(strconn1);
            if (txtCheque.Text != "")
            {
                cheque = txtCheque.Text;
            }
            if (rdCred.Checked == true && rdCash.Checked == true)
            {
                clstr.Transaction("INSERT_TRANSACTION",transno, date, "0000", "0000", head, "N", cheque, vouch, "0", "0", amt, "0", "0", "0", Session["username"].ToString(), sqlFormattedDate, mac);
            }
            else if (rdCred.Checked == true && rdAdj.Checked == true)
            {
                clstr.Transaction("INSERT_TRANSACTION",transno, date, "0000", "0000", head, "N", cheque, vouch, amt, "0", "0", "0", "0", "0", Session["username"].ToString(), sqlFormattedDate, mac);
            }
            else if (rdDeb.Checked == true && rdCash.Checked == true)
            {
                clstr.Transaction("INSERT_TRANSACTION",transno,date,"0000","0000",head,"N", cheque, vouch, "0", "0", "0", amt, "0", "0", Session["username"].ToString(), sqlFormattedDate, mac);
            }
            else
            {
                clstr.Transaction("INSERT_TRANSACTION", transno.ToString(), date, "0000", "0000", head, "N", cheque, vouch, "0", amt, "0", "0", "0", "0", Session["username"].ToString(), sqlFormattedDate, mac);
            }
            lblsuccess.Visible = true;
            lblsuccess.Text = "Inserted Successfully";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            Response.Redirect("DayBookTrans.aspx");
        }
        catch (Exception ex)
        {
            lblerror.Visible = true;
            string msg = ex.Message;
            lblerror.Text = msg;
        }
    }
    public int gettrans()
    {
        SqlConnection con = new SqlConnection(strconn1);
        string command = "select max(Trans_no) as transno,max(SNo) as Sno from tbltransaction";
        SqlCommand cmd = new SqlCommand(command,con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        string transno = ds.Tables[0].Rows[0]["transno"].ToString();
        string transnumber = Regex.Match(transno, @"\d+").Value;
        int transnumber1 = Convert.ToInt32(transnumber);
        transnumber1 = transnumber1 + 1;
        return transnumber1;
    }
    public string gethead(string input)
    {
        SqlConnection con = new SqlConnection(strconn1);
        SqlCommand cmd = new SqlCommand("select * from tblVoachermaster where Subhead='" + input + "' ",con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        string head = ds.Tables[0].Rows[0]["Headercode"].ToString();
        return head;
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
            //  sMacAddress = sMacAddress.Replace(":", "");
        } return sMacAddress;
    }
    protected void txtCheque_TextChanged(object sender, EventArgs e)
    {
        txtAmt.Focus();
    }
}