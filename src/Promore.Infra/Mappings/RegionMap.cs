using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Promore.Core.Contexts.Region.Entity;

namespace Promore.Infra.Mappings;

public class RegionMap : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        //// Table
        builder.ToTable("Region");

        
        //// Primary Key - Identity
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();
        
        
        //// Properties
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);
        
        builder.Property(x => x.EstablishedDate)
            .HasColumnName("EstablishedDate")
            .HasColumnType("DATE");
        
        builder.Property(x => x.StartDate)
            .HasColumnName("StartDate")
            .HasColumnType("DATE");
        
        builder.Property(x => x.EndDate)
            .HasColumnName("EndDate")
            .HasColumnType("DATE");
        
    }
    
}