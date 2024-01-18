using examPattern.Models;
using Microsoft.EntityFrameworkCore;

public class InsuranceDbContext : DbContext
{
    public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<InsuranceProduct> InsuranceProducts { get; set; }
    public DbSet<UserInsuranceProduct> UserInsuranceProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserInsuranceProduct>()
            .HasKey(uip => new { uip.UserId, uip.InsuranceProductId });

        modelBuilder.Entity<UserInsuranceProduct>()
            .HasOne(uip => uip.User)
            .WithMany(u => u.UserInsuranceProducts)
            .HasForeignKey(uip => uip.UserId);

        modelBuilder.Entity<UserInsuranceProduct>()
            .HasOne(uip => uip.InsuranceProduct)
            .WithMany(ip => ip.UserInsuranceProducts)
            .HasForeignKey(uip => uip.InsuranceProductId);


        base.OnModelCreating(modelBuilder);
    }
}
