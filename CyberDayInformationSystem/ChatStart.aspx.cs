using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class ChatStart : System.Web.UI.Page
    {
        void Page_PreInit(Object sender, EventArgs e)
        {
            if (Session["TYPE"] != null)
            {
                MasterPageFile = (Session["Master"].ToString());
                if (Session["TYPE"].ToString() != "Coordinator" && Session["TYPE"].ToString() != "Student Volunteer" && Session["TYPE"].ToString() != "Staff Volunteer"
                    && Session["TYPE"].ToString() != "Teacher")
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
            if (Session["NAME"] != null)
            {
                hiddenName.Value = Session["NAME"].ToString();
            }
        }
    }
}