using System;
namespace MentorApp440.Models
{

    // This object is for the GOAL Table in the database.
    // these attributes can be called anywhere in the codebase.

    public class GoalViewModel
    {
        public int MemId { get; set; }

        public int GoalId { get; set; }

        public string? GoalStr { get; set; }

        public bool IsComplete { get; set; }

        public GoalViewModel(int memId, int goalId, string goalStr, bool isComplete)
        {
            MemId = memId;
            GoalId = goalId;
            GoalStr = goalStr;
            IsComplete = isComplete;
        }
    
        public GoalViewModel() {
    
        }
    }

}

