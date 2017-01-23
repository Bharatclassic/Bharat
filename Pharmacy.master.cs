using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Microsoft.Win32;
using System.Data.OleDb;
using System.Drawing;

public partial class Pharmacy : System.Web.UI.MasterPage
{
    ClsBLLGeneraldetails clsBLLGeneral = new ClsBLLGeneraldetails();
    EncryptDecryptQueryString EncDycQyStr = new EncryptDecryptQueryString();
    protected static string strconn11 = Dbconn.conmenthod();

    //protected void Page_Init(object sender, EventArgs e)
    //{
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
    //    Response.Cache.SetNoStore();
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] != null)
        {
            int count = 0;
            string strDesignation = "";
            string strconn11 = Dbconn.conmenthod();
            int i = 0;


            SqlConnection conr = new SqlConnection(strconn11);
            SqlCommand cmdr = new SqlCommand("select * from tblLogin where UserName = '" + Session["username"] + "'", conr);
            DataSet dsr = new DataSet();
            SqlDataAdapter dar = new SqlDataAdapter(cmdr);
            dar.Fill(dsr);

            string numm = dsr.Tables[0].Rows[0]["Flag4"].ToString();

            string session = Session["random"].ToString();
            if (session != numm)
            {

                Response.Redirect("Index.aspx");
            }

            SqlConnection conb = new SqlConnection(strconn11);
            SqlCommand cmdb = new SqlCommand("select * from tblProductMaster", conb);
            DataSet ds1 = new DataSet();
            SqlDataAdapter da1 = new SqlDataAdapter(cmdb);
            da1.Fill(ds1);
            for (i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                
                Double reorder = Convert.ToDouble(ds1.Tables[0].Rows[i]["Reorderlevel"].ToString());
                string code = ds1.Tables[0].Rows[i]["Productcode"].ToString();
                SqlCommand cmdsum = new SqlCommand("select sum(Stockinhand) as stockinhand from tblProductinward where Productcode='" + code + "' ", conb);
                DataSet dssum = new DataSet();
                SqlDataAdapter dasum = new SqlDataAdapter(cmdsum);
                dasum.Fill(dssum);
                if (dssum.Tables.Count > 0)
                {
                    if (dssum.Tables[0].Rows[0]["stockinhand"].ToString() != "")
                    {
                        double sumstock = Convert.ToDouble(dssum.Tables[0].Rows[0]["stockinhand"].ToString());
                        
                        if (sumstock <= reorder && sumstock!=0)
                        {
                            info1.Visible = true;
                            {
                                string nameofprod = ds1.Tables[0].Rows[i]["Productname"].ToString();
                                if (count == 0)
                                {
                                    strDesignation += " " + nameofprod + " " + sumstock.ToString();
                                }
                                else { strDesignation += "--" + nameofprod + " " + sumstock.ToString(); }
                            }
                            count++;
                        }

                    }
                }

                /*if (i == ds1.Tables[0].Rows.Count)
                {
                    i = 0;
                }*/
            }
            lblMarquee.Text = strDesignation;
            //------------------------mahesh bhat 17-12-2016---------------------------------
            string expiredate = "";
            int count1 = 0;
            DateTime checkdate = DateTime.Now;
            checkdate = checkdate.AddDays(90);
            String checkdate1 = checkdate.ToString("yyyy/MM/dd");
            SqlCommand cmdExp = new SqlCommand("Select a.Stockinhand as stockinhand,b.Pharmflag,a.ProductName as PROD from tblProductinward a left join tblProductMaster b on a.Productcode=b.Productcode where a.Expiredate <'" + checkdate1 + "' and a.Stockinhand>'0' and b.Pharmflag !='N' group by a.Stockinhand,a.ProductName,b.Pharmflag ", conb);
            SqlDataAdapter daExp = new SqlDataAdapter(cmdExp);
            DataSet dsExp = new DataSet();
            daExp.Fill(dsExp);
            for (int j = 0; j < dsExp.Tables[0].Rows.Count; j++)
            {
                double stock = Convert.ToDouble(dsExp.Tables[0].Rows[j]["stockinhand"].ToString());
                string prodname = dsExp.Tables[0].Rows[j]["PROD"].ToString();
                info2.Visible = true;
                if (count1 == 0)
                {
                    expiredate += " " + prodname + " " + stock.ToString();
                }
                else
                {
                    expiredate += "--" + prodname + " " + stock.ToString();
                }
                count1++;
            }
            LabelExp.Text = expiredate;
            if (!IsPostBack)
            {


                GetMenuData();
                DataSet dsLoginDetails = clsBLLGeneral.GetcondDataSet2("*", "tblLogin", "UserName", Session["username"].ToString(), "ClosedFlag", "N");
                // DataSet dsEmpDetails = clsBLLGeneral.GetcondDataSet2("*", "tblEmployeeMaster", "EmpCode", dsLoginDetails.Tables[0].Rows[0]["EmpCode"].ToString(), "CLosedFlag", "N");
                string strEmpName = dsLoginDetails.Tables[0].Rows[0]["UserName"].ToString();
                string strflag = dsLoginDetails.Tables[0].Rows[0]["flag1"].ToString();
                //string strconn11 = ConfigurationManager.AppSettings["ConnectionString"];
                // MemoryStream stream = new MemoryStream();
                SqlConnection con11 = new SqlConnection(strconn11);
                SqlCommand cmd1 = new SqlCommand("Select * from tblBankname", con11);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblbankname.Text = ds.Tables[0].Rows[0]["PharmacyName"].ToString();
                }
                SqlConnection con1 = new SqlConnection(strconn11);
                con1.Open();

                SqlCommand cmd = new SqlCommand("select Photo from tblUsercreation where Abbr='" + strEmpName + "' and Admin='" + strflag + "'", con1);

                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                if (reader.HasRows)
                {



                    byte[] imagem = (byte[])(reader[0]);
                    string base64String = Convert.ToBase64String(imagem, 0, imagem.Length);
                    if (base64String == "")
                    {
                    }
                    else
                    {

                        Image1.ImageUrl = "data:image/png;base64," + base64String;
                        Image1.Visible = true;
                    }



                }
                //byte[] image = (byte[])cmd.ExecuteScalar();
                //stream.Write(image, 0, image.Length);
                //con1.Close();
                // Bitmap bitmap = new Bitmap(stream);

                //Image1.Visible = true;
                // else
                // {
                //  DataSet dsDesignationDet = clsBLLGeneral.GetcondDataSet2("*", "tblDesignationMaster", "DesignationCode", dsEmpDetails.Tables[0].Rows[0]["Designation"].ToString(), "CloseFlag", "N");
                // strDesignation = dsDesignationDet.Tables[0].Rows[0]["DesignationName"].ToString();
                //}
                lblEmpName.Text = "Welcome ! " + strEmpName;
                lblDesignation.Text = strDesignation;
                

            }

            FillDate();
        }
        else
        {
            Response.Redirect("Index.aspx");
        }

        /*if (Convert.ToString(Session["UserName"]).Length <= 0)
        {
            Response.Redirect(Page.ResolveUrl("~/Home.aspx"));
        } */

        string strPreviousPage = "";
        if (Request.UrlReferrer != null)
        {
            strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
        }
        if (strPreviousPage == "")
        {
            Response.Redirect("Home.aspx");
        }

        // Build the HTML meta tag
        HtmlMeta meta = new HtmlMeta();
        meta.Name = "DownloadOptions";

        // Disable the save button
        meta.Content = "nosave";


        // Add the meta tag to the page
        Page.Header.Controls.Add(meta);

    }
    private string DecryptQueryString(string strQueryString)
    {
        EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
        return objEDQueryString.Decrypt(strQueryString, "r0b1nr0y");
    }
    public void FillDate()
    {
        DateTime dtNow = DateTime.Now;
        lblTodayDateText.Text = dtNow.ToString("dd/MM/yyyy");
    }
    protected void lnkBtnLogout_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(strconn11);
        con.Open();
        SqlCommand cmd = new SqlCommand("delete from tblMacid where UserName='" + (Session["username"]) + "'", con);
        cmd.ExecuteNonQuery();
        
        clsBLLGeneral.UpdateRecords("tblLogin", "LoggedIn='N'", "UserName='" + (Session["username"]) + "'");
        Session.Abandon();
        Response.Redirect("Index.aspx?LogOut=" + EncryptQueryString("LoggedOut"));

        //*******************************************
        Response.Write("<script>javascript: parent.opener=''; " +
                                "parent.close();</script>");

        //*******************************************************
    }

    //*****************************************************
    protected void Session_End(Object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]).Length > 0)
        {
            // Your code to update database>
            Session["username"] = null;
        }
    }
    private void GetMenuData()
    {
        string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            DataTable table = new DataTable();
            string strCon = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(strCon);
            string sql = "";
            string strSessionName = Session["username"].ToString();
            DataSet dsEmpDet = clsBLLGeneral.GetcondDataSet("*", "tblLogin", "UserName", Session["username"].ToString());
            if ("A" == dsEmpDet.Tables[0].Rows[0]["Flag1"].ToString())
            {
                sql = "select menu_id, menu_name, menu_parent_id, menu_url from menuMaster where sflag='Y'";

              //  sql = "select menu_id, menu_name, menu_parent_id, menu_url from menuMaster";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);
                DataView view = new DataView(table);
                view.RowFilter = "menu_parent_id is NULL";
                foreach (DataRowView row in view)
                {
                    MenuItem menuItem = new MenuItem(row["menu_name"].ToString(), row["menu_id"].ToString());
                    menuItem.NavigateUrl = row["menu_url"].ToString();
                    menuBar.Items.Add(menuItem);
                    AddChildItems(table, menuItem);
                }
            }
            else
            {
                sql = "select menu_id, menu_name, menu_parent_id, menu_url from checkMaster where abbre='" + Session["username"].ToString() + "' and flag='N'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);
                DataView view = new DataView(table);
                //view.RowFilter = "menu_parent_id=" + 0;
                view.RowFilter = "menu_parent_id is NULL";
                foreach (DataRowView row in view)
                {
                    MenuItem menuItem = new MenuItem(row["menu_name"].ToString(),
                    row["menu_id"].ToString());
                    menuItem.NavigateUrl = row["menu_url"].ToString();
                    menuBar.Items.Add(menuItem);
                    AddChildItems(table, menuItem);
                }
            }
        }
        else
        {
            DataTable table = new DataTable();
            string strCon = ConfigurationManager.AppSettings["Connect"]; 
            OleDbConnection conn = new OleDbConnection(strCon);
            string sql = "";
            string strSessionName = Session["username"].ToString();
            DataSet dsEmpDet = clsBLLGeneral.GetcondDataSet("*", "tblLogin", "UserName", Session["username"].ToString());
            if ("A" == dsEmpDet.Tables[0].Rows[0]["Flag1"].ToString())
            {
                sql = "select menu_id, menu_name, menu_parent_id, menu_url from menuMaster where sflag='Y'";

               // sql = "select menu_id, menu_name, menu_parent_id, menu_url from menuMaster";

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(table);
                DataView view = new DataView(table);
                view.RowFilter = "menu_parent_id is NULL";
                foreach (DataRowView row in view)
                {
                    MenuItem menuItem = new MenuItem(row["menu_name"].ToString(), row["menu_id"].ToString());
                    menuItem.NavigateUrl = row["menu_url"].ToString();
                    menuBar.Items.Add(menuItem);
                    AddChildItems(table, menuItem);
                }
            }
            else
            {
                sql = "select menu_id, menu_name, menu_parent_id, menu_url from checkMaster where name='" + Session["username"].ToString() + "' and flag='N'";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(table);
                DataView view = new DataView(table);
                view.RowFilter = "menu_parent_id=" + 0;
                foreach (DataRowView row in view)
                {
                    MenuItem menuItem = new MenuItem(row["menu_name"].ToString(),
                    row["menu_id"].ToString());
                    menuItem.NavigateUrl = row["menu_url"].ToString();
                    menuBar.Items.Add(menuItem);
                    AddChildItems(table, menuItem);
                }
            }
        }

    }
    private void AddChildItems(DataTable table, MenuItem menuItem)
    {
        DataView viewItem = new DataView(table);
        viewItem.RowFilter = "menu_parent_id=" + menuItem.Value;
        foreach (DataRowView childView in viewItem)
        {
            /*DataSet dsMLog = clsBLLGeneral.GetDataSet("*", "tblM_Log");
            string strLotNumberFlag = dsMLog.Tables[0].Rows[0]["LotNumber"].ToString();
            string strOldLoanEntry = dsMLog.Tables[0].Rows[0]["OldLoan"].ToString();
            string strConnString = dsMLog.Tables[0].Rows[0]["EncrConn"].ToString();*/


            string strMenuName = childView["menu_name"].ToString();



            /*if (strOldLoanEntry == "N")
            {
                if (strMenuName == "Old Loan Entry")
                {
                    continue;
                }
                else
                {
                    if (strMenuName == "Stock Inward")
                    {
                        if (strLotNumberFlag == "A")
                        {
                            MenuItem childItem = new MenuItem(childView["menu_name"].ToString(), childView["menu_id"].ToString());
                            childItem.NavigateUrl = childView["menu_url"].ToString();
                            menuItem.ChildItems.Add(childItem);
                            AddChildItems(table, childItem);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (strMenuName == "Stock Inward(Manual)")
                    {
                        if (strMenuName == "Old Loan Entry")
                        {
                            continue;
                        }
                        else
                        {
                            if (strLotNumberFlag == "A")
                            {
                                continue;
                            }
                            else
                            {
                                MenuItem childItem = new MenuItem(childView["menu_name"].ToString(), childView["menu_id"].ToString());
                                childItem.NavigateUrl = childView["menu_url"].ToString();
                                menuItem.ChildItems.Add(childItem);
                                AddChildItems(table, childItem);
                            }
                        }
                    }
                    else
                    {
                        if (strMenuName == "Security")
                        {
                            if (strConnString == "E")
                            {
                                MenuItem childItem = new MenuItem(childView["menu_name"].ToString(), childView["menu_id"].ToString());
                                childItem.NavigateUrl = childView["menu_url"].ToString();
                                menuItem.ChildItems.Add(childItem);
                                AddChildItems(table, childItem);
                            }
                            else if (strConnString == "D")
                            {
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            MenuItem childItem = new MenuItem(childView["menu_name"].ToString(), childView["menu_id"].ToString());
                            childItem.NavigateUrl = childView["menu_url"].ToString();
                            menuItem.ChildItems.Add(childItem);
                            AddChildItems(table, childItem);
                        }
                    }
                }
            }
            else
            {
                if (strMenuName == "Old Loan Entry")
                {
                    MenuItem childItem = new MenuItem(childView["menu_name"].ToString(), childView["menu_id"].ToString());
                    childItem.NavigateUrl = childView["menu_url"].ToString();
                    menuItem.ChildItems.Add(childItem);
                    AddChildItems(table, childItem);
                }
                else
                {
                    if (strMenuName == "Stock Inward" || strMenuName == "Stock Inward(Manual)")
                    {
                        continue;
                    }
                    else
                    {
                        if (strMenuName == "Security")
                        {
                            if (strConnString == "E")
                            {
                                MenuItem childItem = new MenuItem(childView["menu_name"].ToString(), childView["menu_id"].ToString());
                                childItem.NavigateUrl = childView["menu_url"].ToString();
                                menuItem.ChildItems.Add(childItem);
                                AddChildItems(table, childItem);
                            }
                            else if (strConnString == "D")
                            {
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {*/
                            MenuItem childItem = new MenuItem(childView["menu_name"].ToString(), childView["menu_id"].ToString());
                            childItem.NavigateUrl = childView["menu_url"].ToString();
                            menuItem.ChildItems.Add(childItem);
                            AddChildItems(table, childItem);
                        
                    
                
            
        }
    }
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static void Updatestatus()
    {
        ClsBLLGeneraldetails clsBALGeneral = new ClsBLLGeneraldetails();
        clsBALGeneral.UpdateRecords("Login", "loggedIn='N'", "username='" + HttpContext.Current.Session["username"].ToString() + "'");
        HttpContext.Current.Session.Abandon();
    }
    public string EncryptQueryString(string strQueryString)
    {
        EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
        string strEncryptedQueryString = objEDQueryString.Encrypt(strQueryString, "r0b1nr0y");
        return objEDQueryString.Encrypt(strQueryString, "r0b1nr0y");
    }
    protected void btnCloseError_Click(object sender, EventArgs e)
    {
        ErrorMessage.Hide();
        Control ctrl = (Control)ContentPlaceHolder1.FindControl(Session["Control"].ToString());
        
        int intClearControl = (int)Session["ClearControl"];
        if (ctrl is TextBox)
        {
            if (intClearControl == 1)
            {
                TextBox txtCtrl = (TextBox)ctrl;
                txtCtrl.Text = string.Empty;
                txtCtrl.Focus();
            }
            else
            {
                TextBox txtCtrl = (TextBox)ctrl;
                txtCtrl.Focus();
            }
        }
        if (ctrl is DropDownList)
        {

            DropDownList ddlCtrl = (DropDownList)ctrl;
            ddlCtrl.SelectedIndex = -1;
            ddlCtrl.Focus();


        }
        if (ctrl is RadioButtonList)
        {

            RadioButtonList rdoList = (RadioButtonList)ctrl;
            rdoList.SelectedIndex = -1;
            rdoList.Focus();
        }

        if (ctrl is DataGrid)
        {
            DataGrid dg = (DataGrid)ctrl;
            dg.SelectedIndex = -1;
            dg.Focus();
        }

        if (ctrl is GridView)
        {
            GridView dgv = (GridView)ctrl;
            dgv.SelectedIndex = -1;
            dgv.Focus();
        }
    }
    public void ShowModal(string strErrorMessage, string ctrl, int intClear)
    {
        btnCloseError.Enabled = true;
        btnCloseError.Focus();
        ErrorMessage.Show();
        lblErrorMessage.Visible = true;
        lblErrorMessage.Text = strErrorMessage;
        Session["Control"] = ctrl;
        Session["ClearControl"] = intClear;
    }
    protected void lnkBtnLogout_Click1(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(strconn11);
        con.Open();
        SqlCommand cmd = new SqlCommand("delete from tblMacid where UserName='" + (Session["username"]) + "'", con);
        cmd.ExecuteNonQuery();
        clsBLLGeneral.UpdateRecords("tblLogin", "LoggedIn='N'", "UserName='" + (Session["username"]) + "'");
        clsBLLGeneral.UpdateRecords("tblLogin", "Flag4='0'", "UserName='" + (Session["username"]) + "' ");
        Session.Abandon();
        Response.Redirect("Index.aspx?LogOut=" + EncryptQueryString("LoggedOut"));

        //*******************************************
        Response.Write("<script>javascript: parent.opener=''; " +
                                "parent.close();</script>");
    }

    protected void lnkProduct_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductSearch.aspx");
    }





   }
