using System;
namespace MentorApp440.Models
{

    // This object is for the GOAL Table in the database.
    // these attributes can be called anywhere in the codebase.

    public class GoalViewModel
    {
        public int MemID { get; set; }

        public int GoalID { get; set; }

        public string GoalStr { get; set; }

        public bool isComplete { get; set; }

        public GoalViewModel(int memId, int goalId, string goalStr, bool isComplete)
        {
            MemID = memId;
            GoalID = goalId;
            GoalStr = goalStr;
            this.isComplete = isComplete;
        }
    
        public GoalViewModel() {
    
        }
    }

}

