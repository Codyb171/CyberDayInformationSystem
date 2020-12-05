using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;

namespace CyberDayInformationSystem
{
    public partial class VolunteerEventReport : System.Web.UI.Page
    {
        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Session["TYPE"] != null)
            {
                MasterPageFile = (Session["Master"].ToString());
                if (Session["TYPE"].ToString() != "Student Volunteer" && Session["TYPE"].ToString() != "Staff Volunteer")
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
            // Fill currently available CyberDay Dates
            if (!Page.IsPostBack)
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                SqlConnection connect = new SqlConnection(cs);
                string sqlCommand = "SELECT EVENTID, EVENTDATE from EVENT";
                connect.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(sqlCommand, connect);
                DataTable dataTable = new DataTable();
                adapt.Fill(dataTable);
                ddlEvents.DataSource = dataTable;
                ddlEvents.DataBind();
                ddlEvents.DataTextField = "EVENTDATE";
                ddlEvents.DataValueField = "EVENTID";
                ddlEvents.DataBind();
                ddlEvents.Items.Insert(0, new ListItem(String.Empty));
                connect.Close();
            }

            ScriptManager.RegisterClientScriptInclude(Page, GetType(), "PrintReport.js", "Scripts/src/methods/PrintReport.js");
            if (Page.IsPostBack)
            {
                FillPanel();
            }
        }

        protected void btnGenReport_Click(object sender, EventArgs e)
        {
            ddlEvents.Enabled = false;
            rowGenReportBtn.Visible = false;
            rowBack.Visible = true;
            PrintBtn.Visible = true;

            EventGrid();
            EventStaffGrid();
            EventRosterGrid();
            EventItinerary();
            FillPanel();
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
            if (ddlEvents.SelectedValue != String.Empty)
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                int eventID = int.Parse(ddlEvents.SelectedValue);
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
        private void EventStaffGrid()
        {
            SecondaryGrid1Lbl.Text = "Volunteers";
            SecondaryGrid1Lbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            int eventID = int.Parse(ddlEvents
                .SelectedValue);
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
            int eventID = int.Parse(ddlEvents.SelectedValue);
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


        protected void btnRunNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("VolunteerEventReport.aspx");
        }
        private void FillPanel()
        {
            printPanel.Controls.Add(ReportTable);
        }
    }
}