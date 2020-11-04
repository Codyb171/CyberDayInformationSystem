using System;
using System.Web.UI;

namespace CyberDayInformationSystem
{
    public partial class Parent : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                string user = Session["NAME"].ToString();
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