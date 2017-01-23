using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.IO;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

public partial class Backuprestore : System.Web.UI.Page
{
    Dbconn dbcon = new Dbconn();
    protected static string filename = Dbconn.Mymenthod();
    protected static string strconn11 = Dbconn.conmenthod();
    protected void Page_Load(object sender, EventArgs e)
    {
         if (!Page.IsPostBack)
        {
            FillDatabases();
            // ReadBackupFiles();
        }

    }

    private void FillDatabases()
    {
        try
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = strconn11;
            sqlConnection.Open();
            string sqlQuery = "SELECT * FROM sys.databases";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlCommand.CommandType = CommandType.Text;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            ddlDatabases.DataSource = dataSet.Tables[0];
            ddlDatabases.DataTextField = "name";
            ddlDatabases.DataValueField = "database_id";
            ddlDatabases.DataBind();
        }
        catch (SqlException sqlException)
        {
            lblsuccess.Text = sqlException.Message.ToString();
        }
        catch (Exception exception)
        {
            lblsuccess.Text = exception.Message.ToString();
        }

        ddlDatabases.Items.Insert(0, new ListItem("--Select Database--", "0"));
    }


     protected void btnBackup_Click(object sender, EventArgs e)
    {
          string _DatabaseName = ddlDatabases.SelectedItem.Text.ToString();
          BackupSqlDatabase("_DatabaseName", "Pharmacy", "Vagi@0903", "localhost", "C:\\YourDataBaseName.bak");
    }

      public void BackupSqlDatabase(String databaseName, String userName, String password, String serverName, String destinationPath)
    {
        Backup sqlBackup = new Backup();

        sqlBackup.Action = BackupActionType.Database;
        sqlBackup.BackupSetDescription = "ArchiveDataBase:" +
                                         DateTime.Now.ToShortDateString();
        sqlBackup.BackupSetName = "Archive";

        sqlBackup.Database = databaseName;

        BackupDeviceItem deviceItem = new BackupDeviceItem(destinationPath, DeviceType.File);
        ServerConnection connection = new ServerConnection(serverName, userName, password);
        Server sqlServer = new Server(connection);

        Database db = sqlServer.Databases[databaseName];

        sqlBackup.Initialize = true;
        sqlBackup.Checksum = true;
        sqlBackup.ContinueAfterError = true;

        sqlBackup.Devices.Add(deviceItem);
        sqlBackup.Incremental = false;

        sqlBackup.ExpirationDate = DateTime.Now.AddDays(3);
        sqlBackup.LogTruncation = BackupTruncateLogType.Truncate;

        sqlBackup.FormatMedia = false;

        sqlBackup.SqlBackup(sqlServer);
    }


       protected void btnexit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    

    
}