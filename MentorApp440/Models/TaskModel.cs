namespace MentorApp440.Models;

// created this class to use with custom query to get mentor name instead of just int ID

public class TaskModel
{
    public int MemId { get; set; }
    public string? MentorName { get; set; }
    public string? MentorUsername { get; set; }
    public int TaskId { get; set; }
    public string? TaskStr { get; set; }
    public bool IsComplete { get; set; }

    public TaskModel(int memId, string? mentorName, string? mentorUsername, int taskId, string? taskStr, bool isComplete)
    {
        MemId = memId;
        MentorName = mentorName;
        MentorUsername = mentorUsername;
        TaskId = taskId;
        TaskStr = taskStr;
        IsComplete = isComplete;
    }
}