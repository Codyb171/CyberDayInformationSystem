using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using ClosedXML.Excel;


namespace CyberDayInformationSystem
{
    public partial class EventAdministration : Page
    {
        void Page_PreInit(Object sender, EventArgs e)
        {
            //if (Session["TYPE"] != null)
            //{
            //    MasterPageFile = (Session["Master"].ToString());
            //    if (Session["TYPE"].ToString() != "Coordinator")
            //    {
            //        Session.Add("Redirected", 1);
            //        Response.Redirect("BadSession.aspx");
            //    }
            //}
            //else
            //{
            //    Session.Add("Redirected", 0);
            //    Response.Redirect("BadSession.aspx");
            //}
        }

        private int _eventToModify;
        protected void Page_Load(object sender, EventArgs e)
        {
            SelectedViewMode.ActiveViewIndex = 0;

            if (Session["EDITEVENTID"] != null)
            {
                _eventToModify = int.Parse(Session["EDITEVENTID"].ToString());
            }
        }

        protected void FunctionSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EventFunctionSelection.SelectedValue == "1") //create event
            {
                NotifLBL.Text = String.Empty;
                SelectedFunction.ActiveViewIndex = 0;
            }
            if (EventFunctionSelection.SelectedValue == "2") //mod event
            {
                NotifLBL.Text = String.Empty;
                SelectedFunction.ActiveViewIndex = 1;
                EventDateList();
            }
            if (EventFunctionSelection.SelectedValue == "3") //delete event
            {
                NotifLBL.Text = String.Empty;
                SelectedFunction.ActiveViewIndex = 2;
                EventDateList();
            }
        }

        public void EventDateList()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string command = "SELECT EVENTDATE, EVENTID FROM EVENT";
            connection.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            EventDateDDL.DataSource = dt;
            EventDateDDL.DataBind();
            EventDateDDL.DataTextField = "EVENTDATE";
            EventDateDDL.DataValueField = "EVENTID";
            EventDateDDL.DataBind();
            EventDateDDL.Items.Insert(0, new ListItem(String.Empty));
            EventDelDDL.DataSource = dt;
            EventDelDDL.DataBind();
            EventDelDDL.DataTextField = "EVENTDATE";
            EventDelDDL.DataValueField = "EVENTID";
            EventDelDDL.DataBind();
            EventDelDDL.Items.Insert(0, new ListItem(String.Empty));

            connection.Close();
        }

        public void OldTimeDisplay() //OriginalDateDisplay
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand select = new SqlCommand("SELECT EVENTDATE FROM EVENT WHERE EVENTID = @VALUE", connection);

            if (EventFunctionSelection.SelectedValue == "2")
            {
                int value = int.Parse(EventDateDDL.SelectedValue);
                connection.Open();
                select.Parameters.AddWithValue("@VALUE", value);
                SqlDataReader reader = select.ExecuteReader();
                while (reader.Read())
                {
                    //fix
                    NewDateTxt.Text = (reader["EVENTDATE"].ToString());
                }
                connection.Close();
            }   
        }

        public void ClearInfo()
        {
            if (EventFunctionSelection.SelectedValue == "1")
            {
                EventDateDDL.ClearSelection();
            }
            if (EventFunctionSelection.SelectedValue == "2")
            {
                EventDateDDL.ClearSelection();
            }
            if (EventFunctionSelection.SelectedValue == "3")
            {
                EventDelDDL.ClearSelection();
            }
        }

        protected void CreateBut_Click(object sender, EventArgs e) // reformat code. looks gross...
        {//fix
            if (Page.IsValid)
            {
                {
                    string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                    SqlConnection connection = new SqlConnection(cs);
                    SqlCommand insert = new SqlCommand("INSERT INTO EVENTITINERARY(FILENAME) VALUES(@FILENAME)", connection);

                    if (ViewState["FILENAME"] != null)
                    {
                        string fileName = HttpUtility.HtmlEncode(ViewState["FILENAME"].ToString());
                        connection.Open();
                        insert.Parameters.AddWithValue("@FILENAME", fileName);
                        insert.ExecuteNonQuery();
                        connection.Close();
                    }
                    //string fileName = HttpUtility.HtmlEncode(ViewState["FILENAME"].ToString());
                    //connection.Open();
                    //insert.Parameters.AddWithValue("@FILENAME", fileName);
                    //insert.ExecuteNonQuery();
                    //connection.Close();
                }

                getItineraryID();

                {
                    if (ViewState["ITINERARYID"] != null)
                    {
                        string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                        SqlConnection connection = new SqlConnection(cs);
                        SqlCommand insert = new SqlCommand("INSERT INTO EVENT(EVENTDATE, EVENTITINERARY) VALUES(@DATE, @ITINERARY)", connection);

                        //string date = HttpUtility.HtmlEncode(EventDateTxt.Text);
                        //date = date.ToString("dd/MM/yyyy HH:mm");
                        string date = String.Format("{0:d/M/yyyy HH:mm}", HttpUtility.HtmlEncode(EventDateTxt.Text));

                        /*String.Format("{0:d/M/yyyy HH:mm}", HttpUtility.HtmlEncode(EventDateTxt.Text))*/
                        ;
                        int itinerary = Int32.Parse(ViewState["ITINERARYID"].ToString());
                        connection.Open();
                        insert.Parameters.AddWithValue("@DATE", date);
                        insert.Parameters.AddWithValue("@ITINERARY", itinerary);
                        insert.ExecuteNonQuery();
                        connection.Close();

                        NotifLBL.ForeColor = System.Drawing.Color.Green;
                        NotifLBL.Text = "Your event on " + date + " has successfully been created!";
                        NotifLBL.Visible = true;
                        ClearInfo();
                    }

                }
            }
        }

        protected void EventDateDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EventDateDDL.SelectedIndex != 0)
            {
                ModifyTbl.Visible = true;
                ModifyBut.Visible = true;
                ImportExcelModify(Int32.Parse(EventDateDDL.SelectedValue));
                OldTimeDisplay();
            }
        }

        protected void ModifyBut_Click(object sender, EventArgs e)
        {
            // get/set new itinerary
            ImportExcelModify();

            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand update = new SqlCommand("UPDATE EVENT SET EVENTDATE = @DATE, EVENTITINERARY = @ITINERARYID WHERE EVENTID = @VALUE", connection);

            int value = int.Parse(EventDateDDL.SelectedValue);
            //string date = HttpUtility.HtmlEncode(NewDateTxt.Text); // protect this from bad formatting!!!
            string date = String.Format("{0:d/M/yyyy HH:mm}", HttpUtility.HtmlEncode(NewDateTxt.Text)); // protect this from bad formatting!!!

            String.Format("{0:d/M/yyyy HH:mm}", HttpUtility.HtmlEncode(EventDateTxt.Text));

            // string itineraryID = HttpUtility.HtmlEncode(NewDateTxt.Text);
            string itineraryID = date;


            connection.Open();
            update.Parameters.AddWithValue("@DATE", date);
            update.Parameters.AddWithValue("@ITINERARYID", getItineraryIDFromModDDL(value));
            update.Parameters.AddWithValue("@VALUE", value);
            update.ExecuteNonQuery();
            connection.Close();

            updateFileName(getItineraryIDFromModDDL(value));

            NotifLBL.ForeColor = System.Drawing.Color.Green;
            NotifLBL.Text = "Your event has successfully been modified!";
            NotifLBL.Visible = true;
            ClearInfo();
        }

        protected void updateFileName(int id)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand update = new SqlCommand("UPDATE EVENTITINERARY SET FILENAME = @FILENAME " +
                "WHERE ITINERARYID = @ID", connection);

            connection.Open();
            update.Parameters.AddWithValue("@FILENAME", NewDateTxt.Text + ".xlsx");
            update.Parameters.AddWithValue("@ID", id);
            update.ExecuteNonQuery();
            connection.Close();
        }

        protected void DelBut_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand query = new SqlCommand("select itineraryid from eventitinerary where itineraryid = (select EVENTITINERARY from event where eventid = " 
                + int.Parse(EventDelDDL.SelectedValue) + ")", connection);
            connection.Open();
            SqlDataReader queryResults = query.ExecuteReader();

            if (queryResults.Read())
            {
                SqlCommand delete1 = new SqlCommand("DELETE FROM EVENT WHERE EVENTID = @EVENTID", connection);
                SqlCommand delete2 = new SqlCommand("DELETE FROM EVENTITINERARY WHERE ITINERARYID = @ITINERARYID", connection);

                string select = "SELECT EVENTDATE, EVENTID FROM EVENT";

                int itineraryID = queryResults.GetInt32(queryResults.GetOrdinal("ITINERARYID"));
                int eventID = int.Parse(EventDelDDL.SelectedValue);

                queryResults.Close();

                delete1.Parameters.AddWithValue("@EVENTID", eventID);
                delete1.ExecuteNonQuery();
                delete2.Parameters.AddWithValue("@ITINERARYID", itineraryID);
                delete2.ExecuteNonQuery();

                SqlDataAdapter adpt = new SqlDataAdapter(select, connection);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                EventDelDDL.DataSource = dt;
                EventDelDDL.DataBind();
                EventDelDDL.DataTextField = "EVENTDATE";
                EventDelDDL.DataValueField = "EVENTID";
                EventDelDDL.DataBind();
                EventDelDDL.Items.Insert(0, "");
                connection.Close();

                ClearInfo();
                NotifLBL.ForeColor = System.Drawing.Color.Green;
                NotifLBL.Text = "Your event has successfully been deleted!"; // not showing up...
                NotifLBL.Visible = true;
            }
            queryResults.Close();
            connection.Close();
        }

        protected void ImportExcel(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile && docType())
            {
                ViewState["FILENAME"] = String.Join(".", String.Format("{0:d/M/yyyy HH:mm}", HttpUtility.HtmlEncode(EventDateTxt.Text))) + "_Itinerary" + Path.GetExtension(FileUpload1.PostedFile.FileName);

                bug.Text= String.Format("{0:d/M/yyyy HH:mm}", HttpUtility.HtmlEncode(NewDateTxt.Text));

                //Save the uploaded Excel file with a dynamically created name; Overwrites if there is already the same name there.
                string filePath = Server.MapPath("~/Uploads/") + ViewState["FILENAME"];
                FileUpload1.SaveAs(filePath);

                //Open the Excel file using ClosedXML.
                using (XLWorkbook workBook = new XLWorkbook(filePath))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);
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

        protected void ImportExcelModify() // saves a new excel on modification
        {
            if (FileUpload2.HasFile && docType())
            { //fix
                ViewState["MODIFYFILENAME"] = String.Join(".", EventDateTxt.Text.Split('/')) + "_Itinerary" + Path.GetExtension(FileUpload2.PostedFile.FileName);
                ViewState["MODIFYFILENAME"] = NewDateTxt.Text;

                //Save the uploaded Excel file with a dynamically created name; Overwrites if there is already the same name there.
                string filePath = Server.MapPath("~/Uploads/") + ViewState["MODIFYFILENAME"];
                FileUpload2.SaveAs(filePath);

                //Open the Excel file using ClosedXML.
                using (XLWorkbook workBook = new XLWorkbook(filePath))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);
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
                        GridViewModify.DataSource = dt;
                        GridViewModify.DataBind();
                    }
                }
            }
            else //fix if no file uploaded but date is changed, rename and save the file
            { // get the correct file name... then resave the excel
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                SqlConnection connection = new SqlConnection(cs);
                SqlCommand query = new SqlCommand("SELECT FILENAME FROM EVENTITINERARY WHERE ITINERARYID = (SELECT EVENTITINERARY FROM EVENT WHERE EVENTID = " + 
                    Int32.Parse(EventDateDDL.SelectedValue.ToString()) +")", connection);
                //string fileName = HttpUtility.HtmlEncode(ViewState["FILENAME"].ToString());
                connection.Open();
               // query.Parameters.AddWithValue("@FILENAME", fileName);
                SqlDataReader queryResults = query.ExecuteReader();

                if (queryResults.Read()) //If a filename is found, update it to the new name.
                {
                    // Renames the file with the updated Date.
                    var sourcePath = Server.MapPath("~/Uploads/") + queryResults.GetString(queryResults.GetOrdinal("FILENAME"));
                    var newName = NewDateTxt.Text + ".xlsx"; // could split the query result to better determine the file extension.
                    var directory = Path.GetDirectoryName(sourcePath);
                    var destinationPath = Path.Combine(directory, newName);
                    File.Move(sourcePath, destinationPath);
                    ViewState["MODIFYFILENAME"] = NewDateTxt.Text;
                }
                queryResults.Close();
                connection.Close();
            }
        }

        protected Boolean docType()
        {
            if (Path.GetExtension(this.FileUpload1.FileName) == ".xlsx" || Path.GetExtension(this.FileUpload1.FileName) == ".xls")
            {
                return true;
            }
            else if (Path.GetExtension(this.FileUpload2.FileName) == ".xlsx" || Path.GetExtension(this.FileUpload2.FileName) == ".xls")
            {
                return true;
            }
            else
            {
                NotifLBL.ForeColor = System.Drawing.Color.Red;
                NotifLBL.Text = "BAD FILE TYPE. Please submit an .xlsx or a .xls file type!";
                return false;
            }
        }
        protected void setTable(DataTable dt)  // use a dataview to filter out empty rows??
        {
            if (dt != null)
            {
                dt = RemoveEmptyRows(dt);
            }
            else
            {
                NotifLBL.ForeColor = System.Drawing.Color.Red;
                NotifLBL.Text = "No Itinerary Uploaded! Please upload a file.";
            }
        }

        private static DataTable RemoveEmptyRows(DataTable source)
        {
            DataTable dt1 = source.Clone(); //copy the structure 
            for (int i = 0; i <= source.Rows.Count - 1; i++) //iterate through the rows of the source
            {
                DataRow currentRow = source.Rows[i];  //copy the current row 
                foreach (var colValue in currentRow.ItemArray)//move along the columns 
                {
                    if (!string.IsNullOrEmpty(colValue.ToString())) // if there is a value in a column, copy the row and finish
                    {
                        dt1.ImportRow(currentRow);
                        break; //break and get a new row                        
                    }
                }
            }
            return dt1;
        }

        protected int getItineraryID()
        {
            if (ViewState["FILENAME"] != null)
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                SqlConnection connection = new SqlConnection(cs);
                SqlCommand query = new SqlCommand("SELECT ITINERARYID FROM EVENTITINERARY WHERE FILENAME = '" + ViewState["FILENAME"].ToString() + "'", connection);
                string fileName = HttpUtility.HtmlEncode(ViewState["FILENAME"].ToString());
                connection.Open();
                query.Parameters.AddWithValue("@FILENAME", fileName);
                SqlDataReader queryResults = query.ExecuteReader();

                if (queryResults.Read())
                {
                    ViewState["ITINERARYID"] = queryResults.GetInt32(queryResults.GetOrdinal("ITINERARYID"));
                }

                queryResults.Close();
                connection.Close();

                return Int32.Parse(ViewState["ITINERARYID"].ToString());
            }
            else return 0;
        }

        protected void ImportExcelModify(int id) // binds table from a saved excel file.
        {
            // Gets the filename for the selected value.
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand query = new SqlCommand("SELECT FILENAME FROM EVENTITINERARY WHERE ITINERARYID = " +
                "(SELECT EVENTITINERARY FROM EVENT WHERE EVENTID = " + id +")", connection);
            connection.Open();
            SqlDataReader queryResults = query.ExecuteReader();

            if (queryResults.Read())
            {
                ViewState["MODIFYFILENAME"] = queryResults.GetString(queryResults.GetOrdinal("FILENAME"));
            }

            queryResults.Close();
            connection.Close();

                //Open the Excel file using ClosedXML.
                using (XLWorkbook workBook = new XLWorkbook(Server.MapPath("~/Uploads/" + ViewState["MODIFYFILENAME"].ToString())))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);
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
                        GridViewModify.DataSource = dt;
                        GridViewModify.DataBind();
                    }
                }
        }

        protected int getItineraryIDFromModDDL(int value)
        {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                SqlConnection connection = new SqlConnection(cs);
                SqlCommand query = new SqlCommand("SELECT EVENTITINERARY FROM EVENT WHERE EVENTID = " + value, connection);
                connection.Open();
                SqlDataReader queryResults = query.ExecuteReader();

                if (queryResults.Read())
                {
                    ViewState["MODIFYITINERARYID"] = queryResults.GetInt32(queryResults.GetOrdinal("EVENTITINERARY"));
                }

                queryResults.Close();
                connection.Close();

            return Int32.Parse(ViewState["MODIFYITINERARYID"].ToString());
        }
    }
}
