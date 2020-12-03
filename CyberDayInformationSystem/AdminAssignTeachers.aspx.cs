using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class AdminAssignTeachers : System.Web.UI.Page
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
                ddlEventDates.DataSource = dataTable;
                ddlEventDates.DataBind();
                ddlEventDates.DataTextField = "EVENTDATE";
                ddlEventDates.DataValueField = "EVENTID";
                ddlEventDates.DataBind();
                ddlEventDates.Items.Insert(0, new ListItem(String.Empty));

                ddlDates.DataSource = dataTable;
                ddlDates.DataBind();
                ddlDates.DataTextField = "EVENTDATE";
                ddlDates.DataValueField = "EVENTID";
                ddlDates.DataBind();
                ddlDates.Items.Insert(0, new ListItem(String.Empty));
                connect.Close();
            }
        }

        protected void btnAssignTeach_Click(object sender, EventArgs e)
        {
            SelectedFunction.ActiveViewIndex = 0;
            rowFunctionBtn.Visible = false;
            rowReset.Visible = true;
        }

        protected void btnDelTeach_Click(object sender, EventArgs e)
        {
            SelectedFunction.ActiveViewIndex = 1;
            rowFunctionBtn.Visible = false;
            rowRestart.Visible = true;
        }

        private static int _eventID;
        protected void btnReset_Click(object sender, EventArgs e)
        {
            SelectedFunction.ActiveViewIndex = -1;
            rowFunctionBtn.Visible = true;
            ddlDates.ClearSelection();
            ddlEventDates.ClearSelection();
            ddlAssignTeachers.ClearSelection();
            ddlRemoveTeacher.ClearSelection();
        }

        protected void btnAssignNext_Click(object sender, EventArgs e)
        {
            ddlEventDates.Enabled = false;
            rowAssignNextBtn.Visible = false;

            _eventID = Convert.ToInt32(ddlEventDates.SelectedItem.Value);

            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connect = new SqlConnection(cs);
            connect.Open();

            string getEdu = "SELECT FIRSTNAME + ' ' + LASTNAME as NAME, TEACHERID from TEACHER";

            SqlDataAdapter adapt = new SqlDataAdapter(getEdu, connect);
            DataTable dataTable = new DataTable();
            adapt.Fill(dataTable);
            ddlAssignTeachers.DataSource = dataTable;
            ddlAssignTeachers.DataBind();
            ddlAssignTeachers.DataTextField = "NAME";
            ddlAssignTeachers.DataValueField = "TEACHERID";
            ddlAssignTeachers.DataBind();
            ddlAssignTeachers.Items.Insert(0, new ListItem(String.Empty));
            connect.Close();

            rowAssignSelTeach.Visible = true;
            rowAssignSubmitBtn.Visible = true;
        }

        protected void btnAssignBack_Click(object sender, EventArgs e)
        {
            rowAssignSelTeach.Visible = false;
            rowAssignSubmitBtn.Visible = false;

            ddlEventDates.Enabled = true;
            rowAssignNextBtn.Visible = true;
        }

        protected void btnAssignSubmit_Click(object sender, EventArgs e)
        {
            int selTeachID = Convert.ToInt32(ddlAssignTeachers.SelectedItem.Value);

            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connect = new SqlConnection(cs);

            string chkDupe = "SELECT COUNT(*) FROM EVENTROSTER where CHAPERONE = @ID";
            SqlCommand chkDupeCmd = new SqlCommand(chkDupe, connect);
            connect.Open();

            chkDupeCmd.Parameters.AddWithValue("@ID", selTeachID);

            string getTeachName = "SELECT FIRSTNAME + ' ' + LASTNAME as NAME from TEACHER WHERE TEACHERID = @ID";
            SqlCommand getTeachCmd = new SqlCommand(getTeachName, connect);
            getTeachCmd.Parameters.AddWithValue("@ID", selTeachID);

            string teachName = (string)getTeachCmd.ExecuteScalar();

            int countDupe = (int)chkDupeCmd.ExecuteScalar();

            if (countDupe > 0)
            {
                lblAssignStatus.Text = "Teacher " + teachName + " is already registered for this day";
            }
            else
            {
                string addRoster = "INSERT INTO EVENTROSTER (EVENT, CHAPERONE) VALUES(@EVENTID, @CHAPID)";
                SqlCommand toRoster = new SqlCommand(addRoster, connect);

                toRoster.Parameters.AddWithValue("@EVENTID", _eventID);
                toRoster.Parameters.AddWithValue("@CHAPID", selTeachID);

                int result = (int)toRoster.ExecuteNonQuery();

                if (result < 0)
                {
                    lblAssignStatus.Text = "There was an unexpected error adding " + teachName;
                }    
                if (result > 0)
                {
                    lblAssignStatus.Text = "Teacher " + teachName + " added successfully";
                }    
            }
        }

        protected void btnRemoveNext_Click(object sender, EventArgs e)
        {
            ddlDates.Enabled = false;
            rowRemoveNextBtn.Visible = false;

            _eventID = Convert.ToInt32(ddlDates.SelectedItem.Value);

            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connect = new SqlConnection(cs);
            connect.Open();

            string getEdu = "SELECT FIRSTNAME + ' ' + LASTNAME as NAME, TEACHERID from TEACHER";

            SqlDataAdapter adapt = new SqlDataAdapter(getEdu, connect);
            DataTable dataTable = new DataTable();
            adapt.Fill(dataTable);
            ddlRemoveTeacher.DataSource = dataTable;
            ddlRemoveTeacher.DataBind();
            ddlRemoveTeacher.DataTextField = "NAME";
            ddlRemoveTeacher.DataValueField = "TEACHERID";
            ddlRemoveTeacher.DataBind();
            ddlRemoveTeacher.Items.Insert(0, new ListItem(String.Empty));
            connect.Close();

            rowRemoveSelTeach.Visible = true;
            rowRemoveSubmitBtn.Visible = true;
        }

        protected void btnRemoveBack_Click(object sender, EventArgs e)
        {
            rowRemoveSelTeach.Visible = false;
            rowRemoveSubmitBtn.Visible = false;

            ddlDates.Enabled = true;
            rowRemoveNextBtn.Visible = true;
        }

        protected void btnRemoveSubmit_Click(object sender, EventArgs e)
        {
            int selTeachID = Convert.ToInt32(ddlRemoveTeacher.SelectedItem.Value);

            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connect = new SqlConnection(cs);
            connect.Open();

            string getTeachName = "SELECT FIRSTNAME + ' ' + LASTNAME as NAME from TEACHER WHERE TEACHERID = @ID";
            SqlCommand getTeachCmd = new SqlCommand(getTeachName, connect);
            getTeachCmd.Parameters.AddWithValue("@ID", selTeachID);

            string teachName = (string)getTeachCmd.ExecuteScalar();

            string removeRoster = "DELETE from EVENTROSTER where CHAPERONE = @CHAPID and EVENT = @EVENTID";
            SqlCommand delRoster = new SqlCommand(removeRoster, connect);

            delRoster.Parameters.AddWithValue("@EVENTID", _eventID);
            delRoster.Parameters.AddWithValue("@CHAPID", selTeachID);

            int result = (int)delRoster.ExecuteNonQuery();

            if (result < 0)
            {
                lblRemoveStatus.Text = "Teacher " + teachName + " was not registered for this event date.";
            }
            if (result > 0)
            {
                lblRemoveStatus.Text = "Teacher " + teachName + " removed successfully";
            }
        }
    }
}