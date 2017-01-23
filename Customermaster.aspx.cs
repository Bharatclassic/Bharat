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

public partial class Customermaster : System.Web.UI.Page
{
    ClsBALCustomermaster ClsBLGP=new ClsBALCustomermaster ();
    ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected static string button_select;

    protected static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();

    DataTable tblcustomer = new DataTable();
    DataTable dt = new DataTable();
    DataRow drrw;
    //string result = "";
    int count;
   // int cuis;
    string custid;
   // string str;
    String sMacAddress="";
    ArrayList arryno = new ArrayList();
    ArrayList arryno1=new ArrayList ();

    ArrayList arrynm=new ArrayList ();
    ArrayList arrynm1=new ArrayList ();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror.Visible = false;
        lblsuccess.Visible = false;
        lblcode.Visible=false;
        txtmobile.Attributes.Add("autocomplete", "off");
        txtcredit.Attributes.Add("autocomplete", "off");
        txtemail.Attributes.Add("autocomplete", "off");
        txtdoorno.Attributes.Add("autocomplete", "off");
        txtadd1.Attributes.Add("autocomplete", "off");
        txtadd2.Attributes.Add("autocomplete", "off");
      //
        if(!Page.IsPostBack)
        {
        System.DateTime Dtnow = DateTime.Now;
        string Sysdatetime= Dtnow.ToString("dd/MM/yyyy");
        txtdate.Text=Sysdatetime;
        txtdate.Focus();
        Table2.Visible = false;
        lblcode.Visible = false;
        btndelete.Enabled = false;
        lbladd1.Visible=false;
        lbladd2.Visible=false;
        lblsadd1.Visible = false;
        lblsadd2.Visible = false;
        lblshobli.Visible = true;
        lblstaluk.Visible = true;
        txtadd1.Visible=false;
        txtadd2.Visible=false;
        lblcity.Visible=false;
        txtcity.Visible=false;
        chkrural.Checked=true;
        txtcredit.Visible=true;
        lblcredit.Visible=true;
        lblscity.Visible = false;
       // Table3.Visible=false;
        txtcustcode.Visible = false;
        Label2.Visible = false;
        refcd();
        refnm();
        Bind();
       // tab();
        txtcustname.Focus();
           // autoincrement();
        }
        
        txtcustcode.Enabled = false;
         if (Session["username"] != null)
        {

        }
        else
        {
            Response.Redirect("Index.aspx");
            
        }
       
        ClientScript.RegisterStartupScript(this.GetType(), "SetInitialFocus", "<script>document.getElementById('" + txtdate.ClientID + "').focus();</script>");
        btnexit.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtdate.ClientID + "').focus();return false;}} else {return true}; ");
        GetMACAddress();
    }
    public void refcd()
     {
        try{
        DataSet dsrfcd=ClsBLGD.GetDataSet("distinct CA_code","tblCustomer");
        for(int i=0;i<dsrfcd.Tables[0].Rows.Count;i++)
        {
            arryno.Add(dsrfcd.Tables[0].Rows[i]["CA_code"].ToString());
        }
        arryno.Sort();
        arryno1.Add("-Select-");
        for(int j=0;j<arryno.Count;j++)
        {
            arryno1.Add(arryno[j]);
        }
        ddrefcode.DataSource=arryno1;
        ddrefcode.DataBind();
        }
        catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Enabled = true;
                lblerror.Text = asd;
            }
    }
    public string autoincrement()
    {
       try{
         if (!File.Exists(filename))
          {
             SqlConnection con = new SqlConnection(strconn11);
             con.Open();
             SqlCommand cmd=new SqlCommand("select Max(CA_code) as CA_code from tblCustomer",con);
             SqlDataAdapter da=new SqlDataAdapter(cmd);
             DataSet ds=new DataSet();
             da.Fill(ds);
            
             
             if(ds.Tables[0].Rows.Count>0)
             {

                 custid=ds.Tables[0].Rows[0]["CA_code"].ToString();
                 double custid1 = Convert.ToDouble(custid);
                 if(custid=="")
                 {
                     custid="0001";
                     txtcustcode.Text=custid;
                     //txtcustmcode.Text=custid;
                 }
                 else
                 {
                    // count=Convert.ToInt16(cmd.ExecuteScalar()) + 1;
                    // custid="000"+count;
                     //txtcustcode.Text=custid;
                     //txtcustmcode.Text=custid;

                     if (custid1 >= 0009)
                     {
                         count = Convert.ToInt16(cmd.ExecuteScalar()) + 1;
                         custid = "00" + count;
                     }
                     else
                     {

                         count = Convert.ToInt16(cmd.ExecuteScalar()) + 1;
                         custid = "000" + count;
                         //txtcustcode.Text = custid;
                         //txtcustmcode.Text=custid;
                     }
                 }
             }
             con.Close();
         }
         else
         {
             OleDbConnection con = new OleDbConnection(strconn11);
             OleDbCommand cmd1=new OleDbCommand("select Max(CA_code) as Cust_code from tblCustomer",con);
             con.Open();
             OleDbDataAdapter da1=new OleDbDataAdapter(cmd1);
             DataSet ds1=new DataSet ();
             da1.Fill(ds1);
             if(ds1.Tables[0].Rows.Count>0)
             {

                 custid=ds1.Tables[0].Rows[0]["Cust_code"].ToString();
                 double custid1 = Convert.ToDouble(custid);
                 if(custid=="")
                 {
                     custid="0001";
                 }
                 else
                 {
                    // count=Convert.ToInt16(cmd1.ExecuteScalar()) + 1;
                     //custid="000"+count;

                     if (custid1 >= 0009)
                     {
                         count = Convert.ToInt16(cmd1.ExecuteScalar()) + 1;
                         custid = "00" + count;
                     }
                     else
                     {

                         count = Convert.ToInt16(cmd1.ExecuteScalar()) + 1;
                         custid = "000" + count;
                         //txtcustcode.Text = custid;
                         //txtcustmcode.Text=custid;
                     }
                 }
             }
             con.Close();
         }
       }
       catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Enabled = true;
                lblerror.Text = asd;
            }
        return custid;
    }
 
    
    public void refnm()
    {
        try{
       DataSet dsrfnm=ClsBLGD.GetDataSet("distinct CA_name","tblCustomer");
        for(int i=0;i<dsrfnm.Tables[0].Rows.Count;i++)
        {
            arrynm.Add(dsrfnm.Tables[0].Rows[i]["CA_name"].ToString());
        }
        arrynm.Sort();
        arrynm1.Add("-Select-");
        for(int j=0;j<arrynm.Count;j++)
        {
            arrynm1.Add(arrynm[j]);
        }
        ddrfnm.DataSource=arrynm1;
        ddrfnm.DataBind();
        }
         catch (Exception ex)
         {
           string asd = ex.Message;
           lblerror.Enabled = true;
           lblerror.Text = asd;
         }
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
      }   return sMacAddress;
    }
  
    public void Bind()
    {
        //string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            try
            {
                Gridcust.DataSource=null;
                Gridcust.DataBind();
                tblcustomer.Rows.Clear();
                SqlConnection con = new SqlConnection(strconn11);
                SqlCommand cmd = new SqlCommand("select * from tblCustomer order by Srno", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                 if (ds.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("slno", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblcustomer.Columns.Add(col);
                    tblcustomer.Columns.Add("CUSTOMER TYPE");
                    tblcustomer.Columns.Add("CUSTOMER NAME");
                      tblcustomer.Columns.Add("CUSTOMER CODE");
                    tblcustomer.Columns.Add(" HOBLI");
                    tblcustomer.Columns.Add(" TALUK");
                    tblcustomer.Columns.Add(" CITY");
                    tblcustomer.Columns.Add(" STATE");
                    tblcustomer.Columns.Add("CREDIT LIMIT");
                       tblcustomer.Columns.Add("MOBILENO");
                    tblcustomer.Columns.Add("EMAIL");
                    tblcustomer.Columns.Add("REFERRED CODE");
                    tblcustomer.Columns.Add("REFERED NAME");
                      

                    Session["Customermaster"] = tblcustomer;

                   for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tblcustomer = (DataTable)Session["Customermaster"];
                        drrw = tblcustomer.NewRow();

                        drrw["CUSTOMER TYPE"] = ds.Tables[0].Rows[i]["CA_type"].ToString();
                        drrw["CUSTOMER NAME"] = ds.Tables[0].Rows[i]["CA_name"].ToString();
                        drrw["CUSTOMER CODE"] = ds.Tables[0].Rows[i]["CA_code"].ToString();
                        drrw[" HOBLI"] = ds.Tables[0].Rows[i]["Hobli"].ToString();
                        drrw[" TALUK"] = ds.Tables[0].Rows[i]["Taluk"].ToString();
                        drrw[" CITY"] = ds.Tables[0].Rows[i]["City"].ToString();
                        drrw[" STATE"] = ds.Tables[0].Rows[i]["State"].ToString();
                        drrw["CREDIT LIMIT"] = ds.Tables[0].Rows[i]["Credit_limit"].ToString();
                         drrw["Mobileno"] = ds.Tables[0].Rows[i]["Mobileno"].ToString();
                        drrw["Email"] = ds.Tables[0].Rows[i]["Email"].ToString();
                        drrw["REFERRED CODE"]=ds.Tables[0].Rows[i]["Rf_code"].ToString();
                        drrw["REFERED NAME"] = ds.Tables[0].Rows[i]["Rf_name"].ToString();
                     
                        tblcustomer.Rows.Add(drrw);
                        //Gridform.DataSource = tblformmaster;
                     }
                      DataView dw = tblcustomer.DefaultView;
                      dw.Sort = "slno ASC";
                      Gridcust.DataSource = tblcustomer;
                      Gridcust.DataBind();
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
                Gridcust.DataSource=null;
                Gridcust.DataBind();
                tblcustomer.Rows.Clear();
                OleDbConnection conn10 = new OleDbConnection(strconn11);
                conn10.Open();
                OleDbCommand cmd1 = new OleDbCommand("select * from tblCustomer order by CA_name", conn10);

                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                 if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataColumn col = new DataColumn("slno", typeof(int));
                    col.AutoIncrement = true;
                    col.AutoIncrementSeed = 1;
                    col.AutoIncrementStep = 1;
                    tblcustomer.Columns.Add(col);
                    tblcustomer.Columns.Add("CUSTOMER TYPE");
                    tblcustomer.Columns.Add("CUSTOMER NAME");
                    tblcustomer.Columns.Add(" HOBLI");
                    tblcustomer.Columns.Add(" TALUK");
                    tblcustomer.Columns.Add(" CITY");
                    tblcustomer.Columns.Add(" STATE");
                    tblcustomer.Columns.Add("CREDIT LIMIT");
                    tblcustomer.Columns.Add("REFFERRED CODE");
                    tblcustomer.Columns.Add("REFFERED NAME");
                      

                    Session["Customermaster"] = tblcustomer;

                   for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        tblcustomer = (DataTable)Session["Customermaster"];
                        drrw = tblcustomer.NewRow();

                        drrw["CUSTOMER TYPE"] = ds1.Tables[0].Rows[i]["CA_type"].ToString();
                        drrw["CUSTOMER NAME"] = ds1.Tables[0].Rows[i]["CA_name"].ToString();
                        drrw[" HOBLI"] = ds1.Tables[0].Rows[i]["Hobli"].ToString();
                        drrw[" TALUK"] = ds1.Tables[0].Rows[i]["Taluk"].ToString();
                        drrw[" CITY"] = ds1.Tables[0].Rows[i]["City"].ToString();
                        drrw[" STATE"] = ds1.Tables[0].Rows[i]["State"].ToString();
                        drrw["CREDIT LIMIT"] = ds1.Tables[0].Rows[i]["Credit_limit"].ToString();
                        drrw["REFFERRED CODE"]=ds1.Tables[0].Rows[i]["Rf_code"].ToString();
                        drrw["REFFERED NAME"] = ds1.Tables[0].Rows[i]["Rf_name"].ToString();
                     
                        tblcustomer.Rows.Add(drrw);
                        //Gridform.DataSource = tblformmaster;
                     }
                      DataView dw = tblcustomer.DefaultView;
                      dw.Sort = "slno ASC";
                      Gridcust.DataSource = tblcustomer;
                      Gridcust.DataBind();
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


    protected void chkrural_CheckedChanged(object sender, EventArgs e)
    {
        if(chkrural.Checked==true)
        {
            chkurban.Checked=false;
            lbladd1.Visible=false;
            lbladd2.Visible=false;
            txtadd1.Visible=false;
            txtadd2.Visible=false;
            txtcredit.Visible=true;
            lblcredit.Visible=true;
            lbldoorno.Visible=false;
            txtdoorno.Visible=true;
            lblsdoorno.Visible = true;
            lblhobli.Visible=true;
            txthobli.Visible=true;
            lbltaluk.Visible=true;
            txttaluk.Visible=true;
            lblcity.Visible=false;
            txtcity.Visible=false;
            lbldoorno.Visible=true;
            lbldist.Visible=true;
            txtdist.Visible=true;
            lblsadd1.Visible = false;
            lblsadd2.Visible = false;
            lblshobli.Visible = true;
            lblstaluk.Visible = true;
            lblsdist.Visible = true;
            lblscity.Visible = false;

        }
        //else if(chkrural.Checked==false)
        //{
            
        //}

    }
    protected void chkurban_CheckedChanged(object sender, EventArgs e)
    {
        if(chkurban.Checked==true)
        {
            chkrural.Checked=false;
            lbldoorno.Visible=false;
            txtdoorno.Visible=false;
            lblsdoorno.Visible = false;
            lblsdoorno.Visible = false;
            lblhobli.Visible=false;
            txthobli.Visible=false;
            lbltaluk.Visible=false;
            txttaluk.Visible=false;
            lbldist.Visible=false;
            txtdist.Visible=false;
            lblcity.Visible=true;
            txtcity.Visible=true;
            lbladd1.Visible=true;
            lbladd2.Visible=true;
            txtadd1.Visible=true;
            txtadd2.Visible=true;
            txtcredit.Visible=true;
            lblcredit.Visible=true;
            lblsadd1.Visible = true;
            lblsadd2.Visible = true;
            lblshobli.Visible = false;
            lblstaluk.Visible = false;
            lblsdist.Visible = false;
            lblscity.Visible = true;

        }
        //else if(chkurban.Checked==false)
        //{
        //   lbldoorno.Visible=true;
        //    txtdoorno.Visible=true;
        //    lblhobli.Visible=true;
        //    txthobli.Visible=true;
        //    lbltaluk.Visible=true;
        //    txttaluk.Visible=true;
        //    txtcredit.Visible=true;
        //    txtcredit.Visible=true;
        //    lblcredit.Visible=true;
           
        //}

      
    }
 
    protected void btnsave_Click(object sender, EventArgs e)
    {
          if (button_select == "Modify")
    {
     try
      {
           int crlimit1=0;
           string custname=txtcustname.Text;
           string custtype1=null;
           string doorr=txtdoorno.Text;
           string addre1=txtadd1.Text;
           string addre2=txtadd2.Text;
           string hobli1=txthobli.Text;
           string taluk1=txttaluk.Text;
           string distr1=txtdist.Text;
           string city1=txtcity.Text;
           string state1=txtstate.Text;
           string rfcod1=ddrefcode.Text;
           string Mobileno=txtmobile.Text;
           string Email=txtemail.Text;
            
           if(rfcod1=="-Select-")
           {
               rfcod1=string.Empty;
           }
          // int rfcode1=Convert.ToInt32(rfcod1);
           string rfname1=ddrfnm.Text;
             if(rfname1=="")
             {
                 rfname1=string.Empty;
             }
          string credlimit1=txtcredit.Text;
          if(credlimit1=="")
           {
            crlimit1=0;
           }
          int cramt1=0;
          string Loginname1="";
          //System.DateTime Dtnow = DateTime.Now;
          //string Sysdatetime1= Dtnow.ToString("dd/MM/yyyy");
          //txtdate.Text=Sysdatetime1;
         string Sysdatetime1=txtdate.Text;

          string cod = lblcode.Text;
          int c = Convert.ToInt32(cod);

           if(chkrural.Checked==true)
            {
                custtype1=chkrural.Checked ? "RURAL" : "URBAN";
            }
            else if(chkurban.Checked==true)
            {
                custtype1=chkrural.Checked ? "RURAL" : "URBAN";
            }
            if(txtadd1.Text=="")
            {
                addre1="0";
            }
            if(txtadd2.Text=="")
            {
                addre2="0";
            }
            if(txtdoorno.Text=="")
            {
                doorr="0";
            }
           if(txthobli.Text=="")
           {
             hobli1=string.Empty;
           }
           if(txtcity.Text=="")
           {
             city1=string.Empty;
           }
           if(txtdist.Text=="")
           {
             distr1=string.Empty;
           }
           if(txttaluk.Text=="")
           {
             taluk1="0";
           }
         if(chkrural.Checked==true)
         {
             if(txtdoorno.Text=="")
             {
                 Master.ShowModal("doorno mandatory","txtdoorno",0);
                 return;
             }
             if(txthobli.Text=="")
             {
                 Master.ShowModal("hobli mandatory","txthobli",0);
                 return;
             }
             if(txttaluk.Text=="")
             {
                 Master.ShowModal("Taluka mandatory","txttaluk",0);
                 return;
             }
             if(txtdist.Text=="")
             {
                 Master.ShowModal("District mandatory","txtdist",0);
                 return;
             }
             if(txtstate.Text=="")
             {
                 Master.ShowModal("State mandatory","txtstate",0);
                 return;
             }
              //if(rfcod1=="-Select-")
              //  {
              //      Master.ShowModal("Select Reference code", "txtdist", 0);
              //      return;
              //  }
              //  if(rfname1=="-Select-")
              //  {
              //       Master.ShowModal("please enter reference name details", "txtdist", 0);
              //       return;
              //  }
         }
         else if(chkurban.Checked==true)
         {
             if(txtadd1.Text=="")
                {
                    Master.ShowModal("Address1 mandatory", "txtadd1", 0);
                    return;
                }
                if(txtadd2.Text=="")
                {
                    Master.ShowModal("Address2 mandatory", "txtadd2", 0);
                    return;
                }
                if(txtcity.Text=="")
                {
                    Master.ShowModal("City mandatory", "txtcity", 0);
                    return;
                }
                if(txtstate.Text=="")
                {
                    Master.ShowModal("State mandatory", "txtstate", 0);
                    return;
                }
                if(txtcredit.Text=="")
                {
                    Master.ShowModal("please enter credit", "txtcredit", 0);
                    return;
                }
               //if(rfcod1=="-Select-")
               // {
               //     Master.ShowModal("Select Reference code", "txtdist", 0);
               //     return;
               // }
               // if(rfname1=="-Select-")
               // {
               //      Master.ShowModal("please enter reference name details", "txtdist", 0);
               //      return;
               // }
           }
          else
            {
                Master.ShowModal("please select either urban or rural","chkrural",0);
            }
                if (!File.Exists(filename))
                {
                    if(chkrural.Checked==true)
                    {
                        string crlimit2=txtcredit.Text;

                    ClsBLGP.updateCustomer("UPDATE_CUSTOMER",custname,custtype1,doorr,addre2,hobli1,taluk1,distr1,state1,rfcod1,rfname1,crlimit2,"0",Mobileno,Email,city1,c);
                    }
                    else if(chkurban.Checked==true)
                    {
                         string crlimit2=txtcredit.Text;
                     ClsBLGP.updateCustomer("UPDATE_CUSTOMER",custname,custtype1,addre1,addre2,hobli1,taluk1,distr1,state1,rfcod1,rfname1,crlimit2,"0",Mobileno,Email,city1,c);
                    }
                 }
                else
                {
                    OleDbConnection conn10 = new OleDbConnection(Dbconn.conmenthod());
                    conn10.Open();
                    if(chkrural.Checked==true)
                    {
                    OleDbCommand cmd1 = new OleDbCommand("update tblCustomer set CA_name='" + custname + "',CA_type='" + custtype1 + "', Address1='" + doorr + "',Address2='" + addre2 + "',Hobli='" + hobli1 + "',Taluk='" + taluk1 + "',District='" + distr1 +"',State='" + state1 + "',Rf_code='" + rfcod1 + "',Rf_name='" + rfname1 +"',Credit_limit='" + crlimit1 + "',Credit_amount='" + cramt1 + "',Login_name='" + Loginname1 +"',Sysdatetime='" + Sysdatetime1 + "',Mac_id='" + sMacAddress + "',City='" + city1 + "' where Srno=" + c + "", conn10);
                    cmd1.ExecuteNonQuery();
                    }
                    else if(chkurban.Checked==true)
                    {
                    OleDbCommand cmd1 = new OleDbCommand("update tblCustomer set CA_name='" + custname + "',CA_type='" + custtype1 + "', Address1='" + addre1 + "',Address2='" + addre2 + "',Hobli='" + hobli1 + "',Taluk='" + taluk1 + "',District='" + distr1 +"',State='" + state1 + "',Rf_code='" + rfcod1 + "',Rf_name='" + rfname1 +"',Credit_limit='" + crlimit1 + "',Credit_amount='" + cramt1 + "',Login_name='" + Loginname1 +"',Sysdatetime='" + Sysdatetime1 + "',Mac_id='" + sMacAddress + "',City='" + city1 + "' where Srno=" + c + "", conn10);
                    cmd1.ExecuteNonQuery();
                    }
                    conn10.Close();
                }
            }

          catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }

              lblsuccess.Visible = true;
              lblsuccess.Text ="modified successfully";
              ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
              Bind();
              button_select = string.Empty;
              txtcustname.Text=string.Empty;
              txtadd1.Text=string.Empty;
              txtadd2.Text=string.Empty;
              txtdoorno.Text=string.Empty;
              txthobli.Text=string.Empty;
              txttaluk.Text=string.Empty;
              txtdist.Text=string.Empty;
              txtcity.Text=string.Empty;
              txtstate.Text=string.Empty;
              txtcredit.Text=string.Empty;
              txtADHAR.Text = string.Empty;
              txtPAN.Text = string.Empty;
              txtTIN.Text = string.Empty;
              txtGST.Text = string.Empty;
              ddrefcode.Text="-Select-";
              ddrfnm.Text="-Select-";
              chkrural.Checked=false;
              chkurban.Checked=false;
              txtcustname.Enabled=true;
              ddrefcode.Enabled=true;
              ddrfnm.Enabled=true;
              
            
          }

       else if (button_select != "Modify")
        {
         // string filename = Dbconn.Mymenthod();
            autoincrement();
           //Table3.Visible=true;
           // btnsave.Enabled=false;

         // if (button_select == "OK")
         //  {
            //  btnsave.Enabled=true;
             //  Table3.Visible=false;
        //   }
        //  else
        //  {
            //  Master.ShowModal("Click Ok", "txthobli", 0);
            //  return;
         // }
         

           

           try
           {
            int crlimit=0;
            string custid=autoincrement();
            string custnm=txtcustname.Text.TrimStart();
            string custtype=null;
            string door=txtdoorno.Text;
            string add1=txtadd1.Text;
            string add2=txtadd2.Text;
            string hobli=txthobli.Text;
            string taluk=txttaluk.Text;
            string distr=txtdist.Text;
            string state=txtstate.Text;
            string Mobileno=txtmobile.Text;
               string Email=txtemail.Text;
            string city=txtcity.Text;
            string rfcod=ddrefcode.Text;
            string pan = txtPAN.Text;
            string tin = txtTIN.Text;
            string adhar = txtADHAR.Text;
            string gst = txtGST.Text;
               if(rfcod=="-Select-")
               {
                   rfcod=string.Empty;
               }
          //  int rfcode=Convert.ToInt32(rfcod);
            string credlimit=txtcredit.Text;
              
               if(credlimit=="")
               {
                   credlimit=string.Empty;
               }
             //crlimit=Convert.ToInt32(credlimit);
             string rfname=ddrfnm.Text;
             if(rfname=="-Select-")
            {
                rfname=string.Empty;
                //rfname=txtcustname.Text;
            }
           //string cramt=0;
            string Loginname=Session["username"].ToString();
          //  GetMACAddress();
               string Sysdatetime=txtdate.Text;
             //string mac="result";
            if(chkrural.Checked==true)
            {
                custtype=chkrural.Checked ? "RURAL" : "URBAN";
            }
            else if(chkurban.Checked==true)
            {
                custtype=chkrural.Checked ? "RURAL" : "URBAN";
            }
            if(txtadd1.Text=="")
            {
                add1="NULL";
            }
            if(txtadd2.Text=="")
            {
                add2="NULL";
            }
            if(txtcity.Text=="")
            {
               city=string.Empty;
            }
            if(txtdist.Text=="")
            {
               distr=string.Empty;
            }
            if(txtdoorno.Text=="")
            {
               door="NULL";
            }
            if(txthobli.Text=="")
            {
              hobli=string.Empty;
            }
            if(txttaluk.Text=="")
            {
              taluk=string.Empty;
            }
            //if(ddrfnm.Text=="")
            //{
            //   rfname="NULL";
            //}
            if(chkrural.Checked==true)
            {
                 string strCaps1 = Regex.Replace(custnm, "[^a-zA-Z + \\s]", "");
                 string strEdited = Regex.Replace(strCaps1, @"\s+", " ");

                if (strEdited == "")
                {

                    Master.ShowModal("Customer Name mandatory", "txtformunitmaster", 0);
                    return;

                }
                if(txtdoorno.Text==string.Empty)
                {
                     Master.ShowModal("please enter door/village details", "txtdoorno", 0);
                    return;
                }
                if(txthobli.Text=="")
                {
                    Master.ShowModal("please enter Hobli details", "txthobli", 0);
                    return;
                }
                if(txttaluk.Text=="")
                {
                     Master.ShowModal("please enter Taluk details", "txttaluk", 0);
                    return;
                }
                if(txtdist.Text=="")
                {
                      Master.ShowModal("please enter District details", "txtdist", 0);
                     return;
                }
               if(txtstate.Text=="")
               {
                   Master.ShowModal("please enter State details", "txtstate", 0);
                     return;
               }
                //if(rfcod=="-Select-")
                //{
                //    Master.ShowModal("Select Reference code", "txtdist", 0);
                //    return;
                //}
                //if(rfname=="-Select-")
                //{
                //     Master.ShowModal("please enter reference name details", "txtdist", 0);
                //     return;
                //}
            }
            else if(chkurban.Checked==true)
            {
               string strCaps1 = Regex.Replace(custnm, "[^a-zA-Z + \\s]", "");
               string strEdited = Regex.Replace(strCaps1, @"\s+", " ");

                if (strEdited == "")
                {

                    Master.ShowModal("Customer Name mandatory", "txtformunitmaster", 0);
                    return;

                }
                if(txtadd1.Text=="")
                {
                    Master.ShowModal("Address1 mandatory", "txtadd1", 0);
                    return;
                }
                if(txtadd2.Text=="")
                {
                    Master.ShowModal("Address2 mandatory", "txtadd2", 0);
                    return;
                }
                if(txtcity.Text=="")
                {
                    Master.ShowModal("City mandatory", "txtcity", 0);
                    return;
                }
                if(txtstate.Text=="")
                {
                    Master.ShowModal("State mandatory", "txtstate", 0);
                    return;
                }
                if(txtcredit.Text=="")
                {
                    Master.ShowModal("please enter credit", "txtcredit", 0);
                    return;
                }
            }
            else
            {
                Master.ShowModal("please select either urban or rural","chkrural",0);
            }
           
            DataSet dscust=ClsBLGD.GetcondDataSet("*","tblCustomer","CA_name",custnm);
            if(dscust.Tables[0].Rows.Count>0)
            {
                Master.ShowModal("data already exists","txtcustname",0);
                return;
            }
            else
            {
              
                 if (!File.Exists(filename))
                 {
                     if(chkrural.Checked==true)
                     {
                      ClsBLGP.Customer("INSERT_CUSTOMER",custid,custnm,custtype,door,"0",hobli,taluk,distr,state,Mobileno,Email,"N","0",rfcod,rfname,credlimit,"0",pan,tin,adhar,gst,Loginname,Sysdatetime,sMacAddress,city);
                          lblsuccess.Visible = true;
         // lblsuccess.Text ="inserted successfully";
            lblsuccess.Text = "Record inserted successfully. Customer Code = " + custid.ToString() ;
          ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
          // autoincrement();
          Bind();
          txtcustname.Text=string.Empty;
          txtadd1.Text=string.Empty;
          txtadd2.Text=string.Empty;
          txtdoorno.Text=string.Empty;
          txthobli.Text=string.Empty;
          txttaluk.Text=string.Empty;
          txtdist.Text=string.Empty;
          txtcity.Text=string.Empty;
          txtstate.Text=string.Empty;
          txtcredit.Text=string.Empty;
           txtmobile.Text=string.Empty;
           txtemail.Text=string.Empty;
           txtcustcode.Text=string.Empty;
           txtADHAR.Text = string.Empty;
           txtPAN.Text = string.Empty;
           txtTIN.Text = string.Empty;
           txtGST.Text = string.Empty;
           //Table3.Visible=false;
          ddrefcode.Text="-Select-";
          refcd();
          refnm();
          ddrefcode.DataBind();
        //txtref_code.Text=string .Empty;
          ddrfnm.Text="-Select-";
          chkrural.Checked=false;
          chkurban.Checked=false;
                     }
                     else if(chkurban.Checked==true)
                     {
                         string credlimit10=txtcredit.Text;
                         if(credlimit10=="")
                         {
                           credlimit10="0";
                      ClsBLGP.Customer("INSERT_CUSTOMER",custid,custnm,custtype,add1,add2,hobli,taluk,distr,state,Mobileno,Email,"N","0",rfcod,rfname,credlimit10,"0",pan,tin,adhar,gst,Loginname,Sysdatetime,sMacAddress,city);
                              lblsuccess.Visible = true;
         // lblsuccess.Text ="inserted successfully";
            lblsuccess.Text = "Record inserted successfully. Customer Code = " + custid.ToString() ;
          ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
          // autoincrement();
          Bind();
          txtcustname.Text=string.Empty;
          txtadd1.Text=string.Empty;
          txtadd2.Text=string.Empty;
          txtdoorno.Text=string.Empty;
          txthobli.Text=string.Empty;
          txttaluk.Text=string.Empty;
          txtdist.Text=string.Empty;
          txtcity.Text=string.Empty;
          txtstate.Text=string.Empty;
          txtcredit.Text=string.Empty;
           txtmobile.Text=string.Empty;
           txtemail.Text=string.Empty;
           txtcustcode.Text=string.Empty;
           txtGST.Text = string.Empty;
           txtPAN.Text = string.Empty;
           txtTIN.Text = string.Empty;
           txtADHAR.Text = string.Empty;
           //Table3.Visible=false;
          ddrefcode.Text="-Select-";
          refcd();
          refnm();
          ddrefcode.DataBind();
        //txtref_code.Text=string .Empty;
          ddrfnm.Text="-Select-";
          chkrural.Checked=false;
          chkurban.Checked=false;
                         }
                         else{
                              ClsBLGP.Customer("INSERT_CUSTOMER",custid,custnm,custtype,add1,add2,hobli,taluk,distr,state,Mobileno,Email,"N","0",rfcod,rfname,credlimit10,"0",pan,tin,adhar,gst,Loginname,Sysdatetime,sMacAddress,city);
                              lblsuccess.Visible = true;
         // lblsuccess.Text ="inserted successfully";
            lblsuccess.Text = "Record inserted successfully. Customer Code = " + custid.ToString() ;
          ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
          // autoincrement();
          Bind();
          txtcustname.Text=string.Empty;
          txtadd1.Text=string.Empty;
          txtadd2.Text=string.Empty;
          txtdoorno.Text=string.Empty;
          txthobli.Text=string.Empty;
          txttaluk.Text=string.Empty;
          txtdist.Text=string.Empty;
          txtcity.Text=string.Empty;
          txtstate.Text=string.Empty;
          txtcredit.Text=string.Empty;
           txtmobile.Text=string.Empty;
           txtemail.Text=string.Empty;
           txtcustcode.Text=string.Empty;
           txtGST.Text = string.Empty;
           txtPAN.Text = string.Empty;
           txtTIN.Text = string.Empty;
           txtADHAR.Text = string.Empty;
          // Table3.Visible=false;
          ddrefcode.Text="-Select-";
          refcd();
          refnm();
          ddrefcode.DataBind();
        //txtref_code.Text=string .Empty;
          ddrfnm.Text="-Select-";
          //chkrural.Checked=false;
          //chkurban.Checked=false;
          chkrural.Checked = true;

                         }
                     }
                 }
                 else
                 {
                  //   String strconn11 = Dbconn.conmenthod();
                     OleDbConnection con = new OleDbConnection(strconn11);
                     con.Open();
                     if(chkrural.Checked==true)
                    {
                        OleDbCommand cmd = new OleDbCommand("insert into tblCustomer(CA_code,CA_name,CA_type,Address1,Address2,Hobli,Taluk,District,State,Rf_code,Rf_name,Credit_limit,Credit_amount,PAN_No,TIN_No,ADHAR_No,GST_No,Login_name,Sysdatetime,Mac_id,City)values('" + custid + "','" + custnm + "','" + custtype + "','" + door + "','" + add2 + "','" + hobli + "','" + taluk + "','" + distr + "','" + state + "','" + rfcod + "','" + rfname + "','" + crlimit + "','0','" + pan + "','" + tin + "','" + adhar + "','" + gst + "','" + Session["username"].ToString() + "','" + Sysdatetime + "','" + sMacAddress + "','" + city + "')", con);
                      cmd.ExecuteNonQuery();
                    }
                     else if(chkurban.Checked==true)
                    {
                        OleDbCommand cmd = new OleDbCommand("insert into tblCustomer(CA_code,CA_name,CA_type,Address1,Address2,Hobli,Taluk,District,State,Rf_code,Rf_name,Credit_limit,Credit_amount,PAN_No,TIN_No,ADHAR_No,GST_No,Login_name,Sysdatetime,Mac_id,City)values('" + custid + "','" + custnm + "','" + custtype + "','" + add1 + "','" + add2 + "','" + hobli + "','" + taluk + "','" + distr + "','" + state + "','" + rfcod + "','" + rfname + "','" + crlimit + "','0','" + pan + "','" + tin + "','" + adhar + "','" + gst + "','" + Session["username"].ToString() + "','" + Sysdatetime + "','" + sMacAddress + "','" + city + "')", con);
                     cmd.ExecuteNonQuery();
                    }
                     con.Close();
                 } 
            }
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
         

    }
          else if (button_select == "Modify")
    {
     try
      {
           int crlimit1=0;
           string custname=txtcustname.Text;
           string custtype1=null;
           string doorr=txtdoorno.Text;
           string addre1=txtadd1.Text;
           string addre2=txtadd2.Text;
           string hobli1=txthobli.Text;
           string taluk1=txttaluk.Text;
           string distr1=txtdist.Text;
           string city1=txtcity.Text;
           string state1=txtstate.Text;
           string rfcod1=ddrefcode.Text;
           string Mobileno=txtmobile.Text;
           string Email=txtemail.Text;
           if(rfcod1=="-Select-")
           {
               rfcod1=string.Empty;
           }
          // int rfcode1=Convert.ToInt32(rfcod1);
           string rfname1=ddrfnm.Text;
             if(rfname1=="")
             {
                 rfname1=string.Empty;
             }
          string credlimit1=txtcredit.Text;
          if(credlimit1=="")
           {
            crlimit1=0;
           }
          int cramt1=0;
          string Loginname1="";
          //System.DateTime Dtnow = DateTime.Now;
          //string Sysdatetime1= Dtnow.ToString("dd/MM/yyyy");
          //txtdate.Text=Sysdatetime1;
         string Sysdatetime1=txtdate.Text;

          string cod = lblcode.Text;
          int c = Convert.ToInt32(cod);

           if(chkrural.Checked==true)
            {
                custtype1=chkrural.Checked ? "RURAL" : "URBAN";
            }
            else if(chkurban.Checked==true)
            {
                custtype1=chkrural.Checked ? "RURAL" : "URBAN";
            }
            if(txtadd1.Text=="")
            {
                addre1="0";
            }
            if(txtadd2.Text=="")
            {
                addre2="0";
            }
            if(txtdoorno.Text=="")
            {
                doorr="0";
            }
           if(txthobli.Text=="")
           {
             hobli1=string.Empty;
           }
           if(txtcity.Text=="")
           {
             city1=string.Empty;
           }
           if(txtdist.Text=="")
           {
             distr1=string.Empty;
           }
           if(txttaluk.Text=="")
           {
             taluk1="0";
           }
         if(chkrural.Checked==true)
         {
             if(txtdoorno.Text=="")
             {
                 Master.ShowModal("doorno mandatory","txtdoorno",0);
                 return;
             }
             if(txthobli.Text=="")
             {
                 Master.ShowModal("hobli mandatory","txthobli",0);
                 return;
             }
             if(txttaluk.Text=="")
             {
                 Master.ShowModal("Taluka mandatory","txttaluk",0);
                 return;
             }
             if(txtdist.Text=="")
             {
                 Master.ShowModal("District mandatory","txtdist",0);
                 return;
             }
             if(txtstate.Text=="")
             {
                 Master.ShowModal("State mandatory","txtstate",0);
                 return;
             }
              //if(rfcod1=="-Select-")
              //  {
              //      Master.ShowModal("Select Reference code", "txtdist", 0);
              //      return;
              //  }
              //  if(rfname1=="-Select-")
              //  {
              //       Master.ShowModal("please enter reference name details", "txtdist", 0);
              //       return;
              //  }
         }
         else if(chkurban.Checked==true)
         {
             if(txtadd1.Text=="")
                {
                    Master.ShowModal("Address1 mandatory", "txtadd1", 0);
                    return;
                }
                if(txtadd2.Text=="")
                {
                    Master.ShowModal("Address2 mandatory", "txtadd2", 0);
                    return;
                }
                if(txtcity.Text=="")
                {
                    Master.ShowModal("City mandatory", "txtcity", 0);
                    return;
                }
                if(txtstate.Text=="")
                {
                    Master.ShowModal("State mandatory", "txtstate", 0);
                    return;
                }
                if(txtcredit.Text=="")
                {
                    Master.ShowModal("please enter credit", "txtcredit", 0);
                    return;
                }
               //if(rfcod1=="-Select-")
               // {
               //     Master.ShowModal("Select Reference code", "txtdist", 0);
               //     return;
               // }
               // if(rfname1=="-Select-")
               // {
               //      Master.ShowModal("please enter reference name details", "txtdist", 0);
               //      return;
               // }
           }
          else
            {
                Master.ShowModal("please select either urban or rural","chkrural",0);
            }
                if (!File.Exists(filename))
                {
                    if(chkrural.Checked==true)
                    {
                        string crlimit2=txtcredit.Text;

                    ClsBLGP.updateCustomer("UPDATE_CUSTOMER",custname,custtype1,doorr,addre2,hobli1,taluk1,distr1,state1,rfcod1,rfname1,crlimit2,"0",Mobileno,Email,city1,c);
                    }
                    else if(chkurban.Checked==true)
                    {
                         string crlimit2=txtcredit.Text;
                     ClsBLGP.updateCustomer("UPDATE_CUSTOMER",custname,custtype1,addre1,addre2,hobli1,taluk1,distr1,state1,rfcod1,rfname1,crlimit2,"0",Mobileno,Email,city1,c);
                    }
                 }
                else
                {
                    OleDbConnection conn10 = new OleDbConnection(Dbconn.conmenthod());
                    conn10.Open();
                    if(chkrural.Checked==true)
                    {
                    OleDbCommand cmd1 = new OleDbCommand("update tblCustomer set CA_name='" + custname + "',CA_type='" + custtype1 + "', Address1='" + doorr + "',Address2='" + addre2 + "',Hobli='" + hobli1 + "',Taluk='" + taluk1 + "',District='" + distr1 +"',State='" + state1 + "',Rf_code='" + rfcod1 + "',Rf_name='" + rfname1 +"',Credit_limit='" + crlimit1 + "',Credit_amount='" + cramt1 + "',Login_name='" + Loginname1 +"',Sysdatetime='" + Sysdatetime1 + "',Mac_id='" + sMacAddress + "',City='" + city1 + "' where Srno=" + c + "", conn10);
                    cmd1.ExecuteNonQuery();
                    }
                    else if(chkurban.Checked==true)
                    {
                    OleDbCommand cmd1 = new OleDbCommand("update tblCustomer set CA_name='" + custname + "',CA_type='" + custtype1 + "', Address1='" + addre1 + "',Address2='" + addre2 + "',Hobli='" + hobli1 + "',Taluk='" + taluk1 + "',District='" + distr1 +"',State='" + state1 + "',Rf_code='" + rfcod1 + "',Rf_name='" + rfname1 +"',Credit_limit='" + crlimit1 + "',Credit_amount='" + cramt1 + "',Login_name='" + Loginname1 +"',Sysdatetime='" + Sysdatetime1 + "',Mac_id='" + sMacAddress + "',City='" + city1 + "' where Srno=" + c + "", conn10);
                    cmd1.ExecuteNonQuery();
                    }
                    conn10.Close();
                }
            }

          catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }

              lblsuccess.Visible = true;
              lblsuccess.Text ="modified successfully";
              ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
              Bind();
              button_select = string.Empty;
              txtcustname.Text=string.Empty;
              txtadd1.Text=string.Empty;
              txtadd2.Text=string.Empty;
              txtdoorno.Text=string.Empty;
              txthobli.Text=string.Empty;
              txttaluk.Text=string.Empty;
              txtdist.Text=string.Empty;
              txtcity.Text=string.Empty;
              txtstate.Text=string.Empty;
              txtcredit.Text=string.Empty;
              txtADHAR.Text = string.Empty;
              txtPAN.Text = string.Empty;
              txtTIN.Text = string.Empty;
              txtGST.Text = string.Empty;
              ddrefcode.Text="-Select-";
              ddrfnm.Text="-Select-";
              chkrural.Checked=false;
              chkurban.Checked=false;
              txtcustname.Enabled=true;
              ddrefcode.Enabled=true;
              ddrfnm.Enabled=true;
            
          }
          if (chkrural.Checked == true)
          {
              chkrural.Checked = true;
              chkurban.Checked = false;
          }
          else if(chkurban.Checked ==true) 
        {
            chkrural.Checked = false;
            chkurban.Checked = true;
        }

        //autoincrement();
      //Table3.Visible=true;
    }
     protected void Gridcust_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Gridcust.PageIndex = e.NewPageIndex;
        Bind();

    }

     protected void ddrefcode_SelectedIndexChanged(object sender, EventArgs e)
      {
         string refe=ddrefcode.SelectedItem.Text;
         try{
         if(ddrefcode.SelectedItem.Text=="-Select-")
         {
             Master.ShowModal("Select Refered code","ddrefcode",0);
             return;
         }
         else
         {
           DataSet dsref=ClsBLGD.GetcondDataSet("CA_name", "tblCustomer","CA_code", refe);
             if(dsref.Tables[0].Rows.Count>0)
             {
                 ddrfnm.Text=dsref.Tables[0].Rows[0]["CA_name"].ToString();
             }
             else
             {
                 Master.ShowModal("Code does not exist","txtcustname",1);
                 return;
             }
         }
         }
          catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Enabled = true;
                lblerror.Text = asd;
            }
         ddrfnm.Enabled=true;
         ddrfnm.Focus();
     }

      protected void btnexit_Click(object sender, EventArgs e)
      {
          Response.Redirect("Home.aspx");
      }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> Buyername(string prefixText)
    {
       // string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
        //string oConn = ConfigurationManager.AppSettings["ConnectionString"];
        SqlConnection conn = new SqlConnection(strconn11);
        conn.Open();
        SqlCommand cmd = new SqlCommand("select CA_name from tblCustomer where CA_name like @1+'%'", conn);
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
            //string strconn1 = Dbconn.conmenthod();
            OleDbConnection conn=new OleDbConnection(strconn11);
            conn.Open();
            OleDbCommand cmd=new OleDbCommand("select CA_name from tblCustomer where CA_name like @1+'%'", conn);
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

      [WebMethod]
    public static string[] Getstate(string prefix)
        {

       // string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection(strconn11))
            {
                //conn.ConnectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

                //conn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "select State from tblCustomer where State like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["State"]));
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

                    cmd.CommandText = "select State from tblCustomer where State like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    cmd.Connection = conn;
                    conn.Open();
                    using (OleDbDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(string.Format("{0}", sdr["State"]));
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
       string cuname=txtcustname.Text;
       try{
       if(chkrural.Checked==true)
        {
          DataSet dscus=ClsBLGD.GetcondDataSet("*","tblCustomer","CA_name",cuname);
          lblmod.Text = "Supplier with below details already exists";
          int code = Convert.ToInt32(dscus.Tables[0].Rows[0]["Srno"].ToString());
          lblcode.Text = Convert.ToString(code);
          Table2.Visible = true;
          ClsBLGD.GetcondDataSet("*", "tblCustomer", "CA_name", cuname);
          txtdoorno.Text=dscus.Tables[0].Rows[0]["Address1"].ToString();
          txthobli.Text=dscus.Tables[0].Rows[0]["Hobli"].ToString();
          txttaluk.Text=dscus.Tables[0].Rows[0]["Taluk"].ToString();
          txtdist.Text=dscus.Tables[0].Rows[0]["District"].ToString();
          txtstate.Text=dscus.Tables[0].Rows[0]["State"].ToString();
          txtcredit.Text=dscus.Tables[0].Rows[0]["Credit_limit"].ToString();
           string ddrf=dscus.Tables[0].Rows[0]["Rf_code"].ToString();
           if(ddrf=="0")
           {
               ddrefcode.Text="-Select-";
           }
           else
           {
             ddrefcode.Text=dscus.Tables[0].Rows[0]["Rf_code"].ToString();
           }
           string ddfnm=dscus.Tables[0].Rows[0]["Rf_name"].ToString();
           if(ddfnm=="NULL")
           {
               ddrfnm.Text="-Select-";
           }
           else
           {
             ddrfnm.Text=dscus.Tables[0].Rows[0]["Rf_name"].ToString();
           }
          mod();
          Table2.Visible = false;
          txtcustname.Enabled=true;
          txtdoorno.Enabled=true;
          txthobli.Enabled=true;
          txtdist.Enabled=true;
          txtstate.Enabled=true;
          txttaluk.Enabled=true;
          txtcredit.Enabled=true;
          ddrfnm.Enabled=false;
          ddrefcode.Enabled=false;
        }
        else if(chkurban.Checked==true)
        {
          DataSet dscus=ClsBLGD.GetcondDataSet("*","tblCustomer","CA_name",cuname);
          lblmod.Text = "Supplier with below details already exists";
          int code = Convert.ToInt32(dscus.Tables[0].Rows[0]["Srno"].ToString());
          lblcode.Text = Convert.ToString(code);
          Table2.Visible = true;
          ClsBLGD.GetcondDataSet("*", "tblCustomer", "CA_name", cuname);
          txtadd1.Text=dscus.Tables[0].Rows[0]["Address1"].ToString();
          txtadd2.Text=dscus.Tables[0].Rows[0]["Address2"].ToString();
          txtcity.Text=dscus.Tables[0].Rows[0]["City"].ToString();
          txtstate.Text=dscus.Tables[0].Rows[0]["State"].ToString();
          txtcredit.Text=dscus.Tables[0].Rows[0]["Credit_limit"].ToString();
          string ddrf=dscus.Tables[0].Rows[0]["Rf_code"].ToString();
           if(ddrf=="0")
           {
               ddrefcode.Text="-Select-";
           }
           else
           {
             ddrefcode.Text=dscus.Tables[0].Rows[0]["Rf_code"].ToString();
           }
           string ddfnm=dscus.Tables[0].Rows[0]["Rf_name"].ToString();
           if(ddfnm=="NULL")
           {
               ddrfnm.Text="-Select-";
           }
           else
           {
             ddrfnm.Text=dscus.Tables[0].Rows[0]["Rf_name"].ToString();
           }
          mod();
          Table2.Visible = false;
          txtcustname.Enabled=true;
          txtadd1.Enabled=true;
          txtadd2.Enabled=true;
          ddrfnm.Enabled=false;
          ddrefcode.Enabled=false;
          txtcredit.Enabled=true;
          txtcity.Enabled=true;
          txtstate.Enabled=true;
        }
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
            txtcustname.Enabled = true;
        }
        return button_select;
    }
      protected void txtcustname_TextChanged(object sender, EventArgs e)
         {
           string cuname=txtcustname.Text;
           try
           {
              if(chkrural.Checked==true)
               {
               DataSet dscus=ClsBLGD.GetcondDataSet("*","tblCustomer","CA_name",cuname);
               if (dscus.Tables[0].Rows.Count > 0)
                {
                 lblmod.Text = "customer with below name already exists.Click Modify to edit details";
                 string catp=dscus.Tables[0].Rows[0]["CA_type"].ToString();
                 if(catp=="URBAN")
                 {
                   Master.ShowModal("Please select Urban to view these record","txtcustname",1);
                     txtcustname.Text=string.Empty;
                   return;
                  }
                 else
                 {
                 int code = Convert.ToInt32(dscus.Tables[0].Rows[0]["Srno"].ToString());
                 txtdoorno.Text=dscus.Tables[0].Rows[0]["Address1"].ToString();
                 txthobli.Text=dscus.Tables[0].Rows[0]["Hobli"].ToString();
                 txtdist.Text=dscus.Tables[0].Rows[0]["District"].ToString();
                 txttaluk.Text=dscus.Tables[0].Rows[0]["Taluk"].ToString();
                 txtstate.Text=dscus.Tables[0].Rows[0]["State"].ToString();
                 txtcredit.Text=dscus.Tables[0].Rows[0]["Credit_limit"].ToString();
                 txtmobile.Text=dscus.Tables[0].Rows[0]["Mobileno"].ToString();
                  txtemail.Text=dscus.Tables[0].Rows[0]["Email"].ToString();
                  string ddrf=dscus.Tables[0].Rows[0]["Rf_code"].ToString();

                  if(String.IsNullOrEmpty(ddrf))
                  {
                     ddrefcode.Text="-Select-";
                  }
                  else
                  {
                      ddrefcode.Text=dscus.Tables[0].Rows[0]["Rf_code"].ToString();
                  }
                 string ddrnm=dscus.Tables[0].Rows[0]["Rf_name"].ToString();
                  if(String.IsNullOrEmpty(ddrf))
                  {
                      ddrfnm.Text="-Select-";
                  }
                  else
                  {
                      ddrfnm.Text=dscus.Tables[0].Rows[0]["Rf_name"].ToString();
                  }
               //  
                 lblcode.Text = Convert.ToString(code);
                 Table2.Visible = true;
                 txtcustname.Enabled=false;
                 txtdoorno.Enabled=false;
                 txtcredit.Enabled=false;
                 txttaluk.Enabled=false;
                 txtdist.Enabled=false;
                 txthobli.Enabled=false;
                 txtstate.Enabled=false;
                 txtmobile.Enabled=false;
                 txtemail.Enabled=false;
                 ddrfnm.Enabled=false;
                 ddrefcode.Enabled=false;
                 btn.Enabled=true;
                  btn.Focus();
                }
               }
               else
               {
                   txtdoorno.Enabled=true;
                   txtdoorno.Focus();
               }
             }
              else if(chkurban.Checked==true)
              {
                DataSet dscus=ClsBLGD.GetcondDataSet("*","tblCustomer","CA_name",cuname);
                if (dscus.Tables[0].Rows.Count > 0)
                {
                 lblmod.Text = "customer with below name already exists.Click Modify to edit details";
                 string catp=dscus.Tables[0].Rows[0]["CA_type"].ToString();
                 if(catp=="RURAL")
                 {
                   Master.ShowModal("Please select Rural to view these record","txtcustname",1);
                   txtcustname.Text=string.Empty;
                   return;
                 }
                 else
                 {
                 int code = Convert.ToInt32(dscus.Tables[0].Rows[0]["Srno"].ToString());
                 txtadd1.Text=dscus.Tables[0].Rows[0]["Address1"].ToString();
                 txtadd2.Text=dscus.Tables[0].Rows[0]["Address2"].ToString();
                // txtdist.Text=dscus.Tables[0].Rows[0]["District"].ToString();
                 txtcity.Text=dscus.Tables[0].Rows[0]["City"].ToString();
                 txtstate.Text=dscus.Tables[0].Rows[0]["State"].ToString();
                 txtcredit.Text=dscus.Tables[0].Rows[0]["Credit_limit"].ToString();
                string ddrf=dscus.Tables[0].Rows[0]["Rf_code"].ToString();

                  if(String.IsNullOrEmpty(ddrf))
                  {
                     ddrefcode.Text="-Select-";
                  }
                  else
                  {
                      ddrefcode.Text=dscus.Tables[0].Rows[0]["Rf_code"].ToString();
                  }
                 string ddrnm=dscus.Tables[0].Rows[0]["Rf_name"].ToString();
                  if(String.IsNullOrEmpty(ddrnm))
                  {
                      ddrfnm.Text="-Select-";
                  }
                  else
                  {
                      ddrfnm.Text=dscus.Tables[0].Rows[0]["Rf_name"].ToString();
                  }

                  lblcode.Text = Convert.ToString(code);
                  Table2.Visible = true;

                  txtcustname.Enabled=false;
                  txtadd1.Enabled=false;
                  txtadd2.Enabled=false;
                  txtcredit.Enabled=false;
                  txtdist.Enabled=false;
                  txtcity.Enabled=false;
                  txtstate.Enabled=false;
                  txtcredit.Enabled=false;
                  ddrfnm.Enabled=false;
                  ddrefcode.Enabled=false;
                  btn.Enabled=true;
                  btn.Focus();
                }
                }
                else
                {
                    txtadd1.Enabled=true;
                    txtadd1.Focus();
                }
               }
              else
              {
                  Master.ShowModal("please select urban or Rural","chkrural",0);
                  txtcustname.Text=string.Empty;
                  return;
              }
           }
          catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }

      }
      protected void btn_Click(object sender, EventArgs e)
      {
          no();
      }
     public void no()
    {
        Table2.Visible = false;
         if(chkrural.Checked==true)
         {
          txtcustname.Enabled=true;
          txtdoorno.Enabled=true;
          txthobli.Enabled=true;
          txttaluk.Enabled=true;
          txtdist.Enabled=true;
          txtstate.Enabled=true;
          txtcredit.Enabled=true;
          ddrefcode.Enabled=true;
          ddrfnm.Enabled=true;

          txtcustname.Text=string.Empty;
          txtdoorno.Text=string.Empty;
          txthobli.Text=string.Empty;
          txttaluk.Text=string.Empty;
          txtdist.Text=string.Empty;
          txtstate.Text=string.Empty;
          txtcredit.Text=string.Empty;
          ddrefcode.Text="-Select-";
          ddrfnm.Text="-Select-";
          button_select = string.Empty;
         }
         else if(chkurban.Checked==true)
         {
             txtcustname.Enabled=true;
             txtadd1.Enabled=true;
             txtadd2.Enabled=true;
             txtcity.Enabled=true;
             txtstate.Enabled=true;
             txtcredit.Enabled=true;
             ddrefcode.Enabled=true;
              ddrfnm.Enabled=true;

             txtcustname.Text=string.Empty;
             txtadd1.Text=string.Empty;
             txtadd2.Text=string.Empty;
             txtcity.Text=string.Empty;
             txtstate.Text=string.Empty;
             txtcredit.Text=string.Empty;
             ddrefcode.Text="-Select-";
             ddrfnm.Text="-Select-";
               button_select = string.Empty;
         }
     }
     //protected void txtref_name_TextChanged(object sender, EventArgs e)
     //{
     //    DataSet dsrfnm=ClsBLGD.GetcondDataSet("*","tblCustomer","CA_name",
     //}
     protected void ddrfnm_SelectedIndexChanged(object sender, EventArgs e)
     {
         string rfnm=ddrfnm.SelectedItem.Text.TrimStart('0');
         rfnm=rfnm.TrimStart();
         rfnm=Regex.Replace(rfnm,"","");
         try{
         if(ddrfnm.SelectedItem.Text=="-Select-")
         {
             Master.ShowModal("Select Refered name","ddrfnm",0);
             return;
         }
         else
         {
             DataSet dsrnm=ClsBLGD.GetcondDataSet("*","tblCustomer","CA_name",rfnm);
             if(dsrnm.Tables[0].Rows.Count>0)
             {
                 ddrefcode.Text=dsrnm.Tables[0].Rows[0]["CA_code"].ToString();
             }
             else
             {
             }
         }
         }
          catch (Exception ex)
            {
                string asd = ex.Message;
                lblerror.Enabled = true;
                lblerror.Text = asd;
            }
         btnsave.Enabled=true;
         btnsave.Focus();
     }
     protected void txtdate_TextChanged(object sender, EventArgs e)
     {
         try
         {

             DateTime startdate = Convert.ToDateTime(txtdate.Text);
         }
         catch (Exception ex)
         {
             string asd = ex.Message;
             Master.ShowModal("Invalid date format...", "txtdate", 1);
             return;
         }
         txtcustname.Enabled=true;
         txtcustname.Focus();
     }

     protected void btnok_Click(object sender, EventArgs e)
     {

        

     }

     protected void  txtmobilenor_TextChanged(object sender, EventArgs e)
         {

            string phonenum =txtmobile.Text;
              if (phonenum != "")
            {
                if (phonenum.Length == 10)
                {
                    //txtemail.Focus();
                    txtcredit.Focus();
                }
                else
                {
                    Master.ShowModal("Phone number cannot be Lesser/Greater than 10 characters !!!!", "txtLandLineNo", 1);
                   txtmobile.Focus(); 
                    return;
                }
            }

              txtemail.Focus();
         }

      protected void  txtemail_TextChanged(object sender, EventArgs e)
         {
          string emailid=txtemail.Text;

            Regex mailIDPattern = new Regex(@"[\w-]+@([\w-]+\.)+[\w-]+");

            if (!string.IsNullOrEmpty(emailid) && !mailIDPattern.IsMatch(emailid))
            {
                Master.ShowModal("Enter Email Address incorrect", "txtemail", 5);
                txtemail.Focus();
                return;
            }
            else{

                txtPAN.Focus();
            }

        }


      protected void txtGST_TextChanged(object sender, EventArgs e)
      {
          ddrefcode.Enabled = true;
          ddrefcode.Focus();
      }
      protected void txtPAN_TextChanged(object sender, EventArgs e)
      {
          txtTIN.Focus(); 
      }
      protected void txtTIN_TextChanged(object sender, EventArgs e)
      {
          txtADHAR.Focus();
      }
      protected void txtADHAR_TextChanged(object sender, EventArgs e)
      {
          txtGST.Focus();
      }
}