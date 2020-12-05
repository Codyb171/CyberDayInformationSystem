using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;

namespace CyberDayInformationSystem
{
    public partial class StudentEventReport : System.Web.UI.Page
    {
        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Session["TYPE"] != null)
            {
                MasterPageFile = (Session["Master"].ToString());
                if (Session["TYPE"].ToString() != "Coordinator" && Session["TYPE"].ToString() != "Student")
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
            EventItinerary();
            FillPanel();
        }

        private void EventItinerary()
        {
            TertiaryGridLbl.Text = "Event Itinerary";
            TertiaryGridLbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            int eventID = int.Parse(ddlEvents.SelectedValue);
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

        protected void btnRunNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentEventReport.aspx");
        }
        private void FillPanel()
        {
            printPanel.Controls.Add(ReportTable);
        }
    }
}