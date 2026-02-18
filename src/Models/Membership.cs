namespace Api.Models;

public class Membership
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }

    public IList<UserMembership> Users { get; set; } = new List<UserMembership>();
}