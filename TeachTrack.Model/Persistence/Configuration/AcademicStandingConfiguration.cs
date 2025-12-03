using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachTrack.Core.Entities;
using TeachTrack.Model.Data.Models;

namespace TeachTrack.Model.Persistence.Configuration;

public class AcademicStandingConfiguration : IEntityTypeConfiguration<AcademicStanding> {
    public void Configure(EntityTypeBuilder<AcademicStanding> builder) {
        builder.ToTable("academic_standing");
        
        builder.HasKey(e => e.Id).HasName("academic_standing_pkey");
        
        builder.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("academic_standing_id");
        
        builder.Property(e => e.StandingName)
            .HasColumnName("standing_name")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(e => e.Description)
            .HasColumnName("description");

        builder.HasIndex(e => e.StandingName)
            .HasDatabaseName("academic_standing_standing_name_key")
            .IsUnique();
    }
}