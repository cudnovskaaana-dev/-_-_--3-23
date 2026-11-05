namespace backend.Models;

public sealed class CableTicket
{
    public int Id { get; set; }
    public string Number { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Status { get; set; } = "New";
    public int SiteId { get; set; }
    public int CreatedByUserId { get; set; } = 1;
    public int? AssigneeUserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
