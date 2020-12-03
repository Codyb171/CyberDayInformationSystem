using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class ParentStudentForms : System.Web.UI.Page
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
                    guardianID = GetId();
                }
                else
                {
                    guardianID = 0;
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

        public int getStudent(int gID)
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            var connection = new SqlConnection(cs);
            string sql = "Select STUDENTID from student where Guardian = " + gID;
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            int id = 0;

            if (dataReader.Read())
            {
                id = int.Parse(dataReader["STUDENTID"].ToString());
            }
            connection.Close();
            return id;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string photo = "No";
            string email = "No";
            int student = getStudent(guardianID);
            //Generate Email output String
            string output =
                "Photo Release \n I hereby grant CyberDay and their agents the absolute right and permission to use pictures, digital images, or videotapes of My Child, or in which My Child may be included \n" +
                "in whole or in part, or reproductions thereof in color or otherwise for any lawful purpose whatsoever, including but not limited to use in any CyberDay publication or on the\n" +
                "CyberDay websites, without payment or any other consideration. \n" +
                "I hereby waive any right that I may have to inspect and/or approve the finished product or the copy that may be used in connection therewith, wherein My Child's likeness" +
                "\nappears, or the use to which it may be applied.\n" +
                "I hereby	release, discharge,	and	agree to indemnify and hold	harmless CyberDay and their agents from	all	claims,	demands, and causes	of action that I or	My Child have or may\n" +
                " have by reason	of	this authorization or use	of	My	Child’s	photographic portraits,	pictures, digital images or videotapes,	including any liability	by	virtue\n" +
                "of	any	blurring, distortion, alteration, optical illusion,	or use	in	composite form,	whether	intentional	or	otherwise, that may	occur or be	produced\n" +
                "in	the	taking of said images or videotapes, or	in	processing	tending	towards	the	completion	of	the	finished product,	including	publication	on	the\n" +
                "internet, in	brochures,	or	any	other advertisements or promotional materials.\n";
            if (ddlPhotoPermission.SelectedValue == "1")
            {
                output += "I DO authorize CyberDay to use my child's photograph.\n";
                photo = "Yes";

            }
            else
            {
                output += "I DO NOT authorize CyberDay to use my child's photograph.\n";
            }

            output += "Permission to Retain Email\n" +
                      "CyberDay would like to follow our student participant's academic progress and be able to reach out to them to provide guidance and opportunities when we are able.\n";
            if (ddlEmailPermission.SelectedValue == "1")
            {
                output +=
                    "I DO authorize CyberDay to retain my student's email address for the purposes of tracking their\n" +
                    "academic progress and informing them of potential opportunities.\n";
                email = "Yes";
            }
            else
            {
                output += "I DO NOT authorize CyberDay to retain my student's email address.\n";
            }

            output +=
                "I represent that I am at least eighteen (18) years of age and am fully competent to sign this Release on behalf of the student.\n";
            output += "Parent Full Name: " + txtParentName.Text + "\n";
            output += "Parent Email: " + txtParentEmail.Text + "\n";
            output += "Today's Date: " + DateTime.Now;
            //End of Generate
            string parName = HttpUtility.HtmlEncode(txtParentName.Text);
            string parEmail = HttpUtility.HtmlEncode(txtParentEmail.Text);
            string subject = "CyberDay Student Permission Form Receipt";
            EmailCenter.send(parEmail, subject, output);

            //Set PhotoRelease
            var dbcs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;

            var dbcon = new SqlConnection(dbcs);

            dbcon.Open();
            //Save the photo approval to DB
            string updatePR = "Insert into STUDENTPERMISSIONS(PHOTORELEASE,EMAILRETENTION,PARENT,STUDENT) VALUES(@PHOTO, @EMAIL, @PARENT, @STUDENT)";
            var updatePRCmd = new SqlCommand(updatePR, dbcon);
            updatePRCmd.Parameters.AddWithValue("@PHOTO",photo);
            updatePRCmd.Parameters.AddWithValue("@EMAIL",email);
            updatePRCmd.Parameters.AddWithValue("@Parent", guardianID);
            updatePRCmd.Parameters.AddWithValue("@STUDENT",student);
            updatePRCmd.ExecuteNonQuery();
            dbcon.Close();
        }
    }
}