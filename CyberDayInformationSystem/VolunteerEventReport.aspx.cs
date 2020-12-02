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

        private void EventGrid()
        {
            SelectedGridLbl.Text = "Event Data";
            SelectedGridLbl.Visible = true;
            if (ddlEvents.SelectedValue != String.Empty)
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                int eventID = int.Parse(ddlEvents.SelectedValue);
                string sql = "Select 'CyberDay' as \"Event Name\", EV.EVENTDATE as \"Event Date\" where EVENTID = " + eventID;
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
            int eventID = int.Parse(ddlEvents.SelectedValue);
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

        private void EventItinerary()
        {
            TertiaryGridLbl.Text = "Event Itinerary";
            TertiaryGridLbl.Visible = true;
            int eventID = int.Parse(ddlEvents.SelectedValue);
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string command =
                "select EI.filename from eventitinerary EI join event E on E.EVENTITINERARY = EI.ITINERARYID WHERE E.EVENTID = " +
                eventID;
            connection.Open();
            SqlCommand sqlCom = new SqlCommand(command, connection);
            SqlDataReader reader = sqlCom.ExecuteReader();
            if (reader.Read())
            {
                string fileName = reader.GetString(0);
                string filePath = Server.MapPath("~/Uploads/") + fileName;
                //Open the Excel file using ClosedXML.
                using (XLWorkbook workBook = new XLWorkbook(filePath))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    //Create a new DataTable.
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
                    TertiaryGridView.DataSource = dt;
                    TertiaryGridView.DataBind();
                }
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