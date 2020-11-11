using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class EventAdministration : Page
    {
        //void Page_PreInit(Object sender, EventArgs e)
        //{
        //    if (Session["TYPE"] != null)
        //    {
        //        MasterPageFile = (Session["Master"].ToString());
        //        if (Session["TYPE"].ToString() != "Coordinator")
        //        {
        //            Session.Add("Redirected", 1);
        //            Response.Redirect("BadSession.aspx");
        //        }
        //    }
        //    else
        //    {
        //        Session.Add("Redirected", 0);
        //        Response.Redirect("BadSession.aspx");
        //    }
        //}
        private int _eventToModify;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EDITEVENTID"] != null)
            {
                _eventToModify = int.Parse(Session["EDITEVENTID"].ToString());
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
                EventRoomList();
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
            CreateEventTaskDDL.DataSource = dt;
            CreateEventTaskDDL.DataBind();
            CreateEventTaskDDL.DataTextField = "EVENTDATE";
            CreateEventTaskDDL.DataValueField = "EVENTID";
            CreateEventTaskDDL.DataBind();
            CreateEventTaskDDL.Items.Insert(0, new ListItem(String.Empty));
            connection.Close();
        }

        public void EventRoomList()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string command = "SELECT (BUILDING + ' ' + ROOMNUMBER) AS Room, ROOMID FROM ROOMRESERVATIONS";
            connection.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            EventLocationDDL.DataSource = dt;
            EventLocationDDL.DataBind();
            EventLocationDDL.DataTextField = "Room";
            EventLocationDDL.DataValueField = "ROOMID";
            EventLocationDDL.DataBind();
            EventLocationDDL.Items.Insert(0, new ListItem(String.Empty));
            connection.Close();
        }

        public void TaskList()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string command = "SELECT (EVENTTASKS.TITLE + ' - ' + EVENT.EVENTDATE) AS eventtask, (EVENTTASKS.TASKID) AS TaskID FROM EVENTTASKS, EVENT;";
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

        public void OldTimeDisplay()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand select = new SqlCommand("SELECT STARTTIME, ENDTIME FROM EVENT WHERE EVENTID = @VALUE", connection);
            SqlCommand select2 = new SqlCommand("SELECT STARTTIME, ENDTIME FROM EVENTTASKS WHERE TASKID = @TASKVALUE", connection);

            if (EventFunctionSelection.SelectedValue == "2")
            {
                int value = int.Parse(EventDateDDL.SelectedValue);
                connection.Open();
                select.Parameters.AddWithValue("@VALUE", value);
                SqlDataReader reader = select.ExecuteReader();
                while (reader.Read())
                {
                    OldStartTxt.Text = (reader["STARTTIME"].ToString());
                    OldEndTxt.Text = (reader["ENDTIME"].ToString());
                }
                connection.Close();
            }

            if (TaskFunctionSelection.SelectedValue == "2")
            {
                int value = int.Parse(ModTaskDDL.SelectedValue);
                connection.Open();
                select2.Parameters.AddWithValue("@TASKVALUE", value);
                SqlDataReader reader = select2.ExecuteReader();
                while (reader.Read())
                {
                    TaskOldStart.Text = reader["STARTTIME"].ToString();
                    TaskOldEnd.Text = reader["ENDTIME"].ToString();
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
                EventLocationDDL.ClearSelection();
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
                    SqlCommand insert = new SqlCommand("INSERT INTO EVENT(EVENTDATE, STARTTIME, ENDTIME, LOCATION) VALUES(@DATE, @STARTTIME, @ENDTIME, @ROOM)", connection);

                    string start = HttpUtility.HtmlEncode(EventTimeTxt.Text);
                    string end = HttpUtility.HtmlEncode(EndTimeTxt.Text);
                    string date = HttpUtility.HtmlEncode(EventDateTxt.Text);
                    int room = int.Parse(EventLocationDDL.SelectedValue);
                    connection.Open();
                    insert.Parameters.AddWithValue("@DATE", date);
                    insert.Parameters.AddWithValue("@STARTTIME", start);
                    insert.Parameters.AddWithValue("@ENDTIME", end);
                    insert.Parameters.AddWithValue("@ROOM", room);
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
                    SqlCommand insert = new SqlCommand("INSERT INTO EVENTTASKS(TITLE, STARTTIME, ENDTIME) VALUES(@TITLE, @STARTTIME, @ENDTIME)", connection);
                    SqlCommand insert2 = new SqlCommand("INSERT INTO EVENTITINERARY VALUES(@EVENT, @TASK)", connection);
                    SqlCommand select = new SqlCommand("SELECT TASKID FROM EVENTTASKS WHERE TITLE = @TITLE AND STARTTIME = @STARTTIME AND ENDTIME = @ENDTIME", connection);

                    string title = HttpUtility.HtmlEncode(TaskTitleTxt.Text);
                    string start = HttpUtility.HtmlEncode(TaskStartTimeTxt.Text);
                    string end = HttpUtility.HtmlEncode(TaskEndTimeTxt.Text);
                    int eventKey = int.Parse(CreateEventTaskDDL.SelectedValue);
                    int taskKey;

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
            SqlCommand check = new SqlCommand("Select count(*) from EVENT where EVENTDATE = @DATE AND STARTTIME = @STARTTIME AND ENDTIME = @ENDTIME" +
                                              " AND LOCATION = @ROOM", connection);

            string start = HttpUtility.HtmlEncode(EventTimeTxt.Text);
            string end = HttpUtility.HtmlEncode(EndTimeTxt.Text);
            string date = HttpUtility.HtmlEncode(EventDateTxt.Text);
            int room = int.Parse(EventLocationDDL.SelectedValue);
            connection.Open();
            check.Parameters.AddWithValue("@DATE", date);
            check.Parameters.AddWithValue("@STARTTIME", start);
            check.Parameters.AddWithValue("@ENDTIME", end);
            check.Parameters.AddWithValue("@ROOM", room);
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
            SqlCommand check = new SqlCommand("Select count(*) from EVENTTASKS where TITLE = @TITLE AND STARTTIME = @STARTTIME AND ENDTIME = @ENDTIME", connection);

            string title = HttpUtility.HtmlEncode(TaskTitleTxt.Text);
            string start = HttpUtility.HtmlEncode(TaskStartTimeTxt.Text);
            string end = HttpUtility.HtmlEncode(TaskEndTimeTxt.Text);
            connection.Open();
            check.Parameters.AddWithValue("@TITLE", title);
            check.Parameters.AddWithValue("@STARTTIME", start);
            check.Parameters.AddWithValue("@ENDTIME", end);
            add = (int)check.ExecuteScalar();
            connection.Close();
            if (add > 0)
            {
                NotifLBL.Text = " A task already exists with this data";
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
                OldTitleTxt.Text = EventDateDDL.SelectedItem.ToString();
                OldTimeDisplay();
            }
        }

        protected void ModTaskDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModTaskDDL.SelectedIndex != 0)
            {
                ModTaskTbl.Visible = true;
                ModTaskBut.Visible = true;
                ModTaskOldTitle.Text = ModTaskDDL.SelectedItem.ToString();
                OldTimeDisplay();
            }
        }

        protected void ModifyBut_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand update = new SqlCommand("UPDATE FROM EVENT SET DATE = @DATE, STARTTIME = @STARTTIME, ENDTIME = @ENDTIME WHERE EVENTID = @VALUE", connection);

            int value = int.Parse(EventDateDDL.SelectedValue);
            string date = HttpUtility.HtmlEncode(NewTitleTxt.Text);
            string start = HttpUtility.HtmlEncode(NewStartTxt.Text);
            string end = HttpUtility.HtmlEncode(NewEndTxt.Text);

            connection.Open();
            update.Parameters.AddWithValue("@TITLE", date);
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
            SqlCommand update = new SqlCommand("UPDATE FROM EVENTTASKS SET TITLE = @TITLE, STARTTIME = @STARTTIME, ENDTIME = @ENDTIME WHERE TASKID = @VALUE", connection);

            int value = int.Parse(ModTaskDDL.SelectedValue);
            string title = HttpUtility.HtmlEncode(ModTaskNewTitle.Text);
            string start = HttpUtility.HtmlEncode(TaskNewStart.Text);
            string end = HttpUtility.HtmlEncode(TaskNewEnd.Text);

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

            int value = int.Parse(EventDelDDL.SelectedValue);
            int eventID = int.Parse(EventDelDDL.SelectedValue);

            connection.Open();
            delete.Parameters.AddWithValue("@VALUE", value);
            delete.ExecuteNonQuery();
            delete2.Parameters.AddWithValue("@EVENTID", eventID);
            delete2.ExecuteNonQuery();
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

            int value = int.Parse(TaskDelDDL.SelectedValue);
            int taskID = int.Parse(TaskDelDDL.SelectedValue);

            connection.Open();
            delete.Parameters.AddWithValue("@VALUE", value);
            delete.ExecuteNonQuery();
            delete2.Parameters.AddWithValue("@TASKID", taskID);
            delete2.ExecuteNonQuery();
            connection.Close();

            NotifLBL.Text = "Your task has successfully been deleted!";
            NotifLBL.Visible = true;
            ClearInfo();
        }

        protected void TimeRangeValid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (EventFunctionSelection.SelectedValue == "2")
            {
                try
                {
                    string start = HttpUtility.HtmlEncode(EventTimeTxt.Text);
                    string end = HttpUtility.HtmlEncode(EndTimeTxt.Text);
                    string randomDt = "01/01/2000 ";
                    args.IsValid = DateTime.Parse(randomDt + end) > DateTime.Parse(randomDt + start);
                }
                catch (Exception)
                {
                    args.IsValid = false;
                }
            }
            
            if (TaskFunctionSelection.SelectedValue == "2")
            {
                try
                {
                    string start = HttpUtility.HtmlEncode(TaskNewStart.Text);
                    string end = HttpUtility.HtmlEncode(TaskNewEnd.Text);
                    string randomDt = "01/01/2000 ";
                    args.IsValid = DateTime.Parse(randomDt + end) > DateTime.Parse(randomDt + start);
                }
                catch (Exception)
                {
                    args.IsValid = false;
                }
            }
        }

        protected void NewTitleValid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (EventFunctionSelection.SelectedValue == "2")
            {
                try
                {
                    string old = HttpUtility.HtmlEncode(OldTitleTxt.Text);
                    string updated = HttpUtility.HtmlEncode(NewTitleTxt.Text);
                    args.IsValid = updated != old;
                }
                catch (Exception)
                {
                    args.IsValid = false;
                }
            }
            
            if (TaskFunctionSelection.SelectedValue == "2")
            {
                try
                {
                    string old = HttpUtility.HtmlEncode(ModTaskOldTitle.Text);
                    string updated = HttpUtility.HtmlEncode(ModTaskNewTitle.Text);
                    args.IsValid = updated != old;
                }
                catch (Exception)
                {
                    args.IsValid = false;
                }
            }
        }

        protected void NewStartValid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (EventFunctionSelection.SelectedValue == "2")
            {
                try
                {
                    string old = HttpUtility.HtmlEncode(OldStartTxt.Text);
                    string updated = HttpUtility.HtmlEncode(NewStartTxt.Text);
                    args.IsValid = updated != old;
                }
                catch (Exception)
                {
                    args.IsValid = false;
                }
            }

            if (TaskFunctionSelection.SelectedValue == "2")
            {
                try
                {
                    string old = HttpUtility.HtmlEncode(TaskOldStart.Text);
                    string updated = HttpUtility.HtmlEncode(TaskNewStart.Text);
                    args.IsValid = updated != old;
                }
                catch (Exception)
                {
                    args.IsValid = false;
                }
            }
        }

        protected void NewEndValid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (EventFunctionSelection.SelectedValue == "2")
            {
                try
                {
                    string old = HttpUtility.HtmlEncode(OldEndTxt.Text);
                    string updated = HttpUtility.HtmlEncode(NewEndTxt.Text);
                    args.IsValid = updated != old;
                }
                catch (Exception)
                {
                    args.IsValid = false;
                }
            }
            
            if (TaskFunctionSelection.SelectedValue == "2")
            {
                try
                {
                    string old = HttpUtility.HtmlEncode(TaskOldEnd.Text);
                    string updated = HttpUtility.HtmlEncode(TaskNewEnd.Text);
                    args.IsValid = updated != old;
                }
                catch (Exception)
                {
                    args.IsValid = false;
                }
            }
        }
    }
}