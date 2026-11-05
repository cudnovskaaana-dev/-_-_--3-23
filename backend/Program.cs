using backend.Data;
using backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connection = builder.Configuration.GetConnectionString("Default") ?? "Host=localhost;Port=5432;Database=ris_cable;Username=postgres;Password=CHANGE_ME";
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection));
builder.Services.AddScoped<ICableTicketService, CableTicketService>();
var app = builder.Build();
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.UseExceptionHandler(handler => handler.Run(async context =>
{
    var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
    var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("UnhandledErrors");
    logger.LogError(error, "Необработанная ошибка запроса {Method} {Path}", context.Request.Method, context.Request.Path);
    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    await context.Response.WriteAsJsonAsync(new { error = "Внутренняя ошибка сервера." });
}));
app.MapGet("/health", () => Results.Text("Healthy"));
app.MapGet("/info", (IHostEnvironment env) => Results.Ok(new { application = "Cable maintenance API", environment = env.EnvironmentName }));
app.MapControllers();
app.Run();
