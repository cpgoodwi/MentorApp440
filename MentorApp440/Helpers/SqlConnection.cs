using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using MentorApp440.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace MentorApp440.Helpers;

public class SqlConnection
{
    private static MySqlConnection connection;
    private static MySqlCommand cmd = null;
    private static DataTable dt;
    private static MySqlDataAdapter da;
    private static MySqlDataReader dr;


    // This class builds a string connection and uses it to connect to the database.
    // It returns a string that tells you if the connect is successful and send it to the controller
    public static string EstablishConnection()
    {
        string ConnectionResult;
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
        builder.Server = "107.180.1.16";
        builder.UserID = "440fall20224";
        builder.Password = "440fall20224";
        builder.Database = "440fall20224";
        builder.SslMode = MySqlSslMode.None;
        connection = new MySqlConnection(builder.ToString());
        connection.Open();
        if (connection.State == ConnectionState.Open)
        {
            ConnectionResult = " Database Connection Successful";
        }
        else
        {
            ConnectionResult = "Connection Failed";
        }

        return ConnectionResult;
    }


    // This method calls a search all store procedure in the database and stores the data it in a menteeViewModelList
    public static List<MenteeViewModel> RunMenteeQuery()
    {
        List<MenteeViewModel> menteeViewModelList = new List<MenteeViewModel>();

        if (connection.State == ConnectionState.Open)
        {
            string sqlQuery = "Sprouc_SearchMentee";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                MenteeViewModel menteeViewModel = new MenteeViewModel();

                menteeViewModel.OrgID = Convert.ToInt32(dr["orgId"]);
                menteeViewModel.MenteeID = Convert.ToInt32(dr["menteeId"]);
                menteeViewModel.MentorID = Convert.ToInt32(dr["mentorId"]);

                menteeViewModelList.Add(menteeViewModel);
            }
        }

        else
        {
            connection.Close();
        }


        return menteeViewModelList;
    }


    // This method calls a search all store procedure in the database and stores the data it in a organizationViewModelList
    public static List<OrganizationViewModel> RunOrganizationQuery()
    {
        EstablishConnection();
        
        List<OrganizationViewModel> organizationViewModelList = new List<OrganizationViewModel>();

        if (connection.State == ConnectionState.Open)
        {
            string sqlQuery = "Sprouc_SearchOrganization";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            organizationViewModelList.AddRange(from DataRow dr in dt.Rows select new OrganizationViewModel(Convert.ToInt32(dr["orgId"]), dr["orgName"].ToString()));
        }

        else
        {
            connection.Close();
        }

        return organizationViewModelList;
    }


    // This method calls a search all store procedure in the database and stores the data it in a goalViewModelList

    public static List<GoalViewModel> RunGoalQuery()
    {
        List<GoalViewModel> goalViewModelList = new List<GoalViewModel>();

        if (connection.State == ConnectionState.Open)
        {
            string sqlQuery = "Sprouc_SearchGoal";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                GoalViewModel goalViewModel = new GoalViewModel();

                goalViewModel.MemId = Convert.ToInt32(dr["memId"]);
                goalViewModel.GoalId = Convert.ToInt32(dr["goalID"]);
                goalViewModel.GoalStr = dr["goalStr"].ToString();
                int isCompleteNum = Convert.ToInt32(dr["iscomplete"]);
                if (isCompleteNum == 1)
                {
                    goalViewModel.IsComplete = true;
                }

                goalViewModelList.Add(goalViewModel);
            }
        }

        else
        {
            connection.Close();
        }

        return goalViewModelList;
    }


    // Not done with this method
    // Todo: work on retrieving enum from sql to modelList
    // This method calls a search all store procedure in the database and stores the data it in a memberViewModelList

    public static List<MemberViewModel> RunMemberQuery()
    {
        List<MemberViewModel> memberViewModelList = new List<MemberViewModel>();

        if (connection.State == ConnectionState.Open)
        {
            string sqlQuery = "Sprouc_SearchMember";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                MemberViewModel memberViewModel = new MemberViewModel();


                memberViewModel.MemID = Convert.ToInt32(dr["memId"]);
                memberViewModel.OrgID = Convert.ToInt32(dr["orgId"]);
                memberViewModel.UserName = dr["username"].ToString();
                memberViewModel.FullName = dr["fullname"].ToString();
                memberViewModel.Description = dr["description"].ToString();
                memberViewModel.Mentor = dr["mentor"].ToString();
                int isOrgAdminNum = Convert.ToInt32(dr["orgadmin"]);
                if (isOrgAdminNum == 1)
                {
                    memberViewModel.isOrgAdmin = true;
                }

                memberViewModelList.Add(memberViewModel);
            }
        }

        else
        {
            connection.Close();
        }


        return memberViewModelList;
    }


    // This method calls a search all store procedure in the database and stores the data it in a taskViewModelList

    public static List<TaskViewModel> RunTaskQuery()
    {
        List<TaskViewModel> taskViewModelList = new List<TaskViewModel>();

        if (connection.State == ConnectionState.Open)
        {
            string sqlQuery = "Sprouc_SearchTask";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                TaskViewModel taskViewModel = new TaskViewModel();

                taskViewModel.MemId = Convert.ToInt32(dr["memId"]);
                taskViewModel.MentorId = Convert.ToInt32(dr["mentorId"]);
                taskViewModel.TaskId = Convert.ToInt32(dr["taskId"]);
                taskViewModel.TaskStr = dr["taskStr"].ToString();
                int isCompleteNum = Convert.ToInt32(dr["iscomplete"]);
                if (isCompleteNum == 1)
                {
                    taskViewModel.IsComplete = true;
                }

                taskViewModelList.Add(taskViewModel);
            }
        }

        else
        {
            connection.Close();
        }

        return taskViewModelList;
    }

    // TODO: update queries to look like this
    // queries ORGANIZATION table to return a list of organizations to display
    public static List<OrganizationViewModel> ListOrgsQuery()
    {
        EstablishConnection();

        var orgList = new List<OrganizationViewModel>();

        if (connection.State == ConnectionState.Open)
        {
            const string sqlQuery = "select * from ORGANIZATION;";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            var da = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            // row[0] is OrgId and row[1] is OrgName
            orgList.AddRange(from DataRow row in dt.Rows select new OrganizationViewModel((int)row[0], (string)row[1]));
        }
        
        connection.Close();
        return orgList;
    }

    public static User ConstructUserFromDatabase(int orgId, string username)
    {
        EstablishConnection();

        var memberData = new MemberViewModel();

        if (connection.State == ConnectionState.Open)
        {
            var sqlQuery = $"select * from MEMBER where OrgId = {orgId} and Username = '{username}'";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            var da = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            // TODO: make this more efficient than a loop, because we only expect one output in the table
            foreach (DataRow dr in dt.Rows)
            {
                memberData.MemID = (int)dr["memId"];
                memberData.OrgID = (int)dr["orgId"];
                memberData.UserName = dr["username"].ToString();
                memberData.FullName = dr["fullname"].ToString();
                memberData.Description = dr["description"].ToString();
                memberData.UserType = (byte)dr["usertype"];
                memberData.Mentor = dr["mentor"].ToString();
                memberData.isOrgAdmin = (bool)dr["orgadmin"];
            }
        }
        
        connection.Close();

        return new User(
            memberData.MemID,
            memberData.UserName,
            memberData.FullName,
            memberData.Description,
            memberData.Mentor , // needed to make this a non-recursive reference
            memberData.UserType,
            memberData.isOrgAdmin
            );
    }

    // TODO: get mentor's name given a username, and maybe a orgId (I can add that param later)
    public static string GetMentorNameFromUsername(string username)
    {
        return "Mentor Man";
    }

    public static List<GoalViewModel> GetGoalsFromMemberId(int memId)
    {
        var goalList = new List<GoalViewModel>();
        
        EstablishConnection();

        if (connection.State == ConnectionState.Open)
        {
            var sqlQuery = $"select * from GOAL where memId = {memId}";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            var da = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                goalList.Add(new GoalViewModel(
                    (int)dr["memId"],
                    (int)dr["goalId"],
                    dr["goalStr"].ToString(),
                    (bool)dr["iscomplete"]
                    ));
            }
        }
        
        connection.Close();

        return goalList;
    }

    public static List<TaskModel> GetTasksFromMemberId(int memId)
    {
        var taskList = new List<TaskModel>();
        
        EstablishConnection();
        
        if (connection.State == ConnectionState.Open)
        {
            // var sqlQuery = $"select * from TASK where memId = {memId}";
            var sqlQuery = $"" +
                           $"select t.memId, m.fullname as mentorName, m.username as mentorUsername, t.taskId, t.taskStr, t.iscomplete " +
                           $"from TASK t, MEMBER m " +
                           $"where t.mentorId = m.memId and t.memId = {memId};";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            var da = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                taskList.Add(new TaskModel(
                    (int)dr["memId"],
                    dr["mentorName"].ToString(),
                    dr["mentorUsername"].ToString(),
                    (int)dr["taskId"],
                    dr["taskStr"].ToString(),
                    (bool)dr["iscomplete"]
                ));
            }
        }
        
        connection.Close();

        return taskList;
    }
}