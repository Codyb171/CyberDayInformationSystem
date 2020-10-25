using System;
using System.Web.UI;

namespace CyberDayInformationSystem
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                LogBtn.Text = "Logout?";
                SignUpBtn.Visible = false;
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

        protected void SignUpBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserCreation.aspx");
        }
    }
}