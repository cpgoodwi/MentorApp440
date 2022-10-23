namespace MentorApp440.Models;

public class User
{
    public string UserId { get; }
    public string Name { get; set; }
    public string Image { get; set; } // string for image url to display their image
    public string Desc { get; set; }
    private User Mentor { get; set; }
    private UserType Type { get; set; }
    private List<Goal> Goals { get; set; }
    private List<Task> Tasks { get; set; }

    private enum UserType
    {
        Admin, // 0
        Newbie, // 1
        Peer // 2
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
    
    public User(string userId)
    {
        // TODO: create an object of User pulling info from database given username as ID
        UserId = userId;
        
        // hardcoded values for testing
        if (userId.Equals("charley"))
        {
            Name = "Charley Goodwin";
            Desc = "I am Charley";
            Mentor = new User("mrk");
            Type = UserType.Newbie;
        } else if (userId.Equals("mrk"))
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
    }

    public bool IsNew()
    {
        if (Type != UserType.Admin)
            return Type == UserType.Newbie;
        return false;
    }

    public List<string> ListGoals()
    {
        List<string> goalList = new List<string>();
        
        foreach (var goal in Goals)
        {
            if (!goal.Completed)
                goalList.Add(goal.GoalStr);
        }

        return goalList;
    }
    
    public List<string> ListTasks()
    {
        List<string> taskList = new List<string>();
        
        foreach (var task in Tasks)
        {
            if (!task.Completed)
                taskList.Add(task.TaskStr);
        }

        return taskList;
    }

    public string GetMentorId()
    {
        return Mentor.UserId;
    }

    public string GetMentorName()
    {
        return Mentor.Name;
    }

    public bool HasMentor()
    {
        return !(Mentor == null);
    }
}