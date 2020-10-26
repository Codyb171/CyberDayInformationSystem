using System;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class VolunteerAdministration : System.Web.UI.Page
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
            if(!Page.IsPostBack)
            {

            }
        }

        protected void btnSelEventNext_Click(object sender, EventArgs e)
        {

        }

        protected void btnSelDateNext_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}