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
    public partial class AttendeeReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["User"] == null)
            //{
            //    Session.Add("Redirected", "Yes");
            //    Response.Redirect("BadSession.aspx");
            //}
        }
        public void StudentRosterGrid()
        {
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString.ToString();
            string sql = "SELECT(S.FIRSTNAME + ' ' + S.LASTNAME) AS NAME, (G.FIRSTNAME + ' ' + G.LASTNAME) AS NAME," + 
                "S.MEALTICKET FROM STUDENT S RIGHT OUTER JOIN GUARDIAN G on G.GUARDIANID = S.GUARDIAN";
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(cs);
            SqlDataAdapter adapt = new SqlDataAdapter(sql, conn);
            conn.Open();
            adapt.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                RosterGridView.DataSource = dt;
                RosterGridView.DataBind();
            }
            else
            {
                dt = new DataTable();
                DataColumn dc1 = new DataColumn("NAME");
                dt.Columns.Add(dc1);
                DataRow dr1 = dt.NewRow();
                dr1[0] = "No Students Registered";
                dt.Rows.Add(dr1);
                RosterGridView.DataSource = dt;
                RosterGridView.DataBind();
            }
        }

        protected void RunBtn_Click(object sender, EventArgs e)
        {
            StudentRosterGrid();

        }

}
}