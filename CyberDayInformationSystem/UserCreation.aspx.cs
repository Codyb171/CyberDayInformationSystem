﻿using Antlr.Runtime.Tree;
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
    public partial class UserCreation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                if (Page.IsPostBack == false)
                {
                    SchoolList();
                    TshirtColorList.Items.Insert(0, new ListItem(String.Empty));
                    TshirtSizeList.Items.Insert(0, new ListItem(String.Empty));
                    TypeDropDown.Items.Insert(0, new ListItem(String.Empty));
                    GradeDropDown.Items.Insert(0, new ListItem(String.Empty));
                    TitleDropDown.Items.Insert(0, new ListItem(String.Empty));
                }
            }
        }

            protected void UserTypeSelection_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (UserTypeSelection.SelectedValue == "1")
                {
                    DefaultTable.Visible = true;
                    SelectedView.ActiveViewIndex = 0;
                }
                if (UserTypeSelection.SelectedValue == "2")
                {
                    DefaultTable.Visible = true;
                    SelectedView.ActiveViewIndex = 1;
                }
                if (UserTypeSelection.SelectedValue == "3")
                {
                    DefaultTable.Visible = true;
                    SelectedView.ActiveViewIndex = 2;
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
                    TshirtColorList.ClearSelection();
                    TypeDropDown.ClearSelection();
                    SchoolDropDown.ClearSelection();
                    GradeDropDown.ClearSelection();
                }
                if (UserTypeSelection.SelectedValue == "2")
                {
                    CreateUser(2);
                    CreateStaff();
                    TitleDropDown.ClearSelection();
                    FirstNameTxt.Text = "";
                    LastNameTxt.Text = "";
                    PhoneTxt.Text = "";
                    EmailTxt.Text = "";
                    TshirtSizeList.ClearSelection();
                    TshirtColorList.ClearSelection();
                    TypeDropDown.ClearSelection();
                    SchoolDropDown.ClearSelection();
                    GradeDropDown.ClearSelection();
                }
            }
            public void SchoolList()
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
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
            public void CreateUser(int type)
            {
                if (CheckUser() == 0)
                {
                    string cs = ConfigurationManager.ConnectionStrings["AUTH"].ConnectionString.ToString();
                    SqlConnection connection = new SqlConnection(cs);
                    SqlCommand command;
                    string sql = "Insert into USERS values(@FIRST, @LAST, @USER, @TYPE)";
                    command = new SqlCommand(sql, connection);
                    string first = HttpUtility.HtmlEncode(FirstNameTxt.Text);
                    string last = HttpUtility.HtmlEncode(LastNameTxt.Text);
                    string user = last + first.Substring(0, 1);
                    connection.Open();
                    command.Parameters.AddWithValue("@FIRST", first);
                    command.Parameters.AddWithValue("@LAST", last);
                    command.Parameters.AddWithValue("@USER", user);
                    if (type == 1)
                    {
                        command.Parameters.AddWithValue("@TYPE", "Teacher");
                    }
                    if (type == 2)
                    {
                        string staff = TypeDropDown.SelectedValue;
                        command.Parameters.AddWithValue("@TYPE", staff);
                    }
                    command.ExecuteNonQuery();
                    SendPassword(user);
                    connection.Close();
                    UserInfoLbl.Text = "User <b>" + user + "</b> Has been created successfully";
                }
            }
            public void SendPassword(string userName)
            {
                int id;
                string password = HttpUtility.HtmlEncode(PasswordTxt1.Text);
                string hashPass = PasswordHash.HashPassword(password);
                string cs = ConfigurationManager.ConnectionStrings["AUTH"].ConnectionString.ToString();
                SqlConnection connection = new SqlConnection(cs);
                SqlCommand command;
                string idsql = "SELECT USERID FROM USERS WHERE USERNAME = @USER";
                command = new SqlCommand(idsql, connection);
                command.Parameters.AddWithValue("@USER", userName);
                connection.Open();
                id = (int)command.ExecuteScalar();
                string insertSql = "INSERT INTO PASSWORDS VALUES(@ID, @USER, @PASS)";
                command = new SqlCommand(insertSql, connection);
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@USER", userName);
                command.Parameters.AddWithValue("@PASS", hashPass);
                command.ExecuteNonQuery();
                connection.Close();
            }
            public void CreateTeacher()
            {
                if (TeacherExists() == 0)
                {
                    int id = GetID(1);
                    SendTshirt(id);
                    string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
                    SqlConnection connection = new SqlConnection(cs);
                    SqlCommand command;
                    string title = TitleDropDown.SelectedValue;
                    string first = HttpUtility.HtmlEncode(FirstNameTxt.Text);
                    string last = HttpUtility.HtmlEncode(LastNameTxt.Text);
                    string email = HttpUtility.HtmlEncode(EmailTxt.Text);
                    SqlInt64 phone = SqlInt64.Parse(HttpUtility.HtmlEncode(PhoneTxt.Text));
                    int grade = int.Parse(GradeDropDown.SelectedValue);
                    int school = int.Parse(SchoolDropDown.SelectedValue);
                    string sql = "Insert into TEACHER VALUES(@TITLE, @FIRST, @LAST, @EMAIL, @PHONE, @GRADE, @TSHIRT, @SCHOOL)";
                    connection.Open();
                    command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@TITLE", title);
                    command.Parameters.AddWithValue("@FIRST", first);
                    command.Parameters.AddWithValue("@LAST", last);
                    command.Parameters.AddWithValue("@EMAIL", email);
                    command.Parameters.AddWithValue("@PHONE", phone);
                    command.Parameters.AddWithValue("@GRADE", grade);
                    command.Parameters.AddWithValue("@TSHIRT", id);
                    command.Parameters.AddWithValue("@SCHOOL", school);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            public void CreateStaff()
            {
                if (StaffExists() == 0)
                {
                    int id = GetID(1);
                    SendTshirt(id);
                    string cs = ConfigurationManager.ConnectionStrings["Lab3"].ConnectionString.ToString();
                    SqlConnection connection = new SqlConnection(cs);
                    SqlCommand command;
                    string first = HttpUtility.HtmlEncode(FirstNameTxt.Text);
                    string last = HttpUtility.HtmlEncode(LastNameTxt.Text);
                    string email = HttpUtility.HtmlEncode(EmailTxt.Text);
                    SqlInt64 phone = SqlInt64.Parse(HttpUtility.HtmlEncode(PhoneTxt.Text));
                    string type = TypeDropDown.SelectedValue;
                    string sql = "Insert into STAFF VALUES( @FIRST, @LAST, @PHONE, @EMAIL, @TSHIRT, @TYPE)";
                    connection.Open();
                    command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@FIRST", first);
                    command.Parameters.AddWithValue("@LAST", last);
                    command.Parameters.AddWithValue("@PHONE", phone);
                    command.Parameters.AddWithValue("@EMAIL", email);
                    command.Parameters.AddWithValue("@TSHIRT", id);
                    command.Parameters.AddWithValue("@TYPE", type);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            public int GetID(int type)
            {
                int ID = 0;
                string cs = ConfigurationManager.ConnectionStrings["Lab3"].ConnectionString.ToString();
                SqlConnection connection = new SqlConnection(cs);
                SqlCommand command;
                SqlDataReader dataReader;
                string sql;
                connection.Open();
                if (type == 1)
                {
                    sql = "select cast(Max(TEACHERID) as varchar) From TEACHER";
                    command = new SqlCommand(sql, connection);
                    dataReader = command.ExecuteReader();
                    if (dataReader.Read())
                    {
                        ID = int.Parse(dataReader.GetString(0));
                    }
                }
                if (type == 2)
                {
                    sql = "select cast(Max(STAFFID) as varchar) From Staff";
                    command = new SqlCommand(sql, connection);
                    dataReader = command.ExecuteReader();
                    if (dataReader.Read())
                    {
                        ID = int.Parse(dataReader.GetString(0));
                    }

                }
                connection.Close();
                return ID + 1;
            }
            public void SendTshirt(int id)
            {
                string cs = ConfigurationManager.ConnectionStrings["Lab3"].ConnectionString.ToString();
                SqlConnection connection = new SqlConnection(cs);
                SqlCommand command;
                connection.Open();
                string sql = "Insert into TSHIRTINFO values(@ID, @SIZE, @COLOR)";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@SIZE", TshirtSizeList.SelectedValue);
                command.Parameters.AddWithValue("@COLOR", TshirtColorList.SelectedValue);
                command.ExecuteNonQuery();
                connection.Close();
            }
            public int TeacherExists()
            {
                int add = 0;
                string cs = ConfigurationManager.ConnectionStrings["Lab3"].ConnectionString.ToString();
                SqlConnection connection;
                SqlCommand command;
                SqlDataReader dataReader;
                connection = new SqlConnection(cs);
                connection.Open();
                string title = TitleDropDown.SelectedValue;
                string first = HttpUtility.HtmlEncode(FirstNameTxt.Text);
                string last = HttpUtility.HtmlEncode(LastNameTxt.Text);
                string email = HttpUtility.HtmlEncode(EmailTxt.Text);
                SqlInt64 phone = SqlInt64.Parse(PhoneTxt.Text);
                int grade = int.Parse(GradeDropDown.SelectedValue);
                int school = int.Parse(SchoolDropDown.SelectedValue);
                string sql = "Select TEACHERID FROM TEACHER WHERE TITLE = @TITLE AND FIRSTNAME = @FIRST AND LASTNAME = @LAST AND EMAILADD = @EMAIL AND PHONE = @PHONE" +
                    " AND GRADE = @GRADE AND SCHOOL = @SCHOOL";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@TITLE", title);
                command.Parameters.AddWithValue("@FIRST", first);
                command.Parameters.AddWithValue("@LAST", last);
                command.Parameters.AddWithValue("@EMAIL", email);
                command.Parameters.AddWithValue("@PHONE", phone);
                command.Parameters.AddWithValue("@GRADE", grade);
                command.Parameters.AddWithValue("@SCHOOL", school);
                dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    add = 1;
                }
                dataReader.Close();
                connection.Close();
                return add;
            }
            public int StaffExists()
            {
                int add = 0;
                string cs = ConfigurationManager.ConnectionStrings["Lab3"].ConnectionString.ToString();
                SqlConnection connection;
                SqlCommand command;
                SqlDataReader dataReader;
                connection = new SqlConnection(cs);
                connection.Open();
                string first = HttpUtility.HtmlEncode(FirstNameTxt.Text);
                string last = HttpUtility.HtmlEncode(LastNameTxt.Text);
                string email = HttpUtility.HtmlEncode(EmailTxt.Text);
                SqlInt64 phone = SqlInt64.Parse(HttpUtility.HtmlEncode(PhoneTxt.Text));
                string type = TypeDropDown.SelectedValue;
                string sql = "SELECT STAFFID FROM STAFF WHERE FIRSTNAME = @FIRST AND LASTNAME = @LAST and EMAILADD = @EMAIL AND PHONE = @PHONE AND TYPE = @TYPE";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@FIRST", first);
                command.Parameters.AddWithValue("@LAST", last);
                command.Parameters.AddWithValue("@EMAIL", email);
                command.Parameters.AddWithValue("@PHONE", phone);
                command.Parameters.AddWithValue("@TYPE", type);
                dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    add = 1;
                }
                dataReader.Close();
                connection.Close();
                return add;
            }
            public int CheckUser()
            {
                int add = 0;
                string cs = ConfigurationManager.ConnectionStrings["AUTH"].ConnectionString.ToString();
                SqlConnection connection;
                SqlCommand command;
                SqlDataReader dataReader;
                connection = new SqlConnection(cs);
                connection.Open();
                string first = HttpUtility.HtmlEncode(FirstNameTxt.Text);
                string last = HttpUtility.HtmlEncode(LastNameTxt.Text);
                string user = last + first.Substring(0, 1);
                string sql = "SELECT USERID from USERS where FIRSTNAME = @FIRST AND LASTNAME = @LAST AND USERNAME = @USER";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@FIRST", first);
                command.Parameters.AddWithValue("@LAST", last);
                command.Parameters.AddWithValue("@USER", user);
                dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    add = 1;
                    UserInfoLbl.Text = "A user with this Information already Exists";
                }
                dataReader.Close();
                connection.Close();
                return add;
            }
        }
    }