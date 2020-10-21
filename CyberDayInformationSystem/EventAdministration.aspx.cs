using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CyberDayInformationSystem
{
    public partial class EventAdministration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void FunctionSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FunctionSelection.SelectedValue == "1")
            {
                SelectedFunction.ActiveViewIndex = 0;
            }
            if (FunctionSelection.SelectedValue == "2")
            {
                SelectedFunction.ActiveViewIndex = 1;
            }
            if (FunctionSelection.SelectedValue == "3")
            {
                SelectedFunction.ActiveViewIndex = 2;
            }
        }

        protected void CreateBut_Click(object sender, EventArgs e)
        {

        }

        protected void EventDDL_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ModifyBut_Click(object sender, EventArgs e)
        {

        }

        protected void EventDelDDL_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DelBut_Click(object sender, EventArgs e)
        {

        }
    }
}