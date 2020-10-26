using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class VolunteerAdministration : System.Web.UI.Page
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
                connect.Close();
            }
        }

        private static int eventID;

        protected void btnSelDateNext_Click(object sender, EventArgs e)
        {
            // Change visiblity to move to next step. Display selected value.
            rowSelDate.Visible = false;
            rowDateNextBtn.Visible = false;

            rowSelectedDate.Visible = true;
            lblSelectedDate.Text = "Selected Date: " + Convert.ToString(ddlEventDates.SelectedItem.Text);

            eventID = Convert.ToInt32(ddlEventDates.SelectedItem.Value);

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
            SqlCommand chkDupe = new SqlCommand(dupeCheck);
            connect.Open();

            int dupeCount = (int)chkDupe.ExecuteNonQuery();

            if (dupeCount > 0)
            {
                lblStatus.Text = "Volunteer is already registered for this day!";
            }
            else
            {
                string addRoster = "INSERT INTO EVENTSTAFF VALUES(" + eventID + ", " + selVolID + ")";
                SqlCommand toRoster = new SqlCommand(addRoster);

                int result = toRoster.ExecuteNonQuery();

                if (result < 0)
                {
                    lblStatus.Text = "There was an unexpected error.";
                }
                if (result > 0)
                {
                    lblStatus.Text = "Volunteer added successfully!";
                }
            }

            connect.Close();
        }
    }
}