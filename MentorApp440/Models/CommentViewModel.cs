namespace MentorApp440.Models;

public class CommentViewModel
{
    public CommentViewModel(string fromMemberName, string fromMemberUsername, string comment)
    {
        FromMemberName = fromMemberName;
        FromMemberUsername = fromMemberUsername;
        Comment = comment;
    }

    public string FromMemberName { get; set; }
    public string FromMemberUsername { get; set; }
    public string Comment { get; set; }
}