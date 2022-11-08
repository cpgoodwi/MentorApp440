using System;
namespace MentorApp440.Models
{

    // This object is for the ORGANIZATION Table in the database.
    // This attributes can be called anywhere in the codebase.

    public class OrganizationViewModel
    {
        public int OrgID { get; set; }

        public string? OrgName { get; set; }

        public OrganizationViewModel(int orgId, string orgName)
        {
            OrgID = orgId;
            OrgName = orgName;
        }

    }
}

