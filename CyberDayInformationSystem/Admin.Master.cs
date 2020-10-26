using System;

namespace CyberDayInformationSystem
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                string user = Session["User"].ToString();
                UserLoggedIn.Text = "Welcome " + user + "\t";
                LogBtn.Text = "Logout?";
            }
        }
        protected void LogBtn_Click(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                Session.Abandon();
                Response.Redirect("BadSession.aspx");
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
        }
    }
}