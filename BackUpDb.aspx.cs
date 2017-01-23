using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;
using System.Collections;
using System.Web.UI.HtmlControls;

public partial class BackUpDb : System.Web.UI.Page
{
    ClsBLLGeneraldetails clsBLLGeneral = new ClsBLLGeneraldetails();
    protected void Page_Load(object sender, EventArgs e)
    {
        DriveInfo[] drDrives = DriveInfo.GetDrives();
    }
    protected void btnBackUp_Click(object sender, EventArgs e)
    {

        FileInfo flDb = new FileInfo("C:/BackUp/Pharmacy.bak");
        if (Directory.Exists("C:/BackUp"))
        {
            SqlConnection _Con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            string strBackUpPath = "C:/BackUp/" + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            DirectoryInfo dirFinal = Directory.CreateDirectory(strBackUpPath);
            SqlCommand _Com = new SqlCommand("BACKUP DATABASE Pharmacy TO DISK='" + strBackUpPath + @"\" + "Pharmacy.bak" + "'", _Con);
            _Con.Open();
            _Com.ExecuteNonQuery();
            _Con.Close();
        }
        else
        {
            Directory.CreateDirectory("C:/BackUp");
            SqlConnection _Con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            DirectoryInfo dirFinalBackUp = Directory.CreateDirectory("C:/BackUp");
            DirectoryInfo dirFin = dirFinalBackUp.CreateSubdirectory(DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss"));
            SqlCommand _Com = new SqlCommand("BACKUP DATABASE Pharmacy TO DISK='" + dirFin + @"\" + "Pharmacy.bak" + "'", _Con);
            _Con.Open();
            _Com.ExecuteNonQuery();
            _Con.Close();
        }

        lblsuccess.Visible = true;
        lblsuccess.Text = "Back Up  Successfully";
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {

        //SqlConnection _Con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
        //SqlCommand _Com = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", _Con);
        //SqlDataAdapter _Oda = new SqlDataAdapter(_Com);
        //DataSet _Ds = new DataSet();
        //_Oda.Fill(_Ds);
        //for (int i = 0; i < _Ds.Tables[0].Rows.Count; i++)
        //{
        //    SqlCommand _ComColumns = new SqlCommand("SELECT * FROM ArecaProject.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'" + _Ds.Tables[0].Rows[i]["TABLE_NAME"].ToString() + "'", _Con);
        //    SqlDataAdapter _DaColumns = new SqlDataAdapter(_ComColumns);
        //    DataSet _DsColumns = new DataSet();
        //    _DaColumns.Fill(_DsColumns);
        //}
        //Response.Redirect("Home.aspx");
        //ArrayList arrColumns = new ArrayList();
        //ArrayList arrDataType = new ArrayList();
        //ArrayList arrDefaultValue = new ArrayList();
        //DataTable dtColumns = (DataTable)Session["tblColumns"];
        //for (int i = 0; i < dlAllTables.Items.Count; i++)
        //{
        //    Label lblTableName = (Label)dlAllTables.Items[i].FindControl("chkTblNames");
        //    for (int j = 0; j < dtColumns.Rows.Count; j++)
        //    {
        //        for (int k = 0; k < dtColumns.Columns.Count; k++)
        //        {
        //            string strColumnName = dtColumns.Rows[j][k].ToString();
        //            arrColumns.Add(strColumnName);
        //        }
        //    }
        //    bool check = clsBLLGeneral.VerifyAndDeploy(lblTableName.Text, arrColumns, arrDataType, arrDefaultValue);
        //}

        Response.Redirect("Home.aspx");
    }

   
    //protected void btnRestrOld_Click(object sender, EventArgs e)
    //{
    //    if (Directory.Exists(@"C:\BackUp"))
    //    {
    //        string[] dirFolders = Directory.GetDirectories(@"C:\BackUp");
    //        string strLastFolder = dirFolders[dirFolders.Count() - 1].ToString() + @"\" + "ArecaProject.bak";

    //        SqlConnection _Con = new SqlConnection("Data Source=localhost;Initial Catalog=master;User Id=sa;Password=vagi0903;Connect Timeout=20; Pooling=false;");
    //        //string strResotrePath = "C:/Restore/ArecaProject.bak";
    //        String sqlCommandText = @"USE MASTER;DROP DATABASE ArecaProject";
    //        SqlCommand _ComDrop = new SqlCommand(sqlCommandText, _Con);
    //        _Con.Open();
    //        _ComDrop.ExecuteNonQuery();
    //        _Con.Close();
    //        SqlCommand _Com = new SqlCommand("RESTORE DATABASE ArecaProject FROM DISK='" + strLastFolder + "'", _Con);
    //        _Con.Open();
    //        _Com.ExecuteNonQuery();
    //        _Con.Close();
    //    }
    //}
    //protected void btnRestrPure_Click(object sender, EventArgs e)
    //{
    //    SqlConnection _Con = new SqlConnection("Data Source=localhost;Initial Catalog=master;User Id=sa;Password=vagi0903;Connect Timeout=20; Pooling=false;");
    //    string strResotrePath = "C:/Restore/ArecaProject.bak";
    //    String sqlCommandText = @"USE MASTER;DROP DATABASE ArecaProject";
    //    SqlCommand _ComDrop = new SqlCommand(sqlCommandText, _Con);
    //    _Con.Open();
    //    _ComDrop.ExecuteNonQuery();
    //    _Con.Close();
    //    SqlCommand _Com = new SqlCommand("RESTORE DATABASE ArecaProject FROM DISK='" + strResotrePath + "'", _Con);
    //    _Con.Open();
    //    _Com.ExecuteNonQuery();
    //    _Con.Close();
    //}
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection _Con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
        //HtmlTable tblHtml = new HtmlTable();
        //tblHtml.Border = 1;
        //tblHtml.ID = "tblColumns";
        //tblHtml.Attributes.Add("runat", "server");
        //HtmlTableRow tblRow;
        //HtmlTableCell tblCell;
        SqlCommand _Com = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", _Con);
        SqlDataAdapter _Oda = new SqlDataAdapter(_Com);
        DataSet _Ds = new DataSet();
        _Oda.Fill(_Ds);
        dlAllTables.DataSource = _Ds;
        dlAllTables.DataBind();
        DataTable dtColum = new DataTable();
        DataRow drColum;
        for (int i = 0; i < dlAllTables.Items.Count; i++)
        {
            drColum = dtColum.NewRow();
            TableRow tblRow = new TableRow();
            Label lblTableName = (Label)dlAllTables.Items[i].FindControl("chkTblNames");
            SqlCommand _ComColumns = new SqlCommand("SELECT * FROM ArecaProject.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'" + lblTableName.Text + "'", _Con);
            SqlDataAdapter _DaColumns = new SqlDataAdapter(_ComColumns);
            DataSet _DsColumns = new DataSet();
            _DaColumns.Fill(_DsColumns);
            for (int j = 0; j < _DsColumns.Tables[0].Rows.Count; j++)
            {
                dtColum.Columns.Add("dt" + i + "/" + j);
                TableCell tblCell = new TableCell();
                Label lblColumn = new Label();
                lblColumn.Text = _DsColumns.Tables[0].Rows[j]["COLUMN_NAME"].ToString();
                lblColumn.Style.Add("font-size", "13px");
                tblCell.Controls.Add(lblColumn);
                tblCell.BorderStyle = BorderStyle.Dotted;
                tblCell.BorderWidth = 1;
                tblCell.BorderColor = System.Drawing.Color.Gray;
                tblRow.Cells.Add(tblCell);
                drColum["dt" + i + "/" + j] = _DsColumns.Tables[0].Rows[j]["COLUMN_NAME"].ToString();
                
            }
            dtColum.Rows.Add(drColum);
            tblColumns.Rows.Add(tblRow);
        }


        Session["tblColumns"] = dtColum;
        
    }
}