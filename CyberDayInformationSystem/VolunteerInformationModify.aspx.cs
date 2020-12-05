using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class VolunteerInformationModify : System.Web.UI.Page
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
        private int volID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TYPE"] != null)
            {
                if (Session["TYPE"].ToString() == "Student Volunteer" && Session["TYPE"].ToString() == "Staff Volunteer")
                {
                    volID = GetId();
                }
                else
                {
                    volID = 0;
                }
            }

            if (!Page.IsPostBack)
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                var connection = new SqlConnection(cs);
                string sql =
                    "Select v.FIRSTNAME, v.LASTNAME, v.EMAILADD, v.PHONE, v.MEALTICKET FROM VOLUNTEER v" +
                    " WHERE STAFFID = @STAFFID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@STAFFID", volID);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    firstNameTxt.Text = dataReader["FIRSTNAME"].ToString();
                    LastNameTxt.Text = dataReader["LASTNAME"].ToString();
                    EmailTxt.Text = dataReader["EMAILADD"].ToString();
                    PhoneTxt.Text = dataReader["PHONE"].ToString();
                    
                    if(dataReader["MEALTICKET"].ToString() == "Yes")
                    {
                        MealTicketDDL.SelectedIndex = 1;
                    }
                    if (dataReader["MEALTICKET"].ToString() == "No")
                    {
                        MealTicketDDL.SelectedIndex = 2;
                    }
                }
                dataReader.Close();

                if (Session["TYPE"].ToString() == "Student Volunteer")
                {
                    RowStuCurMajor.Visible = true;
                    RowStuVolMajor.Visible = true;
                    RowStuCurMinor.Visible = true;
                    RowStuVolMinor.Visible = true;
                    RowCurStuOrg.Visible = true;
                    RowStuVolOrg.Visible = true;
                    RowStuPrevVol.Visible = true;

                    string stuSql = "Select s.MAJOR, s.MINOR, s.ORGANIZATION, s.PREVIOUSVOLUNTEER FROM STUDENTVOLUNTEER s" +
                        " WHERE VOLUNTEERID = @VOLID";
                    SqlCommand stuCmd = new SqlCommand(stuSql, connection);
                    stuCmd.Parameters.AddWithValue("@VOLID", volID);
                    SqlDataReader dr = stuCmd.ExecuteReader();

                    if (dr.Read())
                    {
                        CurMajorLbl.Text = "Current Major: " + dr["MAJOR"].ToString();
                        CurMinorLbl.Text = "Current Minor: " + dr["MINOR"].ToString();
                        CurOrgLbl.Text = "Current Organization: " + dr["ORGANIZATION"].ToString();

                        if (dataReader["PREVIOUSVOLUNTEER"].ToString() == "Yes")
                        {
                            PrevVolDDL.SelectedIndex = 1;
                        }
                        if (dataReader["PREVIOUSVOLUNTEER"].ToString() == "No")
                        {
                            PrevVolDDL.SelectedIndex = 2;
                        }
                    }
                }
            }
        }

        protected int GetId()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            var connection = new SqlConnection(cs);
            string sql = "Select STAFFID FROM VOLUNTEER WHERE CONCAT(FIRSTNAME, ' ', LASTNAME) LIKE '%" + Session["NAME"] + "%'";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            int id = 0;

            if (dataReader.Read())
            {
                id = int.Parse(dataReader["STAFFID"].ToString());
            }
            connection.Close();
            return id;

        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            UpdateAuthDB();
            string CS = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            var authcon = new SqlConnection(CS);
            authcon.Open();

            string firstName = HttpUtility.HtmlEncode(firstNameTxt.Text);
            string lastName = HttpUtility.HtmlEncode(LastNameTxt.Text);
            string email = HttpUtility.HtmlEncode(EmailTxt.Text);
            string maskedPhone = HttpUtility.HtmlEncode(PhoneTxt.Text);
            string result = Regex.Replace(maskedPhone, @"[^0-9]", "");
            SqlInt64 phone = SqlInt64.Parse(result);

            string sql = "Update VOLUNTEER set FIRSTNAME = @FIRSTNAME, LASTNAME = @LASTNAME, PHONE = @PHONE, EMAILADD = @EMAIL, MEALTICKET = @MEALTICKET WHERE STAFFID = @ID";

            var command = new SqlCommand(sql, authcon);
            command.Parameters.AddWithValue("@FIRSTNAME", firstName);
            command.Parameters.AddWithValue("@LASTNAME", lastName);
            command.Parameters.AddWithValue("@PHONE", phone);
            command.Parameters.AddWithValue("@EMAIL", email);
            command.Parameters.AddWithValue("@MEALTICKET", MealTicketDDL.SelectedValue);
            command.Parameters.AddWithValue("@ID", volID);
            command.ExecuteNonQuery();

            string stuVol = "Update STUDENTVOLUNTEER set MAJOR = @MAJOR, MINOR = @MINOR, ORGANIZATION = @ORG, PREVIOUSVOLUNTTER = @PREV WHERE VOLUNTEERID = @ID";
            var stuCmd = new SqlCommand(stuVol, authcon);

            stuCmd.Parameters.AddWithValue("@MAJOR", MajorDropDown.SelectedValue);
            stuCmd.Parameters.AddWithValue("@MINOR", MinorDropDown.SelectedValue);
            stuCmd.Parameters.AddWithValue("@ORG", OrgDropDown.SelectedValue);
            stuCmd.Parameters.AddWithValue("@PREV", PrevVolDDL.SelectedValue);
            stuCmd.Parameters.AddWithValue("@ID", volID);
            stuCmd.ExecuteNonQuery();

            authcon.Close();
            UpdateSuccessfulLbl.Text = "Information updated successfully!";
        }

        public void UpdateAuthDB()
        {
            string CS = ConfigurationManager.ConnectionStrings["AUTH"].ConnectionString;
            var authcon = new SqlConnection(CS);
            authcon.Open();

            string firstName = HttpUtility.HtmlEncode(firstNameTxt.Text);
            string lastName = HttpUtility.HtmlEncode(LastNameTxt.Text);
            string email = HttpUtility.HtmlEncode(EmailTxt.Text);
            string sql =
                "Update Users set FIRSTNAME = @FIRSTNAME, LASTNAME = @LASTNAME, USERNAME = @USERNAME, where CONCAT(FIRSTNAME, ' ', LASTNAME) LIKE '%" +
                Session["NAME"] + "%'";
            var command = new SqlCommand(sql, authcon);
            command.Parameters.AddWithValue("@FIRSTNAME", firstName);
            command.Parameters.AddWithValue("@LASTNAME", lastName);
            command.Parameters.AddWithValue("@USERNAME", email);
            command.ExecuteNonQuery();
            authcon.Close();
            Session["NAME"] = firstName + " " + lastName;
        }
    }
}