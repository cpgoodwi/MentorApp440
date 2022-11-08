using System;
namespace MentorApp440.Models
{

    // This object is for the MEMBER Table in the database.
    // This attributes can be called anywhere in the codebase.

    public class MemberViewModel
    {
        public int MemID { get; set; }

        public int OrgID { get; set; }

        public string? UserName { get; set; }

        public string? FullName { get; set; }

        public string? Description { get; set; }

        public int UserType { get; set; }

        public string? Mentor { get; set; }

        public bool isOrgAdmin { get; set; }

    }

}

