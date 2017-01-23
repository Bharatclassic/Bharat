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
using System.Drawing; 

public partial class SalesReturn : System.Web.UI.Page
{
    ClsBLLGeneraldetails clsgd = new ClsBLLGeneraldetails();
    ClsBALSalesReturn ClsBLGP = new ClsBALSalesReturn();
    //ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
    DataTable tblsalereturn = new DataTable();
    protected static string button_select;
    string sMacAddress = "";
    DataRow drrw;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Panel2.Visible = false;
            //Panel3.Visible = false;

            System.DateTime Dtnow = DateTime.Now;
            //txtdate.Text = Dtnow.ToString("dd/MM/yyyy");
            txtdate1.Text = Dtnow.ToString("dd/MM/yyyy");

            

        }

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
        } return sMacAddress;
    }

    protected void txtbillno_TextChanged(object sender, EventArgs e)
    {
        string billno = txtbillno.Text;
        if (!File.Exists(filename))
        {
            try
            {


                gvDetails.DataSource = null;
                gvDetails.DataBind();
                tblsalereturn.Rows.Clear();
                SqlConnection con = new SqlConnection(strconn11);
                SqlCommand cmd = new SqlCommand("SELECT STransno,Invoiceno,Productcode,ProductName,Batchno,Expiredate,Quantity,Rate from tblProductsale where Invoiceno='" + billno + "' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblsalereturn.Columns.Add(col);
                    tblsalereturn.Columns.Add("STransno");
                    tblsalereturn.Columns.Add("Invoiceno");
                    tblsalereturn.Columns.Add("Productcode");
                    tblsalereturn.Columns.Add("ProductName");
                    tblsalereturn.Columns.Add("Batchno");
                    tblsalereturn.Columns.Add("Expiredate");
                    tblsalereturn.Columns.Add("Quantity");
                    tblsalereturn.Columns.Add("Rate");
                   

                    Session["Group"] = tblsalereturn;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tblsalereturn = (DataTable)Session["Group"];
                        drrw = tblsalereturn.NewRow();
                        drrw["STransno"] = ds.Tables[0].Rows[i]["STransno"].ToString();
                        drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                        drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                        drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                        drrw["Batchno"] = ds.Tables[0].Rows[i]["Batchno"].ToString();
                        drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                        drrw["Quantity"] = ds.Tables[0].Rows[i]["Quantity"].ToString();
                        drrw["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();
                         tblsalereturn.Rows.Add(drrw);
                    }
                    DataView dw = tblsalereturn.DefaultView;
                    dw.Sort = "SLNO ASC";
                    gvDetails.DataSource = tblsalereturn;
                    gvDetails.DataBind();
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
                DateTime dtEntered1 = Convert.ToDateTime(txtdate1.Text);
                string strEnteredDate1 = dtEntered1.ToString("MM/dd/yyyy");

                gvDetails.DataSource = null;
                gvDetails.DataBind();
                tblsalereturn.Rows.Clear();
                OleDbConnection con = new OleDbConnection(strconn11);
                OleDbCommand cmd = new OleDbCommand("SELECT STransno,Invoiceno,Productcode,ProductName,Batchno,Expiredate,Quantity,Rate from tblProductsale where Invoiceno='" + billno + "'", con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblsalereturn.Columns.Add(col);
                    tblsalereturn.Columns.Add("STransno");
                    tblsalereturn.Columns.Add("Invoiceno");
                    tblsalereturn.Columns.Add("Productcode");
                    tblsalereturn.Columns.Add("ProductName");
                    tblsalereturn.Columns.Add("Batchno");
                    tblsalereturn.Columns.Add("Expiredate");
                    tblsalereturn.Columns.Add("Quantity");
                    tblsalereturn.Columns.Add("Rate");

                    Session["Group"] = tblsalereturn;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tblsalereturn = (DataTable)Session["Group"];
                        drrw = tblsalereturn.NewRow();
                        drrw["STransno"] = ds.Tables[0].Rows[i]["STransno"].ToString();
                        drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                        drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                        drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                        drrw["Batchno"] = ds.Tables[0].Rows[i]["Batchno"].ToString();
                        drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                        drrw["Quantity"] = ds.Tables[0].Rows[i]["Quantity"].ToString();
                        drrw["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();

                        tblsalereturn.Rows.Add(drrw);
                    }
                    DataView dw = tblsalereturn.DefaultView;
                    dw.Sort = "SLNO ASC";
                    gvDetails.DataSource = tblsalereturn;
                    gvDetails.DataBind();
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
    protected void btnExit_Click(object sender, EventArgs e)
    {

        Response.Redirect("Home.aspx");
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvDetails.PageIndex = e.NewPageIndex;

        //BindUserDetails();

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        string Billno1 = txtbillno.Text;
        if (Billno1 == "")
        {

            Master.ShowModal("Billno is mandatory", "txtbillno", 0);
            txtbillno.Focus();
            return;

        }

        {
            if (!File.Exists(filename))
            {
                foreach (GridViewRow gvrow in gvDetails.Rows)
                {

                    CheckBox chkdelete = (CheckBox)gvrow.FindControl("chkSelect");
                    //Condition to check checkbox selected or not
                    if (chkdelete != null & chkdelete.Checked)
                    {

                        //int Transno = Convert.ToInt32(gvDetails.DataKeys[gvrow.RowIndex].Value);
                        string Billno = txtbillno.Text;
                        //string Transno = Convert.ToString(gvDetails.DataKeys[gvrow.RowIndex].Value);
                        string Stransno = Convert.ToString(gvDetails.DataKeys[gvrow.RowIndex].Value);
                        string Invoiceno = gvrow.Cells[4].Text;
                        string Productcode = gvrow.Cells[5].Text;
                        string ProductName = gvrow.Cells[6].Text;
                        string Batchid = gvrow.Cells[7].Text;
                        string Expiredate = gvrow.Cells[8].Text;
                        string Quantity = gvrow.Cells[9].Text;
                        string Purchaseprice = gvrow.Cells[10].Text;
                        //string Chequedate = gvDetails.Rows[1].Cells[8].Text;
                        //string Invoicedate = gvrow.Cells[11].Text;




                        //string TransNo1 = Convert.ToString((gvDetails.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Text);
                        using (SqlConnection con = new SqlConnection(strconn11))
                        {
                            con.Open();

                            System.DateTime Dtnow = DateTime.Now;
                            string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");

                            //string In_flag1 = "N";
                            //SqlCommand cmd20 = new SqlCommand("UPDATE tblProductinward SET  In_falg1='" + In_flag1 + "' WHERE  TransNo ='" + TransNo1 + "'", con);
                            //cmd20.ExecuteNonQuery();

                            ClsBLGP.SalesReturn("INSERT_SALESRETURN", Billno, Stransno,Invoiceno, Productcode, ProductName, Batchid, Expiredate, Quantity, Purchaseprice, Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                            

                           

                           

                            lblsuccess.Visible = true;
                            lblsuccess.Text = "inserted successfully";

                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);


                            con.Close();
                        }
                    }
                }
            }
            else
            {
                foreach (GridViewRow gvrow in gvDetails.Rows)
                {

                    CheckBox chkdelete = (CheckBox)gvrow.FindControl("chkSelect");
                    //Condition to check checkbox selected or not
                    if (chkdelete.Checked)
                    {
                        //int Transno = Convert.ToInt32(gvDetails.DataKeys[gvrow.RowIndex].Value);
                        string Billno = txtbillno.Text;
                        //string Transno = Convert.ToString(gvDetails.DataKeys[gvrow.RowIndex].Value);
                        string Stransno = Convert.ToString(gvDetails.DataKeys[gvrow.RowIndex].Value);
                        string Invoiceno = gvrow.Cells[4].Text;
                        string Productcode = gvrow.Cells[5].Text;
                        string ProductName = gvrow.Cells[6].Text;
                        string Batchid = gvrow.Cells[7].Text;
                        string Expiredate = gvrow.Cells[8].Text;
                        string Quantity = gvrow.Cells[9].Text;
                        string Purchaseprice = gvrow.Cells[10].Text;
                        


                        System.DateTime Dtnow = DateTime.Now;
                        string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");

                        OleDbConnection con = new OleDbConnection(strconn11);
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("insert into tblSalesreturn(Stransno,Billno,Invoiceno,Productcode,ProductName,Batchid,Expiredate,Quantity,Purchaseprice,Login_name,Mac_id,Sysdatetime) values('" + Stransno + "','" + Billno + "','" + Invoiceno + "','" + Productcode + "','" + ProductName + "','" + Batchid + "','" + Expiredate + "','" + Quantity + "','" + Purchaseprice + "','" + Session["username"].ToString() + "','" + sMacAddress + "','" + sqlFormattedDate + "')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                lblsuccess.Visible = true;
                lblsuccess.Text = "inserted successfully";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

            }
        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

        foreach (GridViewRow gvrow in gvDetails.Rows)
        {
            //CheckBox chkdelete = (CheckBox)gvrow.FindControl("chkSelect");
            //Condition to check checkbox selected or not

            //string Transno = gvDetails.Rows[0].Cells[3].Text;
            //Session["Transno"] = gvDetails.Rows[0].Cells[3].Text;
            Session["STransno"] = Convert.ToString(gvDetails.DataKeys[gvrow.RowIndex].Value);
            this.ModalPopupExtender2.Enabled = true;
            ModalPopupExtender2.Show();

        }

    }





    protected void btncancel_click(object sender, EventArgs e)
    {
        this.ModalPopupExtender2.Enabled = false;
        ModalPopupExtender2.Hide();

        //ddgrpcode.Items.Clear();
        //BindUserDetails();

        

        if (!File.Exists(filename))
        {
            try
            {
                string billno = txtbillno.Text;

                gvDetails.DataSource = null;
                gvDetails.DataBind();
                tblsalereturn.Rows.Clear();
                SqlConnection con = new SqlConnection(strconn11);
                //string In_flag1 = "Y";
                SqlCommand cmd = new SqlCommand("SELECT STransno,Invoiceno,Productcode,ProductName,Batchno,Expiredate,Quantity,Rate from tblProductsale where Invoiceno='" + billno + "' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblsalereturn.Columns.Add(col);
                    tblsalereturn.Columns.Add("STransno");
                    tblsalereturn.Columns.Add("Invoiceno");
                    tblsalereturn.Columns.Add("Productcode");
                    tblsalereturn.Columns.Add("ProductName");
                    tblsalereturn.Columns.Add("Batchno");
                    tblsalereturn.Columns.Add("Expiredate");
                    tblsalereturn.Columns.Add("Quantity");
                    tblsalereturn.Columns.Add("Rate");


                    Session["Group"] = tblsalereturn;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tblsalereturn = (DataTable)Session["Group"];
                        drrw = tblsalereturn.NewRow();
                        drrw["STransno"] = ds.Tables[0].Rows[i]["STransno"].ToString();
                        drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                        drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                        drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                        drrw["Batchno"] = ds.Tables[0].Rows[i]["Batchno"].ToString();
                        drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                        drrw["Quantity"] = ds.Tables[0].Rows[i]["Quantity"].ToString();
                        drrw["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();
                        tblsalereturn.Rows.Add(drrw);
                    }
                    DataView dw = tblsalereturn.DefaultView;
                    dw.Sort = "SLNO ASC";
                    gvDetails.DataSource = tblsalereturn;
                    gvDetails.DataBind();
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
                string billno1 = txtbillno.Text;
                DateTime dtEntered1 = Convert.ToDateTime(txtdate1.Text);
                string strEnteredDate1 = dtEntered1.ToString("MM/dd/yyyy");

                gvDetails.DataSource = null;
                gvDetails.DataBind();
                tblsalereturn.Rows.Clear();
                OleDbConnection con = new OleDbConnection(strconn11);
                string In_flag1 = "Y";
                OleDbCommand cmd = new OleDbCommand("SELECT STransno,Invoiceno,Productcode,ProductName,Batchno,Expiredate,Quantity,Rate from tblProductsale where Invoiceno='" + billno1 + "' ", con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblsalereturn.Columns.Add(col);
                    tblsalereturn.Columns.Add("STransno");
                    tblsalereturn.Columns.Add("Invoiceno");
                    tblsalereturn.Columns.Add("Productcode");
                    tblsalereturn.Columns.Add("ProductName");
                    tblsalereturn.Columns.Add("Batchno");
                    tblsalereturn.Columns.Add("Expiredate");
                    tblsalereturn.Columns.Add("Quantity");
                    tblsalereturn.Columns.Add("Rate");

                    Session["Group"] = tblsalereturn;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tblsalereturn = (DataTable)Session["Group"];
                        drrw = tblsalereturn.NewRow();
                        drrw["STransno"] = ds.Tables[0].Rows[i]["STransno"].ToString();
                        drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                        drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                        drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                        drrw["Batchno"] = ds.Tables[0].Rows[i]["Batchno"].ToString();
                        drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                        drrw["Quantity"] = ds.Tables[0].Rows[i]["Quantity"].ToString();
                        drrw["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();

                        tblsalereturn.Rows.Add(drrw);
                    }
                    DataView dw = tblsalereturn.DefaultView;
                    dw.Sort = "SLNO ASC";
                    gvDetails.DataSource = tblsalereturn;
                    gvDetails.DataBind();
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

    

}