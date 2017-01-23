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
using System.Web.UI.WebControls.WebParts;
using System.Web.Services;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Collections.Specialized;
using System.Net.NetworkInformation;
using System.Management;
using System.Drawing;


public partial class PurchaseReturn : System.Web.UI.Page
{
    ClsBLLGeneraldetails clsgd = new ClsBLLGeneraldetails();
    ClsBALPurchaseReturn ClsBLGP = new ClsBALPurchaseReturn();
    clsBALSuspenseAsset ClsBLGP10 = new clsBALSuspenseAsset();


    //ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    ClsBALTransaction ClsBLGP2 = new ClsBALTransaction();

    ClsBALProductinward Clsprdinw = new ClsBALProductinward();
    Dbconn dbcon = new Dbconn();
    protected static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
    DataTable tblProductinward = new DataTable();
    DataTable tblProductinward10 = new DataTable();
    protected static string button_select;
    string sMacAddress = "";
    DataRow drrw;

    ArrayList arryno19 = new ArrayList();

    ArrayList arryname19 = new ArrayList();

    ArrayList arryno15 = new ArrayList();

    ArrayList arryname15 = new ArrayList();

    ArrayList arryno = new ArrayList();

    ArrayList arryname = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Visible = false;
        lblsuccess.Visible = false;


        if (!Page.IsPostBack)
        {

            Panel2.Visible = false;
            Panel3.Visible = false;

            System.DateTime Dtnow = DateTime.Now;
            //txtdate.Text = Dtnow.ToString("dd/MM/yyyy");
          //  txtdate1.Text = Dtnow.ToString("dd/MM/yyyy");




            manufacture();
            suppliername();
            // BindUserDetails();

        }

        // GetMACAddress();

    }
    protected void rdtrans_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdtrans.SelectedIndex == 0)
        {
            Panel2.Visible = true;
            Panel3.Visible = false;
            // System.DateTime Dtnow = DateTime.Now;
            //string date1 = Dtnow.ToString("dd/MM/yyyy");





            DateTime today = DateTime.Now;
            //DateTime answer = today.AddDays(31);

            DateTime origDT = Convert.ToDateTime(today);
            DateTime lastDate = new DateTime(origDT.Year, origDT.Month, 1).AddMonths(1).AddDays(-1);

            DateTime answer = lastDate.AddDays(91);

            txtda.Text = Convert.ToString(today);


            // genersdate();

            ddlsupplier.Focus();





        }
        else if (rdtrans.SelectedIndex == 1)
        {
            Panel2.Visible = false;
            Panel3.Visible = true;
            //System.DateTime Dtnow = DateTime.Now;
            //txtdate10.Text = Dtnow.ToString("dd/MM/yyyy");

            Label1.Visible = true;

            ddlmanufacturer.Visible = true;


            DateTime today = DateTime.Now;
            //DateTime answer = today.AddDays(31);

            DateTime origDT = Convert.ToDateTime(today);
            DateTime lastDate = new DateTime(origDT.Year, origDT.Month, 1).AddMonths(1).AddDays(-1);
            DateTime answer = lastDate.AddDays(91);
            txtdate10.Text = Convert.ToString(today);

            //  genersdate10();

            ddlmanufacturer.Focus();


        }

    }

    public void suppliername()
    {

        DataSet dsgroup20 = clsgd.GetDataSet("distinct SupplierName", "tblsuppliermaster");
        for (int i = 0; i < dsgroup20.Tables[0].Rows.Count; i++)
        {
            DataSet dsgroup21 = clsgd.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", dsgroup20.Tables[0].Rows[i]["SupplierName"].ToString());
            arryname19.Add(dsgroup21.Tables[0].Rows[0]["SupplierName"].ToString());

        }
        arryname19.Sort();
        arryno19.Add("-Select-");
        //arryno19.Add("Add New");

        for (int i = 0; i < arryname19.Count; i++)
        {
            arryno19.Add(arryname19[i].ToString());
        }
        ddlsupplier.DataSource = arryno19;
        ddlsupplier.DataBind();
    }


    protected void ddlmanufacturer_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtdate10.Focus();
        genersdate10();
        binddetails10();

    }




    protected void txtdate10_TextChanged(object sender, EventArgs e)
    {
        genersdate10();
        binddetails10();
        Button1.Focus();
    }




    public void manufacture()
    {
        DataSet dsgroup17 = clsgd.GetDataSet("distinct ManufactureName", "tblmanufacture");
        for (int i = 0; i < dsgroup17.Tables[0].Rows.Count; i++)
        {
            DataSet dsgroup18 = clsgd.GetcondDataSet("*", "tblmanufacture", "ManufactureName", dsgroup17.Tables[0].Rows[i]["ManufactureName"].ToString());
            arryname15.Add(dsgroup18.Tables[0].Rows[0]["ManufactureName"].ToString());

        }
        arryname15.Sort();
        arryno15.Add("-Select-");
        //arryno15.Add("Add New");

        for (int i = 0; i < arryname15.Count; i++)
        {
            arryno15.Add(arryname15[i].ToString());
        }
        ddlmanufacturer.DataSource = arryno15;
        ddlmanufacturer.DataBind();


    }

    protected void Return_Qty_TextChanged(object sender, EventArgs e)
    {


        for (int k = 0; k < Gridreturn.Rows.Count; k++)
        {
            double sum = 0.0;
            TextBox txt1 = (TextBox)Gridreturn.Rows[k].Cells[0].FindControl("Return_Qty");
            string rtnqty = txt1.Text;

            if (rtnqty != "0")
            {

                double rtnqty10 = Convert.ToDouble(rtnqty);

                string price = Gridreturn.Rows[k].Cells[5].Text;

                double price10 = Convert.ToDouble(price);

                double dprice = rtnqty10 * price10;

                double amt = Convert.ToDouble(dprice);

                sum = sum + amt;

                txtamount.Text = Convert.ToString(sum);

            }



        }

        btnsave.Focus();


    }


    public void genersdate()
    {
        if (txtda.Text != "")
        {
            DateTime expdate = Convert.ToDateTime(txtda.Text);

            string expdate10 = expdate.ToString("dd/MM/yyyy");
            string close_flag = "N";
            string supp = ddlsupplier.SelectedItem.Text;

            //  string p_flag = ddgrpcode.SelectedItem.Text;

            DataSet dsgroup20 = clsgd.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", supp);

            if (dsgroup20.Tables[0].Rows.Count > 0)
            {

                string supcode = dsgroup20.Tables[0].Rows[0]["SupplierCode"].ToString();

                DataSet dsgroup25 = clsgd.GetcondDataSet("*", "tblProductinward", "SuppplierCode", supcode);

                if (dsgroup25.Tables[0].Rows.Count > 0)
                {


                    DataSet dsgroup = clsgd.GetDataSet("distinct In_falg9", "tblpreturnProductinward");
                    for (int i = 0; i < dsgroup.Tables[0].Rows.Count; i++)
                    {
                        DataSet dsgroup1 = clsgd.GetcondDataSet3("*", "tblpreturnProductinward", "In_falg9", expdate10, "In_falg8", close_flag, "SuppplierCode", supcode);

                        if (dsgroup1.Tables[0].Rows.Count > 0)
                        {
                            arryname.Add(dsgroup1.Tables[0].Rows[0]["In_falg9"].ToString());
                        }
                        //else
                        //{
                        //    Master.ShowModal("No genarate record are possible. !!!", "ddlsupplier", 1);
                        //    return;
                        //}


                    }

                    arryname.Sort();
                    //arryno.Add("-Select-");
                    //arryno.Add("Add New");
                    for (int i = 0; i < arryname.Count; i++)
                    {
                        arryno.Add(arryname[i].ToString());
                    }
                    dddate.DataSource = arryno;
                    dddate.DataBind();
                }


                else
                {

                    Master.ShowModal("No date available for this supplier. !!!", "ddlsupplier", 1);
                    return;


                }
            }
            else
            {
                Master.ShowModal("No Supplier available for this supplier. !!!", "ddlsupplier", 1);
                return;


            }
        }




        // ddGecode.Focus();

    }


    public void genersdate10()
    {
        if (txtdate10.Text != "")
        {
            DateTime expdate = Convert.ToDateTime(txtdate10.Text);
            string expdate10 = expdate.ToString("dd/MM/yyyy");
            string close_flag = "N";
            string supp = ddlmanufacturer.SelectedItem.Text;

            //  string p_flag = ddgrpcode.SelectedItem.Text;

            DataSet dsgroup20 = clsgd.GetcondDataSet("*", "tblmanufacture", "ManufactureName", supp);

            if (dsgroup20.Tables[0].Rows.Count > 0)
            {
                string supcode = dsgroup20.Tables[0].Rows[0]["ManufactureCode"].ToString();


                DataSet dsgroup25 = clsgd.GetcondDataSet("*", "tblpreturnProductinward", "ManufactureCode", supcode);
                //string supcode = dsgroup20.Tables[0].Rows[0]["ManufactureCode"].ToString();




                if (dsgroup25.Tables[0].Rows.Count > 0)
                {

                    DataSet dsgroup = clsgd.GetDataSet("distinct In_falg9", "tblpreturnProductinward");
                    for (int i = 0; i < dsgroup.Tables[0].Rows.Count; i++)
                    {
                        DataSet dsgroup1 = clsgd.GetcondDataSet3("*", "tblpreturnProductinward", "In_falg9", expdate10, "In_falg8", close_flag, "ManufactureCode", supcode);
                        if (dsgroup1.Tables[0].Rows.Count > 0)
                        {

                            arryname.Add(dsgroup1.Tables[0].Rows[0]["In_falg9"].ToString());
                        }
                        //else
                        //{
                        //    Master.ShowModal("No genarate record are possible. !!!", "ddlsupplier", 1);
                        //    return;
                        //}


                    }

                    arryname.Sort();
                    //arryno.Add("-Select-");
                    //arryno.Add("Add New");
                    for (int i = 0; i < arryname.Count; i++)
                    {
                        arryno.Add(arryname[i].ToString());
                    }
                    dddate.DataSource = arryno;
                    dddate.DataBind();
                }
                //else
                //{

                //      Master.ShowModal("No date available for this manufacture. !!!", "ddlsupplier", 1);
                //        return;


                //}
            }
            else
            {
                Master.ShowModal("No Manufacture available for this supplier. !!!", "ddlsupplier", 1);
                return;


            }
        }



        // ddGecode.Focus();

    }

    /*  private void BindUserDetails()
      {
          //string constr = ConfigurationManager
          //.ConnectionStrings["conString"].ConnectionString;
          //SqlConnection con = new SqlConnection(strconn11);
          //string query = "select Productcode,ProductName,Batchid,Expiredate,Totalvalues from tblProductinward";
          //SqlConnection con = new SqlConnection(constr);
          //SqlDataAdapter sda = new SqlDataAdapter(query, con);
          //DataTable dt = new DataTable();
          //sda.Fill(dt);
          //gvAll.DataSource = dt;
          //gvAll.DataBind();

          //string expdate=txtdate1.Text;

          DateTime dtEntered = Convert.ToDateTime(txtdate1.Text);
          string strEnteredDate = dtEntered.ToString("MM/dd/yyyy");

          // txtdate1.Text = calender1.Value.ToString("dd/MM/yyyy");

          if (!File.Exists(filename))
          {
              try
              {

                  gvDetails.DataSource = null;
                  gvDetails.DataBind();
                  tblProductinward.Rows.Clear();
                  SqlConnection con = new SqlConnection(strconn11);
                  string In_flag1 = "Y";
                  SqlCommand cmd = new SqlCommand("SELECT TransNo,Productcode,ProductName,Batchid,Expiredate,Stockinhand,Invoiceno,Purchaseprice,Invoicedate,Totalvalues,ManufactureName,SupplierName from tblProductinward JOIN tblmanufacture ON tblProductinward.ManufactureCode = tblmanufacture.ManufactureCode JOIN tblsuppliermaster ON tblProductinward.SuppplierCode= tblsuppliermaster.SupplierCode where Stockinhand > 0", con);
                  SqlDataAdapter da = new SqlDataAdapter(cmd);
                  DataSet ds = new DataSet();
                  da.Fill(ds);

                  if (ds.Tables[0].Rows.Count > 0)
                  {

                      DataColumn col = new DataColumn("SLNO", typeof(int));
                      col.AutoIncrement = true;
                      col.AutoIncrementSeed = 1;
                      col.AutoIncrementStep = 1;
                      tblProductinward.Columns.Add(col);
                      tblProductinward.Columns.Add("TransNo");
                      tblProductinward.Columns.Add("Productcode");
                      tblProductinward.Columns.Add("ProductName");
                      tblProductinward.Columns.Add("Batchid");
                      tblProductinward.Columns.Add("Expiredate");
                      tblProductinward.Columns.Add("Stockinhand");
                      tblProductinward.Columns.Add("Invoiceno");
                      tblProductinward.Columns.Add("Purchaseprice");
                      tblProductinward.Columns.Add("Invoicedate");
                      //tblProductinward.Columns.Add("Totalvalues");
                      tblProductinward.Columns.Add("ManufactureName");
                      tblProductinward.Columns.Add("SupplierName");

                      Session["Group"] = tblProductinward;

                      for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                      {
                          tblProductinward = (DataTable)Session["Group"];
                          drrw = tblProductinward.NewRow();
                          drrw["TransNo"] = ds.Tables[0].Rows[i]["TransNo"].ToString();                                                                       
                          drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                          drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                          drrw["Batchid"] = ds.Tables[0].Rows[i]["Batchid"].ToString();
                          drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                          drrw["Stockinhand"] = ds.Tables[0].Rows[i]["Stockinhand"].ToString();
                          drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                          drrw["Purchaseprice"] = ds.Tables[0].Rows[i]["Purchaseprice"].ToString();
                          drrw["Invoicedate"] = ds.Tables[0].Rows[i]["Invoicedate"].ToString();
                         // drrw["Totalvalues"] = ds.Tables[0].Rows[i]["Totalvalues"].ToString();
                          drrw["ManufactureName"] = ds.Tables[0].Rows[i]["ManufactureName"].ToString();
                          drrw["SupplierName"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();

                          tblProductinward.Rows.Add(drrw);
                      }
                      DataView dw = tblProductinward.DefaultView;
                      dw.Sort = "SLNO ASC";
                      gvDetails.DataSource = tblProductinward;
                      gvDetails.DataBind();
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
                  DateTime dtEntered1 = Convert.ToDateTime(txtdate1.Text);
                  string strEnteredDate1 = dtEntered.ToString("MM/dd/yyyy");

                  gvDetails.DataSource = null;
                  gvDetails.DataBind();
                  tblProductinward.Rows.Clear();
                  OleDbConnection con = new OleDbConnection(strconn11);
                  string In_flag1 = "Y";
                  OleDbCommand cmd = new OleDbCommand("select TransNo,Productcode, ProductName, Batchid, Expiredate, Stockinhand,Invoiceno,Purchaseprice,Invoicedate,Totalvalues,tblmanufacture.ManufactureName,tblsuppliermaster.SupplierName from ((tblProductinward inner join tblmanufacture on tblProductinward.ManufactureCode=CStr(tblmanufacture.ManufactureCode)) inner join tblsuppliermaster on tblProductinward.SuppplierCode= CStr(tblsuppliermaster.Suppliercode)) where Stockinhand > 0", con);
                  OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                  DataSet ds = new DataSet();
                  da.Fill(ds);

                  if (ds.Tables[0].Rows.Count > 0)
                  {

                      DataColumn col = new DataColumn("SLNO", typeof(int));
                      col.AutoIncrement = true;
                      col.AutoIncrementSeed = 1;
                      col.AutoIncrementStep = 1;
                      tblProductinward.Columns.Add(col);
                      tblProductinward.Columns.Add("TransNo");
                      tblProductinward.Columns.Add("Productcode");
                      tblProductinward.Columns.Add("ProductName");
                      tblProductinward.Columns.Add("Batchid");
                      tblProductinward.Columns.Add("Expiredate");
                      tblProductinward.Columns.Add("Stockinhand");
                      tblProductinward.Columns.Add("Invoiceno");
                      tblProductinward.Columns.Add("Purchaseprice");
                      tblProductinward.Columns.Add("Invoicedate");
                      //tblProductinward.Columns.Add("Totalvalues");
                      tblProductinward.Columns.Add("ManufactureName");
                      tblProductinward.Columns.Add("SupplierName");

                      Session["Group"] = tblProductinward;

                      for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                      {
                          tblProductinward = (DataTable)Session["Group"];
                          drrw = tblProductinward.NewRow();
                          drrw["TransNo"] = ds.Tables[0].Rows[i]["TransNo"].ToString();
                          drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                          drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                          drrw["Batchid"] = ds.Tables[0].Rows[i]["Batchid"].ToString();
                          drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                          drrw["Stockinhand"] = ds.Tables[0].Rows[i]["Stockinhand"].ToString();
                          drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                          drrw["Purchaseprice"] = ds.Tables[0].Rows[i]["Purchaseprice"].ToString();
                          drrw["Invoicedate"] = ds.Tables[0].Rows[i]["Invoicedate"].ToString();
                          // drrw["Totalvalues"] = ds.Tables[0].Rows[i]["Totalvalues"].ToString();
                          drrw["ManufactureName"] = ds.Tables[0].Rows[i]["ManufactureName"].ToString();
                          drrw["SupplierName"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();

                          tblProductinward.Rows.Add(drrw);
                      }
                      DataView dw = tblProductinward.DefaultView;
                      dw.Sort = "SLNO ASC";
                      gvDetails.DataSource = tblProductinward;
                      gvDetails.DataBind();
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



      private void Bindsupplier()
      {
          //string constr = ConfigurationManager
          //.ConnectionStrings["conString"].ConnectionString;
          //SqlConnection con = new SqlConnection(strconn11);
          //string query = "select Productcode,ProductName,Batchid,Expiredate,Totalvalues from tblProductinward";
          //SqlConnection con = new SqlConnection(constr);
          //SqlDataAdapter sda = new SqlDataAdapter(query, con);
          //DataTable dt = new DataTable();
          //sda.Fill(dt);
          //gvAll.DataSource = dt;
          //gvAll.DataBind();

          //string expdate=txtdate1.Text;

          DateTime dtEntered = Convert.ToDateTime(txtdate1.Text);
          string strEnteredDate = dtEntered.ToString("MM/dd/yyyy");

          // txtdate1.Text = calender1.Value.ToString("dd/MM/yyyy");

          if (!File.Exists(filename))
          {
              try
              {


                  gvDetails.DataSource = null;
                  gvDetails.DataBind();
                  tblProductinward.Rows.Clear();
                  SqlConnection con = new SqlConnection(strconn11);
                  string In_flag1 = "Y";
                  SqlCommand cmd = new SqlCommand("SELECT TransNo,Productcode,ProductName,Batchid,Expiredate,Stockinhand,Invoiceno,Purchaseprice,Invoicedate,Totalvalues,tblmanufacture.ManufactureName,tblsuppliermaster.SupplierName from tblProductinward JOIN tblmanufacture ON tblProductinward.ManufactureCode = tblmanufacture.ManufactureCode JOIN tblsuppliermaster ON tblProductinward.SuppplierCode= tblsuppliermaster.SupplierCode where Stockinhand > 0", con);
                  SqlDataAdapter da = new SqlDataAdapter(cmd);
                  DataSet ds = new DataSet();
                  da.Fill(ds);

                  if (ds.Tables[0].Rows.Count > 0)
                  {

                      DataColumn col = new DataColumn("SLNO", typeof(int));
                      col.AutoIncrement = true;
                      col.AutoIncrementSeed = 1;
                      col.AutoIncrementStep = 1;
                      tblProductinward.Columns.Add(col);
                      tblProductinward.Columns.Add("TransNo");
                      tblProductinward.Columns.Add("Productcode");
                      tblProductinward.Columns.Add("ProductName");
                      tblProductinward.Columns.Add("Batchid");
                      tblProductinward.Columns.Add("Expiredate");
                      tblProductinward.Columns.Add("Stockinhand");
                      tblProductinward.Columns.Add("Invoiceno");
                      tblProductinward.Columns.Add("Purchaseprice");
                      tblProductinward.Columns.Add("Invoicedate");
                     // tblProductinward.Columns.Add("Totalvalues");
                      tblProductinward.Columns.Add("ManufactureName");
                      tblProductinward.Columns.Add("SupplierName");

                      Session["Group"] = tblProductinward;

                      for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                      {
                          tblProductinward = (DataTable)Session["Group"];
                          drrw = tblProductinward.NewRow();
                          drrw["TransNo"] = ds.Tables[0].Rows[i]["TransNo"].ToString();
                          drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                          drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                          drrw["Batchid"] = ds.Tables[0].Rows[i]["Batchid"].ToString();
                          drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                          drrw["Stockinhand"] = ds.Tables[0].Rows[i]["Stockinhand"].ToString();
                          drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                          drrw["Purchaseprice"] = ds.Tables[0].Rows[i]["Purchaseprice"].ToString();
                          drrw["Invoicedate"] = ds.Tables[0].Rows[i]["Invoicedate"].ToString();
                         // drrw["Totalvalues"] = ds.Tables[0].Rows[i]["Totalvalues"].ToString();
                          drrw["ManufactureName"] = ds.Tables[0].Rows[i]["ManufactureName"].ToString();
                          drrw["SupplierName"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();

                          tblProductinward.Rows.Add(drrw);
                      }
                      DataView dw = tblProductinward.DefaultView;
                      dw.Sort = "SLNO ASC";
                      gvDetails.DataSource = tblProductinward;
                      gvDetails.DataBind();
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
                  DateTime dtEntered1 = Convert.ToDateTime(txtdate1.Text);
                  string strEnteredDate1 = dtEntered.ToString("MM/dd/yyyy");

                  gvDetails.DataSource = null;
                  gvDetails.DataBind();
                  tblProductinward.Rows.Clear();
                  OleDbConnection con = new OleDbConnection(strconn11);
                  string In_flag1 = "Y";
                  OleDbCommand cmd = new OleDbCommand("select TransNo,Productcode, ProductName, Batchid, Expiredate, Stockinhand,Invoiceno,Purchaseprice,Invoicedate,Totalvalues,tblmanufacture.ManufactureName,tblsuppliermaster.SupplierName from ((tblProductinward inner join tblmanufacture on tblProductinward.ManufactureCode=CStr(tblmanufacture.ManufactureCode)) inner join tblsuppliermaster on tblProductinward.Supppliercode= CStr(tblsuppliermaster.Suppliercode)) where Stockinhand > 0", con);
                  OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                  DataSet ds = new DataSet();
                  da.Fill(ds);

                  if (ds.Tables[0].Rows.Count > 0)
                  {

                      DataColumn col = new DataColumn("SLNO", typeof(int));
                      col.AutoIncrement = true;
                      col.AutoIncrementSeed = 1;
                      col.AutoIncrementStep = 1;
                      tblProductinward.Columns.Add(col);
                      tblProductinward.Columns.Add("TransNo");
                      tblProductinward.Columns.Add("Productcode");
                      tblProductinward.Columns.Add("ProductName");
                      tblProductinward.Columns.Add("Batchid");
                      tblProductinward.Columns.Add("Expiredate");
                      tblProductinward.Columns.Add("Stockinhand");
                      tblProductinward.Columns.Add("Invoiceno");
                      tblProductinward.Columns.Add("Purchaseprice");
                      tblProductinward.Columns.Add("Invoicedate");
                      // tblProductinward.Columns.Add("Totalvalues");
                      tblProductinward.Columns.Add("ManufactureName");
                      tblProductinward.Columns.Add("SupplierName");

                      Session["Group"] = tblProductinward;

                      for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                      {
                          tblProductinward = (DataTable)Session["Group"];
                          drrw = tblProductinward.NewRow();
                          drrw["TransNo"] = ds.Tables[0].Rows[i]["TransNo"].ToString();
                          drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                          drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                          drrw["Batchid"] = ds.Tables[0].Rows[i]["Batchid"].ToString();
                          drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                          drrw["Stockinhand"] = ds.Tables[0].Rows[i]["Stockinhand"].ToString();
                          drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                          drrw["Purchaseprice"] = ds.Tables[0].Rows[i]["Purchaseprice"].ToString();
                          drrw["Invoicedate"] = ds.Tables[0].Rows[i]["Invoicedate"].ToString();
                          // drrw["Totalvalues"] = ds.Tables[0].Rows[i]["Totalvalues"].ToString();
                          drrw["ManufactureName"] = ds.Tables[0].Rows[i]["ManufactureName"].ToString();
                          drrw["SupplierName"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();

                          tblProductinward.Rows.Add(drrw);
                      }
                      DataView dw = tblProductinward.DefaultView;
                      dw.Sort = "SLNO ASC";
                      gvDetails.DataSource = tblProductinward;
                      gvDetails.DataBind();
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
      protected void ddlsupplier_SelectedIndexChanged(object sender, EventArgs e)
      {
        

           DateTime dtEntered = Convert.ToDateTime(txtdate1.Text);
          string strEnteredDate = dtEntered.ToString("MM/dd/yyyy");

          // txtdate1.Text = calender1.Value.ToString("dd/MM/yyyy");

          if (!File.Exists(filename))
          {
              try
              {
                  string supp = ddlsupplier.SelectedItem.Text;

                  gvDetails.DataSource = null;
                  gvDetails.DataBind();
                  tblProductinward.Rows.Clear();
                  SqlConnection con = new SqlConnection(strconn11);
                  string In_flag1 = "Y";
                  SqlCommand cmd = new SqlCommand("SELECT TransNo,Productcode,ProductName,Batchid,Expiredate,Stockinhand,Invoiceno,Purchaseprice,Invoicedate,Totalvalues,ManufactureName,SupplierName from tblProductinward JOIN tblmanufacture ON tblProductinward.ManufactureCode = tblmanufacture.ManufactureCode JOIN tblsuppliermaster ON tblProductinward.Supppliercode= tblsuppliermaster.Suppliercode where Stockinhand > 0 and tblsuppliermaster.SupplierName='" + supp + "'", con);
                  SqlDataAdapter da = new SqlDataAdapter(cmd);
                  DataSet ds = new DataSet();
                  da.Fill(ds);

                  if (ds.Tables[0].Rows.Count > 0)
                  {

                      DataColumn col = new DataColumn("SLNO", typeof(int));
                      col.AutoIncrement = true;
                      col.AutoIncrementSeed = 1;
                      col.AutoIncrementStep = 1;
                      tblProductinward.Columns.Add(col);
                      tblProductinward.Columns.Add("TransNo");
                      tblProductinward.Columns.Add("Productcode");
                      tblProductinward.Columns.Add("ProductName");
                      tblProductinward.Columns.Add("Batchid");
                      tblProductinward.Columns.Add("Expiredate");
                      tblProductinward.Columns.Add("Stockinhand");
                      tblProductinward.Columns.Add("Invoiceno");
                      tblProductinward.Columns.Add("Purchaseprice");
                      tblProductinward.Columns.Add("Invoicedate");
                      //tblProductinward.Columns.Add("Totalvalues");
                      tblProductinward.Columns.Add("ManufactureName");
                      tblProductinward.Columns.Add("SupplierName");

                      Session["Group"] = tblProductinward;

                      for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                      {
                          tblProductinward = (DataTable)Session["Group"];
                          drrw = tblProductinward.NewRow();
                          drrw["TransNo"] = ds.Tables[0].Rows[i]["TransNo"].ToString();
                          drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                          drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                          drrw["Batchid"] = ds.Tables[0].Rows[i]["Batchid"].ToString();
                          drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                          drrw["Stockinhand"] = ds.Tables[0].Rows[i]["Stockinhand"].ToString();
                          drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                          drrw["Purchaseprice"] = ds.Tables[0].Rows[i]["Purchaseprice"].ToString();
                          drrw["Invoicedate"] = ds.Tables[0].Rows[i]["Invoicedate"].ToString();
                          //drrw["Totalvalues"] = ds.Tables[0].Rows[i]["Totalvalues"].ToString();
                          drrw["ManufactureName"] = ds.Tables[0].Rows[i]["ManufactureName"].ToString();
                          drrw["SupplierName"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();

                          tblProductinward.Rows.Add(drrw);
                      }
                      DataView dw = tblProductinward.DefaultView;
                      dw.Sort = "SLNO ASC";
                      gvDetails.DataSource = tblProductinward;
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
                  string supp = ddlsupplier.SelectedItem.Text;
                  DateTime dtEntered1 = Convert.ToDateTime(txtdate1.Text);
                  string strEnteredDate1 = dtEntered.ToString("MM/dd/yyyy");

                  gvDetails.DataSource = null;
                  gvDetails.DataBind();
                  tblProductinward.Rows.Clear();
                  OleDbConnection con = new OleDbConnection(strconn11);
                  string In_flag1 = "Y";
                  OleDbCommand cmd = new OleDbCommand("select TransNo,Productcode, ProductName, Batchid, Expiredate, Stockinhand,Invoiceno,Purchaseprice,Invoicedate,Totalvalues,tblmanufacture.ManufactureName,tblsuppliermaster.SupplierName from ((tblProductinward inner join tblmanufacture on tblProductinward.ManufactureCode=CStr(tblmanufacture.ManufactureCode)) inner join tblsuppliermaster on tblProductinward.Supppliercode= CStr(tblsuppliermaster.Suppliercode)) where Stockinhand > 0 and tblsuppliermaster.SupplierName='" + supp + "'", con);
                  OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                  DataSet ds = new DataSet();
                  da.Fill(ds);

                  if (ds.Tables[0].Rows.Count > 0)
                  {

                      DataColumn col = new DataColumn("SLNO", typeof(int));
                      col.AutoIncrement = true;
                      col.AutoIncrementSeed = 1;
                      col.AutoIncrementStep = 1;
                      tblProductinward.Columns.Add(col);
                      tblProductinward.Columns.Add("TransNo");
                      tblProductinward.Columns.Add("Productcode");
                      tblProductinward.Columns.Add("ProductName");
                      tblProductinward.Columns.Add("Batchid");
                      tblProductinward.Columns.Add("Expiredate");
                      tblProductinward.Columns.Add("Stockinhand");
                      tblProductinward.Columns.Add("Invoiceno");
                      tblProductinward.Columns.Add("Purchaseprice");
                      tblProductinward.Columns.Add("Invoicedate");
                      //tblProductinward.Columns.Add("Totalvalues");
                      tblProductinward.Columns.Add("ManufactureName");
                      tblProductinward.Columns.Add("SupplierName");

                      Session["Group"] = tblProductinward;

                      for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                      {
                          tblProductinward = (DataTable)Session["Group"];
                          drrw = tblProductinward.NewRow();
                          drrw["TransNo"] = ds.Tables[0].Rows[i]["TransNo"].ToString();
                          drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                          drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                          drrw["Batchid"] = ds.Tables[0].Rows[i]["Batchid"].ToString();
                          drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                          drrw["Stockinhand"] = ds.Tables[0].Rows[i]["Stockinhand"].ToString();
                          drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                          drrw["Purchaseprice"] = ds.Tables[0].Rows[i]["Purchaseprice"].ToString();
                          drrw["Invoicedate"] = ds.Tables[0].Rows[i]["Invoicedate"].ToString();
                          //drrw["Totalvalues"] = ds.Tables[0].Rows[i]["Totalvalues"].ToString();
                          drrw["ManufactureName"] = ds.Tables[0].Rows[i]["ManufactureName"].ToString();
                          drrw["SupplierName"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();


                          tblProductinward.Rows.Add(drrw);
                      }
                      DataView dw = tblProductinward.DefaultView;
                      dw.Sort = "SLNO ASC";
                      gvDetails.DataSource = tblProductinward;
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


      protected void ddlmanufacturer_SelectedIndexChanged(object sender, EventArgs e)
      {


          DateTime dtEntered = Convert.ToDateTime(txtdate1.Text);
          string strEnteredDate = dtEntered.ToString("MM/dd/yyyy");

          // txtdate1.Text = calender1.Value.ToString("dd/MM/yyyy");

          if (!File.Exists(filename))
          {
              try
              {
                  string manu = ddlmanufacturer.SelectedItem.Text;

                  gvDetails.DataSource = null;
                  gvDetails.DataBind();
                  tblProductinward.Rows.Clear();
                  SqlConnection con = new SqlConnection(strconn11);
                  string In_flag1 = "Y";
                  SqlCommand cmd = new SqlCommand("SELECT TransNo,Productcode,ProductName,Batchid,Expiredate,Stockinhand,Invoiceno,Purchaseprice,Invoicedate,Totalvalues,ManufactureName,SupplierName from tblProductinward JOIN tblmanufacture ON tblProductinward.ManufactureCode = tblmanufacture.ManufactureCode JOIN tblsuppliermaster ON tblProductinward.Supppliercode= tblsuppliermaster.Suppliercode where Stockinhand > 0 and ManufactureName='" + manu + "'", con);
                  SqlDataAdapter da = new SqlDataAdapter(cmd);
                  DataSet ds = new DataSet();
                  da.Fill(ds);

                  if (ds.Tables[0].Rows.Count > 0)
                  {

                      DataColumn col = new DataColumn("SLNO", typeof(int));
                      col.AutoIncrement = true;
                      col.AutoIncrementSeed = 1;
                      col.AutoIncrementStep = 1;
                      tblProductinward.Columns.Add(col);
                      tblProductinward.Columns.Add("TransNo");
                      tblProductinward.Columns.Add("Productcode");
                      tblProductinward.Columns.Add("ProductName");
                      tblProductinward.Columns.Add("Batchid");
                      tblProductinward.Columns.Add("Expiredate");
                      tblProductinward.Columns.Add("Stockinhand");
                      tblProductinward.Columns.Add("Invoiceno");
                      tblProductinward.Columns.Add("Purchaseprice");
                      tblProductinward.Columns.Add("Invoicedate");
                     // tblProductinward.Columns.Add("Totalvalues");
                      tblProductinward.Columns.Add("ManufactureName");
                      tblProductinward.Columns.Add("SupplierName");

                      Session["Group"] = tblProductinward;

                      for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                      {
                          tblProductinward = (DataTable)Session["Group"];
                          drrw = tblProductinward.NewRow();
                          drrw["TransNo"] = ds.Tables[0].Rows[i]["TransNo"].ToString();
                          drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                          drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                          drrw["Batchid"] = ds.Tables[0].Rows[i]["Batchid"].ToString();
                          drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                          drrw["Stockinhand"] = ds.Tables[0].Rows[i]["Stockinhand"].ToString();
                          drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                          drrw["Purchaseprice"] = ds.Tables[0].Rows[i]["Purchaseprice"].ToString();
                          drrw["Invoicedate"] = ds.Tables[0].Rows[i]["Invoicedate"].ToString();
                          //drrw["Totalvalues"] = ds.Tables[0].Rows[i]["Totalvalues"].ToString();
                          drrw["ManufactureName"] = ds.Tables[0].Rows[i]["ManufactureName"].ToString();
                          drrw["SupplierName"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();

                          tblProductinward.Rows.Add(drrw);
                      }
                      DataView dw = tblProductinward.DefaultView;
                      dw.Sort = "SLNO ASC";
                      gvDetails.DataSource = tblProductinward;
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
                  string manu = ddlmanufacturer.SelectedItem.Text;
                  string supp = ddlsupplier.SelectedItem.Text;
                  DateTime dtEntered1 = Convert.ToDateTime(txtdate1.Text);
                  string strEnteredDate1 = dtEntered.ToString("MM/dd/yyyy");

                  gvDetails.DataSource = null;
                  gvDetails.DataBind();
                  tblProductinward.Rows.Clear();
                  OleDbConnection con = new OleDbConnection(strconn11);
                  string In_flag1 = "Y";
                  OleDbCommand cmd = new OleDbCommand("select TransNo,Productcode, ProductName, Batchid, Expiredate, Stockinhand,Invoiceno,Purchaseprice,Invoicedate,Totalvalues,tblmanufacture.ManufactureName,tblsuppliermaster.SupplierName from ((tblProductinward inner join tblmanufacture on tblProductinward.ManufactureCode=CStr(tblmanufacture.ManufactureCode)) inner join tblsuppliermaster on tblProductinward.Supppliercode= CStr(tblsuppliermaster.Suppliercode)) where Stockinhand > 0 and  ManufactureName='" + manu + "'", con);
                  OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                  DataSet ds = new DataSet();
                  da.Fill(ds);

                  if (ds.Tables[0].Rows.Count > 0)
                  {

                      DataColumn col = new DataColumn("SLNO", typeof(int));
                      col.AutoIncrement = true;
                      col.AutoIncrementSeed = 1;
                      col.AutoIncrementStep = 1;
                      tblProductinward.Columns.Add(col);
                      tblProductinward.Columns.Add("TransNo");
                      tblProductinward.Columns.Add("Productcode");
                      tblProductinward.Columns.Add("ProductName");
                      tblProductinward.Columns.Add("Batchid");
                      tblProductinward.Columns.Add("Expiredate");
                      tblProductinward.Columns.Add("Stockinhand");
                      tblProductinward.Columns.Add("Invoiceno");
                      tblProductinward.Columns.Add("Purchaseprice");
                      tblProductinward.Columns.Add("Invoicedate");
                      // tblProductinward.Columns.Add("Totalvalues");
                      tblProductinward.Columns.Add("ManufactureName");
                      tblProductinward.Columns.Add("SupplierName");

                      Session["Group"] = tblProductinward;

                      for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                      {
                          tblProductinward = (DataTable)Session["Group"];
                          drrw = tblProductinward.NewRow();
                          drrw["TransNo"] = ds.Tables[0].Rows[i]["TransNo"].ToString();
                          drrw["Productcode"] = ds.Tables[0].Rows[i]["Productcode"].ToString();
                          drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                          drrw["Batchid"] = ds.Tables[0].Rows[i]["Batchid"].ToString();
                          drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                          drrw["Stockinhand"] = ds.Tables[0].Rows[i]["Stockinhand"].ToString();
                          drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                          drrw["Purchaseprice"] = ds.Tables[0].Rows[i]["Purchaseprice"].ToString();
                          drrw["Invoicedate"] = ds.Tables[0].Rows[i]["Invoicedate"].ToString();
                          //drrw["Totalvalues"] = ds.Tables[0].Rows[i]["Totalvalues"].ToString();
                          drrw["ManufactureName"] = ds.Tables[0].Rows[i]["ManufactureName"].ToString();
                          drrw["SupplierName"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();


                          tblProductinward.Rows.Add(drrw);
                      }
                      DataView dw = tblProductinward.DefaultView;
                      dw.Sort = "SLNO ASC";
                      gvDetails.DataSource = tblProductinward;
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
      protected void btnsave_Click(object sender, EventArgs e)
      {

          {

              if (!File.Exists(filename))
              {
               
                  foreach (GridViewRow gvrow in gvDetails.Rows)
                  {
                   
                      CheckBox chkdelete = (CheckBox)gvrow.FindControl("chkSelect");
                      //Condition to check checkbox selected or not
                      if (chkdelete != null & chkdelete.Checked)
                      {
                          if (rdtrans.SelectedIndex == 0)
                          {
                              if (ddlsupplier.SelectedItem.Text == "-Select-")
                              {
                                  Master.ShowModal("Please Select SupplierName", "ddlsupplier", 1);
                                  return;
                              }
                          }
                          if (rdtrans.SelectedIndex == 1)
                          {
                              if (ddlmanufacturer.SelectedItem.Text == "-Select-")
                              {
                                  Master.ShowModal("Please Select Manufacture Name", "ddlmanufacturer", 1);
                                  return;
                              }
                          }
                        
                         
                          //int Transno = Convert.ToInt32(gvDetails.DataKeys[gvrow.RowIndex].Value);
                          string Transno =Convert.ToString (gvDetails.DataKeys[gvrow.RowIndex].Value);
                          string Returnperson = rdtrans.SelectedValue;
                          string Suppliername = ddlsupplier.SelectedItem.Text;
                          string Manufacturename = ddlmanufacturer.SelectedItem.Text;
                          string Productcode = gvrow.Cells[4].Text;
                          string ProductName = gvrow.Cells[5].Text;
                          string Batchid = gvrow.Cells[6].Text;
                          string Expiredate = gvrow.Cells[7].Text;
                          string Stockinhand = gvrow.Cells[8].Text;
                          string Invoiceno = gvrow.Cells[9].Text;
                          string Purchaseprice = gvrow.Cells[10].Text;
                          //string Chequedate = gvDetails.Rows[1].Cells[8].Text;
                          string Invoicedate = gvrow.Cells[11].Text;



                          //string TransNo1 = Convert.ToString((gvDetails.Rows[0].Cells[1].FindControl("txtproductcode") as TextBox).Text);
                          using (SqlConnection con = new SqlConnection(strconn11))
                          {
                              con.Open();

                              System.DateTime Dtnow = DateTime.Now;
                              string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                           
                              //string In_flag1 = "N";
                              //SqlCommand cmd20 = new SqlCommand("UPDATE tblProductinward SET  In_falg1='" + In_flag1 + "' WHERE  TransNo ='" + TransNo1 + "'", con);
                              //cmd20.ExecuteNonQuery();
                              if (rdtrans.SelectedIndex == -1)
                              {

                                  ClsBLGP.PurchaseReturn("INSERT_PURCHASERETURN", Returnperson, "0", "0", Transno, Productcode, ProductName, Batchid, Expiredate, Stockinhand, Invoiceno, Purchaseprice, Invoicedate, Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                              }

                              if (rdtrans.SelectedIndex == 0)
                              {

                                  ClsBLGP.PurchaseReturn("INSERT_PURCHASERETURN", Returnperson, "0", Suppliername, Transno, Productcode, ProductName, Batchid, Expiredate, Stockinhand, Invoiceno, Purchaseprice, Invoicedate, Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                              }

                              if (rdtrans.SelectedIndex == 1)
                            
                              {
                                  ClsBLGP.PurchaseReturn("INSERT_PURCHASERETURN", Returnperson, Manufacturename, "0", Transno, Productcode, ProductName, Batchid, Expiredate, Stockinhand, Invoiceno, Purchaseprice, Invoicedate, Session["username"].ToString(), sqlFormattedDate, sMacAddress);

                              }

                              lblsuccess.Visible = true;
                              lblsuccess.Text = "inserted successfully";

                              ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                            
                           
                              con.Close();
                          }
                      }
                  }

                  SqlConnection con10 = new SqlConnection(strconn11);
                  SqlCommand cmd10 = new SqlCommand("select * from tblPurchasereturn", con10);
                  SqlDataAdapter da10 = new SqlDataAdapter(cmd10);
                  DataSet ds10 = new DataSet();
                  da10.Fill(ds10);

                  string Purchaseprice1 = ds10.Tables[0].Rows[0]["Purchaseprice"].ToString();
                  string Suppliername1 = ddlsupplier.SelectedItem.Text;
                 // string p_flag2 = ddgrpcode.SelectedItem.Text;

                  DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", Suppliername1);

                  string Suppliercode1 = dsgroup10.Tables[0].Rows[0]["SupplierCode"].ToString();

                  if (rdtrans.SelectedIndex == -1)
                  {
                      System.DateTime Dtnow = DateTime.Now;
                      string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                      ClsBLGP2.Transaction("INSERT_TRANSACTION", "1111", sqlFormattedDate, "0000", "0000", "9998", "N", "0000", "1234", Purchaseprice1, "0000.00", "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                   
                  }

                  if (rdtrans.SelectedIndex == 0)
                  {
                      System.DateTime Dtnow = DateTime.Now;
                      string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                      ClsBLGP2.Transaction("INSERT_TRANSACTION", "1111", sqlFormattedDate, "0000", Suppliercode1, "9998", "N", "0000", "1234", Purchaseprice1, "0000.00", "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);
                  
                  }

                  if (rdtrans.SelectedIndex == 1)
                  {
                      System.DateTime Dtnow = DateTime.Now;
                      string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");
                      ClsBLGP2.Transaction("INSERT_TRANSACTION", "1111", sqlFormattedDate, "0000", "0000", "9998", "N", "0000", "1234", Purchaseprice1, "0000.00", "0000.00", "0000.00", "0000.00", "0000.00", Session["username"].ToString(), sqlFormattedDate, sMacAddress);

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
                          string Transno = Convert.ToString(gvDetails.DataKeys[gvrow.RowIndex].Value);
                          string Returnperson = rdtrans.SelectedValue;
                          string Suppliername = ddlsupplier.SelectedItem.Text;
                          string Manufacturename = ddlmanufacturer.SelectedItem.Text;
                          string Productcode = gvrow.Cells[4].Text;
                          string ProductName = gvrow.Cells[5].Text;
                          string Batchid = gvrow.Cells[6].Text;
                          string Expiredate = gvrow.Cells[7].Text;
                          string Stockinhand = gvrow.Cells[8].Text;
                          string Invoiceno = gvrow.Cells[9].Text;
                          string Purchaseprice = gvrow.Cells[10].Text;
                          //string Chequedate = gvDetails.Rows[1].Cells[8].Text;
                          string Invoicedate = gvrow.Cells[11].Text;

                          System.DateTime Dtnow = DateTime.Now;
                          string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");

                          OleDbConnection con = new OleDbConnection(strconn11);
                          con.Open();
                          OleDbCommand cmd = new OleDbCommand("insert into tblPurchasereturn(Returnperson,Manufacturename,Suppliername,Transno,Productcode,ProductName,Batchid,Expiredate,Stockinhand,Invoiceno,Purchaseprice,Invoicedate,Login_name,Mac_id,Sysdatetime) values('" + Returnperson + "','" + Suppliername + "','" + Manufacturename + "','" + Transno + "','" + Productcode + "','" + ProductName + "','" + Batchid + "','" + Expiredate + "','" + Stockinhand + "','" + Invoiceno + "','" + Purchaseprice + "','" + Invoicedate + "','" + Session["username"].ToString() + "','" + sMacAddress + "','" + sqlFormattedDate + "')", con);
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
      protected void btnExit_Click(object sender, EventArgs e)
      {
          Response.Redirect("Home.aspx");
      }


      protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
      {
        
          foreach (GridViewRow gvrow in gvDetails.Rows)
          {
              //CheckBox chkdelete = (CheckBox)gvrow.FindControl("chkSelect");
              //Condition to check checkbox selected or not
            
                  //string Transno = gvDetails.Rows[0].Cells[3].Text;
                  //Session["Transno"] = gvDetails.Rows[0].Cells[3].Text;
              Session["Transno"] = Convert.ToString(gvDetails.DataKeys[gvrow.RowIndex].Value);
                  this.ModalPopupExtender2.Enabled = true;
                  ModalPopupExtender2.Show();
            
          }

      }*/










    protected void Button1_Click(object sender, EventArgs e)
    {
        if (rdtrans.SelectedIndex == 0)
        {

            if (ddlsupplier.SelectedItem.Text == "-Select-")
            {
                Master.ShowModal("Please select a supplier name. !!!", "ddlsupplier", 1);
                return;
            }

            if (txtda.Text == "")
            {
                Master.ShowModal("Please enter date. !!!", "txtda", 1);
                return;
            }




            string expdate = txtda.Text;
            DateTime dt = Convert.ToDateTime(expdate);
            string expdate10 = dt.ToString("MM/dd/yyyy");


            string p_flag2 = ddlsupplier.SelectedItem.Text;

            DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", p_flag2);

            string flag2 = dsgroup10.Tables[0].Rows[0]["SupplierCode"].ToString();


            DataSet dsgroup15 = clsgd.GetcondDataSet("*", "tblProductinward", "SuppplierCode", flag2);

            if (dsgroup15.Tables[0].Rows.Count > 0)
            {



                SqlConnection conn = new SqlConnection(strconn11);
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblProductinward  where tblProductinward.Expiredate <='" + expdate10 + "' and tblProductinward.Stockinhand>0  and tblProductinward.SuppplierCode='" + flag2 + "'", conn);
                SqlDataAdapter da30 = new SqlDataAdapter(cmd);
                DataSet ds30 = new DataSet();
                da30.Fill(ds30);


                if (ds30.Tables[0].Rows.Count > 0)
                {


                    // Now you have a collection of rows that you can iterate over
                    for (int i = 0; i < ds30.Tables[0].Rows.Count; i++)
                    {

                        //string Invoiceno = row["Invoiceno"].ToString();
                        string Invoiceno = ds30.Tables[0].Rows[i]["Invoiceno"].ToString();
                        string Invoicedate = ds30.Tables[0].Rows[i]["Invoicedate"].ToString();
                        string Paymenttype = ds30.Tables[0].Rows[i]["Paymenttype"].ToString();
                        string Paymentflag = ds30.Tables[0].Rows[i]["Paymentflag"].ToString();

                        string SupplierCode = ds30.Tables[0].Rows[i]["SuppplierCode"].ToString();

                        string Indate = ds30.Tables[0].Rows[i]["Indate"].ToString();

                        string Productcode = ds30.Tables[0].Rows[i]["Productcode"].ToString();

                        string ProductName = ds30.Tables[0].Rows[i]["ProductName"].ToString();

                        string g_code = ds30.Tables[0].Rows[i]["g_code"].ToString();

                        string GN_code = ds30.Tables[0].Rows[i]["GN_code"].ToString();
                        string CC_code = ds30.Tables[0].Rows[i]["CC_code"].ToString();
                        string FA_code = ds30.Tables[0].Rows[i]["FA_code"].ToString();
                        string unitcode = ds30.Tables[0].Rows[i]["unitcode"].ToString();
                        string formcode = ds30.Tables[0].Rows[i]["formcode"].ToString();
                        string ManufactureCode = ds30.Tables[0].Rows[i]["ManufactureCode"].ToString();
                        string se_code = ds30.Tables[0].Rows[i]["se_code"].ToString();
                        string Rack = ds30.Tables[0].Rows[i]["Rack"].ToString();
                        string Supliercode = ds30.Tables[0].Rows[i]["Supliercode"].ToString();

                        string Freesupply = ds30.Tables[0].Rows[i]["Freesupply"].ToString();

                        string Tax = ds30.Tables[0].Rows[i]["Tax"].ToString();
                        string Stockinward = ds30.Tables[0].Rows[i]["Stockinward"].ToString();
                        string Stockinhand = ds30.Tables[0].Rows[i]["Stockinhand"].ToString();
                        string Batchid = ds30.Tables[0].Rows[i]["Batchid"].ToString();
                        string Expiredate = ds30.Tables[0].Rows[i]["Expiredate"].ToString();
                        string Purchaseprice = ds30.Tables[0].Rows[i]["Purchaseprice"].ToString();
                        string MRP = ds30.Tables[0].Rows[i]["MRP"].ToString();
                        string Totalvalues = ds30.Tables[0].Rows[i]["Totalvalues"].ToString();
                        string Taxamount = ds30.Tables[0].Rows[i]["Taxamount"].ToString();
                        string Narration = ds30.Tables[0].Rows[i]["Narration"].ToString();
                        string Sellprice = ds30.Tables[0].Rows[i]["Sellprice"].ToString();
                        string Login_name = ds30.Tables[0].Rows[i]["Login_name"].ToString();
                        string Sysdatetime = ds30.Tables[0].Rows[i]["Sysdatetime"].ToString();
                        string Mac_id = ds30.Tables[0].Rows[i]["Mac_id"].ToString();
                        string taxable = ds30.Tables[0].Rows[i]["taxable"].ToString();
                        string In_falg2 = ds30.Tables[0].Rows[i]["In_falg2"].ToString();
                        string In_falg3 = ds30.Tables[0].Rows[i]["In_falg3"].ToString();
                        string In_falg4 = ds30.Tables[0].Rows[i]["In_falg4"].ToString();
                        string In_falg5 = ds30.Tables[0].Rows[i]["In_falg5"].ToString();
                        string In_falg6 = ds30.Tables[0].Rows[i]["In_falg6"].ToString();
                        string In_falg7 = ds30.Tables[0].Rows[i]["In_falg7"].ToString();
                        string In_falg8 = ds30.Tables[0].Rows[i]["In_falg8"].ToString();
                        string In_falg9 = ds30.Tables[0].Rows[i]["In_falg9"].ToString();
                        string In_falg10 = ds30.Tables[0].Rows[i]["In_falg10"].ToString();

                        System.DateTime Dtnow = DateTime.Now;
                        string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");

                        string gndate = txtda.Text;

                        ClsBLGP.TempProductinward("INSERT_PreturnProductinward", Invoiceno, Invoicedate, Paymenttype, Paymentflag, SupplierCode, Indate, Productcode, ProductName, g_code, GN_code, CC_code, FA_code, unitcode, formcode, ManufactureCode, se_code, Rack, "0", Freesupply, Tax, Stockinward, Stockinhand, Batchid, Expiredate, Purchaseprice, MRP, Totalvalues, Taxamount, Narration, Sellprice, Session["username"].ToString(), sqlFormattedDate, sMacAddress, taxable, "00", "00", "00", "00", "00", "00", "N", gndate, "Y");

                        lblsuccess.Visible = true;
                        lblsuccess.Text = "inserted successfully";

                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);


                        ddlsupplier.ClearSelection();


                    }
                }
                else
                {
                    Master.ShowModal("expiry medicine not available. !!!", "ddlsupplier", 1);
                    return;

                }
            }
            else
            {

                Master.ShowModal("No Product available for this supplier. !!!", "ddlsupplier", 1);
                return;


            }


       }
        else
        {
            if (ddlmanufacturer.SelectedItem.Text == "-Select-")
            {
                Master.ShowModal("Please select a Manufacture  Name. !!!", "ddlsupplier", 1);
                return;
            }

            if (txtdate10.Text == "")
            {
                Master.ShowModal("Please enter date. !!!", "txtdate10", 1);
                return;
            }




            string expdate = txtdate10.Text;
            DateTime dt = Convert.ToDateTime(expdate);
            string expdate10 = dt.ToString("MM/dd/yyyy");


            string p_flag3 = ddlmanufacturer.SelectedItem.Text;

            DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblmanufacture", "ManufactureName", p_flag3);

            string flag4 = dsgroup10.Tables[0].Rows[0]["ManufactureCode"].ToString();


            DataSet dsgroup15 = clsgd.GetcondDataSet("*", "tblProductinward", "ManufactureCode", flag4);

            if (dsgroup15.Tables[0].Rows.Count > 0)
            {



                SqlConnection conn = new SqlConnection(strconn11);
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblProductinward INNER JOIN tblGroup ON tblProductinward.g_code=tblGroup.g_code where tblProductinward.Expiredate <='" + expdate10 + "' and tblProductinward.Stockinhand>0 and tblGroup.p_flag='Y' and tblProductinward.ManufactureCode='" + flag4 + "'", conn);
                // DataTable table = new DataTable();
                //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //adapter.Fill(table);

                SqlDataAdapter da30 = new SqlDataAdapter(cmd);
                DataSet ds30 = new DataSet();
                da30.Fill(ds30);

                if (ds30.Tables[0].Rows.Count > 0)
                {


                    // Now you have a collection of rows that you can iterate over
                    for (int i = 0; i < ds30.Tables[0].Rows.Count; i++)
                    {

                        //string Invoiceno = row["Invoiceno"].ToString();
                        string Invoiceno = ds30.Tables[0].Rows[i]["Invoiceno"].ToString();
                        string Invoicedate = ds30.Tables[0].Rows[i]["Invoicedate"].ToString();
                        string Paymenttype = ds30.Tables[0].Rows[i]["Paymenttype"].ToString();
                        string Paymentflag = ds30.Tables[0].Rows[i]["Paymentflag"].ToString();

                        string SupplierCode = ds30.Tables[0].Rows[i]["SuppplierCode"].ToString();

                        string Indate = ds30.Tables[0].Rows[i]["Indate"].ToString();

                        string Productcode = ds30.Tables[0].Rows[i]["Productcode"].ToString();

                        string ProductName = ds30.Tables[0].Rows[i]["ProductName"].ToString();

                        string g_code = ds30.Tables[0].Rows[i]["g_code"].ToString();

                        string GN_code = ds30.Tables[0].Rows[i]["GN_code"].ToString();
                        string CC_code = ds30.Tables[0].Rows[i]["CC_code"].ToString();
                        string FA_code = ds30.Tables[0].Rows[i]["FA_code"].ToString();
                        string unitcode = ds30.Tables[0].Rows[i]["unitcode"].ToString();
                        string formcode = ds30.Tables[0].Rows[i]["formcode"].ToString();
                        string ManufactureCode = ds30.Tables[i].Rows[0]["ManufactureCode"].ToString();
                        string se_code = ds30.Tables[0].Rows[i]["se_code"].ToString();
                        string Rack = ds30.Tables[0].Rows[i]["Rack"].ToString();
                        string Supliercode = ds30.Tables[0].Rows[i]["Supliercode"].ToString();

                        string Freesupply = ds30.Tables[0].Rows[i]["Freesupply"].ToString();

                        string Tax = ds30.Tables[0].Rows[i]["Tax"].ToString();
                        string Stockinward = ds30.Tables[0].Rows[i]["Stockinward"].ToString();
                        string Stockinhand = ds30.Tables[0].Rows[i]["Stockinhand"].ToString();
                        string Batchid = ds30.Tables[0].Rows[i]["Batchid"].ToString();
                        string Expiredate = ds30.Tables[0].Rows[i]["Expiredate"].ToString();
                        string Purchaseprice = ds30.Tables[0].Rows[i]["Purchaseprice"].ToString();
                        string MRP = ds30.Tables[0].Rows[i]["MRP"].ToString();
                        string Totalvalues = ds30.Tables[0].Rows[i]["Totalvalues"].ToString();
                        string Taxamount = ds30.Tables[0].Rows[i]["Taxamount"].ToString();
                        string Narration = ds30.Tables[0].Rows[i]["Narration"].ToString();
                        string Sellprice = ds30.Tables[0].Rows[i]["Sellprice"].ToString();
                        string Login_name = ds30.Tables[0].Rows[i]["Login_name"].ToString();
                        string Sysdatetime = ds30.Tables[0].Rows[i]["Sysdatetime"].ToString();
                        string Mac_id = ds30.Tables[0].Rows[i]["Mac_id"].ToString();
                        string taxable = ds30.Tables[0].Rows[i]["taxable"].ToString();
                        string In_falg2 = ds30.Tables[0].Rows[i]["In_falg2"].ToString();
                        string In_falg3 = ds30.Tables[0].Rows[i]["In_falg3"].ToString();
                        string In_falg4 = ds30.Tables[0].Rows[i]["In_falg4"].ToString();
                        string In_falg5 = ds30.Tables[0].Rows[i]["In_falg5"].ToString();
                        string In_falg6 = ds30.Tables[0].Rows[i]["In_falg6"].ToString();
                        string In_falg7 = ds30.Tables[0].Rows[i]["In_falg7"].ToString();
                        string In_falg8 = ds30.Tables[0].Rows[i]["In_falg8"].ToString();
                        string In_falg9 = ds30.Tables[0].Rows[i]["In_falg9"].ToString();
                        string In_falg10 = ds30.Tables[0].Rows[i]["In_falg10"].ToString();

                        System.DateTime Dtnow = DateTime.Now;
                        string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");

                        string gndate = txtdate10.Text;

                        //  ClsBLGP.TempProductinward("INSERT_PreturnProductinward", Invoiceno, Invoicedate, Paymenttype, Paymentflag, SupplierCode, Indate, Productcode, ProductName, g_code, GN_code, CC_code, FA_code, unitcode, formcode, ManufactureCode, se_code, Rack, "0", Freesupply, Tax, Stockinward, Stockinhand, Batchid, Expiredate, Purchaseprice, MRP, Totalvalues, Taxamount, Narration, Sellprice, Session["username"].ToString(), sqlFormattedDate, sMacAddress, taxable, In_falg2, In_falg3, "Y", "Y", "Y", "Y", "Y", "Y", "Y");

                        ClsBLGP.TempProductinward("INSERT_PreturnProductinward", Invoiceno, Invoicedate, Paymenttype, Paymentflag, SupplierCode, Indate, Productcode, ProductName, g_code, GN_code, CC_code, FA_code, unitcode, formcode, ManufactureCode, se_code, Rack, "0", Freesupply, Tax, Stockinward, Stockinhand, Batchid, Expiredate, Purchaseprice, MRP, Totalvalues, Taxamount, Narration, Sellprice, Session["username"].ToString(), sqlFormattedDate, sMacAddress, taxable, "00", "00", "00", "00", "00", "00", "N", gndate, "Y");
                        lblsuccess.Visible = true;
                        lblsuccess.Text = "inserted successfully";

                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

                        ddlmanufacturer.ClearSelection();


                    }
                }
                else
                {
                    Master.ShowModal("expiry medicine not available. !!!", "ddlsupplier", 1);
                    return;

                }
            }
            else
            {

                Master.ShowModal("No Product available for this supplier. !!!", "ddlsupplier", 1);
                return;

            }
        }





    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < Gridreturn.Rows.Count; i++)
        {
            TextBox txt1 = (TextBox)Gridreturn.Rows[i].Cells[0].FindControl("Return_Qty");
            string pname = Gridreturn.Rows[i].Cells[0].Text;
            string batchno = Gridreturn.Rows[i].Cells[1].Text;
            string invcno = Gridreturn.Rows[i].Cells[2].Text;

            string ret15 = txt1.Text;






            if (ret15 != "0")
            {
                int ret = Convert.ToInt32(txt1.Text);


                SqlConnection con = new SqlConnection(strconn11);
                con.Open();
                SqlCommand cmd = new SqlCommand("select Stockinhand from tblProductinward WHERE ProductName= '" + pname + "' and Batchid='" + batchno + "' and Invoiceno='" + invcno + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string pstock = ds.Tables[0].Rows[0]["Stockinhand"].ToString();

                int pstock10 = Convert.ToInt32(pstock);

                string upstock = Convert.ToString(pstock10 - ret);

                SqlConnection conn52 = new SqlConnection(strconn11);
                conn52.Open();
                SqlCommand cmd52 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand ='" + upstock + "',In_falg10='Y',In_falg9='Y'  WHERE ProductName= '" + pname + "' and Batchid='" + batchno + "' and Invoiceno='" + invcno + "'", conn52);
                cmd52.ExecuteNonQuery();

                SqlConnection conn53 = new SqlConnection(strconn11);
                conn53.Open();
                SqlCommand cmd53 = new SqlCommand("UPDATE tblpreturnProductinward SET  In_falg8 ='Y' WHERE ProductName= '" + pname + "' and Batchid='" + batchno + "' and Invoiceno='" + invcno + "'", conn52);
                cmd53.ExecuteNonQuery();
            }
        }

        if (rdtrans.SelectedIndex == 0)
        {

            string p_flag2 = ddlsupplier.SelectedItem.Text;

            DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", p_flag2);

            string flag2 = dsgroup10.Tables[0].Rows[0]["SupplierCode"].ToString();
            System.DateTime Dtnow = DateTime.Now;
            string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");

            string p_flag20 = ddlsupplier.SelectedItem.Text;

            DataSet dsgroup15 = clsgd.GetcondDataSet("*", "tblSuspenseAsset", "SupplierCode", flag2);

            if (dsgroup15.Tables[0].Rows.Count <= 0)
            {

                ClsBLGP10.SuspenseAsset("INSERT_tblSuspenseAsset", flag2, p_flag2, "Debit", "AD", txtamount.Text, "0", sqlFormattedDate, txtamount.Text, "N", "N", "N", "N", "N");
            }
            else
            {
                string p_flag25 = ddlsupplier.SelectedItem.Text;

                DataSet dsgroup18 = clsgd.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", p_flag25);

                string flag20 = dsgroup10.Tables[0].Rows[0]["SupplierCode"].ToString();

                DataSet dsgroup50 = clsgd.GetcondDataSet("*", "tblSuspenseAsset", "SupplierCode", flag20);

                string amt = dsgroup50.Tables[0].Rows[0]["Amount"].ToString();

                double amt20 = Convert.ToDouble(amt);

                string amt25 = txtamount.Text;

                double amt50 = Convert.ToDouble(amt25);

                double famt = amt20 + amt50;

                string famt20 = Convert.ToString(famt);

                ClsBLGP10.SuspenseAsset("INSERT_tblSuspenseAsset", flag2, p_flag2, "Debit", "AD", txtamount.Text, "0", sqlFormattedDate, famt20, "N", "N", "N", "N", "N");

            }
        }

        if (rdtrans.SelectedIndex == 0)
        {
            binddetails();
           // ddlsupplier.ClearSelection();
            dddate.ClearSelection();
            txtamount.Text = string.Empty;
        }
        else
        {
            binddetails10();
            //ddlmanufacturer.ClearSelection();
            dddate.ClearSelection();
            txtamount.Text = string.Empty;
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {

        Response.Redirect("Home.aspx");
    }

    public void binddetails()
    {
        if (!File.Exists(filename))
        {
            try
            {
                string supp = ddlsupplier.SelectedItem.Text;

                DataSet dsgroup20 = clsgd.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", supp);
                string supcode = dsgroup20.Tables[0].Rows[0]["SupplierCode"].ToString();

                Gridreturn.DataSource = null;
                Gridreturn.DataBind();
                tblProductinward.Rows.Clear();
                SqlConnection con = new SqlConnection(strconn11);
                string In_flag1 = "Y";
                DateTime expdate5 = Convert.ToDateTime(txtda.Text);
                string expdate = expdate5.ToString("dd/MM/yyyy");
                SqlCommand cmd = new SqlCommand("SELECT a.Productcode,a.ProductName,a.Batchid,a.Invoiceno,a.Expiredate,a.PStockinhand,a.Purchaseprice,ManufactureName,SupplierName  from tblpreturnProductinward a LEFT JOIN tblmanufacture ON a.ManufactureCode = tblmanufacture.ManufactureCode LEFT JOIN tblsuppliermaster ON a.Supppliercode= tblsuppliermaster.Suppliercode  where a.In_falg9='" + expdate + "' and a.SuppplierCode='" + supcode + "' and a.In_falg8='N'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);



                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblProductinward.Columns.Add(col);
                    tblProductinward.Columns.Add("ProductName");
                    tblProductinward.Columns.Add("Batchid");
                    tblProductinward.Columns.Add("Invoiceno");
                    tblProductinward.Columns.Add("Expiredate");
                    tblProductinward.Columns.Add("PStockinhand");
                    tblProductinward.Columns.Add("Purchaseprice");
                    tblProductinward.Columns.Add("ManufactureName");
                    tblProductinward.Columns.Add("SupplierName");
                    // tblProductinward.Columns.Add("Totalvalues");
                    tblProductinward.Columns.Add("Productinhand");
                    tblProductinward.Columns.Add("ReturnQuantity");
                    tblProductinward.Columns.Add("CurrentDate");



                    Session["Group"] = tblProductinward;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        string flag = ds.Tables[0].Rows[i]["Productcode"].ToString();

                        SqlConnection con10 = new SqlConnection(strconn11);
                       // string In_flag1 = "Y";
                        DateTime expdate50 = Convert.ToDateTime(txtda.Text);
                        string expdate10 = expdate50.ToString("dd/MM/yyyy");
                        SqlCommand cmd50 = new SqlCommand("SELECT Stockinhand as Productinhand from tblProductinward  where Productcode='" + flag + "'", con10);
                        SqlDataAdapter da50 = new SqlDataAdapter(cmd50);
                        DataSet ds50 = new DataSet();
                        da50.Fill(ds50);

                        if (ds50.Tables[0].Rows.Count > 0)
                        {


                            tblProductinward = (DataTable)Session["Group"];
                            drrw = tblProductinward.NewRow();
                            drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                            drrw["Batchid"] = ds.Tables[0].Rows[i]["Batchid"].ToString();
                            drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                            drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                            drrw["PStockinhand"] = ds.Tables[0].Rows[i]["PStockinhand"].ToString();
                            drrw["Purchaseprice"] = ds.Tables[0].Rows[i]["Purchaseprice"].ToString();
                            drrw["ManufactureName"] = ds.Tables[0].Rows[i]["ManufactureName"].ToString();
                            drrw["SupplierName"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();
                            drrw["Productinhand"] = ds50.Tables[0].Rows[0]["Productinhand"].ToString();
                            drrw["ReturnQuantity"] = "0";
                            string Sysdatetime = DateTime.Now.ToString();
                            drrw["CurrentDate"] = Sysdatetime;


                            tblProductinward.Rows.Add(drrw);
                            //}
                            DataView dw = tblProductinward.DefaultView;
                            dw.Sort = "SLNO ASC";
                            Gridreturn.DataSource = tblProductinward;
                            Gridreturn.DataBind();
                        }
                }

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


    public void binddetails10()
    {
        if (!File.Exists(filename))
        {
            try
            {
               



                string supp = ddlmanufacturer.SelectedItem.Text;

                DataSet dsgroup20 = clsgd.GetcondDataSet("*", "tblmanufacture", "ManufactureName", supp);
                string supcode = dsgroup20.Tables[0].Rows[0]["ManufactureCode"].ToString();

                Gridreturn.DataSource = null;
                Gridreturn.DataBind();
                tblProductinward.Rows.Clear();
                SqlConnection con = new SqlConnection(strconn11);
                string In_flag1 = "Y";
               // string expdate10 = txtdate10.Text;

                DateTime expdate1 = Convert.ToDateTime(txtdate10.Text);
                string expdate10 = expdate1.ToString("dd/MM/yyyy");


                SqlCommand cmd = new SqlCommand("SELECT a.Productcode,a.ProductName,a.Batchid,a.Invoiceno,a.Expiredate,a.PStockinhand,a.Purchaseprice,ManufactureName,SupplierName  from tblpreturnProductinward a LEFT JOIN tblmanufacture ON a.ManufactureCode = tblmanufacture.ManufactureCode LEFT JOIN tblsuppliermaster ON a.Supppliercode= tblsuppliermaster.Suppliercode  where a.In_falg9='" + expdate10 + "' and a.ManufactureCode='" + supcode + "' and a.In_falg8='N'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);



                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblProductinward.Columns.Add(col);
                    tblProductinward.Columns.Add("ProductName");
                    tblProductinward.Columns.Add("Batchid");
                    tblProductinward.Columns.Add("Invoiceno");
                    tblProductinward.Columns.Add("Expiredate");
                    tblProductinward.Columns.Add("PStockinhand");
                    tblProductinward.Columns.Add("Purchaseprice");
                    tblProductinward.Columns.Add("ManufactureName");
                    tblProductinward.Columns.Add("SupplierName");
                    // tblProductinward.Columns.Add("Totalvalues");
                    tblProductinward.Columns.Add("Productinhand");
                    tblProductinward.Columns.Add("ReturnQuantity");
                    tblProductinward.Columns.Add("CurrentDate");



                    Session["Group"] = tblProductinward;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        string flag = ds.Tables[0].Rows[i]["Productcode"].ToString();

                        SqlConnection con10 = new SqlConnection(strconn11);
                        // string In_flag1 = "Y";
                       // DateTime expdate50 = Convert.ToDateTime(txtda10.Text);
                      //  string expdate10 = expdate50.ToString("dd/MM/yyyy");
                        SqlCommand cmd50 = new SqlCommand("SELECT Stockinhand as Productinhand from tblProductinward  where Productcode='" + flag + "'", con10);
                        SqlDataAdapter da50 = new SqlDataAdapter(cmd50);
                        DataSet ds50 = new DataSet();
                        da50.Fill(ds50);

                        if (ds50.Tables[0].Rows.Count > 0)
                        {


                            tblProductinward = (DataTable)Session["Group"];
                            drrw = tblProductinward.NewRow();
                            drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                            drrw["Batchid"] = ds.Tables[0].Rows[i]["Batchid"].ToString();
                            drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                            drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                            drrw["PStockinhand"] = ds.Tables[0].Rows[i]["PStockinhand"].ToString();
                            drrw["Purchaseprice"] = ds.Tables[0].Rows[i]["Purchaseprice"].ToString();
                            drrw["ManufactureName"] = ds.Tables[0].Rows[i]["ManufactureName"].ToString();
                            drrw["SupplierName"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();
                            drrw["Productinhand"] = ds50.Tables[0].Rows[0]["Productinhand"].ToString();
                            drrw["ReturnQuantity"] = "0";
                            string Sysdatetime = DateTime.Now.ToString();
                            drrw["CurrentDate"] = Sysdatetime;


                            tblProductinward.Rows.Add(drrw);
                            //}
                            DataView dw = tblProductinward.DefaultView;
                            dw.Sort = "SLNO ASC";
                            Gridreturn.DataSource = tblProductinward;
                            Gridreturn.DataBind();
                        }
                    }

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



   



    protected void btnExit_Click1(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }



    protected void ddlsupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  txtda.Enabled = true;
        txtda.Focus();
        //txt.Focus();
       genersdate();
       binddetails();
       //binddetails20();
    }


    protected void txtda_TextChanged(object sender, EventArgs e)
    {
        Button1.Focus();
        genersdate();
        binddetails();
        //binddetails10();
        //binddetails20();
    }


    public void binddetails15()
    {
        if (!File.Exists(filename))
        {
            try
            {
                string supp = ddlmanufacturer.SelectedItem.Text;

                DataSet dsgroup20 = clsgd.GetcondDataSet("*", "tblmanufacture", "ManufactureName", supp);
                string supcode = dsgroup20.Tables[0].Rows[0]["ManufactureCode"].ToString();

                Gridreturn.DataSource = null;
                Gridreturn.DataBind();
                tblProductinward.Rows.Clear();
                SqlConnection con = new SqlConnection(strconn11);
                string In_flag1 = "Y";
                string expdate10 = txtdate10.Text;
                SqlCommand cmd = new SqlCommand("SELECT a.ProductName,a.Batchid,a.Invoiceno,a.Expiredate,a.Stockinhand,a.Purchaseprice,ManufactureName,SupplierName,b.Stockinhand as Productinhand from tblpreturnProductinward a JOIN tblmanufacture ON a.ManufactureCode = tblmanufacture.ManufactureCode JOIN tblsuppliermaster ON a.Supppliercode= tblsuppliermaster.Suppliercode join tblProductinward b ON a.Batchid=b.Batchid where a.In_falg9='" + expdate10 + "' and a.ManufactureCode='" + supcode + "' and a.In_falg8='N'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataColumn col = new DataColumn("SLNO", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblProductinward.Columns.Add(col);
                    tblProductinward.Columns.Add("ProductName");
                    tblProductinward.Columns.Add("Batchid");
                    tblProductinward.Columns.Add("Invoiceno");
                    tblProductinward.Columns.Add("Expiredate");
                    tblProductinward.Columns.Add("Stockinhand");
                    tblProductinward.Columns.Add("Purchaseprice");
                    tblProductinward.Columns.Add("ManufactureName");
                    tblProductinward.Columns.Add("SupplierName");
                    // tblProductinward.Columns.Add("Totalvalues");
                    tblProductinward.Columns.Add("Productinhand");
                    tblProductinward.Columns.Add("ReturnQuantity");
                    tblProductinward.Columns.Add("CurrentDate");


                    Session["Group"] = tblProductinward;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tblProductinward = (DataTable)Session["Group"];
                        drrw = tblProductinward.NewRow();
                        drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
                        drrw["Batchid"] = ds.Tables[0].Rows[i]["Batchid"].ToString();
                        drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
                        drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
                        drrw["Stockinhand"] = ds.Tables[0].Rows[i]["Stockinhand"].ToString();
                        drrw["Purchaseprice"] = ds.Tables[0].Rows[i]["Purchaseprice"].ToString();
                        drrw["ManufactureName"] = ds.Tables[0].Rows[i]["ManufactureName"].ToString();
                        drrw["SupplierName"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();
                        drrw["Productinhand"] = ds.Tables[0].Rows[i]["Productinhand"].ToString();
                        drrw["ReturnQuantity"] = "0";
                        string Sysdatetime = DateTime.Now.ToString();
                        drrw["CurrentDate"] = Sysdatetime;


                        tblProductinward.Rows.Add(drrw);
                    }
                    DataView dw = tblProductinward.DefaultView;
                    dw.Sort = "SLNO ASC";
                    Gridreturn.DataSource = tblProductinward;
                    Gridreturn.DataBind();
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


    //public void binddetails20()
    //{
    //    if (!File.Exists(filename))
    //    {
    //        try
    //        {
    //            string supp = ddlsupplier.SelectedItem.Text;

    //            DataSet dsgroup20 = clsgd.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", supp);
    //            string supcode = dsgroup20.Tables[0].Rows[0]["SupplierCode"].ToString();

    //            GridView1.DataSource = null;
    //            GridView1.DataBind();
    //            tblProductinward10.Rows.Clear();
    //            SqlConnection con = new SqlConnection(strconn11);
    //            string In_flag1 = "Y";
    //            string expdate = txtda.Text;
    //            SqlCommand cmd = new SqlCommand("SELECT a.ProductName,a.Batchid,a.Invoiceno,a.Expiredate,a.Stockinhand,a.Purchaseprice,ManufactureName,SupplierName,b.Stockinhand as Productinhand  from tblpreturnProductinward a LEFT JOIN tblmanufacture ON a.ManufactureCode = tblmanufacture.ManufactureCode LEFT JOIN tblsuppliermaster ON a.Supppliercode= tblsuppliermaster.Suppliercode join tblProductinward b ON a.Batchid=b.Batchid where  a.SuppplierCode='" + supcode + "' and a.In_falg8='N'", con);
    //            SqlDataAdapter da = new SqlDataAdapter(cmd);
    //            DataSet ds = new DataSet();
    //            da.Fill(ds);

    //            if (ds.Tables[0].Rows.Count > 0)
    //            {

    //                DataColumn col = new DataColumn("SLNO", typeof(int));
    //                col.AutoIncrement = true;
    //                col.AutoIncrementSeed = 1;
    //                col.AutoIncrementStep = 1;
    //                tblProductinward10.Columns.Add(col);
    //                tblProductinward10.Columns.Add("ProductName");
    //                tblProductinward10.Columns.Add("Batchid");
    //                tblProductinward10.Columns.Add("Invoiceno");
    //                tblProductinward10.Columns.Add("Expiredate");
    //                tblProductinward10.Columns.Add("Stockinhand");
    //                tblProductinward10.Columns.Add("Purchaseprice");
    //                tblProductinward10.Columns.Add("ManufactureName");
    //                tblProductinward10.Columns.Add("SupplierName");
    //                // tblProductinward.Columns.Add("Totalvalues");
    //                tblProductinward10.Columns.Add("Productinhand");
    //                tblProductinward10.Columns.Add("ReturnQuantity");
    //                tblProductinward10.Columns.Add("CurrentDate");



    //                Session["Group"] = tblProductinward10;

    //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //                {
    //                    tblProductinward10 = (DataTable)Session["Group"];
    //                    drrw = tblProductinward10.NewRow();
    //                    drrw["ProductName"] = ds.Tables[0].Rows[i]["ProductName"].ToString();
    //                    drrw["Batchid"] = ds.Tables[0].Rows[i]["Batchid"].ToString();
    //                    drrw["Invoiceno"] = ds.Tables[0].Rows[i]["Invoiceno"].ToString();
    //                    drrw["Expiredate"] = ds.Tables[0].Rows[i]["Expiredate"].ToString();
    //                    drrw["Stockinhand"] = ds.Tables[0].Rows[i]["Stockinhand"].ToString();
    //                    drrw["Purchaseprice"] = ds.Tables[0].Rows[i]["Purchaseprice"].ToString();
    //                    drrw["ManufactureName"] = ds.Tables[0].Rows[i]["ManufactureName"].ToString();
    //                    drrw["SupplierName"] = ds.Tables[0].Rows[i]["SupplierName"].ToString();
    //                    drrw["Productinhand"] = ds.Tables[0].Rows[i]["Productinhand"].ToString();
    //                    drrw["ReturnQuantity"] = "0";
    //                    string Sysdatetime = DateTime.Now.ToString();
    //                    drrw["CurrentDate"] = Sysdatetime;


    //                    tblProductinward10.Rows.Add(drrw);
    //                }
    //                DataView dw = tblProductinward10.DefaultView;
    //                dw.Sort = "SLNO ASC";
    //                GridView1.DataSource = tblProductinward10;
    //                GridView1.DataBind();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            string asd = ex.Message;
    //            lblerror.Enabled = true;
    //            lblerror.Text = asd;
    //        }
    //    }
    //}



    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    for (int i = 0; i < GridView1.Rows.Count; i++)
    //    {
    //        TextBox txt1 = (TextBox)GridView1.Rows[i].Cells[0].FindControl("Return1_Qty");
    //        string pname = GridView1.Rows[i].Cells[0].Text;
    //        string batchno = GridView1.Rows[i].Cells[1].Text;
    //        string invcno = GridView1.Rows[i].Cells[2].Text;

    //        string ret15 = txt1.Text;






    //        if (ret15 != "0")
    //        {
    //            int ret = Convert.ToInt32(txt1.Text);


    //            SqlConnection con = new SqlConnection(strconn11);
    //            con.Open();
    //            SqlCommand cmd = new SqlCommand("select Stockinhand from tblProductinward WHERE ProductName= '" + pname + "' and Batchid='" + batchno + "' and Invoiceno='" + invcno + "'", con);
    //            SqlDataAdapter da = new SqlDataAdapter(cmd);
    //            DataSet ds = new DataSet();
    //            da.Fill(ds);

    //            string pstock = ds.Tables[0].Rows[0]["Stockinhand"].ToString();

    //            int pstock10 = Convert.ToInt32(pstock);

    //            string upstock = Convert.ToString(pstock10 - ret);

    //            SqlConnection conn52 = new SqlConnection(strconn11);
    //            conn52.Open();
    //            SqlCommand cmd52 = new SqlCommand("UPDATE tblProductinward SET  Stockinhand ='" + upstock + "',In_falg10='Y',In_falg9='Y'  WHERE ProductName= '" + pname + "' and Batchid='" + batchno + "' and Invoiceno='" + invcno + "'", conn52);
    //            cmd52.ExecuteNonQuery();

    //            SqlConnection conn53 = new SqlConnection(strconn11);
    //            conn53.Open();
    //            SqlCommand cmd53 = new SqlCommand("UPDATE tblpreturnProductinward SET  In_falg8 ='Y' WHERE ProductName= '" + pname + "' and Batchid='" + batchno + "' and Invoiceno='" + invcno + "'", conn52);
    //            cmd53.ExecuteNonQuery();
    //        }
    //    }

    //    string p_flag2 = ddlsupplier.SelectedItem.Text;

    //    DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", p_flag2);

    //    string flag2 = dsgroup10.Tables[0].Rows[0]["SupplierCode"].ToString();
    //    System.DateTime Dtnow = DateTime.Now;
    //    string sqlFormattedDate = Dtnow.ToString("dd/MM/yyyy");

    //    string p_flag20 = ddlsupplier.SelectedItem.Text;

    //    DataSet dsgroup15 = clsgd.GetcondDataSet("*", "tblSuspenseAsset", "SupplierCode", flag2);

    //    if (dsgroup15.Tables[0].Rows.Count <= 0)
    //    {

    //        ClsBLGP10.SuspenseAsset("INSERT_tblSuspenseAsset", flag2, p_flag2, "Debit", "AD", txtamount10.Text, "0", sqlFormattedDate, txtamount10.Text, "N", "N", "N", "N", "N");
    //    }
    //    else
    //    {
    //        string p_flag25 = ddlsupplier.SelectedItem.Text;

    //        DataSet dsgroup18 = clsgd.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", p_flag25);

    //        string flag20 = dsgroup10.Tables[0].Rows[0]["SupplierCode"].ToString();

    //        DataSet dsgroup50 = clsgd.GetcondDataSet("*", "tblSuspenseAsset", "SupplierCode", flag20);

    //        string amt = dsgroup50.Tables[0].Rows[0]["Amount"].ToString();

    //        double amt20 = Convert.ToDouble(amt);

    //        string amt25 = txtamount.Text;

    //        double amt50 = Convert.ToDouble(amt25);

    //        double famt = amt20 + amt50;

    //        string famt20 = Convert.ToString(famt);

    //        ClsBLGP10.SuspenseAsset("INSERT_tblSuspenseAsset", flag2, p_flag2, "Debit", "AD", txtamount10.Text, "0", sqlFormattedDate, famt20, "N", "N", "N", "N", "N");

    //    }

    //    if (rdtrans.SelectedIndex == 0)
    //    {
    //        binddetails();
    //        ddlsupplier.ClearSelection();
    //        dddate.ClearSelection();
    //    }
    //    else
    //    {
    //        binddetails10();
    //        ddlmanufacturer.ClearSelection();
    //        dddate.ClearSelection();
    //    }

    //}

    //protected void Return1_Qty_TextChanged(object sender, EventArgs e)
    //{


    //    for (int k = 0; k < GridView1.Rows.Count; k++)
    //    {
    //        double sum = 0.0;
    //        TextBox txt110 = (TextBox)GridView1.Rows[k].Cells[0].FindControl("Return1_Qty");
    //        string rtnqty = txt110.Text;

    //        if (rtnqty != "0")
    //        {

    //            double rtnqty10 = Convert.ToDouble(rtnqty);

    //            string price = GridView1.Rows[k].Cells[5].Text;

    //            double price10 = Convert.ToDouble(price);

    //            double dprice = rtnqty10 * price10;

    //            double amt = Convert.ToDouble(dprice);

    //            sum = sum + amt;

    //            txtamount10.Text = Convert.ToString(sum);

    //        }



    //    }

    //    btnsave.Focus();


    //}
}
        