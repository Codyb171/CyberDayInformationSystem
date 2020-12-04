using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class AdminSchoolCreation : System.Web.UI.Page
    {
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

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtAdd1.Text = "";
            txtAdd2.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtZip.Text = "";
            txtPhone.Text = "";
            lblStatus.Text = "";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            connection.Open();

            //Check for existance by Name
            string school = HttpUtility.HtmlEncode(txtName.Text);
            string check = "SELECT COUNT (*) FROM SCHOOL WHERE NAME = @NAME";
            SqlCommand cd = new SqlCommand(check, connection);
            cd.Parameters.AddWithValue("@NAME", school);

            int dupeCount = (int)cd.ExecuteScalar();

            if (dupeCount > 0)
            {
                lblStatus.Text = "School named " + school + "already exists in the database";
            }
            else
            {
                string add1 = HttpUtility.HtmlEncode(txtAdd1.Text);
                string add2 = HttpUtility.HtmlEncode(txtAdd2.Text);
                string city = HttpUtility.HtmlEncode(txtCity.Text);
                string state = HttpUtility.HtmlEncode(txtState.Text);
                string zip = HttpUtility.HtmlEncode(txtZip.Text);
                string maskedPhone = HttpUtility.HtmlEncode(txtPhone.Text);
                string result = Regex.Replace(maskedPhone, @"[^0-9]", "");
                SqlInt64 phone = SqlInt64.Parse(result);

                string sql = "Insert into SCHOOL(NAME, ADDRESS1, ADDRESS2, CITY, STATE, ZIPCODE, PHONE) " +
                    "VALUES(@SCHOOL, @ADD1, @ADD2, @CITY, @STATE, @ZIP, @PHONE)";

                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@SCHOOL", school);
                cmd.Parameters.AddWithValue("@ADD1", add1);
                cmd.Parameters.AddWithValue("@ADD2", add2);
                cmd.Parameters.AddWithValue("@CITY", city);
                cmd.Parameters.AddWithValue("@STATE", state);
                cmd.Parameters.AddWithValue("@ZIP", zip);
                cmd.Parameters.AddWithValue("@PHONE", phone);

                cmd.ExecuteNonQuery();
                connection.Close();
                lblStatus.Text = "Successfully added school " + school;
            }                
        }
    }
}