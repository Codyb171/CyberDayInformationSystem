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
            string sql = "Select FIRSTNAME, LASTNAME, NOTES FROM STUDENT " +
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
        }

        protected void StudentModifyDtl_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {
            studentModDtl.PageIndex = e.NewPageIndex;
            studentModDtl.DataBind();
        }
    }
}