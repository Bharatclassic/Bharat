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

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Threading;

public partial class Loading : System.Web.UI.Page
{
    EncryptDecryptQueryString EncDycQyStr = new EncryptDecryptQueryString();
    protected void Page_Load(object sender, EventArgs e)
    {

        int intDirCount = Directory.GetFiles(Server.MapPath("~/DB/"), "*.sql").Length;

        if (intDirCount == 2)
        {
            FileInfo file = new FileInfo(Server.MapPath("~/DB/DatPubArecaProject.sql"));
            FileInfo filePrilims = new FileInfo(Server.MapPath("~/DB/InsertPrilims.sql"));
            FileInfo fileNewTables = new FileInfo(Server.MapPath("~/DB/NewTablesAreca.sql"));
            if (file.Exists)
            {
                string script = file.OpenText().ReadToEnd();
                string scriptPrilims = filePrilims.OpenText().ReadToEnd();
                SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
                Server server = new Server(new ServerConnection(conn));
                LoadingBody.Attributes.Add("style", "background:Images/ajax_loader.gif");
                Response.Write("<div id=\"loading\" style=\"position:absolute; width:100%; text-align:center; top:100px;\"><img src=\"images/ajaxloader.GIF\" border=0 width=100px height=100px><h1>Loading.....</h1></div>");
                Response.Flush();
                server.ConnectionContext.ExecuteNonQuery(script);
                server.ConnectionContext.ExecuteNonQuery(scriptPrilims);
                File.Create(Server.MapPath("~/DB/Dummy.sql"));
                Response.Write("<script>document.getElementById('loading').style.display='none';</script>");
                Response.Write("<script type='text/javascript'>");
                Response.Write("window.location = 'Index.aspx?Name=Loading Completed'</script>");
                Response.Flush();
            }
            else if (fileNewTables.Exists)
            {
                string scriptNewTables = fileNewTables.OpenText().ReadToEnd();
                SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionStrings"]);
                Server server = new Server(new ServerConnection(conn));
                LoadingBody.Attributes.Add("style", "background:Images/ajax_loader.gif");
                Response.Write("<div id=\"loading\" style=\"position:absolute; width:100%; text-align:center; top:100px;\"><img src=\"images/ajaxloader.GIF\" border=0 width=100px height=100px><h1>Loading.....</h1></div>");
                Response.Flush();
                server.ConnectionContext.ExecuteNonQuery(scriptNewTables);
                File.Create(Server.MapPath("~/DB/Dummy.sql"));
                Response.Write("<script>document.getElementById('loading').style.display='none';</script>");
                Response.Write("<script type='text/javascript'>");
                Response.Write("window.location = 'Index.aspx?Name=Loading Completed'</script>");
                Response.Flush();
            }
        }
        else
        {
            Response.Write("<script>document.getElementById('loading').style.display='none';</script>");
            Response.Write("<script type='text/javascript'>");
            Response.Write("window.location = 'Index.aspx?Name=" + EncryptQueryString("Loading Completed") + "'</script>");
            Response.Flush();
        }
    }
    public string EncryptQueryString(string strQueryString)
    {
        EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
        string strEncryptedQueryString = objEDQueryString.Encrypt(strQueryString, "r0b1nr0y");
        return objEDQueryString.Encrypt(strQueryString, "r0b1nr0y");
    }
}