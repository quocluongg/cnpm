using EventApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApp.DataAccess.Data
{
    public class EventAppDbContext : DbContext
    {
        public EventAppDbContext(DbContextOptions<EventAppDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string))
                .ToList()
                .ForEach(p => p.SetIsUnicode(false));
            
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    TenDanhMuc = "Music",
                },
                new Category
                {
                    Id = 2,
                    TenDanhMuc = "Sports",
                },
                new Category
                {
                    Id = 3,
                    TenDanhMuc = "Arts",
                }
            );
            
            modelBuilder.Entity<EventCategory>()
                .HasKey(ec => new { ec.EventId, ec.CategoryId });
            
            modelBuilder.Entity<PromotionOrder>()
                .HasKey(ec => new { ec.PromotionId, ec.OrderId });
            
            modelBuilder.Entity<ExtrasOrder>()
                .HasKey(ec => new { ec.ExtrasId, ec.OrderId });
        }
    }
}
