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
                string type = Session["TYPE"].ToString();
                if (type == "Student")
                {
                    this.MasterPageFile = "~/Student.Master";
                }
                else if (type == "Coordinator")
                {
                    this.MasterPageFile = "~/Admin.Master";
                }
                else if (type == "Student Volunteer" || type == "Staff Volunteer")
                {
                    this.MasterPageFile = "~/Volunteer.Master";
                }
            }
            else
            {
                this.MasterPageFile = "~/Site.Master";
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