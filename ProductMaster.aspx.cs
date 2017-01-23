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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections.Generic; 


public partial class ProductMaster : System.Web.UI.Page
{

    ClsBLLGeneraldetails clsgd = new ClsBLLGeneraldetails();
    ClsBALProductMaster ClsBLGP = new ClsBALProductMaster();
    //ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    Dbconn dbcon = new Dbconn();
    protected  static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
    protected static string button_select;
    string sMacAddress = "";
    ArrayList arryno = new ArrayList();

    ArrayList arryname = new ArrayList();


    ArrayList arryno10 = new ArrayList();

    ArrayList arryname10 = new ArrayList();

    ArrayList arryno11 = new ArrayList();

    ArrayList arryname11 = new ArrayList();

    ArrayList arryno12 = new ArrayList();

    ArrayList arryname12 = new ArrayList();

    ArrayList arryno13 = new ArrayList();

    ArrayList arryname13 = new ArrayList();

    ArrayList arryno14 = new ArrayList();

    ArrayList arryname14 = new ArrayList();

    ArrayList arryno15 = new ArrayList();

    ArrayList arryname15 = new ArrayList();

    ArrayList arryno16 = new ArrayList();

    ArrayList arryname16 = new ArrayList();

    ArrayList arryno17 = new ArrayList();

    ArrayList arryname17 = new ArrayList();

    ArrayList arryno18 = new ArrayList();

    ArrayList arryname18 = new ArrayList();

    ArrayList arryno19 = new ArrayList();

    ArrayList arryname19 = new ArrayList();

    ArrayList arryno20 = new ArrayList();

    ArrayList arryname20 = new ArrayList();


    ArrayList arryno21 = new ArrayList();

    ArrayList arryname21 = new ArrayList();

    ArrayList arryno22 = new ArrayList();

    ArrayList arryname22 = new ArrayList();


    DataTable dt = new DataTable();
    DataRow dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        ddgrpcode.BorderColor = System.Drawing.Color.Orange;
        ddgrpcode.BorderWidth = 1;
        ddgrpcode.BorderStyle = BorderStyle.Solid;

        ddshelf.BorderColor = System.Drawing.Color.Orange;
        ddshelf.BorderWidth = 1;
        ddshelf.BorderStyle = BorderStyle.Solid;


        ddmed.BorderColor = System.Drawing.Color.Orange;
        ddmed.BorderWidth = 1;
        ddmed.BorderStyle = BorderStyle.Solid;

        ddunit.BorderColor = System.Drawing.Color.Orange;
        ddunit.BorderWidth = 1;
        ddunit.BorderStyle = BorderStyle.Solid;

        ddform.BorderColor = System.Drawing.Color.Orange;
        ddform.BorderWidth = 1;
        ddform.BorderStyle = BorderStyle.Solid;

        ddmanu.BorderColor = System.Drawing.Color.Orange;
        ddmanu.BorderWidth = 1;
        ddmanu.BorderStyle = BorderStyle.Solid;

        ddpack.BorderColor = System.Drawing.Color.Orange;
        ddpack.BorderWidth = 1;
        ddpack.BorderStyle = BorderStyle.Solid;
        

        ddlshelf.BorderColor = System.Drawing.Color.Orange;
        ddlshelf.BorderWidth = 1;
        ddlshelf.BorderStyle = BorderStyle.Solid;

        ddlrow.BorderColor = System.Drawing.Color.Orange;
        ddlrow.BorderWidth = 1;
        ddlrow.BorderStyle = BorderStyle.Solid;

        ddsupplier.BorderColor = System.Drawing.Color.Orange;
        ddsupplier.BorderWidth = 1;
        ddsupplier.BorderStyle = BorderStyle.Solid;

        ddrow.BorderColor = System.Drawing.Color.Orange;
        ddrow.BorderWidth = 1;
        ddrow.BorderStyle = BorderStyle.Solid;

        ddlunit.BorderColor = System.Drawing.Color.Orange;
        ddlunit.BorderWidth = 1;
        ddlunit.BorderStyle = BorderStyle.Solid;

        ddGecode.BorderColor = System.Drawing.Color.Orange;
        ddGecode.BorderWidth = 1;
        ddGecode.BorderStyle = BorderStyle.Solid;
        lblsuccess.Visible = false;
        lblerror.Visible = false;
        //txtProd.TabIndex = 1;
        //txtPname.TabIndex = 2;
        //ddgrpcode.Enabled = true;
        //ddshelf.TabIndex = 4;
        //ddrow.TabIndex = 5;
        //txtreorder.TabIndex = 6;
       // txtrateofint.TabIndex = 7;
        //ddlunit.TabIndex = 8;
        
        
        if (!Page.IsPostBack)
        {

            //createtableforimage();
            tblPhoto.Visible = false;
            groupcode();
            genericcode();
            chemicalcode();
            medicinetype();
            unit();
            form();
            manufacture();
            packsize();
            shelf();
            //shelfrow();
            suppliername();
            pshelfname();
            unitdetails();

            lblgrcode.Visible = false;
            lblgncode.Visible = false;
            lblpfflag.Visible = false;
            lblcemcode.Visible = false;
            lblmedcode.Visible = false;
            lblunitcode.Visible = false;
            lblformcode.Visible = false;
            lblmanucode.Visible = false;
            lblPackcode.Visible = false;
            lblShelfcode.Visible = false;
            lblShelfcount.Visible = false;
            lblsuplier.Visible = false;
            lblShelf1code.Visible = false;
            lblShelf1count.Visible = false;
            lblunitcode1.Visible = false;
            Table2.Visible = false;
            lblcode.Visible = false;
            //lkbUniqueAction.Visible = false;
            ddgrpcode.Enabled = false;
           // ddrow.SelectedItem.Text = "";
            lblmod.Visible = false;
            txtProd.Focus();
            Panel2.Visible = false;
            Panel3.Visible = false;

        }
        /*var ctrlName = Request.Params[Page.postEventSourceID];
        var args = Request.Params[Page.postEventArgumentID];
        if (IsPostBack)
        HandleCustomPostbackEvent(ctrlName, args);*/
      
        if (Session["username"] != null)
        {

        }
        else
        {
            Response.Redirect("Index.aspx");
        }
        //txtProd.Focus();
        GetMACAddress();
        ClientScript.RegisterStartupScript(this.GetType(), "SetInitialFocus", "<script>document.getElementById('" + txtProd.ClientID + "').focus();</script>");
        btnExit.Attributes.Add("onkeydown", "if(event.which || event.keyCode)" + "{if ((event.which == 9) || (event.keyCode == 9)) " + "{document.getElementById('" + txtProd.ClientID + "').focus();return false;}} else {return true}; ");
    }
    /*private void HandleCustomPostbackEvent(string ctrlName, string args)
    {
        if (ctrlName == Image1.UniqueID && args == "OnMouseover")
        {
            Image1.Width = 300;
            Image1.Height = 300;
        }
        else
        {
            Image1.Width = 100;
            Image1.Height = 100;
        }
    }*/
    /*protected void Page_Init(object sender, EventArgs e)
    {
        var OnMouseoverScript = Page.ClientScript.GetPostBackEventReference(Image1, "OnMouseover");
        var OnMouseMoveScript = Page.ClientScript.GetPostBackEventReference(Image1, "OnMouseMove");
        Image1.Attributes.Add("OnMouseover", OnMouseoverScript);
        Image1.Attributes.Add("OnMouseMove", OnMouseMoveScript);
    }*/
  
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
   
   

   

    protected void ddgrpcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        //groupcode();
        
        if (ddgrpcode.SelectedItem.Text == "Add New")
        {
            this.ModalPopupExtender1.Enabled = true;
            ModalPopupExtender1.Show();
           
            // Response.Redirect("~/UserControls/UC_Login.ascx");
            //this.form1.Controls.Add(this.LoadControl("~/UserControls/UC_Login.ascx"));
            //this.Parent.Controls.Add(this.LoadControl("~/UserControls/UC_Login.ascx"));
            //UserControls_UC_Login ucSimpleControl = LoadControl("~/UserControls/UC_Login.ascx") as UserControls_UC_Login;
            //Control uc = (Control)Page.LoadControl("~/UserControls/UC_Login.ascx");
            //Panel1.Controls.Add(uc);
            //System.Threading.Thread.Sleep(5000);

            //Response.Redirect(Request.Url.ToString());



        }
        else
        {
            //this.ModalPopupExtender1.Enabled = false;
            //ModalPopupExtender1.Hide();
            //ddgrpcode.Items.Clear();
            //groupcode();

            if (ddgrpcode.SelectedItem.Text == "-Select-")
            {
                //ddgrpcode.Items.Clear();
               // groupcode();
                Master.ShowModal("Please select a groupname. !!!", "ddgrpcode", 1);
                return;
            }
            else
            {

                string p_flag = ddgrpcode.SelectedItem.Text;

                DataSet dsgroup1 = clsgd.GetcondDataSet("*", "tblGroup", "g_name", p_flag);

                string flag = dsgroup1.Tables[0].Rows[0]["p_flag"].ToString();
                if (flag == "Y")
                {

                    Panel2.Visible = true;
                    Panel3.Visible = false;
                    //genericcode();
                    //chemicalcode();
                    //medicinetype();
                    //unit();
                    //form();
                    //manufacture();
                    //packsize();
                    //shelf();
                    //shelfrow();
                    suppliername();
                    ddGecode.Enabled = true;
                    ddGecode.Focus();
                    ddGecode.BorderColor = System.Drawing.Color.Black;
                    ddGecode.BorderWidth = 1;
                    ddGecode.BorderStyle = BorderStyle.Dotted;
                }
                else
                {

                    Panel3.Visible = true;
                    Panel2.Visible = false;
                    //Panel3.EnableViewState = true;
                   // pshelfname();
                    //unitdetails();
                    //shelfrowcount();
                    ddshelf.Enabled = true;
                    ddshelf.Focus();
                    ddshelf.BorderColor = System.Drawing.Color.Black;
                    ddshelf.BorderWidth = 1;
                    ddshelf.BorderStyle = BorderStyle.Dotted;



                }


                DataSet dsgroup2 = clsgd.GetcondDataSet("*", "tblGroup", "g_name", ddgrpcode.SelectedItem.Text);
                int code = Convert.ToInt32(dsgroup2.Tables[0].Rows[0]["g_code"].ToString());
                lblgrcode.Text = Convert.ToString(code);

                DataSet dsgroup25 = clsgd.GetcondDataSet("*", "tblGroup", "g_name", p_flag);
                string pfflag = dsgroup25.Tables[0].Rows[0]["p_flag"].ToString();
                lblpfflag.Text = Convert.ToString(pfflag);

            }
        }
        tblPhoto.Visible = true;
    }
    public void groupcode()
    {
        DataSet dsgroup = clsgd.GetDataSet("distinct g_name", "tblGroup");
        for (int i = 0; i < dsgroup.Tables[0].Rows.Count; i++)
        {
            DataSet dsgroup1 = clsgd.GetcondDataSet("*", "tblGroup", "g_name", dsgroup.Tables[0].Rows[i]["g_name"].ToString());
            arryname.Add(dsgroup1.Tables[0].Rows[0]["g_name"].ToString());
            

        }

        arryname.Sort();
        arryno.Add("-Select-");
        arryno.Add("Add New");
        for (int i = 0; i < arryname.Count; i++)
        {
            arryno.Add(arryname[i].ToString());
        }
        ddgrpcode.DataSource = arryno;
        ddgrpcode.DataBind();
       

       
         ddGecode.Focus();

    }


      protected void chkgroup_CheckedChanged(object sender, EventArgs e)
    {
        if (chkgroup.Checked == true)//this is working
        {
           using (SqlConnection conn10 = new SqlConnection(strconn11))
                    {
                        conn10.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

                       

                        DataSet ds = new DataSet();
                        conn10.Open();

                        //string cmdstr = "Select Batchid from tblProductinward where productcode ="'+ productcode +'"";
                        string cmdstr = "select g_name from tblGroup where p_flag='Y'";

                        SqlCommand cmd10 = new SqlCommand(cmdstr, conn10);

                        SqlDataAdapter adp = new SqlDataAdapter(cmd10);

                        adp.Fill(ds);

                        ddgrpcode.DataSource = ds.Tables[0];

                        
                       ddgrpcode.DataTextField = "g_name";

                       ddgrpcode.DataBind();
                       ddgrpcode.BackColor = Color.Red; 

                       ddgrpcode.Items.Insert(0, new ListItem("-Select-", "0"));

                        conn10.Close();

                    }

                

            
            
        }
        else
        {
            using (SqlConnection conn10 = new SqlConnection(strconn11))
                    {
                        conn10.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

                       

                        DataSet ds = new DataSet();
                        conn10.Open();

                        //string cmdstr = "Select Batchid from tblProductinward where productcode ="'+ productcode +'"";
                        string cmdstr = "select g_name from tblGroup where p_flag='N'";

                        SqlCommand cmd10 = new SqlCommand(cmdstr, conn10);

                        SqlDataAdapter adp = new SqlDataAdapter(cmd10);

                        adp.Fill(ds);

                        ddgrpcode.DataSource = ds.Tables[0];

                        
                       ddgrpcode.DataTextField = "g_name";

                       ddgrpcode.DataBind();
                       ddgrpcode.BackColor = Color.Red; 

                       ddgrpcode.Items.Insert(0, new ListItem("--Select--", "0"));

                        conn10.Close();

                    }

            
            
        }
        //ddgrpcode.Focus();
          ddgrpcode.Enabled = true;
          ddgrpcode.Focus();
        ddgrpcode.BorderColor = System.Drawing.Color.Black;
        ddgrpcode.BorderWidth = 1;
        ddgrpcode.BorderStyle = BorderStyle.Dotted;
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
       
        Response.Redirect("Home.aspx");
    }


    public void genericcode()
    {
        //lkbUniqueAction.Visible=true;
        DataSet dsgroup10 = clsgd.GetDataSet("distinct GN_name", "tblGeneric");
        for (int i = 0; i < dsgroup10.Tables[0].Rows.Count; i++)
        {
            DataSet dsgroup11 = clsgd.GetcondDataSet("*", "tblGeneric", "GN_name", dsgroup10.Tables[0].Rows[i]["GN_name"].ToString());
            arryname10.Add(dsgroup11.Tables[0].Rows[0]["GN_name"].ToString());

        }
        arryname10.Sort();
        arryno10.Add("-Select-");
        arryno10.Add("Add New");
        for (int i = 0; i < arryname10.Count; i++)
        {
            arryno10.Add(arryname10[i].ToString());
        }
        ddGecode.DataSource = arryno10;
        ddGecode.DataBind();
        ddchem.Focus();

    }

    public void chemicalcode()
    {
        //lkbUniqueAction.Visible = true;
        DataSet dsgroup12 = clsgd.GetDataSet("distinct CC_name", "tblChemical");
        for (int i = 0; i < dsgroup12.Tables[0].Rows.Count; i++)
        {
            DataSet dsgroup13 = clsgd.GetcondDataSet("*", "tblChemical", "CC_name", dsgroup12.Tables[0].Rows[i]["CC_name"].ToString());
            arryname11.Add(dsgroup13.Tables[0].Rows[0]["CC_name"].ToString());

        }
        arryname11.Sort();
        arryno11.Add("-Select-");
        arryno11.Add("Add New");
        for (int i = 0; i < arryname11.Count; i++)
        {
            arryno11.Add(arryname11[i].ToString());
        }
        ddchem.DataSource = arryno11;
        ddchem.DataBind();


    }


    public void medicinetype()
    {
        DataSet dsgroup14 = clsgd.GetDataSet("distinct FA_name", "tblMedicine");
        for (int i = 0; i < dsgroup14.Tables[0].Rows.Count; i++)
        {
            DataSet dsgroup15 = clsgd.GetcondDataSet("*", "tblMedicine", "FA_name", dsgroup14.Tables[0].Rows[i]["FA_name"].ToString());
            arryname12.Add(dsgroup15.Tables[0].Rows[0]["FA_name"].ToString());

        }
        arryname12.Sort();
        arryno12.Add("-Select-");
        arryno12.Add("Add New");

        for (int i = 0; i < arryname12.Count; i++)
        {
            arryno12.Add(arryname12[i].ToString());
        }
        ddmed.DataSource = arryno12;
        ddmed.DataBind();


    }

    public void unit()
    {
        DataSet dsgroup15 = clsgd.GetDataSet("distinct unitname", "tblunitmaster");
        for (int i = 0; i < dsgroup15.Tables[0].Rows.Count; i++)
        {
            DataSet dsgroup16 = clsgd.GetcondDataSet("*", "tblunitmaster", "unitname", dsgroup15.Tables[0].Rows[i]["unitname"].ToString());
            arryname13.Add(dsgroup16.Tables[0].Rows[0]["unitname"].ToString());

        }
        arryname13.Sort();
        arryno13.Add("-Select-");
        arryno13.Add("Add New");
        for (int i = 0; i < arryname13.Count; i++)
        {
            arryno13.Add(arryname13[i].ToString());
        }
        ddunit.DataSource = arryno13;
        ddunit.DataBind();


    }

   

    public void form()
    {
        DataSet dsgroup16 = clsgd.GetDataSet("distinct formname", "tblformmaster");
        for (int i = 0; i < dsgroup16.Tables[0].Rows.Count; i++)
        {
            DataSet dsgroup17 = clsgd.GetcondDataSet("*", "tblformmaster", "formname", dsgroup16.Tables[0].Rows[i]["formname"].ToString());
            arryname14.Add(dsgroup17.Tables[0].Rows[0]["formname"].ToString());

        }
        arryname14.Sort();
        arryno14.Add("-Select-");
        arryno14.Add("Add New");

        for (int i = 0; i < arryname14.Count; i++)
        {
            arryno14.Add(arryname14[i].ToString());
        }
        ddform.DataSource = arryno14;
        ddform.DataBind();
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
        arryno15.Add("Add New");

        for (int i = 0; i < arryname15.Count; i++)
        {
            arryno15.Add(arryname15[i].ToString());
        }
        ddmanu.DataSource = arryno15;
        ddmanu.DataBind();


    }

    public void packsize()
    {
        DataSet dsgroup18 = clsgd.GetDataSet("distinct Pack_name", "tblPacksize");
        for (int i = 0; i < dsgroup18.Tables[0].Rows.Count; i++)
        {
            DataSet dsgroup19 = clsgd.GetcondDataSet("*", "tblPacksize", "Pack_name", dsgroup18.Tables[0].Rows[i]["Pack_name"].ToString());
            arryname16.Add(dsgroup19.Tables[0].Rows[0]["Pack_name"].ToString());

        }
        arryname16.Sort();
        arryno16.Add("-Select-");
        arryno16.Add("Add New");

        for (int i = 0; i < arryname16.Count; i++)
        {
            arryno16.Add(arryname16[i].ToString());
        }
        ddpack.DataSource = arryno16;
        ddpack.DataBind();


    }


    public void shelf()
    {
        DataSet dsgroup18 = clsgd.GetDataSet("distinct Se_name", "tblShelf");
        for (int i = 0; i < dsgroup18.Tables[0].Rows.Count; i++)
        {
            DataSet dsgroup19 = clsgd.GetcondDataSet("*", "tblShelf", "Se_name", dsgroup18.Tables[0].Rows[i]["Se_name"].ToString());
            arryname17.Add(dsgroup19.Tables[0].Rows[0]["Se_name"].ToString());

        }
        arryname17.Sort();
        arryno17.Add("-Select-");
        // arryno17.Add("Add New");

        for (int i = 0; i < arryname17.Count; i++)
        {
            arryno17.Add(arryname17[i].ToString());
        }
        ddlshelf.DataSource = arryno17;
        ddlshelf.DataBind();


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
        arryno19.Add("Add New");

        for (int i = 0; i < arryname19.Count; i++)
        {
            arryno19.Add(arryname19[i].ToString());
        }
        ddsupplier.DataSource = arryno19;
        ddsupplier.DataBind();
      }

    public void pshelfname()
    {

        DataSet dsgroup21 = clsgd.GetDataSet("distinct Se_name", "tblShelf");
        for (int i = 0; i < dsgroup21.Tables[0].Rows.Count; i++)
        {
           DataSet dsgroup22 = clsgd.GetcondDataSet("*", "tblShelf", "Se_name", dsgroup21.Tables[0].Rows[i]["Se_name"].ToString());
           arryname20.Add(dsgroup22.Tables[0].Rows[0]["Se_name"].ToString());

        }
        arryname20.Sort();
        arryno20.Add("-Select-");
        arryno20.Add("Add New");

        for (int i = 0; i < arryname20.Count; i++)
        {
            arryno20.Add(arryname20[i].ToString());
        }
        ddshelf.DataSource = arryno20;
        ddshelf.DataBind();
    }


    public void unitdetails()
    {

        DataSet dsgroup23 = clsgd.GetDataSet("distinct unitname,unitcode", "tblunitmaster");
        for (int i = 0; i < dsgroup23.Tables[0].Rows.Count; i++)
        {
            DataSet dsgroup24 = clsgd.GetcondDataSet("*", "tblunitmaster", "unitname", dsgroup23.Tables[0].Rows[i]["unitname"].ToString());
            arryname21.Add(dsgroup24.Tables[0].Rows[0]["unitname"].ToString());

        }
        arryname21.Sort();
        arryno21.Add("-Select-");
        arryno21.Add("Add New");

        for (int i = 0; i < arryname21.Count; i++)
        {
            arryno21.Add(arryname21[i].ToString());
        }
        ddlunit.DataSource = arryno21;
       ddlunit.DataBind();
    }



protected void ddGecode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddGecode.SelectedItem.Text == "Add New")
        {
            this.ModalPopupExtender2.Enabled = true;
            ModalPopupExtender2.Show();
        }
        else
        {
            //this.ModalPopupExtender2.Enabled = false;
            //ModalPopupExtender2.Hide();
            //genericcode();
            //ddGecode.Items.Clear();

            if (ddGecode.SelectedItem.Text == "-Select-")
            {
                Master.ShowModal("Please select a Generic Name . !!!", "ddgrpcode", 1);
                return;
            }

            DataSet dsgroup2 = clsgd.GetcondDataSet("*", "tblGeneric", "GN_name", ddGecode.SelectedItem.Text);
            int gncode = Convert.ToInt32(dsgroup2.Tables[0].Rows[0]["GN_code"].ToString());
            lblgncode.Text = Convert.ToString(gncode);
        }
        ddchem.Enabled = true;
        ddchem.Focus();

    }
protected void ddchem_SelectedIndexChanged(object sender, EventArgs e)
{
    
    if (ddchem.SelectedItem.Text == "Add New")
    {
        this.ModalPopupExtender3.Enabled = true;
        ModalPopupExtender3.Show();
    }
    else
    {
        //this.ModalPopupExtender3.Enabled = false;
        //ModalPopupExtender3.Hide();
        //chemicalcode();
        //ddchem.Items.Clear();

        if (ddchem.SelectedItem.Text == "-Select-")
        {
            Master.ShowModal("Please select a Chemical Composition. !!!", "ddgrpcode", 1);
            return;
        }
        DataSet dsgroup3 = clsgd.GetcondDataSet("*", "tblChemical", "CC_name", ddchem.SelectedItem.Text);
        int cemcode = Convert.ToInt32(dsgroup3.Tables[0].Rows[0]["CC_code"].ToString());
        lblcemcode.Text = Convert.ToString(cemcode);
    }
    ddmed.Enabled = true;
    ddmed.Focus();
    ddmed.BorderColor = System.Drawing.Color.Black;
    ddmed.BorderWidth = 1;
    ddmed.BorderStyle = BorderStyle.Dotted;

}
protected void ddmed_SelectedIndexChanged(object sender, EventArgs e)
{
   
    if (ddmed.SelectedItem.Text == "Add New")
    {
        this.ModalPopupExtender4.Enabled = true;
        ModalPopupExtender4.Show();
    }
    else
    {
        //this.ModalPopupExtender4.Enabled = false;
        //ModalPopupExtender4.Hide();
        //medicinetype();
        //ddmed.Items.Clear();

        if (ddmed.SelectedItem.Text == "-Select-")
        {
            Master.ShowModal("Please select a Medical Type. !!!", "ddgrpcode", 1);
            return;
        }
        DataSet dsgroup4 = clsgd.GetcondDataSet("*", "tblMedicine", "FA_name", ddmed.SelectedItem.Text);
        int medcode = Convert.ToInt32(dsgroup4.Tables[0].Rows[0]["FA_code"].ToString());
        lblmedcode.Text = Convert.ToString(medcode);

    }
    ddunit.Enabled = true;
    ddunit.Focus();
    ddunit.BorderColor = System.Drawing.Color.Black;
    ddunit.BorderWidth = 1;
    ddunit.BorderStyle = BorderStyle.Dotted;
}


protected void ddunit_SelectedIndexChanged(object sender, EventArgs e)
{
    ddform.Enabled = true;
    if (ddunit.SelectedItem.Text == "Add New")
    {
        this.ModalPopupExtender5.Enabled = true;
        ModalPopupExtender5.Show();
    }
    else
    {
        
        //this.ModalPopupExtender5.Enabled = false;
        //ModalPopupExtender5.Hide();
        //unit();
        //ddunit.Items.Clear();
        if (ddunit.SelectedItem.Text == "-Select-")
        {
            Master.ShowModal("Please select a Form Type. !!!", "ddgrpcode", 1);
            return;
        }
        DataSet dsgroup5 = clsgd.GetcondDataSet("*", "tblunitmaster", "unitname", ddunit.SelectedItem.Text);
        int Unitcode = Convert.ToInt32(dsgroup5.Tables[0].Rows[0]["unitcode"].ToString());
        lblunitcode.Text = Convert.ToString(Unitcode);
    }
    ddform.Enabled = true;
    ddform.Focus();
    ddform.BorderColor = System.Drawing.Color.Black;
    ddform.BorderWidth = 1;
    ddform.BorderStyle = BorderStyle.Dotted;
}
protected void ddform_SelectedIndexChanged(object sender, EventArgs e)
{
    
    if (ddform.SelectedItem.Text == "Add New")
    {
        this.ModalPopupExtender6.Enabled = true;
        ModalPopupExtender6.Show();
    }
    else
    {
        //this.ModalPopupExtender6.Enabled = false;
        //ModalPopupExtender6.Hide();
        //form();
        //ddform.Items.Clear();
        if (ddform.SelectedItem.Text == "-Select-")
        {
            Master.ShowModal("Please select a Form Type. !!!", "ddgrpcode", 1);
            return;
        }
        DataSet dsgroup6 = clsgd.GetcondDataSet("*", "tblformmaster", "formname", ddform.SelectedItem.Text);
        int formcode = Convert.ToInt32(dsgroup6.Tables[0].Rows[0]["formcode"].ToString());
        lblformcode.Text = Convert.ToString(formcode);
    }
    ddmanu.Enabled = true;
    ddmanu.Focus();
    ddmanu.BorderColor = System.Drawing.Color.Black;
    ddmanu.BorderWidth = 1;
    ddmanu.BorderStyle = BorderStyle.Dotted;

}
protected void ddmanu_SelectedIndexChanged(object sender, EventArgs e)
{
    ddpack.Enabled = true;
    if (ddmanu.SelectedItem.Text == "Add New")
    {
        this.ModalPopupExtender7.Enabled = true;
        ModalPopupExtender7.Show();
    }
    else
    {
        //this.ModalPopupExtender7.Enabled = false;
        //ModalPopupExtender7.Hide();
        //manufacture();
        //ddmanu.Items.Clear();
        if (ddmanu.SelectedItem.Text == "-Select-")
        {
            Master.ShowModal("Please select a Manufactureer Name. !!!", "ddgrpcode", 1);
            return;
        }
        DataSet dsgroup7 = clsgd.GetcondDataSet("*", "tblmanufacture", "ManufactureName", ddmanu.SelectedItem.Text);
        int manucode = Convert.ToInt32(dsgroup7.Tables[0].Rows[0]["ManufactureCode"].ToString());
        lblmanucode.Text = Convert.ToString(manucode);
    }
    ddpack.Enabled = true;
    ddpack.Focus();
    ddpack.BorderColor = System.Drawing.Color.Black;
    ddpack.BorderWidth = 1;
    ddpack.BorderStyle = BorderStyle.Dotted;
}
protected void ddpack_SelectedIndexChanged(object sender, EventArgs e)
{
    ddlshelf.Enabled = true;

    if (ddpack.SelectedItem.Text == "Add New")
    {
        this.ModalPopupExtender8.Enabled = true;
        ModalPopupExtender8.Show();
    }
    else
    {
        //this.ModalPopupExtender8.Enabled = false;
        //ModalPopupExtender8.Hide();
        //packsize();
        //ddpack.Items.Clear();
        if (ddpack.SelectedItem.Text == "-Select-")
        {
            Master.ShowModal("Please select a Pack Size. !!!", "ddgrpcode", 1);
            return;
        }
        DataSet dsgroup8 = clsgd.GetcondDataSet("*", "tblPacksize", "Pack_name", ddpack.SelectedItem.Text);
        int Pack_code = Convert.ToInt32(dsgroup8.Tables[0].Rows[0]["Pack_code"].ToString());
        lblPackcode.Text = Convert.ToString(Pack_code);
    }
    ddlshelf.Enabled = true;
    ddlshelf.Focus();
    ddlshelf.BorderColor = System.Drawing.Color.Black;
    ddlshelf.BorderWidth = 1;
    ddlshelf.BorderStyle = BorderStyle.Dotted;

}
protected void ddlshelf_SelectedIndexChanged(object sender, EventArgs e)
{
    ddlrow.Enabled = true;
    if (ddlshelf.SelectedItem.Text == "Add New")
    {
        this.ModalPopupExtender10.Enabled = true;
        ModalPopupExtender10.Show();
    }
    else
    {

        //this.ModalPopupExtender9.Enabled = false;
        //ModalPopupExtender9.Hide();
        //shelf();
        //ddlshelf.Items.Clear();
        ddlrow.Focus();
        if (ddlshelf.SelectedItem.Text == "-Select-")
        {
            Master.ShowModal("Please select a Shelf. !!!", "ddgrpcode", 1);
            return;
        }
        DataSet dsgroup9 = clsgd.GetcondDataSet("*", "tblShelf", "Se_name", ddlshelf.SelectedItem.Text);
        int Shelf_code = Convert.ToInt32(dsgroup9.Tables[0].Rows[0]["se_code"].ToString());
        lblShelfcode.Text = Convert.ToString(Shelf_code);

        string rack = ddlshelf.SelectedItem.Text;


        DataSet dsgroup19 = clsgd.GetDataSet("*", "tblShelf");

        //string rack = ddlshelf.SelectedItem.Text;


        //int shrow = Convert.ToInt32(dsgroup19.Tables[0].Rows[i]["srcount"]);
        DataSet dsgroup20 = clsgd.GetcondDataSet("*", "tblShelf", "Se_name", rack);

        int srcount = Convert.ToInt32(dsgroup20.Tables[0].Rows[0]["srcount"].ToString());
        string filename = Dbconn.Mymenthod();
        if (!File.Exists(filename))
        {

            switch (srcount)
            {
                case 1:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    break;
                case 2:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    break;

                case 3:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_3"].ToString());
                    break;

                case 4:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_4"].ToString());
                    break;

                case 5:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_5"].ToString());
                    break;

                case 6:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_6"].ToString());
                    break;

                case 7:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_7"].ToString());
                    break;

                case 8:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_8"].ToString());
                    break;


                case 9:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_9"].ToString());
                    break;


                case 10:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_10"].ToString());
                    break;


                case 11:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_11"].ToString());
                    break;

                case 12:
                     arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_11"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_12"].ToString());
                    break;

                case 13:
                     arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_11"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_12"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_13"].ToString());
                    break;

                case 14:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_11"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_12"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_13"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_14"].ToString());
                    break;

                case 15:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_11"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_12"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_13"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_14"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_15"].ToString());
                    break;

                case 16:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_11"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_12"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_13"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_14"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_15"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_16"].ToString());
                    break;


                case 17:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_11"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_12"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_13"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_14"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_15"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_16"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_17"].ToString());
                    break;


                case 18:
                   arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_11"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_12"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_13"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_14"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_15"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_16"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_17"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_18"].ToString());
                    break;

                case 19:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_11"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_12"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_13"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_14"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_15"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_16"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_17"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_18"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_19"].ToString());
                    break;

                case 20:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_11"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_12"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_13"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_14"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_15"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_16"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_17"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_18"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_19"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row_20"].ToString());
                    break;




            }


        }

        else
        {
            switch (srcount)
            {
                case 1:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    break;
                case 2:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    break;

                case 3:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row3"].ToString());
                    break;

                case 4:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row4"].ToString());
                    break;

                case 5:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row5"].ToString());
                    break;

                case 6:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row6"].ToString());
                    break;

                case 7:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row7"].ToString());
                    break;

                case 8:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row8"].ToString());
                    break;


                case 9:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row9"].ToString());
                    break;


                case 10:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row10"].ToString());
                    break;


                case 11:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row11"].ToString());
                    break;

                case 12:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row11"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row12"].ToString());
                    break;

                case 13:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row11"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row12"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row13"].ToString());
                    break;

                case 14:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row11"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row12"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row13"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row14"].ToString());
                    break;

                case 15:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row11"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row12"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row13"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row14"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row15"].ToString());
                    break;

                case 16:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row11"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row12"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row13"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row14"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row15"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row16"].ToString());
                    break;


                case 17:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row11"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row12"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row13"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row14"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row15"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row16"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row17"].ToString());
                    break;


                case 18:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row11"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row12"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row13"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row14"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row15"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row16"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row17"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row18"].ToString());
                    break;

                case 19:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row11"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row12"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row13"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row14"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row15"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row16"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row17"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row18"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row19"].ToString());
                    break;

                case 20:
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row1"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row2"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row3"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row4"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row5"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row6"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row7"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row8"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row9"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row10"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row11"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row12"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row13"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row14"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row15"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row16"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row17"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row18"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row19"].ToString());
                    arryname18.Add(dsgroup20.Tables[0].Rows[0]["row20"].ToString());
                    break;




            }



        }




            arryname18.Sort();
            arryno18.Add("-Select-");
            for (int i = 0; i < arryname18.Count; i++)
            {
                arryno18.Add(arryname18[i].ToString());
            }
            ddlrow.DataSource = arryno18;
            ddlrow.DataBind();

        }
    ddlrow.Enabled = true;
    ddlrow.Focus();
    ddlrow.BorderColor = System.Drawing.Color.Black;
    ddlrow.BorderWidth = 1;
    ddlrow.BorderStyle = BorderStyle.Dotted;
   


}
protected void ddlrow_SelectedIndexChanged(object sender, EventArgs e)
{
    
    if (ddlrow.SelectedItem.Text == "Add New")
    {
       // this.ModalPopupExtender10.Enabled = true;
       // ModalPopupExtender10.Show();
    }
    else
    {
        //this.ModalPopupExtender10.Enabled = false;
        //ModalPopupExtender10.Hide();
        //shelfrow();
        //ddlrow.Items.Clear();
        ddsupplier.Focus();
        string rack = ddlshelf.SelectedItem.Text;
        if (ddlrow.SelectedItem.Text == "-Select-")
        {
            Master.ShowModal("Please select a Row. !!!", "ddgrpcode", 1);
            return;
        }

        DataSet dsgroup20 = clsgd.GetcondDataSet("*", "tblShelf", "Se_name", rack);

        int srcount = Convert.ToInt32(dsgroup20.Tables[0].Rows[0]["srcount"].ToString());
        int row = Convert.ToInt32(ddlrow.SelectedItem.Text.ToString());


 string filename = Dbconn.Mymenthod();
 if (!File.Exists(filename))
 {


     switch (row)
     {
         case 1:
             DataSet dsgroup10 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_1", ddlrow.SelectedItem.Text);
             int Shelf_count = Convert.ToInt32(dsgroup10.Tables[0].Rows[0]["row_1"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count);
             break;

         case 2:
             DataSet dsgroup30 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_2", ddlrow.SelectedItem.Text);
             int Shelf_count2 = Convert.ToInt32(dsgroup30.Tables[0].Rows[0]["row_2"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count2);
             break;

         case 3:
             DataSet dsgroup31 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_3", ddlrow.SelectedItem.Text);
             int Shelf_count3 = Convert.ToInt32(dsgroup31.Tables[0].Rows[0]["row_3"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count3);
             break;


         case 4:
             DataSet dsgroup32 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_4", ddlrow.SelectedItem.Text);
             int Shelf_count4 = Convert.ToInt32(dsgroup32.Tables[0].Rows[0]["row_4"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count4);
             break;

         case 5:
             DataSet dsgroup33 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_5", ddlrow.SelectedItem.Text);
             int Shelf_count5 = Convert.ToInt32(dsgroup33.Tables[0].Rows[0]["row_5"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count5);
             break;


         case 6:
             DataSet dsgroup34 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_6", ddlrow.SelectedItem.Text);
             int Shelf_count6 = Convert.ToInt32(dsgroup34.Tables[0].Rows[0]["row_6"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count6);
             break;

         case 7:
             DataSet dsgroup35 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_7", ddlrow.SelectedItem.Text);
             int Shelf_count7 = Convert.ToInt32(dsgroup35.Tables[0].Rows[0]["row_7"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count7);
             break;

         case 8:
             DataSet dsgroup36 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_8", ddlrow.SelectedItem.Text);
             int Shelf_count8 = Convert.ToInt32(dsgroup36.Tables[0].Rows[0]["row_8"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count8);
             break;

         case 9:
             DataSet dsgroup37 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_9", ddlrow.SelectedItem.Text);
             int Shelf_count9 = Convert.ToInt32(dsgroup37.Tables[0].Rows[0]["row_9"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count9);
             break;

         case 10:
             DataSet dsgroup38 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_10", ddlrow.SelectedItem.Text);
             int Shelf_count10 = Convert.ToInt32(dsgroup38.Tables[0].Rows[0]["row_10"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count10);
             break;

         case 11:
             DataSet dsgroup39 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_11", ddlrow.SelectedItem.Text);
             int Shelf_count11 = Convert.ToInt32(dsgroup39.Tables[0].Rows[0]["row_11"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count11);
             break;

         case 12:
             DataSet dsgroup40 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_12", ddlrow.SelectedItem.Text);
             int Shelf_count12 = Convert.ToInt32(dsgroup40.Tables[0].Rows[0]["row_12"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count12);
             break;


         case 13:
             DataSet dsgroup41 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_13", ddlrow.SelectedItem.Text);
             int Shelf_count13 = Convert.ToInt32(dsgroup41.Tables[0].Rows[0]["row_13"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count13);
             break;

         case 14:
             DataSet dsgroup42 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_14", ddlrow.SelectedItem.Text);
             int Shelf_count14 = Convert.ToInt32(dsgroup42.Tables[0].Rows[0]["row_14"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count14);
             break;

         case 15:
             DataSet dsgroup43 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_15", ddlrow.SelectedItem.Text);
             int Shelf_count15 = Convert.ToInt32(dsgroup43.Tables[0].Rows[0]["row_15"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count15);
             break;

         case 16:
             DataSet dsgroup44 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_16", ddlrow.SelectedItem.Text);
             int Shelf_count16 = Convert.ToInt32(dsgroup44.Tables[0].Rows[0]["row_16"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count16);
             break;

         case 17:
             DataSet dsgroup45 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_17", ddlrow.SelectedItem.Text);
             int Shelf_count17 = Convert.ToInt32(dsgroup45.Tables[0].Rows[0]["row_17"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count17);
             break;

         case 18:
             DataSet dsgroup46 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_18", ddlrow.SelectedItem.Text);
             int Shelf_count18 = Convert.ToInt32(dsgroup46.Tables[0].Rows[0]["row_18"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count18);
             break;

         case 19:
             DataSet dsgroup47 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_19", ddlrow.SelectedItem.Text);
             int Shelf_count19 = Convert.ToInt32(dsgroup47.Tables[0].Rows[0]["row_19"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count19);
             break;

         case 20:
             DataSet dsgroup48 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row_20", ddlrow.SelectedItem.Text);
             int Shelf_count20 = Convert.ToInt32(dsgroup48.Tables[0].Rows[0]["row_20"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count20);
             break;

     }
 }
 else
 {
     switch (row)
     {
         case 1:
             DataSet dsgroup10 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row1", ddlrow.SelectedItem.Text);
             int Shelf_count = Convert.ToInt32(dsgroup10.Tables[0].Rows[0]["row1"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count);
             break;

         case 2:
             DataSet dsgroup30 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row2", ddlrow.SelectedItem.Text);
             int Shelf_count2 = Convert.ToInt32(dsgroup30.Tables[0].Rows[0]["row2"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count2);
             break;

         case 3:
             DataSet dsgroup31 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row3", ddlrow.SelectedItem.Text);
             int Shelf_count3 = Convert.ToInt32(dsgroup31.Tables[0].Rows[0]["row3"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count3);
             break;


         case 4:
             DataSet dsgroup32 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row4", ddlrow.SelectedItem.Text);
             int Shelf_count4 = Convert.ToInt32(dsgroup32.Tables[0].Rows[0]["row4"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count4);
             break;

         case 5:
             DataSet dsgroup33 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row5", ddlrow.SelectedItem.Text);
             int Shelf_count5 = Convert.ToInt32(dsgroup33.Tables[0].Rows[0]["row5"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count5);
             break;


         case 6:
             DataSet dsgroup34 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row6", ddlrow.SelectedItem.Text);
             int Shelf_count6 = Convert.ToInt32(dsgroup34.Tables[0].Rows[0]["row6"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count6);
             break;

         case 7:
             DataSet dsgroup35 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row7", ddlrow.SelectedItem.Text);
             int Shelf_count7 = Convert.ToInt32(dsgroup35.Tables[0].Rows[0]["row7"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count7);
             break;

         case 8:
             DataSet dsgroup36 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row8", ddlrow.SelectedItem.Text);
             int Shelf_count8 = Convert.ToInt32(dsgroup36.Tables[0].Rows[0]["row8"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count8);
             break;

         case 9:
             DataSet dsgroup37 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row9", ddlrow.SelectedItem.Text);
             int Shelf_count9 = Convert.ToInt32(dsgroup37.Tables[0].Rows[0]["row9"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count9);
             break;

         case 10:
             DataSet dsgroup38 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row10", ddlrow.SelectedItem.Text);
             int Shelf_count10 = Convert.ToInt32(dsgroup38.Tables[0].Rows[0]["row9"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count10);
             break;

         case 11:
             DataSet dsgroup39 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row11", ddlrow.SelectedItem.Text);
             int Shelf_count11 = Convert.ToInt32(dsgroup39.Tables[0].Rows[0]["row9"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count11);
             break;

         case 12:
             DataSet dsgroup40 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row12", ddlrow.SelectedItem.Text);
             int Shelf_count12 = Convert.ToInt32(dsgroup40.Tables[0].Rows[0]["row12"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count12);
             break;


         case 13:
             DataSet dsgroup41 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row13", ddlrow.SelectedItem.Text);
             int Shelf_count13 = Convert.ToInt32(dsgroup41.Tables[0].Rows[0]["row13"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count13);
             break;

         case 14:
             DataSet dsgroup42 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row14", ddlrow.SelectedItem.Text);
             int Shelf_count14 = Convert.ToInt32(dsgroup42.Tables[0].Rows[0]["row14"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count14);
             break;

         case 15:
             DataSet dsgroup43 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row15", ddlrow.SelectedItem.Text);
             int Shelf_count15 = Convert.ToInt32(dsgroup43.Tables[0].Rows[0]["row15"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count15);
             break;

         case 16:
             DataSet dsgroup44 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row16", ddlrow.SelectedItem.Text);
             int Shelf_count16 = Convert.ToInt32(dsgroup44.Tables[0].Rows[0]["row16"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count16);
             break;

         case 17:
             DataSet dsgroup45 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row17", ddlrow.SelectedItem.Text);
             int Shelf_count17 = Convert.ToInt32(dsgroup45.Tables[0].Rows[0]["row17"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count17);
             break;

         case 18:
             DataSet dsgroup46 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row18", ddlrow.SelectedItem.Text);
             int Shelf_count18 = Convert.ToInt32(dsgroup46.Tables[0].Rows[0]["row18"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count18);
             break;

         case 19:
             DataSet dsgroup47 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row19", ddlrow.SelectedItem.Text);
             int Shelf_count19 = Convert.ToInt32(dsgroup47.Tables[0].Rows[0]["row19"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count19);
             break;

         case 20:
             DataSet dsgroup48 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "row20", ddlrow.SelectedItem.Text);
             int Shelf_count20 = Convert.ToInt32(dsgroup48.Tables[0].Rows[0]["row20"]);
             lblShelfcount.Text = Convert.ToString(Shelf_count20);
             break;

     }


 }
    }

    ddsupplier.Enabled = true;
    ddsupplier.Focus();
    ddsupplier.BorderColor = System.Drawing.Color.Black;
    ddsupplier.BorderWidth = 1;
    ddsupplier.BorderStyle = BorderStyle.Dotted;

}
protected void ddsupplier_SelectedIndexChanged(object sender, EventArgs e)
{
    txtrecordlevel.Enabled = true;
    if (ddsupplier.SelectedItem.Text == "Add New")
    {
        this.ModalPopupExtender13.Enabled = true;
        ModalPopupExtender13.Show();
    }
    else
    {
        //this.ModalPopupExtender10.Enabled = false;
        //ModalPopupExtender10.Hide();
        //suppliername();
        //ddsupplier.Items.Clear();

        if (ddsupplier.SelectedItem.Text == "-Select-")
        {
            Master.ShowModal("Please select a Supplier. !!!", "ddgrpcode", 1);
            return;
        }

        DataSet dsgroup11 = clsgd.GetcondDataSet("*", "tblsuppliermaster", "SupplierName", ddsupplier.SelectedItem.Text);
        int Supp_count = Convert.ToInt32(dsgroup11.Tables[0].Rows[0]["SupplierCode"].ToString());
        lblsuplier.Text = Convert.ToString(Supp_count);
    }
    txtrecordlevel.Enabled = true;
    txtrecordlevel.Focus();

}
protected void ddshelf_SelectedIndexChanged(object sender, EventArgs e)
{
        if (ddshelf.SelectedItem.Text == "Add New")
    {
        this.ModalPopupExtender12.Enabled = true;
        ModalPopupExtender12.Show();
    }
    else
    {

        //this.ModalPopupExtender12.Enabled = false;
        //ModalPopupExtender12.Hide();
        //pshelfname();
        //ddshelf.Items.Clear();

        if (ddshelf.SelectedItem.Text == "-Select-")
        {
            Master.ShowModal("Please select a Shelf. !!!", "ddgrpcode", 1);
            return;
        }
        DataSet dsgroup12 = clsgd.GetcondDataSet("*", "tblShelf", "Se_name", ddshelf.SelectedItem.Text);
        int Shelf1_code = Convert.ToInt32(dsgroup12.Tables[0].Rows[0]["se_code"].ToString());
        lblShelf1code.Text = Convert.ToString(Shelf1_code);


        string rack = ddshelf.SelectedItem.Text;


        DataSet dsgroup19 = clsgd.GetDataSet("*", "tblShelf");

        //string rack = ddlshelf.SelectedItem.Text;


        //int shrow = Convert.ToInt32(dsgroup19.Tables[0].Rows[i]["srcount"]);



        DataSet dsgroup21 = clsgd.GetcondDataSet("*", "tblShelf", "Se_name", rack);
        // arryname22.Add(dsgroup21.Tables[0].Rows[0]["srcount"].ToString());

        int srcount = Convert.ToInt32(dsgroup21.Tables[0].Rows[0]["srcount"].ToString());

        if (!File.Exists(filename))
        {

            switch (srcount)
            {
                case 1:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    break;
                case 2:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    break;

                case 3:
                     arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_3"].ToString());
                    break;

                case 4:
                   arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_4"].ToString());
                   
                    break;

                case 5:
                     arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_5"].ToString());
                    
                    break;

                case 6:
                   arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_6"].ToString());
                    
                    break;

                case 7:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_7"].ToString());
                   
                    break;

                case 8:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_8"].ToString());
                   
                    break;


                case 9:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_9"].ToString());
                   
                    break;


                case 10:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_10"].ToString());
                   
                    break;


                case 11:
                  arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_11"].ToString());
                    
                    break;

                case 12:
                     arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_11"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_12"].ToString());
                    
                    break;

                case 13:
                     arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_11"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_12"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_13"].ToString());
                    
                    break;

                case 14:
                     arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_11"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_12"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_13"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_14"].ToString());
                   
                    break;

                case 15:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_11"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_12"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_13"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_14"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_15"].ToString());
                    break;

                case 16:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_11"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_12"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_13"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_14"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_15"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_16"].ToString());
                    break;


                case 17:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_11"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_12"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_13"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_14"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_15"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_16"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_17"].ToString());
                    break;


                case 18:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_11"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_12"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_13"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_14"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_15"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_16"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_17"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_18"].ToString());
                    break;

                case 19:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_11"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_12"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_13"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_14"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_15"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_16"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_17"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_18"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_19"].ToString());
                    break;

                case 20:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_11"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_12"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_13"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_14"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_15"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_16"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_17"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_18"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_19"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row_20"].ToString());
                    break;




            }
        }
        else
        {

            switch (srcount)
            {
                case 1:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    break;
                case 2:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    break;

                case 3:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row3"].ToString());
                    break;

                case 4:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row4"].ToString());
                    break;

                case 5:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row5"].ToString());
                    break;

                case 6:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row6"].ToString());
                    break;

                case 7:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row7"].ToString());
                    break;

                case 8:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row8"].ToString());
                    break;


                case 9:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row9"].ToString());
                    break;


                case 10:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row10"].ToString());
                    break;


                case 11:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row11"].ToString());
                    break;

                case 12:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row11"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row12"].ToString());
                    break;

                case 13:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row11"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row12"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row13"].ToString());
                    break;

                case 14:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row11"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row12"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row13"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row14"].ToString());
                    break;

                case 15:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row11"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row12"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row13"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row14"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row15"].ToString());
                    break;

                case 16:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row11"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row12"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row13"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row14"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row15"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row16"].ToString());
                    break;


                case 17:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row11"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row12"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row13"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row14"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row15"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row16"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row17"].ToString());
                    break;


                case 18:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row11"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row12"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row13"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row14"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row15"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row16"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row17"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row18"].ToString());
                    break;

                case 19:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row11"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row12"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row13"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row14"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row15"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row16"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row17"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row18"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row19"].ToString());
                    break;

                case 20:
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row1"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row2"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row3"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row4"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row5"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row6"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row7"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row8"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row9"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row10"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row11"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row12"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row13"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row14"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row15"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row16"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row17"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row18"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row19"].ToString());
                    arryname22.Add(dsgroup21.Tables[0].Rows[0]["row20"].ToString());
                    break;




            }

        }





        arryname22.Sort();
        arryno22.Add("-Select-");
        for (int i = 0; i < arryname22.Count; i++)
        {
            arryno22.Add(arryname22[i].ToString());
        }
        //arryname22.Add("-Select-");
        ddrow.DataSource = arryno22;
        ddrow.DataBind();

        ddrow.Enabled = true;
        ddrow.Focus();
        ddrow.BorderColor = System.Drawing.Color.Black;
        ddrow.BorderWidth = 1;
        ddrow.BorderStyle = BorderStyle.Dotted;

    



    }
    
}
protected void ddrow_SelectedIndexChanged(object sender, EventArgs e)
{
    if (ddrow.SelectedItem.Text == "Add New")
    {
        //this.ModalPopupExtender13.Enabled = true;
        //ModalPopupExtender13.Show();
    }
    else
    {
        //this.ModalPopupExtender13.Enabled = false;
        //ModalPopupExtender13.Hide();
        //shelfrowcount();
        //ddrow.Items.Clear();

        //string rack1 = ddshelf.SelectedItem.Text;
        if (ddrow.SelectedItem.Text == "-Select-")
        {
            Master.ShowModal("Please select a Row. !!!", "ddgrpcode", 1);
            return;
        }
        // int rowcount = Convert.ToInt32(ddrow.SelectedItem.Text);
        //DataSet dsgroup13 = clsgd.GetcondDataSetintnumber("*", "tblShelf", "srcount", ddrow.SelectedItem.Text);
        //int Shelf_count1 = Convert.ToInt32(dsgroup13.Tables[0].Rows[0]["srcount"]);
        //lblShelf1count.Text = Convert.ToString(Shelf_count1);

        txtreorder.Enabled = true; 
            txtreorder.Focus();

            
             

         

    }

}
protected void ddlunit_SelectedIndexChanged(object sender, EventArgs e)
{
    if (ddlunit.SelectedItem.Text == "Add New")
    {
        this.ModalPopupExtender14.Enabled = true;
        ModalPopupExtender14.Show();
    }
    else
    {
        //this.ModalPopupExtender14.Enabled = false;
        //ModalPopupExtender14.Hide();
        //unitdetails();
        if (ddlunit.SelectedItem.Text == "-Select-")
        {
            Master.ShowModal("Please select a Unit. !!!", "ddgrpcode", 1);
            return;
        }
        DataSet dsgroup14 = clsgd.GetcondDataSet("*", "tblunitmaster", "unitname", ddlunit.SelectedItem.Text);
        int Unitcode1 = Convert.ToInt32(dsgroup14.Tables[0].Rows[0]["unitcode"].ToString());
        lblunitcode1.Text = Convert.ToString(Unitcode1);
        btnsave.Enabled = true;
        btnsave.Focus();
    }

}
public void shelfrow()
{
    DataSet dsgroup19 = clsgd.GetDataSet("distinct srcount", "tblShelf");

    for (int i = 0; i < dsgroup19.Tables[0].Rows.Count; i++)
    {
        int shrow = Convert.ToInt32(dsgroup19.Tables[0].Rows[i]["srcount"]);
        DataSet dsgroup20 = clsgd.GetcondDataSetint("*", "tblShelf", "srcount", shrow);
        arryname18.Add(dsgroup20.Tables[0].Rows[0]["srcount"].ToString());

    }
    arryname18.Sort();
    arryno18.Add("-Select-");

    //arryno18.Add("Add New");

    for (int i = 0; i < arryname18.Count; i++)
    {
        arryno18.Add(arryname18[i].ToString());
    }
    ddlrow.DataSource = arryno18;
    ddlrow.DataBind();


}
public void shelfrowcount()
{
    DataSet dsgroup20 = clsgd.GetDataSet("distinct srcount", "tblShelf");

    for (int i = 0; i < dsgroup20.Tables[0].Rows.Count; i++)
    {
        int shrow = Convert.ToInt32(dsgroup20.Tables[0].Rows[i]["srcount"]);
        DataSet dsgroup21 = clsgd.GetcondDataSetint("*", "tblShelf", "srcount", shrow);
        arryname22.Add(dsgroup21.Tables[0].Rows[0]["srcount"].ToString());

    }
    arryname22.Sort();
    arryno22.Add("-Select-");
    arryno22.Add("Add New");

    for (int i = 0; i < arryname22.Count; i++)
    {
        arryno22.Add(arryname22[i].ToString());
    }
    ddrow.DataSource = arryno22;
    ddrow.DataBind();


}
protected void btnsave_Click(object sender, EventArgs e)
{
    try
    {


        string prodcode = txtProd.Text.TrimStart();
        string strCaps1 = Regex.Replace(prodcode, "[0-9]", "");
        string strEdited = Regex.Replace(strCaps1, @"\s+", " ");
        string Sysdatetime=DateTime.Now.ToString();


        string prodname = txtPname.Text.TrimStart();
        string strCaps2 = Regex.Replace(prodname, "[^a-zA-Z + \\s]", "");
        string strEdited2 = Regex.Replace(strCaps2, @"\s+", " ");

        string groupname = lblgrcode.Text;
        string Pharmflag = lblpfflag.Text;

        string Login_name = Session["username"].ToString();
        string genericcode = lblgncode.Text;
        string Cemcode = lblcemcode.Text;
        string Medcode = lblmedcode.Text;
        string Unit = lblunitcode.Text;
        string Form = lblformcode.Text;
        string Manufacturer = lblmanucode.Text;
        string Packsize = lblPackcode.Text;
        string Shelf = lblShelfcode.Text;
        string Row = lblShelfcount.Text;
        string Suppname = lblsuplier.Text;
        string Reorderlevel = txtrecordlevel.Text;
        string Reorderlevel1 = txtreorder.Text;
        
        //string RateofIntrest = txtrateofinterest.Text;
        //string RateofInterest1 = txtrateofint.Text;
        //string txtrateofint = txtrateofint.text;
        string Shelf1 = lblShelf1code.Text;
        //string row1 = ddrow.SelectedItem.Text;
        string  unit1  =lblunitcode1.Text;
       // string Pm_flag1 = "00000";
        //string Pm_flag2 = "N";
        //string Pm_flag3 = "N";
        //string Pm_flag4 = "N";
        //string Pm_flag5 = "N";
        //string Pm_flag6 = "N";
        //string Pm_flag7 = "N";
        //string Pm_flag8 = "N";
        //string Pm_flag9 = "N";
        //string Pm_flag10 = "N";

        if (prodcode == "")
        {
            Master.ShowModal("Product Code is mandotory", "txtProd", 0);
            return;

        }
        if (strEdited2 == "")
        {
            Master.ShowModal("Product Name is mandotory", "txtPname", 0);
            return;
        }

        if (prodcode != "" || strEdited2 != "")
        {
            ddgrpcode.Visible = true;
        }
        if (ddgrpcode.SelectedItem.Text == "-Select-")
        {
            Master.ShowModal("Group Name is mandotory", "ddgrpcode", 0);
            return;

        }  
        string p_flag2 = ddgrpcode.SelectedItem.Text;

                 DataSet dsgroup10 = clsgd.GetcondDataSet("*", "tblGroup", "g_name", p_flag2);
                
                 
        

                 string flag2 = dsgroup10.Tables[0].Rows[0]["p_flag"].ToString();
                 if (flag2 == "Y")
                 {
                     Panel2.Visible = true;
                     Panel3.Visible = false;
                 }
                 else
                 {
                     Panel3.Visible = true;
                     Panel2.Visible = false;
                    
                    
                 }
        DataSet dsgrp = clsgd.GetcondDataSet("*", "tblProductMaster", "Productcode", prodcode);
        if (dsgrp.Tables[0].Rows.Count > 0)
        {
            //lblmod.Text = "Product Code  Already Exists";
            //int code = Convert.ToInt32(dsgrp.Tables[0].Rows[0]["Productcode"].ToString());
            //lblcode.Text = Convert.ToString(code);
            //Table2.Visible = true;
            Master.ShowModal("Product Code  Already Exists", "txtProd", 1);
            return;
        }

        DataSet dsgrp1 = clsgd.GetcondDataSet("*", "tblProductMaster", "Productname", prodname);
        if (dsgrp1.Tables[0].Rows.Count > 0)
        {
            //lblmod.Text = "Product Name  Already Exists";
            //int code = Convert.ToInt32(dsgrp1.Tables[0].Rows[0]["Productname"].ToString());
            //lblcode.Text = Convert.ToString(code);
            //Table2.Visible = true;
            Master.ShowModal("Product Name already exists", "txtPname", 1);
            return;
        }



        else  
        {
            
             string filename = Dbconn.Mymenthod();
            if (!File.Exists(filename))
            {
               
                if (flag2 == "Y")
                {

                    if (groupname == "")
                    {
                        Master.ShowModal("Group Name is mandotory", "lblgrcode", 0);
                        return;

                    }




                    if (genericcode == "")
                    {
                        Master.ShowModal("Generic Name is mandotory", "lblgncode", 0);
                        return;

                    }

                    if (Cemcode == "")
                    {
                        Master.ShowModal("Chemical Name is mandotory", "lblcemcode", 0);
                        return;

                    }

                    if (Medcode == "")
                    {
                        Master.ShowModal("Med Name is mandotory", "lblmedcode", 0);
                        return;

                    }
                    if (Unit == " ")
                    {
                        Master.ShowModal("Unit Name is mandotory", "lblunitcode", 0);
                        return;

                    }

                    if (Form == "")
                    {
                        Master.ShowModal("Form Name is mandotory", "lblformcode", 0);
                        return;

                    }

                    if (Manufacturer == "")
                    {
                        Master.ShowModal("Manufacturer Name is mandotory", "lblmanucode", 0);
                        return;

                    }

                    if (Packsize == "")
                    {
                        Master.ShowModal("Packsize Name is mandotory", "lblPackcode", 0);
                        return;

                    }
                    if (Shelf == " ")
                    {
                        Master.ShowModal("Shelf Name is mandotory", "lblShelfcode", 0);
                        return;

                    }
                    if (Row == "")
                    {
                        Master.ShowModal("Row Name is mandotory", "lblShelfcount", 0);
                        return;

                    }

                    if (Suppname == "")
                    {
                        // Master.ShowModal("Suppler  Name is mandotory", "lblsuplier", 0);
                        // return;

                        Suppname = "0";

                    }
                    else
                    {
                        Suppname = lblsuplier.Text;
                    }

                    if (Reorderlevel == "")
                    {
                        Master.ShowModal("Reorder Level is mandotory", "txtrecordlevel", 0);
                        return;

                    }

                    SqlConnection cond = new SqlConnection(strconn11);
                    cond.Open();
                    SqlCommand cmdd = new SqlCommand("Select * from tblProductPhoto where P_Code='" + txtProd.Text + "'", cond);
                    SqlDataAdapter dad = new SqlDataAdapter(cmdd);
                    DataSet dsd = new DataSet();
                    dad.Fill(dsd);
                    if (dsd.Tables[0].Rows.Count > 0)
                    {
                        string url = dsd.Tables[0].Rows[0]["Image_url"].ToString();
                        SqlCommand cmdp = new SqlCommand("Select Image from tblProductPhoto where P_Code='" + txtProd.Text + "'", cond);
                        SqlDataReader reader = cmdp.ExecuteReader();
                        reader.Read();
                        if (reader.HasRows)
                        {
                            byte[] photo = (byte[])(reader[0]);
                            ClsBLGP.Product("INSERT_PRODUCTMASTER", prodcode, prodname, groupname, Pharmflag, genericcode, Cemcode, Medcode, Unit, Form, Manufacturer, Packsize, Shelf, Row, Suppname, Reorderlevel, "0", "00000", "N", "N", "N", "N", "N", "N", "N", "N", "N", Login_name, sMacAddress, Sysdatetime, photo);
                        }
                    }
                    else
                    {
                        byte[] na = { 00 };
                        ClsBLGP.Product("INSERT_PRODUCTMASTER", prodcode, prodname, groupname, Pharmflag, genericcode, Cemcode, Medcode, Unit, Form, Manufacturer, Packsize, Shelf, Row, Suppname, Reorderlevel, "0", "00000", "N", "N", "N", "N", "N", "N", "N", "N", "N", Login_name, sMacAddress, Sysdatetime, na);

                    }
                }
                else
                {
                    string row1 = ddrow.SelectedItem.Text;

                    if (Shelf1 == "-Select-")
                    {
                        Master.ShowModal("Shelf Name is mandotory", "txtrateofinterest", 0);
                        return;

                    }



                    if (row1 == "-Select-")
                    {
                        Master.ShowModal("Row Name is mandotory", "txtrateofinterest", 0);
                        return;

                    }

                    if (Reorderlevel1 == "")
                    {
                        Master.ShowModal("Reorder Level is mandotory", "txtrateofinterest", 0);
                        return;

                    }

                   

                    if (unit1 == "")
                    {
                        Master.ShowModal("Unit name is mandotory", "txtrateofinterest", 0);
                        return;

                    }
                    SqlConnection cond = new SqlConnection(strconn11);
                    cond.Open();
                    SqlCommand cmdd = new SqlCommand("Select * from tblProductPhoto where P_Code='" + txtProd.Text + "'", cond);
                    SqlDataAdapter dad = new SqlDataAdapter(cmdd);
                    DataSet dsd = new DataSet();
                    dad.Fill(dsd);
                    if (dsd.Tables[0].Rows.Count > 0)
                    {
                        string url = dsd.Tables[0].Rows[0]["Image_url"].ToString();
                        SqlCommand cmdp = new SqlCommand("Select Image from tblProductPhoto where P_Code='" + txtProd.Text + "'", cond);
                        SqlDataReader reader = cmdp.ExecuteReader();
                        reader.Read();
                        if (reader.HasRows)
                        {
                            byte[] photo = (byte[])(reader[0]);
                            //SqlCommand cmdup = new SqlCommand("update tblProductMaster set Photo_url='" + url + "',Photo='" + img + "' where Productcode='" + txtProd.Text + "'");
                            ClsBLGP.Product("INSERT_PRODUCTMASTER", prodcode, prodname, groupname, Pharmflag, "0", "0", "0", unit1, "0", "0", "0", Shelf1, row1, "0", Reorderlevel1, "0", "00000", "N", "N", "N", "N", "N", "N", "N", "N", url, Login_name, sMacAddress, Sysdatetime, photo);
                            //cmdd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        byte[] na = { 00 };
                        ClsBLGP.Product("INSERT_PRODUCTMASTER", prodcode, prodname, groupname, Pharmflag, "0", "0", "0", unit1, "0", "0", "0", Shelf1, row1, "0", Reorderlevel1, "0", "00000", "N", "N", "N", "N", "N", "N", "N", "N", "N", Login_name, sMacAddress, Sysdatetime, na);
                    }
                }

            }
            else
             {
                 if (flag2 == "Y")
                 {

                    


                     if (genericcode == "")
                     {
                         Master.ShowModal("Generic Name is mandotory", "lblgncode", 0);
                         return;

                     }

                     if (Cemcode == "")
                     {
                         Master.ShowModal("Chemical Name is mandotory", "lblcemcode", 0);
                         return;

                     }

                     if (Medcode == "")
                     {
                         Master.ShowModal("Med Name is mandotory", "lblmedcode", 0);
                         return;

                     }
                     if (Unit == "")
                     {
                         Master.ShowModal("Unit Name is mandotory", "lblunitcode", 0);
                         return;

                     }

                     if (Form == "")
                     {
                         Master.ShowModal("Form Name is mandotory", "lblformcode", 0);
                         return;

                     }

                     if (Manufacturer == "")
                     {
                         Master.ShowModal("Manufacturer Name is mandotory", "lblmanucode", 0);
                         return;

                     }

                     if (Packsize == "")
                     {
                         Master.ShowModal("Packsize Name is mandotory", "lblPackcode", 0);
                         return;

                     }
                     if (Shelf == "")
                     {
                         Master.ShowModal("Shelf Name is mandotory", "lblShelfcode", 0);
                         return;

                     }
                     if (Row == "")
                     {
                         Master.ShowModal("Row Name is mandotory", "lblShelfcount", 0);
                         return;

                     }

                     if (Suppname == "")
                     {
                         Master.ShowModal("Suppler  Name is mandotory", "lblsuplier", 0);
                         return;

                     }

                     if (Reorderlevel == "")
                     {
                         Master.ShowModal("Reorder Level is mandotory", "txtrecordlevel", 0);
                         return;

                     }

                    

                     //String strconn11 = Dbconn.conmenthod();
                     OleDbConnection con = new OleDbConnection(strconn11);
                     con.Open();
                     OleDbCommand cmd = new OleDbCommand("insert into tblProductMaster(Productcode,Productname,Groupname,Pharmflag,Genericcode,Cemcode,Medcode,Unit,Form,Manufacturer,Packsize,Shelf,Row,Suppname,Reorderlevel,RateofIntrest,Pm_flag1,Pm_flag2,Pm_flag3,Pm_flag4,Pm_flag5,Pm_flag6,Pm_flag7,Pm_flag8,Pm_flag9,Pm_flag10,LoginName,Mac_id,Sysdatetime) values('" + prodcode + "','" + prodname + "','" + groupname + "','" + Pharmflag + "','" + genericcode + "','" + Cemcode + "','" + Medcode + "','" + Unit + "','" + Form + "','" + Manufacturer + "','" + Packsize + "','" + Shelf + "','" + Row + "','" + Suppname + "','" + Reorderlevel + "','0',00000,'N','N','N','N','N','N','N','N','N','" + Login_name + "','" + sMacAddress + "','" + Sysdatetime + "')", con);
                     cmd.ExecuteNonQuery();
                     con.Close();
                 }
                 else
                 {
                     string row1 = ddrow.SelectedItem.Text;
                     if (prodcode == "")
                     {
                         Master.ShowModal("Product Code is mandotory", "txtProd", 0);
                         return;

                     }





                     if (strEdited2 == "")
                     {
                         Master.ShowModal("Product Name is mandotory", "txtPname", 0);
                         return;

                     }
                     if (Shelf1 == "")
                     {
                         Master.ShowModal("Shelf Name is mandotory", "txtrateofinterest", 0);
                         return;

                     }

                     if (row1 == "")
                     {
                         Master.ShowModal("Row Name is mandotory", "txtrateofinterest", 0);
                         return;

                     }

                     if (Reorderlevel1 == "")
                     {
                         Master.ShowModal("Reorder Level is mandotory", "txtrateofinterest", 0);
                         return;

                     }

                    

                     if (unit1 == "")
                     {
                         Master.ShowModal("Unit name is mandotory", "txtrateofinterest", 0);
                         return;

                     }

                      //String strconn11 = Dbconn.conmenthod();
                     OleDbConnection con = new OleDbConnection(strconn11);
                     con.Open();
                     //OleDbCommand cmd = new OleDbCommand("insert into tblProductMaster(Productcode,Productname,Groupname,Pharmflag,Genericcode,Cemcode,Medcode,Unit,Form,Manufacturer,Packsize,Shelf,Row,Suppname,Reorderlevel,RateofIntrest) values('" + prodcode + "','" + prodname + "','" + groupname + "','" + Pharmflag + "','" + genericcode + "',0,0,0,0,0,0,'" + Shelf1 + "','" + row1 + "',0,'" + Reorderlevel1 + "','" + RateofInterest1 + "')", con);
                     OleDbCommand cmd = new OleDbCommand("insert into tblProductMaster(Productcode,Productname,Groupname,Pharmflag,Genericcode,Cemcode,Medcode,Unit,Form,Manufacturer,Packsize,Shelf,Row,Suppname,Reorderlevel,RateofIntrest,Pm_flag1,Pm_flag2,Pm_flag3,Pm_flag4,Pm_flag5,Pm_flag6,Pm_flag7,Pm_flag8,Pm_flag9,Pm_flag10)values('" + prodcode + "','" + prodname + "','" + groupname + "','" + Pharmflag + "',0,0,0,'" + unit1 + "',0,0,0,'" + Shelf1 + "','" + row1 + "',0,'" + Reorderlevel1 + "','0',00000,'N','N','N','N','N','N','N','N','N','" + Login_name + "','" + sMacAddress + "','" + Sysdatetime + "')", con);
                     cmd.ExecuteNonQuery();
                     con.Close();

                 }


            }
           /* SqlConnection cond =new SqlConnection(strconn11);
            cond.Open();
            SqlCommand cmdd = new SqlCommand("Select * from tblProductPhoto where P_Code='" + txtProd.Text + "'", cond);
            SqlDataAdapter dad = new SqlDataAdapter(cmdd);
            DataSet dsd = new DataSet();
            dad.Fill(dsd);
            if (dsd.Tables[0].Rows.Count > 0)
            {
                string url = dsd.Tables[0].Rows[0]["Image_url"].ToString();
                string img = dsd.Tables[0].Rows[0]["Image"].ToString();
                SqlCommand cmdup = new SqlCommand("update tblProductMaster set Photo_url='" + url + "',Photo='" + img + "' where Productcode='" + txtProd.Text +"'");
                cmdd.ExecuteNonQuery();
            }*/

            lblsuccess.Visible = true;
            lblsuccess.Text = "inserted successfully";
           
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            txtProd.Text = string.Empty;
            txtPname.Text = string.Empty;
            chkgroup.Checked = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            txtProd.Focus();
            Image1.Visible = false;
            Image1.ImageUrl = "";
            
            //DropDownList.Items.Clear();#sthash.ac7tyYN6.dpuf
            ddgrpcode.ClearSelection();
            lblgrcode.Text = string.Empty;
              
           if (flag2 == "Y")
           {
               ddGecode.ClearSelection();
               ddchem.ClearSelection();
               ddmed.ClearSelection();
               ddunit.ClearSelection();
               ddform.ClearSelection();
               ddmanu.ClearSelection();
               ddpack.ClearSelection();
               ddlshelf.ClearSelection();
               ddlrow.ClearSelection();
               ddsupplier.ClearSelection();
               txtrecordlevel.Text = string.Empty;
              // txtrateofinterest.Text = string.Empty;
               
               lblgncode.Text = string.Empty;
               lblcemcode.Text = string.Empty;
               lblmedcode.Text=string.Empty;
               lblunitcode.Text=string.Empty;
                lblformcode.Text=string.Empty;
                lblmanucode.Text=string.Empty;
               lblPackcode.Text=string.Empty;
                lblShelfcode.Text=string.Empty;
                lblShelfcount.Text=string.Empty;
                lblsuplier.Text = string.Empty;



           }
           else
           {
               ddshelf.ClearSelection();
               ddlunit.ClearSelection();
               ddrow.ClearSelection();
               ddrow.SelectedItem.Text = "";
               
               
               txtreorder.Text = string.Empty;
               //txtrateofint.Text = string.Empty;
               lblShelf1code.Text=string.Empty;
               //lblShelf1count.Text = string.Empty;
               lblunitcode1.Text = string.Empty;


           }
           //btnExit.Focus();
            //Response.Redirect("Unitmaster.aspx");

        }
    }
    catch (Exception ex)
    {
        string asd = ex.Message;
        lblerror.Visible = true;
        lblerror.Text = asd;
    }
}

protected void txtrecordlevel_TextChanged(object sender, EventArgs e)
{
    //txtrateofinterest.Focus();
}
protected void txtreorder_TextChanged(object sender, EventArgs e)
{
    //txtrateofint.Enabled = true;
    //txtrateofint.Focus();
    //ddlunit.Enabled = true;
    ddlunit.Focus();
    ddlunit.BorderColor = System.Drawing.Color.Black;
    ddlunit.BorderWidth = 1;
    ddlunit.BorderStyle = BorderStyle.Dotted;
}
protected void txtrateofint_TextChanged(object sender, EventArgs e)
{
    ddlunit.Enabled = true;
    ddlunit.Focus();
}

protected void txtrateofinterest_TextChanged(object sender, EventArgs e)
{
    btnsave.Focus();
}
protected void txtProd_TextChanged(object sender, EventArgs e)
{
    string productcode = txtProd.Text;
    DataSet dschm = clsgd.GetcondDataSet9("*", "tblProductMaster", "Productcode", productcode);
    if (dschm.Tables[0].Rows.Count > 0)
    {
        try
        {
            //lblmod.Visible = true;
           // lblmod.Text = "Product code Already Exists";

            Master.ShowModal("Product Code already exists", "txtProd", 0);
            txtProd.Text = string.Empty;
            return;
           
        }
        catch (Exception ex)
        {
            string asd = ex.Message;
            lblerror.Visible = true;
            lblerror.Text = asd;
        }
    }
    string prodcode = txtProd.Text.TrimStart();

    if (prodcode == "")
    {
        btnExit.Focus();
    }
    txtPname.Focus();

}
protected void txtPname_TextChanged(object sender, EventArgs e)
{

    if (txtProd.Text != "" || txtPname.Text != "")
    {

        ddgrpcode.Enabled = true;
        //ddgrpcode.Focus();
       // ddgrpcode.BorderColor = System.Drawing.Color.Black;
       // ddgrpcode.BorderWidth = 1;
       // ddgrpcode.BorderStyle = BorderStyle.Dotted;
         // groupcode();
         using (SqlConnection conn10 = new SqlConnection(strconn11))
                    {
                        conn10.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

                       

                        DataSet ds = new DataSet();
                        conn10.Open();

                        //string cmdstr = "Select Batchid from tblProductinward where productcode ="'+ productcode +'"";
                        string cmdstr = "select g_name from tblGroup where p_flag='N'";

                        SqlCommand cmd10 = new SqlCommand(cmdstr, conn10);

                        SqlDataAdapter adp = new SqlDataAdapter(cmd10);

                        adp.Fill(ds);

                        ddgrpcode.DataSource = ds.Tables[0];

                        
                       ddgrpcode.DataTextField = "g_name";

                       ddgrpcode.DataBind();
                       ddgrpcode.BackColor = Color.Red; 

                       ddgrpcode.Items.Insert(0, new ListItem("-Select-", "0"));

                        conn10.Close();

                    }

          chkgroup.BorderColor = System.Drawing.Color.Black;
          chkgroup.BorderWidth = 1;
          chkgroup.BorderStyle = BorderStyle.Dotted;
          chkgroup.Enabled = true;
          chkgroup.Focus();
    }
    else
    {
       // ddgrpcode.Enabled = true;
        //ddgrpcode.Focus();
       // groupcode();
        Master.ShowModal("Product  name is mandotory", "txtPname", 0);
        return;
    }
}
protected void btncancel_click(object sender, EventArgs e)
{
    this.ModalPopupExtender1.Enabled = false;
    ModalPopupExtender1.Hide();
   
    //ddgrpcode.Items.Clear();
    groupcode();
    ddgrpcode.Focus();

}

protected void btncancel1_click(object sender, EventArgs e)
{
    this.ModalPopupExtender2.Enabled = false;
    ModalPopupExtender2.Hide();
    
    //ddgrpcode.Items.Clear();
    genericcode();
    ddGecode.Focus();

}

protected void btncancel2_click(object sender, EventArgs e)
{
    this.ModalPopupExtender3.Enabled = false;
    ModalPopupExtender3.Hide();
   
    //ddgrpcode.Items.Clear();
    chemicalcode();
    ddchem.Focus();

}

protected void btncancel3_click(object sender, EventArgs e)
{
    this.ModalPopupExtender4.Enabled = false;
    ModalPopupExtender4.Hide();
    //ddgrpcode.Items.Clear();
    medicinetype();
    ddmed.Focus();
}
protected void btncancel4_click(object sender, EventArgs e)
{
    this.ModalPopupExtender5.Enabled = false;
    ModalPopupExtender5.Hide();
    //ddgrpcode.Items.Clear();
    unit();
}
protected void btncancel5_click(object sender, EventArgs e)
{
    this.ModalPopupExtender6.Enabled = false;
    ModalPopupExtender6.Hide();
    //ddgrpcode.Items.Clear();
    form();
    ddform.Focus();
}
protected void btncancel6_click(object sender, EventArgs e)
{
    this.ModalPopupExtender7.Enabled = false;
    ModalPopupExtender7.Hide();
    //ddgrpcode.Items.Clear();
    manufacture();
    ddmanu.Focus();
}
protected void btncancel7_click(object sender, EventArgs e)
{
    this.ModalPopupExtender8.Enabled = false;
    ModalPopupExtender8.Hide();
    //ddgrpcode.Items.Clear();
    packsize();
}
protected void btncancel8_click(object sender, EventArgs e)
{
    this.ModalPopupExtender9.Enabled = false;
    ModalPopupExtender9.Hide();
    //ddgrpcode.Items.Clear();
    shelf();
}

protected void btncancel9_click(object sender, EventArgs e)
{
    //this.ModalPopupExtender10.Enabled = false;
    //ModalPopupExtender10.Hide();
    //ddgrpcode.Items.Clear();
    //shelfrow();
}
protected void btncancel10_click(object sender, EventArgs e)
{
    this.ModalPopupExtender13.Enabled = false;
    ModalPopupExtender13.Hide();
    //ddgrpcode.Items.Clear();
    suppliername();
    ddsupplier.Focus();
}

protected void btncancel11_click(object sender, EventArgs e)
{
    this.ModalPopupExtender12.Enabled = false;
    ModalPopupExtender12.Hide();
    //ddgrpcode.Items.Clear();
    pshelfname();
}
protected void btncancel12_click(object sender, EventArgs e)
{
    //this.ModalPopupExtender13.Enabled = false;
    //ModalPopupExtender12.Hide();
    //ddgrpcode.Items.Clear();
    //shelfrowcount();
}
protected void btncancel13_click(object sender, EventArgs e)
{
    this.ModalPopupExtender14.Enabled = false;
    ModalPopupExtender14.Hide();
    //ddgrpcode.Items.Clear();
    //unitdetails();
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
        SqlCommand cmd = new SqlCommand("select Productname from tblProductMaster where Productname like @1+'%'", conn);
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
            OleDbCommand cmd=new OleDbCommand("select Productname from tblProductMaster where Productname like @1+'%'", conn);
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
       


       protected void btnUpload_Click(object sender, EventArgs e)
       {
           //string filepath = FileUpload1.PostedFile.FileName; 
           int slno=1;
           string filename = string.Empty;
           filename = FileUpload1.FileName;
           if (!File.Exists(filename))
           {
               int img = FileUpload1.PostedFile.ContentLength;
               byte[] myimg = new byte[img];
               FileUpload1.PostedFile.InputStream.Read(myimg, 0, img);
               string img1 = Convert.ToBase64String(myimg,0,myimg.Length);
               Image1.ImageUrl = "data:image/png;base64," + img1;
               Image1.Visible = true;
               ViewState["imagestore"] = img1;
               SqlConnection con1=new SqlConnection(strconn11);
               SqlCommand cmd=new SqlCommand("Select max(Sl_no) as Sl_no from tblProductPhoto",con1);
               SqlDataAdapter da=new SqlDataAdapter(cmd);
               DataSet ds=new DataSet();
               da.Fill(ds);
               SqlCommand cmd1=new SqlCommand("Insert into tblProductPhoto values(@Sl_no,@P_Code,@P_Name,@Image,@Image_url,@Flag1,@Flag2,@Flag3,@Flag4)",con1);
               if(ds.Tables[0].Rows[0].IsNull("Sl_no"))
               {

                   
                   cmd1.Parameters.AddWithValue("@Sl_no",slno);
                   cmd1.Parameters.AddWithValue("@P_Code",txtProd.Text);
                   cmd1.Parameters.AddWithValue("@P_Name",txtPname.Text);
                   cmd1.Parameters.AddWithValue("@Image",myimg);
                   cmd1.Parameters.AddWithValue("@Image_url",filename);
                   cmd1.Parameters.AddWithValue("@Flag1",'N');
                   cmd1.Parameters.AddWithValue("@Flag2",'N');
                   cmd1.Parameters.AddWithValue("@Flag3",'N');
                   cmd1.Parameters.AddWithValue("@Flag4",'N');
               }
               else
               {
                   slno=Convert.ToInt32(ds.Tables[0].Rows[0]["Sl_no"])+1;
                   cmd1.Parameters.AddWithValue("@Sl_no",slno);
                   cmd1.Parameters.AddWithValue("@P_Code",txtProd.Text);
                   cmd1.Parameters.AddWithValue("@P_Name",txtPname.Text);
                   cmd1.Parameters.AddWithValue("@Image",myimg);
                   cmd1.Parameters.AddWithValue("@Image_url",filename);
                   cmd1.Parameters.AddWithValue("@Flag1",'N');
                   cmd1.Parameters.AddWithValue("@Flag2",'N');
                   cmd1.Parameters.AddWithValue("@Flag3",'N');
                   cmd1.Parameters.AddWithValue("@Flag4",'N');
               }
               if(con1.State==ConnectionState.Closed)
               {
                   con1.Open();
               }
               cmd1.ExecuteNonQuery();
               }
          
       }

       
}