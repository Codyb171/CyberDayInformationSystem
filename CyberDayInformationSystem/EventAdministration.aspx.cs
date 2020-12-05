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
        private int _eventToModify;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EDITEVENTID"] != null)
            {
                _eventToModify = int.Parse(Session["EDITEVENTID"].ToString());
            }

            if (!Page.IsPostBack)
            {
                SchoolList();
                EventorTask.Items.Insert(0, new ListItem(String.Empty));
                TaskFunctionSelection.Items.Insert(0, new ListItem(String.Empty));
                EventFunctionSelection.Items.Insert(0, new ListItem(String.Empty));
            }
        }

        protected void EventorTask_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (EventorTask.SelectedValue == "1")
            {
                SelectedViewMode.ActiveViewIndex = 0;
            }

            if (EventorTask.SelectedValue == "2")
            {
                SelectedViewMode.ActiveViewIndex = 1;
            }
        }

        protected void FunctionSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaskMultiView.ActiveViewIndex = -1;

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

        protected void TaskFunctionSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedFunction.ActiveViewIndex = -1;

            if (TaskFunctionSelection.SelectedValue == "1") //create task
            {
                NotifLBL.Text = String.Empty;
                TaskMultiView.ActiveViewIndex = 0;
                EventDateList();
            }

            if (TaskFunctionSelection.SelectedValue == "2") //mod task
            {
                NotifLBL.Text = String.Empty;
                TaskMultiView.ActiveViewIndex = 1;
                TaskList();
            }

            if (TaskFunctionSelection.SelectedValue == "3") //delete task
            {
                NotifLBL.Text = String.Empty;
                TaskMultiView.ActiveViewIndex = 2;
                TaskList();
            }
        }

        private void SchoolList()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string command = "select S.SCHOOLID, S.NAME from School s";
            connection.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            SchoolDropDown.DataSource = dt;
            SchoolDropDown.DataBind();
            SchoolDropDown.DataTextField = "Name";
            SchoolDropDown.DataValueField = "SCHOOLID";
            SchoolDropDown.DataBind();
            SchoolDropDown.Items.Insert(0, new ListItem(String.Empty));
            NewSchoolDDL.DataSource = dt;
            NewSchoolDDL.DataTextField = "Name";
            NewSchoolDDL.DataValueField = "SCHOOLID";
            NewSchoolDDL.DataBind();
            NewSchoolDDL.Items.Insert(0, new ListItem(String.Empty));
            EXSchoolDDL.DataSource = dt;
            EXSchoolDDL.DataTextField = "NAME";
            EXSchoolDDL.DataValueField = "SCHOOLID";
            EXSchoolDDL.DataBind();
            connection.Close();
        }

        public void EventDateList()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string command = "SELECT EVENTTITLE, EVENTID FROM EVENT";
            connection.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            EventDateDDL.DataSource = dt;
            EventDateDDL.DataBind();
            EventDateDDL.DataTextField = "EVENTTITLE";
            EventDateDDL.DataValueField = "EVENTID";
            EventDateDDL.DataBind();
            EventDateDDL.Items.Insert(0, new ListItem(String.Empty));
            EventDelDDL.DataSource = dt;
            EventDelDDL.DataBind();
            EventDelDDL.DataTextField = "EVENTTITLE";
            EventDelDDL.DataValueField = "EVENTID";
            EventDelDDL.DataBind();
            EventDelDDL.Items.Insert(0, new ListItem(String.Empty));
            CreateEventTaskDDL.DataSource = dt;
            CreateEventTaskDDL.DataBind();
            CreateEventTaskDDL.DataTextField = "EVENTTITLE";
            CreateEventTaskDDL.DataValueField = "EVENTID";
            CreateEventTaskDDL.DataBind();
            CreateEventTaskDDL.Items.Insert(0, new ListItem(String.Empty));
            connection.Close();
        }

        public void TaskList()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string command =
                "SELECT (EVENTTASKS.TITLE + ' - ' + EVENT.EVENTDATE) AS eventtask, (EVENTTASKS.TASKID) AS TaskID FROM EVENTTASKS, EVENT;";
            connection.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ModTaskDDL.DataSource = dt;
            ModTaskDDL.DataBind();
            ModTaskDDL.DataTextField = "eventtask";
            ModTaskDDL.DataValueField = "TaskID";
            ModTaskDDL.DataBind();
            ModTaskDDL.Items.Insert(0, new ListItem(String.Empty));
            TaskDelDDL.DataSource = dt;
            TaskDelDDL.DataBind();
            TaskDelDDL.DataTextField = "eventtask";
            TaskDelDDL.DataValueField = "TaskID";
            TaskDelDDL.DataBind();
            TaskDelDDL.Items.Insert(0, new ListItem(String.Empty));
            connection.Close();
        }

        public void OldDataDisplay()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand select =
                new SqlCommand("SELECT EVENTTITLE, EVENTDATE, STARTTIME, ENDTIME FROM EVENT WHERE EVENTID = @VALUE",
                    connection);
            SqlCommand select2 =
                new SqlCommand("SELECT TITLE, STARTTIME, ENDTIME FROM EVENTTASKS WHERE TASKID = @TASKVALUE",
                    connection);

            if (EventFunctionSelection.SelectedValue == "2")
            {
                int value = int.Parse(EventDateDDL.SelectedValue);
                connection.Open();
                string title = "CyberDay-";
                select.Parameters.AddWithValue("@VALUE", value);
                SqlDataReader reader = select.ExecuteReader();
                while (reader.Read())
                {
                    title = reader["EVENTTITLE"].ToString();
                    NewDateTxt.Text = reader["EVENTDATE"].ToString();
                    NewStartTxt.Text = (reader["STARTTIME"].ToString().Insert(2,":"));
                    NewEndTxt.Text = (reader["ENDTIME"].ToString().Insert(2, ":"));
                }

                connection.Close();
                if (title.Length > 10)
                {
                    string school = title.Substring(11);
                    NewSchoolDDL.SelectedValue = school;
                }
            }

            if (TaskFunctionSelection.SelectedValue == "2")
            {
                int value = int.Parse(ModTaskDDL.SelectedValue);
                connection.Open();
                select2.Parameters.AddWithValue("@TASKVALUE", value);
                SqlDataReader reader = select2.ExecuteReader();
                while (reader.Read())
                {
                    ModTaskNewTitle.Text = reader["TITLE"].ToString();
                    TaskNewStart.Text = reader["STARTTIME"].ToString().Insert(2, ":");
                    TaskNewEnd.Text = reader["ENDTIME"].ToString().Insert(2, ":");
                }

                connection.Close();
            }
        }

        public void ClearInfo()
        {
            if (EventFunctionSelection.SelectedValue == "1")
            {
                EventDateDDL.ClearSelection();
                EventTimeTxt.Text = String.Empty;
                EndTimeTxt.Text = String.Empty;
            }

            if (EventFunctionSelection.SelectedValue == "2")
            {
                EventDateDDL.ClearSelection();
            }

            if (EventFunctionSelection.SelectedValue == "3")
            {
                EventDelDDL.ClearSelection();
            }

            if (TaskFunctionSelection.SelectedValue == "1")
            {
                CreateEventTaskDDL.ClearSelection();
                TaskTitleTxt.Text = String.Empty;
                TaskStartTimeTxt.Text = String.Empty;
                TaskEndTimeTxt.Text = String.Empty;
            }

            if (TaskFunctionSelection.SelectedValue == "2")
            {
                ModTaskDDL.ClearSelection();
            }

            if (TaskFunctionSelection.SelectedValue == "3")
            {
                TaskDelDDL.ClearSelection();
            }
        }

        protected void CreateBut_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (CheckEvent() == 0)
                {
                    string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                    SqlConnection connection = new SqlConnection(cs);
                    SqlCommand insert =
                        new SqlCommand(
                            "INSERT INTO EVENT(EVENTNAME, EVENTDATE, STARTTIME, ENDTIME) VALUES(@NAME, @DATE, @STARTTIME, @ENDTIME)",
                            connection);
                    string school = SchoolDropDown.SelectedItem.Text;
                    string start = HttpUtility.HtmlEncode(EventTimeTxt.Text);
                    string end = HttpUtility.HtmlEncode(EndTimeTxt.Text);
                    string date = HttpUtility.HtmlEncode(EventDateTxt.Text);
                    connection.Open();
                    insert.Parameters.AddWithValue("@NAME", "CyberDay- " + school);
                    insert.Parameters.AddWithValue("@DATE", date);
                    insert.Parameters.AddWithValue("@STARTTIME", start);
                    insert.Parameters.AddWithValue("@ENDTIME", end);
                    insert.ExecuteNonQuery();
                    connection.Close();

                    NotifLBL.Text = "Your event has successfully been created!";
                    NotifLBL.Visible = true;
                    ClearInfo();
                }
            }
        }

        protected void CreateTaskBut_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (CheckTask() == 0)
                {
                    string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                    SqlConnection connection = new SqlConnection(cs);
                    SqlCommand insert =
                        new SqlCommand(
                            "INSERT INTO EVENTTASKS(TITLE, STARTTIME, ENDTIME) VALUES(@TITLE, @STARTTIME, @ENDTIME)",
                            connection);
                    SqlCommand insert2 =
                        new SqlCommand("INSERT INTO EVENTITINERARY VALUES(@EVENT, @TASK)", connection);
                    SqlCommand select =
                        new SqlCommand(
                            "SELECT TASKID FROM EVENTTASKS WHERE TITLE = @TITLE AND STARTTIME = @STARTTIME AND ENDTIME = @ENDTIME",
                            connection);

                    string title = HttpUtility.HtmlEncode(TaskTitleTxt.Text);
                    string start = HttpUtility.HtmlEncode(TaskStartTimeTxt.Text);
                    string end = HttpUtility.HtmlEncode(TaskEndTimeTxt.Text);
                    int eventKey = int.Parse(CreateEventTaskDDL.SelectedValue);
                    int taskKey;
                    start.Replace(":", "");
                    end.Replace(":", "");
                    connection.Open();
                    insert.Parameters.AddWithValue("@TITLE", title);
                    insert.Parameters.AddWithValue("@STARTTIME", start);
                    insert.Parameters.AddWithValue("@ENDTIME", end);
                    insert.ExecuteNonQuery();
                    select.Parameters.AddWithValue("@TITLE", title);
                    select.Parameters.AddWithValue("@STARTTIME", start);
                    select.Parameters.AddWithValue("@ENDTIME", end);
                    taskKey = (int)select.ExecuteScalar();
                    insert2.Parameters.AddWithValue("@EVENT", eventKey);
                    insert2.Parameters.AddWithValue("@TASK", taskKey);
                    insert2.ExecuteNonQuery();
                    connection.Close();

                    NotifLBL.Text = "Your task has successfully been created!";
                    NotifLBL.Visible = true;
                    ClearInfo();
                }
            }
        }

        public int CheckEvent()
        {
            int add;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand check =
                new SqlCommand(
                    "Select count(*) from EVENT where EVENTTITLE = @TITLE AND EVENTDATE = @DATE AND STARTTIME = @STARTTIME AND ENDTIME = @ENDTIME",
                    connection);

            string start = HttpUtility.HtmlEncode(EventTimeTxt.Text);
            string end = HttpUtility.HtmlEncode(EndTimeTxt.Text);
            string date = HttpUtility.HtmlEncode(EventDateTxt.Text);
            start.Replace(":", "");
            end.Replace(":", "");
            connection.Open();
            check.Parameters.AddWithValue("@DATE", date);
            check.Parameters.AddWithValue("@STARTTIME", start);
            check.Parameters.AddWithValue("@ENDTIME", end);
            add = (int)check.ExecuteScalar();
            connection.Close();
            if (add > 0)
            {
                NotifLBL.Text = "An event already exists with this data";
                NotifLBL.Visible = true;
            }

            return add;
        }

        public int CheckTask()
        {
            int add;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand check =
                new SqlCommand(
                    "Select count(*) from EVENTTASKS where TITLE = @TITLE AND STARTTIME = @STARTTIME AND ENDTIME = @ENDTIME",
                    connection);

            string title = HttpUtility.HtmlEncode(TaskTitleTxt.Text);
            string start = HttpUtility.HtmlEncode(TaskStartTimeTxt.Text);
            string end = HttpUtility.HtmlEncode(TaskEndTimeTxt.Text);
            start.Replace(":", "");
            end.Replace(":", "");
            connection.Open();
            check.Parameters.AddWithValue("@TITLE", title);
            check.Parameters.AddWithValue("@STARTTIME", start);
            check.Parameters.AddWithValue("@ENDTIME", end);
            add = (int)check.ExecuteScalar();
            connection.Close();
            if (add > 0)
            {
                NotifLBL.Text = "A task already exists with this data";
                NotifLBL.Visible = true;
            }

            return add;
        }

        protected void EventDateDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EventDateDDL.SelectedIndex != 0)
            {
                ModifyTbl.Visible = true;
                ModifyBut.Visible = true;

                OldDataDisplay();
            }
        }

        protected void ModTaskDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModTaskDDL.SelectedIndex != 0)
            {
                ModTaskTbl.Visible = true;
                ModTaskBut.Visible = true;
                ModTaskNewTitle.Text = ModTaskDDL.SelectedItem.ToString();
                OldDataDisplay();
            }
        }

        protected void ModifyBut_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand update =
                new SqlCommand(
                    "UPDATE FROM EVENT SET EVENTTITLE = @TITLE, EVENTDATE = @DATE, STARTTIME = @STARTTIME, ENDTIME = @ENDTIME WHERE EVENTID = @VALUE",
                    connection);

            int value = int.Parse(EventDateDDL.SelectedValue);
            string school = NewSchoolDDL.SelectedItem.Text;
            string date = HttpUtility.HtmlEncode(NewDateTxt.Text);
            string start = HttpUtility.HtmlEncode(NewStartTxt.Text);
            string end = HttpUtility.HtmlEncode(NewEndTxt.Text);
            start.Replace(":", "");
            end.Replace(":", "");
            connection.Open();
            update.Parameters.AddWithValue("@TITLE", "CyberDay- " + school);
            update.Parameters.AddWithValue("@DATE", date);
            update.Parameters.AddWithValue("@STARTTIME", start);
            update.Parameters.AddWithValue("@ENDTIME", end);
            update.Parameters.AddWithValue("@VALUE", value);
            update.ExecuteNonQuery();
            connection.Close();

            NotifLBL.Text = "Your event has successfully been modified!";
            NotifLBL.Visible = true;
            ClearInfo();
        }

        protected void ModtaskBut_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand update =
                new SqlCommand(
                    "UPDATE FROM EVENTTASKS SET TITLE = @TITLE, STARTTIME = @STARTTIME, ENDTIME = @ENDTIME WHERE TASKID = @VALUE",
                    connection);

            int value = int.Parse(ModTaskDDL.SelectedValue);
            string title = HttpUtility.HtmlEncode(ModTaskNewTitle.Text);
            string start = HttpUtility.HtmlEncode(TaskNewStart.Text);
            string end = HttpUtility.HtmlEncode(TaskNewEnd.Text);
            start.Replace(":", "");
            end.Replace(":", "");
            connection.Open();
            update.Parameters.AddWithValue("@TITLE", title);
            update.Parameters.AddWithValue("@STARTTIME", start);
            update.Parameters.AddWithValue("@ENDTIME", end);
            update.Parameters.AddWithValue("@VALUE", value);
            update.ExecuteNonQuery();
            connection.Close();

            NotifLBL.Text = "Your task has successfully been modified!";
            NotifLBL.Visible = true;
            ClearInfo();
        }

        protected void DelBut_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand delete = new SqlCommand("DELETE FROM EVENTITINERARY WHERE EVENT = @VALUE", connection);
            SqlCommand delete2 = new SqlCommand("DELETE FROM EVENT WHERE EVENTID = @EVENTID", connection);
            string select = "SELECT (EVENTTITLE + '-' + EVENTDATE) AS EVENTINFO, EVENTID FROM EVENT";

            int value = int.Parse(EventDelDDL.SelectedValue);
            int eventID = int.Parse(EventDelDDL.SelectedValue);

            connection.Open();
            delete.Parameters.AddWithValue("@VALUE", value);
            delete.ExecuteNonQuery();
            delete2.Parameters.AddWithValue("@EVENTID", eventID);
            delete2.ExecuteNonQuery();
            SqlDataAdapter adpt = new SqlDataAdapter(select, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            EventDelDDL.DataSource = dt;
            EventDelDDL.DataBind();
            EventDelDDL.DataTextField = "EVENTINFO";
            EventDelDDL.DataValueField = "EVENTID";
            EventDelDDL.DataBind();
            connection.Close();

            NotifLBL.Text = "Your event has successfully been deleted!";
            NotifLBL.Visible = true;
            ClearInfo();
        }

        protected void TaskDelBut_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand delete = new SqlCommand("DELETE FROM EVENTITINERARY WHERE TASK = @VALUE", connection);
            SqlCommand delete2 = new SqlCommand("DELETE FROM EVENTTASKS WHERE TASKID = @TASKID", connection);
            string select =
                "SELECT (EVENTTASKS.TITLE + ' - ' + EVENT.EVENTDATE) AS eventtask, (EVENTTASKS.TASKID) AS TaskID FROM EVENTTASKS, EVENT";

            int value = int.Parse(TaskDelDDL.SelectedValue);
            int taskID = int.Parse(TaskDelDDL.SelectedValue);

            connection.Open();
            delete.Parameters.AddWithValue("@VALUE", value);
            delete.ExecuteNonQuery();
            delete2.Parameters.AddWithValue("@TASKID", taskID);
            delete2.ExecuteNonQuery();
            SqlDataAdapter adpt = new SqlDataAdapter(select, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            TaskDelDDL.DataSource = dt;
            TaskDelDDL.DataBind();
            TaskDelDDL.DataTextField = "eventtask";
            TaskDelDDL.DataValueField = "TaskID";
            TaskDelDDL.DataBind();
            connection.Close();

            NotifLBL.Text = "Your task has successfully been deleted!";
            NotifLBL.Visible = true;
            ClearInfo();
        }

        protected void ManualBtn_OnClick(object sender, EventArgs e)
        {
            ManualOrExcel.ActiveViewIndex = 0;
        }
        //end of manual event and tasks functions

        //start event management using excel
        protected void ExcelBtn_OnClick(object sender, EventArgs e)
        {
            ManualOrExcel.ActiveViewIndex = 1;
        }

        public void BulkTaskInsert(DataTable dt)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            // Open a connection to the AdventureWorks database.
            DataTableReader reader = dt.CreateDataReader();
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                connection.Open();
                // Perform an initial count on the destination table.
                SqlCommand commandRowCount = new SqlCommand(
                    "SELECT COUNT(TASKID) FROM " +
                    "dbo.EVENTTASKS;",
                    connection);
                long countStart = System.Convert.ToInt32(
                    commandRowCount.ExecuteScalar());
                Console.WriteLine("Starting row count = {0}", countStart);
                connection.Close();
                // Create the SqlBulkCopy object.
                // Note that the column positions in the source DataTable
                // match the column positions in the destination table so
                // there is no need to map columns.
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connectionString))
                {
                    bulkCopy.DestinationTableName =
                        "CyberDayInfo.dbo.EVENTTASKS";

                    SqlBulkCopyColumnMapping mapTitle =
                        new SqlBulkCopyColumnMapping("Title", "TITLE");
                    bulkCopy.ColumnMappings.Add(mapTitle);

                    SqlBulkCopyColumnMapping mapTime =
                        new SqlBulkCopyColumnMapping("Time", "STARTTIME");
                    bulkCopy.ColumnMappings.Add(mapTime);

                    SqlBulkCopyColumnMapping mapLocation =
                        new SqlBulkCopyColumnMapping("Location", "LOCATION");
                    bulkCopy.ColumnMappings.Add(mapLocation);

                    try
                    {
                        // Write the array of rows to the destination.
                        bulkCopy.WriteToServer(reader);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                connection.Open();
                // Perform a final count on the destination
                // table to see how many rows were added.
                long countEnd = System.Convert.ToInt32(
                    commandRowCount.ExecuteScalar());
                SqlCommand max = new SqlCommand("SELECT MAX(TASKID) from EVENTTASKS", connection);
                long maxID = System.Convert.ToInt32(max.ExecuteScalar());

                Session.Add("Start", maxID - (countEnd - countStart));
                Session.Add("End", maxID);
            }
        }

        protected void ImportExcel(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile && docType())
            {
                ViewState["FILENAME"] = String.Join(".", String.Format("{0:d/M/yyyy HH:mm}", HttpUtility.HtmlEncode(EventDateTxt.Text))) + Path.GetExtension(FileUpload1.PostedFile.FileName);

                bug.Text = String.Format("{0:d/M/yyyy HH:mm}", HttpUtility.HtmlEncode(NewDateTxt.Text));

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

                    }

                    BulkTaskInsert(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }

        protected Boolean docType()
        {
            if (Path.GetExtension(this.FileUpload1.FileName) == ".xlsx" ||
                Path.GetExtension(this.FileUpload1.FileName) == ".xls")
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

        protected DataTable setTable(DataTable dt) // use a dataview to filter out empty rows??
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

            return dt;
        }

        private static DataTable RemoveEmptyRows(DataTable source)
        {
            DataTable dt1 = source.Clone(); //copy the structure 
            for (int i = 0; i <= source.Rows.Count - 1; i++) //iterate through the rows of the source
            {
                DataRow currentRow = source.Rows[i]; //copy the current row 
                foreach (var colValue in currentRow.ItemArray) //move along the columns 
                {
                    if (!string.IsNullOrEmpty(colValue.ToString())
                    ) // if there is a value in a column, copy the row and finish
                    {
                        dt1.ImportRow(currentRow);
                        break; //break and get a new row                        
                    }
                }
            }

            return dt1;
        }

        public void CreateExEvent()
        {
            if (Page.IsValid)
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                SqlConnection connection = new SqlConnection(cs);
                SqlCommand insert =
                    new SqlCommand(
                        "INSERT INTO EVENT(EVENTNAME, EVENTDATE, STARTTIME, ENDTIME) VALUES(@NAME, @DATE, @STARTTIME, @ENDTIME)",
                        connection);
                string school = EXSchoolDDL.SelectedItem.Text;
                string start = HttpUtility.HtmlEncode(EXStartTimeTxt.Text);
                string end = HttpUtility.HtmlEncode(EXEndTimeTxt.Text);
                string date = HttpUtility.HtmlEncode(EXDateTxt.Text);
                start.Replace(":", "");
                end.Replace(":", "");
                connection.Open();
                insert.Parameters.AddWithValue("@NAME", "CyberDay- " + school);
                insert.Parameters.AddWithValue("@DATE", date);
                insert.Parameters.AddWithValue("@STARTTIME", start);
                insert.Parameters.AddWithValue("@ENDTIME", end);
                insert.ExecuteNonQuery();
                connection.Close();

                NotifLBL.Text = "Your event has successfully been created!";
                NotifLBL.Visible = true;
                ClearInfo();
            }
        }

        public void contructEXItinerary()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            int eventID = getEXEventID();
            if (Session["Start"] != null && Session["End"] != null)
            {
                int start = int.Parse(Session["Start"].ToString());
                int stop = int.Parse(Session["End"].ToString());
                DataTable dt = new DataTable();
                DataColumn dc1 = new DataColumn("Event");
                DataColumn dc2 = new DataColumn("Task");
                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);
                int l = 0;
                for (int i = start; i <= stop; i++)
                {
                    dt.Rows.Add();
                    dt.Rows[l][0] = eventID;
                    dt.Rows[l][1] = i;
                    l++;
                }
                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName =
                            "dbo.EVENTITINERARY";
                        try
                        {
                            bulkCopy.WriteToServer(dt);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }

        public int getEXEventID()
        {
            int id = 0;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            connection.Open();
            SqlCommand command = new SqlCommand("Select MAX(EVENTID) from EVENT", connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                id = int.Parse(reader[0].ToString());
            }

            return id;
        }

        protected void EXCreateBtn_OnClick(object sender, EventArgs e)
        {
            CreateExEvent();
            contructEXItinerary();
        }

        protected void ViewButton_OnClick(object sender, EventArgs e)
        {
            ManualOrExcel.ActiveViewIndex = 2;
            EventList();
        }

        protected void RunBtn_OnClick(object sender, EventArgs e)
        {
            if (SelectionDropDown.SelectedValue != String.Empty)
            {
                EventGrid();
                EventItinerary();
                FillPanel();
            }
        }

        private void EmptyGridView()
        {
            SelectedGridView.DataSource = null;
            SelectedGridView.DataBind();
            TertiaryGridView.DataSource = null;
            TertiaryGridView.DataBind();
            SelectedGridLbl.Text = "";
            SelectedGridLbl.Visible = false;
            TertiaryGridLbl.Text = "";
            TertiaryGridLbl.Visible = false;
            PrintBtn.Visible = false;
        }
        private void FillPanel()
        {
            printPanel.Controls.Add(ReportTable);
        }
        private void EventItinerary()
        {
            TertiaryGridLbl.Text = "Event Itinerary";
            TertiaryGridLbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            int eventID = int.Parse(SelectionDropDown.SelectedValue);
            string sql = "select ET.TITLE," +
                         " right(convert(varchar(20),cast(stuff(right('0000' + convert(varchar(4),ET.STARTTIME),4),3,0,':')as datetime),100),7) AS \"Start Time\"," +
                         " ET.LOCATION as Location" +
                         " FROM EVENTTASKS ET JOIN EVENTITINERARY EI ON EI.TASK = ET.TASKID JOIN VOLUNTEER V ON ET.INSTRUCTOR = V.STAFFID JOIN EVENT E ON E.EVENTID = EI.EVENT" +
                         " WHERE E.EVENTID = " + eventID +
                         " ORDER BY EI.TASK; ";
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(cs);
            SqlDataAdapter adapt = new SqlDataAdapter(sql, conn);
            conn.Open();
            adapt.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                TertiaryGridView.DataSource = dt;
                TertiaryGridView.DataBind();
            }
            else
            {
                dt = new DataTable();
                DataColumn dc1 = new DataColumn("Itinerary");
                dt.Columns.Add(dc1);
                DataRow dr1 = dt.NewRow();
                dr1[0] = "No Event Itinerary Found";
                dt.Rows.Add(dr1);
                TertiaryGridView.DataSource = dt;
                TertiaryGridView.DataBind();
            }
        }
        private void EventGrid()
        {
            SelectedGridLbl.Text = "Event Data";
            SelectedGridLbl.Visible = true;
            if (SelectionDropDown.SelectedValue != String.Empty)
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                int eventID = int.Parse(SelectionDropDown.SelectedValue);
                string sql = "Select EV.EVENTNAME as \"Event Name\", EV.EVENTDATE as \"Event Date\", " +
                             " right('0' + ltrim(right(convert(varchar,cast(EV.STARTTIME as dateTime), 100), 7)), 7)  AS \"START\"," +
                             " right('0' + ltrim(right(convert(varchar,cast(EV.ENDTIME as dateTime), 100), 7)), 7)  AS \"END\" " +
                             " from EVENT EV where EV.EVENTID = " + eventID;
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(cs);
                SqlDataAdapter adapt = new SqlDataAdapter(sql, conn);
                conn.Open();
                adapt.Fill(dt);
                conn.Close();
                if (dt.Rows.Count > 0)
                {
                    SelectedGridView.DataSource = dt;
                    SelectedGridView.DataBind();
                }
            }

        }
        private void EventList()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string command = "select EVENTID, (EVENTNAME + ' - ' + EVENTDATE) as INFO from EVENT ";
            connection.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                SelectionDropDown.DataSource = dt;
                SelectionDropDown.DataTextField = "INFO";
                SelectionDropDown.DataValueField = "EVENTID";
                SelectionDropDown.DataBind();
            }
            else
            {
                DataTable noData = new DataTable();
                DataColumn dc1 = new DataColumn("EVENTDATE");
                noData.Columns.Add(dc1);
                DataRow dr1 = noData.NewRow();
                dr1[0] = "No Event Found";
                noData.Rows.Add(dr1);
                SelectionDropDown.DataSource = noData;
                SelectionDropDown.DataTextField = "EVENTDATE";
                SelectionDropDown.DataBind();
            }
        }
        protected void clearBtn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("EventAdministration.aspx");
        }
    }
}
