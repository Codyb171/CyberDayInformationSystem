using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class BadSession : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Redirected"] != null)
            {
                StatusLbl.Text = "Access To Confidential Records Denied!!";
                StatusLbl.ForeColor = System.Drawing.Color.Red;
                AltLbl.Text = "Please Login First";
            }
        }

        protected void RedirectBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginPage.aspx");
        }
    }
}