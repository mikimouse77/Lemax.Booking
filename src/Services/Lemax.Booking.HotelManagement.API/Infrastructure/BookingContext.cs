using BookingManagement.API.Infrastructure.EntityConfigurations;
using BookingManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagement.API.Infrastructure
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HotelEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
