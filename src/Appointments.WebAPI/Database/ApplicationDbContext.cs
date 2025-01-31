using Appointments.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appointments.WebAPI.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<SalesManager> SalesManagers => Set<SalesManager>();
    public DbSet<Slot> Slots => Set<Slot>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}