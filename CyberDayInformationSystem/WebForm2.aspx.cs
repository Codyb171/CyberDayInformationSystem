using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using ClosedXML.Excel;
using System.Data;

namespace CyberDayInformationSystem
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // lbl1.Text = Path.GetFileName("C:\\Users\\cloud\\source\\repos\\CyberDayInformationSystem\\CyberDayInformationSystem\\Uploads\\ ");
            // C: \Users\cloud\source\repos\CyberDayInformationSystem\CyberDayInformationSystem\Uploads\Default_Itinerary.xlsx
            //lbl1.Text = Server.MapPath("~/Uploads/");
            ImportExcel("12.12.2021_Itinerary.xlsx");//string parameter??

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String st = TextBox1.Text;
            remove_all_chars();
        }
        void remove_all_chars()
        {
            String dt = "05/05/1995";
           /* String.Join("", dt.Split('/'))*/;
            lbl1.Text = String.Join(".", dt.Split('/'));
        }

        protected void ImportExcel(String str) // binds table from a saved excel file.
        {
            {
                //Open the Excel file using ClosedXML.
                using (XLWorkbook workBook = new XLWorkbook(Server.MapPath("~/Uploads/" + str)))
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
                    }
                }
            }
        }

        protected Boolean docType()
        {
            if (Path.GetExtension(this.FileUpload1.FileName) == ".xlsx" || Path.GetExtension(this.FileUpload1.FileName) == ".xls")
            {
              //  Label1.Text = "";
                return true;
            }
            else
            {
                //Label1.ForeColor = System.Drawing.Color.Red;
                //Label1.Text = "BAD FILE TYPE. Please submit an .xlsx or a .xls file type!";
                return false;
            }
        }
    }
}