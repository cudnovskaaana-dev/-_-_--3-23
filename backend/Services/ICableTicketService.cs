using backend.Dtos;
using backend.Models;
namespace backend.Services;
public interface ICableTicketService
{
    Task<List<CableTicket>> GetAllAsync(int? siteId = null, int? assigneeUserId = null);
    Task<CableTicket?> GetAsync(int id);
    Task<CableTicket> CreateAsync(CreateCableTicketRequest request);
    Task<CableTicket?> AssignAsync(int id, int userId);
    Task<(CableTicket? Ticket, string? Error)> ChangeStatusAsync(int id, string status);
}
