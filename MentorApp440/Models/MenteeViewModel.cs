using System;
namespace MentorApp440.Models
{

    // This object is for the MENTEE Table in the database.
    // This attriubuets can be called anywhere in the codebase.

    public class MenteeViewModel
    {
        public int OrgID { get; set; }

        public int MenteeID { get; set; }

        public int MentorID { get; set; }
    }
}

