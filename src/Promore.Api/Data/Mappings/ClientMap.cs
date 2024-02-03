using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Promore.Api.Entities;

namespace Promore.Api.Data.Mappings;

public class ClientMap : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        //// Table
        builder.ToTable("Client");

        
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
        
        builder.Property(x => x.Phone)
            .HasColumnName("Phone")
            .HasColumnType("VARCHAR")
            .HasMaxLength(11);
        
        builder.Property(x => x.MothersName)
            .IsRequired()
            .HasColumnName("MothersName")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);
        
        builder.Property(x => x.BirthdayDate)
            .HasColumnName("BirthdayDate")
            .HasColumnType("DATE");


        //// Relation
        // Client -> Lot
        builder.HasOne(x => x.Lot)
            .WithMany(x => x.Clients)
            .HasConstraintName("FK_Lot_Client");
    }
}