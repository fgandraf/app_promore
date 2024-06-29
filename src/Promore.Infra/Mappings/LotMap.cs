using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Promore.Core.Models;

namespace Promore.Infra.Mappings;

public class LotMap : IEntityTypeConfiguration<Lot>
{
    public void Configure(EntityTypeBuilder<Lot> builder)
    {
        //// Table
        builder.ToTable("Lot");
        
        
        //// Primary Key
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasMaxLength(5);
        
        
        //// Properties
        builder.Property(x => x.Block)
            .IsRequired()
            .HasColumnName("Block")
            .HasColumnType("VARCHAR")
            .HasMaxLength(2);

        builder.Property(x => x.Number)
            .IsRequired()
            .HasColumnName("Number")
            .HasColumnType("INT");
        
        builder.Property(x => x.SurveyDate)
            .HasColumnName("SurveyDate")
            .HasColumnType("DATE");
        
        builder.Property(x => x.LastModifiedDate)
            .HasColumnName("LastModifiedDate")
            .HasColumnType("DATETIME2")
            .HasDefaultValue(DateTime.Now.ToUniversalTime());
        
        builder.Property(x => x.Status)
            .IsRequired()
            .HasColumnName("Status")
            .HasColumnType("INT");

        builder.Property(x => x.Comments)
            .IsRequired(false)
            .HasColumnName("Comments")
            .HasColumnType("TEXT");


        builder.Property(x => x.UserId)
            .HasColumnName("UserId")
            .IsRequired(false)
            .HasColumnType("INT");
        
        
        // Relation
        // Lot -> Region
        builder.HasOne(x => x.Region)
            .WithMany(x => x.Lots)
            .IsRequired()
            .HasConstraintName("FK_Lot_Region");
    }
    
}