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
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Extras> Extras { get; set; }
        
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<ExtrasOrder> ExtrasOrders { get; set; }
        public DbSet<PromotionOrder> PromotionOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite keys
            modelBuilder.Entity<EventCategory>()
                .HasKey(ec => new { ec.EventId, ec.CategoryId });
    
            modelBuilder.Entity<PromotionOrder>()
                .HasKey(po => new { po.PromotionId, po.OrderId });
        
            modelBuilder.Entity<ExtrasOrder>()
                .HasKey(eo => new { eo.ExtrasId, eo.OrderId });

            // Configure 1:1 relationships
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Ticket-Seat relationship
            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Ticket)
                .WithOne(t => t.Seat)
                .HasForeignKey<Seat>(s => s.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
    
            // Decimal precision configuration
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");
    
            modelBuilder.Entity<TicketType>()
                .Property(t => t.BasePrice)
                .HasColumnType("decimal(18,2)");
    
            // Add indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        
            modelBuilder.Entity<Promotion>()
                .HasIndex(p => p.PromotionCode)
                .IsUnique();
        }
    }
}
