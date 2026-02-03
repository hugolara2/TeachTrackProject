using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachTrack.Core.Entities;

namespace TeachTrack.Infrastructure.Configurations;

public class SemesterConfiguration : IEntityTypeConfiguration<Semester> {
    public void Configure(EntityTypeBuilder<Semester> builder) {
        builder.ToTable("semester");

        builder.HasKey(e => e.Id).HasName("semester_pkey");

        builder.Property(e => e.Id)
            .HasColumnName("semester_id")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();

        // Map C# DateTime to Postgres 'date' type (ignores time)
        builder.Property(e => e.StartDate)
            .HasColumnName("start_date")
            .HasColumnType("date") 
            .IsRequired();

        builder.Property(e => e.EndDate)
            .HasColumnName("end_date")
            .HasColumnType("date")
            .IsRequired();

        builder.HasIndex(e => e.Name)
            .HasDatabaseName("semester_name_key")
            .IsUnique();
    }
}