using MentorApp440.Helpers;

namespace MentorApp440.Models;

public class User
{

    public int MemberId { get; }
    public string Username { get; }
    public string Name { get; set; }
    public string Image { get; set; } // string for image url to display their image
    public string Desc { get; set; }
    private MentorViewModel Mentor { get; set; }
    private UserType Type { get; set; }
    private List<Goal> Goals { get; set; }
    private List<Task> Tasks { get; set; }
    public bool IsAdmin { get; set; }


    private enum UserType
    {
        // Admin,
        Newbie = 1,
        Peer = 2,
        Mentor = 3
    }

    private class Goal
    {
        public bool Completed { get; set; }
        public string GoalStr { get; set; }

        public Goal(bool completed, string goalStr)
        {
            Completed = completed;
            GoalStr = goalStr;
        }
    }

    private class Task
    {
        public bool Completed { get; set; }
        public string TaskStr { get; set; }
        
        public Task(bool completed, string taskStr)
        {
            Completed = completed;
            TaskStr = taskStr;
        }
    }

    public User(int memberId, string username, string name, string desc, int type, bool isAdmin)
    {
        MemberId = memberId;
        Username = username;
        Name = name;
        // Image = image; // excluding image from scope of sprints
        Desc = desc;
        Mentor = SqlConnection.GetMentor(MemberId);
        Type = (UserType)type;
        IsAdmin = isAdmin;
    }

    public User()
    {
    }

    public bool IsNew()
    {
        if (!IsAdmin)
            return Type == UserType.Newbie;
        return false;
    }

    public List<string> ListGoals()
    {
        return (from goal in Goals where !goal.Completed select goal.GoalStr).ToList();
    }
    
    public List<string> ListTasks()
    {
        return (from task in Tasks where !task.Completed select task.TaskStr).ToList();
    }

    public string GetMentorId()
    {
        return Mentor.Username;
    }

    public string GetMentorName()
    {
        return Mentor.Fullname;
    }

    public bool HasMentor()
    {
        return !(string.IsNullOrWhiteSpace(Mentor.Username));
    }
    
    public bool IsMentee(string userId)
    {
        return HasMentor() && userId.Equals(GetMentorId);
    }
}