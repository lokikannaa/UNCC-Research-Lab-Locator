using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

public partial class AddEditLabsaspx : System.Web.UI.Page
{
    DataAccessLayer DAL;
    MySqlDataReader drFetchUserDetails;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["userPosition"].ToString()!="Admin")
        {
            Response.Redirect("CustomePage.aspx");
        }
        DAL = new DataAccessLayer();
        if (!IsPostBack)
        {
            try
            {
                DataTable dt = DAL.FetchLabDetails();
                gvLabs.DataSource = dt;
                gvLabs.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Some Error Occured')</script>");
            }

            try
            {
                DataTable dt = DAL.FetchDepartmentDetails();
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataValueField = "departmentID";
                ddlDepartment.DataTextField = "deptName";
                ddlDepartment.DataBind();
                ddlDepartment1.DataSource = dt;
                ddlDepartment1.DataValueField = "departmentID";
                ddlDepartment1.DataTextField = "deptName";
                ddlDepartment1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Some Error Occured')</script>");
            }
            
        }
    }

    protected void btnAddEditLabs_Click(object sender, EventArgs e)
    {
        pnlLabs.Visible = true;
        pnlUpdateLabs.Visible = false;    
    }


    //protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    string userID = Convert.ToString(gvUser.DataKeys[e.RowIndex].Values[0]);
    //    DAL.DeleteUser(userID);
    //    DataTable dt = DAL.FetchUserDetails();
    //    gvUser.DataSource = dt;
    //    gvUser.DataBind();
    //}

    protected void btnLab_Click(object sender, EventArgs e)
    {
        string message = "";
        try
        {
            DataTable dt;
            string labType = txtLabType.Text.ToString();
            int organizationID = Convert.ToInt32(ddlOrganization.SelectedValue.ToString());
            int collegeID = Convert.ToInt32(ddlCollege.SelectedValue.ToString());
            int departmentID = Convert.ToInt32(ddlDepartment.SelectedValue.ToString());
            string area = txtLabArea.Text.ToString();
            //string status = txtStatus.Text.ToString();
            message = DAL.AddLabs(labType, organizationID, collegeID, departmentID, area);
            dt = DAL.FetchLabDetails();
            gvLabs.DataSource = dt;
            gvLabs.DataBind();
            txtLabArea.Text = "";
            txtLabType.Text = "";
            //txtStatus.Text = "";
            ddlCollege.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            ddlOrganization.SelectedIndex = 0;
            Response.Write("<script>alert('"+message+"')</script>");
        }
        catch
        {
            Response.Write("<script>alert('Error occured while Adding Labs')</script>");
        }
    }
    protected void btnUpdateLabs_Click(object sender, EventArgs e)
    {
        string message = "";
        try
        {
            DataTable dt;
            int labID = Convert.ToInt16(txtLabID.Text);
            string labType = txtLabType1.Text.ToString();
            int organizationID = Convert.ToInt32(ddlOrganization1.SelectedValue.ToString());
            int collegeID = Convert.ToInt32(ddlCollege1.SelectedValue.ToString());
            int departmentID = Convert.ToInt32(ddlDepartment1.SelectedValue.ToString());
            string area = txtLabArea1.Text.ToString();
            //string status = txtStatus.Text.ToString();
            message = DAL.UpdateLabs(labID,labType, organizationID, collegeID, departmentID, area);
            dt = DAL.FetchLabDetails();
            gvLabs.DataSource = dt;
            gvLabs.DataBind();
            txtLabArea1.Text = "";
            txtLabType1.Text = "";
            txtLabID.Text = "";
            ddlCollege1.SelectedIndex = 0;
            ddlDepartment1.SelectedIndex = 0;
            ddlOrganization1.SelectedIndex = 0;
            Response.Write("<script>alert('" + message + "')</script>");
        }
        catch
        {
            Response.Write("<script>alert('Error occured while Updating Labs')</script>");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlLabs.Visible = false;
        txtLabArea.Text = "";
        txtLabType.Text = "";
        //txtStatus.Text = "";
        ddlCollege.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        ddlOrganization.SelectedIndex = 0;
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        pnlUpdateLabs.Visible = false;
        txtLabArea1.Text = "";
        txtLabType1.Text = "";
        //txtStatus.Text = "";
        ddlCollege1.SelectedIndex = 0;
        ddlDepartment1.SelectedIndex = 0;
        ddlOrganization1.SelectedIndex = 0;
    }

    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message = "";
        string status = gvLabs.Rows[e.RowIndex].Cells[8].Text.ToString();
        //string status = status.Text.ToString();
        if (status == "Available")
        {
            try
            {
                int labID = Convert.ToInt32(gvLabs.DataKeys[e.RowIndex].Values[0]);
                message = DAL.DeleteLab(labID);
                DataTable dt = DAL.FetchLabDetails();
                gvLabs.DataSource = dt;
                gvLabs.DataBind();
                Response.Write("<script>alert('" + message + "')</script>");
            }
            catch
            {
                Response.Write("<script>alert('Error occured while deleting Labs')</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('The lab is already assigned. You cannot delete this lab.')</script>");
        }
    }

    protected void gvLabs_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (gvLabs.SelectedRow.Cells[8].Text== "Assigned")
        {
            Response.Write("<script>alert('The lab is already assigned. You cannot modify the lab information.')</script>");
            pnlUpdateLabs.Visible = false;
        }
        else
        {
            pnlUpdateLabs.Visible = true;
            pnlLabs.Visible = false;
            txtLabID.Text = gvLabs.SelectedRow.Cells[2].Text.ToString();
            txtLabType1.Text = gvLabs.SelectedRow.Cells[3].Text.ToString();
            txtLabArea1.Text = gvLabs.SelectedRow.Cells[7].Text.ToString();
            ddlOrganization1.SelectedValue = gvLabs.SelectedRow.Cells[4].Text;
            ddlCollege1.SelectedValue = gvLabs.SelectedRow.Cells[5].Text;
            ddlDepartment1.SelectedValue = gvLabs.SelectedRow.Cells[6].Text;
        }
    }
}