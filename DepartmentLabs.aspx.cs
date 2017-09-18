using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

public partial class DepartmentLabs : System.Web.UI.Page
{
    DataAccessLayer DAL;
    //MySqlDataReader drFetchUserDetails;
    protected void Page_Load(object sender, EventArgs e)
    {
        DAL = new DataAccessLayer();
        if (Request.QueryString["labs"] == "department")
        {
            if (Session["userPosition"].ToString() != "Admin" && Session["userPosition"].ToString() != "Dept_Chair")
            {
                Response.Redirect("CustomePage.aspx");
            }
        }
        else if (Request.QueryString["labs"] == "college")
        {
            if (Session["userPosition"].ToString() != "Admin" && Session["userPosition"].ToString() != "Dean")
            {
                Response.Redirect("CustomePage.aspx");
            }
        }
       
        if (!IsPostBack)
        {
            if (Request.QueryString["labs"] == "department")
            {
                if (Session["userPosition"].ToString() == "Dept_Chair" || Session["userPosition"].ToString() == "Admin")
                {
                    try
                    {
                        string userID = Session["userID"].ToString();
                        string userPosition = Session["userPosition"].ToString();
                        DataTable dt = DAL.FetchDeptLabDetails(userID, userPosition);
                        gvDeptLabs.DataSource = dt;
                        gvDeptLabs.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error Occured while fetching dept labs')</script>");
                    }
                }
            }
            if (Request.QueryString["labs"] == "college")
            {
                if (Session["userPosition"].ToString() == "Dean" || Session["userPosition"].ToString() == "Admin")
                {
                    try
                    {
                        string userID = Session["userID"].ToString();
                        string userPosition = Session["userPosition"].ToString();
                        DataTable dt = DAL.FetchCollegeLabDetails(userID, userPosition);
                        gvDeptLabs.DataSource = dt;
                        gvDeptLabs.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error Occured while fetching college labs')</script>");
                    }
                }
            }
            if (Request.QueryString["labs"] == "univ")
            {
                if (Session["userPosition"].ToString() == "Vice Provost" || Session["userPosition"].ToString() == "Admin")
                {
                    try
                    {
                        string userID = Session["userID"].ToString();
                        string userPosition = Session["userPosition"].ToString();
                        DataTable dt = DAL.FetchUnivLabDetails(userID, userPosition);
                        gvDeptLabs.DataSource = dt;
                        gvDeptLabs.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error Occured while fetching college labs')</script>");
                    }
                }
            }
            if (!IsPostBack)
            {
                try
                {
                    DataTable dt = DAL.FetchFaculty();
                    ddlAssignto.DataSource = dt;
                    ddlAssignto.DataTextField = "Name";
                    ddlAssignto.DataValueField = "userID";
                    ddlAssignto.DataBind();

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Some Error Occured2')</script>");
                }
            }
        }
    }

    protected void gvDeptLabs_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnUnassign.Visible = false;
        pnlAssignLabs.Visible = true;
        txtLabID.Text=gvDeptLabs.SelectedRow.Cells[1].Text;
        txtAssignedBy.Text = Session["userID"].ToString();
        txtStatus.Text = gvDeptLabs.SelectedRow.Cells[5].Text.ToString();
        if(gvDeptLabs.SelectedRow.Cells[5].Text.ToString() == "Assigned")
        {
            btnUnassign.Visible = true;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtAssignedBy.Text = "";
        txtLabID.Text = "";
        ddlAssignto.SelectedIndex = 0;
        txtStatus.Text = "";
        pnlAssignLabs.Visible = false;
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        DateTime startDate = DateTime.Today;
        if (txtStatus.Text == "Available")
        {
            int labID = Convert.ToInt32(txtLabID.Text);
            string assignto = ddlAssignto.SelectedValue.ToString();
            if (assignto == "99")
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please select the Faculty";
            }
            else
            {
                string assignedBy = txtAssignedBy.Text.ToString();
                int success = DAL.AssignLabs(labID, assignto, assignedBy,startDate);
                if (Request.QueryString["labs"] == "department")
                {
                    string userID = Session["userID"].ToString();
                    string userPosition = Session["userPosition"].ToString();
                    DataTable dt = DAL.FetchDeptLabDetails(userID, userPosition);
                    gvDeptLabs.DataSource = dt;
                    gvDeptLabs.DataBind();
                }
                else if (Request.QueryString["labs"] == "college")
                {
                    string userID = Session["userID"].ToString();
                    string userPosition = Session["userPosition"].ToString();
                    DataTable dt = DAL.FetchCollegeLabDetails(userID, userPosition);
                    gvDeptLabs.DataSource = dt;
                    gvDeptLabs.DataBind();
                }
                txtAssignedBy.Text = "";
                txtLabID.Text = "";
                ddlAssignto.SelectedIndex = 0;
                if (success == 1)
                {
                    Response.Write("<script>alert('The lab has been assigned successfully')</script>");
                }
                else
                {
                    Response.Write("<script>alert('The lab is not assigned.Some error occured')</script>");
                }
            }
        }
        else if(txtStatus.Text == "Assigned")
        {
           Response.Write("<script>alert('The lab is already assigned')</script>");
        }
    }

    protected void btnUnassign_Click(object sender, EventArgs e)
    {
        int labID = Convert.ToInt32(gvDeptLabs.SelectedRow.Cells[1].Text);
        int sucess = DAL.UnassignLabs(labID);
        if(sucess ==1)
        {
            Response.Write("<script>alert('Unassigned Successfully')</script>");
        }
        else
        {
            Response.Write("<script>alert('Error in the process')</script>");
        }
        
        if (Request.QueryString["labs"] == "department")
                {
                    string userID = Session["userID"].ToString();
                    string userPosition = Session["userPosition"].ToString();
                    DataTable dt = DAL.FetchDeptLabDetails(userID, userPosition);
                    gvDeptLabs.DataSource = dt;
                    gvDeptLabs.DataBind();
                }
                else if (Request.QueryString["labs"] == "college")
                {
                    string userID = Session["userID"].ToString();
                    string userPosition = Session["userPosition"].ToString();
                    DataTable dt = DAL.FetchCollegeLabDetails(userID, userPosition);
                    gvDeptLabs.DataSource = dt;
                    gvDeptLabs.DataBind();
                }
                txtAssignedBy.Text = "";
                txtLabID.Text = "";
                ddlAssignto.SelectedIndex = 0;
        
    }
}