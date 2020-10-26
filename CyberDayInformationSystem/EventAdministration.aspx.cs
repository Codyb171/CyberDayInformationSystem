using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class EventAdministration : System.Web.UI.Page
    {
        //void Page_PreInit(Object sender, EventArgs e)
        //{
        //    if (Session["TYPE"] != null)
        //    {
        //        this.MasterPageFile = (Session["Master"].ToString());
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
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void FunctionSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FunctionSelection.SelectedValue == "1")
            {
                NotifLBL.Text = String.Empty;
                SelectedFunction.ActiveViewIndex = 0;
                EventDateList();
                EventRoomList();
            }
            if (FunctionSelection.SelectedValue == "2")
            {
                NotifLBL.Text = String.Empty;              
                SelectedFunction.ActiveViewIndex = 1;
                EventList();
            }
            if (FunctionSelection.SelectedValue == "3")
            {
                NotifLBL.Text = String.Empty;
                SelectedFunction.ActiveViewIndex = 2;
                EventList();
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

        public void EventList()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string command = "SELECT TITLE, TASKID FROM EVENTTASKS";
            connection.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            EventDDL.DataSource = dt;
            EventDDL.DataBind();
            EventDDL.DataTextField = "TITLE";
            EventDDL.DataValueField = "TASKID";
            EventDDL.DataBind();
            EventDDL.Items.Insert(0, new ListItem(String.Empty));
            EventDelDDL.DataSource = dt;
            EventDelDDL.DataBind();
            EventDelDDL.DataTextField = "TITLE";
            EventDelDDL.DataValueField = "TASKID";
            EventDelDDL.DataBind();
            EventDelDDL.Items.Insert(0, new ListItem(String.Empty));
            connection.Close();
        }

        public void OldStartTimeDisplay()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand select = new SqlCommand("SELECT STARTTIME FROM EVENTTASKS WHERE TASKID = @VALUE", connection);            
            connection.Open();

            int value = int.Parse(EventDDL.SelectedValue);
            select.Parameters.AddWithValue("@VALUE", value);
            SqlDataReader reader = select.ExecuteReader();

            while (reader.Read())
            {
                OldStartTxt.Text = (reader["STARTTIME"].ToString());
            }
            connection.Close();
        }

        public void OldEndTimeDisplay()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand select = new SqlCommand("SELECT ENDTIME FROM EVENTTASKS WHERE TASKID = @VALUE", connection);
            connection.Open();

            int value = int.Parse(EventDDL.SelectedValue);
            select.Parameters.AddWithValue("@VALUE", value);
            SqlDataReader reader = select.ExecuteReader();

            while (reader.Read())
            {
                OldEndTxt.Text = (reader["ENDTIME"].ToString());
            }
            connection.Close();
        }

        public void ClearInfo()
        {
            if (FunctionSelection.SelectedValue == "1")
            {
                EventNameTxt.Text = String.Empty;
                EventDateDDL.ClearSelection();
                EventTimeTxt.Text = String.Empty;
                EndTimeTxt.Text = String.Empty;
                EventLocationDDL.ClearSelection();
            }           
            if (FunctionSelection.SelectedValue == "2")
            {
                EventDDL.ClearSelection();
            }
            if (FunctionSelection.SelectedValue == "3")
            {
                EventDelDDL.ClearSelection();
            }            
        }

        protected void CreateBut_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand insert = new SqlCommand("INSERT INTO EVENTTASKS VALUES(@TITLE, @STARTTIME, @ENDTIME)");

            string title = HttpUtility.HtmlEncode(EventNameTxt.Text);
            string start = HttpUtility.HtmlEncode(EventTimeTxt.Text);
            string end = HttpUtility.HtmlEncode(EndTimeTxt.Text);

            connection.Open();
            insert.Parameters.AddWithValue("@TITLE", title);
            insert.Parameters.AddWithValue("@STARTTIME", start);
            insert.Parameters.AddWithValue("@ENDTIME", end);
            insert.ExecuteNonQuery();
            connection.Close();

            NotifLBL.Text = "Your event has successfully been created!";
            NotifLBL.Visible = true;            
            ClearInfo();
        }

        protected void EventDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModifyTbl.Visible = true;
            ModifyBut.Visible = true;
            OldTitleTxt.Text = EventDDL.SelectedItem.ToString();
            OldStartTimeDisplay();
            OldEndTimeDisplay();
        }

        protected void ModifyBut_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand update = new SqlCommand("UPDATE FROM EVENTTASKS SET TITLE = @TITLE, STARTTIME = @STARTTIME, ENDTIME = @ENDTIME WHERE TASKID = @VALUE");

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
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand delete = new SqlCommand("DELETE FROM EVENTTASKS WHERE TASKID = @VALUE");

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
            catch (Exception)
            {
                args.IsValid = false;
            }
        }
    }
}