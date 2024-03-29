using IWantApp.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace IWantApp.Infra.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Ignore<Notification>();

            builder.Entity<Product>()
                .Property(p=> p.Name).IsRequired();
            builder.Entity<Product>()
                .Property(p=> p.Description).HasMaxLength(255);
            builder.Entity<Product>()
                .Property(p => p.Price).HasColumnType("decimal(10,2)").IsRequired();

            builder.Entity<Category>()
                .Property(c=> c.Name).IsRequired();
            
            builder.Entity<Order>()
                .Property(o => o.CustomerId).IsRequired();
            builder.Entity<Order>()
                .Property(o => o.DeliveryAddress).IsRequired();
            builder.Entity<Order>()
                .HasMany(o => o.Products)
                .WithMany(p => p.Orders)
                .UsingEntity(x => x.ToTable("OrderProdutcs"));

             
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configuretion)
        {
            configuretion.Properties<string>()
                .HaveMaxLength(100);
        }

        

    }
}