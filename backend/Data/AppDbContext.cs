using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<CableTicket> CableTickets => Set<CableTicket>();
    public DbSet<NetworkSite> NetworkSites => Set<NetworkSite>();
    public DbSet<AppUser> AppUsers => Set<AppUser>();

    protected override void OnModelCreating(ModelBuilder model)
    {
        var ticket = model.Entity<CableTicket>();
        ticket.ToTable("cable_tickets", table =>
        {
            table.HasCheckConstraint("cable_tickets_status_check", "status IN ('New', 'InProgress', 'Closed', 'Cancelled')");
            table.HasCheckConstraint("cable_tickets_title_length_check", "char_length(title) BETWEEN 5 AND 80");
            table.HasCheckConstraint("cable_tickets_description_length_check", "char_length(description) BETWEEN 10 AND 500");
        });
        ticket.HasIndex(x => x.Status);
        ticket.HasIndex(x => x.SiteId);
        ticket.HasIndex(x => x.Number).IsUnique();
        ticket.HasIndex(x => x.AssigneeUserId);
        ticket.Property(x => x.Id).HasColumnName("id");
        ticket.Property(x => x.Number).HasColumnName("number").HasMaxLength(20);
        ticket.Property(x => x.Title).HasColumnName("title").HasMaxLength(80);
        ticket.Property(x => x.Description).HasColumnName("description").HasMaxLength(500);
        ticket.Property(x => x.Status).HasColumnName("status").HasMaxLength(20);
        ticket.Property(x => x.SiteId).HasColumnName("site_id");
        ticket.Property(x => x.CreatedByUserId).HasColumnName("created_by_user_id");
        ticket.Property(x => x.AssigneeUserId).HasColumnName("assignee_user_id");
        ticket.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()");
        ticket.HasOne<NetworkSite>().WithMany().HasForeignKey(x => x.SiteId);
        ticket.HasOne<AppUser>().WithMany().HasForeignKey(x => x.CreatedByUserId);
        ticket.HasOne<AppUser>().WithMany().HasForeignKey(x => x.AssigneeUserId);

        var site = model.Entity<NetworkSite>();
        site.ToTable("network_sites");
        site.Property(x => x.Id).HasColumnName("id");
        site.Property(x => x.Name).HasColumnName("name");
        site.HasIndex(x => x.Name).IsUnique();

        var user = model.Entity<AppUser>();
        user.ToTable("app_users", table => table.HasCheckConstraint("app_users_role_check", "role IN ('Dispatcher', 'Electrician')"));
        user.Property(x => x.Id).HasColumnName("id");
        user.Property(x => x.Login).HasColumnName("login");
        user.Property(x => x.Role).HasColumnName("role");
        user.HasIndex(x => x.Login).IsUnique();
    }
}
