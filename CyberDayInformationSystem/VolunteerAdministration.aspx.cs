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
                ddlShirtSize.Items.Insert(0, new ListItem(String.Empty));
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnPopulate_Click(object sender, EventArgs e)
        {

        }

        protected void FunctionSelection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void VolunteerType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void PrevVol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}