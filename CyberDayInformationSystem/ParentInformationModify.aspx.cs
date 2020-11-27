using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

            LoadParentInfo();
        }

        protected void LoadParentInfo()
        {
            int _idToEdit = GetId();
            Session.Add("GuardianID", _idToEdit);
            string CS = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string sql = "Select G.GUARDIANID, G.FIRSTNAME, G.LASTNAME, G.EMAILADD, G.PHONE FROM GUARDIAN G " +
                    "WHERE G.GUARDIANID = " + _idToEdit;
                SqlCommand cmd = new SqlCommand(sql, con);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                DataTable dt = new DataTable();
                sda.Fill(dt);
                parentModifyDtl.DataSource = dt;
                parentModifyDtl.DataBind();
                con.Close();
            }
        }

        protected int GetId()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            var connection = new SqlConnection(cs);
            string sql = "Select GUARDIANID FROM GUARDIAN WHERE CONCAT(FIRSTNAME, ' ', LASTNAME) LIKE '" + Session["NAME"] + "'";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();

            if (dataReader.Read())
            {
                _idToEdit = int.Parse(dataReader["GUARDIANID"].ToString());
            }
            connection.Close();
            return _idToEdit;

        }

        protected void parentModifyDtl_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            if (parentModifyDtl.CurrentMode == DetailsViewMode.ReadOnly)
            {
                parentModifyDtl.ChangeMode(DetailsViewMode.Edit);
                LoadParentInfo();
            }
            else if (parentModifyDtl.CurrentMode == DetailsViewMode.Edit)
            {

                parentModifyDtl.ChangeMode(DetailsViewMode.ReadOnly);
                LoadParentInfo();
            }
        }

        protected void parentModifyDtl_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            TextBox firstName = parentModifyDtl.Rows[0].Cells[1].Controls[0] as TextBox;
            TextBox lastName = parentModifyDtl.Rows[1].Cells[1].Controls[0] as TextBox;
            TextBox email = parentModifyDtl.Rows[2].Cells[1].Controls[0] as TextBox;
            TextBox phone = parentModifyDtl.Rows[3].Cells[1].Controls[0] as TextBox;

            string CS = ConfigurationManager.ConnectionStrings["AUTH"].ConnectionString;
            var authcon = new SqlConnection(CS);
            authcon.Open();


            string sql = "Update USERS set FIRSTNAME = '" + firstName.Text + "', LASTNAME = '" +
                lastName.Text + "' WHERE CONCAT(FIRSTNAME, ' ', LASTNAME) LIKE '" + Session["NAME"] + "'";
            var stuParConnectCmd = new SqlCommand(sql, authcon);
            stuParConnectCmd.ExecuteNonQuery();
            authcon.Close();
            UpdateGuardian(firstName.Text, lastName.Text, email.Text, phone.Text);
        }

        protected void UpdateGuardian(string firstName, string lastName, string email, string phone)
        {
            string CS = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            var authcon = new SqlConnection(CS);
            authcon.Open();
            string sql = "Update GUARDIAN set FIRSTNAME = '" + firstName + "', LASTNAME = '" +
                    lastName + "', EMAILADD = '" + email + "', PHONE = " + Int32.Parse(phone)
                    + " where GUARDIANID = " + _idToEdit;

            SqlDataAdapter SQLAdapter = new SqlDataAdapter(sql, authcon);
            DataTable data = new DataTable();
            SQLAdapter.Fill(data);
            parentModifyDtl.ChangeMode(DetailsViewMode.ReadOnly);
            authcon.Close();
            LoadParentInfo();
        }

    }
}


