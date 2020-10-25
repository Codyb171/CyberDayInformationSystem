using System;

namespace CyberDayInformationSystem
{
    public partial class BadSession : System.Web.UI.Page
    {
        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Session["TYPE"] != null)
            {
                this.MasterPageFile = (Session["Master"].ToString());
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Redirected"] != null)
            {
                if(Session["Redirected"].ToString() == "0")
                {
                    StatusLbl.Text = "Access To Confidential Records Denied!!";
                    StatusLbl.ForeColor = System.Drawing.Color.Red;
                    AltLbl.Text = "Please Login First";
                }
                else
                {

                    StatusLbl.Text = "Access To Confidential Records Denied!!";
                    StatusLbl.ForeColor = System.Drawing.Color.Red;
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