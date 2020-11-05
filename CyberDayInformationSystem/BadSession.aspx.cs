using System;
using System.Drawing;
using System.Web.UI;

namespace CyberDayInformationSystem
{
    public partial class BadSession : Page
    {
        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Session["TYPE"] != null)
            {
                MasterPageFile = (Session["Master"].ToString());
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Redirected"] != null)
            {
                if(Session["Redirected"].ToString() == "0")
                {
                    StatusLbl.Text = "Access To Confidential Records Denied!!";
                    StatusLbl.ForeColor = Color.Red;
                    AltLbl.Text = "Please Login First";
                }
                else
                {

                    StatusLbl.Text = "Access To Confidential Records Denied!!";
                    StatusLbl.ForeColor = Color.Red;
                    AltLbl.Text = "You do not have permission to access this page";
                    RedirectBtn.Visible = false;
                }

            }
        }

        protected void RedirectBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginPage.aspx");
        }
    }
}