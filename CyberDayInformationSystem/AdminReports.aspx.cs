using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class AdminReports : System.Web.UI.Page
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
        public void EventList()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(cs);
            string command = "select EVENTID, EVENTDATE from EVENT";
            connection.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            SelectionDropDown.DataSource = dt;
            SelectionDropDown.DataTextField = "EVENTDATE";
            SelectionDropDown.DataValueField = "EVENTID";
            SelectionDropDown.DataBind();
            SelectionDropDown.Items.Insert(0, new ListItem(String.Empty));
        }
        public void StaffList()
        {
            SelectionDropDown.Items.Clear();
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string command = "select STAFFID, (firstname + ' '+ lastname) as NAME from Volunteer";
            SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            SelectionDropDown.DataSource = dt;
            SelectionDropDown.DataTextField = "NAME";
            SelectionDropDown.DataValueField = "STAFFID";
            SelectionDropDown.DataBind();
            SelectionDropDown.Items.Insert(0, new ListItem(String.Empty, String.Empty));
        }
        public void StudentList()
        {
            SelectionDropDown.Items.Clear();
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(cs);
            string command = "select STUDENTID as ID, (FIRSTNAME + ' ' + LASTNAME) as NAME from STUDENT ";
            SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
            connection.Open();
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            SelectionDropDown.DataSource = dt;
            SelectionDropDown.DataTextField = "NAME";
            SelectionDropDown.DataValueField = "ID";
            SelectionDropDown.DataBind();
            SelectionDropDown.Items.Insert(0, new ListItem(String.Empty, String.Empty));
        }
        public void EventGrid()
        {
            SelectedGridLbl.Text = "Event Data";
            SelectedGridLbl.Visible = true;
            if (SelectionDropDown.SelectedIndex == 0)
            {
                SelectedGridView.DataSource = null;
                SelectedGridView.DataBind();
            }
            else
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
                int eventID = int.Parse(SelectionDropDown.SelectedValue);
                string sql = "Select 'CyberDay' as \"Event Name\", EV.EVENTDATE as \"Event Date\", " +
                    " right(convert(varchar(20),cast(stuff(right('0000' + convert(varchar(4),EV.STARTTIME),4),3,0,':')as datetime),100),7) AS \"START\"," +
                    " right(convert(varchar(20), cast(stuff(right('0000' + convert(varchar(4), EV.ENDTIME), 4), 3, 0, ':') as datetime), 100), 7) AS \"END\"," +
                    " (RR.BUILDING + ' ' + RR.ROOMNUMBER) AS \"Room\" from EVENT EV LEFT OUTER JOIN ROOMRESERVATIONS RR " +
                    " on EV.LOCATION = RR.ROOMID where EVENTID = " + eventID;
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
        public void StaffGrid()
        {
            SelectedGridLbl.Text = "Volunteer Data";
            SelectedGridLbl.Visible = true;
            if (SelectionDropDown.SelectedIndex == 0)
            {
                SelectedGridView.DataSource = null;
                SelectedGridView.DataBind();
            }
            else
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
                int staffID = int.Parse(SelectionDropDown.SelectedValue);
                string sql = "SELECT (V.FIRSTNAME + ' ' + V.LASTNAME) AS NAME, FORMAT(V.PHONE,'(###)-###-####') AS \"PHONE NUMBER\", V.EMAILADD AS \"EMAIL ADDRESS\", V.TYPE FROM VOLUNTEER V" +
                    " where V.STAFFID = " + staffID;
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
        public void StudentVolunteerData()
        {
            TertiaryGridLbl.Text = "Student Volunteer Data";
            TertiaryGridLbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            int staffID = int.Parse(SelectionDropDown.SelectedValue);
            string sql = "SELECT SV.Major, SV.Minor, SV.ORGANIZATION, SV.PREVIOUSVOLUNTEER AS \"Previous Volunteer\" from STUDENTVOLUNTEER SV" +
                " where SV.VOLUNTEERID = " + staffID;
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
                TertiaryGridLbl.Text = "";
                TertiaryGridLbl.Visible = false;
            }
        }
        public void EventStaffGrid()
        {
            SecondaryGrid1Lbl.Text = "Event Volunteers";
            SecondaryGrid1Lbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            int eventID = int.Parse(SelectionDropDown.SelectedValue);
            string sql = "SELECT(V.FIRSTNAME + ' ' + V.LASTNAME) AS NAME, V.TYPE as ROLE FROM VOLUNTEER V join EVENTSTAFF ES on ES.STAFF = V.STAFFID join " +
            "EVENT EV on EV.EVENTID = ES.EVENT where EVENTID = " + eventID;
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(cs);
            SqlDataAdapter adapt = new SqlDataAdapter(sql, conn);
            conn.Open();
            adapt.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                SecondaryGridView1.DataSource = dt;
                SecondaryGridView1.DataBind();
            }
            else
            {
                dt = new DataTable();
                DataColumn dc1 = new DataColumn("NAME");
                dt.Columns.Add(dc1);
                DataRow dr1 = dt.NewRow();
                dr1[0] = "No Students Registered";
                dt.Rows.Add(dr1);
                SecondaryGridView1.DataSource = dt;
                SecondaryGridView1.DataBind();
            }
        }
        public void EventRosterGrid()
        {
            SecondaryGrid2Lbl.Text = "Event Roster";
            SecondaryGrid2Lbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            int eventID = int.Parse(SelectionDropDown.SelectedValue);
            string sql = "SELECT (S.FIRSTNAME + ' ' + S.LASTNAME) AS NAME FROM STUDENT S RIGHT OUTER JOIN EVENTROSTER ER on " +
                "ER.STUDENT = S.STUDENTID WHERE ER.EVENT = " + eventID;
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(cs);
            SqlDataAdapter adapt = new SqlDataAdapter(sql, conn);
            conn.Open();
            adapt.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                SecondaryGridView2.DataSource = dt;
                SecondaryGridView2.DataBind();
            }
            else
            {
                dt = new DataTable();
                DataColumn dc1 = new DataColumn("NAME");
                dt.Columns.Add(dc1);
                DataRow dr1 = dt.NewRow();
                dr1[0] = "No Students Registered";
                dt.Rows.Add(dr1);
                SecondaryGridView2.DataSource = dt;
                SecondaryGridView2.DataBind();
            }
        }
        public void StudentGridFill(int StudentID)
        {
            SelectedGridLbl.Text = "Student Data";
            SelectedGridLbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            string sql = "Select FIRSTNAME, LASTNAME, GENDER, AGE from STUDENT where STUDENTID = " + StudentID;
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
        public void StudentNotesInfo(int StudentID)
        {
            SecondaryGrid1Lbl.Text = "Student Notes";
            SecondaryGrid1Lbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            string sql = "Select NOTES FROM STUDENTNOTES where STUDENT = " + StudentID;
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(cs);
            SqlDataAdapter adapt = new SqlDataAdapter(sql, conn);
            conn.Open();
            adapt.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                SecondaryGridView1.DataSource = dt;
                SecondaryGridView1.DataBind();
            }
            else
            {
                dt = new DataTable();
                DataColumn dc1 = new DataColumn("Notes");
                dt.Columns.Add(dc1);
                DataRow dr1 = dt.NewRow();
                dr1[0] = "No Notes on File";
                dt.Rows.Add(dr1);
                SecondaryGridView1.DataSource = dt;
                SecondaryGridView1.DataBind();
            }
        }
        public void StudentTeacherInfo(int StudentID)
        {
            SecondaryGrid2Lbl.Text = "Contact People";
            SecondaryGrid2Lbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            string sql = "Select (T.TITLE + ' ' + T.FIRSTNAME + ' ' + T.LASTNAME) AS \"Name\", FORMAT(T.PHONE,'(###)-###-####') AS \"PHONE NUMBER\", 'Teacher' as Type from TEACHER T join " +
                " STUDENT S ON T.TEACHERID = S.TEACHER where STUDENTID = " + StudentID +
                " UNION " +
                "Select (G.FIRSTNAME + ' ' + G.LASTNAME) AS \"Name\", FORMAT(G.PHONE,'(###)-###-####') AS \"PHONE NUMBER\", 'Guardian' as type from GUARDIAN G join " +
                "STUDENT S ON G.GUARDIANID = S.GUARDIAN where STUDENTID = " + StudentID;
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(cs);
            SqlDataAdapter adapt = new SqlDataAdapter(sql, conn);
            conn.Open();
            adapt.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                SecondaryGridView2.DataSource = dt;
                SecondaryGridView2.DataBind();
            }
            else
            {
                dt = new DataTable();
                DataColumn dc1 = new DataColumn("NAME");
                dt.Columns.Add(dc1);
                DataRow dr1 = dt.NewRow();
                dr1[0] = "No Contact Persons on file for Student";
                dt.Rows.Add(dr1);
                SecondaryGridView2.DataSource = dt;
                SecondaryGridView2.DataBind();
            }
        }
        public void MealTicketGrid()
        {
            TertiaryGridLbl.Text = "Meal Ticket Data";
            TertiaryGridLbl.Visible = true;
            int EventID = int.Parse(SelectionDropDown.SelectedValue);
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            string sql = "select(S.FIRSTNAME + ' ' + S.LASTNAME) AS NAME, MT.USED AS RECIEVED FROM MEALTICKET MT JOIN STUDENT S " +
                "ON MT.MEALTICKETID = S.MEALTICKET JOIN EVENT EV ON MT.EVENT = EV.EVENTID WHERE EV.EVENTID = " + EventID + " UNION" +
                " select (T.FIRSTNAME + ' ' + T.LASTNAME) AS NAME, MT.USED AS RECIEVED FROM MEALTICKET MT JOIN TEACHER T ON MT.MEALTICKETID = T.MEALTICKET" +
                " JOIN EVENT EV ON MT.EVENT = EV.EVENTID WHERE EV.EVENTID = " + EventID + " UNION select (G.FIRSTNAME + ' ' + G.LASTNAME) AS NAME," +
                " MT.USED AS RECIEVED FROM MEALTICKET MT JOIN GUARDIAN G ON MT.MEALTICKETID = G.MEALTICKET JOIN EVENT EV ON MT.EVENT = EV.EVENTID" +
                " WHERE EV.EVENTID = " + EventID + " UNION select (V.FIRSTNAME + ' ' + V.LASTNAME) AS NAME, MT.USED AS RECIEVED FROM MEALTICKET MT" +
                " JOIN VOLUNTEER V ON MT.MEALTICKETID = V.MEALTICKET JOIN EVENT EV ON MT.EVENT = EV.EVENTID WHERE EV.EVENTID = " + EventID;
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
        }
        public void EventItinerary()
        {

            TertiaryGridLbl.Text = "Event Itinerary";
            TertiaryGridLbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            int eventID = int.Parse(SelectionDropDown.SelectedValue);
            string sql = "select ET.TITLE, (V.FIRSTNAME + ' ' + V.LASTNAME) AS INSTRUCTOR," +
                " right(convert(varchar(20),cast(stuff(right('0000' + convert(varchar(4),ET.STARTTIME),4),3,0,':')as datetime),100),7) AS \"START\"," +
                " right(convert(varchar(20), cast(stuff(right('0000' + convert(varchar(4), ET.ENDTIME), 4), 3, 0, ':') as datetime), 100), 7) AS \"END\"" +
                " FROM EVENTTASKS ET JOIN EVENTITINERARY EI ON EI.TASK = ET.TASKID JOIN VOLUNTEER V ON ET.INSTRUCTOR = V.STAFFID JOIN EVENT E ON E.EVENTID = EI.EVENT" +
                " WHERE E.EVENTID = " +eventID + 
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
        protected void FunctionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmptyGridView();
            if (FunctionList.SelectedValue == "1")
            {
                SelectionLbl.Text = "Event: ";
                EventList();
                SelectionLbl.Visible = true;
                SelectionDropDown.Visible = true;
            }
            if (FunctionList.SelectedValue == "2")
            {
                SelectionLbl.Text = "Staff: ";
                StaffList();
                SelectionLbl.Visible = true;
                SelectionDropDown.Visible = true;
            }
            if (FunctionList.SelectedValue == "3")
            {
                SelectionLbl.Text = "Student: ";
                StudentList();
                SelectionLbl.Visible = true;
                SelectionDropDown.Visible = true;
            }
            if(FunctionList.SelectedValue == "4")
            {
                SelectionLbl.Text = "Event Date: ";
                EventList();
                SelectionLbl.Visible = true;
                SelectionDropDown.Visible = true;
            }
        }

        protected void RunBtn_Click(object sender, EventArgs e)
        {
            EmptyGridView();
            if (FunctionList.SelectedValue == "1")
            {
                EventGrid();
                EventStaffGrid();
                EventRosterGrid();
                EventItinerary();
            }
            if (FunctionList.SelectedValue == "2")
            {
                StaffGrid();
                StudentVolunteerData();
            }
            if(FunctionList.SelectedValue == "3")
            {
                int StudentID = int.Parse(SelectionDropDown.SelectedValue);
                StudentGridFill(StudentID);
                StudentNotesInfo(StudentID);
                StudentTeacherInfo(StudentID);
            }
            if(FunctionList.SelectedValue == "4")
            {
                EventGrid();
                MealTicketGrid();
            }
        }
        public void EmptyGridView()
        {
            SelectedGridView.DataSource = null;
            SelectedGridView.DataBind();
            SecondaryGridView2.DataSource = null;
            SecondaryGridView2.DataBind();
            SecondaryGridView1.DataSource = null;
            SecondaryGridView1.DataBind();
            TertiaryGridView.DataSource = null;
            TertiaryGridView.DataBind();
            SelectedGridLbl.Text = "";
            SelectedGridLbl.Visible = false;
            SecondaryGrid1Lbl.Text = "";
            SecondaryGrid1Lbl.Visible = false;
            SecondaryGrid2Lbl.Text = "";
            SecondaryGrid2Lbl.Visible = false;
            TertiaryGridLbl.Text = "";
            TertiaryGridLbl.Visible = false;
        }

        protected void PrintBtn_Click(object sender, EventArgs e)
        {

        }
    }
}