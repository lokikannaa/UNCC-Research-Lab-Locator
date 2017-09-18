using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.Common;

/// <summary>
/// Summary description for DataAccessLayer
/// </summary>
public class DataAccessLayer
{
    MySqlConnection con;
    MySqlDataAdapter da;
    static DataTable dtUser;
    static DataTable dtLab;
    
    public DataAccessLayer()
    {
        con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    }
    //public MySqlDataReader FetchUserDetails()
    //{
    //    MySqlDataReader drFetchUserDetails = null;
    //    try
    //    {
    //        MySqlCommand cmd = new MySqlCommand("select userID,firstName,lastName,userPosition,password,u.departmentID,deptName from Users u inner join Department d on u.departmentID = d.departmentID ", con);
    //        con.Open();
    //        drFetchUserDetails = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            
    //    }
    //    catch(MySqlException ex)
    //    {
    //        drFetchUserDetails = null;
    //    }
    //    return drFetchUserDetails;
    //}
    public DataTable FetchUserDetails()
    {
        dtUser = new DataTable();
        da = new MySqlDataAdapter("select userID, firstName, lastName, userPosition, password, u.departmentID, deptName from Users u inner join Department d on u.departmentID = d.departmentID ", con);
        try
        {
            da.Fill(dtUser);
        }
        catch (MySqlException ex)
        {
            dtUser = null;
        }
        return dtUser;
    }

    public DataTable FetchDepartmentList()
    {
        DataTable dt = new DataTable();
        da = new MySqlDataAdapter("select * from Department",con);
        try
        {
            da.Fill(dt);
        }
        catch (MySqlException ex)
        {
            dt = null;
        }
        return dt;
    }

    public string AddUser(string userID, string firstName, string lastName, string userPosition, string password, int departmentID)
    {
        string message = "";
        try
        {
            da = new MySqlDataAdapter();
            DataRow drUser = dtUser.NewRow();
            drUser[0] = userID;
            drUser[1] = firstName;
            drUser[2] = lastName;
            drUser[3] = userPosition;
            drUser[4] = password;
            drUser[5] = departmentID;
            dtUser.Rows.Add(drUser);
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Users (userID,firstName,lastName,userPosition,password,departmentID) VALUES(@userID,@firstName,@lastName,@userPosition,@password,@departmentID)", con);
            cmd.Parameters.AddWithValue("@userID", drUser[0].ToString());
            cmd.Parameters.AddWithValue("@firstName", drUser[1].ToString());
            cmd.Parameters.AddWithValue("@lastName", drUser[2].ToString());
            cmd.Parameters.AddWithValue("@userPosition", drUser[3].ToString());
            cmd.Parameters.AddWithValue("@password", drUser[4].ToString());
            cmd.Parameters.AddWithValue("@departmentID", drUser[5].ToString());
            da.InsertCommand = cmd;
            da.Update(dtUser);
            message = "User added successfully";
            return message;
        }
        catch(Exception ex)
        {
            message = "User already exists";
            return message;
        }
    }
    public string AddLabs(string labType, int organizationID, int collegeID, int departmentID, string area)
    {
        string message = "";
        try
        {
            da = new MySqlDataAdapter();
            DataRow drLabs = dtLab.NewRow();
            drLabs[1] = labType;
            drLabs[2] = organizationID;
            drLabs[3] = collegeID;
            drLabs[4] = departmentID;
            drLabs[5] = area;
            //drLabs[6] = status;
            dtLab.Rows.Add(drLabs);
            MySqlCommand cmd = new MySqlCommand("Insert into Labs(labType,organizationID,collegeID,departmentID,labAreaSqft) Values(@labType,@organizationID,@collegeID,@departmentID,@area)", con);
            cmd.Parameters.AddWithValue("@labType", drLabs[1].ToString());
            cmd.Parameters.AddWithValue("@organizationID", drLabs[2].ToString());
            cmd.Parameters.AddWithValue("@collegeID", Convert.ToInt32(drLabs[3].ToString()));
            cmd.Parameters.AddWithValue("@departmentID", Convert.ToInt32(drLabs[4].ToString()));
            cmd.Parameters.AddWithValue("@area", drLabs[5].ToString());
            //cmd.Parameters.AddWithValue("@status", drLabs[6].ToString());
            da.InsertCommand = cmd;
            da.Update(dtLab);
            message = "Lab is added successfully";
            return message;
        }
        catch(Exception ex)
        {
            message = "Error occured while adding labs";
            return message;
        }
    }
    public string UpdateLabs(int labID,string labType, int organizationID, int collegeID, int departmentID, string labAreaSqft)
    {
            string message = "";
        int success = 0;
            //da = new MySqlDataAdapter();
            //DataRow drLabs = dtLab.NewRow();
            //drLabs[1] = labType;
            //drLabs[2] = organizationID;
            //drLabs[3] = collegeID;
            //drLabs[4] = departmentID;
            //drLabs[5] = area;
            //drLabs[6] = status;
            //dtLab.Rows.Add(drLabs);
        MySqlCommand cmd = new MySqlCommand("Update Labs set labType=@labType,organizationID=@organizationID,collegeID=@collegeID,departmentID=@departmentID,labAreaSqft=@labAreaSqft where labID = @labID", con);
            cmd.Parameters.AddWithValue("@labType",labType );
            cmd.Parameters.AddWithValue("@organizationID",organizationID );
            cmd.Parameters.AddWithValue("@collegeID",collegeID );
            cmd.Parameters.AddWithValue("@departmentID",departmentID );
            cmd.Parameters.AddWithValue("@labAreaSqft", labAreaSqft);
            cmd.Parameters.AddWithValue("@labID", labID);
            //cmd.Parameters.AddWithValue("@status", drLabs[6].ToString());
            //da.UpdateCommand = cmd;
            //da.Update(dtLab);
            con.Open();
            try
            {
                success = cmd.ExecuteNonQuery();
                if (success == 1)
                {
                    message = "Lab details are updated successfully";                    
                }
                con.Close();
             }
            catch (Exception ex)
            {
                message = "Error occured while updating labs";               
            }
            return message;
    }

    public int AssignLabs(int labID,string assignto,string assignedBy,DateTime startDate)
    {
        int success = 0;
        MySqlCommand cmd = new MySqlCommand("Insert into LabAssignment(labID,userID,assignedBy,start_date) Values(@labID,@userID,@assignedBy,@startDate)", con);
        cmd.Parameters.AddWithValue("@labID", labID);
        cmd.Parameters.AddWithValue("@userID", assignto);
        cmd.Parameters.AddWithValue("@assignedBy", assignedBy);
        cmd.Parameters.AddWithValue("@startDate", startDate);
        cmd.Connection = con;
        MySqlCommand cmd1 = new MySqlCommand("Update Labs set status = 'Assigned' where labID = @labID", con);
        cmd1.Parameters.AddWithValue("@labID", labID);
        cmd1.Connection = con;
        con.Open();
        try
        {
            success = cmd.ExecuteNonQuery();
            if (success == 1)
            {
                cmd1.ExecuteNonQuery();
            }
        }
        catch(Exception ex)
        {
            success = 99;
        }
        con.Close();
        return success;
    }

    public string DeleteUser(string userID)
    {
        string message = "";
        try
        {
            MySqlCommand cmd = new MySqlCommand("Delete from Users where userID=@userID", con);
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            message = "User is deleted successfully";
            return message;
        }
        catch
        {
            message = "Error occured while deleting user";
            return message;
        }
    }
    public string DeleteLab(int labID)
    {
        string message = "";
        try
        {
            MySqlCommand cmd = new MySqlCommand("Delete from Labs where labID=@labID", con);
            cmd.Parameters.AddWithValue("@labID", labID);
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            message = "Lab is deleted successfully";
            return message;
        }
        catch(Exception ex)
        {
            message = "The lab is already assigned. You cannot delete this lab.";
            return message;
        }
    }

    public DataTable FetchLabDetails()
    {
        dtLab = new DataTable();
        da = new MySqlDataAdapter("select * from Labs", con);
        try
        {
            da.Fill(dtLab);
        }
        catch (MySqlException ex)
        {
            dtLab = null;
        }
        return dtLab;
    }
    public DataTable FetchDepartmentDetails()
    {
        DataTable dt = new DataTable();
        da = new MySqlDataAdapter("select departmentID,deptName from Department", con);
        try
        {
            da.Fill(dt);
        }
        catch (MySqlException ex)
        {
            dt = null;
        }
        return dt;
    }

    public DataTable FetchDeptLabDetails(string userID,string userPosition)
    {
        DataTable dt = new DataTable();
        if (userPosition =="Dept_Chair")
        {
            MySqlCommand cmd = new MySqlCommand("select labID,labType,l.departmentID,d.deptName,l.status from Labs l inner join Department d on l.departmentID = d.departmentID where l.departmentID in(select departmentID from Users where userID =@userID)", con);
            cmd.Parameters.AddWithValue("@userID", userID);
            da = new MySqlDataAdapter(cmd); 
        }
        else if(userPosition=="Admin")
        {
            MySqlCommand cmd = new MySqlCommand("select labID,labType,l.departmentID,d.deptName,l.status from Labs l inner join Department d on l.departmentID = d.departmentID", con);
            cmd.Parameters.AddWithValue("@userID", userID);
            da = new MySqlDataAdapter(cmd);
        }
        try
        {
            da.Fill(dt);
        }
        catch (MySqlException ex)
        {
            dt = null;
        }
        return dt;
    }

    public DataTable FetchCollegeLabDetails(string userID, string userPosition)
    {
        DataTable dt = new DataTable();
        if (userPosition == "Dean" )
        {
            MySqlCommand cmd = new MySqlCommand("select labID,labType,l.departmentID,d.deptName,l.status,c.collegeName from Labs l inner join Department d on l.departmentID = d.departmentID inner join College c on d.collegeID = c.collegeID where l.collegeID in(select collegeID from Department where departmentID in(select departmentID from Users where userID =@userID))", con);
            cmd.Parameters.AddWithValue("@userID", userID);
            da = new MySqlDataAdapter(cmd);
        }
        else if (userPosition == "Admin")
        {
            MySqlCommand cmd = new MySqlCommand("select labID,labType,l.departmentID,d.deptName,l.status,c.collegeName from Labs l inner join Department d on l.departmentID = d.departmentID inner join College c on c.collegeID = l.collegeID", con);
            cmd.Parameters.AddWithValue("@userID", userID);
            da = new MySqlDataAdapter(cmd);
        }
        try
        {
            da.Fill(dt);
        }
        catch (MySqlException ex)
        {
            dt = null;
        }
        return dt;
    }

    public DataTable FetchUnivLabDetails(string userID, string userPosition)
    {
        DataTable dt = new DataTable();
        if (userPosition == "Vice Provost")
        {
            MySqlCommand cmd = new MySqlCommand("select labID,labType,l.departmentID,d.deptName,l.status,c.collegeName from Labs l inner join Department d on l.departmentID = d.departmentID inner join College c on c.collegeID = l.collegeID", con);
            cmd.Parameters.AddWithValue("@userID", userID);
            da = new MySqlDataAdapter(cmd);
        }
        else if (userPosition == "Admin")
        {
            MySqlCommand cmd = new MySqlCommand("select labID,labType,l.departmentID,d.deptName,l.status,c.collegeName from Labs l inner join Department d on l.departmentID = d.departmentID inner join College c on c.collegeID = l.collegeID", con);
            cmd.Parameters.AddWithValue("@userID", userID);
            da = new MySqlDataAdapter(cmd);
        }
        try
        {
            da.Fill(dt);
        }
        catch (MySqlException ex)
        {
            dt = null;
        }
        return dt;
    }

    public DataTable LogIn(string userID,string password)
    {
        DataTable dt = new DataTable();
        MySqlCommand cmd = new MySqlCommand("select * from Users where userID=@userID and password=@password", con);
        cmd.Parameters.AddWithValue("@userID",userID);
        cmd.Parameters.AddWithValue("@password", password);
        da = new MySqlDataAdapter(cmd);
        //da.SelectCommand = cmd;
        try
        {
            da.Fill(dt);
        }
        catch (MySqlException ex)
        {
            dt = null;
        }
        return dt;
    }
    public string FindUserPosition(string userID, string password)
    {
        string userPosition = "";
        MySqlCommand cmd = new MySqlCommand("select userPosition from Users where userID=@userID and password=@password", con);
        con.Open();
        cmd.Parameters.AddWithValue("@userID", userID);
        cmd.Parameters.AddWithValue("@password", password);
        userPosition = cmd.ExecuteScalar().ToString();
        return userPosition;
    }

    public DataTable FetchFaculty()
    {
        DataTable dt = new DataTable();
        da = new MySqlDataAdapter("select CONCAT_WS('', firstName, lastName) as Name,userID from Users where userPosition='Faculty'", con);
        try
        {
            da.Fill(dt);
        }
        catch (MySqlException ex)
        {
            dt = null;
        }
        return dt;
    }

    public int UnassignLabs(int labID)
    {
        int rowsAffected;
        MySqlCommand cmd = new MySqlCommand("sp_UnassignLabs",con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@p_labID",labID);
        try
        {
            con.Open();
            rowsAffected = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            rowsAffected = 99;
            con.Close();
        }
        con.Close();
        return rowsAffected;
    }

    public DataTable ViewLabStatus(string position)
    {
        DataTable dt = new DataTable();
        da = new MySqlDataAdapter("sp_LabStatusView", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@position",position);
        try
        {
            da.Fill(dt);
        }
        catch (MySqlException ex)
        {
            dt = null;
        }
        return dt;
    }

    public DataTable ViewFacultyLabStatus(string facultyID)
    {
        DataTable dt = new DataTable();
        da = new MySqlDataAdapter("sp_FacultyView", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@facultyID", facultyID);
        try
        {
            da.Fill(dt);
        }
        catch (MySqlException ex)
        {
            dt = null;
        }
        return dt;
    }
}