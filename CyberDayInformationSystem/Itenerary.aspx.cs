using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data;
using ClosedXML.Excel;

using System.Configuration;
using System.Data.SqlClient;

namespace CyberDayInformationSystem
{
    public partial class Itenerary : System.Web.UI.Page
    {

        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Session["TYPE"] != null)
            {
                MasterPageFile = (Session["Master"].ToString());
                if (Session["TYPE"].ToString() != "Coordinator")
                {
                    Session.Add("Redirected", 1);
                    Response.Redirect("BadSession.aspx");
                }
            }
            else
            {
                Session.Add("Redirected", 0);
                Response.Redirect("BadSession.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                
            }
        }

        protected Boolean docType()
        {
            if (Path.GetExtension(this.FileUpload1.FileName) == ".xlsx" || Path.GetExtension(this.FileUpload1.FileName) == ".xls")
            {
                Label1.Text = "";
                return true;
            }
            else
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "BAD FILE TYPE. Please submit an .xlsx or a .xls file type!";
                return false;
            }
        }

        protected void ImportExcel(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile && docType())
            {
                //Save the uploaded Excel file.
                string filePath = Server.MapPath("~/Uploads/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(filePath);

                //Open the Excel file using ClosedXML.
                using (XLWorkbook workBook = new XLWorkbook(filePath))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    //Create a new DataTable.
                    DataTable dt = new DataTable();

                    //Loop through the Worksheet rows.
                    bool firstRow = true;
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        //Use the first row to add columns to DataTable.
                        if (firstRow)
                        {
                            foreach (IXLCell cell in row.Cells())
                            {
                                dt.Columns.Add(cell.Value.ToString());
                            }
                            firstRow = false;
                        }
                        else
                        {
                            //Add rows to DataTable.
                            dt.Rows.Add();
                            int i = 0;
                            foreach (IXLCell cell in row.Cells())
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                i++;
                            }
                        }

                        GridView1.DataSource = dt;
                        GridView1.DataBind();

                        ViewState["table"] = dt;
                    }
                }
            }
        }

        protected void setTable(DataTable dt)
        {
            if (dt != null)
            {
                using (var bulkCopy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["INFO"].ConnectionString, SqlBulkCopyOptions.KeepIdentity))
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                    }

                    bulkCopy.BulkCopyTimeout = 600;
                    bulkCopy.DestinationTableName = "CurrentItenerary";
                    bulkCopy.WriteToServer(dt);
                    Label1.ForeColor = System.Drawing.Color.Green;
                    Label1.Text = "Itenerary Commited Successfully!";
                }
            }
            else
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "No Itenerary Uploaded! Please upload a file.";
            }
        }

        protected void clearTable()
        {
            String query1 = "delete from CurrentItenerary;";

            // define db connection 
            SqlConnection sqlConnect = new SqlConnection(ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString());

            // create the sql command object that will process our query
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnect;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = query1;

            // open the db connection and send query
            sqlConnect.Open();
            SqlDataReader queryResults = sqlCommand.ExecuteReader();

            sqlCommand.Dispose();
            sqlConnect.Close();
        }

        protected void ButtonCommit_Click(object sender, EventArgs e)
        {
            clearTable();
            setTable(ViewState["table"] as DataTable);
        }
    }
}