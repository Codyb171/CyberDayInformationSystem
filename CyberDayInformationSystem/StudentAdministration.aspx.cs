using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class StudentAdministration : Page
    {
        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Session["TYPE"] != null)
            {
                MasterPageFile = (Session["Master"].ToString());
                if (Session["TYPE"].ToString() != "Coordinator" && Session["TYPE"].ToString() != "Teacher")
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
        private int _teacherID;
        private int _school;
        private int _idToEdit;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TshirtSizeList.Items.Insert(0, new ListItem(String.Empty));
                EditSizeList.Items.Insert(0, new ListItem(String.Empty));
                GenderDropDown.Items.Insert(0, new ListItem(String.Empty));
            }

            if (Page.IsPostBack)
            {
                if (Session["StudentID"] != null)
                {
                    _idToEdit = int.Parse(Session["StudentID"].ToString());
                }
            }

            if (Session["TYPE"] != null)
            {
                if (Session["TYPE"].ToString() == "Teacher")
                {
                    _teacherID = int.Parse(Session["ID"].ToString());
                    _school = int.Parse(Session["SCHOOL"].ToString());
                }
                else
                {
                    _teacherID = 0;
                    _school = 0;
                }

            }

        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SendStudent();
            }

            ClearForm();
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearForm();
            UserInfoLbl.Text = "Form Cleared Successfully!";
        }

        private void ClearForm()
        {
            FirstNameTxt.Text = "";
            LastNameTxt.Text = "";
            AgeTxt.Text = "";
            TshirtSizeList.ClearSelection();
            GenderDropDown.ClearSelection();
            TeacherDropDown.ClearSelection();
            noteTxt.Text = "";
        }

        private void SendStudent()
        {
            if (StudentExists() == 0)
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                string sql = "Insert into Student (FIRSTNAME, LASTNAME, AGE, GENDER, PREVIOUSATTENDEE, MEALTICKET, TEACHER, SCHOOL)" +
                             " values( @FIRSTNAME, @LASTNAME, @AGE, @GENDER, @PRE, @MEAL, @TEACHER, @SCHOOL)";
                string first = HttpUtility.HtmlEncode(FirstNameTxt.Text);
                string last = HttpUtility.HtmlEncode(LastNameTxt.Text);
                int age = int.Parse(HttpUtility.HtmlEncode(AgeTxt.Text));
                string gender = GenderDropDown.SelectedItem.Text;
                var connection = new SqlConnection(cs);
                connection.Open();
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@FIRSTNAME", first);
                command.Parameters.AddWithValue("@LASTNAME", last);
                command.Parameters.AddWithValue("@AGE", age);
                command.Parameters.AddWithValue("@GENDER", gender);
                command.Parameters.AddWithValue("@PRE", PreAttendBtn.SelectedItem.Text);
                command.Parameters.AddWithValue("@MEAL",MealBtn.SelectedItem.Text);
                if (_teacherID != 0)
                {
                    command.Parameters.AddWithValue("@TEACHER", _teacherID);
                    command.Parameters.AddWithValue("@SCHOOL", _school);
                }
                else
                {
                    command.Parameters.AddWithValue("@TEACHER", int.Parse(TeacherDropDown.SelectedValue));
                    command.Parameters.AddWithValue("@SCHOOL", int.Parse(SchoolDropDown.SelectedValue));
                }

                command.ExecuteNonQuery();
                UserInfoLbl.Text = "Student Saved Successfully!!";
                ShowLbl.Visible = true;
                ShowPassCheck.Visible = true;

                Generations(first, last);
            }
        }

        private void Generations(string first, string last)
        {
            var dbcs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            var authcs = ConfigurationManager.ConnectionStrings["AUTH"].ConnectionString;

            var dbcon = new SqlConnection(dbcs);
            var authcon = new SqlConnection(authcs);

            dbcon.Open();
            authcon.Open();

            //Generate username for student
            string generatedStuUser = last + first;

            //Generate username for parent
            string generatedParUser = "Parent" + first + last;

            //Generate password for student
            string generatedPassStu = last + "student";
            string hashPW = PasswordHash.HashPassword(generatedPassStu);

            //Generate password for parent
            string generatedPassPar = last + "parent";
            string parHashPW = PasswordHash.HashPassword(generatedPassPar);

            //Write to User DB - Student
            string userWrite = "INSERT into Users (FIRSTNAME, LASTNAME, USERNAME, USERTYPE)" +
                " values (@FIRSTNAME, @LASTNAME, @USERNAME, @USERTYPE)";
            var writeCommand = new SqlCommand(userWrite, authcon);
            string fName = first;
            string lName = last;
            string userName = generatedStuUser;

            writeCommand.Parameters.AddWithValue("@FIRSTNAME", fName);
            writeCommand.Parameters.AddWithValue("@LASTNAME", lName);
            writeCommand.Parameters.AddWithValue("@USERNAME", userName);
            writeCommand.Parameters.AddWithValue("@USERTYPE", "Student");

            writeCommand.ExecuteNonQuery();

            //Get the userID & write to PW DB - Student
            string getUseId = "SELECT MAX(USERID) FROM USERS";
            var getIdCommand = new SqlCommand(getUseId, authcon);
            getIdCommand.Parameters.AddWithValue("@USER", userName);
            int stuid = 0;

            SqlDataReader sturead = getIdCommand.ExecuteReader();

            while (sturead.Read())
            {
                stuid = sturead.GetInt32(0);
            }

            sturead.Close();

            string insertSql = "INSERT INTO PASSWORDS VALUES(@ID, @USER, @PASS)";
            var insertCommand = new SqlCommand(insertSql, authcon);
            insertCommand.Parameters.AddWithValue("@ID", stuid);
            insertCommand.Parameters.AddWithValue("@USER", userName);
            insertCommand.Parameters.AddWithValue("@PASS", hashPW);
            insertCommand.ExecuteNonQuery();

            //Write to User DB - Parent
            string parWrite = "INSERT INTO USERS (FIRSTNAME, LASTNAME, USERNAME, USERTYPE) VALUES (@FNAME, @LNAME, @USER, @TYPE)";
            var parWriteCmd = new SqlCommand(parWrite, authcon);
            parWriteCmd.Parameters.AddWithValue("@FNAME", "Parent " + fName);
            parWriteCmd.Parameters.AddWithValue("@LNAME", lName);
            parWriteCmd.Parameters.AddWithValue("@USER", generatedParUser);
            parWriteCmd.Parameters.AddWithValue("@TYPE", "Parent");

            parWriteCmd.ExecuteNonQuery();

            //Write to PW DB - Parent
            string getParId = "SELECT MAX(USERID) FROM USERS";
            var getParIdCmd = new SqlCommand(getParId, authcon);
            int parid = 0;

            SqlDataReader paridread = getParIdCmd.ExecuteReader();

            while (paridread.Read())
            {
                parid = paridread.GetInt32(0);
            }

            paridread.Close();

            string insertParSql = "INSERT INTO PASSWORDS VALUES(@ID, @USER, @PASS)";
            var insertParCmd = new SqlCommand(insertParSql, authcon);
            insertParCmd.Parameters.AddWithValue("@ID", parid);
            insertParCmd.Parameters.AddWithValue("@USER", generatedParUser);
            insertParCmd.Parameters.AddWithValue("@PASS", parHashPW);

            insertParCmd.ExecuteNonQuery();

            //Create placeholder Parent
            string parentWrite = "INSERT into Guardian (FIRSTNAME, LASTNAME, EMAILADD, PHONE, CONTACT) " +
                "values (@FNAME, @LNAME, @EMAIL, @PHONE, @CONTACT)";
            var parWriteCommand = new SqlCommand(parentWrite, dbcon);
            parWriteCommand.Parameters.AddWithValue("@FNAME", "Parent " + fName);
            parWriteCommand.Parameters.AddWithValue("@LNAME", lName);
            parWriteCommand.Parameters.AddWithValue("@EMAIL", " ");
            parWriteCommand.Parameters.AddWithValue("@PHONE", "0000000000");
            parWriteCommand.Parameters.AddWithValue("@CONTACT", "YES");
            parWriteCommand.ExecuteNonQuery();

            //Get ParentID and connect to student
            string getGuardID = "SELECT GUARDIANID FROM GUARDIAN WHERE FIRSTNAME = @FNAME AND LASTNAME = @LNAME";
            var getGuardIDCmd = new SqlCommand(getGuardID, dbcon);
            getGuardIDCmd.Parameters.AddWithValue("@FNAME", "Parent " + fName);
            getGuardIDCmd.Parameters.AddWithValue("@LNAME", lName);
            int guardId = 0;

            SqlDataReader guardRead = getGuardIDCmd.ExecuteReader();

            while (guardRead.Read())
            {
                guardId = guardRead.GetInt32(0);
            }

            guardRead.Close();

            string stuParConnect = "UPDATE STUDENT SET GUARDIAN = @GUARDIAN WHERE FIRSTNAME = @FNAME AND LASTNAME = @LNAME";
            var stuParConnectCmd = new SqlCommand(stuParConnect, dbcon);
            stuParConnectCmd.Parameters.AddWithValue("@GUARDIAN", guardId);
            stuParConnectCmd.Parameters.AddWithValue("@FNAME", fName);
            stuParConnectCmd.Parameters.AddWithValue("@LNAME", lName);
            stuParConnectCmd.ExecuteNonQuery();

            ViewPW(generatedStuUser, generatedPassStu, generatedParUser, generatedPassPar);
        }

        // Sets the username and passwords to the labels
        private void ViewPW(string stuUser, string stuPass, string parUser, string parPass)
        {
            UsernameLbl.Text = stuUser;
            PasswordLbl.Text = stuPass;
            UsernameLblPar.Text = parUser;
            PasswordLblPar.Text = parPass;
        }

        public int GetCurrentStudent()
        {
            int count = 1;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            string testSql = "Select count(*) from Student";
            var connection = new SqlConnection(cs);
            connection.Open();
            var command = new SqlCommand(testSql, connection);
            var dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                count = dataReader.GetInt32(0);
            }

            dataReader.Close();
            count++;
            return count;
        }

        private int StudentExists()
        {
            int add = 0;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            string testSql =
                "Select StudentID from Student where FirstName = @FIRST and LASTNAME = @LAST and Age = @AGE and TEACHER = @TEACHER and SCHOOL = @SCHOOL";
            var connection = new SqlConnection(cs);
            connection.Open();
            string first = HttpUtility.HtmlEncode(FirstNameTxt.Text);
            string last = HttpUtility.HtmlEncode(LastNameTxt.Text);
            int age = int.Parse(HttpUtility.HtmlEncode(AgeTxt.Text));
            var command = new SqlCommand(testSql, connection);
            command.Parameters.AddWithValue("@FIRST", first);
            command.Parameters.AddWithValue("@LAST", last);
            command.Parameters.AddWithValue("@AGE", age);
            if (_teacherID != 0)
            {
                command.Parameters.AddWithValue("@TEACHER", _teacherID);
                command.Parameters.AddWithValue("@SCHOOL", _school);
            }
            else
            {
                command.Parameters.AddWithValue("@TEACHER", int.Parse(TeacherDropDown.SelectedValue));
                command.Parameters.AddWithValue("@SCHOOL", int.Parse(SchoolDropDown.SelectedValue));
            }

            var dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                add = 1;
                UserInfoLbl.Text = "Student Already Exists!";
            }


            dataReader.Close();
            return add;
        }

        protected void btnCreateStuClick(object sender, EventArgs e)
        {
            SelectedFunction.ActiveViewIndex = 0;
            //if (_teacherID == 0)
            //{
            //    TeacherList(1);
            //    CoordinatorView.ActiveViewIndex = 0;
            //}
            ClearEditForms();
        }

        protected void btnModStuClick(object sender, EventArgs e)
        {
            ClearEditForms();
            SelectedFunction.ActiveViewIndex = 1;
        }

        // Searches based on user input to the textbox
        protected void SearchByTagButton_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            var connection = new SqlConnection(cs);
            DataTable dt = new DataTable();
            connection.Open();
            string sql =
                "Select S.STUDENTID, S.FIRSTNAME, S.LASTNAME, S.AGE, S.GENDER, S.PREVIOUSATTENDEE, S.MEALTICKET," +
                " (T.TITLE + ' ' + T.FIRSTNAME + ' ' + T.LASTNAME) AS \"TEACHER\", SC.NAME FROM STUDENT S LEFT JOIN TEACHER T on S.TEACHER = T.TEACHERID" +
                " join SCHOOL SC ON SC.SCHOOLID = S.SCHOOL WHERE";

            try
            {
                if (SearchByTagFN.Text != String.Empty && SearchByTagLN.Text != String.Empty)
                {
                    sql += " S.FIRSTNAME LIKE @FIRST AND S.LASTNAME LIKE @LAST";
                    SqlDataAdapter select = new SqlDataAdapter(sql, connection);
                    select.SelectCommand.Parameters.AddWithValue("@FIRST",
                        "%" + HttpUtility.HtmlEncode(SearchByTagFN.Text) + "%");
                    select.SelectCommand.Parameters.AddWithValue("@LAST",
                        "%" + HttpUtility.HtmlEncode(SearchByTagLN.Text) + "%");
                    select.Fill(dt);
                }
                else if (SearchByTagFN.Text != String.Empty)
                {
                    sql += " S.FIRSTNAME LIKE @FIRST";
                    SqlDataAdapter select = new SqlDataAdapter(sql, connection);
                    select.SelectCommand.Parameters.AddWithValue("@FIRST",
                        "%" + HttpUtility.HtmlEncode(SearchByTagFN.Text) + "%");
                    select.Fill(dt);
                }
                else
                {
                    sql += " S.LASTNAME LIKE @LAST";
                    SqlDataAdapter select = new SqlDataAdapter(sql, connection);
                    select.SelectCommand.Parameters.AddWithValue("@LAST",
                        "%" + HttpUtility.HtmlEncode(SearchByTagLN.Text) + "%");
                    select.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    studentModifyDtl.DataSource = dt;
                    studentModifyDtl.DataBind();
                }
                else
                {
                    dt = new DataTable();
                    DataColumn dc1 = new DataColumn("No Data");
                    dt.Columns.Add(dc1);
                    DataRow dr1 = dt.NewRow();
                    dr1[0] = "No Students found with that data";
                    dt.Rows.Add(dr1);
                    studentModifyDtl.DataSource = dt;
                    studentModifyDtl.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            //DeleteStudentBtn.Visible = true;
            EditStudentBtn.Visible = true;
        }

        protected void EditStudentBtn_Click(object sender, EventArgs e)
        {
            _idToEdit = int.Parse(studentModifyDtl.DataKey[0].ToString());
            Session.Add("StudentID", _idToEdit);
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            var connection = new SqlConnection(cs);
            string sql =
                "Select S.FIRSTNAME, S.LASTNAME, S.AGE, S.GENDER, S.PREVIOUSATTENDEE, S.MEALTICKET, S.TEACHER FROM STUDENT S" +
                " WHERE STUDENTID = @ID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", _idToEdit);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            TeacherList(2);
            if (dataReader.Read())
            {
                EditTeacherDropDown.SelectedValue = dataReader["TEACHER"].ToString();
                EditFirstNameTxt.Text = dataReader["FIRSTNAME"].ToString();
                EditLastNameTxt.Text = dataReader["LASTNAME"].ToString();
                EditAgeTxt.Text = dataReader["AGE"].ToString();
                EditGenderList.SelectedValue = dataReader["GENDER"].ToString();
                EditAttendeeBtn.SelectedValue = dataReader["PREVIOUSATTENDEE"].ToString();
                EditMealBtn.SelectedValue = dataReader["MEALTICKET"].ToString();
                SchoolList(int.Parse(EditTeacherDropDown.SelectedValue), 2);
            }

            SelectedFunction.ActiveViewIndex = 2;
        }

        private void TeacherList(int caller)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string command = "select teacherid, (title + ' ' + firstname + ' '+ lastname) as NAME from Teacher";
            connection.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (caller == 1)
            {
                TeacherDropDown.DataSource = dt;
                TeacherDropDown.DataTextField = "NAME";
                TeacherDropDown.DataValueField = "TEACHERID";
                TeacherDropDown.DataBind();
                TeacherDropDown.Items.Insert(0, new ListItem(String.Empty));
            }
            else
            {
                EditTeacherDropDown.DataSource = dt;
                EditTeacherDropDown.DataTextField = "NAME";
                EditTeacherDropDown.DataValueField = "TEACHERID";
                EditTeacherDropDown.DataBind();
                EditTeacherDropDown.Items.Insert(0, new ListItem(String.Empty));
            }

        }

        private void SchoolList(int teacherID,int caller)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            string command =
                "select S.SCHOOLID, S.NAME from School s join Teacher t on S.SCHOOLID = T.SCHOOL where TEACHERID = " +
                teacherID;
            connection.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (caller == 1)
                {
                    SchoolDropDown.DataSource = dt;
                    SchoolDropDown.DataBind();
                    SchoolDropDown.DataTextField = "Name";
                    SchoolDropDown.DataValueField = "SCHOOLID";
                    SchoolDropDown.DataBind();
                    SchoolDropDown.SelectedIndex = 0;
                }

                else
                {
                    EditSchoolDropDown.DataSource = dt;
                    EditSchoolDropDown.DataBind();
                    EditSchoolDropDown.DataTextField = "Name";
                    EditSchoolDropDown.DataValueField = "SCHOOLID";
                    EditSchoolDropDown.DataBind();
                    EditSchoolDropDown.SelectedIndex = 0;
                }
            }

        }

        protected void TeacherDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TeacherDropDown.SelectedIndex == 0)
            {
                SchoolDropDown.Items.Clear();
            }
            if(SelectedFunction.ActiveViewIndex == 1)
            {
                if (TeacherDropDown.SelectedIndex == 0)
                {
                    SchoolList(0, 1);
                }
                else
                {
                    int teacherID = int.Parse(TeacherDropDown.SelectedValue);
                    SchoolList(teacherID, 1);
                }

            }
            if (SelectedFunction.ActiveViewIndex == 2)
            {
                if (EditTeacherDropDown.SelectedIndex == 0)
                {
                    EditSchoolDropDown.Items.Clear();
                }
                else
                {
                    int teacherID = int.Parse(EditTeacherDropDown.SelectedValue);
                    SchoolList(teacherID, 2);
                }

            }
        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            var connection = new SqlConnection(cs);
            connection.Open();

            SqlCommand updateStudent = new SqlCommand(
                "UPDATE Student set firstname = @FIRSTNAME, lastname = @LASTNAME, age = @AGE, GENDER = @GENDER, PREVIOUSATTENDEE = @PRE, MEALTICKET = @MEAL, " +
                "teacher = @TEACHER, school = @SCHOOL Where STUDENTID = @SID", connection);
            string firstname = HttpUtility.HtmlEncode(EditFirstNameTxt.Text);
            string lastname = HttpUtility.HtmlEncode(EditLastNameTxt.Text);
            int age = int.Parse(HttpUtility.HtmlEncode(EditAgeTxt.Text));
            int teacher = int.Parse(EditTeacherDropDown.SelectedValue);
            int school = int.Parse(EditSchoolDropDown.SelectedValue);


            try
            {
                updateStudent.Parameters.AddWithValue("@FIRSTNAME", firstname);
                updateStudent.Parameters.AddWithValue("@LASTNAME", lastname);
                updateStudent.Parameters.AddWithValue("@AGE", age);
                updateStudent.Parameters.AddWithValue("@GENDER", EditGenderList.SelectedItem.Text);
                updateStudent.Parameters.AddWithValue("@PRE", EditAttendeeBtn.SelectedItem.Text);
                updateStudent.Parameters.AddWithValue("@MEAL", EditMealBtn.SelectedItem.Text);
                updateStudent.Parameters.AddWithValue("@TEACHER", teacher);
                updateStudent.Parameters.AddWithValue("@SCHOOL", school);
                updateStudent.Parameters.AddWithValue("@SID", _idToEdit);
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

        private void ClearEditForms()
        {
            SearchByTagFN.Text = "";
            SearchByTagLN.Text = "";
            studentModifyDtl.DataSource = null;
            studentModifyDtl.DataBind();
            EditAgeTxt.Text = "";
            EditFirstNameTxt.Text = "";
            EditLastNameTxt.Text = "";
            EditGenderList.ClearSelection();
            EditMealBtn.ClearSelection();
            EditAttendeeBtn.ClearSelection();
            EditSizeList.ClearSelection();
            TeacherDropDown.ClearSelection();
            SchoolDropDown.Items.Clear();
            EditStudentBtn.Visible = false;
        }

        protected void StudentModifyDtl_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {
            studentModifyDtl.PageIndex = e.NewPageIndex;
            studentModifyDtl.DataBind();
            SearchByTagButton_Click(sender, e);
        }

        //protected void DeleteStudentBtn_OnClick(object sender, EventArgs e)
        //{
            
        //    int idToDelete= int.Parse(studentModifyDtl.DataKey[0].ToString());
        //    var cs = ConfigurationManager.ConnectionStrings["AUTH"].ConnectionString;
        //    var connection = new SqlConnection(cs);
        //    //var loginCommand = new SqlCommand();
        //    var deleteCommand = new SqlCommand();
        //    //string user = Session["USER"].ToString();
        //    //string pass = "FROM THE THING SARA DID";
        //    ////add function to require password
        //    ////need sara's help for this
        //    //connection.Open();
        //    //loginCommand.Connection = connection;
        //    //loginCommand.CommandType = CommandType.StoredProcedure;
        //    //loginCommand.CommandText = "UserLogin";
        //    //loginCommand.Parameters.AddWithValue("@username", user);
        //    //var loginResults = loginCommand.ExecuteReader();
        //    //var passHash = loginResults["PASSWORDHASH"].ToString();
        //    //if (PasswordHash.ValidatePassword(pass, passHash))
        //    //{
        //        deleteCommand.Connection = connection;
        //        deleteCommand.CommandType = CommandType.StoredProcedure;
        //        deleteCommand.CommandText = "DeleteStudent";
        //        deleteCommand.Parameters.AddWithValue("@STUDENTID", idToDelete);
        //        deleteCommand.ExecuteNonQuery();
        //    //}
        //}

        // When the check box is checked, view student and parent credentials
        protected void ShowPassCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowPassCheck.Checked == true)
            {
                StudentUserLbl.Visible = true;
                UsernameLbl.Visible = true;
                StudentUserPass.Visible = true;
                PasswordLbl.Visible = true;
                ParentUserLbl.Visible = true;
                UsernameLblPar.Visible = true;
                ParentUserPass.Visible = true;
                PasswordLblPar.Visible = true;
            } else
            {
                StudentUserLbl.Visible = false;
                UsernameLbl.Visible = false;
                StudentUserPass.Visible = false;
                PasswordLbl.Visible = false;
                ParentUserLbl.Visible = false;
                UsernameLblPar.Visible = false;
                ParentUserPass.Visible = false;
                PasswordLblPar.Visible = false;
            }
        }

    }
}