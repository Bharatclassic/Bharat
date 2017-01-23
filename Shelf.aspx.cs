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
using System.Windows.Forms;

using System.Net.NetworkInformation;
using System.Management;
using System.Runtime.InteropServices; 

public partial class Self : System.Web.UI.Page
{
    ClsBALSelf ClsBLGP = new ClsBALSelf();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string button_select;
    protected  static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
    DataTable tblShelf = new DataTable();
    DataTable dt = new DataTable();
    DataRow drrw;
    string sMacAddress = "";
        //string result = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Visible = false;
        lblsuccess.Visible = true;
        //txtself.Attributes.Add("onkeyup", "toUppercase()");

        Table3.Visible = false;
        lblcode.Visible = false;
        btndelete.Enabled = false;
        if (!IsPostBack)
        {

            Table3.Visible = false;
            lblcode.Visible = false;
            btndelete.Enabled = false;
            Bind();
            txtself.Focus();
        }
        txtrows.Attributes.Add("autocomplete","off");
         if (Session["username"] != null)
        {

        }
        else
        {
            Response.Redirect("Index.aspx");
        }
          GetMACAddress();
        ClientScript.RegisterStartupScript(this.GetType(), "SetInitialFocus", "<script>document.getElementById('" + txtself.ClientID + "').focus();</script>");
        btnexit.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtself.ClientID + "').focus();return false;}} else {return true}; ");       

    }
    //public void GridBind()
    //{

    //    string filename = Dbconn.Mymenthod();
    //    if (!File.Exists(filename))
    //    {
    //        try
    //        {
    //            DataColumn dcol = new DataColumn("se_code", typeof(Int32));
    //            dcol.AutoIncrement = true;
    //            dcol.AutoIncrementSeed = 1;
    //            dcol.AutoIncrementStep = 1;

    //            // tblShelf.Columns.Add("se_code");
    //            tblShelf.Columns.Add("Se_name");
    //            // tblShelf.Columns.Add("srcount");
    //            tblShelf.Columns.Add("row_1");
    //            tblShelf.Columns.Add("row_2");
    //            tblShelf.Columns.Add("row_3");
    //            tblShelf.Columns.Add("row_4");
    //            tblShelf.Columns.Add("row_5");
    //            tblShelf.Columns.Add("row_6");
    //            tblShelf.Columns.Add("row_7");
    //            tblShelf.Columns.Add("row_8");
    //            tblShelf.Columns.Add("row_9");
    //            tblShelf.Columns.Add("row_10");
    //            tblShelf.Columns.Add("row_11");
    //            tblShelf.Columns.Add("row_12");
    //            tblShelf.Columns.Add("row_13");
    //            tblShelf.Columns.Add("row_14");
    //            tblShelf.Columns.Add("row_15");
    //            tblShelf.Columns.Add("row_16");
    //            tblShelf.Columns.Add("row_17");
    //            tblShelf.Columns.Add("row_18");
    //            tblShelf.Columns.Add("row_19");
    //            tblShelf.Columns.Add("row_20");
    //            Session["Shelf"] = tblShelf;
    //        }
    //        catch
    //        {

    //        }
    //    }
    //    else
    //    {
    //        try
    //        {
    //            DataColumn dcol = new DataColumn("se_code", typeof(Int32));
    //            dcol.AutoIncrement = true;
    //            dcol.AutoIncrementSeed = 1;
    //            dcol.AutoIncrementStep = 1;
    //            // tblShelf.Columns.Add("se_code");
    //            tblShelf.Columns.Add("Se_name");
    //            //  tblShelf.Columns.Add("srcount");
    //            tblShelf.Columns.Add("row1");
    //            tblShelf.Columns.Add("row2");
    //            tblShelf.Columns.Add("row3");
    //            tblShelf.Columns.Add("row4");
    //            tblShelf.Columns.Add("row5");
    //            tblShelf.Columns.Add("row6");
    //            tblShelf.Columns.Add("row7");
    //            tblShelf.Columns.Add("row8");
    //            tblShelf.Columns.Add("row9");
    //            tblShelf.Columns.Add("row10");
    //            tblShelf.Columns.Add("row11");
    //            tblShelf.Columns.Add("row12");
    //            tblShelf.Columns.Add("row13");
    //            tblShelf.Columns.Add("row14");
    //            tblShelf.Columns.Add("row15");
    //            tblShelf.Columns.Add("row16");
    //            tblShelf.Columns.Add("row17");
    //            tblShelf.Columns.Add("row18");
    //            tblShelf.Columns.Add("row19");
    //            tblShelf.Columns.Add("row20");
    //            Session["Shelf"] = tblShelf;
    //        }
    //        catch
    //        {
    //        }
    //    }
    //}

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
  
    public void Bind()
    {
       // string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            try
            {
                Gridviewshelf.DataSource = null;
                Gridviewshelf.DataBind();
                tblShelf.Rows.Clear();
                SqlConnection con = new SqlConnection(strconn11);
                SqlCommand cmd = new SqlCommand("select * from tblShelf", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataColumn dcol = new DataColumn("slno", typeof(Int32));
                    dcol.AutoIncrement = true;
                    dcol.AutoIncrementSeed = 1;
                    dcol.AutoIncrementStep = 1;

                    // tblShelf.Columns.Add("se_code");
                    tblShelf.Columns.Add("Se_name");
                    // tblShelf.Columns.Add("srcount");
                    tblShelf.Columns.Add("row_1");
                    tblShelf.Columns.Add("row_2");
                    tblShelf.Columns.Add("row_3");
                    tblShelf.Columns.Add("row_4");
                    tblShelf.Columns.Add("row_5");
                    tblShelf.Columns.Add("row_6");
                    tblShelf.Columns.Add("row_7");
                    tblShelf.Columns.Add("row_8");
                    tblShelf.Columns.Add("row_9");
                    tblShelf.Columns.Add("row_10");
                    tblShelf.Columns.Add("row_11");
                    tblShelf.Columns.Add("row_12");
                    tblShelf.Columns.Add("row_13");
                    tblShelf.Columns.Add("row_14");
                    tblShelf.Columns.Add("row_15");
                    tblShelf.Columns.Add("row_16");
                    tblShelf.Columns.Add("row_17");
                    tblShelf.Columns.Add("row_18");
                    tblShelf.Columns.Add("row_19");
                    tblShelf.Columns.Add("row_20");
                    Session["Shelf"] = tblShelf;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tblShelf = (DataTable)Session["Shelf"];
                        drrw = tblShelf.NewRow();
                        //drrw["se_code"] = ds.Tables[0].Rows[i]["se_code"].ToString();
                        drrw["Se_name"] = ds.Tables[0].Rows[i]["Se_name"].ToString();
                        //  drrw["srcount"] = ds.Tables[0].Rows[i]["srcount"].ToString();
                        drrw["row_1"] = ds.Tables[0].Rows[i]["row_1"].ToString();
                        drrw["row_2"] = ds.Tables[0].Rows[i]["row_2"].ToString();
                        drrw["row_3"] = ds.Tables[0].Rows[i]["row_3"].ToString();
                        drrw["row_4"] = ds.Tables[0].Rows[i]["row_4"].ToString();
                        drrw["row_5"] = ds.Tables[0].Rows[i]["row_5"].ToString();
                        drrw["row_6"] = ds.Tables[0].Rows[i]["row_6"].ToString();
                        drrw["row_7"] = ds.Tables[0].Rows[i]["row_7"].ToString();
                        drrw["row_8"] = ds.Tables[0].Rows[i]["row_8"].ToString();
                        drrw["row_9"] = ds.Tables[0].Rows[i]["row_9"].ToString();
                        drrw["row_10"] = ds.Tables[0].Rows[i]["row_10"].ToString();
                        drrw["row_11"] = ds.Tables[0].Rows[i]["row_11"].ToString();
                        drrw["row_12"] = ds.Tables[0].Rows[i]["row_12"].ToString();
                        drrw["row_13"] = ds.Tables[0].Rows[i]["row_13"].ToString();
                        drrw["row_14"] = ds.Tables[0].Rows[i]["row_14"].ToString();
                        drrw["row_15"] = ds.Tables[0].Rows[i]["row_15"].ToString();
                        drrw["row_16"] = ds.Tables[0].Rows[i]["row_16"].ToString();
                        drrw["row_17"] = ds.Tables[0].Rows[i]["row_17"].ToString();
                        drrw["row_18"] = ds.Tables[0].Rows[i]["row_18"].ToString();
                        drrw["row_19"] = ds.Tables[0].Rows[i]["row_19"].ToString();
                        drrw["row_20"] = ds.Tables[0].Rows[i]["row_20"].ToString();
                        tblShelf.Rows.Add(drrw);
                        //Gridviewshelf.DataSource = tblShelf;
                        //Gridviewshelf.DataBind();
                    }
                    DataView dws = tblShelf.DefaultView;
                    dws.Sort = "Se_name ASC";
                    Gridviewshelf.DataSource = tblShelf;
                    Gridviewshelf.DataBind();
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
                Gridviewshelf.DataSource = null;
                Gridviewshelf.DataBind();
                tblShelf.Rows.Clear();
                OleDbConnection conn10 = new OleDbConnection(strconn11);
                conn10.Open();
                OleDbCommand cmd1 = new OleDbCommand("select * from tblShelf", conn10);

                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataColumn dcol = new DataColumn("slno", typeof(Int32));
                    dcol.AutoIncrement = true;
                    dcol.AutoIncrementSeed = 1;
                    dcol.AutoIncrementStep = 1;
                    // tblShelf.Columns.Add("se_code");
                    tblShelf.Columns.Add("Se_name");
                    //  tblShelf.Columns.Add("srcount");
                    tblShelf.Columns.Add("row1");
                    tblShelf.Columns.Add("row2");
                    tblShelf.Columns.Add("row3");
                    tblShelf.Columns.Add("row4");
                    tblShelf.Columns.Add("row5");
                    tblShelf.Columns.Add("row6");
                    tblShelf.Columns.Add("row7");
                    tblShelf.Columns.Add("row8");
                    tblShelf.Columns.Add("row9");
                    tblShelf.Columns.Add("row10");
                    tblShelf.Columns.Add("row11");
                    tblShelf.Columns.Add("row12");
                    tblShelf.Columns.Add("row13");
                    tblShelf.Columns.Add("row14");
                    tblShelf.Columns.Add("row15");
                    tblShelf.Columns.Add("row16");
                    tblShelf.Columns.Add("row17");
                    tblShelf.Columns.Add("row18");
                    tblShelf.Columns.Add("row19");
                    tblShelf.Columns.Add("row20");
                    Session["Shelf"] = tblShelf;

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        tblShelf = (DataTable)Session["Shelf"];
                        drrw = tblShelf.NewRow();
                        // drrw["se_code"] = ds1.Tables[0].Rows[i]["se_code"].ToString();
                        drrw["Se_name"] = ds1.Tables[0].Rows[i]["Se_name"].ToString();
                        //  drrw["srcount"] = ds1.Tables[0].Rows[i]["srcount"].ToString();
                        drrw["row1"] = ds1.Tables[0].Rows[i]["row1"].ToString();
                        drrw["row2"] = ds1.Tables[0].Rows[i]["row2"].ToString();
                        drrw["row3"] = ds1.Tables[0].Rows[i]["row3"].ToString();
                        drrw["row4"] = ds1.Tables[0].Rows[i]["row4"].ToString();
                        drrw["row5"] = ds1.Tables[0].Rows[i]["row5"].ToString();
                        drrw["row6"] = ds1.Tables[0].Rows[i]["row6"].ToString();
                        drrw["row7"] = ds1.Tables[0].Rows[i]["row7"].ToString();
                        drrw["row8"] = ds1.Tables[0].Rows[i]["row8"].ToString();
                        drrw["row9"] = ds1.Tables[0].Rows[i]["row9"].ToString();
                        drrw["row10"] = ds1.Tables[0].Rows[i]["row10"].ToString();
                        drrw["row11"] = ds1.Tables[0].Rows[i]["row11"].ToString();
                        drrw["row12"] = ds1.Tables[0].Rows[i]["row12"].ToString();
                        drrw["row13"] = ds1.Tables[0].Rows[i]["row13"].ToString();
                        drrw["row14"] = ds1.Tables[0].Rows[i]["row14"].ToString();
                        drrw["row15"] = ds1.Tables[0].Rows[i]["row15"].ToString();
                        drrw["row16"] = ds1.Tables[0].Rows[i]["row16"].ToString();
                        drrw["row17"] = ds1.Tables[0].Rows[i]["row17"].ToString();
                        drrw["row18"] = ds1.Tables[0].Rows[i]["row18"].ToString();
                        drrw["row19"] = ds1.Tables[0].Rows[i]["row19"].ToString();
                        drrw["row20"] = ds1.Tables[0].Rows[i]["row20"].ToString();
                        tblShelf.Rows.Add(drrw);
                        //Gridviewshelf.DataSource = tblShelf;
                        //Gridviewshelf.DataBind();

                    }
                    DataView dws = tblShelf.DefaultView;
                    dws.Sort = "Se_name ASC";
                    Gridviewshelf.DataSource = tblShelf;
                    Gridviewshelf.DataBind();
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
    protected void Gridviewshelf_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Gridviewshelf.PageIndex = e.NewPageIndex;
        Bind();

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {

        string gennam = txtself.Text;
      
        if (button_select == "Modify")
        {
       // GetHDDSerialNo() ;
            string Sysdatetime=DateTime.Now.ToString();
            string cod = lblcode.Text;
            int c = Convert.ToInt32(cod);
            string couni = txtrows.Text;
            try
            {
                int srcount1 = Convert.ToInt32(couni);
                if (!File.Exists(filename))
                {
                    switch (srcount1)
                    {

                        case 1:
                            using (SqlConnection conn1 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn1.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd1 = new SqlCommand())
                                {

                                    conn1.Open();
                                    SqlCommand com1 = new SqlCommand();
                                    com1.Connection = conn1;
                                    com1.CommandType = CommandType.Text;
                                    com1.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row_1='01',row_2=NULL,row_3=NULL,row_4=NULL,row_5=NULL,row_6=NULL,row_7=NULL,row_8=NULL,row_9=NULL,row_10=NULL,row_11=NULL,row_12=NULL,row_13=NULL,row_14=NULL,row_15=NULL,row_16=NULL,row_17=NULL,row_18=NULL,row_19=NULL,row_20=NULL  where Se_code='" + c + "'");
                                    com1.ExecuteNonQuery();
                                    break;
                                }
                            }

                        case 2:
                            using (SqlConnection conn2 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                              //  conn2.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd2 = new SqlCommand())
                                {

                                    conn2.Open();
                                    SqlCommand com2 = new SqlCommand();
                                    com2.Connection = conn2;
                                    com2.CommandType = CommandType.Text;
                                    com2.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row_1='01',row_2='02',row_3=NULL,row_4=NULL,row_5=NULL,row_6=NULL,row_7=NULL,row_8=NULL,row_9=NULL,row_10=NULL,row_11=NULL,row_12=NULL,row_13=NULL,row_14=NULL,row_15=NULL,row_16=NULL,row_17=NULL,row_18=NULL,row_19=NULL,row_20=NULL  where Se_code='" + c + "'");
                                    com2.ExecuteNonQuery();
                                    break;
                                }
                            }
                        case 3:
                            using (SqlConnection conn3 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn3.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd3 = new SqlCommand())
                                {

                                    conn3.Open();
                                    SqlCommand com3 = new SqlCommand();
                                    com3.Connection = conn3;
                                    com3.CommandType = CommandType.Text;
                                    com3.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "', row_1='01',row_2='02',row_3='03',row_4=NULL,row_5=NULL,row_6=NULL,row_7=NULL,row_8=NULL,row_9=NULL,row_10=NULL,row_11=NULL,row_12=NULL,row_13=NULL,row_14=NULL,row_15=NULL,row_16=NULL,row_17=NULL,row_18=NULL,row_19=NULL,row_20=NULL  where Se_code='" + c + "'");
                                    com3.ExecuteNonQuery();
                                    break;
                                }
                            }
                        case 4:
                            using (SqlConnection conn4 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn4.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd4 = new SqlCommand())
                                {

                                    conn4.Open();
                                    SqlCommand com4 = new SqlCommand();
                                    com4.Connection = conn4;
                                    com4.CommandType = CommandType.Text;
                                    com4.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "', row_1='01',row_2='02',row_3='03',row_4='04',row_5=NULL,row_6=NULL,row_7=NULL,row_8=NULL,row_9=NULL,row_10=NULL,row_11=NULL,row_12=NULL,row_13=NULL,row_14=NULL,row_15=NULL,row_16=NULL,row_17=NULL,row_18=NULL,row_19=NULL,row_20=NULL  where Se_code='" + c + "'");
                                    com4.ExecuteNonQuery();
                                    break;
                                }
                            }
                        case 5:
                            using (SqlConnection conn5 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn5.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd5 = new SqlCommand())
                                {

                                    conn5.Open();
                                    SqlCommand com5 = new SqlCommand();
                                    com5.Connection = conn5;
                                    com5.CommandType = CommandType.Text;
                                    com5.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "', row_1='01',row_2='02',row_3='03',row_4='04',row_5='05',row_6=NULL,row_7=NULL,row_8=NULL,row_9=NULL,row_10=NULL,row_11=NULL,row_12=NULL,row_13=NULL,row_14=NULL,row_15=NULL,row_16=NULL,row_17=NULL,row_18=NULL,row_19=NULL,row_20=NULL  where Se_code='" + c + "'");
                                    com5.ExecuteNonQuery();
                                    break;
                                }
                            }
                        case 6:
                            using (SqlConnection conn6 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn6.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd6 = new SqlCommand())
                                {


                                    conn6.Open();
                                    SqlCommand com6 = new SqlCommand();
                                    com6.Connection = conn6;
                                    com6.CommandType = CommandType.Text;
                                    com6.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "', row_1='01',row_2='02',row_3='03',row_4='04',row_5='05',row_6='06',row_7=NULL,row_8=NULL,row_9=NULL,row_10=NULL,row_11=NULL,row_12=NULL,row_13=NULL,row_14=NULL,row_15=NULL,row_16=NULL,row_17=NULL,row_18=NULL,row_19=NULL,row_20=NULL  where Se_code='" + c + "'");
                                    com6.ExecuteNonQuery();
                                    break;
                                }
                            }
                        case 7:
                            using (SqlConnection conn7 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn7.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd7 = new SqlCommand())
                                {

                                    conn7.Open();
                                    SqlCommand com7 = new SqlCommand();
                                    com7.Connection = conn7;
                                    com7.CommandType = CommandType.Text;
                                    com7.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "', row_1='01',row_2='02',row_3='03',row_4='04',row_5='05',row_6='06',row_7='07',row_8=NULL,row_9=NULL,row_10=NULL,row_11=NULL,row_12=NULL,row_13=NULL,row_14=NULL,row_15=NULL,row_16=NULL,row_17=NULL,row_18=NULL,row_19=NULL,row_20=NULL  where Se_code='" + c + "'");
                                    com7.ExecuteNonQuery();
                                    break;
                                }
                            }
                        case 8:
                            using (SqlConnection conn8 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn8.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd8 = new SqlCommand())
                                {

                                    conn8.Open();
                                    SqlCommand com8 = new SqlCommand();
                                    com8.Connection = conn8;
                                    com8.CommandType = CommandType.Text;
                                    com8.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "', row_1='01',row_2='02',row_3='03',row_4='04',row_5='05',row_6='06',row_7='07',row_8='08',row_9=NULL,row_10=NULL,row_11=NULL,row_12=NULL,row_13=NULL,row_14=NULL,row_15=NULL,row_16=NULL,row_17=NULL,row_18=NULL,row_19=NULL,row_20=NULL  where Se_code='" + c + "'");
                                    com8.ExecuteNonQuery();
                                    break;
                                }
                            }
                        case 9:
                            using (SqlConnection conn9 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn9.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd9 = new SqlCommand())
                                {

                                    conn9.Open();
                                    SqlCommand com9 = new SqlCommand();
                                    com9.Connection = conn9;
                                    com9.CommandType = CommandType.Text;
                                    com9.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "', row_1='01',row_2='02',row_3='03',row_4='04',row_5='05',row_6='06',row_7='07',row_8='08',row_9='09',row_10=NULL,row_11=NULL,row_12=NULL,row_13=NULL,row_14=NULL,row_15=NULL,row_16=NULL,row_17=NULL,row_18=NULL,row_19=NULL,row_20=NULL  where Se_code='" + c + "'");
                                    com9.ExecuteNonQuery();
                                    break;
                                }
                            }
                        case 10:
                            using (SqlConnection conn10 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn10.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd10 = new SqlCommand())
                                {

                                    conn10.Open();
                                    SqlCommand com10 = new SqlCommand();
                                    com10.Connection = conn10;
                                    com10.CommandType = CommandType.Text;
                                    com10.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "', row_1='01',row_2='02',row_3='03',row_4='04',row_5='05',row_6='06',row_7='07',row_8='08',row_9='09',row_10='10',row_11=NULL,row_12=NULL,row_13=NULL,row_14=NULL,row_15=NULL,row_16=NULL,row_17=NULL,row_18=NULL,row_19=NULL,row_20=NULL where Se_code='" + c + "'");
                                    com10.ExecuteNonQuery();
                                    break;
                                }
                            }
                        case 11:
                            using (SqlConnection conn11 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn11.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd11 = new SqlCommand())
                                {

                                    conn11.Open();
                                    SqlCommand com11 = new SqlCommand();
                                    com11.Connection = conn11;
                                    com11.CommandType = CommandType.Text;
                                    com11.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "', row_1='01',row_2='02',row_3='03',row_4='04',row_5='05',row_6='06',row_7='07',row_8='08',row_9='09',row_10='10',row_11='11',row_12=NULL,row_13=NULL,row_14=NULL,row_15=NULL,row_16=NULL,row_17=NULL,row_18=NULL,row_19=NULL,row_20=NULL  where Se_code='" + c + "'");
                                    com11.ExecuteNonQuery();
                                    break;
                                }
                            }
                        case 12:
                            using (SqlConnection conn12 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn12.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd12 = new SqlCommand())
                                {

                                    conn12.Open();
                                    SqlCommand com12 = new SqlCommand();
                                    com12.Connection = conn12;
                                    com12.CommandType = CommandType.Text;
                                    com12.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "', row_1='01',row_2='02',row_3='03',row_4='04',row_5='05',row_6='06',row_7='07',row_8='08',row_9='09',row_10='10',row_11='11',row_12='12',row_13=NULL,row_14=NULL,row_15=NULL,row_16=NULL,row_17=NULL,row_18=NULL,row_19=NULL,row_20=NULL where Se_code='" + c + "'");
                                    com12.ExecuteNonQuery();
                                    break;
                                }
                            }
                        case 13:
                            using (SqlConnection conn13 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn13.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd13 = new SqlCommand())
                                {

                                    conn13.Open();
                                    SqlCommand com13 = new SqlCommand();
                                    com13.Connection = conn13;
                                    com13.CommandType = CommandType.Text;
                                    com13.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "', row_1='01',row_2='02',row_3='03',row_4='04',row_5='05',row_6='06',row_7='07',row_8='08',row_9='09',row_10='10',row_11='11',row_12='12',row_13='13',row_14=NULL,row_15=NULL,row_16=NULL,row_17=NULL,row_18=NULL,row_19=NULL,row_20=NULL where Se_code='" + c + "'");
                                    com13.ExecuteNonQuery();
                                    break;
                                }
                            }
                        case 14:
                            using (SqlConnection conn14 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn14.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd14 = new SqlCommand())
                                {

                                    conn14.Open();
                                    SqlCommand com14 = new SqlCommand();
                                    com14.Connection = conn14;
                                    com14.CommandType = CommandType.Text;
                                    com14.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "', row_1='01',row_2='02',row_3='03',row_4='04',row_5='05',row_6='06',row_7='07',row_8='08',row_9='09',row_10='10',row_11='11',row_12='12',row_13='13',row_14='14',row_15=NULL,row_16=NULL,row_17=NULL,row_18=NULL,row_19=NULL,row_20=NULL where Se_code='" + c + "'");
                                    com14.ExecuteNonQuery();
                                    break;
                                }
                            }
                        case 15:
                            using (SqlConnection conn15 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn15.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd15 = new SqlCommand())
                                {

                                    conn15.Open();
                                    SqlCommand com15 = new SqlCommand();
                                    com15.Connection = conn15;
                                    com15.CommandType = CommandType.Text;
                                    com15.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "', row_1='01',row_2='02',row_3='03',row_4='04',row_5='05',row_6='06',row_7='07',row_8='08',row_9='09',row_10='10',row_11='11',row_12='12',row_13='13',row_14='14',row_15='15',row_16=NULL,row_17=NULL,row_18=NULL,row_19=NULL,row_20=NULL  where Se_code='" + c + "'");
                                    com15.ExecuteNonQuery();
                                    break;
                                }
                            }
                        case 16:
                            using (SqlConnection conn16 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn16.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd16 = new SqlCommand())
                                {

                                    conn16.Open();
                                    SqlCommand com16 = new SqlCommand();
                                    com16.Connection = conn16;
                                    com16.CommandType = CommandType.Text;
                                    com16.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "', row_1='01',row_2='02',row_3='03',row_4='04',row_5='05',row_6='06',row_7='07',row_8='08',row_9='09',row_10='10',row_11='11',row_12='12',row_13='13',row_14='14',row_15='15',row_16='16',row_17=NULL,row_18=NULL,row_19=NULL,row_20=NULL  where Se_code='" + c + "'");
                                    com16.ExecuteNonQuery();
                                    break;
                                }
                            }
                        case 17:
                            using (SqlConnection conn17 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn17.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd17 = new SqlCommand())
                                {

                                    conn17.Open();
                                    SqlCommand com17 = new SqlCommand();
                                    com17.Connection = conn17;
                                    com17.CommandType = CommandType.Text;
                                    com17.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "', row_1='01',row_2='02',row_3='03',row_4='04',row_5='05',row_6='06',row_7='07',row_8='08',row_9='09',row_10='10',row_11='11',row_12='12',row_13='13',row_14='14',row_15='15',row_16='16',row_17='17',row_18=NULL,row_19=NULL,row_20=NULL  where Se_code='" + c + "'");
                                    com17.ExecuteNonQuery();
                                    break;
                                }
                            }
                        case 18:
                            using (SqlConnection conn18 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn18.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd18 = new SqlCommand())
                                {

                                    conn18.Open();
                                    SqlCommand com18 = new SqlCommand();
                                    com18.Connection = conn18;
                                    com18.CommandType = CommandType.Text;
                                    com18.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "', row_1='01',row_2='02',row_3='03',row_4='04',row_5='05',row_6='06',row_7='07',row_8='08',row_9='09',row_10='10',row_11='11',row_12='12',row_13='13',row_14='14',row_15='15',row_16='16',row_17='17',row_18='18',row_19=NULL,row_20=NULL  where Se_code='" + c + "'");
                                    com18.ExecuteNonQuery();
                                    break;
                                }
                            }
                        case 19:
                            using (SqlConnection conn19 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn19.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd19 = new SqlCommand())
                                {

                                    conn19.Open();
                                    SqlCommand com19 = new SqlCommand();
                                    com19.Connection = conn19;
                                    com19.CommandType = CommandType.Text;
                                    com19.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "', row_1='01',row_2='02',row_3='03',row_4='04',row_5='05',row_6='06',row_7='07',row_8='08',row_9='09',row_10='10',row_11='11',row_12='12',row_13='13',row_14='14',row_15='15',row_16='16',row_17='17',row_18='18',row_19='19',row_20=NULL  where Se_code='" + c + "'");
                                    com19.ExecuteNonQuery();
                                    break;
                                }
                            }
                        case 20:
                            using (SqlConnection conn20 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn20.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd20 = new SqlCommand())
                                {

                                    conn20.Open();
                                    SqlCommand com20 = new SqlCommand();
                                    com20.Connection = conn20;
                                    com20.CommandType = CommandType.Text;
                                    com20.CommandText = ("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "', row_1=01,row_2=02,row_3=03,row_4=04,row_5='05',row_6='06',row_7='07',row_8='08',row_9='09',row_10='10',row_11='11',row_12='12',row_13='13',row_14='14',row_15='15',row_16='16',row_17='17',row_18='18',row_19='19',row_20='20'  where Se_code='" + c + "'");
                                    com20.ExecuteNonQuery();
                                    break;
                                }
                            }

                    }
                }
                else
                {
                    switch (srcount1)
                    {
                        case 1:
                            String strconn11 = Dbconn.conmenthod();
                            OleDbConnection con1 = new OleDbConnection(strconn11);
                            con1.Open();
                            OleDbCommand cmd1 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2=NULL,row3=NULL,row4=NULL,row5=NULL,row6=NULL,row7=NULL,row8=NULL,row9=NULL,row10=NULL,row11=NULL,row12=NULL,row13=NULL,row14=NULL,row15=NULL,row16=NULL,row17=NULL,row18=NULL,row19=NULL,row20=NULL  where Se_code=" + c + "", con1);
                            cmd1.ExecuteNonQuery();
                            con1.Close();
                            break;
                        case 2:
                            String strconn12 = Dbconn.conmenthod();
                            OleDbConnection con2 = new OleDbConnection(strconn12);
                            con2.Open();
                            OleDbCommand cmd2 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3=NULL,row4=NULL,row5=NULL,row6=NULL,row7=NULL,row8=NULL,row9=NULL,row10=NULL,row11=NULL,row12=NULL,row13=NULL,row14=NULL,row15=NULL,row16=NULL,row17=NULL,row18=NULL,row19=NULL,row20=NULL  where Se_code=" + c + "", con2);
                            cmd2.ExecuteNonQuery();
                            con2.Close();
                            break;
                        case 3:
                            String strconn13 = Dbconn.conmenthod();
                            OleDbConnection con3 = new OleDbConnection(strconn13);
                            con3.Open();
                            OleDbCommand cmd3 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3='03',row4=NULL,row5=NULL,row6=NULL,row7=NULL,row8=NULL,row9=NULL,row10=NULL,row11=NULL,row12=NULL,row13=NULL,row14=NULL,row15=NULL,row16=NULL,row17=NULL,row18=NULL,row19=NULL,row20=NULL  where Se_code=" + c + "", con3);
                            cmd3.ExecuteNonQuery();
                            con3.Close();
                            break;
                        case 4:
                            String strconn14 = Dbconn.conmenthod();
                            OleDbConnection con4 = new OleDbConnection(strconn14);
                            con4.Open();
                            OleDbCommand cmd4 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3='03',row4='04',row5=NULL,row6=NULL,row7=NULL,row8=NULL,row9=NULL,row10=NULL,row11=NULL,row12=NULL,row13=NULL,row14=NULL,row15=NULL,row16=NULL,row17=NULL,row18=NULL,row19=NULL,row20=NULL  where Se_code=" + c + "", con4);
                            cmd4.ExecuteNonQuery();
                            con4.Close();
                            break;
                        case 5:
                            String strconn15 = Dbconn.conmenthod();
                            OleDbConnection con5 = new OleDbConnection(strconn15);
                            con5.Open();
                            OleDbCommand cmd5 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3='03',row4='04',row5='05',row6=NULL,row7=NULL,row8=NULL,row9=NULL,row10=NULL,row11=NULL,row12=NULL,row13=NULL,row14=NULL,row15=NULL,row16=NULL,row17=NULL,row18=NULL,row19=NULL,row20=NULL  where Se_code=" + c + "", con5);
                            cmd5.ExecuteNonQuery();
                            con5.Close();
                            break;
                        case 6:
                            String strconn16 = Dbconn.conmenthod();
                            OleDbConnection con6 = new OleDbConnection(strconn16);
                            con6.Open();
                            OleDbCommand cmd6 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3='03',row4='04',row5='05',row6='06',row7=NULL,row8=NULL,row9=NULL,row10=NULL,row11=NULL,row12=NULL,row13=NULL,row14=NULL,row15=NULL,row16=NULL,row17=NULL,row18=NULL,row19=NULL,row20=NULL  where Se_code=" + c + "", con6);
                            cmd6.ExecuteNonQuery();
                            con6.Close();
                            break;
                        case 7:
                            String strconn17 = Dbconn.conmenthod();
                            OleDbConnection con7 = new OleDbConnection(strconn17);
                            con7.Open();
                            OleDbCommand cmd7 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3='03',row4='04',row5='05',row6='06',row7='07',row8=NULL,row9=NULL,row10=NULL,row11=NULL,row12=NULL,row13=NULL,row14=NULL,row15=NULL,row16=NULL,row17=NULL,row18=NULL,row19=NULL,row20=NULL  where Se_code=" + c + "", con7);
                            cmd7.ExecuteNonQuery();
                            con7.Close();
                            break;
                        case 8:
                            String strconn18 = Dbconn.conmenthod();
                            OleDbConnection con8 = new OleDbConnection(strconn18);
                            con8.Open();
                            OleDbCommand cmd8 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3='03',row4='04',row5='05',row6='06',row7='07',row8='08',row9=NULL,row10=NULL,row11=NULL,row12=NULL,row13=NULL,row14=NULL,row15=NULL,row16=NULL,row17=NULL,row18=NULL,row19=NULL,row20=NULL  where Se_code=" + c + "", con8);
                            cmd8.ExecuteNonQuery();
                            con8.Close();
                            break;
                        case 9:
                            String strconn19 = Dbconn.conmenthod();
                            OleDbConnection con9 = new OleDbConnection(strconn19);
                            con9.Open();
                            OleDbCommand cmd9 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3='03',row4='04',row5='05',row6='06',row7='07',row8='08',row9='09',row10=NULL,row11=NULL,row12=NULL,row13=NULL,row14=NULL,row15=NULL,row16=NULL,row17=NULL,row18=NULL,row19=NULL,row20=NULL  where Se_code=" + c + "", con9);
                            cmd9.ExecuteNonQuery();
                            con9.Close();
                            break;
                        case 10:
                            String strconn20 = Dbconn.conmenthod();
                            OleDbConnection con10 = new OleDbConnection(strconn20);
                            con10.Open();
                            OleDbCommand cmd10 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3='03',row4='04',row5='05',row6='06',row7='07',row8='08',row9='09',row10='10',row11=NULL,row12=NULL,row13=NULL,row14=NULL,row15=NULL,row16=NULL,row17=NULL,row18=NULL,row19=NULL,row20=NULL  where Se_code=" + c + "", con10);
                            cmd10.ExecuteNonQuery();
                            con10.Close();
                            break;
                        case 11:
                            String strconn21 = Dbconn.conmenthod();
                            OleDbConnection con11 = new OleDbConnection(strconn21);
                            con11.Open();
                            OleDbCommand cmd11 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3='03',row4='04',row5='05',row6='06',row7='07',row8='08',row9='09',row10='10',row11='11',row12=NULL,row13=NULL,row14=NULL,row15=NULL,row16=NULL,row17=NULL,row18=NULL,row19=NULL,row20=NULL  where Se_code=" + c + "", con11);
                            cmd11.ExecuteNonQuery();
                            con11.Close();
                            break;
                        case 12:
                            String strconn22 = Dbconn.conmenthod();
                            OleDbConnection con12 = new OleDbConnection(strconn22);
                            con12.Open();
                            OleDbCommand cmd12 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3='03',row4='04',row5='05',row6='06',row7='07',row8='08',row9='09',row10='10',row11='11',row12='12',row13=NULL,row14=NULL,row15=NULL,row16=NULL,row17=NULL,row18=NULL,row19=NULL,row20=NULL  where Se_code=" + c + "", con12);
                            cmd12.ExecuteNonQuery();
                            con12.Close();
                            break;
                        case 13:
                            String strconn23 = Dbconn.conmenthod();
                            OleDbConnection con13 = new OleDbConnection(strconn23);
                            con13.Open();
                            OleDbCommand cmd13 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3='03',row4='04',row5='05',row6='06',row7='07',row8='08',row9='09',row10='10',row11='11',row12='12',row13='13',row14=NULL,row15=NULL,row16=NULL,row17=NULL,row18=NULL,row19=NULL,row20=NULL  where Se_code=" + c + "", con13);
                            cmd13.ExecuteNonQuery();
                            con13.Close();
                            break;
                        case 14:
                            String strconn24 = Dbconn.conmenthod();
                            OleDbConnection con14 = new OleDbConnection(strconn24);
                            con14.Open();
                            OleDbCommand cmd14 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3='03',row4='04',row5='05',row6='06',row7='07',row8='08',row9='09',row10='10',row11='11',row12='12',row13='13',row14='14',row15=NULL,row16=NULL,row17=NULL,row18=NULL,row19=NULL,row20=NULL  where Se_code=" + c + "", con14);
                            cmd14.ExecuteNonQuery();
                            con14.Close();
                            break;
                        case 15:
                            String strconn25 = Dbconn.conmenthod();
                            OleDbConnection con15 = new OleDbConnection(strconn25);
                            con15.Open();
                            OleDbCommand cmd15 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3='03',row4='04',row5='05',row6='06',row7='07',row8='08',row9='09',row10='10',row11='11',row12='12',row13='13',row14='14',row15='15',row16=NULL,row17=NULL,row18=NULL,row19=NULL,row20=NULL  where Se_code=" + c + "", con15);
                            cmd15.ExecuteNonQuery();
                            con15.Close();
                            break;
                        case 16:
                            String strconn26 = Dbconn.conmenthod();
                            OleDbConnection con16 = new OleDbConnection(strconn26);
                            con16.Open();
                            OleDbCommand cmd16 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3='03',row4='04',row5='05',row6='06',row7='07',row8='08',row9='09',row10='10',row11='11',row12='12',row13='13',row14='14',row15='15',row16='16',row17=NULL,row18=NULL,row19=NULL,row20=NULL  where Se_code=" + c + "", con16);
                            cmd16.ExecuteNonQuery();
                            con16.Close();
                            break;
                        case 17:
                            String strconn27 = Dbconn.conmenthod();
                            OleDbConnection con17 = new OleDbConnection(strconn27);
                            con17.Open();
                            OleDbCommand cmd17 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3='03',row4='04',row5='05',row6='06',row7='07',row8='08',row9='09',row10='10',row11='11',row12='12',row13='13',row14='14',row15='15',row16='16',row17='17',row18=NULL,row19=NULL,row20=NULL  where Se_code=" + c + "", con17);
                            cmd17.ExecuteNonQuery();
                            con17.Close();
                            break;
                        case 18:
                            String strconn28 = Dbconn.conmenthod();
                            OleDbConnection con18 = new OleDbConnection(strconn28);
                            con18.Open();
                            OleDbCommand cmd18 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3='03',row4='04',row5='05',row6='06',row7='07',row8='08',row9='09',row10='10',row11='11',row12='12',row13='13',row14='14',row15='15',row16='16',row17='17',row18='18',row19=NULL,row20=NULL  where Se_code=" + c + "", con18);
                            cmd18.ExecuteNonQuery();
                            con18.Close();
                            break;
                        case 19:
                            String strconn29 = Dbconn.conmenthod();
                            OleDbConnection con19 = new OleDbConnection(strconn29);
                            con19.Open();
                            OleDbCommand cmd19 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3='03',row4='04',row5='05',row6='06',row7='07',row8='08',row9='09',row10='10',row11='11',row12='12',row13='13',row14='14',row15='15',row16='16',row17='17',row18='18',row19='19',row20=NULL  where Se_code=" + c + "", con19);
                            cmd19.ExecuteNonQuery();
                            con19.Close();
                            break;
                        case 20:
                            String strconn30 = Dbconn.conmenthod();
                            OleDbConnection con20 = new OleDbConnection(strconn30);
                            con20.Open();
                            OleDbCommand cmd20 = new OleDbCommand("update tblshelf set srcount='" + srcount1 + "',Se_name='" + gennam + "',row1='01',row2='02',row3='03',row4='04',row5='05',row6='06',row7='07',row8='08',row9='09',row10='10',row11='11',row12='12',row13='13',row14='14',row15='15',row16='16',row17='17',row18='18',row19='19',row20='20'  where Se_code=" + c + "", con20);
                            cmd20.ExecuteNonQuery();
                            con20.Close();
                            break;
                    }
                }
                //  Response.Redirect("Shelf.aspx");
            }
            catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Visible = true;
                lblerror.Text = asd;
            }
            Bind();
            lblsuccess.Text = "modified successfully";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            selecting();
            button_select = string.Empty;
            txtself.Text = "";
            txtrows.Text = "";
            txtself.Focus();
            txtself.Enabled = true;
            Gridviewshelf.DataSource = null;
            Gridviewshelf.Columns.Clear();

        }

        else if (button_select != "Modify")
        {
            //Gridviewshelf.DataSource = null;
            //Bind();
            //updateGrid.Update();
            try
            {
                 // GetHDDSerialNo() ;
                string Sysdatetime=DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
                string srow = txtself.Text.TrimStart();
                //string srcount = Int32.Parse(txtrows.Text);
                string strCaps1 = Regex.Replace(srow, "[^a-zA-Z + \\s]", "");
                string strEdited = Regex.Replace(strCaps1, @"\s+", " ");
                string coun = txtrows.Text;

                if (strEdited == "")
                {
                    Master.ShowModal("ShelfName is mandatory", "txtself", 0);
                    return;
                }
                if (coun == "")
                {
                    Master.ShowModal("Shelfrows is mandatory", "txtrows", 0);
                    return;
                }
                if (txtself.Text.Length == '2')
                {
                    Master.ShowModal("Enter 3 characters only", "txtself", 0);
                    return;
                }
                int srcount = Convert.ToInt32(coun);
                //DataSet dsself = ClsBLGD.GetcondDataSet("*", "tblShelf", "Se_name", srow);
                //if (dsself.Tables[0].Rows.Count > 0)
                //{
                //    lblmod.Text = "shelf with below name already exists.Click Modify to edit details";
                //    int code = Convert.ToInt32(dsself.Tables[0].Rows[0]["se_code"].ToString());
                //    // txtrows.Text = dsself.Tables[0].Rows[0]["srcount"].ToString();
                //    lblcode.Text = Convert.ToString(code);
                //    Master.ShowModal("shelf with above name already exists", "txtself", 0);
                //    return;
                //    // Table3.Visible = true;
                //}

                if (!File.Exists(filename))
                {
                    //ClsBLGP.Self("INSERT_SELF", srow);

                    switch (srcount)
                    {

                        case 1:
                            using (SqlConnection conn1 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn1.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd1 = new SqlCommand())
                                {

                                    conn1.Open();
                                    SqlCommand com1 = new SqlCommand();
                                    com1.Connection = conn1;
                                    com1.CommandType = CommandType.Text;

                                    com1.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com1.ExecuteNonQuery();
                                    break;

                                }
                            }

                        case 2:
                            using (SqlConnection conn2 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn2.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd2 = new SqlCommand())
                                {

                                    conn2.Open();
                                    SqlCommand com2 = new SqlCommand();
                                    com2.Connection = conn2;
                                    com2.CommandType = CommandType.Text;

                                    com2.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com2.ExecuteNonQuery();
                                    break;

                                }
                            }

                        case 3:
                            using (SqlConnection conn3 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn3.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd2 = new SqlCommand())
                                {

                                    conn3.Open();
                                    SqlCommand com3 = new SqlCommand();
                                    com3.Connection = conn3;
                                    com3.CommandType = CommandType.Text;

                                    com3.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com3.ExecuteNonQuery();
                                    break;

                                }
                            }

                        case 4:
                            using (SqlConnection conn4 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn4.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd4 = new SqlCommand())
                                {

                                    conn4.Open();
                                    SqlCommand com4 = new SqlCommand();
                                    com4.Connection = conn4;
                                    com4.CommandType = CommandType.Text;

                                    com4.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com4.ExecuteNonQuery();
                                    break;

                                }
                            }

                        case 5:
                            using (SqlConnection conn5 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn5.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd5 = new SqlCommand())
                                {

                                    conn5.Open();
                                    SqlCommand com5 = new SqlCommand();
                                    com5.Connection = conn5;
                                    com5.CommandType = CommandType.Text;

                                    com5.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com5.ExecuteNonQuery();
                                    break;

                                }
                            }

                        case 6:
                            using (SqlConnection conn6 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn6.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd6 = new SqlCommand())
                                {

                                    conn6.Open();
                                    SqlCommand com6 = new SqlCommand();
                                    com6.Connection = conn6;
                                    com6.CommandType = CommandType.Text;

                                    com6.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com6.ExecuteNonQuery();
                                    break;

                                }
                            }

                        case 7:
                            using (SqlConnection conn7 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn7.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd7 = new SqlCommand())
                                {

                                    conn7.Open();
                                    SqlCommand com7 = new SqlCommand();
                                    com7.Connection = conn7;
                                    com7.CommandType = CommandType.Text;

                                    com7.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com7.ExecuteNonQuery();
                                    break;

                                }
                            }

                        case 8:
                            using (SqlConnection conn8 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn8.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd8 = new SqlCommand())
                                {

                                    conn8.Open();
                                    SqlCommand com8 = new SqlCommand();
                                    com8.Connection = conn8;
                                    com8.CommandType = CommandType.Text;

                                    com8.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com8.ExecuteNonQuery();
                                    break;

                                }
                            }


                        case 9:
                            using (SqlConnection conn9 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn9.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd9 = new SqlCommand())
                                {

                                    conn9.Open();
                                    SqlCommand com9 = new SqlCommand();
                                    com9.Connection = conn9;
                                    com9.CommandType = CommandType.Text;

                                    com9.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com9.ExecuteNonQuery();
                                    break;

                                }
                            }

                        case 10:
                            using (SqlConnection conn10 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn10.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd10 = new SqlCommand())
                                {

                                    conn10.Open();
                                    SqlCommand com10 = new SqlCommand();
                                    com10.Connection = conn10;
                                    com10.CommandType = CommandType.Text;

                                    com10.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com10.ExecuteNonQuery();
                                    break;

                                }
                            }

                        case 11:
                            using (SqlConnection conn11 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn11.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd11 = new SqlCommand())
                                {

                                    conn11.Open();
                                    SqlCommand com11 = new SqlCommand();
                                    com11.Connection = conn11;
                                    com11.CommandType = CommandType.Text;

                                    com11.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com11.ExecuteNonQuery();
                                    break;

                                }
                            }


                        case 12:
                            using (SqlConnection conn12 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn12.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd11 = new SqlCommand())
                                {

                                    conn12.Open();
                                    SqlCommand com12 = new SqlCommand();
                                    com12.Connection = conn12;
                                    com12.CommandType = CommandType.Text;

                                    com12.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11','12',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com12.ExecuteNonQuery();
                                    break;

                                }
                            }

                        case 13:
                            using (SqlConnection conn13 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn13.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd11 = new SqlCommand())
                                {

                                    conn13.Open();
                                    SqlCommand com13 = new SqlCommand();
                                    com13.Connection = conn13;
                                    com13.CommandType = CommandType.Text;

                                    com13.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11','12','13',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com13.ExecuteNonQuery();
                                    break;

                                }
                            }


                        case 14:
                            using (SqlConnection conn14 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn14.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd14 = new SqlCommand())
                                {

                                    conn14.Open();
                                    SqlCommand com14 = new SqlCommand();
                                    com14.Connection = conn14;
                                    com14.CommandType = CommandType.Text;

                                    com14.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11','12','13','14',NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com14.ExecuteNonQuery();
                                    break;

                                }
                            }

                        case 15:
                            using (SqlConnection conn15 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn15.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd15 = new SqlCommand())
                                {

                                    conn15.Open();
                                    SqlCommand com15 = new SqlCommand();
                                    com15.Connection = conn15;
                                    com15.CommandType = CommandType.Text;

                                    com15.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11','12','13','14','15',NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com15.ExecuteNonQuery();
                                    break;

                                }
                            }


                        case 16:
                            using (SqlConnection conn16 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn16.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd16 = new SqlCommand())
                                {

                                    conn16.Open();
                                    SqlCommand com16 = new SqlCommand();
                                    com16.Connection = conn16;
                                    com16.CommandType = CommandType.Text;

                                    com16.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11','12','13','14','15','16',NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com16.ExecuteNonQuery();
                                    break;

                                }
                            }

                        case 17:
                            using (SqlConnection conn17 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn17.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd17 = new SqlCommand())
                                {

                                    conn17.Open();
                                    SqlCommand com17 = new SqlCommand();
                                    com17.Connection = conn17;
                                    com17.CommandType = CommandType.Text;

                                    com17.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11','12','13','14','15','16','17',NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com17.ExecuteNonQuery();
                                    break;

                                }
                            }

                        case 18:
                            using (SqlConnection conn18 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                                //conn18.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd18 = new SqlCommand())
                                {

                                    conn18.Open();
                                    SqlCommand com18 = new SqlCommand();
                                    com18.Connection = conn18;
                                    com18.CommandType = CommandType.Text;

                                    com18.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11','12','13','14','15','16','17','18',NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com18.ExecuteNonQuery();
                                    break;

                                }
                            }

                        case 19:
                            using (SqlConnection conn19 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn19.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd19 = new SqlCommand())
                                {

                                    conn19.Open();
                                    SqlCommand com19 = new SqlCommand();
                                    com19.Connection = conn19;
                                    com19.CommandType = CommandType.Text;

                                    com19.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11','12','13','14','15','16','17','18','19',NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com19.ExecuteNonQuery();
                                    break;

                                }
                            }

                        case 20:
                            using (SqlConnection conn20 = new SqlConnection(strconn11))
                            {
                                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                               // conn20.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                                using (SqlCommand cmd20 = new SqlCommand())
                                {

                                    conn20.Open();
                                    SqlCommand com20 = new SqlCommand();
                                    com20.Connection = conn20;
                                    com20.CommandType = CommandType.Text;

                                    com20.CommandText = "insert into tblShelf(Se_name,srcount,row_1,row_2,row_3,row_4,row_5,row_6,row_7,row_8,row_9,row_10,row_11,row_12,row_13,row_14,row_15,row_16,row_17,row_18,row_19,row_20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11','12','13','14','15','16','17','18','19',20,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')";
                                    com20.ExecuteNonQuery();
                                    break;

                                }
                            }

                    }
                }
                else
                {
                    //string srcount = Convert.ToString(txtrows.Text);
                    switch (srcount)
                    {
                        case 1:
                            String strconn11 = Dbconn.conmenthod();
                            OleDbConnection con1 = new OleDbConnection(strconn11);
                            con1.Open();
                            OleDbCommand cmd1 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con1);
                            cmd1.ExecuteNonQuery();
                            con1.Close();
                            break;
                        case 2:
                            String strconn12 = Dbconn.conmenthod();
                            OleDbConnection con2 = new OleDbConnection(strconn12);
                            con2.Open();
                            OleDbCommand cmd2 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con2);
                            cmd2.ExecuteNonQuery();
                            con2.Close();
                            break;
                        case 3:
                            String strconn13 = Dbconn.conmenthod();
                            OleDbConnection con3 = new OleDbConnection(strconn13);
                            con3.Open();
                            OleDbCommand cmd3 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con3);
                            cmd3.ExecuteNonQuery();
                            con3.Close();

                            break;

                        case 4:
                            String strconn14 = Dbconn.conmenthod();
                            OleDbConnection con4 = new OleDbConnection(strconn14);
                            con4.Open();
                            OleDbCommand cmd4 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con4);
                            cmd4.ExecuteNonQuery();
                            con4.Close();
                            break;
                        case 5:
                            String strconn15 = Dbconn.conmenthod();
                            OleDbConnection con5 = new OleDbConnection(strconn15);
                            con5.Open();
                            OleDbCommand cmd5 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con5);
                            cmd5.ExecuteNonQuery();
                            con5.Close();
                            break;

                        case 6:
                            String strconn16 = Dbconn.conmenthod();
                            OleDbConnection con6 = new OleDbConnection(strconn16);
                            con6.Open();
                            OleDbCommand cmd6 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con6);
                            cmd6.ExecuteNonQuery();
                            con6.Close();
                            break;

                        case 7:
                            String strconn17 = Dbconn.conmenthod();
                            OleDbConnection con7 = new OleDbConnection(strconn17);
                            con7.Open();
                            OleDbCommand cmd7 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con7);
                            cmd7.ExecuteNonQuery();
                            con7.Close();
                            break;

                        case 8:
                            String strconn18 = Dbconn.conmenthod();
                            OleDbConnection con8 = new OleDbConnection(strconn18);
                            con8.Open();
                            OleDbCommand cmd8 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con8);
                            cmd8.ExecuteNonQuery();
                            con8.Close();
                            break;

                        case 9:
                            String strconn19 = Dbconn.conmenthod();
                            OleDbConnection con9 = new OleDbConnection(strconn19);
                            con9.Open();
                            OleDbCommand cmd9 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con9);
                            cmd9.ExecuteNonQuery();
                            con9.Close();
                            break;

                        case 10:
                            String strconn20 = Dbconn.conmenthod();
                            OleDbConnection con10 = new OleDbConnection(strconn20);
                            con10.Open();
                            OleDbCommand cmd10 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con10);
                            cmd10.ExecuteNonQuery();
                            con10.Close();
                            break;

                        case 11:
                            String strconn21 = Dbconn.conmenthod();
                            OleDbConnection con11 = new OleDbConnection(strconn21);
                            con11.Open();
                            OleDbCommand cmd11 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con11);
                            cmd11.ExecuteNonQuery();
                            con11.Close();
                            break;

                        case 12:
                            String strconn22 = Dbconn.conmenthod();
                            OleDbConnection con12 = new OleDbConnection(strconn22);
                            con12.Open();
                            OleDbCommand cmd12 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11','12',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con12);
                            cmd12.ExecuteNonQuery();
                            con12.Close();
                            break;

                        case 13:
                            String strconn23 = Dbconn.conmenthod();
                            OleDbConnection con13 = new OleDbConnection(strconn23);
                            con13.Open();
                            OleDbCommand cmd13 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11','12','13',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con13);
                            cmd13.ExecuteNonQuery();
                            con13.Close();
                            break;

                        case 14:
                            String strconn24 = Dbconn.conmenthod();
                            OleDbConnection con14 = new OleDbConnection(strconn24);
                            con14.Open();
                            OleDbCommand cmd14 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11','12','13','14',NULL,NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con14);
                            cmd14.ExecuteNonQuery();
                            con14.Close();
                            break;

                        case 15:
                            String strconn25 = Dbconn.conmenthod();
                            OleDbConnection con15 = new OleDbConnection(strconn25);
                            con15.Open();
                            OleDbCommand cmd15 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11','12','13','14','15',NULL,NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con15);
                            cmd15.ExecuteNonQuery();
                            con15.Close();
                            break;

                        case 16:
                            String strconn26 = Dbconn.conmenthod();
                            OleDbConnection con16 = new OleDbConnection(strconn26);
                            con16.Open();
                            OleDbCommand cmd16 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11','12','13','14','15','16',NULL,NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con16);
                            cmd16.ExecuteNonQuery();
                            con16.Close();
                            break;

                        case 17:
                            String strconn27 = Dbconn.conmenthod();
                            OleDbConnection con17 = new OleDbConnection(strconn27);
                            con17.Open();
                            OleDbCommand cmd17 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11','12','13','14','15','16','17',NULL,NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con17);
                            cmd17.ExecuteNonQuery();
                            con17.Close();
                            break;

                        case 18:
                            String strconn28 = Dbconn.conmenthod();
                            OleDbConnection con18 = new OleDbConnection(strconn28);
                            con18.Open();
                            OleDbCommand cmd18 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11','12','13','14','15','16','17','18',NULL,NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con18);
                            cmd18.ExecuteNonQuery();
                            con18.Close();
                            break;
                        case 19:
                            String strconn29 = Dbconn.conmenthod();
                            OleDbConnection con19 = new OleDbConnection(strconn29);
                            con19.Open();
                            OleDbCommand cmd19 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11','12','13','14','15','16','17','18','19',NULL,'" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con19);
                            cmd19.ExecuteNonQuery();
                            con19.Close();
                            break;

                        case 20:
                            String strconn30 = Dbconn.conmenthod();
                            OleDbConnection con20 = new OleDbConnection(strconn30);
                            con20.Open();
                            OleDbCommand cmd20 = new OleDbCommand("insert into tblShelf(Se_name,srcount,row1,row2,row3,row4,row5,row6,row7,row8,row9,row10,row11,row12,row13,row14,row15,row16,row17,row18,row19,row20,LoginName,Mac_id,Sysdatetime) values('" + srow + "','" + srcount + "','01','02','03','04','05','06','07','08','09','10','11','12','13','14','15','16','17','18','19','20','" + Session["username"].ToString() + "','" + sMacAddress + "','" + Sysdatetime + "')", con20);
                            cmd20.ExecuteNonQuery();
                            con20.Close();
                            break;


                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
             

                lblsuccess.Visible = true;
                lblsuccess.Text = "inserted successfully";
                //Master.ShowModal("inserted successfully", "txtself", 0);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                Bind();
                selecting();
                updateGrid.Update();
                txtself.Text = string.Empty;
                txtrows.Text = string.Empty;
                txtself.Focus();
                txtself.Enabled = true;

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
       //string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
       // string oConn = ConfigurationManager.AppSettings["ConnectionString"];
        SqlConnection conn = new SqlConnection(strconn11);
        conn.Open();
        SqlCommand cmd = new SqlCommand("select Se_name from tblShelf where Se_name like @1+'%'", conn);
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
            OleDbCommand cmd=new OleDbCommand("select Se_name from tblShelf where Se_name like @1+'%'", conn);
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
        mod();
        Table3.Visible = false;
        txtrows.Enabled = true;
        txtself.Enabled = false;
        txtrows.Focus();
       // unselect();
        // Gridviewshelf.DataSource = null;

       // selecting();
       
        updateGrid.Update();
        
    }
    public string mod()
    {
        button_select = dbcon.modify();

        if (button_select == "Modify")
        {
            txtself.Enabled = true;
        }
        return button_select;
    }
    public void no()
    {
        Table3.Visible = false;
        txtrows.Enabled = true;
        txtself.Enabled = true;
        txtself.Text = "";
        txtrows.Text = "";
        txtself.Focus();
        return;
    }
    protected void btn_Click(object sender, EventArgs e)
    {      
        no();
    }

    protected void txtrows_TextChanged(object sender, EventArgs e)
    {                                                                                         // for each       
        string row = txtrows.Text;
        int row1 = Convert.ToInt32(row);
        try
        {
            if (row1 > 20 || row1==0)
            {
                //lblsuccess.Text = "Entered rack number must be less then 20 and greater then 0";
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                Master.ShowModal("Entered rack number must be less than 20 and greater than 0", "txtrows",0);
                txtrows.Text = string.Empty;
                return;
                //txtrows.Text = string.Empty; 
                txtrows.Focus();
                row = string.Empty;
                return;
            }
            else
            {
                btnsave.Focus();
                lblsuccess.Text = "";
                return;
            }
        }
        catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Enabled = true;
                lblerror.Text = asd;
            }
       
        //unselect();
    }

    protected void txtself_TextChanged(object sender, EventArgs e)
    {       
        string nam = txtself.Text;
        try{
        if (nam.Length == 1)
        {
            lblsuccess.Text = "Enter 3 characters only";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            // Master.ShowModal("Enter 3 characters only", "txtself", 0);
            txtself.Text = "";
            txtself.Focus();
            return;
        }
        else if (nam.Length == 2)
        {
            lblsuccess.Text = "Enter 3 characters only";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            // Master.ShowModal("Enter 3 characters only","txtself",0);
            txtself.Text = "";
            txtself.Focus();
            return;
        }
        else if (nam.Length == 3)
        {
            txtrows.Focus();
            lblsuccess.Text = "";
        }
        }
         catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Enabled = true;
                lblerror.Text = asd;
            }
        try
        {
            DataSet dsself = ClsBLGD.GetcondDataSet("*", "tblShelf", "Se_name", nam);
            if (dsself.Tables[0].Rows.Count > 0)
            {

                lblmod.Text = "shelf with below name already exists.Click Modify to edit details";
                int code = Convert.ToInt32(dsself.Tables[0].Rows[0]["se_code"].ToString());
                txtrows.Text = dsself.Tables[0].Rows[0]["srcount"].ToString();
                lblcode.Text = Convert.ToString(code);
              //  Gridviewshelf.DataSource = null;
                //updateGrid.Update();
                // unselect();
                Table3.Visible = true;
                txtself.Enabled = false;
                txtrows.Enabled = false;
                btn.Focus();
                updateGrid.Update();
                // Bind();
            }
        }
         catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Enabled = true;
                lblerror.Text = asd;
            }

    }
    protected void btnrefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("Shelf.aspx");
    }


    public void selecting()
    {
        
        string svshelf = txtself.Text;
        string svrow = txtrows.Text;
        int svcode = Convert.ToInt16(svrow);
        if (svcode > 20)
        {
            lblsuccess.Text = "number of Racks must not be greater then 20";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            //Master.ShowModal("Racks must only be maximum 20", "txtrows", 0);
            txtrows.Text = "";
            txtrows.Focus();
            return;
        }


        if (Gridviewshelf.Rows.Count <= 0)
        {
            //Master.ShowModal("No records available,click ok to enter new record", "txtrows", 0);
            txtrows.Focus();
            return;
        }

        else
        {
            try{
            int col = Gridviewshelf.Rows[0].Cells.Count;
            foreach (GridViewRow row in Gridviewshelf.Rows)
            {
                for (int i = 0; i < col; i++)
                {
                    if (row.Cells[0].Text == svshelf)
                    {
                        for (int j = 1; j < col; j++)
                        {
                            for (int k = 1; k <= svcode; k++)
                            {
                         
                                if (svcode == 1)
                                {
                                  row.Cells[k].BackColor = System.Drawing.Color.Red;
                                }
                                else if (svcode == 2)
                                {
                                    row.Cells[k].BackColor = System.Drawing.Color.Red;

                                }
                                else if (svcode == 3)
                                {
                                    row.Cells[k].BackColor = System.Drawing.Color.Red;

                                }
                                else if (svcode == 4)
                                {
                                    row.Cells[k].BackColor = System.Drawing.Color.Red;

                                }
                                else if (svcode == 5)
                                {
                                    row.Cells[k].BackColor = System.Drawing.Color.Red;
                                }
                                else if (svcode == 6)
                                {
                                   
                                    row.Cells[k].BackColor = System.Drawing.Color.Red;
                                }
                                else if (svcode == 7)
                                {

                                    row.Cells[k].BackColor = System.Drawing.Color.Red;

                                }
                                else if (svcode == 8)
                                {

                                    row.Cells[k].BackColor = System.Drawing.Color.Red;

                                }
                                else if (svcode == 9)
                                {

                                    row.Cells[k].BackColor = System.Drawing.Color.Red;
                                }
                                else if (svcode == 10)
                                {

                                    row.Cells[k].BackColor = System.Drawing.Color.Red;
                                }
                                else if (svcode == 11)
                                {
                                    row.Cells[k].BackColor = System.Drawing.Color.Red;
                                }
                                else if (svcode == 12)
                                {

                                    row.Cells[k].BackColor = System.Drawing.Color.Red;
                                }
                                else if (svcode == 13)
                                {
                                   
                                        row.Cells[k].BackColor = System.Drawing.Color.Red;

                                }
                                else if (svcode == 14)
                                {
                                   
                                        row.Cells[k].BackColor = System.Drawing.Color.Red;
 
                                }
                                else if (svcode == 15)
                                {
                                   
                                        row.Cells[k].BackColor = System.Drawing.Color.Red;

                                }
                                else if (svcode == 16)
                                {
                                    
                                        row.Cells[k].BackColor = System.Drawing.Color.Red;
                                }
                                else if (svcode == 17)
                                {
                                    
                                        row.Cells[k].BackColor = System.Drawing.Color.Red;
                                }
                                else if (svcode == 18)
                                {
                                 
                                        row.Cells[k].BackColor = System.Drawing.Color.Red;

                                }
                                else if (svcode == 19)
                                {
                                      row.Cells[k].BackColor = System.Drawing.Color.Red;

                                }
                                else if (svcode == 20)
                                {
                                    
                                    row.Cells[k].BackColor = System.Drawing.Color.Red;

                                }

                            }                                                                            //for k
                        }                                                                                //for j
                    }                                                                                   //if  svshelf                   
                }                                                                                      //for i
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
    //public void unselect()
    //{
    //    string svshelf1 = txtself.Text;
    //    string svrow1 = txtrows.Text;
    //    int svcode1 = Convert.ToInt16(svrow1);
    //    if (svcode1 > 20)
    //    {
    //        lblsuccess.Text = "number of Racks must not be greater then 20";
    //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
    //        //Master.ShowModal("Racks must only be maximum 20", "txtrows", 0);
    //        txtrows.Text = "";
    //        txtrows.Focus();
    //        return;
    //    }


    //    if (Gridviewshelf.Rows.Count <= 0)
    //    {
    //        //Master.ShowModal("No records available,click ok to enter new record", "txtrows", 0);
    //        txtrows.Focus();
    //        return;
    //    }

    //    else
    //    {
    //        int col = Gridviewshelf.Rows[0].Cells.Count;
    //        foreach (GridViewRow row in Gridviewshelf.Rows)
    //        {
    //            for (int i = 0; i < col; i++)
    //            {
    //                if (row.Cells[0].Text == svshelf1)
    //                {
    //                    for (int j = 1; j < col; j++)
    //                    {
    //                        for (int k = 1; k <= svcode1; k++)
    //                        {
    //                            if (svcode1 == 1)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                row.Cells[i].BackColor = System.Drawing.Color.Blue;
    //                            }

    //                            else if (svcode1 == 2)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }
    //                            else if (svcode1 == 3)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }
    //                            else if (svcode1 == 4)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }
    //                            else if (svcode1 == 5)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }
    //                            else if (svcode1 == 6)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }
    //                            else if (svcode1 == 7)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }
    //                            else if (svcode1 == 8)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }
    //                            else if (svcode1 == 9)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }
    //                            else if (svcode1 == 10)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }
    //                            else if (svcode1 == 11)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }
    //                            else if (svcode1 == 12)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }
    //                            else if (svcode1 == 13)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }
    //                            else if (svcode1 == 14)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }
    //                            else if (svcode1 == 15)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }
    //                            else if (svcode1 == 16)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }
    //                            else if (svcode1 == 17)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }
    //                            else if (svcode1 == 18)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }
    //                            else if (svcode1 == 19)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }
    //                            else if (svcode1 == 20)
    //                            {
    //                                row.Cells[k].BackColor = System.Drawing.Color.Yellow;
    //                                //k++;
    //                            }

    //                        }                                                                            //for k
    //                    }                                                                                //for j
    //                }                                                                                   //if  svshelf                   
    //            }                                                                                      //for i
    //        }
    //    }
    //}
//}

   
   




