using System;
namespace MentorApp440.Models
{

    // This object is for the GOAL Table in the database.
    // This attriubuets can be called anywhere in the codebase.

    public class GoalViewModel
    {
        public int MemID { get; set; }

        public int GoalID { get; set; }

        public String? Goal { get; set; }

        public bool isComplete { get; set; }
    }

}

