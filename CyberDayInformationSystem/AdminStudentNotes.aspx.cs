using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class AdminStudentNotes : System.Web.UI.Page
    {
        private int _selId;
        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Session["TYPE"] != null)
            {
                MasterPageFile = (Session["Master"].ToString());
                if (Session["TYPE"].ToString() != "Coordinator")
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

        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            var connection = new SqlConnection(cs);
            DataTable dt = new DataTable();
            connection.Open();
            string sql = "Select STUDENTID, FIRSTNAME, LASTNAME, NOTES FROM STUDENT " +
                "LEFT JOIN STUDENTNOTES on STUDENTID = STUDENT";
            SqlDataAdapter select = new SqlDataAdapter(sql, connection);
            select.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                studentModDtl.DataSource = dt;
                studentModDtl.DataBind();
            }
            else
            {
                dt = new DataTable();
                DataColumn dc1 = new DataColumn("No Data");
                dt.Columns.Add(dc1);
                DataRow dr1 = dt.NewRow();
                dr1[0] = "No Students found with that data";
                dt.Rows.Add(dr1);
                studentModDtl.DataSource = dt;
                studentModDtl.DataBind();
            }

            CurView.ActiveViewIndex = 0;

            if (Page.IsPostBack)
            {
                if (Session["StudentID"] != null)
                {
                    _selId = int.Parse(Session["StudentID"].ToString());
                }
            }
        }

        protected void StudentModifyDtl_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {
            studentModDtl.PageIndex = e.NewPageIndex;
            studentModDtl.DataBind();
        }

        protected void btnAddNotes_Click(object sender, EventArgs e)
        {
            _selId = int.Parse(studentModDtl.DataKey[0].ToString());
            Session.Add("StudentID", _selId);
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            var connection = new SqlConnection(cs);
            string sql = "Select FIRSTNAME, LASTNAME FROM STUDENT WHERE STUDENTID = @ID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", _selId);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                lblStuFName.Text = dataReader["FIRSTNAME"].ToString();
                lblStuFName.Text = dataReader["LASTNAME"].ToString();
            }
            CurView.ActiveViewIndex = 1;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            var connection = new SqlConnection(cs);
            connection.Open();

            SqlCommand addNote = new SqlCommand("INSERT INTO STUDENTNOTES (STUDENT, NOTES) VALUES (@STUDENTID, @NOTE)", connection);
            string note = HttpUtility.HtmlEncode(txtAddNote.Text);

            try
            {
                addNote.Parameters.AddWithValue("@STUDENTID", _selId);
                addNote.Parameters.AddWithValue("@NOTE", note);
                addNote.ExecuteNonQuery();
                txtAddNote.Text = "";
                lblStatus.Text = "Note Added Successfully!!";
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            CurView.ActiveViewIndex = 1;

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            CurView.ActiveViewIndex = 0;
            txtAddNote.Text = "";
            lblStatus.Text = "";
        }
    }
}