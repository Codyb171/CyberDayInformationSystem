using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace CyberDayInformationSystem
{
	public partial class TeacherDashboard : Page
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
		protected void Page_Load(object sender, EventArgs e)
		{
			
		}
		public void GetTeacherInfo(string email)
		{
			string cs = ConfigurationManager.ConnectionStrings["INFO"].ConnectionString;
			SqlConnection connection = new SqlConnection(cs);
			SqlCommand command;
			string sql = "Select SCHOOL from TEACHER where EMAILADD = " + email;
			command = new SqlCommand(sql, connection);

			connection.Open();
			SqlDataReader dataReader = command.ExecuteReader();
			if (dataReader.Read())
			{
				Session.Add("SCHOOL", dataReader["SCHOOL"].ToString());
			}
		}
    }
}