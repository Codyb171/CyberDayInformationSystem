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
        public void StudentList()
        {
            SelectionDropDown.Items.Clear();
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string command = "select STUDENTID as ID, (FIRSTNAME + ' ' + LASTNAME) as NAME from STUDENT " /*+*/
                             /*"where Teacher = " + TeacherID*/;
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
            SecondaryGrid2Lbl.Text = "Contact People";
            SecondaryGrid2Lbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
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
        protected void FunctionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmptyGridView();
            if (FunctionList.SelectedValue == "1")
            {
                SelectionLbl.Text = "Event: ";
                SelectionLbl.Visible = true;
                SelectionDropDown.Visible = true;
            }

            if (FunctionList.SelectedValue == "2")
            {
                SelectionLbl.Text = "Staff: ";
                SelectionLbl.Visible = true;
                SelectionDropDown.Visible = true;
            }

            if (FunctionList.SelectedValue == "3")
            {
                SelectionLbl.Text = "Student: ";
                SelectionLbl.Visible = true;
                SelectionDropDown.Visible = true;
            }

            if (FunctionList.SelectedValue == "4")
            {
                SelectionLbl.Text = "Event Date: ";
                SelectionLbl.Visible = true;
                SelectionDropDown.Visible = true;
            }
        }

        protected void RunBtn_Click(object sender, EventArgs e)
        {
            EmptyGridView();
            if (FunctionList.SelectedValue == "1")
            {
            }
            if (FunctionList.SelectedValue == "2")
            {
                var studentId = int.Parse(SelectionDropDown.SelectedValue);
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