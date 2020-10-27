using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class StudentDashboard : System.Web.UI.Page
    {
        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Session["TYPE"] != null)
            {
                this.MasterPageFile = (Session["Master"].ToString());
                if (Session["TYPE"].ToString() != "Coordinator" && Session["TYPE"].ToString() != "Student")
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
    }
}