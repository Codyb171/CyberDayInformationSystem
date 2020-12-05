using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Vml;

namespace CyberDayInformationSystem
{
    public partial class AdminReports : Page
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
            ScriptManager.RegisterClientScriptInclude(Page, GetType(), "PrintReport.js", "Scripts/src/methods/PrintReport.js");
            if (Page.IsPostBack)
            {
                FillPanel();
            }
        }

        private void EventList()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string command = "select EVENTID, EVENTDATE from EVENT ";
            connection.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                SelectionDropDown.DataSource = dt;
                SelectionDropDown.DataTextField = "EVENTDATE";
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

        private void StaffList()
        {
            SelectionDropDown.Items.Clear();
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string command = "select STAFFID, (firstname + ' ' + lastname) as NAME from Volunteer ";
            if (FirstNameTxt.Text != String.Empty && LastNameTxt.Text != String.Empty)
            {
                command += "where FIRSTNAME LIKE '%" + HttpUtility.HtmlEncode(FirstNameTxt.Text) + "%' AND LASTNAME LIKE '%" + HttpUtility.HtmlEncode(LastNameTxt.Text)
                    + "%'";
            }
            else if (FirstNameTxt.Text != String.Empty)
            {
                command += "where FIRSTNAME LIKE '%" + HttpUtility.HtmlEncode(FirstNameTxt.Text) + "%'";
            }
            else if(LastNameTxt.Text != String.Empty)
            {
                command += "where LASTNAME LIKE '% " + HttpUtility.HtmlEncode(LastNameTxt.Text) + "%'";
            }

            SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                SelectionDropDown.DataSource = dt;
                SelectionDropDown.DataTextField = "NAME";
                SelectionDropDown.DataValueField = "STAFFID";
                SelectionDropDown.DataBind();
            }
            else
            {
                DataTable noData = new DataTable();
                DataColumn dc1 = new DataColumn("NAME");
                noData.Columns.Add(dc1);
                DataRow dr1 = noData.NewRow();
                dr1[0] = "No Volunteers Found";
                noData.Rows.Add(dr1);
                SelectionDropDown.DataSource = noData;
                SelectionDropDown.DataTextField = "NAME";
                SelectionDropDown.DataBind();
            }
        }

        private void StudentList()
        {
            SelectionDropDown.Items.Clear();
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string command = "select STUDENTID as ID, (FIRSTNAME + ' ' + LASTNAME) as NAME from STUDENT ";
            if (FirstNameTxt.Text != String.Empty && LastNameTxt.Text != String.Empty)
            {
                command += "where FIRSTNAME LIKE '%" + HttpUtility.HtmlEncode(FirstNameTxt.Text) + "%' AND LASTNAME LIKE '%" + HttpUtility.HtmlEncode(LastNameTxt.Text)
                           + "%'";
            }
            else if (FirstNameTxt.Text != String.Empty)
            {
                command += "where FIRSTNAME LIKE '%" + HttpUtility.HtmlEncode(FirstNameTxt.Text) + "%'";
            }
            else if (LastNameTxt.Text != String.Empty)
            {
                command += "where LASTNAME LIKE '%" + HttpUtility.HtmlEncode(LastNameTxt.Text) + "%'";
            }
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

        private void EventGrid()
        {
            SelectedGridLbl.Text = "Event Data";
            SelectedGridLbl.Visible = true;
            if (SelectionDropDown.SelectedValue != String.Empty)
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                int eventID = int.Parse(SelectionDropDown.SelectedValue);
                string sql ="Select EV.EVENTNAME as \"Event Name\", EV.EVENTDATE as \"Event Date\", " +
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

        private void StaffGrid()
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
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
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

        private void StudentVolunteerData()
        {
            TertiaryGridLbl.Text = "Student Volunteer Data";
            TertiaryGridLbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
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

        private void EventStaffGrid()
        {
            SecondaryGrid1Lbl.Text = "Volunteers";
            SecondaryGrid1Lbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
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

        private void EventRosterGrid()
        {
            SecondaryGrid2Lbl.Text = "Attendees";
            SecondaryGrid2Lbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            int eventID = int.Parse(SelectionDropDown.SelectedValue);
            string sql = "SELECT (S.FIRSTNAME + ' ' + S.LASTNAME) AS NAME, \'Student\' as Type FROM STUDENT S left OUTER JOIN EVENTROSTER ER on " +
                "ER.STUDENT = S.STUDENTID WHERE ER.EVENT = " + eventID +
                " Union " +
                "Select (T.TITLE + \' \' + T.FIRSTNAME + \' \' + T.LASTNAME) AS \"Name\", \'Chaperone\' as Type from Teacher T left OUTER JOIN EVENTROSTER ER on" +
                " ER.CHAPERONE = T.TEACHERID WHERE ER.EVENT = " + eventID;
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

        private void StudentGridFill(int studentID)
        {
            SelectedGridLbl.Text = "Student Data";
            SelectedGridLbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            string sql = "Select FIRSTNAME, LASTNAME, GENDER, AGE from STUDENT where STUDENTID = " + studentID;
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

        private void StudentNotesInfo(int studentID)
        {
            SecondaryGrid1Lbl.Text = "Student Notes";
            SecondaryGrid1Lbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            string sql = "Select NOTES FROM STUDENTNOTES where STUDENT = " + studentID;
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

        private void StudentTeacherInfo(int studentID)
        {
            SecondaryGrid2Lbl.Text = "Contact People";
            SecondaryGrid2Lbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            string sql = "Select (T.TITLE + ' ' + T.FIRSTNAME + ' ' + T.LASTNAME) AS \"Name\", FORMAT(T.PHONE,'(###)-###-####') AS \"PHONE NUMBER\", 'Teacher' as Type from TEACHER T join " +
                " STUDENT S ON T.TEACHERID = S.TEACHER where STUDENTID = " + studentID +
                " UNION " +
                "Select (G.FIRSTNAME + ' ' + G.LASTNAME) AS \"Name\", FORMAT(G.PHONE,'(###)-###-####') AS \"PHONE NUMBER\", 'Guardian' as type from GUARDIAN G join " +
                "STUDENT S ON G.GUARDIANID = S.GUARDIAN where STUDENTID = " + studentID;
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

        private void StudentPermissionInfo(int studentID)
        {
            TertiaryGridLbl.Text = "Student Permission Form";
            TertiaryGridLbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            string sql =
                "SELECT SP.PHOTORELEASE AS \"Photo Release\", SP.EMAILRETENTION AS \" Email Retention\", format((dateadd(hour, -5,SP.DATESIGNED)), 'MM/dd/yyyy hh:mm tt') as \"Signed On \", (G.FIRSTNAME + \' \' + G.LASTNAME) AS \"Signed By\" FROM STUDENTPERMISSIONS SP JOIN " +
                "STUDENT S ON SP.STUDENT = S.STUDENTID JOIN GUARDIAN G ON G.GUARDIANID = S.GUARDIAN WHERE S.STUDENTID = " +
                studentID;
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
                DataColumn dc1 = new DataColumn("Permission Form");
                dt.Columns.Add(dc1);
                DataRow dr1 = dt.NewRow();
                dr1[0] = "No Permission form on file";
                dt.Rows.Add(dr1);
                TertiaryGridView.DataSource = dt;
                TertiaryGridView.DataBind();
            }
        }

        private void EventItinerary()
        {
            TertiaryGridLbl.Text = "Event Itinerary";
            TertiaryGridLbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            int eventID = int.Parse(SelectionDropDown.SelectedValue);
            string sql = "select ET.TITLE," +
                         " right('0' + ltrim(right(convert(varchar,cast(ET.STARTTIME as dateTime), 100), 7)), 7) AS \"Start Time\"," +
                         " ET.LOCATION as Location" +
                         " FROM EVENTTASKS ET JOIN EVENTITINERARY EI ON EI.TASK = ET.TASKID JOIN EVENT E ON E.EVENTID = EI.EVENT" +
                         " WHERE E.EVENTID = " + eventID +
                         " ORDER BY EI.TASK ";
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
            SelectionDropDown.DataSource = null;
            SelectionDropDown.DataBind();
            SelectionDropDown.Visible = false;
            SelectionLbl.Visible = false;
            RunBtn.Visible = false;
            switch (FunctionList.SelectedValue)
            {
                case "1":
                    SelectionLbl.Text = "Event Date: ";
                    EventList();
                    SelectionLbl.Visible = true;
                    SelectionDropDown.Visible = true;
                    RunBtn.Visible = true;
                    break;
                case "4":
                    ReportSearch.ActiveViewIndex = 0;
                    break;
                case "2":
                case "3":
                    ReportSearch.ActiveViewIndex = 1;
                    break;
                case "5":
                    RunBtn.Visible = true;
                    break;
            }
        }

        protected void RunBtn_Click(object sender, EventArgs e)
        {
            EmptyGridView();
            if (FunctionList.SelectedValue == "1")
            {
                if (SelectionDropDown.SelectedValue != String.Empty)
                {
                    ReportSearch.ActiveViewIndex = -1;
                    EventGrid();
                    EventStaffGrid();
                    EventRosterGrid();
                    EventItinerary();
                    FillPanel();
                }
            }
            if (FunctionList.SelectedValue == "2")
            {
                if (SelectionDropDown.SelectedValue != String.Empty)
                {
                    ReportSearch.ActiveViewIndex = -1;
                    StaffGrid();
                    StudentVolunteerData();
                    FillPanel();
                }
            }
            if(FunctionList.SelectedValue == "3")
            {
                if (SelectionDropDown.SelectedValue != String.Empty)
                {
                    ReportSearch.ActiveViewIndex = -1;
                    int studentID = int.Parse(SelectionDropDown.SelectedValue);
                    StudentGridFill(studentID);
                    StudentNotesInfo(studentID);
                    StudentTeacherInfo(studentID);
                    StudentPermissionInfo(studentID);
                    FillPanel();
                }

            }
            if(FunctionList.SelectedValue == "5")
            {
                Response.Redirect("HeatMap.aspx");
            }

            PrintBtn.Visible = true;
            clearBtn.Visible = true;
        }

        private void EmptyGridView()
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
            PrintBtn.Visible = false;
        }

        private void FillPanel()
        {
            printPanel.Controls.Add(ReportTable);
        }

        protected void SearchDate_OnClick(object sender, EventArgs e)
        {
            EmptyGridView();
            if (FunctionList.SelectedValue == "1")
            {
                ReportSearch.ActiveViewIndex = 0;
                SelectionLbl.Text = "Event Date: ";
                EventList();
                SelectionLbl.Visible = true;
                SelectionDropDown.Visible = true;
                RunBtn.Visible = true;
            }
            if (FunctionList.SelectedValue == "4")
            {
                ReportSearch.ActiveViewIndex = 0;
                SelectionLbl.Text = "Event Date: ";
                EventList();
                SelectionLbl.Visible = true;
                SelectionDropDown.Visible = true;
                RunBtn.Visible = true;
            }
        }

        protected void SearchNameBtn_OnClick(object sender, EventArgs e)
        {
            EmptyGridView();
            if (FunctionList.SelectedValue == "2")
            {
                ReportSearch.ActiveViewIndex = 1;
                SelectionLbl.Text = "Staff: ";
                StaffList();
                SelectionLbl.Visible = true;
                SelectionDropDown.Visible = true;
                RunBtn.Visible = true;
            }
            if (FunctionList.SelectedValue == "3")
            {
                ReportSearch.ActiveViewIndex = 1;
                SelectionLbl.Text = "Student: ";
                StudentList();
                SelectionLbl.Visible = true;
                SelectionDropDown.Visible = true;
                RunBtn.Visible = true;
            }
        }

        protected void clearBtn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("AdminReports.aspx");
        }
    }
}