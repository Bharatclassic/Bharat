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

public partial class ModifyCustomerAccount : System.Web.UI.Page
{
    ClsBALCustomermaster ClsBLGP = new ClsBALCustomermaster();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string strconn11 = Dbconn.conmenthod();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Visible = false;
        lblsuccess.Visible = false;
        lblcode.Visible = false;
        txtcustid.Focus();
    }

    protected void txtcustid_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string cid = txtcustid.Text;
            DataSet ds1 = ClsBLGD.GetcondDataSet("*", "tblCustomer", "CA_code", cid);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                txtcustname.Text = ds1.Tables[0].Rows[0]["CA_name"].ToString();
                txtclimit.Text = ds1.Tables[0].Rows[0]["Credit_limit"].ToString();
                txtemail.Text = ds1.Tables[0].Rows[0]["Email"].ToString();
                txtmobile.Text = ds1.Tables[0].Rows[0]["Mobileno"].ToString();
            }
            else
            {
                Master.ShowModal("Customed ID Doesn't Exists", "txtcustid", 0);
                return;
            }

        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
        btnsave.Focus();
        txtcustid.Enabled = false;
        txtcustname.Enabled = false;
    }
    protected void txtcustname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string cname = txtcustname.Text;
            DataSet ds2 = ClsBLGD.GetcondDataSet("*", "tblCustomer", "CA_name", cname);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                txtcustid.Text = ds2.Tables[0].Rows[0]["CA_code"].ToString();
                txtclimit.Text = ds2.Tables[0].Rows[0]["Credit_limit"].ToString();
                txtemail.Text = ds2.Tables[0].Rows[0]["Email"].ToString();
                txtmobile.Text = ds2.Tables[0].Rows[0]["Mobileno"].ToString();
            }
            else
            {
                Master.ShowModal("Customed ID Doesn't Exists", "txtcustid", 1);
                return;
            }

        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
        btnsave.Focus();
        txtcustid.Enabled = false;
        txtcustname.Enabled = false;
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(strconn11);
        con1.Open();
        SqlCommand cmd = new SqlCommand("UPDATE tblCustomer SET Credit_limit='" + txtclimit.Text + "',Email='" + txtemail.Text + "', Mobileno='" + txtmobile.Text + "' where CA_code = '" + txtcustid.Text + "' and CA_name = '" + txtcustname.Text + "'", con1);
        cmd.ExecuteNonQuery();
        lblsuccess.Visible = true;
        lblsuccess.Text = "Updated Successfully";
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        txtcustid.Text = string.Empty;
        txtcustname.Text = string.Empty;
        txtclimit.Text = string.Empty;
        txtemail.Text = string.Empty;
        txtmobile.Text = string.Empty;
    }
    protected void btnexit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
        
    }
}