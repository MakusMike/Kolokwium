using Kolokwium.Models;
using Microsoft.EntityFrameworkCore;
namespace Kolokwium.AppContext;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions<AppContext> options)
        : base(options)
    {
    }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
        "Data Source=db-mssql;Initial Catalog=S25713;Integrated Security=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Client)
            .WithMany(c => c.Sales)
            .HasForeignKey(s => s.IdClient);

        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Subscription)
            .WithMany(sub => sub.Sales)
            .HasForeignKey(s => s.IdSubscription);

        modelBuilder.Entity<Discount>()
            .HasOne(d => d.Subscription)
            .WithMany(sub => sub.Discounts)
            .HasForeignKey(d => d.IdSubscription);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Client)
            .WithMany(c => c.Payments)
            .HasForeignKey(p => p.IdClient);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Subscription)
            .WithMany(sub => sub.Payments)
            .HasForeignKey(p => p.IdSubscription);
    }
}