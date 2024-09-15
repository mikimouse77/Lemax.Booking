using BookingManagement.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingManagement.API.Infrastructure.EntityConfigurations
{
    public class HotelEntityTypeConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(h => h.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(h => h.Location)
                .IsRequired()
                .HasColumnType("geography");
        }
    }
}
