using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class UserCreation : Page
    {
        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Session["TYPE"] != null)
            {
                if (Session["TYPE"].ToString() == "Coordinator")
                {
                    MasterPageFile = "~/Admin.Master";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TYPE"] != null)
            {
                if (Session["TYPE"].ToString() == "Coordinator")
                {
                    if (Page.IsPostBack == false)
                    {
                        UserTypeSelection.Items.Insert(3, new ListItem("Coordinator", "4"));
                    }
                }
            }
            if (Page.IsPostBack == false)
            {
                SchoolList();
                TshirtSizeList.Items.Insert(0, new ListItem(String.Empty));
                GradeDropDown.Items.Insert(0, new ListItem(String.Empty));
                TitleDropDown.Items.Insert(0, new ListItem(String.Empty));
                MajorDropDown.Items.Insert(0, new ListItem(String.Empty));
                MinorDropDown.Items.Insert(0, new ListItem(String.Empty));
                OrgDropDown.Items.Insert(0, new ListItem(String.Empty));
                UserTypeSelection.Items.Insert(0,new ListItem(String.Empty));
            }
        }

        protected void UserTypeSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (UserTypeSelection.SelectedValue)
            {
                case "1":
                    DefaultTable.Visible = true;
                    SelectedView.ActiveViewIndex = 0;
                    break;
                case "2":
                case "4":
                    DefaultTable.Visible = true;
                    SelectedView.ActiveViewIndex = -1;
                    break;
                case "3":
                    DefaultTable.Visible = true;
                    SelectedView.ActiveViewIndex = 1;
                    break;
            }
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            if (UserTypeSelection.SelectedValue == "1")
            {
                CreateUser(1);
                CreateTeacher();
                TitleDropDown.ClearSelection();
                FirstNameTxt.Text = "";
                LastNameTxt.Text = "";
                PhoneTxt.Text = "";
                EmailTxt.Text = "";
                TshirtSizeList.ClearSelection();
                SchoolDropDown.ClearSelection();
                GradeDropDown.ClearSelection();
            }

            if (UserTypeSelection.SelectedValue == "2")
            {
                CreateUser(2);
                CreateStaff(2);
                TitleDropDown.ClearSelection();
                FirstNameTxt.Text = "";
                LastNameTxt.Text = "";
                PhoneTxt.Text = "";
                EmailTxt.Text = "";
                TshirtSizeList.ClearSelection();
                SchoolDropDown.ClearSelection();
                GradeDropDown.ClearSelection();
            }

            if (UserTypeSelection.SelectedValue == "3")
            {
                CreateUser(3);
                CreateStaff(3);
                TitleDropDown.ClearSelection();
                FirstNameTxt.Text = "";
                LastNameTxt.Text = "";
                PhoneTxt.Text = "";
                EmailTxt.Text = "";
                TshirtSizeList.ClearSelection();
            }

            if (UserTypeSelection.SelectedValue == "4")
            {
                CreateUser(4);
                CreateStaff(4);
                TitleDropDown.ClearSelection();
                FirstNameTxt.Text = "";
                LastNameTxt.Text = "";
                PhoneTxt.Text = "";
                EmailTxt.Text = "";
                TshirtSizeList.ClearSelection();
            }
        }

        private void SchoolList()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string command = "select S.SCHOOLID, S.NAME from School s";
            connection.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            SchoolDropDown.DataSource = dt;
            SchoolDropDown.DataBind();
            SchoolDropDown.DataTextField = "Name";
            SchoolDropDown.DataValueField = "SCHOOLID";
            SchoolDropDown.DataBind();
            SchoolDropDown.Items.Insert(0, new ListItem(String.Empty));
            connection.Close();
        }

        private void CreateUser(int type)
        {
            if (CheckUser() == 0)
            {
                string cs = ConfigurationManager.ConnectionStrings["AUTH"].ConnectionString;
                SqlConnection connection = new SqlConnection(cs);
                string sql = "Insert into USERS values(@FIRST, @LAST, @USER, @TYPE)";
                var command = new SqlCommand(sql, connection);
                string first = HttpUtility.HtmlEncode(FirstNameTxt.Text);
                string last = HttpUtility.HtmlEncode(LastNameTxt.Text);
                string user = EmailTxt.Text;
                connection.Open();
                command.Parameters.AddWithValue("@FIRST", first);
                command.Parameters.AddWithValue("@LAST", last);
                command.Parameters.AddWithValue("@USER", user);
                switch (type)
                {
                    case 1:
                        command.Parameters.AddWithValue("@TYPE", "Teacher");
                        break;
                    case 2:
                        command.Parameters.AddWithValue("@TYPE", "Staff Volunteer");
                        break;
                    case 3:
                        command.Parameters.AddWithValue("@TYPE", "Student Volunteer");
                        break;
                    case 4:
                        command.Parameters.AddWithValue("@TYPE", "Coordinator");
                        break;
                }

                command.ExecuteNonQuery();
                SendPassword(user);
                connection.Close();
                UserInfoLbl.Text = "User " + first + last + " has been created successfully";
            }
        }

        private void SendPassword(string userName)
        {
            string password = HttpUtility.HtmlEncode(PasswordTxt1.Text);
            string hashPass = PasswordHash.HashPassword(password);
            string cs = ConfigurationManager.ConnectionStrings["AUTH"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string idsql = "SELECT USERID FROM USERS WHERE USERNAME = @USER";
            var command = new SqlCommand(idsql, connection);
            command.Parameters.AddWithValue("@USER", userName);
            connection.Open();
            var id = (int) command.ExecuteScalar();
            string insertSql = "INSERT INTO PASSWORDS VALUES(@ID, @USER, @PASS)";
            command = new SqlCommand(insertSql, connection);
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@USER", userName);
            command.Parameters.AddWithValue("@PASS", hashPass);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void CreateTeacher()
        {
            if (TeacherExists() == 0)
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                SqlConnection connection = new SqlConnection(cs);
                string title = TitleDropDown.SelectedValue;
                string first = HttpUtility.HtmlEncode(FirstNameTxt.Text);
                string last = HttpUtility.HtmlEncode(LastNameTxt.Text);
                string email = HttpUtility.HtmlEncode(EmailTxt.Text);
                SqlInt64 phone = SqlInt64.Parse(HttpUtility.HtmlEncode(PhoneTxt.Text));
                int grade = int.Parse(GradeDropDown.SelectedValue);
                int school = int.Parse(SchoolDropDown.SelectedValue);
                string sql =
                    "Insert into TEACHER(TITLE, FIRSTNAME, LASTNAME, EMAILADD, PHONE, GRADE, SCHOOL) VALUES(@TITLE, @FIRST, @LAST, @EMAIL, @PHONE, @GRADE, @SCHOOL)";
                connection.Open();
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@TITLE", title);
                command.Parameters.AddWithValue("@FIRST", first);
                command.Parameters.AddWithValue("@LAST", last);
                command.Parameters.AddWithValue("@EMAIL", email);
                command.Parameters.AddWithValue("@PHONE", phone);
                command.Parameters.AddWithValue("@GRADE", grade);
                command.Parameters.AddWithValue("@SCHOOL", school);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void CreateStaff(int type)
        {
            if (StaffExists(type) == 0)
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                SqlConnection connection = new SqlConnection(cs);
                string first = HttpUtility.HtmlEncode(FirstNameTxt.Text);
                string last = HttpUtility.HtmlEncode(LastNameTxt.Text);
                string email = HttpUtility.HtmlEncode(EmailTxt.Text);
                SqlInt64 phone = SqlInt64.Parse(HttpUtility.HtmlEncode(PhoneTxt.Text));
                string sql = "Insert into VOLUNTEER(FIRSTNAME,LASTNAME,PHONE,EMAILADD,TYPE) VALUES(@FIRST, @LAST, @PHONE, @EMAIL, @TYPE)";
                connection.Open();
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@FIRST", first);
                command.Parameters.AddWithValue("@LAST", last);
                command.Parameters.AddWithValue("@PHONE", phone);
                command.Parameters.AddWithValue("@EMAIL", email);
                switch (type)
                {
                    case 2:
                        command.Parameters.AddWithValue("@TYPE", "Staff Volunteer");
                        break;
                    case 3:
                        command.Parameters.AddWithValue("@TYPE", "Student Volunteer");
                        break;
                    case 4:
                        command.Parameters.AddWithValue("@TYPE", "Coordinator");
                        break;
                }

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private int TeacherExists()
        {
            int add = 0;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            var connection = new SqlConnection(cs);
            connection.Open();
            string title = TitleDropDown.SelectedValue;
            string first = HttpUtility.HtmlEncode(FirstNameTxt.Text);
            string last = HttpUtility.HtmlEncode(LastNameTxt.Text);
            string email = HttpUtility.HtmlEncode(EmailTxt.Text);
            SqlInt64 phone = SqlInt64.Parse(PhoneTxt.Text);
            int grade = int.Parse(GradeDropDown.SelectedValue);
            int school = int.Parse(SchoolDropDown.SelectedValue);
            string sql =
                "Select TEACHERID FROM TEACHER WHERE TITLE = @TITLE AND FIRSTNAME = @FIRST AND LASTNAME = @LAST AND EMAILADD = @EMAIL AND PHONE = @PHONE" +
                " AND GRADE = @GRADE AND SCHOOL = @SCHOOL";
            var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@TITLE", title);
            command.Parameters.AddWithValue("@FIRST", first);
            command.Parameters.AddWithValue("@LAST", last);
            command.Parameters.AddWithValue("@EMAIL", email);
            command.Parameters.AddWithValue("@PHONE", phone);
            command.Parameters.AddWithValue("@GRADE", grade);
            command.Parameters.AddWithValue("@SCHOOL", school);
            var dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                add = 1;
            }

            dataReader.Close();
            connection.Close();
            return add;
        }

        private int StaffExists(int type)
        {
            int add = 0;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            var connection = new SqlConnection(cs);
            connection.Open();
            string first = HttpUtility.HtmlEncode(FirstNameTxt.Text);
            string last = HttpUtility.HtmlEncode(LastNameTxt.Text);
            string email = HttpUtility.HtmlEncode(EmailTxt.Text);
            SqlInt64 phone = SqlInt64.Parse(HttpUtility.HtmlEncode(PhoneTxt.Text));
            string sql =
                "SELECT STAFFID FROM VOLUNTEER WHERE FIRSTNAME = @FIRST AND LASTNAME = @LAST and EMAILADD = @EMAIL AND PHONE = @PHONE AND TYPE = @TYPE";
            var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@FIRST", first);
            command.Parameters.AddWithValue("@LAST", last);
            command.Parameters.AddWithValue("@EMAIL", email);
            command.Parameters.AddWithValue("@PHONE", phone);
            if (type == 2)
            {
                command.Parameters.AddWithValue("@TYPE", "Staff Volunteer");
            }

            if (type == 3)
            {
                command.Parameters.AddWithValue("@TYPE", "Student Volunteer");
            }

            var dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                add = 1;
            }

            dataReader.Close();
            connection.Close();
            return add;
        }

        private int CheckUser()
        {
            int add = 0;
            string cs = ConfigurationManager.ConnectionStrings["AUTH"].ConnectionString;
            var connection = new SqlConnection(cs);
            connection.Open();
            string first = HttpUtility.HtmlEncode(FirstNameTxt.Text);
            string last = HttpUtility.HtmlEncode(LastNameTxt.Text);
            string user = EmailTxt.Text;
            string sql = "SELECT USERID from USERS where FIRSTNAME = @FIRST AND LASTNAME = @LAST AND USERNAME = @USER";
            var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@FIRST", first);
            command.Parameters.AddWithValue("@LAST", last);
            command.Parameters.AddWithValue("@USER", user);
            var dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                add = 1;
                UserInfoLbl.Text = "A user with thie name " + first + last + " already exists";

            }

            dataReader.Close();
            connection.Close();
            return add;
        }
    }
}