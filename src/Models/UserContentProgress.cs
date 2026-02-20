namespace Api.Models;

public class UserContentProgress
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null;

    public int ContentId { get; set; }
    public Content Content { get; set; } = null;

    public DateTime? CompletedAt { get; set; }
}