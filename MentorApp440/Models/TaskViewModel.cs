using System;
namespace MentorApp440.Models
{

    // This object is for the TASK Table in the database.
    // This attriubuets can be called anywhere in the codebase.

    public class TaskViewModel
    {
        public int MemID { get; set; }

        public int MentorID { get; set; }

        public int TaskID { get; set; }
             
        public String? Task { get; set; }

        public bool isComplete { get; set; }
    }
}

