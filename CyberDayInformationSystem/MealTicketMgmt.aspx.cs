using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class MealTicketMgmt : System.Web.UI.Page
    {
        //void Page_PreInit(Object sender, EventArgs e)
        //{
        //    if (Session["TYPE"] != null)
        //    {
        //        this.MasterPageFile = (Session["Master"].ToString());
        //        if (Session["TYPE"].ToString() != "Coordinator")
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
        protected void Page_Load(object sender, EventArgs e)
        {
            MealTicketGrid();
        }
        public void MealTicketGrid()
        {
            TertiaryGridLbl.Text = "Meal Ticket Data";
            TertiaryGridLbl.Visible = true;
            string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
            string sql = "select(S.FIRSTNAME + ' ' + S.LASTNAME) AS NAME, MT.USED AS RECIEVED FROM MEALTICKET MT JOIN STUDENT S " +
                 "ON MT.MEALTICKETID = S.MEALTICKET JOIN EVENT EV ON MT.EVENT = EV.EVENTID UNION " +
                 "select(T.FIRSTNAME + ' ' + T.LASTNAME) AS NAME, MT.USED AS RECIEVED FROM MEALTICKET MT JOIN TEACHER T ON MT.MEALTICKETID = T.MEALTICKET " +
                 "JOIN EVENT EV ON MT.EVENT = EV.EVENTID UNION select(G.FIRSTNAME +' ' + G.LASTNAME) AS NAME, " +
                 "MT.USED AS RECIEVED FROM MEALTICKET MT JOIN GUARDIAN G ON MT.MEALTICKETID = G.MEALTICKET JOIN EVENT EV ON MT.EVENT = EV.EVENTID " +
                 "UNION select(V.FIRSTNAME +' ' + V.LASTNAME) AS NAME, MT.USED AS RECIEVED FROM MEALTICKET MT " +
                 "JOIN VOLUNTEER V ON MT.MEALTICKETID = V.MEALTICKET JOIN EVENT EV ON MT.EVENT = EV.EVENTID";
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(cs);
            SqlDataAdapter adapt = new SqlDataAdapter(sql, conn);
            conn.Open();
            adapt.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                TertiaryGridView.DataSource = dt;
                TertiaryGridView.DataBind();
            }
        }
    }
}