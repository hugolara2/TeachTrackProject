using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachTrack.Core.Entities;

namespace TeachTrack.Infrastructure.Configurations;

public class DegreeTypeConfiguration : IEntityTypeConfiguration<DegreeType> {
    public void Configure(EntityTypeBuilder<DegreeType> builder) {
        builder.ToTable("degree_type");

        builder.HasKey(t => t.Id).HasName("degree_type_pkey");

        builder.Property(t => t.Id)
            .HasColumnName("type_id")
            .HasDefaultValueSql("gen_random_uuid()");

        // Sequence for Code (Standard Integer)
        // Note: You didn't provide a sequence name for this in the SQL, 
        // but typically types have simple integer codes. 
        // If it's just a raw integer, remove the DefaultValueSql.
        builder.Property(t => t.Code)
            .HasColumnName("code")
            .IsRequired();

        builder.Property(t => t.Name)
            .HasColumnName("type_name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasColumnName("description");

        builder.HasIndex(t => t.Code)
            .HasDatabaseName("degree_type_code_key")
            .IsUnique();

        builder.HasIndex(t => t.Name)
            .HasDatabaseName("degree_type_type_name_key")
            .IsUnique();
    }
}