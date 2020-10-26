using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class TeacherReports : System.Web.UI.Page
    {
        //void Page_PreInit(Object sender, EventArgs e)
        //{
        //    if (Session["TYPE"] != null)
        //    {
        //        this.MasterPageFile = (Session["Master"].ToString());
        //        if (Session["TYPE"].ToString() != "Coordinator" && Session["TYPE"].ToString() != "Teacher")
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
        private int TeacherID;
        private int School;
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptInclude(this.Page, this.GetType(), "PrintReport.js", "Scripts/src/methods/PrintReport.js");
            if (Page.IsPostBack == true)
            {
                FillPanel();
            }

            if (Session["TYPE"] != null)
            {
                if (Session["TYPE"].ToString() == "Teacher")
                {
                    TeacherID = int.Parse((string)Session["ID"]);
                    School = int.Parse((string)Session["SCHOOL"]);
                }
                else
                {
                    TeacherID = 0;
                    School = 0;
                }
            }
        }
        public void EventList()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
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
        public void StudentList()
        {
            SelectionDropDown.Items.Clear();
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string command = "select STUDENTID as ID, (FIRSTNAME + ' ' + LASTNAME) as NAME from STUDENT";
            if (TeacherID != 0)
            {
                command += " where Teacher = " + TeacherID;
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
        public void StudentGridFill(int StudentID)
        {
            SelectedGridLbl.Text = "Student Data";
            SelectedGridLbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
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
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
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
            SecondaryGrid2Lbl.Text = "Guardian";
            SecondaryGrid2Lbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            string sql = "Select (G.FIRSTNAME + ' ' + G.LASTNAME) AS \"Name\", FORMAT(G.PHONE,'(###)-###-####') AS \"PHONE NUMBER\", 'Guardian' as type from GUARDIAN G join " +
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
                DataColumn dc1 = new DataColumn("Guardian");
                dt.Columns.Add(dc1);
                DataRow dr1 = dt.NewRow();
                dr1[0] = "No Guardian on file for Student";
                dt.Rows.Add(dr1);
                SecondaryGridView2.DataSource = dt;
                SecondaryGridView2.DataBind();
            }
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
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
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
        public void EventItinerary()
        {

            TertiaryGridLbl.Text = "Event Itinerary";
            TertiaryGridLbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            int eventID = int.Parse(SelectionDropDown.SelectedValue);
            string sql = "select ET.TITLE, (V.FIRSTNAME + ' ' + V.LASTNAME) AS INSTRUCTOR," +
                         " right(convert(varchar(20),cast(stuff(right('0000' + convert(varchar(4),ET.STARTTIME),4),3,0,':')as datetime),100),7) AS \"START\"," +
                         " right(convert(varchar(20), cast(stuff(right('0000' + convert(varchar(4), ET.ENDTIME), 4), 3, 0, ':') as datetime), 100), 7) AS \"END\"" +
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
        protected void FunctionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmptyGridView();
            if (FunctionList.SelectedValue == "1")
            {
                EventList();
                SelectionLbl.Text = "Event: ";
                SelectionLbl.Visible = true;
                SelectionDropDown.Visible = true;
            }

            if (FunctionList.SelectedValue == "2")
            {
                StudentList();
                SelectionLbl.Text = "Student: ";
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
                EventItinerary();
                FillPanel();
            }
            if (FunctionList.SelectedValue == "2")
            {
                var studentId = int.Parse(SelectionDropDown.SelectedValue);
                StudentGridFill(studentId);
                StudentNotesInfo(studentId);
                StudentTeacherInfo(studentId);
                FillPanel();
            }

            PrintBtn.Visible = true;
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
            PrintBtn.Visible = false;
        }

        public void FillPanel()
        {
            printPanel.Controls.Add(ReportTable);
        }
    }
}