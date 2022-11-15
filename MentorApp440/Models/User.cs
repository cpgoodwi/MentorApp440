namespace MentorApp440.Models;

public class User
{

    public int MemberId { get; }
    public string Username { get; }
    public string Name { get; set; }
    public string Image { get; set; } // string for image url to display their image
    public string Desc { get; set; }
    private MentorStruct Mentor { get; set; }
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

    private class MentorStruct // should this be class or struct? why doesn't null work if its a struct?
    {
        public readonly string Username; // what is readonly??
        public readonly string Name;

        public MentorStruct(string username)
        {
            Username = username;
            Name = Helpers.SqlConnection.GetMentorNameFromUsername(username);
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

    public User(int memberId, string username, string name, string desc, string mentorUsername, int type, bool isAdmin)
    {
        MemberId = memberId;
        Username = username;
        Name = name;
        // Image = image; // excluding image from scope of sprints
        Desc = desc;
        Mentor = new MentorStruct(mentorUsername);
        Type = (UserType)type;
        IsAdmin = isAdmin;
    }

    public User(int orgId, string username)
    {
        // TODO: create an object of User pulling info from database given username as ID
        Username = username;
        
        // hardcoded values for testing
        if (username.Equals("charley"))
        {
            Name = "Charley Goodwin";
            Desc = "I am Charley";
            Mentor = new MentorStruct("mrk");
            Type = UserType.Newbie;
        } else if (username.Equals("mrk"))
        {
            Name = "Mr. K";
            Desc = "I am Charley's mentor";
            Type = UserType.Peer;
        }

        Goals = new List<Goal>
        {
            new Goal(false, "I did this one"),
            new Goal(false, "get my code to work"),
            new Goal(true, "this shouldn't be here"),
            new Goal(false, "this is goal number 4")
        };
        
        Tasks = new List<Task>
        {
            new Task(false, "This is the first task"),
            new Task(false, "This is the second task"),
            new Task(true, "This is the third task"),
            new Task(false, "This is the fourth task")
        };

        IsAdmin = false;
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
        return Mentor.Name;
    }

    public bool HasMentor()
    {
        return !(Mentor is null);
    }
    
    public bool IsMentee(string userId)
    {
        return HasMentor() && userId.Equals(GetMentorId);
    }
}