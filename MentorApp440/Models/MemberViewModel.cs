using System;
namespace MentorApp440.Models
{

    // This object is for the MEMBER Table in the database.
    // This attriubuets can be called anywhere in the codebase.

    public class MemberViewModel
    {
        public int memID { get; set; }

        public int OrgID { get; set; }

        public String? UserName { get; set; }

        public String? FullName { get; set; }

        public String? Description { get; set; }

        public UserType UserType { get; set; }

        public String? Mentor { get; set; }

        public bool isOrgAdmin { get; set; }

    }

    public enum UserType
    {
        Newbie, // 0

        Peer, // 1

        Mentor // 2
    }

}

