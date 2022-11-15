using System;
namespace MentorApp440.Models
{

    // This object is for the TASK Table in the database.
    // these attributes can be called anywhere in the codebase.

    public class TaskViewModel
    {
        public int MemId { get; set; }

        public int MentorId { get; set; }

        public int TaskId { get; set; }
             
        public string? TaskStr { get; set; }

        public bool IsComplete { get; set; }

        public TaskViewModel(int memId, int mentorId, int taskId, string? taskStr, bool isComplete)
        {
            MemId = memId;
            MentorId = mentorId;
            TaskId = taskId;
            TaskStr = taskStr;
            IsComplete = isComplete;
        }

        public TaskViewModel()
        {
            
        }
    }
}

