using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachTrack.Core.Entities;

namespace TeachTrack.Model.Persistence.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room> {
    public void Configure(EntityTypeBuilder<Room> builder) {
        builder.ToTable("room");
        
        builder.HasKey(e => e.Id).HasName("room_pkey");
        
        builder.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("room_id");
        
        builder.Property(r => r.RoomNumber)
            .HasColumnName("room_number")
            .HasMaxLength(20) // Matches VARCHAR(20)
            .IsRequired();

        builder.Property(r => r.Building)
            .HasColumnName("building")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(e => e.Capacity).HasColumnName("capacity");
        
        builder.HasIndex(r => r.RoomNumber)
            .HasDatabaseName("room_room_number_key")
            .IsUnique();
    }
}