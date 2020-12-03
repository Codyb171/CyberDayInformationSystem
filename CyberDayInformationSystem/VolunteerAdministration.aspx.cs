using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class VolunteerAdministration : Page
    {
        //void Page_PreInit(Object sender, EventArgs e)
        //{
        //    if (Session["TYPE"] != null)
        //    {
        //        MasterPageFile = (Session["Master"].ToString());
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
            // Fill currently available CyberDay Dates
            if(!Page.IsPostBack)
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
        protected void btnAssignVol_Click(object sender, EventArgs e)
        {
            SelectedFunction.ActiveViewIndex = 0;
            rowFunctionBtn.Visible = false;
            rowBtnReset.Visible = true;
        }

        protected void btnDelVol_Click(object sender, EventArgs e)
        {
            SelectedFunction.ActiveViewIndex = 1;
            rowFunctionBtn.Visible = false;
            rowReset.Visible = true;
        }

        private static int _eventID;

        protected void btnSelDateNext_Click(object sender, EventArgs e)
        {
            // Change visiblity to move to next step. Display selected value.
            ddlEventDates.Enabled = false;
            rowDateNextBtn.Visible = false;

            _eventID = Convert.ToInt32(ddlEventDates.SelectedItem.Value);

            // Get volunteers
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connect = new SqlConnection(cs);
            string sqlCommand = "SELECT FIRSTNAME + ' ' + LASTNAME as NAME, STAFFID from VOLUNTEER";
            connect.Open();
            SqlDataAdapter adapt = new SqlDataAdapter(sqlCommand, connect);
            DataTable dataTable = new DataTable();
            adapt.Fill(dataTable);
            ddlVols.DataSource = dataTable;
            ddlVols.DataBind();
            ddlVols.DataTextField = "NAME";
            ddlVols.DataValueField = "STAFFID";
            ddlVols.DataBind();
            ddlVols.Items.Insert(0, new ListItem(String.Empty));
            connect.Close();

            rowSelVol.Visible = true;
            rowSubmitBtn.Visible = true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Write to DB. Display confirmation once complete.
            int selVolID = Convert.ToInt32(ddlVols.SelectedItem.Value);

            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connect = new SqlConnection(cs);

            // Checks if already on roster
            string dupeCheck = "SELECT COUNT(*) FROM EVENTSTAFF WHERE STAFF = " + selVolID;
            SqlCommand chkDupe = new SqlCommand(dupeCheck, connect);
            connect.Open();

            string getNameCmd = "SELECT FIRSTNAME + ' ' + LASTNAME as NAME from VOLUNTEER WHERE STAFFID = @ID";
            SqlConnection getCon = new SqlConnection(cs);
            SqlCommand getName = new SqlCommand(getNameCmd, getCon);
            getName.Parameters.AddWithValue("@ID", selVolID);
            getCon.Open();

            string volName = (string)getName.ExecuteScalar();


            int dupeCount = (int)chkDupe.ExecuteScalar();

            if (dupeCount > 0)
            {
                lblStatus.Text = "Volunteer " + volName + " is already registered for this day!";
            }
            else
            {
                string addRoster = "INSERT INTO EVENTSTAFF VALUES(" + _eventID + ", " + selVolID + ")";
                SqlCommand toRoster = new SqlCommand(addRoster, connect);

                int result = toRoster.ExecuteNonQuery();


                if (result < 0)
                {
                    lblStatus.Text = "There was an unexpected error adding " + volName + ".";
                }
                if (result > 0)
                {
                    lblStatus.Text = "Volunteer " + volName + " added successfully!";
                }
            }

            connect.Close();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            rowSelVol.Visible = false;
            rowSubmitBtn.Visible = false;

            ddlEventDates.Enabled = true;
            rowDateNextBtn.Visible = true;
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            // Change visiblity to move to next step. Display selected value.
            ddlDates.Enabled = false;
            rowNext.Visible = false;

            _eventID = Convert.ToInt32(ddlDates.SelectedItem.Value);

            // Get volunteers
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connect = new SqlConnection(cs);
            string sqlCommand = "SELECT (V.FIRSTNAME + ' ' + V.LASTNAME) as NAME, V.STAFFID from VOLUNTEER V " +
                "JOIN EVENTSTAFF ES on ES.STAFF = V.STAFFID WHERE ES.EVENT = @ToEdit";
            connect.Open();
            SqlCommand getCmd = new SqlCommand(sqlCommand, connect);
            getCmd.Parameters.AddWithValue("@ToEdit", _eventID);
            SqlDataAdapter adapt = new SqlDataAdapter(getCmd);
            DataTable dataTable = new DataTable();
            adapt.Fill(dataTable);
            ddlVols.DataSource = dataTable;
            ddlVols.DataBind();
            ddlVols.DataTextField = "NAME";
            ddlVols.DataValueField = "STAFFID";
            ddlVols.DataBind();
            ddlVols.Items.Insert(0, new ListItem(String.Empty));
            connect.Close();

            rowVol.Visible = true;
            rowBtns.Visible = true;
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            rowVol.Visible = false;
            rowBtns.Visible = false;

            ddlDates.Enabled = true;
            rowNext.Visible = true;
        }
        
        protected void btnUnassign_Click(object sender, EventArgs e)
        {
            int selVolID = Convert.ToInt32(ddlVols.SelectedItem.Value);
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connect = new SqlConnection(cs);
            string sqlUnassign = "DELETE from EVENTSTAFF where STAFF = @VOLID and EVENT = @EVENTID";
            connect.Open();
            SqlCommand delCmd = new SqlCommand(sqlUnassign, connect);
            delCmd.Parameters.AddWithValue("@VOLID", selVolID);
            delCmd.Parameters.AddWithValue("@EVENTID", _eventID);
            int result = delCmd.ExecuteNonQuery();

            string getNameCmd = "SELECT FIRSTNAME + ' ' + LASTNAME as NAME from VOLUNTEER WHERE STAFFID = @ID";
            SqlConnection getCon = new SqlConnection(cs);
            SqlCommand getName = new SqlCommand(getNameCmd, getCon);
            getName.Parameters.AddWithValue("@ID", selVolID);
            getCon.Open();

            string volName = (string)getName.ExecuteScalar();


            if (result < 0)
            {
                lblStatus.Text = "There was an unexpected error removing " + volName + ".";
            }
            if (result > 0)
            {
                lblStatus.Text = "Volunteer " + volName + " removed successfully!";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            SelectedFunction.ActiveViewIndex = -1;
            rowFunctionBtn.Visible = true;
            ddlDates.ClearSelection();
            ddlEventDates.ClearSelection();
            ddlVol.ClearSelection();
            ddlVols.ClearSelection();
        }
    }
}