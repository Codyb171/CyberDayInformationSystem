using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;

namespace CyberDayInformationSystem
{
    public partial class TeacherReports : Page
    {
        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Session["TYPE"] != null)
            {
                MasterPageFile = (Session["Master"].ToString());
                if (Session["TYPE"].ToString() != "Coordinator" && Session["TYPE"].ToString() != "Teacher")
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
        private int _teacherID;
        private int _studentID;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptInclude(Page, GetType(), "PrintReport.js", "Scripts/src/methods/PrintReport.js");
            if (Page.IsPostBack)
            {
                FillPanel();
                if (Session["TEACHERREPORTID"] != null)
                {
                    _studentID = int.Parse(Session["TEACHERREPORTID"].ToString());
                }
            }

            if (Session["TYPE"] != null)
            {
                if (Session["TYPE"].ToString() == "Teacher")
                {
                    _teacherID = int.Parse(Session["ID"].ToString());
                    
                }
                else
                {
                    _teacherID = 0;
                }
            }
        }

        private void EventList()
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

        protected void SearchByTagButton_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            var connection = new SqlConnection(cs);
            DataTable dt = new DataTable();
            connection.Open();
            string sql =
                "Select S.STUDENTID, S.FIRSTNAME, S.LASTNAME, S.AGE, S.GENDER, S.PREVIOUSATTENDEE, S.MEALTICKET," +
                " (T.TITLE + ' ' + T.FIRSTNAME + ' ' + T.LASTNAME) AS \"TEACHER\", SC.NAME FROM STUDENT S LEFT JOIN TEACHER T on S.TEACHER = T.TEACHERID" +
                " join SCHOOL SC ON SC.SCHOOLID = S.SCHOOL WHERE";
            if (_teacherID != 0)
            {
                sql += " TEACHER = @TEACHER AND";
            }
            try
            {
                if (SearchByTagFN.Text != String.Empty && SearchByTagLN.Text != String.Empty)
                {
                    sql += " S.FIRSTNAME LIKE @FIRST AND S.LASTNAME LIKE @LAST";
                    SqlDataAdapter select = new SqlDataAdapter(sql, connection);
                    if (_teacherID != 0)
                    {
                        select.SelectCommand.Parameters.AddWithValue("@TEACHER", _teacherID);
                    }
                    select.SelectCommand.Parameters.AddWithValue("@FIRST",
                        "%" + HttpUtility.HtmlEncode(SearchByTagFN.Text) + "%");
                    select.SelectCommand.Parameters.AddWithValue("@LAST",
                        "%" + HttpUtility.HtmlEncode(SearchByTagLN.Text) + "%");
                    
                    select.Fill(dt);
                }
                else if (SearchByTagFN.Text != String.Empty)
                {
                    sql += " S.FIRSTNAME LIKE @FIRST";
                    SqlDataAdapter select = new SqlDataAdapter(sql, connection);
                    if (_teacherID != 0)
                    {
                        select.SelectCommand.Parameters.AddWithValue("@TEACHER", _teacherID);
                    }
                    select.SelectCommand.Parameters.AddWithValue("@FIRST",
                        "%" + HttpUtility.HtmlEncode(SearchByTagFN.Text) + "%");
                    select.Fill(dt);
                }
                else
                {
                    sql += " S.LASTNAME LIKE @LAST";
                    SqlDataAdapter select = new SqlDataAdapter(sql, connection);
                    if (_teacherID != 0)
                    {
                        select.SelectCommand.Parameters.AddWithValue("@TEACHER", _teacherID);
                    }
                    select.SelectCommand.Parameters.AddWithValue("@LAST",
                        "%" + HttpUtility.HtmlEncode(SearchByTagLN.Text) + "%");
                    select.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    studentModifyDtl.DataSource = dt;
                    studentModifyDtl.DataBind();
                }
                else
                {
                    dt = new DataTable();
                    DataColumn dc1 = new DataColumn("No Data");
                    dt.Columns.Add(dc1);
                    DataRow dr1 = dt.NewRow();
                    dr1[0] = "No Students found with that data";
                    dt.Rows.Add(dr1);
                    studentModifyDtl.DataSource = dt;
                    studentModifyDtl.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                connection.Close();
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
            SecondaryGrid2Lbl.Text = "Guardian";
            SecondaryGrid2Lbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            string sql = "Select (G.FIRSTNAME + ' ' + G.LASTNAME) AS \"Name\", FORMAT(G.PHONE,'(###)-###-####') AS \"PHONE NUMBER\", 'Guardian' as type from GUARDIAN G join " +
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
                DataColumn dc1 = new DataColumn("Guardian");
                dt.Columns.Add(dc1);
                DataRow dr1 = dt.NewRow();
                dr1[0] = "No Guardian on file for Student";
                dt.Rows.Add(dr1);
                SecondaryGridView2.DataSource = dt;
                SecondaryGridView2.DataBind();
            }
        }
        private void EventGrid()
        {
            SelectedGridLbl.Text = "Event Data";
            SelectedGridLbl.Visible = true;
            if (SelectionDropDown.SelectedValue != String.Empty)
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                int eventID = int.Parse(SelectionDropDown.SelectedValue);
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
            if (FunctionList.SelectedValue == "1")
            {
                EventList();
                SearchByView.ActiveViewIndex = 0;
                SelectionLbl.Text = "Event: ";
                SelectionLbl.Visible = true;
                SelectionDropDown.Visible = true;
            }

            if (FunctionList.SelectedValue == "2")
            {
                SearchByView.ActiveViewIndex = 1;
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
                _studentID = int.Parse(studentModifyDtl.DataKey[0].ToString());
                Session.Add("@TEACHERREPORTID", _studentID);
                EmptyGridView();
                StudentGridFill(_studentID);
                StudentNotesInfo(_studentID);
                StudentTeacherInfo(_studentID);
                FillPanel();
            }

            PrintBtn.Visible = true;
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
        protected void StudentModifyDtl_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {
            studentModifyDtl.PageIndex = e.NewPageIndex;
            studentModifyDtl.DataBind();
            SearchByTagButton_Click(sender, e);
        }
        private void FillPanel()
        {
            printPanel.Controls.Add(ReportTable);
        }
    }
}