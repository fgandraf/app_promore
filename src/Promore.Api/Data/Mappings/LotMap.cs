using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Promore.Api.Entities;

namespace Promore.Api.Data.Mappings;

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
            .HasColumnName("Status")
            .HasColumnType("INT");

        builder.Property(x => x.Comments)
            .HasColumnName("Comments")
            .HasColumnType("TEXT");


        //// Relation
        // Lot -> User
        builder.HasOne(x => x.User)
            .WithMany(x => x.Lots)
            .IsRequired()
            .HasConstraintName("FK_Lot_User");

        // Lot -> Region
        builder.HasOne(x => x.Region)
            .WithMany(x => x.Lots)
            .IsRequired()
            .HasConstraintName("FK_Lot_Region");
        
        // Lot <- Clients
    }
}