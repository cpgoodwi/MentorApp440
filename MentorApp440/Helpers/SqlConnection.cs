using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using MentorApp440.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using MentorApp440.Session;

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
    private static string EstablishConnection()
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

    public static bool ValidUser(int orgId, string username)
    {
        var isValid = false;
        
        EstablishConnection();

        if (connection.State == ConnectionState.Open)
        {
            // TODO: make a more efficient query
            var sqlQuery = $"select count(*) as n from MEMBER where orgId = {orgId} and username = '{username}';";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            var da = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Cast<DataRow>().Any(dr => (long)dr["n"] > 0))
            {
                isValid = true;
            }
            
        }
        
        connection.Close();

        return isValid;
    }

    public static int? UserMemId(int orgId, string username)
    {
        int? userId = null;
        
        EstablishConnection();

        if (connection.State == ConnectionState.Open)
        {
            // TODO: make a more efficient query
            var sqlQuery = $"select memId from MEMBER where orgId = {orgId} and username = '{username}';";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            var da = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                userId = (int)dr["memId"];
            }
            
        }
        
        connection.Close();

        return userId;
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
                memberData.isOrgAdmin = (bool)dr["orgadmin"];
            }
        }
        
        connection.Close();

        return new User(
            memberData.MemID,
            memberData.UserName,
            memberData.FullName,
            memberData.Description,
            memberData.UserType,
            memberData.isOrgAdmin
            );
    }

    // TODO: get mentor's name given a memId, and maybe a orgId (I can add that param later)
    public static MentorViewModel GetMentor(int memId)
    {
        var mentorMan = new MentorViewModel();

        EstablishConnection();
        
        if (connection.State == ConnectionState.Open)
        {
            const string sqlQuery = "Sprouc_getMentor";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@memId_param", memId);
            
            var da = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            
            foreach (DataRow dr in dt.Rows)
            {
                mentorMan.Username = dr["username"].ToString();
                mentorMan.Fullname = dr["fullname"].ToString();
            }
        }
        
        connection.Close();
        
        return mentorMan;
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

    public static bool LoginAdmin(int orgId, string username)
    {
        EstablishConnection();

        var isAdmin = false;
        
        if (connection.State == ConnectionState.Open)
        {
            var sqlQuery = $"select username from MEMBER where orgId = {orgId} and orgadmin;";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            var da = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            
            // damn rider is cool 😎
            if (dt.Rows.Cast<DataRow>().Any(dr => username.Equals(dr["username"].ToString())))
            {
                isAdmin = true;
            }
        }
        
        connection.Close();

        return isAdmin;
    }
    
    public static string InsertGoal(int memId, string goalStr)
    {
        EstablishConnection();

        string entryresult = "";
        int queryResult;


        if (connection.State == ConnectionState.Open)
        {
            string sqlQuery = "Sprouc_InsertGoal";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@memId", memId);
            cmd.Parameters.AddWithValue("@goalStr", goalStr);

            queryResult = cmd.ExecuteNonQuery();

            if (queryResult == 1)
            {
                entryresult = "Food has been added!";
            }
            else
            {
                entryresult = "Food entry has Failed. Please try again!";
            }
        }
        
        //connection.Close();

        return entryresult;
    }

    // given a memberId, it returns a pair of lists,
    // the first list is list of peers, and the second is everyone who's not a peer
    // don't even need this monstrosity
    public static IEnumerable<List<PeerViewModel>> GetPeersFromMemberId(int memId, int orgId)
    {
        var peerList = new List<PeerViewModel>();
        var newPeers = new List<PeerViewModel>();

        EstablishConnection();

        if (connection.State == ConnectionState.Open)
        {
            // queries database for list of added peers
            const string peerQuery = "Sprouc_GetPeersByMemId";
            using var peerCmd = new MySqlCommand(peerQuery, connection);
            peerCmd.CommandType = CommandType.StoredProcedure;

            peerCmd.Parameters.AddWithValue("@memId_param", memId);
            peerCmd.Parameters.AddWithValue("@orgId_param", orgId);

            var da = new MySqlDataAdapter(peerCmd);
            var dt = new DataTable();
            da.Fill(dt);

            // can be made more efficient, but like this for readability, for now...
            foreach (DataRow dr in dt.Rows)
            {
                peerList.Add(new PeerViewModel(
                    (int)dr["orgId"],
                    (int)dr["memId"],
                    dr["username"].ToString(),
                    dr["fullname"].ToString(),
                    (int)dr["usertype"],
                    dr["description"].ToString()
                ));
            }
            
            // queries database for list of potential peers
            const string newPeerQuery = "Sprouc_GetNewPeers";
            using var newPeerCmd = new MySqlCommand(newPeerQuery, connection);
            newPeerCmd.CommandType = CommandType.StoredProcedure;

            newPeerCmd.Parameters.AddWithValue("@memId_param", memId);
            newPeerCmd.Parameters.AddWithValue("@orgId_param", orgId);

            da = new MySqlDataAdapter(peerCmd);
            dt = new DataTable();
            da.Fill(dt);

            // can be made more efficient, but like this for readability, for now...
            foreach (DataRow dr in dt.Rows)
            {
                newPeers.Add(new PeerViewModel(
                    (int)dr["orgId"],
                    (int)dr["memId"],
                    dr["username"].ToString(),
                    dr["fullname"].ToString(),
                    (int)dr["usertype"],
                    dr["description"].ToString()
                ));
            }
        }
        
        connection.Close();
        
        return new List<List<PeerViewModel>> { peerList, newPeers };
    }

    // given memId and orgId, it will return a list of the person's peers
    public static List<PeerViewModel> GetCurPeers(int memId, int orgId)
    {
        var peerList = new List<PeerViewModel>();
        
        EstablishConnection();
        
        if (connection.State == ConnectionState.Open)
        {
            // queries database for list of added peers
            const string peerQuery = "Sprouc_GetPeersByMemId";
            using var peerCmd = new MySqlCommand(peerQuery, connection);
            peerCmd.CommandType = CommandType.StoredProcedure;

            peerCmd.Parameters.AddWithValue("@memId_param", memId);
            peerCmd.Parameters.AddWithValue("@orgId_param", orgId);

            var da = new MySqlDataAdapter(peerCmd);
            var dt = new DataTable();
            da.Fill(dt);

            // can be made more efficient, but like this for readability, for now...
            foreach (DataRow dr in dt.Rows)
            {
                peerList.Add(new PeerViewModel(
                    (int)dr["orgId"],
                    (int)dr["memId"],
                    dr["username"].ToString(),
                    dr["fullname"].ToString(),
                    (byte)dr["usertype"],
                    dr["description"].ToString()
                ));
            }
        }
        
        connection.Close();

        return peerList;
    }
    
    // given orgId and memId, it will return a list of all not currently peers
    public static List<PeerViewModel> GetPotPeers(int memId, int orgId)
    {
        var peerList = new List<PeerViewModel>();
        
        EstablishConnection();
        
        if (connection.State == ConnectionState.Open)
        {
            // queries database for list of added peers
            const string peerQuery = "Sprouc_GetNewPeers";
            using var peerCmd = new MySqlCommand(peerQuery, connection);
            peerCmd.CommandType = CommandType.StoredProcedure;

            peerCmd.Parameters.AddWithValue("@memId_param", memId);
            peerCmd.Parameters.AddWithValue("@orgId_param", orgId);

            var da = new MySqlDataAdapter(peerCmd);
            var dt = new DataTable();
            da.Fill(dt);

            // this is the same as before, but Rider converted it :)
            peerList.AddRange(from DataRow dr in dt.Rows select new PeerViewModel((int)dr["orgId"], (int)dr["memId"], dr["username"].ToString(), dr["fullname"].ToString(), (byte)dr["usertype"], dr["description"].ToString()));
        }
        
        connection.Close();

        return peerList;
    }
    
    // load an individual goal view model
    public static GoalViewModel GetGoal(int memId, int goalId)
    {
        GoalViewModel goal = null;

        EstablishConnection();
        
        if (connection.State == ConnectionState.Open)
        {
            const string sqlQuery = $"Sprouc_getGoal";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@memId_param", memId);
            cmd.Parameters.AddWithValue("@goalId_param", goalId);
            
            var da = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            
            foreach (DataRow dr in dt.Rows)
            {
                goal = new GoalViewModel(
                    (int)dr["memId"],
                    (int)dr["goalId"],
                    dr["goalStr"].ToString(),
                    (bool)dr["iscomplete"]
                );
            }
        }
        
        connection.Close();
        
        return goal;
    }
    
    // load an individual task view model
    public static TaskModel GetTask(int memId, int taskId)
    {
        TaskModel task = null;

        EstablishConnection();
        
        if (connection.State == ConnectionState.Open)
        {
            const string sqlQuery = $"Sprouc_getTask";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@memId_param", memId);
            cmd.Parameters.AddWithValue("@taskId_param", taskId);
            
            var da = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            
            foreach (DataRow dr in dt.Rows)
            {
                task = new TaskModel(
                    (int)dr["memId"],
                    dr["mentorName"].ToString(),
                    dr["mentorUsername"].ToString(),
                    (int)dr["taskId"],
                    dr["taskStr"].ToString(),
                    (bool)dr["iscomplete"]
                );
            }
        }
        
        connection.Close();
        
        return task;
    }
    
    // post a comment to a goal
    public static string GoalComment(int fromId, int memId, int goalId, string comm)
    {
        var entryresult = "";
        
        EstablishConnection();

        if (connection.State == ConnectionState.Open)
        {
            const string sqlQuery = "Sprouc_CommentGoal";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@fromId_param", fromId);
            cmd.Parameters.AddWithValue("@memId_param", memId);
            cmd.Parameters.AddWithValue("@goalId_param", goalId);
            cmd.Parameters.AddWithValue("@comm_param", comm);

            var queryresult = cmd.ExecuteNonQuery();

            entryresult = queryresult == 1 ? "posted comment" : "comment failed";
        }

        return entryresult;
    }
    
    // post a comment to a task
    public static string TaskComment(int fromId, int memId, int taskId, string comm)
    {
        var entryresult = "";
        
        EstablishConnection();

        if (connection.State == ConnectionState.Open)
        {
            const string sqlQuery = "Sprouc_CommentTask";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@fromId_param", fromId);
            cmd.Parameters.AddWithValue("@memId_param", memId);
            cmd.Parameters.AddWithValue("@taskId_param", taskId);
            cmd.Parameters.AddWithValue("@comm_param", comm);

            var queryresult = cmd.ExecuteNonQuery();

            entryresult = queryresult == 1 ? "posted comment" : "comment failed";
        }

        return entryresult;
    }

    public static List<CommentViewModel> LoadGoalComments(int memId, int goalId)
    {
        var goalComList = new List<CommentViewModel>();
        
        EstablishConnection();

        if (connection.State == ConnectionState.Open)
        {
            var sqlQuery = $"Sprouc_GetGoalComs";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@memId_p", memId);
            cmd.Parameters.AddWithValue("@goalId_p", goalId);
            
            var da = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                goalComList.Add(new CommentViewModel(
                    dr["fullname"].ToString(),
                    dr["username"].ToString(),
                    dr["comm"].ToString()
                ));
            }
        }
        
        connection.Close();

        return goalComList;
    }
    
    public static List<CommentViewModel> LoadTaskComments(int memId, int taskId)
    {
        var taskComments = new List<CommentViewModel>();
        
        EstablishConnection();

        if (connection.State == ConnectionState.Open)
        {
            var sqlQuery = $"Sprouc_GetTaskComs";
            using var cmd = new MySqlCommand(sqlQuery, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@memId_p", memId);
            cmd.Parameters.AddWithValue("@taskId_p", taskId);
            
            var da = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                taskComments.Add(new CommentViewModel(
                    dr["fullname"].ToString(),
                    dr["username"].ToString(),
                    dr["comm"].ToString()
                ));
            }
        }
        
        connection.Close();

        return taskComments;
    }
}