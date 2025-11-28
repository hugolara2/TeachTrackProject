using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachTrack.Core.Entities;

namespace TeachTrack.Model.Persistence.Configurations;

public class SemesterConfiguration : IEntityTypeConfiguration<Semester> {
    public void Configure(EntityTypeBuilder<Semester> builder) {
        builder.ToTable("semester");
        builder.HasKey(e => e.Id).HasName("semester_pkey");
        builder.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("semester_id");
        builder.HasIndex(e => e.Name, "semester_name_key").IsUnique();
        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .HasColumnName("name");
        builder.Property(e => e.StartDate).HasColumnName("start_date");
        builder.Property(e => e.EndDate).HasColumnName("end_date");
    }
}