using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UniversityLabs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["userPosition"].ToString() != "Admin" && Session["userPosition"].ToString() != "Dept_Chair")
        //{
            Response.Redirect("CustomePage.aspx");
        //}
    }
}