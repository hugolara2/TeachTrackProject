using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachTrack.Core.Entities;

namespace TeachTrack.Model.Persistence.Configurations;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule> {
    public void Configure(EntityTypeBuilder<Schedule> builder) {
        builder.ToTable("schedule");
        
        builder.HasKey(e => e.Id).HasName("schedule_pkey");
        
        builder.Property(e => e.Id)
            .HasColumnName("schedule_id")
            .HasDefaultValueSql("gen_random_uuid()");
        
        builder.Property(e => e.DayOfWeek)
            .HasColumnName("day_of_week")
            .IsRequired();
        
        builder.Property(e => e.StartTime)
            .HasColumnName("start_time")
            .HasColumnType("time")
            .IsRequired();
        
        builder.Property(e => e.EndTime)
            .HasColumnName("end_time")
            .HasColumnType("time")
            .IsRequired();
        
        builder.HasIndex(e => new { e.DayOfWeek, e.StartTime, e.EndTime })
            .IsUnique();
    }
}