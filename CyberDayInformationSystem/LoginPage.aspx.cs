using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            string user = HttpUtility.HtmlEncode(UsernameTxt.Text);
            string pass = HttpUtility.HtmlEncode(PasswordTxt.Text);
            string cs = ConfigurationManager.ConnectionStrings["AUTH"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand loginCommand = new SqlCommand();
            SqlDataReader loginResults;
            connection.Open();
            loginCommand.Connection = connection;
            loginCommand.CommandType = CommandType.StoredProcedure;
            loginCommand.CommandText = "UserLogin";
            loginCommand.Parameters.AddWithValue("@username", user);
            loginResults = loginCommand.ExecuteReader();
            if (loginResults.Read())
            {
                string PassHash = loginResults["PASSWORDHASH"].ToString();
                if (PasswordHash.ValidatePassword(pass, PassHash))
                {

                    Session.Add("USER", loginResults["USERNAME"].ToString());
                    Session.Add("FIRSTNAME", loginResults["FIRSTNAME"].ToString());
                    Session.Add("LASTNAME", loginResults["LASTNAME"].ToString());
                    string type = loginResults["USERTYPE"].ToString();
                    Session.Add("TYPE", type);

                    //Label1.Text = first;
                    //Label2.Text = last;
                    //Label3.Text = type;
                    if(type == "Teacher")
                    {
                        Session.Add("Master", "~/Teacher.Master");
                    }
                    else if(type == "Coordinator")
                    {
                        Session.Add("Master", "~/Admin.Master");
                    }
                    else if(type == "Student Volunteer" || type == "Staff Volunteer")
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


        public void getInfo(String type)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand command;
            string sql = "Select " + type +"ID from " + type + " where EMAILADD Like @email"; 
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", Session["USER"].ToString());
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                Session.Add("ID", dataReader[type + "ID"].ToString());
            }

            Response.Redirect(type + "Dashboard.aspx");
        }

    }
}