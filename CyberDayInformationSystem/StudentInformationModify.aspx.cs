using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class StudentInformationModify : System.Web.UI.Page
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
        private int guardianID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TYPE"] != null)
            {
                if (Session["TYPE"].ToString() == "Parent")
                {
                        guardianID = GetId(); /*int.Parse(Session["ID"].ToString())*/
                }
                else
                {
                    guardianID = 0;
                }
            }
            if (!Page.IsPostBack)
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                var connection = new SqlConnection(cs);
                string sql =
                    "Select S.FIRSTNAME, S.LASTNAME, S.AGE, S.GENDER, S.PREVIOUSATTENDEE, S.EMAILADD FROM STUDENT S" +
                    " WHERE GUARDIAN = @GUARDIAN";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@GUARDIAN", guardianID);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    EditFirstNameTxt.Text = dataReader["FIRSTNAME"].ToString();
                    EditLastNameTxt.Text = dataReader["LASTNAME"].ToString();
                    EditAgeTxt.Text = dataReader["AGE"].ToString();
                    EditGenderList.SelectedValue = dataReader["GENDER"].ToString();
                    EditAttendeeBtn.SelectedValue = dataReader["PREVIOUSATTENDEE"].ToString();
                    StudentEmailTxt.Text = dataReader["EMAILADD"].ToString();
                    EditGenderList.Items.Insert(0, new ListItem(String.Empty));
                }
            }
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

        protected void ClearEditForms()
        {
            EditAgeTxt.Text = "";
            EditFirstNameTxt.Text = "";
            EditLastNameTxt.Text = "";
            EditGenderList.ClearSelection();
            StudentEmailTxt.Text = "";
            EditAttendeeBtn.ClearSelection();
        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            var connection = new SqlConnection(cs);
            connection.Open();

            SqlCommand updateStudent = new SqlCommand(
                "UPDATE Student set firstname = @FIRSTNAME, lastname = @LASTNAME, age = @AGE, GENDER = @GENDER, PREVIOUSATTENDEE = @PRE, " +
                "EMAILADD = @EMAIL Where GUARDIAN = @GUARDIAN", connection);
            string firstname = HttpUtility.HtmlEncode(EditFirstNameTxt.Text);
            string lastname = HttpUtility.HtmlEncode(EditLastNameTxt.Text);
            int age = int.Parse(HttpUtility.HtmlEncode(EditAgeTxt.Text));
            string email = HttpUtility.HtmlEncode(StudentEmailTxt.Text);

            try
            {
                updateStudent.Parameters.AddWithValue("@FIRSTNAME", firstname);
                updateStudent.Parameters.AddWithValue("@LASTNAME", lastname);
                updateStudent.Parameters.AddWithValue("@AGE", age);
                updateStudent.Parameters.AddWithValue("@GENDER", EditGenderList.SelectedItem.Text);
                updateStudent.Parameters.AddWithValue("@PRE", EditAttendeeBtn.SelectedItem.Text);
                updateStudent.Parameters.AddWithValue("@EMAIL", email);
                updateStudent.Parameters.AddWithValue("@GUARDIAN", guardianID);
                updateStudent.ExecuteNonQuery();
                ClearEditForms();
                EditLabelStatus.Text = "Student Updated Successfully!!";
            }
            catch (Exception ex)
            {
                Console.Out.Write(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}