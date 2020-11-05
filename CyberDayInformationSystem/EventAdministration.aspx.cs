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
        }

        protected void FunctionSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EventFunctionSelection.SelectedValue == "1")
            {
                NotifLBL.Text = String.Empty;
                SelectedFunction.ActiveViewIndex = 0;
                EventRoomList();
            }
            if (EventFunctionSelection.SelectedValue == "2")
            {
                NotifLBL.Text = String.Empty;              
                SelectedFunction.ActiveViewIndex = 1;
                EventDateList();
            }
            if (EventFunctionSelection.SelectedValue == "3")
            {
                NotifLBL.Text = String.Empty;
                SelectedFunction.ActiveViewIndex = 2;
                EventDateList();
            }
            if (TaskFunctionSelection.SelectedValue == "1")
            {

            }
            if (TaskFunctionSelection.SelectedValue == "1")
            {

            }
            if (TaskFunctionSelection.SelectedValue == "1")
            {

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

        //public void TaskList()
        //{
        //    string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
        //    SqlConnection connection = new SqlConnection(cs);
        //    string command = "SELECT TITLE, TASKID FROM EVENTTASKS";
        //    connection.Open();
        //    SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
        //    DataTable dt = new DataTable();
        //    adpt.Fill(dt);
        //    EventDDL.DataSource = dt;
        //    EventDDL.DataBind();
        //    EventDDL.DataTextField = "TITLE";
        //    EventDDL.DataValueField = "TASKID";
        //    EventDDL.DataBind();
        //    EventDDL.Items.Insert(0, new ListItem(String.Empty));
        //    EventDelDDL.DataSource = dt;
        //    EventDelDDL.DataBind();
        //    EventDelDDL.DataTextField = "TITLE";
        //    EventDelDDL.DataValueField = "TASKID";
        //    EventDelDDL.DataBind();
        //    EventDelDDL.Items.Insert(0, new ListItem(String.Empty));
        //    connection.Close();
        //}

        //public void OldTimeDisplay()
        //{
        //    string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
        //    SqlConnection connection = new SqlConnection(cs);
        //    SqlCommand select = new SqlCommand("SELECT STARTTIME, ENDTIME FROM EVENTTASKS WHERE TASKID = @VALUE", connection);            
        //    connection.Open();

        //    int value = int.Parse(EventDDL.SelectedValue);
        //    select.Parameters.AddWithValue("@VALUE", value);
        //    SqlDataReader reader = select.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        OldStartTxt.Text = (reader["STARTTIME"].ToString());
        //        OldEndTxt.Text = (reader["ENDTIME"].ToString());
        //    }
        //    connection.Close();
        //}

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

        public int CheckEvent()
        {
            int add;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand check = new SqlCommand("Select count(*) from EVENT where EVENTDATE = @DATE AND STARTTIME = @STARTTIME AND ENDTIME = @ENDTIME" +
                                              " AND LOCATION = @ROOM)", connection);

            string start = HttpUtility.HtmlEncode(EventTimeTxt.Text);
            string end = HttpUtility.HtmlEncode(EndTimeTxt.Text);
            string date = HttpUtility.HtmlEncode(EventDateTxt.Text);
            int room = int.Parse(EventLocationDDL.SelectedValue);
            connection.Open();
            check.Parameters.AddWithValue("@DATE", date);
            check.Parameters.AddWithValue("@STARTTIME", start);
            check.Parameters.AddWithValue("@ENDTIME", end);
            check.Parameters.AddWithValue("@ROOM", room);
            add = (int) check.ExecuteScalar();
            connection.Close();
            if (add > 0)
            {
                NotifLBL.Text = "Event Already Exists with this data";
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
                //OldTimeDisplay();
            }
        }

        protected void ModifyBut_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand update = new SqlCommand("UPDATE FROM EVENTTASKS SET TITLE = @TITLE, STARTTIME = @STARTTIME, ENDTIME = @ENDTIME WHERE TASKID = @VALUE", connection);

            int value = int.Parse(EventDelDDL.SelectedValue);
            string title = HttpUtility.HtmlEncode(NewTitleTxt.Text);
            string start = HttpUtility.HtmlEncode(NewStartTxt.Text);
            string end = HttpUtility.HtmlEncode(NewEndTxt.Text);

            connection.Open();
            update.Parameters.AddWithValue("@TITLE", title);
            update.Parameters.AddWithValue("@STARTTIME", start);
            update.Parameters.AddWithValue("@ENDTIME", end);
            update.Parameters.AddWithValue("@VALUE", value);
            update.ExecuteNonQuery();
            connection.Close();

            NotifLBL.Text = "Your event has successfully been modified!";
            NotifLBL.Visible = true;
            ClearInfo();
        }

        protected void DelBut_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand delete = new SqlCommand("DELETE FROM EVENTTASKS WHERE TASKID = @VALUE", connection);

            int value = int.Parse(EventDelDDL.SelectedValue);

            connection.Open();
            delete.Parameters.AddWithValue("@VALUE", value);
            delete.ExecuteNonQuery();
            connection.Close();

            NotifLBL.Text = "Your event has successfully been deleted!";
            NotifLBL.Visible = true;
            ClearInfo();
        }

        protected void TimeRangeValid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                string start = HttpUtility.HtmlEncode(EventTimeTxt.Text);
                string end = HttpUtility.HtmlEncode(EndTimeTxt.Text);
                string randomDt = "01/01/2000 ";
                args.IsValid = DateTime.Parse(randomDt + end) > DateTime.Parse(randomDt + start);
            }
            catch(Exception)
            {
                args.IsValid = false;
            }
        }

        protected void NewTitleValid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                string old = HttpUtility.HtmlEncode(OldTitleTxt.Text);
                string updated = HttpUtility.HtmlEncode(NewTitleTxt.Text);
                args.IsValid = updated != old;
            }
            catch(Exception)
            {
                args.IsValid = false;
            }
        }

        protected void NewStartValid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                string old = HttpUtility.HtmlEncode(OldStartTxt.Text);
                string updated = HttpUtility.HtmlEncode(NewStartTxt.Text);
                args.IsValid = updated != old;
            }
            catch(Exception)
            {
                args.IsValid = false;
            }
        }

        protected void NewEndValid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                string old = HttpUtility.HtmlEncode(OldEndTxt.Text);
                string updated = HttpUtility.HtmlEncode(NewEndTxt.Text);
                args.IsValid = updated != old;
            }
            catch(Exception)
            {
                args.IsValid = false;
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
    }
}