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
                    Session.Add("User", loginResults["USERNAME"].ToString());
                    string first = loginResults["FIRSTNAME"].ToString();
                    string last = loginResults["LASTNAME"].ToString();
                    string type = loginResults["USERTYPE"].ToString();
                    if (type == "Teacher")
                    {
                        getTeacherInfo(first, last);
                        Session.Add("TYPE", type);
                    }
                    else
                    {
                        if (type == "Coordinator")
                        {
                            Session.Add("TYPE", type);
                        }
                        Session.Add("TYPE", type);
                    }
                    connection.Close();
                    Response.Redirect("StartPage.aspx");
                }

            }
            else
            {
                LoginStat.Visible = true;
            }
        }

        protected void NewUserBtn_Click(object sender, EventArgs e)
        {
            //Response.Redirect("UserCreation.aspx");
        }
        public void getTeacherInfo(string first, string last)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(cs);
            SqlCommand command;
            string sql = "Select TEACHERID, SCHOOL from TEACHER where FIRSTNAME Like @FIRSTNAME AND LASTNAME Like @LASTNAME";
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@FIRSTNAME", first);
            command.Parameters.AddWithValue("@LASTNAME", last);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                Session.Add("TEACHERID", dataReader["TEACHERID"].ToString());
                Session.Add("SCHOOL", dataReader["SCHOOL"].ToString());
            }
        }
    }
}