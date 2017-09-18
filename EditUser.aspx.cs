using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

public partial class EditUser : System.Web.UI.Page
{
    DataAccessLayer DAL;
    MySqlDataReader drFetchUserDetails;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        DAL = new DataAccessLayer();
        if (Session["userPosition"].ToString() != "Admin")
        {
            Response.Redirect("CustomePage.aspx");
        }
        if (!IsPostBack)
        {
            //try
            //{
            //    drFetchUserDetails = DAL.FetchUserDetails();
            //    gvUser.DataSource = drFetchUserDetails;
            //    gvUser.DataBind();
            //    drFetchUserDetails.Close();
            //}
            //catch (Exception ex)
            //{
            //    Response.Write("<script>alert('Some Error Occured')</script>");
            //}

            try
            {
                DataTable dt = DAL.FetchUserDetails();
                gvUser.DataSource = dt;
                gvUser.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Some Error Occured')</script>");
            }
            //FetchDepartmentList
            try
            {
                DataTable dt = DAL.FetchDepartmentList();
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataTextField = "deptName";
                ddlDepartment.DataValueField = "departmentID";
                ddlDepartment.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Some Error Occured')</script>");
            }
        }
    }

    protected void gvUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlAddUser.Visible = false;
        pnlDeleteEditUser.Visible = true;
        try
        {
            txtUserID0.Text = gvUser.SelectedRow.Cells[2].Text;
            txtFName0.Text = gvUser.SelectedRow.Cells[3].Text;
            txtLName0.Text = gvUser.SelectedRow.Cells[4].Text;
            txtPassword0.Text = gvUser.SelectedRow.Cells[6].Text;

        }
        catch(Exception ex)
        {
            Response.Write("<script>alert('Some Error Occured')</script>");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtUserID.Text = "";
        txtFName.Text = "";
        txtLName.Text = "";
        txtPassword.Text = "";
        ddlDepartment.SelectedIndex = 0;
        ddlDesignation.SelectedIndex = 0;
        pnlAddUser.Visible = false;
        pnlDeleteEditUser.Visible = false;
    }
    protected void btnCancel0_Click(object sender, EventArgs e)
    {
        txtUserID0.Text = "";
        txtFName0.Text = "";
        txtLName0.Text = "";
        txtPassword0.Text = "";
        pnlAddUser.Visible = false;
        pnlDeleteEditUser.Visible = false;
    }
    protected void btnAddUserLink_Click(object sender, EventArgs e)
    {
        pnlAddUser.Visible = true;
        pnlDeleteEditUser.Visible = false;

    }


    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        string message = "";
        try
        {
            DAL = new DataAccessLayer();
            DataTable dt;
            string userID = txtUserID.Text.ToString();
            string firstName = txtFName.Text.ToString();
            string lastName = txtLName.Text.ToString();
            string userPosition = ddlDesignation.SelectedItem.Text.ToString();
            string password = txtPassword.Text.ToString();
            int deptID = Convert.ToInt32(ddlDepartment.SelectedValue);
            message = DAL.AddUser(userID, firstName, lastName, userPosition, password, deptID);
            dt = DAL.FetchUserDetails();
            gvUser.DataSource = dt;
            gvUser.DataBind();
            txtUserID.Text = "";
            txtFName.Text = "";
            txtLName.Text = "";
            txtPassword.Text = "";
            ddlDepartment.SelectedIndex = 0;
            ddlDesignation.SelectedIndex = 0;
            Response.Write("<script>alert('" + message + "')</script>");
        }
        catch
        {
            Response.Write("<script>alert('Some Error Occured while adding user')</script>");
        }
    }

    //protected void btnDeleteUser_Click(object sender, EventArgs e)
    //{
    //    DAL = new DataAccessLayer();
    //    DataTable dt;
    //    string userID = txtUserID0.Text.ToString();
    //    DAL.DeleteUser(userID);
    //    dt = DAL.FetchUserDetails();
    //    gvUser.DataSource = dt;
    //    gvUser.DataBind();
    //    txtUserID0.Text = "";
    //    txtFName0.Text = "";
    //    txtLName0.Text = "";
    //    txtPassword0.Text = "";
    //}

    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message = "";
        try
        {
            string userID = Convert.ToString(gvUser.DataKeys[e.RowIndex].Values[0]);
            message=DAL.DeleteUser(userID);
            DataTable dt = DAL.FetchUserDetails();
            gvUser.DataSource = dt;
            gvUser.DataBind();
            Response.Write("<script>alert('"+message+"')</script>");
        }
        catch
        {
            Response.Write("<script>alert('Error occured while deleting user')</script>");
        }
    }
}