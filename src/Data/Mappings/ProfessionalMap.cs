using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromoreApi.Entities;

namespace PromoreApi.Data.Mappings;

public class ProfessionalMap : IEntityTypeConfiguration<Professional>
{
    public void Configure(EntityTypeBuilder<Professional> builder)
    {
        //// Table
        builder.ToTable("Professional");

        
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
        
        builder.Property(x => x.Cpf)
            .IsRequired()
            .HasColumnName("Cpf")
            .HasColumnType("VARCHAR")
            .HasMaxLength(11);
        
        builder.Property(x => x.Profession)
            .HasColumnName("Profession")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);


        //// Relation
        // Professional -> User
        builder.HasOne(x => x.User);
    }
}