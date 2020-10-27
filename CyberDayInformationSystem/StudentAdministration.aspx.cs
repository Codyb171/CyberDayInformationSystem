using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class StudentAdministration : System.Web.UI.Page
    {
        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Session["TYPE"] != null)
            {
                this.MasterPageFile = (Session["Master"].ToString());
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
        private int CurrentStudentID;
        private int TeacherID;
        private int School;
        private int IDToEdit;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TshirtSizeList.Items.Insert(0, new ListItem(String.Empty));
                EditSizeList.Items.Insert(0, new ListItem(String.Empty));
                GenderDropDown.Items.Insert(0, new ListItem(String.Empty));
            }

            if (Page.IsPostBack == true)
            {
                if (Session["StudentID"] != null)
                {
                    IDToEdit = int.Parse(Session["StudentID"].ToString());
                }
            }

            if (Session["TYPE"] != null)
            {
                if (Session["TYPE"].ToString() == "Teacher")
                {
                    TeacherID = int.Parse(Session["ID"].ToString());
                    School = int.Parse(Session["SCHOOL"].ToString());
                }
                else
                {
                    TeacherID = 0;
                    School = 0;
                }

                CurrentStudentID = GetCurrentStudent();
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                sendShirt(CurrentStudentID);
                sendStudent(CurrentStudentID);
            }

            ClearForm();
        }

        protected void PopulateBtn_Click(object sender, EventArgs e)
        {
            FirstNameTxt.Text = "Dakota";
            LastNameTxt.Text = "Lawyer";
            AgeTxt.Text = "13";
            TshirtSizeList.SelectedIndex = 3;
            UserInfoLbl.Text = "Form Populated Successfully!";
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearForm();
            UserInfoLbl.Text = "Form Cleared Successfully!";
        }

        protected void ClearForm()
        {
            FirstNameTxt.Text = "";
            LastNameTxt.Text = "";
            AgeTxt.Text = "";
            TshirtSizeList.ClearSelection();
            GenderDropDown.ClearSelection();
            TeacherDropDown.ClearSelection();
        }

        public void sendShirt(int id)
        {
            if (StudentExists(0) == 0)
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                SqlConnection connection;
                SqlCommand command;
                string sql;
                string size = TshirtSizeList.SelectedValue;
                connection = new SqlConnection(cs);
                connection.Open();
                sql = "insert into TSHIRTINFO values(@ID, @size, @type)";
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@size", size);
                command.Parameters.AddWithValue("@type", "Student Shirt");
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void sendStudent(int id)
        {
            if (StudentExists(1) == 0)
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
                SqlConnection connection;
                SqlCommand command;
                string sql = "Insert into Student values( @FIRSTNAME, @LASTNAME, @AGE, @TSHIRT, @TEACHER, @SCHOOL)";
                string first = HttpUtility.HtmlEncode(FirstNameTxt.Text);
                string last = HttpUtility.HtmlEncode(LastNameTxt.Text);
                int age = int.Parse(HttpUtility.HtmlEncode(AgeTxt.Text));
                int tshirt = id;
                connection = new SqlConnection(cs);
                connection.Open();
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@FIRSTNAME", first);
                command.Parameters.AddWithValue("@LASTNAME", last);
                command.Parameters.AddWithValue("@AGE", age);
                command.Parameters.AddWithValue("@TSHIRT", tshirt);
                if (TeacherID != 0)
                {
                    command.Parameters.AddWithValue("@TEACHER", TeacherID);
                    command.Parameters.AddWithValue("@SCHOOL", School);
                }
                else
                {
                    command.Parameters.AddWithValue("@TEACHER", int.Parse(TeacherDropDown.SelectedValue));
                    command.Parameters.AddWithValue("@SCHOOL", int.Parse(SchoolDropDown.SelectedValue));
                }

                command.ExecuteNonQuery();
                UserInfoLbl.Text = "Student Saved Successfully!!";
            }
        }

        public int GetCurrentStudent()
        {
            int count = 1;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection;
            SqlCommand command;
            SqlDataReader dataReader;
            string testSql = "Select count(*) from Student";
            connection = new SqlConnection(cs);
            connection.Open();
            command = new SqlCommand(testSql, connection);
            dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                count = dataReader.GetInt32(0);
            }

            dataReader.Close();
            count++;
            return count;
        }

        public int StudentExists(int caller)
        {
            int add = 0;
            int id = 0;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection;
            SqlCommand command;
            SqlDataReader dataReader;
            string testSql =
                "Select StudentID from Student where FirstName = @FIRST and LASTNAME = @LAST and Age = @AGE and TEACHER = @TEACHER and SCHOOL = @SCHOOL";
            connection = new SqlConnection(cs);
            connection.Open();
            string first = HttpUtility.HtmlEncode(FirstNameTxt.Text);
            string last = HttpUtility.HtmlEncode(LastNameTxt.Text);
            int age = int.Parse(HttpUtility.HtmlEncode(AgeTxt.Text));
            command = new SqlCommand(testSql, connection);
            command.Parameters.AddWithValue("@FIRST", first);
            command.Parameters.AddWithValue("@LAST", last);
            command.Parameters.AddWithValue("@AGE", age);
            if (TeacherID != 0)
            {
                command.Parameters.AddWithValue("@TEACHER", TeacherID);
                command.Parameters.AddWithValue("@SCHOOL", School);
            }
            else
            {
                command.Parameters.AddWithValue("@TEACHER", int.Parse(TeacherDropDown.SelectedValue));
                command.Parameters.AddWithValue("@SCHOOL", int.Parse(SchoolDropDown.SelectedValue));
            }

            dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                add = 1;
                id = dataReader.GetInt32(0);
                UserInfoLbl.Text = "Student Already Exists!";
            }

            if (caller == 0)
            {
                add = CheckShirt(id);
            }

            dataReader.Close();
            return add;
        }

        public int CheckShirt(int id)
        {
            int shirt = 0;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection;
            SqlCommand command;
            SqlDataReader dataReader;
            string testSql = "Select Count(*) from TSHIRTINFO where TSHIRTID = @id";
            connection = new SqlConnection(cs);
            connection.Open();
            command = new SqlCommand(testSql, connection);
            command.Parameters.AddWithValue("@id", id);
            dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                shirt = dataReader.GetInt32(0);
            }

            return shirt;
        }

        protected void FunctionSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FunctionSelection.SelectedValue == "1")
            {
                SelectedFunction.ActiveViewIndex = 0;
                if (TeacherID == 0)
                {
                    TeacherList(1);
                    CoordinatorView.ActiveViewIndex = 0;
                }
                clearEditForms();
            }
            else if (FunctionSelection.SelectedValue == "2")
            {
                clearEditForms();
                SelectedFunction.ActiveViewIndex = 1;
            }
        }

        // Searches based on user input to the textbox
        protected void SearchByTagButton_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection;
            connection = new SqlConnection(cs);
            DataTable dt = new DataTable();
            connection.Open();
            string sql =
                "Select S.STUDENTID, S.FIRSTNAME, S.LASTNAME, S.AGE, TS.TSHIRTSIZE, (T.TITLE + ' ' + T.FIRSTNAME + ' ' + T.LASTNAME) AS \"TEACHER\"," +
                " SC.NAME FROM STUDENT S LEFT JOIN TSHIRTINFO TS on S.TSHIRT = TS.TSHIRTID JOIN TEACHER T on S.TEACHER = T.TEACHERID join SCHOOL SC ON" +
                " SC.SCHOOLID = S.SCHOOL WHERE";

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

            EditStudentBtn.Visible = true;
        }

        protected void EditStudentBtn_Click(object sender, EventArgs e)
        {
            IDToEdit = int.Parse(studentModifyDtl.DataKey[0].ToString());
            Session.Add("StudentID", IDToEdit);
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection;
            connection = new SqlConnection(cs);
            string sql =
                "Select S.FIRSTNAME, S.LASTNAME, S.AGE, TS.TSHIRTSIZE, S.TEACHER FROM STUDENT S join TSHIRTINFO TS" +
                " ON S.STUDENTID = TS.TSHIRTID WHERE STUDENTID = @ID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", IDToEdit);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            TeacherList(2);
            if (dataReader.Read())
            {
                EditTeacherDropDown.SelectedValue = dataReader["TEACHER"].ToString();
                EditFirstNameTxt.Text = dataReader["FIRSTNAME"].ToString();
                EditLastNameTxt.Text = dataReader["LASTNAME"].ToString();
                EditAgeTxt.Text = dataReader["AGE"].ToString();
                EditSizeList.SelectedValue = dataReader["TSHIRTSIZE"].ToString();
                SchoolList(int.Parse(EditTeacherDropDown.SelectedValue),2);
            }

            SelectedFunction.ActiveViewIndex = 2;
        }

        public void TeacherList(int caller)
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
            if (caller == 2)
            {
                EditTeacherDropDown.DataSource = dt;
                EditTeacherDropDown.DataTextField = "NAME";
                EditTeacherDropDown.DataValueField = "TEACHERID";
                EditTeacherDropDown.DataBind();
                EditTeacherDropDown.Items.Insert(0, new ListItem(String.Empty));
            }
            
        }

        public void SchoolList(int teacherID,int caller)
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

                if (caller == 2)
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
            if(FunctionSelection.SelectedValue == "1")
            {
                int teacherID = int.Parse(TeacherDropDown.SelectedValue);
                SchoolList(teacherID,1);
            }
            else
            {
                int teacherID = int.Parse(EditTeacherDropDown.SelectedValue);
                SchoolList(teacherID,2);
            }
        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection;
            connection = new SqlConnection(cs);
            connection.Open();

            SqlCommand updateStudent = new SqlCommand(
                "UPDATE Student set firstname = @FIRSTNAME, lastname = @LASTNAME, age = @AGE, " +
                "teacher = @TEACHER, school = @SCHOOL Where STUDENTID = @SID", connection);
            SqlCommand updateTshirt =
                new SqlCommand("UPDATE TSHIRTINFO TSHIRTSIZE = @SIZE WHERE TSHIRTID = @TID", connection);
            string firstname = HttpUtility.HtmlEncode(EditFirstNameTxt.Text);
            string lastname = HttpUtility.HtmlEncode(EditLastNameTxt.Text);
            int age = int.Parse(HttpUtility.HtmlEncode(EditAgeTxt.Text));
            int teacher = int.Parse(EditTeacherDropDown.SelectedValue);
            int school = int.Parse(EditSchoolDropDown.SelectedValue);
            string size = EditSizeList.SelectedValue;


            try
            {
                updateStudent.Parameters.AddWithValue("@FIRSTNAME", firstname);
                updateStudent.Parameters.AddWithValue("@LASTNAME", lastname);
                updateStudent.Parameters.AddWithValue("@AGE", age);
                updateStudent.Parameters.AddWithValue("@TEACHER", teacher);
                updateStudent.Parameters.AddWithValue("@SCHOOL", school);
                updateStudent.Parameters.AddWithValue("@SID", IDToEdit);
                updateStudent.ExecuteNonQuery();
                updateTshirt.Parameters.AddWithValue("@SIZE", size);
                updateTshirt.Parameters.AddWithValue("@TID", IDToEdit);
                clearEditForms();
                EditLabelStatus.Text = "Student Updated Successfully!!";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void clearEditForms()
        {
            SearchByTagFN.Text = "";
            SearchByTagLN.Text = "";
            studentModifyDtl.DataSource = null;
            studentModifyDtl.DataBind();
            EditAgeTxt.Text = "";
            EditFirstNameTxt.Text = "";
            EditLastNameTxt.Text = "";
            EditSizeList.ClearSelection();
            TeacherDropDown.ClearSelection();
            SchoolDropDown.Items.Clear();
            EditStudentBtn.Visible = false;
        }

        protected void studentModifyDtl_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {
            studentModifyDtl.PageIndex = e.NewPageIndex;
            studentModifyDtl.DataBind();
            SearchByTagButton_Click(sender, e);
        }
    }
}