namespace MentorApp440.Models;

public class PeerViewModel
{
    public int OrgId { get; set; }
    public int MemId { get; set; }
    public string Username { get; set; }
    public string Fullname { get; set; }
    // public string ImageUrl { get; set; } // url for image loading not yet implemented
    public int Type { get; set; }
    public string Description { get; set; }

    public PeerViewModel(int orgId, int memId, string username, string fullname, int type, string description)
    {
        OrgId = orgId;
        MemId = memId;
        Username = username;
        Fullname = fullname;
        Type = type;
        Description = description;
    }
}