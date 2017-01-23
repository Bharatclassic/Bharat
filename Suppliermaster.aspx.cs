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

public partial class Suppliermaster : System.Web.UI.Page
{
    ClsBALSuppliermaster ClsBLGP = new ClsBALSuppliermaster();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
     protected  static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
    Dbconn dbcon = new Dbconn();
    protected static string button_select;
    DataTable tblsuppliermaster = new DataTable();
    DataTable dt = new DataTable();
    DataRow drrw;
    string sMacAddress = "";
    string S_code;
    double count = 0;

    double S_code10 = 0;
       // string result = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        lblerror.Visible = false;
        lblsuccess.Visible = false;
        if (!IsPostBack)
        {
            txtphone.Attributes.Add("autocomplete", "off");
            txtemail.Attributes.Add("autocomplete", "off");
            txtmobilenor.Attributes.Add("autocomplete", "off");
            txtcontactperson.Attributes.Add("autocomplete", "off");
            txtsupplier.Focus();
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
        //ClientScript.RegisterStartupScript(this.GetType(), "SetInitialFocus", "<script>document.getElementById('" + txtsupplier.ClientID + "').focus();</script>");
        Button2.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtsupplier.ClientID + "').focus();return false;}} else {return true}; ");       
    }
    public void Bind()
    {
       // string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            try
            {
                Gridsupp.DataSource = null;
                Gridsupp.DataBind();
                tblsuppliermaster.Rows.Clear();
                SqlConnection con = new SqlConnection(strconn11);
                SqlCommand cmd = new SqlCommand("select * from tblsuppliermaster order by SupplierName", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblsuppliermaster.Columns.Add(col);
                    tblsuppliermaster.Columns.Add("SUPPLIER NAME");
                    tblsuppliermaster.Columns.Add("PHONE NUMBER");
                    tblsuppliermaster.Columns.Add("EMAIL ADDRESS");
                    tblsuppliermaster.Columns.Add("MOBILE NUMBER");
                    tblsuppliermaster.Columns.Add("CONTACT PERSON NAME");
                    tblsuppliermaster.Columns.Add("CONTACT PERSON NUMBER");
                    Session["Suppliermaster"] = tblsuppliermaster;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tblsuppliermaster = (DataTable)Session["Suppliermaster"];
                        drrw = tblsuppliermaster.NewRow();

                        drrw["SUPPLIER NAME"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();
                        drrw["PHONE NUMBER"] = ds.Tables[0].Rows[i]["phone"].ToString();
                        drrw["EMAIL ADDRESS"] = ds.Tables[0].Rows[i]["email"].ToString();
                        drrw["MOBILE NUMBER"] = ds.Tables[0].Rows[i]["mobileNo"].ToString();
                        drrw["CONTACT PERSON NAME"] = ds.Tables[0].Rows[i]["cperson"].ToString();
                        drrw["CONTACT PERSON NUMBER"] = ds.Tables[0].Rows[i]["cphone"].ToString();
                        tblsuppliermaster.Rows.Add(drrw);
                        //Gridsupp.DataSource = tblsuppliermaster;
                        //Gridsupp.DataBind();
                    }
                    DataView dws = tblsuppliermaster.DefaultView;
                    dws.Sort = "slno ASC";
                    Gridsupp.DataSource = tblsuppliermaster;
                    Gridsupp.DataBind();
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
                Gridsupp.DataSource = null;
                Gridsupp.DataBind();
                tblsuppliermaster.Rows.Clear();
                OleDbConnection conn10 = new OleDbConnection(strconn11);
                conn10.Open();
                OleDbCommand cmd1 = new OleDbCommand("select * from tblsuppliermaster order by SupplierName", conn10);

                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblsuppliermaster.Columns.Add(col);
                    tblsuppliermaster.Columns.Add("SUPPLIER NAME");
                    tblsuppliermaster.Columns.Add("PHONE NUMBER");
                    tblsuppliermaster.Columns.Add("EMAIL ADDRESS");
                    tblsuppliermaster.Columns.Add("MOBILE NUMBER");
                    tblsuppliermaster.Columns.Add("CONTACT PERSON NAME");
                    tblsuppliermaster.Columns.Add("CONTACT PERSON NUMBER");
                    Session["Suppliermaster"] = tblsuppliermaster;

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        tblsuppliermaster = (DataTable)Session["Suppliermaster"];
                        drrw = tblsuppliermaster.NewRow();
                        drrw["SUPPLIER NAME"] = ds1.Tables[0].Rows[i]["SupplierName"].ToString();
                        drrw["PHONE NUMBER"] = ds1.Tables[0].Rows[i]["phone"].ToString();
                        drrw["EMAIL ADDRESS"] = ds1.Tables[0].Rows[i]["email"].ToString();
                        drrw["MOBILE NUMBER"] = ds1.Tables[0].Rows[i]["mobileNo"].ToString();
                        drrw["CONTACT PERSON NAME"] = ds1.Tables[0].Rows[i]["cperson"].ToString();
                        drrw["CONTACT PERSON NUMBER"] = ds1.Tables[0].Rows[i]["cphone"].ToString();
                        tblsuppliermaster.Rows.Add(drrw);
                        //Gridsupp.DataSource = tblsuppliermaster;
                        //Gridsupp.DataBind();

                    }
                    DataView dws = tblsuppliermaster.DefaultView;
                    dws.Sort = "SLNO ASC";
                    Gridsupp.DataSource = tblsuppliermaster;
                    Gridsupp.DataBind();
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
         public string autoincrement()
      {
          
              if (!File.Exists(filename))
              {
                  SqlConnection con = new SqlConnection(strconn11);
                  con.Open();
                  SqlCommand cmd = new SqlCommand("select Max(SupplierCode) as SupplierCode from tblsuppliermaster", con);
                  SqlDataAdapter da = new SqlDataAdapter(cmd);
                  DataSet ds = new DataSet();
                  da.Fill(ds);


                  if (ds.Tables[0].Rows.Count > 0)
                  {

                      S_code = ds.Tables[0].Rows[0]["SupplierCode"].ToString();
                      double S_code1 = Convert.ToDouble(S_code);

                      if (S_code == "")
                      {
                          S_code = "0001";
                          //txtcustcode.Text = custid;
                          //txtcustmcode.Text=custid;
                      }
                           
                      else
                      {
                          if (S_code1 >= 0009)
                          {
                              count = Convert.ToInt16(cmd.ExecuteScalar()) + 1;
                              S_code = "00" + count;
                          }
                          else
                          {

                              count = Convert.ToInt16(cmd.ExecuteScalar()) + 1;
                              S_code = "000" + count;
                              //txtcustcode.Text = custid;
                              //txtcustmcode.Text=custid;
                          }
                          //double S_code10 = Convert.ToDouble(S_code);

                          //if (S_code10 >= 0009)
                          //{
                          //   S_code10 = S_code10 + 1;
                          //}
                      }

                  }
                  con.Close();
              }
              return S_code;
        }
    protected void Gridsupp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Gridsupp.PageIndex = e.NewPageIndex;
        Bind();

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (button_select == "Modify")
        {

            string Sysdatetime = DateTime.Now.ToString();
            string supcode = lblcode.Text;
            string suname = txtsupplier.Text;
            string addre1 = txtaddress1.Text;
            string addre2 = txtaddress2.Text;
            string addre3 = txtaddress3.Text;
            string phonenum = txtphone.Text;
            string emailid = txtemail.Text;
            string mobilenum = txtmobilenor.Text;
            string tguesti = "0";
            string cpersonnm = txtcontactperson.Text;
            string cphonenum = txtcontactpersphone.Text;
            string cod = lblcode.Text;
            string pan = txtPAN.Text;
            string tin = txtTin.Text;
            string adhar = txtAdhar.Text;
            string gst = TxtGST.Text;
            string credlim = TxtCreLim.Text;
            if (credlim == "")
            {
                credlim = "0";
            }
            int c = Convert.ToInt32(cod);
            // ClsBLGD.UpdateRecords("tblGeneric", "GN_name='" + gennam + "'", "GN_code='" + lblcode.Text + "'");
            if (addre1 == "")
            {

                Master.ShowModal("Address1 mandotory", "txtaddress1", 1);
                txtaddress1.Focus();
                return;

            }

            if (addre2 == "")
            {

                Master.ShowModal("Address2 mandotory", "txtaddress2", 2);
                txtaddress2.Focus();
                return;

            }

            if (addre3 == "")
            {

                Master.ShowModal("Address3 mandotory", "txtaddress3", 3);
                txtaddress3.Focus();
                return;

            }

            if (phonenum == "")
            {

                Master.ShowModal("Phone Number mandotory", "txtphone", 3);
                txtphone.Focus();
                return;

            }



            if (phonenum != "")
            {
                if (phonenum.Length == 11)
                {
                }
                else
                {
                    Master.ShowModal("Phone number cannot be Lesser/Greater than 11 characters !!!!", "txtphone", 1);
                    txtphone.Focus();
                    return;
                }
            }


            if (emailid == "")
            {

                Master.ShowModal("Enter Emai Address", "txtemail", 5);
                txtemail.Focus();
                return;

            }

            Regex mailIDPattern = new Regex(@"[\w-]+@([\w-]+\.)+[\w-]+");

            if (!string.IsNullOrEmpty(emailid) && !mailIDPattern.IsMatch(emailid))
            {
                Master.ShowModal("Enter Emai Address incorrect", "txtemail", 5);
                txtemail.Focus();
                return;
            }



            if (mobilenum == "")
            {

                Master.ShowModal("Mobile nor mandotory", "txtmobilenor", 3);
                txtmobilenor.Focus();
                return;

            }


            if (mobilenum != null)
            {
                if (mobilenum.Length == 10)
                {
                }
                else
                {
                    Master.ShowModal("Mobile number cannot be Lesser/Greater than 10 characters !!!!", "txtmobilenor", 1);
                    txtmobilenor.Focus();
                    return;
                }
            }

            //if (tguesti == "")
            //{

            //    Master.ShowModal("Guest Details mandotory", "txttinguest", 7);
            //    txttinguest.Focus();
            //    return;

            //}

            if (cpersonnm == "")
            {

                Master.ShowModal("Contact Person Name mandotory", "txtcontactperson", 8);
                txtcontactperson.Focus();
                return;

            }

            if (cphonenum == "")
            {

                Master.ShowModal("Contact Person Mobile Nor mandotory", "txtcontactpersphone", 8);
                txtcontactpersphone.Focus();
                return;

            }

            if (cphonenum != null)
            {
                if (cphonenum.Length == 10)
                {
                }
                else
                {
                    Master.ShowModal(" Contact person Mobile number cannot be Lesser/Greater than 10 characters !!!!", "txtcontactpersphone", 1);
                    txtcontactpersphone.Focus();
                    return;
                }
            }
            try
            {
                if (!File.Exists(filename))
                {
                    try
                    {
                       // ClsBLGP.updateSupplier("UPDATE_SUPPLIERMASTER", suname, addre1, addre2, addre3, phonenum, emailid, mobilenum, tguesti, cpersonnm, cphonenum, c);
                        ClsBLGP.updateSupplier("UPDATE_SUPPLIERMASTER", supcode,suname, addre1, addre2, addre3, phonenum, emailid, mobilenum, cpersonnm, cphonenum, pan, tin, adhar, gst, credlim);
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
                        OleDbConnection conn10 = new OleDbConnection(strconn11);
                        conn10.Open();
                        OleDbCommand cmd1 = new OleDbCommand("update tblsuppliermaster set add1='" + addre1 + "',add2='" + addre2 + "',add3='" + addre3 + "',phone='" + phonenum + "',email='" + emailid + "',mobileNo='" + mobilenum + "',tguest='" + tguesti + "', cperson='" + cpersonnm + "',cphone='" + cphonenum + "' where Suppliercode=" + c + "", conn10);
                        cmd1.ExecuteNonQuery();
                        conn10.Close();
                    }

                    catch (Exception ex)
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
                txtsupplier.Enabled = true;
                lblcode.Text = string.Empty;
                txtsupplier.Text = string.Empty;
                txtaddress1.Text = string.Empty;
                txtaddress2.Text = string.Empty;
                txtaddress3.Text = string.Empty;
                txtphone.Text = string.Empty;
                txtemail.Text = string.Empty;
                txtmobilenor.Text = string.Empty;
                txtAdhar.Text = string.Empty;
                txtPAN.Text = string.Empty;
                txtTin.Text = string.Empty;
                TxtGST.Text = string.Empty;
                TxtCreLim.Text = string.Empty;
                //txttinguest.Text = "";
                txtcontactperson.Text = string.Empty;
                txtcontactpersphone.Text = string.Empty;
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
                string Sysdatetime = DateTime.Now.ToString();
                string sid = autoincrement();
                string sname = txtsupplier.Text;
                string add1 = txtaddress1.Text;
                string add2 = txtaddress2.Text;
                string add3 = txtaddress3.Text;
                string phone = txtphone.Text;
                string email = txtemail.Text;
                string mobilenor = txtmobilenor.Text;
                string tguest = "0";
                string Balamount = "0";
                string cperson = txtcontactperson.Text;
                string cphone = txtcontactpersphone.Text;
                string pan = txtPAN.Text;
                string tin = txtTin.Text;
                string adhar = txtAdhar.Text;
                string gst = TxtGST.Text;
                string credlim = TxtCreLim.Text;
                if (credlim == "")
                {
                    credlim = "0";
                }

                if (sname == "")
                {

                    Master.ShowModal("Supplier Name mandatory", "txtsupplier", 0);
                    txtsupplier.Focus();
                    return;

                }
                DataSet dsgrp = ClsBLGD.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", sname);
                if (sname == "")
                {

                    Master.ShowModal("Supplier Name mandatory", "txtsupplier", 0);
                    txtsupplier.Focus();
                    return;

                }
                if (add1 == "")
                {

                    Master.ShowModal("Address1 mandotory", "txtaddress1", 1);
                    txtaddress1.Focus();
                    return;

                }

                if (add2 == "")
                {

                    Master.ShowModal("Address2 mandotory", "txtaddress2", 2);
                    txtaddress2.Focus();
                    return;

                }

                if (add3 == "")
                {

                    Master.ShowModal("Address3 mandotory", "txtaddress3", 3);
                    txtaddress3.Focus();
                    return;

                }

                if (phone == "")
                {

                    Master.ShowModal("Phone Number mandotory", "txtphone", 3);
                    txtphone.Focus();
                    return;

                }
                //if (phone != "")
                //{
                //    if (phone.Length == 11)
                //    {
                //    }
                //    else
                //    {
                //        Master.ShowModal("Phone number cannot be Lesser/Greater than 11 characters !!!!", "txtphone", 3);
                        
                //        return;
                //    }
                //}


                if (email == "")
                {

                    Master.ShowModal("Enter Emai Address", "txtemail", 5);
                    txtemail.Focus();
                    return;

                }

                if (mobilenor == "")
                {

                    Master.ShowModal("Mobile nor mandotory", "txtmobilenor", 3);
                    txtmobilenor.Focus();
                    return;

                }


                if (mobilenor != null)
                {
                    if (mobilenor.Length == 10)
                    {
                    }
                    else
                    {
                        Master.ShowModal("Mobile number cannot be Lesser/Greater than 10 characters !!!!", "txtmobilenor", 1);
                       // txtmobilenor.Focus();
                        return;
                    }
                }

                //if (tguest == "")
                //{

                //    Master.ShowModal("Guest Details mandotory", "txttinguest", 7);
                //    txttinguest.Focus();
                //    return;

                //}

                if (cperson == "")
                {

                    Master.ShowModal("Contact Person Name mandotory", "txtcontactperson", 8);
                    txtcontactperson.Focus();
                    return;

                }

                if (cphone == "")
                {

                    Master.ShowModal("Contact Person Mobile Nor mandotory", "txtcontactpersphone", 8);
                    txtcontactpersphone.Focus();
                    return;

                }
                if (cphone != null)
                {
                    if (cphone.Length == 10)
                    {
                    }
                    else
                    {
                        Master.ShowModal(" Contact person Mobile number cannot be Lesser/Greater than 10 characters !!!!", "txtcontactpersphone", 1);
                       // txtcontactpersphone.Focus();
                        return;
                    }
                }

                if (!File.Exists(filename))
                {
                    try
                    {
                        ClsBLGP.Supplier("INSERT_SUPPLIERMASTER", sid, sname, add1, add2, add3, phone, email, mobilenor, tguest, cperson, cphone, Balamount,pan,tin,adhar,gst,credlim,Session["username"].ToString(), sMacAddress, Sysdatetime);
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
                    // String strconn11 = Dbconn.conmenthod();
                    try
                    {
                        OleDbConnection con = new OleDbConnection(strconn11);
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("insert into tblsuppliermaster(SupplierName,add1,add2,add3,phone,email,mobileNo,tguest,cperson,cphone,LoginName,Mac_id,Sysdatetime) values('" + sname + "','" + add1 + "','" + add2 + "','" + add3 + "','" + phone + "','" + email + "','" + mobilenor + "','" + tguest + "','" + cperson + "','" + cphone + "','" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con);
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
                txtsupplier.Text = string.Empty;
                txtsupplier.Focus();
                txtaddress1.Text = string.Empty;
                txtaddress2.Text = string.Empty;
                txtaddress3.Text = string.Empty;
                txtphone.Text = string.Empty;
                txtemail.Text = string.Empty;
                txtmobilenor.Text = string.Empty;
                txtPAN.Text = string.Empty;
                txtTin.Text = string.Empty;
                txtAdhar.Text = string.Empty;
                TxtGST.Text = string.Empty;
                TxtCreLim.Text = string.Empty;
                //   txttinguest.Text = string.Empty;
                txtcontactperson.Text = string.Empty;
                txtcontactpersphone.Text = string.Empty;
                // Response.Redirect("Suppliermaster.aspx");

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


     [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Buyername(string prefixText)
    {
       // string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
       // string oConn = ConfigurationManager.AppSettings["ConnectionString"];
        SqlConnection conn = new SqlConnection(strconn11);
        conn.Open();
        SqlCommand cmd = new SqlCommand("select SupplierName from tblsuppliermaster where SupplierName like @1+'%'", conn);
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
            OleDbConnection conn=new OleDbConnection(strconn11);
            conn.Open();
            OleDbCommand cmd=new OleDbCommand("select SupplierName from tblsuppliermaster where SupplierName like @1+'%'", conn);
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
        
        string sname = txtsupplier.Text;
        try
        {
        DataSet dsgrp1 = ClsBLGD.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", sname);
        lblmod.Text = "Supplier with below details already exists";
        int code = Convert.ToInt32(dsgrp1.Tables[0].Rows[0]["SupplierCode"].ToString());
        lblcode.Text = Convert.ToString(code);
        Table2.Visible = true;
        ClsBLGD.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", sname);
        txtsupplier.Text = dsgrp1.Tables[0].Rows[0]["SupplierName"].ToString();
        txtaddress1.Text = dsgrp1.Tables[0].Rows[0]["add1"].ToString();
        txtaddress2.Text = dsgrp1.Tables[0].Rows[0]["add2"].ToString();
        txtaddress3.Text = dsgrp1.Tables[0].Rows[0]["add3"].ToString();
        txtphone.Text = dsgrp1.Tables[0].Rows[0]["phone"].ToString();
        txtemail.Text = dsgrp1.Tables[0].Rows[0]["email"].ToString();
        txtmobilenor.Text = dsgrp1.Tables[0].Rows[0]["mobileNo"].ToString();
      //  txttinguest.Text = dsgrp1.Tables[0].Rows[0]["tguest"].ToString();
        txtcontactperson.Text = dsgrp1.Tables[0].Rows[0]["cperson"].ToString();
        txtcontactpersphone.Text = dsgrp1.Tables[0].Rows[0]["cphone"].ToString();
        //Master.ShowModal("Supplier Name already Exists", "txtsupplier", 1);
        mod();
        Table2.Visible = false;
        txtsupplier.Enabled = false;
        txtaddress1.Enabled = true;
        txtaddress2.Enabled = true;
        txtaddress3.Enabled = true;
        txtphone.Enabled = true;
        txtemail.Enabled = true;
        txtmobilenor.Enabled = true;
       // txttinguest.Enabled = true;
        txtcontactperson.Enabled = true;
        txtcontactpersphone.Enabled = true;
        txtaddress1.Focus();
        return;
        }
          catch (Exception ex)
          {
            string asd = ex.Message;
            lblerror.Enabled = true;
            lblerror.Text = asd;
          }


    }
    public string mod()
    {
        button_select = dbcon.modify();

        if (button_select == "Modify")
        {
            txtsupplier.Enabled = true;
        }
        return button_select;
    }
    protected void btn_Click(object sender, EventArgs e)
    {

        no();
    }
    public void no()
    {
        try
        {
        Table2.Visible = false;
        txtsupplier.Enabled = true;
        txtaddress1.Enabled = true;
        txtaddress2.Enabled = true;
        txtaddress3.Enabled = true;
        txtphone.Enabled = true;
        txtemail.Enabled = true;
        txtmobilenor.Enabled = true;
      //  txttinguest.Enabled = true;
        txtcontactperson.Enabled = true;
        txtcontactpersphone.Enabled = true;
        txtsupplier.Text = string.Empty;
        txtaddress1.Text = string.Empty;
        txtaddress2.Text = string.Empty;
        txtaddress3.Text = string.Empty;
        txtphone.Text = string.Empty;
        txtemail.Text = string.Empty;
        txtmobilenor.Text = string.Empty;
        txtPAN.Text = string.Empty;
        txtTin.Text = string.Empty;
        txtAdhar.Text = string.Empty;
        TxtGST.Text = string.Empty;
        TxtCreLim.Text = string.Empty;
       // txttinguest.Text = "";
        txtcontactperson.Text = string.Empty;
        txtcontactpersphone.Text = string.Empty;
        txtsupplier.Enabled = true;
        txtsupplier.Focus();
        }
          catch (Exception ex)
            {
              string asd = ex.Message;
              lblerror.Enabled = true;
              lblerror.Text = asd;
            }
    }
    protected void txtemail_TextChanged(object sender, EventArgs e)
         {
             if (txtemail.Text == "")
             {
                 txtemail.Focus();
             }

             else if (txtmobilenor.Text == "")
             {
                 txtmobilenor.Focus();
             }

             else if (txtcontactperson.Text == "")
             {
                 txtcontactperson.Focus();
             }

             else if (txtcontactpersphone.Text == "")
             {
                 txtcontactpersphone.Focus();
             }
             else
             {
                 txtmobilenor.Focus();
             }

             try
             {
                 bool Validity = false;
                 Validity = ClsBLGD.IsValidEmail(txtemail.Text);
                 if (Validity == true)
                 {
                     txtmobilenor.Focus();
                 }
                 else
                 {
                     Master.ShowModal("Entered Email is not in Correct Format. !!!", "txtemail", 1);
                 }
             }
             catch (Exception ex)
               {
                 string asd = ex.Message;
                 lblerror.Visible = true;
                 lblerror.Text = asd;
                 lblerror.Style.Add("text-decoration", "blink");
               }

         }


    protected void txtsupplier_TextChanged(object sender, EventArgs e)
    {

        try
        {
            string name1 = txtsupplier.Text.TrimStart();
            if (name1 == "")
            {
                txtsupplier.Focus();
            }
            else
            {
                txtaddress1.Focus();
            }
            DataSet dsgrp = ClsBLGD.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", name1);
            if (dsgrp.Tables[0].Rows.Count > 0)
            {
                lblmod.Text = "Supplier with below name already exists.Click Modify to edit details";
                int code = Convert.ToInt32(dsgrp.Tables[0].Rows[0]["SupplierCode"].ToString());
                txtaddress1.Text = dsgrp.Tables[0].Rows[0]["add1"].ToString();
                txtaddress2.Text = dsgrp.Tables[0].Rows[0]["add2"].ToString();
                txtaddress3.Text = dsgrp.Tables[0].Rows[0]["add3"].ToString();
                txtphone.Text = dsgrp.Tables[0].Rows[0]["phone"].ToString();
                txtemail.Text = dsgrp.Tables[0].Rows[0]["email"].ToString();
                txtmobilenor.Text = dsgrp.Tables[0].Rows[0]["mobileNo"].ToString();
              //  txttinguest.Text = dsgrp.Tables[0].Rows[0]["tguest"].ToString();
                txtcontactperson.Text = dsgrp.Tables[0].Rows[0]["cperson"].ToString();
                txtcontactpersphone.Text = dsgrp.Tables[0].Rows[0]["cphone"].ToString();
                txtPAN.Text = dsgrp.Tables[0].Rows[0]["PAN_No"].ToString();
                txtTin.Text = dsgrp.Tables[0].Rows[0]["TIN_No"].ToString();
                txtAdhar.Text = dsgrp.Tables[0].Rows[0]["ADHAR_No"].ToString();
                TxtGST.Text = dsgrp.Tables[0].Rows[0]["GST_No"].ToString();
                TxtCreLim.Text = dsgrp.Tables[0].Rows[0]["Credit_Limit"].ToString();

                lblcode.Text = Convert.ToString(code);
                Table2.Visible = true;
                txtaddress1.Enabled = false;
                txtaddress2.Enabled = false;
                txtaddress3.Enabled = false;
                txtphone.Enabled = false;
                txtemail.Enabled = false;
                txtmobilenor.Enabled = false;
                //txttinguest.Enabled = false;
                txtcontactperson.Enabled = false;
                txtcontactpersphone.Enabled = false;
                btn.Enabled = true;
                btn.Focus();
                return;
            }
            else
            {
                txtaddress1.Enabled = true;
                txtaddress1.Focus();
            }
        }          //try
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Enabled = true;
            lblerror.Text = asd;
        }

    }



    protected void txtmobilenor_TextChanged(object sender, EventArgs e)
    {
        if (txtmobilenor.Text == "")
        {
            txtmobilenor.Focus();
        }

        else if (txtcontactperson.Text == "")
        {
            txtcontactperson.Focus();
        }

        else if (txtcontactpersphone.Text == "")
        {
            txtcontactpersphone.Focus();
        }
        else
        {
            txtcontactperson.Focus();
        }
        string mobilenor = txtmobilenor.Text;
        txtcontactpersphone.Text = mobilenor;
        txtcontactperson.Focus();
    }

    protected void txtaddress1_TextChanged(object sender, EventArgs e)
    {
        if (txtaddress1.Text == "")
        {
            txtaddress1.Focus();
        }
        else if (txtaddress2.Text=="") 
        {
            txtaddress2.Focus();
        }
        else if (txtaddress3.Text == "")
        {
            txtaddress3.Focus();
        }
        else if(txtphone.Text=="")
        {
            txtphone.Focus();
        }
        else if (txtemail.Text == "")
        {
            txtemail.Focus();
        }

        else if (txtmobilenor.Text == "")
        {
            txtmobilenor.Focus();
        }

        else if (txtcontactperson.Text == "")
        {
            txtcontactperson.Focus();
        }

        else if (txtcontactpersphone.Text == "")
        {
            txtcontactpersphone.Focus();
        }
        else
        {
            txtaddress2.Focus();
        }



    }

    protected void txtaddress2_TextChanged(object sender, EventArgs e)
    {
        if (txtaddress2.Text == "")
        {
            txtaddress2.Focus();
        }
        else if (txtaddress3.Text == "")
        {
            txtaddress3.Focus();
        }
        else if (txtphone.Text == "")
        {
            txtphone.Focus();
        }
        else if (txtemail.Text == "")
        {
            txtemail.Focus();
        }

        else if (txtmobilenor.Text == "")
        {
            txtmobilenor.Focus();
        }

        else if (txtcontactperson.Text == "")
        {
            txtcontactperson.Focus();
        }

        else if (txtcontactpersphone.Text == "")
        {
            txtcontactpersphone.Focus();
        }
        else
        {
            txtaddress3.Focus();
        }


    }

    protected void txtaddress3_TextChanged(object sender, EventArgs e)
    {
        if (txtaddress3.Text == "")
        {
            txtaddress3.Focus();
        }
        else if (txtphone.Text == "")
        {
            txtphone.Focus();
        }
        else if (txtemail.Text == "")
        {
            txtemail.Focus();
        }

        else if (txtmobilenor.Text == "")
        {
            txtmobilenor.Focus();
        }

        else if (txtcontactperson.Text == "")
        {
            txtcontactperson.Focus();
        }

        else if (txtcontactpersphone.Text == "")
        {
            txtcontactpersphone.Focus();
        }
        else
        {
            txtphone.Focus();
        }
       

    }

    protected void txtphone_TextChanged(object sender, EventArgs e)
    {
        if (txtphone.Text == "")
        {
            txtphone.Focus();
        }
        else if (txtemail.Text == "")
        {
            txtemail.Focus();
        }

        else if (txtmobilenor.Text == "")
        {
            txtmobilenor.Focus();
        }

        else if (txtcontactperson.Text == "")
        {
            txtcontactperson.Focus();
        }

        else if (txtcontactpersphone.Text == "")
        {
            txtcontactpersphone.Focus();
        }
        else
        {
            txtphone.Focus();
        }


    }




    protected void txtcontactperson_TextChanged(object sender, EventArgs e)
    {
        if (txtcontactperson.Text == "")
        {
            txtcontactperson.Focus();
        }

        else if (txtcontactpersphone.Text == "")
        {
            txtcontactpersphone.Focus();
        }
        else
        {
            txtcontactpersphone.Focus();
        }


    }





    protected void txtcontactpersphone_TextChanged(object sender, EventArgs e)
    {
        txtPAN.Focus();
    }
    protected void TxtCreLim_TextChanged(object sender, EventArgs e)
    {
        Button1.Focus();
    }
    protected void txtPAN_TextChanged(object sender, EventArgs e)
    {
        txtTin.Focus();
    }
    protected void txtTin_TextChanged(object sender, EventArgs e)
    {
        txtAdhar.Focus();
    }
    protected void txtAdhar_TextChanged(object sender, EventArgs e)
    {
        TxtGST.Focus();
    }
    protected void TxtGST_TextChanged(object sender, EventArgs e)
    {
        TxtCreLim.Focus();
    }
}
