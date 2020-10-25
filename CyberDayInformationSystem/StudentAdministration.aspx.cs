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
        //void Page_PreInit(Object sender, EventArgs e)
        //{
        //    if (Session["TYPE"] != null)
        //    {
        //        this.MasterPageFile = (Session["Master"].ToString());
        //        if (Session["TYPE"].ToString() != "Coordinator" && Session["TYPE"].ToString() != "Teacher")
        //        {
        //            Session.Add("Redirected", 1);
        //            Response.Redirect("BadSession.aspx");
        //        }
        //    }
        //    else
        //    {
        //        Session.Add("Redirected", 0);
        //        Response.Redirect("BadSession.aspx");
        //    }
        //}
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
                if (Session["ID"] != null)
                {
                    IDToEdit = int.Parse(Session["ID"].ToString());
                }
            }

            if (Session["TYPE"] != null)
            {
                if (Session["TYPE"].ToString() == "Teacher")
                {
                    TeacherID = int.Parse((string) Session["TEACHERID"]);
                    School = int.Parse((string) Session["SCHOOL"]);
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
        }

        public void sendShirt(int id)
        {
            if (StudentExists(0) == 0)
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
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
                command.Parameters.AddWithValue("@color", "Student Shirt");
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void sendStudent(int id)
        {
            if (StudentExists(1) == 0)
            {
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
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
                    command.Parameters.AddWithValue("@TEACHER", null);
                    command.Parameters.AddWithValue("@SCHOOL", null);
                }

                command.ExecuteNonQuery();
                UserInfoLbl.Text = "Student Saved Successfully!!";
            }
        }

        public int GetCurrentStudent()
        {
            int count = 1;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
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
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
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
                command.Parameters.AddWithValue("@TEACHER", null);
                command.Parameters.AddWithValue("@SCHOOL", null);
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
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
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
                clearEditForms();
            }
            else if (FunctionSelection.SelectedValue == "2")
            {
                clearEditForms();
                SelectedFunction.ActiveViewIndex = 1;
            }
            else if (FunctionSelection.SelectedValue == "3")
            {
                clearEditForms();
                SelectedFunction.ActiveViewIndex = 3;
                StudentListFill();
            }
        }

        // Searches based on user input to the textbox
        protected void SearchByTagButton_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
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
            Session.Add("ID", IDToEdit);
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            SqlConnection connection;
            connection = new SqlConnection(cs);
            string sql =
                "Select S.FIRSTNAME, S.LASTNAME, S.AGE, TS.TSHIRTSIZE, S.TEACHER FROM STUDENT S join TSHIRTINFO TS" +
                " ON S.STUDENTID = TS.TSHIRTID WHERE STUDENTID = @ID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", IDToEdit);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            TeacherList();
            if (dataReader.Read())
            {
                TeacherDropDown.SelectedValue = dataReader["TEACHER"].ToString();
                EditFirstNameTxt.Text = dataReader["FIRSTNAME"].ToString();
                EditLastNameTxt.Text = dataReader["LASTNAME"].ToString();
                EditAgeTxt.Text = dataReader["AGE"].ToString();
                EditSizeList.SelectedValue = dataReader["TSHIRTSIZE"].ToString();
                SchoolList(int.Parse(TeacherDropDown.SelectedValue));
            }

            SelectedFunction.ActiveViewIndex = 2;
        }

        public void TeacherList()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(cs);
            string command = "select teacherid, (title + ' ' + firstname + ' '+ lastname) as NAME from Teacher";
            connection.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            TeacherDropDown.DataSource = dt;
            TeacherDropDown.DataTextField = "NAME";
            TeacherDropDown.DataValueField = "TEACHERID";
            TeacherDropDown.DataBind();
            TeacherDropDown.Items.Insert(0, new ListItem(String.Empty));
        }

        public void SchoolList(int teacherID)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(cs);
            string command =
                "select S.SCHOOLID, S.NAME from School s join Teacher t on S.SCHOOLID = T.SCHOOL where TEACHERID = " +
                teacherID;
            connection.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            SchoolDropDown.DataSource = dt;
            SchoolDropDown.DataBind();
            SchoolDropDown.DataTextField = "Name";
            SchoolDropDown.DataValueField = "SCHOOLID";
            SchoolDropDown.DataBind();
            SchoolDropDown.SelectedIndex = 0;
        }

        protected void TeacherDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TeacherDropDown.SelectedIndex == 0)
            {
                SchoolDropDown.Items.Clear();
            }
            else
            {
                int teacherID = int.Parse(TeacherDropDown.SelectedValue);
                SchoolList(teacherID);
            }
        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
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
            int teacher = int.Parse(TeacherDropDown.SelectedValue);
            int school = int.Parse(SchoolDropDown.SelectedValue);
            string size = EditSizeList.SelectedValue.ToString();


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

        public void StudentListFill()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            SqlConnection connection = new SqlConnection(cs);
            string command =
                "select STUDENTID, (firstname + ' '+ lastname) as NAME from STUDENT where Teacher is null or SCHOOL is null";
            connection.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(command, connection);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            StudentList.DataSource = dt;
            StudentList.DataTextField = "NAME";
            StudentList.DataValueField = "STUDENTID";
            StudentList.DataBind();
            StudentList.Items.Insert(0, new ListItem(String.Empty));
        }

        protected void ClaimBtn_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            SqlConnection connection;
            connection = new SqlConnection(cs);
            int ID = int.Parse(StudentList.SelectedValue);
            connection.Open();
            SqlCommand updateStudent =
                new SqlCommand("UPDATE Student set teacher = @TEACHER, school = @SCHOOL Where STUDENTID = @SID",
                    connection);
            try
            {
                updateStudent.Parameters.AddWithValue("@TEACHER", TeacherID);
                updateStudent.Parameters.AddWithValue("@SCHOOL", School);
                updateStudent.Parameters.AddWithValue("@SID", ID);
                updateStudent.ExecuteNonQuery();
                ClaimStatusLbl.Text = "Student Claimed Successfully!!";
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

        public void StudentGrid()
        {
            if (StudentList.SelectedIndex == 0)
            {
                StudentGridView.DataSource = null;
                StudentGridView.DataBind();
            }
            else
            {
                int ID = int.Parse(StudentList.SelectedValue);
                string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
                string sql = "Select FIRSTNAME, LASTNAME, Age from STUDENT where STUDENTID = " + ID;
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(cs);
                SqlDataAdapter adapt = new SqlDataAdapter(sql, conn);
                conn.Open();
                adapt.Fill(dt);
                conn.Close();
                if (dt.Rows.Count > 0)
                {
                    StudentGridView.DataSource = dt;
                    StudentGridView.DataBind();
                }
            }
        }

        protected void StudentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            StudentGrid();
        }

        protected void studentModifyDtl_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {
            studentModifyDtl.PageIndex = e.NewPageIndex;
            studentModifyDtl.DataBind();
            SearchByTagButton_Click(sender, e);
        }
    }
}