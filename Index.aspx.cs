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
using System.Threading;
using System.Data.OleDb;


public partial class Index : System.Web.UI.Page
{
    ClsBLLGeneraldetails clsBALGeneral = new ClsBLLGeneraldetails();
    ClsDALGeneraldetails clsDALGeneral = new ClsDALGeneraldetails();
    BackupDbTask bdTask = new BackupDbTask();
    Dbconn db1 = new Dbconn();
    Dbconn dbcon = new Dbconn();
    String sMacAddress = "";
    string macid = "";
    string numm;
    string button;
    protected static string button_select;
    protected static string strconn11 = Dbconn.conmenthod();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        Message.Visible = false;
        txtUserName.Enabled = true;
        txtUserName.Focus();
        GetMACAddress();
       // Image1.Visible = true;
        //Image1.ImageUrl = "~/Images/topfile.ico";
        if (txtUserName.Text != "")
        {
            SqlConnection conr = new SqlConnection(strconn11);
            SqlCommand cmdr = new SqlCommand("select * from tblLogin where UserName = '"+ txtUserName.Text + "'", conr);
            DataSet dsr = new DataSet();
            SqlDataAdapter dar = new SqlDataAdapter(cmdr);
            dar.Fill(dsr);
                numm = dsr.Tables[0].Rows[0]["Flag4"].ToString();
        }
       

        if (Session["random"] != null)
        {

            string session = Session["random"].ToString();

            if (session != numm)
            {

                // txtUserName.Text = Session["username"].ToString();
               // ShowModal1("Another one is logged in, do you want to log him out", "txtUserName", 1);
                
                Message.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Someone Has been Logged In from Your User If Not You Please Change Your Password";

                // Master.ShowModal("Someone Has been Logged In from Your User If Not You Please Change Your Password", "txtUserName", 0);
                // return;

            }
            //Session.Abandon();
        }
        if (!IsPostBack)
        {
            // string strLotNo = clsBALGeneral.SlashReplace("2013-14-1-0000009-14/1");
            string strKeyValue = "";
           // Table2.Visible = false;
            tblpanel.Visible = false;
            string strLoggedOut = "";
            string strKey = Request.QueryString["Name"];
            if (strKey != null)
            {
                strKeyValue = DecryptQueryString(strKey);
            }
            string strLogged = Request.QueryString["LogOut"];
            if (strLogged != null)
            {
                strLoggedOut = DecryptQueryString(strLogged);
            }
            if (strKeyValue == "Loading Completed")
            {

            }
            else if (strLoggedOut == "LoggedOut")
            {

            }
            else
            {
                Response.Redirect("Loading.aspx");
            }
        }
    }
    private string DecryptQueryString(string strQueryString)
    {
        EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
        return objEDQueryString.Decrypt(strQueryString, "r0b1nr0y");
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
            // sMacAddress = sMacAddress.Replace(":", "");
        } return sMacAddress;
    }

    public void Login()
    {
        string filename = Dbconn.Mymenthod();
        string strUserName = txtUserName.Text;
        string strPassword = txtPassword.Text;
        string strDecodedPwd = clsBALGeneral.base64Encode(strPassword);
        SqlConnection con = new SqlConnection(strconn11);
        con.Open();
        SqlCommand cmd = new SqlCommand("insert into tblMacid(MacId,UserName)values('" + sMacAddress + "','" + strUserName + "')", con);
        cmd.ExecuteNonQuery();

        string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
        Random rand = new Random();
        int num = rand.Next(0, chars.Length - 1);



        if (strUserName == "")
        {
            Message.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Text = "* Username cannot be empty !";
            lblMessage.Style.Add("text-decoration", "blink");
            txtUserName.Focus();
            return;
        }
        if (strPassword == "")
        {
            Message.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Text = "* Password cannot be empty !";
            lblMessage.Style.Add("text-decoration", "blink");
            txtPassword.Focus();
            return;
        }



        DataSet dsLoginDet = clsBALGeneral.GetcondDataSet3("*", "tblLogin", "Username", strUserName, "Password", strDecodedPwd, "ClosedFlag", "N");
        DataSet dsMacid = clsBALGeneral.GetcondDataSet("*", "tblMacid", "UserName", strUserName);

        if (dsMacid.Tables[0].Rows[0].IsNull("UserName"))
        {
        }
        else if (dsMacid.Tables[0].Rows.Count > 0)
        {
            macid = dsMacid.Tables[0].Rows[0]["MacId"].ToString();
        }

        if (dsLoginDet.Tables[0].Rows.Count > 0)
        {
            string strClosedFlag = dsLoginDet.Tables[0].Rows[0]["ClosedFlag"].ToString();
            string strLoggedIn = dsLoginDet.Tables[0].Rows[0]["LoggedIn"].ToString();
            if (strClosedFlag == "Y")
            {
                Message.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "* User has been Deleted. !!!!";
                lblMessage.Style.Add("text-decoration", "blink");
                clsBALGeneral.ClearInputs(Page.Controls);
                txtUserName.Focus();
            }


       
            else
            {
                Session["username"] = txtUserName.Text;

                //Response.Redirect("AdminHome.aspx");
                HttpCookie LoginCookie = new HttpCookie("LoginCookie");
                LoginCookie["UserNameCookie"] = txtUserName.Text;
                Response.Cookies.Add(LoginCookie);


                clsBALGeneral.UpdateRecords("tblLogin", "Flag4='" + num + "'", "UserName='" + txtUserName.Text + "'");
                Session["random"] = num;
                clsBALGeneral.UpdateRecords("tblLogin", "LoggedIn='Y'", "UserName='" + txtUserName.Text + "'");
                    //ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
                    //Dbconn dbcon = new Dbconn();
                    //SqlConnection con58 = new SqlConnection(strconn1);
                    //con58.Open();
                    //SqlCommand cmd58 = new SqlCommand("delete FROM tbltempprodsale where LoginName = '" + txtUserName.Text + "'", con58);
                
                    //cmd58.ExecuteNonQuery();
                
                Server.Transfer("Home.aspx");
            }
        }
        else
        {
            Message.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Text = "* Username or Password is Wrong. !!!!";
            lblMessage.Style.Add("text-decoration", "blink");
            clsBALGeneral.ClearInputs(Page.Controls);
            txtUserName.Focus();
        }

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string filename = Dbconn.Mymenthod();
        string strUserName = txtUserName.Text;
        string strPassword = txtPassword.Text;
        string strDecodedPwd = clsBALGeneral.base64Encode(strPassword);
        SqlConnection con =new SqlConnection(strconn11);
        con.Open();
        SqlCommand cmd=new SqlCommand("insert into tblMacid(MacId,UserName)values('" + sMacAddress + "','" + strUserName + "')",con);
        cmd.ExecuteNonQuery();

        string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
        Random rand = new Random();
        int num = rand.Next(0, chars.Length - 1);
       

        if (numm != "0")
        {
            tblpanel.Enabled = true;
            tblpanel.Visible = true;
            Table2.Visible = true;
            lblmod.Text = "This User is Already Logged In.Do You Want to kill That Session!!!!";
           // ShowModal1("This User is Already Logged in.Do You want to log him out", "btnLogin", 1);
            btn.Enabled = true;
            btn.Focus();
            return;
           
            //ErrorMessage.Show();
            if (button_select == "Yes")
            {
                Login();               
                
            }
            else if (button_select == "No")
            {
                Response.Redirect("Index.aspx");
                return;
            }
           

        }

     
        if (strUserName == "")
        {
            Message.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Text = "* Username cannot be empty !";
            lblMessage.Style.Add("text-decoration", "blink");
            txtUserName.Focus();
            return;
        }
        if (strPassword == "")
        {
            Message.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Text = "* Password cannot be empty !";
            lblMessage.Style.Add("text-decoration", "blink");
            txtPassword.Focus();
            return;
        }
        
            
           
                DataSet dsLoginDet = clsBALGeneral.GetcondDataSet3("*", "tblLogin", "Username", strUserName, "Password", strDecodedPwd, "ClosedFlag", "N");
                DataSet dsMacid = clsBALGeneral.GetcondDataSet("*", "tblMacid", "UserName", strUserName);
        
                if (dsMacid.Tables[0].Rows[0].IsNull("UserName"))
                {
                }
                else if (dsMacid.Tables[0].Rows.Count > 0)
                {
                     macid = dsMacid.Tables[0].Rows[0]["MacId"].ToString();
                }

                if (dsLoginDet.Tables[0].Rows.Count > 0)
                {
                    string strClosedFlag = dsLoginDet.Tables[0].Rows[0]["ClosedFlag"].ToString();
                    string strLoggedIn = dsLoginDet.Tables[0].Rows[0]["LoggedIn"].ToString();
                    if (strClosedFlag == "Y")
                    {
                        Message.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "* User has been Deleted. !!!!";
                        lblMessage.Style.Add("text-decoration", "blink");
                        clsBALGeneral.ClearInputs(Page.Controls);
                        txtUserName.Focus();
                    }
                
               
                //else if ((strLoggedIn == "Y") && (macid!=sMacAddress))
                // else if (strLoggedIn == "Y")
                //{
                //    Message.Visible = true;
                //    lblMessage.Visible = true;
                //    lblMessage.Text = "* This User is already Logged In. !!!!";
                //    lblMessage.Style.Add("text-decoration", "blink");
                //    clsBALGeneral.ClearInputs(Page.Controls);
                //    txtUserName.Focus();
                //}
                else
                {
                    Session["username"] = txtUserName.Text;

                    //Response.Redirect("AdminHome.aspx");
                    HttpCookie LoginCookie = new HttpCookie("LoginCookie");
                    LoginCookie["UserNameCookie"] = txtUserName.Text;
                    Response.Cookies.Add(LoginCookie);

                    /*HttpCookie PwdCookie = new HttpCookie("PwdCookie");
                    PwdCookie["PasswordCookie"] = txtPassword.Text;
                    Response.Cookies.Add(PwdCookie);*/


                    /*HttpCookie username = new HttpCookie(txtUserName.Text, "a");
                    HttpCookie password = new HttpCookie(txtPassword.Text, "a");
                    Response.Cookies.Add(username);
                    Response.Cookies.Add(password);
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddHours(1);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddHours(1);*/
                    
                        clsBALGeneral.UpdateRecords("tblLogin", "LoggedIn='Y'", "UserName='" + txtUserName.Text + "'");
                        clsBALGeneral.UpdateRecords("tblLogin", "Flag4='" + num + "'", "UserName='" + txtUserName.Text + "'");
                        Session["random"] = num;
                        Server.Transfer("Home.aspx");
                }
        }
        else
        {
            Message.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Text = "* Username or Password is Wrong. !!!!";
            lblMessage.Style.Add("text-decoration", "blink");
            clsBALGeneral.ClearInputs(Page.Controls);
            txtUserName.Focus();
        }
                //txtPassword.Text = strPassword;
    }


    protected void txtUserName_TextChanged(object sender, EventArgs e)
    {
        string strconn11 = ConfigurationManager.AppSettings["ConnectionString"];
        // MemoryStream stream = new MemoryStream();
        SqlConnection con1 = new SqlConnection(strconn11);
        
        con1.Open();

        SqlCommand cmd = new SqlCommand("select Photo from tblUsercreation where Abbr='" + txtUserName.Text + "'", con1);

        SqlDataReader reader = cmd.ExecuteReader();

        reader.Read();
        if (reader.HasRows)
        {


            byte[] imagem = (byte[])(reader[0]);
            string base64String = Convert.ToBase64String(imagem, 0, imagem.Length);

            Image1.ImageUrl = "data:image/png;base64," + base64String;
            Image1.Visible = true;


        }
        txtPassword.Enabled = true;
        txtPassword.Focus();
    }
    public void ShowModal1(string strErrorMessage, string ctrl, int intClear)
    {
        btnOk.Enabled = true;
        btnNo.Enabled = true;
        btnOk.Focus();
        //btnCloseError.Focus();
        ErrorMessage.Show();
        lblErrorMessage.Visible = true;
        lblErrorMessage.Text = strErrorMessage;
        Session["Control"] = ctrl;
        Session["ClearControl"] = intClear;
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        ErrorMessage.Hide();
        Login();
       

    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        ErrorMessage.Hide();
        Response.Redirect("Index.aspx");
       // Response.Redirect("Home.aspx");
    }
    public string mod()
    {
        button_select = dbcon.modify();
        if (button_select == "Yes")
        {
            Response.Redirect("Home.aspx");
        }
        return button_select;
    }
    protected static string strconn1 = Dbconn.conmenthod();
    protected void btn_Click(object sender, EventArgs e)
    {

      
        SqlConnection con1 = new SqlConnection(strconn1);
        con1.Open();
        SqlCommand cmd1 = new SqlCommand("delete FROM tbltempprodsale where LoginName = '" + txtUserName.Text + "'", con1);
        cmd1.ExecuteNonQuery();

        Login();
        
        
      //  txtPassword.Enabled = true;
        tblpanel.Visible = false;

    }
    protected void btnmodify_Click(object sender, EventArgs e)
    {
        Response.Redirect("Index.aspx");
        tblpanel.Visible = false;
    }
    
}