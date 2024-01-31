using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromoreApi.Entities;

namespace PromoreApi.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        
        //// Table
        builder.ToTable("User");

        
        //// Primary Key - Identity
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();
        
        
        //// Properties
        builder.Property(x => x.Active)
            .HasColumnName("Active")
            .HasColumnType("BIT");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50)
            .HasAnnotation("EmailAddress", true);
            
        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasColumnName("PasswordHash")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);
        
        
        //// Index
        builder.HasIndex(x => x.Email, "IX_User_Email")
            .IsUnique();
        
        
        //// Relation
        // User <-> Role
        builder.HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<Dictionary<string, object>>
            (
                "UserRole",
                user => user.HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .HasConstraintName("FK_UserRole_UserId"),
                role => role.HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_UserRole_RoleId")
            );
        
        // User <-> Region
        builder.HasMany(x => x.Regions)
            .WithMany(x => x.Users)
            .UsingEntity<Dictionary<string, object>>
            (
                "UserRegion",
                user => user.HasOne<Region>()
                    .WithMany()
                    .HasForeignKey("RegionId")
                    .HasConstraintName("FK_UserRegion_UserId"),
                region => region.HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_UserRegion_RegionId")
            );
        
        // User <- Professional
        
    }
}