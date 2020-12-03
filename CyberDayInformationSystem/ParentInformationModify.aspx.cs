using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class ParentInformationModify : System.Web.UI.Page
    {
        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Session["TYPE"] != null)
            {
                MasterPageFile = (Session["Master"].ToString());
                if (Session["TYPE"].ToString() != "Parent")
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

        private int _idToEdit;

        protected void Page_Load(object sender, EventArgs e)
        {
            _idToEdit = GetId();
        }

        protected int GetId()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            var connection = new SqlConnection(cs);
            string sql = "Select GUARDIANID FROM GUARDIAN WHERE CONCAT(FIRSTNAME, ' ', LASTNAME) LIKE '%" + Session["NAME"] + "%'";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            int id = 0;

            if (dataReader.Read())
            {
               id = int.Parse(dataReader["GUARDIANID"].ToString());
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
            SqlInt64 phone = SqlInt64.Parse(HttpUtility.HtmlEncode(PhoneTxt.Text));

            string sql = "Update GUARDIAN set FIRSTNAME = @FIRSTNAME, LASTNAME = @LASTNAME, EMAILADD = @EMAIL, PHONE = @PHONE WHERE GUARDIANID = @ID";

            var command = new SqlCommand(sql, authcon);
            command.Parameters.AddWithValue("@FIRSTNAME", firstName);
            command.Parameters.AddWithValue("@LASTNAME", lastName);
            command.Parameters.AddWithValue("@EMAIL", email);
            command.Parameters.AddWithValue("@PHONE", phone);
            command.Parameters.AddWithValue("@ID", _idToEdit);
            command.ExecuteNonQuery();
            
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
            string sql =
                "Update Users set FIRSTNAME = @FIRSTNAME, LASTNAME = @LASTNAME where CONCAT(FIRSTNAME, ' ', LASTNAME) LIKE '%" +
                Session["NAME"] + "%'";
            var command = new SqlCommand(sql, authcon);
            command.Parameters.AddWithValue("@FIRSTNAME", firstName);
            command.Parameters.AddWithValue("@LASTNAME", lastName);
            command.ExecuteNonQuery();
            authcon.Close();
            Session["NAME"] = firstName + " " + lastName;
        }
    }
}


