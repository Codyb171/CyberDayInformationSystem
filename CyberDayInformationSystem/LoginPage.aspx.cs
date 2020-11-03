using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace CyberDayInformationSystem
{
    public partial class LoginPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            var user = HttpUtility.HtmlEncode(UsernameTxt.Text);
            var pass = HttpUtility.HtmlEncode(PasswordTxt.Text);
            var cs = ConfigurationManager.ConnectionStrings["AUTH"].ConnectionString;
            var connection = new SqlConnection(cs);
            var loginCommand = new SqlCommand();
            SqlDataReader loginResults;
            connection.Open();
            loginCommand.Connection = connection;
            loginCommand.CommandType = CommandType.StoredProcedure;
            loginCommand.CommandText = "UserLogin";
            loginCommand.Parameters.AddWithValue("@username", user);
            loginResults = loginCommand.ExecuteReader();
            if (loginResults.Read())
            {
                var PassHash = loginResults["PASSWORDHASH"].ToString();
                if (PasswordHash.ValidatePassword(pass, PassHash))
                {
                    Session.Add("USER", loginResults["USERNAME"].ToString());
                    Session.Add("NAME", (loginResults["FIRSTNAME"].ToString() + " " + loginResults["LASTNAME"].ToString()));
                    var type = loginResults["USERTYPE"].ToString();
                    Session.Add("TYPE", type);

                    if (type == "Teacher")
                    {
                        Session.Add("Master", "~/Teacher.Master");
                    }
                    else if (type == "Coordinator")
                    {
                        Session.Add("Master", "~/Admin.Master");
                    }
                    else if (type == "Student Volunteer" || type == "Staff Volunteer")
                    {
                        Session.Add("Master", "~/Volunteer.Master");
                    }
                    else
                    {
                        Session.Add("Master", "~/Site.Master");
                    }
                    
                    getInfo(type);
                }
                else
                { 
                    LoginStat.Visible = true;
                }
               
            }
        }

        protected void NewUserBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserCreation.aspx");
        }


        public void getInfo(string type)
        {
            var sql = "Select ";
            var cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            var connection = new SqlConnection(cs);
            SqlCommand command;

            if (type == "Teacher")
            {
                sql += "TEACHERID, SCHOOL FROM TEACHER";
            }

            if (type == "Student Volunteer" || type == "Staff Volunteer")
            {
                sql += "STAFFID from VOLUNTEER";
            }

            if (type == "Coordinator")
            {
                sql += "STAFFID from VOLUNTEER";
            }
            sql += " where EMAILADD Like @email";
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", Session["USER"].ToString());
            connection.Open();
            var dataReader = command.ExecuteReader();
            if(type == "Teacher")
            {
                if (dataReader.Read()) Session.Add("ID", dataReader["TEACHERID"].ToString());
                Session.Add("SCHOOL", dataReader["SCHOOL"]);
                Response.Redirect("TeacherDashboard.aspx");
            }

            if (type == "Student Volunteer" || type == "Staff Volunteer")
            {
                if (dataReader.Read()) Session.Add("ID", dataReader["STAFFID"].ToString());
                Response.Redirect("VolunteerDashboard.aspx");
            }

            if (type == "Coordinator")
            {
                if (dataReader.Read()) Session.Add("ID", dataReader["STAFFID"].ToString());
                Response.Redirect("AdminDashboard.aspx");
            }
        }
    }
}