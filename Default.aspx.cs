using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    DataAccessLayer DAL;
    protected void Page_Load(object sender, EventArgs e)
    {
        DAL = new DataAccessLayer();
        string position = Session["userPosition"].ToString();
        string userID = Session["userID"].ToString();
        if (position == "Faculty")
        {
            DataTable dt = new DataTable();
            dt = DAL.ViewFacultyLabStatus(userID);
            gvLabStatus.DataSource = dt;
            gvLabStatus.DataBind();
        }
        else
        {
            DataTable dt = new DataTable();
            dt = DAL.ViewLabStatus(position);
            gvLabStatus.DataSource = dt;
            gvLabStatus.DataBind();
        }
    }
}