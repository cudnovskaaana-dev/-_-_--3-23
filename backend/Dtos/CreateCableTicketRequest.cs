using System.ComponentModel.DataAnnotations;
namespace backend.Dtos;
public sealed class CreateCableTicketRequest
{
    [Required, StringLength(80, MinimumLength = 5)] public string Title { get; init; } = "";
    [Range(1, int.MaxValue)] public int SiteId { get; init; }
    [Required, StringLength(500, MinimumLength = 10)] public string Description { get; init; } = "";
}
public sealed class AssignCableTicketRequest
{
    [Range(1, int.MaxValue)] public int AssigneeUserId { get; init; }
}
public sealed class ChangeCableTicketStatusRequest
{
    [Required] public string Status { get; init; } = "";
}
