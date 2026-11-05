using backend.Dtos;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
namespace backend.Services;
public sealed class CableTicketService(AppDbContext db) : ICableTicketService
{
    public Task<List<CableTicket>> GetAllAsync(int? siteId = null, int? assigneeUserId = null)
    {
        var query = db.CableTickets.AsNoTracking().AsQueryable();
        if (siteId is not null) query = query.Where(x => x.SiteId == siteId.Value);
        if (assigneeUserId is not null) query = query.Where(x => x.AssigneeUserId == assigneeUserId.Value);
        return query.OrderByDescending(x => x.Id).ToListAsync();
    }
    public Task<CableTicket?> GetAsync(int id) => db.CableTickets.FindAsync(id).AsTask();
    public async Task<CableTicket> CreateAsync(CreateCableTicketRequest request)
    {
        if (!await db.NetworkSites.AnyAsync(x => x.Id == request.SiteId)) throw new ArgumentException("Участок не найден.");
        var item = new CableTicket { Number = "", Title = request.Title.Trim(), SiteId = request.SiteId, Description = request.Description.Trim() };
        db.CableTickets.Add(item); await db.SaveChangesAsync();
        item.Number = $"КБ-{100 + item.Id}"; await db.SaveChangesAsync(); return item;
    }
    public async Task<CableTicket?> AssignAsync(int id, int userId)
    {
        var item = await db.CableTickets.FindAsync(id); if (item is null) return null;
        if (!await db.AppUsers.AnyAsync(x => x.Id == userId && x.Role == "Electrician")) throw new ArgumentException("Электромонтёр не найден.");
        item.AssigneeUserId = userId; await db.SaveChangesAsync(); return item;
    }
    public async Task<(CableTicket? Ticket, string? Error)> ChangeStatusAsync(int id, string status)
    {
        var item = await db.CableTickets.FindAsync(id); if (item is null) return (null, null);
        if (status is not ("New" or "InProgress" or "Closed" or "Cancelled")) return (item, "Недопустимый статус.");
        if (status == "InProgress" && item.AssigneeUserId is null) return (item, "Нельзя начать работу без назначенного электромонтёра.");
        if (status == "Closed" && item.Status != "InProgress") return (item, "Закрыть можно только заявку в работе.");
        item.Status = status; await db.SaveChangesAsync(); return (item, null);
    }
}
