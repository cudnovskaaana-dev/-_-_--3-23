using backend.Dtos;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
namespace backend.Controllers;
[ApiController, Route("api/cable-tickets")]
public sealed class CableTicketsController(ICableTicketService service) : ControllerBase
{
    [HttpGet] public async Task<IActionResult> GetAll([FromQuery] int? siteId, [FromQuery] int? assigneeUserId) => Ok(await service.GetAllAsync(siteId, assigneeUserId));
    [HttpGet("{id:int}")] public async Task<IActionResult> Get(int id) => await service.GetAsync(id) is { } item ? Ok(item) : NotFound(new { error = $"Заявка {id} не найдена." });
    [HttpPost] public async Task<IActionResult> Create(CreateCableTicketRequest request)
    {
        try { var item = await service.CreateAsync(request); return CreatedAtAction(nameof(Get), new { id = item.Id }, item); }
        catch (ArgumentException ex) { return BadRequest(new { error = ex.Message }); }
    }
    [HttpPatch("{id:int}/assignee")] public async Task<IActionResult> Assign(int id, AssignCableTicketRequest request)
    {
        try { var item = await service.AssignAsync(id, request.AssigneeUserId); return item is null ? NotFound(new { error = "Заявка не найдена." }) : Ok(item); }
        catch (ArgumentException ex) { return BadRequest(new { error = ex.Message }); }
    }
    [HttpPatch("{id:int}/status")] public async Task<IActionResult> ChangeStatus(int id, ChangeCableTicketStatusRequest request)
    {
        var (item, error) = await service.ChangeStatusAsync(id, request.Status);
        if (item is null) return NotFound(new { error = "Заявка не найдена." });
        if (error is not null) return error.StartsWith("Недопустимый") ? BadRequest(new { error }) : Conflict(new { error });
        return Ok(item);
    }
}
