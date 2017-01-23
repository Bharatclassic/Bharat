using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LogOut : System.Web.UI.Page
{
    ClsBLLGeneraldetails clsBLLGeneral = new ClsBLLGeneraldetails();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] != null)
        {
            clsBLLGeneral.UpdateRecords("tblLogin", "LoggedIn='N'", "UserName='" + Session["username"].ToString() + "'");
            Session.Abandon();
            Response.Redirect("Index.aspx");
        }
        else
        {
            Session.Abandon();
            Response.Redirect("Index.aspx");
        }
    }
}