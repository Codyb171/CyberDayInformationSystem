using System;
using System.Web.UI;

namespace CyberDayInformationSystem
{
    public partial class About : Page
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

        }
    }
}