<%@ Application Language="C#" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.IO" %>



<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }
    //ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
    ClsBLLGeneraldetails clsBLLGeneral = new ClsBLLGeneraldetails();
    protected static string strconn1 = Dbconn.conmenthod();
    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        ClsBLLGeneraldetails ClsBLGD = new ClsBLLGeneraldetails();
        Dbconn dbcon = new Dbconn();
        SqlConnection con1 = new SqlConnection(strconn1);
        con1.Open();
        SqlCommand cmd1 = new SqlCommand("delete FROM tbltempprodsale where LoginName = '" + Session["username"].ToString() + "'", con1);
        cmd1.ExecuteNonQuery();

       
       
        //  protected  static string strconn11 = Dbconn.conmenthod();
       // clsBLLGeneral.UpdateRecords("tblLogin", "LoggedIn='N'", "UserName='" + Session["username"].ToString() + "'");

    }
       
</script>
