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
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Music",
                    SortingOrder = 1
                },
                new Category
                {
                    Id = 2,
                    Name = "Sports",
                    SortingOrder = 2
                },
                new Category
                {
                    Id = 3,
                    Name = "Arts",
                    SortingOrder = 3
                }
            );
        }
    }
}
