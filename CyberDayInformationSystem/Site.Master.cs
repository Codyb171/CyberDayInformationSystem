using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LogBtn_Click(object sender, EventArgs e)
        {
            //if (Session["User"] != null)
            //{
            //    Session.Abandon();
            //    Response.Redirect("BadSession.aspx");
            //}
            //else
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}
        }
    }
}