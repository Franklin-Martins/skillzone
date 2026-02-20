namespace Api.Models;

public class MembershipContent
{
    public int Id { get; set; }

    public Membership Membership { get; set; } = null;
    public int MembershipId { get; set; }

    public Content Content { get; set; } = null;
    public int ContentId { get; set; }
}