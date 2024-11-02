using API_Coffee.Domain.Produto;
using Coffee_Break.Domain.Produto;
using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_Coffee.Infra.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Product> Product { get; set; }
    public DbSet<Category> Categories { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Ignore<Notification>();

        builder.Entity<Product>()
            .Property(p => p.name).IsRequired();
        builder.Entity<Category>()
            .Property(static c => c.name).IsRequired();
        builder.Entity<Product>()
            .Property(p => p.descricao).HasMaxLength(255);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
    {
        configuration.Properties<string>()
            .HaveMaxLength(100);
    }
}
