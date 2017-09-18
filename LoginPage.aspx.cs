using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;


public partial class LoginPage : System.Web.UI.Page
{
    DataAccessLayer DAL;
    protected void Page_Load(object sender, EventArgs e)
    {
        DAL = new DataAccessLayer();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        DataTable dt;
        string userID = txtUsername.Text.ToString();
        string password = txtPassword.Text.ToString();
        dt = DAL.LogIn(userID,password);
        if(dt.Rows.Count>0)
        {
            FindUserPosition();
            Session["userID"] = txtUsername.Text;
            Response.Redirect("Default.aspx");
        }
        else
        {
            Response.Write("<script>alert('Please enter valid username and password')</script>");
        }
    }

    public void FindUserPosition()
    {
        string userPosition = "";
        string userID = txtUsername.Text.ToString();
        string password = txtPassword.Text.ToString();
        userPosition = DAL.FindUserPosition(userID, password);
        if(userPosition != null)
        {
            Session["userPosition"] = userPosition;
        }
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }
}
