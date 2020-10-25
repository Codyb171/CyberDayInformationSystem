using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web;
using System.Web.UI;
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
                SelectedFunction.ActiveViewIndex = 0;
                EventDateList();
                EventRoomList();
            }
            if (FunctionSelection.SelectedValue == "2")
            {
                SelectedFunction.ActiveViewIndex = 1;
                EventList();
            }
            if (FunctionSelection.SelectedValue == "3")
            {
                SelectedFunction.ActiveViewIndex = 2;
                EventList();
            }
        }

        public void EventDateList()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
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
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
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
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
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

        public void ClearInfo()
        {
            EventNameTxt.Text = String.Empty;
            EventDateDDL.ClearSelection();
            EventTimeTxt.Text = String.Empty;
            EventLocationDDL.ClearSelection();
            EventDDL.ClearSelection();
            EventDelDDL.ClearSelection();
        }

        protected void CreateBut_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand insert = new SqlCommand("INSERT INTO EVENTTASKS VALUES(@TITLE, @EVENTTIME)");

            string title = HttpUtility.HtmlEncode(EventNameTxt.Text);
            string time = HttpUtility.HtmlEncode(EventTimeTxt.Text);

            connection.Open();
            insert.Parameters.AddWithValue("@TITLE", title);
            insert.Parameters.AddWithValue("@EVENTTIME", time);
            insert.ExecuteNonQuery();
            connection.Close();

            successLBL.Text = "Your event has successfully been created!";
            ClearInfo();
        }

        protected void EventDDL_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ModifyBut_Click(object sender, EventArgs e)
        {

        }

        protected void EventDelDDL_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DelBut_Click(object sender, EventArgs e)
        {

        }
    }
}